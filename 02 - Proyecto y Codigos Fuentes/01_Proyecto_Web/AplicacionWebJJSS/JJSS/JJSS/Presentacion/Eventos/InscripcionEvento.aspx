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
                        <strong>Descargar comprobante <a runat="server" id="btn_descargar">Aquí</a>
                            <br />
                        </strong>
                    </asp:Panel>

                    <br />
                    <asp:Panel ID="pnl_pago" runat="server" Visible="False">
                        <div class="text-center">
                            ¿Desea pagar ahora?<br />
                            <asp:Button ID="btn_pagar" runat="server" class="btn btn-default" Text="Si" formnovalidate="true" OnClientClick="btn_pagar_click()" />
                            <asp:Button ID="btn_no" runat="server" class="btn btn-default" Text="No" formnovalidate="true" OnClientClick="btn_no_click()" />
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
    </div>


    <asp:Panel ID="pnlFormulario" runat="server" CssClass="justify-content-center">
        <div id="crearTorneowrap">
            <div class="container">
                <div>
                    <p>&nbsp;</p>
                </div>

                <div class="row centered justify-content-center">
                    <h1>Inscripción de Eventos</h1>
                </div>
                <div>
                    <p>&nbsp;</p>
                </div>
                <form id="form1" runat="server">

                    <!--Elegir Evento-->


                    <asp:Panel ID="pnl_elegirEvento" CssClass="panel panel-default border rounded p-2" runat="server">
                        <div class="form-group">
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <div class="row justify-content-center">
                                <div class="col-lg-1 col-md-2 col-sm-8">
                                    <label>Evento:</label>
                                </div>
                                <div class="col col-lg-3 col-md-3 col-sm-8">
                                    <asp:DropDownList ID="ddl_evento" class="caja2" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col col-lg-2 col-md-2 col-sm-12">
                                    <asp:Button ID="btnAceptarEvento" runat="server" Text="Aceptar" CssClass="btn btn-outline-dark" formnovalidate="true" OnClick="btnAceptarEvento_Click" OnClientClick="this.disabled=true" UseSubmitBehavior="False" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <div>
                        <p>&nbsp;</p>
                    </div>

                    <!--PANEL DE INFORMACION DEL TORNEO-->
                    <asp:Panel ID="pnl_InfoTorneo" CssClass="panel panel-default justify-content-center border rounded p-2" runat="server">

                        <!--Nombre-->
                        <div>
                            <p>&nbsp;</p>
                        </div>

                        <div class="row centered ">
                            <div class="col centered col-lg-12 col-md-12 col-sm-12">
                                <asp:Label ID="Label5" runat="server" Text="Información del Evento: " CssClass=" h3 " Font-Size="Large"></asp:Label>
                                <asp:Label ID="lbl_nombreEvento" CssClass="centered h3 " runat="server" Text="" Font-Size="Large"></asp:Label>
                            </div>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--Fecha-->
                        <div class="row centered justify-content-center">
                            <div class="col-md-2 hidden-sm hidden-xs"></div>
                            <div class="col-md-6 col-sm-10 ">
                                <label class="pull-left h6">Fecha del evento:&nbsp; </label>
                                <asp:Label ID="lbl_FechaDeEvento" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                <asp:Label ID="Label1" CssClass="pull-left" runat="server" Text=", "></asp:Label>
                                <asp:Label ID="lbl_HoraEvento" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                <asp:Label ID="Label2" CssClass="pull-left" runat="server" Text=" hs"></asp:Label>
                            </div>
                        </div>


                        <!--Cierre Inscripciones-->

                        <div class="row centered justify-content-center">
                            <div class="col-md-2 hidden-sm hidden-xs"></div>
                            <div class="col-md-6 col-sm-10 ">
                                <label class="pull-left h6">Cierre de Inscripción:&nbsp;</label>
                                <asp:Label ID="lbl_FechaCierreInscripcion" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                <asp:Label ID="Label3" CssClass="pull-left" runat="server" Text=", "></asp:Label>
                                <asp:Label ID="lbl_HoraCierre" CssClass="pull-left" runat="server" Text=""></asp:Label>
                                <asp:Label ID="Label8" CssClass="pull-left" runat="server" Text=" hs"></asp:Label>
                            </div>
                        </div>

                        <!--direccion de la sede-->
                        <div class="row centered justify-content-center">
                            <div class="col-md-2 hidden-sm hidden-xs"></div>
                            <div class="col-md-6 col-sm-10 ">
                                <label class="pull-left h6">Sede:&nbsp;</label>
                                <asp:Label ID="lbl_sede" CssClass="pull-left" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row centered justify-content-center">
                            <div class="col-md-2 hidden-sm hidden-xs"></div>
                            <div class="col-md-6 col-sm-10 ">
                                <label class="pull-left h6">Dirección:&nbsp;</label>
                                <asp:Label ID="lbl_direccion_sede" CssClass="pull-left" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <!--Precio-->
                        <div class="row centered justify-content-center">
                            <div class="col-md-2 hidden-sm hidden-xs"></div>
                            <div class="col-md-6 col-sm-10">

                                <label class="pull-left h6">Costo Categoría:&nbsp; </label>
                                <asp:Label ID="Label4" CssClass="pull-left" runat="server" Text="$"></asp:Label>
                                <asp:Label ID="lbl_CostoInscripcion" CssClass="pull-left" runat="server" Text=""></asp:Label>


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
                    <asp:Panel ID="pnl_dni" CssClass="panel panel-default justify-content-center border rounded p-2" runat="server" Visible="true">

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <div class="col centered">
                            <asp:Label ID="Label7" runat="server" Text="Identificación" CssClass=" h3 " Font-Size="Large"></asp:Label>
                        </div>

                        <div>
                            <p>&nbsp;</p>
                        </div>


                        <!--Ingresar DNI-->

                        <div class="row centered justify-content-center">


                            <!--Ingresar Tipo-->
                            <div class="col-md-2 col-xl-auto">
                                <label class="pull-left">Tipo: <a class="text-danger">*</a></label>
                            </div>
                            <div class="col-md-3 col-xl-auto">
                                <asp:DropDownList ID="ddl_tipo" class="caja2" runat="server" placeholder="Ingrese Tipo" ValidationGroup="grupoDni"></asp:DropDownList>

                            </div>


                            <!--Ingresar Numero-->
                            <div class="col-md-2 col-xl-auto">
                                <label class="pull-left">Número: <a class="text-danger">*</a></label>
                            </div>
                            <div class="col-md-3 col-xl-auto">

                                <asp:TextBox ID="txtDni" class="caja2" required="true" runat="server" placeholder="Ingrese Número" ValidationGroup="grupoDni"></asp:TextBox>

                            </div>


                            <!--Boton-->
                            <div class="col-md-1 col-xl-auto">
                                <asp:Button ID="btnBuscar" runat="server" formnovalidate="true" UseSubmitBehaviour="false" CausesValidation="false" Text="Buscar" CssClass="btn btn-outline-dark" ValidationGroup="grupoDni" OnClick="btnBuscarDni_Click" />
                            </div>
                        </div>

                        <div >
                            <p>&nbsp;</p>
                        </div>

                        <div class=" p-4 ">
                            <p class="text-danger pull-right  " style="font-size: small">* Campo requerido</p>
                        </div>

                    </asp:Panel>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>


                    <!--PANEL DE INSCRIPCION-->
                    <asp:Panel ID="pnl_Inscripcion" CssClass="panel panel-default justify-content-center border rounded p-2" runat="server" Visible="true">

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <div class="row centered">
                            <div class="col centered">
                                <asp:Label ID="Label6" runat="server" Text="Datos del Participante" CssClass=" h3 " Font-Size="Large"></asp:Label>
                            </div>
                        </div>


                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--Ingresar nombre-->

                        <div class="row centered">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">
                                <label class="pull-left">Nombre: <a class="text-danger">*</a></label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt_nombre" class="caja2" required="true" pattern="[A-Za-z]*" MaxLength="50" runat="server" placeholder="Ingrese nombre"></asp:TextBox>

                            </div>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--Ingresar apellido-->

                        <div class="row centered">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">
                                <label class="pull-left">Apellido: <a class="text-danger">*</a></label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt_apellido" required="true" MaxLength="50" pattern="[A-Za-z]*" class="caja2" runat="server" placeholder="Ingrese apellido"></asp:TextBox>

                            </div>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>
                        <!--Sexo-->

                        <div class="row centered">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">
                                <label class="pull-left">Sexo: </label>
                            </div>
                            <div class="col-md-2">
                                <asp:RadioButtonList ID="rbSexo" runat="server" AutoPostBack="False" OnSelectedIndexChanged="rbSexo_SelectedIndexChanged">
                                    <asp:ListItem>&nbsp;Femenino</asp:ListItem>
                                    <asp:ListItem>&nbsp;Masculino</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                        </div>


                        <div class="row centered">
                            <p>&nbsp;</p>
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
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>
                        <!--Fecha de nacimiento-->
                        <div class="row centered">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">
                                <label class="pull-left text-left">Fecha de Nacimiento: <a class="text-danger">*</a></label>
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


                        <!--Boton Aceptar-->
                        <div class="row centered justify-content-center">
                            <asp:Button ID="btn_aceptar" type="submit" class="btn btn-outline-dark " runat="server" Text="Aceptar" OnClick="btn_aceptar_Click" ValidationGroup="vgDatos" />
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                    </asp:Panel>
                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <asp:Button ID="btn_Cancelar" runat="server" Text="Volver" CssClass="btn btn-link" formnovalidate="true" CausesValidation="false" OnClick="btn_Cancelar_Click" />
                </form>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

<asp:Content ID="cphP" ContentPlaceHolderID="cphP" runat="server">
    <script type="text/javascript">

</script>
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
            location.href = "EventoPago.aspx";
        }

        function btn_no_click() {
            location.href = "InscripcionEvento.aspx";
        }
    </script>
</asp:Content>