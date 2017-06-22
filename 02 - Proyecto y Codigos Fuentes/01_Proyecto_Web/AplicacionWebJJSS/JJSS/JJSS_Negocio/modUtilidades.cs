using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data;
using System.Security.Cryptography;

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

        /*
         * Método que nos permite encriptar con MD5 Hash un string
         */

        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }

        }
    }
}
