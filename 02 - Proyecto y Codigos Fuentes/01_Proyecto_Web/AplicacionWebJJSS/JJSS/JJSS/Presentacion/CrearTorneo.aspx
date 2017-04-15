<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="CrearTorneo.aspx.cs" Inherits="JJSS.Presentacion.CrearTorneo" %>

<asp:Content ID="crearTorneoMenu" ContentPlaceHolderID="cphMenu" runat="server">
    <a href="Inicio.aspx" class="smoothScroll">Home</a>			
    <a href="#crearTorneo" class="smoothScroll">Crear</a>
</asp:Content>

<asp:Content ID="crearTorneoEncabezado" ContentPlaceHolderID="cphEncabezado" runat="server">
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


<asp:Content ID="crearTorneoContenido"  ContentPlaceHolderID="cphContenido" runat="server">

    <section id="crearTorneo" title="crearTorneo"></section>
    <asp:Panel ID="pnlFormulario" runat="server">
    <div id="crearTorneowrap">
        <div class="container">
            <div class="row mt centered">

                <h1>FORMULARIO DE ALTA</h1>
                <p>&nbsp;</p>                            

                <div class="col-lg-5 mt centered">
                    <form role="form">
                        <div class="form-group ">
                            <p><label class="pull-left">Nombre</label></p>
                            <p><input class="form-control" id="lblNombre" placeholder="Ingrese nombre"></p>

                            <p><label class="pull-left">Fecha a producirse</label></p>
                            <p><input type="text" placeholder="Seleccione fecha del torneo" class="form-control" id="datepicker1"></p>
                            
                            <p><label class="pull-left">Fecha de cierre de inscripciones</label></p>
                            <p><input type="text" placeholder="Seleccione fecha de cierre de inscripciones" class="form-control" id="datepicker2"></p>

                            <p><label class="pull-left">Precio de inscripcion categoria</label></p>
                            <p><input class="form-control" id="lblPrecioCategoria" placeholder="Ingrese precio de inscripcion categoria"></p>

                            <p><label class="pull-left">Precio de inscripcion absoluta</label></p>
                            <p><input class="form-control" id="lblPrecioAbsoluto" placeholder="Ingrese precio de inscripcion absoluta"></p>

                            <p><label class="pull-left">Comentarios</label></p>
                            <textarea class="form-control"  rows="3"></textarea>
                        </div>
                        <button type="submit" class="btn btn-default">Submit</button>
                    </form>
                </div>
                <! --/row -->		
            </div>
            <!-- /row -->
        </div>
        <!-- /container -->
    </div>
    <!-- /aboutwrap -->
    </asp:Panel>


</asp:Content>
