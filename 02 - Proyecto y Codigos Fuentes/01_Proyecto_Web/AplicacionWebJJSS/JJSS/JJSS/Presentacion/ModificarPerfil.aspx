<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site2.Master" AutoEventWireup="true" CodeBehind="ModificarPerfil.aspx.cs" Inherits="JJSS.Presentacion.ModificarPerfil" %>


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
                        <h1>MODIFICAR PERMISOS</h1>
                        <p>&nbsp;</p>
                    </div>

                    <!--Nombre-->
                    <asp:Panel ID="pnl_horariosClase" CssClass="panel panel-default" runat="server">
                        <div class="row centered">
                            <h3>Grupos</h3>
                        </div>
                        <div class="row centered">
                            &nbsp;
                        </div>

                        <!-- precio-->
                        <div class="row centered">
                            <div class="col-xs-2">
                                <asp:DropDownList ID="ddl_grupos" runat="server" CausesValidation="true" CssClass="caja2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-2 col-md-2">
                                <asp:Button ID="btn_agregar" runat="server" CssClass="btn btn-default" OnClick="btn_agregar_Click" Text="Agregar" ValidationGroup="vg_agregar_horario" />
                            </div>
                        </div>

                        <div>
                            &nbsp;
                        </div>

                        <div class="row centered">
                            <!--OnRowDataBound="dg_horarios_RowDataBound"-->
                            <asp:GridView ID="dg_grupos" runat="server" BorderColor="Black" BorderStyle="Double" CssClass="table" DataKeyNames="id_grupo" EmptyDataText="No hay grupos asignados a este usuario" OnItemDataBound="dg_horarios_ItemDataBound" OnRowCommand="dg_grupos_RowCommand" >
                                <Columns>
                                    <asp:BoundField DataField="nombre" HeaderStyle-CssClass="text-center" HeaderText="Grupo">
                                    <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
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
                        <%--<asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="btn btn-default" CausesValidation="false" OnClick="btn_cancelar_Click"></asp:Button>--%>
                        <asp:Button ID="btn_aceptar" runat="server" OnClick="btn_aceptar_Click" Text="Aceptar" CssClass="btn btn-default" ValidationGroup="vg_aceptar"></asp:Button>
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
