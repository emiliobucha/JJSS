using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;
using System.Data;
using System.Configuration;

namespace JJSS_Negocio
{

    public class GestorTorneos
    {
        private const int InscripcionAbierta = 1;


        /*Ya con este codigo te genera el torneo y lo guarda en la base de datos y todo*/
        public String GenerarNuevoTorneo(DateTime pFecha, String pNombre, Decimal pPrecio_categoria, Decimal pPrecio_absoluto, String pHora, int pSede, DateTime pFecha_cierre, string pHora_cierre)
        {
            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                estado estadoTorneo = db.estado.Find(InscripcionAbierta);
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
                        id_sede = pSede, 
                        estado = estadoTorneo
                    };

                    db.torneo.Add(nuevoTorneo);

                    //+Acá puede ser asincrono, asi que puede quedar guardandose y seguir ejecutandose lo demas /

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
                encontradoTorneo = db.torneo.Find(pID);
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
                var torneosAbiertos =
                    from torneo in db.torneo
                    where torneo.estado.nombre == "InscripcionAbierta"
                    select torneo;
                return torneosAbiertos.ToList();
            }
        }

        public List<sede> ObtenerSedes()
        {
            GestorSedes gestorSede = new GestorSedes();
            return gestorSede.ObtenerSedes();
        }

        public String EliminarTorneoPorID(int pID) {
            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    db.torneo.Remove(db.torneo.Find(pID));

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

        public List<participante> ObtenerParticipantesATorneo(int pID) {
            List<inscripcion> inscripcionesTorneo = ObtenerInscripcionesATorneo(pID);
            List<participante> participantes = new List<participante>();

            foreach (var auxInscripcion in inscripcionesTorneo)
            {
                    participantes.Add(auxInscripcion.participante);
            }

            return participantes;
        }

        public List<Object> ListadoParticipantes(int pID)
        {
            using (var db = new JJSSEntities())
            {
                var participantes = from inscr in db.inscripcion
                                    join part in db.participante on inscr.id_participante equals part.id_participante
                                    join cat_tor in db.categoria_torneo on inscr.id_categoria_torneo equals cat_tor.id_categoria_torneo
                                    where inscr.id_torneo == pID
                                    select new 
                                    {
                                        par_nombre = part.nombre,
                                        par_apellido = part.apellido,
                                        par_fecha_nac = part.fecha_nacimiento,
                                        par_sexo = part.sexo,
                                        par_peso = part.peso,
                                        par_academia = part.academia.nombre,
                                        par_faja = cat_tor.faja.color,
                                        par_categoria = cat_tor.categoria.nombre,
                                        
                                    };
                
                return  participantes.ToList<Object>();
            }
        }

        public string GenerarDuelos(torneo pTorneo)
        {
            //+Lógica que usamos?, random entre la cantidad de participantes?

            //+1. Generamos la cantidad de duelos 
            //
            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    lucha nuevaLucha = new lucha() {
                        torneo = pTorneo,
                    };
                    db.lucha.Add(nuevaLucha);

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

        public String GenararListado(int pID)
        {
            GestorReportes gestorReportes = new GestorReportes();

            return gestorReportes.GenerarReporteListadoParticipantes(ListadoParticipantes(pID));


        }
    }
}
