using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data.Entity;
using JJSS_Negocio.Administracion;

namespace JJSS_Negocio.Administracion
{   /*
    * Clase para gestionar sedes de los torneos
    */
    public class GestorSedes
    {
        /*Genera una nueva sede para el torneo
         * Parametros: 
         *              pNombre : String que indica el nombre de la nueva sede
         *              pDireccion: String que indica la direccion de esta nueva sede
         */
        public String GenerarNuevaSede(string pNombre, direccion pDireccion, long? pTelefono)
        {
            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    direccion nuevaDireccion = new direccion()
                    {
                        barrio = pDireccion.barrio,
                        calle = pDireccion.calle,
                        departamento = pDireccion.departamento,
                        id_ciudad = pDireccion.id_ciudad,
                        numero = pDireccion.numero,
                        piso = pDireccion.piso,
                        torre = pDireccion.torre,
                    };
                    db.direccion.Add(nuevaDireccion);
                    db.SaveChanges();

                    sede nuevaSede = new sede()
                    {
                        nombre = pNombre,
                        direccion = nuevaDireccion,
                        telefono = pTelefono,
                        actual = Constantes.ConstatesBajaLogica.ACTUAL,
                    };

                    db.sede.Add(nuevaSede);

                    /*Acá puede ser asincrono, asi que puede quedar guardandose y seguir ejecutandose lo demas */

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
         * Busca una sede por ID
         * Parametros: ID de la sede
         * Retorno: Entidad sede escogida
         */
        public sede BuscarSedePorID(int? pID)
        {
            sede sedeEncontrada = null;
            using (var db = new JJSSEntities())
            {
                sedeEncontrada = db.sede.Find(pID);
            }
            return sedeEncontrada;
        }

        /*
         * Lista todos los torneos que posee una sede
         * Parametros: ID de la sede 
         * Retorno: Lista de Torneos los cuales tienen su participacion en esa sede
         */
        public List<torneo> ObtenerTorneoEnSede(int pID)
        {
            sede sedeEncontrada = null;
            using (var db = new JJSSEntities())
            {
                sedeEncontrada = db.sede.Find(pID);
            }
            return sedeEncontrada.torneo.ToList();
        }



        /*
         * Obtenemos todas las sedes
         * Retorno: Listado total de todas las sedes
         */
        public List<sede> ObtenerSedes()
        {
            using (var db = new JJSSEntities())
            {
                var sede = from s in db.sede
                           where s.actual == Constantes.ConstatesBajaLogica.ACTUAL
                           select s;
                return sede.ToList();
            }
        }


        public String ObtenerDireccion(int? pID)
        {
            String direccion = "";
            using (var db = new JJSSEntities())
            {
                sede sedeEncontrada = db.sede.Find(pID);
                // direccion = sedeEncontrada.direccion.calle + " " + sedeEncontrada.direccion.numero;
            }
            return direccion;
        }

        public Resultados.SedeDireccion ObtenerDireccionSede(int pIDSede)
        {
            using (var db = new JJSSEntities())
            {
                var sede = from s in db.sede
                           where s.id_sede == pIDSede
                           select new Resultados.SedeDireccion()
                           {
                               sede = s.nombre,
                               calle = s.direccion.calle,
                               numero = (int)s.direccion.numero,
                               ciudad = s.direccion.ciudad.nombre,
                               provincia = s.direccion.ciudad.provincia.nombre,
                               pais = s.direccion.ciudad.provincia.pais.nombre,
                               barrio = s.direccion.barrio,
                           };
                return sede.FirstOrDefault();
            }
        }

        public Resultados.DireccionAlumno ObtenerDireccionSedeCompleta(int pID)
        {
            using (var db = new JJSSEntities())
            {
                var sede = from s in db.sede
                           where s.id_sede == pID
                           select new Resultados.DireccionAlumno()
                           {
                               depto = s.direccion.departamento,
                               piso = s.direccion.piso,
                               idCiudad = s.direccion.id_ciudad,
                               idProvincia = s.direccion.ciudad.id_provincia,
                               torre = s.direccion.torre,
                               calle = s.direccion.calle,
                               numero = (int)s.direccion.numero,
                               barrio = s.direccion.barrio
                           };
                return sede.FirstOrDefault();
            }
        }

        public string ModificarSede(sede pSede)
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    sede sedeSeleccionada = db.sede.Find(pSede.id_sede);
                    if (sedeSeleccionada == null) return "No existe esa sede";
                    if (sedeSeleccionada != pSede)
                    {
                        sedeSeleccionada.nombre = pSede.nombre;
                        sedeSeleccionada.direccion = pSede.direccion;
                        sedeSeleccionada.telefono = pSede.telefono;

                        db.SaveChanges();
                        transaction.Commit();
                    }

                    return "";
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    return ex.Message;
                }
            }
        }

        public string EliminarSede(int pID)
        {
            try
            {
                using (var db = new JJSSEntities())
                {
                    sede sedeAeliminar = db.sede.Find(pID);
                    sedeAeliminar.actual = Constantes.ConstatesBajaLogica.NO_ACTUAL;
                    db.SaveChanges();

                    return "";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public List<Resultados.SedeDireccion> ObtenerSedesConFiltro(string filtroNombre, string filtroCiudad)
        {
            if (filtroCiudad.CompareTo("Todos") == 0) filtroCiudad = "";
            using (var db = new JJSSEntities())
            {
                List<Resultados.SedeDireccion> sedes = (from se in db.sede
                                                        where se.actual == Constantes.ConstatesBajaLogica.ACTUAL
                                                        && se.nombre.StartsWith(filtroNombre)
                                                        && se.direccion.ciudad.nombre.StartsWith(filtroCiudad)
                                                        select new Resultados.SedeDireccion()
                                                        {
                                                            calle = se.direccion.calle,
                                                            numero = se.direccion.numero.Value,
                                                            ciudad = se.direccion.ciudad.nombre,
                                                            sede = se.nombre,
                                                            idSede = se.id_sede,
                                                            telefono = se.telefono,
                                                            barrio = se.direccion.barrio
                                                        }).ToList();
                return sedes;
            }
        }

        public List<ciudad> ObtenerCiudades()
        {
            using (var db = new JJSSEntities())
            {
                return db.ciudad.ToList();
            }
        }
    }
}
