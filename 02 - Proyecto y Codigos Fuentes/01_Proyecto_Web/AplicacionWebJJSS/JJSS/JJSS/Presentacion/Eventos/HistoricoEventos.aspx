<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="HistoricoEventos.aspx.cs" Inherits="JJSS.Presentacion.Eventos.HistoricoEventos" %>

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
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row centered">
        <div class=" col-md-4"></div>
        <div class="col-md-4">
            <p>&nbsp;</p>
            <h1>Histórico de eventos</h1>
        </div>
    </div>

    <div>
        <p>&nbsp;</p>
    </div>

    <form id="form" runat="server" class="container">


        <!--APARTADO DE EVENTOS FUTUROS-->
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

            <!---------------------------------**************Muestra de eventos************---------------------------------------->
            <div class=" row mt centered justify-content-center  p-1" runat="server">
                <div class="col-12">
                    <asp:GridView ID="gv_eventos" runat="server" CssClass="table table-hover" CellPadding="4" DataKeyNames="id_torneo" ForeColor="#333333"
                        GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay eventos para mostrar" OnRowCommand="gv_eventos_RowCommand"
                        AllowPaging="True" OnPageIndexChanging="gv_eventos_PageIndexChanging" PageSize="20">
                        <Columns>
                            <asp:BoundField DataField="nombre" HeaderText="Evento" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                            <asp:BoundField DataField="estado" HeaderText="Estado" />
                            <asp:ButtonField CommandName="seleccionar" Text="Seleccionar" ItemStyle-ForeColor="#007bff" HeaderText="Seleccionar" />
                        </Columns>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                    </asp:GridView>
                </div>
            </div>
        </div>

        <div>
            <p>&nbsp;</p>
        </div>

        <asp:Button ID="btn_Cancelar" runat="server" Text="Volver" formnovalidate="true" CssClass="btn btn-link" CausesValidation="false" OnClick="btn_Cancelar_Click"/>
    </form>

</asp:Content>
