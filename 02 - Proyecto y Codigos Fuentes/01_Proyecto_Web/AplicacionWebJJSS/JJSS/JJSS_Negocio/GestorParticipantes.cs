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

        public participante obtenerParticipantePorId(int pId)
        {
            using ( var db = new JJSSEntities())
            {
                return db.participante.Find(pId);
            }
        }

        public String crearParticipante(String pNombre, String pApellido, short pSexo, DateTime pFechaNacimiento, int pDni, int? pAlumno)
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    participante nuevoParticipante;

                    nuevoParticipante = new participante()
                    {
                        nombre = pNombre,
                        apellido = pApellido,
                        sexo = pSexo,
                        fecha_nacimiento = pFechaNacimiento,
                        dni = pDni,
                        id_alumno = pAlumno
                    };
                    db.participante.Add(nuevoParticipante);
                    db.SaveChanges();
                    transaction.Commit();
                    return "";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }
            }
        }
    }
}
