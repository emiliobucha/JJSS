using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;
using System.Runtime.InteropServices.ComTypes;
using JJSS_Negocio.Constantes;
using JJSS_Negocio.Resultados;

namespace JJSS_Negocio
{
    /*Clase que se encarga de gestionar las inscripciones a Torneos*/
    public class GestorInscripciones
    {
        private alumno alumnoExistente;


        //TODO nacionalidad y tipo dni
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
        public string InscribirATorneo(int pTorneo, string pNombre, string pApellido, double pPeso, DateTime pFechaNacimiento, int pFaja, short pSexo, int pTipoDoc, string pDni, int? pIDAlumno, short pTipoInscripcion, int pIdPais)
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
                    int edad = DateTime.Now.Year - pFechaNacimiento.Year;

                    var cat =
                        from categoria in db.categoria
                        where (categoria.edad_desde <= edad)
                        && (categoria.edad_hasta >= edad)
                        && (categoria.peso_desde <= pPeso)
                        && (categoria.peso_hasta > pPeso)
                        && (categoria.sexo == pSexo)
                        && !categoria.nombre.StartsWith("Absoluto")
                        select categoria;

                    categoria categoriaPerteneciente = cat.First();
                    categoria_torneo categoriaTorneoCat = null;

                    var catTorneoExistente = from catTor in db.categoria_torneo
                                             where (catTor.id_categoria == categoriaPerteneciente.id_categoria)
                                             && catTor.id_faja == fajaElegida.id_faja
                                             
                                             select catTor;

                    if (catTorneoExistente.Count() == 0)

                    {
                        categoriaTorneoCat = new categoria_torneo()
                        {
                            id_categoria = categoriaPerteneciente.id_categoria,
                            faja = fajaElegida,

                        };
                        db.categoria_torneo.Add(categoriaTorneoCat);
                        db.SaveChanges();
                    }
                    else
                    {
                        categoriaTorneoCat = catTorneoExistente.First();
                    }

                    //si es absoluto
                    categoria_torneo categoriaTorneoAbs = null;
                    if (pTipoInscripcion == Constantes.ConstantesTipoInscripcion.ABSOLUTO)
                    {
                        var abs =
                        from categoria in db.categoria
                        where (categoria.edad_desde <= edad)
                        && (categoria.edad_hasta >= edad)
                        && (categoria.sexo == pSexo)
                        && categoria.nombre.StartsWith("Absoluto")
                        select categoria;

                        var categoriaAbsolutoPerteneciente = abs.First();
                        
                        var catTorneoExistenteAbs = from catTor in db.categoria_torneo
                                                 where (catTor.id_categoria == categoriaAbsolutoPerteneciente.id_categoria)
                                                 && catTor.id_faja == fajaElegida.id_faja
                                                 
                                                 select catTor;

                        if (catTorneoExistenteAbs.Count() == 0)

                        {
                            categoriaTorneoAbs = new categoria_torneo()
                            {
                                id_categoria = categoriaAbsolutoPerteneciente.id_categoria,
                                faja = fajaElegida,

                            };
                            db.categoria_torneo.Add(categoriaTorneoAbs);
                            db.SaveChanges();
                        }
                        else
                        {
                            categoriaTorneoAbs = catTorneoExistenteAbs.First();
                        }
                        
                    }

                    //Nuevos
                    if (obtenerParticipanteDeTorneo(pTipoDoc,pDni, pTorneo) != null)
                    {
                        return "El participante ya se inscribió a este torneo";
                    }


                    var nuevoParticipante = ObtenerParticipanteporDNITipo(pTipoDoc, pDni);

