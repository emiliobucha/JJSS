<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="MisInscripciones.aspx.cs" Inherits="JJSS.Presentacion.MisInscripciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
    <script type='text/javascript'>
        var x = 0;
        function button() {
            var objwordstonum = document.getElementById('<%=txtIDSeleccionado.ClientID%>');
            objwordstonum.value = x;
            return true;
        }
        function openModal(id) {
            $('[id*=confirmacion]').modal('show');
            x = id;
            return false;
        }
    </script>
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
                        </div>
                        <br />

                        <div class="col-12">
                            <asp:GridView ID="gv_clases" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None"
                                AutoGenerateColumns="False" EmptyDataText="No tenés inscripciones a clases" AllowPaging="True"
                                OnPageIndexChanging="gv_clases_PageIndexChanging" PageSize="7" DataKeyNames="id_inscripcion">
                                <Columns>
                                    <asp:BoundField DataField="nombre" HeaderText="Clase" />
                                    <asp:BoundField DataField="tipo_clase" HeaderText="Disciplina" />
                                    <asp:BoundField DataField="inscr_faja" HeaderText="Faja" />
                                    <asp:BoundField DataField="inscr_fecha_vto_mensual" HeaderText="Próximo Vencimiento" />
                                    <asp:BoundField DataField="inscr_pago" HeaderText="Pagado" />
                                    
                                    <asp:TemplateField HeaderText="Dar de Baja" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_baja" CommandName="baja" runat="server" CommandArgument='<%#Eval("id_inscripcion") %>'
                                                OnClientClick='<%# Eval("id_inscripcion", "return openModal({0})") %>'> Dar de Baja</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                            </asp:GridView>


                        </div>
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
                        </div>
                        <br />

                        <div class="col-12">
                            <asp:GridView ID="gv_torneos" runat="server" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None"
                                AutoGenerateColumns="False" EmptyDataText="No tenés inscripciones a torneos" AllowPaging="True" PageSize="7"
                                OnPageIndexChanging="gv_torneos_PageIndexChanging" DataKeyNames="id_inscripcion" OnRowDataBound="gv_torneos_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="nombre" HeaderText="Torneo" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha Torneo" />
                                    <asp:BoundField DataField="hora" HeaderText="Hora Torneo" />
                                    <asp:BoundField DataField="fecha_inscripcion" HeaderText="Fecha Inscripción" />

                                    <asp:TemplateField HeaderText="Pagado" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbl_pagado"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                            </asp:GridView>
                        </div>

                        <div class="row pull-right ">
                            <asp:Button runat="server" ID="btn_todos_torneos" CssClass="btn btn-link" OnClick="btn_todos_torneos_Click" />
                        </div>
                        <br />

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
                        </div>
                        <br />

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

                                    <asp:TemplateField HeaderText="Pagado" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbl_pagado"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                            </asp:GridView>
                        </div>


                        <div class="row pull-right ">
                            <asp:Button runat="server" ID="btn_todos_eventos" CssClass="btn btn-link" OnClick="btn_todos_eventos_Click" />
                        </div>
                        <br />
                    </div>
                </asp:Panel>


                <div class="row centered">
                    <p>&nbsp;</p>
                </div>

                <asp:HyperLink runat="server" href="/Presentacion/MenuInicial.aspx" CssClass="btn btn-link" Text="Volver"></asp:HyperLink>
                <asp:TextBox ID="txtIDSeleccionado" runat="server" Text="" hidden="true"></asp:TextBox>


                <!-- VENTANA EMERGENTE CARGA NUEVO PARTICIPANTE-->
                <div class="modal fade col-lg-12 col-md-12 col-xs-8 col-sm-8" id="confirmacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <!--Cabecera-->
                            <div class="modal-header">
                                <h4 class="modal-title" id="exampleModalLabe2">¿Seguro que desea darse de baja a esta clase? </h4>
                                
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <h5> Si lo hace no podrá asistir más a la clase y deberá contactarse con el administrador de la academia</h5>
                            </div>

                            <!--Botonero-->
                            <div class="modal-footer">
                                <asp:Button ID="btn_si" type="button" runat="server" class="btn btn-outline-dark" OnClientClick="return button()" OnClick="btn_si_Click" Text="SI" />
                                <button id="btn_no" type="button" class="btn btn-default" value="No" data-dismiss="modal">No</button>

                            </div>

                        </div>
                    </div>
                </div>

            </form>

        </div>
    </asp:Panel>


</asp:Content>
