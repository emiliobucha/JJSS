using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Negocio;
using JJSS_Entidad;
using System.Data;

namespace JJSS.Presentacion
{
    public partial class Perfil : System.Web.UI.Page
    {
        private GestorUsuarios gestorUsuario;
        private GestorAlumnos gestorAlumnos;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorUsuario = new GestorUsuarios();
            gestorAlumnos = new GestorAlumnos();

            if (!IsPostBack)
            {
                pnlDatos.Visible = true;
                pnl_cambiar_pass.Visible = false;
                CargarDatos();
            }
        }

        protected void CargarDatos()
        {
            GestorSesiones gestorSesion = new GestorSesiones();
            seguridad_usuario alumnoActual = gestorSesion.getActual().usuario;
            int id_usuario = alumnoActual.id_usuario;

            alumno alumnoEncontrado = gestorAlumnos.ObtenerAlumnoPorIdUsuario(id_usuario);
            int dni = alumnoEncontrado.dni;
            string nombre = alumnoEncontrado.nombre;
            string apellido = alumnoEncontrado.apellido;
            
            txt_apellido.Text = apellido;
            txt_nombre.Text = nombre;
            txt_dni.Text = dni.ToString();
            txt_usuario.Text = alumnoActual.login;
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiarDatos();
            Response.Redirect("~/Presentacion/Inicio.aspx");
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            string apellido = txt_apellido.Text;
            string nombre = txt_nombre.Text;

        }

        protected void lnk_cambiar_pass_Click(object sender, EventArgs e)
        {
            pnlDatos.Visible = false;
            pnl_cambiar_pass.Visible = true;
        }

        protected void btn_cambiar_Click(object sender, EventArgs e)
        {
            string claveAnterior = txt_pass_anterior.Text;
            string claveNueva = txt_pass_nueva.Text;
            string login = txt_usuario.Text;
            string sReturn =gestorUsuario.CambiarClave(claveAnterior,claveNueva,login);
            if (sReturn.CompareTo("") == 0)
            {
                mensaje("Se ha realizado el cambio correctamente", true);
                limpiarCambio();
                pnlDatos.Visible = true;
                pnl_cambiar_pass.Visible = false;
            }
            else mensaje(sReturn, false);
            
        }

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

        protected void limpiarDatos()
        {
            txt_apellido.Text = "";
            txt_nombre.Text = "";
            txt_usuario.Text = "";
        }

        protected void limpiarCambio()
        {
            txt_pass_anterior.Text = "";
            txt_pass_nueva.Text = "";
            txt_pass_repetida.Text = "";
        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            pnlDatos.Visible = true;
            pnl_cambiar_pass.Visible = false;
            limpiarCambio();
        }
    }
}