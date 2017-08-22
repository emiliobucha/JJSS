﻿using System;
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
    /*
     * Clase que nos permite gestionar Torneos
     */
    public class GestorTorneos
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

        public String GenerarNuevoTorneo(DateTime pFecha, String pNombre, Decimal pPrecio_categoria, Decimal pPrecio_absoluto, String pHora, int pSede, DateTime pFecha_cierre, string pHora_cierre, byte[] pImagen)
        {
            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                estado estadoTorneo = db.estado.Find(InscripcionAbierta);
                var transaction = db.Database.BeginTransaction();
                try
                {
                    torneo torneoEncontrado= BuscarTorneoPorNombreFecha(pNombre, pFecha);
                    if (torneoEncontrado != null)
                    {
                        sReturn = "El torneo ya ha sido creado";
                        return sReturn;
                    }

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
                    db.SaveChanges();

                    torneo_imagen nuevoTorneoImagen = new torneo_imagen()
                    {
                        id_torneo = nuevoTorneo.id_torneo,
                        imagen = pImagen
                    };

                    db.torneo_imagen.Add(nuevoTorneoImagen);
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
        public torneo BuscarTorneoPorNombreFecha(string pNombre,DateTime pFecha)
        {
            
            using (var db=new JJSSEntities())
            {
                var encontradoTorneo = from torneo in db.torneo
                                       where torneo.nombre == pNombre && torneo.fecha == pFecha
                                       select torneo;
                return encontradoTorneo.FirstOrDefault();
            }
        }

        /*
         * Buscamos un torneo en particular por su ID
         */
        public torneo BuscarTorneoPorID(int pID)
        {
            torneo encontradoTorneo = null;
            using (var db = new JJSSEntities()) {
                encontradoTorneo = db.torneo.Find(pID);
            }
            return encontradoTorneo;
        }

        /*
         * Obtenemos todas las inscripciones que estan un particular torneo
         * 
         */
        public List<inscripcion> ObtenerInscripcionesATorneo(int pID)
        {
            torneo encontradoTorneo = null;
            using (var db = new JJSSEntities())
            {
                encontradoTorneo = db.torneo.Find(pID);
            }
            return encontradoTorneo.inscripcion.ToList();
        }

        /*
         * Lista de todos los torneos que están disponibles a inscribirse
         */
        public List<torneo> ObtenerTorneos()
        {

            using (var db = new JJSSEntities())
            {
                var torneosAbiertos =
                    from torneo in db.torneo
                    where torneo.id_estado == 1
                    select torneo;
                return torneosAbiertos.ToList();
            }
        }


        public List<torneo> ObtenerTorneosAbiertosCerrados()
        {

            using (var db = new JJSSEntities())
            {
                var torneosAbiertos =
                    from torneo in db.torneo
                    where torneo.id_estado == 1 || torneo.id_estado == 2
                    select torneo;
                return torneosAbiertos.ToList();
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
         * Eliminar un torneo indicando el ID
         */
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

        /*
         * Obtenemos un listado de los paticipantes a un torneo 
         */
        public List<participante> ObtenerParticipantesATorneo(int pID) {
            List<inscripcion> inscripcionesTorneo = ObtenerInscripcionesATorneo(pID);
            List<participante> participantes = new List<participante>();

            foreach (var auxInscripcion in inscripcionesTorneo)
            {
                    participantes.Add(auxInscripcion.participante);
            }

            return participantes;
        }

        /*
         * Obtenemos un listado de todos los participantes que estan en un torneo con datos de su categoria a la cual esta inscripto, la faja, y datos
         * propios del participante
         */
        public List<Object> ListadoParticipantes(int pID)
        {
            using (var db = new JJSSEntities())
            {
                var participantes = from inscr in db.inscripcion
                                    join part in db.participante on inscr.id_participante equals part.id_participante
                                    join cat_tor in db.categoria on inscr.id_categoria equals cat_tor.id_categoria
                                    where inscr.id_torneo == pID
                                    select new
                                    {
                                        tor_nombre = inscr.torneo.nombre,
                                        tor_sede = inscr.torneo.sede.nombre,
                                        tor_direccion = inscr.torneo.sede.direccion.calle + " " + inscr.torneo.sede.direccion.numero,
                                        tor_fecha = inscr.torneo.fecha,
                                        tor_hora = inscr.torneo.hora,
                                        par_nombre = part.nombre,
                                        par_apellido = part.apellido,
                                        par_fecha_nac = part.fecha_nacimiento,
                                        par_sexo = part.sexo,
                                        par_faja= inscr.faja.descripcion,
                                        par_categoria=inscr.categoria.nombre,
                                    };
                List<Object> participantesList = participantes.ToList<Object>();
              
                return participantesList;
            }
        }


        private string Sexo(string pSexo)
        {
            if (pSexo == "0")
                return  "Femenino";
            else
                return "Masculino";
        }
        /*
         * Aun no aplica
         */

        /*
         * Aun no aplica
         */

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


        /*
         * Genera listado de participantes a un torneo con su reporte.
         * Retorno: String del archivo resultante de la generacion del reporte en PDF
         */
        public String GenerarListado(int pID)
        {
            GestorReportes gestorReportes = new GestorReportes();
            //torneo torneoAListar = BuscarTorneoPorID(pID);
            //GestorSedes gestorSedes = new GestorSedes();
            //gestorSedes.BuscarSedePorID(torneoAListar.id_sede);
            //gestorSedes.ObtenerDireccion(torneoAListar.id_sede);
            List<Object> listado = ListadoParticipantes(pID);

            if (listado.Count == 0)
                throw new Exception("No posee inscriptos el torneo seleccionado");

            return gestorReportes.GenerarReporteListadoParticipantes(listado);
                

        }
    }
}
