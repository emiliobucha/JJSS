﻿using System;
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
        private alumno alumnoExistente;

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
         *              pDni : 
         *  Retornos: String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              Participante ya inscripto
         *          
         * 
         */
        public string InscribirATorneo(int pTorneo, string pNombre, string pApellido, double pPeso, int pEdad, int pFaja, short pSexo, int pDni, int? pIDAlumno)
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
                   

                    if (ObtenerParticipanteporDNI(pDni) != null)
                    {
                        return "Participante exitente";
                    }

                   //alumnoExistente = ObtenerAlumnoPorDNI(pDni);

                    participante nuevoParticipante;




                    nuevoParticipante = new participante()
                    {
                        nombre = pNombre,
                        apellido = pApellido,
                        //peso = pPeso,
                        faja = fajaElegida,
                        id_faja = fajaElegida.id_faja,
                        sexo = pSexo,
                        fecha_nacimiento = new DateTime(DateTime.Now.Year - pEdad, 1, 1), //Invento fecha de nacimiento con la edad que le pasamos por parametro
                        dni = pDni,
                        id_alumno=pIDAlumno

                    };
                    db.participante.Add(nuevoParticipante);
                    db.SaveChanges();
                    var catTorneoExistente = from catTor in db.categoria_torneo
                                             where (catTor.id_categoria == categoriaPerteneciente.id_categoria)
                                             && (catTor.faja.id_faja == fajaElegida.id_faja)
                                             && (catTor.sexo == pSexo)
                                             select catTor;
                    categoria_torneo nuevaCategoriaTorneo;
                    //+rever esto
                    if (catTorneoExistente.Count() == 0)

                    {
                        nuevaCategoriaTorneo = new categoria_torneo()
                        {
                            id_categoria = categoriaPerteneciente.id_categoria,
                            faja = fajaElegida,
                            sexo = pSexo,

                        };
                    }
                    else
                    {
                        nuevaCategoriaTorneo = catTorneoExistente.First();
                    }
                    db.categoria_torneo.Add(nuevaCategoriaTorneo);
                    db.SaveChanges();

                    string hora = hora = DateTime.Now.ToString("hh:mm tt");
                    DateTime fecha = DateTime.Now.Date;
                    inscripcion nuevaInscripcion = new inscripcion()
                    {
                        
                        hora = hora,
                        fecha = fecha,
                        codigo_barra = 123456789,
                        participante = nuevoParticipante,
                        id_participante = nuevoParticipante.id_participante,
                        id_torneo = torneoInscripto.id_torneo,
                        torneo = torneoInscripto,
                        categoria_torneo = nuevaCategoriaTorneo,
                        id_categoria_torneo = nuevaCategoriaTorneo.id_categoria_torneo,
                        peso = pPeso

                    };

                   
                  
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

        public alumno ObtenerAlumnoPorDNI(int pDni)
        {
            GestorAlumnos gestorAlumnos = new GestorAlumnos();

            return  gestorAlumnos.ObtenerAlumnoPorDNI(pDni);
        }

        /*
         * Método que busca un participante por DNI, permite bajar el acoplamiente delegando la tarea a su gestor correspondiente
         * Parámetros:
         *              pDni: entero que representa el dni a buscar
         */

        public participante ObtenerParticipanteporDNI(int pDni)
        {
            GestorParticipantes gestorParticipantes = new GestorParticipantes();
            return gestorParticipantes.ObtenerParticipantePorDNI(pDni);
        }


        /*
         * Método que calcula la edad
         * Paramétro: 
         *          pFechaNacimiento: Datetime, que puede ser nulo, que representa la fecha de nacimiento del participante a inscribirse
         */
        public string calcularEdad(DateTime? pFechaNacimiento)
        {
            //+habria que ver como calcularla mejor porque si estuviera calculando mi edad daria 22 pero todavia tengo 21
            DateTime fechaNac = Convert.ToDateTime(pFechaNacimiento);
            int edad = DateTime.Today.Year - fechaNac.Year;
            return edad.ToString();
        }

    }
}
