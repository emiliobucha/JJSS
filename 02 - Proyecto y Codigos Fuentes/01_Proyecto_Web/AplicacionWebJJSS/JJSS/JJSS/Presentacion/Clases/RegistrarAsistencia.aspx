<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarAsistencia.aspx.cs" Inherits="JJSS.Presentacion.RegistrarAsistencia" %>



<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">

    <section id="registrarAsistencia" title="registrarAsistencia"></section>

    <form id="formRegAsistencia" runat="server">
        <div id="registrowrap">

            <div class="container">
                <asp:Panel ID="pnl_mensaje_exito" runat="server" Visible="false">
                    <div class="col-md-2"></div>
                    <div class="col-md-8">
                        <div class="alert alert-success alert-dismissible" role="alert">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <a class="ui-icon ui-icon-check"></a>
                            <strong>
                                <asp:Label ID="lbl_exito" runat="server"></asp:Label></strong>
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
                            <asp:Label ID="lbl_error" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>
                </asp:Panel>
            </div>

            <asp:Panel ID="pnl_registro" runat="server">

                <div id="registrarAsistenciawrap">



                    <div class="row mt centered justify-content-center ">
                        <h1>Registro de Asistencia </h1>
                    </div>

                    <div>
                        &nbsp;
                    </div>

                    <div class="container">
                        <div class=" justify-content-center centered  border rounded p-4">
                            <div>
                                &nbsp;
                            </div>
                            <!-- DNI-->
                            <div class="row centered justify-content-center ">
                                
                                <!--Ingresar Tipo-->
                                <div class="col-md-2 col-xl-auto">
                                    <label class="pull-left">Tipo: <a class="text-danger">*</a></label>
                                </div>
                                <div class="col-md-3 col-xl-auto">
                                    <asp:DropDownList ID="ddl_tipo" class="caja2" runat="server" placeholder="Ingrese Tipo" ValidationGroup="grupoDni"></asp:DropDownList>
                                </div>
                                
                                <div class="col-md-2 col-xl-auto">
                                    <label class="pull-left">Número: <a class="text-danger">*</a></label>
                                </div>
                                <div class=" col col-lg-3 col-md-3 col-sm-12">
                                    <asp:TextBox ID="txtDni" required="true" class="caja2" runat="server" placeholder="Ingrese Número"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>


                            <!-- Ubicacion -->
                            <div class="row centered justify-content-center  ">
                                <div class=" col-lg-1 col-md-1 col-sm-12">
                                    <label>Ubicación</label>
                                </div>
                                <div class=" col col-lg-3 col-md-3 col-sm-12">
                                    <%--<asp:TextBox ID="txt_localidad" class="caja2" runat="server" placeholder="Ingrese localidad"></asp:TextBox>--%>
                                    <asp:DropDownList class="caja2" ID="ddl_ubicacion" runat="server" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>

                                <asp:CustomValidator ID="cstm_ubicacion" runat="server" ErrorMessage="Debe seleccionar una ubicacion" CssClass="text-danger" ValidationGroup="vgRegistro" ControlToValidate="ddl_ubicacion" Display="Dynamic" OnServerValidate="cstm_ubicacion_ServerValidate"> </asp:CustomValidator>
                            </div>
                            <div class="row centered">
                                <p>&nbsp;</p>
                            </div>

                            <!--Boton-->
                            <div class="row centered">
                                <div class="col">
                                    <asp:Button ID="btn_aceptar" runat="server" CssClass="btn btn-outline-dark" Text="Aceptar" ValidationGroup="vgRegistro" OnClick="btn_aceptar_Click" />
                                </div>
                            </div>

                            <div>
                                <p>&nbsp;</p>
                            </div>

                        </div>

                        <div>
                            <p>&nbsp;</p>
                        </div>

                        <div class="container p-1">
                            <div class="row centered">
                                <div class="col col-auto">
                                    <asp:HyperLink ID="lnk_volver" runat="server" Text="Volver" class="btn btn-link" href="Menu_Clase.aspx"></asp:HyperLink>
                                </div>
                            </div>
                        </div>

                        <div>
                            <p>&nbsp;</p>
                        </div>
                    </div>
                </div>
                <!-- /row -->
            </asp:Panel>
            <div>
                <p>&nbsp;</p>
            </div>
        </div>

    </form>

</asp:Content>
