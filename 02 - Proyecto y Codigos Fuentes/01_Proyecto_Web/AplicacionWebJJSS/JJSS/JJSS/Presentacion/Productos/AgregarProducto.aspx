﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site2.master" AutoEventWireup="true" CodeBehind="AgregarProducto.aspx.cs" Inherits="JJSS.Presentacion.AgregarProducto" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <section id="inscripcionTorneo" title="inscripcionTorneo"></section>

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
    <form id="form1" runat="server">
        <div class="row centered">
            <p>&nbsp;</p>
        </div>
        <div class="col-sm-12 col-xs-12 col-md-12 col-lg-12">
            <asp:Button ID="btn_formulario" runat="server" CssClass="btn btn-default" Text="Agregar Productos" CausesValidation="false" OnClick="btn_formulario_Click" />
            <asp:Button ID="btn_grilla" runat="server" CssClass="btn btn-default" Text="Ver Productos" CausesValidation="false" OnClick="btn_grilla_Click" />
        </div>

        <asp:MultiView ID="MultiView1" runat="server">

            <!-------------AGREGAR PRODUCTOS--------------->

            <asp:View ID="view_formulario" runat="server">
                <asp:Panel ID="pnlFormulario" runat="server">
                    <div id="agregarProductoswrap">
                        <div class="container">
                            <div class="row mt centered">
                                <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                                    <asp:Label ID="lbl_agregar_prodcutos" runat="server" Text="AGREGAR PRODUCTOS" Font-Size="XX-Large"></asp:Label>
                                </div>

                                <div class="form-group ">

                                    <div class="row centered col-md-12 col-lg-12 col-sm-12 col-xs-12">
                                        <p>&nbsp;</p>
                                    </div>

                                    <!--Ingresar nombre-->

                                    <div class="row centered">
                                        <div class="col-md-2 col-lg-2 col-sm-10 col-xs-10">
                                            <label class="pull-left">Nombre</label>
                                        </div>
                                        <div class="col-md-3 col-lg-3 col-sm-10 col-xs-10">
                                            <asp:TextBox ID="txt_nombre" class="caja2" required="true" MaxLength="50" runat="server" placeholder="Ingrese nombre"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <!--categoria-->

                                    <div class="row centered">
                                        <div class="col-md-2 col-lg-2 col-sm-10 col-xs-10">
                                            <label class="pull-left">Categoria</label>
                                        </div>

                                        <div class="col-md-3 col-lg-3  col-sm-10 col-xs-10">
                                            <asp:DropDownList class="caja2" ID="ddl_categoria" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <!--Foto-->
                                    <div class="row centered">
                                        <div class="col-md-2 col-lg-2 hidden-sm hidden-xs"></div>

                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                                                <label class=" pull-left">Imagen</label>
                                            </div>
                                            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                                                <input id="avatarUpload" type="file" name="file" onchange="previewFile()" runat="server" />
                                            </div>
                                            <%--<asp:FileUpload ID="avatarUpload" runat="server" />--%>
                                            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                                                <asp:Image ID="Avatar" CssClass="pull-left" runat="server" Height="225px" ImageUrl="~/Images/NoUser.jpg" Width="225px" />
                                            </div>
                                        </asp:Panel>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <!--Boton Aceptar-->
                                    <div class="row centered">
                                        <asp:Button ID="btn_aceptar" type="submit" class="btn btn-default" runat="server" Text="Aceptar" ValidationGroup="vgDatos" OnClick="btn_aceptar_Click" />



                                    </div>
                                    <asp:Button ID="Button1" runat="server" Text="Volver a inicio" CssClass="btn-link pull-left" CausesValidation="false" formnovalidate="true" OnClick="btn_cancelar_Click" />


                                </div>

                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </asp:View>


            <!-------------VER PRODUCTOS--------------->
            <asp:View ID="view_grilla" runat="server">

                <div class=" container">
                    <div class="row centered">
                        <p>&nbsp;</p>
                        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                            <asp:Label ID="lbl_grilla" runat="server" Text="VER PRODUCTOS" Font-Size="XX-Large"></asp:Label>
                        </div>
                        <div>
                            <p>&nbsp;</p>
                        </div>
                    </div>
                    <!-------------filtro--------------->
                    <div class="row centered">

                        <div class="col-md-1 col-sm-2 col-xs-2">
                            <strong>Nombre</strong>
                        </div>
                        <div class="col-md-2 col-sm-6 col-xs-6 ">
                            <asp:TextBox ID="txt_filtro_nombre" runat="server" CssClass="caja2"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="caracteres_nombre_filtro" runat="server" ControlToValidate="txt_filtro_nombre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Nombre demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgFiltro"> </asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-1 col-sm-3 col-xs-3 ">
                            <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="btn btn-default" OnClick="btn_buscar_Click" ValidationGroup="vgFiltro" />
                        </div>
                    </div>


                    <div>
                        <p>&nbsp;</p>
                    </div>

                    <!-------------grilla--------------->
                    <div class="col-lg-12 col-xs-12 col-md-12 col-sm-12">
                        <div>
                            <asp:GridView ID="gv_productos" CssClass="table" EmptyDataText="No hay productos para mostrar" AllowPaging="True" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="gv_productos_PageIndexChanging" PageSize="20">
                                <Columns>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="categoria" HeaderText="Categoria" />
                                    <asp:BoundField DataField="stock" HeaderText="Stock Disponible" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:View>


        </asp:MultiView>

    </form>


</asp:Content>
<asp:Content ID="cphP" ContentPlaceHolderID="cphP" runat="server">

    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=Avatar.ClientID %>');
            var file = document.querySelector('#<%=avatarUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>

</asp:Content>
