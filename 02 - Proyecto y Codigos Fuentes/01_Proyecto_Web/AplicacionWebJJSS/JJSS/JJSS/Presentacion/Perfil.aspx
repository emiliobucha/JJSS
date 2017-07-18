<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="JJSS.Presentacion.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">

    <form id="formPerfil" runat="server">

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

        <div class="row mt centered">
            <h1>MI PERFIL</h1>
            <p>&nbsp;</p>
        </div>

        <asp:Button ID="btn_datos_personales" runat="server" Text="Datos Personales" CssClass="btn btn-default" CausesValidation="false" OnClick="btn_datos_personales_Click" />
        <asp:Button ID="btn_contacto" runat="server" Text="Datos de Contacto" CssClass="btn btn-default" CausesValidation="false" OnClick="btn_contacto_Click" />
        <asp:Button ID="btn_pass" runat="server" Text="Contraseña" CssClass="btn btn-default" CausesValidation="false" OnClick="btn_pass_Click" />
        <asp:Button ID="btn_foto_perfil" runat="server" Text="Foto de Perfil" CssClass="btn btn-default" CausesValidation="false" OnClick="btn_foto_perfil_Click" />

        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="view_datos_personales" runat="server">
                <asp:Panel ID="pnl_datos_personales" CssClass="panel panel-footer" runat="server">

                    <div class="row centered">
                        <h2>Datos Personales</h2>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>
                    </div>

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
                            <asp:RequiredFieldValidator ID="requerido_apellido" runat="server" ErrorMessage="Debe ingresar un apellido" CssClass="text-danger" Display="Dynamic" ControlToValidate="txt_apellido" ValidationGroup="vgPersonal"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="requerido_nombre" runat="server" ErrorMessage="Debe ingresar un nombre" CssClass="text-danger" Display="Dynamic" ControlToValidate="txt_nombre" ValidationGroup="vgPersonal"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="caracteres_apellido" runat="server" ControlToValidate="txt_apellido" CssClass="text-danger" Display="Dynamic" ErrorMessage="Apellido demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgPersonal"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="caracteres_nombre" runat="server" ControlToValidate="txt_nombre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Nombre demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgPersonal"></asp:RegularExpressionValidator>
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
                </asp:Panel>
                <div class="row centered">
                    <asp:Button ID="btn_guardar_personal" CssClass="btn btn-default" ValidationGroup="vgPersonal" runat="server" Text="Guardar" OnClick="btn_guardar_personal_Click" />
                </div>
            </asp:View>

            <asp:View ID="view_contacto" runat="server">
                <asp:Panel ID="pnl_datos_de_contacto" CssClass="panel panel-footer" runat="server">

                    <div class="row centered">
                        <h2>Datos de Contacto</h2>
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>
                    </div>

                    <!-- Telefono -->
                    <div class="row centered">
                        <div class="col-xs-2">
                            <label class="pull-left">Teléfono</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txt_telefono" CssClass="caja2" runat="server" placeholder="Ingrese télefono"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe ingresar el teléfono" ControlToValidate="txt_telefono" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgAlumnos"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="numerico_telefono" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_telefono" Type="Double" ErrorMessage="El telefono tiene que ser un valor numérico" Operator="DataTypeCheck" ValidationGroup="vgAlumnos"></asp:CompareValidator>
                            <asp:CompareValidator ID="mayor_telefono" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_telefono" Type="Double" ErrorMessage="El teléfono debe ser un valor mayor a 0" ValueToCompare="0" Operator="GreaterThan" ValidationGroup="vgAlumnos"></asp:CompareValidator>
                            <asp:CompareValidator ID="menor_telefono" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_telefono" Type="Double" ErrorMessage="El teléfono debe ser un valor menor" ValueToCompare="2147483647" Operator="LessThan" ValidationGroup="vgAlumnos"></asp:CompareValidator>
                        </div>
                        <div class="col-xs-2">

                            <!-- Telefono urgencia-->
                            <label class=" pull-right">Teléfono de urgencia</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txt_telefono_urgencia" CssClass="caja2" runat="server" placeholder="Ingrese teléfono en caso de urgencia"></asp:TextBox>
                        </div>
                        <div class="col-xs-3">
                            <asp:RequiredFieldValidator ID="requerido_telemergencia" runat="server" ErrorMessage="Debe ingresar el teléfono de urgencia" ControlToValidate="txt_telefono_urgencia" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgAlumnos"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="numerico_telemergencia" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_telefono_urgencia" Type="Double" ErrorMessage="El telefono tiene que ser un valor numérico" Operator="DataTypeCheck" ValidationGroup="vgAlumnos"></asp:CompareValidator>
                            <asp:CompareValidator ID="mayor_telemergencia" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_telefono_urgencia" Type="Double" ErrorMessage="El teléfono de urgencia debe ser un valor mayor a 0" ValueToCompare="0" Operator="GreaterThan" ValidationGroup="vgAlumnos"></asp:CompareValidator>
                            <asp:CompareValidator ID="menor_telemergencia" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_telefono_urgencia" Type="Double" ErrorMessage="El teléfono de urgencia debe ser un valor menor" ValueToCompare="2147483647" Operator="LessThan" ValidationGroup="vgAlumnos"></asp:CompareValidator>
                        </div>
                    </div>

                    <div class="row centered">
                        &nbsp;
                    </div>

                    <!-- E-mail -->
                    <div class="row centered">
                        <div class="col-xs-2">
                            <label class="pull-left">E-mail</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txt_email" class="caja2" runat="server" placeholder="Ingrese e-mail"></asp:TextBox>
                        </div>
                        <div class="col-xs-3">
                            <asp:RequiredFieldValidator ID="requerido_mail" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar el mail" ValidationGroup="vgAlumnos"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="caracteres_nombre0" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Mail demasiado largo" ValidationExpression="^[\s\S]{0,80}$" ValidationGroup="vgAlumnos"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="regex_mail" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de mail" ValidationExpression="^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$" ValidationGroup="vgAlumnos"></asp:RegularExpressionValidator>
                        </div>
                    </div>

                    <div class="row centered">
                        &nbsp;
                    </div>
                    <!-- Calle y numero -->
                    <div class="row centered">
                        <div class="col-xs-2">
                            <label class="pull-left">Calle</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txt_calle" class="caja2" runat="server" placeholder="Ingrese calle"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="caracteres_calle" runat="server" ControlToValidate="txt_calle" CssClass="text-danger" Display="Dynamic" ErrorMessage="Calle demasiado larga" ValidationExpression="^[\s\S]{0,50}$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-xs-1">
                            <label class="pull-right">Numero</label>
                        </div>
                        <div class="col-xs-1">
                            <asp:TextBox ID="txt_numero" class="caja2" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="mayor_numero" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_numero" Type="Integer" ErrorMessage="El numéro debe ser un valor mayor a 0" ValueToCompare="0" Operator="GreaterThan" ValidationGroup="vgAlumnos"></asp:CompareValidator>
                            <asp:CompareValidator ID="menor_numero" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_numero" Type="Integer" ErrorMessage="Número demasiado largo" ValueToCompare="2147483647" Operator="LessThan" ValidationGroup="vgAlumnos"></asp:CompareValidator>
                        </div>
                        <div class="col-xs-1">
                            <label class="pull-right">Piso</label>
                        </div>
                        <div class="col-xs-1">
                            <asp:TextBox ID="txt_piso" class="caja2" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="mayor_piso" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_piso" Type="Integer" ErrorMessage="El piso debe ser un valor mayor a 0" ValueToCompare="0" Operator="GreaterThan" ValidationGroup="vgAlumnos"></asp:CompareValidator>
                            <asp:CompareValidator ID="menor_piso" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_piso" Type="Integer" ErrorMessage="Piso demasiado largo" ValueToCompare="2147483647" Operator="LessThan" ValidationGroup="vgAlumnos"></asp:CompareValidator>
                        </div>
                        <div class="col-xs-1">
                            <label class=" pull-right">Dpto</label>
                        </div>
                        <div class="col-xs-1">
                            <asp:TextBox ID="txt_nro_dpto" class="caja2" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="caracteres_departamento" runat="server" ControlToValidate="txt_nro_dpto" CssClass="text-danger" Display="Dynamic" ErrorMessage="Departamento demasiado largo" ValidationExpression="^[\s\S]{0,20}$" ValidationGroup="vgAlumnos"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="row centered">
                        &nbsp;
                    </div>
                    <!-- Provincia y localidad -->
                    <div class="row centered">
                        <div class="col-xs-2">
                            <label class="pull-left">Provincia</label>
                        </div>
                        <div class="col-xs-3">
                            <%--<asp:TextBox ID="txt_localidad" class="caja2" runat="server" placeholder="Ingrese localidad"></asp:TextBox>--%>
                            <asp:DropDownList class="caja2" ID="ddl_provincia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_provincia_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-2">
                            <label class="pull-left">Localidad</label>
                        </div>
                        <div class="col-xs-3">
                            <%--<asp:TextBox ID="txt_localidad" class="caja2" runat="server" placeholder="Ingrese localidad"></asp:TextBox>--%>
                            <asp:DropDownList class="caja2" ID="ddl_localidad" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>


                    <div class="row centered">
                        &nbsp;
                    </div>
                </asp:Panel>
                <div class="row centered">
                    <asp:Button ID="btn_guardar_contacto" CssClass="btn btn-default" ValidationGroup="vgContacto" runat="server" Text="Guardar" OnClick="btn_guardar_contaco_Click" />
                </div>
            </asp:View>

            <asp:View ID="view_pass" runat="server">
                <!-- pass-->
                <asp:Panel ID="pnl_cambiar_pass" runat="server" CssClass="panel-footer">
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
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="row centered">
                    <asp:Button ID="btn_cambiar_pass" runat="server" CssClass="btn btn-default" ValidationGroup="val_cambiar_pass" Text="Cambiar Contraseña" OnClick="btn_cambiar_pass_Click" />
                </div>
            </asp:View>

            <asp:View ID="view_foto_perfil" runat="server">
                <div class="row centered">
                    <asp:Panel ID="Panel1" runat="server" CssClass="panel-footer">

                        <label class="pull-left">Imagen</label>
                        <input id="avatarUpload" type="file" name="file" onchange="previewFile()" runat="server" />
                        <%--<asp:FileUpload ID="avatarUpload" runat="server" />--%>
                        <asp:Image ID="Avatar" runat="server" Height="225px" ImageUrl="~/Images/NoUser.jpg" Width="225px" />
                    </asp:Panel>
                    <div class="row centered">
                        <asp:Button ID="btn_cambiar_foto" CssClass="btn btn-default" CausesValidation="false" runat="server" Text="Cambiar Foto Perfil" OnClick="btn_cambiar_foto_Click" />
                    </div>
                </div>


            </asp:View>
        </asp:MultiView>
        <div class="row centered">
            <asp:Button ID="btn_cancelar" runat="server" Text="Volver" CssClass="btn btn-default" OnClick="btn_cancelar_Click" />
        </div>
    </form>


</asp:Content>