                    if (nuevoParticipante == null)
                    {
                        nuevoParticipante = new participante()
                        {
                            nombre = pNombre,
                            apellido = pApellido,
                            id_tipo_documento = pTipoDoc,
                            sexo = pSexo,
                            fecha_nacimiento = pFechaNacimiento,
                            dni = pDni,
                            id_alumno = pIDAlumno,
                            id_pais = pIdPais,
                            

                        };
                        db.participante.Add(nuevoParticipante);
                        db.SaveChanges();
                    }
                    else
                    {
                        if (nuevoParticipante.nombre != pNombre)
                        {
                            nuevoParticipante.nombre = pNombre;
                            db.SaveChanges();
                        }
                        if (nuevoParticipante.apellido != pApellido)
                        {
                            nuevoParticipante.apellido = pApellido;
                            db.SaveChanges();
                        }
                        if (nuevoParticipante.fecha_nacimiento != pFechaNacimiento)
                        {
                            nuevoParticipante.fecha_nacimiento = pFechaNacimiento;
                            db.SaveChanges();
                        }
                        if (nuevoParticipante.id_pais != pIdPais)
                        {
                            nuevoParticipante.id_pais = pIdPais;
                            db.SaveChanges();
                        }
                        if (nuevoParticipante.sexo != pSexo)
                        {
                            nuevoParticipante.sexo = pSexo;
                            db.SaveChanges();
                        }


                    }

                    string hora  = DateTime.Now.ToString("hh:mm tt");
                    DateTime fecha = DateTime.Now.Date;
                    inscripcion nuevaInscripcion = new inscripcion()
                    {

                        hora = hora,
                        fecha = fecha,
                        codigo_barra = 123456789,
                        id_participante = nuevoParticipante.id_participante,
                        peso = pPeso,
                        id_faja = pFaja,
                        tipo_inscripcion = pTipoInscripcion
                    };

                    if (torneoInscripto != null)
                    {
                        nuevaInscripcion.id_torneo = torneoInscripto.id_torneo;
                    }
                    if (categoriaTorneoAbs != null)
                    {
                        nuevaInscripcion.id_absoluto = categoriaTorneoAbs.id_categoria_torneo;
                    }
                    if (categoriaTorneoCat != null)
                    {
                        nuevaInscripcion.id_categoria = categoriaTorneoCat.id_categoria_torneo;

                    }

                    
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
               

