using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;


namespace JJSS_Negocio
{
    public class GestorSedes
    {
        /*Ya con este codigo te genera el torneo y lo guarda en la base de datos y todo*/
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

        public sede BuscarSedePorID(int pID)
        {
            sede sedeEncontrada = null;
            using (var db = new JJSSEntities()) {
                sedeEncontrada= db.sede.Find(pID);
            }
            return sedeEncontrada;
        }

        public List<torneo> ObtenerTorneoEnSede(int pID)
        {
            sede sedeEncontrada = null;
            using (var db = new JJSSEntities())
            {
                sedeEncontrada = db.sede.Find(pID);
            }
            return sedeEncontrada.torneo.ToList();
        }

        public List<torneo> ObtenerTorneos()
        {
            
            using (var db = new JJSSEntities())
            {
                var torneosAbiertos =
                    from torneo in db.torneo
                    where torneo.estado.nombre == "InscripcionAbierta"
                    select torneo;
                return torneosAbiertos.ToList();
            }
        }

        public List<sede> ObtenerSedes()
        {
            using (var db = new JJSSEntities())
            {
                return db.sede.ToList();
            }
        }
    }
}
