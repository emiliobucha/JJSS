using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio
{
   public  class GestorParticipantes
    {
        /*
         * Método que nos permite obtener un participante a un torneo mediante su DNI
         * Parámetros:
         *              pDni: entero que representa el dni del participant
         * Retornos: participante
         *          Retorna el participante buscado por DNI
         */
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
