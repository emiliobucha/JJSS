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
    public partial class AlumnoPagoClase : System.Web.UI.Page
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


                if (Session["Clase"] == null) Response.Redirect("AlumnoClase.aspx");

                lbl_fecha1.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                int id = int.Parse(Session["Clase"].ToString());
                var dni = Session["AlumnoDNI"].ToString();
                alumnoElegido = gestorAlumnos.ObtenerAlumnoPorDNI(dni);
                lbl_alumno.Text = alumnoElegido.apellido + ", " + alumnoElegido.nombre;

                clase oClase = gestorClase.ObtenerClasePorId(id);
                lbl_clase.Text = oClase.nombre;
                double recargo = gestorClase.calcularRecargo(id, alumnoElegido.id_alumno);


                double monto = recargo + (double)oClase.precio;
                lbl_monto.Text = "$" + monto;

                if (recargo > 0)
                {
                    pagoRecargo = 1;
                    lbl_recargo.Visible = true;
                    lbl_recargoMonto.Visible = true;
                    lbl_recargoMonto.Text = "$" + recargo;

                }
                else
                {
                    lbl_recargo.Visible = false;
                    lbl_recargoMonto.Visible = false;

                }
                String sInit_Point = "";
                gestorMP = new GestorMercadoPago();
                sInit_Point = gestorMP.NuevoPago(monto,"Pago de Clase");
                mp_checkout.Attributes.Add("href", sInit_Point);
            }
        }


        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("../Presentacion/AlumnoClases.aspx");
        }

        protected void limpiar()
        {

            lbl_alumno.Text = "No hay alumno seleccionado";
            Session["PagoClase"] = "";
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

    }
}