using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;
using System.Data;

namespace JJSS.Presentacion.Clases
{
    public partial class RegistrarAsistenciasAnteriores : System.Web.UI.Page
    {
        private GestorAsistencia ga;
        protected void Page_Load(object sender, EventArgs e)
        {
            ga = new GestorAsistencia();
            if (!IsPostBack)
            {
                CargarComboClases();
                
            }
        }

        private void CargarComboClases()
        {
            GestorClases gc = new GestorClases();
            List<ClasesDisponibles> clases = gc.ObtenerClasesDisponibles("", 0, 0, 0);

            ClasesDisponibles primerElemento = new ClasesDisponibles()
            {
                id_clase = -1,
                nombre = "Seleccione una clase",
            };
            clases.Insert(0, primerElemento);
            ddl_clases.DataSource = clases;
            ddl_clases.DataValueField = "id_clase";
            ddl_clases.DataTextField = "nombre";
            ddl_clases.DataBind();
        }

        private void CargarGrilla()
        {
            int idClase = Convert.ToInt32(ddl_clases.SelectedValue);
            DateTime fecha = DateTime.Parse(dp_fecha.Text);
            try
            {
                gv_inscriptos.DataSource = ga.BuscarAlumnosConAsistencia(idClase, fecha);
                gv_inscriptos.DataBind();
                chkSelectAll.Visible = true;
                lbl_select_all.Visible = true;
            }
            catch (Exception ex)
            {
                mensaje(ex.Message, false);
            }
        }

        private void CargarDatosClase()
        {

        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            chkSelectAll.Visible = false;
            lbl_select_all.Visible = false;
            gv_inscriptos.DataSource = null;
            gv_inscriptos.DataBind();

            int idClase = int.Parse(ddl_clases.SelectedValue);
            if (idClase == -1)
            {
                mensaje("Debe seleccionar una clase", false);
                return;
            }

            CargarDatosClase();
            CargarGrilla();
            
        }

        protected void gv_inscriptos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_inscriptos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void gv_inscriptos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = ((CheckBox)e.Row.FindControl("chk_asistio"));
                Boolean asistio = Convert.ToBoolean( DataBinder.Eval(e.Row.DataItem, "asistio"));
                chk.Checked = asistio;
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

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            List<AlumnoAsistencia> alumnos = new List<AlumnoAsistencia>();
            for (int i = 0; i < gv_inscriptos.Rows.Count; i++)
            {
                int idAlu = Convert.ToInt32(gv_inscriptos.DataKeys[i].Value);
                CheckBox chk = (CheckBox)gv_inscriptos.Rows[i].Cells[3].FindControl("chk_asistio");
                //CheckBox chk = (CheckBox)gv_inscriptos.Rows[i].Cells[4].FindControl("chk_asistio");
                AlumnoAsistencia alu_agregar = new AlumnoAsistencia()
                {
                    alu_id = idAlu,
                    asistio = chk.Checked,
                };
                alumnos.Add(alu_agregar);
            }

            try
            {
                ga.RegistrarMuchasAsistencias(alumnos, Convert.ToInt32(ddl_clases.SelectedValue), DateTime.Parse(dp_fecha.Text));
                mensaje("Se registraron las asistencias correctamente", true);
            }
            catch(Exception ex)
            {
                mensaje(ex.Message, false);
            }
        }
    }
}