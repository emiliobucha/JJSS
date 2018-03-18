<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site2.Master" AutoEventWireup="true" CodeBehind="CrearCategoria.aspx.cs" Inherits="JJSS.Administracion.CrearCategoria" %>
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
        
        <asp:Panel ID="pnlFormulario" runat="server">
            <div id="agregarProductoswrap">
                <div class="container">
                    <div class="row mt centered">
                        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                            <asp:Label ID="lbl_crear_sede" runat="server" Text="CREAR CATEGORIA" Font-Size="XX-Large"></asp:Label>
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
                                    <asp:TextBox ID="txt_nombre" class="caja2" required="true" MaxLength="60" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <div class="row centered">
                                <div class="col-xs-2">
                                    <label class="pull-left">Sexo</label>
                                </div>
                                <div class="col-xs-2">
                                    <asp:RadioButtonList ID="rbSexo" runat="server" AutoPostBack="False">
                                        <asp:ListItem Selected="True" Value="masculino">Masculino</asp:ListItem>
                                        <asp:ListItem Value="femenino">Femenino</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>


                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                           
                            <div class="row centered">
                                <div class="col-xs-1">
                                    <label class="pull-left">Peso mínimo</label>
                                </div>
                                <div class="col-xs-2">
                                    <asp:TextBox ID="txtPesoMinimo" class="caja2" type="Number" min="0" max="200" step="0.01" runat="server" required="true" ></asp:TextBox>
                                </div>
                                <div class="col-xs-1">
                                    <label class="pull-left">Peso máximo</label>
                                </div>
                                <div class="col-xs-2">
                                    <asp:TextBox ID="txtPesoMaximo" class="caja2" type="Number" min="0" max="200" step="0.01" runat="server" required="true" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>
                           
                            <div class="row centered">
                                <div class="col-xs-1">
                                    <label class="pull-left">Edad mínima</label>
                                </div>
                                <div class="col-xs-2">
                                    <asp:TextBox ID="txtEdadMinima" class="caja2" type="Number" min="0" max="100" runat="server" required="true" ></asp:TextBox>
                                </div>
                                <div class="col-xs-1">
                                    <label class="pull-left">Edad máxima</label>
                                </div>
                                <div class="col-xs-2">
                                    <asp:TextBox ID="txtEdadMaxima" class="caja2" type="Number" min="0" max="100" runat="server" required="true" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!-- tipo clase -->
                            <div class="row centered">
                                <div class="col-xs-2">
                                    <label class="pull-left">Disciplina</label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:DropDownList class="caja2" ID="ddlDisciplina" runat="server" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Boton Aceptar-->
                            <div class="row centered">
                                <asp:Button ID="btn_aceptar" type="submit" class="btn btn-default" runat="server" Text="Aceptar" ValidationGroup="vgDatos" OnClick="btn_aceptar_Click" />



                            </div>
                            <asp:Button ID="btnInicio" runat="server" Text="Volver a inicio" CssClass="btn-link pull-left" CausesValidation="false" formnovalidate="true" OnClick="btnInicio_Click" />


                        </div>

                    </div>
                </div>
            </div>
        </asp:Panel>

    </form>
</asp:Content>
