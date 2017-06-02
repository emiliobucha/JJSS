﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarAlumno.aspx.cs" Inherits="JJSS.Presentacion.RegistrarAlumno" %>

<asp:Content ID="registrarAlumnoMenu" ContentPlaceHolderID="cphMenu" runat="server">
    <a href="Inicio.aspx" class="smoothScroll">Home</a>
    <a href="#regitro" class="smoothScroll">Registrar Alumno</a>
    <a href="#grilla" class="smoothScroll">Mostrar Alumnos</a>
</asp:Content>

<asp:Content ID="registrarAlumnoEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>

<asp:Content ID="registrarAlumnoContenido" ContentPlaceHolderID="cphContenido" runat="server">

    <section id="registrarAlumno" title="registrarAlumno"></section>
    <form id="formRegAlumno" runat="server">
        <div id="registrowrap">
            <asp:Panel ID="pnlFormulario" runat="server">

                <div id="registrarAlumnowrap">

                    <div class="container">
                        <div class="row mt centered">
                            <h1>FORMULARIO DE REGISTRO DE ALUMNO</h1>
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
                                        <label class="pull-left">Nombres</label>
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
                                        <asp:RequiredFieldValidator ID="requerido_apellido" CssClass=" text text-danger" runat="server" ErrorMessage="Debe ingresar el apellido" ControlToValidate="txt_apellido"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="requeridoNombre" CssClass="text text-danger" runat="server" ErrorMessage="Debe ingresar el nombre" ControlToValidate="txt_nombres"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="caracteres_apellido" runat="server" ControlToValidate="txt_apellido" CssClass="text-danger" Display="Dynamic" ErrorMessage="Apellido demasiado largo" ValidationExpression="^[\s\S]{0,50}$"></asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="caracteres_nombre" runat="server" ControlToValidate="txt_nombres" CssClass="text-danger" Display="Dynamic" ErrorMessage="Nombre demasiado largo" ValidationExpression="^[\s\S]{0,50}$"></asp:RegularExpressionValidator>
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
                                        <asp:RequiredFieldValidator ID="requeridoDni" runat="server" ErrorMessage="Debe ingresar el DNI" ControlToValidate="txtDni" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="mayor_dni" CssClass="text text-danger" Display="Dynamic"  runat="server" ControlToValidate="txtDni" Type="Integer" ErrorMessage="El DNI debe ser un valor mayor a 0" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                                        <asp:CompareValidator ID="menor_dni" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txtDni" Type="Integer" ErrorMessage="El DNI debe ser un valor menor" ValueToCompare="2147483647" Operator="LessThan"></asp:CompareValidator>
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
                                        <asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar fecha"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Fecha mal Ingresada" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$"></asp:RegularExpressionValidator>
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
                                        <asp:RadioButtonList ID="rbSexo" runat="server" AutoPostBack="False" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe ingresar el teléfono" ControlToValidate="txt_telefono" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="numerico_telefono" CssClass="text text-danger" Display="Dynamic"  runat="server" ControlToValidate="txt_telefono" Type="Double" ErrorMessage="El telefono tiene que ser un valor numérico" Operator="DataTypeCheck"></asp:CompareValidator>
                                        <asp:CompareValidator ID="mayor_telefono" CssClass="text text-danger" Display="Dynamic"  runat="server" ControlToValidate="txt_telefono" Type="Double" ErrorMessage="El teléfono debe ser un valor mayor a 0" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                                        <asp:CompareValidator ID="menor_telefono" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_telefono" Type="Double" ErrorMessage="El teléfono debe ser un valor menor" ValueToCompare="2147483647" Operator="LessThan"></asp:CompareValidator>
                                    </div>
                                    <div class="col-xs-2">

                                        <!-- Telefono urgencia-->
                                        <label class=" pull-right">Teléfono de urgencia</label>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:TextBox ID="txt_telefono_urgencia" CssClass="caja2" runat="server" placeholder="Ingrese teléfono en caso de urgencia"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:CompareValidator ID="numerico_telemergencia" CssClass="text text-danger" Display="Dynamic"  runat="server" ControlToValidate="txt_telefono_urgencia" Type="Double" ErrorMessage="El telefono tiene que ser un valor numérico" Operator="DataTypeCheck"></asp:CompareValidator>
                                        <asp:CompareValidator ID="mayor_telemergencia" CssClass="text text-danger" Display="Dynamic"  runat="server" ControlToValidate="txt_telefono_urgencia" Type="Double" ErrorMessage="El teléfono de urgencia debe ser un valor mayor a 0" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                                        <asp:CompareValidator ID="menor_telemergencia" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_telefono_urgencia" Type="Double" ErrorMessage="El teléfono de urgencia debe ser un valor menor" ValueToCompare="2147483647" Operator="LessThan"></asp:CompareValidator>
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
                                        <asp:RequiredFieldValidator ID="requerido_mail" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar el mail"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="caracteres_nombre0" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Mail demasiado largo" ValidationExpression="^[\s\S]{0,80}$"></asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="regex_mail" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido" ValidationExpression="^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$"></asp:RegularExpressionValidator>
                                    </div>
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
                                        <asp:CompareValidator ID="mayor_numero" CssClass="text text-danger" Display="Dynamic"  runat="server" ControlToValidate="txt_numero" Type="Integer" ErrorMessage="El numéro debe ser un valor mayor a 0" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                                        <asp:CompareValidator ID="menor_numero" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_numero" Type="Integer" ErrorMessage="El numéro debe ser un valor menor" ValueToCompare="2147483647" Operator="LessThan"></asp:CompareValidator>
                                    </div>
                                    <div class="col-xs-1">
                                        <label class="pull-right">Piso</label>
                                    </div>
                                    <div class="col-xs-1">
                                        <asp:TextBox ID="txt_piso" class="caja2" runat="server"></asp:TextBox>
                                        <asp:CompareValidator ID="mayor_piso" CssClass="text text-danger" Display="Dynamic"  runat="server" ControlToValidate="txt_piso" Type="Integer" ErrorMessage="El numéro debe ser un valor mayor a 0" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                                        <asp:CompareValidator ID="menor_piso" CssClass="text text-danger" Display="Dynamic" runat="server" ControlToValidate="txt_piso" Type="Integer" ErrorMessage="El numéro debe ser un valor menor" ValueToCompare="2147483647" Operator="LessThan"></asp:CompareValidator>
                                    </div>
                                    <div class="col-xs-1">
                                        <label class=" pull-right">Dpto</label>
                                    </div>
                                    <div class="col-xs-1">
                                        <asp:TextBox ID="txt_nro_dpto" class="caja2" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="caracteres_departamento" runat="server" ControlToValidate="txt_nro_dpto" CssClass="text-danger" Display="Dynamic" ErrorMessage="Departamento demasiado largo" ValidationExpression="^[\s\S]{0,20}$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <!-- Provincia -->
                                <div class="row centered">
                                    <div class="col-xs-2">
                                        <label class="pull-left">Provincia</label>
                                    </div>
                                    <div class="col-xs-3">
                                        <%--<asp:TextBox ID="txt_localidad" class="caja2" runat="server" placeholder="Ingrese localidad"></asp:TextBox>--%>
                                        <asp:DropDownList class="caja2" ID="ddl_provincia" runat="server" OnSelectedIndexChanged="ddl_provincia_SelectedIndexChanged">
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

                            <asp:Panel ID="pnl_datos_academicos" CssClass="panel panel-footer" runat="server">
                                <div class="row centered">
                                    <h2>Datos Academicos</h2>
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>
                                </div>

                                <!--Faja y Categoria-->
                                <div class="row centered">
                                    <!--Faja-->
                                    <div class="col-xs-2">
                                        <label class="pull-left">Faja</label>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:DropDownList class="caja2" ID="ddl_fajas" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <!--Categoria-->
                                    <div class="col-xs-2">
                                        <label class="pull-rigth">Categoria</label>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:DropDownList class="caja2" ID="ddl_categoria" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                            </asp:Panel>


                            <!--Boton-->
                            <div class="row centered">
                                <asp:Button ID="btn_cancelar" runat="server" CssClass="btn btn-default" Text="Cancelar" CausesValidation="false" />
                                <asp:Button ID="btn_guardar" runat="server" CssClass="btn btn-default" Text="Guardar" OnClick="btn_guardar_click" />
                            </div>
                        </div>
                    </div>

                </div>
                <!-- /row -->
            </asp:Panel>
        </div>
        <div id="grillawrap">
            <asp:Panel ID="pnl_mostrar_alumnos" runat="server">

                <div id="mostrarAlumnowrap">

                    <div class="container">
                        <div class="row mt centered">
                            <h1>LISTADO DE ALUMNOS</h1>
                            <p>&nbsp;</p>
                        </div>
                        <div class="form-group ">
                            <!--Boton-->
                            <div class="row centered">
                                <strong>DNI a buscar:</strong>
                                <asp:TextBox ID="txt_filtro_dni" runat="server"></asp:TextBox>
                                <asp:Button ID="btn_buscar_alumno" runat="server" Text="Buscar alumnos" OnClick="btn_buscar_alumno_Click" CausesValidation="false" CssClass="btn btn-default" />

                                <asp:GridView ID="gvAlumnos" runat="server" CssClass="table" CellPadding="4" DataKeyNames="alu_dni" OnSelectedIndexChanged="gvAlumnos_SelectedIndexChanged" OnPageIndexChanging="gvAlumnos_PageIndexChanging" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                                    <Columns>

                                        <asp:CommandField HeaderText="Eliminar" SelectText="Eliminar" ShowCancelButton="True" ShowDeleteButton="False" ShowSelectButton="True" />
                                        <asp:BoundField DataField="alu_dni" HeaderText="D.N.I" SortExpression="dni" />
                                        <asp:BoundField DataField="alu_apellido" HeaderText="Apellido" SortExpression="apellido" />
                                        <asp:BoundField DataField="alu_nombre" HeaderText="Nombre" SortExpression="nombre" />
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

<asp:Content ID="cphP" ContentPlaceHolderID="cphP" runat="server">
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