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
            <h1>Histórico de torneos</h1>
        </div>
    </div>

     <div><p>&nbsp;</p></div>

    <form id="form" runat="server" class="container">
 

        <!--APARTADO DE TORNEOS FUTUROS-->
        <div class=" container p-2 ">

            <!--Filtros-->
            <div>
                <div class="row p-2 pt-4 pl-4">

                    <div class=" col-lg-1 col-md-1 col-sm-2 p-1 text-left"><a>Nombre</a></div>
                    <div class="col col-lg-2 col-md-2 col-sm-10 p-1">
                        <asp:TextBox ID="txt_filtro_nombre" class="caja2" runat="server"></asp:TextBox>
                    </div>

                    <div class=" col-lg-1 col-md-1 col-sm-2 p-1 text-left"><a>Estado</a></div>
                    <div class="col col-lg-2 col-md-2 col-sm-10 p-1">
                        <asp:DropDownList ID="ddl_estados" runat="server" class="caja2"></asp:DropDownList>
                    </div>
                </div>

                <div class="row  p-2 pl-4">
                    <div class=" col-lg-1 col-md-1 col-sm-2 p-1 text-left"><a>Desde</a></div>
                    <div class="col col-lg-2 col-md-2 col-sm-10 p-1">
                        <asp:TextBox ID="dp_filtro_fecha_desde" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" value="01/01/2000" required="true" placeholder="Seleccione fecha "></asp:TextBox>
                    </div>

                    <div class=" col-lg-1 col-md-1 col-sm-2 p-1 text-left"><a>Hasta</a></div>
                    <div class="col col-lg-2 col-md-2 col-sm-10 p-1">
                        <asp:TextBox ID="dp_filtro_fecha_hasta" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" value="01/01/2000" required="true" placeholder="Seleccione fecha "></asp:TextBox>

                    </div>
                    <div class="col-md-1 col-lg-1 justify-content-center centered center-block">
                        <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="btn btn-outline-dark" OnClick="btn_buscar_Click" />
                    </div>
                </div>
            </div>

            <!---------------------------------**************Muestra de Torneos************---------------------------------------->
                <div class=" row mt centered justify-content-center  p-1" runat="server">
                    <div  class="col-12">
                    <asp:GridView ID="gv_torneos" runat="server" CssClass="table table-hover" CellPadding="4" DataKeyNames="id_torneo" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay torneos para mostrar" OnRowCommand="gv_torneo_RowCommand" AllowPaging="True" OnPageIndexChanging="gv_torneo_PageIndexChanging" PageSize="20">
                        <Columns>
                            <asp:BoundField DataField="nombre" HeaderText="Torneo" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                            <asp:BoundField DataField="estado" HeaderText="Estado" />
                            <asp:ButtonField CommandName="seleccionar" Text="Seleccionar" ItemStyle-ForeColor="#007bff" HeaderText="Seleccionar" />
                        </Columns>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                    </asp:GridView>
                        </div>
                </div>

               <!-- <div class=" row centered mt justify-content-center p-2" runat="server">
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
                                    Estado:
                                       
                                        <asp:Label ID="lv_lbl_estado" runat="server" Text='<%# Eval("estado") %>' />
                                </div>
                                <div runat="server">
                                    <asp:Button ID="lv_btn_seleccionar" runat="server" CommandArgument='<%# Eval("id_torneo") %>' CssClass="btn btn-outline-dark" Text="Seleccionar" />

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


                </div>-->
                <!---------------FIN Cuadricula-------------------->

         
        </div>
       
        <div><p>&nbsp;</p></div>
            
        <asp:Button ID="btn_Cancelar" runat="server" Text="Volver" formnovalidate="true" CssClass="btn btn-link" CausesValidation="false" OnClick="btn_Cancelar_Click"/>
    </form>
</asp:Content>
