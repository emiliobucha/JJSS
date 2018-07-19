using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;
using JJSS_Negocio.Administracion;

namespace JJSS.Presentacion
{
    public partial class Menu_Clase : System.Web.UI.Page
    {

        private GestorClases gestorDeClases;
        private GestorTipoClase gestorTipo;
        public bool MostrarEditar { get; set; } = true;
        public bool MostrarInscribir { get; set; } = true;


        protected void Page_Load(object sender, EventArgs e)
        {
            gestorDeClases = new GestorClases();
            gestorTipo = new GestorTipoClase();
            if (!IsPostBack)
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

                            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

                        }
                        ocultarPermiso();
                    }

                }
                catch (Exception ex)
                {
                    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");
                }

                cargarComboAcademias();
                cargarComboProfesores();
                cargarComboTipoClase();
                cargarClasesView();
               

                if (Session["mensaje"] != null)
                {
                    mensaje(Session["mensaje"].ToString(), Boolean.Parse(Session["exito"].ToString()));
                    Session["mensaje"] = null;
                }
            }
        }


        protected void ocultarPermiso()
        {
            try
            {
                muetra_clases_invitado.Style["display"] = "none";
                Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                if (sesionActiva.estado == "INGRESO ACEPTADO")
                {
                    

                    //Administración de eventos

                    int permiso = 0;
                    System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_CREACION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        crear_clase.Style["display"] = "none";
                        MostrarEditar = false;
                       
                    }


                     permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_INSCRIPCION'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        
                        MostrarInscribir = false;

                    }


                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'ALUMNO_GRADUAR'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        graduar_alumno.Style["display"] = "none";
                    }

                    permiso = 0;
                    drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_ASISTENCIA'");
                    if (drsAux.Length > 0)
                    {
                        int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    }
                    if (permiso != 1)
                    {
                        asistencia.Style["display"] = "none";
                        listado_asistencia.Style["display"] = "none";
                        asistenciaAnteriores.Style["display"] = "none";
                    }

                    //permiso = 0;
                    //drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_CALENDARIO'");
                    //if (drsAux.Length > 0)
                    //{
                    //    int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                    //}
                    //if (permiso != 1)
                    //{
                    //    calendario.Style["display"] = "none";
                    //}
                  

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" + "', 2000);</script>");

            }
        }

        private void ocultarInvitado()
        {
            muetra_clases_profe_admin.Style["display"] = "none";
            crear_clase.Style["display"] = "none";
            //calendario.Style["display"] = "none";
            asistencia.Style["display"] = "none";
            listado_asistencia.Style["display"] = "none";
            graduar_alumno.Style["display"] = "none";
            crear_clase.Style["display"] = "none";
        }





        private void cargarComboProfesores()
        {
            GestorProfesores gp = new GestorProfesores();
            List<profesor> profesores = gp.ObtenerProfesores();
            foreach(profesor p in profesores)
            {
                p.nombre = p.nombre + " " + p.apellido;
            }
            profesor itemTodos = new profesor()
            {
                id_profesor = 0,
                nombre = "Todos"
            };
            profesores.Insert(0,itemTodos);
            ddl_profesores.DataSource = profesores;
            ddl_profesores.DataValueField = "id_profesor";
            ddl_profesores.DataTextField = "nombre";
            ddl_profesores.DataBind();
        }

        private void cargarComboAcademias()
        {
            GestorAcademias ga = new GestorAcademias();
            List<academia> academias = ga.ObtenerAcademias();
            academia itemTodos = new academia()
            {
                id_academia = 0,
                nombre = "Todas"
            };
            academias.Insert(0, itemTodos);
            ddl_academias.DataSource = academias;
            ddl_academias.DataValueField = "id_academia";
            ddl_academias.DataTextField = "nombre";
            ddl_academias.DataBind();
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

        protected void cargarClasesView()
        {
            string filtroNombre = txt_filtro_nombre.Text.Trim();
            int filtroAcademia = int.Parse(ddl_academias.SelectedValue);
            int filtroProfesor = int.Parse(ddl_profesores.SelectedValue);
            int filtroTipoClase = int.Parse(ddl_tipo_clase.SelectedValue);
            List<ClasesDisponibles> clasesDisponibles = gestorDeClases.ObtenerClasesDisponibles(filtroNombre, filtroProfesor, filtroAcademia, filtroTipoClase);
            clasesDisponibles.ForEach(x =>
            {
                x.MostrarEditar = MostrarEditar;
                x.MostrarInscribir = MostrarInscribir;
            });

            lv_clasesDisponibles.DataSource = clasesDisponibles;
            lv_clasesDisponibles.DataBind();

            lv_clasesDisponibles_invitado.DataSource = clasesDisponibles;
            lv_clasesDisponibles_invitado.DataBind();
        }

        protected void lv_clasesDisponibles_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.CompareTo("inscribir") == 0)
            {
                Session["id_clase"] = id;
                Response.Redirect("InscripcionClase.aspx");
            }
            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                if (gestorDeClases.ClaseTieneInscriptos(id)) lbl_tiene.Visible = true;
            }
            if (e.CommandName.CompareTo("seleccionar") == 0)
            {
                Session["clase"] = id;
                Response.Redirect("CrearClase.aspx");
            }
            if (e.CommandName.CompareTo("ver") == 0)
            {
                Session["clase"] = id;
                Response.Redirect("VerClase.aspx");
            }
            if (e.CommandName.CompareTo("inscriptos") == 0)
            {
                Session["idClase"] = id;
                Response.Redirect("VerInscriptos.aspx");
            }

        }


        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarClasesView();
        }

        protected void lv_clasesDisponibles_invitado_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.CompareTo("ver") == 0)
            {
                Session["clase"] = id;
                Response.Redirect("VerClase.aspx");
            }
        }

        protected void btn_si_Click1(object sender, EventArgs e)
        {
            if (txtIDSeleccionado.Text != "")
            {
                int id = Convert.ToInt32(txtIDSeleccionado.Text);
                gestorDeClases.eliminarClase(id);
                mensaje("Se eliminó la clase correctamente", true);
                cargarClasesView();
            }
        }

        protected void cargarComboTipoClase()
        {
            List<tipo_clase> lista = gestorTipo.ObtenerTipoClase();
            tipo_clase tc = new tipo_clase();
            tc.id_tipo_clase = 0;
            tc.nombre = "Todos";
            lista.Insert(0, tc);

            ddl_tipo_clase.DataSource = lista;
            ddl_tipo_clase.DataValueField = "id_tipo_clase";
            ddl_tipo_clase.DataTextField = "nombre";

            ddl_tipo_clase.DataBind();
        }
    }
}