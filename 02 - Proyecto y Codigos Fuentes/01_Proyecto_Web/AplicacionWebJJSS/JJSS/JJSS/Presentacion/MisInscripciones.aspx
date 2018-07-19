<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="MisInscripciones.aspx.cs" Inherits="JJSS.Presentacion.MisInscripciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">

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

    <asp:Panel ID="pnlFormulario" runat="server" CssClass="justify-content-center" ValidateRequestMode="Disabled">
        <div class=" container">
            <div class="row centered">
                <p>&nbsp;</p>
            </div>
            <div class="row centered justify-content-center">
                <h1 class=" centered ">Mis Inscripciones</h1>
            </div>

            <div>
                <p>&nbsp;</p>
            </div>

            <form id="form1" runat="server">

                <asp:Panel ID="pnl_clase" CssClass="panel panel-default border rounded p-2" runat="server">

                    <div class="form-group">
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <div class="row centered justify-content-center">
                            <h2 class=" centered ">Mis inscripciones de clases</h2>
                        </div><br />

                        <div class="col-12">
                            <asp:GridView ID="gv_clases" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None"
                                AutoGenerateColumns="False" EmptyDataText="No tenés inscripciones a clases" AllowPaging="True"
                                OnPageIndexChanging="gv_clases_PageIndexChanging" PageSize="7" DataKeyNames="id_inscripcion" OnRowCommand="gv_clases_RowCommand"
                                 OnRowDataBound="gv_clases_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="nombre" HeaderText="Clase" />
                                    <asp:BoundField DataField="tipo_clase" HeaderText="Disciplina" />
                                    <asp:BoundField DataField="fecha_inscripcion" HeaderText="Fecha Inscripción" />
                                    <asp:BoundField DataField="prox_vencimiento" HeaderText="Próximo Vencimiento" />
                                    
                                    <asp:TemplateField HeaderText="Pagar" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" ID="lnk_pago_clase" href="/Presentacion/Pagos/PagosPanel.aspx" Text="Pagar"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dar de Baja" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Button runat="server" Text="Dar de Baja" ID="btn_baja" CommandName ="baja" CssClass="btn btn-link" CommandArgument='<%#Eval("idClase") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                            </asp:GridView>


                        </div>
                        <div class="row pull-right ">
                            <asp:Button runat="server" ID="btn_todos_clases" CssClass="btn btn-link" OnClick="btn_todos_clases_Click" />
                        </div><br />

                    </div>
                </asp:Panel>
                <br />

                <asp:Panel ID="pnl_torneos" CssClass="panel panel-default border rounded p-2" runat="server">

                    <div class="form-group">
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <div class="row centered justify-content-center">
                            <h2 class=" centered ">Mis inscripciones de torneos</h2>
                        </div><br />

                        <div class="col-12">
                            <asp:GridView ID="gv_torneos" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None"
                                AutoGenerateColumns="False" EmptyDataText="No tenés inscripciones a torneos" AllowPaging="True" PageSize="7"
                                OnPageIndexChanging="gv_torneos_PageIndexChanging" DataKeyNames="id_inscripcion" OnRowDataBound="gv_torneos_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="nombre" HeaderText="Torneo" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha Torneo" />
                                    <asp:BoundField DataField="hora" HeaderText="Hora Torneo" />
                                    <asp:BoundField DataField="fecha_inscripcion" HeaderText="Fecha Inscripción" />
                                    
                                    <asp:TemplateField HeaderText="Pagar" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" ID="lnk_pago" href="/Presentacion/Pagos/PagosPanel.aspx" Text="Pagar"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                            </asp:GridView>
                        </div>
                        
                        <div class="row pull-right ">
                            <asp:Button runat="server" ID="btn_todos_torneos" CssClass="btn btn-link" OnClick="btn_todos_torneos_Click" />
                        </div><br />

                    </div>
                </asp:Panel>
                <br />

                <asp:Panel ID="pnl_eventos" CssClass="panel panel-default border rounded p-2" runat="server">

                    <div class="form-group">
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>

                        <div class="row centered justify-content-center">
                            <h2 class=" centered ">Mis inscripciones de eventos</h2>
                        </div><br />

                        <div class="col-12">
                            <asp:GridView ID="gv_eventos" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None"
                                AutoGenerateColumns="False" EmptyDataText="No tenés inscripciones a eventos" AllowPaging="True" PageSize="7"
                                OnPageIndexChanging="gv_eventos_PageIndexChanging" DataKeyNames="id_inscripcion" OnRowDataBound="gv_torneos_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="nombre" HeaderText="Evento" />
                                    <asp:BoundField DataField="tipo_evento" HeaderText="Tipo Evento" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha Evento" />
                                    <asp:BoundField DataField="hora" HeaderText="Hora Evento" />
                                    <asp:BoundField DataField="fecha_inscripcion" HeaderText="Fecha Inscripción" />
                                    
                                    <asp:TemplateField HeaderText="Pagar" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" ID="lnk_pago" href="/Presentacion/Pagos/PagosPanel.aspx" Text="Pagar"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                            </asp:GridView>
                        </div>

                        
                        <div class="row pull-right ">
                            <asp:Button runat="server" ID="btn_todos_eventos" CssClass="btn btn-link" OnClick="btn_todos_eventos_Click" />
                        </div><br />
                    </div>
                </asp:Panel>


                <div class="row centered">
                    <p>&nbsp;</p>
                </div>

                <asp:HyperLink runat="server" href="/Presentacion/MenuInicial.aspx" CssClass="btn btn-link" Text="Volver"></asp:HyperLink>

            </form>

        </div>
    </asp:Panel>


</asp:Content>
