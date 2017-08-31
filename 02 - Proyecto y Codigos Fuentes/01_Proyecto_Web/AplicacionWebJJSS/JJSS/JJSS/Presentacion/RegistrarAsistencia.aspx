<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarAsistencia.aspx.cs" Inherits="JJSS.Presentacion.RegistrarAsistencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">

    <section id="registrarAsistencia" title="registrarAsistencia"></section>
    <form id="formRegAsistencia" runat="server">
        <div id="registrowrap">

            <asp:Panel ID="pnl_mensaje_exito" runat="server" Visible="false">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <div class="alert alert-success alert-dismissible" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <a class="ui-icon ui-icon-check"></a>
                        <strong>
                            <asp:Label ID="lbl_exito" runat="server"></asp:Label></strong>
                    </div>
                </div>
                <div class="row centered">
                    <p>&nbsp;</p>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnl_mensaje_error" runat="server" Visible="false">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <div class="alert alert-danger alert-dismissible" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <a class="ui-icon ui-icon-alert"></a>
                        <strong>Error! </strong>
                        <asp:Label ID="lbl_error" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row centered">
                    <p>&nbsp;</p>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnl_registro" runat="server">

                <div id="registrarAsistenciawrap">

                    <div class="container">
                        <div class="row mt centered">
                            <h1>REGISTRO DE ASISTENCIA</h1>
                            <p>&nbsp;</p>
                        </div>


                        <div class="form-group ">



                            <!-- DNI-->
                            <div class="row centered">
                                <div class ="col-xs-3"></div>
                                <div class="col-xs-2">
                                    <label class="pull-left">DNI</label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:TextBox ID="txtDni" class="caja2" runat="server" placeholder="Ingrese DNI"></asp:TextBox>
                                </div>
                                <div class="col-xs-3">
                                    <asp:RequiredFieldValidator ID="requeridoDni" runat="server" ErrorMessage="Debe ingresar el DNI" ControlToValidate="txtDni" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgRegistro"> </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regex_dni" runat="server" ErrorMessage="Formato inválido" ControlToValidate="txtDni" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgRegistro" ValidationExpression="^[0-9]{0,9}$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>


                            <!-- Ubicacion -->
                            <div class="row centered">
                                <div class ="col-xs-3"></div>
                                <div class="col-xs-2">
                                    <label class="pull-left">Ubicación</label>
                                </div>
                                <div class="col-xs-3">
                                    <%--<asp:TextBox ID="txt_localidad" class="caja2" runat="server" placeholder="Ingrese localidad"></asp:TextBox>--%>
                                    <asp:DropDownList class="caja2" ID="ddl_ubicacion" runat="server" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Boton-->
                            <div class="row centered">
                                <asp:Button ID="btn_cancelar" runat="server" CssClass="btn btn-default" Text="Cancelar" CausesValidation="false" OnClick="btn_cancelar_Click" />
                                <asp:Button ID="btn_aceptar" runat="server" CssClass="btn btn-default" Text="Aceptar" ValidationGroup="vgRegistro" OnClick="btn_aceptar_Click" />

                            </div>
                        </div>
                    </div>

                </div>
                <!-- /row -->
            </asp:Panel>
        </div>

    </form>

</asp:Content>
