using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio.Administracion
{
    public class GestorEstados
    {
        public List<estado> obtenerEstados()
        {
            using (var db = new JJSSEntities())
            {
                return db.estado.ToList();
            }
        }

        public List<estado> obtenerEstados(String filtroAmbito)
        {
            using (var db = new JJSSEntities())
            {
                var estados = from est in db.estado
                              where est.ambito == filtroAmbito
                              select est;
                return estados.ToList();
            }
        }
    }

    
}
