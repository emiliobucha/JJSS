using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;


namespace JJSS_Negocio
{   /*
    * Clase para gestionar sedes de los torneos
    */
    public class GestorSedes
    {
        /*Genera una nueva sede para el torneo
         * Parametros: 
         *              pNombre : String que indica el nombre de la nueva sede
         *              pDireccion: String que indica la direccion de esta nueva sede
         */
        public String GenerarNuevaSede(string pNombre, direccion pDireccion)
        {
            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    sede nuevaSede = new sede()
                    {
                        nombre = pNombre,
                        direccion = pDireccion
                    };

                    db.sede.Add(nuevaSede);

                    /*Acá puede ser asincrono, asi que puede quedar guardandose y seguir ejecutandose lo demas */

                    db.SaveChanges();
                    transaction.Commit();
                    return sReturn;
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    return ex.Message;
                }
            }

        }

        /*
         * Busca una sede por ID
         * Parametros: ID de la sede
         * Retorno: Entidad sede escogida
         */
        public sede BuscarSedePorID(int? pID)
        {
            sede sedeEncontrada = null;
            using (var db = new JJSSEntities())
            {
                sedeEncontrada = db.sede.Find(pID);
            }
            return sedeEncontrada;
        }

        /*
         * Lista todos los torneos que posee una sede
         * Parametros: ID de la sede 
         * Retorno: Lista de Torneos los cuales tienen su participacion en esa sede
         */
        public List<torneo> ObtenerTorneoEnSede(int pID)
        {
            sede sedeEncontrada = null;
            using (var db = new JJSSEntities())
            {
                sedeEncontrada = db.sede.Find(pID);
            }
            return sedeEncontrada.torneo.ToList();
        }



        /*
         * Obtenemos todas las sedes
         * Retorno: Listado total de todas las sedes
         */
        public List<sede> ObtenerSedes()
        {
            using (var db = new JJSSEntities())
            {
                return db.sede.ToList();
            }
        }


        public String ObtenerDireccion(int? pID)
        {
            String direccion = "";
            using (var db = new JJSSEntities())
            {
                sede sedeEncontrada = db.sede.Find(pID);
                // direccion = sedeEncontrada.direccion.calle + " " + sedeEncontrada.direccion.numero;
            }
            return direccion;
        }

        public Resultados.SedeDireccion ObtenerDireccionSede(int pIDSede)
        {
            using (var db = new JJSSEntities())
            {
                var sede = from s in db.sede
                           where s.id_sede == pIDSede
                           select new Resultados.SedeDireccion()
                           {
                               sede = s.nombre,
                               calle = s.direccion.calle,
                               numero = (int)s.direccion.numero,
                               ciudad = s.direccion.ciudad.nombre,
                               provincia = s.direccion.ciudad.provincia.nombre,
                               pais = s.direccion.ciudad.provincia.pais.nombre,
                           };
                return sede.FirstOrDefault();
            }
        }
    }
}
