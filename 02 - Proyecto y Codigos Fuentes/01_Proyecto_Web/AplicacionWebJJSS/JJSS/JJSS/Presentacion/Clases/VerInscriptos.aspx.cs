using System;
using System.Collections.Generic;
using System.Globalization;
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
        private static GestorAlumnos gestorAlumnos;
        private static int idClaseGlobal;
        private static bool filtros;

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
                gestorAlumnos = new GestorAlumnos();

                CargarComboTipoDocumentos();
                CargarComboClases();
                dp_fecha_desde.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                dp_fecha_hasta.Text = new DateTime(DateTime.Now.Year,DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).ToString("dd/MM/yyyy");

                if (Session["idClase"] != null)
                {
                    var idClase = (int)Session["idClase"];
                    idClaseGlobal = idClase;
                    Session["idClase"] = null;
                    CargarGrillaFiltros();
                    CargarInformacion(idClase);

                }
                else
                {
                    div_combo_clase.Visible = true;
                    div_label_clase.Visible = true;
                    div_nombre_clase.Visible = false;
                }
            }





        }

        private void CargarGrilla(int idClase)
        {

            var listado = gestorInscripciones.ObtenerAlumnosInscriptosClase(idClase, null, null, null,null,null);


            gv_inscripciones.Visible = true;
            gv_inscripciones.DataSource = listado;
            gv_inscripciones.DataBind();

        }

        private void CargarGrillaFiltros()
        {
            try
            {
                if (div_combo_clase.Visible)
                {
                    int idClase;
                    int.TryParse(ddl_clase.SelectedValue, out idClase);

                    idClaseGlobal = idClase;
                }


                int idTipoDoc;
                int.TryParse(ddl_tipo.SelectedValue, out idTipoDoc);


                var dni = txt_filtro_dni.Text;

                var apellido = txt_filtro_apellido.Text;

                var desde = dp_fecha_desde.Text;
                var hasta = dp_fecha_hasta.Text;
                var desdeDt = DateTime.ParseExact(desde,"dd/MM/yyyy",CultureInfo.CurrentCulture);
                var hastaDt = DateTime.ParseExact(hasta, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                var listado = gestorInscripciones.ObtenerAlumnosInscriptosClase(idClaseGlobal, idTipoDoc, dni, apellido,desdeDt,hastaDt );

                gv_inscripciones.Visible = true;
                gv_inscripciones.DataSource = listado;
                gv_inscripciones.DataBind();

            }
            catch (Exception)
            {
                Mensaje("No se pudo obtener los alumnos", false);
            }
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
            CargarGrillaFiltros();
        }



        protected void CargarComboClases()
        {

            List<ClasesDisponibles> clases = gestorClases.ObtenerClasesDisponibles("", 0, 0, 0);
            ddl_clase.DataSource = clases;
            ddl_clase.DataTextField = "nombre";
            ddl_clase.DataValueField = "id_clase";
            ddl_clase.DataBind();

        }

        protected void CargarComboTipoDocumentos()
        {
            List<tipo_documento> tiposdoc = gestorAlumnos.ObtenerTiposDocumentos();
            tipo_documento tc = new tipo_documento();
            tc.id_tipo_documento = 0;
            tc.codigo = "Todos";
            tiposdoc.Insert(0, tc);

           
            ddl_tipo.DataSource = tiposdoc;
            ddl_tipo.DataTextField = "codigo";
            ddl_tipo.DataValueField = "id_tipo_documento";
            ddl_tipo.DataBind();

        }

        protected void gv_inscripciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("modificar") == 0)
            {

                int index = Convert.ToInt32(e.CommandArgument);
                //var id = int.Parse(gv_inscripciones.DataKeys[index].Value.ToString());
                //Session["idClase"] = idClaseGlobal;

                Session["idInscripcion"] = index;

                Response.Redirect("../Pagos/PagosCambioVto");
            }
            if (e.CommandName.CompareTo("moroso") ==0)
            {
                int index = Convert.ToInt32(e.CommandArgument);
               
                var retorno = gestorInscripciones.HabilitarDeshabilitarMoroso(index);
                if (string.IsNullOrEmpty(retorno))
                {
                    Mensaje("Se ha actualizado la inscripción del alumno correctamente",true);
                }
                else
                {
                    Mensaje("Ha ocurrido un error al tratar de actualizar la inscripción del alumno. Error: " + retorno,false);
                }
                CargarGrillaFiltros();
            }

        }

        protected void gv_inscripciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lblPago = (LinkButton)e.Row.FindControl("habilitar");

                var valorDataItem = DataBinder.Eval(e.Row.DataItem, "moroso_si");

                int moroso_si;
                int.TryParse(valorDataItem.ToString(), out moroso_si);

                bool moroso = moroso_si == 1  ;
                if (moroso)
                {
                    lblPago.Text = "Deshabilitar";
                }
                else
                {
                    lblPago.Text = "Habilitar";
                }

            }
        }
    }
}