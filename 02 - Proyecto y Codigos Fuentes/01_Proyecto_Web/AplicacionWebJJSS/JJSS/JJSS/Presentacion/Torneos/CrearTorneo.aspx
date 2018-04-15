<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="CrearTorneo.aspx.cs" Inherits="JJSS.Presentacion.CrearTorneo" %>



<asp:Content ID="crearTorneoEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>

<asp:Content ID="crearTorneoContenido" ContentPlaceHolderID="cphContenido" runat="server">


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

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="row centered justify-content-center">
                        <h1 class=" centered ">Alta de Torneo</h1>
                        <%--<asp:Label ID="lbl_Inscripcion" runat="server" Text="INSCRIPCIÓN DE TORNEO" CssClass=" h2 font-weight-light"></asp:Label>--%>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="form-group ">
                        <asp:Panel runat="server" CssClass="panel panel-default border rounded p-2">
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Nombre-->
                            <div class="row centered center-block">
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left 4">Nombre</label>
                                </div>
                                <div class="col-md-4 col-lg-4 col-sm-12 col-xs-12">
                                    <asp:TextBox ID="txt_nombre" runat="server" required="true" MaxLength="50" onblur="ValidatorOnChange(event)" placeholder="Ingrese nombre" CssClass="caja2"></asp:TextBox>
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
                                    <a class="btn btn-outline-dark" runat="server" formnovalidate="true" href="~/Administracion/CrearSede.aspx" >+ </a>
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Fecha de torneo-->
                            <div class="row centered center-block">
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left">Fecha a realizarse</label>
                                </div>
                                <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12">
                                    <!--SOMEE-->
                                    <%--<asp:TextBox ID="dp_fecha" runat="server" class="caja2" pattern="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" required="true" placeholder="Seleccione fecha "></asp:TextBox>--%>
                                    <!--LOCAL-->
                                    <asp:TextBox ID="dp_fecha" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" required="true" placeholder="Seleccione fecha "></asp:TextBox>
                                </div>

                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left">Hora a realizarse</label>
                                </div>

                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <asp:TextBox ID="txt_hora" required="true" type="time" runat="server" CssClass="caja2" placeholder="Seleccione hora del torneo"></asp:TextBox>
                                </div>
                                <%--                                <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12">
                                    <asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar fecha del torneo" ValidationGroup="vgTorneo"> </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rev_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de fecha de torneo" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" ValidationGroup="vgTorneo"> </asp:RegularExpressionValidator>
                                </div>--%>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Fecha de cierre de inscripcion-->
                            <div class="row centered center-block">
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left ">Cierre de inscripción</label>
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
                                    <asp:TextBox ID="txt_hora_cierre" required="true" type="time" runat="server" CssClass="caja2" placeholder="Seleccione hora de cierre de inscripción"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12">

                                    <asp:CustomValidator ID="val_fechas" runat="server" CssClass="text-danger" Display="Dynamic" ErrorMessage="La fecha de cierre de inscripción no puede ser mayor a la fecha de comienzo del torneo" OnServerValidate="val_fechas_ServerValidate" ValidationGroup="vgTorneo"> </asp:CustomValidator>
                                    <asp:CustomValidator ID="val_fecha_actual" runat="server" CssClass="text-danger" Display="Dynamic" ErrorMessage="La fecha no puede ser anterior a la actual" OnServerValidate="val_fecha_actual_ServerValidate" ValidationGroup="vgTorneo"> </asp:CustomValidator>
                                    <%--<asp:RegularExpressionValidator ID="rev_fecha_cierre" runat="server" ControlToValidate="dp_fecha_cierre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de fecha de cierre" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" ValidationGroup="vgTorneo"> </asp:RegularExpressionValidator>--%>
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Precio por categoria-->
                            <div class="row centered center-block">
                                <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12">
                                    <label class="pull-left left">Precio de inscripción categoria $</label>
                                </div>
                                <div class="col-md-4 col-lg-4 col-sm-12 col-xs-12">
                                    <asp:TextBox ID="txt_precio_cat" required="true" type="number" min="0" max="100000" step="0.01" CssClass="form-control" placeholder="Ingrese precio de inscripción categoria" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Precio absoluto-->
                            <div class="row centered center-block">
                                <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12">
                                    <label class="pull-left">Precio de inscripción absoluta $</label>
                                </div>
                                <div class="col-md-4 col-lg-4 col-sm-12 col-xs-12">
                                    <asp:TextBox ID="txt_precio_abs" required="true" type="number" min="0" max="100000" step="0.01" CssClass="form-control" placeholder="Ingrese precio de inscripción absoluta" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Foto-->

                            <div class="row centered center-block">
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left">Imagen</label>
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
                                    <asp:Image ID="Avatar" runat="server" Height="225px" CssClass="pull-left left" ImageUrl="~/Images/NoUser.jpg" Width="225px" />
                                </div>
                            </div>


                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Comentarios-->
                            <div class="row centered center-block">
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left">Comentarios </label>
                                </div>
                                <div class="col-md-6 col-lg-6 col-sm-12 col-xs-12">
                                    <textarea class="form-control" rows="3"></textarea>
                                </div>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Boton-->

                            <div class="row centered justify-content-center">
                                <asp:Button ID="btn_aceptar" class=" btn btn-outline-dark" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click1" OnClientClick="btn_aceptar_Click1" CausesValidation="true" ValidationGroup="vgTorneo" />
                            </div>

                             <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                        </asp:Panel>


                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <asp:Button ID="btn_Cancelar" runat="server" Text="Volver" formnovalidate="true" CssClass="btn btn-link" CausesValidation="false" OnClick="btn_Cancelar_Click" />
                    </div>

                </form>

            </div>

        </div>
        <!-- /row -->
    </asp:Panel>


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
