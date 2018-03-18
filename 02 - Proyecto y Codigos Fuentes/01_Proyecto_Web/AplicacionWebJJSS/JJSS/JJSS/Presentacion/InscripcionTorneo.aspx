<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site2.Master" AutoEventWireup="true" CodeBehind="InscripcionTorneo.aspx.cs" Inherits="JJSS.InscripcionTorneo" %>




<asp:Content ID="crearTorneoEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>

<asp:Content ID="crearTorneoContenido" ContentPlaceHolderID="cphContenido" runat="server">

    <div class="container">
        <section id="inscripcionTorneo" title="inscripcionTorneo"></section>


        <asp:Panel ID="pnl_mensaje_exito" runat="server" Visible="false">
            <div class="col-md-2 hidden-xs"></div>
            <div class="col-md-8 col-xs-12 col-sm-12">
                <div class="alert alert-success alert-dismissible" role="alert">
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <div>
                        <a class="ui-icon ui-icon-check"></a>
                        <strong>
                            <asp:Label ID="lbl_exito" runat="server" Text=""></asp:Label>
                            <br />
                        </strong>
                    </div>

                    <asp:Panel ID="pnl_comprobante" runat="server" Visible="False">
                        <a class="ui-icon ui-icon-check"></a>
                        <strong>
                            Descargar comprobante <a  runat="server" id="btn_descargar">Aquí</a>
                            <br />
                        </strong>
                    </asp:Panel>

                    <br />
                    <asp:Panel ID="pnl_pago" runat="server" Visible="False">
                        <div class="text-center">
                            ¿Desea pagar ahora?<br />
                            <asp:Button ID="btn_pagar" runat="server" class="btn btn-default" Text="Si" OnClientClick="btn_pagar_click()" />
                            <asp:Button ID="btn_no" runat="server" class="btn btn-default" Text="No" OnClientClick="btn_no_click()" />
                        </div>
                    </asp:Panel>
                </div>
            </div>



            <div class="row centered">
                <p>&nbsp;</p>
            </div>
        </asp:Panel>





        <asp:Panel ID="pnl_mensaje_error" runat="server" Visible="false">
            <div class="col-md-2 hidden-xs"></div>
            <div class="col-md-8 col-xs-12 col-sm-12">
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


        <asp:Panel ID="pnlFormulario" runat="server" CssClass="col-sm-12 col-xs-12 col-md-10 col-lg-10">

            <div class="row centered">
                <p>&nbsp;</p>
            </div>

            <div id="crearTorneowrap">
                <div class="container">
                    <div class="row mt centered">
                        <div>
                            <asp:Label ID="lbl_Inscripcion" runat="server" Text="INSCRIPCIÓN DE TORNEO" Font-Size="XX-Large" CssClass=""></asp:Label>
                        </div>

                        <form id="form1" runat="server">
                            <div class="form-group ">
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                                <!--Elegir torneo-->
                                <asp:Panel ID="pnl_elegirTorneo" CssClass="panel panel-default" runat="server">


                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row centered">
                                        <div class="col-md-2 hidden-xs hidden-sm"></div>
                                        <div class="col-md-2 col-sm-6 col-xs-6">
                                            <label class="pull-left">Torneo:</label>
                                        </div>
                                        <div class="col-md-3 col-sm-6 col-xs-6">
                                            <asp:DropDownList ID="ddl_torneos" class="caja2" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-1 col-sm-12 col-xs-12">
                                            <asp:Button ID="btnAceptarTorneo" formnovalidate="true" CausesValidation="false" runat="server" Text="Aceptar" CssClass="btn btn-default" OnClick="btnAceptarTorneo_Click" />
                                        </div>
                                    </div>

                                </asp:Panel>
                                <!--PANEL DE INFORMACION DEL TORNEO-->

                                <asp:Panel ID="pnl_InfoTorneo" CssClass="panel panel-default" runat="server">

                                    <!--Nombre-->
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row centered">
                                        <div class="col centered col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                            <asp:Label ID="Label5" runat="server" Text="Información del Evento: " Font-Bold="true" Font-Size="Large"></asp:Label>
                                            <asp:Label ID="lbl_NombreTorneo" CssClass="centered" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
                                        </div>
                                    </div>

                                    <!--Direccion-->
                                    <!--  <div class="row centered"><p>&nbsp;</p></div>

                                    <div class="row centered">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <label class="pull-left">Direccion del evento:&nbsp; </label>
                                        <asp:Label ID="lbl_Direccion" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                -->
                                    <!--Fecha-->
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row centered">
                                        <div class="col-md-2 hidden-sm hidden-xs"></div>
                                        <div class="col-md-4 ">
                                            <label class="pull-left">Fecha del torneo:&nbsp; </label>
                                            <asp:Label ID="lbl_FechaDeTorneo" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label1" CssClass="pull-left" runat="server" Text=", "></asp:Label>
                                            <asp:Label ID="lbl_HoraTorneo" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label2" CssClass="pull-left" runat="server" Text=" hs"></asp:Label>
                                        </div>
                                    </div>

                                    <!--Cierre Inscripciones-->
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row centered">
                                        <div class="col-md-2 hidden-sm hidden-xs"></div>
                                        <div class="col-md-6">
                                            <label class="pull-left">Cierre de Inscripción:&nbsp;</label>
                                            <asp:Label ID="lbl_FechaCierreInscripcion" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label7" CssClass="pull-left" runat="server" Text=", "></asp:Label>
                                            <asp:Label ID="lbl_HoraCierreTorneo" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label9" CssClass="pull-left" runat="server" Text=" hs"></asp:Label>
                                        </div>
                                    </div>
                                    <!--direccion de la sede-->
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row centered">
                                        <div class="col-md-2 hidden-sm hidden-xs"></div>
                                        <div class="col-md-6">
                                            <label class="pull-left">Sede:&nbsp;</label>
                                            <asp:Label ID="lbl_sede" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row centered">
                                        <div class="col-md-2 hidden-sm hidden-xs"></div>
                                        <div class="col-md-6">
                                            <label class="pull-left">Dirección:&nbsp;</label>
                                            <asp:Label ID="lbl_direccion_sede" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>


                                    <!--Precio-->
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row centered">
                                        <div class="col-md-2 hidden-sm hidden-xs"></div>
                                        <div class="col-md-5 col-lg-10 col-sm-10">

                                            <label class="pull-left">Costo Categoría:&nbsp; </label>
                                            <asp:Label ID="Label4" CssClass="pull-left" runat="server" Text="$"></asp:Label>
                                            <asp:Label ID="lbl_CostoInscripcion" CssClass="pull-left" runat="server" Text=""></asp:Label>

                                            <div class="row centered">
                                                <p>&nbsp;</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row centered">
                                        <div class="col-md-2 hidden-sm hidden-xs"></div>
                                        <div class="col-md-5 col-lg-10 col-sm-10">

                                            <label class="pull-left">Costo Absoluto:&nbsp;</label>
                                            <asp:Label ID="Label3" CssClass="pull-left" runat="server" Text="$ "></asp:Label>
                                            <asp:Label ID="lbl_CostoInscripcionAbsoluto" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>


                                    <div class="row centered">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                </asp:Panel>

                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                                <!-- PANEL DNI-->
                                <asp:Panel ID="pnl_dni" CssClass="panel panel-default" runat="server" Visible="false">
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <!--Ingresar DNI-->

                                    <div class="row centered">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left">DNI del participante:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtDni" class="caja2" required="true" min="1000000" max="1000000000" type="number" runat="server" placeholder="Ingrese DNI" ValidationGroup="grupoDni"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-default" ValidationGroup="grupoDni" OnClick="btnBuscarDni_Click"  />
                                        </div>
                                    </div>
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                </asp:Panel>

                                <!--PANEL DE INSCRIPCION-->

                                <asp:Panel ID="pnl_Inscripcion" CssClass="panel panel-default" runat="server" Visible="false">

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row centered">
                                        <div class="col centered">
                                            <asp:Label ID="Label6" runat="server" Text="Datos del Participante" Font-Bold="true" Font-Size="Large"></asp:Label>
                                        </div>
                                    </div>


                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <!--Ingresar nombre-->

                                    <div class="row centered">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left">Nombre:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txt_nombre" class="caja2" required="true" MaxLength="50" runat="server" placeholder="Ingrese nombre"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>
                                    <!--Ingresar apellido-->

                                    <div class="row centered">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left">Apellido:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txt_apellido" required="true" MaxLength="50" class="caja2" runat="server" placeholder="Ingrese apellido"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>
                                    <!--Sexo-->

                                    <div class="row centered">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left">Sexo:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:RadioButtonList ID="rbSexo" runat="server" AutoPostBack="False" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                <asp:ListItem>Femenino</asp:ListItem>
                                                <asp:ListItem>Masculino</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>


                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>
                                    <!--Peso-->

                                    <div class="row centered">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left">Peso:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox class="caja2" ID="txt_peso" runat="server" required="true" type="number" min="1" max="200" step="0.01" placeholder="Ingrese peso"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>


                                    <!--Fecha de nacimiento-->
                                    <div class="row centered">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left text-left">Fecha de Nacimiento</label>
                                        </div>
                                        <div class="col-md-3">
                                            <!--SOMEE-->
                                            <%--<asp:TextBox ID="dp_fecha" runat="server" class="caja2" pattern="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" value="01/01/2000" required="true" placeholder="Seleccione fecha "></asp:TextBox>--%>
                                            <!--LOCAL-->
                                            <asp:TextBox ID="dp_fecha" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" value="01/01/2000" required="true" placeholder="Seleccione fecha "></asp:TextBox>
                                        </div>

                                        <div class="col-md-3">
                                            <%--<asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar fecha" ValidationGroup="vgDatos"> </asp:RequiredFieldValidator>--%>
                                            <%--<asp:RegularExpressionValidator ID="rev_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de fecha" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" ValidationGroup="vgDatos"> </asp:RegularExpressionValidator>--%>
                                        </div>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <!--Faja-->

                                    <div class="row centered">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left">Faja:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList class="caja2" ID="ddl_fajas" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>
                                     <!--Tipo inscripcion-->

                                    <div class="row centered">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left">Tipo de inscripción:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:RadioButtonList ID="rb_tipo" runat="server" AutoPostBack="False">
                                                <asp:ListItem Selected="true">Categoría</asp:ListItem>
                                                <asp:ListItem>Absoluto</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>


                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>



                                    <!--Boton Aceptar-->
                                    <div class="row centered">
                                        
                                        <asp:Button ID="btn_aceptar" type="submit" class="btn btn-default" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click" ValidationGroup="vgDatos" />
                                        
                                    </div>
                                </asp:Panel>
                                <asp:Button ID="btn_Cancelar" runat="server" Text="Volver a inicio" CssClass="btn-link pull-left" formnovalidate="true" CausesValidation="false" OnClick="btn_Cancelar_Click" />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="inscripcionCorrecta" hidden="true" runat="server">
            <div class="container">
                <p>
                    &nbsp;<asp:Button ID="btn_cod_barra" class="btn btn-default" runat="server" Text="Imprimir codigo de barra" Height="32px" />
                </p>
            </div>

        </asp:Panel>
    </div>
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
        );
    </script>
    <script type="text/javascript">
        function btn_pagar_click() {
            location.href = "TorneoPago.aspx";
        }

        function btn_no_click() {
            location.href = "InscripcionTorneo.aspx";
        }
    </script>
</asp:Content>
