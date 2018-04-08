using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Resultados;

namespace JJSS.Presentacion
{
    public partial class CargarResultadosTorneo : System.Web.UI.Page
    {
        private static GestorResultados gestorResultados;
        private static GestorTorneos gestorTorneos;
        private static List<CategoriasTorneoResultado> categoriasConInscriptos;
        private static List<CategoriasTorneoResultado> categoriasSinInscriptos;
        private List<DatosParticipanteTorneo> participantes;
        private static torneo torneoSeleccionado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gestorResultados = new GestorResultados();
                gestorTorneos = new GestorTorneos();
                if (Session["idTorneo"] != null)
                {
                    int idTorneo = int.Parse(Session["idTorneo"].ToString());
                    torneoSeleccionado = gestorTorneos.BuscarTorneoPorID(idTorneo);

                    categoriasConInscriptos = gestorResultados.mostrarCategoriasConInscriptos(idTorneo);
                    categoriasSinInscriptos = gestorResultados.mostrarCategoriasSinInscriptos(idTorneo);
                    cargarCombosCategorias();
                }
                else
                {
                    Response.Redirect("../Presentacion/HistoricoTorneos.aspx");
                }
            }
        }

        private void cargarCombosCategorias()
        {
            ddl_categoriasConInscriptos.DataSource = categoriasConInscriptos;
            ddl_categoriasConInscriptos.DataTextField = "nombreParaMostrar";
            ddl_categoriasConInscriptos.DataValueField = "idCategoriaTorneo";
            ddl_categoriasConInscriptos.DataBind();

            ddl_categoriasSinInscriptos.DataSource = categoriasSinInscriptos;
            ddl_categoriasSinInscriptos.DataTextField = "nombreParaMostrar";
            ddl_categoriasSinInscriptos.DataValueField = "idCategoriaTorneo";
            ddl_categoriasSinInscriptos.DataBind();
        }

        private void cargarComboParticipantes()
        {
            ddl_1.DataSource = participantes;
            ddl_1.DataTextField = "participante";
            ddl_1.DataValueField = "idParticipante";
            ddl_1.DataBind();

            ddl_2.DataSource = participantes;
            ddl_2.DataTextField = "participante";
            ddl_2.DataValueField = "idParticipante";
            ddl_2.DataBind();

            ddl_3_1.DataSource = participantes;
            ddl_3_1.DataTextField = "participante";
            ddl_3_1.DataValueField = "idParticipante";
            ddl_3_1.DataBind();

            ddl_3_2.DataSource = participantes;
            ddl_3_2.DataTextField = "participante";
            ddl_3_2.DataValueField = "idParticipante";
            ddl_3_2.DataBind();
        }

        protected void btn_menos_Click(object sender, EventArgs e)
        {

        }

        protected void btn_mas_Click(object sender, EventArgs e)
        {

        }

        protected void btn_agregar_Click(object sender, EventArgs e)
        {
            int idCategoria = int.Parse(ddl_categoriasConInscriptos.SelectedItem.Value);
            participantes = gestorResultados.mostrarParticipantesDeCategoria(idCategoria, torneoSeleccionado.id_torneo);
            cargarComboParticipantes();
            lbl_nombreCategoria.Text = ddl_categoriasConInscriptos.SelectedItem.Text;
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;
            pnl_participantes.Visible = true;
        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Presentacion/VerTorneo.aspx");
        }

        protected void btn_agregarResultado_Click(object sender, EventArgs e)
        {
            try
            {
                int idCategoria = int.Parse(ddl_categoriasConInscriptos.SelectedItem.Value);
                int idPrimerPuesto = int.Parse(ddl_1.SelectedItem.Value);
                int idSegundoPuesto = int.Parse(ddl_2.SelectedItem.Value);
                int idTercerPuesto1 = int.Parse(ddl_3_1.SelectedItem.Value);
                int idTercerPuesto2 = int.Parse(ddl_3_2.SelectedItem.Value);
                string res = gestorResultados.cargarResultado(torneoSeleccionado.id_torneo, idCategoria, idPrimerPuesto, idSegundoPuesto, idTercerPuesto1, idTercerPuesto2);
                if (res.CompareTo("") == 0)
                {
                    mensaje("El resultado se cargó correctamente", true);
                }
                else
                {
                    mensaje(res, false);
                }
            }catch (NullReferenceException ex)
            {
                mensaje("Debe seleccionar un participante", false);
            }
            
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

        protected void btn_masParticipante_Click(object sender, EventArgs e)
        {
            
        }

        protected void btn_aceptar_participante_Click(object sender, EventArgs e)
        {
            int idCategoria = int.Parse(ddl_categoriasConInscriptos.SelectedItem.Value);
            string nombre = txt_nombre.Text;
            string apellido = txt_apellido.Text;
            int dni = int.Parse(txt_dni.Text);
            //TODO nacionalidad y tipo dni
            GestorInscripciones gi = new GestorInscripciones();
            string res = gi.InscribirATorneo(torneoSeleccionado.id_torneo, nombre, apellido, dni, idCategoria);
            if (res.CompareTo("") == 0)
            {
                participantes = gestorResultados.mostrarParticipantesDeCategoria(idCategoria, torneoSeleccionado.id_torneo);
                cargarComboParticipantes();
            }
            else
            {
                mensaje(res, false);
            }
        }
    }
}