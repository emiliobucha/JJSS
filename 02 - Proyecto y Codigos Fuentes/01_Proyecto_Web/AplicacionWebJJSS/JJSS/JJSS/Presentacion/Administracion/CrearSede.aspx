<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="CrearSede.aspx.cs" Inherits="JJSS.Presentacion.Administracion.CrearSede" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContenido" runat="server">


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
                <h1 class=" centered ">Crear Sede/Academia</h1>
            </div>

            <div>
                <p>&nbsp;</p>
            </div>


            <div class="container">

                <div class="p-2">
                    <asp:LinkButton runat="server" ID="l" class="btn btn-link pull-right" Text="Ir al listado de sedes" href="AdministrarSedes.aspx"></asp:LinkButton>
                </div>

                <div class="form-group  border rounded p-1">
                    <div>
                        <p>&nbsp;</p>
                    </div>

                    <div class="row pl-lg-5 pl-md-5 ">
                        <div class="col-lg-2 col-md-2 col-sm-12">
                            <label class="pull-left">¿Qué deseas crear?</label>
                        </div>
                        <div class="col col-auto">
                            <asp:RadioButtonList ID="rbCrear" runat="server" AutoPostBack="False">
                                <asp:ListItem Selected="True" Value="sede">&nbsp;Sede</asp:ListItem>
                                <asp:ListItem Value="academia">&nbsp;Academia</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div>
                        <p>&nbsp;</p>
                    </div>

                    <!--Ingresar nombre-->

                    <div class="row centered pl-lg-5 pl-md-5">
                        <div class="col-lg-2 col-md-2 col-sm-12">
                            <label class="pull-left">Nombre <a class="text-danger">*</a></label>
                        </div>
                        <div class="col col-lg-3 col-md-3 col-sm-10">
                            <asp:TextBox ID="txt_nombre" class="caja2" required="true" MaxLength="50" runat="server" placeholder="Ingrese nombre"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>


                    <div class="row centered  pl-lg-5 pl-md-5">
                        <div class="col-lg-2 col-md-2 col-sm-12">
                            <label class="pull-left">Teléfono</label>
                        </div>
                        <div class="col col-lg-3 col-md-3 col-sm-10">
                            <asp:TextBox ID="txt_telefono" class="caja2" type="number" min="0" max="9999999999999" runat="server" placeholder="Ingrese teléfono"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="col col-auto">
                        <asp:Label ID="lbl_direccion" runat="server" Text="Dirección" Font-Size="Large"></asp:Label>
                    </div>
                    <!--Ingresar calle y numero-->

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <!-- Calle y numero -->
                    <div class="row p-1  pl-lg-5 pl-md-5">
                        <div class="col-lg-2 col-md-2 col-sm-12">
                            <label>Calle <a class="text-danger">*</a></label>
                        </div>
                        <div class="col col-md-4 col-lg-4 col-sm-10">
                            <asp:TextBox ID="txt_calle" class="caja2" type="text" required="true" MaxLength="50" runat="server" placeholder="Ingrese calle"></asp:TextBox>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-12 pl-lg-5 pl-md-5">
                            <label>Número <a class="text-danger">*</a></label>
                        </div>
                        <div class="col col-md-1 col-lg-1 col-sm-10 col-xs-10">
                            <asp:TextBox ID="txt_numero" type="number" required="true" min="0" max="100000" class="caja2" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row p-1  pl-lg-5 pl-md-5">
                        <div class="col-lg-2 col-md-2 col-sm-12 ">
                            <label>Piso</label>
                        </div>
                        <div class="col col-md-1 col-lg-1 col-sm-10 col-xs-10">
                            <asp:TextBox ID="txt_piso" class="caja2" type="number" min="0" max="100000" runat="server"></asp:TextBox>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-2">
                            <label>Dpto</label>
                        </div>
                        <div class="col col col-md-2 col-lg-2 col-sm-10 col-xs-10">
                            <asp:TextBox ID="txt_nro_dpto" type="text" MaxLength="20" class="caja2" runat="server"></asp:TextBox>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-2">
                            <label>Torre</label>
                        </div>
                        <div class="col col-md-2 col-lg-2 col-sm-10 col-xs-10">
                            <asp:TextBox ID="txt_torre" type="text" MaxLength="20" class="caja2" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    
                            <!-- Barrio -->
                            <div class="row p-1  pl-lg-5 pl-md-5">
                                <div class="col-lg-2 col-md-2 col-sm-12 ">
                                    <label>Barrio </label>
                                </div>
                                <div class="col col-md-3 col-lg-3 col-sm-10 col-xs-10">
                                    <asp:TextBox ID="txt_barrio" type="text" MaxLength="20" class="caja2" runat="server"></asp:TextBox>
                                </div>
                            </div>

                    <!-- Provincia -->
                    <div class="row p-1  pl-lg-5 pl-md-5">
                        <div class="col-lg-2 col-md-2 col-sm-12 ">
                            <label>Provincia <a class="text-danger">*</a></label>
                        </div>
                        <div class="col col-md-3 col-lg-3 col-sm-10 col-xs-10">
                            <asp:DropDownList class="caja2" ID="ddl_provincia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_provincia_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Localidad -->
                    <div class="row p-1  pl-lg-5 pl-md-5">
                        <div class="col-lg-2 col-md-2 col-sm-12 ">
                            <label>Localidad <a class="text-danger">*</a></label>
                        </div>
                        <div class="col col-md-3 col-lg-3 col-sm-10 col-xs-10">
                            <asp:DropDownList class="caja2" ID="ddl_localidad" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div>
                        <p>&nbsp;</p>
                    </div>

                    <!--Boton Aceptar-->
                    <div class="row centered">
                        <div class="col">
                            <asp:Button ID="btn_aceptar" type="submit" class="btn btn-outline-dark" runat="server" Text="Aceptar" ValidationGroup="vgDatos" OnClick="btn_aceptar_Click" />
                        </div>
                    </div>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class=" p-2 ">
                        <p class="text-danger pull-right " style="font-size: small">* Campo requerido</p>
                    </div>

                    <div>
                        <p>&nbsp;</p>
                    </div>

                </div>

                 

                <div class="row pull-left">
                    <div class="col">
                        <asp:Button ID="btn_volver" class="btn btn-link" runat="server" Text="Volver" OnClick="btn_volver_Click" formnovalidate="true" />
                    </div>
                </div>
            </div>

           



            <div>
                <p>&nbsp;</p>
            </div>

        </asp:Panel>

    </form>


</asp:Content>
