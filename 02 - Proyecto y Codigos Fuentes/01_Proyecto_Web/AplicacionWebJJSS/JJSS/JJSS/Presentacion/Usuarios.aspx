<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="JJSS.Presentacion.Usuarios" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <section id="alumnoClases" title="alumnoClases"></section>

    <form id="formAlumnoclases" runat="server">

        <div class="container centered justify-content-center">
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
        </div>

        <div class="row mt centered justify-content-center ">
            <h1 class="centered">Usuarios</h1>
        </div>

        <div>
            &nbsp;
        </div>



        <asp:Panel ID="pnlClases" runat="server">
            <div class="container">
                <div class="form-group">

                    <asp:Panel ID="pnl_mostrar_clases" runat="server">

                        <div id="alumnosClases">

                            <div class=" row mt centered justify-content-center  p-1">
                                <div class=" col-lg-1 col-md-1 col-sm-2 p-1 text-left text-lg-right text-md-right"><strong>Nombre</strong></div>
                                <div class="col col-lg-3 col-md-3 col-sm-10 p-1">
                                    <asp:TextBox ID="txt_filtro_apellido" MaxLength="45" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class=" col-lg-1 col-md-1 col-sm-2 p-1 text-left text-lg-right text-md-right "><strong>Grupo</strong></div>
                                <div class="col col-lg-3 col-md-3 col-sm-10 p-1">
                                    <asp:TextBox ID="txt_filtro_grupo" MaxLength="45" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-md-1 col-lg-1 justify-content-center centered center-block">
                                    <asp:Button ID="btn_buscar" OnClick="btn_buscar_Click" runat="server" Text="Buscar" CssClass="btn btn-outline-dark" />
                                </div>
                                <p>&nbsp;</p>
                            </div>

                            <div>
                                &nbsp;
                            </div>

                            <div class="form-group ">
                                <!--Boton-->
                                <div class="row centered justify-content-center  p-1">

                                    <asp:GridView ID="gvUsuarios" runat="server" CssClass="table" CellPadding="4" DataKeyNames="id_usuario" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay usuarios para mostrar" OnRowCommand="gvClases_RowCommand" AllowPaging="True" OnPageIndexChanging="gvClases_PageIndexChanging" PageSize="20">
                                        <Columns>
                                            <%--  <asp:BoundField DataField="id_clase" HeaderText="ID de clase" />--%>
                                            <asp:BoundField DataField="login" HeaderText="Login" />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                            <asp:BoundField DataField="grupo_nombre" HeaderText="Grupo" />
                                            <asp:BoundField DataField="mail" HeaderText="Mail" />
                                            <asp:ButtonField CommandName="permisos" Text="Editar Permisos" HeaderText="Editar Permisos" ItemStyle-ForeColor="#007bff" />
                                        </Columns>
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class=" row">
                    <div class="col col-auto">
                        <asp:Button ID="btn_Cancelar" runat="server" Text="Volver a inicio" CssClass="btn btn-link pull-left" CausesValidation="false" formnovalidate="true" OnClick="btn_Cancelar_Click1" />
                    </div>
                </div>
                <div>
                    &nbsp;
                </div>
            </div>
        </asp:Panel>
    </form>

    <script src="https://secure.mlstatic.com/sdk/javascript/v1/mercadopago.js"></script>
    <script type="text/javascript">
        (function () { function $MPC_load() { window.$MPC_loaded !== true && (function () { var s = document.createElement("script"); s.type = "text/javascript"; s.async = true; s.src = document.location.protocol + "//secure.mlstatic.com/mptools/render.js"; var x = document.getElementsByTagName('script')[0]; x.parentNode.insertBefore(s, x); window.$MPC_loaded = true; })(); } window.$MPC_loaded !== true ? (window.attachEvent ? window.attachEvent('onload', $MPC_load) : window.addEventListener('load', $MPC_load, false)) : null; })();
    </script>




</asp:Content>
