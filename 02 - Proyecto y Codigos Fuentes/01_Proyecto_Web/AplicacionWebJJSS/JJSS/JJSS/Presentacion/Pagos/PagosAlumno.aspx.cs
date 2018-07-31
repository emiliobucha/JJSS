using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados.Pagos;
using Newtonsoft.Json;

namespace JJSS.Presentacion.Pagos
{
    public partial class PagosAlumno : System.Web.UI.Page
    {
        private GestorPagos gestorPagos;
        private GestorInscripciones gestorInscripciones;
        private GestorInscripcionesEvento gestorInscripcionesEvento;
        private GestorAlumnos gestorAlumnos;



        private static int tipoDoc;
        private static string dni;
        private static string nombre;
        private static List<ObjetoPagable> objetosGrilla;
        private static List<tipo_documento> tiposdoc;



        protected void Page_Load(object sender, EventArgs e)
        {
            gestorPagos = new GestorPagos();
            gestorInscripciones = new GestorInscripciones();
            gestorAlumnos = new GestorAlumnos();

            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'PAGO_ALUMNO'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                            var usuario = sesionActiva.usuario;
                            if (usuario != null)
                            {
                                var alumno = gestorAlumnos.ObtenerAlumnoPorIdUsuario(usuario.id_usuario);


                                if (alumno != null)
                                {
                                    if (alumno.id_tipo_documento != null) tipoDoc = (int) alumno.id_tipo_documento;
                                    dni = alumno.dni;
                                    nombre = alumno.nombre + " " + alumno.apellido;
                                    CargarGrilla();
                                    divDNIAlumno.Visible = true;
                                    divDNI.Visible = false;
                                    lblDni.InnerText = dni;
                                    lblNombre.InnerText = nombre;

                                    var td = tiposdoc.FirstOrDefault(x => x.id_tipo_documento == tipoDoc);
                                    if (td != null)
                                    {
                                        lblTipoDoc.InnerText = td.codigo;
                                    }


                                }
                                else
                                {
                                    divDNI.Visible = true;
                                    divDNIAlumno.Visible = false;
                                }
                            }



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

                CargarComboTipoDocumentos();
                

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


        protected void gvPagos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // To check condition on integer value
            if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Pago")) == 1)
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
        }


        protected void CargarGrilla()
        {
          
            objetosGrilla = gestorPagos.ObtenerObjetosPagablesHistoricos(tipoDoc, dni);
            if (objetosGrilla.Count > 0)
            {
                nombre = objetosGrilla[0].NombreParticipante;
                lblNombreBuscado.InnerText = nombre;
            }

            gvPagos.DataSource = objetosGrilla;
            gvPagos.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int idTipo;
            int.TryParse(ddl_tipo.SelectedValue, out idTipo);
            tipoDoc = idTipo;
            dni = txtDni.Text;

            CargarGrilla();
        }

       
        protected void CargarComboTipoDocumentos()
        {

            tiposdoc = gestorInscripciones.ObtenerTiposDocumentos();
            ddl_tipo.DataSource = tiposdoc;
            ddl_tipo.DataTextField = "codigo";
            ddl_tipo.DataValueField = "id_tipo_documento";
            ddl_tipo.DataBind();

        }

      
    }
}