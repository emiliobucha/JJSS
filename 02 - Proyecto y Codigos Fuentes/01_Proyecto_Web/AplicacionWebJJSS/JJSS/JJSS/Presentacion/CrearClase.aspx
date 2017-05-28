<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true"  CodeBehind="~/Presentacion/CrearClase.aspx.cs" Inherits="JJSS.Presentacion.CrearClase" %>
<asp:Content ID="crearClaseMenu" ContentPlaceHolderID="cphMenu" runat="server">
    <a href="Inicio.aspx" class="smoothScroll">Home</a>		
</asp:Content>
<asp:Content ID="crearClaseEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">

</asp:Content>
<asp:Content ID="crearClaseContenido" ContentPlaceHolderID="cphContenido" runat="server">

    <asp:Panel ID="pnl_InfoClase" CssClass="panel panel-default" runat="server">

        <div id="registrarAlumnowrap">

            <div class="container">

                <div class="row mt centered">
                    <h1>FORMULARIO DE ALTA DE CLASE</h1>
                    <p>&nbsp;</p>
                </div>

                <form id="form1" runat="server">

                    <!--Nombre-->
                    <div class="row centered">
                        <div class="col-xs-2">
                            <label class="pull-left">Nombre</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txt_nombre" runat="server" placeholder="Ingrese nombre" CssClass="caja2"></asp:TextBox>
                        </div>
                        <div class="col-xs-2">
                            <asp:RequiredFieldValidator ID="requeridoNombre" CssClass="text text-danger" runat="server" ErrorMessage="Debe ingresar el nombre" ControlToValidate="txt_nombre"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="caracteres_nombre" runat="server" ControlToValidate="txt_nombre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Nombre demasiado largo" ValidationExpression="^[\s\S]{0,50}$"></asp:RegularExpressionValidator>
                        </div>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>


                    <!--Precio-->
                    <div class="row centered">
                        <div class="col-xs-2">
                            <label class="pull-left">Precio</label>
                        </div>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txt_precio" runat="server" placeholder="Ingrese precio" CssClass="caja2"></asp:TextBox>
                        </div>
                        <div class="col-xs-2">
                            <asp:RequiredFieldValidator ID="requeridoPrecio" CssClass="text text-danger" runat="server" ErrorMessage="Debe ingresar un precio" ControlToValidate="txt_precio"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                     <!--Horarios-->
                    <div class="row centered">
                        <div class="col-xs-2">
                            <label class="pull-left">Horarios</label>
                        </div>
                    </div>
                    <div class="row centered">
                        <div class="col-xs-1">
                            <asp:CheckBox ID="chk_lunes" runat="server" Text="Lunes" OnCheckedChanged="chk_lunes_CheckedChanged" CssClass="text pull-left" />
                        </div>
                        <div class="col-xs-4 ">
           
                            <asp:DataGrid ID="dt_lunes" runat="server" CssClass="table table-responsive">
                                <Columns>
                                    <asp:BoundColumn HeaderStyle-CssClass="text-center" HeaderText="Desde">                                      
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Hasta">
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="Delete" Text="Eliminar"></asp:ButtonColumn>                                   
                                </Columns>  
                            </asp:DataGrid>
                        </div>
                        <div class="col-xs-1">
                            <asp:Button ID="btnLunesMas" runat="server" Text="+" CssClass="btn bottom" CausesValidation="false" OnClick="btnLunesMas_Click" />
                        </div>

                        <%--<asp:Table ID="table_lunes" CssClass="table table-responsive" runat="server">
                                <asp:TableHeaderRow>
                                    <asp:TableHeaderCell><a class=" text-center">Desde</a></asp:TableHeaderCell>
                                    <asp:TableHeaderCell>Hasta</asp:TableHeaderCell>
                                    <asp:TableHeaderCell></asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell><input type="time"/></asp:TableCell>
                                    <asp:TableCell><input type="time"/></asp:TableCell>
                                    <asp:TableCell><button>x</button></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>--%>

                  <%--      <div class="col-xs-3 ventanaHorario" id="lunes_1">
                            <a href="#" class=" close-thin"></a>
                            <a  style="color:black">Horario:</a>   
                            <input type="time" id="txt_lunes_desde_1" runat="server" class=" panel panel-default " />                          
                            <label >-</label>
                            <input type="time" id="txt_lunes_hasta_1" runat="server" class=" panel panel-default" />
                           <input type="button" id="btn_lunes_cancel_1" runat="server" value="x"  style=" color:red;"  class="panel "/>                      
                        </div> --%>                       
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
