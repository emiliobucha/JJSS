<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="CrearClase.aspx.cs" Inherits="JJSS.Presentacion.CrearClase" %>

<asp:Content ID="crearClaseMenu" ContentPlaceHolderID="cphMenu" runat="server">
    <a href="Inicio.aspx" class="smoothScroll">Home</a>
</asp:Content>
<asp:Content ID="crearClaseEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="crearClaseContenido" ContentPlaceHolderID="cphContenido" runat="server">

    <asp:Panel ID="pnl_InfoClase" CssClass="panel panel-default" runat="server">

        <div id="registrarAlumnowrap">

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
                    <h1>FORMULARIO DE ALTA DE CLASE</h1>
                    <p>&nbsp;</p>
                </div>

                    <!--Nombre-->
                    <asp:Panel ID="pnl_datosClase" CssClass="panel panel-default" runat="server">
                        <div class="row centered">
                            <h3>Datos de la Clase&nbsp;</h3>
                        </div>
                        <div class="row centered">
                            &nbsp;
                        </div>

                        <div class="row centered">
                            <div class="col-xs-2">
                                <label class="pull-left">Nombre</label>
                            </div>
                            <div class="col-xs-3">
                                <asp:TextBox ID="txt_nombre" runat="server" placeholder="Ingrese nombre" CssClass="caja2"></asp:TextBox>
                            </div>
                            <div class="col-xs-3">
                                <asp:RequiredFieldValidator ID="requeridoNombre" CssClass="text text-danger" runat="server" ErrorMessage="Debe ingresar el nombre" ControlToValidate="txt_nombre" ValidationGroup="vg_aceptar">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="caracteres_nombre" runat="server" ControlToValidate="txt_nombre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Nombre demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vg_aceptar">*</asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="row centered">
                            &nbsp;
                        </div>

                        <div class="row centered">
                            <div class="col-xs-2">
                                <label class="pull-left">Precio</label>
                            </div>
                            <div class="col-xs-3">
                                <asp:TextBox ID="txt_precio" runat="server" placeholder="Ingrese precio" CssClass="caja2"></asp:TextBox>
                            </div>
                            <div class="col-xs-3">
                                <asp:RequiredFieldValidator ID="requeridoPrecio" ValidationGroup="vg_aceptar" CssClass="text text-danger" runat="server" ErrorMessage="Debe ingresar un precio" ControlToValidate="txt_precio" Display="Dynamic">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regex_precio" ValidationGroup="vg_aceptar" runat="server" ControlToValidate="txt_precio" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido del precio" ValidationExpression="^[0-9]{0,16}(,?[0-9][0-9]{0,1})$">*</asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="row centered">
                            &nbsp;
                        </div>

                        <!-- tipoclase-->
                        <div class="row centered">
                            <div class="col-xs-2">
                                <label class="pull-left">Tipo de Clase</label>
                            </div>
                            <div class="col-xs-3">
                                <asp:DropDownList ID="ddl_tipo_clase" runat="server" CssClass="caja2"></asp:DropDownList>
                            </div>
                            <div class="col-xs-3">
                            </div>
                        </div>

                        <div class="row centered">
                            &nbsp;
                        </div>

                        <!-- ubicacion-->
                        <div class="row centered">
                            <div class="col-xs-2">
                                <label class="pull-left">Ubicación</label>
                            </div>
                            <div class="col-xs-3">
                                <asp:DropDownList ID="ddl_ubicacion" runat="server" CssClass="caja2"></asp:DropDownList>
                            </div>
                            <div class="col-xs-3">
                            </div>
                        </div>

                        <div class="row centered">
                            &nbsp;
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="pnl_horariosClase" CssClass="panel panel-default" runat="server">
                        <div class="row centered">
                            <h3>Horarios&nbsp;</h3>
                        </div>
                        <div class="row centered">
                            &nbsp;
                        </div>
                        <!--Horarios-->
                        <div class="row centered">
                            <div class="col-xs-2">
                                <asp:DropDownList runat="server" ID="ddl_dia" CausesValidation="true" CssClass="caja2">
                                    <asp:ListItem Value="Lunes">Lunes</asp:ListItem>
                                    <asp:ListItem Value="Martes">Martes</asp:ListItem>
                                    <asp:ListItem Value="Miércoles">Miércoles</asp:ListItem>
                                    <asp:ListItem Value="Jueves">Jueves</asp:ListItem>
                                    <asp:ListItem Value="Viernes">Viernes</asp:ListItem>
                                    <asp:ListItem Value="Sábado">Sábado</asp:ListItem>
                                    <asp:ListItem Value="Domingo">Domingo</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-xs-2 col-md-2">
                                <label class="pull-left">Hora Desde:</label>
                            </div>
                            <div class="col-xs-2 col-md-2">
                                <asp:TextBox ID="txt_horadesde" runat="server" TextMode="Time" CssClass="caja2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="requerido_horadesde" CssClass="text text-danger" runat="server" ErrorMessage="Debe ingresar un horario de inicio" ControlToValidate="txt_horadesde" Display="Dynamic" ValidationGroup="vg_agregar_horario">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regex_horadesde" runat="server" ControlToValidate="txt_horadesde" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido. Debe ser hh:mm" ValidationExpression="^(([0-1][0-9])|([2][0-3])):[0-5][0-9]$">*</asp:RegularExpressionValidator>
                            </div>
                            <div class="col-xs-2 col-md-2">
                                <label class="pull-left">Hora Hasta:</label>
                            </div>
                            <div class="col-xs-2 col-md-2">
                                <asp:TextBox ID="txt_horahasta" TextMode="Time" runat="server" CssClass="caja2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="requerido_horahasta" CssClass="text text-danger" runat="server" ErrorMessage="Debe ingresar un horario de fin" ControlToValidate="txt_horahasta" ValidationGroup="vg_agregar_horario" Display="Dynamic">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regex_horahasta" runat="server" ControlToValidate="txt_horahasta" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido. Debe ser hh:mm" ValidationExpression="^(([0-1][0-9])|([2][0-3])):[0-5][0-9]$">*</asp:RegularExpressionValidator>
                            </div>
                            <div class="col-xs-2 col-md-2">
                                <asp:Button ID="btn_agregar" runat="server" OnClick="btn_agregar_Click" Text="Agregar" CssClass="btn btn-default" ValidationGroup="vg_agregar_horario"></asp:Button>
                            </div>
                        </div>
                        <div>
                            &nbsp;
                        </div>

                        <div class="row centered">
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="false" ShowSummary="true" EnableClientScript="true" CssClass="text-danger" BorderStyle="Double" ValidationGroup="vg_agregar_horario" HeaderText="Se han producido los siguientes errores:" />
                        </div>

                        <div class="row centered">

                            <!--OnRowDataBound="dg_horarios_RowDataBound"-->
                            <asp:GridView ID="dg_horarios" runat="server" DataKeyNames="id_horario" EmptyDataText="No hay horarios actualmente" CssClass="table table-responsive" OnItemDataBound="dg_horarios_ItemDataBound" OnRowCommand="dg_horarios_RowCommand" OnRowDataBound="dg_horarios_RowDataBound" BorderColor="Black" BorderStyle="Double">

                                <Columns>
                                    <asp:BoundField HeaderStyle-CssClass="text-center" HeaderText="Día" DataField="nombre_dia">
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-CssClass="text-center" HeaderText="Desde" DataField="hora_desde">
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Hasta" DataField="hora_hasta" />
                                    <asp:ButtonField CommandName="Eliminar" Text="Eliminar" />
                                </Columns>
                            </asp:GridView>

                        </div>
                        <div class="row centered">
                            &nbsp;
                        </div>

                    </asp:Panel>


                    <div class="row centered">
                        &nbsp;
                    </div>

                    <div class="row centered">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="false" ShowSummary="true" EnableClientScript="true" CssClass="text-danger" BorderStyle="Double" ValidationGroup="vg_aceptar" HeaderText="Se han producido los siguientes errores:" />
                        </div>

                    <div class="row centered">
                        <asp:Button ID="btn_aceptar" runat="server" OnClick="btn_aceptar_Click" Text="Aceptar" CssClass="btn btn-default" ValidationGroup="vg_aceptar"></asp:Button>
                        <asp:Button ID="btn_nueva_clase" runat="server" Text="Crear Nueva Clase" CssClass="btn btn-default" CausesValidation="false" OnClick="btn_nueva_clase_Click"></asp:Button>
                    </div>
                </form>
            </div>
        </div>

    </asp:Panel>
</asp:Content>

<asp:Content ID="cphP" ContentPlaceHolderID="cphP" runat="server">

    <script type="text/javascript">

</script>
</asp:Content>
