using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;

using System.Collections;

namespace JJSS.Presentacion
{
    public partial class AlumnoPagoFinalizado : System.Web.UI.Page
    {
        private GestorClases gestorClase;
        private GestorFormaPago gestorFPago;
        private GestorPagoClase gestorPago;
        private GestorAlumnos gestorAlumnos;
        private GestorMercadoPago gestorMP;
        private alumno alumnoElegido;
        private short pagoRecargo = 0; //si es 0 no pago recargo, si es 1 si lo pago


        protected void Page_Load(object sender, EventArgs e)
        {


            gestorClase = new GestorClases();
            gestorFPago = new GestorFormaPago();
            gestorAlumnos = new GestorAlumnos();
            gestorPago = new GestorPagoClase();

            if (!IsPostBack)
            {

                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASES_MIS_CLASES'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                }
                if (Session["Clase"] == null || Session["Clase"].ToString() == "") Response.Redirect("AlumnoClases.aspx");



                var resultado = Request["Estado"];
                if (resultado == "ok")
                {

                    lbl_fecha1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                    int id = int.Parse(Session["Clase"].ToString());
                    int dni = int.Parse(Session["AlumnoDNI"].ToString());
                    alumnoElegido = gestorAlumnos.ObtenerAlumnoPorDNI(dni);
                    clase oClase = gestorClase.ObtenerClasePorId(id);
                    lbl_alumno.Text = alumnoElegido.apellido + ", " + alumnoElegido.nombre;
                    lbl_clase.Text = oClase.nombre;

                    double recargo = gestorClase.calcularRecargo(id, alumnoElegido.id_alumno);
                    double monto = recargo + (double)oClase.precio;
                    string mes = DateTime.Today.Month.ToString();



                    if (int.TryParse(Session["AlumnoDNI"].ToString(), out dni))
                    {
                        alumnoElegido = gestorAlumnos.ObtenerAlumnoPorDNI(dni);

                        string sReturn = gestorPago.registrarPago(alumnoElegido.id_alumno, id, (decimal)monto, mes, 4, pagoRecargo);
                        if (sReturn.CompareTo("") == 0)
                        {
                            mensaje("Se ha registrado el pago exitosamente", true);
                            limpiar();
                        }
                        else mensaje(sReturn, false);
                    }
                    else mensaje("No hay alumno seleccionado", false);
                    Session["Clase"] = null;
                }
                else
                {
                    Session["Clase"] = null;
                    Response.Redirect("AlumnoClases.aspx");
                }


            }


        }




        protected void limpiar()
        {

            lbl_alumno.Text = "No hay alumno seleccionado";
            Session["Clase"] = "";
            pagoRecargo = 0;
        }

        private void mensaje(string pMensaje, Boolean pEstado)
        {
            // Response.Write("<script>window.alert('" + pMensaje.Trim() + "');</script>");
            if (pEstado == true)
            {
                pnl_mensaje_exito.Visible = true;
                pnl_mensaje_error.Visible = false;
                lbl_exito.Text = pMensaje;
            }
            else
            {
                pnl_mensaje_exito.Visible = false;
                pnl_mensaje_error.Visible = true;
                lbl_error.Text = pMensaje;
            }
        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("AlumnoClases.aspx");
        }
    }
}