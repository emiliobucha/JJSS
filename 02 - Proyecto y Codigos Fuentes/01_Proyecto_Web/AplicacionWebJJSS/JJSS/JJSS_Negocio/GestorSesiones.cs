﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JJSS_Entidad;

namespace JJSS_Negocio
{

    public class GestorSesiones
    {
        private Sesion xSesion
        {
            get
            {
                HttpContext.Current.Session.Timeout = 20;
                if (HttpContext.Current == null)
                {
                    return xSesion;
                }
                else
                {
                    return (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                }
            }
            set
            {
                HttpContext.Current.Session.Timeout = 20;
                if (HttpContext.Current == null)
                {
                    xSesion = value;
                }
                else
                {
                    HttpContext.Current.Session["SEGURIDAD_SESION"] = value;
                }
            }
        }


        /*
         * Metodo que permite iniciar una sesion
         * Parametros:  pClave : string clave del usuario
         *              pLogin : string nombre de usuario
         * Retornos: Sesion : sesion activa
         * 
         */
        public Sesion IniciarSesion(string pLogin, string pClave)
        {
            Sesion sesionAux = new Sesion();
            try
            {
                string claveMD5 = modUtilidades.GetMd5Hash(pClave);
                using (var db = new JJSSEntities())
                {
                    var sesionUsuario = from usu in db.seguridad_usuario
                                        where usu.clave == claveMD5 &&
                                              usu.login == pLogin
                                        select usu;

                    if (sesionUsuario.ToList().Count == 1)
                    {
                        if (sesionUsuario.First().baja_logica == 0) throw new Exception("Usuario inhabilitado");
                        sesionAux.usuario = sesionUsuario.ToList()[0];
                        var permisosUsuario = (from x in
                                              (from op in db.seguridad_opcion
                                               join gop in db.seguridad_grupoxopcion on op.clave_opcion equals gop.clave_opcion
                                               join gus in db.seguridad_usuarioxgrupo on gop.id_grupo equals gus.id_grupo
                                               where gus.id_usuario == sesionAux.usuario.id_usuario
                                               select new
                                               {
                                                   perm_clave = op.clave_opcion,
                                                   perm_nombre = op.nombre,
                                                   perm_ejecutar = gop.ejecutar,
                                                   perm_ver = gop.ver,
                                                   perm_modificar = gop.modificar,
                                                   perm_agregar = gop.agregar,
                                                   perm_eliminar = gop.eliminar

                                               })
                                               group x by new
                                               {
                                                   x.perm_clave,
                                                   x.perm_nombre
                                               } into g
                                               select new
                                               {
                                                   perm_clave = g.Key.perm_clave,
                                                   perm_nombre = g.Key.perm_nombre,
                                                   perm_ejecutar = g.Max(x => x.perm_ejecutar),
                                                   perm_ver = g.Max(x => x.perm_ver),
                                                   perm_modificar = g.Max(x => x.perm_modificar),
                                                   perm_agregar = g.Max(x => x.perm_agregar),
                                                   perm_eliminar = g.Max(x => x.perm_eliminar)
                                               }
                                               );


                        try
                        {
                            var Lista = permisosUsuario.ToList();

                            sesionAux.permisos = permisosUsuario.ToList().ToDataTable();
                        }
                        catch (Exception ex)
                        {
                            sesionAux.permisos = null;
                        }
                        sesionAux.estado = "INGRESO ACEPTADO";
                        xSesion = sesionAux;
                    }
                    else
                    {
                        sesionAux.estado = "INGRESO RECHAZADO - USUARIO O CLAVE INCORRECTOS";
                    }

                    HttpContext.Current.Session.Timeout = 20;

                }
            }
            catch (Exception ex)
            {
                sesionAux.estado = "INGRESO RECHAZADO - USUARIO O CLAVE INCORRECTOS";
            }

            return sesionAux;
        }


        /*
         * Metodo que permite cerrar una sesion
         * 
         */
        public void CerrarSesion()
        {
            xSesion = new Sesion();
        }


        /*
         * Método que permite ingresar con una sesión invitado
         * 
         *
         */
        public Sesion IniciarInvitado()
        {
            Sesion sesionAux = new Sesion();
            try
            {

                sesionAux.estado = "INGRESO ACEPTADO";
                sesionAux.usuario.nombre = "INVITADO";
                sesionAux.usuario.login = "INVITADO";
                
                xSesion = sesionAux;

                HttpContext.Current.Session.Timeout = 20;


            }
            catch (Exception ex)
            {
                sesionAux.estado = "INGRESO RECHAZADO - USUARIO O CLAVE INCORRECTOS";
            }

            return sesionAux;
        }


        /*
         * Metodo que permite obtener la sesion actual
         * Retornos: xSesion : sesion activa
         * 
         */
        public Sesion getActual()
        {
            return xSesion ?? (xSesion = new Sesion());
        }


        /*
         * Metodo que permite cambiar la sesion actual
         * Parametros: pSeg : nueva sesion
         * 
         */
        public void setActual(Sesion pSeg)
        {
            xSesion = pSeg;
        }


    }
}
