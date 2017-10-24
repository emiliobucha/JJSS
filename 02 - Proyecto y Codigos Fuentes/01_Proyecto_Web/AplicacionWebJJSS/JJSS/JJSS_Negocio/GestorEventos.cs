using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    public class GestorEventos
    {
        private const int InscripcionAbierta = 1;
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
    }
}
