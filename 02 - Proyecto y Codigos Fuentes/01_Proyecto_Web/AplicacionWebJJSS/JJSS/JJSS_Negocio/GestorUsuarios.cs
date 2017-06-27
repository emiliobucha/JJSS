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
            catch
            {

            }
            return sReturn;
        }
    }
}
