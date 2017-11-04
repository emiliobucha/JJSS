<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="InscripcionEvento.aspx.cs" Inherits="JJSS.Presentacion.InscripcionEvento" %>

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
            <div id="crearTorneowrap">
                <div class="container">
                    <div class="row mt centered">
                        <div>
                            <asp:Label ID="lbl_Inscripcion" runat="server" Text="INSCRIPCIÓN DE EVENTOS" Font-Size="XX-Large" CssClass=""></asp:Label>
                        </div>

                        <form id="form1" runat="server">
                            <div class="form-group ">
                                <div class="row centered">
                                    <p>&nbsp;</p>
                                </div>

                                <!--Elegir torneo-->
                                <asp:Panel ID="pnl_elegirEvento" CssClass="panel panel-default" runat="server">
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row centered center-block">
                                        <div class="col-md-2 hidden-xs hidden-sm"></div>
                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                            <label class="pull-left">Evento:</label>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                            <asp:DropDownList ID="ddl_evento" class="caja2" runat="server"></asp:DropDownList>
                                        </div>

                                        <div class="col-sm-12 col-xs-12 hidden-lg hidden-md">
                                            <p>&nbsp;</p>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                            <asp:Button ID="btnAceptarEvento" runat="server" Text="Aceptar" CssClass="btn btn-default" OnClick="btnAceptarEvento_Click" OnClientClick="this.disabled=true" UseSubmitBehavior="False" />
                                        </div>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                </asp:Panel>


                                <!--PANEL DE INFORMACION DEL TORNEO-->
                                <asp:Panel ID="pnl_InfoTorneo" CssClass="panel panel-default" runat="server">

                                    <!--Nombre-->
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>
                                    
                                    <div class="row centered center-block">
                                        <div class="col-md-2 hidden-xs hidden-sm"></div>
                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">                                            
                                            <label class="pull-left">Información del Evento:&nbsp;</label>
                                            <asp:Label ID="lbl_nombreEvento" CssClass=" pull-left left" runat="server" Text="" ></asp:Label>
                                        </div>
                                    </div>


                                    <!--Fecha-->
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row centered center-block">
                                        <div class="col-md-2 hidden-sm hidden-xs"></div>
                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                            <label class="pull-left">Fecha del evento:&nbsp; </label>
                                            <asp:Label ID="lbl_FechaDeEvento" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label1" CssClass="pull-left" runat="server" Text=", "></asp:Label>
                                            <asp:Label ID="lbl_HoraEvento" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label2" CssClass="pull-left" runat="server" Text=" hs"></asp:Label>
                                        </div>                                        
                                    </div>

                                    <!--Cierre Inscripciones-->
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row centered center-block">
                                        <div class="col-md-2 hidden-sm hidden-xs"></div>
                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                            <label class="pull-left">Cierre de Inscripción:&nbsp;</label>
                                            <asp:Label ID="lbl_FechaCierreInscripcion" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label3" CssClass="pull-left" runat="server" Text=", "></asp:Label>
                                            <asp:Label ID="lbl_HoraCierre" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label8" CssClass="pull-left" runat="server" Text=" hs"></asp:Label>
                                        </div>
                                    </div>

                                    <!--direccion de la sede-->
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row centered center-block">
                                        <div class="col-md-2 hidden-sm hidden-xs"></div>
                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                            <label class="pull-left">Sede:&nbsp;</label>
                                            <asp:Label ID="lbl_sede" CssClass="pull-left left" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row  center-block">
                                        <div class="col-md-2 hidden-sm hidden-xs"></div>

                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                            <label class="pull-left">Dirección:&nbsp;</label>
                                            <asp:Label ID="lbl_direccion_sede" CssClass="pull-left left" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>

                                    <!--Precio-->
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row centered center-block">
                                        <div class="col-md-2 hidden-sm hidden-xs"></div>
                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">

                                            <label class="pull-left">Costo:&nbsp; </label>
                                            <asp:Label ID="Label4" CssClass="pull-left" runat="server" Text="$"></asp:Label>
                                            <asp:Label ID="lbl_CostoInscripcion" CssClass="pull-left" runat="server" Text=""></asp:Label>

                                            <div class="row centered">
                                                <p>&nbsp;</p>
                                            </div>
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

                               

                                <!-- PANEL DNI-->
                                <asp:Panel ID="pnl_dni" CssClass="panel panel-default" runat="server" Visible="true">
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <!--Ingresar DNI-->

                                    <div class="row centered center-block">
                                        <div class="col-md-2"></div>
                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                            <label class="pull-left">DNI del participante:</label>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                            <asp:TextBox ID="txtDni" autofocus="true" required="true"  type="number" class="form-control" min="1" max="1000000000" runat="server" placeholder="Ingrese DNI" ValidationGroup="grupoDni"></asp:TextBox>
                                        </div>

                                        <div class="col-sm-12 col-xs-12 hidden-lg hidden-md">
                                            <p>&nbsp;</p>
                                        </div>

                                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-default" ValidationGroup="grupoDni" OnClick="btnBuscar_Click" OnClientClick="this.disabled=true" UseSubmitBehavior="False" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:RequiredFieldValidator ID="requeridoDni" runat="server" ErrorMessage="Debe ingresar el DNI" ValidationGroup="grupoDni" ControlToValidate="txtDni" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <%--<asp:RegularExpressionValidator ID="regex_dni" runat="server" ErrorMessage="Formato inválido" ControlToValidate="txtDni" CssClass="text-danger" Display="Dynamic" ValidationGroup="grupoDni" ValidationExpression="^[0-9]{0,9}$"></asp:RegularExpressionValidator>--%>
                                        </div>
                                    </div>
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                </asp:Panel>

                                <!--PANEL DE INSCRIPCION-->

                                <asp:Panel ID="pnl_Inscripcion" CssClass="panel panel-default" runat="server" Visible="true">

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <div class="row centered center-block">
                                        <div class="col centered">
                                            <asp:Label ID="Label6" runat="server" Text="Datos del Participante" Font-Bold="true" Font-Size="Large"></asp:Label>
                                        </div>
                                    </div>


                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <!--Ingresar nombre-->

                                    <div class="row centered center-block">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left">Nombre:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txt_nombre" required="true" maxLength="50" class="caja2" runat="server" placeholder="Ingrese nombre"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <%--<asp:RequiredFieldValidator ID="requeridoNombre" runat="server" ErrorMessage="Debe ingresar el nombre" ControlToValidate="txt_nombre" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgDatos"> </asp:RequiredFieldValidator>--%>
                                            <%--<asp:RegularExpressionValidator ID="caracteres_nombre" runat="server" ControlToValidate="txt_nombre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Nombre demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgDatos"> </asp:RegularExpressionValidator>--%>
                                        </div>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>
                                    <!--Ingresar apellido-->

                                    <div class="row centered center-block">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left">Apellido:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txt_apellido"  maxLength="50" required="true" class="caja2" runat="server" placeholder="Ingrese apellido"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <%--<asp:RequiredFieldValidator ID="requeridoApellido" runat="server" ErrorMessage="Debe ingresar el apellido" ControlToValidate="txt_apellido" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgDatos"> </asp:RequiredFieldValidator>--%>
                                            <%--<asp:RegularExpressionValidator ID="caracteres_apellido" runat="server" ControlToValidate="txt_apellido" CssClass="text-danger" Display="Dynamic" ErrorMessage="Apellido demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgDatos"> </asp:RegularExpressionValidator>--%>
                                        </div>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>
                                    <!--Sexo-->

                                    <div class="row centered center-block">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left">Sexo:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:RadioButtonList ID="rbSexo" required="true" runat="server" AutoPostBack="False" OnSelectedIndexChanged="rbSexo_SelectedIndexChanged">
                                                <asp:ListItem>Femenino</asp:ListItem>
                                                <asp:ListItem>Masculino</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <asp:CustomValidator ID="val_sexo" runat="server" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe seleccionar un sexo" ValidationGroup="vgDatos" ControlToValidate="rbSexo" OnServerValidate="val_sexo_ServerValidate"></asp:CustomValidator>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>


                                    <!--Fecha de nacimiento-->
                                    <div class="row centered center-block">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left text-left">Fecha de Nacimiento</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="dp_fecha" required="true" pattern="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" runat="server" CssClass="datepicker caja2" placeholder="Seleccione fecha "></asp:TextBox>
                                        </div>

                                        <div class="col-md-3">
                                            <%--<asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar fecha" ValidationGroup="vgDatos"> </asp:RequiredFieldValidator>--%>
                                            <%--<asp:RegularExpressionValidator ID="rev_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de fecha" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" ValidationGroup="vgDatos"> </asp:RegularExpressionValidator>--%>
                                        </div>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>


                                    <!--Boton Aceptar-->
                                    <div class="row centered center-block">
                                        <asp:Button ID="btn_aceptar" type="submit" class="btn btn-default" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click" ValidationGroup="vgDatos" />
                                        <asp:Button ID="btn_cancelar" class="btn btn-default" runat="server" Text="Cancelar" OnClick="btn_cancelar_Click" CausesValidation="false"  OnClientClick="this.disabled=true" UseSubmitBehavior="False" />
                                    </div>

                                      <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>
                                </asp:Panel>

                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

<asp:Content ID="cphP" ContentPlaceHolderID="cphP" runat="server">
    <script type="text/javascript">

</script>
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
        function btn_pagar_click() {
            location.href = "EventoPago.aspx";
        }

        function btn_no_click() {
            location.href = "InscripcionEvento.aspx";
        }
    </script>
</asp:Content>
