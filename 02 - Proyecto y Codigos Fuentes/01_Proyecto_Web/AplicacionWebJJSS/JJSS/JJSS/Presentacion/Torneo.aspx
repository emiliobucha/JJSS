<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="Torneo.aspx.cs" Inherits="JJSS.Presentacion.Torneo" %>


<asp:Content ID="TorneoMenu"  ContentPlaceHolderID="cphMenu" runat="server">
        
    <a href="Inicio.aspx" class="smoothScroll">Home</a>
    <a href="#crearTorneo" class="smoothScroll">Crear</a>
    <a href="#buscarTorneo" class="smoothScroll">Buscar</a>       
    <a href="#buscarTorneo" class="smoothScroll">Inscribir</a>    
</asp:Content>

<asp:Content ID="TorneoEncabezado"  ContentPlaceHolderID="cphEncabezado" runat="server">
    
    <section id="home" title="home"></section>
	<div id="headerwrap">
		<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3">
					<h1>Torneos</h1>
				</div>
			</div><! --/row -->
		</div><! --/container -->
	</div><! --/headerwrap -->
</asp:Content>

<asp:Content ID="TorneoContenido" ContentPlaceHolderID="cphContenido" runat="Server">   

    <asp:Panel ID="pnl_InicioTorneo" runat="server">
    
        <section id="crearTorneo" title="crearTorneo"></section>
        <div class="container">
			<div class="row mt centered">
				<h1>CREAR TORNEO</h1>
            </div><! --/row -->
		</div><! --/container -->

    </asp:Panel>

    <asp:Panel ID="pnl_BuscarTorneo" runat="server">
          
        <section id="buscarTorneo" title="buscarTorneo"></section>
        <div class="container">
			<div class="row mt centered">
				<h1>BUSCAR TORNEO</h1>
            </div>				
	    </div><! --/row -->

    </asp:Panel>

    <asp:Panel ID="pnl_Inscripcion" runat="server">
          
        <section id="inscribirTorneo" title="inscribirTorneo"></section>
        <div class="container">
			<div class="row mt centered">
				<h1>INSCRIPCION TORNEO</h1>
            </div>				
	    </div><! --/row -->

    </asp:Panel>

</asp:Content>
