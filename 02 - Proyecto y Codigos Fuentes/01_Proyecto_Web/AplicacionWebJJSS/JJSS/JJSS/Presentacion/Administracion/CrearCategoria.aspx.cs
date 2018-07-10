using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JJSS_Entidad;
using JJSS_Negocio;
using JJSS_Negocio.Administracion;
using System.Globalization;

namespace JJSS.Administracion
{
    public partial class CrearCategoria : System.Web.UI.Page
    {
        private static GestorCategoria gestorCategorias;
        private static GestorTipoClase gestorTipoClase;
        private static int idCategoria = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gestorCategorias = new GestorCategoria();
                gestorTipoClase = new GestorTipoClase();

                if (!IsPostBack)
                {

                    try
                    {
                        Sesion sesionActiva = (Sesion) HttpContext.Current.Session["SEGURIDAD_SESION"];
                        if (sesionActiva.estado == "INGRESO ACEPTADO")
                        {
                            int permiso = 0;
                            System.Data.DataRow[] drsAux =
                                sesionActiva.permisos.Select("perm_clave = 'CATEGORIA_CREACION'");
                            if (drsAux.Length > 0)
                            {
                                int.TryParse(drsAux[0]["perm_ejecutar"].ToString(), out permiso);
                            }
                            if (permiso != 1)
                            {
                                Response.Write("<script>window.alert('" +
                                               "No se encuentra logueado correctamente o tiene los permisos para estar aquí"
                                                   .Trim() + "');</script>" +
                                               "<script>window.setTimeout(location.href='" + "../Login.aspx" +
                                               "', 2000);</script>");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>window.alert('" + "No se encuentra logueado correctamente".Trim() +
                                       "');</script>" + "<script>window.setTimeout(location.href='" + "../Login.aspx" +
                                       "', 2000);</script>");
                    }



                    CargarComboTipoClase();

                    if (Session["categoria"] != null)
                    {
                        idCategoria = Convert.ToInt32(Session["categoria"]);
                        CargarDatosCategoriaSeleccionada();
                        Session["categoria"] = null;
                    }
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void CargarComboTipoClase()
        {
            List<tipo_clase> tipos = gestorTipoClase.ObtenerTipoClase();
            ddlDisciplina.DataSource = tipos;
            ddlDisciplina.DataTextField = "nombre";
            ddlDisciplina.DataValueField = "id_tipo_clase";
            ddlDisciplina.DataBind();
        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            limpiar();
            Response.Redirect("../Inicio.aspx");
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            if (validar())
            {

                double pesoMin = float.Parse(txtPesoMinimo.Text, CultureInfo.InvariantCulture);
                pesoMin = Math.Round(pesoMin, 2);

                double pesoMax = float.Parse(txtPesoMaximo.Text, CultureInfo.InvariantCulture);
                pesoMax = Math.Round(pesoMax, 2);

                categoria nuevaCategoria = new categoria();
                nuevaCategoria.nombre = txt_nombre.Text;
                nuevaCategoria.peso_desde = pesoMin;
                nuevaCategoria.peso_hasta = pesoMax;
                nuevaCategoria.edad_desde = int.Parse(txtEdadMinima.Text);
                nuevaCategoria.edad_hasta = int.Parse(txtEdadMaxima.Text);

                short? sexo = Convert.ToInt16(rbSexo.SelectedValue);

                nuevaCategoria.sexo = sexo;
                nuevaCategoria.id_tipo_clase = int.Parse(ddlDisciplina.SelectedValue);
                nuevaCategoria.actual = JJSS_Negocio.Constantes.ConstatesBajaLogica.ACTUAL;

                if (idCategoria == 0)
                {//crear
                    String res = gestorCategorias.crearCategoria(nuevaCategoria);
                    if (res.CompareTo("") == 0)
                    {
                        limpiar();
                        Session["mensaje"] = "Se ha creado la categoría exitosamente";
                        Session["exito"] = true;
                        Response.Redirect("AdministrarCategorias.aspx");
                    }
                    else mensaje(res, false);
                }
                else
                {//modificar
                    nuevaCategoria.id_categoria = idCategoria;
                    String res = gestorCategorias.ModificarCategoria(nuevaCategoria);
                    if (res.CompareTo("") == 0)
                    {
                        limpiar();
                        Session["mensaje"] = "Se ha modicado la categoría exitosamente";
                        Session["exito"] = true;
                        Response.Redirect("AdministrarCategorias.aspx");
                    }
                    else mensaje(res, false);
                }
            }

        }

        private Boolean validar()
        {
            float pesoMin = float.Parse(txtPesoMinimo.Text, CultureInfo.InvariantCulture);
            float pesoMax = float.Parse(txtPesoMaximo.Text, CultureInfo.InvariantCulture);
            if (pesoMax <= pesoMin)
            {
                mensaje("El peso mínimo debe ser menor al peso máximo", false);
                return false;
            }
            int edadMin = int.Parse(txtEdadMinima.Text);
            int edadMax = int.Parse(txtEdadMaxima.Text);
            if (edadMax <= edadMin)
            {
                mensaje("La edad mínima debe ser menor a la edad máxima", false);
                return false;
            }
            return true;
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

        private void limpiar()
        {
            txtEdadMaxima.Text = "";
            txtEdadMinima.Text = "";
            txtPesoMaximo.Text = "";
            txtPesoMinimo.Text = "";
            txt_nombre.Text = "";

            ddlDisciplina.SelectedIndex = 0;
            pnl_mensaje_error.Visible = false;
            pnl_mensaje_exito.Visible = false;

        }

        private void CargarDatosCategoriaSeleccionada()
        {
            limpiar();
            categoria categoriaSeleccionada = gestorCategorias.ObtenerCategoriaPorID(idCategoria);
            txt_nombre.Text = categoriaSeleccionada.nombre;
            txtPesoMinimo.Text = categoriaSeleccionada.peso_desde.ToString().Replace(",", ".");
            txtPesoMaximo.Text = categoriaSeleccionada.peso_hasta.ToString().Replace(",", ".");
            txtEdadMinima.Text = categoriaSeleccionada.edad_desde.ToString();
            txtEdadMaxima.Text = categoriaSeleccionada.edad_hasta.ToString();
            ddlDisciplina.SelectedValue = categoriaSeleccionada.id_tipo_clase.ToString();
            rbSexo.SelectedValue = categoriaSeleccionada.sexo.ToString();
        }
    }
}