        public string InscribirATorneo(int pTorneo, string pNombre, string pApellido, int pTipoDoc, string pDni, int pIdCategoriaTorneo, short pSexo)
        {
            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                
                try
                {
                    //Foraneos
                    torneo torneoInscripto = db.torneo.Find(pTorneo);

                    //Nuevos
                    if (obtenerParticipanteDeTorneo(pTipoDoc,pDni, pTorneo) != null)
                    {
                        return "El participante ya se inscribió a este torneo";
                    }

                    var nuevoParticipante = ObtenerParticipanteporDNITipo(pTipoDoc, pDni);

                    if (nuevoParticipante == null)
                    {
                        nuevoParticipante = new participante()
                        {
                            nombre = pNombre,
                            apellido = pApellido,
                            id_tipo_documento = pTipoDoc,
                            sexo = pSexo,
                             dni = pDni,


                        };
                        db.participante.Add(nuevoParticipante);
                        db.SaveChanges();
                    }
                    else
                    {
                        if (nuevoParticipante.nombre != pNombre)
                        {
                            nuevoParticipante.nombre = pNombre;
                            db.SaveChanges();
                        }
                        if (nuevoParticipante.apellido != pApellido)
                        {
                            nuevoParticipante.apellido = pApellido;
                            db.SaveChanges();
                        }
                        if (nuevoParticipante.sexo != pSexo)
                        {
                            nuevoParticipante.sexo = pSexo;
                            db.SaveChanges();
                        }


                    }

                    short tipoInscripcion;
                    categoria_torneo categoriaAbsoluto = null;
                    categoria_torneo categoriaCategoria = null;
                    if (esAbsoluto(pIdCategoriaTorneo))
                    {
                        categoriaAbsoluto = db.categoria_torneo.Find(pIdCategoriaTorneo);
                        tipoInscripcion = Constantes.ConstantesTipoInscripcion.ABSOLUTO;
                    }
                    else
                    {
                        categoriaCategoria = db.categoria_torneo.Find(pIdCategoriaTorneo);
                        tipoInscripcion = Constantes.ConstantesTipoInscripcion.CATEGORIA;
                    }
                    
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
                        tipo_inscripcion = tipoInscripcion,
                        categoria_torneo = categoriaCategoria,
                        categoria_torneo1 = categoriaAbsoluto,
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

        private Boolean esAbsoluto(int pIDCategoria)
        {
            using (var db = new JJSSEntities())
            {
                
                var categoriaSeleccionada= from cat in db.categoria
                                   join catt in db.categoria_torneo on cat.id_categoria equals catt.id_categoria
                                   where catt.id_categoria_torneo == pIDCategoria
                                   select cat;

                categoria cate = categoriaSeleccionada.First();
                return cate.nombre.StartsWith("Absoluto");
            }
        }

        public inscripcion obtenerInscripcionATorneoPorIdParticipantePorDni(int idTipo ,string pDni, int pIdTorneo)
        {
            using (var db = new JJSSEntities())
            {
                inscripcion inscripcion = (from ins in db.inscripcion
                                           join part in db.participante on ins.id_participante equals part.id_participante
                                           where part.dni == pDni && ins.id_torneo == pIdTorneo && part.id_tipo_documento == idTipo
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
        public alumno ObtenerAlumnoPorDNI(int pTipo,string pDni)
        {
            GestorAlumnos gestorAlumnos = new GestorAlumnos();

            return gestorAlumnos.ObtenerAlumnoPorDNI(pDni);
        }


        /*
         * Método que busca un alumno por DNI, permite bajar el acoplamiente delegando la tarea a su gestor correspondiente
         * Parámetros:
         *              pDni: entero que representa el dni a buscar
         */
        public alumno ObtenerAlumnoPorDNITipo(int pTipo, string pDni)
        {
            GestorAlumnos gestorAlumnos = new GestorAlumnos();

            return gestorAlumnos.ObtenerAlumnoPorDNITipo(pTipo,pDni);
        }


        /*
         * Método que busca un participante por DNI, permite bajar el acoplamiente delegando la tarea a su gestor correspondiente
         * Parámetros:
         *              pDni: entero que representa el dni a buscar
         */

        public participante ObtenerParticipanteporDNI(string pDni)
        {
            GestorParticipantes gestorParticipantes = new GestorParticipantes();
            return gestorParticipantes.ObtenerParticipantePorDNI(pDni);
        }

        public participante ObtenerParticipanteporDNITipo(int pTipo, string pDni)
        {
            GestorParticipantes gestorParticipantes = new GestorParticipantes();
            return gestorParticipantes.ObtenerParticipantePorDNITipo(pTipo, pDni);
        }


        public participante obtenerParticipanteDeTorneo(int pTipoDoc, string pDni, int pIDTorneo)
        {
            using (var db = new JJSSEntities())
            {
                participante participante = (from part in db.participante
                                             join ins in db.inscripcion on part.id_participante equals ins.id_participante
                                             where part.dni == pDni && ins.id_torneo == pIDTorneo && part.id_tipo_documento == pTipoDoc
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
                                        par_dni = part.dni,
                                        par_faja = inscr.faja.descripcion,
                                        par_categoria = inscr.categoria_torneo.categoria.nombre,
                                        inscr_tipoI = inscr.tipo_inscripcion,
                                        par_tipo_documento = part.tipo_documento.codigo,
                                        tor_precio_absoluto = inscr.torneo.precio_absoluto.ToString(),
                                        tor_precio_categoria = inscr.torneo.precio_categoria.ToString()


                                    };
                List<CompInscripcionTorneo> participantesList = participantes.ToList();


                foreach (var part in participantesList)
                {
                    part.par_sexo_nombre = part.par_sexo == 1 ? "M" : "F";

                    if (string.IsNullOrEmpty(part.par_categoria))
                    {
                        part.par_categoria = "Sin categoría";
                    }

                    if (part.inscr_tipoI != null && part.inscr_tipoI == ConstantesTipoInscripcion.CATEGORIA)
                    {
                        part.tor_precio = part.tor_precio_categoria;
                    }
                    else if (part.inscr_tipoI != null && part.inscr_tipoI == ConstantesTipoInscripcion.ABSOLUTO)
                    {
                        part.tor_precio = part.tor_precio_absoluto;
                    }

                    part.inscr_tipo = part.inscr_tipoI == ConstantesTipoInscripcion.CATEGORIA ? "Inscripción a categoría" : "Inscripción absoluta";


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
        public string ComprobanteInscripcionPago(int pID, string pMail, int formaPago)
        {
            GestorReportes gestorReportes = new GestorReportes();

            using (var db = new JJSSEntities())
            {
                var forma = db.forma_pago.Find(formaPago);
                string formaString= "";

                if (forma!=null)
                {
                    formaString = forma.nombre;
                }

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
                                        tor_precio_absoluto = inscr.torneo.precio_absoluto.ToString(),
                                        tor_precio_categoria = inscr.torneo.precio_categoria.ToString(),
                                        inscr_tipoI = inscr.tipo_inscripcion,
                                        par_nombre = part.nombre,
                                        par_apellido = part.apellido,
                                        par_fecha_nacD = part.fecha_nacimiento,
                                        par_sexo = part.sexo,
                                        par_dni = part.dni,
                                        par_faja = inscr.faja.descripcion,
                                        par_categoria = cat.nombre,
                                        pag_forma_pago = formaString,
                                        par_tipo_documento = part.tipo_documento.codigo

                                    };
                List<CompInscripcionTorneo> participantesList = participantes.ToList();


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

                    if (part.inscr_tipoI != null && part.inscr_tipoI == ConstantesTipoInscripcion.CATEGORIA)
                    {
                        part.tor_precio = part.tor_precio_categoria;
                    }
                    else if (part.inscr_tipoI != null && part.inscr_tipoI == ConstantesTipoInscripcion.ABSOLUTO)
                    {
                        part.tor_precio = part.tor_precio_absoluto;
                    }

                    part.inscr_tipo = part.inscr_tipoI == ConstantesTipoInscripcion.CATEGORIA ? "Inscripción a categoría" : "Inscripción absoluta";

                    part.pag_fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " hs";
                    part.par_fecha_nac = part.par_fecha_nacD?.ToString("dd/MM/yyyy") ?? " - ";
                    part.tor_fecha = part.tor_fechaD?.ToString("dd/MM/yyyy") ?? " - ";

                }

                string sFile = gestorReportes.GenerarReporteComprInscripcionTorneoPago(participantesList);
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

        public List<tipo_documento> ObtenerTiposDocumentos()
        {
            try
            {
                using (var db = new JJSSEntities())
                {

                    var list = db.tipo_documento.ToList();       
                    return list;
                }
            }
            catch (Exception e)
            {
                return new List<tipo_documento>();
            }
        }

        public List<pais> ObtenerNacionalidades()
        {
            try
            {
                using (var db = new JJSSEntities())
                {
                    var list = db.pais.ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                return new List<pais>();
            }
        }

        public List<MisInscripciones> ObtenerInscripcionesDeAlumno(int pIDAlumno, Boolean verTodos)
        {
            DateTime fechaActual = DateTime.Now;
            using (var db = new JJSSEntities())
            {

                var inscripcion = from it in db.inscripcion
                                  where it.participante.id_alumno == pIDAlumno && (verTodos || it.torneo.fecha >= fechaActual)
                                  select new MisInscripciones()
                                  {
                                      nombre = it.torneo.nombre,
                                      dtFecha = it.torneo.fecha,
                                      hora = it.torneo.hora,
                                      id_inscripcion = it.id_inscripcion,
                                      dtFechaInscripcion = it.fecha,
                                      pago = it.pago
                                  };
                List<MisInscripciones> inscripcionesList = inscripcion.ToList();
                foreach (MisInscripciones ins in inscripcionesList)
                {
                    ins.fecha_inscripcion = ((DateTime)ins.dtFechaInscripcion).ToString("dd/MM/yyyy");
                    ins.fecha = ((DateTime)ins.dtFecha).ToString("dd/MM/yyyy");
                    ins.hora = ins.hora + " hs";
                }
                return inscripcionesList;
            }
        }

    }
}
