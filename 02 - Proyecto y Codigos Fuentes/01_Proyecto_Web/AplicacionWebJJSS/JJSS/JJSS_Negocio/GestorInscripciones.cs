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
        public string InscribirATorneo(int pTorneo, string pNombre, string pApellido, float pPeso, int pEdad, int pFaja, short pSexo)
        {

            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    //Foraneos
                    torneo torneoInscripto = db.torneo.Find(pTorneo);

                    faja fajaElegida = db.faja.Find(pFaja);

                    var cat =
                        from categoria in db.categoria
                        where (categoria.edad_desde <= pEdad)
                        && (categoria.edad_hasta > pEdad)
                        && (categoria.peso_desde <= pPeso)
                        && (categoria.peso_hasta > pPeso)
                        && (categoria.sexo == pSexo)
                        select categoria;

                    categoria categoriaPerteneciente = cat.First();

                    //Nuevos
                    //+Reveer la edad
                    participante nuevoParticipante = new participante()
                    {
                        nombre = pNombre,
                        apellido = pApellido,
                        peso = pPeso,
                        faja = fajaElegida,
                        sexo = pSexo,
                        fecha_nacimiento =  new DateTime(DateTime.Now.Year - pEdad,1,1) //Invento fecha de nacimiento con la edad que le pasamos por parametro

                    };

                    

                    categoria_torneo nuevaCategoriaTorneo = new categoria_torneo()
                    {
                        categoria = categoriaPerteneciente,
                        faja = fajaElegida,
                        sexo = pSexo,

                    };

                    //+Reveer la hora
                    inscripcion nuevaInscripcion = new inscripcion()
                    {
                        hora = "12:00",
                        fecha = DateTime.Now.Date,
                        codigo_barra = 123456789,
                        participante=nuevoParticipante,
                        torneo = torneoInscripto,
                        categoria_torneo = nuevaCategoriaTorneo

                    };

                    db.participante.Add(nuevoParticipante);
                    db.categoria_torneo.Add(nuevaCategoriaTorneo);
                    db.inscripcion.Add(nuevaInscripcion);
                    db.SaveChanges();
                    transaction.Commit();
                    return sReturn;
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    return ex.Message;
                }
            }
        }

    


    public List<torneo> ObtenerTorneos()
        {
            GestorTorneos gestorTorneos = new GestorTorneos();
            return gestorTorneos.ObtenerTorneos();
        }

        public List<faja> ObtenerFajas()
        {
            using (var db = new JJSSEntities())
            {
                return db.faja.ToList();
            }
        }
    }
}
