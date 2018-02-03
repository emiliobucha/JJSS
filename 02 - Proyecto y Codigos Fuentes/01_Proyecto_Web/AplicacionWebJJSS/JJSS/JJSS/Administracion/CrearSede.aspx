<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site2.Master" AutoEventWireup="true" CodeBehind="CrearSede.aspx.cs" Inherits="JJSS.Presentacion.Administracion.CrearSede" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContenido" runat="server">

    <asp:Panel ID="pnl_mensaje_exito" runat="server" Visible="false">
        <div class="col-md-2 hidden-sm"></div>
        <div class="col-md-8 col-sm-10 col-xs-10">
            <div class="alert alert-success alert-dismissible" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <a class="ui-icon ui-icon-check"></a>
                <strong>
                    <asp:Label ID="lbl_exito" runat="server" Text=""></asp:Label></strong>
            </div>
        </div>
        <div class="row centered">
            <p>&nbsp;</p>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnl_mensaje_error" runat="server" Visible="false">
        <div class="col-md-2 hidden-sm"></div>
        <div class="col-md-8 col-sm-10 col-xs-10">
            <div class="alert alert-danger alert-dismissible" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <a class="ui-icon ui-icon-alert"></a>
                <strong>Error! </strong>
                <asp:Label ID="lbl_error" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="row centered">
            <p>&nbsp;</p>
        </div>
    </asp:Panel>

    <form id="form1" runat="server">
        <div class="row centered">
            <p>&nbsp;</p>
        </div>

        <asp:Panel ID="pnlFormulario" runat="server">
            <div id="agregarProductoswrap">
                <div class="container">
                    <div class="row mt centered">
                        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                            <asp:Label ID="lbl_crear_sede" runat="server" Text="CREAR SEDE" Font-Size="XX-Large"></asp:Label>
                        </div>

                        <div class="form-group ">

                            <div class="row centered col-md-12 col-lg-12 col-sm-12 col-xs-12">
                                <p>&nbsp;</p>
                            </div>

                            <!--Ingresar nombre-->

                            <div class="row centered">
                                <div class="col-md-2 col-lg-2 col-sm-10 col-xs-10">
                                    <label class="pull-left">Nombre</label>
                                </div>
                                <div class="col-md-3 col-lg-3 col-sm-10 col-xs-10">
                                    <asp:TextBox ID="txt_nombre" class="caja2" required="true" MaxLength="50" runat="server" placeholder="Ingrese nombre"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                                <asp:Label ID="lbl_direccion" runat="server" Text="Direccion" Font-Size="Large"></asp:Label>
                            </div>
                            <!--Ingresar calle y numero-->

                            <!-- Calle y numero -->
                            <div class="row centered">
                                <div class="col-xs-2">
                                    <label class="pull-left">Calle</label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:TextBox ID="txt_calle" class="caja2" type="text" MaxLength="50" runat="server" placeholder="Ingrese calle"></asp:TextBox>
                                </div>
                                <div class="col-xs-1">
                                    <label class="pull-right">Numero</label>
                                </div>
                                <div class="col-xs-1">
                                    <asp:TextBox ID="txt_numero" type="number" min="0" max="100000" class="caja2" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-xs-1">
                                    <label class="pull-right">Piso</label>
                                </div>
                                <div class="col-xs-1">
                                    <asp:TextBox ID="txt_piso" class="caja2" type="number" min="0" max="100000" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-xs-1">
                                    <label class=" pull-right">Dpto</label>
                                </div>
                                <div class="col-xs-1">
                                    <asp:TextBox ID="txt_nro_dpto" type="text" MaxLength="20" class="caja2" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="caracteres_departamento" runat="server" ControlToValidate="txt_nro_dpto" CssClass="text-danger" Display="Dynamic" ErrorMessage="Departamento demasiado largo" ValidationExpression="^[\s\S]{0,20}$" ValidationGroup="vgDatos"> </asp:RegularExpressionValidator>
                                </div>
                                <div class="col-xs-1">
                                    <label class=" pull-right">Torre</label>
                                </div>
                                <div class="col-xs-1">
                                    <asp:TextBox ID="txt_torre" type="text" MaxLength="20" class="caja2" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_torre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Departamento demasiado largo" ValidationExpression="^[\s\S]{0,20}$" ValidationGroup="vgDatos"> </asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <!-- Provincia -->
                            <div class="row centered">
                                <div class="col-xs-2">
                                    <label class="pull-left">Provincia</label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:DropDownList class="caja2" ID="ddl_provincia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_provincia_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <!-- Localidad -->
                            <div class="row centered">
                                <div class="col-xs-2">
                                    <label class="pull-left">Localidad</label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:DropDownList class="caja2" ID="ddl_localidad" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <!--Boton Aceptar-->
                            <div class="row centered">
                                <asp:Button ID="btn_aceptar" type="submit" class="btn btn-default" runat="server" Text="Aceptar" ValidationGroup="vgDatos" OnClick="btn_aceptar_Click" />



                            </div>
                            <asp:Button ID="Button1" runat="server" Text="Volver a inicio" CssClass="btn-link pull-left" CausesValidation="false" formnovalidate="true" OnClick="btn_cancelar_Click" />


                        </div>

                    </div>
                </div>
            </div>
        </asp:Panel>

    </form>


</asp:Content>
