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

        public int buscarClaseSegunHoraActual(int pIdUbicacion)
        {
            gestorClases = new GestorClases();
            return gestorClases.buscarClaseSegunHoraActual(pIdUbicacion);
        }

        public string validarInscripcionAClase(int pIdAlumno, int pIdClase)
        {
            gestorIns = new GestorInscripcionesClase();
            if (gestorIns.ObtenerAlumnoInscripto(pIdAlumno, pIdClase) == null) return "El alumno no está inscripto a esa clase";
            return "";
        }

    }
}
