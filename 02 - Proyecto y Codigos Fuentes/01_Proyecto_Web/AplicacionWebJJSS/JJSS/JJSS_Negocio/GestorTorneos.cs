using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;
using System.Data;
using System.Configuration;
using System.Globalization;
using JJSS_Negocio.Resultados;
using JJSS_Negocio.Administracion;
using JJSS_Negocio.Constantes;

namespace JJSS_Negocio
{
    /*
     * Clase que nos permite gestionar Torneos
     */
    public class GestorTorneos
    {

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

        public string GenerarNuevoTorneo(DateTime pFecha, string pNombre, decimal pPrecio_categoria, decimal pPrecio_absoluto, string pHora, int pSede, DateTime pFecha_cierre, string pHora_cierre, byte[] pImagen)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                estado estadoTorneo = db.estado.Find(ConstantesEstado.TORNEO_INSCRIPCION_ABIERTA);
                var transaction = db.Database.BeginTransaction();
                try
                {
                    torneo torneoEncontrado = BuscarTorneoPorNombreFecha(pNombre, pFecha);
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
                        estado = estadoTorneo,
                        id_tipo_clase = ConstantesTipoClase.JIU_JITSU
                    };
                    db.torneo.Add(nuevoTorneo);
                    db.SaveChanges();

                    byte[] arrayImagen = pImagen;
                    if (arrayImagen.Length > 7000)
                    {
                        arrayImagen = new byte[0];
                    }

                    string imagenUrl = modUtilidades.SaveImage(pImagen, pNombre, "torneos");

                    torneo_imagen nuevoTorneoImagen = new torneo_imagen()
                    {
                        id_torneo = nuevoTorneo.id_torneo,
                        imagen = arrayImagen,
                        imagen_url = imagenUrl
                    };

                    db.torneo_imagen.Add(nuevoTorneoImagen);
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
         * Busca un torneo a partir de su nombre y fecha
         * Parametros: 
         *              pFecha : DateTime que nos indica en que fecha se va a realizar el torneo
         *              pNombre: String indica el nombre del torneo
         * Retorno: torneo 
         *          torneo encontrado, si no existe devuelve null
         */
        public torneo BuscarTorneoPorNombreFecha(string pNombre, DateTime pFecha)
        {

            using (var db = new JJSSEntities())
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
            using (var db = new JJSSEntities())
            {
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
                    where torneo.id_estado == ConstantesEstado.TORNEO_INSCRIPCION_ABIERTA
                    select torneo;
                return torneosAbiertos.ToList();
            }
        }

        public List<TorneoResultado> ObtenerTorneosConImagenYFiltro(String filtroNombre, DateTime filtroFecha, DateTime filtroFechaHasta)
        {
            cambiarEstadoTorneos();
            using (var db = new JJSSEntities())
            {

                var torneos = from tor in db.torneo
                              join est in db.estado on tor.id_estado equals est.id_estado
                              join i in db.torneo_imagen on tor.id_torneo equals i.id_torneo
                              into ps
                              from i in ps.DefaultIfEmpty()
                              where tor.nombre.StartsWith(filtroNombre) &&
                              tor.fecha >= filtroFecha && tor.fecha <= filtroFechaHasta &&
                              (est.id_estado == ConstantesEstado.TORNEO_INSCRIPCION_ABIERTA || est.id_estado == ConstantesEstado.TORNEO_IN_SCRIPCION_CERRADA)
                              orderby tor.fecha ascending
                              select new TorneoResultado()
                              {
                                  id_torneo = tor.id_torneo,
                                  nombre = tor.nombre,
                                  dtFecha = tor.fecha,
                                  imagenB = i.imagen,
                                  estado = est.nombre,
                                  hora = tor.hora,
                                  imagen = i.imagen_url
                              };
                List<TorneoResultado> tr = torneos.ToList();
                foreach (TorneoResultado t in tr)
                {
                    t.fecha = t.dtFecha?.ToString("dd/MM/yyyy") ?? " - ";
                }
                return tr;
            }
        }

