using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;
using System.Data;
using System.Data.Linq;
using System.Configuration;
using System.Globalization;
using JJSS_Negocio.Resultados;
using JJSS_Negocio.Administracion;

namespace JJSS_Negocio
{
    /*
     * Clase que nos permite gestionar Clases
     */
    public class GestorClases
    {


        /*
         * Genera una nueva clase
         * Parametros:
         *              pTipo: Entero Indica el tipo de clase a la que pertenece
         *              pPrecio: Double Indica el precio que cuesta inscribirse a esa clase en particular
         *              pHorarios: DataTable los horarios que han sido cargados en la grilla de horarios de clase
         *               pNombre: string nombre indentificatorio para la clase
         *              pUbicacion: Entero indica la ubicacion donde se da la clase
         * Retornos:String 
         *          "" - Todo salió bien
         *          mensaje de excepcion
         *          
         */

        public String GenerarNuevaClase(int pTipo, double pPrecio, DataTable pHorarios, string pNombre, int pUbicacion, int pProfe)
        {
            String sReturn = "";
            if (validarClaseExistente(pNombre, pUbicacion))
            {
                return "Existe una clase con ese mismo nombre en esa academia";
            }
            try
            {
                using (var db = new JJSSEntities())
                {

                    var transaction = db.Database.BeginTransaction();
                    try
                    {
                        clase nuevaClase = new clase()
                        {
                            id_tipo_clase = pTipo,
                            precio = pPrecio,
                            nombre = pNombre,
                            id_ubicacion = pUbicacion,
                            id_profe = pProfe,
                            baja_logica = 1 //clase disponible
                        };

                        db.clase.Add(nuevaClase);
                        db.SaveChanges();

                        foreach (DataRow drAux in pHorarios.Rows)
                        {
                            horario nuevoHorario = new horario()
                            {
                                hora_desde = drAux["hora_desde"].ToString(),
                                hora_hasta = drAux["hora_hasta"].ToString(),
                                id_clase = nuevaClase.id_clase,
                                nombre_dia = drAux["nombre_dia"].ToString(),
                                dia = int.Parse(drAux["dia"].ToString())

                            };

                            db.horario.Add(nuevoHorario);

                        }

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
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private Boolean validarClaseExistente(string nombreClase, int idAcademia)
        {
            clase claseExistente = null;
            using (var db = new JJSSEntities())
            {
                claseExistente = (from cl in db.clase
                                  where cl.nombre.Equals(nombreClase, StringComparison.OrdinalIgnoreCase)
                                  && cl.id_ubicacion == idAcademia && cl.baja_logica == Constantes.ConstatesBajaLogica.BAJA_LOGICA
                                  select cl).FirstOrDefault();
            }
            return claseExistente != null;
        }


        /*
         * Método que devuelve los horarios disponibles de una clase, si esta no existe devuelve una tabla vacia que nos permite ver el formato de dicha tabla
         * Parametros:
         *              int pID indica el id de clase a la que se quiere ver los horarios
         * Retorno:
         *          Datatable con todos los horarios
         */
        public DataTable ObtenerTablaHorarios(int pID)
        {
            using (var db = new JJSSEntities())
            {
                var horarios = from hor in db.horario
                               where hor.id_clase == pID
                               select hor;

                return modUtilidadesTablas.ToDataTable(horarios.ToList());
            }
        }

        public List<ClaseHorario> ObtenerTodosLosHorarios(string filtroNombre, string filtroTipoClase, int filtroIDProfesor,
            string filtroAcademia)
        {

            GestorProfesores gp = new GestorProfesores();

            string apellidoProfesor = "";
            string nombreProfesor = "";

            if (filtroIDProfesor != 0)
            {
                profesor p = gp.ObtenerProfesorPorID(filtroIDProfesor);
                apellidoProfesor = p.apellido;
                nombreProfesor = p.nombre;
            }

            using (var db = new JJSSEntities())
            {
                var horarios = from h in db.horario
                               where h.clase.baja_logica == Constantes.ConstatesBajaLogica.BAJA_LOGICA
                               && h.clase.nombre.StartsWith(filtroNombre) && h.clase.academia.nombre.StartsWith(filtroAcademia)
                               && h.clase.profesor.nombre.StartsWith(nombreProfesor) && h.clase.profesor.apellido.StartsWith(apellidoProfesor)
                               && h.clase.tipo_clase.nombre.StartsWith(filtroTipoClase)
                               select new ClaseHorario()
                               {
                                   desde = h.hora_desde,
                                   hasta = h.hora_hasta,
                                   id = h.id_horario,
                                   idClase = h.clase.id_clase,
                                   nombreClase = h.clase.nombre,
                                   academia = h.clase.academia.nombre,
                                   dia = h.dia,
                                   tipoClase = h.clase.tipo_clase.nombre,
                                   profesor = h.clase.profesor.nombre + " " + h.clase.profesor.nombre,
                               };
                return horarios.ToList();
            }
        }


        /*
         * Método que devuelve los horarios disponibles de una clase, si esta no existe devuelve una lista vacia que nos permite ver el formato de dicha tabla
         * Parametros:
         *              int pID indica el id de clase a la que se quiere ver los horarios
         * Retorno:
         *          Datatable con todos los horarios
         */
        public List<HorariosResultado> ObtenerHorariosResultadosDeFecha(DateTime pFecha)
        {
            using (var db = new JJSSEntities())
            {
                var horarios = from hor in db.horario
                               where hor.dia == (int)pFecha.DayOfWeek
                               select new HorariosResultado
                               {
                                   id = hor.id_horario,
                                   nombre_horario = hor.clase.nombre + " / " + hor.nombre_dia + " - " + hor.hora_desde + " - " + hor.hora_hasta,
                                   id_clase = hor.id_clase
                               };

                return horarios.ToList();
            }
        }





        /*
         * Obtiene un listado de todas las clases que están disponibles
         * Retorno: List<clase> 
         *          Listado de todas las clases
         */
        public List<ClasesDisponibles> ObtenerClasesDisponibles(string filtroNombre, int filtroProfesor, int filtroAcademia, int filtroTipoClase)
        {
            List<ClasesDisponibles> claseLista = null;
            using (var db = new JJSSEntities())
            {
                var clasesDisponibles = from clases in db.clase
                                        join ubic in db.academia on clases.id_ubicacion equals ubic.id_academia
                                        join tipo in db.tipo_clase on clases.id_tipo_clase equals tipo.id_tipo_clase
                                        join profe in db.profesor on clases.id_profe equals profe.id_profesor
                                        where clases.baja_logica == 1 && clases.nombre.StartsWith(filtroNombre)
                                        select new ClasesDisponibles()
                                        {
                                            id_clase = clases.id_clase,
                                            nombre = clases.nombre,
                                            precio = clases.precio,
                                            tipo_clase = tipo.nombre,
                                            ubicacion = ubic.nombre,
                                            profesor = profe.nombre + " " + profe.apellido,
                                            id_academia = ubic.id_academia,
                                            id_profesor = profe.id_profesor,
                                            id_tipo_clase = (int)clases.id_tipo_clase
                                        };
                claseLista = clasesDisponibles.ToList();
            }
            List<ClasesDisponibles> clasesParaMostrar = new List<ClasesDisponibles>();

            foreach (var clase in claseLista)
            {
                //000
                if (filtroAcademia == 0 && filtroProfesor == 0 && filtroTipoClase == 0)
                {
                    clasesParaMostrar.Add(clase);
                }            
                //001
                else if (filtroAcademia == 0 && filtroProfesor == 0 && clase.id_tipo_clase == filtroTipoClase)
                {
                    clasesParaMostrar.Add(clase);
                }
                //010
                else if (filtroAcademia == 0 && clase.id_profesor == filtroProfesor && filtroTipoClase == 0)
                {
                    clasesParaMostrar.Add(clase);
                }
                //011
                else if (filtroAcademia == 0 && clase.id_profesor == filtroProfesor && clase.id_tipo_clase == filtroTipoClase)
                {
                    clasesParaMostrar.Add(clase);
                }
                //100
                else if (clase.id_academia == filtroAcademia && filtroProfesor == 0 && filtroTipoClase == 0  )
                {
                    clasesParaMostrar.Add(clase);
                }
                //101
                else if (clase.id_academia == filtroAcademia && filtroProfesor == 0 && clase.id_tipo_clase == filtroTipoClase)
                {
                    clasesParaMostrar.Add(clase);
                }
                //110
                else if (clase.id_academia == filtroAcademia && clase.id_profesor == filtroProfesor && filtroTipoClase == 0)
                {
                    clasesParaMostrar.Add(clase);
                }
                //111
                else if (clase.id_profesor == filtroProfesor && clase.id_academia == filtroAcademia && clase.id_tipo_clase == filtroTipoClase)
                {
                    clasesParaMostrar.Add(clase);
                }
            }


            return clasesParaMostrar;
        }


        /*
         * Obtiene los datos de una clase dado un id
         * Parametros: 
         *              pId : id de la clase a buscar
         * Retorno
         *              la clase encontrada
         *              null si no encuentra
         * */
        public clase ObtenerClasePorId(int pId)
        {
            using (var db = new JJSSEntities())
            {
                return db.clase.Find(pId);
            }
        }

        /*
         * Actualiza el precio y los horarios de una clase
         * Parametros: 
         *              pId : Entero id de la clase que se va a actualizar
         *              pHorarios : DataTable tabla con los nuevos horarios
         *              pPrecio : Double precio a actualizar
         * Retorno: 
         *          string con el mensaje de error de la BD
         *          "" si esta correcto
         * 
         * */
        public string modificarClase(int pId, DataTable pHorarios, double pPrecio, int pProfe)
        {
            string sReturn = "";
            try
            {
                using (var db = new JJSSEntities())
                {
                    var transaction = db.Database.BeginTransaction();
                    try
                    {
                        //busca la clase y actualiza el precio y profe
                        clase claseEncontrada = db.clase.Find(pId);
                        claseEncontrada.precio = pPrecio;
                        claseEncontrada.id_profe = pProfe;
                        db.SaveChanges();

                        var horarios = from h in db.horario
                                       where h.id_clase == pId
                                       select h;

                        //elimina los horarios que tengan que ser eliminados
                        List<horario> horariosAEliminar = getHorariosEliminar(pHorarios, horarios.ToList());
                        db.horario.RemoveRange(horariosAEliminar);
                        db.SaveChanges();

                        //agrega los nuevos horarios
                        List<horario> horariosAAgregar = getNuevosHorarios(pHorarios, horarios.ToList(), pId);

                        db.horario.AddRange(horariosAAgregar);
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
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //compara los nuevos horarios con los que estan en la DB para no sobreescribir los mismos, 
        // devuelve aquellos horarios que no estan en la DB
        private List<horario> getNuevosHorarios(DataTable nuevosHorarios, List<horario> horariosAnteriores, int idClase)
        {
            Boolean encuentra;
            List<horario> horariosAAgregar = new List<horario>();
            foreach (DataRow dr in nuevosHorarios.Rows)
            {
                encuentra = false;
                foreach (horario h in horariosAnteriores)
                {
                    if (dr["hora_desde"].ToString().CompareTo(h.hora_desde) == 0 && dr["hora_hasta"].ToString().CompareTo(h.hora_hasta) == 0
                        && Convert.ToInt16(dr["dia"]) == h.dia)
                    {
                        encuentra = true;
                        break;

                    }
                }
                if (!encuentra)
                {
                    horario nuevoHorario = new horario()
                    {
                        dia = Convert.ToInt16(dr["dia"]),
                        nombre_dia = dr["nombre_dia"].ToString(),
                        hora_desde = dr["hora_desde"].ToString(),
                        hora_hasta = dr["hora_hasta"].ToString(),
                        id_clase = idClase,
                    };
                    horariosAAgregar.Add(nuevoHorario);
                }
            }
            return horariosAAgregar;
        }

        //compara los nuevos horarios con los que estan en la DB para eliminar los que tengan que ser eliminados, 
        private List<horario> getHorariosEliminar(DataTable nuevosHorarios, List<horario> horariosAnteriores)
        {
            List<horario> horariosAEliminar = new List<horario>();
            Boolean encuentra;

            foreach (horario h in horariosAnteriores)
            {
                encuentra = false;
                foreach (DataRow dr in nuevosHorarios.Rows)
                {
                    if (dr["hora_desde"].ToString().CompareTo(h.hora_desde) == 0 && dr["hora_hasta"].ToString().CompareTo(h.hora_hasta) == 0
                        && Convert.ToInt16(dr["dia"]) == h.dia)
                    {
                        encuentra = true;
                        break;
                    }
                }
                if (!encuentra)
                {
                    horariosAEliminar.Add(h);
                }
            }

            return horariosAEliminar;
        }

        /*
         * Metodo que elimina una clase logicamente
         * Parametros: pIdClase: entero - id de la clase a eliminar
         * Retorno: "" Transaccion correcta de BD
         *          ex.Message: Mensaje de error de la BD
         * 
         */
        public string eliminarClase(int pIdClase)
        {
            string sReturn = "";

            try
            {
                using (var db = new JJSSEntities())
                {
                    var transaction = db.Database.BeginTransaction();
                    try
                    {
                        clase claseEncontrada = db.clase.Find(pIdClase);
                        claseEncontrada.baja_logica = 0;

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
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /*
         * Método que devuelve un listado de todas los tipos de clases cargadas
         * Retorno: List<tipo_clase>
         *          Retorna toda lista de tipos de clases
         */
        public List<tipo_clase> ObtenerTipoClases()
        {
            GestorTipoClase gestorTipoClase = new GestorTipoClase();
            return gestorTipoClase.ObtenerTipoClase();
        }

        /*
         * Método que devuelve un tipo de clase segun su id
         * Parametros: pIDTipoClase: entero que representa el id del tipo de clase a buscar
         * Retorno: tipoclase
         *          null
         */
        public tipo_clase ObtenerTipoClasesPorID(int pIDTipoClase)
        {
            GestorTipoClase gestorTipoClase = new GestorTipoClase();
            return gestorTipoClase.ObtenerTipoClasePorID(pIDTipoClase);
        }

        /*
         * Método que devuelve un listado de todas las academias cargadas
         * Retorno: List<academia>
         *          Retorna toda lista de academias
         */
        public List<academia> ObtenerAcademias()
        {
            GestorAcademias gestorAcademias = new GestorAcademias();
            return gestorAcademias.ObtenerAcademias();
        }

        /*
         * Método que devuelve una academia segun su id
         * Parametros: pIDAcademia: entero que representa el id del tipo de clase a buscar
         * Retorno: academia
         *          null
         */
        public academia ObtenerAcademiasPorID(int pIDAcademia)
        {
            GestorAcademias gestorAcademias = new GestorAcademias();
            return gestorAcademias.ObtenerAcademiasPorID(pIDAcademia);
        }

        /*
         * Método que devuelve un listado de todas los profesores cargadas
         * Retorno: List<profesor>
         *          Retorna toda lista de profesores
         */
        public List<profesor> ObtenerProfesores()
        {
            GestorProfesores gestorProfesores = new GestorProfesores();
            return gestorProfesores.ObtenerProfesores();
        }


        /*
         * Método que devuelve un profe segun su id
         * Parametros: pIDProfe: entero que representa el id del tipo de clase a buscar
         * Retorno: profe
         *          null
         */
        public profesor ObtenerProfesorPorID(int pIDProfe)
        {
            GestorProfesores gestorProfesores = new GestorProfesores();
            return gestorProfesores.ObtenerProfesorPorID(pIDProfe);
        }

        /*
         * Metodo que valida si existe una clase en el mismo horario que se quiere agregar
         * Parametros:  pDTHorarios: DataTable contiene los dias y horarios que se quieren agregar
         *              pIdUbicacion: entero que representa el ID de la ubicacion de la clase
         * Retornos: boolean    false: ya existe una clase en ese horario
         *                      true: los horarios estan todos disponibles
         * 
         */
        public Boolean validarDisponibilidadHorario(DataTable pDTHorarios, int pIdUbicacion, int pidClase)
        {
            using (var db = new JJSSEntities())
            {

                for (int j = 0; j < pDTHorarios.Rows.Count; j++)
                {
                    int dia = int.Parse(pDTHorarios.Rows[j]["dia"].ToString());
                    var claseEncontrada = from clase in db.clase
                                          join hora in db.horario on clase.id_clase equals hora.id_clase
                                          where clase.id_ubicacion == pIdUbicacion && hora.dia == dia
                                          select new ClaseHorario()
                                          {
                                              desde = hora.hora_desde,
                                              hasta = hora.hora_hasta,
                                              id = clase.id_clase
                                          };

                    var claseEncontradaHorarios = claseEncontrada.ToList();

                    foreach (var horario in claseEncontradaHorarios)
                    {
                        if (horario.id == pidClase) break;
                        if (horario.desde.CompareTo(pDTHorarios.Rows[j]["hora_desde"].ToString()) == 0) return false;
                        if (horario.desde.CompareTo(pDTHorarios.Rows[j]["hora_desde"].ToString()) < 0 && horario.hasta.CompareTo(pDTHorarios.Rows[j]["hora_desde"].ToString()) > 0) return false;
                        if (horario.hasta.CompareTo(pDTHorarios.Rows[j]["hora_hasta"].ToString()) == 0) return false;
                        if (horario.desde.CompareTo(pDTHorarios.Rows[j]["hora_hasta"].ToString()) < 0 && horario.hasta.CompareTo(pDTHorarios.Rows[j]["hora_hasta"].ToString()) > 0) return false;
                        if (horario.desde.CompareTo(pDTHorarios.Rows[j]["hora_desde"].ToString()) > 0 && horario.hasta.CompareTo(pDTHorarios.Rows[j]["hora_hasta"].ToString()) < 0) return false;
                    }
                }
            }
            return true;
        }

        /*
         * Metodo que devuelve el valor de recargo a pagar segun el dia de inscripcion
         * Parametros:  pIdClase : entero - id de la clase a la que esta inscripto el alumno
         *              pIdAlumno : entero - id del alumno
         * Retorno: double >=0 - representa el valor del recargo 
         *                  <= no esta inscripto a esa clase
         * 
         */
        public double calcularRecargo(int pIdClase, int pIdAlumno)
        {
            inscripcion_clase inscripcionDelAlumno;
            DateTime diaMaximo;
            double recargo = 0;
            using (var db = new JJSSEntities())
            {
                //{
                //    GestorInscripcionesClase ins = new GestorInscripcionesClase();
                //    inscripcionDelAlumno = ins.ObtenerAlumnoInscripto(pIdAlumno, pIdClase);
                //    var recargoParametro = db.parametro.Find(1);
                //    recargo = (double)recargoParametro.valor;
                //}
                //if (inscripcionDelAlumno == null)
                //{
                //    return -1;
                //}
                //else
                //{
                //    DateTime fechaInscripcion = (DateTime)inscripcionDelAlumno.fecha;

                //    diaMaximo = fechaInscripcion.AddDays(5);

                //    if (diaMaximo < DateTime.Today) // se cobra recargo
                //    {
                //        return recargo;
                //    }
                //}

                var recargoParametro = db.parametro.Find(1);
                recargo = (double)recargoParametro.valor;
                clase clase = db.clase.Find(pIdClase);
                GestorPagoClase gestorPago = new GestorPagoClase();
                bool pago = gestorPago.validarPagoParaAsistencia(pIdAlumno, (int)clase.id_tipo_clase);
                if (pago) return 0;
                else return recargo;

            }
            return 0;
        }


        /*
         * Metodo que devuelve el ID de la clase que se está dictando en el momento actual segun la academia
         * Parametros:  pIdUbicacion : entero - ID de la ubicacion de la clase
         * Retorno:     entero - 0: no se esta dando ninguna clase
         *                      >0 : ID de la clase actual
         */
        public ClasesHorariosAsistencia buscarClaseSegunHoraActual(int pIdUbicacion)
        {

            CultureInfo ci = new CultureInfo("Es-Es");



            TimeSpan horaActual = DateTime.Now.TimeOfDay;

            string nombreDia = ci.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);

            DataTable dtClases;
            List<ClasesHorariosAsistencia> listaClases;

            using (var db = new JJSSEntities())
            {
                var claseEncontrada = from clase in db.clase
                                      join horario in db.horario on clase.id_clase equals horario.id_clase
                                      join tipo in db.tipo_clase on clase.id_tipo_clase equals tipo.id_tipo_clase
                                      where clase.id_ubicacion == pIdUbicacion
                                      && horario.nombre_dia == nombreDia
                                      select new ClasesHorariosAsistencia()
                                      {
                                          idHorario = horario.id_horario,
                                          idClase = clase.id_clase,
                                          dia = horario.nombre_dia,
                                          desde = horario.hora_desde,
                                          hasta = horario.hora_hasta,
                                          tipoClase = tipo.id_tipo_clase,

                                      };
                //dtClases = modUtilidadesTablas.ToDataTable(claseEncontrada.ToList());
                listaClases = claseEncontrada.ToList();
            }

            ClasesHorariosAsistencia claseActual = null;

            foreach (var clase in listaClases)
            {
                TimeSpan horaDesde = TimeSpan.Parse(clase.desde);
                if (horaActual > horaDesde.Subtract(TimeSpan.FromMinutes(15)) && horaActual <= horaDesde.Add(TimeSpan.FromMinutes(45)))
                {
                    claseActual = clase;
                    break;
                }

            }


            //int idClase = 0;
            //int idHorario = 0;
            //for (int i = 0; i < dtClases.Rows.Count; i++)
            //{
            //    DataRow dr = dtClases.Rows[i];

            //    TimeSpan horaDesde = TimeSpan.Parse( dr["desde"].ToString());
            //    if (horaActual > horaDesde.Subtract(TimeSpan.FromMinutes(15)) && horaActual<= horaDesde.Add(TimeSpan.FromMinutes(45)))
            //    {
            //        idHorario = int.Parse(dr["idHorario"].ToString());
            //        break;
            //    }
            //}

            return claseActual;

        }


        /*
         * Metodo que obtiene una lista de clases a las que esta inscripto un alumno
         * Parametros: pIdAlumno: entero- representa el ID del alumno
         * Retornos: List<clase> - lista de las clases
         * 
         */
        public List<clase> ObtenerClaseSegunAlumno(int pIdAlumno)
        {
            using (var db = new JJSSEntities())
            {
                var claseSegunAlumno = from clase in db.clase
                                       join ins in db.inscripcion_clase on clase.id_clase equals ins.id_clase
                                       where ins.id_alumno == pIdAlumno
                                       select clase;
                return claseSegunAlumno.ToList();
            }
        }


        /*
         * Metodo que obtiene una lista de clases a las que esta inscripto un alumno
         * Parametros: pIdAlumno: entero- representa el ID del alumno
         * Retornos: List<clase> - lista de las clases
         * 
         */
        public List<ClasesDisponibles> ObtenerClaseSegunAlumnoGrilla(int pIdAlumno)
        {
            using (var db = new JJSSEntities())
            {
                var clasesDisponibles = from clases in db.clase
                                        join ubic in db.academia on clases.id_ubicacion equals ubic.id_academia
                                        join tipo in db.tipo_clase on clases.id_tipo_clase equals tipo.id_tipo_clase
                                        join profe in db.profesor on clases.id_profe equals profe.id_profesor
                                        join ins in db.inscripcion_clase on clases.id_clase equals ins.id_clase
                                        where clases.baja_logica == 1
                                          && ins.id_alumno == pIdAlumno
                                        orderby clases.nombre
                                        select new ClasesDisponibles
                                        {
                                            id_clase = clases.id_clase,
                                            nombre = clases.nombre,
                                            precio = clases.precio,
                                            tipo_clase = tipo.nombre,
                                            ubicacion = ubic.nombre,
                                            profesor = profe.nombre + " " + profe.apellido,

                                        };
                List<ClasesDisponibles> claseLista = clasesDisponibles.ToList();

                return claseLista;
            }
        }


        public bool ClaseTieneInscriptos(int id)
        {
            using (var db = new JJSSEntities())
            {
                var clase = db.clase.Find(id);
                if (clase!=null && clase.inscripcion_clase.ToList().Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

    }
}
