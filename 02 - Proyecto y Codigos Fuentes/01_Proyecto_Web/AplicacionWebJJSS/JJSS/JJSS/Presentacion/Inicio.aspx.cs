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

        protected void Page_Load(object sender, EventArgs e)
        {

            //try
            //{ 
            //    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
            //    if (sesionActiva.estado != "INGRESO ACEPTADO")
            //    {
            //        Response.Write("<script>window.alert('" + "No se encuentra logeado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

            //    }
            //}
            //catch(Exception ex)
            //{
            //    Response.Write("<script>window.alert('" + "No se encuentra logeado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

            //}

            gestorDeTorneos = new GestorTorneos();
            gestorInscripciones = new GestorInscripciones();
            gestorDeClases = new GestorClases();
            gestorTipoClase = new GestorTipoClase();
            gestorInscripcionClase = new GestorInscripcionesClase();
            if (!IsPostBack)
            {
                cargarTorneosExportarListado();
                cargarClases();
                cargarTorneosAbiertos();
                cargarRecarga();
                cargarTorneosAbiertosView();
            }



            //   this.btn_confirmar_dni.ServerClick += MetodoClick;


            //ClientScript.GetPostBackEventReference(this, string.Empty);
            //if (Request.Form["__EVENTTARGET"] == "btnGenerarListado_Click")
            //{
            //    //llamamos el metodo que queremos ejecutar, en este caso el evento onclick del boton Button2
            //    btnGenerarListado_Click(this, new EventArgs());
            //}

        }

        //protected void cargarComboFajas()
        //{

        //    int idClase = 0;
        //    int.TryParse(hf_claseSeleccionada_id.Value, out idClase);
        //    if (idClase != 0)
        //    {
        //        List<faja> fajas = gestorTipoClase.ObtenerFajasSegunTipoClase(idClase);
        //        if (fajas != null)
        //        {
        //            ddl_faja.DataSource = fajas;
        //            ddl_faja.DataTextField = "descripcion";
        //            ddl_faja.DataValueField = "id_faja";
        //            ddl_faja.DataBind();
        //        }
        //        else
        //        {
        //            ddl_faja.Visible = false;
        //        }
        //    }
        //}
        
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

        protected void cargarTorneosAbiertos()
        {
            gv_torneosAbiertos.DataSource = gestorDeTorneos.ObtenerTorneos();
            gv_torneosAbiertos.DataBind();   
        }

        protected void cargarTorneosAbiertosView()
        {
            lv_torneos_abiertos.DataSource = gestorDeTorneos.ObtenerTorneosConImagen();
            lv_torneos_abiertos.DataBind();
        }


        protected void gv_torneosAbiertos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("inscribir") == 0)
            {
                int indice = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(gv_torneosAbiertos.DataKeys[indice].Value);


                Session["torneoSeleccionado"] = id;

                Response.Redirect("~/Presentacion/InscripcionTorneo.aspx");
            }


        }
        protected void cargarClases()
        {
            gv_clasesDisponibles.DataSource = gestorDeClases.ObtenerClasesDisponibles();
            gv_clasesDisponibles.DataBind();
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

        //protected void btn_acpetarTorneoExportarLista_Click(object sender, EventArgs e)
        //{
        //    btnGenerarListado();
        //}



        protected void btn_inscripcionClase_aceptar_Click(object sender, EventArgs e)
        {

            try
            {
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                if (sesionActiva.estado != "INGRESO ACEPTADO")
                {
                    if ((int?)sesionActiva.permisos.Select("perm_clave = 'CLASE_INSCRIPCION'")[0]["perm_ejecutar"] != 1)
                    {
                        Response.Write("<script>window.alert('" + "Debe estar logueado como alumno para inscribirse".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentra logeado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

            }

            int idClase = 0;
            int.TryParse(hf_claseSeleccionada_id.Value, out idClase);

            //int idClase = Convert.ToInt32(lbl_claseSeleccionada_id.Text);
            int dniAlumno = int.Parse(txt_inscripcionClase_dni.Text);
            DateTime pfecha = DateTime.Now;

            string phora = pfecha.ToShortTimeString();
            try
            {
                string sReturn = gestorInscripcionClase.InscribirAlumnoAClase(dniAlumno, idClase, phora, pfecha);
                //if (sReturn != "") mensajeInsClase(sReturn, false);
                //else
                //{
                //    gestorAlumnos = new GestorAlumnos();
                //    string ddl = ddl_faja.SelectedValue;
                //    if (ddl_faja.SelectedIndex >= 0) sReturn = gestorAlumnos.AsignarFaja(dniAlumno, int.Parse(ddl_faja.SelectedValue));
                //    if (sReturn != "") mensajeInsClase(sReturn, false);
                //    else mensajeInsClase("Alumno inscripto", true);

                //}
            }
            catch (Exception ex)
            {
                //mensajeInsClase(ex.Message.Trim(), false);
            }
        }

        
        protected void btn_acpetarTorneoExportarLista_Click(object sender, EventArgs e)
        {
            btnGenerarListado();
        }


        protected void gv_clasesDisponibles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Eliminar") == 0)
            {
                // int idClase = int.Parse(gv_clasesDisponibles.SelectedValue.ToString());
                int index = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(gv_clasesDisponibles.DataKeys[index].Value);
                string sReturn = gestorDeClases.eliminarClase(id);
                if (sReturn.CompareTo("") == 0) sReturn = "Se eliminó la clase correctamente";
                mensaje(sReturn);
                cargarClases();
            }
            else if (e.CommandName.CompareTo("Seleccionar") == 0)
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(gv_clasesDisponibles.DataKeys[index].Value);
                Session["clase"] = id;
                Response.Redirect("~/Presentacion/CrearClase");
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

            Session["torneoSeleccionado"] = id;

            Response.Redirect("~/Presentacion/InscripcionTorneo.aspx");
        }

    }
}