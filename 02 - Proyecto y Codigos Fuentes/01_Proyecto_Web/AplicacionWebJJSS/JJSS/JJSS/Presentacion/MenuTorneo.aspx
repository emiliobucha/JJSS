<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="MenuTorneo.aspx.cs" Inherits="JJSS.Presentacion.MenuTorneo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContenido" runat="server">




    <h1>ÚLTIMOS TORNEOS</h1>
    <div class="row centered">
        <p>&nbsp;</p>
    </div>
    <!---------------------------------**************Muestra de Torneos************---------------------------------------->
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <asp:ListView ID="lv_torneos_abiertos" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1" GroupItemCount="3" runat="server" OnItemCommand="lv_torneos_abiertos_ItemCommand">

            <LayoutTemplate>
                <table>
                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>

            <GroupTemplate>
                <tr>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                </tr>
            </GroupTemplate>

            <ItemTemplate>

                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" style="border: 1px medium gray">
                    <div>
                        <asp:Label ID="lv_lbl_nombre" CssClass="h3" runat="server" Text='<%# Eval("nombre") %>' />
                    </div>
                    <div>
                        <%--   <img src="../img/Imagen%20por%20Defecto.png" Width="250" Height="404"/>--%>
                        <asp:Image ID="lv_imagen" ImageUrl='<%# Eval("imagen") %>' runat="server" Width="250" Height="404" />
                    </div>
                    <div>
                        Fecha:
                                       
                                        <asp:Label ID="lv_lbl_fecha" runat="server" Text='<%# Eval("fecha") %>' />
                    </div>
                    <div>
                        Hora:
                                       
                                        <asp:Label ID="lv_lbl_hora" runat="server" Text='<%# Eval("hora") %>' />
                    </div>
                    <div>
                        <asp:Button ID="lv_btn_inscribir" runat="server" CommandArgument='<%# Eval("id_torneo") %>' CssClass=" btn-link" Text="Inscribir" />
                    </div>
                </div>

            </ItemTemplate>
            <EmptyDataTemplate>
                <h3>No hay torneos disponibles por el momento</h3>
            </EmptyDataTemplate>
        </asp:ListView>


    </div>

</asp:Content>
