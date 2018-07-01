using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio.Administracion;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion.Administracion
{
    public partial class CrearSede : System.Web.UI.Page
    {
        private static GestorCiudades gestorCiudades;
        private static GestorProvincias gestorProvincias;
        private static GestorSedes gestorSedes;
        private static GestorAcademias gestorAcademias;
        private static int idSede = 0;
        private static int idAcademia = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.UrlReferrer == null) ViewState["RefUrl"] = "/Presentacion/Menu_Administracion.aspx";
                else ViewState["RefUrl"] = Request.UrlReferrer.ToString();

                gestorCiudades = new GestorCiudades();
                gestorProvincias = new GestorProvincias();
                gestorSedes = new GestorSedes();
                gestorAcademias = new GestorAcademias();

                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'SEDES_CREACION'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
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





                CargarComboProvincias();

                if (Session["sede"] != null)
                {
                    object a = Session["sede"];
                    idSede = Convert.ToInt32(Session["sede"]);
                    CargarDatosSedeSeleccionada();
                    Session["sede"] = null;
                    rbCrear.Enabled = false;
                }
                else if (Session["academia"] != null)
                {
                    idAcademia = Convert.ToInt32(Session["academia"]);
                    CargarDatosAcademiaSeleccionada();
                    Session["academia"] = null;
                    rbCrear.Enabled = false;
                }
                else limpiar();
            }
        }

        private void CargarDatosSedeSeleccionada()
        {
            sede sedeSeleccionada = gestorSedes.BuscarSedePorID(idSede);
            DireccionAlumno direccionSede = gestorSedes.ObtenerDireccionSedeCompleta(idSede);
            limpiar();
            txt_calle.Text = direccionSede.calle;
            txt_nombre.Text = sedeSeleccionada.nombre;
            txt_numero.Text = Convert.ToString( direccionSede.numero);
            txt_piso.Text = direccionSede.piso.ToString();
            txt_nro_dpto.Text = direccionSede.depto;
            txt_telefono.Text = Convert.ToString( sedeSeleccionada.telefono);
            txt_torre.Text = direccionSede.torre;
            rbCrear.SelectedValue = "sede";
            ddl_provincia.SelectedValue = Convert.ToString( direccionSede.idProvincia);
            CargarComboCiudades(direccionSede.idProvincia.Value);
            ddl_localidad.SelectedValue = Convert.ToString(direccionSede.idCiudad);
        }

        private void CargarDatosAcademiaSeleccionada()
        {
            academia academiaSeleccionada = gestorAcademias.ObtenerAcademiasPorID(idAcademia);
            DireccionAlumno direccionAcademia = gestorAcademias.ObtenerDireccionAcademiaCompleta(idAcademia);
            limpiar();
            txt_calle.Text = direccionAcademia.calle;
            txt_nombre.Text = academiaSeleccionada.nombre;
            txt_numero.Text = Convert.ToString( direccionAcademia.numero);
            txt_piso.Text = Convert.ToString( direccionAcademia.piso);
            txt_nro_dpto.Text =  direccionAcademia.depto;
            txt_telefono.Text = Convert.ToString( academiaSeleccionada.telefono);
            txt_torre.Text = direccionAcademia.torre;
            rbCrear.SelectedValue = "academia";
            ddl_provincia.SelectedValue = Convert.ToString(direccionAcademia.idProvincia);
            CargarComboCiudades(direccionAcademia.idProvincia.Value);
            ddl_localidad.SelectedValue = Convert.ToString(direccionAcademia.idCiudad);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void CargarComboProvincias()
        {
            List<provincia> provincias = gestorProvincias.ObtenerProvincias();
            ddl_provincia.DataSource = provincias;
            ddl_provincia.DataTextField = "nombre";
            ddl_provincia.DataValueField = "id_provincia";
            ddl_provincia.DataBind();
        }

        protected void CargarComboCiudades(int pProvincia)
        {
            List<ciudad> ciudades = gestorCiudades.ObtenerCiudadesPorProvincia(pProvincia);
            ddl_localidad.DataSource = ciudades;
            ddl_localidad.DataTextField = "nombre";
            ddl_localidad.DataValueField = "id_ciudad";
            ddl_localidad.DataBind();
        }

        private void mensaje(string pMensaje, Boolean pEstado)
        {
            // Response.Write("<script>window.alert('" + pMensaje.Trim() + "');</script>");
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

        protected void ddl_provincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_provincia = int.Parse(ddl_provincia.SelectedValue);
            CargarComboCiudades(id_provincia);
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("../Presentacion/Inicio.aspx");
        }

        private void limpiar()
        {
            rbCrear.Enabled = true;

            txt_calle.Text = "";
            txt_nombre.Text = "";
            txt_nro_dpto.Text = "";
            txt_numero.Text = "";
            txt_piso.Text = "";

            if (ddl_localidad.Items.Count != 0) ddl_localidad.SelectedIndex = 0;

            ddl_provincia.SelectedIndex = 0;
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;

            idSede = 0;
            idAcademia = 0;

        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            
            string calle = txt_calle.Text;
            string departamento = txt_nro_dpto.Text;
            string torre = txt_torre.Text;

            int? piso = null;
            if (txt_piso.Text != "")
            {
                piso = int.Parse(txt_piso.Text);
            }
            int? numero = null;
            if (txt_numero.Text != "")
            {
                numero = int.Parse(txt_numero.Text);
            }
            int ciudad = int.Parse(ddl_localidad.SelectedValue);
            long? telefono = null;
            if (txt_telefono.Text != "")
            {
                telefono = long.Parse(txt_telefono.Text);
            }

            direccion nuevaDireccion = new direccion();
            nuevaDireccion.calle = calle;
            nuevaDireccion.id_ciudad = ciudad;
            nuevaDireccion.numero = numero;
            nuevaDireccion.piso = piso;
            nuevaDireccion.torre = torre;
            nuevaDireccion.departamento = departamento;

            string nombre = txt_nombre.Text;
            string res = "";
            if (idSede == 0 && idAcademia == 0)
            {//creacion
                if (rbCrear.SelectedValue.CompareTo("sede") == 0)
                {
                    crearSede(nombre, nuevaDireccion, telefono);
                }
                else if (rbCrear.SelectedValue.CompareTo("academia") == 0)
                {
                    crearAcademia(nombre, nuevaDireccion, telefono);
                }
            }else if (idSede != 0)
            {//modificar sede
                sede sSeleccionada = new sede()
                {
                    id_sede = idSede,
                    direccion = nuevaDireccion,
                    nombre = nombre,
                    telefono = telefono,
                };
                res = gestorSedes.ModificarSede(sSeleccionada);
                if (res.CompareTo("") == 0)
                {
                    limpiar();
                    Session["mensaje"] = "Se modificó la sede correctamente";
                    Session["exito"] = true;
                    Response.Redirect("AdministrarSedes.aspx");
                }
                else mensaje(res, false);
            }
            else if (idAcademia != 0)
            {//modificar academia
                academia aSeleccionada = new academia()
                {
                    id_academia = idAcademia,
                    direccion = nuevaDireccion,
                    nombre = nombre,
                    telefono = telefono,
                };
                res = gestorAcademias.ModificarAcademia(aSeleccionada);
                if (res.CompareTo("") == 0)
                {
                    limpiar();
                    Session["mensaje"] = "Se modificó la academia correctamente";
                    Session["exito"] = true;
                    Response.Redirect("AdministrarSedes.aspx");
                }
                else mensaje(res, false);
            }
        }

        private void crearSede(String nombre, direccion nuevaDireccion,long? telefono)
        {
            String res = gestorSedes.GenerarNuevaSede(nombre, nuevaDireccion,telefono);
            if (res.CompareTo("") == 0)
            {
                limpiar();
                Session["mensaje"] = "Se creó la sede correctamente";
                Session["exito"] = true;
                Response.Redirect("AdministrarSedes.aspx");
            }
            else mensaje(res, false);
        }

        private void crearAcademia(String nombre, direccion nuevaDireccion, long? telefono)
        {
            String res = gestorAcademias.GenerarAcademia(nombre, nuevaDireccion, telefono);
            if (res.CompareTo("") == 0)
            {
                limpiar();
                Session["mensaje"] = "Se creó la academia correctamente";
                Session["exito"] = true;
                Response.Redirect("AdministrarSedes.aspx");

            }
            else mensaje(res, false);
        }

        protected void btn_cancelar_Click1(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["RefUrl"].ToString());
        }
    }
}