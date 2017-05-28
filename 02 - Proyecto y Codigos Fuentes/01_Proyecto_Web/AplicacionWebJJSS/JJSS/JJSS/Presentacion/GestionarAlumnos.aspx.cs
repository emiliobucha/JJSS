using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;

namespace JJSS.Presentacion
{
    public partial class GestionarAlumnos : System.Web.UI.Page
    {
        private GestorAlumnos gestorAlumnos;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorAlumnos = new GestorAlumnos();

            if (!IsPostBack)
            {


                //carga de grilla
                ViewState["gvAlumnosOrden"] = "dni";
                gvAlumnos.AllowPaging = true;
                gvAlumnos.AllowSorting = true;
                gvAlumnos.AutoGenerateColumns = false;
                gvAlumnos.PageSize = 10;
                CargarGrilla();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void CargarGrilla()
        {
            gvAlumnos.DataSource = gestorAlumnos.BuscarAlumnoPorApellido(txt_filtro_apellido.Text);
            gvAlumnos.DataBind();
        }

        protected void gvAlumnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAlumnos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void gvAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {

            int dni = (int)gvAlumnos.SelectedValue;
            string sReturn = gestorAlumnos.EliminarAlumno(dni);

            if (sReturn.CompareTo("") == 0) sReturn = "Se ha eliminado el alumno correctamente";

            mensaje(sReturn, "GestionarAlumnos.aspx");

        }

        private void mensaje(string pMensaje, string pRef)
        {
            Response.Write("<script>window.alert('" + pMensaje.Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + pRef + "', 2000);</script>");
        }

        protected void btn_buscar_alumno_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void gvAlumnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int dni = (int)gvAlumnos.SelectedValue;
                string sReturn = gestorAlumnos.EliminarAlumno(dni);

                if (sReturn.CompareTo("") == 0)
                {
                    mensaje("Se ha eliminado el alumno correctamente", "RegistrarAlumno.aspx");
                }
                else
                {
                    mensaje(sReturn, "RegistrarAlumno.aspx");
                }
            }
        }

    }
}