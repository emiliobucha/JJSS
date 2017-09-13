using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using System.IO;
using System.Globalization;
using System.Data;

namespace JJSS.Presentacion
{
    public partial class InscripcionClase : System.Web.UI.Page
    {
        private GestorAlumnos gestorAlumnos;
        private GestorClases gestorClases;
        private GestorInscripcionesClase gestorInscripcionClase;
        private DataTable dtHorarios;
        private int id_Clase;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorAlumnos = new GestorAlumnos();
            gestorClases = new GestorClases();
            gestorInscripcionClase = new GestorInscripcionesClase();



            if (!IsPostBack)
            {
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
                            Response.Write("<script>window.alert('" + "No se encuentra logeado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logeado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                }

                if (Session["id_clase"] == null )
                {
                    Response.Redirect("../Presentacion/Inicio.aspx");
                }
                else 
                {
                    id_Clase = (int)Session["id_clase"];
                }

                

                ViewState["gvDatosOrden"] = "dni";
                gvAlumnos.AllowPaging = true;
                gvAlumnos.AutoGenerateColumns = false;
                gvAlumnos.PageSize = 20;
                CargarGrilla();
                CargarDatosDeClase();
                CargarComboFajas();
                definirVisualizacionDePaneles(false);
            }
        }



        //____________    Carga inicial de datos      __________
        /*
         * Carga de datos de la clase seleccionada
         */


        protected void CargarComboFajas()
        {
            List<faja> fajas = gestorInscripcionClase.ObtenerFajas();
            ddl_fajas.DataSource = fajas;
            ddl_fajas.DataTextField = "descripcion";
            ddl_fajas.DataValueField = "id_faja";
            ddl_fajas.DataBind();
        }

        protected void CargarDatosDeClase()
        {         
            clase datos_clase = gestorClases.ObtenerClasePorId(id_Clase);

            //cargar datos generales
            lbl_nombre_clase.Text = datos_clase.nombre.ToString();
            lbl_tipo_clase.Text = datos_clase.id_tipo_clase.ToString();
            lbl_precio.Text = datos_clase.precio.ToString();

            //cargar horarios con sus respectivos horarios
            dtHorarios = gestorClases.ObtenerTablaHorarios(id_Clase);
            dtHorarios.AcceptChanges();

            DataView dv_horarios = dtHorarios.DefaultView;
            dv_horarios.Sort = "dia asc, hora_desde asc";
            dg_horarios.DataSource = dv_horarios;
            dg_horarios.DataBind();
        }

        /*
         * Carga de la Grilla del listado de alumnos
         */
        protected void CargarGrilla()
        {
            int dni = 0;
            if (txt_filtro_dni.Text.CompareTo("") != 0) dni = int.Parse(txt_filtro_dni.Text);
            string orden = ViewState["gvDatosOrden"].ToString();
            gvAlumnos.DataSource = gestorAlumnos.BuscarAlumnoPorDni(dni);
            gvAlumnos.DataBind();
        }

        //____________     Mensajes de error    __________
        /*
         * Administracion de visualizaciones de mensajes
         */
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

        //____________  Metodos de uso del panel de alumnos  __________

            
        /*
         * Acción del boton buscar, que se encarga de filtrar por dni la grilla de alumnos
         */
        protected void btn_buscar_alumno_Click(object sender, EventArgs e)
        {
            CargarGrilla();
            //Session["alumnos"] = "Administrar";
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            pnl_listado_alumnos.Visible = true;
        }        
     
        /*
         * Método que surge de la interacción con la grilla de alumnos para llevar adelante la inscripcion
         */
        protected void gvAlumnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("inscribir") == 0)
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
                
                //captura de datos de la grilla
                int index = Convert.ToInt32(e.CommandArgument);
                int dniAlumno = Convert.ToInt32(gvAlumnos.DataKeys[index].Value);

                string nombre_Alumno = gvAlumnos.Rows[index].Cells[0].Text;
                string apellido_Alumno = gvAlumnos.Rows[index].Cells[1].Text;
                cargaDatosAlumno(dniAlumno,nombre_Alumno,apellido_Alumno);

              
            }
            
        }
        /*
         * Método de ordenacion de la grilla por dni
         */
        protected void gvAlumnos_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["gvDatosOrden"] = e.SortExpression;  //titulo de la columna que hizo click, la base de datos es mas optima para ordenarlos.
            CargarGrilla();
        }


        /*
         * Armado de la paginacion
         */
        protected void gvAlumnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAlumnos.PageIndex = e.NewPageIndex;
            CargarGrilla();
            pnl_listado_alumnos.Visible = true;
        }

        //____________  Métodos de uso del panel de datos del alumno  __________

        /*
         * Carga de los datos del alumno seleccionado para inscribir.
         */
        protected void cargaDatosAlumno(int dni, string nombre, string apellido)
        {
            definirVisualizacionDePaneles(true);
            lbl_alumno_apellido.Text = apellido;
            lbl_alumno_dni.Text = dni.ToString();
            lbl_alumno_nombre.Text = nombre;
        }

        /*
         * Se define la visibilidad del panel con los datos del alumno y del panel del listado de los mismos
         */
        protected void definirVisualizacionDePaneles(bool ver)
        {
            if(ver == true)
            {
                pnl_listado_alumnos.Visible = false;
                pnl_datos_alumnos.Visible = true;
            }
            else
            {
                pnl_listado_alumnos.Visible = true;
                pnl_datos_alumnos.Visible = false;
            }
        }

        /*
        * Método que finaliza la inscripcion del alumno
        */
        protected void btn_aceptar_inscripcion_Click(object sender, EventArgs e)
        {
            int dniAlumno = Convert.ToInt32(lbl_alumno_dni.ToString()); 
            DateTime pfecha = DateTime.Now;

            string phora = pfecha.ToShortTimeString();
            try
            {
                string sReturn = gestorInscripcionClase.InscribirAlumnoAClase(dniAlumno, id_Clase, phora, pfecha);
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

            mensaje("Proximamente", false);
        }



        //____________  Metodos y Botones generales __________

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Presentacion/Inicio.aspx");
        }

       
    }
}