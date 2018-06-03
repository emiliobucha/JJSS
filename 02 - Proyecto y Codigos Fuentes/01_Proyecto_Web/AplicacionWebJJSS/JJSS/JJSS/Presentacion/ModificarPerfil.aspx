<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="ModificarPerfil.aspx.cs" Inherits="JJSS.Presentacion.ModificarPerfil" %>


<asp:Content ID="crearClaseEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="crearClaseContenido" ContentPlaceHolderID="cphContenido" runat="server">

    <asp:Panel ID="pnl_InfoClase" CssClass="panel panel-default" runat="server">


        <form id="form1" runat="server">
            <div class="container">

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
            </div>


            <div class="row mt centered justify-content-center ">
                <h1 class="centered">Modificar Permisos</h1>
            </div>

            <div>
                &nbsp;
            </div>


            <!--Nombre-->
            <div class="container">
                <div>
                    &nbsp;
                </div>

                <asp:Panel ID="pnl_horariosClase" CssClass="panel panel-default" runat="server">
                    <div class="row justify-content-center centered">
                        <div class=" col-sm-12 col-md-4 col-lg-4 p-4 border rounded ">

                            <div class="row centered">
                                <h3>Grupos</h3>
                            </div>
                            <div class="row centered">
                                &nbsp;
                            </div>

                            <!-- precio-->
                            <div class="row centered">
                                <div class="col col-lg-6 col-md-6 col-sm-6">
                                    <asp:DropDownList ID="ddl_grupos" runat="server" CausesValidation="true" CssClass="caja2">
                                    </asp:DropDownList>
                                </div>
                                <div class="col col-lg-2 col-md-2 col-sm-2">
                                    <asp:Button ID="btn_agregar" runat="server" CssClass="btn btn-outline-dark" OnClick="btn_agregar_Click" Text="Agregar" ValidationGroup="vg_agregar_horario" />
                                </div>
                            </div>
                            <div>
                                &nbsp;
                            </div>
                        </div>
                        <div class="col col-sm-0 col-md-1 col-lg-1 ">
                            <div>
                                &nbsp;
                            </div>
                        </div>
                        <div class=" col-sm-12 col-md-4 col-lg-4 p-4 border rounded ">

                            <div class="row centered">
                                <h3>Grupos Asignados</h3>
                            </div>
                            <div class="row centered">
                                &nbsp;
                            </div>

                            <div class="row justify-content-center centered">
                                <!--OnRowDataBound="dg_horarios_RowDataBound"-->
                                <asp:GridView ID="dg_grupos" runat="server" CssClass="table" DataKeyNames="id_grupo" ForeColor="#333333" GridLines="None"  EmptyDataText="No hay grupos asignados a este usuario" OnItemDataBound="dg_horarios_ItemDataBound" OnRowCommand="dg_grupos_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="nombre" HeaderStyle-CssClass="text-center" HeaderText="Grupo">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ItemStyle-ForeColor="#007bff" />
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="row centered">
                                &nbsp;
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <div class="row centered">
                    &nbsp;
                </div>

                <div class="row centered justify-content-center p-1">
                    <div class="col col-auto">
                        <%--<asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="btn btn-default" CausesValidation="false" OnClick="btn_cancelar_Click"></asp:Button>--%>
                        <asp:Button ID="btn_aceptar" runat="server" OnClick="btn_aceptar_Click" Text="Aceptar" CssClass="btn btn-outline-dark" ValidationGroup="vg_aceptar"></asp:Button>
                    </div>
                </div>

            </div>

            <div>
                &nbsp;
            </div>
        </form>

    </asp:Panel>
</asp:Content>

<asp:Content ID="cphP" ContentPlaceHolderID="cphP" runat="server">

    <script type="text/javascript">

</script>
</asp:Content>
