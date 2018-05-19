<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="GraduarAlumno.aspx.cs" Inherits="JJSS.Presentacion.GraduarAlumno" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <form id="formGraduacion" runat="server">

        <div class="container">
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

            </asp:Panel>
        </div>

        <div>
            <p>&nbsp;</p>
        </div>
        <div class="row centered justify-content-center">
            <h1>Graduar Alumnos</h1>
        </div>
        <div>
            <p>&nbsp;</p>
        </div>


        <div class=" container">
            <div class=" p-3">

                <%--   <div class="row">
                    <div class=" col-2  centered">
                        <h2 class="">Filtros</h2>
                    </div>
                </div>
                <div>
                    <p>&nbsp;</p>
                </div>--%>

                <div class="row ">

                    <div class="col-md-1 col-sm-12 text-left p-1"><strong>Disciplina</strong></div>
                    <div class="col-md-4  col-sm-12 centered p-1">
                        <asp:RadioButtonList ID="rb_tipo_clase" CssClass="border rounded caja2" runat="server">
                            <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-1  col-sm-12 text-left p-1"><strong>Apellido</strong></div>
                    <div class="col-md-3 col-sm-12 ">
                        <asp:TextBox ID="txt_filtro_apellido" CssClass="caja2" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-2 col-sm-12 centered justify-content-center p-1">
                        <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="btn btn-outline-dark centered" OnClick="btn_buscar_Click" ValidationGroup="vgFiltro" />
                    </div>

                </div>
            </div>

            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="view_elegir_graduacion" runat="server">

                    <div class="row centered">
                        <div class="row centered">
                            <p>&nbsp;</p>
                        </div>
                    </div>

                    <asp:GridView ID="gv_graduacion" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" DataKeyNames="idAlu" EmptyDataText="No existen elementos para esta selección" AllowPaging="True" OnPageIndexChanging="gv_graduacion_PageIndexChanging" PageSize="20">
                        <Columns>
                            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="tipo" HeaderText="Disciplina" />
                            <asp:BoundField DataField="faja" HeaderText="Faja Actual" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha de Última Graduación" />
                            <asp:TemplateField HeaderText="Grados a aumentar" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_grados" runat="server" required="true" type="number" min="0" max="20" CssClass="form-control" Text="0"></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle CssClass="table" />
                    </asp:GridView>

                    <div class="row centered justify-content-center">
                        <asp:Button ID="btn_aceptar" CssClass="btn btn-outline-dark" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click" ValidationGroup="vg_grados" />
                    </div>
                </asp:View>

            </asp:MultiView>

            <div class="row centered">
                <p>&nbsp;</p>
            </div>
            <div class="row centered">
                <asp:Button ID="btn_cancelar" runat="server" Text="Volver" CssClass="btn btn-link" formnovalidate="true" OnClick="btn_cancelar_Click" CausesValidation="false" />
            </div>
        </div>

    </form>


</asp:Content>
