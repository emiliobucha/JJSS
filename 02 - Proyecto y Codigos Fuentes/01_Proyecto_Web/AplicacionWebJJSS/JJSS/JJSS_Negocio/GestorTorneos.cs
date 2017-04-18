﻿using System;
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
        public String GenerarNuevoTorneo(DateTime pFecha, String pNombre, Decimal pPrecio_categoria, Decimal pPrecio_absoluto, String pHora, int pSede, DateTime pFecha_cierre, string pHora_cierre)
        {
            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    torneo nuevoTorneo = new torneo()
                    {
                        fecha = pFecha,
                        fecha_cierre = pFecha_cierre,
                        nombre = pNombre,
                        precio_categoria = pPrecio_categoria,
                        precio_absoluto = pPrecio_absoluto,
                        hora = pHora,
                        hora_cierre = pHora_cierre,
                        id_sede = pSede
                    };

                    db.torneo.Add(nuevoTorneo);

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

        public List<torneo> ObtenerTorneos()
        {
            
            using (var db = new JJSSEntities())
            {
                var torneosDisponibles =
                    from torneo in db.torneo
                    where torneo.estado.nombre == "DISPONIBLE"
                    select torneo;
                return torneosDisponibles.ToList();
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
