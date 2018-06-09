using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;
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
        private static int id_Clase;
        private static List<AlumnoConEstado> alumnos;

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
                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                }

                if (Session["id_clase"] == null)
                {
                    Response.Redirect("Menu_Clase.aspx");
                }
                else
                {
                    id_Clase = Convert.ToInt32(Session["id_clase"]);
                    Session["id_clase"] = null;
                }



                ViewState["gvDatosOrden"] = "dni";
                gvAlumnos.AllowPaging = true;
                gvAlumnos.AutoGenerateColumns = false;
                gvAlumnos.PageSize = 15;
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
            clase datos_clase = gestorClases.ObtenerClasePorId(id_Clase);

            List<faja> fajas = gestorInscripcionClase.ObtenerFajasPorTipoClase((int)datos_clase.id_tipo_clase);
            if (fajas.Count == 0)
            {
                ddl_fajas.Enabled = false;
            }
            else
            {
                ddl_fajas.Enabled = true;
                ddl_fajas.DataSource = fajas;
                ddl_fajas.DataTextField = "descripcion";
                ddl_fajas.DataValueField = "id_faja";
                ddl_fajas.DataBind();
            }
        }

        protected void CargarDatosDeClase()
        {
            clase datos_clase = gestorClases.ObtenerClasePorId(id_Clase);

            //cargar datos generales
            lbl_nombre_clase.Text = datos_clase.nombre.ToString();
            profesor profe = gestorClases.ObtenerProfesorPorID((int)datos_clase.id_profe);
            lbl_profesor.Text = "Profesor: " + profe.apellido + ", " + profe.nombre;
            lbl_ubicacion.Text = "Ubicación: " + gestorClases.ObtenerAcademiasPorID((int)datos_clase.id_ubicacion).nombre;
            lbl_tipo_clase.Text = "Tipo de Clase: " + gestorClases.ObtenerTipoClasesPorID((int)datos_clase.id_tipo_clase).nombre;



            lbl_precio.Text = "Precio: $" + datos_clase.precio.ToString();

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
            var dni = txt_filtro_dni.Text;
            List<AlumnoConEstado> listaCompleta = mostrarAlumnosNoInscriptos();
            alumnos = listaCompleta;
            List<AlumnoConEstado> listaConFiltro = new List<AlumnoConEstado>();
            string filtroApellido = txt_filtro_apellido.Text.ToUpper();

            foreach (AlumnoConEstado i in listaCompleta)
            {
                string apellido = i.alu_apellido.ToUpper();
                if (string.IsNullOrEmpty(dni)) if (apellido.StartsWith(filtroApellido)) listaConFiltro.Add(i);

                if (apellido.StartsWith(filtroApellido) && i.alu_dni == dni) listaConFiltro.Add(i);
            }

            gvAlumnos.DataSource = listaConFiltro;
            gvAlumnos.DataBind();
        }

        private List<AlumnoConEstado> mostrarAlumnosNoInscriptos()
        {
            List<alumno> alumnosInscriptos = gestorInscripcionClase.ObtenerAlumnosDeUnaClase(id_Clase);
            List<alumno> alumnos = gestorAlumnos.BuscarAlumno();
            List<AlumnoConEstado> alumnoParaMostrar=new List<AlumnoConEstado>();
            Boolean encontro = false;

            foreach (alumno a in alumnos)
            {
                foreach (alumno alumnoInscripto in alumnosInscriptos)
                {
                    if (a.dni == alumnoInscripto.dni)
                    {
                        encontro = true;
                        break;
                    }
                    else encontro = false;
                }

                AlumnoConEstado ae = new AlumnoConEstado()
                {
                    alu_apellido = a.apellido,
                    alu_dni = a.dni,
                    alu_nombre = a.nombre,
                    alu_id= a.id_alumno,
                };
                if (encontro) ae.inscripto = "SI";
                else ae.inscripto = "NO";
                
                alumnoParaMostrar.Add(ae);
            }
            
            return alumnoParaMostrar;
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
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                }

                //captura de datos de la grilla
                int id = Convert.ToInt32(e.CommandArgument);
                AlumnoConEstado alumnoSeleccionado = new AlumnoConEstado();
                foreach(AlumnoConEstado a in alumnos)
                {
                    if (a.alu_id == id)
                    {
                        alumnoSeleccionado = a;
                        break;
                    }
                }
                
                string nombre_Alumno = alumnoSeleccionado.alu_nombre;
                string apellido_Alumno = alumnoSeleccionado.alu_apellido;
                string dniAlumno = alumnoSeleccionado.alu_dni;
                cargaDatosAlumno(dniAlumno, nombre_Alumno, apellido_Alumno);


            }else if (e.CommandName.CompareTo("desinscribir") == 0)
            {
                int id = Convert.ToInt32(e.CommandArgument);

                string res = gestorInscripcionClase.DarDeBajaInscripcion(id, id_Clase);
                if (res.CompareTo("") == 0)
                {
                    mensaje("Se dio de baja a la inscripción correctamente", true);
                    CargarGrilla();
                }
                else
                {
                    mensaje(res, false);
                }
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
        protected void cargaDatosAlumno(string dni, string nombre, string apellido)
        {
            definirVisualizacionDePaneles(true);
            lbl_alumno_apellido.Text = apellido;
            lbl_alumno_dni.Text = dni.ToString();
            lbl_alumno_nombre.Text = nombre;
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }

        /*
         * Se define la visibilidad del panel con los datos del alumno y del panel del listado de los mismos
         */
        protected void definirVisualizacionDePaneles(bool ver)
        {
            if (ver == true)
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
            var dniAlumno = lbl_alumno_dni.Text;
            DateTime pfecha = DateTime.Now;

            string phora = pfecha.ToShortTimeString();
            int idFaja = 0;
            int.TryParse(ddl_fajas.SelectedValue.ToString(), out idFaja);
            id_Clase = int.Parse(Session["id_clase"].ToString());
            try
            {
                string sReturn = gestorInscripcionClase.InscribirAlumnoAClase(dniAlumno, id_Clase, phora, pfecha, idFaja);
                if (sReturn == "")
                {
                    mensaje("La inscripción se ha realizado correctamente", true);


                    definirVisualizacionDePaneles(true);
                    CargarGrilla();

                }
                else mensaje(sReturn, false);
            }
            catch (Exception ex)
            {
                mensaje(ex.Message, false);
            }
        }



        //____________  Metodos y Botones generales __________


        protected void btn_alumnos_Click(object sender, EventArgs e)
        {
            definirVisualizacionDePaneles(false);
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
        }

        protected void btn_buscar_apellido_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void btn_registrar_alumno_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Administracion/RegistrarAlumno.aspx");
        }

        protected void btn_Cancelar_Click1(object sender, EventArgs e)
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            Response.Redirect("Menu_Clase.aspx");
        }

        protected void gvAlumnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnInscribir = ((Button)e.Row.FindControl("btn_inscribir"));
                Button btnDesInscribir = ((Button)e.Row.FindControl("btn_desinscribir"));
                string inscripto =DataBinder.Eval(e.Row.DataItem, "inscripto").ToString();
                if (inscripto == "NO")//no esta inscripto
                {
                    btnInscribir.Text = "Inscribir";
                    btnDesInscribir.Text = "";
                }
                else
                {
                    btnInscribir.Text = "";
                    btnDesInscribir.Text = "Dar de baja";
                }
            }
        }
    }
}