<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarTipoClase.aspx.cs" Inherits="JJSS.Presentacion.Administracion.AdministrarTipoClase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">

    <div class="container">
        <asp:Panel ID="pnl_mensaje_exito" runat="server" Visible="false">
            <div class="col-md-2 hidden-sm"></div>
            <div class="col-md-8 col-sm-10 col-xs-10">
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
            <div class="col-md-2 hidden-sm"></div>
            <div class="col-md-8 col-sm-10 col-xs-10">
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
    </div>

    <form id="form1" runat="server">

        <asp:Panel ID="pnlFormulario" runat="server">

            <div>
                <p>&nbsp;</p>
            </div>

            <div class="row centered justify-content-center">
                <h1 class=" centered ">Administrar Tipo de Clase</h1>
            </div>

            <div>
                <p>&nbsp;</p>
            </div>

            <div class="container">

                
                <div class="border rounded p-5">
                    
                    <!--Ingresar nombre-->

                    <div class="row  pl-lg-5 pl-md-5 justify-content-center">
                        <div class="col-md-2 col-lg-2 col-sm-12 col-xs-12 ">
                            <label>Nombre <a class="text-danger">*</a></label>
                        </div>
                        <div class="col col-lg-3 col-md-3 col-sm-10">
                            <asp:TextBox ID="txt_nombre" class="caja2" required="true" MaxLength="60" runat="server"></asp:TextBox>
                        </div>
                        <div class="col col-lg-3 col-md-3 col-sm-10">
                            <asp:Button ID="btn_aceptar" type="submit" class="btn btn-outline-dark" runat="server" Text="Agregar" OnClick="btn_aceptar_Click" />
                        </div>
                    </div>

                    <div class=" p-2 ">
                        <p class="text-danger pull-right " style="font-size: small">* Campo requerido</p>
                    </div>
                    
                </div>

                <div>
                    <p>&nbsp;</p>
                </div>

                <div class="border rounded p-2">
                    <asp:GridView ID="gv_tipo_evento" runat="server" CssClass="table" CellPadding="4" DataKeyNames="id_tipo_clase" OnPageIndexChanging="gv_tipo_evento_PageIndexChanging"
                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay tipos de clase para mostrar"
                        OnRowCommand="gv_tipo_evento_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />

                            <asp:ButtonField CommandName="eliminar" Text="Eliminar" HeaderText="Eliminar" />
                        </Columns>
                    </asp:GridView>
                </div>

                <div>
                    <p>&nbsp;</p>
                </div>

                <div class="row pull-left">
                    <div class="col">
                        <asp:Button ID="btn_volver" class="btn btn-link" runat="server" Text="Volver" OnClick="btn_volver_Click" formnovalidate="true" />
                    </div>
                </div>

                <div>
                    <p>&nbsp;</p>
                </div>

            </div>


        </asp:Panel>

    </form>
</asp:Content>
