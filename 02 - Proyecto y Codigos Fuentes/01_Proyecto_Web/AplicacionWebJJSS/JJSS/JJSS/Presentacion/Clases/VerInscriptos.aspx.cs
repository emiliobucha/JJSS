using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion.Clases
{
    public partial class VerInscriptos : System.Web.UI.Page
    {
        private static GestorInscripcionesClase gestorInscripciones;
        private static GestorClases gestorClases;
        private static int idClaseGlobal;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_INSCRIPCION'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" +
                                           "No se encuentra logueado correctamente o no tiene los permisos para estar aquí"
                                               .Trim() + "');</script>" + "<script>window.setTimeout(location.href='" +
                                           "../Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() +
                                   "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" +
                                   "', 2000);</script>");
                }


                gestorInscripciones = new GestorInscripcionesClase();
                gestorClases = new GestorClases();

                CargarComboClases();
                if (Session["idClase"] != null)
                {
                    var idClase = (int)Session["idClase"];
                    idClaseGlobal = idClase;

                    CargarGrilla(idClase);
                    CargarInformacion(idClase);

                }
                else
                {
                    div_combo_clase.Visible = true;
                    div_nombre_clase.Visible = false;
                }
            }

     

         

        }

        private void CargarGrilla(int idClase)
        {
            var listado = gestorInscripciones.ObtenerAlumnosInscriptosClase(idClase);


            gv_inscripciones.Visible = true;
            gv_inscripciones.DataSource = listado;
            gv_inscripciones.DataBind();

        }

        private void CargarInformacion(int idClase)
        {
            lbl_nombre_clase.Text = gestorClases.ObtenerClasePorId(idClase).nombre;
        }

        protected void gv_participantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_inscripciones.PageIndex = e.NewPageIndex;
            CargarGrilla(idClaseGlobal);
        }


        protected void btn_imprimir_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    String sFile = gestorEventos.GenerarListado(eventoSeleccionado.id_evento);

            //    Response.Clear();
            //    Response.AddHeader("Content-Type", "Application/octet-stream");
            //    Response.AddHeader("Content-Disposition", "attachment; filename=\"" + System.IO.Path.GetFileName(sFile) + "\"");
            //    Response.WriteFile(sFile);
            //}
            //catch (Exception ex)
            //{
            //    Mensaje("No se encuentran alumnos inscriptos a ese torneo", false);
            //}
        }

        private void Mensaje(string pMensaje, bool pEstado)
        {
            if (pEstado)
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
            try
            {
                int idClase;
                int.TryParse(ddl_clase.SelectedValue, out idClase);

                idClaseGlobal = idClase;
                CargarGrilla(idClase);


            }
            catch (Exception)
            {
                Mensaje("No se pudo obtener los alumnos", false);
            }
        }



        protected void CargarComboClases()
        {

            List<ClasesDisponibles> clases= gestorClases.ObtenerClasesDisponibles("",0,0,0);
            ddl_clase.DataSource = clases;
            ddl_clase.DataTextField = "nombre";
            ddl_clase.DataValueField = "id_clase";
            ddl_clase.DataBind();

        }
    }
}