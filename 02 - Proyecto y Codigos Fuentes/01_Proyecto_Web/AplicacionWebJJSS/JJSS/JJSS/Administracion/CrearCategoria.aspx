<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="CrearCategoria.aspx.cs" Inherits="JJSS.Administracion.CrearCategoria" %>
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

            <div>
                <p>&nbsp;</p>
            </div>

            <div class="row centered justify-content-center">
                <h1 class=" centered ">Crear Categoria</h1>
            </div>

            <div>
                <p>&nbsp;</p>
            </div>

            <div class="container">

                <div class="border rounded p-1">


                    <div>
                        <p>&nbsp;</p>
                    </div>

                    <!--Ingresar nombre-->

                    <div class="row ">
                        <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                            <label>Nombre</label>
                        </div>
                        <div class="col col-lg-3 col-md-3 col-sm-10">
                            <asp:TextBox ID="txt_nombre" class="caja2" required="true" MaxLength="60" runat="server"></asp:TextBox>
                        </div>
                        <div class="col col-1"><a class="text-danger">*</a></div>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <!-- Sexo -->
                    <div class="row">
                        <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                            <label>Sexo</label>
                        </div>
                        <div class="col col-lg-2 col-md-2 col-sm-10">
                            <asp:RadioButtonList ID="rbSexo" runat="server" AutoPostBack="False">
                                <asp:ListItem Selected="True" Value="masculino">Masculino</asp:ListItem>
                                <asp:ListItem Value="femenino">Femenino</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <!-- Peso -->
                    <div class="row ">
                        <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                            <label>Peso mínimo</label>
                        </div>
                        <div class="col col-lg-2 col-md-2 col-sm-10">
                            <asp:TextBox ID="txtPesoMinimo" class="caja2" type="Number" min="0" max="200" step="0.01" runat="server" required="true"></asp:TextBox>
                        </div>
                        <div class="col col-1"><a class="text-danger">*</a></div>
                         <!----------------->
                        <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                            <label>Peso máximo</label>
                        </div>
                        <div class="col col-lg-2 col-md-2 col-sm-10">
                            <asp:TextBox ID="txtPesoMaximo" class="caja2" type="Number" min="0" max="200" step="0.01" runat="server" required="true"></asp:TextBox>
                        </div>
                        <div class="col col-1"><a class="text-danger">*</a></div>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>


                    <!-- Edad -->
                    <div class="row ">
                        <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                            <label>Edad mínima</label>
                        </div>
                        <div class="col col-lg-2 col-md-2 col-sm-10">
                            <asp:TextBox ID="txtEdadMinima" class="caja2" type="Number" min="0" max="100" runat="server" required="true"></asp:TextBox>
                        </div>
                        <div class="col col-1"><a class="text-danger">*</a></div>
                        <!----------------->
                        <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                            <label>Edad máxima</label>
                        </div>
                        <div class="col col-lg-2 col-md-2 col-sm-10">
                            <asp:TextBox ID="txtEdadMaxima" class="caja2" type="Number" min="0" max="100" runat="server" required="true"></asp:TextBox>
                        </div>
                        <div class="col col-1"><a class="text-danger">*</a></div>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <!-- tipo clase -->
                    <div class="row">
                        <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                            <label>Disciplina</label>
                        </div>
                        <div class="col col-lg-3 col-md-3 col-sm-10">
                            <asp:DropDownList class="caja2" ID="ddlDisciplina" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <!--Boton Aceptar-->
                    <div class="row centered">
                        <div class="col">
                            <asp:Button ID="btn_aceptar" type="submit" class="btn btn-outline-dark" runat="server" Text="Aceptar" ValidationGroup="vgDatos" OnClick="btn_aceptar_Click" />
                        </div>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class=" p-2 ">
                        <p class="text-danger pull-right " style="font-size: small">* Campo requerido</p>
                    </div>

                    <div>
                        <p>&nbsp;</p>
                    </div>
                    
                </div>

                 <div>
                        <p>&nbsp;</p>
                    </div>

                <div class="row pull-left">
                    <div class="col">
                        <asp:LinkButton runat="server" ID="lnk_cancelar" class="btn btn-link " Text="Volver" href="" CausesValidation="false" formnovalidate="true" OnClick="btnInicio_Click"></asp:LinkButton>
                    </div>
                </div>

                 <div>
                        <p>&nbsp;</p>
                    </div>

            </div>              


        </asp:Panel>

    </form>
</asp:Content>
