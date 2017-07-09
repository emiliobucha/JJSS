using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    class GestorUsuarios
    {
        public string CambiarClave(string pClave, string pNueva, string pLogin)
        {
            String sReturn = "";
            try
            {
                string claveMD5 = modUtilidades.GetMd5Hash(pClave);
                string nuevaMD5 = modUtilidades.GetMd5Hash(pNueva);
                using (var db = new JJSSEntities())
                {
                    var sesionUsuario = from usu in db.seguridad_usuario
                                        where usu.clave == claveMD5 &&
                                              usu.login == pLogin
                                        select usu;


                    if (sesionUsuario.ToList().Count == 1)
                    {
                        var transaction = db.Database.BeginTransaction();
                        seguridad_usuario usuarioCambiar = sesionUsuario.First();
                        usuarioCambiar.clave = nuevaMD5;
                        db.SaveChanges();
                        transaction.Commit();

                    }
                    else
                    {
                        sReturn = "Usuario y/o clave anteriores incorrectas";
                    }
                }
            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }
            return sReturn;
        }

        /*
         * Grupos: 
         * 1 Admin
         * 2 Profe
         * 3 Alumno
         */
        public string GenerarNuevoUsuario(string pLogin, string pClave, int pGrupo, string pMail, string pNombre)
        {
            String sReturn = "";

            try
            {
                string claveMD5 = modUtilidades.GetMd5Hash(pClave);
                using (var db = new JJSSEntities())
                {
                    var transaction = db.Database.BeginTransaction();
                    try
                    {
                        seguridad_usuario nuevoUsuario = new seguridad_usuario()
                        {
                            login = pLogin,
                            clave = claveMD5,
                            mail= pMail,
                            nombre = pNombre
                        };

                        db.seguridad_usuario.Add(nuevoUsuario);

                        db.SaveChanges();


                        seguridad_usuarioxgrupo usuarioXGrupo = new seguridad_usuarioxgrupo()
                        {
                            id_usuario = nuevoUsuario.id_usuario,
                            id_grupo = pGrupo
                        };

                        db.seguridad_usuarioxgrupo.Add(usuarioXGrupo);

                        db.SaveChanges();

                        transaction.Commit();
                        return sReturn;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        sReturn = ex.Message;
                    }

                }
            }
            catch (Exception ex)
            {
                sReturn = ex.Message;
            }
            return sReturn;
        }
    }
}
