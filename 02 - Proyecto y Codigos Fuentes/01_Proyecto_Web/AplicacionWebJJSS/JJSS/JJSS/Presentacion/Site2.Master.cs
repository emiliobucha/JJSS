using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;


namespace JJSS.Presentacion
{
    public partial class Site2 : System.Web.UI.MasterPage
    {
        GestorSesiones gestorSesion;
        GestorUsuarios gestorUsuarios;


        protected void Page_Load(object sender, EventArgs e)
        {
            gestorSesion = new GestorSesiones();
            gestorUsuarios = new GestorUsuarios();
            try
            {

                if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
                {
                    pnl_iniciar_sesion.Visible = true;
                    pnl_sesion_activa.Visible = false;
                    ocultarInvitado();
                }
                else
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva == null)
                    {
                        pnl_iniciar_sesion.Visible = true;
                        pnl_sesion_activa.Visible = false;
                    }
                    else
                    {
                        ocultarPermiso();
                        pnl_iniciar_sesion.Visible = false;
                        pnl_sesion_activa.Visible = true;
                        lbl_sesion_nombre.Text = sesionActiva.usuario.nombre.ToString();

                        lbl_roles.Text =
                            gestorUsuarios.obtenerTablaUsuarios().Select("login =  '" + sesionActiva.usuario.login + "'")[0][
                                "grupo_nombre"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() +
                               "');</script>" + "<script>window.setTimeout(location.href='" +
                               "../Presentacion/Login.aspx" + "', 2000);</script>");

            }

        }
        protected void cerrar_sesion_Click(object sender, EventArgs e)
        {
            gestorSesion = new GestorSesiones();
            gestorSesion.CerrarSesion();
        }


        protected void ocultarPermiso()
        {
            try
            {
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                if (sesionActiva.estado == "INGRESO ACEPTADO")
                {


                    //Administración de torneos

                    int permiso = 0;
                    System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        crearTorneo.Style["display"] = "none";
                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_INSCRIPCION_LISTA'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        listadoTorneo.Style["display"] = "none";
                    }



                    //Clases

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_INSCRIPCION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                    }
                    if (permiso != 1)
                    {
                        inscribirClase.Style["display"] = "none";
                    }


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASES_MIS_CLASES'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        misClases.Style["display"] = "none";

                    }


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                    }
                    if (permiso != 1)
                    {
                        crearClase.Style["display"] = "none";
                        listadoClase.Style["display"] = "none";
                        asisteciaClases.Style["display"] = "none";


                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'PARAMETROS'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_modificar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        recargaClase.Style["display"] = "none";
                    }


                    //Alumnos
                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'ALUMNO_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        registrarAlumno.Style["display"] = "none";
                        graduarAlumno.Style["display"] = "none";
                        pnl_alumnos.Style["display"] = "none";

                    }

                    //Profesores


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'PROFESOR_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        pnl_profesores.Style["display"] = "none";
                    }

                    //Tienda
                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave= 'PRODUCTOS_AGREGAR'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        agregarProducto.Style["display"] = "none";

                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave= 'PRODUCTO_COMPRA'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        comprarProducto.Style["display"] = "none";

                    }


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_INSCRIPCION_LISTA'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        listadoEvento.Style["display"] = "none";
                    }



                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        crearEvento.Style["display"] = "none";
                    }



                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'SEGURIDAD_PERMISOS'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        pnl_permisos.Style["display"] = "none";
                    }






                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

            }
        }

        protected void ocultarInvitado()
        {
            //item_modificar_perfil.Style["display"] = "none";

            crearTorneo.Style["display"] = "none";
            listadoTorneo.Style["display"] = "none";
            pnl_clases.Style["display"] = "none";

            pnl_alumnos.Style["display"] = "none";
            pnl_profesores.Style["display"] = "none";
            pnl_tienda.Style["diplay"] = "none";
            pnl_eventos_especiales.Style["diplay"] = "none";
            pnl_permisos.Style["display"] = "none";

        }

    }

}