using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;
using JJSS_Negocio.Constantes;

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
                                  where tor.id_torneo == idTorneo && cat.id_tipo_clase == ConstantesTipoClase.JIU_JITSU
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
                                  where cat.id_tipo_clase == ConstantesTipoClase.JIU_JITSU
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
            foreach (CategoriasTorneoResultado categoria in primera)
            {
                bool esta = false;
                foreach (CategoriasTorneoResultado inscripto in segunda)
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

        public List<DatosParticipanteTorneo> mostrarParticipantesDeCategoria(int idCategoria, int idTorneo)
        {
            using (var db = new JJSSEntities())
            {
                var participantes = from part in db.participante
                                    join ins in db.inscripcion on part.id_participante equals ins.id_participante
                                    where ins.id_torneo == idTorneo && ins.id_categoria == idCategoria
                                    select new DatosParticipanteTorneo
                                    {
                                        participante = part.nombre + " " + part.apellido,
                                        idParticipante = part.id_participante,
                                    };
                return participantes.ToList<DatosParticipanteTorneo>();
            }
        }

        public String cargarResultado(int idTorneo, int idCategoria, int idPrimerPuesto, int idSegundoPuesto, int idTercerPuesto1, int idTercerPuesto2)
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    if (validarCargaResultados(idTorneo, idCategoria) != null)
                    {
                        return "Ya se cargaron resultados para esa categoria del torneo";
                    }

                    resultado nuevoResultado = new resultado()
                    {
                        id_torneo = idTorneo,
                        id_categoria_torneo = idCategoria,
                        id_primer = idPrimerPuesto,
                        id_segundo = idSegundoPuesto,
                        id_tercero1 = idTercerPuesto1,
                        id_tercero2 = idTercerPuesto2
                    };
                    db.resultado.Add(nuevoResultado);
                    db.SaveChanges();

                    transaction.Commit();
                    return "";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }
            }
        }

        // valida que no se haya cargado un resultado a una categoria del mismo torneo
        public resultado validarCargaResultados(int idTorneo, int idCategoria)
        {
            using (var db = new JJSSEntities())
            {
                var resultado = from res in db.resultado
                                where res.id_categoria_torneo == idCategoria && res.id_torneo == idTorneo
                                select res;
                return resultado.FirstOrDefault();
            }
        }
    }
}
