using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using JJSS_Negocio.Constantes;

namespace JJSS_Negocio.Administracion
{
    public class GestorTipoEvento
    {

        public string crearTipoEvento(string pNombre)
        {
            using (var db = new JJSSEntities())
            {
                try
                {
                    tipo_evento_especial nuevoTipoEvento = new tipo_evento_especial()
                    {
                        nombre = pNombre,
                        actual = ConstatesBajaLogica.ACTUAL,
                    };
                    db.tipo_evento_especial.Add(nuevoTipoEvento);
                    db.SaveChanges();
                    return "";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
        }

        public List<tipo_evento_especial> ObtenerTodosTipoEventosConFiltro(string filtroNombre)
        {
            using (var db = new JJSSEntities())
            {
                var tipoEvento = from te in db.tipo_evento_especial
                                 where te.nombre.StartsWith(filtroNombre) && te.actual == ConstatesBajaLogica.ACTUAL
                                 orderby te.nombre ascending
                                 select te;
                return tipoEvento.ToList();
            }
        }

        public string eliminarTipoEvento(int idTipoEvento)
        {
            using (var db = new JJSSEntities())
            {
                try
                {
                    tipo_evento_especial tipoEvento = db.tipo_evento_especial.Find(idTipoEvento);
                    tipoEvento.actual = ConstatesBajaLogica.NO_ACTUAL;
                    db.SaveChanges();
                    return "";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
        }

    }
}
