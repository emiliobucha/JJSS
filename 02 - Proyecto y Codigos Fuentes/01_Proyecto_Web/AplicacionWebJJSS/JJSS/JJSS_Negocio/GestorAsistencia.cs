using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using JJSS_Negocio;

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
        public int buscarTipoClaseSegunHoraActual(int pIdUbicacion)
        {
            gestorClases = new GestorClases();
            return gestorClases.buscarTipoClaseSegunHoraActual(pIdUbicacion);
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
        public string registrarAsistencia(int pIdAlumno, int pIdTipoClase)
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
                        id_tipo_clase = pIdTipoClase,
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

    }
}
