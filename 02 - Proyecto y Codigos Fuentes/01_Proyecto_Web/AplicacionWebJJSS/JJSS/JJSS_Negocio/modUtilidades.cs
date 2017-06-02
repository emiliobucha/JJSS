using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data;

namespace JJSS_Negocio
{        /*
         * Modulo estatico que con diferentes herramientas utiles
         */
    public static class modUtilidades
    {
        /*
         * Metodo que devuelve si una conexion al EntityFramwork es exitoso o no
         */
        public static bool TestConnectionEF()
        {
            using (var db = new JJSSEntities())
            {
                try
                {
                    db.Database.Connection.Open();
                    if (db.Database.Connection.State == ConnectionState.Open)
                    {
                        db.Database.Connection.Close();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
