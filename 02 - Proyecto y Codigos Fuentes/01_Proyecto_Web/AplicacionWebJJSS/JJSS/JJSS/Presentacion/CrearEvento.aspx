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
                        <!--Nombre-->
                        <div class="row centered">
                            <div class="col-md-2">
                                <label class="pull-left 4">Nombre</label>
                            </div>
                            <div class="col-md-5">
                                <asp:TextBox ID="txt_nombre" required="true" runat="server" MaxLength="50" onblur="ValidatorOnChange(event)" placeholder="Ingrese nombre" CssClass="caja2"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:RegularExpressionValidator ID="caracteres_nombre" runat="server" ControlToValidate="txt_nombre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Nombre demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgTorneo"> </asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--tipo evento-->
                        <div class="row centered">
                            <div class="col-md-2">
                                <label class="pull-left 4">Tipo de Evento</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="dll_tipo_evento" runat="server" CssClass="caja2 pull-right"></asp:DropDownList>
                                <asp:Button ID="btn_tipo_evento" class="btn btn-default" runat="server" Text="+" formnovalidate="true"  OnClientClick="javascript:alert('Próximamente');" CausesValidation="false" OnClick="btn_mas_Click" />
                            </div>
                            <%--<div class="col-md-1">
                                <asp:Button ID="btn_mas" class="btn btn-default" runat="server" Text="+"  OnClientClick="javascript:alert('Próximamente');" CausesValidation="false" OnClick="btn_mas_Click" />
                            </div>--%>
                        </div>

                        <!--Sedes-->
                        <div class="row centered">
                            <div class="col-md-2">
                                <label class="pull-left 4">Sede</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddl_sedes" runat="server" CssClass="caja2 pull-right"></asp:DropDownList>
                                <asp:Button ID="btn_mas" class="btn btn-default" runat="server" Text="+" formnovalidate="true" OnClientClick="javascript:alert('Próximamente');" CausesValidation="false" OnClick="btn_mas_Click" />
                            </div>
                            <%--<div class="col-md-1">
                                <asp:Button ID="btn_mas" class="btn btn-default" runat="server" Text="+"  OnClientClick="javascript:alert('Próximamente');" CausesValidation="false" OnClick="btn_mas_Click" />
                            </div>--%>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--Fecha de torneo-->
                        <div class="row centered">
                            <div class="col-md-2">
                                <label class="pull-left">Fecha a producirse</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="dp_fecha" required="true" runat="server" CssClass="datepicker caja2" placeholder="Seleccione fecha del evento" pattern="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$"></asp:TextBox>
                            </div>

                            <div class="col-md-2">
                                <label class="pull-left">Hora de inicio</label>
                            </div>

                            <div class="col-md-2">
                                <asp:TextBox ID="txt_hora" required="true" runat="server" CssClass="caja2" type="time" placeholder="Ingrese la hora del evento"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <%--<asp:RegularExpressionValidator ID="rev_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de fecha de torneo" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" ValidationGroup="vgTorneo"> </asp:RegularExpressionValidator>--%>
                            </div>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--Fecha de cierre de inscripcion-->
                        <div class="row centered">
                            <div class="col-md-2">
                                <label class="pull-left ">Cierre de inscripcion</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="dp_fecha_cierre" required="true" placeholder="Seleccione fecha de cierre de inscripciones" CssClass="datepicker caja2" runat="server" pattern="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label class="pull-left">Hora de cierre</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txt_hora_cierre" required="true" runat="server" CssClass="caja2" type="time" placeholder="Ingrese la hora de cierre de inscripciones"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:CustomValidator ID="val_fechas" runat="server" CssClass="text-danger" Display="Dynamic" ErrorMessage="La fecha de cierre de inscripción no puede ser mayor a la fecha de comienzo del evento" OnServerValidate="val_fechas_ServerValidate" ValidationGroup="vgTorneo"> </asp:CustomValidator>
                                <asp:CustomValidator ID="val_fecha_actual" runat="server" CssClass="text-danger" Display="Dynamic" ErrorMessage="La fecha no puede ser anterior a la actual" OnServerValidate="val_fecha_actual_ServerValidate" ValidationGroup="vgTorneo"> </asp:CustomValidator>
                                <%--<asp:RegularExpressionValidator ID="rev_fecha_cierre" runat="server" ControlToValidate="dp_fecha_cierre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de fecha de cierre" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" ValidationGroup="vgTorneo"> </asp:RegularExpressionValidator>--%>
                            </div>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--Precio-->
                        <div class="row centered">
                            <div class="col-md-3">
                                <label class="pull-left left">Precio de inscripción $</label>
                            </div>
                            <div class="col-md-3">
                               <asp:TextBox ID="txt_precio" required="true" min="0" max="1000000" type="number" placeholder="Ingrese el precio de la inscripción" step ="0.1" CssClass="form-control" runat="server" ></asp:TextBox>
                            </div>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--Foto-->
                        <div class="row centered">
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
                       

                        <!--Boton-->
                        <div class="row centered">
                            <asp:Button ID="btn_aceptar" class="btn btn-default" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click" OnClientClick="btn_aceptar_Click1" ValidationGroup="vgTorneo" />
                            <asp:Button ID="btn_cancelar" class="btn btn-default" runat="server" Text="Cancelar" formnovalidate="true" OnClick="btn_cancelar_Click" CausesValidation="false" />
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
