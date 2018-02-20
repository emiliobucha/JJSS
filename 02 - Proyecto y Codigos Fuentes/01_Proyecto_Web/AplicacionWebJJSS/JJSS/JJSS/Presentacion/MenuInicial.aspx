<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="MenuInicial.aspx.cs" Inherits="JJSS.Presentacion.MenuInicial" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphEncabezado" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphP" runat="server">

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">


    <div class="row mt centered">


        <div class="col-sm-12 col-md-6 col-lg-2  ">
            <a href="MenuTorneo.aspx">
                <div class="btn " style="width: 10rem;" >
                    <img class=" img-fluid" src="../img/torneo.png" alt="Card image cap">
                    <div class="">
                        <h4 class="mb-5">Torneo</h4>
                    </div>
                </div>
            </a>
        </div>


        <div class="btn col-sm-12 col-md-6 col-lg-1 rounded centered modal-dialog-centered m-4 " style="background-color: #2f2f2f;">
            <a href="MenuTorneo.aspx">
                <div class=" btn modal-body centered text-center  justify-content-center ">
                    <p>&nbsp;</p>
                    <h2 class=" text-center  text-white ">Torneo</h2>
                    <p>&nbsp;</p>

                </div>
            </a>
        </div>

        <div class="btn col-sm-12 col-md-6 col-lg-2 rounded centered modal-dialog-centered m-4 bg-secondary " >
            <div class="  modal-body centered text-center  justify-content-center " >
                  <p>&nbsp;</p>
                    <h2 class=" text-center style="background-color: #2f2f2f;"">Torneo</h2>
                <p>&nbsp;</p>
            </div>
        </div>
        
        
        <div class="col-lg-4 proc" id="Div1" runat="server">
            <i class="fa fa-pencil"></i>
            <h3><a href="../Presentacion/InscripcionTorneo.aspx" style="color: #000000">Torneo </a></h3>
        </div>


    </div>


</asp:Content>
