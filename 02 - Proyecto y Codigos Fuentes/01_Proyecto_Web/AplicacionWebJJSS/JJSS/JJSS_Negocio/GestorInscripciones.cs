using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;

namespace JJSS_Negocio
{
    public class GestorInscripciones
    {
        public void InscribirATorneo(int pTorneo, string pHora, DateTime pFecha, long pCodigoBarra, int pParticipante, int pCategoriaTorneo)
        {
            inscripcion nuevaInscripcion = new inscripcion()
            {
                id_categoria_torneo = pCategoriaTorneo,
                id_torneo = pTorneo,
                hora = pHora,
                fecha = pFecha,
                codigo_barra = pCodigoBarra,
                id_participante = pParticipante

            };

            using (var db = new JJSSEntities()) {
                db.inscripcion.Add(nuevaInscripcion);
                db.SaveChanges();
            }
        }


    }
}
