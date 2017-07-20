using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;
using JJSS_Entidad;
using System.Data;

namespace JJSS.Presentacion
{
    public partial class Perfil : System.Web.UI.Page
    {
        private GestorUsuarios gestorUsuario;
        private GestorAlumnos gestorAlumnos;
        private GestorProfesores gestorProfe;
        private GestorProvincias gestorProvincias;
        private GestorCiudades gestorCiudades;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorUsuario = new GestorUsuarios();
            gestorAlumnos = new GestorAlumnos();
            gestorProfe = new GestorProfesores();
            gestorProvincias = new GestorProvincias();
            gestorCiudades = new GestorCiudades();

            if (!IsPostBack)
            {
                MultiView1.SetActiveView(view_datos_personales);
                CargarDatos();
                CargarComboCiudades(1);
                CargarComboProvincias();
            }
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

        protected void CargarDatos()
        {
            GestorSesiones gestorSesion = new GestorSesiones();
            seguridad_usuario alumnoActual = gestorSesion.getActual().usuario;
            int id_usuario = alumnoActual.id_usuario;


            alumno alumnoEncontrado = gestorAlumnos.ObtenerAlumnoPorIdUsuario(id_usuario);
            if (alumnoEncontrado == null) //es un profe
            {
                profesor profeEncontrado = gestorProfe.ObtenerProfesorPorIdUsuario(id_usuario);
                if (profeEncontrado == null) mensaje("No se encontró el usuario o es admin", false); //no existe o es admin
                else
                {
                    DataTable direccionEncontrada = gestorProfe.ObtenerDireccionProfesor(profeEncontrado.id_profesor);

                    txt_dni.Text = profeEncontrado.dni.ToString();
                    txt_nombre.Text = profeEncontrado.nombre;
                    txt_apellido.Text = profeEncontrado.apellido;
                    txt_email.Text = profeEncontrado.mail;
                    txt_telefono.Text = profeEncontrado.telefono.ToString();
                    txt_telefono_urgencia.Text = profeEncontrado.telefono_emergencia.ToString();
                    if (direccionEncontrada.Rows.Count > 0)
                    {
                        DataRow row = direccionEncontrada.Rows[0];
                        txt_calle.Text = row["calle"].ToString();
                        txt_nro_dpto.Text = row["depto"].ToString();
                        txt_numero.Text = row["numero"].ToString();
                        txt_piso.Text = row["piso"].ToString();
                        ddl_localidad.SelectedValue = row["idCiudad"].ToString();
                        ddl_provincia.SelectedValue = row["idProvincia"].ToString();
                    }
                }
                


            }
            else
            {
                // busco la direccion
                DataTable direccionEncontrada= gestorAlumnos.ObtenerDireccionAlumno(alumnoEncontrado.id_alumno);
                
                txt_dni.Text = alumnoEncontrado.dni.ToString();
                txt_nombre.Text = alumnoEncontrado.nombre;
                txt_apellido.Text = alumnoEncontrado.apellido;
                txt_email.Text = alumnoEncontrado.mail;
                txt_telefono.Text = alumnoEncontrado.telefono.ToString();
                txt_telefono_urgencia.Text = alumnoEncontrado.telefono_emergencia.ToString();
                if (direccionEncontrada.Rows.Count > 0)
                {
                    DataRow row = direccionEncontrada.Rows[0];
                    txt_calle.Text = row["calle"].ToString();
                    txt_nro_dpto.Text = row["depto"].ToString();
                    txt_numero.Text = row["numero"].ToString();
                    txt_piso.Text = row["piso"].ToString();
                    ddl_localidad.SelectedValue = row["idCiudad"].ToString();
                    ddl_provincia.SelectedValue = row["idProvincia"].ToString();
                }
                


            }

            txt_usuario.Text = alumnoActual.login;
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
            Response.Redirect("~/Presentacion/Inicio.aspx");
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

        protected void limpiarDatos()
        {
            txt_apellido.Text = "";
            txt_nombre.Text = "";
            txt_calle.Text = "";
            txt_dni.Text = "";
            txt_email.Text = "";
            txt_nro_dpto.Text = "";
            txt_numero.Text = "";
            txt_piso.Text = "";
            txt_telefono.Text = "";
            txt_telefono_urgencia.Text = "";
            txt_usuario.Text = "";

        }

        protected void limpiarCambio()
        {
            txt_pass_anterior.Text = "";
            txt_pass_nueva.Text = "";
            txt_pass_repetida.Text = "";
        }
        

        protected void btn_guardar_contaco_Click(object sender, EventArgs e)
        {
            int tel = 0;
            if (txt_telefono.Text != "")
            {
                tel = int.Parse(txt_telefono.Text);
            }
            int telUrg = 0;
            if (txt_telefono_urgencia.Text != "")
            {
                telUrg = int.Parse(txt_telefono_urgencia.Text);
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

            int idCiudad = int.Parse(ddl_localidad.SelectedValue);

            string sreturn =gestorAlumnos.ModificarAlumno(calle, departamento, numero, piso, tel,telUrg, mail,int.Parse(txt_dni.Text),idCiudad);
            if (sreturn.CompareTo("NO") == 0)//es un profe
            {
                sreturn = gestorProfe.ModificarProfesor(calle,departamento,numero,piso,tel,telUrg,mail, int.Parse(txt_dni.Text), idCiudad);
                if (sreturn.CompareTo("NO") == 0) mensaje("No se encontró el usuario o es admin", false);//no existe o es admin
            }
            if (sreturn.CompareTo("") == 0)
            {
                mensaje("Se modificaron los datos correctamente", true);

            }
            else mensaje(sreturn, false);
        }

        protected void btn_cambiar_pass_Click(object sender, EventArgs e)
        {
            string claveAnterior = txt_pass_anterior.Text;
            string claveNueva = txt_pass_nueva.Text;
            string login = txt_usuario.Text;
            string sReturn = gestorUsuario.CambiarClave(claveAnterior, claveNueva, login);
            if (sReturn.CompareTo("") == 0)
            {
                mensaje("Se ha realizado el cambio correctamente", true);
                limpiarCambio();
                MultiView1.SetActiveView(view_datos_personales);
            }
            else mensaje(sReturn, false);
        }

        protected void btn_cambiar_foto_Click(object sender, EventArgs e)
        {
            mensaje("Todavia no :)", false);
        }

        protected void btn_guardar_personal_Click(object sender, EventArgs e)
        {
            string apellido = txt_apellido.Text;
            string nombre = txt_nombre.Text;
            int dni = int.Parse(txt_dni.Text);

            //+ NO ESTA CONTEMPLADO SI ES ADMIN...
            string sreturn = gestorAlumnos.ModificarAlumno(dni, nombre, apellido);
            if (sreturn.CompareTo("NO") == 0)//es un profe
            {
                sreturn = gestorProfe.Modificarprofesor(dni, nombre, apellido);
                if (sreturn.CompareTo("NO") == 0) mensaje("No se encontró el usuario o es admin", false);//no existe o es admin
            }
            if (sreturn.CompareTo("") == 0)
            {
                mensaje("Se modificaron los datos correctamente", true);
                
            }
            else mensaje(sreturn, false);
        }

        protected void btn_datos_personales_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(view_datos_personales);

        }

        protected void btn_contacto_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(view_contacto);

        }

        protected void btn_pass_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(view_pass);

        }

        protected void btn_foto_perfil_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(view_foto_perfil);

        }

        protected void ddl_provincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboCiudades(int.Parse(ddl_localidad.SelectedValue));
        }
    }
}