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
                    <form id="form1" runat="server">
                        <div class="form-group ">
                            <p><label class="pull-left">Nombre</label></p>
                            <p><asp:TextBox ID="txt_nombre" runat="server" placeholder="Ingrese nombre" CssClass="form-control"></asp:TextBox></p>
                            <asp:RequiredFieldValidator ID="rfv_nombre" runat="server"   text="*" ErrorMessage="Se requiere que ingrese un nombre" ControlToValidate="txt_nombre"></asp:RequiredFieldValidator>
                            
                            <p><label class="pull-left">Fecha a producirse</label></p>
                            <p><asp:TextBox ID="dp_fecha" placeholder="Seleccione fecha del torneo" CssClass="datepicker" runat="server"></asp:TextBox></p>
                            <asp:RequiredFieldValidator ID="rfv_fecha" runat="server"  text="*" ErrorMessage="Se requiere que ingrese fecha del torneo" ControlToValidate="dp_fecha"></asp:RequiredFieldValidator>
                            
                            <p><label class="pull-left">Fecha de cierre de inscripciones</label></p>
                            <p><asp:TextBox ID="dp_fecha_cierre" placeholder="Seleccione fecha de cierre de inscripciones" CssClass="datepicker" runat="server"></asp:TextBox></p>
                            <asp:RequiredFieldValidator ID="rfv_fecha_cierre" runat="server" ErrorMessage="Se requiere que ingrese fecha de cierre de inscripciones" ControlToValidate="dp_fecha_cierre"  text="*"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="Compare_fechas" runat="server" ErrorMessage="La fecha de cierre no puede ser posterior a la del torneo" ControlToValidate="dp_fecha" ControlToCompare="dp_fecha_cierre" Operator="LessThanEqual"  text="*"></asp:CompareValidator>

                            <p><label class="pull-left">Precio de inscripcion categoria</label></p>
                            <p><asp:TextBox ID="txt_precio_cat" CssClass="form-control" placeholder="Ingrese precio de inscripcion categoria" runat="server"></asp:TextBox></p>

                            <p><label class="pull-left">Precio de inscripcion absoluta</label></p>
                            <p><asp:TextBox ID="txt_precio_abs" CssClass="form-control" placeholder="Ingrese precio de inscripcion absoluta" runat="server"></asp:TextBox></p>

                            <p><label class="pull-left">Hora de inicio del torneo</label></p>
                            <p><asp:DropDownList ID="ddl_hora" CssClass="form-control" runat="server">
                                <asp:ListItem>10:00</asp:ListItem>
                                <asp:ListItem>11:00</asp:ListItem>
                                <asp:ListItem Selected="True">12:00</asp:ListItem>
                                <asp:ListItem>13:00</asp:ListItem>
                                <asp:ListItem>14:00</asp:ListItem>
                                <asp:ListItem>15:00</asp:ListItem>
                                <asp:ListItem>16:00</asp:ListItem>
                                <asp:ListItem>17:00</asp:ListItem>
                                <asp:ListItem>18:00</asp:ListItem>
                                <asp:ListItem>19:00</asp:ListItem>
                                </asp:DropDownList></p>

                            <p><label class="pull-left">Hora de cierre de inscripciones</label></p>
                            <p><asp:DropDownList ID="ddl_hora_cierre" CssClass="form-control" runat="server">
                                <asp:ListItem>10:00</asp:ListItem>
                                <asp:ListItem>11:00</asp:ListItem>
                                <asp:ListItem Selected="True">12:00</asp:ListItem>
                                <asp:ListItem>13:00</asp:ListItem>
                                <asp:ListItem>14:00</asp:ListItem>
                                <asp:ListItem>15:00</asp:ListItem>
                                <asp:ListItem>16:00</asp:ListItem>
                                <asp:ListItem>17:00</asp:ListItem>
                                <asp:ListItem>18:00</asp:ListItem>
                                <asp:ListItem>19:00</asp:ListItem>
                                </asp:DropDownList></p>

                            <p><label class="pull-left">Comentarios</label></p>
                            <textarea class="form-control"  rows="3"></textarea>
                        </div>
                        
                          <p> <asp:Button ID="btn_aceptar" class="btn btn-default" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click1" OnClientClick="btn_aceptar_Click1" CausesValidation="true"/></p>


                    </form>
                </div>
                <!--/row -->		
            </div>
            <!-- /row -->
        </div>
        <!-- /container -->
    </div>
    <!-- /aboutwrap -->
    </asp:Panel>


</asp:Content>

<asp:Content  ID="cphP" ContentPlaceHolderID="cphP"  runat="server">
<script type="text/javascript">
        $(document).ready(
            function () {
                $(".datepicker").datepicker({
                    dateFormat: "dd/mm/yy",
                    monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                    dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"]
                });
            }
            );
    </script>
</asp:Content>
