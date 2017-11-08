﻿using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using JJSS_Negocio;
using System.Data.Entity;
using JJSS_Negocio.Resultados;

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
        public ClasesHorariosAsistencia buscarClaseSegunHoraActual(int pIdUbicacion)
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
        public string registrarAsistencia(int pIdAlumno, int pIDClase, int pIdHorario)
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
                        fecha_hora = fechaActual,
                        id_horario = pIdHorario
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


        public List<ListadoAsistencia> ListadoAsistentes(int pIdHorario, DateTime? pFecha)
        {
            using (var db = new JJSSEntities())
            {
                var horario = db.horario.FirstOrDefault(x => x.id_horario == pIdHorario);


                var participantes = from asis in db.asistencia_clase
                                    where asis.id_clase == horario.id_clase && DbFunctions.TruncateTime(asis.fecha_hora) == DbFunctions.TruncateTime(pFecha)
                                    && asis.id_horario == horario.id_horario
                                    select new ListadoAsistencia()
                                    {
                                        horario_nombre = horario.nombre_dia +  " / " + horario.hora_desde + " - " + horario.hora_hasta,
                                        cla_fechaD = DbFunctions.TruncateTime(asis.fecha_hora),
                                        cla_nombre = asis.clase.nombre,
                                        cla_profesor = asis.clase.profesor.apellido + " " + asis.clase.profesor.nombre,
                                        cla_tipo = asis.clase.tipo_clase.nombre,
                                        alu_apellido = asis.alumno.apellido,
                                        alu_dni = asis.alumno.dni.ToString(),
                                        alu_nombre = asis.alumno.nombre,
                                        alu_sexoI = asis.alumno.sexo,
                                        alu_telefono = asis.alumno.telefono.ToString(),
                                        alu_horaT = DbFunctions.CreateTime(asis.fecha_hora.Hour, asis.fecha_hora.Minute, 0)


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


        public string GenerarListado(int pID, DateTime pFechaDateTime)
        {

            int id = pID;

            GestorReportes gestorReportes = new GestorReportes();
            //torneo torneoAListar = BuscarTorneoPorID(pID);
            //GestorSedes gestorSedes = new GestorSedes();
            //gestorSedes.BuscarSedePorID(torneoAListar.id_sede);
            //gestorSedes.ObtenerDireccion(torneoAListar.id_sede);
            List<ListadoAsistencia> listado = ListadoAsistentes(id, pFechaDateTime);

            if (listado.Count == 0)
                throw new Exception("No asistió ningún alumno a la clase");

            return gestorReportes.GenerarReporteListadoAsistentes(listado);


        }

        //+SOLO MIRA LA ASISTENCIA DE ESE DIA PARA CUALQUIER LUGAR
        public asistencia_clase ValidarAsistenciaAnterior(int pIDAlumno)
        {
            DateTime fechaActual = DateTime.Now;
            using (var db = new JJSSEntities())
            {
                asistencia_clase asistencia = (from a in db.asistencia_clase
                                 where a.id_alumno == pIDAlumno 
                                 orderby a.fecha_hora descending
                                 select a).FirstOrDefault();

                if (asistencia != null)
                {
                    if (asistencia.fecha_hora.Date == fechaActual.Date) return asistencia;
                    else return null;
                }
                else return null;
            }
        }

    }
}
