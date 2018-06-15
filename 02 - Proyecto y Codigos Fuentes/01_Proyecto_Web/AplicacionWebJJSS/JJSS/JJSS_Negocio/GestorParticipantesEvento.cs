using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
   public  class GestorParticipantesEvento
    {
        /*
         * Método que nos permite obtener un participante a un torneo mediante su DNI
         * Parámetros:
         *              pDni: entero que representa el dni del participant
         * Retornos: participante
         *          Retorna el participante buscado por DNI
         */
        public participante_evento ObtenerParticipantePorDNI(int pTipo, string pDni)
        {
            using (var db = new JJSSEntities())
            {
                var participanteEncontrado = from part in db.participante_evento
                                             where part.dni == pDni && part.id_tipo_documento == pTipo
                                       select part;
                return participanteEncontrado.FirstOrDefault();            
            }
        }

        public participante_evento obtenerParticipantePorId(int pId)
        {
            using ( var db = new JJSSEntities())
            {
                return db.participante_evento.Find(pId);
            }
        }
    }
}
