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
        private static List<categoria> categorias;
        private static List<faja> fajas;
        private static List<DatosParticipanteTorneo> participantes;
        private static torneo torneoSeleccionado;
        private static GestorInscripciones gestorInscripciones;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gestorResultados = new GestorResultados();
                gestorTorneos = new GestorTorneos();
                gestorInscripciones = new GestorInscripciones();
                if (Session["idTorneo"] != null)
                {
                    int idTorneo = int.Parse(Session["idTorneo"].ToString());
                    torneoSeleccionado = gestorTorneos.BuscarTorneoPorID(idTorneo);

                    categoriasConInscriptos = gestorResultados.mostrarCategoriasConInscriptos(idTorneo);
                    categorias = gestorResultados.mostrarCategorias();
                    fajas = gestorResultados.mostrarFajas();
                    cargarCombosCategorias();
                    cargarComboTipoDNI();
                }
                else
                {
                    Response.Redirect("HistoricoTorneos.aspx");
                }
            }
        }

        private void cargarCombosCategorias()
        {
            ddl_categoriasConInscriptos.DataSource = categoriasConInscriptos;
            ddl_categoriasConInscriptos.DataTextField = "nombreParaMostrar";
            ddl_categoriasConInscriptos.DataValueField = "idCategoriaTorneo";
            ddl_categoriasConInscriptos.DataBind();

            ddl_categorias.DataSource = categorias;
            ddl_categorias.DataTextField = "nombre";
            ddl_categorias.DataValueField = "id_categoria";
            ddl_categorias.DataBind();

            ddl_fejas.DataSource = fajas;
            ddl_fejas.DataTextField = "descripcion";
            ddl_fejas.DataValueField = "id_faja";
            ddl_fejas.DataBind();
        }

        private void cargarComboTipoDNI()
        {
            List<tipo_documento> tiposdoc = gestorInscripciones.ObtenerTiposDocumentos();
            ddl_tipo.DataSource = tiposdoc;
            ddl_tipo.DataTextField = "codigo";
            ddl_tipo.DataValueField = "id_tipo_documento";
            ddl_tipo.DataBind();
        }

        private void cargarComboParticipantes()
        {
            DatosParticipanteTorneo dpt = new DatosParticipanteTorneo();
            dpt.participante = "Seleccione un participante";
            dpt.idParticipante = 0;
            participantes.Insert(0, dpt);

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

        protected void btn_mas_Click(object sender, EventArgs e)
        {
            int idFaja = int.Parse(ddl_fejas.SelectedItem.Value);
            int idCategoria = int.Parse(ddl_categorias.SelectedItem.Value);
            CategoriasTorneoResultado categoriaAAgregar = gestorResultados.categoriaAAgregar(idFaja, idCategoria);
            categoriasConInscriptos.Add(categoriaAAgregar);
            cargarCombosCategorias();
        }

        protected void btn_agregar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCategoria = int.Parse(ddl_categoriasConInscriptos.SelectedItem.Value);
                participantes = gestorResultados.mostrarParticipantesDeCategoria(idCategoria, torneoSeleccionado.id_torneo);
                cargarComboParticipantes();
                lbl_nombreCategoria.Text = ddl_categoriasConInscriptos.SelectedItem.Text;
                pnl_mensaje_error.Visible = false;
                pnl_mensaje_exito.Visible = false;
                pnl_participantes.Visible = true;
                validarParticipantes();
            }
            catch (NullReferenceException ex)
            {
                mensaje("Debe seleccionar una categoría", false);
            }

        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Presentacion/Torneos/VerTorneo.aspx");
        }

        protected void btn_agregarResultado_Click(object sender, EventArgs e)
        {
            try
            {
                int idCategoria = int.Parse(ddl_categoriasConInscriptos.SelectedItem.Value);
                int idPrimerPuesto = int.Parse(ddl_1.SelectedItem.Value);
                int idSegundoPuesto = int.Parse(ddl_2.SelectedItem.Value);
                int? idTercerPuesto1 = int.Parse(ddl_3_1.SelectedItem.Value);
                int? idTercerPuesto2 = int.Parse(ddl_3_2.SelectedItem.Value);
                if (idTercerPuesto1 == 0) idTercerPuesto1 = null;
                if (idTercerPuesto2 == 0) idTercerPuesto2 = null;

                if (validarPuestos())
                {
                    string res = gestorResultados.cargarResultado(torneoSeleccionado.id_torneo, idCategoria, idPrimerPuesto, idSegundoPuesto, idTercerPuesto1, idTercerPuesto2);
                    if (res.CompareTo("") == 0)
                    {
                        mensaje("El resultado se cargó correctamente", true);
                    }
                    else
                    {
                        mensaje(res, false);
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                mensaje("Debe seleccionar un participante", false);
            }

        }

        private Boolean validarPuestos()
        {
            int idPrimerPuesto = int.Parse(ddl_1.SelectedItem.Value);
            int idSegundoPuesto = int.Parse(ddl_2.SelectedItem.Value);
            int idTercerPuesto1 = int.Parse(ddl_3_1.SelectedItem.Value);
            int idTercerPuesto2 = int.Parse(ddl_3_2.SelectedItem.Value);
            if (participantes.Count - 1 == 2)
            {
                if (idPrimerPuesto == idSegundoPuesto)
                {
                    mensaje("Los participantes no se pueden repetir en distintos puestos", false);
                    return false;
                }
                else if (idPrimerPuesto == 0 || idSegundoPuesto == 0)
                {
                    mensaje("Debe seleccionar un participante para cada puesto", false);
                    return false;
                }
            }
            else if (participantes.Count -1 == 3)
            {
                if (idPrimerPuesto == idSegundoPuesto || idPrimerPuesto == idTercerPuesto1 
                    || idSegundoPuesto == idTercerPuesto1)
                {
                    mensaje("Los participantes no se pueden repetir en distintos puestos", false);
                    return false;
                }
                else if (idPrimerPuesto == 0 || idSegundoPuesto == 0 || idTercerPuesto1 == 0)
                {
                    mensaje("Debe seleccionar un participante para cada puesto", false);
                    return false;
                }
            }
            else if (participantes.Count - 1 >= 4)
            {
                if (idPrimerPuesto == idSegundoPuesto || idPrimerPuesto == idTercerPuesto1 || idPrimerPuesto == idTercerPuesto2
                    || idSegundoPuesto == idTercerPuesto1 || idSegundoPuesto == idTercerPuesto2
                    || idTercerPuesto2 == idTercerPuesto1)
                {
                    mensaje("Los participantes no se pueden repetir en distintos puestos", false);
                    return false;
                }
                else if (idPrimerPuesto == 0 || idSegundoPuesto == 0 || idTercerPuesto1 == 0 || idTercerPuesto2 == 0)
                {
                    mensaje("Debe seleccionar un participante para cada puesto", false);
                    return false;
                }
            }

            //TODO terminar la validacion
            return true;
        }
        
        private void validarParticipantes()
        {
            if (participantes.Count - 1 == 0)
            {
                ddl_1.Enabled = false;
                ddl_2.Enabled = false;
                ddl_3_1.Enabled = false;
                ddl_3_2.Enabled = false;
                btn_agregarResultado.Enabled = false;
            }
            else if (participantes.Count - 1 == 1)
            {
                ddl_1.Enabled = false;
                ddl_2.Enabled = false;
                ddl_3_1.Enabled = false;
                ddl_3_2.Enabled = false;
                btn_agregarResultado.Enabled = false;
            }
            else if (participantes.Count - 1 == 2)
            {
                ddl_1.Enabled = true;
                ddl_2.Enabled = true;
                ddl_3_1.Enabled = false;
                ddl_3_2.Enabled = false;
                btn_agregarResultado.Enabled = true;
            }
            else if (participantes.Count - 1 == 3)
            {
                ddl_1.Enabled = true;
                ddl_2.Enabled = true;
                ddl_3_1.Enabled = true;
                ddl_3_2.Enabled = false;
                btn_agregarResultado.Enabled = true;
            }
            else
            {
                ddl_1.Enabled = true;
                ddl_2.Enabled = true;
                ddl_3_1.Enabled = true;
                ddl_3_2.Enabled = true;
                btn_agregarResultado.Enabled = true;
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

        protected void btn_aceptar_participante_Click(object sender, EventArgs e)
        {
            int idCategoria = int.Parse(ddl_categoriasConInscriptos.SelectedItem.Value);
            string nombre = txt_nombre.Text;
            string apellido = txt_apellido.Text;
            var dni = txt_dni.Text;

            short sexo = 0;
            if (rbSexo.SelectedIndex == 0) sexo = JJSS_Negocio.Constantes.ContantesSexo.FEMENINO;
            if (rbSexo.SelectedIndex == 1) sexo = JJSS_Negocio.Constantes.ContantesSexo.MASCULINO;

            GestorInscripciones gi = new GestorInscripciones();


            int idTipo = 0;
            int.TryParse(ddl_tipo.SelectedValue, out idTipo);

            string res = gi.InscribirATorneo(torneoSeleccionado.id_torneo, nombre, apellido, idTipo, dni, idCategoria, sexo);
            if (res.CompareTo("") == 0)
            {
                participantes = gestorResultados.mostrarParticipantesDeCategoria(idCategoria, torneoSeleccionado.id_torneo);
                cargarComboParticipantes();
                validarParticipantes();
                limpiarModal();
            }
            else
            {
                mensaje(res, false);
            }
        }

        private void limpiarModal()
        {
            txt_apellido.Text = "";
            txt_dni.Text = "";
            txt_nombre.Text = "";
            ddl_tipo.SelectedIndex = -1;
        }
    }
}