using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;

namespace JJSS.Presentacion
{
    public partial class MenuInicial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {



                if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
                {
                    ocultarInvitado();

                }
                else
                {

                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado != "INGRESO ACEPTADO")
                    {

                        Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

                    }
                    ocultarPermiso();
                }

            }
            catch (Exception ex)
            {

                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

            }

        }


        protected void ocultarPermiso()
        {
            try
            {
               
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                if (sesionActiva.estado == "INGRESO ACEPTADO")
                {

                    //Administración de eventos

                    int permiso = 0;
                    System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'POWER_BI'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        menuBI.Style["display"] = "none";
                    }


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'ADMINISTRACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        menuAdministracion.Style["display"] = "none";
                    }



                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

            }
        }


        private void ocultarInvitado()
        {
            menuPerfil.Style["display"] = "none";
            menuAdministracion.Style["display"] = "none";
            menuBI.Style["display"] = "none";
            menuPagos.Style["display"] = "none";
        }
    }
}