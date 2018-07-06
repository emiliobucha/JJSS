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

            try
            {
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                if (sesionActiva.estado == "INGRESO ACEPTADO")
                {
                    int permiso = 0;
                    System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'PROFESOR_ADMINISTRACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente o tiene los permisos para estar aquí".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");
            }

            gestorProfes = new GestorProfesores();
            if (!IsPostBack)
            {
                if (Session["mensaje"] != null && Session["mensaje"].ToString().Trim() != "")
                {
                    mensaje(Session["mensaje"].ToString(), Convert.ToBoolean(Session["exito"]));
                    Session["mensaje"] = null;
                }

                CargarComboTipoDocumentos();
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
            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var id = int.Parse(gvprofes.DataKeys[index].Value.ToString());

                string sReturn = gestorProfes.EliminarProfesorID(id);
                Boolean estado = true;
                if (sReturn.CompareTo("") == 0) sReturn = "Se ha eliminado el profesor correctamente";
                else estado = false;
                mensaje(sReturn, estado);
                CargarGrilla();
            }
            else if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var id = int.Parse(gvprofes.DataKeys[index].Value.ToString());

                Session["profesorEditar"] = id;
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
            
            string filtroApellido = txt_filtro_apellido.Text.ToUpper();
            int idTipo = 0;
            int.TryParse(ddl_tipo.SelectedValue, out idTipo);


            List<PersonaResultado.ProfesorResultado> listaCompleta = gestorProfes.ObtenerProfesoresFiltro(idTipo, dni, filtroApellido);

            gvprofes.DataSource = listaCompleta;
            gvprofes.DataBind();
        }

        protected void btn_si_Click1(object sender, EventArgs e)
        {
            int id = int.Parse(txtIDSeleccionado.Text) ;
            string sReturn = gestorProfes.EliminarProfesorID(id);
            Boolean estado = true;
            if (sReturn.CompareTo("") == 0) sReturn = "Se ha eliminado el profesor correctamente";
            else estado = false;
            mensaje(sReturn, estado);
            CargarGrilla();
            txtIDSeleccionado.Text = "";
        }

        protected void CargarComboTipoDocumentos()
        {

            List<tipo_documento> tiposdoc = gestorProfes.ObtenerTiposDocumentos();
            tipo_documento primerElemento = new tipo_documento()
            {
                id_tipo_documento = 0,
                codigo = "Todos",
            };
            tiposdoc.Insert(0, primerElemento);



            ddl_tipo.DataSource = tiposdoc;
            ddl_tipo.DataTextField = "codigo";
            ddl_tipo.DataValueField = "id_tipo_documento";
            ddl_tipo.DataBind();

        }


    }
}