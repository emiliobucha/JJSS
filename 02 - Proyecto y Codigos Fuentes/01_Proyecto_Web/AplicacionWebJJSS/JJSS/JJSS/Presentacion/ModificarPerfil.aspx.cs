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
        private GestorUsuarios gestorUsuarios;
        private GestorPermisos gestorPermisos;
        private long id_usuario_editar;

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

                if (Session["UsuarioEditar"] == null)
                {
                    Response.Write("<script>window.alert('" + "No hay usuario para editar correctamente".Trim() +
                                   "');</script>" + "<script>window.setTimeout(location.href='" +
                                   "../Presentacion/MenuInicial.aspx" + "', 2000);</script>");

                }
                else
                {
                    id_usuario_editar = int.Parse(Session["UsuarioEditar"].ToString());
                }

                dg_grupos.AutoGenerateColumns = false;
                gestorUsuarios = new GestorUsuarios();
                gestorPermisos = new GestorPermisos();
                CargarComboGrupos();
                CargarGrilla();

            }
        }

        private void CargarGrilla()
        {
            dtGrupos = gestorPermisos.obtenerTablaGrupos(id_usuario_editar);

            Session["dtGrupos"] = dtGrupos;
            dg_grupos.DataSource = dtGrupos;
            dg_grupos.DataBind();

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

            //Validación con los grupos que ya tiene el usuario
            if (dtGrupos.Select("id_grupo = " + ddl_grupos.SelectedValue).Length > 0)
            {

                mensaje("El usuario ya posee ese grupo asignado", false);
                return;
            }

            DataRow drNuevoGrupo = dtGrupos.NewRow();
            drNuevoGrupo["nombre"] = ddl_grupos.SelectedItem.Text;
            drNuevoGrupo["id_grupo"] = ddl_grupos.SelectedValue;

            dtGrupos.Rows.Add(drNuevoGrupo);

            dtGrupos.AcceptChanges();
            dg_grupos.DataSource = dtGrupos;
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
            DataTable dt = (DataTable)Session["dtGrupos"];
            id_usuario_editar = int.Parse(Session["UsuarioEditar"].ToString());
            DataTable gruposViejos = gestorPermisos.obtenerTablaGrupos(id_usuario_editar);

            foreach (DataRow drGrupo in dt.Rows)
            {
                if (gruposViejos.Select("id_grupo = " + drGrupo["id_grupo"]).Length == 0)
                {
                    sReturn = gestorPermisos.AgregarGrupo(id_usuario_editar, (int)drGrupo["id_grupo"]);
                    estado = sReturn == "";
                }

            }
          
            foreach (DataRow drGrupo in gruposViejos.Rows)
            {
                if (dt.Select("id_grupo = " + drGrupo["id_grupo"]).Length == 0)
                {
                    sReturn = gestorPermisos.QuitarGrupo(id_usuario_editar, (int)drGrupo["id_grupo"]);
                    estado = sReturn == "";
                }

            }

            if (estado)
            {
                mensaje("Se han actualizado con exito los grupos al usuario", estado);
                Response.Redirect("../Presentacion/Usuarios.aspx");
            }
            else
            {
                mensaje(sReturn, estado);
            }

        }

        protected void dg_grupos_RowCommand(object sender, GridViewCommandEventArgs e)
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



        protected void limpiar()
        {
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;

            Session["UsuarioEditar"] = null;

            dg_grupos.DataSource = null;
            dg_grupos.DataBind();
            Session["dtGrupos"] = null;
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
            Response.Redirect("../Presentacion/MenuInicial.aspx");
        }


    }

}