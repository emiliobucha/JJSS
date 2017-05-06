using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio
{ /*
    *   Modulo estatico con metodos Auxliares
    */
    public static class ModReportes
    {

           /*
            * Obtiene una referencia de la ubicacion de la generacion del reporte
            */
        public static System.IO.Stream ObtenerReporte(String pReporte)
        {
            System.Reflection.Assembly oCurrentAssembly;
            oCurrentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            String name = oCurrentAssembly.GetName().Name;
            return oCurrentAssembly.GetManifestResourceStream(name + "." + pReporte + ".rdlc");

        }

        /*
         * Obtiene archivo temporal donde se va a generar el reporte dentro del servidor
         * Tambien se encarga de limpiar si este poseen 2 dias de antigüedad, para evitar sobrecargas
         */
        public static String GetTempFileName(String pTempDir, String pNombre, String pExt)
        {

            String sDir = System.Web.HttpContext.Current.Server.MapPath("//temp");

            if (!System.IO.Directory.Exists(sDir))
            {
                System.IO.Directory.CreateDirectory(sDir);
            }

            String sFile = DateTime.Now.Ticks + "_" + pNombre + "." + pExt;
            char[] sTrim = "\\".ToCharArray();
            sFile = sDir.Trim(sTrim) + "\\" + sFile;

            try
            {
                System.IO.File.Open(sFile, System.IO.FileMode.CreateNew).Close();
            }
            catch (Exception ex)
            {
                return GetTempFileName(pTempDir, pNombre, pExt);
            }

            long nViejos = DateTime.Now.AddDays(-2).Ticks;
            String[] sArchivos = System.IO.Directory.GetFiles(sDir);
            Array.Sort(sArchivos);

            String sAux;

            foreach (String sArchivo in sArchivos)
            {
                sAux = System.IO.Path.GetFileNameWithoutExtension(sArchivo);
                char[] sSplit = "_".ToCharArray();
                sAux = sAux.Split(sSplit, StringSplitOptions.None)[0];
                if (nViejos > long.Parse(sAux))
                {
                    System.IO.File.Delete(sArchivo);
                }
                else
                {
                    break;
                }



            }
            return sFile;




        }

    }

   


}

