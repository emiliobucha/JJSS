<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarProfesores.aspx.cs" Inherits="JJSS.Presentacion.Administracion.AdministrarProfesores" %>

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
    <form id="formRegProfe" runat="server">

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

            <asp:Panel ID="pnl_mostrar_profes" runat="server">

                <div id="mostrarprofewrap">

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="row centered justify-content-center">
                        <h1>Listado de Profesores</h1>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="container">


                        <div class="form-group border rounded p-4 ">

                            <!--Boton-->
                            <div class="row justify-content-center">

                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>DNI</strong>
                                </div>
                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:TextBox ID="txt_filtro_dni" type="number" CssClass="caja2" min="0" runat="server"></asp:TextBox>
                                </div>
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>Apellido</strong>
                                </div>
                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:TextBox ID="txt_filtro_apellido" CssClass="caja2" runat="server"></asp:TextBox>
                                </div>
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <asp:Button ID="btn_buscar_profe" runat="server" Text="Buscar" OnClick="btn_buscar_profe_Click" ValidationGroup="vgFiltro" CssClass="btn btn-outline-dark" />
                                </div>

                                <asp:HyperLink CssClass="btn btn-link" Text="Ir a registrar" runat="server" href="RegistrarProfe.aspx"></asp:HyperLink>

                            </div>
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <div class="row centered justify-content-center">
                                <asp:GridView ID="gvprofes" runat="server" CssClass="table" CellPadding="4" DataKeyNames="dni" OnPageIndexChanging="gvprofes_PageIndexChanging"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay profes para mostrar"
                                    OnRowCommand="gvprofes_RowCommand" AllowPaging="True" PageSize="20">
                                    <Columns>
                                        <asp:BoundField DataField="apellido" HeaderText="Apellido" SortExpression="apellido" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                                        <asp:BoundField DataField="dni" HeaderText="D.N.I" SortExpression="dni" />

                                        <asp:ButtonField CommandName="seleccionar" Text="Seleccionar" HeaderText="Seleccionar" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton id="aa" CommandName ="eliminar" runat="server" CommandArgument ='<%# Eval("dni") %>' 
                                                    OnClientClick='<%# Eval("dni", "return openModal({0})") %>' > Eliminar</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Position="TopAndBottom" />
                                </asp:GridView>
                            </div>
                            <%--<div class="row">
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <asp:Button ID="btn_registro" runat="server" CausesValidation="false" CssClass=" btn btn-link pull-left" formnovalidate="true"
                                        OnClick="btn_registro_Click" Text="Volver a registrar" />
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </asp:Panel>


        </div>

        <div class="row centered">
            <p>&nbsp;</p>
        </div>

        <div class=" container">
            <div class="row centered">
                <asp:HyperLink runat="server" Text="Volver" CssClass="btn btn-link" href="Menu_Administracion.aspx"></asp:HyperLink>
            </div>
        </div>

        <div class="row centered">
            <p>&nbsp;</p>
        </div>

        
        <div class="modal fade col-lg-12 col-md-12 col-xs-8 col-sm-8" id="confirmacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <!--Cabecera-->
                    <div class="modal-header">
                        <h4 class="modal-title" id="exampleModalLabe2">¿Seguro que desea eliminar el profesor?</h4>
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
