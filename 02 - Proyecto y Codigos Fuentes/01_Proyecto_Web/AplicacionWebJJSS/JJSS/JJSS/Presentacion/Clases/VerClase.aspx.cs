using System;
using JJSS_Entidad;
using JJSS_Negocio;
using System.Data;

namespace JJSS.Presentacion.Clases
{
    public partial class VerClase : System.Web.UI.Page
    {
        private GestorClases gestorClases;
        private static int id_Clase;
        protected void Page_Load(object sender, EventArgs e)
        {
            gestorClases = new GestorClases();
            if (!IsPostBack)
            {

                if (Session["clase"] == null)
                {
                    Response.Redirect("Menu_Clase.aspx");
                }else
                {
                    id_Clase = Convert.ToInt32(Session["clase"]);
                    Session["clase"] = null;
                }

                cargarDatos();
            }
        }

        private void cargarDatos()
        {
            clase datos_clase = gestorClases.ObtenerClasePorId(id_Clase);

            //cargar datos generales
            lbl_nombre_clase.Text = datos_clase.nombre.ToString();
            profesor profe = gestorClases.ObtenerProfesorPorID((int)datos_clase.id_profe);
            lbl_profesor.Text = "Profesor: " + profe.apellido + ", " + profe.nombre;
            lbl_ubicacion.Text = "Ubicación: " + gestorClases.ObtenerAcademiasPorID((int)datos_clase.id_ubicacion).nombre;
            lbl_tipo_clase.Text = "Tipo de Clase: " + gestorClases.ObtenerTipoClasesPorID((int)datos_clase.id_tipo_clase).nombre;
            lbl_precio.Text = "Precio: $" + datos_clase.precio.ToString();

            cargarGrilla();
        }

        private void cargarGrilla()
        {
            DataTable dtHorarios;

            dtHorarios = gestorClases.ObtenerTablaHorarios(id_Clase);
            dtHorarios.AcceptChanges();

            DataView dv_horarios = dtHorarios.DefaultView;
            dv_horarios.Sort = "dia asc, hora_desde asc";
            dg_horarios.DataSource = dv_horarios;
            dg_horarios.DataBind();
        }
    }
}