        public void cambiarEstadoTorneos()
        {
            using (var db = new JJSSEntities())
            {
                // del estado 1 al 2
                List<torneo> torneosAbiertos =
                        (from torneo in db.torneo
                         where torneo.id_estado == ConstantesEstado.TORNEO_INSCRIPCION_ABIERTA
                         select torneo).ToList<torneo>();

                foreach (torneo x in torneosAbiertos)
                {
                    DateTime cierre = (DateTime)x.fecha_cierre;
                    if (cierre.Date < DateTime.Now.Date) x.id_estado = ConstantesEstado.TORNEO_IN_SCRIPCION_CERRADA;
                    else if (cierre.Date == DateTime.Now.Date && x.hora_cierre.CompareTo(DateTime.Now.ToShortTimeString()) < 0) x.id_estado = ConstantesEstado.TORNEO_IN_SCRIPCION_CERRADA;
                    db.SaveChanges();
                }

                //del estado 2 al 3
                List<torneo> torneosCerrados =
                        (from torneo in db.torneo
                         where torneo.id_estado == ConstantesEstado.TORNEO_IN_SCRIPCION_CERRADA
                         select torneo).ToList<torneo>();

                foreach (torneo x in torneosCerrados)
                {
                    DateTime fecha = (DateTime)x.fecha;
                    if (fecha.Date < DateTime.Now.Date) x.id_estado = ConstantesEstado.TORNEO_EN_CURSO;
                    else if (fecha.Date == DateTime.Now.Date && x.hora.CompareTo(DateTime.Now.ToShortTimeString()) < 0) x.id_estado = ConstantesEstado.TORNEO_EN_CURSO;
                    db.SaveChanges();
                }

                //del estado 3 al 4
                List<torneo> torneosEnCurso =
                        (from torneo in db.torneo
                         where torneo.id_estado == ConstantesEstado.TORNEO_EN_CURSO
                         select torneo).ToList<torneo>();

                foreach (torneo x in torneosEnCurso)
                {
                    DateTime fecha = (DateTime)x.fecha;
                    if (fecha.Date < DateTime.Now.Date) x.id_estado = ConstantesEstado.TORNEO_FINALIZADO;
                    db.SaveChanges();
                }

            }
        }

