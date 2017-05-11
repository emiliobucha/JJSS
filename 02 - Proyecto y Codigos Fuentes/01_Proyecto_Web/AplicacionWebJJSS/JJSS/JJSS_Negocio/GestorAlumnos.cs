using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    class GestorAlumnos
    {
        public alumno ObtenerAlumnoPorDNI(int pDni)
        {
            using (var db = new JJSSEntities())
            {
                var alumnoEncontrado = from alu in db.alumno
                                       where alu.dni == pDni
                                       select alu;
                return alumnoEncontrado.FirstOrDefault();            
            }
        }
    }
}
