<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="JJSS.Presentacion.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">

    <section id="login" title="login"></section>
    <form id="form2" runat="server">
        <asp:Panel ID="pnlLogin" runat="server" CssClass="panel panel-default">
            <div id="loginwrap">
                <div class="container">
                    <div class="row mt centered">

                        <h1>Iniciar Sesión</h1>
                        <p>&nbsp;</p>
                    </div>

                    <div class="form-group ">
                        <!--usuario-->
                        <div class="row centered">
                            <div class="col-md-3"></div>
                            <div class="col-md-2">
                                <label class="pull-left 4">Nombre de Usuario</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_usuario" runat="server" placeholder="Nombre de usuario" CssClass="caja2"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:RequiredFieldValidator ID="requeridoUsuario" runat="server" ErrorMessage="Debe ingresar un nombre de usuario" ControlToValidate="txt_usuario" CssClass="text-danger" ValidationGroup="val_inicio_sesion" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--pass-->
                        <div class="row centered">
                            <div class="col-md-3"></div>
                            <div class="col-md-2">
                                <label class="pull-left 4">Contraseña</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_pass" runat="server" TextMode="Password" placeholder="Contraseña" CssClass="caja2"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:RequiredFieldValidator ID="requeridoPass" runat="server" ErrorMessage="Debe ingresar una contraseña" ControlToValidate="txt_pass" CssClass="text-danger" ValidationGroup="val_inicio_sesion" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--Recordarme-->
                        <div class="row centered">
                            <div>
                                <asp:CheckBox ID="chk_recordar" Text="  Recordarme" runat="server" />
                            </div>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--olvido pass-->
                        <div class="row centered">
                            <div>
                                <asp:LinkButton ID="lnk_olvido" runat="server" OnClick="lnk_olvido_Click">¿Ha olvidado la contraseña?</asp:LinkButton>
                            </div>
                        </div>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--Boton-->
                        <div class="row centered">
                            <asp:Button ID="btn_iniciar_sesion" CssClass="btn btn-default" runat="server" Text="Iniciar Sesión" ValidationGroup="val_inicio_sesion" OnClick="btn_iniciar_sesion_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- /row -->
        </asp:Panel>

        <asp:Panel ID="pnl_cambiar_pass" runat="server" CssClass="panel panel-default">
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
                            <asp:Button ID="btn_cambiar" CssClass="btn btn-default" runat="server" Text="Cambiar" ValidationGroup="val_cambiar_pass" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>


    </form>

</asp:Content>
