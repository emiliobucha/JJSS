using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;


namespace JJSS.Presentacion.Pagos
{
    public partial class PagosPanel : System.Web.UI.Page
    {
        
        

        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        protected void btn_buscar_alumno_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }


        protected void gvPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPagos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void gvAlumnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            var dni = gvPagos.DataKeys[index].Value.ToString();

         
            if (e.CommandName.CompareTo("pago") == 0)
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

    

        protected void CargarGrilla()
        {
          
        }
    }
}
