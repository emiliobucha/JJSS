<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site2.Master" AutoEventWireup="true" CodeBehind="CrearEvento.aspx.cs" Inherits="JJSS.Presentacion.CrearEvento" %>


<asp:Content ID="crearTorneoEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>

<asp:Content ID="crearTorneoContenido" ContentPlaceHolderID="cphContenido" runat="server">

    <section id="crearTorneo" title="crearTorneo"></section>
    <asp:Panel ID="pnlFormulario" runat="server">
        <div id="crearTorneowrap">
            <div class="container">
                <form id="form1" runat="server">

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


                    <div class="row mt centered">

                        <h1>ALTA DE EVENTO ESPECIAL</h1>
                        <p>&nbsp;</p>
                    </div>


                    <div class="form-group ">
                        <asp:Panel runat="server" CssClass="panel panel-default">

                             <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Nombre-->
                            <div class="row centered center-block">
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left 4">Nombre</label>
                                </div>
                                <div class="col-md-4 col-lg-4 col-sm-12 col-xs-12">
                                    <asp:TextBox ID="txt_nombre" required="true" runat="server" MaxLength="50" onblur="ValidatorOnChange(event)" placeholder="Ingrese nombre" CssClass="caja2"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-lg-3  col-sm-12 col-xs-12 ">
                                    <asp:RegularExpressionValidator ID="caracteres_nombre" runat="server" ControlToValidate="txt_nombre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Nombre demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgTorneo"> </asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--tipo evento-->
                            <div class="row centered center-block">
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left 4">Tipo de Evento</label>
                                </div>
                                <div class="col-md-4 col-lg-4 col-sm-10 col-xs-10">
                                    <asp:DropDownList ID="dll_tipo_evento" runat="server" CssClass="caja2 pull-right"></asp:DropDownList>
                                </div>
                                <div class="col-md-1 col-lg-1 col-sm-2 col-xs-2">
                                    <asp:Button ID="btn_tipo_evento" class="btn btn-default" runat="server" Text="+" formnovalidate="true" OnClientClick="javascript:alert('Próximamente');" CausesValidation="false" OnClick="btn_mas_Click" />
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Sedes-->
                            <div class="row centered center-block">
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left 4">Sede</label>
                                </div>
                                <div class="col-md-4 col-lg-4 col-sm-10 col-xs-10">
                                    <asp:DropDownList ID="ddl_sedes" runat="server" CssClass="caja2 pull-right"></asp:DropDownList>
                                </div>
                                <div class="col-md-1 col-lg-1 col-sm-2 col-xs-2">
                                    <asp:Button ID="btn_mas" class="btn btn-default" runat="server" Text="+" formnovalidate="true" OnClientClick="javascript:alert('Próximamente');" CausesValidation="false" OnClick="btn_mas_Click" />
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Fecha de evento-->
                            <div class="row centered center-block">
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left">Fecha a producirse</label>
                                </div>
                                <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12">
                                    <!--SOMEE-->
                                            <%--<asp:TextBox ID="dp_fecha" runat="server" class="caja2" pattern="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" required="true" placeholder="Seleccione fecha "></asp:TextBox>--%>
                                            <!--LOCAL-->
                                            <asp:TextBox ID="dp_fecha" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" required="true" placeholder="Seleccione fecha "></asp:TextBox>
                                </div>

                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left">Hora de inicio</label>
                                </div>

                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <asp:TextBox ID="txt_hora" required="true" runat="server" CssClass="caja2" type="time" placeholder="Ingrese la hora del evento"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12">
                                    <%--<asp:RegularExpressionValidator ID="rev_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de fecha de torneo" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" ValidationGroup="vgTorneo"> </asp:RegularExpressionValidator>--%>
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Fecha de cierre de inscripcion-->
                            <div class="row centered center-block">
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left ">Cierre de inscripcion</label>
                                </div>
                                <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12">
                                    <!--SOMEE-->
                                            <%--<asp:TextBox ID="dp_fecha_cierre" runat="server" class="caja2" pattern="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" required="true" placeholder="Seleccione fecha "></asp:TextBox>--%>
                                            <!--LOCAL-->
                                            <asp:TextBox ID="dp_fecha_cierre" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" required="true" placeholder="Seleccione fecha "></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left">Hora de cierre</label>
                                </div>
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <asp:TextBox ID="txt_hora_cierre" required="true" runat="server" CssClass="caja2" type="time" placeholder="Ingrese la hora de cierre de inscripciones"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12">
                                    <asp:CustomValidator ID="val_fechas" runat="server" CssClass="text-danger" Display="Dynamic" ErrorMessage="La fecha de cierre de inscripción no puede ser mayor a la fecha de comienzo del evento" OnServerValidate="val_fechas_ServerValidate" ValidationGroup="vgTorneo"> </asp:CustomValidator>
                                    <asp:CustomValidator ID="val_fecha_actual" runat="server" CssClass="text-danger" Display="Dynamic" ErrorMessage="La fecha no puede ser anterior a la actual" OnServerValidate="val_fecha_actual_ServerValidate" ValidationGroup="vgTorneo"> </asp:CustomValidator>
                                    <%--<asp:RegularExpressionValidator ID="rev_fecha_cierre" runat="server" ControlToValidate="dp_fecha_cierre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de fecha de cierre" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" ValidationGroup="vgTorneo"> </asp:RegularExpressionValidator>--%>
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Precio-->
                            <div class="row centered center-block">
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left left">Precio inscripción $</label>
                                </div>
                                <div class="col-md-4 col-lg-4 col-sm-12 col-xs-12">
                                    <asp:TextBox ID="txt_precio" required="true" min="0" max="1000000" type="number" placeholder="Ingrese el precio de la inscripción" step="0.1" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Foto-->
                            <div class="row centered center-block">
                                <asp:Panel ID="Panel1" runat="server">

                                    <div class="row centered center-block">
                                        <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                            <label class="pull-left left">Imagen</label>
                                        </div>
                                        <div class="col-md-4 col-lg-4 col-sm-12 col-xs-12">
                                            <input id="avatarUpload" type="file" name="file" onchange="previewFile()" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row centered center-block">
                                        <div class="col-md-2 col-lg-2 hidden-sm hidden-xs">
                                            <p>&nbsp;</p>
                                        </div>

                                        <div class="col-md-8 col-lg-8 col-sm-12 col-xs-12">
                                            <asp:Image ID="Avatar" runat="server" CssClass="pull-left left" Height="225px" ImageUrl="~/Images/NoUser.jpg" Width="225px" />
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>
                        </asp:Panel>

                        <!--Boton-->
                        <div class="row centered center-block   ">
                            <asp:Button ID="btn_aceptar" class="btn btn-default" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click" OnClientClick="btn_aceptar_Click1" ValidationGroup="vgTorneo" />
                            <asp:Button ID="btn_Cancelar" runat="server" Text="Volver a inicio" CssClass="btn-link pull-left" CausesValidation="false" formnovalidate="true" OnClick="btn_cancelar_Click" />
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
