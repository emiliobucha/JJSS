<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarProfesores.aspx.cs" Inherits="JJSS.Presentacion.Administracion.AdministrarProfesores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <form id="formRegProfe" runat="server">

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

            <asp:Panel ID="pnl_mostrar_profes" runat="server">

                <div id="mostrarprofewrap">

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="row centered justify-content-center">
                        <h1>Listado de Profesores</h1>
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
                                    <asp:Button ID="btn_buscar_profe" runat="server" Text="Buscar" OnClick="btn_buscar_profe_Click" ValidationGroup="vgFiltro" CssClass="btn btn-outline-dark" />
                                </div>

                                <asp:HyperLink CssClass="btn btn-link" Text="Ir a registrar" runat="server" href="RegistrarProfe.aspx"></asp:HyperLink>

                            </div>
                            <div class="row centered justify-content-center">
                                <asp:GridView ID="gvprofes" runat="server" CssClass="table" CellPadding="4" DataKeyNames="dni" OnPageIndexChanging="gvprofes_PageIndexChanging"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay profes para mostrar"
                                    OnRowCommand="gvprofes_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="apellido" HeaderText="Apellido" SortExpression="apellido" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                                        <asp:BoundField DataField="dni" HeaderText="D.N.I" SortExpression="dni" />

                                        <asp:ButtonField CommandName="eliminar" Text="Eliminar" HeaderText="Eliminar" />
                                        <asp:ButtonField CommandName="seleccionar" Text="Seleccionar" HeaderText="Seleccionar" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <%--<div class="row">
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <asp:Button ID="btn_registro" runat="server" CausesValidation="false" CssClass=" btn btn-link pull-left" formnovalidate="true"
                                        OnClick="btn_registro_Click" Text="Volver a registrar" />
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </asp:Panel>


        </div>

        <div class="row centered">
            <p>&nbsp;</p>
        </div>

        <div class=" container">
            <div class="row centered">
                <asp:HyperLink runat="server" Text="Volver" CssClass="btn btn-link" href="Menu_Administracion.aspx"></asp:HyperLink>
            </div>
        </div>

        <div class="row centered">
            <p>&nbsp;</p>
        </div>
    </form>
</asp:Content>
