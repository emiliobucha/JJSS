<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeFile="CrearTorneo.aspx.cs" CodeBehind="CrearTorneo.aspx.cs" Inherits="JJSS.Presentacion.CrearTorneo" %>

<asp:Content ID="crearTorneoMenu" ContentPlaceHolderID="cphMenu" runat="server">
    <a href="Inicio.aspx" class="smoothScroll">Home</a>			
    <a href="#crearTorneo" class="smoothScroll">Crear</a>
</asp:Content>

<asp:Content ID="crearTorneoEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
	<script type="text/javascript" src="../js/funciones.js"> </script>
    <div id="headerwrap">
		<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3">
					<h1>Generar Torneo</h1>
				</div>
			</div><! --/row -->
		</div><! --/container -->
	</div><! --/headerwrap -->
</asp:Content>



<asp:Content ID="crearTorneoContenido" ContentPlaceHolderID="cphContenido" runat="server">
   
    <section id="crearTorneo" title="crearTorneo"></section>
	<div id="crearTorneowrap">
		<div class="container">
			<div class="row mt centered">     
                
				<h1>FORMULARIO DE ALTA</h1>
                <p>&nbsp;</p>

        
				<div class="col-lg-6">
					<form role="form">
					  <div class="form-group ">
                          <label>Fecha a producirse</label>
                          <input class="form-control"  id="lblFecha" placeholder="Enter name">

                          <p>Date: <input type="text" id="datepicker"></p>


                          <label>Precio</label>
                          <input class="form-control" id="lblPrecio" placeholder="Enter precio">

					      <label for="exampleInputName1">Your Name</label>					    
                          <input type="email" class="form-control" id="exampleInputEmail12" placeholder="Enter name">
					    <label for="exampleInputEmail1">Email address</label>
					    <input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">
					    <label for="exampleInputText1">Message</label>
					    <textarea class="form-control" rows="3"></textarea>
					  </div>
					  <button class="btn btn-default" onclick="CrearNuevoTorneo">Submit</button>
                        
					</form>

		
			</div><! --/row -->
		
			</div><!-- /row -->
		</div><!-- /container -->
	</div><!-- /aboutwrap -->


        
</asp:Content>
