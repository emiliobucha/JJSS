using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;

namespace JJSS_Negocio
{
    /*Clase que se encarga de gestionar las inscripciones a Torneos*/
    public class GestorInscripciones
    {
        /*Método que permite crear un objeto de Entidad de la clase Inscripción
         * Como asi tambien genera una nueva categoria si esta no estaba 
         * Y si el participante no estaba ya inscripto
         * 
         * Parametros: 
         *              pTorneo : Entero id del torneo a inscribir
         *              pNombre : String nombre del participante
         *              pApellido : String apellido del participante
         *              pPeso : Float peso del participante
         *              pEdad : Entero edad del participante
         *              pFaja : Entero id de la faja que posee el participante
         *              pSexo : Short 0 Mujer 1 Hombre
         *  Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              Participante ya inscripto
         *          
         * 
         */
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
                    var participanteExistente = from part in db.participante
                                                         where (part.nombre == pNombre)
                                                         && (part.apellido == pApellido)
                                                         && (part.peso == pPeso)
                                                         select part;

                    if (participanteExistente.Count() > 0)
                    {
                        return "Participante exitente";
                    }

                    participante nuevoParticipante = new participante()
                    {
                        nombre = pNombre,
                        apellido = pApellido,
                        peso = pPeso,
                        faja = fajaElegida,
                        sexo = pSexo,
                        fecha_nacimiento =  new DateTime(DateTime.Now.Year - pEdad,1,1) //Invento fecha de nacimiento con la edad que le pasamos por parametro

                    };

                    var catTorneoExistente = from catTor in db.categoria_torneo
                                             where (catTor.categoria == categoriaPerteneciente)
                                             && (catTor.faja == fajaElegida)
                                             && (catTor.sexo == pSexo)
                                             select catTor;
                    categoria_torneo nuevaCategoriaTorneo;
                    if (catTorneoExistente.Count() == 0)

                    {
                        nuevaCategoriaTorneo = new categoria_torneo()
                        {
                            categoria = categoriaPerteneciente,
                            faja = fajaElegida,
                            sexo = pSexo,

                        };
                    }
                    else
                    {
                        nuevaCategoriaTorneo = catTorneoExistente.First();
                    }
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

    
        /*
         * Metodo que nos permite bajar el acoplamiente y obtener los diferentes torneos para el momento de inscribirse
         */
        public List<torneo> ObtenerTorneos()
        {
            GestorTorneos gestorTorneos = new GestorTorneos();
            return gestorTorneos.ObtenerTorneos();
        }

        /*
         * Metodo que nos devuelve una lista de fajas para luego ser seleccionadas al momento de inscribirse
         */

        public List<faja> ObtenerFajas()
        {
            using (var db = new JJSSEntities())
            {
                return db.faja.ToList();
            }
        }
    }
}
