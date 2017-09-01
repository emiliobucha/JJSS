﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarProfe.aspx.cs" Inherits="JJSS.Presentacion.RegistrarProfe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMenu" runat="server">
    <a href="Inicio.aspx" class="smoothScroll">Home</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <section id="registrarProfe" title="registrarProfe"></section>
    <form id="formRegProfe" runat="server">
        <div id="registrowrap">

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
                        <asp:Label ID="lbl_error" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row centered">
                    <p>&nbsp;</p>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlFormulario" runat="server">

                <div id="registrarProfewrap">

                    <div class="container">
                        <div class="row mt centered">
                            <h1>REGISTRO DE PROFESOR</h1>
                            <p>&nbsp;</p>
                        </div>


                        <div class="form-group ">

                            <asp:Panel ID="pnl_datos_personales" CssClass="panel panel-footer" runat="server">

                                <div class="row centered">
                                    <h2>Datos Personales</h2>
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>
                                </div>

                                <!--Nombre y Apellido-->
                                <div class="row centered">
                                    <div class="col-xs-2">
                                        <!--Nombre-->
                                        <label class="pull-left">Nombre</label>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:TextBox ID="txt_nombres" runat="server" placeholder="Ingrese nombres" CssClass="caja2"></asp:TextBox>

                                    </div>
                                    <!--Apellido-->
                                    <div class="col-xs-1">
                                        <label class="pull-right">Apellido</label>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:TextBox ID="txt_apellido" runat="server" placeholder="Ingrese apellido" CssClass="caja2"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:RequiredFieldValidator ID="requerido_apellido" CssClass=" text text-danger" runat="server" ErrorMessage="Debe ingresar el apellido" ControlToValidate="txt_apellido" ValidationGroup="vgProfes"> </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="requeridoNombre" CssClass="text text-danger" runat="server" ErrorMessage="Debe ingresar el nombre" ControlToValidate="txt_nombres" ValidationGroup="vgProfes"> </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="caracteres_apellido" runat="server" ControlToValidate="txt_apellido" CssClass="text-danger" Display="Dynamic" ErrorMessage="Apellido demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgProfes"> </asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="caracteres_nombre" runat="server" ControlToValidate="txt_nombres" CssClass="text-danger" Display="Dynamic" ErrorMessage="Nombre demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgProfes"> </asp:RegularExpressionValidator>
                                    </div>

                                </div>

                                <!-- DNI-->
                                <div class="row centered">
                                    <div class="col-xs-2">
                                        <label class="pull-left">DNI</label>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:TextBox ID="txtDni" class="caja2" runat="server" placeholder="Ingrese DNI"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:RequiredFieldValidator ID="requeridoDni" runat="server" ErrorMessage="Debe ingresar el DNI" ControlToValidate="txtDni" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgProfes"> </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regex_dni" runat="server" ErrorMessage="Formato inválido" ControlToValidate="txtDni" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgProfes" ValidationExpression="^[0-9]{0,9}$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>


                                <!--Fecha de nacimiento-->
                                <div class="row centered">
                                    <div class="col-xs-2">
                                        <label class="pull-left text-left">Fecha de Nacimiento</label>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:TextBox ID="dp_fecha" runat="server" CssClass="datepicker caja2" placeholder="Seleccione fecha "></asp:TextBox>
                                    </div>

                                    <div class="col-xs-3">
                                        <asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar fecha" ValidationGroup="vgProfes"> </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inávlido de fecha" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" ValidationGroup="vgProfes"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>


                                <!--Sexo-->
                                <div class="row centered">
                                    <div class="col-xs-2">
                                        <label class="pull-left">Sexo</label>
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:RadioButtonList ID="rbSexo" runat="server" AutoPostBack="False">
                                            <asp:ListItem>Femenino</asp:ListItem>
                                            <asp:ListItem>Masculino</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-xs-1">
                                    </div>
                                    <!--Foto-->
                                    <div class="col-xs-2">
                                        <asp:Panel ID="Panel1" runat="server">

                                            <label class=" pull-left">Imagen</label>
                                            <input id="avatarUpload" type="file" name="file" onchange="previewFile()" runat="server" />
                                            <%--<asp:FileUpload ID="avatarUpload" runat="server" />--%>
                                            <asp:Image ID="Avatar" runat="server" Height="225px" ImageUrl="~/Images/NoUser.jpg" Width="225px" />
                                        </asp:Panel>
                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>


                            </asp:Panel>

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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe ingresar el teléfono" ControlToValidate="txt_telefono" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgProfes"> </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regex_telefono" runat="server" ErrorMessage="Formato inválido" ControlToValidate="txt_telefono" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgProfes" ValidationExpression="^[0-9]{0,9}$"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-xs-2">

                                        <!-- Telefono urgencia-->
                                        <label class=" pull-right">Teléfono de urgencia</label>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:TextBox ID="txt_telefono_urgencia" CssClass="caja2" runat="server" placeholder="Ingrese teléfono en caso de urgencia"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:RequiredFieldValidator ID="requerido_telemergencia" runat="server" ErrorMessage="Debe ingresar el teléfono de urgencia" ControlToValidate="txt_telefono_urgencia" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgProfes"> </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regex_tel_emergencia" runat="server" ErrorMessage="Formato inválido" ControlToValidate="txt_telefono_urgencia" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgProfes" ValidationExpression="^[0-9]{0,9}$"></asp:RegularExpressionValidator>
                                    </div>
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
                                        <asp:RequiredFieldValidator ID="requerido_mail" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar el mail" ValidationGroup="vgProfes"> </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="caracteres_mail" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Mail demasiado largo" ValidationExpression="^[\s\S]{0,80}$" ValidationGroup="vgProfes"> </asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="regex_mail" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de mail" ValidationExpression="^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$" ValidationGroup="vgProfes"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <!-- Calle y numero -->
                                <div class="row centered">
                                    <div class="col-xs-2">
                                        <label class="pull-left">Calle</label>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:TextBox ID="txt_calle" class="caja2" runat="server" placeholder="Ingrese calle"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="caracteres_calle" runat="server" ControlToValidate="txt_calle" CssClass="text-danger" Display="Dynamic" ErrorMessage="Calle demasiado larga" ValidationExpression="^[\s\S]{0,50}$"> </asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-xs-1">
                                        <label class="pull-right">Numero</label>
                                    </div>
                                    <div class="col-xs-1">
                                        <asp:TextBox ID="txt_numero" class="caja2" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Formato inválido" ControlToValidate="txt_numero" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgProfes" ValidationExpression="^[0-9]{0,9}$"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-xs-1">
                                        <label class="pull-right">Piso</label>
                                    </div>
                                    <div class="col-xs-1">
                                        <asp:TextBox ID="txt_piso" class="caja2" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Formato inválido" ControlToValidate="txt_piso" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgProfes" ValidationExpression="^[0-9]{0,9}$"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-xs-1">
                                        <label class=" pull-right">Dpto</label>
                                    </div>
                                    <div class="col-xs-1">
                                        <asp:TextBox ID="txt_nro_dpto" class="caja2" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="caracteres_departamento" runat="server" ControlToValidate="txt_nro_dpto" CssClass="text-danger" Display="Dynamic" ErrorMessage="Departamento demasiado largo" ValidationExpression="^[\s\S]{0,20}$" ValidationGroup="vgProfes"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <!-- Provincia -->
                                <div class="row centered">
                                    <div class="col-xs-2">
                                        <label class="pull-left">Provincia</label>
                                    </div>
                                    <div class="col-xs-3">
                                        <%--<asp:TextBox ID="txt_localidad" class="caja2" runat="server" placeholder="Ingrese localidad"></asp:TextBox>--%>
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
                                        <%--<asp:TextBox ID="txt_localidad" class="caja2" runat="server" placeholder="Ingrese localidad"></asp:TextBox>--%>
                                        <asp:DropDownList class="caja2" ID="ddl_localidad" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </asp:Panel>

                            <!--Boton-->
                            <div class="row centered">
                                <asp:Button ID="btn_cancelar" runat="server" CssClass="btn btn-default" Text="Cancelar" CausesValidation="false" OnClick="btn_cancelar_Click" />
                                <asp:Button ID="btn_guardar" runat="server" CssClass="btn btn-default" Text="Aceptar" OnClick="btn_guardar_click" ValidationGroup="vgProfes" />
                                <asp:Button ID="btn_ver_profes" runat="server" CssClass="btn btn-default" Text="Ver profesores" CausesValidation="False" OnClick="btn_ver_profes_Click" />
                            </div>
                        </div>
                    </div>

                </div>
                <!-- /row -->
            </asp:Panel>
        </div>
        <div id="grillawrap">
            <asp:Panel ID="pnl_mostrar_profes" runat="server">

                <div id="mostrarprofewrap">

                    <div class="container">
                        <div class="row mt centered">
                            <h1>LISTADO DE PROFESORES</h1>
                            <p>&nbsp;</p>
                        </div>
                        <div class="form-group ">
                            <!--Boton-->
                            <div class="row centered">
                                <strong>DNI</strong>
                                <asp:TextBox ID="txt_filtro_dni" runat="server"></asp:TextBox>
                                <asp:CompareValidator ID="mayor_dni0" runat="server" ControlToValidate="txt_filtro_dni" CssClass="text text-danger" Display="Dynamic" ErrorMessage="El DNI debe ser un valor mayor a 0" Operator="GreaterThan" Type="Integer" ValidationGroup="vgFiltro" ValueToCompare="0"></asp:CompareValidator>
                                <asp:CompareValidator ID="menor_dni0" runat="server" ControlToValidate="txt_filtro_dni" CssClass="text text-danger" Display="Dynamic" ErrorMessage="DNI demasiado largo" Operator="LessThan" Type="Integer" ValidationGroup="vgFiltro" ValueToCompare="2147483647"></asp:CompareValidator>
                                <asp:Button ID="btn_buscar_profe" runat="server" Text="Buscar profesores" OnClick="btn_buscar_profe_Click" ValidationGroup="vgFiltro" CssClass="btn btn-default" />

                                <asp:Button ID="btn_registro" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btn_registro_Click" Text="Volver a registrar" />

                                <asp:GridView ID="gvprofes" runat="server" CssClass="table" CellPadding="4" DataKeyNames="alu_dni" OnPageIndexChanging="gvprofes_PageIndexChanging" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay profes para mostrar" OnRowCommand="gvprofes_RowCommand">
                                    <Columns>

                                        <asp:BoundField DataField="alu_dni" HeaderText="D.N.I" SortExpression="dni" />
                                        <asp:BoundField DataField="alu_apellido" HeaderText="Apellido" SortExpression="apellido" />
                                        <asp:BoundField DataField="alu_nombre" HeaderText="Nombre" SortExpression="nombre" />
                                        <asp:ButtonField CommandName="eliminar" Text="Eliminar" />
                                        <asp:ButtonField CommandName="seleccionar" Text="Seleccionar" />
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>


        </div>
    </form>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
    <script type="text/javascript">
        $(document).ready(
            function () {
                $(".datepicker").datepicker({
                    dateFormat: "mm/dd/yy",
                    monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                    dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"]
                });
            }
        );
    </script>
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