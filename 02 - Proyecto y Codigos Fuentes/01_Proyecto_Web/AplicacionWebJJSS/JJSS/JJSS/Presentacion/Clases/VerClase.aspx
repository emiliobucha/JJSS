<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="VerClase.aspx.cs" Inherits="JJSS.Presentacion.Clases.VerClase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <form id="formRegAlumno" runat="server">
        <div class="container">

            <div class="row container">
                <div class="col-lg-12 col-md-12 col-sm-12 centered ">
                    <asp:Panel ID="pnl_datos_clase" runat="server">
                        <div class="centered">
                            <h1>Datos de la Clase</h1>
                            <p>&nbsp;</p>
                        </div>

                        <div style="border: 1px medium gray">
                            <div>
                                <asp:Label ID="lbl_nombre_clase" CssClass="h3" runat="server" />
                            </div>
                            <div>
                                <asp:Label ID="lbl_tipo_clase" runat="server" />
                            </div>
                            <div>
                                <asp:Label ID="lbl_ubicacion" runat="server" />
                            </div>
                            <div>
                                <asp:Label ID="lbl_profesor" runat="server" />
                            </div>
                            <div>
                                <asp:Label ID="lbl_precio" runat="server" />
                            </div>

                            <p>&nbsp;</p>
                            <div class="centered center-block">

                                <asp:GridView ID="dg_horarios" runat="server" DataKeyNames="id_horario" EmptyDataText="No hay horarios actualmente" AutoGenerateColumns="False" GridLines="None" CssClass="table" BorderColor="Black" BorderStyle="Double">

                                    <Columns>
                                        <asp:BoundField HeaderStyle-CssClass="text-center" HeaderText="Día" DataField="nombre_dia">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderStyle-CssClass="text-center" HeaderText="Desde" DataField="hora_desde">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderStyle-CssClass="text-center" HeaderText="Hasta" DataField="hora_hasta">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </asp:Panel>

                </div>
            </div>
        </div>
        <div class="row centered">
            <div class="col col-auto">
                <asp:HyperLink ID="lnk_volver" runat="server" Text="Volver" class="btn btn-link" href="Menu_Clase.aspx"></asp:HyperLink>
            </div>
        </div>
    </form>
</asp:Content>
