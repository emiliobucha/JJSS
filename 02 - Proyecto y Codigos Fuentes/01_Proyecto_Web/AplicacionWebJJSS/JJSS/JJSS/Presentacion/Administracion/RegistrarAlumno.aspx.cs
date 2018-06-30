
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
using System.Data;
using JJSS_Negocio.Resultados;
using JJSS_Negocio.Administracion;
using JJSS_Negocio.Constantes;

namespace JJSS.Presentacion
{
    public partial class RegistrarAlumno : System.Web.UI.Page
    {
        private GestorInscripciones gestorInscripciones;
        private GestorAlumnos gestorAlumnos;
        private GestorCiudades gestorCiudades;
        private GestorProvincias gestorProvincias;
        private GestorEstados gestorEstados;
        private static string dniAlumno;

        protected void Page_Load(object sender, EventArgs e)
        {
            

            //try
            //{
            //    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
            //    if (sesionActiva.estado != "INGRESO ACEPTADO")
            //    {
            //        Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

            //}


            gestorAlumnos = new GestorAlumnos();
            gestorInscripciones = new GestorInscripciones();
            gestorCiudades = new GestorCiudades();
            gestorProvincias = new GestorProvincias();
            gestorEstados = new GestorEstados();
            dniAlumno = null;

            if (!IsPostBack)
            {
                if (Request.UrlReferrer == null) ViewState["RefUrl"] = "Menu_Administracion.aspx";
                else ViewState["RefUrl"] = Request.UrlReferrer.ToString();

                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'ALUMNO_CREACION'");
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
                
                CargarComboProvincias();
                CargarComboTipoDocumentos();
                CargarComboNacionalidades();
                if (Session["alumnoEditar"] != null)
                {
                    dniAlumno = Session["alumnoEditar"].ToString();
                    Session["alumnoEditar"] = null;
                    CargarDatosAlumno();
                }
            }
        }

        private void CargarDatosAlumno()
        {
            alumno alu = gestorAlumnos.ObtenerAlumnoPorDNI(dniAlumno);
            txtDni.Text = alu.dni.ToString();
            txt_apellido.Text = alu.apellido;
            txt_email.Text = alu.mail;
            txt_nombres.Text = alu.nombre;
            txt_telefono.Text = alu.telefono.ToString();
            txt_telefono_urgencia.Text = alu.telefono_emergencia.ToString();

            DateTime fecha = (DateTime)alu.fecha_nacimiento;
            dp_fecha.Text = fecha.ToString("dd/MM/yyyy");

            if (alu.sexo == 0) rbSexo.SelectedIndex = 0;
            if (alu.sexo == 1) rbSexo.SelectedIndex = 1;

            DireccionAlumno direccionEncontrada = gestorAlumnos.ObtenerDireccionAlumno(alu.id_alumno);
            if (direccionEncontrada != null)
            {
                txt_calle.Text = direccionEncontrada.calle;
                txt_nro_dpto.Text = direccionEncontrada.depto;
                txt_numero.Text = direccionEncontrada.numero.ToString();
                txt_piso.Text = direccionEncontrada.piso.ToString();
                ddl_provincia.SelectedValue = direccionEncontrada.idProvincia.ToString();
                CargarComboCiudades(int.Parse(ddl_provincia.SelectedValue));
                ddl_localidad.SelectedValue = direccionEncontrada.idCiudad.ToString();
                txt_torre.Text = direccionEncontrada.torre;
            }

            txtDni.Enabled = false;
            
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
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

            
            string nombre = txt_nombres.Text;
            string apellido = txt_apellido.Text;
            
            DateTime fechaNac = DateTime.Parse(dp_fecha.Text);

            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = ContantesSexo.FEMENINO; 
            if (rbSexo.SelectedIndex == 1) sexo = ContantesSexo.MASCULINO;

            long tel = 0;
            if (txt_telefono.Text != "")
            {
                tel = long.Parse(txt_telefono.Text);
            }

            string mail = txt_email.Text;
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

            long telEmergencia = long.Parse(txt_telefono_urgencia.Text);
            //Image imagenPerfil = Avatar;
            int? ciudad = null;
            if (ddl_localidad.SelectedValue != "")
            {
                ciudad = int.Parse(ddl_localidad.SelectedValue);
            }


            var dni = txtDni.Text;

            int idTipo;
            int.TryParse(ddl_tipo.SelectedValue, out idTipo);

            if (!modValidaciones.validarFormatoDocumento(dni, idTipo))
            {
                mensaje("El documento debe tener sólo números", false);
                return;
            }

            int idPais;
            int.TryParse(ddl_nacionalidad.SelectedValue, out idPais);


            if (txtDni.Enabled == false)
            {
                //actualiza datos
                try
                {
                    gestorAlumnos.ModificarAlumno(idTipo, dni, nombre, apellido, fechaNac, sexo, null, idPais);
                    gestorAlumnos.ModificarAlumnoContacto(calle, departamento, numero, piso, tel, telEmergencia, mail, idTipo,dni, ciudad, torre);
                    mensaje("Se modificaron los datos correctamente", true);
                    limpiar();
                }
                catch (Exception ex)
                {
                    if (ex.Message.CompareTo("El usuario no existe") == 0) mensaje("El alumno no existe", false);
                    else mensaje(ex.Message, false);
                }

            }
            else
            {
                System.IO.Stream imagen = avatarUpload.PostedFile.InputStream;
                byte[] imagenByte;
                using (MemoryStream ms = new MemoryStream())
                {
                    imagen.CopyTo(ms);
                    imagenByte = ms.ToArray();
                }
                //registra un nuevo alumno
                try
                {
                    gestorAlumnos.RegistrarAlumno(nombre, apellido, fechaNac, sexo, idTipo,dni, tel, mail, telEmergencia, imagenByte, calle, numero, departamento, piso, ciudad, torre, idPais);
                    mensaje("Se ha creado el alumno exitosamente", true);
                    limpiar();
                }
                catch (Exception ex)
                {
                    mensaje(ex.Message, false);
                }
            }
        }

        private void mensaje(string pMensaje, Boolean pEstado)
        {
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

        private void limpiar()
        {
            txtDni.Text = "";
            txt_apellido.Text = "";
            txt_calle.Text = "";
            txt_email.Text = "";
            txt_nombres.Text = "";
            txt_nro_dpto.Text = "";
            txt_numero.Text = "";
            txt_piso.Text = "";
            txt_telefono.Text = "";
            txt_telefono_urgencia.Text = "";

            if (ddl_localidad.Items.Count!=0) ddl_localidad.SelectedIndex = 0;

            ddl_provincia.SelectedIndex = 0;

            ddl_tipo.SelectedIndex = 0;
            ddl_nacionalidad.SelectedIndex = 0;

            txtDni.Enabled = true;

        }

        protected void btn_Cancelar_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }


        protected void CargarComboTipoDocumentos()
        {

            List<tipo_documento> tiposdoc = gestorInscripciones.ObtenerTiposDocumentos();
            ddl_tipo.DataSource = tiposdoc;
            ddl_tipo.DataTextField = "codigo";
            ddl_tipo.DataValueField = "id_tipo_documento";
            ddl_tipo.DataBind();

        }

        protected void CargarComboNacionalidades()
        {

            List<pais> paises = gestorInscripciones.ObtenerNacionalidades();
            ddl_nacionalidad.DataSource = paises;
            ddl_nacionalidad.DataTextField = "nombre";
            ddl_nacionalidad.DataValueField = "id_pais";
            ddl_nacionalidad.DataBind();

        }



    }
}