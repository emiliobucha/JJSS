<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarAlumnos.aspx.cs" Inherits="JJSS.Presentacion.Administracion.AdministrarAlumnos" %>

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

                            <!--Boton-->
                            <div class="row justify-content-center">
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>DNI</strong>
                                </div>

                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:TextBox ID="txt_filtro_dni" type="number" CssClass="caja2" min="0" runat="server"></asp:TextBox>
                                </div>
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>Apellido</strong>
                                </div>
                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:TextBox ID="txt_filtro_apellido" CssClass="caja2" runat="server"></asp:TextBox>
                                </div>
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <strong>Estado</strong>
                                </div>
                                <div class=" col-lg-2 col-md-2 col-sm-12">
                                    <asp:DropDownList ID ="ddl_filtro_estado" runat="server" CssClass="caja2"></asp:DropDownList>
                                </div>

                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <asp:Button ID="btn_buscar_alumno" runat="server" Text="Buscar" OnClick="btn_buscar_alumno_Click" ValidationGroup="vgFiltro" CssClass="btn btn-outline-dark" />
                                </div>

                                <asp:HyperLink CssClass="btn btn-link" Text="Ir a registrar" runat="server" href="RegistrarAlumno.aspx"></asp:HyperLink>
                            </div>

                            <div class="row centered justify-content-center">
                                <asp:GridView ID="gvAlumnos" runat="server" CssClass="table" CellPadding="4" DataKeyNames="alu_dni" OnPageIndexChanging="gvAlumnos_PageIndexChanging"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay alumnos para mostrar"
                                    OnRowCommand="gvAlumnos_RowCommand" AllowPaging="True" PageSize="20">
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
