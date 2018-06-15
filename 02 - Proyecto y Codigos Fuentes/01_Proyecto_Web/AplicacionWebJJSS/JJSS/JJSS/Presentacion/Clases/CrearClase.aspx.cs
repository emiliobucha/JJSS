using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;
using JJSS_Entidad;

namespace JJSS.Presentacion
{
    public partial class CrearClase : System.Web.UI.Page
    {
        private DataTable dtHorarios;
        private GestorClases gestorClases;
        private int idClase;

        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {
                //try
                //{
                //    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                //    if (sesionActiva.estado == "INGRESO ACEPTADO")
                //    {
                //        int permiso = 0;
                //        System.Data.DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'CLASE_CREACION'");
                //        if (drsAux.Length > 0)
                //        {
                //            int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);

                //        }
                //        if (permiso != 1)
                //        {
                //            Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "../Presentacion/Login.aspx" + "', 2000);</script>");

                //}
                dg_horarios.AutoGenerateColumns = false;
                gestorClases = new GestorClases();
                CargarComboTipos();
                CargarComboUbicacion();
                cargarDatosClase();
                CargarComboProfes();
                txt_horadesde.Text = "00:00";
                txt_horahasta.Text = "01:00";
            }
        }


        protected void cargarDatosClase()
        {
            /*Si el id de la clase no esta seleccionada de modificacion*/
            int idClase = 0;

            if (Session["clase"] != null)
            {
                idClase = int.Parse(Session["clase"].ToString());
                clase claseSeleccionada = gestorClases.ObtenerClasePorId(idClase);
                txt_nombre.Text = claseSeleccionada.nombre.ToString();
                txt_precio.Text = claseSeleccionada.precio.ToString();
                ddl_tipo_clase.SelectedValue = claseSeleccionada.id_tipo_clase.ToString();
                ddl_ubicacion.SelectedValue = claseSeleccionada.id_ubicacion.ToString();
                ddl_profesor.SelectedValue = claseSeleccionada.id_profe.ToString();
                txt_nombre.Enabled = false;
                ddl_tipo_clase.Enabled = false;
                ddl_ubicacion.Enabled = false;
                idClase = Convert.ToInt32(Session["clase"]);
                Session["clase"] = null;
            }
            else {
                limpiar();
                idClase = 0;
            }
            dtHorarios = gestorClases.ObtenerTablaHorarios(idClase);
            dtHorarios.Columns.Add("id");
            for (int i = 0; i < dtHorarios.Rows.Count; i++)
            {
                dtHorarios.Rows[i]["id"] = i;
            }
            dtHorarios.AcceptChanges();

            DataView dv_horarios = dtHorarios.DefaultView;
            dv_horarios.Sort = "dia asc, hora_desde asc";
            dg_horarios.DataSource = dv_horarios;
            dg_horarios.DataBind();

            //Session.Add("dtHorarios", dtHorarios);
            Session["dtHorarios"] = dtHorarios;
        }

        protected void btn_agregar_Click(object sender, EventArgs e)
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            dtHorarios = (DataTable)Session["dtHorarios"];
            //dtHorarios = (DataTable)dg_horarios;

            //validacion con los horarios en esa clase
            if (dtHorarios.Select("dia = " + ddl_dia.SelectedIndex).Length > 0)
            {
                if (dtHorarios.Select("dia = " + ddl_dia.SelectedIndex + " and hora_desde <= '" + txt_horadesde.Text + "' and hora_hasta > '" + txt_horadesde.Text + "'").Length > 0)
                {
                    mensaje("Ya hay una clase en ese horario", false);
                    return;
                }
                if (dtHorarios.Select("dia = " + ddl_dia.SelectedIndex + " and hora_desde < '" + txt_horahasta.Text + "' and hora_hasta >= '" + txt_horahasta.Text + "'").Length > 0)
                {
                    mensaje("Ya hay una clase en ese horario", false);
                    return;
                }
            }

            DataRow drNuevoHorario = dtHorarios.NewRow();
            drNuevoHorario["nombre_dia"] = ddl_dia.SelectedValue;
            drNuevoHorario["dia"] = ddl_dia.SelectedIndex;
            drNuevoHorario["hora_desde"] = txt_horadesde.Text;
            drNuevoHorario["hora_hasta"] = txt_horahasta.Text;
            drNuevoHorario["id"] = dtHorarios.Rows.Count;
            dtHorarios.Rows.Add(drNuevoHorario);

