using System;
using JJSS_Negocio.Resultados;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;

namespace JJSS_Negocio.Administracion
{
    public class GestorCategoria
    {

        public String crearCategoria(categoria pCategoria)
        {
            String sReturn = "";
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    db.categoria.Add(pCategoria);

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

        public string ModificarCategoria (categoria pCategoria)
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    categoria catSeleccionada = db.categoria.Find(pCategoria.id_categoria);
                    if (catSeleccionada == null) return "No existe esa categoría";
                    if (catSeleccionada != pCategoria)
                    {
                        catSeleccionada.nombre = pCategoria.nombre;
                        catSeleccionada.edad_desde = pCategoria.edad_desde;
                        catSeleccionada.edad_hasta = pCategoria.edad_hasta;
                        catSeleccionada.peso_desde = pCategoria.peso_desde;
                        catSeleccionada.peso_hasta = pCategoria.peso_hasta;
                        catSeleccionada.sexo = pCategoria.sexo;
                        catSeleccionada.id_tipo_clase = pCategoria.id_tipo_clase;

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

        public List<CategoriaResultado> ObtenerTodasCategoriasConFiltros(string filtroNombre, int filtroSexo, string filtroDisciplina)
        {
            if (filtroDisciplina.CompareTo("Todos") == 0) filtroDisciplina = "";
            using (var db = new JJSSEntities())
            {
                List<CategoriaResultado> categorias = new List<CategoriaResultado>();
                if (filtroSexo == -1)
                {
                    categorias = (from c in db.categoria
                                  where c.actual == Constantes.ConstatesBajaLogica.ACTUAL
                                  && c.nombre.StartsWith(filtroNombre) && c.tipo_clase.nombre.StartsWith(filtroDisciplina)
                                  orderby c.sexo descending, c.edad_desde ascending, c.peso_desde ascending
                                  select new CategoriaResultado()
                                  {
                                      sexo = c.sexo,
                                      disciplina = c.tipo_clase.nombre,
                                      nombre = c.nombre,
                                      edadMax = c.edad_hasta.ToString(),
                                      edadMin = c.edad_desde.ToString(),
                                      pesoMax = c.peso_hasta.ToString() + " kg",
                                      pesoMin = c.peso_desde.ToString() + " kg",
                                      idCategoria = c.id_categoria,
                                      idDisciplina = c.id_tipo_clase.Value,
                                  }).ToList();
                }
                else
                {
                    categorias = (from c in db.categoria
                                  where c.actual == Constantes.ConstatesBajaLogica.ACTUAL
                                  && c.nombre.StartsWith(filtroNombre) && c.tipo_clase.nombre.StartsWith(filtroDisciplina)
                                  && c.sexo == filtroSexo
                                  orderby c.sexo descending, c.edad_desde ascending, c.peso_desde ascending
                                  select new CategoriaResultado()
                                  {
                                      sexo = c.sexo,
                                      disciplina = c.tipo_clase.nombre,
                                      nombre = c.nombre,
                                      edadMax = c.edad_hasta.ToString(),
                                      edadMin = c.edad_desde.ToString(),
                                      pesoMax = c.peso_hasta.ToString(),
                                      pesoMin = c.peso_desde.ToString(),
                                      idCategoria = c.id_categoria,
                                      idDisciplina = c.id_tipo_clase.Value,
                                  }).ToList();
                }
                foreach (CategoriaResultado c in categorias)
                {
                    if (c.sexo == Constantes.ContantesSexo.FEMENINO) c.sexoMostrar = "F";
                    else c.sexoMostrar = "M";

                    if (c.edadMin.CompareTo("0") == 0) c.edadMin = "-";
                    if (c.edadMax.CompareTo("99") == 0) c.edadMax = "-";
                    if (c.pesoMax.CompareTo("999") == 0) c.pesoMax = "-";
                    if (c.pesoMin.CompareTo("999") == 0) c.pesoMin = "-";

                    if (c.nombre.StartsWith("Absoluto"))
                    {
                        c.pesoMin = "Libre";
                        c.pesoMax = "Libre";
                    }
                }
                return categorias;
            }
        }

        public categoria ObtenerCategoriaPorID(int pID)
        {
            using (var db = new JJSSEntities())
            {
                return db.categoria.Find(pID);
            }
        }

        public string EliminarCategoria(int pID)
        {
            try
            {
                using (var db = new JJSSEntities())
                {
                    categoria categoriaAeliminar = db.categoria.Find(pID);
                    categoriaAeliminar.actual = Constantes.ConstatesBajaLogica.NO_ACTUAL;
                    db.SaveChanges();

                    return "";
                }
            }catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
