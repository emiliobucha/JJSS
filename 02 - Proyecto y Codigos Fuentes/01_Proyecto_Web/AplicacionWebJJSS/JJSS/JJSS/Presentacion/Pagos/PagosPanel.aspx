<%@ Page Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" AutoEventWireup="true" CodeBehind="PagosPanel.aspx.cs" Inherits="JJSS.Presentacion.Pagos.PagosPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <form id="formRegAlumno" runat="server">

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

        <div id="grillawrap">

            <asp:Panel ID="pnl_mostrar_alumnos" runat="server">

                <div id="mostrarAlumnowrap">

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="row centered justify-content-center">
                        <h1>Listado de Alumnos</h1>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="container">

                        <div class="form-group border rounded p-4 ">

                      
                            <div class="row centered justify-content-center">
                                <asp:GridView ID="gvPagos" runat="server" CssClass="table" CellPadding="4" DataKeyNames="alu_dni" OnPageIndexChanging="gvPagos_PageIndexChanging"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay alumnos para mostrar"
                                    OnRowCommand="gvPagos_RowCommand" AllowPaging="True" PageSize="20">
                                    <Columns>
                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="tipo" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="nombre" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fecha" />
                                        <asp:BoundField DataField="Monto" HeaderText="Monto" SortExpression="monto" />
                                        <asp:ButtonField CommandName="pago" Text="Registrar pago" HeaderText="Registrar Pago" />
                                    </Columns>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                    <PagerSettings Mode="NextPrevious" Position="TopAndBottom" />
                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

    </form>
</asp:Content>
