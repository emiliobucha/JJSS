using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;


namespace JJSS_Negocio
{
    public class GestorTorneos
    {
        /*Ya con este codigo te genera el torneo y lo guarda en la base de datos y todo*/
        public void GenerarNuevoTorneo(DateTime pFecha, String pNombre, float pPrecio, String pHora, int pSede)
        {
            torneo nuevoTorneo = new torneo()
            {
                fecha = pFecha,
                nombre = pNombre,
                precio = pPrecio,
                hora = pHora,
                id_sede = pSede
            };


            using (var db = new JJSSEntities()) {
                db.torneo.Add(nuevoTorneo);

                /*Acá puede ser asincrono, asi que puede quedar guardandose y seguir ejecutandose lo demas */

                db.SaveChanges();
            }

        }

        public torneo BuscarTorneoPorID(int pID)
        {
            torneo encontradoTorneo = null;
            using (var db = new JJSSEntities()) {
                encontradoTorneo= db.torneo.Find(pID);
            }
            return encontradoTorneo;
        }

        public List<inscripcion> ObtenerInscripcionesATorneo(int pID)
        {
            torneo encontradoTorneo = null;
            using (var db = new JJSSEntities())
            {
                encontradoTorneo = db.torneo.Find(pID);
            }
            return encontradoTorneo.inscripcion.ToList();
        }



    }
}
