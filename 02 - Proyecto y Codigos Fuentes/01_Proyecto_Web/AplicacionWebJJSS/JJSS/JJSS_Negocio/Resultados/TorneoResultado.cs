using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Caching;

namespace JJSS_Negocio.Resultados
{
    public class TorneoResultado
    {
        private byte[] _imagenB;
        public int id_torneo { get; set; }
        public string nombre { get; set; }
        public DateTime? fecha { get; set; }
        public string hora { get; set; }
        public string estado { get; set; }

        public Image imagenI { get; set; }
        public string imagen { get; set; }

        public byte[] imagenB
        {
            get => _imagenB;
            set
            {
                _imagenB = value;
                if (imagenB != null)
                {
                    using (MemoryStream ms = new MemoryStream(imagenB))
                    {
                        try
                        {

                            string sDir = System.Web.HttpContext.Current.Server.MapPath("//temp");

                            if (!System.IO.Directory.Exists(sDir))
                            {
                                System.IO.Directory.CreateDirectory(sDir);
                            }


                            var archivo = nombre.Replace(" ", "") + ".jpeg";
                            imagenI = Image.FromStream(ms);

                            char[] sTrim = "\\".ToCharArray();
                            var archivoGuardar = sDir.Trim(sTrim) + "\\" + archivo;

                            //if (!File.Exists(archivo))
                            //{
                            imagenI.Save(archivoGuardar, ImageFormat.Jpeg);
                            //}


                            imagen = "\\temp\\" + archivo;

                        }
                        catch (Exception ex)
                        {
                        }

                    }
                }
                else
                {
                    //TODO imagen por defecto
                }
            }
        }



    }
}
