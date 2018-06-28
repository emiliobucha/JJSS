using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio.Administracion
{
    public class GestorAcademias
    {
        /*
         * Método que devuelve un listado de todas las academias cargadas
         * Retorno: List<academia>
         *          Retorna toda lista de academias
         */
        public List<academia> ObtenerAcademias()
        {
            using (var db = new JJSSEntities())
            {
                var academ = from a in db.academia
                             where a.actual == Constantes.ConstatesBajaLogica.ACTUAL
                             select a;
                return academ.ToList();
            }
        }


        /*
         * Método que devuelve un tipo de clase segun su id
         * Parametros: pIDAcademia: entero que representa el id del tipo de clase a buscar
         * Retorno: tipoclase
         *          null
         */
        public academia ObtenerAcademiasPorID(int pIDAcademia)
        {
            using (var db = new JJSSEntities())
            {

                return db.academia.Find(pIDAcademia);

            }
        }

        public String GenerarAcademia(string pNombre, direccion pDireccion, long? pTelefono)
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

                    academia nuevaAcademia = new academia()
                    {
                        nombre = pNombre,
                        direccion = nuevaDireccion,
                        telefono = pTelefono,
                        actual = Constantes.ConstatesBajaLogica.ACTUAL,
                    };

                    db.academia.Add(nuevaAcademia);

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

        public string ModificarAcademia(academia pAcademia)
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    academia academiaSeleccionada = db.academia.Find(pAcademia.id_academia);
                    if (academiaSeleccionada == null) return "No existe esa academia";
                    if (academiaSeleccionada != pAcademia)
                    {
                        academiaSeleccionada.nombre = pAcademia.nombre;
                        academiaSeleccionada.direccion = pAcademia.direccion;
                        academiaSeleccionada.telefono = pAcademia.telefono;

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

        public string EliminarAcademia(int pID)
        {
            try
            {
                using (var db = new JJSSEntities())
                {
                    academia academiaAeliminar = db.academia.Find(pID);
                    academiaAeliminar.actual = Constantes.ConstatesBajaLogica.NO_ACTUAL;
                    db.SaveChanges();

                    return "";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public List<Resultados.SedeDireccion> ObtenerAcademiasConFiltro(string filtroNombre, string filtroCiudad)
        {
            if (filtroCiudad.CompareTo("Todos") == 0) filtroCiudad = "";
            using (var db = new JJSSEntities())
            {
                List<Resultados.SedeDireccion> academias = (from ac in db.academia
                                                        where ac.actual == Constantes.ConstatesBajaLogica.ACTUAL
                                                        && ac.nombre.StartsWith(filtroNombre)
                                                        && ac.direccion.ciudad.nombre.StartsWith(filtroCiudad)
                                                        select new Resultados.SedeDireccion()
                                                        {
                                                            calle = ac.direccion.calle,
                                                            numero = ac.direccion.numero.Value,
                                                            ciudad = ac.direccion.ciudad.nombre,
                                                            sede = ac.nombre,
                                                            idSede = ac.id_academia,
                                                            telefono = ac.telefono,
                                                        }).ToList();
                return academias;
            }
        }

        public Resultados.DireccionAlumno ObtenerDireccionAcademiaCompleta(int pID)
        {
            using (var db = new JJSSEntities())
            {
                var sede = from ac in db.academia
                           where ac.id_academia == pID
                           select new Resultados.DireccionAlumno()
                           {
                               depto = ac.direccion.departamento,
                               piso = ac.direccion.piso,
                               idCiudad = ac.direccion.id_ciudad,
                               idProvincia = ac.direccion.ciudad.id_provincia,
                               torre = ac.direccion.torre,
                               calle = ac.direccion.calle,
                               numero = (int)ac.direccion.numero,
                           };
                return sede.FirstOrDefault();
            }
        }
    }
}
