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
        private torneo torneoSeleccionado;
        private int claseSeleccionada;
        private int? idAlumno=null;

        protected void Page_Load(object sender, EventArgs e)
        {

            gestorDeTorneos = new GestorTorneos();
            gestorInscripciones = new GestorInscripciones();
            gestorDeClases = new GestorClases();
            if (!IsPostBack)
            {
                cargarTorneosExportarListado();
                cargarClases();
                cargarTorneosAbiertos();
                CargarComboFajas();
                pnl_Inscripcion.Visible = false;
                pnl_dni.Visible = true;
                limpiar(true);
            }
            


            //   this.btn_confirmar_dni.ServerClick += MetodoClick;


            //ClientScript.GetPostBackEventReference(this, string.Empty);
            //if (Request.Form["__EVENTTARGET"] == "btnGenerarListado_Click")
            //{
            //    //llamamos el metodo que queremos ejecutar, en este caso el evento onclick del boton Button2
            //    btnGenerarListado_Click(this, new EventArgs());
            //}
            btn_aceptar.Visible = false;
        }
        

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

        protected void CargarComboFajas()
        {
            List<faja> fajas = gestorInscripciones.ObtenerFajas();
            ddl_fajas.DataSource = fajas;
            ddl_fajas.DataTextField = "color";
            ddl_fajas.DataValueField = "id_faja";
            ddl_fajas.DataBind();
        }

        private void limpiar(Boolean limpiaTodo)
        {
            txt_apellido.Text = "";
            txt_edad.Text = "";
            txt_nombre.Text = "";
            txt_peso.Text = "";
            //ya se que no usamos el index pero solo tiene que setearlo en el primer valor que haya en el combo
            ddl_fajas.SelectedIndex = 1;

            if (limpiaTodo == true)
            {
                txtDni.Text = "";
            }
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {

            //+Ver Bien el SelectedValue del combo

            int idTorneo = 1;

            //solo para invitados
            string nombre = txt_nombre.Text;
            string apellido = txt_apellido.Text;
            float peso = float.Parse(txt_peso.Text);
            int edad = int.Parse(txt_edad.Text);
            int dni = int.Parse(txtDni.Text);


            int idFaja = 0;
            int.TryParse(ddl_fajas.SelectedValue, out idFaja);

            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = 0; //Femenino
            if (rbSexo.SelectedIndex == 1) sexo = 1; //Masculino

            //para todos
            gestorInscripciones.InscribirATorneo(idTorneo, nombre, apellido, peso, edad, idFaja, sexo, dni, idAlumno);
            limpiar(true);

        }       


        protected void btnBuscarDni_Click(object sender, EventArgs e)
        {
            txt_apellido.ReadOnly = false;
            txt_nombre.ReadOnly = false;
            txt_edad.ReadOnly = false;
            ddl_fajas.Enabled = true;
            rbSexo.Enabled = true;

            limpiar(false);
            pnl_Inscripcion.Visible = true;
            participante participanteEncontrado = gestorInscripciones.ObtenerParticipanteporDNI(int.Parse(txtDni.Text));

            //Partipante ya estaba inscripto con ese dni
            if (participanteEncontrado != null) return;

            alumno alumnoEncontrado = gestorInscripciones.ObtenerAlumnoPorDNI(int.Parse(txtDni.Text));
            if (alumnoEncontrado != null)
            {
                //Completa los campos con los datos del alumno, asi luego cuando se va a inscribir, al participante ya le manda los datos y no hay que modificar el metodo de carga de participantes
                txt_apellido.ReadOnly = true;
                txt_nombre.ReadOnly = true;
                txt_edad.ReadOnly = true;
                ddl_fajas.Enabled = false;
                rbSexo.Enabled = false;

                txt_apellido.Text = alumnoEncontrado.apellido;
                txt_nombre.Text = alumnoEncontrado.nombre;

                txt_edad.Text = calcularEdad(alumnoEncontrado.fecha_nacimiento);

                ddl_fajas.SelectedValue = alumnoEncontrado.id_faja.ToString();

                if (alumnoEncontrado.sexo == 0) rbSexo.SelectedIndex = 0;
                if (alumnoEncontrado.sexo == 1) rbSexo.SelectedIndex = 1;

                idAlumno = alumnoEncontrado.id_alumno;
            }
            btn_aceptar.Visible = true;

        }

        //protected void btn_confirmar_dni_Click(object sender, EventArgs e)
        //{
        //    txt_apellido.ReadOnly = false;
        //    txt_nombre.ReadOnly = false;
        //    txt_edad.ReadOnly = false;
        //    ddl_fajas.Enabled = true;
        //    rbSexo.Enabled = true;

        //    pnl_Inscripcion.Visible = true;
        //    participante participanteEncontrado = gestorInscripciones.ObtenerParticipanteporDNI(int.Parse(txt_dni.Text));

        //    //Partipante ya estaba inscripto con ese dni
        //    if (participanteEncontrado != null) return;

        //    alumno alumnoEncontrado = gestorInscripciones.ObtenerAlumnoPorDNI(int.Parse(txt_dni.Text));
        //    if (alumnoEncontrado != null)
        //    {
        //        //Completa los campos con los datos del alumno, asi luego cuando se va a inscribir, al participante ya le manda los datos y no hay que modificar el metodo de carga de participantes
        //        txt_apellido.ReadOnly = true;
        //        txt_nombre.ReadOnly = true;
        //        txt_edad.ReadOnly = true;
        //        ddl_fajas.Enabled = false;
        //        rbSexo.Enabled = false;

        //        txt_apellido.Text = alumnoEncontrado.apellido;
        //        txt_nombre.Text = alumnoEncontrado.nombre;

        //        DateTime fechaNac = Convert.ToDateTime(alumnoEncontrado.fecha_nacimiento);
        //        int edad = DateTime.Today.Year - fechaNac.Year;
        //        txt_edad.Text = edad.ToString();

        //        ddl_fajas.SelectedValue = alumnoEncontrado.id_faja.ToString();

        //        if (alumnoEncontrado.sexo == 0) rbSexo.SelectedIndex = 0;
        //        if (alumnoEncontrado.sexo == 1) rbSexo.SelectedIndex = 1;

        //        idAlumno = alumnoEncontrado.id_alumno;
        //       // ClientScript.RegisterStartupScript(this.GetType(), "myScript", "mostraPanelInscripcion();", false);
        //    }
        //}

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
                Response.Write("<script>window.alert('" + "No se encuentran alumnos inscriptos a ese torneo".Trim() + "');</script>" + "<script>window.setTimeout(location.href='" + "Inicio.aspx"  + "', 2000);</script>");
            }
        }

        protected void cargarTorneosAbiertos()
        {
            gv_torneosAbiertos.DataSource = gestorDeTorneos.ObtenerTorneos();
            gv_torneosAbiertos.DataBind();
           
        }

        protected void gv_torneosAbiertos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gv_torneosAbiertos.Rows[index];

            int id = Convert.ToInt32(row.Cells[0].Text);


                // int id = (int)Convert.ToInt64(gv_torneosAbiertos.SelectedRow.Cells[1].Text);
                //int id = (int)gv_torneosAbiertos.SelectedDataKey.Value;
               
           // int id = (int)this.gv_torneosAbiertos.SelectedValue;

            Session["torneoSeleccionado"] = id;         

            Response.Redirect("~/Presentacion/InscripcionTorneo.aspx");

        }
        protected void cargarClases()
        {
            gv_clasesDisponibles.DataSource = gestorDeClases.ObtenerClases();
            gv_clasesDisponibles.DataBind();
        }

        protected void registrarAlumno_Click(object sender, EventArgs e)
        {
            Session["alumnos"] = "Registrar";
            Response.Redirect("RegistrarAlumno.aspx");
        }

        protected void administrarAlumnos_Click(object sender, EventArgs e)
        {
            Session["alumnos"] = "Administrar";
            Response.Redirect("RegistrarAlumno.aspx");
        }

        protected void gv_clasesDisponibles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gv_clasesDisponibles.Rows[index];

            int id = Convert.ToInt32(row.Cells[0].Text);
           
            claseSeleccionada = id;
            //Response.Redirect("~/Presentacion/InscripcionTorneo.aspx");
             
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup();", true);
            // ClientScript.RegisterStartupScript(GetType(), "alert", "ShowPopup();", true);
        }

        protected void cargarTorneosExportarListado()
        {
            List<torneo> torneosExportar = gestorDeTorneos.ObtenerTorneosAbiertosCerrados();
            ddl_torneoExportarListado.DataSource = torneosExportar;
            ddl_torneoExportarListado.DataTextField = "nombre";
            ddl_torneoExportarListado.DataValueField = "id_torneo";
            ddl_torneoExportarListado.DataBind();
        }

        protected void btn_acpetarTorneoExportarLista_Click(object sender, EventArgs e)
        {
            btnGenerarListado();
        }
    }
}