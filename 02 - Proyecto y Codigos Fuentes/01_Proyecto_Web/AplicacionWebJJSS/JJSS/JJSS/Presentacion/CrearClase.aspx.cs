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
                /*Si el id de la clase no esta seleccionada de modificacion*/

                dtHorarios = gestorClases.ObtenerTablaHorarios(0);
                dtHorarios.Columns.Add("dia");
                dg_horarios.DataSource = dtHorarios;
                dg_horarios.DataBind();
                dtHorarios.AcceptChanges();
               
                    Session.Add("dtHorarios", dtHorarios);
            }
        }




        protected void btn_agregar_Click(object sender, EventArgs e)
        {

            
            dtHorarios = (DataTable)Session["dtHorarios"];

            if (dtHorarios.Select("dia = " + ddl_dia.SelectedIndex).Length > 0)
            {
                if (dtHorarios.Select("dia = " + ddl_dia.SelectedIndex + " and hora_desde <= '" + txt_horadesde.Text + "' and hora_hasta > '" + txt_horadesde.Text + "'").Length > 0)
                {
                    return;
                }
                if (dtHorarios.Select("dia = " + ddl_dia.SelectedIndex + " and hora_desde < '" + txt_horahasta.Text + "' and hora_hasta >= '" + txt_horahasta.Text + "'").Length > 0)
                {
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
            dg_horarios.DataSource =dv_horarios ;    
           
            dg_horarios.DataBind();

        }

        private void CargarComboTipos() {
                
        }

        protected void dg_horarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int dia = Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "dia"));
            switch (dia)
            {
                case 0:
                    e.Row.BackColor = System.Drawing.Color.Cyan;
                    break;
                case 1:
                    e.Row.BackColor = System.Drawing.Color.Red;
                    break;
                case 2:
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                    break;
                case 3:
                    e.Row.BackColor = System.Drawing.Color.Green;
                    break;
                case 4:
                    e.Row.BackColor = System.Drawing.Color.Gold;
                    break;
                case 5:
                    e.Row.BackColor = System.Drawing.Color.Gray;
                    break;
                case 6:
                    e.Row.BackColor = System.Drawing.Color.Firebrick;
                    break;
                case 7:
                    e.Row.BackColor = System.Drawing.Color.Lime;
                    break;
            }

          
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            gestorClases = new GestorClases();
            string sReturn = gestorClases.GenerarNuevaClase(1, Double.Parse(txt_precio.Text), (DataTable)Session["dtHorarios"], txt_nombre.Text);

            if (sReturn.CompareTo("") == 0) sReturn = "La Clase se ha creado exitosamente";
            mensaje(sReturn, "CrearClase.aspx");
            //Response.Redirect("Inicio.aspx");
        }

        private void mensaje(string pMensaje, string pRef)
        {
            Response.Write("<script>window.alert('" + pMensaje.Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + pRef + "', 2000);</script>");
        }
    }
}