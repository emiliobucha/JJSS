using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;

namespace JJSS_Negocio
{
    public class GestorResultados
    {
        public List<CategoriasTorneoResultado> mostrarCategoriasConInscriptos(int idTorneo)
        {
            using (var db = new JJSSEntities())
            {
                var categorias = (from catt in db.categoria_torneo
                                  join ins in db.inscripcion on catt.id_categoria_torneo equals ins.id_categoria
                                  join tor in db.torneo on ins.id_torneo equals tor.id_torneo
                                  join cat in db.categoria on catt.id_categoria equals cat.id_categoria
                                  join faj in db.faja on catt.id_faja equals faj.id_faja
                                  where tor.id_torneo == idTorneo
                                  select new CategoriasTorneoResultado
                                  {
                                      nombreCategoria = cat.nombre,
                                      nombreFaja = faj.descripcion
                                  }).Distinct();
                return categorias.ToList();
            }
        }

        public List<CategoriasTorneoResultado> mostrarCategorias()
        {
            using (var db = new JJSSEntities())
            {
                var categorias = (from catt in db.categoria_torneo
                                  join cat in db.categoria on catt.id_categoria equals cat.id_categoria
                                  join faj in db.faja on catt.id_faja equals faj.id_faja
                                  select new CategoriasTorneoResultado
                                  {
                                      nombreCategoria = cat.nombre,
                                      nombreFaja = faj.descripcion
                                  }).Distinct();
                return categorias.ToList();
            }
        }

        public List<CategoriasTorneoResultado> mostrarCategoriasSinInscriptos(int idTorneo)
        {
            List<CategoriasTorneoResultado> todas = mostrarCategorias();
            List<CategoriasTorneoResultado> conInscriptos = mostrarCategoriasConInscriptos(idTorneo);

            List<CategoriasTorneoResultado> sinInscriptos = quitarCategorias(todas, conInscriptos);
            return sinInscriptos;
        }

        public List<CategoriasTorneoResultado> quitarCategorias(List<CategoriasTorneoResultado> primera, List<CategoriasTorneoResultado> segunda)
        {
            List<CategoriasTorneoResultado> tercera = new List<CategoriasTorneoResultado>();
            foreach(CategoriasTorneoResultado categoria in primera)
            {
                bool esta = false;
                foreach(CategoriasTorneoResultado inscripto in segunda)
                {
                    if (categoria == inscripto)
                    {
                        esta = true;
                        break;
                    }
                }
                if (!esta) tercera.Add(categoria);
            }
            return tercera;
        }

        public List<Object> mostrarAlumnosDeCategoria(int idCategoria, int idTorneo)
        {
            using (var db = new JJSSEntities())
            {
                var participantes = from part in db.participante
                              join ins in db.inscripcion on part.id_participante equals ins.id_participante
                              where ins.id_torneo == idTorneo && ins.id_categoria == idCategoria
                              select new
                              {
                                  participante = part.nombre + " " + part.apellido,
                                  id_participante = part.id_participante,
                              };
                return participantes.ToList<Object>();
            }
        }
    }
}
