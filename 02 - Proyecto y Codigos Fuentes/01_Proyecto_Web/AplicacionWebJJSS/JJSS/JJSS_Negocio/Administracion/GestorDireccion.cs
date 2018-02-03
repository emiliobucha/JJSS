using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio.Administracion
{
    public class GestorDireccion
    {
        public direccion buscarDireccion(direccion pDireccion)
        {
            direccion direccionEncontrada = null;
            using (var db = new JJSSEntities())
            {
                //var direccion = from d in db.direccion
                //           where d.numero == pDireccion.numero
                //           && d.piso==pDireccion.piso
                //           && d.torre==pDireccion.torre
                //           && d.barrio==pDireccion.barrio
                //           && d.
                //           select new Resultados.SedeDireccion()
                //           {
                //               sede = s.nombre,
                //               calle = s.direccion.calle,
                //               numero = (int)s.direccion.numero,
                //               ciudad = s.direccion.ciudad.nombre,
                //               provincia = s.direccion.ciudad.provincia.nombre,
                //               pais = s.direccion.ciudad.provincia.pais.nombre,
                //           };
                //return sede.FirstOrDefault();
            }
            return direccionEncontrada;
        }
    }
}

