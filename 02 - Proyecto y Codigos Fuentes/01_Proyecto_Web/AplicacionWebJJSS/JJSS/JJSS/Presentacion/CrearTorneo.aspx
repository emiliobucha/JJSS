﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site2.Master" AutoEventWireup="true" CodeBehind="CrearTorneo.aspx.cs" Inherits="JJSS.Presentacion.CrearTorneo" %>



<asp:Content ID="crearTorneoEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>

<asp:Content ID="crearTorneoContenido" ContentPlaceHolderID="cphContenido" runat="server">


    <asp:Panel ID="pnlFormulario" runat="server" >
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

                        <h1>ALTA DE TORNEO</h1>
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
                                    <asp:TextBox ID="txt_nombre" runat="server" onblur="ValidatorOnChange(event)" placeholder="Ingrese nombre" CssClass="caja2"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-lg-3  col-sm-12 col-xs-12 ">
                                    <asp:RequiredFieldValidator ID="requeridoNombre" CssClass="text-danger" runat="server" ErrorMessage="Debe ingresar un nombre de torneo" ControlToValidate="txt_nombre" EnableClientScript="false" ValidationGroup="vgTorneo"> </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="caracteres_nombre" runat="server" ControlToValidate="txt_nombre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Nombre demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgTorneo"> </asp:RegularExpressionValidator>
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
                                    <asp:Button ID="btn_mas" class="btn btn-default" runat="server" Text="+" OnClientClick="javascript:alert('Próximamente');" CausesValidation="false" OnClick="btn_mas_Click" />
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
                                    <asp:TextBox ID="dp_fecha" runat="server" CssClass="datepicker caja2" placeholder="Seleccione fecha del torneo"></asp:TextBox>
                                </div>

                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left">Hora a realizarse</label>
                                </div>

                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <asp:DropDownList ID="ddl_hora" runat="server" CssClass="caja2 ">
                                        <asp:ListItem>06:00</asp:ListItem>
                                        <asp:ListItem>07:00</asp:ListItem>
                                        <asp:ListItem>08:00</asp:ListItem>
                                        <asp:ListItem>09:00</asp:ListItem>
                                        <asp:ListItem>10:00</asp:ListItem>
                                        <asp:ListItem>11:00</asp:ListItem>
                                        <asp:ListItem Selected="True">12:00</asp:ListItem>
                                        <asp:ListItem>13:00</asp:ListItem>
                                        <asp:ListItem>14:00</asp:ListItem>
                                        <asp:ListItem>15:00</asp:ListItem>
                                        <asp:ListItem>16:00</asp:ListItem>
                                        <asp:ListItem>17:00</asp:ListItem>
                                        <asp:ListItem>18:00</asp:ListItem>
                                        <asp:ListItem>19:00</asp:ListItem>
                                        <asp:ListItem>20:00</asp:ListItem>
                                        <asp:ListItem>21:00</asp:ListItem>
                                        <asp:ListItem>22:00</asp:ListItem>
                                        <asp:ListItem>23:00</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12">
                                    <asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar fecha del torneo" ValidationGroup="vgTorneo"> </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rev_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de fecha de torneo" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" ValidationGroup="vgTorneo"> </asp:RegularExpressionValidator>
                                </div>
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
                                    <asp:TextBox ID="dp_fecha_cierre" placeholder="Seleccione fecha de cierre de inscripciones" CssClass="datepicker caja2" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <label class="pull-left">Hora de cierre</label>
                                </div>
                                <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                                    <asp:DropDownList ID="ddl_hora_cierre" CssClass="form-control" runat="server">
                                        <asp:ListItem>06:00</asp:ListItem>
                                        <asp:ListItem>07:00</asp:ListItem>
                                        <asp:ListItem>08:00</asp:ListItem>
                                        <asp:ListItem>09:00</asp:ListItem>
                                        <asp:ListItem>10:00</asp:ListItem>
                                        <asp:ListItem>11:00</asp:ListItem>
                                        <asp:ListItem Selected="True">12:00</asp:ListItem>
                                        <asp:ListItem>13:00</asp:ListItem>
                                        <asp:ListItem>14:00</asp:ListItem>
                                        <asp:ListItem>15:00</asp:ListItem>
                                        <asp:ListItem>16:00</asp:ListItem>
                                        <asp:ListItem>17:00</asp:ListItem>
                                        <asp:ListItem>18:00</asp:ListItem>
                                        <asp:ListItem>19:00</asp:ListItem>
                                        <asp:ListItem>20:00</asp:ListItem>
                                        <asp:ListItem>21:00</asp:ListItem>
                                        <asp:ListItem>22:00</asp:ListItem>
                                        <asp:ListItem>23:00</asp:ListItem>
                                        <asp:ListItem>00:00</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12">
                                    <asp:RequiredFieldValidator ID="rfv_fecha_cierre" runat="server" Display="Dynamic" CssClass="text-danger" ErrorMessage="Debe ingresar fecha de cierre de inscripciones" ControlToValidate="dp_fecha_cierre" ValidationGroup="vgTorneo"> </asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="val_fechas" runat="server" CssClass="text-danger" Display="Dynamic" ErrorMessage="La fecha de cierre de inscripción no puede ser mayor a la fecha de comienzo del torneo" OnServerValidate="val_fechas_ServerValidate" ValidationGroup="vgTorneo"> </asp:CustomValidator>
                                    <asp:CustomValidator ID="val_fecha_actual" runat="server" CssClass="text-danger" Display="Dynamic" ErrorMessage="La fecha no puede ser anterior a la actual" OnServerValidate="val_fecha_actual_ServerValidate" ValidationGroup="vgTorneo"> </asp:CustomValidator>
                                    <asp:RegularExpressionValidator ID="rev_fecha_cierre" runat="server" ControlToValidate="dp_fecha_cierre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido de fecha de cierre" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$" ValidationGroup="vgTorneo"> </asp:RegularExpressionValidator>
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
                                    <asp:TextBox ID="txt_precio_cat" CssClass="form-control" placeholder="Ingrese precio de inscripción categoria" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12">
                                    <asp:RequiredFieldValidator ID="requeridoPrecioCat" runat="server" Display="Dynamic" CssClass="text-danger" ErrorMessage="Debe ingresar precio de categoría" ControlToValidate="txt_precio_cat" ValidationGroup="vgTorneo"> </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regex_peso_cat" runat="server" ControlToValidate="txt_precio_cat" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido del precio" ValidationExpression="^[0-9]{0,16}(,?[0-9][0-9]{0,1})$" ValidationGroup="vgTorneo"> </asp:RegularExpressionValidator>
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
                                    <asp:TextBox ID="txt_precio_abs" CssClass="form-control" placeholder="Ingrese precio de inscripción absoluta" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-lg-3 col-sm-12 col-xs-12">
                                    <asp:RequiredFieldValidator ID="requeridoPrecioAbs" runat="server" Display="Dynamic" CssClass="text-danger" ErrorMessage="Debe ingresar precio de inscripción absoluta" ControlToValidate="txt_precio_abs" ValidationGroup="vgTorneo"> </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regex_precio_abs" runat="server" ControlToValidate="txt_precio_abs" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido del precio" ValidationExpression="^[0-9]{0,16}(,?[0-9][0-9]{0,1})$" ValidationGroup="vgTorneo"> </asp:RegularExpressionValidator>

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
                                            <asp:Image ID="Avatar" runat="server" Height="225px" CssClass="pull-left left" ImageUrl="~/Images/NoUser.jpg" Width="225px" />
                                        </div>
                                    </div>

                                </asp:Panel>
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
                        </asp:Panel>
                        
                        <!--Boton-->
                        <div class="row centered center-block">
                            <asp:Button ID="btn_aceptar" class="btn btn-default"  runat="server" Text="Aceptar" OnClick="btn_aceptar_Click1" OnClientClick="btn_aceptar_Click1" CausesValidation="true" ValidationGroup="vgTorneo" />
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
