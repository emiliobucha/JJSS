<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="MenuInicial.aspx.cs" Inherits="JJSS.Presentacion.MenuInicial" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">


    <div style="background-image: url(../img/d.png); background-position:right;
       background-repeat:no-repeat; background-attachment:scroll" >
       
        <div class="row centered">
            <p>&nbsp;</p>
        </div>


        <div class="row centered justify-content-center">

            <h1 class=" centered ">Academia Hinojal</h1>

        </div>

        <div class="row centered">
            <p>&nbsp;</p>
        </div>

        <div class=" container">
            <div class="row mt centered justify-content-center">

                <!--Torneo-->
                <div class="col-sm-12 col-md-6 col-lg-3  ">
                    <a class="text-dark" href="Torneos/MenuTorneo.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../img/torneo.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Torneos</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Clase-->
                <div class="col-sm-12 col-md-6 col-lg-3  ">
                    <a class="text-dark" href="Clases/Menu_Clase.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../img/Clase.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Clases</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Eventos-->
                <div class="col-sm-12 col-md-6 col-lg-3  ">
                    <a class="text-dark" href="Eventos/Menu_Evento.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../img/Evento.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Eventos</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Perfil-->
                <div class="col-sm-12 col-md-6 col-lg-3  ">
                    <a class="text-dark" href="/Presentacion/Perfil.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../img/Perfil.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Mi Perfil</h4>
                            </div>
                        </div>
                    </a>
                </div>

            </div>
            <div class="row mt centered justify-content-center">
                <!--BI-->
                <div class="col-sm-12 col-md-6 col-lg-3">
                    <a class="text-dark" href="">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../img/BI.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">BI</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Pagos-->
                <div class="col-sm-12 col-md-6 col-lg-3 ">
                    <a class="text-dark" href="">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../img/Pago.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Pagos</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Administracion-->
                <div class="col-sm-12 col-md-6 col-lg-3 ">
                    <a class="text-dark" href="Administracion/Menu_Administracion.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../img/Administracion.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Administracion</h4>
                            </div>
                        </div>
                    </a>
                </div>

                <!--Info-->
                <div class="col-sm-12 col-md-6 col-lg-3">
                    <a class="text-dark" href="SobreNosotros.aspx">
                        <div class="btn " style="width: 10rem;">
                            <img class=" img-fluid" src="../img/Info.png" alt="Card image cap">
                            <div class="">
                                <h4 class="mb-5">Info</h4>
                            </div>
                        </div>
                    </a>
                </div>




            </div>
        </div>


        <div class="row centered">
            <p>&nbsp;</p>
        </div>

    </div>
</asp:Content>
