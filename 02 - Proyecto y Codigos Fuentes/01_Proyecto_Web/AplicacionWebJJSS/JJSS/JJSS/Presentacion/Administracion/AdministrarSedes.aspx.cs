using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio.Administracion;

namespace JJSS.Presentacion.Administracion
{
    public partial class AdministrarSedes : System.Web.UI.Page
    {
        private GestorSedes gs;
        private GestorAcademias ga;

        protected void Page_Load(object sender, EventArgs e)
        {
            gs = new GestorSedes();
            ga = new GestorAcademias();
            if (!IsPostBack)
            {
                if (Session["mensaje"] != null)
                {
                    mensaje(Session["mensaje"].ToString(), Convert.ToBoolean(Session["exito"]));
                    Session["mensaje"] = null;
                }
                CargarComboCiudades();
                CargarGrilla();
            }
        }

        private void CargarComboCiudades()
        {
            List<ciudad> ciudadList = gs.ObtenerCiudades();
            ciudad primerElemento = new ciudad()
            {
                id_ciudad = 0,
                nombre = "Todos",
            };
            ciudadList.Insert(0, primerElemento);
            ddl_filtro_ciudad.DataSource = ciudadList;
            ddl_filtro_ciudad.DataValueField = "id_ciudad";
            ddl_filtro_ciudad.DataTextField = "nombre";
            ddl_filtro_ciudad.DataBind();

        }

        private void CargarGrilla()
        {
            //filtros
            string filtroNombre = txt_filtro_nombre.Text;
            string filtroCiudad = ddl_filtro_ciudad.SelectedItem.Text;
            int filtroSede = Convert.ToInt16(rbSede.SelectedItem.Value);

            if (filtroSede == 0)
            {
               gvSedes.DataSource = gs.ObtenerSedesConFiltro(filtroNombre, filtroCiudad);
                gvSedes.DataBind();
            }else if (filtroSede == 1)
            {
                gvSedes.DataSource = ga.ObtenerAcademiasConFiltro(filtroNombre, filtroCiudad);
                gvSedes.DataBind();
            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }

        protected void gvSedes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSedes.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void gvSedes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvSedes.DataKeys[index].Value);
            int filtroSede = Convert.ToInt16(rbSede.SelectedItem.Value);

            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                string res = "";

                if (filtroSede == 0)
                {
                    res = gs.EliminarSede(id);
                }
                else if (filtroSede == 1)
                {
                    res = ga.EliminarAcademia(id);
                }

                res = gs.EliminarSede(id);
                if (res.CompareTo("") == 0) mensaje("Se eliminó la sede exitosamente", true);
                else mensaje(res, false);
                CargarGrilla();
            }
            else if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                if (filtroSede == 0) Session["sede"] = id;
                else if (filtroSede == 1) Session["academia"] = id;

                Response.Redirect("CrearSede.aspx");
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
    }
}