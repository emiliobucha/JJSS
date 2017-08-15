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

        public int buscarTipoClaseSegunHoraActual(int pIdUbicacion)
        {
            gestorClases = new GestorClases();
            return gestorClases.buscarTipoClaseSegunHoraActual(pIdUbicacion);
        }

        public string ValidarTipoClaseAlumno(int pIdAlumno, int pIdTipoClase)
        {
            gestorIns = new GestorInscripcionesClase();
            if (gestorIns.ValidarTipoClaseAlumno(pIdAlumno, pIdTipoClase) == false) return "El alumno no está inscripto a esa clase";
            return "";
        }

        public Boolean ValidarPagoParaAsistencia(int pIdAlumno, int pIdTipoClase)
        {
            gestorPago = new GestorPagoClase();
            return gestorPago.validarPagoParaAsistencia(pIdAlumno, pIdTipoClase);
        }

        public string registrarAsistencia(int pIdAlumno,int pIdTipoClase)
        {
            DateTime fechaActual = DateTime.Today;
            try
            {
                using (var db= new JJSSEntities())
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
