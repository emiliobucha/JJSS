using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data;
using System.Globalization;

namespace JJSS_Negocio
{
    public class GestorPagoClase
    {
        /*
         *Metodo que registra un pago
         * Parametros:
         *              pAlumno : entero que representa el id del alumno
         *              pClase : entero que representa el id de la clase
         *              pMonto : decimal que representa el monto 
         *              pMes : string  que representa el mes de la cuota
         *              pFormaPago : entero que representa el id de la forma de pago
         *Retornos:     String
         *              "" : Transaccion Correcta
         *              ex.Message : Mensaje de error provocado por una excepción
         *              El alumno no está inscripto a esa clase
         *              Ya se registró este pago
         */
        public string registrarPago(int pAlumno, int pClase, decimal pMonto, string pMes, int pFormaPago, short pPagoRecargo)
        {
            string sReturn="";
            
            DateTime pFecha = DateTime.Now;
            using (var db = new JJSSEntities())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    //validar que el alumno este inscripto en esa clase
                    if (validarInscripcion(pAlumno, pClase) == null)
                    {
                        return "El alumno no está inscripto a esa clase";
                    }

                    //buscar alumno
                    alumno alumnoSelect = db.alumno.Find(pAlumno);
                    //buscar clase
                    clase claseSelect = db.clase.Find(pClase);
                    //buscar forma pago
                    forma_pago formaSelect = db.forma_pago.Find(pFormaPago);

                    // validar que no pago antes esa cuota
                    if (!validarPago(alumnoSelect.id_alumno, pClase, pMes))
                    {
                        return "Ya se registró este pago";
                    }
                    
                    //buscar pago
                    pago_clase pagoRegistrado = buscarPagoSegunAlumnoClase(pAlumno, pClase);
                    if (pagoRegistrado == null) //no existe pago
                    {
                        //crear pago
                        pago_clase nuevoPago;
                        nuevoPago = new pago_clase()
                        {
                            alumno = alumnoSelect,
                            clase = claseSelect
                        };
                        db.pago_clase.Add(nuevoPago);
                        pagoRegistrado = nuevoPago;
                    }
                    
                    //crear detalle
                    detalle_pago_clase nuevoDetalle;
                    nuevoDetalle = new detalle_pago_clase()
                    {
                        //pago_clase = pagoRegistrado,
                        id_pago_clase= pagoRegistrado.id_pago_clase,
                        monto = pMonto,
                        fecha_hora=pFecha,
                        mes = pMes,
                        forma_pago=formaSelect,
                        recargo=pPagoRecargo
                    };
                    db.detalle_pago_clase.Add(nuevoDetalle);

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

        /*
         * busca si existe un pago con caracteristicas similares
         * Parametros: pAlumno : entero que representa el id del alumno
         *              pClase : entero que representa el id de la clase
         *              pMes : string  que representa el mes de la cuota
         * Retornos:    false si no se encontro un pago similar
         *              true si se encontro un pago similar
         * 
         */
        public Boolean validarPago(int pAlumno, int pClase, string pMes)
        {
            int year = DateTime.Today.Year;
            using (var db = new JJSSEntities())
            {
                var pagoEncontrado = from pago in db.pago_clase
                                     join detalle in db.detalle_pago_clase on pago.id_pago_clase equals detalle.id_pago_clase
                                     where pago.id_alumno == pAlumno && pago.id_clase == pClase && detalle.mes == pMes
                                     orderby detalle.fecha_hora descending
                                     select new
                                     {
                                         alu = pago.id_alumno,
                                         cla = pago.id_clase,
                                         mes = detalle.mes,
                                         fecha = detalle.fecha_hora
                                     };

                 DataTable dt= modUtilidadesTablas.ToDataTable(pagoEncontrado);
                if (pagoEncontrado.Count() > 0)
                {
                    DataRow row = dt.Rows[0];
                    DateTime fechaPago = DateTime.Parse(row["fecha"].ToString());
                    if (year == fechaPago.Year)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /*
         * Busca si se realizo un pago de un alumno a una clase
         * Parametros:
         *              pAlumno: entero que representa el id del alumno
         *              pClase: entero que representa el id de la clase 
         * Retornos:pago_clase
         *          pago  del alumno a dicha clase pudiendo ser nulo el resultado si no habia pagado
         */
        public pago_clase buscarPagoSegunAlumnoClase(int pAlumno, int pClase)
        {
            using (var db = new JJSSEntities())
            {
                var pagoEncontrado = from pago in db.pago_clase
                                     where pago.id_alumno == pAlumno && pago.id_clase == pClase
                                     select pago;
                return pagoEncontrado.FirstOrDefault();
            }
        }

        /*
         * Obtener inscripcion de un alumno a una clase
         * Parametros:
         *              pAlumno: entero que representa el id del alumno
         *              pClase: entero que representa el id de la clase 
         * Retornos:Inscripcion_clase
         *          Inscripcion del alumno a dicha clase pudiendo ser nulo el resultado si no estaba inscripto
         *              
         */
        public inscripcion_clase validarInscripcion(int pAlumno, int pClase)
        {
            GestorInscripcionesClase inscripcion = new GestorInscripcionesClase();
            return inscripcion.ObtenerAlumnoInscripto(pAlumno, pClase);
        }


        /*
         * Metodo que valida si un alumno pago para asistir a clases
         * Parametros : pIdAlumno entero que representa el id del alumno
         *              pIDTipoClase entero que representa el id del tipo de clase
         * Retornos : true - puede asistir
         *              false - no puede asistir
         * 
         */
        public Boolean validarPagoParaAsistencia(int pIdAlumno,int pIdTipoClase)
        {
            DataTable dt;
            DateTime fechaInscripcion;
            int diasRecargo = 0;
            using (var db = new JJSSEntities())
            {
                var inscripcion = from ins in db.inscripcion_clase
                                                join alu in db.alumno on ins.id_alumno equals alu.id_alumno
                                                where alu.id_alumno == pIdAlumno 
                                                select ins;
                if (inscripcion == null) return false;
                fechaInscripcion = (DateTime)inscripcion.FirstOrDefault().fecha;

                var pago = from alu in db.alumno
                           join pag in db.pago_clase on alu.id_alumno equals pag.id_alumno
                           join detalle in db.detalle_pago_clase on pag.id_pago_clase equals detalle.id_pago_clase
                           where alu.id_alumno == pIdAlumno
                           orderby detalle.fecha_hora
                           select new
                           {
                               idPago = pag.id_pago_clase,
                               mes = detalle.mes,
                               fecha = detalle.fecha_hora
                           };
                dt=modUtilidadesTablas.ToDataTable(pago.ToList());

                var parametro = from param in db.parametro
                                  where param.id_parametro == 2
                                  select param;
                diasRecargo = (int)parametro.FirstOrDefault().valor;

            }
            int contPagos = dt.Rows.Count;
            if (contPagos >0)
            { //tiene un pago
                DataRow dr = dt.Rows[0];
                string mesPago = dr["mes"].ToString();
                DateTime fechaPago = DateTime.Parse(dr["fecha"].ToString());

                CultureInfo ci = new CultureInfo("Es-Es");
                string nombreMes = ci.DateTimeFormat.GetMonthName(DateTime.Now.Month);
                if (mesPago.ToLower().CompareTo(nombreMes)==0 && fechaPago.Year == DateTime.Today.Year)
                {
                    return true;
                }
                else
                {
                    DateTime pagoRecargo = fechaInscripcion.AddDays(diasRecargo);
                    if (DateTime.Today.Day <= pagoRecargo.Day) return true;
                    else return false;
                }
            }
            else
            {// no tiene ningun pago

                using (var db = new JJSSEntities())
                {
                    var asistencia = from asi in db.asistencia_clase
                                     join alu in db.alumno on asi.id_alumno equals alu.id_alumno
                                     join cla in db.clase on asi.id_clase equals cla.id_clase
                                     where alu.id_alumno == pIdAlumno && cla.id_tipo_clase == pIdTipoClase
                                     select asi;
                    if (asistencia == null) return true;
                    else return false;
                }
            }

            /* A- buscar fecha inscripcion
             * B- buscar ultimo pago
             *      1- si tiene un pago, validar que corresponda con el mes y año actual
             *          a- si corresponde, se deja asistir - true X
             *          b- si no corresponde, validar si esta a tiempo de pagar con recargo (7 dias)
             *              i- si esta a tiempo de pagar - true X
             *              ii- si se paso la fecha - false X
             *      2- si no tiene un pago, validar si ya asistio una vez
             *          a- si asistio una vez, no se deja asistir - false X
             *          b- si no asistio nunca, se deja asistir - true X
             *      
             * 
             * 
             */
        }
    }
}
