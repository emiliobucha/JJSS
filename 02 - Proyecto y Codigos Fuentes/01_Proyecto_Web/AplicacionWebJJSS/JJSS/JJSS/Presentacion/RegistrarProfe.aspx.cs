
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using System.IO;
using System.Globalization;

namespace JJSS.Presentacion
{
    public partial class RegistrarProfe : System.Web.UI.Page
    {

        private GestorInscripciones gestorInscripciones;
        private GestorProfesores gestorProfes;
        private GestorCiudades gestorCiudades;
        private GestorProvincias gestorProvincias;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorProfes = new GestorProfesores();
            gestorInscripciones = new GestorInscripciones();
            gestorCiudades = new GestorCiudades();
            gestorProvincias = new GestorProvincias();
            


            if (!IsPostBack)
            {

                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'PROFESOR_CREACION'");
                        if (drsAux.Length > 0)
                        {
                            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                        }
                        if (permiso != 1)
                        {
                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                }

                //CargarComboCiudades(1);
                CargarComboProvincias();
                ViewState["gvAlumnosOrden"] = "dni";
                gvprofes.AllowPaging = true;
                gvprofes.AutoGenerateColumns = false;
                gvprofes.PageSize = 20;
                CargarGrilla();
                mostrarPaneles();
            }


        }

        protected void mostrarPaneles()
        {
            if (Session["profesor"] == null || Session["profesor"].ToString().CompareTo("Registrar") == 0)
            {
                pnl_mostrar_profes.Visible = false;
                pnlFormulario.Visible = true;
                Session["profesor"] = "profe";
            }
            else if (Session["profesor"].ToString().CompareTo("Administrar") == 0)
            {
                pnl_mostrar_profes.Visible = true;
                pnlFormulario.Visible = false;
                Session["profesor"] = "profe";
            }
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

        protected void btn_guardar_click(object sender, EventArgs e)
        {
            //+ver categorias, direccion y foto perfil
            int dni = int.Parse(txtDni.Text);
            string nombre = txt_nombres.Text;
            string apellido = txt_apellido.Text;

            string[] formats = { "MM/dd/yyyy" };
            DateTime fechaNac = DateTime.ParseExact(dp_fecha.Text, formats, new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
            
            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = 0; //Femenino
            if (rbSexo.SelectedIndex == 1) sexo = 1; //Masculino

            int tel = 0;
            if (txt_telefono.Text != "")
            {
                tel = int.Parse(txt_telefono.Text);
            }

            string mail = txt_email.Text;
            string calle = txt_calle.Text;
            string departamento = txt_nro_dpto.Text;

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
            System.IO.Stream imagen = avatarUpload.PostedFile.InputStream;
            byte[] imagenByte;
            using (MemoryStream ms = new MemoryStream())
            {
                imagen.CopyTo(ms);
                imagenByte = ms.ToArray();
            }
            int telEmergencia = int.Parse(txt_telefono_urgencia.Text);
            //Image imagenPerfil = Avatar;

            int ciudad = int.Parse(ddl_localidad.SelectedValue);

            string sReturn = gestorProfes.RegistrarProfesor(nombre, apellido, fechaNac, sexo, dni, tel, mail, telEmergencia, imagenByte, calle, numero, departamento, piso, ciudad);
            Boolean estado;
            if (sReturn.CompareTo("") == 0)
            {
                sReturn = "Se ha creado el profesor exitosamente";
                estado = true;
                //Session["alumnos"] = "Administrar";
                pnl_mostrar_profes.Visible = true;
                pnlFormulario.Visible = false;
                limpiar();
            }
            else
            {
                estado = false;
                //Session["alumnos"] = "Registrar";
                pnl_mostrar_profes.Visible = false;
                pnlFormulario.Visible = true;
            }
            mensaje(sReturn, estado);
            CargarGrilla();
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
            CargarComboCiudades(int.Parse(ddl_provincia.SelectedValue));
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("../Presentacion/Inicio.aspx");
        }

        private void limpiar()
        {
            txtDni.Text = "";
            txt_apellido.Text = "";
            txt_calle.Text = "";
            txt_email.Text = "";
            txt_filtro_dni.Text = "";
            txt_nombres.Text = "";
            txt_nro_dpto.Text = "";
            txt_numero.Text = "";
            txt_piso.Text = "";
            txt_telefono.Text = "";
            txt_telefono_urgencia.Text = "";
            ddl_localidad.SelectedIndex = 0;
            ddl_provincia.SelectedIndex = 0;
        }

        protected void CargarGrilla()
        {
            int dni = 0;
            if (txt_filtro_dni.Text.CompareTo("") != 0) dni = int.Parse(txt_filtro_dni.Text);

            gvprofes.DataSource = gestorProfes.BuscarProfePorDni(dni);
            gvprofes.DataBind();
        }

        protected void btn_ver_profes_Click(object sender, EventArgs e)
        {
            //Session["alumnos"] = "Administrar";
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            pnl_mostrar_profes.Visible = true;
            pnlFormulario.Visible = false;
        }

        protected void btn_buscar_profe_Click(object sender, EventArgs e)
        {
            CargarGrilla();
            //Session["alumnos"] = "Administrar";
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            pnl_mostrar_profes.Visible = true;
            pnlFormulario.Visible = false;
        }

        protected void btn_registro_Click(object sender, EventArgs e)
        {
            //Session["alumnos"] = "Registrar";
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            pnl_mostrar_profes.Visible = false;
            pnlFormulario.Visible = true;
        }

        protected void gvprofes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvprofes.PageIndex = e.NewPageIndex;
            CargarGrilla();
            pnlFormulario.Visible = false;
            pnl_mostrar_profes.Visible = true;
        }

        protected void gvprofes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                //if (confirmar("¿Está seguro de eliminar este alumno?") == true) { }
                int index = Convert.ToInt32(e.CommandArgument);
                int dni = Convert.ToInt32(gvprofes.DataKeys[index].Value);
                string sReturn = gestorProfes.EliminarProfesor(dni);
                Boolean estado = true;
                if (sReturn.CompareTo("") == 0) sReturn = "Se ha eliminado el profesor correctamente";
                else estado = false;
                //Session["alumnos"] = "Administrar";
                pnl_mostrar_profes.Visible = true;
                pnlFormulario.Visible = false;
                mensaje(sReturn, estado);
                CargarGrilla();
            }
            else if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                mensaje("Proximamente", false);
            }
        }

    }
}