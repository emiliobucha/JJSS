using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data;

namespace JJSS_Negocio
{
    public class GestorGraduacion
    {

        /**
         * Metodo que busca todos los alumnos que tienen una faja asociada actualmente y devuelve los datos del alumno, faja y tipo de clase
         * Retorno: Lista de object  -  apellido y nombre del alumno, faja, fecha de ultima graduacion, tipo de clase, id de alumno
         * 
         */
        public List<Object> buscarFajasAlumnos()
        {
            using (var db = new JJSSEntities())
            {
                var graduacion = from alu in db.alumno
                                 join axf in db.alumnoxfaja on alu.id_alumno equals axf.id_alumno
                                 join faj in db.faja on axf.id_faja equals faj.id_faja
                                 join tip in db.tipo_clase on faj.id_tipo_clase equals tip.id_tipo_clase
                                 where axf.actual == 1
                                 orderby alu.apellido
                                 select new
                                 {
                                     alumno = alu.apellido + ", " + alu.nombre,
                                     faja = faj.descripcion,
                                     fecha = axf.fecha,
                                     tipo = tip.nombre,
                                     idAlu = alu.id_alumno
                                 };
                List<Object> lista = graduacion.ToList<Object>();
                return lista;
            }
        }


        /*
         * Metodo que busca todos los alumnos que tienen una faja asociada actualmente con un filtro y devuelve los datos del alumno, faja y tipo de clase
         * Parametros: pIdTipoClase: id de tipo de clase para filtrar
         * Retorno: Lista de object  -  apellido y nombre del alumno, faja, fecha de ultima graduacion, tipo de clase, id de alumno
         * 
         */

        public List<Object> buscarFajasAlumnosConFiltro( int pIdTipoClase)
        {
            using (var db = new JJSSEntities())
            {
                var graduacion = from alu in db.alumno
                                 join axf in db.alumnoxfaja on alu.id_alumno equals axf.id_alumno
                                 join faj in db.faja on axf.id_faja equals faj.id_faja
                                 join tip in db.tipo_clase on faj.id_tipo_clase equals tip.id_tipo_clase
                                 where axf.actual == 1 && tip.id_tipo_clase==pIdTipoClase
                                 orderby alu.apellido
                                 select new
                                 {
                                     alumno = alu.apellido + ", " + alu.nombre,
                                     faja = faj.descripcion,
                                     fecha = axf.fecha,
                                     tipo = tip.nombre,
                                     idAlu = alu.id_alumno
                                 };
                List<Object> lista = graduacion.ToList<Object>();
                return lista;
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

                        var aluxf = from axf in db.alumnoxfaja
                                    where axf.id_alumno == idAlu && axf.id_faja == idFajaActual
                                    select axf;
                        aluxf.FirstOrDefault().actual = 0;
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
    }
}
