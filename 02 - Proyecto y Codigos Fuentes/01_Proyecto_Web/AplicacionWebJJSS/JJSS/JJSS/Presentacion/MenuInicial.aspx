<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="MenuInicial.aspx.cs" Inherits="JJSS.Presentacion.MenuInicial" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">


    <div class="row mt centered">

        <!--Torneo-->
        <div class="col-sm-12 col-md-6 col-lg-2  ">
            <a class="text-dark" href="MenuTorneo.aspx">
                <div class="btn " style="width: 10rem;">
                    <img class=" img-fluid" src="../img/torneo.png" alt="Card image cap">
                    <div class="">
                        <h4 class="mb-5">Torneos</h4>
                    </div>
                </div>
            </a>
        </div>

        <!--Clase-->
        <div class="col-sm-12 col-md-6 col-lg-2  ">
            <a class="text-dark" href="MenuTorneo.aspx">
                <div class="btn " style="width: 10rem;">
                    <img class=" img-fluid" src="../img/Clase.png"  alt="Card image cap">
                    <div class="">
                        <h4 class="mb-5">Clases</h4>
                    </div>
                </div>
            </a>
        </div>

         <!--Eventos-->
        <div class="col-sm-12 col-md-6 col-lg-2  ">
            <a class="text-dark" href="MenuTorneo.aspx">
                <div class="btn " style="width: 10rem;">
                    <img class=" img-fluid" src="../img/Evento.png"  alt="Card image cap">
                    <div class="">
                        <h4 class="mb-5">Eventos</h4>
                    </div>
                </div>
            </a>
        </div>

          <!--Permisos-->
        <div class="col-sm-12 col-md-6 col-lg-2  ">
            <a class="text-dark" href="MenuTorneo.aspx">
                <div class="btn " style="width: 10rem;">
                    <img class=" img-fluid" src="../img/Permisos.png"  alt="Card image cap">
                    <div class="">
                        <h4 class="mb-5">Perfiles y Permisos</h4>
                    </div>
                </div>
            </a>
        </div>

    </div>


</asp:Content>
