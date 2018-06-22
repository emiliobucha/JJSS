<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarAlumno.aspx.cs" Inherits="JJSS.Presentacion.RegistrarAlumno" %>



<asp:Content ID="registrarAlumnoEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>

<asp:Content ID="registrarAlumnoContenido" ContentPlaceHolderID="cphContenido" runat="server">

    <section id="registrarAlumno" title="registrarAlumno"></section>
    <form id="formRegAlumno" runat="server">


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

                    <div id="registrarAlumnowrap">

                        <div class="row mt centered justify-content-center ">
                            <h1 class="centered">Registro de Alumnos</h1>
                        </div>


                        <div class="container">

                            <div class="form-group border rounded p-4 ">

                                <asp:Panel ID="pnl_datos_personales" CssClass="panel panel-footer" runat="server">
                                    <div>
                                        <p>&nbsp;</p>
                                    </div>
                                    <div class="row centered">
                                        <h2>Datos Personales</h2>
                                    </div>
                                    <div>
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
                                        
                                        <div class="col-md-2 col-xl-auto">
                                            <label class="pull-left">Tipo: <a class="text-danger">*</a></label>
                                        </div>
                                        <div class="col-md-3 col-xl-auto">
                                            <asp:DropDownList ID="ddl_tipo" class="caja2" runat="server" placeholder="Tipo Documento" ValidationGroup="grupoDni"></asp:DropDownList>

                                        </div>
                                        

                                        <div class="col-lg-2 col-md-2 col-sm-12">
                                            <label class="text-left">Número <a class="text-danger">*</a></label>
                                        </div>
                                        <div class="col col-lg-3 col-md-3 col-sm-12">
                                            <asp:TextBox ID="txtDni" class="caja2" required="true" runat="server" placeholder="Ingrese DNI"></asp:TextBox>
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
                                    
                                 
                                    <div class="row centered">
                                        <div class="col-md-2"></div>
                                        <!--Ingresar Nacionalidad-->
                                        <div class="col-md-2">
                                            <label class="pull-left">País: <a class="text-danger">*</a></label>
                                        </div>
                                        <div class="col-md-4 col-xl-4">
                                            <asp:DropDownList ID="ddl_nacionalidad" class="caja2" runat="server" placeholder="Ingrese Nacionalidad"></asp:DropDownList>

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
                                            <label>Imagen</label>
                                        </div>
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
                                            <label>Teléfono <a class="text-danger">*</a></label>
                                        </div>
                                        <div class="col col-md-3 col-lg-3 col-sm-12 ">
                                            <asp:TextBox ID="txt_telefono" CssClass="caja2" required="true" type="number" max="100000000000000" min="1000000" runat="server" placeholder="Ingrese télefono"></asp:TextBox>
                                        </div>
                                        <div class=" col-lg-3 col-md-3 col-sm-12 pl-lg-5 pl-md-5">
                                            <!-- Telefono urgencia-->
                                            <label>Teléfono de urgencia <a class="text-danger">*</a></label>
                                        </div>
                                        <div class="col col-md-3 col-lg-3 col-sm-12 ">
                                            <asp:TextBox ID="txt_telefono_urgencia" CssClass="caja2" required="true" type="number" max="100000000000000" min="1000000" runat="server" placeholder="Ingrese teléfono en caso de urgencia"></asp:TextBox>
                                        </div>
                                    </div>


                                    <!-- E-mail -->
                                    <div class="row p-1  pl-lg-5 pl-md-5">
                                        <div class="col-lg-2 col-md-2 col-sm-12">
                                            <label class="text-left">E-mail<a class="text-danger">*</a></label>
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
                                            <label>Calle </label>
                                        </div>
                                        <div class="col col-md-4 col-lg-4 col-sm-10">
                                            <asp:TextBox ID="txt_calle" class="caja2" type="text" MaxLength="50" runat="server" placeholder="Ingrese calle"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-12 pl-lg-5 pl-md-5">
                                            <label>Número </label>
                                        </div>
                                        <div class="col col-md-1 col-lg-1 col-sm-10 col-xs-10">
                                            <asp:TextBox ID="txt_numero" type="number" min="0" max="100000" class="caja2" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row p-1  pl-lg-5 pl-md-5">
                                        <div class="col-lg-2 col-md-2 col-sm-12 ">
                                            <label>Piso </label>
                                        </div>
                                        <div class="col col-md-1 col-lg-1 col-sm-10 col-xs-10">
                                            <asp:TextBox ID="txt_piso" class="caja2" type="number" min="0" max="100000" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-1 col-md-1 col-sm-2">
                                            <label>Dpto </label>
                                        </div>
                                        <div class="col col col-md-2 col-lg-2 col-sm-10 col-xs-10">
                                            <asp:TextBox ID="txt_nro_dpto" type="text" MaxLength="20" class="caja2" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-1 col-md-1 col-sm-2">
                                             <label>Torre </label>
                                        </div>
                                        <div class="col col-md-2 col-lg-2 col-sm-10 col-xs-10">
                                            <asp:TextBox ID="TextBox1" type="text" MaxLength="20" class="caja2" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <!-- Provincia -->
                                    <div class="row p-1  pl-lg-5 pl-md-5">
                                        <div class="col-lg-2 col-md-2 col-sm-12 ">
                                            <label>Provincia </label>
                                        </div>
                                        <div class="col col-md-3 col-lg-3 col-sm-10 col-xs-10">
                                            <asp:DropDownList class="caja2" ID="ddl_provincia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_provincia_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <!-- Localidad -->
                                    <div class="row p-1  pl-lg-5 pl-md-5">
                                        <div class="col-lg-2 col-md-2 col-sm-12 ">
                                            <label>Localidad</label>
                                        </div>
                                        <div class="col col-md-3 col-lg-3 col-sm-10 col-xs-10">
                                            <asp:DropDownList class="caja2" ID="ddl_localidad" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </asp:Panel>

                                <!--Boton-->
                                <div class="row centered justify-content-center">

                                    <asp:Button ID="btn_guardar" runat="server" CssClass="btn btn-outline-dark" Text="Aceptar" OnClick="btn_guardar_click" ValidationGroup="vgAlumnos" />
                                </div>
                                
                                <div class=" p-2 ">
                                    <p class="text-danger pull-right " style="font-size: small">* Campo requerido</p>
                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- /row -->
                </asp:Panel>
        </form>

</asp:Content>

<asp:Content ID="cphP" ContentPlaceHolderID="cphP" runat="server">
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
        ); txt_nro_dpto
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