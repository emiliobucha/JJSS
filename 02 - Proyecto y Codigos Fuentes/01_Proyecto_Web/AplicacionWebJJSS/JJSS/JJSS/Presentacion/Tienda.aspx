<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site2.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Tienda.aspx.cs" Inherits="JJSS.Presentacion.Tienda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="cphContenido" runat="server">
    <form id="form1" runat="server">
        <asp:Panel ID="pnl_mensaje_exito" runat="server" Visible="false">
            <div class="col-md-2 hidden-sm"></div>
            <div class="col-md-8 col-xs-12">
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
            <div class="col-md-2 hidden-sm"></div>
            <div class="col-md-8 col-xs-12">
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

        <div class=" container">

            <h1>TIENDA</h1>


            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <asp:ListView ID="lv_tienda" GroupPlaceholderID="groupPlaceHolder_clase" ItemPlaceholderID="itemPlaceHolder_clase" GroupItemCount="3" OnItemCommand="lv_tienda_ItemCommand" runat="server">

                    <LayoutTemplate>
                        <table>
                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder_clase"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>

                    <GroupTemplate>
                        <tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder_clase"></asp:PlaceHolder>
                        </tr>
                    </GroupTemplate>

                    <ItemTemplate>
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" style="border: 1px medium gray">
                            <div>
                                <asp:Image ID="lv_imagen" ImageUrl='<%# Eval("imagen") %>' runat="server" Width="250" Height="250" />
                            </div>
                            <div>
                                <asp:Label ID="lv_lbl_nombre_clase" CssClass="h3" runat="server" Text='<%# Eval("nombre") %>' />
                            </div>
                            <div>
                                Stock:
                            <asp:Label ID="lv_lbl_profesor" runat="server" Text='<%# Eval("stock") %>' />
                            </div>
                            <div>
                                Precio: $<asp:Label ID="lv_lbl_precio" runat="server" Text='<%# Eval("precio") %>' />
                            </div>
                            <div>
                                <asp:TextBox ID="txt_cantidad" Text="1" runat="server" Width="40px" TextMode="Number"></asp:TextBox>
                                <asp:Button ID="btn_reservar" runat="server" CommandArgument='<%# Eval("id_producto") %>' Text="Reservar" CssClass="btn btn-link" />
                            </div>
                        </div>

                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <h3>No hay productos disponibles por el momento</h3>
                    </EmptyDataTemplate>
                </asp:ListView>


            </div>
            <div class="centered">
                <p>&nbsp;</p>
                <h2>Productos Seleccionados</h2>
                <p>&nbsp;</p>
                <asp:GridView ID="gv_items" CssClass="table" runat="server" DataKeyNames="id_producto" EmptyDataText="No seleccionó ningún producto" OnRowCommand="gv_items_RowCommand" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="nombre" HeaderText="Producto" />
                        <asp:BoundField DataField="precio_venta" HeaderText="Precio Unitario" />
                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                        <asp:ButtonField CommandName="eliminar" Text="Eliminar" HeaderText="Eliminar Item" />
                    </Columns>

                </asp:GridView>
            </div>


            <div class="centered">
                <asp:Button ID="btn_confirmar_reserva" runat="server" Text="Confirmar Reserva" CssClass="btn btn-default" CausesValidation="false" OnClick="btn_confirmar_reserva_Click" />
                <asp:Button ID="btn_limpiar" runat="server" Text="Eliminar productos" CssClass="btn btn-default" CausesValidation="false" OnClick="btn_limpiar_Click" />
            </div>

            <asp:Button ID="btn_Cancelar" runat="server" Text="Cancelar" CssClass="btn btn-default" CausesValidation="false" OnClick="btn_Cancelar_Click"/>
        </div>
    </form>
</asp:Content>
