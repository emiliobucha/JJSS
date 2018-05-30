<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarProfe.aspx.cs" Inherits="JJSS.Presentacion.RegistrarProfe" %>



<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <section id="registrarProfe" title="registrarProfe"></section>
    <form id="formRegProfe" runat="server">
        <div id="registrowrap">
            <div class="container">
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
            </div>


            <asp:Panel ID="pnlFormulario" runat="server">

                <div id="registrarProfewrap">

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="row centered justify-content-center">
                        <h1>Registro de Profesor</h1>
                    </div>

                     <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="container">                                              
                        
                        <div class="form-group border rounded p-4 ">

                            <asp:Panel ID="pnl_datos_personales" CssClass="panel panel-footer" runat="server">

                                <div class="row centered">
                                    <h2>Datos Personales</h2>
                                </div>
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                                <!--Nombre y Apellido-->
                                <div class="row p-1  pl-lg-5 pl-md-5">
                                    <!--Nombre-->
                                    <div class="col-lg-2 col-md-2 col-sm-12">
                                        <label class="text-left">Nombre <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col-lg-3 col-md-3 col-sm-12">
                                        <asp:TextBox ID="txt_nombres" runat="server" required="true" MaxLength="50" placeholder="Ingrese nombres" CssClass="caja2"></asp:TextBox>
                                    </div>

                                    <!--Apellido-->
                                    <div class="col-lg-2 col-md-2 col-sm-12 pl-lg-5 pl-md-5">
                                        <label class="text-left">Apellido <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col-lg-3 col-md-3 col-sm-12">
                                        <asp:TextBox ID="txt_apellido" runat="server" required="true" MaxLength="50" placeholder="Ingrese apellido" CssClass="caja2"></asp:TextBox>
                                    </div>
                                </div>


                                <!-- DNI-->
                                <div class="row p-1  pl-lg-5 pl-md-5">
                                    <div class="col-lg-2 col-md-2 col-sm-12">
                                        <label class="text-left">DNI <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col-lg-3 col-md-3 col-sm-12">
                                        <asp:TextBox ID="txtDni" class="caja2" required="true" type="number" min="1000000" max="100000000" runat="server" placeholder="Ingrese DNI"></asp:TextBox>
                                    </div>
                                </div>

                                <!--Fecha de nacimiento-->
                                <div class="row p-1  pl-lg-5 pl-md-5">
                                    <div class="col-lg-2 col-md-2 col-sm-12">
                                        <label class="text-left">Fecha de Nacimiento <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col-lg-3 col-md-3 col-sm-12">
                                        <!--SOMEE-->
                                            <%--<asp:TextBox ID="dp_fecha" runat="server" class="caja2" pattern="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" value="01/01/2000" required="true" placeholder="Seleccione fecha "></asp:TextBox>--%>
                                            <!--LOCAL-->
                                            <asp:TextBox ID="dp_fecha" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" value="01/01/2000" required="true" placeholder="Seleccione fecha "></asp:TextBox>
                                    </div>

                                    <div class="col-xs-3">
                                        <%--<asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar fecha" ValidationGroup="vgProfes"> </asp:RequiredFieldValidator>--%>
                                        <%--<asp:RegularExpressionValidator ID="rev_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inávlido de fecha" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" ValidationGroup="vgProfes"> </asp:RegularExpressionValidator>--%>
                                    </div>
                                </div>

                                <!--Sexo-->
                                <div class="row p-1  pl-lg-5 pl-md-5">
                                    <div class="col-lg-2 col-md-2 col-sm-12">
                                        <label class="text-left">Sexo <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col-lg-2 col-md-2 col-sm-12">
                                        <asp:RadioButtonList ID="rbSexo" runat="server" AutoPostBack="False">
                                            <asp:ListItem>Femenino</asp:ListItem>
                                            <asp:ListItem>Masculino</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                                <!--Foto-->

                                <div class="row p-1  pl-lg-5 pl-md-5">
                                    <div class="col-lg-2 col-md-2 col-sm-12">
                                        <label>Imagen</label></div>
                                    <div class="col col-auto">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:Image ID="Avatar" runat="server" Height="225px" ImageUrl="~/Images/NoUser.jpg" Width="225px" />
                                            <input id="avatarUpload" type="file" name="file" onchange="previewFile()" runat="server" />
                                            <%--<asp:FileUpload ID="avatarUpload" runat="server" />--%>
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
                                </div>
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                                <!-- Telefono -->
                                <div class="row p-1  pl-lg-5 pl-md-5">
                                    <div class="col-lg-2 col-md-2 col-sm-12">
                                        <label >Teléfono <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col-md-3 col-lg-3 col-sm-12 ">
                                        <asp:TextBox ID="txt_telefono" CssClass="caja2" required="true" type="number" max="100000000000000" min="1000000" runat="server" placeholder="Ingrese télefono"></asp:TextBox>
                                    </div>
                                    <div class=" col-lg-3 col-md-3 col-sm-12 pl-lg-5 pl-md-5">
                                        <!-- Telefono urgencia-->
                                        <label >Teléfono de urgencia <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col-md-3 col-lg-3 col-sm-12 ">
                                        <asp:TextBox ID="txt_telefono_urgencia" CssClass="caja2" required="true" type="number" max="100000000000000" min="1000000" runat="server" placeholder="Ingrese teléfono en caso de urgencia"></asp:TextBox>
                                    </div>
                                </div>


                                <!-- E-mail -->
                                <div class="row p-1  pl-lg-5 pl-md-5">
                                    <div class="col-lg-2 col-md-2 col-sm-12">
                                        <label class="text-left">E-mail</label>
                                    </div>
                                    <div class="col col-md-4 col-lg-4 col-sm-12">
                                        <asp:TextBox ID="txt_email" class="caja2" required="true" MaxLength="80" runat="server" placeholder="Ingrese e-mail"></asp:TextBox>
                                    </div>
                                    <div class="col col-md-1 col-lg-1 col-sm-12">
                                        <%--<asp:RequiredFieldValidator ID="requerido_mail" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar el mail" ValidationGroup="vgProfes"> </asp:RequiredFieldValidator>--%>
                                        <%--<asp:RegularExpressionValidator ID="caracteres_mail" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Mail demasiado largo" ValidationExpression="^[\s\S]{0,80}$" ValidationGroup="vgProfes"> </asp:RegularExpressionValidator>--%>
                                        <asp:RegularExpressionValidator ID="regex_mail" runat="server" ControlToValidate="txt_email" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de mail" ValidationExpression="^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$" ValidationGroup="vgProfes"> </asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <!-- Calle y numero -->
                                <div class="row p-1  pl-lg-5 pl-md-5">
                                    <div class="col-lg-2 col-md-2 col-sm-12">
                                        <label>Calle <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col-md-4 col-lg-4 col-sm-10">
                                        <asp:TextBox ID="txt_calle" class="caja2" type="text" MaxLength="50" runat="server" placeholder="Ingrese calle"></asp:TextBox>
                                    </div>

                                    <div class="col-lg-2 col-md-2 col-sm-12 pl-lg-5 pl-md-5">
                                        <label>Número <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col-md-1 col-lg-1 col-sm-10 col-xs-10">
                                        <asp:TextBox ID="txt_numero" type="number" min="0" max="100000" class="caja2" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row p-1  pl-lg-5 pl-md-5">
                                    <div class="col-lg-2 col-md-2 col-sm-12 ">
                                        <label>Piso <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col-md-1 col-lg-1 col-sm-10 col-xs-10">
                                        <asp:TextBox ID="txt_piso" class="caja2" type="number" min="0" max="100000" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-lg-1 col-md-1 col-sm-2">
                                        <label>Dpto <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col col-md-2 col-lg-2 col-sm-10 col-xs-10">
                                        <asp:TextBox ID="txt_nro_dpto" type="text" MaxLength="20" class="caja2" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-lg-1 col-md-1 col-sm-2">
                                        <label>Torre <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col-md-2 col-lg-2 col-sm-10 col-xs-10">
                                        <asp:TextBox ID="txt_torre" type="text" MaxLength="20" class="caja2" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <!-- Provincia -->
                                <div class="row p-1  pl-lg-5 pl-md-5">
                                    <div class="col-lg-2 col-md-2 col-sm-12 ">
                                        <label>Provincia <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col-md-3 col-lg-3 col-sm-10 col-xs-10">
                                        <asp:DropDownList class="caja2" ID="ddl_provincia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_provincia_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <!-- Localidad -->
                                <div class="row p-1  pl-lg-5 pl-md-5">
                                    <div class="col-lg-2 col-md-2 col-sm-12 ">
                                        <label>Localidad <a class="text-danger">*</a></label>
                                    </div>
                                    <div class="col col-md-3 col-lg-3 col-sm-10 col-xs-10">
                                        <asp:DropDownList class="caja2" ID="ddl_localidad" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </asp:Panel>

                            <!--Boton-->
                             <div class="row centered justify-content-center">
                                 <asp:Button ID="btn_guardar" runat="server" CssClass="btn btn-outline-dark" Text="Aceptar" OnClick="btn_guardar_click" ValidationGroup="vgProfes" />
                             </div>

                            <div class="row ">
                                <asp:Button ID="btn_ver_profes" runat="server" CssClass=" btn btn-link pull-left" Text="Ver profesores" formnovalidate="true" CausesValidation="False" OnClick="btn_ver_profes_Click" />
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

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="row centered justify-content-center">
                        <h1>Listado de Profesores</h1>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="container">


                        <div class="form-group border rounded p-4 ">

                            <!--Boton-->
                            <div class="row justify-content-center">

                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>DNI</strong>
                                </div>
                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:TextBox ID="txt_filtro_dni" type="number" CssClass="caja2" min="0" runat="server"></asp:TextBox>
                                </div>
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>Apellido</strong>
                                </div>
                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:TextBox ID="txt_filtro_apellido" CssClass="caja2" runat="server"></asp:TextBox>
                                </div>
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <asp:Button ID="btn_buscar_profe" runat="server" Text="Buscar" OnClick="btn_buscar_profe_Click" ValidationGroup="vgFiltro" CssClass="btn btn-outline-dark" />
                                </div>

                            </div>
                            <div class="row centered justify-content-center">
                                <asp:GridView ID="gvprofes" runat="server" CssClass="table" CellPadding="4" DataKeyNames="dni" OnPageIndexChanging="gvprofes_PageIndexChanging" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay profes para mostrar" OnRowCommand="gvprofes_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="apellido" HeaderText="Apellido" SortExpression="apellido" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                                        <asp:BoundField DataField="dni" HeaderText="D.N.I" SortExpression="dni" />

                                        <asp:ButtonField CommandName="eliminar" Text="Eliminar" HeaderText="Eliminar" />
                                        <asp:ButtonField CommandName="seleccionar" Text="Seleccionar" HeaderText="Seleccionar" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="row">
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <asp:Button ID="btn_registro" runat="server" CausesValidation="false" CssClass=" btn btn-link pull-left" formnovalidate="true" OnClick="btn_registro_Click" Text="Volver a registrar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>


        </div>

        <div class="row centered">
            <p>&nbsp;</p>
        </div>

        <div class=" container">
            <div class="row centered">
                <asp:Button ID="btn_Cancelar" runat="server" Text="Volver a inicio" CssClass=" btn btn-link pull-left" CausesValidation="false" formnovalidate="true" OnClick="btn_cancelar_Click" />
            </div>
        </div>

        <div class="row centered">
            <p>&nbsp;</p>
        </div>

    </form>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
    <!-- FECHA SOMEE-->
    <%--<script type="text/javascript">
        $(document).ready(
            function () {
                $(".datepicker").datepicker({
                    dateFormat: "mm/dd/yy",
                    monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                    dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"]
                });
            }
        );
    </script>--%>
    <!--LOCAL-->
    <script type="text/javascript">
        $(document).ready(
            function () {
                $(".datepicker").datepicker({
                    dateFormat: "dd/mm/yy",
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
