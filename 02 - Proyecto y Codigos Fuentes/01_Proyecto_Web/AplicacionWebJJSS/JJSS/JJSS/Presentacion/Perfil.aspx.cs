using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;
using JJSS_Entidad;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using JJSS_Negocio.Resultados;
using Image = System.Drawing.Image;

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
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                if (sesionActiva.estado == "INGRESO ACEPTADO")
                {



                    MultiView1.SetActiveView(view_datos_personales);
                    CargarComboProvincias();
                    CargarDatos();
                    //CargarComboCiudades(1);

                }
                else
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                }
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
            if (alumnoActual == null) mensaje("No está logueado", false);
            else
            {


                int id_usuario = alumnoActual.id_usuario;


                alumno alumnoEncontrado = gestorAlumnos.ObtenerAlumnoPorIdUsuario(id_usuario);
                if (alumnoEncontrado == null) //es un profe
                {
                    profesor profeEncontrado = gestorProfe.ObtenerProfesorPorIdUsuario(id_usuario);
                    if (profeEncontrado == null)
                        mensaje("No se encontró el usuario o es admin", false); //no existe o es admin
                    else
                    {
                        DataTable direccionEncontrada =
                            gestorProfe.ObtenerDireccionProfesor(profeEncontrado.id_profesor);

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
                            ddl_provincia.SelectedValue = row["idProvincia"].ToString();
                            CargarComboCiudades(int.Parse(ddl_provincia.SelectedValue));
                            ddl_localidad.SelectedValue = row["idCiudad"].ToString();
                            txt_torre.Text = row["torre"].ToString();
                        }

                        var imgBytes = gestorProfe.ObtenerImagenPerfil(profeEncontrado.id_profesor);
                        if (imgBytes != null)
                        {
                            using (MemoryStream ms = new MemoryStream(imgBytes))
                            {
                                try
                                {

                                    string sDir = System.Web.HttpContext.Current.Server.MapPath("//temp");

                                    if (!System.IO.Directory.Exists(sDir))
                                    {
                                        System.IO.Directory.CreateDirectory(sDir);
                                    }


                                    var archivo =
                                        (profeEncontrado.nombre + profeEncontrado.apellido + profeEncontrado.dni)
                                        .Replace(" ", "") + ".jpeg";
                                    var imagenI = Image.FromStream(ms);

                                    char[] sTrim = "\\".ToCharArray();
                                    var archivoGuardar = sDir.Trim(sTrim) + "\\" + archivo;

                                    //if (!File.Exists(archivo))
                                    //{
                                    imagenI.Save(archivoGuardar, ImageFormat.Jpeg);
                                    //}


                                    var link = "\\temp\\" + archivo;
                                    Avatar.ImageUrl = link;

                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                    }

                }



                else
                {
                   

                    txt_dni.Text = alumnoEncontrado.dni.ToString();
                    txt_nombre.Text = alumnoEncontrado.nombre;
                    txt_apellido.Text = alumnoEncontrado.apellido;
                    txt_email.Text = alumnoEncontrado.mail;
                    txt_telefono.Text = alumnoEncontrado.telefono.ToString();
                    txt_telefono_urgencia.Text = alumnoEncontrado.telefono_emergencia.ToString();

                    // busco la direccion
                    DireccionAlumno direccionEncontrada = gestorAlumnos.ObtenerDireccionAlumno(alumnoEncontrado.id_alumno);
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

                    var imgBytes = gestorAlumnos.ObtenerImagenPerfil(alumnoEncontrado.id_alumno);

                    if (imgBytes != null)
                    {
                        using (MemoryStream ms = new MemoryStream(imgBytes))
                        {
                            try
                            {

                                string sDir = System.Web.HttpContext.Current.Server.MapPath("//temp");

                                if (!System.IO.Directory.Exists(sDir))
                                {
                                    System.IO.Directory.CreateDirectory(sDir);
                                }


                                var archivo = (alumnoEncontrado.nombre + alumnoEncontrado.apellido + alumnoEncontrado.dni).Replace(" ", "") + ".jpeg";
                                var imagenI = Image.FromStream(ms);

                                char[] sTrim = "\\".ToCharArray();
                                var archivoGuardar = sDir.Trim(sTrim) + "\\" + archivo;

                                //if (!File.Exists(archivo))
                                //{
                                imagenI.Save(archivoGuardar, ImageFormat.Jpeg);
                                //}


                                var link = "\\temp\\" + archivo;
                                Avatar.ImageUrl = link;

                            }
                            catch (Exception ex)
                            {
                            }

                        }
                    }

                }

                txt_usuario.Text = alumnoActual.login;
            }
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
            string torre = txt_torre.Text;

            int idCiudad = int.Parse(ddl_localidad.SelectedValue);


            //modifica los datos
            try
            {
                gestorAlumnos.ModificarAlumno(calle, departamento, numero, piso, tel, telUrg, mail, int.Parse(txt_dni.Text), idCiudad, torre);
                mensaje("Se modificaron los datos correctamente", true);
            }
            catch (Exception ex)
            {
                if (ex.Message.CompareTo("El usuario no existe") == 0)

                {
                    try
                    {
                        gestorProfe.ModificarProfesor(calle, departamento, numero, piso, tel, telUrg, mail, int.Parse(txt_dni.Text), idCiudad, torre);
                        mensaje("Se modificaron los datos correctamente", true);
                    }
                    catch (Exception exx)
                    {
                        mensaje(exx.Message, false);
                    }
                }
                else mensaje(ex.Message, false);
            }


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
            System.IO.Stream imagen = avatarUpload.PostedFile.InputStream;
            byte[] imagenByte;
            using (MemoryStream ms = new MemoryStream())
            {
                imagen.CopyTo(ms);
                imagenByte = ms.ToArray();
            }


            try
            {
                gestorAlumnos.CambiarFotoPerfil(int.Parse(txt_dni.Text), imagenByte);
                mensaje("Se modificaron los datos correctamente", true);
                CargarDatos();
            }
            catch (Exception ex)
            {
                if (ex.Message.CompareTo("El usuario no existe") == 0)

                {
                    try
                    {
                        gestorProfe.CambiarFotoPerfil(int.Parse(txt_dni.Text), imagenByte);
                        mensaje("Se modificaron los datos correctamente", true);
                        CargarDatos();
                    }
                    catch (Exception exx)
                    {
                        mensaje(exx.Message, false);
                    }
                }
                else mensaje(ex.Message, false);
            }



        }

        protected void btn_guardar_personal_Click(object sender, EventArgs e)
        {
            string apellido = txt_apellido.Text;
            string nombre = txt_nombre.Text;
            int dni = int.Parse(txt_dni.Text);

            //+ NO ESTA CONTEMPLADO SI ES ADMIN...

            //modifica los datos
            try
            {
                gestorAlumnos.ModificarAlumno(dni, nombre, apellido, null, null);
                mensaje("Se modificaron los datos correctamente", true);
            }
            catch (Exception ex)
            {
                if (ex.Message.CompareTo("El usuario no existe") == 0)

                {
                    try
                    {
                        gestorProfe.ModificarProfesor(dni, nombre, apellido);
                        mensaje("Se modificaron los datos correctamente", true);
                    }
                    catch (Exception exx)
                    {
                        mensaje(exx.Message, false);
                    }
                }
                else mensaje(ex.Message, false);
            }

        }

        protected void btn_datos_personales_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(view_datos_personales);
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;

        }

        protected void btn_contacto_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(view_contacto);
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }

        protected void btn_pass_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(view_pass);
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }

        protected void btn_foto_perfil_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(view_foto_perfil);
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }

        protected void ddl_provincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboCiudades(int.Parse(ddl_provincia.SelectedValue));
        }
    }
}