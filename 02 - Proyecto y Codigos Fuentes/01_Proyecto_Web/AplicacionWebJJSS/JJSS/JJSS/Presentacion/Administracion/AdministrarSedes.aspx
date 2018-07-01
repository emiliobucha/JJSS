<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarSedes.aspx.cs" Inherits="JJSS.Presentacion.Administracion.AdministrarSedes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
            <script type='text/javascript'>
        var x = 0;
        function button() {
            var objwordstonum = document.getElementById('<%=txtIDSeleccionado.ClientID%>');
                objwordstonum.value = x;
                return true;
        }
        function openModal(id) {
            $('[id*=confirmacion]').modal('show');
            x = id;
            return false;
    }   
</script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <form id="formRegAlumno" runat="server">

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

        <div id="grillawrap">

            <asp:Panel ID="pnl_mostrar_alumnos" runat="server">

                <div id="mostrarAlumnowrap">

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="row centered justify-content-center">
                        <h1>Listado de Sedes</h1>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="container">

                        <div class="form-group border rounded p-4 ">

                            <!--Boton-->
                            <div class="row justify-content-center">
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>Nombre</strong>
                                </div>

                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:TextBox ID="txt_filtro_nombre" CssClass="caja2" runat="server"></asp:TextBox>
                                </div>
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>Ciudad</strong>
                                </div>
                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:DropDownList ID="ddl_filtro_ciudad" runat="server" CssClass="caja2"></asp:DropDownList>
                                </div>
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                </div>
                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:RadioButtonList ID="rbSede" runat="server" AutoPostBack="False">
                                        <asp:ListItem Value="0" Selected="True">&nbsp;Sedes</asp:ListItem>
                                        <asp:ListItem Value="1">&nbsp;Academias</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <asp:Button ID="btn_buscar" runat="server" Text="Buscar" OnClick="btn_buscar_Click" CssClass="btn btn-outline-dark" />
                                </div>

                                <asp:HyperLink CssClass="btn btn-link" Text="Ir a registrar" runat="server" href="CrearSede.aspx"></asp:HyperLink>
                            </div>

                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <div class="row centered justify-content-center">
                                <asp:GridView ID="gvSedes" runat="server" CssClass="table table-responsive-xl" CellPadding="4" DataKeyNames="idSede" OnPageIndexChanging="gvSedes_PageIndexChanging"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay sedes para mostrar"
                                    OnRowCommand="gvSedes_RowCommand" AllowPaging="True" PageSize="10">
                                    <Columns>
                                        <asp:BoundField DataField="sede" HeaderText="Nombre" />
                                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                                        <asp:BoundField DataField="calle" HeaderText="Calle" />
                                        <asp:BoundField DataField="numero" HeaderText="Número" />
                                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                                        
                                        <asp:ButtonField CommandName="seleccionar" Text="Seleccionar" HeaderText="Seleccionar" ItemStyle-ForeColor="#007bff" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton id="aa" CommandName ="eliminar" runat="server" CommandArgument ='<%# Eval("idSede") %>' 
                                                    OnClientClick='<%# Eval("idSede", "return openModal({0})") %>' > Eliminar</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                </asp:GridView>
                            </div>

                           

                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class=" container">
                <div>
                    <p>&nbsp;</p>
                </div>

                <div class="row pull-left">
                    <div class="col">
                        <asp:LinkButton runat="server" ID="lnk_cancelar" class="btn btn-link " Text="Volver" href="Menu_Administracion.aspx"></asp:LinkButton>
                    </div>
                </div>
                <div>
                    <p>&nbsp;</p>
                </div>
            </div>
        </div>

        <div class="modal fade col-lg-12 col-md-12 col-xs-8 col-sm-8" id="confirmacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <!--Cabecera-->
                    <div class="modal-header">
                        <h4 class="modal-title" id="exampleModalLabe2">¿Seguro que desea eliminar la sede?</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>

                    <!--Botonero-->
                    <div class="modal-footer">
                        <asp:button ID="btn_si" type="button" runat="server" class="btn btn-outline-dark" OnClientClick="return button()" OnClick="btn_si_Click1"  TExt="SI"/>
                        <Button ID="btn_no" type="button" class="btn btn-default"  value="No" data-dismiss="modal">No</button>

                    </div>

                </div>
            </div>
        </div>
        <asp:TextBox ID ="txtIDSeleccionado" runat="server" Text="" hidden="true"></asp:TextBox>

    </form>


</asp:Content>
