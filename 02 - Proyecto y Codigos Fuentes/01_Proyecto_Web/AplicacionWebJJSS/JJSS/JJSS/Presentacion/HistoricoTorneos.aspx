<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="HistoricoTorneos.aspx.cs" Inherits="JJSS.Presentacion.HistoricoTorneos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
        <script type="text/javascript">
        $(document).ready(
            function () {
                $(".datepicker").datepicker({
                    dateFormat: "dd/mm/yy",
                    monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                    dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"]
                });
            }
        );
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row centered">
        <div class=" col-md-4"></div>
        <div class="col-md-4">
            <p>&nbsp;</p>
            <h1>HISTÓRICO DE TORNEOS</h1>
        </div>
    </div>
    <form id="form" runat="server" class="container">
        <div class="row mt centered">
            <div class="col-md-3">
                <h2>Filtros</h2>
            </div>
            <div class="col-md-2"><strong>Nombre</strong></div>
            <div class="col-md-2">
                <asp:TextBox ID="txt_filtro_nombre" class="caja2" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-2"><strong>Fecha desde</strong></div>
            <div class="col-md-2">
                <asp:TextBox ID="dp_filtro_fecha_desde" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" value="01/01/2000" required="true" placeholder="Seleccione fecha "></asp:TextBox>
            </div>
            <p>&nbsp;</p>
        </div>
        <div class="row mt centered">
            <div class="col-md-7"></div>
            <div class="col-md-2"><strong>Fecha hasta</strong></div>
            <div class="col-md-2">
                <asp:TextBox ID="dp_filtro_fecha_hasta" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" value="01/01/2000" required="true" placeholder="Seleccione fecha "></asp:TextBox>
            </div>
        </div>
        <div class="row mt centered">
            <div class="col-md-9"></div>
            <div class="col-md-2"><asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="btn btn-default" OnClick="btn_buscar_Click"/></div>
        </div>

        <!--APARTADO DE TORNEOS FUTUROS-->
        <div class="container">
            <div class="row mt centered">

                <div class="row centered">
                    <p>&nbsp;</p>
                </div>
                <!---------------------------------**************Muestra de Torneos************---------------------------------------->
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:ListView ID="lv_torneos" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1" GroupItemCount="3" runat="server" OnItemCommand="lv_torneos_ItemCommand">

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
                                    Estado: 
                                       
                                        <asp:Label ID="lv_lbl_estado" runat="server" Text='<%# Eval("estado") %>' />
                                </div>
                                <div>
                                    <asp:Button ID="lv_btn_seleccionar" runat="server" CommandArgument='<%# Eval("id_torneo") %>' CssClass=" btn-link" Text="Seleccionar" />
                                </div>
                            </div>

                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <h3>No hay torneos disponibles por el momento</h3>
                        </EmptyDataTemplate>
                    </asp:ListView>


                </div>
                <!---------------FIN Cuadricula-------------------->
            </div>
        </div>
    </form>
</asp:Content>
