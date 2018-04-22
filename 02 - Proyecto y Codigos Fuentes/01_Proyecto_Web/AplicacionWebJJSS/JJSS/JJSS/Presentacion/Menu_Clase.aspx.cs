using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion
{
    public partial class Menu_Clase : System.Web.UI.Page
    {

        private GestorClases gestorDeClases;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorDeClases = new GestorClases();
            if (!IsPostBack)
            {

                cargarClasesView();

                if (Session["mensaje"] != null)
                {
                    mensaje(Session["mensaje"].ToString(), Boolean.Parse(Session["exito"].ToString()));
                    Session["mensaje"] = null;
                }
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

        protected void cargarClasesView()
        {
            lv_clasesDisponibles.DataSource = gestorDeClases.ObtenerClasesDisponibles();
            lv_clasesDisponibles.DataBind();

            lv_clasesDisponibles_invitado.DataSource = gestorDeClases.ObtenerClasesDisponibles();
            lv_clasesDisponibles_invitado.DataBind();
        }

        protected void lv_clasesDisponibles_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.CompareTo("inscribir") == 0)
            {
                Session["id_clase"] = id;
                Response.Redirect("~/Presentacion/InscripcionClase.aspx");
            }
            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                gestorDeClases.eliminarClase(id);
                Response.Write("<script>window.alert('" + "Se eliminó la clase correctamente".Trim() + "');</script>");
                cargarClasesView();
            }
            if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                Session["clase"] = id;
                Response.Redirect("~/Presentacion/CrearClase.aspx");
            }

        }

    }
}