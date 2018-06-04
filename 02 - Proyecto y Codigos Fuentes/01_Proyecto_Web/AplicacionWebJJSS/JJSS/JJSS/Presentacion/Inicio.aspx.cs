using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion
{
    public partial class Inicio : System.Web.UI.Page
    {
        private GestorInscripciones gestorInscripciones;
        private GestorTorneos gestorDeTorneos;
        private GestorClases gestorDeClases;
        private GestorInscripcionesClase gestorInscripcionClase;
        private torneo torneoSeleccionado;
        private int? idAlumno = null;
        private GestorAlumnos gestorAlumnos;
        private GestorParametro gestorParametro;
        private GestorTipoClase gestorTipoClase;
        private GestorEventos gestorEventos;
        private GestorAsistencia gestorAsistencia;

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {



                if (HttpContext.Current.Session["SEGURIDAD_SESION"].ToString() == "INVITADO")
                {
                    ocultarInvitado();

                }
                else
                {

                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado != "INGRESO ACEPTADO")
                    {

                        Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                    }
                    ocultarPermiso();
                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

            }

            gestorDeTorneos = new GestorTorneos();
            gestorInscripciones = new GestorInscripciones();
            gestorDeClases = new GestorClases();
            gestorTipoClase = new GestorTipoClase();
            gestorInscripcionClase = new GestorInscripcionesClase();
            gestorEventos = new GestorEventos();
            gestorAsistencia = new GestorAsistencia();
            if (!IsPostBack)
            {
                cargarTorneosExportarListado();
                cargarEventosExportarListado();
                cargarClasesExportarListado();
                cargarClasesView();
                cargarRecarga();
                cargarTorneosAbiertosView();
                CargarListViewEventos();
            }



            //   this.btn_confirmar_dni.ServerClick += MetodoClick;


            //ClientScript.GetPostBackEventReference(this, string.Empty);
            //if (Request.Form["__EVENTTARGET"] == "btnGenerarListado_Click")
            //{
            //    //llamamos el metodo que queremos ejecutar, en este caso el evento onclick del boton Button2
            //    btnGenerarListado_Click(this, new EventArgs());
            //}

        }



        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private string calcularEdad(DateTime? pFechaNacimiento)
        {
            //+habria que ver como calcularla mejor porque si estuviera calculando mi edad daria 22 pero todavia tengo 21
            DateTime fechaNac = Convert.ToDateTime(pFechaNacimiento);
            int edad = DateTime.Today.Year - fechaNac.Year;
            return edad.ToString();
        }


        protected void btn_acpetarAsistenciaExportarLista_Click(object sender, EventArgs e)
        {
            btnGenerarListadoAsistencia();


        }

        protected void btnGenerarListadoAsistencia()
        {
            int idHorario = 0;
            int.TryParse(ddl_clasesListado.SelectedValue, out idHorario);
      

            // int.TryParse(ddl_torneos.SelectedValue, out idTorneo);
            try
            {
              

                    String sFile = gestorAsistencia.GenerarListado(idHorario, DateTime.Today);

                    Response.Clear();
                    Response.AddHeader("Content-Type", "Application/octet-stream");
                    Response.AddHeader("Content-Disposition",
                        "attachment; filename=\"" + System.IO.Path.GetFileName(sFile) + "\"");
                    Response.WriteFile(sFile);
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentran alumnos asistentes a esta clase".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Inicio.aspx" + "', 2000);</script>");
            }
        }



        protected void btn_acpetarEventoExportarLista_Click(object sender, EventArgs e)
        {
            btnGenerarListadoEvento();
        }


        protected void btnGenerarListadoEvento()
        {
            int idEvento = 0;
            int.TryParse(ddl_eventoExportarListado.SelectedValue, out idEvento);

            // int.TryParse(ddl_torneos.SelectedValue, out idTorneo);
            try
            {
                String sFile = gestorEventos.GenerarListado(idEvento);

                Response.Clear();
                Response.AddHeader("Content-Type", "Application/octet-stream");
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + System.IO.Path.GetFileName(sFile) + "\"");
                Response.WriteFile(sFile);
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentran alumnos inscriptos a ese evento".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Inicio.aspx" + "', 2000);</script>");
            }
        }




        /*
         * Carga de datos en las cuadriculas correspondientes de  torneo y clases
         */
        protected void cargarTorneosAbiertosView()
        {
            //lv_torneos_abiertos.DataSource = gestorDeTorneos.ObtenerTorneosConImagen();
            lv_torneos_abiertos.DataBind();
        }

        protected void cargarClasesView()
        {
            lv_clasesDisponibles.DataSource = gestorDeClases.ObtenerClasesDisponibles("",0,0);
            lv_clasesDisponibles.DataBind();

            lv_clasesDisponibles_invitado.DataSource = gestorDeClases.ObtenerClasesDisponibles("",0,0);
            lv_clasesDisponibles_invitado.DataBind();
        }

        protected void registrarAlumno_Click(object sender, EventArgs e)
        {
            Session["alumnos"] = "Registrar";
            Response.Redirect("~/Presentacion/RegistrarAlumno.aspx");
        }

        protected void administrarAlumnos_Click(object sender, EventArgs e)
        {
            Session["alumnos"] = "Administrar";
            Response.Redirect("~/Presentacion/RegistrarAlumno.aspx");
        }

        protected void cargarRecarga()
        {
            txt_modal_recarga.Text = "100";
        }

        protected void cargarTorneosExportarListado()
        {
            List<torneo> torneosExportar = gestorDeTorneos.ObtenerTorneosAbiertosCerrados();
            ddl_torneoExportarListado.DataSource = torneosExportar;
            ddl_torneoExportarListado.DataTextField = "nombre";
            ddl_torneoExportarListado.DataValueField = "id_torneo";
            ddl_torneoExportarListado.DataBind();
        }

        protected void cargarEventosExportarListado()
        {
            List<evento_especial> eventosExportar = gestorEventos.ObtenerEventosAbiertosCerrados();
            ddl_eventoExportarListado.DataSource = eventosExportar;
            ddl_eventoExportarListado.DataTextField = "nombre";
            ddl_eventoExportarListado.DataValueField = "id_evento";
            ddl_eventoExportarListado.DataBind();
        }

        protected void cargarClasesExportarListado()
        {
            List<HorariosResultado> horarios = gestorDeClases.ObtenerHorariosResultadosDeFecha(DateTime.Now);
            ddl_clasesListado.DataSource = horarios;
            ddl_clasesListado.DataTextField = "nombre_horario";
            ddl_clasesListado.DataValueField = "id";
            ddl_clasesListado.DataBind();


        }

        //protected void btn_acpetarTorneoExportarLista_Click(object sender, EventArgs e)
        //{
        //    btnGenerarListado();
        //}

        protected void btn_acpetarTorneoExportarLista_Click(object sender, EventArgs e)
        {
            btnGenerarListado();
        }

        protected void btnGenerarListado()
        {
            int idTorneo = 0;
            int.TryParse(ddl_torneoExportarListado.SelectedValue, out idTorneo);

            // int.TryParse(ddl_torneos.SelectedValue, out idTorneo);
            try
            {
                String sFile = gestorDeTorneos.GenerarListado(idTorneo);

                Response.Clear();
                Response.AddHeader("Content-Type", "Application/octet-stream");
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + System.IO.Path.GetFileName(sFile) + "\"");
                Response.WriteFile(sFile);
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentran alumnos inscriptos a ese torneo".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Inicio.aspx" + "', 2000);</script>");
            }
        }

        private void mensaje(string pMensaje)
        {
            Response.Write("<script>window.alert('" + pMensaje.Trim() + "');</script>");
        }

        protected void btn_cerrar_sesion_Click(object sender, EventArgs e)
        {
            GestorSesiones gestorSesion = new GestorSesiones();
            gestorSesion.CerrarSesion();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Presentacion/Login.aspx");
        }

        protected void btn_modal_recarga_aceptar_Click(object sender, EventArgs e)
        {
            gestorParametro = new GestorParametro();
            string mensaje = gestorParametro.modificarParametro(1, decimal.Parse(txt_modal_recarga.Text));
            if (mensaje.CompareTo("") == 0) mensaje = "Se actualizó exitosamente";
            Response.Write("<script>window.alert('" + mensaje + "');</script>");
        }

        protected void lnk_gradiacion_alumnos_Click(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            GestorPagoClase gp = new GestorPagoClase();
            gp.validarPagoParaAsistencia(11, 3);
        }

        protected void rb_tipo_clase_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cargarComboFajas();
        }

        protected void lv_torneos_abiertos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Session["idTorneo_inscribirTorneo"] = id;
            Response.Redirect("~/Presentacion/InscripcionTorneo.aspx");
        }

        protected void ocultarInvitado()
        {
            //item_modificar_perfil.Style["display"] = "none";

            administracion_torneos.Style["display"] = "none";
            administracion_clases.Style["display"] = "none";
            administracion_alumnos.Style["display"] = "none";
            administracion_profesores.Style["diplay"] = "none";
            administracion_tienda.Style["diplay"] = "none";
            administracion_permisos.Style["display"] = "none";
           
            muetra_clases_profe_admin.Style["display"] = "none";
            item_crear_evento.Style["display"] = "none";
            item_listado_evento.Style["display"] = "none";

            menuAlumnos.Style["display"] = "none";
            menuProfesores.Style["display"] = "none";
            menuTienda.Style["display"] = "none";
            menuPermisos.Style["display"] = "none";
            menuClases.Style["display"] = "none";


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
                        item_Generar_Torneo.Style["display"] = "none";
                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_INSCRIPCION_LISTA'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        item_Generar_Listado_inscriptos.Style["display"] = "none";
                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_INSCRIPCION_LISTA'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        item_listado_evento.Style["display"] = "none";
                    }


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'TORNEO_INSCRIPCION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ver"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        //item_mis_Torneos.Style["display"] = "none";
                    }


                    //Administración de Clases

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_INSCRIPCION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                        muetra_clases_invitado.Style["display"] = "none";
                    }
                    if (permiso != 1)
                    {
                        item_inscribir_Alumno_Clase.Style["display"] = "none";
                       

                    }


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASES_MIS_CLASES'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                        muetra_clases_profe_admin.Style["display"] = "none";
                    }
                    if (permiso != 1)
                    {
                        item_mis_Clases.Style["display"] = "none";
                        
                    }




                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_HORARIO'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {

                    }


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_ALUMNOS'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {

                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                        muetra_clases_invitado.Style["display"] = "none";
                    }
                    if (permiso != 1)
                    {
                        item_crear_nueva_clase.Style["display"] = "none";
                        item_listado_asistencia.Style["display"] = "none";
                        item_registrar_asistencia.Style["display"] = "none";
                        muetra_clases_profe_admin.Style["display"] = "none";

                    }



                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'ALUMNO_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        item_registrar_alumno.Style["display"] = "none";
                        item_graduacion_alumnos.Style["display"] = "none";
                       
                        administracion_alumnos.Style["display"] = "none";
                        menuAlumnos.Style["display"] = "none";
                    }


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'EVENTO_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        item_crear_evento.Style["display"] = "none";
                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'ALUMNO_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_modificar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        item_administrar_alumnos.Style["display"] = "none";
                        item_graduacion_alumnos.Style["display"] = "none";
                        alumnos.Style["display"] = "none";
                    }



                    //permiso = 0;
                    //drsAux = sesionActiva.permisos.Select("perm_clave = 'SEGURIDAD_USUARIOS'");
                    //if (drsAux.Length > 0)
                    //{
                    //    int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    //}
                    //if (permiso != 1)
                    //{
                    //    section_permisos.Style["display"] = "none";
                    //}

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'SEGURIDAD_PERMISOS'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        administracion_permisos.Style["display"] = "none";
                        menuPermisos.Style["display"] = "none";
                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'PROFESOR_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        administracion_profesores.Style["display"] = "none";
                        menuProfesores.Style["display"] = "none";
                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'PARAMETROS'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_modificar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        item_recarga_por_atraso.Style["display"] = "none";
                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'PRODUCTO_COMPRA'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_modificar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        item_compra_producto.Style["display"] = "none";

                        
                    }

                    //+REVER ESTOOOOO
                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'PRODUCTO_COMPRA'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_modificar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        item_seguimiento_reserva.Style["display"] = "none";


                    }


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave= 'PRODUCTOS_AGREGAR'");
                    if (drsAux.Length >0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        btn_agregarProducto.Style["display"] = "none";
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

            }
        }

        protected void lv_clasesDisponibles_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.CompareTo("inscribir") == 0)
            {
                Session["id_clase"] = id;
                Response.Redirect("~/Presentacion/InscripcionClase.aspx");
            }
            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                gestorDeClases.eliminarClase(id);
                Response.Write("<script>window.alert('" + "Se eliminó la clase correctamente".Trim() + "');</script>");
                cargarClasesView();
            }
            if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                Session["clase"] = id;
                Response.Redirect("~/Presentacion/CrearClase.aspx");
            }

        }

        protected void lnk_registrar_profe_Click(object sender, EventArgs e)
        {

            Session["profesor"] = "Registrar";
            Response.Redirect("~/Presentacion/RegistrarProfe.aspx");

        }

        protected void lnk_administrar_profes_Click(object sender, EventArgs e)
        {
            Session["profesor"] = "Administrar";
            Response.Redirect("~/Presentacion/RegistrarProfe.aspx");
        }

        protected void lnk_crearClase_Click(object sender, EventArgs e)
        {
            Session["clase"] = null;
            Response.Redirect("../Presentacion/CrearClase.aspx");
        }

        protected void ddl_clasesListado_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_clasesListado.SelectedIndex > 0)
            {

            }
        }

        protected void lv_eventos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Session["eventoSeleccionado"] = id;
            Response.Redirect("~/Presentacion/InscripcionEvento.aspx");
        }

        private void CargarListViewEventos()
        {
            gestorEventos = new GestorEventos();
            lv_eventos.DataSource = gestorEventos.ObtenerEventosConImagen();
            lv_eventos.DataBind();
        } 
    }
}