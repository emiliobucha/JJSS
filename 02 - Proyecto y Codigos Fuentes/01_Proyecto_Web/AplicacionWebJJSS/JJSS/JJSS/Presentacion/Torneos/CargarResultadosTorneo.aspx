<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="CargarResultadosTorneo.aspx.cs" Inherits="JJSS.Presentacion.CargarResultadosTorneo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <asp:Panel ID="pnl_mensaje_exito" runat="server" Visible="false">
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <div class="alert alert-success alert-dismissible" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <a class="ui-icon ui-icon-check"></a>
                <strong>
                    <asp:Label ID="lbl_exito" runat="server" Text=""></asp:Label></strong>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnl_mensaje_error" runat="server" Visible="false">
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <div class="alert alert-danger alert-dismissible" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <a class="ui-icon ui-icon-alert"></a>
                <strong>Error! </strong>
                <asp:Label ID="lbl_error" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </asp:Panel>


    <form runat="server">
        <div class="container ">
            <div class="row centered">
                <div class=" col-md-4"></div>
                <div class="col-md-4">
                    <p>&nbsp;</p>
                    <h1>CARGA DE RESULTADOS</h1>

                </div>
            </div>
            <div>
                <p>&nbsp;</p>
            </div>
            <div class="row centered">
                <h5>* Si a una categoría se le cargó resultados y se vuelven a cargar, los resultados se sobreescribirán.</h5>
            </div>
            <div>
                <p>&nbsp;</p>
            </div>
            <asp:Panel ID="pnl_categorias" CssClass="panel panel-default" runat="server">
                <div class="row centered center-block">
                    <div class="col-md-3">
                        <label class="pull-left 4">Categorías disponibles</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddl_categoriasConInscriptos" runat="server" class="caja2 pull-right"></asp:DropDownList>
                    </div>
                </div>
                <div>
                    <p>&nbsp;</p>
                </div>
                <div class="row centered center-block">
                    <div class="col-md-2">
                        <label class="pull-left 4">Otras categorías</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddl_categorias" runat="server" CssClass="caja2 pull-right"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddl_fejas" runat="server" CssClass="caja2 pull-right"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btn_mas" class="btn btn-outline-dark" runat="server" Text="Agregar categoría" formnovalidate="true" CausesValidation="false" OnClick="btn_mas_Click" />
                    </div>
                </div>
                <div>
                    <p>&nbsp;</p>
                </div>
                <div>
                    <asp:Button ID="btn_agregar" class="btn btn-outline-dark pull-right" runat="server" Text="Agregar resultado" formnovalidate="true" CausesValidation="false" OnClick="btn_agregar_Click" />
                </div>
            </asp:Panel>
            <p>&nbsp;</p>
            <asp:Panel ID="pnl_participantes" CssClass="panel panel-default" runat="server" Visible="false">
                <p>&nbsp;</p>
                <div class="row centered">
                    <h2>
                        <asp:Label ID="lbl_nombreCategoria" runat="server" Text="Sin Categoría"></asp:Label>
                    </h2>
                </div>
                <div>
                    <div>
                        <div class="centered">
                            Primer puesto
                            <asp:DropDownList ID="ddl_1" runat="server" CssClass="caja2 pull-right"></asp:DropDownList>
                            <p>&nbsp;</p>
                        </div>
                        <div class="centered">
                            Segundo puesto
                            <asp:DropDownList ID="ddl_2" runat="server" CssClass="caja2 pull-right"></asp:DropDownList>
                            <p>&nbsp;</p>
                        </div>
                        <div class="centered">
                            Tercer puesto 1
                            <asp:DropDownList ID="ddl_3_1" runat="server" CssClass="caja2 pull-right"></asp:DropDownList>
                            <p>&nbsp;</p>
                        </div>
                        <div class="centered">
                            Tercer puesto 2
                            <asp:DropDownList ID="ddl_3_2" runat="server" CssClass="caja2 pull-right"></asp:DropDownList>
                            <p>&nbsp;</p>
                        </div>
                    </div>
                    <div>
                        <a id="btn_agregar_participante" href="" data-toggle="modal" data-target="#nuevoParticipante" class="btn btn-outline-dark">Agregar nuevo participante</a>
                    </div>
                </div>
                <p>&nbsp;</p>
                <asp:Button ID="btn_agregarResultado" class="btn btn-outline-dark pull-right" runat="server" Text="Aceptar" formnovalidate="true" CausesValidation="false" OnClick="btn_agregarResultado_Click" />
            </asp:Panel>

            <p>&nbsp;</p>

            <asp:Button ID="btn_volver" runat="server" Text="Volver" CssClass="btn btn-link pull-left" OnClick="btn_volver_Click" />
        </div>


        <!-- VENTANA EMERGENTE CARGA NUEVO PARTICIPANTE-->
        <div class="modal fade col-lg-12 col-md-12 col-xs-8 col-sm-8" id="nuevoParticipante" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <!--Cabecera-->
                    <div class="modal-header">
                        <h4 class="modal-title" id="exampleModalLabe2">Agregar nuevo participante</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>

                    <!--Cuerpo-->
                    <a class="text-danger">* Campo requerido</a>
                    <div class="modal-body">
                        <div class="form-group">

                            <!--Seleccione Torneo-->
                            <asp:Panel ID="pnl_nuevoParticipante" CssClass="panel panel-default" runat="server">
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <div class="row centered center-block">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-3">
                                        <label class="pull-left">Nombre:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txt_nombre" required="true" MaxLength="50" class="form-control" runat="server" placeholder="Ingrese nombre"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1"><a class="text-danger">*</a></div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                <!--Ingresar apellido-->

                                <div class="row centered center-block">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-3">
                                        <label class="pull-left">Apellido:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txt_apellido" MaxLength="50" required="true" class="form-control" runat="server" placeholder="Ingrese apellido"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1"><a class="text-danger">*</a></div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>
                                
                         
                            

                                <div class="row centered center-block">
                                    <div class="col-md-2 col-xl-auto">
                                        <label class="pull-left">Tipo:</label>
                                    </div>
                                    <div class="col-md-3 col-xl-auto">
                                        <asp:DropDownList ID="ddl_tipo" class="caja2" runat="server" placeholder="Ingrese Tipo" ValidationGroup="grupoDni"></asp:DropDownList>
                                        <a class="text-danger">*</a>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddl_tipo_dni" runat="server" class="caja2 pull-right"></asp:DropDownList>
                                        <a class="text-danger">*</a>
                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                                <div class="row centered center-block">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-3">
                                        <label class="pull-left">DNI:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txt_dni" required="true" class="form-control" MaxLength="20" runat="server" placeholder="Ingrese DNI"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1"><a class="text-danger">*</a></div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                                <div class="row centered center-block">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-3">
                                        <label class="pull-left">Nacionalidad:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddl_nacionalidad" runat="server" class="caja2 pull-right"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-1"><a class="text-danger">*</a></div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                    <!--Botonero-->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btn_aceptar_participante" CssClass="btn btn-outline-dark" AutoPostBack="true" runat="server" Text="Aceptar" OnClick="btn_aceptar_participante_Click" />

                    </div>

                </div>
            </div>
        </div>


    </form>

</asp:Content>
