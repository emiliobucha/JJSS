using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    public class GestorUsuarios
    {

        /*
         * Metodo que permite cambiar al clave de un usuario
         * Parametros:  pClave : string clave antigua
         *              pNueva : string clave nueva
         *              pLogin : string nombre de usuario
         * Retornos: string
         *          "": transaccion correcta
         *          Usuario y/o clave anteriores incorrectas
         *          ex.Message : error en la BD
         * 
         */
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
         * Metodo que permite generar un usuario
         * Parametros:  pClave : string clave del usuario
         *              pGrupo : grupo de usuario al que va a pertenecer
         *              pLogin : string nombre de usuario
         *              pMail : mail del usuario
         *              pNombre : nombre y apellido del usuario
         * Retornos: string
         *          "": transaccion correcta
         *          ex.Message : error en la BD
         * 
         * 
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
                            mail = pMail,
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
                        sReturn = nuevoUsuario.id_usuario.ToString();
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

        public string GenerarLogin(string pNombre, string pApellido)
        {
            string login = pNombre.Substring(0, 1).ToLower();
            login += pApellido.ToLower();
            login = login.Replace(" ", "");
            using (var db = new JJSSEntities())
            {
                var usuario = from usu in db.seguridad_usuario
                              where usu.login.StartsWith(login)
                              select usu;
                if (usuario.Count() != 0)
                {
                    int num = usuario.Count() + 1;
                    return GenerarLogin(pNombre, pApellido + num);
                }
                else
                {
                    return login;
                }
            }
        }

        public List<seguridad_usuario> obtenerListaUsuarios()
        {
            using (var db = new JJSSEntities())
            {
                var usuarios = from usu in db.seguridad_usuario
                               where usu.baja_logica == 1
                               select usu;
                return usuarios.ToList();
            }
        }

        public DataTable obtenerTablaUsuarios()
        {
            using (var db = new JJSSEntities())
            {
                var usuarios = from usu in db.seguridad_usuario
                               join uxg in db.seguridad_usuarioxgrupo on usu.id_usuario equals uxg.id_usuario
                               join gru in db.seguridad_grupo on uxg.id_grupo equals gru.id_grupo

                               where usu.baja_logica == 1
                               select new
                               {
                                   id_usuario = usu.id_usuario,
                                   nombre = usu.nombre,
                                   mail = usu.mail,
                                   login = usu.login,
                                   grupo_nombre = gru.nombre

                               };

                var tabla = usuarios.ToList().ToDataTable();
                DataTable tablaFinal = new DataTable();
                tablaFinal.Columns.Add("id_usuario",Type.GetType("System.Int32"));
                tablaFinal.Columns.Add("nombre", Type.GetType("System.String"));
                tablaFinal.Columns.Add("mail", Type.GetType("System.String"));
                tablaFinal.Columns.Add("login", Type.GetType("System.String"));
                tablaFinal.Columns.Add("grupo_nombre", Type.GetType("System.String"));
                foreach (DataRow grupo in tabla.Rows)
                {
                    DataRow[] viejas = tablaFinal.Select("id_usuario = " + grupo["id_usuario"]);
                    if (viejas.Length == 0)
                    {
                        DataRow nueva = tablaFinal.NewRow();
                        nueva["id_usuario"] = grupo["id_usuario"];
                        nueva["nombre"] = grupo["nombre"];
                        nueva["mail"] = grupo["mail"];
                        nueva["login"] = grupo["login"];
                        nueva["grupo_nombre"] = grupo["grupo_nombre"];
                        tablaFinal.Rows.Add(nueva);

                    }
                    else
                    {
                        viejas[0]["grupo_nombre"] = viejas[0]["grupo_nombre"] + " - " +  grupo["grupo_nombre"];
                    }
                }


                return tablaFinal;
            }
        }

        /*
         *  busca todas los profesores y alumnos inscriptos
         */
        public DataTable BuscarProfesAlumnos()
        {
            using (var db = new JJSSEntities())
            {
                var alumnos = from usu in db.seguridad_usuario
                               join alu in db.alumno on usu.id_usuario equals alu.id_usuario
                               where alu.baja_logica == 1
                               select new
                               {
                                   nombre = alu.nombre,
                                   apellido = alu.apellido,
                                   dni = alu.dni,
                                   id=usu.id_usuario,
                               }; 

                var profesores = from usu in db.seguridad_usuario
                              join pro in db.profesor on usu.id_usuario equals pro.id_usuario
                              select new
                              {
                                  nombre = pro.nombre,
                                  apellido = pro.apellido,
                                  dni = pro.dni,
                                  id=usu.id_usuario
                              };

                DataTable dt = modUtilidadesTablas.unirDataTable(modUtilidadesTablas.ToDataTable(alumnos.ToList()), modUtilidadesTablas.ToDataTable(profesores.ToList()));

                return dt;
            }
        }
    }
}
