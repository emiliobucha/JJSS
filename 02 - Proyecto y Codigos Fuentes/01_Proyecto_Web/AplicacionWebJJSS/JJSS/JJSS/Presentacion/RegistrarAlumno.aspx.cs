
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;


namespace JJSS.Presentacion
{
    public partial class RegistrarAlumno : System.Web.UI.Page
    {
        private GestorInscripciones gestorInscripciones;
        private GestorAlumnos gestorAlumnos;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorAlumnos = new GestorAlumnos();
            gestorInscripciones = new GestorInscripciones();
            if (!IsPostBack)
            {
                CargarComboFajas();

                //carga de grilla
                ViewState["gvAlumnosOrden"] = "dni";
                gvAlumnos.AllowPaging = true;
                gvAlumnos.AllowSorting = true;
                gvAlumnos.AutoGenerateColumns = false;
                gvAlumnos.PageSize = 10;
                CargarGrilla();
                gvAlumnos.DataBind();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CargarComboFajas()
        {
            List<faja> fajas = gestorInscripciones.ObtenerFajas();
            ddl_fajas.DataSource = fajas;
            ddl_fajas.DataTextField = "color";
            ddl_fajas.DataValueField = "id_faja";
            ddl_fajas.DataBind();
        }

        protected void CargarGrilla()
        {
            String orden = ViewState["gvAlumnosOrden"].ToString();
            gvAlumnos.DataSource = gestorAlumnos.BuscarAlumnoPorApellido(txt_filtro_apellido.Text,orden);
            gvAlumnos.DataBind();
        }

        protected void gvAlumnos_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["gvAlumnosOrden"] = e.SortExpression;  //titulo de la columna que hizo click, la base de datos es mas optima para ordenarlos.
            CargarGrilla();
        }

        protected void gvAlumnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAlumnos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void gvAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int IdAuto = (int)gvAlumnos.SelectedValue;
        }

        protected void btn_guardar_click(object sender, EventArgs e)
        {
            //+ver categorias, direccion y foto perfil
            int dni = int.Parse(txtDni.Text);
            string nombre = txt_nombres.Text;
            string apellido = txt_apellido.Text;
            DateTime fechaNac = DateTime.Parse(dp_fecha.Text);
            int idFaja = int.Parse(ddl_fajas.SelectedValue);
            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = 0; //Femenino
            if (rbSexo.SelectedIndex == 1) sexo = 1; //Masculino
            int tel = int.Parse(txt_telefono.Text);
            string mail = txt_email.Text;
            int telEmergencia = int.Parse(txt_telefono_urgencia.Text);
            
            string sReturn = gestorAlumnos.RegistrarAlumno(nombre,apellido,fechaNac,idFaja,1,sexo,dni,tel,mail,1,telEmergencia);

            if (sReturn.CompareTo("") == 0)
            {
                mensaje("Creación exitosa", "Inicio.aspx");
            }
            else
            {
                mensaje(sReturn, "RegistrarAlumno.aspx");
            }
        }

        private void mensaje(string pMensaje, string pRef)
        {
            Response.Write("<script>window.alert('" + pMensaje.Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + pRef + "', 2000);</script>");
        }


    }
}