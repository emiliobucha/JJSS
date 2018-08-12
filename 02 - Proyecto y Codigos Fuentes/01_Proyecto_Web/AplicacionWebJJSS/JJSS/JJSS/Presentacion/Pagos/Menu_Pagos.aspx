<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Menu_Pagos.aspx.cs" Inherits="JJSS.Presentacion.Pagos.Menu_Pagos" %>
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
            <h1 class=" centered">Pagos</h1>
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
              

                  <!--Pagos de Alumnos-->
                <div class="col-sm-6 col-md-6 col-lg-3">
                    <a class="text-dark" href="../Pagos/PagosAlumno.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/PagoRealizados2.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Pagos de Alumnos</h4>
                            </div>
                        </div>
                    </a>
                </div>

                  <!--Pagos Pendientes-->
                <div class="col-sm-6 col-md-6 col-lg-3">
                    <a class="text-dark" href="../Pagos/PagosPanel.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/PagoPendiente2.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Pagos Pendientes</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Pagos por Período-->
                <div class="col-sm-6 col-md-6 col-lg-3">
                    <a class="text-dark" href="../Pagos/PagosMes.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/PagoPorPeriodo2.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Pagos por Período</h4>
                            </div>
                        </div>
                    </a>
                </div>

              

                <!--Mis Pagos-->
                <div class="col-sm-6 col-md-6 col-lg-3">
                    <a class="text-dark" href="../Pagos/MisPagos.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../../img/MisPagos2.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Mis Pagos</h4>
                            </div>
                        </div>
                    </a>
                </div>

            </div>

        </div>
    </form>
</asp:Content>
