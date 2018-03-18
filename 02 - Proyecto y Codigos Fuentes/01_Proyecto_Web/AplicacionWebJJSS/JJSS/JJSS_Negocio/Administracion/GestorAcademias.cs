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
                return db.academia.ToList();
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
    }
}
