using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data;
using JJSS_Negocio.Resultados;

namespace JJSS_Negocio
{
    public class GestorGraduacion
    {

        /**
         * Metodo que busca todos los alumnos que tienen una faja asociada actualmente y devuelve los datos del alumno, faja y tipo de clase
         * Retorno: Lista de object  -  apellido y nombre del alumno, faja, fecha de ultima graduacion, tipo de clase, id de alumno
         * 
         */
        public List<AlumnoFaja> buscarFajasAlumnos()
        {
            using (var db = new JJSSEntities())
            {
                var graduacion = from alu in db.alumno
                                 join axf in db.alumnoxfaja on alu.id_alumno equals axf.id_alumno
                                 join faj in db.faja on axf.id_faja equals faj.id_faja
                                 join tip in db.tipo_clase on faj.id_tipo_clase equals tip.id_tipo_clase
                                 where axf.actual == 1
                                 orderby alu.apellido
                                 select new AlumnoFaja
                                 {
                                     apellido = alu.apellido,
                                     nombre= alu.nombre,
                                     faja = faj.descripcion,
                                     fecha = axf.fecha,
                                     tipo = tip.nombre,
                                     idAlu = alu.id_alumno,
                                     idTipo = tip.id_tipo_clase,
                                 };

                return graduacion.ToList();
            }
        }


        /*
         * Metodo que busca todos los alumnos que tienen una faja asociada actualmente con un filtro y devuelve los datos del alumno, faja y tipo de clase
         * Parametros: pIdTipoClase: id de tipo de clase para filtrar
         * Retorno: Lista de object  -  apellido y nombre del alumno, faja, fecha de ultima graduacion, tipo de clase, id de alumno
         * 
         */

        public List<AlumnoFaja> buscarFajasAlumnosConFiltro( int pIdTipoClase)
        {
            using (var db = new JJSSEntities())
            {
                var graduacion = from alu in db.alumno
                                 join axf in db.alumnoxfaja on alu.id_alumno equals axf.id_alumno
                                 join faj in db.faja on axf.id_faja equals faj.id_faja
                                 join tip in db.tipo_clase on faj.id_tipo_clase equals tip.id_tipo_clase
                                 where axf.actual == 1 && tip.id_tipo_clase==pIdTipoClase
                                 orderby alu.apellido
                                 select new AlumnoFaja
                                 {
                                     apellido = alu.apellido,
                                     nombre= alu.nombre,
                                     faja = faj.descripcion,
                                     fecha = axf.fecha,
                                     tipo = tip.nombre,
                                     idAlu = alu.id_alumno,
                                     idTipo = tip.id_tipo_clase,
                                 };
                return graduacion.ToList();
            }
        }


