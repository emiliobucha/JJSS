<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="JJSS.Presentacion.Perfil" %>



<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">

    <form id="formPerfil" runat="server">
        <div class=" container">
            <div class="row centered center-block">
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
            </div>
            <div class="row centered center-block">
                <asp:Panel ID="pnl_mensaje_error" runat="server" Visible="false">
                    <div class="col-md-2"></div>
                    <div class="col-md-10">
                        <div class="alert alert-danger alert-dismissible" role="alert">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <a class="ui-icon ui-icon-alert"></a>
                            <strong>Error! </strong>
                            <asp:Label ID="lbl_error" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                </asp:Panel>
            </div>
        </div>

        <div class="row mt centered justify-content-center ">
            <h1 class="centered">Mi Perfil</h1>
        </div>

        <div>
            &nbsp;
        </div>

        <div class=" container">
            <div class="p-2">
                <asp:Button ID="btn_datos_personales" runat="server" Text="Datos Personales" CssClass="btn btn-outline-dark" CausesValidation="false" OnClick="btn_datos_personales_Click" />
                <asp:Button ID="btn_contacto" runat="server" Text="Datos de Contacto" CssClass="btn btn-outline-dark" CausesValidation="false" OnClick="btn_contacto_Click" />
                <asp:Button ID="btn_pass" runat="server" Text="Contraseña" CssClass="btn btn-outline-dark" CausesValidation="false" OnClick="btn_pass_Click" />
                <asp:Button ID="btn_foto_perfil" runat="server" Text="Foto de Perfil" CssClass="btn btn-outline-dark" CausesValidation="false" OnClick="btn_foto_perfil_Click" />
            </div>

            <asp:MultiView ID="MultiView1" runat="server">

                <asp:View ID="view_datos_personales" runat="server">

                    <div class=" border rounded p-4 centered justify-content-center">

                        <asp:Panel ID="pnl_datos_personales" CssClass="panel panel-footer" runat="server">

                            <div class="row centered">
                                <h2>Datos Personales</h2>

                            </div>
                            <div>
                                <p>&nbsp;</p>
                            </div>

                            <!--Nombre y Apellido-->
                            <div class="row p-1  pl-lg-5 pl-md-5">
                                <!--Nombre-->
                                <div class="col-lg-2 col-md-2 col-sm-12 text-left">
                                    <label class="text-left">Nombre </label>
                                </div>
                                <div class="col col-lg-3 col-md-3 col-sm-12">
                                    <asp:TextBox ID="txt_nombre" runat="server" CssClass="caja2"></asp:TextBox>
                                </div>

                                <!--Apellido-->
                                <div class="col-lg-2 col-md-2 col-sm-12 text-left pl-lg-5 pl-md-5">
                                    <label class="text-left">Apellido </label>
                                </div>
                                <div class="col col-lg-3 col-md-3 col-sm-12">
                                    <asp:TextBox ID="txt_apellido" runat="server" CssClass="caja2"></asp:TextBox>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-12">
                                    <asp:RequiredFieldValidator ID="requerido_apellido" runat="server" ErrorMessage="Debe ingresar un apellido" CssClass="text-danger" Display="Dynamic" ControlToValidate="txt_apellido" ValidationGroup="vgPersonal"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="requerido_nombre" runat="server" ErrorMessage="Debe ingresar un nombre" CssClass="text-danger" Display="Dynamic" ControlToValidate="txt_nombre" ValidationGroup="vgPersonal"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="caracteres_apellido" runat="server" ControlToValidate="txt_apellido" CssClass="text-danger" Display="Dynamic" ErrorMessage="Apellido demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgPersonal"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="caracteres_nombre" runat="server" ControlToValidate="txt_nombre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Nombre demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgPersonal"></asp:RegularExpressionValidator>
                                </div>
                            </div>


                            <!-- DNI-->
                            <div class="row p-1  pl-lg-5 pl-md-5">
                                <div class="col-lg-2 col-md-2 col-sm-12  text-left">
                                    <label class="text-left">DNI </label>
                                </div>
                                <div class="col col-lg-3 col-md-3 col-sm-12">
                                    <asp:TextBox ID="txt_dni" runat="server" CssClass="caja2" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <!-- nombre de usuario-->
                            <div class="row p-1  pl-lg-5 pl-md-5">
                                <div class="col-lg-2 col-md-2 col-sm-12  text-left">
                                    <label>Nombre de usuario</label>
                                </div>
                                <div class="col col-lg-3 col-md-3 col-sm-12">
                                    <asp:TextBox ID="txt_usuario" runat="server" CssClass="caja2" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row centered">
                                &nbsp;
                            </div>

                        </asp:Panel>

                        <div class="row centered justify-content-center">
                            <asp:Button ID="btn_guardar_personal" CssClass="btn btn-outline-dark" ValidationGroup="vgPersonal" runat="server" Text="Aceptar" OnClick="btn_guardar_personal_Click" />
                        </div>
                    </div>

                </asp:View>

                <asp:View ID="view_contacto" runat="server">

                    <div class=" border rounded p-4 centered justify-content-center">

                        <asp:Panel ID="pnl_datos_de_contacto" CssClass="panel panel-footer" runat="server">

                            <div class="row centered">
                                <h2>Datos de Contacto</h2>
                            </div>
                            <div>
                                <p>&nbsp;</p>
                            </div>

                            <!-- Telefono -->
                            <div class="row p-1  pl-lg-5 pl-md-5">
                                <div class="col-lg-2 col-md-2 col-sm-12 text-left">
                                    <label>Teléfono</label>
                                </div>
                                <div class="col-md-3 col-lg-3 col-sm-12 ">
                                    <asp:TextBox ID="txt_telefono" CssClass="caja2" runat="server" placeholder="Ingrese télefono"></asp:TextBox>
                                </div>

                                <!-- Telefono urgencia-->
                                <div class=" col-lg-3 col-md-3 col-sm-12 pl-lg-5 pl-md-5 text-left">
                                    <label>Teléfono de urgencia</label>
                                </div>
                                <div class="col-md-3 col-lg-3 col-sm-12 ">
                                    <asp:TextBox ID="txt_telefono_urgencia" CssClass="caja2" runat="server" placeholder="Ingrese teléfono en caso de urgencia"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-lg-2 col-sm-12 ">
                                    <asp:RequiredFieldValidator ID="requerido_telemergencia" runat="server" ErrorMessage="Debe ingresar el teléfono de urgencia" ControlToValidate="txt_telefono_urgencia" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgContacto"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Formato inválido" ControlToValidate="txt_telefono_urgencia" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgContacto" ValidationExpression="^[0-9]{0,15}$"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe ingresar el teléfono" ControlToValidate="txt_telefono" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgContacto"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regex_telefono" runat="server" ErrorMessage="Formato inválido" ControlToValidate="txt_telefono" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgContacto" ValidationExpression="^[0-9]{0,15}$"></asp:RegularExpressionValidator>
                                </div>
                            </div>


                            <!-- E-mail -->
                            <div class="row p-1  pl-lg-5 pl-md-5">
                                <div class="col-lg-2 col-md-2 col-sm-12 text-left">
                                    <label class="text-left">E-mail</label>
                                </div>
                                <div class="col col-md-4 col-lg-4 col-sm-12">
                                    <asp:TextBox ID="txt_email" class="caja2" runat="server" placeholder="Ingrese e-mail"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-lg-2 col-sm-12">
                                    <asp:RequiredFieldValidator ID="requerido_mail" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar el mail" ValidationGroup="vgContacto"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="caracteres_nombre0" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Mail demasiado largo" ValidationExpression="^[\s\S]{0,80}$" ValidationGroup="vgContacto"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="regex_mail" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de mail" ValidationExpression="^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$" ValidationGroup="vgContacto"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <!-- Calle y numero -->
                            <div class="row p-1  pl-lg-5 pl-md-5">
                                <!-- Calle -->
                                <div class="col-lg-2 col-md-2 col-sm-12 text-left">
                                    <label>Calle </label>
                                </div>
                                <div class="col col-md-4 col-lg-4 col-sm-10">
                                    <asp:TextBox ID="txt_calle" class="caja2" runat="server" placeholder="Ingrese calle"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="caracteres_calle" runat="server" ControlToValidate="txt_calle" CssClass="text-danger" Display="Dynamic" ErrorMessage="Calle demasiado larga" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgContacto"></asp:RegularExpressionValidator>
                                </div>
                                <!-- Número -->
                                <div class="col-lg-2 col-md-2 col-sm-12 pl-lg-5 pl-md-5 text-left">
                                    <label>Número </label>
                                </div>
                                <div class="col col-md-1 col-lg-1 col-sm-10">
                                    <asp:TextBox ID="txt_numero" class="caja2" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Formato inválido" ControlToValidate="txt_numero" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgContacto" ValidationExpression="^[0-9]{0,9}$"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="row p-1  pl-lg-5 pl-md-5">
                                <!-- Piso -->
                                <div class="col-lg-2 col-md-2 col-sm-12 text-left">
                                    <label>Piso </label>
                                </div>
                                <div class="col col-md-1 col-lg-1 col-sm-10 ">
                                    <asp:TextBox ID="txt_piso" class="caja2" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Formato inválido" ControlToValidate="txt_piso" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgContacto" ValidationExpression="^[0-9]{0,9}$"></asp:RegularExpressionValidator>
                                </div>
                                <!-- Dpto -->
                                <div class="col-lg-1 col-md-1 col-sm-2 text-left">
                                    <label>Dpto </label>
                                </div>
                                <div class="col col col-md-2 col-lg-2 col-sm-10 text-left">
                                    <asp:TextBox ID="txt_nro_dpto" class="caja2" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="caracteres_departamento" runat="server" ControlToValidate="txt_nro_dpto" CssClass="text-danger" Display="Dynamic" ErrorMessage="Departamento demasiado largo" ValidationExpression="^[\s\S]{0,20}$" ValidationGroup="vgContacto"></asp:RegularExpressionValidator>
                                </div>
                                <!-- Torre -->
                                <div class="col-lg-1 col-md-1 col-sm-2 text-left">
                                    <label>Torre</label>
                                </div>
                                <div class="col col-md-2 col-lg-2 col-sm-10">
                                    <asp:TextBox ID="txt_torre" type="text" MaxLength="20" class="caja2" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_torre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Departamento demasiado largo" ValidationExpression="^[\s\S]{0,20}$" ValidationGroup="vgAlumnos"> </asp:RegularExpressionValidator>
                                </div>
                            </div>


                            <!-- Provincia -->
                            <div class="row p-1  pl-lg-5 pl-md-5 text-left">
                                <div class="col-lg-2 col-md-2 col-sm-12 ">
                                    <label>Provincia</label>
                                </div>
                                <div class="col col-md-3 col-lg-3 col-sm-10 col-xs-10">
                                    <%--<asp:TextBox ID="txt_localidad" class="caja2" runat="server" placeholder="Ingrese localidad"></asp:TextBox>--%>
                                    <asp:DropDownList class="caja2" ID="ddl_provincia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_provincia_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <!--  localidad -->
                            <div class="row p-1  pl-lg-5 pl-md-5 text-left">
                                <div class="col-lg-2 col-md-2 col-sm-12 ">
                                    <label>Localidad</label>
                                </div>
                                <div class="col col-md-3 col-lg-3 col-sm-10 col-xs-10">
                                    <%--<asp:TextBox ID="txt_localidad" class="caja2" runat="server" placeholder="Ingrese localidad"></asp:TextBox>--%>
                                    <asp:DropDownList class="caja2" ID="ddl_localidad" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div>
                                &nbsp;
                            </div>

                        </asp:Panel>

                        <div class="row centered justify-content-center">
                            <div class="col col-auto">
                                <asp:Button ID="btn_guardar_contacto" CssClass="btn btn-outline-dark" ValidationGroup="vgContacto" runat="server" Text="Aceptar" OnClick="btn_guardar_contacto_Click" />
                            </div>
                        </div>
                    </div>
                </asp:View>

                <asp:View ID="view_pass" runat="server">
                    <!-- pass-->

                    <div class=" border rounded p-4 centered justify-content-center">

                        <asp:Panel ID="pnl_cambiar_pass" runat="server" CssClass="panel-footer">


                            <div class="row centered">
                                <h2>Cambiar contraseña</h2>
                            </div>
                            <div>
                                <p>&nbsp;</p>
                            </div>


                            <!--pass anterior-->
                            <div class="row centered p-1">
                                <div class="col-md-2"></div>
                                <div class="col-md-3 text-left">
                                    <label>Contraseña anterior</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txt_pass_anterior" runat="server" CssClass="caja2" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:RequiredFieldValidator ID="requeridoPassAnterios" runat="server" ErrorMessage="Debe ingresar una contraseña anterior" ControlToValidate="txt_pass_anterior" CssClass="text-danger" ValidationGroup="val_cambiar_pass" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>


                            <!--pass nueva-->
                            <div class="row centered  p-1">
                                <div class="col-md-2"></div>
                                <div class="col-md-3 text-left">
                                    <label>Contraseña nueva</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txt_pass_nueva" TextMode="Password" runat="server" CssClass="caja2"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:RequiredFieldValidator ID="requeridoPassNueva" runat="server" ErrorMessage="Debe ingresar una contraseña nueva" ControlToValidate="txt_pass_nueva" CssClass="text-danger" ValidationGroup="val_cambiar_pass" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="compararNueva" runat="server" CssClass="text-danger" ErrorMessage="La contraseña nueva debe ser distinta a la anterior" Display="Dynamic" ControlToValidate="txt_pass_nueva" ControlToCompare="txt_pass_anterior" ValidationGroup="val_cambiar_pass" Operator="NotEqual"></asp:CompareValidator>
                                </div>
                            </div>
                            <!--pass anterior-->
                            <div class="row centered  p-1">
                                <div class="col-md-2"></div>
                                <div class="col-md-3 text-left">
                                    <label>Repetir contraseña nueva</label>
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

                        </asp:Panel>
                        <div class="row centered justify-content-center">
                            <div class="col col-auto">
                                <asp:Button ID="btn_cambiar_pass" runat="server" CssClass="btn btn-outline-dark" ValidationGroup="val_cambiar_pass" Text="Cambiar Contraseña" OnClick="btn_cambiar_pass_Click" />
                            </div>
                        </div>
                    </div>
                </asp:View>

                <asp:View ID="view_foto_perfil" runat="server">

                    <div class=" border rounded p-4 centered justify-content-center">

                        <asp:Panel ID="Panel1" runat="server" CssClass="panel-footer">

                            <div class="row centered">
                                <h2>Imagen</h2>
                            </div>
                            <div>
                                <p>&nbsp;</p>
                            </div>
                            <div class="row centered">
                                <div class="col col-auto">
                                    <input id="avatarUpload" type="file" name="file" onchange="previewFile()" runat="server" />
                                </div>
                            </div>
                            <div class="row centered">
                                <div class="col col-auto">
                                    <asp:Image ID="Avatar" runat="server" Height="225px" ImageUrl="~/Images/NoUser.jpg" Width="225px" />
                                    <%--<asp:FileUpload ID="avatarUpload" runat="server" />--%>
                                </div>
                            </div>
                        </asp:Panel>

                        <div>
                            <p>&nbsp;</p>
                        </div>

                        <div class="row centered justify-content-center">
                            <div class="col col-auto">
                                <asp:Button ID="btn_cambiar_foto" CssClass="btn btn-outline-dark" CausesValidation="false" runat="server" Text="Cambiar Foto Perfil" OnClick="btn_cambiar_foto_Click" />
                            </div>
                        </div>
                    </div>

                </asp:View>

            </asp:MultiView>

            <div>
                <p>&nbsp;</p>
            </div>

            <div class="row p-1">
                <div class="col col-auto">
                    <asp:Button ID="btn_Cancelar" runat="server" Text="Volver a inicio" CssClass="btn btn-link pull-left" CausesValidation="false" formnovalidate="true" OnClick="btn_cancelar_Click" />
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=Avatar.ClientID %>');
        var file = document.querySelector('#<%=avatarUpload.ClientID %>').files[0];
        var reader = new FileReader();

        reader.onloadend = function () {
            preview.src = reader.result;
        }

        if (file) {
            reader.readAsDataURL(file);
        } else {
            preview.src = "";
        }
        }
    </script>


</asp:Content>
