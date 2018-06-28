﻿using System;
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



        private static int tipoDoc;
        private static string dni;
        private static string nombre;
        private static List<ObjetoPagable> objetosGrilla;

        


        protected void Page_Load(object sender, EventArgs e)
        {
            gestorPagos = new GestorPagos();
            gestorAdmin = new GestorAdministradores();
            gestorAlumnos = new GestorAlumnos();
            gestorProfesores = new GestorProfesores();
            gestorFPago = new GestorFormaPago();
            gestorInscripciones = new GestorInscripciones();

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
                            if (alumno.id_tipo_documento != null) tipoDoc = (int) alumno.id_tipo_documento;
                            dni = alumno.dni;
                            nombre = alumno.nombre + " " + alumno.apellido;
                            CargarGrilla();
                        }
                        else
                        {



                            var profesor = gestorProfesores.ObtenerProfesorPorIdUsuario(usuario.id_usuario);
                            if (profesor != null)
                            {
                                if (profesor.id_tipo_documento != null) tipoDoc = (int) profesor.id_tipo_documento;
                                dni = profesor.dni;

                                nombre = profesor.nombre + " " + profesor.apellido;
                                CargarGrilla();
                            }
                            else
                            {
                                var admin = gestorAdmin.ObtenerAdminPorIdUsuario(usuario.id_usuario);
                                if (admin != null)
                                {
                                    if (admin.id_tipo_documento != null) tipoDoc = (int) admin.id_tipo_documento;
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
               


                CargarComboFormaPago();
                CargarComboTipoDocumentos();

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


            Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];

            int idFPago;
            int.TryParse(ddl_forma_pago.SelectedValue, out idFPago);

            var pagoMultiple = new PagoMultiple(listaPagables, idFPago, sesionActiva.usuario.id_usuario,tipoDoc,dni,nombre);
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
            ddl_forma_pago.DataSource = formasPago;
            ddl_forma_pago.DataTextField = "nombre";
            ddl_forma_pago.DataValueField = "id_forma_pago";
            ddl_forma_pago.DataBind();
        }
        protected void CargarComboTipoDocumentos()
        {

            List<tipo_documento> tiposdoc = gestorInscripciones.ObtenerTiposDocumentos();
            ddl_tipo.DataSource = tiposdoc;
            ddl_tipo.DataTextField = "codigo";
            ddl_tipo.DataValueField = "id_tipo_documento";
            ddl_tipo.DataBind();

        }
    }
}
