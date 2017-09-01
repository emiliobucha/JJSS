using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using System.Data;

namespace JJSS.Presentacion
{
    public partial class GraduarAlumno : System.Web.UI.Page
    {
        private GestorGraduacion gestorGraduacion;
        private GestorTipoClase gestorTipo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gestorTipo = new GestorTipoClase();
                gestorGraduacion = new GestorGraduacion();

                cargarGrilla();
                MultiView1.SetActiveView(view_elegir_graduacion);

                cargarRadioButton();

            }

        }

        protected void cargarGrilla()
        {
            int tipoClase = int.Parse(rb_tipo_clase.SelectedValue);
            List<Object> lista= new List<Object>();

            if (tipoClase==0)
            {
                gestorGraduacion = new GestorGraduacion();
                lista = gestorGraduacion.buscarFajasAlumnos();
            }
            else
            {
                gestorGraduacion = new GestorGraduacion();
                lista = gestorGraduacion.buscarFajasAlumnosConFiltro(tipoClase);
            }

             

            gv_graduacion.DataSource = lista;


            gv_graduacion.DataBind();


        }

        protected void cargarRadioButton()
        {
            List<tipo_clase> lista = gestorTipo.ObtenerTipoClase();
            tipo_clase tc = new tipo_clase();
            tc.id_tipo_clase = 0;
            tc.nombre = "Todos";
            lista.Add(tc);
            rb_tipo_clase.DataSource = lista;
            rb_tipo_clase.DataValueField = "id_tipo_clase";
            rb_tipo_clase.DataTextField = "nombre";
            
            rb_tipo_clase.DataBind();
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Presentacion/Inicio.aspx");
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("idAlumno");
            dt.Columns.Add("grados");
            dt.Columns.Add("fajaActual");
            for (int i = 0; i < gv_graduacion.Rows.Count; i++)
            {
                TextBox tb = (TextBox)gv_graduacion.Rows[i].Cells[4].FindControl("txt_grados");
                int grados = int.Parse(tb.Text);
                if (grados >= 1)
                {
                    int idAlu = Convert.ToInt32(gv_graduacion.DataKeys[i].Value);
                    string fajaActual = gv_graduacion.Rows[i].Cells[2].Text;

                    DataRow dr = dt.NewRow();
                    dr["idAlumno"] = idAlu;
                    dr["grados"] = grados;
                    dr["fajaActual"] = fajaActual;
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();


                }
            }
            gestorGraduacion = new GestorGraduacion();
            string result = "-";
             result = gestorGraduacion.graduar(dt);
            if (result == "")
            {
                mensaje("Graduado exitosamente", true);
                cargarGrilla();
            }
            else if (result!="-") mensaje(result, false);
        }

        class graduacion
        {
            private int idAlumno;
            private int grados;
            private string fajaActual;

            public graduacion(int pIdAlumno, int pGrados, string pFajaActual)
            {
                this.idAlumno = pIdAlumno;
                this.grados = pGrados;
                this.fajaActual = pFajaActual;
            }
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

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }
    }
}