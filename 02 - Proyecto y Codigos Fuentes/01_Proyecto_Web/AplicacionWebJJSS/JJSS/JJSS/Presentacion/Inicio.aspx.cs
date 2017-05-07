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
        private torneo torneoSeleccionado;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorInscripciones = new GestorInscripciones();
            if (!IsPostBack)
            {
                CargarComboFajas();
            }
            gestorDeTorneos = new GestorTorneos();
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void CargarComboFajas()
        {
            List<faja> fajas = gestorInscripciones.ObtenerFajas();            
            ddl_fajas.DataSource = fajas;
            ddl_fajas.DataTextField = "color";
            ddl_fajas.DataValueField = "id_faja";
            ddl_fajas.DataBind();
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {

            //+Ver Bien el SelectedValue del combo
            int idTorneo = 0;
            int.TryParse(ddl_torneos.SelectedValue, out idTorneo);

            string nombre = txt_nombre.Text;
            string apellido = txt_apellido.Text;
            float peso = float.Parse(txt_peso.Text);
            int edad = int.Parse(txt_edad.Text);

            int idFaja = 0;
            int.TryParse(ddl_fajas.SelectedValue, out idFaja);

            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = 0; //Femenino
            if (rbSexo.SelectedIndex == 1) sexo = 1; //Masculino
            gestorInscripciones.InscribirATorneo(idTorneo, nombre, apellido, peso, edad, idFaja, sexo);

        }
    }
}