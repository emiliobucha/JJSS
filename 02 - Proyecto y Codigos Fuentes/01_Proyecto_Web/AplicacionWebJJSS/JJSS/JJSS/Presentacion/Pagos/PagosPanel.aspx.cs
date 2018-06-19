using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;


namespace JJSS.Presentacion.Pagos
{
    public partial class PagosPanel : System.Web.UI.Page
    {

        private GestorPagos gestorPagos;
        private GestorAlumnos gestorAlumnos;
        private GestorProfesores gestorProfesores;
        private GestorAdministradores gestorAdmin;



        private static int tipoDoc;
        private static string dni;
        private static List<ObjetoPagable> objetosGrilla;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorPagos = new GestorPagos();
            gestorAdmin = new GestorAdministradores();
            gestorAlumnos = new GestorAlumnos();
            gestorProfesores = new GestorProfesores();

            if (!IsPostBack)
            {
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                if (sesionActiva.estado == "INGRESO ACEPTADO")
                {
                    var usuario = sesionActiva.usuario;
                    if (usuario != null)
                    {
                        var alumno = gestorAlumnos.ObtenerAlumnoPorIdUsuario(usuario.id_usuario);


                        if (alumno != null)
                        {
                            if (alumno.id_tipo_documento != null) tipoDoc = (int)alumno.id_tipo_documento;
                            dni = alumno.dni;
                        }
                        else
                        {
                            var profesor = gestorProfesores.ObtenerProfesorPorIdUsuario(usuario.id_usuario);
                            if (profesor != null)
                            {
                                if (profesor.id_tipo_documento != null) tipoDoc = (int)profesor.id_tipo_documento;
                                dni = profesor.dni;
                            }
                            else
                            {
                                var admin = gestorAdmin.ObtenerAdminPorIdUsuario(usuario.id_usuario);
                                if (admin != null)
                                {
                                    if (admin.id_tipo_documento != null) tipoDoc = (int)admin.id_tipo_documento;
                                    dni = admin.dni;
                                }
                            }
                        }
                    }
                }


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

        protected void gvPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);


            if (e.CommandName.CompareTo("pago") == 0)
            {

                var objetoPagable = objetosGrilla[index];


                Session["ObjetoPagable"] = objetoPagable;
                if (objetoPagable.TipoPago.Id == 1)
                {
                    Session["Torneo"] = objetoPagable.IdObjeto;
                    Session["ParticipanteDNI"] = dni;
                    Session["ParticipanteTipoDni"] = tipoDoc;
                    Response.Redirect("../Torneos/TorneoPago");
                }
                else if (objetoPagable.TipoPago.Id == 2)
                {
                    Session["Evento"] = objetoPagable.IdObjeto;
                    Session["ParticipanteDNI"] = dni;
                    Session["ParticipanteTipoDni"] = tipoDoc;
                    Response.Redirect("../Eventos/EventoPago");
                }
                else if (objetoPagable.TipoPago.Id == 3)
                {
                    Session["Clase"] = objetoPagable.IdObjeto;
                    Session["ParticipanteDNI"] = dni;
                    Session["ParticipanteTipoDni"] = tipoDoc;
                    Response.Redirect("../Presentacion/PagoClase");
                }
                
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
            objetosGrilla = gestorPagos.ObtenerObjetosPagablesPendientes(tipoDoc, dni);
            gvPagos.DataSource = objetosGrilla;
            gvPagos.DataBind();
        }
    }
}
