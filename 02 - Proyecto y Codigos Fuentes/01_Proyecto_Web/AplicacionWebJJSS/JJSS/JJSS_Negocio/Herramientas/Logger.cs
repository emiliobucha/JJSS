using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JJSS_Negocio.Herramientas
{
    public class Logger
    {
        private readonly string archivo;
        private StreamWriter sw;
        public List<string> mensajes { get; set; }
       

        public Logger(string archivo)
        {
            this.archivo = "log-" + archivo;
            mensajes = new List<string>();
            const string ruta = "C:\\Users\\Public\\Logs\\";

            var info =  new FileInfo(ruta + this.archivo + ".txt");
            
            info.Refresh();
            var cont = 0;
            while (info.Exists && info.Length > 314572800)
            {
                cont++;
                if (this.archivo.Length > ("log-" + archivo).Length)
                {
                    this.archivo = this.archivo.Substring(0, this.archivo.Length - 1);
                }
                this.archivo += cont.ToString();
                info = new FileInfo(ruta + this.archivo + ".txt");
                info.Refresh();

            }
            this.archivo += ".txt";



        }

        public void AgregarMensaje(string tipo, string mensaje)
        {
            mensajes.Add("{\"TIPO\":\"" + tipo + "\",\"MENSAJE\":\"" + mensaje + "\"}"); 
        }

        public void EscribirLog()
        {
            try
            {
                sw = new StreamWriter("C:\\Users\\Public\\Logs\\" + archivo, true);
                sw.WriteLine("             . " + DateTime.Now.ToString("dd/MM HH:mm:ss"));
                foreach (var mensaje in mensajes)
                {
                   
                    sw.WriteLine(mensaje);
                }

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }
    }
}

