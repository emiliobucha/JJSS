<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarSedes.aspx.cs" Inherits="JJSS.Presentacion.Administracion.AdministrarSedes" %>
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
                        <h1>Listado de Categorias</h1>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="container">

                        <div class="form-group border rounded p-4 ">

                            <!--Boton-->
                            <div class="row justify-content-center">
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>Nombre</strong>
                                </div>

                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:TextBox ID="txt_filtro_nombre" type="number" CssClass="caja2" min="0" runat="server"></asp:TextBox>
                                </div>
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>Disciplina</strong>
                                </div>
                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:DropDownList ID ="ddl_filtro_disciplina" runat="server" CssClass="caja2"></asp:DropDownList>
                                </div>
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>Estado</strong>
                                </div>
                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:DropDownList ID ="ddl_filtro_estado" runat="server" CssClass="caja2"></asp:DropDownList>
                                </div>

                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <asp:Button ID="btn_buscar" runat="server" Text="Buscar" OnClick="btn_buscar_Click" CssClass="btn btn-outline-dark" />
                                </div>

                                <asp:HyperLink CssClass="btn btn-link" Text="Ir a registrar" runat="server" href="CrearCategoria.aspx"></asp:HyperLink>
                            </div>

                            <div class="row centered justify-content-center">
                                <asp:GridView ID="gvCategorias" runat="server" CssClass="table" CellPadding="4" DataKeyNames="id" OnPageIndexChanging="gvCategorias_PageIndexChanging"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay categorias para mostrar"
                                    OnRowCommand="gvCategorias_RowCommand" AllowPaging="True" PageSize="20">
                                    <Columns>
                                        <asp:BoundField DataField="alu_apellido" HeaderText="Apellido" SortExpression="apellido" />
                                        <asp:BoundField DataField="alu_nombre" HeaderText="Nombre" SortExpression="nombre" />
                                        <asp:BoundField DataField="alu_dni" HeaderText="D.N.I" SortExpression="dni" />
                                        <asp:BoundField DataField="alu_estado" HeaderText="Estado" SortExpression="estado" />
                                        <asp:ButtonField CommandName="eliminar" Text="Eliminar" HeaderText="Eliminar" />
                                        <asp:ButtonField CommandName="seleccionar" Text="Seleccionar" HeaderText="Seleccionar" />
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
