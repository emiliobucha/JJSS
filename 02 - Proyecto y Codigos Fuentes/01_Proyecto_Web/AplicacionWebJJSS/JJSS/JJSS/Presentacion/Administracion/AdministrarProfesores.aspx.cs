using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;


namespace JJSS.Presentacion.Administracion
{
    public partial class AdministrarProfesores : System.Web.UI.Page
    {
        private GestorProfesores gestorProfes;
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorProfes = new GestorProfesores();
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        protected void btn_buscar_profe_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void gvprofes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvprofes.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void gvprofes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            var dni = gvprofes.DataKeys[index].Value.ToString();

            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                string sReturn = gestorProfes.EliminarProfesor(dni);
                Boolean estado = true;
                if (sReturn.CompareTo("") == 0) sReturn = "Se ha eliminado el profesor correctamente";
                else estado = false;
                mensaje(sReturn, estado);
                CargarGrilla();
            }
            else if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                Session["profesorEditar"] = dni;
                Response.Redirect("../Administracion/RegistrarProfe.aspx");
            }
        }

        private void mensaje(string pMensaje, Boolean pEstado)
        {
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
        
        protected void CargarGrilla()
        {
            var dni = txt_filtro_dni.Text;
            List<profesor> listaCompleta = gestorProfes.ObtenerProfesores();
            List<profesor> listaConFiltro = new List<profesor>();

            string filtroApellido = txt_filtro_apellido.Text.ToUpper();

            foreach (profesor i in listaCompleta)
            {
                string apellido = i.apellido.ToUpper();
                if (string.IsNullOrEmpty(dni)) if (apellido.StartsWith(filtroApellido)) listaConFiltro.Add(i);

                if (apellido.StartsWith(filtroApellido) && i.dni == dni) listaConFiltro.Add(i);
            }

            gvprofes.DataSource = listaConFiltro;
            gvprofes.DataBind();
        }
    }
}