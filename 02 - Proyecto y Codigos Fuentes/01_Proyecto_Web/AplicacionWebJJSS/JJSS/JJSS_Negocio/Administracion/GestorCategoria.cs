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

        public List<CategoriaResultado> ObtenerTodasCategoriasConFiltros(string filtroNombre, int filtroSexo, string filtroDisciplina)
        {
            using (var db = new JJSSEntities())
            {
                var categorias = from c in db.categoria
                                 where c.actual == Constantes.ConstatesBajaLogica.ACTUAL
                                 && c.nombre.StartsWith(filtroNombre) && c.tipo_clase.nombre.StartsWith(filtroDisciplina)
                                 && c.sexo == filtroSexo
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
                                 };
                List<CategoriaResultado> cats = categorias.ToList();
                foreach (CategoriaResultado c in cats)
                {
                    if (c.sexo == Constantes.ContantesSexo.FEMENINO) c.sexoMostrar = "F";
                    else c.sexoMostrar = "M";
                }
                return cats;
            }
        }
    }
}
