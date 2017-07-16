using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using System.Data;

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
        public string registrarPago(int pAlumno, int pClase, decimal pMonto, string pMes, int pFormaPago)
        {
            string sReturn="";
            
            DateTime pFecha = DateTime.Today.Date;
            string pHora = DateTime.Now.ToString("hh:mm:ss");
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
                    if (validarPago(alumnoSelect.id_alumno, pClase, pMes))
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
                        fecha=pFecha,
                        hora= pHora,
                        mes = pMes,
                        forma_pago=formaSelect
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
                                     orderby detalle.fecha descending
                                     select new
                                     {
                                         alu = pago.id_alumno,
                                         cla = pago.id_clase,
                                         mes = detalle.mes,
                                         fecha = detalle.fecha
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
    }
}
