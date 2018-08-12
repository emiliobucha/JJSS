using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using System.Web;

namespace JJSS.Presentacion.Pagos
{
    public partial class MisPagos : System.Web.UI.Page
    {
        private static GestorPagos gestorPagos;
        private static DateTime fechaDesdeG;
        private static DateTime fechaHastaG;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorPagos = new GestorPagos();
            if (!IsPostBack)
            {
                DateTime fechaDesde = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DateTime fechaHasta = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                    DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

                dp_fecha_desde.Text = fechaDesde.ToString("dd/MM/yyyy");
                dp_fecha_hasta.Text = fechaHasta.ToString("dd/MM/yyyy");

                fechaDesdeG = fechaDesde;
                fechaHastaG = fechaHasta;
                CargarGrillaPagados(fechaDesde, fechaHasta);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime.TryParse(dp_fecha_desde.Text, out fechaDesde);

            DateTime fechaHasta = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            DateTime.TryParse(dp_fecha_hasta.Text, out fechaHasta);

            fechaDesdeG = fechaDesde;
            fechaHastaG = fechaHasta;

            CargarGrillaPagados(fechaDesde, fechaHasta);
        }

        protected void gvPagos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TipoPago.Tipo")) == "Torneo")
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
            if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TipoPago.Tipo")) == "Evento")
            {
                e.Row.BackColor = System.Drawing.Color.LightBlue;
            }
            if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TipoPago.Tipo")) == "Clase")
            {
                e.Row.BackColor = System.Drawing.Color.LightYellow;
            }
        }

        protected void gvPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPagos.PageIndex = e.NewPageIndex;
            CargarGrillaPagados(fechaDesdeG, fechaHastaG);
        }

        private void CargarGrillaPagados(DateTime fechaDesde, DateTime fechaHasta)
        {
            Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
            if (sesionActiva.estado == "INGRESO ACEPTADO")
            {
                GestorAlumnos gestorAlumnos = new GestorAlumnos();

                var usuario = sesionActiva.usuario;
                if (usuario != null)
                {
                    var alumno = gestorAlumnos.ObtenerAlumnoPorIdUsuario(usuario.id_usuario);

                    if (alumno != null)
                    {
                        var pagos = gestorPagos.ObtenerObjetosPagablesPagadosPorAlumno(fechaDesde, fechaHasta, alumno.id_alumno);
                        gvPagos.DataSource = pagos;
                        gvPagos.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente o tiene los permisos para estar aquí".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "/Presentacion/Login.aspx" + "', 2000);</script>");
                    }
                }
            }
        }
    }
}