            dtHorarios.AcceptChanges();
            DataView dv_horarios = dtHorarios.DefaultView;
            dv_horarios.Sort = "dia asc, hora_desde asc";
            dg_horarios.DataSource = dv_horarios;
            dg_horarios.DataBind();
            Session["dtHorarios"] = dtHorarios;

        }

        private void CargarComboTipos()
        {
            List<tipo_clase> tipo = gestorClases.ObtenerTipoClases();
            tipo_clase primerElemento = new tipo_clase()
            {
                nombre = "Seleccione un tipo de clase",
                id_tipo_clase = 0
            };
            tipo.Insert(0, primerElemento);
            ddl_tipo_clase.DataSource = tipo;
            ddl_tipo_clase.DataTextField = "nombre";
            ddl_tipo_clase.DataValueField = "id_tipo_clase";
            ddl_tipo_clase.DataBind();
        }

        private void CargarComboUbicacion()
        {
            List<academia> academias = gestorClases.ObtenerAcademias();
            academia primerElemento = new academia()
            {
                nombre = "Seleccione una academia",
                id_academia = 0,
            };
            academias.Insert(0, primerElemento);
            ddl_ubicacion.DataSource = academias;
            ddl_ubicacion.DataTextField = "nombre";
            ddl_ubicacion.DataValueField = "id_academia";
            ddl_ubicacion.DataBind();
        }

        private void CargarComboProfes()
        {
            List<profesor> profesor = gestorClases.ObtenerProfesores();
            foreach(profesor p in profesor)
            {
                p.nombre = p.nombre + " " + p.apellido;
            }
            profesor primerElemento = new profesor()
            {
                nombre = "Seleccione un profesor",
                id_profesor=0,
            };
            profesor.Insert(0, primerElemento);
            ddl_profesor.DataSource = profesor;
            ddl_profesor.DataTextField = "nombre";
            ddl_profesor.DataValueField = "id_profesor";
            ddl_profesor.DataBind();
        }

        protected void dg_horarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int dia = Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "dia"));
                switch (dia)
                {
                    case 0:
                        e.Row.BackColor = System.Drawing.Color.Lavender;
                        break;
                    case 1:
                        e.Row.BackColor = System.Drawing.Color.PaleTurquoise;
                        break;
                    case 2:
                        e.Row.BackColor = System.Drawing.Color.PowderBlue;
                        break;
                    case 3:
                        e.Row.BackColor = System.Drawing.Color.LightBlue;
                        break;
                    case 4:
                        e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
                        break;
                    case 5:
                        e.Row.BackColor = System.Drawing.Color.Turquoise;
                        break;
                    case 6:
                        e.Row.BackColor = System.Drawing.Color.MediumTurquoise;
                        break;
                    default:
                        e.Row.BackColor = System.Drawing.Color.Black;
                        break;
                }
            }


        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            gestorClases = new GestorClases();
            string sReturn = "";
            Boolean estado = false;
            
            //validacion con los horarios de otras clases
            DataTable dt = (DataTable)Session["dtHorarios"];
            Boolean bReturn = gestorClases.validarDisponibilidadHorario(dt, int.Parse(ddl_ubicacion.SelectedValue.ToString()),idClase);
            if (bReturn == false) sReturn = "Hay un horario que se superpone con otra clase";
            else
            {
                if (idClase != 0)
                {
                    
                    int profe = int.Parse(ddl_profesor.SelectedValue.ToString());
                    sReturn = gestorClases.modificarClase(idClase, (DataTable)Session["dtHorarios"], double.Parse(txt_precio.Text), profe);
                    if (sReturn.CompareTo("") == 0)
                    {
                        estado = true;
                        sReturn = "La clase se actualizó correctamente";
                        limpiar();
                    }
                }
                else
                {
                    int tipo = int.Parse(ddl_tipo_clase.SelectedValue.ToString());
                    int ubicacion = int.Parse(ddl_ubicacion.SelectedValue.ToString());
                    int profe = int.Parse(ddl_profesor.SelectedValue.ToString());
                    sReturn = gestorClases.GenerarNuevaClase(tipo, Double.Parse(txt_precio.Text), (DataTable)Session["dtHorarios"], txt_nombre.Text, ubicacion, profe);
                    if (sReturn.CompareTo("") == 0)
                    {
                        estado = true;
                        sReturn = "La clase se ha creado exitosamente";
                        limpiar();
                    }
                }
            }
            mensaje(sReturn, estado);

        }

        protected void dg_horarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Eliminar") == 0)
            {
                pnl_mensaje_error.Visible = false;
                pnl_mensaje_exito.Visible = false;

                int index= Convert.ToInt32(e.CommandArgument);                
                int id = Convert.ToInt32(dg_horarios.DataKeys[index].Value);

                dtHorarios = (DataTable)Session["dtHorarios"];
                DataRow ItemAEliminar = dtHorarios.Select("id = " + id)[0];
                dtHorarios.Rows.Remove(ItemAEliminar);
                
                dg_horarios.DataSource = dtHorarios;
                dg_horarios.DataBind();

            }
        }

        protected void btn_nueva_clase_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void limpiar()
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            txt_horadesde.Text = "";
            txt_horahasta.Text = "";
            txt_nombre.Text = "";
            txt_precio.Text = "";
            txt_nombre.Enabled = true;
            ddl_ubicacion.Enabled = true;
            ddl_tipo_clase.Enabled = true;
            ddl_profesor.SelectedIndex = 0;
            ddl_tipo_clase.SelectedIndex = 0;
            ddl_ubicacion.SelectedIndex = 0;
            Session["clase"] = null;
            dtHorarios = (DataTable)Session["dtHorarios"];
            if (dtHorarios!=null)dtHorarios.Clear();

            dg_horarios.DataSource = null;
            dg_horarios.DataBind();
            Session["dtHorarios"] = dtHorarios;
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

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("../Presentacion/Inicio.aspx#section_clases");
        }
    }

}