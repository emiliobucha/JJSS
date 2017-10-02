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
    public partial class PagoClase : System.Web.UI.Page
    {
        private GestorClases gestorClase;
        private GestorFormaPago gestorFPago;
        private GestorPagoClase gestorPago;
        private GestorAlumnos gestorAlumnos;

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
                //try
                //{
                //    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                //    if (sesionActiva.estado == "INGRESO ACEPTADO")
                //    {
                //        int permiso = 0;
                //        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'ALUMNO_CREACION'");
                //        if (drsAux.Length > 0)
                //        {
                //            int.TryParse(drsAux[0]["perm_modificar"].ToString(), out permiso);

                //        }
                //        if (permiso != 1)
                //        {
                //            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                //}
                CargarComboClase();
                CargarComboFormaPago();
                int dni = int.Parse(Session["PagoClase"].ToString());
                alumnoElegido = gestorAlumnos.ObtenerAlumnoPorDNI(dni);
                lbl_alumno.Text = alumnoElegido.apellido + ", " + alumnoElegido.nombre;

            }
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            Session["alumnos"] = "Administrar";
            Response.Redirect("../Presentacion/RegistrarAlumno.aspx");
        }


        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            decimal monto = decimal.Parse(txt_monto.Text.Replace(".", ","));
            string mes = ddl_mes.SelectedValue;
            int idClase = int.Parse(ddl_clase.SelectedValue);
            int idFormaPago = int.Parse(ddl_forma_pago.SelectedValue);
            int dni;
            if (int.TryParse(Session["PagoClase"].ToString(), out dni))
            {
                alumnoElegido = gestorAlumnos.ObtenerAlumnoPorDNI(dni);

                string sReturn = gestorPago.registrarPago(alumnoElegido.id_alumno, idClase, monto, mes, idFormaPago, pagoRecargo);
                if (sReturn.CompareTo("") == 0)
                {
                    mensaje("Se ha registrado el pago exitosamente", true);
                    limpiar();
                }
                else mensaje(sReturn, false);
            }
            else mensaje("No hay alumno seleccionado", false);

        }

        protected void limpiar()
        {
            txt_monto.Text = "";
            if (ddl_clase.Items.Count>0) ddl_clase.SelectedIndex = 0;

            ddl_forma_pago.SelectedIndex = 0;
            ddl_mes.SelectedIndex = 0;
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

        protected void CargarComboClase()
        {
            int dni;
            int.TryParse(Session["PagoClase"].ToString(), out dni);
            alumnoElegido = gestorAlumnos.ObtenerAlumnoPorDNI(dni);
            List<clase> clase = gestorClase.ObtenerClaseSegunAlumno(alumnoElegido.id_alumno);
            ddl_clase.DataSource = clase;
            ddl_clase.DataTextField = "nombre";
            ddl_clase.DataValueField = "id_clase";
            ddl_clase.DataBind();

            if (clase.Count == 0)
            {
                mensaje("El alumno no está inscripto a ninguna clase", false);
            }
        }

        protected void CargarComboFormaPago()
        {
            List<forma_pago> formasPago = gestorFPago.ObtenerFormasPago();
            ddl_forma_pago.DataSource = formasPago;
            ddl_forma_pago.DataTextField = "nombre";
            ddl_forma_pago.DataValueField = "id_forma_pago";
            ddl_forma_pago.DataBind();
        }

        protected void ddl_clase_SelectedIndexChanged(object sender, EventArgs e)
        {
            clase claseSelect = gestorClase.ObtenerClasePorId(int.Parse(ddl_clase.SelectedValue));
            int dni;
            int.TryParse(Session["PagoClase"].ToString(), out dni);
            alumnoElegido = gestorAlumnos.ObtenerAlumnoPorDNI(dni);

            double recargo = gestorClase.calcularRecargo(int.Parse(ddl_clase.SelectedValue), alumnoElegido.id_alumno);
            if (recargo == -1) mensaje("El alumno no está inscripto a esa clase", false);
            else
            {
                double monto = recargo + (double)claseSelect.precio;
                txt_monto.Text = monto.ToString();
            }
            if (recargo > 0) pagoRecargo = 1;

        }




        //protected void ddl_clase_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    clase claseSelect = gestorClase.ObtenerClasePorId(int.Parse(ddl_clase.SelectedValue));
        //    int dni;
        //    int.TryParse(Session["PagoClase"].ToString(), out dni);
        //    alumnoElegido = gestorAlumnos.ObtenerAlumnoPorDNI(dni);

        //    double recargo = gestorClase.calcularRecargo(int.Parse(ddl_clase.SelectedValue), alumnoElegido.id_alumno);
        //    if (recargo == -1) mensaje("El alumno no está inscripto a esa clase", false);
        //    else
        //    {
        //        double monto = recargo + (double)claseSelect.precio;
        //        txt_monto.Text = monto.ToString();
        //    }
        //    if (recargo > 0) pagoRecargo = 1;


        //}
    }
}