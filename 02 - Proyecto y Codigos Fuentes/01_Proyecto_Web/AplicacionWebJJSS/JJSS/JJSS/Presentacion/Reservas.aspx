<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site2.Master" AutoEventWireup="true" CodeBehind="Reservas.aspx.cs" Inherits="JJSS.Presentacion.Reservas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <form id="formGraduacion" runat="server">

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

            <h1>ADMINISTRACIÓN DE RESERVAS</h1>
            <p>&nbsp;</p>
        </div>

        <asp:Button ID="btn_grilla" runat="server" CssClass="btn btn-default" Text="Ver reservas" CausesValidation="false" OnClick="btn_grilla_Click" />
        <asp:Button ID="btn_sin_reserva" runat="server" CssClass="btn btn-default" Text="Venta sin reserva" CausesValidation="false" OnClick="btn_sin_reserva_Click" />

        <div class="row mt centered">
            <p>&nbsp;</p>
            <h2>Filtros</h2>

            <div class="col-md-2"><strong>Apellido</strong></div>
            <div class="col-md-3">
                <asp:TextBox ID="txt_filtro_apellido" MaxLength="45" CssClass="form-control" runat="server"></asp:TextBox>

            </div>
            <asp:Button ID="btn_buscar" OnClick="btn_buscar_Click" runat="server" Text="Buscar" CssClass="btn btn-default" />
        </div>

        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="view_grilla" runat="server">
                <div class="row centered">

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>
                </div>

                <asp:GridView ID="gv_reservas" runat="server" CssClass="table" AutoGenerateColumns="False" DataKeyNames="id_reserva" EmptyDataText="No existen elementos para esta selección" AllowPaging="True" OnPageIndexChanging="gv_reservas_PageIndexChanging" OnRowCommand="gv_reservas_RowCommand" PageSize="20">
                    <Columns>
                        <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />

                        <asp:BoundField DataField="fecha" HeaderText="Fecha de Reserva" />
                        <asp:ButtonField CommandName="retirado" Text="Retirar" />
                        <asp:ButtonField CommandName="cancelado" Text="Cancelar" />
                        <asp:ButtonField CommandName="detalle" Text="Ver" />
                    </Columns>
                    <EmptyDataRowStyle CssClass="table" />
                </asp:GridView>

            </asp:View>

            <asp:View ID="view_detalle_reserva" runat="server">

                <div class="row centered">

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>
                </div>
                <p>&nbsp;</p>
                <h2>Detalle de reserva</h2>
                <p>&nbsp;</p>
                <asp:GridView ID="gv_items" CssClass="table" runat="server" DataKeyNames="id_detalle" EmptyDataText="No hay productos para mostrar" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="nombre" HeaderText="Producto" />
                        <asp:BoundField DataField="precio_venta" HeaderText="Precio Unitario ($)" />
                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                    </Columns>

                </asp:GridView>

            </asp:View>

            <asp:View ID="view_sin_reserva" runat="server">
                <div class="row centered">

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>
                </div>


            </asp:View>

        </asp:MultiView>
        <div class="row centered">
            <asp:Button ID="btn_cancelar" OnClick="btn_cancelar_Click" runat="server" Text="Volver a inicio" CssClass="btn-link" CausesValidation="false" />
        </div>
    </form>
</asp:Content>

