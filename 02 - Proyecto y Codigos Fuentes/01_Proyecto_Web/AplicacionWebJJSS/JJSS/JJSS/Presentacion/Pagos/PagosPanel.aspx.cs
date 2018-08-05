using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Constantes;
using JJSS_Negocio.Resultados.Pagos;
using Newtonsoft.Json;


namespace JJSS.Presentacion.Pagos
{
    public partial class PagosPanel : System.Web.UI.Page
    {

        private GestorPagos gestorPagos;
        private GestorAlumnos gestorAlumnos;
        private GestorProfesores gestorProfesores;
        private GestorAdministradores gestorAdmin;
        private GestorInscripciones gestorInscripciones;
        private GestorInscripcionesEvento gestorInscripcionesEvento;
        private GestorFormaPago gestorFPago;
        private GestorUsuarios gestorUsuarios;



        private static int tipoDoc;
        private static string dni;
        private static string nombre;
        private static List<ObjetoPagable> objetosGrilla;
        private static List<tipo_documento> tiposdoc;
        private static Sesion sesionActiva;


        protected void Page_Load(object sender, EventArgs e)
        {
            gestorPagos = new GestorPagos();
            gestorAdmin = new GestorAdministradores();
            gestorAlumnos = new GestorAlumnos();
            gestorProfesores = new GestorProfesores();
            gestorFPago = new GestorFormaPago();
            gestorInscripciones = new GestorInscripciones();
            gestorUsuarios = new GestorUsuarios();

            if (!IsPostBack)
            {
                try
                {
                    CargarComboFormaPago();
                    CargarComboTipoDocumentos();

                    if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
                    {
                        divDNI.Visible = true;



                    }
                    else
                    {


                        sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];

                        if (sesionActiva.estado == "INGRESO ACEPTADO")
                        {
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



                                    var profesor = gestorProfesores.ObtenerProfesorPorIdUsuario(usuario.id_usuario);
                                    if (profesor != null)
                                    {
                                        if (profesor.id_tipo_documento != null)
                                            tipoDoc = (int) profesor.id_tipo_documento;
                                        dni = profesor.dni;

                                        nombre = profesor.nombre + " " + profesor.apellido;
                                        CargarGrilla();
                                    }
                                    else
                                    {
                                        var admin = gestorAdmin.ObtenerAdminPorIdUsuario(usuario.id_usuario);
                                        if (admin != null)
                                        {
                                            if (admin.id_tipo_documento != null)
                                                tipoDoc = (int) admin.id_tipo_documento;
                                            dni = admin.dni;

                                            nombre = admin.nombre + " " + admin.apellido;
                                            CargarGrilla();
                                        }
                                    }

                                    divDNI.Visible = true;
                                    txtDni.Text = dni;
                                    ddl_tipo.SelectedValue = tipoDoc.ToString();

                                }
                            }
                            else
                            {
                                divDNI.Visible = true;
                              
                            


                           
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");
                }

                if (Request.UrlReferrer == null) ViewState["RefUrl"] = "/Presentacion/MenuInicial.aspx";
                else ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }

        }

        protected void btn_buscar_alumno_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }


        protected void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            var listaPagables = new List<ObjetoPagable>();
            foreach (GridViewRow row in gvPagos.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow != null && chkRow.Checked)
                    {
                        var objetoPagable = objetosGrilla[row.RowIndex];
                        listaPagables.Add(objetoPagable);
                    }
                }
            }

            if (listaPagables.Count == 0)
            {
                mensaje("No seleccionó ningún ítem para pagar", false);
                return;
            }

            int idUsuario;

            if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
            {


                idUsuario = gestorUsuarios.ObtenerIdUsuarioInvitado();
                if (idUsuario == -1)
                {
                    mensaje("Error, usuario invitado no se puede registrar pago", false);
                    return;
                }
            }
            else
            {
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                idUsuario = sesionActiva.usuario.id_usuario;
            }
            int idFPago;
            int.TryParse(ddl_forma_pago.SelectedValue, out idFPago);

            var pagoMultiple = new PagoMultiple(listaPagables, idFPago, idUsuario, tipoDoc, dni, nombre);
            HttpContext.Current.Session["PagoMultiple"] = JsonConvert.SerializeObject(pagoMultiple);

            Response.Redirect("PagosMultiple.aspx");

        }


        protected void gvPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPagos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void gvPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("pago") == 0)
            {
                int index = Convert.ToInt32(e.CommandArgument);


                

            }
        }

        private void mensaje(string pMensaje, bool pEstado)
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



        protected void CargarGrilla()
        {
            //Para validar y permitir que si es invitado no muestre datos del alumno
            bool invitado = HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO";

            objetosGrilla = gestorPagos.ObtenerObjetosPagablesPendientes(tipoDoc, dni, invitado);
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

        protected void CargarComboFormaPago()
        {
            List<forma_pago> formasPago = gestorFPago.ObtenerFormasPago();

            if (HttpContext.Current.Session == null)
            {

            }
            else if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
            {
                formasPago.RemoveAll(x => x.nombre == "Efectivo");
            }
            else
            {
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                int permiso = 0;
                System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'PAGO_EFECTIVO'");
                if (drsAux.Length > 0)
                {
                    int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                }
                if (permiso != 1)
                {
                    formasPago.RemoveAll(x => x.nombre == "Efectivo");
                }
            }



            ddl_forma_pago.DataSource = formasPago;
            ddl_forma_pago.DataTextField = "nombre";
            ddl_forma_pago.DataValueField = "id_forma_pago";
            ddl_forma_pago.DataBind();
        }
        protected void CargarComboTipoDocumentos()
        {

            tiposdoc = gestorInscripciones.ObtenerTiposDocumentos();
            ddl_tipo.DataSource = tiposdoc;
            ddl_tipo.DataTextField = "codigo";
            ddl_tipo.DataValueField = "id_tipo_documento";
            ddl_tipo.DataBind();

        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            Response.Redirect((string)refUrl);
        }

        protected void gvPagos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // To check condition on integer value
            //Anular pagos

            //int permiso = 0;
            //if (sesionActiva == null) return;
          
            //System.Data.DataRow[] drsAux =
            //    sesionActiva.permisos.Select("perm_clave = 'PAGO_ANULAR'");
            //if (drsAux.Length > 0)
            //{
            //    int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
            //}

            //if (permiso == 1)
            //{
            //    var btn = e.Row.FindControl("btn_anular");
            //    if (btn !=null)
            //    {
            //        if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TipoPago.Tipo")) == "Clase")
            //        {
            //            btn.Visible = true;
            //        }
                    
            //    }
                    
                    
                   
            //}
        }
    }
}
