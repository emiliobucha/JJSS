using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;
namespace JJSS.Presentacion
{
    public partial class CrearClase : System.Web.UI.Page
    {
        private DataTable dtHorarios;
        private GestorClases gestorClases;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dg_horarios.AutoGenerateColumns = false;
                gestorClases = new GestorClases();
                
                cargarDatosClase();
            }
        }

        protected void cargarDatosClase()
        {
            /*Si el id de la clase no esta seleccionada de modificacion*/
            int idClase = 0;

            if (Session["clase"] != null)
            {
                idClase = int.Parse(Session["clase"].ToString());
                txt_nombre.Text = gestorClases.ObtenerClasePorId(idClase).nombre.ToString();
                txt_precio.Text = gestorClases.ObtenerClasePorId(idClase).precio.ToString();
                txt_nombre.Enabled = false;
            }
            else txt_nombre.Enabled = true;
            dtHorarios = gestorClases.ObtenerTablaHorarios(idClase);
            dtHorarios.Columns.Add("dia");
            //asignarDia();
            dg_horarios.DataSource = dtHorarios;
            dg_horarios.DataBind();
            dtHorarios.AcceptChanges();

            Session.Add("dtHorarios", dtHorarios);
        }

        protected void asignarDia()
        {
            DataTable auxDt = new DataTable();
            
            for (int i = 0; i < dtHorarios.Rows.Count; i++)
            {
                DataRow dr = dtHorarios.Rows[i];
                if (dr["nombre_dia"].ToString().CompareTo("Lunes") == 0) dr["dia"] = 0;
                if (dr["nombre_dia"].ToString().CompareTo("Martes") == 0) dr["dia"] =1;
                if (dr["nombre_dia"].ToString().CompareTo("Miercoles") == 0) dr["dia"] = 2;
                if (dr["nombre_dia"].ToString().CompareTo("Jueves") == 0) dr["dia"] = 3;
                if (dr["nombre_dia"].ToString().CompareTo("Viernes") == 0) dr["dia"] = 4;
                if (dr["nombre_dia"].ToString().CompareTo("Sabado") == 0) dr["dia"] = 5;
                if (dr["nombre_dia"].ToString().CompareTo("Domingo") == 0) dr["dia"] = 6;
                //dtHorarios.Rows.RemoveAt(i);
                auxDt.Rows.Add(dr);
            }
            dtHorarios = auxDt;

        }

        protected void btn_agregar_Click(object sender, EventArgs e)
        {


            dtHorarios = (DataTable)Session["dtHorarios"];

            if (dtHorarios.Select("dia = " + ddl_dia.SelectedIndex).Length > 0)
            {
                if (dtHorarios.Select("dia = " + ddl_dia.SelectedIndex + " and hora_desde <= '" + txt_horadesde.Text + "' and hora_hasta > '" + txt_horadesde.Text + "'").Length > 0)
                {
                    mensaje("Ya hay una clase en ese horario");
                    return;
                }
                if (dtHorarios.Select("dia = " + ddl_dia.SelectedIndex + " and hora_desde < '" + txt_horahasta.Text + "' and hora_hasta >= '" + txt_horahasta.Text + "'").Length > 0)
                {
                    mensaje("Ya hay una clase en ese horario");
                    return;
                }
            }

            DataRow drNuevoHorario = dtHorarios.NewRow();
            drNuevoHorario["nombre_dia"] = ddl_dia.SelectedValue;
            drNuevoHorario["dia"] = ddl_dia.SelectedIndex;
            drNuevoHorario["hora_desde"] = txt_horadesde.Text;
            drNuevoHorario["hora_hasta"] = txt_horahasta.Text;
            dtHorarios.Rows.Add(drNuevoHorario);

            dtHorarios.AcceptChanges();
            DataView dv_horarios = dtHorarios.DefaultView;
            dv_horarios.Sort = "dia asc, hora_desde asc";
            dg_horarios.DataSource = dv_horarios;
            dg_horarios.DataBind();

        }

        private void CargarComboTipos()
        {

        }

        protected void dg_horarios_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            gestorClases = new GestorClases();
            string sReturn = "";
            if (Session["clase"] != null)
            {
                int idClase = 0;
                idClase = int.Parse(Session["clase"].ToString());
                sReturn = gestorClases.modificarClase(idClase, (DataTable)Session["dtHorarios"], double.Parse(txt_precio.Text));
                if (sReturn.CompareTo("") == 0) sReturn = "La clase se actualizó correctamente";
            }
            else
            {
                sReturn = gestorClases.GenerarNuevaClase(1, Double.Parse(txt_precio.Text), (DataTable)Session["dtHorarios"], txt_nombre.Text);
                if (sReturn.CompareTo("") == 0) sReturn = "La clase se ha creado exitosamente";
            }
            
            mensaje(sReturn);
            
        }

        private void mensaje(string pMensaje)
        {
            Response.Write("<script>window.alert('" + pMensaje.Trim() + "');</script>");
        }

        protected void dg_horarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("Eliminar") == 0)
            {
                int horario = Convert.ToInt32(e.CommandArgument);
                
                dtHorarios = (DataTable)Session["dtHorarios"];
                dtHorarios.Rows.RemoveAt(horario);
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
            txt_horadesde.Text = "";
            txt_horahasta.Text = "";
            txt_nombre.Text = "";
            txt_precio.Text = "";
            txt_nombre.Enabled = true;
            Session["clase"] = null;
            dtHorarios = (DataTable)Session["dtHorarios"];
            for (int i=0;i< dtHorarios.Rows.Count; i++)
            {
                dtHorarios.Rows.RemoveAt(i);
            }
            dg_horarios.DataSource = dtHorarios;
            dg_horarios.DataBind();
            Session["dtHorarios"] = null;
        }
    }
}