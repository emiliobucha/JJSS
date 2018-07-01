using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Administracion;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion.Administracion
{
    public partial class AdministrarAlumnos : System.Web.UI.Page
    {
        private GestorAlumnos gestorAlumnos;
        private GestorEstados gestorEstados;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorAlumnos = new GestorAlumnos();
            gestorEstados = new GestorEstados();

            if (!IsPostBack)
            {
                CargarCheckboxEstados();
                CargarGrilla();
            }
        }

        protected void btn_buscar_alumno_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }


        protected void gvAlumnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAlumnos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void gvAlumnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            var dni = gvAlumnos.DataKeys[index].Value.ToString();

            if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                Session["alumnoEditar"] = dni;
                Response.Redirect("../Administracion/RegistrarAlumno.aspx");
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

        protected void CargarCheckboxEstados()
        {
            List<estado> estados = gestorEstados.obtenerEstados("ALUMNOS");
            ddl_filtro_estado.DataSource = estados;
            ddl_filtro_estado.DataTextField = "nombre";
            ddl_filtro_estado.DataValueField = "id_estado";
            ddl_filtro_estado.DataBind();
        }

        protected void CargarGrilla()
        {
            var filtroDni = "";
            if (!string.IsNullOrEmpty(txt_filtro_dni.Text)) filtroDni = txt_filtro_dni.Text;
            //int[] filtroEstados = new int[5];

            int filtroEstados = 8;
            int.TryParse(ddl_filtro_estado.SelectedValue, out filtroEstados);

            List<AlumnoConEstado> listaCompleta = gestorAlumnos.BuscarAlumnoConEstado(filtroEstados, txt_filtro_apellido.Text, filtroDni);

            gvAlumnos.DataSource = listaCompleta;
            gvAlumnos.DataBind();
        }
        
        protected void btn_si_Click1(object sender, EventArgs e)
        {
            string dni = txtIDSeleccionado.Text;
            string sReturn = gestorAlumnos.EliminarAlumno(dni);
            Boolean estado = true;
            if (sReturn.CompareTo("") == 0) sReturn = "Se ha eliminado el alumno correctamente";
            else estado = false;
            mensaje(sReturn, estado);
            CargarGrilla();
            txtIDSeleccionado.Text = "";
        }
    }
}