using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;
using JJSS_Negocio.Administracion;

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
                cargarComboAcademias();
                cargarComboProfesores();
                cargarClasesView();

                if (Session["mensaje"] != null)
                {
                    mensaje(Session["mensaje"].ToString(), Boolean.Parse(Session["exito"].ToString()));
                    Session["mensaje"] = null;
                }
            }
        }

        private void cargarComboProfesores()
        {
            GestorProfesores gp = new GestorProfesores();
            List<profesor> profesores = gp.ObtenerProfesores();
            foreach(profesor p in profesores)
            {
                p.nombre = p.nombre + " " + p.apellido;
            }
            profesor itemTodos = new profesor()
            {
                id_profesor = 0,
                nombre = "Todos"
            };
            profesores.Insert(0,itemTodos);
            ddl_profesores.DataSource = profesores;
            ddl_profesores.DataValueField = "id_profesor";
            ddl_profesores.DataTextField = "nombre";
            ddl_profesores.DataBind();
        }

        private void cargarComboAcademias()
        {
            GestorAcademias ga = new GestorAcademias();
            List<academia> academias = ga.ObtenerAcademias();
            academia itemTodos = new academia()
            {
                id_academia = 0,
                nombre = "Todas"
            };
            academias.Insert(0, itemTodos);
            ddl_academias.DataSource = academias;
            ddl_academias.DataValueField = "id_academia";
            ddl_academias.DataTextField = "nombre";
            ddl_academias.DataBind();
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
            string filtroNombre = txt_filtro_nombre.Text.Trim();
            int filtroAcademia = int.Parse(ddl_academias.SelectedValue);
            int filtroProfesor = int.Parse(ddl_profesores.SelectedValue);

            List<ClasesDisponibles> clasesDisponibles = gestorDeClases.ObtenerClasesDisponibles(filtroNombre, filtroProfesor, filtroAcademia);
            lv_clasesDisponibles.DataSource = clasesDisponibles;
            lv_clasesDisponibles.DataBind();

            lv_clasesDisponibles_invitado.DataSource = clasesDisponibles;
            lv_clasesDisponibles_invitado.DataBind();
        }

        protected void lv_clasesDisponibles_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.CompareTo("inscribir") == 0)
            {
                Session["id_clase"] = id;
                Response.Redirect("InscripcionClase.aspx");
            }
            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                
            }
            if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                Session["clase"] = id;
                Response.Redirect("CrearClase.aspx");
            }

        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarClasesView();
        }

        protected void lv_clasesDisponibles_invitado_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.CompareTo("ver") == 0)
            {
                Session["clase"] = id;
                Response.Redirect("VerClase.aspx");
            }
        }

        protected void btn_si_Click1(object sender, EventArgs e)
        {
            if (txtIDSeleccionado.Text != "")
            {
                int id = Convert.ToInt32(txtIDSeleccionado.Text);
                gestorDeClases.eliminarClase(id);
                mensaje("Se eliminó la clase correctamente", true);
                cargarClasesView();
            }
        }
    }
}