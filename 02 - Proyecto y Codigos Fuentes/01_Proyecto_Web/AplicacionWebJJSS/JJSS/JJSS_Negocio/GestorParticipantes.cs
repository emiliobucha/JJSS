using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
    class GestorParticipantes
    {
        public participante ObtenerParticipantePorDNI(int pDni)
        {
            using (var db = new JJSSEntities())
            {
                var participanteEncontrado = from part in db.participante
                                       where part.dni == pDni
                                       select part;
                return participanteEncontrado.FirstOrDefault();            
            }
        }
    }
}
