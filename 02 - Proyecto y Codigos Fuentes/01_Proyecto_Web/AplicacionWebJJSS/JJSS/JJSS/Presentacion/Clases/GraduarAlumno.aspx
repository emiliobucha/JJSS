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
            <h1>Graduación Alumnos</h1>
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
                        <asp:DropDownList ID="ddl_tipo_clase" runat="server" CssClass="border rounded caja2">

                        </asp:DropDownList>
                       <%-- <asp:RadioButtonList ID="rb_tipo_clase" CssClass="border rounded caja2" runat="server">
                            <asp:ListItem Selected="True" Value="0">&nbsp;Todos</asp:ListItem>
                        </asp:RadioButtonList>--%>
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

                    <asp:GridView ID="gv_graduacion" runat="server" CssClass="table table-hover" AutoGenerateColumns="False" DataKeyNames="idAlu, idTipo" 
                        EmptyDataText="No hay alumnos para mostrar" AllowPaging="True" OnPageIndexChanging="gv_graduacion_PageIndexChanging" 
                        PageSize="20" OnRowCommand="gv_graduacion_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="tipo" HeaderText="Disciplina" />
                            <asp:BoundField DataField="faja" HeaderText="Faja Actual" />
                            <asp:BoundField DataField="fechaParaMostrar" HeaderText="Fecha de Última Graduación" />
                            <asp:ButtonField CommandName="graduar" Text="Graduar" ItemStyle-ForeColor="#007bff" HeaderText="Graduar" />
                        </Columns>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                        <EmptyDataRowStyle CssClass="table" />
                    </asp:GridView>

                </asp:View>

            </asp:MultiView>

            <div class="row centered">
                <p>&nbsp;</p>
            </div>
            <div class="container p-1">
                <div class="row centered">
                    <div class="col col-auto">
                        <asp:HyperLink ID="lnk_volver" runat="server" Text="Volver" class="btn btn-link" href="Menu_Clase.aspx"></asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>

    </form>


</asp:Content>