        /*
         * Metodo que le cambia la faja a un alumno
         * Parametros: pDt: DataTable - contiene los datos de los alumnos a graduar incluida la cantidad de grados a graduar y el id del alumno
         * Retorno: string -    "" - Transaccion correcta
         *                      ex.Message - error de la BD
         * 
         */
        public string graduar(DataTable pDt)
        {

            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    for (int i = 0; i < pDt.Rows.Count; i++)
                    {
                        DataRow dr = pDt.Rows[i];
                        string tClase = dr["tipoClase"].ToString();
                        int grados = int.Parse(dr["grados"].ToString());
                        int idAlu = int.Parse(dr["idAlumno"].ToString());
                        DateTime fecha = DateTime.Now;

                        faja fajaActual = (from faj in db.faja
                                         join axf in db.alumnoxfaja on faj.id_faja equals axf.id_faja
                                         join tc in db.tipo_clase on faj.id_tipo_clase equals tc.id_tipo_clase
                                         where axf.id_alumno == idAlu && axf.actual == 1 && tc.nombre==tClase
                                         select faj).FirstOrDefault();


                        int ordenSiguiente = (int)fajaActual.orden + grados;
                        int tipoClase = (int)fajaActual.id_tipo_clase;
                        int idFajaActual = (int)fajaActual.id_faja;

                        faja fajaSiguiente = (from faj in db.faja
                                            where faj.orden == ordenSiguiente && faj.id_tipo_clase == tipoClase
                                            select faj).FirstOrDefault();
                        if (fajaSiguiente == null) throw new Exception("No existe esa faja");
                        int idFajaSiguiente = fajaSiguiente.id_faja;

                        //elimna la faja anterior
                        alumnoxfaja aluxf = (from axf in db.alumnoxfaja
                                    where axf.id_alumno == idAlu && axf.id_faja == idFajaActual && axf.faja.id_tipo_clase == tipoClase
                                    && axf.actual == Constantes.ConstatesBajaLogica.ACTUAL
                                             select axf).FirstOrDefault();
                        aluxf.actual = Constantes.ConstatesBajaLogica.NO_ACTUAL;
                        db.SaveChanges();

                        //crea la nueva faja
                        alumnoxfaja nuevoAxF;
                        nuevoAxF = new alumnoxfaja()
                        {
                            id_alumno = idAlu,
                            id_faja = idFajaSiguiente,
                            fecha = fecha,
                            actual = 1,
                        };
                        db.alumnoxfaja.Add(nuevoAxF);
                        db.SaveChanges();
                        
                    }
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

        public AlumnoFaja buscarAlumnoFajaPorID(int idAlumno, int idTipoClase)
        {
                using (var db = new JJSSEntities())
                {
                    var graduacion = from alu in db.alumno
                                     join axf in db.alumnoxfaja on alu.id_alumno equals axf.id_alumno
                                     where alu.id_alumno==idAlumno && axf.faja.id_tipo_clase == idTipoClase 
                                     && axf.actual == Constantes.ConstatesBajaLogica.ACTUAL
                                     orderby alu.apellido, axf.faja.tipo_clase.nombre
                                     select new AlumnoFaja
                                     {
                                         apellido = alu.apellido,
                                         nombre = alu.nombre,
                                         faja = axf.faja.descripcion,
                                         fecha = axf.fecha,
                                         tipo = axf.faja.tipo_clase.nombre,
                                         idAlu = alu.id_alumno,
                                         idTipo = axf.faja.tipo_clase.id_tipo_clase,
                                         dni = alu.tipo_documento.codigo + " - " + alu.dni
                                     };
                
                    return graduacion.FirstOrDefault();
                }
            
        }

        public void graduarIndividual(int idAlumno, int idTipoClase, int grados)
        {

            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {       DateTime fecha = DateTime.Now;

                        faja fajaActual = (from faj in db.faja
                                           join axf in db.alumnoxfaja on faj.id_faja equals axf.id_faja
                                           join tc in db.tipo_clase on faj.id_tipo_clase equals tc.id_tipo_clase
                                           where axf.id_alumno == idAlumno && axf.actual == Constantes.ConstatesBajaLogica.ACTUAL 
                                           && tc.id_tipo_clase==idTipoClase
                                           select faj).FirstOrDefault();


                        int ordenSiguiente = (int)fajaActual.orden + grados;
                        int idFajaActual = (int)fajaActual.id_faja;

                        faja fajaSiguiente = (from faj in db.faja
                                              where faj.orden == ordenSiguiente && faj.id_tipo_clase == idTipoClase
                                              select faj).FirstOrDefault();
                        if (fajaSiguiente == null) throw new Exception("No existe esa faja");
                        int idFajaSiguiente = fajaSiguiente.id_faja;

                        //elimna la faja anterior
                        alumnoxfaja aluxf = (from axf in db.alumnoxfaja
                                             where axf.id_alumno == idAlumno && axf.id_faja == idFajaActual && axf.faja.id_tipo_clase == idTipoClase
                                             && axf.actual == Constantes.ConstatesBajaLogica.ACTUAL
                                             select axf).FirstOrDefault();
                        aluxf.actual = Constantes.ConstatesBajaLogica.NO_ACTUAL;
                        db.SaveChanges();

                        //crea la nueva faja
                        alumnoxfaja nuevoAxF;
                        nuevoAxF = new alumnoxfaja()
                        {
                            id_alumno = idAlumno,
                            id_faja = idFajaSiguiente,
                            fecha = fecha,
                            actual = Constantes.ConstatesBajaLogica.ACTUAL,
                        };
                        db.alumnoxfaja.Add(nuevoAxF);
                        db.SaveChanges();

                    
                    transaction.Commit();
                    
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
