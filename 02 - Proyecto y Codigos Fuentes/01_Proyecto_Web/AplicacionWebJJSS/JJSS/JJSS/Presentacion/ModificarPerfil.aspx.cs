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
    public partial class ModificarPerfil : System.Web.UI.Page
    {
        private DataTable dtGrupos;
        private GestorPermisos gestorPermisos;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    Sesion sesionActiva = (Sesion)HttpContext.Current.Session["SEGURIDAD_SESION"];
                    if (sesionActiva.estado == "INGRESO ACEPTADO")
                    {
                        int permiso = 0;
                        DataRow[] drsAux = sesionActiva.permisos.Select("perm_clave = 'SEGURIDAD_GRUPOS'");
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
                dg_grupos.AutoGenerateColumns = false;
                gestorPermisos = new GestorPermisos();
                CargarComboGrupos();

            }
        }



        private void CargarComboGrupos()
        {
            List<seguridad_grupo> grupos = gestorPermisos.obtenerListaGrupos();
            ddl_grupos.DataSource = grupos;
            ddl_grupos.DataTextField = "nombre";
            ddl_grupos.DataValueField = "id_grupo";
            ddl_grupos.DataBind();
        }


        protected void btn_agregar_Click(object sender, EventArgs e)
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            dtGrupos = (DataTable)Session["dtGrupos"];
            //dtHorarios = (DataTable)dg_horarios;

            //validacion con los horarios en esa clase
            if (dtGrupos.Select("id = " + ddl_grupos.SelectedIndex).Length > 0)
            {
               
            }

            DataRow drNuevoGrupo = dtGrupos.NewRow();
            drNuevoGrupo["nombre"] = ddl_grupos.SelectedValue;
            drNuevoGrupo["id_grupo"] = ddl_grupos.SelectedIndex;
           
            dtGrupos.Rows.Add(drNuevoGrupo);

            dtGrupos.AcceptChanges();
            DataView dv_horarios = dtGrupos.DefaultView;
            dv_horarios.Sort = "dia asc, hora_desde asc";
            dg_grupos.DataSource = dv_horarios;
            dg_grupos.DataBind();
            Session["dtGrupos"] = dtGrupos;

        }

      


        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            gestorPermisos = new GestorPermisos();
            string sReturn = "";
            Boolean estado = false;

            //validacion con los horarios de otras clases
            DataTable dt = (DataTable)Session["dtHorarios"];
            Boolean bReturn = gestorPermisos.validarDisponibilidadHorario(dt, int.Parse(ddl_ubicacion.SelectedValue.ToString()));
            if (bReturn == false) sReturn = "Hay un horario que se superpone con otra clase";
            else
            {
                if (Session["clase"] != null)
                {
                    int idClase = 0;
                    idClase = int.Parse(Session["clase"].ToString());
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
                int horario = Convert.ToInt32(e.CommandArgument);

                dtGrupos = (DataTable)Session["dtGrupos"];
                dtGrupos.Rows.RemoveAt(horario);
                dg_grupos.DataSource = dtGrupos;
                dg_grupos.DataBind();

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
            Session["clase"] = null;
            dtHorarios = (DataTable)Session["dtHorarios"];
            //if (dtHorarios != null)
            //{
            //    for (int i = 0; i < dtHorarios.Rows.Count; i++)
            //    {
            //        dtHorarios.Rows.RemoveAt(i);
            //    }
            //}

            dg_grupos.DataSource = null;
            dg_grupos.DataBind();
            Session["dtHorarios"] = null;
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
            Response.Redirect("../Presentacion/Inicio.aspx");
        }


    }

}