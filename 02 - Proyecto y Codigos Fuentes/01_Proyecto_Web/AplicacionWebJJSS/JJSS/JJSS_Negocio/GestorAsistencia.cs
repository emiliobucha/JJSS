using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using JJSS_Negocio;
using System.Data.Entity;

namespace JJSS_Negocio
{
    public class GestorAsistencia
    {
        private GestorClases gestorClases;
        private GestorInscripcionesClase gestorIns;
        private GestorPagoClase gestorPago;

        /*
         * Metodo que devuelve el ID de la clase que se está dictando en el momento actual segun la academia
         * Parametros:  pIdUbicacion : entero - ID de la ubicacion de la clase
         * Retorno:     entero - 0: no se esta dando ninguna clase
         *                      >0 : ID de la clase actual
         */
        public clase buscarClaseSegunHoraActual(int pIdUbicacion)
        {
            gestorClases = new GestorClases();
            return gestorClases.buscarClaseSegunHoraActual(pIdUbicacion);
        }

        /*
         * Metodo que valida si el alumno esta inscripto a ese tipo de clase
         * Parametros:  pIdAlumno: entero - ID del alumno
         *              pIdTipoClase: entero - ID tipo clase
         * Retorno: "": el alumno está inscripto en la clase
         *          El alumno no está inscripto a esa clase
         * 
         */
        public string ValidarTipoClaseAlumno(int pIdAlumno, int pIdTipoClase)
        {
            gestorIns = new GestorInscripcionesClase();
            if (gestorIns.ValidarTipoClaseAlumno(pIdAlumno, pIdTipoClase) == false) return "El alumno no está inscripto a esa clase";
            return "";
        }

        /*
        * Metodo que valida si un alumno pago para asistir a clases
        * Parametros : pIdAlumno entero que representa el id del alumno
        *              pIDTipoClase entero que representa el id del tipo de clase
        * Retornos : true - puede asistir
        *              false - no puede asistir
        * 
        */
        public Boolean ValidarPagoParaAsistencia(int pIdAlumno, int pIdTipoClase)
        {
            gestorPago = new GestorPagoClase();
            return gestorPago.validarPagoParaAsistencia(pIdAlumno, pIdTipoClase);
        }

        /*
         * Metodo que registra una asistencia a una clase
         * Parametros: pIdAlumno entero que representa el id del alumno
         *              pIDTipoClase entero que representa el id del tipo de clase
         * Retornos: "" Transaccion correcta de la BD
         *           ex.Message: mensaje de error en la BD
         */
        public string registrarAsistencia(int pIdAlumno, int pIDClase)
        {
            DateTime fechaActual = DateTime.Now;
            try
            {
                using (var db = new JJSSEntities())
                {

                    asistencia_clase nuevaAsistencia;
                    nuevaAsistencia = new asistencia_clase
                    {
                        id_alumno = pIdAlumno,
                        id_clase = pIDClase,
                        fecha_hora = fechaActual
                    };
                    db.asistencia_clase.Add(nuevaAsistencia);
                    db.SaveChanges();
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public List<ListadoAsistencia> ListadoAsistentes(int pIdClase, DateTime? pFecha)
        {
            using (var db = new JJSSEntities())
            {
                var participantes = from asis in db.asistencia_clase
                                    where asis.id_clase == pIdClase && DbFunctions.TruncateTime(asis.fecha_hora) == DbFunctions.TruncateTime(pFecha)
                                    select new ListadoAsistencia()
                                    {
                                      cla_fechaD = DbFunctions.TruncateTime(asis.fecha_hora),
                                      cla_nombre = asis.clase.nombre,
                                      cla_profesor = asis.clase.profesor.apellido + " " + asis.clase.profesor.nombre,
                                      cla_tipo = asis.clase.tipo_clase.nombre,
                                      alu_apellido = asis.alumno.apellido,
                                      alu_dni = asis.alumno.dni.ToString(),
                                      alu_nombre = asis.alumno.nombre,
                                      alu_sexoI = asis.alumno.sexo,
                                      alu_telefono = asis.alumno.telefono.ToString(),
                                      alu_horaT = DbFunctions.CreateTime(asis.fecha_hora.Hour, asis.fecha_hora.Minute,0),
                                      

                                    };
                List<ListadoAsistencia> asistenciaList = participantes.ToList();


                foreach (ListadoAsistencia asistencia in asistenciaList)
                {
                    if (asistencia.alu_sexoI == 1)
                    {
                        asistencia.alu_sexo = "M";

                    }
                    else
                    {
                        asistencia.alu_sexo = "F";
                    }
                    asistencia.cla_fecha = asistencia.cla_fechaD?.ToString("dd/MM/yyyy") ?? " - ";
                    asistencia.alu_hora = asistencia.alu_horaT?.ToString(@"hh\:mm") ?? " - ";

                }


                return asistenciaList;
            }
        }


        public String GenerarListado(int pID, DateTime pFechaDateTime)
        {
            GestorReportes gestorReportes = new GestorReportes();
            //torneo torneoAListar = BuscarTorneoPorID(pID);
            //GestorSedes gestorSedes = new GestorSedes();
            //gestorSedes.BuscarSedePorID(torneoAListar.id_sede);
            //gestorSedes.ObtenerDireccion(torneoAListar.id_sede);
            List<ListadoAsistencia> listado = ListadoAsistentes(pID, pFechaDateTime);

            if (listado.Count == 0)
                throw new Exception("No asistió ningún alumno a la clase");

            return gestorReportes.GenerarReporteListadoAsistentes(listado);


        }


    }
}
