using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados
{
    public class ProductosResultados
    {

        private byte[] _imagenB;
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string categoria { get; set; }
        public int? stock { get; set; }
        public decimal? precio { get; set; }
        
        public Image imagenI { get; set; }
        public string imagen { get; set; }
        

        public byte[] imagenB
        {
            get => _imagenB;
            set
            {
                _imagenB = value;
                
                
                if (_imagenB == null || _imagenB.GetUpperBound(0)<1)
                {
                    //imagen = "../img/noimage.jpg";
                }
                else
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
            }
        }
    }
}
