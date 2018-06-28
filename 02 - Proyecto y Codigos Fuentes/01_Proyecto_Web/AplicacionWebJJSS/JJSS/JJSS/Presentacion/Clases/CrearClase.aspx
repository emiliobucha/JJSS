<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="CrearClase.aspx.cs" Inherits="JJSS.Presentacion.CrearClase" %>


<asp:Content ID="crearClaseEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="crearClaseContenido" ContentPlaceHolderID="cphContenido" runat="server">


    <form id="form1" runat="server">

          <div class="container centered justify-content-center">

            <asp:Panel ID="pnl_mensaje_exito" runat="server" Visible="false">
                <div class="container">
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
                </div>
            </asp:Panel>

            <asp:Panel ID="pnl_mensaje_error" runat="server" Visible="false">
                <div class="container">
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
                </div>
            </asp:Panel>
        </div>

        <div class="row mt centered justify-content-center ">
            <h1 class="centered">Alta de Clase</h1>
        </div>

        <div>
            &nbsp;
        </div>
        
        <asp:Panel ID="pnl_datosClase" CssClass="panel panel-default p-1 " runat="server">            
            <div class="container border rounded p-4">                            

                <div class="row ">
                    <h3 class="text-left">Datos de la Clase</h3>
                </div>

                <div>
                    &nbsp;
                </div>

                <!--Nombre-->
                <div class="row  pl-lg-5 pl-md-5">
                    <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                        <label class=" text-left">Nombre <a class="text-danger">*</a></label>
                    </div>
                    <div class="col col-md-3 col-lg-3 col-sm-11 col-xs-11">
                        <asp:TextBox ID="txt_nombre" required="true" runat="server" placeholder="Ingrese nombre" CssClass="caja2"></asp:TextBox>
                    </div>
                </div>
                <div>
                    &nbsp;
                </div>

                <!-- precio-->
                <div class="row  pl-lg-5 pl-md-5">
                    <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                        <label class="text-left">Precio <a class="text-danger">*</a></label>
                    </div>
                    <div class="col col-md-3 col-lg-3 col-sm-11 col-xs-11">
                        <asp:TextBox ID="txt_precio" required="true" min="0" max="999999" type="number" step="0.01" runat="server" placeholder="Ingrese precio" CssClass=" caja2 form-control"></asp:TextBox>
                    </div>
                </div>
                <div>
                    &nbsp;
                </div>

                <!-- tipoclase-->
                <div class="row  pl-lg-5 pl-md-5">
                    <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                        <label class="text-left">Tipo de Clase <a class="text-danger">*</a></label>
                    </div>
                    <div class="col col-md-3 col-lg-3 col-sm-11 col-xs-11">
                        <asp:DropDownList ID="ddl_tipo_clase" runat="server" CssClass="caja2"></asp:DropDownList>
                    </div>
                </div>
                <div>
                    &nbsp;
                </div>

                <!-- ubicacion-->
                <div class="row  pl-lg-5 pl-md-5">
                    <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                        <label class="pull-left">Ubicación <a class="text-danger">*</a></label>
                    </div>
                    <div class="col col-md-3 col-lg-3 col-sm-11 col-xs-11">
                        <asp:DropDownList ID="ddl_ubicacion" runat="server" CssClass="caja2"></asp:DropDownList>
                    </div>
                    
                    <div class="col col-md-3 col-lg-3 col-sm-11 col-xs-11">
                        <asp:LinkButton ID ="lnk_sede" href="/Presentacion/Administracion/CrearSede.aspx" runat="server" CssClass="btn btn-outline-dark">+</asp:LinkButton>
                        </div>
                </div>
                <div>
                    &nbsp;
                </div>

                <!-- profesor-->
                <div class="row  pl-lg-5 pl-md-5" >
                    <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12">
                        <label class="pull-left">Profesor <a class="text-danger">*</a></label>
                    </div>
                    <div class="col col-md-3 col-lg-3 col-sm-11 col-xs-11">
                        <asp:DropDownList ID="ddl_profesor" runat="server" CssClass="caja2"></asp:DropDownList>
                    </div>
                </div>
                <div>
                    &nbsp;
                </div>

                <div class=" p-2 ">
                    <p class="text-danger pull-right " style="font-size: small">* Campo requerido</p>
                </div>
                
            </div>
        </asp:Panel>

        <div>
                    &nbsp;
                </div>

        <asp:Panel ID="pnl_horariosClase" CssClass="panel panel-default p-1" runat="server">

            <div class="container border rounded p-4">

                <div class="row">
                    <h3 class="text-left">Horarios</h3>
                </div>
                <div class="row centered">
                    &nbsp;
                </div>
                <!--Horarios-->
                <div class="row ">

                    <div class="col-md-2 col-lg-2 col-xs-12 col-sm-12 p-1">
                        <asp:DropDownList runat="server" ID="ddl_dia" CausesValidation="true" CssClass="caja2">
                            <asp:ListItem Value="Domingo">Domingo</asp:ListItem>
                            <asp:ListItem Value="Lunes">Lunes</asp:ListItem>
                            <asp:ListItem Value="Martes">Martes</asp:ListItem>
                            <asp:ListItem Value="Miércoles">Miércoles</asp:ListItem>
                            <asp:ListItem Value="Jueves">Jueves</asp:ListItem>
                            <asp:ListItem Value="Viernes">Viernes</asp:ListItem>
                            <asp:ListItem Value="Sábado">Sábado</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <div class=" hidden-lg hidden-md">
                        &nbsp;
                    </div>

                    <div class="col col-md-2 col-lg-2 col-xs-6 col-sm-6 p-1 ml-lg-5">
                        <label class=" text-right centered">Hora Desde:</label>
                    </div>
                    <div class="col col-auto  p-1">
                        <asp:TextBox ID="txt_horadesde" runat="server" required="true" type="time" CssClass="caja2" Text="00:00"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regex_horadesde" runat="server" ControlToValidate="txt_horadesde" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido. Debe ser hh:mm" ValidationExpression="^(([0-1][0-9])|([2][0-3])):[0-5][0-9]$"> </asp:RegularExpressionValidator>
                    </div>

                    <div class=" hidden-lg hidden-md">
                        &nbsp;
                    </div>
                    <div class="col col-md-2 col-lg-2 col-xs-6 col-sm-6 p-1 ml-lg-5">
                        <label class=" text-right centered">Hora Hasta:</label>
                    </div>
                    <div class=" col col-auto  p-1">
                        <asp:TextBox ID="txt_horahasta" type="time" required="true" runat="server" CssClass="caja2" Text="01:00"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regex_horahasta" runat="server" ControlToValidate="txt_horahasta" CssClass="text-danger" Display="Dynamic" ErrorMessage="Formato inválido. Debe ser hh:mm" ValidationExpression="^(([0-1][0-9])|([2][0-3])):[0-5][0-9]$"> </asp:RegularExpressionValidator>
                        <asp:CompareValidator ID="cmp_hora" ControlToValidate="txt_horadesde" ControlToCompare="txt_horahasta" CssClass="text-danger" Display="Dynamic" runat="server" ErrorMessage="La hora desde debe ser menor que la hora hasta" SetFocusOnError="true" Operator="LessThan"></asp:CompareValidator>
                    </div>

                    <div class=" hidden-lg hidden-md">
                        &nbsp;
                    </div>
                    <div class="col-md-2 col-lg-2 col-xs-12 col-sm-12 p-1 centered justify-content-center">
                        <asp:Button ID="btn_agregar" runat="server" OnClick="btn_agregar_Click" Text="Agregar" CssClass=" btn btn-outline-dark " ValidationGroup="vg_agregar_horario"></asp:Button>
                    </div>
                </div>
                <div>
                    &nbsp;
                </div>

                <div class="row centered">

                    <!--OnRowDataBound="dg_horarios_RowDataBound"-->
                    <asp:GridView ID="dg_horarios" runat="server" DataKeyNames="id" EmptyDataText="No hay horarios actualmente" CssClass="table" OnItemDataBound="dg_horarios_ItemDataBound" OnRowCommand="dg_horarios_RowCommand" OnRowDataBound="dg_horarios_RowDataBound" BorderColor="Black" BorderStyle="Double">

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
            </div>
        </asp:Panel>

        <div>
            &nbsp;
        </div>

        <div class="container p-1">
            <div class="row centered">
                <div class="col col-auto">
                    <asp:HyperLink ID="lnk_volver" runat="server" Text="Volver" class="btn btn-link" href="Menu_Clase.aspx"></asp:HyperLink>
                </div>
                <div class="col col-auto">
                    <asp:Button ID="btn_aceptar" runat="server" OnClick="btn_aceptar_Click" Text="Aceptar" CssClass="btn btn-outline-dark" ValidationGroup="vg_aceptar"></asp:Button>
                </div>
                <div class="col col-auto">
                    <asp:Button ID="btn_nueva_clase" runat="server" Text="Limpiar" CssClass="btn btn-outline-dark" CausesValidation="false" OnClick="btn_nueva_clase_Click" formnovalidate="true"></asp:Button>
                </div>
            </div>
        </div>                    
        
       <div>
            &nbsp;
        </div>

    </form>
  
</asp:Content>

<asp:Content ID="cphP" ContentPlaceHolderID="cphP" runat="server">

    <script type="text/javascript">

</script>
</asp:Content>
