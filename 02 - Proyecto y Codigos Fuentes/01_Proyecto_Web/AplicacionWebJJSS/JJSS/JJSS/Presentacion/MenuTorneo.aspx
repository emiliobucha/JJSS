<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="MenuTorneo.aspx.cs" Inherits="JJSS.Presentacion.MenuTorneo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContenido" runat="server">

    <form id="form1" runat="server">

        
         <div class="row centered">
            <p>&nbsp;</p>
        </div>


        <div class="row centered justify-content-center ">
            <h1 class=" centered">Torneos</h1>
        </div>
              
        <div class="row centered">
            <p>&nbsp;</p>
        </div>

        
        <!---------------------------------**************Menu**********---------------------------------------->


        <div class="container border rounded">

            <div class="row centered">
                <p>&nbsp;</p>
            </div>

            <div class="row  centered justify-content-center p-2">
                
                <div class="row centered">
                    <p>&nbsp;</p>
                </div>

                <!--Incribir-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto">
                    <a class="text-dark" href="InscripcionTorneo.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../img/Inscribir.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Incribir</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Crear-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto ">
                    <a class="text-dark" href="CrearTorneo.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../img/Crear2.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Crear</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Histórico-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto ">
                    <a class="text-dark" href="HistoricoTorneos.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../img/Historial.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Histórico</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Ver-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto ">
                    <a class="text-dark" href="VerTorneo.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../img/Ver.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Ver</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Pago-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto ">
                    <a class="text-dark" href="TorneoPago.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../img/Pago.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Pagar</h4>
                            </div>
                        </div>
                    </a>
                </div>

            </div>

        </div>

        <div class="row centered ">
            <p>&nbsp;</p>
        </div>
               
        
        <!---------------------------------**************Muestra de Torneos************---------------------------------------->
        <div class="row centered">
            <p>&nbsp;</p>
        </div>
        
        <h2 class=" centered">Últimos Torneos</h2>

        <div class="row centered">
            <p>&nbsp;</p>
        </div>


        <div class="container ">

            <div class=" row centered mt justify-content-center p-2" runat="server">

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

                        <div class="col-lg-3 col-md-3  col-sm-12 " runat="server">
                            <div runat="server">
                                <asp:Label ID="lv_lbl_nombre" CssClass="h4" runat="server" Text='<%# Eval("nombre") %>' />
                            </div>
                            <div runat="server">
                                <%--   <img src="../img/Imagen%20por%20Defecto.png" Width="250" Height="404"/>--%>
                                <asp:Image ID="lv_imagen" ImageUrl='<%# Eval("imagen") %>' runat="server" Width="250" Height="404" />
                            </div>
                            <div runat="server">
                                Fecha:
                                       
                                        <asp:Label ID="lv_lbl_fecha" runat="server" Text='<%# Eval("fecha") %>' />
                            </div>
                            <div runat="server">
                                Hora:
                                       
                                        <asp:Label ID="lv_lbl_hora" runat="server" Text='<%# Eval("hora") %>' />
                            </div>
                            <div runat="server">
                                <asp:Button ID="lv_btn_inscribir" runat="server" CommandArgument='<%# Eval("id_torneo") %>' CssClass=" btn btn-outline-dark" Text="Inscribir" />
                            </div>
                            <div class="row centered ">
                                <p>&nbsp;</p>
                            </div>
                        </div>

                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <h3>No hay torneos disponibles por el momento</h3>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>

        </div>

        <div class="row centered">
            <p>&nbsp;</p>
        </div>

</form>
</asp:Content>