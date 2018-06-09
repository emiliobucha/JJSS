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

            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                string sReturn = gestorAlumnos.EliminarAlumno(dni);
                Boolean estado = true;
                if (sReturn.CompareTo("") == 0) sReturn = "Se ha eliminado el alumno correctamente";
                else estado = false;
                mensaje(sReturn, estado);
                CargarGrilla();
            }
            else if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                Session["alumnoEditar"] = dni;
                Response.Redirect("../Administracion/RegistrarAlumno.aspx");
            }
            else if (e.CommandName.CompareTo("pago") == 0)
            {
                Session["PagoClase"] = dni.ToString();
                Response.Redirect("../Presentacion/PagoClase");
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
            chFiltroEstado.DataSource = estados;
            chFiltroEstado.DataTextField = "nombre";
            chFiltroEstado.DataValueField = "id_estado";
            chFiltroEstado.DataBind();
        }

        protected void CargarGrilla()
        {
            var filtroDni = "";
            if (!string.IsNullOrEmpty(txt_filtro_dni.Text)) filtroDni = txt_filtro_dni.Text;
            int[] filtroEstados = new int[5];

            for (int i = 0; i < chFiltroEstado.Items.Count; i++)
            {
                if (chFiltroEstado.Items[i].Selected)
                {
                    filtroEstados[i] = int.Parse(chFiltroEstado.Items[i].Value);
                }
                else filtroEstados[i] = 0;
            }

            Boolean sinFiltro = true;
            foreach (int i in filtroEstados)
            {
                if (i != 0)
                {
                    sinFiltro = false;
                    break;
                }
            }
            if (sinFiltro) filtroEstados[0] = 8;

            List<AlumnoConEstado> listaCompleta = gestorAlumnos.BuscarAlumnoConEstado(filtroEstados, txt_filtro_apellido.Text, filtroDni);

            gvAlumnos.DataSource = listaCompleta;
            gvAlumnos.DataBind();
        }
    }
}