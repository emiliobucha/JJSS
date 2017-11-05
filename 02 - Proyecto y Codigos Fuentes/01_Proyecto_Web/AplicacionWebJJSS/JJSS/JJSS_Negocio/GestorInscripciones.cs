using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;
using System.Runtime.InteropServices.ComTypes;

namespace JJSS_Negocio
{
    /*Clase que se encarga de gestionar las inscripciones a Torneos*/
    public class GestorInscripciones
    {
        private alumno alumnoExistente;

        /*Método que permite crear un objeto de Entidad de la clase Inscripción
         * Como asi tambien genera una nueva categoria si esta no estaba 
         * Y si el participante no estaba ya inscripto
         * 
         * Parametros: 
         *              pTorneo : Entero id del torneo a inscribir
         *              pNombre : String nombre del participante
         *              pApellido : String apellido del participante
         *              pPeso : Float peso del participante
         *              pFechaNacimiento : DateTime fecha de nacimiento del participante
         *              pFaja : Entero id de la faja que posee el participante
         *              pSexo : Short 0 Mujer 1 Hombre
         *              pDni : 
         *  Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              Participante ya inscripto
         *          
         * 
         */
        public string InscribirATorneo(int pTorneo, string pNombre, string pApellido, double pPeso, DateTime pFechaNacimiento, int pFaja, short pSexo, int pDni, int? pIDAlumno, short pTipoInscripcion)
        {

            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();


                try
                {
                    //Foraneos
                    torneo torneoInscripto = db.torneo.Find(pTorneo);

                    faja fajaElegida = db.faja.Find(pFaja);
                    int edad = DateTime.Today.AddTicks(-pFechaNacimiento.Ticks).Year - 1;

                    var cat =
                        from categoria in db.categoria
                        where (categoria.edad_desde <= edad)
                        && (categoria.edad_hasta >= edad)
                        && (categoria.peso_desde <= pPeso)
                        && (categoria.peso_hasta > pPeso)
                        && (categoria.sexo == pSexo)
                        select categoria;

                    categoria categoriaPerteneciente = cat.First();

                    //Nuevos


                    if (obtenerParticipanteDeTorneo(pDni, pTorneo) != null)
                    {
                        return "El participante ya se inscribió a este torneo";
                    }

                    //alumnoExistente = ObtenerAlumnoPorDNI(pDni);

                    participante nuevoParticipante;

                    nuevoParticipante = new participante()
                    {
                        nombre = pNombre,
                        apellido = pApellido,
                        //peso = pPeso,

                        sexo = pSexo,
                        fecha_nacimiento = pFechaNacimiento,
                        dni = pDni,
                        id_alumno = pIDAlumno

                    };
                    db.participante.Add(nuevoParticipante);
                    db.SaveChanges();
                    var catTorneoExistente = from catTor in db.categoria_torneo
                                             where (catTor.id_categoria == categoriaPerteneciente.id_categoria)
                                             && catTor.id_faja==pFaja
                                             select catTor;
                    categoria_torneo nuevaCategoriaTorneo;
                    //+rever esto
                    if (catTorneoExistente.Count() == 0)

                    {
                        nuevaCategoriaTorneo = new categoria_torneo()
                        {
                            id_categoria = categoriaPerteneciente.id_categoria,
                            faja = fajaElegida,

                        };
                    }
                    else
                    {
                        nuevaCategoriaTorneo = catTorneoExistente.First();
                    }
                    db.categoria_torneo.Add(nuevaCategoriaTorneo);
                    db.SaveChanges();

                    string hora = hora = DateTime.Now.ToString("hh:mm tt");
                    DateTime fecha = DateTime.Now.Date;
                    inscripcion nuevaInscripcion = new inscripcion()
                    {

                        hora = hora,
                        fecha = fecha,
                        codigo_barra = 123456789,
                        participante = nuevoParticipante,
                        id_participante = nuevoParticipante.id_participante,
                        id_torneo = torneoInscripto.id_torneo,
                        torneo = torneoInscripto,
                        peso = pPeso,
                        id_faja = pFaja,
                        faja=fajaElegida,
                        categoria_torneo=nuevaCategoriaTorneo,
                        tipo_inscripcion=pTipoInscripcion,
                    };



                    db.inscripcion.Add(nuevaInscripcion);
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



        public inscripcion obtenerInscripcionATorneoPorIdParticipantePorDni(int pDni, int pIdTorneo)
        {
            using (var db = new JJSSEntities())
            {
                inscripcion inscripcion = (from ins in db.inscripcion
                                           join part in db.participante on ins.id_participante equals part.id_participante
                                           where part.dni == pDni && ins.id_torneo == pIdTorneo
                                           select ins).FirstOrDefault();
                return inscripcion;
            }
        }





        /*
         * Metodo que nos permite bajar el acoplamiente y obtener los diferentes torneos para el momento de inscribirse
         */
        public List<torneo> ObtenerTorneos()
        {
            GestorTorneos gestorTorneos = new GestorTorneos();
            return gestorTorneos.ObtenerTorneos();
        }

        /*
         * Metodo que nos devuelve una lista de fajas para luego ser seleccionadas al momento de inscribirse
         */

        public List<faja> ObtenerFajas()
        {
            using (var db = new JJSSEntities())
            {
                return db.faja.ToList();
            }
        }

        public List<faja> ObtenerFajasPorTipoClase(int pIdTipoClase)
        {
            using (var db = new JJSSEntities())
            {
                var fajasEncontradas = from faj in db.faja
                                       where faj.id_tipo_clase == pIdTipoClase
                                       orderby faj.orden
                                       select faj;
                return fajasEncontradas.ToList();
            }
        }

        /*
         * Método que busca un alumno por DNI, permite bajar el acoplamiente delegando la tarea a su gestor correspondiente
         * Parámetros:
         *              pDni: entero que representa el dni a buscar
         */
        public alumno ObtenerAlumnoPorDNI(int pDni)
        {
            GestorAlumnos gestorAlumnos = new GestorAlumnos();

            return gestorAlumnos.ObtenerAlumnoPorDNI(pDni);
        }

        /*
         * Método que busca un participante por DNI, permite bajar el acoplamiente delegando la tarea a su gestor correspondiente
         * Parámetros:
         *              pDni: entero que representa el dni a buscar
         */

        public participante ObtenerParticipanteporDNI(int pDni)
        {
            GestorParticipantes gestorParticipantes = new GestorParticipantes();
            return gestorParticipantes.ObtenerParticipantePorDNI(pDni);
        }


        public participante obtenerParticipanteDeTorneo(int pDni, int pIDTorneo)
        {
            using (var db = new JJSSEntities())
            {
                participante participante = (from part in db.participante
                                             join ins in db.inscripcion on part.id_participante equals ins.id_participante
                                             where part.dni == pDni && ins.id_torneo == pIDTorneo
                                             select part).FirstOrDefault();
                return participante;
            }
        }


        public inscripcion obtenerInscripcionATorneoPorIdParticipante(int pId, int pIDTorneo)
        {
            using (var db = new JJSSEntities())
            {
                inscripcion inscripcion = (from ins in db.inscripcion
                                           join part in db.participante on ins.id_participante equals part.id_participante
                                           where part.id_participante == pId && ins.id_torneo == pIDTorneo
                                           select ins).FirstOrDefault();
                return inscripcion;
            }
        }

        /*
        * Obtenemos un listado de todos los participantes que estan en un torneo con datos de su categoria a la cual esta inscripto, la faja, y datos
        * propios del participante
        */
        public string ComprobanteInscripcion(int pID, string pMail)
        {
            GestorReportes gestorReportes = new GestorReportes();

            using (var db = new JJSSEntities())
            {
                var participantes = from inscr in db.inscripcion
                                    join part in db.participante on inscr.id_participante equals part.id_participante
                                    where inscr.id_inscripcion == pID
                                    select new CompInscripcionTorneo()
                                    {
                                        tor_nombre = inscr.torneo.nombre,
                                        tor_sede = inscr.torneo.sede.nombre,
                                        tor_direccion = inscr.torneo.sede.direccion.calle + " " + inscr.torneo.sede.direccion.numero + " - " + inscr.torneo.sede.direccion.ciudad.nombre + " - " + inscr.torneo.sede.direccion.ciudad.provincia.nombre + " - " + inscr.torneo.sede.direccion.ciudad.provincia.pais.nombre,
                                        tor_fechaD = inscr.torneo.fecha,
                                        tor_hora = inscr.torneo.hora,
                                        tor_tipo = inscr.torneo.tipo_clase.nombre,
                                        tor_precio = inscr.torneo.precio_absoluto.ToString(),
                                        par_nombre = part.nombre,
                                        par_apellido = part.apellido,
                                        par_fecha_nacD = part.fecha_nacimiento,
                                        par_sexo = part.sexo,
                                        par_dni = part.dni.ToString(),
                                        par_faja = inscr.faja.descripcion,
                                        par_categoria = inscr.categoria_torneo.categoria.nombre,
                                        inscr_tipoI = inscr.tipo_inscripcion
                                       

                                    };
                List<CompInscripcionTorneo> participantesList = participantes.ToList();


                foreach (var part in participantesList)
                {
                    part.par_sexo_nombre = part.par_sexo == 1 ? "M" : "F";

                    if (string.IsNullOrEmpty(part.par_categoria))
                    {
                        part.par_categoria = "Sin categoría";
                    }

                    part.inscr_tipo = part.inscr_tipoI == 0 ? "Inscripción a categoria" : "Inscripción absoluta";


                    part.par_fecha_nac = part.par_fecha_nacD?.ToString("dd/MM/yyyy") ?? " - ";
                    part.tor_fecha = part.tor_fechaD?.ToString("dd/MM/yyyy") ?? " - ";

                }

                string sFile = gestorReportes.GenerarReporteComprInscripcionTorneo(participantesList);
                if (pMail != null)
                {
                    EnviarMail(sFile, pMail);
                }

                return sFile;

            }

        }

        /*
       * Obtenemos un listado de todos los participantes que estan en un torneo con datos de su categoria a la cual esta inscripto, la faja, y datos
       * propios del participante
       */
        public string ComprobanteInscripcionPago(int pID, string pMail)
        {
            GestorReportes gestorReportes = new GestorReportes();

            using (var db = new JJSSEntities())
            {
                var participantes = from inscr in db.inscripcion
                                    join part in db.participante on inscr.id_participante equals part.id_participante
                                    join cat_tor in db.categoria_torneo on inscr.id_categoria equals cat_tor.id_categoria_torneo
                                    join cat in db.categoria on cat_tor.id_categoria equals cat.id_categoria
                                    where inscr.id_inscripcion == pID
                                    select new CompInscripcionTorneo()
                                    {
                                        tor_nombre = inscr.torneo.nombre,
                                        tor_sede = inscr.torneo.sede.nombre,
                                        tor_direccion = inscr.torneo.sede.direccion.calle + " " + inscr.torneo.sede.direccion.numero + " - " + inscr.torneo.sede.direccion.ciudad.nombre + " - " + inscr.torneo.sede.direccion.ciudad.provincia.nombre + " - " + inscr.torneo.sede.direccion.ciudad.provincia.pais.nombre,
                                        tor_fechaD = inscr.torneo.fecha,
                                        tor_hora = inscr.torneo.hora,
                                        tor_tipo = inscr.torneo.tipo_clase.nombre,
                                        tor_precio = inscr.torneo.precio_absoluto.ToString(),
                                        par_nombre = part.nombre,
                                        par_apellido = part.apellido,
                                        par_fecha_nacD = part.fecha_nacimiento,
                                        par_sexo = part.sexo,
                                        par_dni = part.dni.ToString(),
                                        par_faja = inscr.faja.descripcion,
                                        par_categoria = cat.nombre


                                    };
                List<CompInscripcionTorneo> participantesList = participantes.ToList<CompInscripcionTorneo>();


                foreach (CompInscripcionTorneo part in participantesList)
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
                    part.tor_fecha = part.tor_fechaD?.ToString("dd/MM/yyyy") ?? " - ";

                }

                string sFile = gestorReportes.GenerarReporteComprInscripcionTorneo(participantesList);
                if (pMail != null)
                {
                    EnviarMail(sFile, pMail);
                }

                return sFile;

            }

        }


        private void EnviarMail(string sFile, string mail)
        {
            modEmails md = new modEmails();
            md.Msg_Adjuntos.Add(sFile);
            md.Msg_Destinatarios.Add(mail);
            md.Msg_Asunto = "Comprobante de Inscripción a Torneo de Lotus Club - Equipo Hinojal";
            md.Enviar();

        }

    }
}
