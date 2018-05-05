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
                                  join resu in db.resultado on catt.id_categoria_torneo equals resu.id_categoria_torneo
                                  where tor.id_torneo == idTorneo && cat.id_tipo_clase == ConstantesTipoClase.JIU_JITSU
                                  orderby cat.nombre
                                  select new CategoriasTorneoResultado
                                  {
                                      nombreCategoria = cat.nombre,
                                      nombreFaja = faj.descripcion,
                                      idCategoriaTorneo = catt.id_categoria_torneo,
                                      sexo = cat.sexo

                                  }).Distinct();
                List<CategoriasTorneoResultado> res = categorias.ToList();
                
                foreach (CategoriasTorneoResultado cat in res)
                {
                    cat.nombreFaja = cat.nombreFaja.Split(new char[] { '-' })[0];
                    string sexo = cat.sexo.Equals(ContantesSexo.FEMENINO) ? "F" : "M";
                    cat.nombreParaMostrar = cat.nombreCategoria.Trim() + " " + sexo + " " + cat.nombreFaja.Trim();
                }
                res.OrderBy(x => x.nombreParaMostrar);
                return res; 
            }
        }

        public List<CategoriasTorneoResultado> mostrarCategoriasTorneo()
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
                                      nombreFaja = faj.descripcion,
                                      idCategoriaTorneo = catt.id_categoria_torneo,
                                      sexo = cat.sexo
                                  }).Distinct();
                List<CategoriasTorneoResultado> res = categorias.ToList();
                foreach (CategoriasTorneoResultado cat in res)
                {
                    cat.nombreFaja = cat.nombreFaja.Split(new char[] { '-' })[0];
                    string sexo = cat.sexo.Equals(ContantesSexo.FEMENINO) ? " F " : " M ";
                    cat.nombreParaMostrar = cat.nombreCategoria + " " + sexo + " " + cat.nombreFaja;
                }

                return res;
            }
        }

        public List<CategoriasTorneoResultado> mostrarCategoriasSinInscriptos(int idTorneo)
        {
            List<CategoriasTorneoResultado> todas = mostrarCategoriasTorneo();
            List<CategoriasTorneoResultado> conInscriptos = mostrarCategoriasConInscriptos(idTorneo);

            List<CategoriasTorneoResultado> sinInscriptos = quitarCategorias(todas, conInscriptos);

            return sinInscriptos.ToList(); ;
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

        public String cargarResultado(int idTorneo, int idCategoria, int idPrimerPuesto, int idSegundoPuesto, int? idTercerPuesto1, int? idTercerPuesto2)
        {
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    resultado resultadoAnterior;
                    var resultado = from res in db.resultado
                                    where res.id_categoria_torneo == idCategoria && res.id_torneo == idTorneo
                                    select res;
                    resultadoAnterior= resultado.FirstOrDefault();
                    if (resultadoAnterior != null)
                    {
                        resultadoAnterior.id_primer = idPrimerPuesto;
                        resultadoAnterior.id_segundo = idSegundoPuesto;
                        resultadoAnterior.id_tercero1 = idTercerPuesto1;
                        resultadoAnterior.id_tercero2 = idTercerPuesto2;
                        db.SaveChanges();
                        transaction.Commit();
                        return "";
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

        public List<categoria> mostrarCategorias()
        {
            using (var db = new JJSSEntities())
            {
                List<categoria> categorias = db.categoria.ToList();
                categorias.OrderBy(x => x.nombre);
                foreach (categoria c in categorias)
                {
                    string sexo = c.sexo == ContantesSexo.FEMENINO ? " F " : " M ";
                    c.nombre = c.nombre + sexo;
                }
                return categorias;
            }
        }

        public List<faja> mostrarFajas()
        {
            using (var db = new JJSSEntities())
            {
                var fajas = from faj in db.faja
                            where faj.id_tipo_clase == ConstantesTipoClase.JIU_JITSU
                            orderby faj.descripcion
                            select faj;
                List<faja> fajaParaMostrar = fajas.ToList();
                foreach (faja f in fajaParaMostrar)
                {
                    f.descripcion = f.descripcion.Split(new char[] { '-' })[0];
                }
                return eliminarRepetidas(fajaParaMostrar);
            }
        }

        private List<faja> eliminarRepetidas(List<faja> fajas)
        {
            List<faja> res = new List<faja>();
            foreach(faja f in fajas)
            {
                bool esta = false;
                foreach(faja f2 in res)
                {
                    if (f.descripcion.CompareTo(f2.descripcion) == 0)
                    {
                        esta = true;
                    }
                }
                if (!esta)
                {
                    res.Add(f);
                }
            }
            return res;
        }


        public CategoriasTorneoResultado categoriaAAgregar(int idFaja, int idCategoria)
        {
            using (var db = new JJSSEntities())
            {
                var categoria = from catt in db.categoria_torneo
                                where catt.id_faja == idFaja && catt.id_categoria == idCategoria
                                select new CategoriasTorneoResultado()
                                {
                                    idCategoriaTorneo = catt.id_categoria_torneo,
                                    nombreCategoria = catt.categoria.nombre,
                                    nombreFaja = catt.faja.descripcion,
                                    sexo = catt.categoria.sexo
                                };
                CategoriasTorneoResultado res = categoria.FirstOrDefault();
                //si no existe la tiene que agregar
                if (res == null)
                {
                    categoria_torneo nuevaCategoriaTorneo = new categoria_torneo()
                    {
                        id_categoria = idCategoria,
                        id_faja = idFaja,
                    };
                    db.categoria_torneo.Add(nuevaCategoriaTorneo);
                    db.SaveChanges();

                    categoria = from catt in db.categoria_torneo
                                    where catt.id_categoria_torneo == nuevaCategoriaTorneo.id_categoria_torneo
                                    select new CategoriasTorneoResultado()
                                    {
                                        idCategoriaTorneo = catt.id_categoria_torneo,
                                        nombreCategoria = catt.categoria.nombre,
                                        nombreFaja = catt.faja.descripcion,
                                        sexo = catt.categoria.sexo
                                    };
                    res = categoria.FirstOrDefault();
                }

                res.nombreFaja = res.nombreFaja.Split(new char[] { '-' })[0];
                string sexo = res.sexo.Equals(ContantesSexo.FEMENINO) ? " F " : " M ";
                res.nombreParaMostrar = res.nombreCategoria + " " + sexo + " " + res.nombreFaja;
                return res;
            }
        }
    }
}
