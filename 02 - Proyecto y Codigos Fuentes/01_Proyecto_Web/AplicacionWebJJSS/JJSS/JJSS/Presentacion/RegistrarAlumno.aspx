<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="~/Presentacion/RegistrarAlumno.aspx.cs" Inherits="JJSS.Presentacion.RegistrarAlumno" %>
<asp:Content ID="registrarAlumnoMenu" ContentPlaceHolderID="cphMenu" runat="server">
    <a href="Inicio.aspx" class="smoothScroll">Home</a>	
</asp:Content>

<asp:Content ID="registrarAlumnoEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>

<asp:Content ID="registrarAlumnoContenido" ContentPlaceHolderID="cphContenido" runat="server">

    <section id="registrarAlumno" title="registrarAlumno"></section>

    <asp:Panel ID="pnlFormulario" runat="server">

        <div id="registrarAlumnowrap">

            <div class="container">
                <div class="row mt centered">
                    <h1>FORMULARIO DE REGISTRO DE ALUMNO</h1>
                    <p>&nbsp;</p>
                </div>

                <form id="formRegAlumno" runat="server">
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass=" text text-danger" runat="server" ErrorMessage="Debe ingresar el apellido" ControlToValidate="txt_apellido"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="requeridoNombre" CssClass="text text-danger" runat="server" ErrorMessage="Debe ingresar el nombre" ControlToValidate="txt_nombres"></asp:RequiredFieldValidator>
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
                                    <asp:RequiredFieldValidator ID="requeridoDni" runat="server" ErrorMessage="Debe ingresar el DNI" ControlToValidate="txtDni" CssClass="text-danger"></asp:RequiredFieldValidator>
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
                                    <asp:TextBox ID="txt_telefono" CssClass="caja2" runat="server" placeholder="Ingrese telefono"></asp:TextBox>
                                </div>
                                <div class="col-xs-2">

                                    <!-- Telefono urgencia-->
                                    <label class=" pull-right">Teléfono de urgencia</label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:TextBox ID="txt_telefono_urgencia" CssClass="caja2" runat="server" placeholder="Ingrese telefono en caso de urgencia"></asp:TextBox>
                                </div>
                                <div class="col-xs-3">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe ingresar el telefono" ControlToValidate="txt_telefono" CssClass="text-danger"></asp:RequiredFieldValidator>
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
                            </div>
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!-- Calle y numero -->
                            <div class="row centered">
                                <div class="col-xs-2">
                                    <label class="pull-left">Calle</label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:TextBox ID="txt_calle" class="caja2" runat="server" placeholder="Ingrese calle"></asp:TextBox>
                                </div>
                                <div class="col-xs-1">
                                    <label class="pull-right">Numero</label>
                                </div>
                                <div class="col-xs-1">
                                    <asp:TextBox ID="txt_numero" class="caja2" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-xs-1">
                                    <label class="pull-right">Piso</label>
                                </div>
                                <div class="col-xs-1">
                                    <asp:TextBox ID="txt_piso" class="caja2" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-xs-1">
                                    <label class=" pull-right">Dpto</label>
                                </div>
                                <div class="col-xs-1">
                                    <asp:TextBox ID="txt_nro_dpto" class="caja2" runat="server"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!-- Localidad -->
                            <div class="row centered">
                                <div class="col-xs-2">
                                    <label class="pull-left">Localidad</label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:TextBox ID="txt_localidad" class="caja2" runat="server" placeholder="Ingrese localidad"></asp:TextBox>
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

                </form>

            </div>

        </div>
        <!-- /row -->
    </asp:Panel>


</asp:Content>

<asp:Content ID="cphP" ContentPlaceHolderID="cphP" runat="server">
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
