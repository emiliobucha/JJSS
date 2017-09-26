<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site2.Master" AutoEventWireup="true" CodeBehind="Tienda.aspx.cs" Inherits="JJSS.Presentacion.Tienda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="cphContenido" runat="server">

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
                            <asp:Button ID="btn_reservar" runat="server" Text="Reservar" CssClass="btn btn-link" />
                        </div>
                    </div>

                </ItemTemplate>
                <EmptyDataTemplate>
                    <h3>No hay productos disponibles por el momento</h3>
                </EmptyDataTemplate>
            </asp:ListView>

        </div>
    </div>
</asp:Content>
