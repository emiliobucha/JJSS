<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="JJSS.Presentacion.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <section id="perfil" title="perfil"></section>
    <form id="formPerfil" runat="server">
        <div id="perfilwrap">

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

            <asp:Panel ID="pnlDatos" runat="server">
                <div class="row centered">
                    <p>&nbsp;</p>
                </div>
                <div class="container">
                    <div class="row mt centered">
                        <h1>MI PERFIL</h1>
                        <p>&nbsp;</p>
                    </div>

                    <div class="form-group">
                        <div class="row centered">

                            <!-- nombre y apellido-->
                            <div class="row centered">
                                <div class="col-xs-2">
                                    <label class="pull-left">Nombre</label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:TextBox ID="txt_nombre" runat="server" CssClass="caja2"></asp:TextBox>
                                </div>
                                <div class="col-xs-2">
                                    <label class="pull-left">Apellido</label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:TextBox ID="txt_apellido" runat="server" CssClass="caja2"></asp:TextBox>
                                </div>
                                <div class="col-xs-2">
                                    <asp:RequiredFieldValidator ID="requerido_apellido" runat="server" ErrorMessage="Debe ingresar un apellido" CssClass="text-danger" Display="Dynamic" ControlToValidate="txt_apellido" ValidationGroup="vgPerfil"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="requerido_nombre" runat="server" ErrorMessage="Debe ingresar un nombre" CssClass="text-danger" Display="Dynamic" ControlToValidate="txt_nombre" ValidationGroup="vgPerfil"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row centered">
                                &nbsp;
                            </div>

                            <!-- DNI-->
                            <div class="row centered">
                                <div class="col-xs-2">
                                    <label class="pull-left">D.N.I</label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:TextBox ID="txt_dni" runat="server" CssClass="caja2" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-xs-3">
                                </div>
                            </div>

                            <div class="row centered">
                                &nbsp;
                            </div>

                            <!-- nombre de usuario-->
                            <div class="row centered">
                                <div class="col-xs-2">
                                    <label class="pull-left">Nombre de usuario</label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:TextBox ID="txt_usuario" runat="server" CssClass="caja2" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-xs-3">
                                </div>
                            </div>

                            <div class="row centered">
                                &nbsp;
                            </div>

                            <!-- pass-->
                            <div class="row centered">
                                <asp:LinkButton ID="lnk_cambiar_pass" runat="server" CausesValidation="false" OnClick="lnk_cambiar_pass_Click">Cambiar Contraseña</asp:LinkButton>
                            </div>

                            <div class="row centered">
                                &nbsp;
                            </div>

                            <!-- BOTONES-->
                            <div class="row centered">
                                <asp:Button ID="btn_cancelar" CssClass="btn btn-default" runat="server" Text="Cancelar" CausesValidation="false" OnClick="btn_cancelar_Click" />
                                <asp:Button ID="btn_aceptar" CssClass="btn btn-default" runat="server" Text="Cambiar Datos" ValidationGroup="vgPerfil" OnClick="btn_aceptar_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnl_cambiar_pass" runat="server">
                <div id="cambiar_pass_wrap">
                <div class="container">
                    <div class="form-group ">

                        <div class="row mt centered">
                            <h1>Cambiar contraseña</h1>
                            <p>&nbsp;</p>
                        </div>

                        <!--pass anterior-->
                        <div class="row centered">
                            <div class="col-md-3"></div>
                            <div class="col-md-2">
                                <label class="pull-left 4">Contraseña anterior</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_pass_anterior" runat="server" CssClass="caja2" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:RequiredFieldValidator ID="requeridoPassAnterios" runat="server" ErrorMessage="Debe ingresar una contraseña anterior" ControlToValidate="txt_pass_anterior" CssClass="text-danger" ValidationGroup="val_cambiar_pass" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--pass nueva-->
                        <div class="row centered">
                            <div class="col-md-3"></div>
                            <div class="col-md-2">
                                <label class="pull-left 4">Contraseña nueva</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_pass_nueva" TextMode="Password" runat="server" CssClass="caja2"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:RequiredFieldValidator ID="requeridoPassNueva" runat="server" ErrorMessage="Debe ingresar una contraseña nueva" ControlToValidate="txt_pass_nueva" CssClass="text-danger" ValidationGroup="val_cambiar_pass" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="compararNueva" runat="server" CssClass="text-danger" ErrorMessage="La contraseña nueva debe ser distinta a la anterior" Display="Dynamic" ControlToValidate="txt_pass_nueva" ControlToCompare="txt_pass_anterior" ValidationGroup="val_cambiar_pass" Operator="NotEqual"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--pass anterior-->
                        <div class="row centered">
                            <div class="col-md-3"></div>
                            <div class="col-md-2">
                                <label class="pull-left 4">Repetir contraseña nueva</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_pass_repetida" runat="server" CssClass="caja2" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:RequiredFieldValidator ID="requeridoPassRepetida" runat="server" ErrorMessage="Debe repetir la contraseña nueva" ControlToValidate="txt_pass_repetida" CssClass="text-danger" ValidationGroup="val_cambiar_pass" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="compararPass" runat="server" CssClass="text-danger" ErrorMessage="Las contraseñas nuevas deben ser iguales" Display="Dynamic" ControlToValidate="txt_pass_repetida" ControlToCompare="txt_pass_nueva" ValidationGroup="val_cambiar_pass"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>


                        <!--Boton-->
                        <div class="row centered">
                            <asp:Button ID="btn_volver" CssClass="btn btn-default" runat="server" Text="Volver" CausesValidation="false" OnClick="btn_volver_Click" />
                            <asp:Button ID="btn_cambiar" CssClass="btn btn-default" runat="server" Text="Cambiar" ValidationGroup="val_cambiar_pass" OnClick="btn_cambiar_Click" />
                        </div>
                    </div>
                </div>
            </div>
            </asp:Panel>
        </div>

    </form>


</asp:Content>
