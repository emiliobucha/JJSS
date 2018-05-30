﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Menu_Administracion.aspx.cs" Inherits="JJSS.Presentacion.Menu_Administracion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">

    <form id="form1" runat="server">



        <div class="row centered">
            <p>&nbsp;</p>
        </div>


        <div class="container ">
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

        <div class="row centered justify-content-center ">
            <h1 class=" centered">Administración</h1>
        </div>

        <div class="row centered">
            <p>&nbsp;</p>
        </div>


        <!---------------------------------**************Menu**********---------------------------------------->


        <div class="container ">

            <div class="row centered">
                <p>&nbsp;</p>
            </div>

            <div class="row  centered justify-content-center p-2">

                <div class="row centered">
                    <p>&nbsp;</p>
                </div>

                <!--Crear Sede-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto ">
                    <a class="text-dark" href="../Administracion/CrearSede.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Crear2.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Crear Sede</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Crear Categoria-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto ">
                    <a class="text-dark" href="../Administracion/CrearCategoria.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Crear2.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Crear Categoria</h4>
                            </div>
                        </div>
                    </a>
                </div>
                


                <!--Histórico-->
                <%-- <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto ">
                    <a class="text-dark" href="HistoricoTorneos.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Historial.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Histórico</h4>
                            </div>
                        </div>
                    </a>
                </div>--%>

                <%--<!--Ver-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto ">
                    <a class="text-dark" href="VerTorneo.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Ver.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Ver</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Pago-->
                <div class="col-sm-12 col-md-6 col-lg-2 col-xl-auto ">
                    <a class="text-dark" href="PagoClase.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/Pago.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Pagar</h4>
                            </div>
                        </div>
                    </a>
                </div>--%>
            </div>

        </div>
    </form>
</asp:Content>