<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="CrearTorneo.aspx.cs" Inherits="JJSS.Presentacion.CrearTorneo" %>

<asp:Content ID="crearTorneoMenu" ContentPlaceHolderID="cphMenu" runat="server">
    <a href="Inicio.aspx" class="smoothScroll">Home</a>			
    <a href="#crearTorneo" class="smoothScroll">Crear</a>
</asp:Content>

<asp:Content ID="crearTorneoEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
 
</asp:Content>


<asp:Content ID="crearTorneoContenido"  ContentPlaceHolderID="cphContenido" runat="server">

    <section id="crearTorneo" title="crearTorneo"></section>
    <asp:Panel ID="pnlFormulario" runat="server">
        <div id="crearTorneowrap">
            <div class="container">
                <div class="row mt centered">

                    <h1>FORMULARIO DE ALTA</h1>
                    <p>&nbsp;</p>
                </div>

                <form id="form1" runat="server">
                    <div class="form-group ">
                        <!--Nombre-->
                        <div class="row centered">
                            <div class="col-md-2">
                                <label class="pull-left 4">Nombre</label>
                            </div>
                            <div class="col-md-5">
                                <asp:TextBox ID="txt_nombre" runat="server" onblur="ValidatorOnChange(event)" placeholder="Ingrese nombre" CssClass="caja2"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:RequiredFieldValidator ID="requeridoNombre" CssClass="text-danger" runat="server" ErrorMessage="Debe ingresar un nombre" ControlToValidate="txt_nombre" EnableClientScript="false"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="caracteres_nombre" runat="server" ControlToValidate="txt_nombre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Nombre demasiado largo" ValidationExpression="^[\s\S]{0,50}$"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="row centered">    <p>&nbsp;</p> </div>

                        <!--Sedes-->
                        <div class="row centered">
                            <div class="col-md-2">
                                <label class="pull-left 4">Sede</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddl_sedes" runat="server" CssClass="caja2 pull-right"></asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btn_mas" class="btn btn-default" runat="server" Text="+"  OnClientClick="javascript:alert('Próximamente');" CausesValidation="false" OnClick="btn_mas_Click" />
                            </div>
                        </div>
                                                
                        <div class="row centered">    <p>&nbsp;</p> </div>

                        <!--Fecha de torneo-->
                        <div class="row centered">
                            <div class="col-md-2">
                                <label class="pull-left">Fecha a producirse</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="dp_fecha" runat="server" CssClass="datepicker caja2" placeholder="Seleccione fecha del torneo"></asp:TextBox>
                            </div>

                            <div class="col-md-2">
                                <label class="pull-left">Hora de inicio</label>
                            </div>

                            <div class="col-md-2">
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
                            <div class="col-md-3">
                                <asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Debe ingresar fecha del torneo"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rev_fecha" runat="server" ControlToValidate="dp_fecha" CssClass="text-danger" Display="Dynamic" ErrorMessage="Fecha mal Ingresada" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="row centered"><p>&nbsp;</p></div>

                        <!--Fecha de cierre de inscripcion-->
                        <div class="row centered">
                            <div class="col-md-2">
                                <label class="pull-left ">Cierre de inscripcion</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="dp_fecha_cierre" placeholder="Seleccione fecha de cierre de inscripciones" CssClass="datepicker caja2" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label class="pull-left">Hora de cierre</label>
                            </div>
                            <div class="col-md-2">
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
                            <div class="col-md-3">
                                <asp:RequiredFieldValidator ID="rfv_fecha_cierre" runat="server" Display="Dynamic" CssClass="text-danger" ErrorMessage="Debe ingresar fecha de cierre de inscripciones" ControlToValidate="dp_fecha_cierre"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="val_fechas" runat="server" CssClass="text-danger" Display="Dynamic" ErrorMessage="La fecha de cierre de inscripción no puede ser mayor a la fecha de comienzo del torneo" OnServerValidate="val_fechas_ServerValidate"></asp:CustomValidator>
                                <asp:RegularExpressionValidator ID="rev_fecha_cierre" runat="server" ControlToValidate="dp_fecha_cierre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Fecha mal Ingresada" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20|21)\d{2}$"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <!--Precio por categoria-->
                        <div class="row centered">
                            <div class="col-md-3">
                                <label class="pull-left left">Precio de inscripcion categoria</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt_precio_cat" CssClass="form-control" placeholder="Ingrese precio de inscripcion categoria" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                 <asp:RequiredFieldValidator ID="requeridoPrecioCat" runat="server" Display="Dynamic" CssClass="text-danger" ErrorMessage="Debe ingresar precio de categoria" ControlToValidate="txt_precio_cat"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="positivoPrecioCat" runat="server" ErrorMessage="El precio debe ser un valor mayor a 0" ControlToValidate="txt_precio_cat" CssClass="text-danger" ValueToCompare="0" Type="Double" Operator="GreaterThan" Display="Dynamic"></asp:CompareValidator>
                                <asp:CompareValidator ID="tipoPrecioCat" runat="server" ErrorMessage="El precio debe ser un valor numérico" ControlToValidate="txt_precio_cat" CssClass="text-danger" Type="Double" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                 <asp:RegularExpressionValidator ID="regex_peso_cat" runat="server" ControlToValidate="txt_precio_cat" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido" ValidationExpression="^[0-9]{0,18}([,.][0-9][0-9]{0,1})$"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                         <div class="row centered"><p>&nbsp;</p></div>

                        <!--Precio absoluto-->
                        <div class="row centered">
                            <div class="col-md-3">
                                <label class="pull-left">Precio de inscripcion absoluta</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt_precio_abs" CssClass="form-control" placeholder="Ingrese precio de inscripcion absoluta" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator ID="requeridoPrecioAbs" runat="server" Display="Dynamic" CssClass="text-danger" ErrorMessage="Debe ingresar precio absoluto" ControlToValidate="txt_precio_abs"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="positivoPrecioAbs" runat="server" ErrorMessage="El precio debe ser un valor mayor a 0" ControlToValidate="txt_precio_abs" CssClass="text-danger" Type="Double" ValueToCompare="0" Operator="GreaterThan" Display="Dynamic"></asp:CompareValidator>
                                <asp:CompareValidator ID="tipoPrecioAbs" runat="server" ErrorMessage="El precio debe ser un valor numérico" ControlToValidate="txt_precio_abs" CssClass="text-danger" Type="Double" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>

                                <asp:RegularExpressionValidator ID="regex_precio_abs" runat="server" ControlToValidate="txt_precio_abs" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido" ValidationExpression="^[0-9]{0,18}([,.][0-9][0-9]{0,1})$"></asp:RegularExpressionValidator>

                            </div>
                        </div>

                          <div class="row centered"><p>&nbsp;</p></div>
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
                        <div class="row centered"><p>&nbsp;</p></div>
                         <!--Comentarios-->
                        <div class="row centered">
                            <div class="col-md-2">
                                <label class="pull-left">Comentarios </label>
                            </div>
                            <div class="col-md-6">
                                <textarea class="form-control" rows="3"></textarea>
                            </div>
                        </div>

                         <div class="row centered"><p>&nbsp;</p></div>

                        <!--Boton-->
                        <div class="row centered">                         
                                <asp:Button ID="btn_aceptar" class="btn btn-default" ValidationGroup="vgTorneo" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click1" OnClientClick="btn_aceptar_Click1" CausesValidation="true" />                         
                        </div>
                    </div>

                </form>              

            </div>

        </div>
        <!-- /row -->
    </asp:Panel>


</asp:Content>

<asp:Content  ID="cphP" ContentPlaceHolderID="cphP"  runat="server">
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