        public List<torneo> ObtenerTorneosAbiertosCerrados()
        {

            using (var db = new JJSSEntities())
            {
                var torneosAbiertos =
                    from torneo in db.torneo
                    where torneo.id_estado == ConstantesEstado.TORNEO_INSCRIPCION_ABIERTA || torneo.id_estado == ConstantesEstado.TORNEO_IN_SCRIPCION_CERRADA
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
        public String EliminarTorneoPorID(int pID)
        {
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
        public List<participante> ObtenerParticipantesATorneo(int pID)
        {
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
        public List<ParticipantesTorneoResultado> ListadoParticipantes(int pID)
        {
            using (var db = new JJSSEntities())
            {
                var participantes = from inscr in db.inscripcion
                                    join part in db.participante on inscr.id_participante equals part.id_participante
                                    join cat_tor in db.categoria_torneo on inscr.id_categoria equals cat_tor.id_categoria_torneo
                                    join cat in db.categoria on cat_tor.id_categoria equals cat.id_categoria
                                    where inscr.id_torneo == pID && ((inscr.pago == 1 && inscr.participante.id_alumno == null) || inscr.participante.id_alumno !=null)
                                    select new ParticipantesTorneoResultado()
                                    {
                                        tor_nombre = inscr.torneo.nombre,
                                        tor_sede = inscr.torneo.sede.nombre,
                                        tor_direccion = inscr.torneo.sede.direccion.calle + " " + inscr.torneo.sede.direccion.numero + " - " + inscr.torneo.sede.direccion.ciudad.nombre + " - " + inscr.torneo.sede.direccion.ciudad.provincia.nombre + " - " + inscr.torneo.sede.direccion.ciudad.provincia.pais.nombre,
                                        tor_fechaD = inscr.torneo.fecha,
                                        tor_hora = inscr.torneo.hora,
                                        par_nombre = part.nombre,
                                        par_apellido = part.apellido,
                                        par_fecha_nacD = part.fecha_nacimiento,
                                        par_sexo = part.sexo,
                                        par_faja = inscr.faja.descripcion,
                                        par_categoria = cat.nombre,
                                        par_dni = part.dni,
                                        par_tipo_documento = part.tipo_documento.codigo,
                                        par_peso = inscr.peso + " Kg",
                                        inscr_pagoI = inscr.pago,
                                        id_alumno = part.id_alumno

                                    };
                List<ParticipantesTorneoResultado> participantesList = participantes.ToList<ParticipantesTorneoResultado>();


                foreach (ParticipantesTorneoResultado part in participantesList)
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

                    part.inscr_pago = part.inscr_pagoI == 1 ? "Si" : "No";
                    part.par_alumno = part.id_alumno != null ? "Si" : "No";


                }


                return participantesList;
            }
        }

        /*
         * busca los torneos con su imagen que comiencen con filtroNombre y la fecha sea mayor a filtroFecha
         */
        public List<TorneoResultado> BuscarTorneosConFiltrosEImagen(String filtroNombre, DateTime filtroFecha, DateTime filtroFechaHasta, int filtroEstado)
        {
            cambiarEstadoTorneos();
            using (var db = new JJSSEntities())
            {
                List<TorneoResultado> tr = new List<TorneoResultado>();
                if (filtroEstado == 0)
                {
                    var torneos = from tor in db.torneo
                                  join est in db.estado on tor.id_estado equals est.id_estado
                                  join i in db.torneo_imagen on tor.id_torneo equals i.id_torneo
                                  into ps
                                  from i in ps.DefaultIfEmpty()
                                  where tor.nombre.StartsWith(filtroNombre) &&
                                  tor.fecha >= filtroFecha && tor.fecha <= filtroFechaHasta &&
                                  (est.id_estado == ConstantesEstado.TORNEO_FINALIZADO || est.id_estado == ConstantesEstado.TORNEO_EN_CURSO)
                                  orderby tor.fecha descending
                                  select new TorneoResultado()
                                  {
                                      id_torneo = tor.id_torneo,
                                      nombre = tor.nombre,
                                      dtFecha = tor.fecha,
                                      imagenB = i.imagen,
                                      estado = est.nombre,
                                      imagen = i.imagen_url
                                  };
                    tr = torneos.ToList();
                }
                else
                {
                    var torneos = from tor in db.torneo
                                  join est in db.estado on tor.id_estado equals est.id_estado
                                  join i in db.torneo_imagen on tor.id_torneo equals i.id_torneo
                                  into ps
                                  from i in ps.DefaultIfEmpty()
                                  where tor.nombre.StartsWith(filtroNombre) &&
                                  tor.fecha >= filtroFecha && tor.fecha <= filtroFechaHasta &&
                                  (est.id_estado == filtroEstado)
                                  orderby tor.fecha descending
                                  select new TorneoResultado()
                                  {
                                      id_torneo = tor.id_torneo,
                                      nombre = tor.nombre,
                                      dtFecha = tor.fecha,
                                      imagenB = i.imagen,
                                      estado = est.nombre,
                                      imagen = i.imagen_url
                                  };
                    tr = torneos.ToList();
                }

                foreach (TorneoResultado t in tr)
                {
                    t.fecha = t.dtFecha?.ToString("dd/MM/yyyy") ?? " - ";
                }
                return tr;
            }
        }

        public string cancelarTorneo(int idTorneo, int idEstado)
        {
            using (var db = new JJSSEntities())
            {
                try
                {
                    torneo torneoSeleccionado = db.torneo.Find(idTorneo);
                    torneoSeleccionado.id_estado = idEstado;
                    db.SaveChanges();
                    return "";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
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
            //torneo torneoAListar = BuscarTorneoPorID(pID);
            //GestorSedes gestorSedes = new GestorSedes();
            //gestorSedes.BuscarSedePorID(torneoAListar.id_sede);
            //gestorSedes.ObtenerDireccion(torneoAListar.id_sede);
            List<ParticipantesTorneoResultado> listado = ListadoParticipantes(pID);

            if (listado.Count == 0)
                throw new Exception("No posee inscriptos el torneo seleccionado");

            return gestorReportes.GenerarReporteListadoParticipantes(listado);


        }


        /*
         * Genera listado de resultados a un torneo con su reporte.
         * Retorno: String del archivo resultante de la generacion del reporte en PDF
         */
        public string GenerarListadoResultados(torneo torneoSeleccionado, List<ResultadoDeTorneo> resultadosTorneo, SedeDireccion sede)
        {
            GestorReportes gestorReportes = new GestorReportes();

            

            var listado = new List<ReporteResultadosTorneo>();
            foreach (var resultado in resultadosTorneo)
            {


                var nuevo = new ReporteResultadosTorneo
                {
                    sexo = resultado.sexo == 1 ? "M" : "F",
                    categoria = resultado.categoria,
                    faja = resultado.faja,
                    primero = resultado.primero,
                    segundo = resultado.segundo,
                    tercero1 = resultado.tercero1,
                    tercero2 = resultado.tercero2,
                    tor_direccion = sede.calle + " " + sede.numero + " - B° " + sede.barrio + " - " + sede.ciudad + " - " + sede.provincia + " - " + sede.pais,
                    tor_sede = sede.sede,
                    tor_fecha = ((DateTime)torneoSeleccionado.fecha).ToString("dd/MM/yyyy"),
                    tor_hora = torneoSeleccionado.hora,
                    tor_nombre = torneoSeleccionado.nombre
                };
                listado.Add(nuevo);
            }



            if (listado.Count == 0)
                throw new Exception("No posee inscriptos el torneo seleccionado");

            return gestorReportes.GenerarReporteListadoResultadosTorneo(listado);


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
                    lucha nuevaLucha = new lucha()
                    {
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


        public TorneoResultado ObtenerTorneoResultado(int id)
        {
            using (var db = new JJSSEntities())
            {
                try
                {
                    var torneo = db.torneo.Find(id);
                    if (torneo != null)
                    {

                        var estado = db.estado.Find(torneo.id_estado);
                        var imagen = db.torneo_imagen.FirstOrDefault(x => x.id_torneo == torneo.id_torneo);
                        return new TorneoResultado()
                        {
                            nombre = torneo.nombre,
                            id_torneo = torneo.id_torneo,
                            dtFecha = torneo.fecha,
                            estado = estado.nombre,
                            hora = torneo.hora,
                            fecha = torneo.fecha.ToString(),
                            imagenB = imagen.imagen,
                            imagen = imagen.imagen_url,
                            hora_cierre = torneo.hora_cierre,
                            dtFechaCierre = torneo.fecha_cierre,
                            precio_absoluto = torneo.precio_absoluto,
                            precio_categoria = torneo.precio_categoria,
                            idSede = torneo.id_sede

                        };

                    }
                    else
                        return new TorneoResultado();


                }
                catch (Exception e)
                {
                    return new TorneoResultado();
                }
            }
        }



        public string modificarTorneo(torneo pTorneo, byte[] pImagen)
        {
            string sReturn = "";
            using (var db = new JJSSEntities())
            {
                estado estadoTorneo = db.estado.Find(ConstantesEstado.TORNEO_INSCRIPCION_ABIERTA);
                var transaction = db.Database.BeginTransaction();
                try
                {
                    torneo torneoEncontrado = db.torneo.Find(pTorneo.id_torneo);
                    if (torneoEncontrado == null) return "El torneo no existe";

                    torneoEncontrado.fecha = pTorneo.fecha;
                    torneoEncontrado.fecha_cierre = pTorneo.fecha_cierre;
                    torneoEncontrado.hora = pTorneo.hora;
                    torneoEncontrado.hora_cierre = pTorneo.hora_cierre;
                    torneoEncontrado.id_sede = pTorneo.id_sede;
                    torneoEncontrado.nombre = pTorneo.nombre;
                    torneoEncontrado.precio_absoluto = pTorneo.precio_absoluto;
                    torneoEncontrado.precio_categoria = pTorneo.precio_categoria;
                    db.SaveChanges();

                    torneo_imagen imagenAnterior = buscarImagenTorneo(pTorneo.id_torneo);
                    if (imagenAnterior == null)
                    {
                        torneo_imagen nuevoTorneoImagen = new torneo_imagen()
                        {
                            id_torneo = torneoEncontrado.id_torneo,
                            imagen = pImagen
                        };
                        db.torneo_imagen.Add(nuevoTorneoImagen);
                        db.SaveChanges();
                    }
                    else
                    {
                        if (pImagen != null && pImagen.Length > 0)
                        {
                            byte[] arrayImagen = pImagen;
                            if (arrayImagen.Length > 7000)
                            {
                                arrayImagen = new byte[0];
                            }
                            imagenAnterior = db.torneo_imagen.FirstOrDefault(x => x.id_torneo == pTorneo.id_torneo);
                            string imagenUrl = modUtilidades.SaveImage(pImagen, pTorneo.nombre, "torneos");

                            imagenAnterior.imagen = arrayImagen;
                            imagenAnterior.imagen_url = imagenUrl;
                            db.SaveChanges();
                        }

                    }

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

        public torneo_imagen buscarImagenTorneo(int idTorneo)
        {
            using (var db = new JJSSEntities())
            {
                var imagen = from ima in db.torneo_imagen
                             where ima.id_torneo == idTorneo
                             select ima;
                return imagen.FirstOrDefault();
            }
        }

        public estado buscarEstadoTorneo(int idTorneo)
        {
            using (var db = new JJSSEntities())
            {
                var estado = from est in db.estado
                             join tor in db.torneo on est.id_estado equals tor.id_estado
                             where tor.id_torneo == idTorneo
                             select est;
                return estado.First();
            }
        }

        public List<ResultadoDeTorneo> buscarResultados(int idTorneo)
        {
            using (var db = new JJSSEntities())
            {
                var resultados = from res in db.resultado
                                 join catt in db.categoria_torneo on res.id_categoria_torneo equals catt.id_categoria_torneo
                                 join tor in db.torneo on res.id_torneo equals tor.id_torneo
                                 where tor.id_torneo == idTorneo
                                 orderby catt.categoria.nombre
                                 select new ResultadoDeTorneo()
                                 {
                                     categoria = catt.categoria.nombre,
                                     faja = catt.faja.descripcion,
                                     primero = res.participante.nombre + " " + res.participante.apellido,
                                     segundo = res.participante1.nombre + " " + res.participante1.apellido,
                                     tercero1 = res.participante2.nombre + " " + res.participante2.apellido,
                                     tercero2 = res.participante3.nombre + " " + res.participante3.apellido,
                                     sexo = catt.categoria.sexo,
                                 };
                List<ResultadoDeTorneo> resu = resultados.ToList();
                foreach (ResultadoDeTorneo r in resu)
                {
                    string sexo = r.sexo == ContantesSexo.FEMENINO ? " F " : " M ";
                    r.categoria = r.categoria + " " + sexo;
                    r.faja = r.faja.Split('-')[0];
                }
                return resu;
            }
        }

        public List<estado> buscarEstadosTorneo()
        {
            using (var db = new JJSSEntities())
            {
                return db.estado.Where(x => x.ambito == "TORNEOS").ToList();
            }
        }
    }
}
