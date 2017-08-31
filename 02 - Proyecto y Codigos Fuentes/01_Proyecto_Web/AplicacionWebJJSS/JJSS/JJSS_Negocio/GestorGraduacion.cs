﻿using System;
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

        public List<Object> buscarFajasAlumnos()
        {
            using (var db = new JJSSEntities())
            {
                var graduacion = from alu in db.alumno
                                 join axf in db.alumnoxfaja on alu.id_alumno equals axf.id_alumno
                                 join faj in db.faja on axf.id_faja equals faj.id_faja
                                 join tip in db.tipo_clase on faj.id_tipo_clase equals tip.id_tipo_clase
                                 where axf.actual == 1
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
                        string faja = dr["fajaActual"].ToString();
                        
                        int grados = int.Parse(dr["grados"].ToString());
                        int idAlu = int.Parse(dr["idAlumno"].ToString());
                        DateTime fecha = DateTime.Now;

                        var fajaActual = from faj in db.faja
                                         where faj.descripcion == faja
                                         select faj;

                        
                        int ordenSiguiente = (int)fajaActual.FirstOrDefault().orden+grados;
                        int tipoClase = (int)fajaActual.FirstOrDefault().id_tipo_clase;
                        int idFajaActual = (int)fajaActual.FirstOrDefault().id_faja;

                        var fajaSiguiente = from faj in db.faja
                                            where faj.orden == ordenSiguiente && faj.id_tipo_clase == tipoClase
                                            select faj;
                        int idFajaSiguiente = fajaSiguiente.FirstOrDefault().id_faja;


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

                        transaction.Commit();
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }

            }

            return "";
        }
    }
}
