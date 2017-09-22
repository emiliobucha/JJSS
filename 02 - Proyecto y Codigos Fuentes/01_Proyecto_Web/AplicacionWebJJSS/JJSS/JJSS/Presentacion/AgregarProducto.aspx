<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="AgregarProducto.aspx.cs" Inherits="JJSS.Presentacion.AgregarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">

    <section id="inscripcionTorneo" title="inscripcionTorneo"></section>

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
    <form id="form1" runat="server">
        <div class="row centered">
            <p>&nbsp;</p>
        </div>
    <asp:Button ID="btn_formulario" runat="server" CssClass="btn btn-default" Text="Agregar Productos" CausesValidation="false" OnClick="btn_formulario_Click" />
    <asp:Button ID="btn_grilla" runat="server" CssClass="btn btn-default" Text="Ver Productos" CausesValidation="false" OnClick="btn_grilla_Click" />

    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="view_formulario" runat="server">
            <asp:Panel ID="pnlFormulario" runat="server">
                <div id="agregarProductoswrap">
                    <div class="container">
                        <div class="row mt centered">
                            <div>
                                <asp:Label ID="lbl_agregar_prodcutos" runat="server" Text="AGREGAR PRODUCTOS" Font-Size="XX-Large"></asp:Label>
                            </div>

                            
                                <div class="form-group ">
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>


                                    <!--Ingresar nombre-->

                                    <div class="row centered">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left">Nombre</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txt_nombre" class="caja2" runat="server" placeholder="Ingrese nombre"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:RequiredFieldValidator ID="requeridoNombre" runat="server" ErrorMessage="Debe ingresar el nombre" ControlToValidate="txt_nombre" CssClass="text-danger" Display="Dynamic" ValidationGroup="vgDatos"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="caracteres_nombre" runat="server" ControlToValidate="txt_nombre" CssClass="text-danger" Display="Dynamic" ErrorMessage="Nombre demasiado largo" ValidationExpression="^[\s\S]{0,50}$" ValidationGroup="vgDatos"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>

                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <!--categoria-->

                                    <div class="row centered">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2">
                                            <label class="pull-left">Categoria</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList class="caja2" ID="ddl_categoria" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <!--Foto-->
                                    <div class="row centered">
                                        <div class="col-md-2"></div>
                                        <div class="col-xs-2">
                                            <asp:Panel ID="Panel1" runat="server">

                                                <label class=" pull-left">Imagen</label>
                                                <input id="avatarUpload" type="file" name="file" onchange="previewFile()" runat="server" />
                                                <%--<asp:FileUpload ID="avatarUpload" runat="server" />--%>
                                                <asp:Image ID="Avatar" runat="server" Height="225px" ImageUrl="~/Images/NoUser.jpg" Width="225px" />
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="row centered">
                                        <p>&nbsp;</p>
                                    </div>

                                    <!--Boton Aceptar-->
                                    <div class="row centered">
                                        <asp:Button ID="btn_aceptar" type="submit" class="btn btn-default" runat="server" Text="Aceptar" ValidationGroup="vgDatos" OnClick="btn_aceptar_Click" />

                                        <asp:Button ID="btn_cancelar" runat="server" CausesValidation="False" class="btn btn-default" OnClick="btn_cancelar_Click" Text="Cancelar" type="submit" />

                                    </div>


                                </div>
                            
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </asp:View>

        <asp:View ID="view_grilla" runat="server">

            <div class="row mt centered">
                <div>
                    <asp:Label ID="lbl_grilla" runat="server" Text="VER PRODUCTOS" Font-Size="XX-Large"></asp:Label>

                    <asp:GridView ID="gv_productos" CssClass="table" AllowPaging="True" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="gv_productos_PageIndexChanging" PageSize="20">
                        <Columns>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="categoria" HeaderText="Categoria" />
                            <asp:BoundField DataField="stock" HeaderText="Stock Disponible" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:View>


    </asp:MultiView>
    </form>


</asp:Content>
