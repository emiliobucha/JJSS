using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;

namespace JJSS.Presentacion
{
    public partial class Power_BI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Redirect("MenuInicial.aspx");
            try
            {
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                if (sesionActiva.estado == "INGRESO ACEPTADO")
                {
                    int permiso = 0;
                    System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'POWER_BI'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                    }
                    if (permiso != 1)
                    {
                        Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente o no tiene los permisos para estar aquí".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "Presentacion/Login.aspx" + "', 2000);</script>");

                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "Presentacion/Login.aspx" + "', 2000);</script>");
            }
        }
    }
}