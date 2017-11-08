using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using JJSS_Negocio.Resultados;

namespace JJSS_Negocio
{
    public class GestorEventos
    {
        private const int InscripcionAbierta = 1;
        private const int InscripcionCerrada = 2;
        private const int EnCurso = 3;
        private const int Finalizado = 4;

        /*
         * Generar Nuevo torneo nos permite crear un nuevo torneo 
         * Parametros: 
         *              pFecha : DateTime que nos indica en que fecha se va a realizar el torneo
         *              pNombre: String indica el nombre del torneo
         *              pPrecio_categoria: Decimal precio segun la categoria
         *              pPrecio_absoluto: Decimal precio de la inscripcion al torneo
         *              pHora: String Hora en la cual se va a realizar el torneo
         *              pSede: Entero Id de la sede en el cual se va a realizar
         *              pFecha_cierre: DateTime que indica la fecha de cierre de inscripciones
         *              pHora_cierre: String que indica la hora de cierre de inscripciones
         * Retorno: String 
         *          El torneo ya ha sido creado
         *          "" Resultado exitoso de la transacciones
         *          ex.message Resultado erroneo indicando el mensaje de la excepcion
         */

        public String GenerarNuevoEvento(DateTime pFecha, String pNombre, Decimal pPrecio, String pHora, int pSede, DateTime pFecha_cierre, string pHora_cierre, byte[] pImagen, int pTipo)
        {
            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                estado estado = db.estado.Find(InscripcionAbierta);
                var transaction = db.Database.BeginTransaction();
                try
                {
                    evento_especial eventoEncontrado = BuscarEventoPorNombreFecha(pNombre, pFecha);
                    if (eventoEncontrado != null)
                    {
                        sReturn = "El evento ya ha sido creado";
                        return sReturn;
                    }

                    evento_especial nuevoEvento = new evento_especial()
                    {
                        fecha = pFecha,
                        fecha_cierre = pFecha_cierre,
                        nombre = pNombre,
                        precio=pPrecio,
                        hora = pHora,
                        hora_cierre = pHora_cierre,
                        id_sede = pSede,
                        estado = estado,
                        id_tipo_evento=pTipo
                    };
                    db.evento_especial.Add(nuevoEvento);
                    db.SaveChanges();

                    evento_especial_imagen nuevoEventoImagen = new evento_especial_imagen()
                    {
                        id_evento = nuevoEvento.id_evento,
                        imagen = pImagen
                    };

                    db.evento_especial_imagen.Add(nuevoEventoImagen);
                    db.SaveChanges();
                    //+Acá puede ser asincrono, asi que puede quedar guardandose y seguir ejecutandose lo demas /


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
         * Busca un torneo a partir de su nombre y fecha
         * Parametros: 
         *              pFecha : DateTime que nos indica en que fecha se va a realizar el torneo
         *              pNombre: String indica el nombre del torneo
         * Retorno: torneo 
         *          torneo encontrado, si no existe devuelve null
         */
        public evento_especial BuscarEventoPorNombreFecha(string pNombre, DateTime pFecha)
        {

            using (var db = new JJSSEntities())
            {
                var encontradoEvento = from ev in db.evento_especial
                                       where ev.nombre == pNombre && ev.fecha == pFecha
                                       select ev;
                return encontradoEvento.FirstOrDefault();
            }
        }
        /*
         * Obtener todas las sedes de los disponibles para que se realicen los torneos
         */
        public List<sede> ObtenerSedes()
        {
            GestorSedes gestorSede = new GestorSedes();
            return gestorSede.ObtenerSedes();
        }

        /*
         * Devuelve una lista con los tipos de eventos
         */
        public List<tipo_evento_especial> ObtenerTipos()
        {
            using (var db = new JJSSEntities())
            {
                return db.tipo_evento_especial.ToList();
            }
        }

        /*
         * Metodo que devuelve una lista de todos los eventos con inscripciones abiertas
         */
        public List<evento_especial> ObtenerEventos()
        {
            using (var db = new JJSSEntities())
            {
                var eventosAbiertos =
                    from evento in db.evento_especial
                    where evento.id_estado == InscripcionAbierta
                    select evento;
                return eventosAbiertos.ToList();
            }
        }


        /*
         * Buscamos un evento en particular por su ID
         */
        public evento_especial BuscarEventoPorID(int pID)
        {
            evento_especial encontrado = null;
            using (var db = new JJSSEntities())
            {
                encontrado = db.evento_especial.Find(pID);
            }
            return encontrado;
        }

        public List<EventoResultado> ObtenerEventosConImagen()
        {
            cambiarEstadoEventos();
            using (var db = new JJSSEntities())
            {
                var eventosAbiertos =
                                           from t in db.evento_especial
                                           join i in db.evento_especial_imagen on t.id_evento equals i.id_evento
                                           into ps
                                           from i in ps.DefaultIfEmpty()
                                           where t.id_estado == 1
                                           select new EventoResultado
                                           {
                                               id_evento = t.id_evento,
                                               nombre = t.nombre,
                                               fecha = t.fecha,
                                               hora = t.hora,
                                               imagenB = i.imagen
                                           };


                List<EventoResultado> lista = eventosAbiertos.ToList<EventoResultado>();
                return lista;
            }
        }


        /*
         * Actualiza el estado de los eventos segun la fecha actual
         */
        public void cambiarEstadoEventos()
        {
            using (var db = new JJSSEntities())
            {
                // del estado 1 al 2
                List<evento_especial> eventosAbiertos =
                        (from ev in db.evento_especial
                         where ev.id_estado == InscripcionAbierta
                         select ev).ToList<evento_especial>();

                foreach (evento_especial x in eventosAbiertos)
                {
                    DateTime cierre = (DateTime)x.fecha_cierre;
                    if (cierre.Date < DateTime.Now.Date) x.id_estado = InscripcionCerrada;
                    else if (cierre.Date == DateTime.Now.Date && x.hora_cierre.CompareTo(DateTime.Now.ToShortTimeString()) < 0) x.id_estado = InscripcionCerrada;
                    db.SaveChanges();
                }

                //del estado 2 al 3
                List<evento_especial> eventosCerrados =
                        (from ev in db.evento_especial
                         where ev.id_estado == InscripcionCerrada
                         select ev).ToList<evento_especial>();

                foreach (evento_especial x in eventosCerrados)
                {
                    DateTime fecha = (DateTime)x.fecha;
                    if (fecha.Date < DateTime.Now.Date) x.id_estado = EnCurso;
                    else if (fecha.Date == DateTime.Now.Date && x.hora.CompareTo(DateTime.Now.ToShortTimeString()) < 0) x.id_estado = EnCurso;
                    db.SaveChanges();
                }

                //del estado 3 al 4
                List<evento_especial> eventoEnCurso =
                        (from ev in db.evento_especial
                         where ev.id_estado == EnCurso
                         select ev).ToList<evento_especial>();

                foreach (evento_especial x in eventoEnCurso)
                {
                    DateTime fecha = (DateTime)x.fecha;
                    if (fecha.Date < DateTime.Now.Date) x.id_estado = Finalizado;
                    db.SaveChanges();
                }

            }
        }
        /*
 * Obtenemos un listado de todos los participantes que estan en un torneo con datos de su categoria a la cual esta inscripto, la faja, y datos
 * propios del participante
 */
        public List<ParticipantesEventoResultado> ListadoParticipantes(int pID)
        {
            using (var db = new JJSSEntities())
            {
                var participantes = from inscr in db.inscripcion_evento
                                    join part in db.participante_evento on inscr.id_participante equals part.id_participante
                                    where inscr.id_evento == pID
                                    select new ParticipantesEventoResultado()
                                    {
                                        ev_nombre = inscr.evento_especial.nombre,
                                        ev_sede = inscr.evento_especial.sede.nombre,
                                        ev_direccion = inscr.evento_especial.sede.direccion.calle + " " + inscr.evento_especial.sede.direccion.numero + " - " + inscr.evento_especial.sede.direccion.ciudad.nombre + " - " + inscr.evento_especial.sede.direccion.ciudad.provincia.nombre + " - " + inscr.evento_especial.sede.direccion.ciudad.provincia.pais.nombre,
                                        ev_fechaD = inscr.evento_especial.fecha,
                                        ev_hora = inscr.evento_especial.hora,
                                        ev_tipo = inscr.evento_especial.tipo_evento_especial.nombre,
                                        par_nombre = part.nombre,
                                        par_apellido = part.apellido,
                                        par_fecha_nacD = part.fecha_nacimiento,
                                        par_sexo = part.sexo,
                                        par_dni = part.dni.ToString()
                                      
                                    };
                List<ParticipantesEventoResultado> participantesList = participantes.ToList<ParticipantesEventoResultado>();


                foreach (ParticipantesEventoResultado part in participantesList)
                {
                    if (part.par_sexo == 1)
                    {
                        part.par_sexo_nombre = "M";

                    }
                    else
                    {
                        part.par_sexo_nombre = "F";
                    }
                    part.par_fecha_nac = part.par_fecha_nacD?.ToString("dd/MM/yyyy") ?? " - ";
                    part.ev_fecha = part.ev_fechaD?.ToString("dd/MM/yyyy") ?? " - ";

                }


                return participantesList;
            }
        }



        /*
         * Aun no aplica
         */

        /*
         * Aun no aplica
         */


        /*
         * Genera listado de participantes a un torneo con su reporte.
         * Retorno: String del archivo resultante de la generacion del reporte en PDF
         */
        public String GenerarListado(int pID)
        {
            GestorReportes gestorReportes = new GestorReportes();
          
            List<ParticipantesEventoResultado> listado = ListadoParticipantes(pID);

            if (listado.Count == 0)
                throw new Exception("No posee inscriptos el torneo seleccionado");

            return gestorReportes.GenerarReporteListadoParticipantesEvento(listado);


        }

        public List<evento_especial> ObtenerEventosAbiertosCerrados()
        {

            using (var db = new JJSSEntities())
            {
                var eventosAbiertos =
                    from evento in db.evento_especial
                    where evento.id_estado == 1 || evento.id_estado == 2
                    select evento;
                return eventosAbiertos.ToList();
            }
        }
    }



}
