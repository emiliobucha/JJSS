<%@ Page Language="C#"  MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="PagosAlumno.aspx.cs" Inherits="JJSS.Presentacion.Pagos.PagosAlumno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">
    <form id="formRegAlumno" runat="server">

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
            <div>
                <p>&nbsp;</p>
            </div>
        </asp:Panel>

        <div id="grillawrap">

            <asp:Panel ID="pnl_mostrar_alumnos" runat="server">

                <div id="mostrarAlumnowrap">

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>

                    <div class="row centered justify-content-center">
                        <h1>Pagos de Alumno</h1>
                    </div>

                    <div>
                        <p>&nbsp;</p>
                    </div>

                    <div runat="server" class="row centered justify-content-center" id="divDNI" Visible="False">


                        <!--Ingresar Tipo-->
                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left">Tipo Doc: <a class="text-danger">*</a></label>
                        </div>
                        <div class="col-md-3 col-xl-auto">
                            <asp:DropDownList ID="ddl_tipo" class="caja2" runat="server" placeholder="Ingrese Tipo" ValidationGroup="grupoDni"></asp:DropDownList>

                        </div>


                        <!--Ingresar Numero-->
                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left">Documento: <a class="text-danger">*</a></label>
                        </div>
                        <div class="col-md-3 col-xl-auto">

                            <asp:TextBox ID="txtDni" class="caja2" required="true"  runat="server" placeholder="Ingrese Número" ValidationGroup="grupoDni"></asp:TextBox>

                        </div>
                        
                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left">Nombre Completo:</label>
                        </div>
                        <div class="col-md-3 col-xl-auto">
                            <label class="pull-left" runat="server" id="lblNombreBuscado"> - </label>
                        </div>

                        <!--Boton-->
                        <div class="col-md-1 col-xl-auto">

                            <asp:Button ID="btnBuscar" runat="server" formnovalidate="true" UseSubmitBehaviour="false" CausesValidation="false" Text="Buscar" CssClass="btn btn-outline-dark" ValidationGroup="grupoDni" OnClick="btnBuscar_Click"  />

                        </div>


                    </div>
               
                    
                    <div runat="server" class="row centered justify-content-center" id="divDNIAlumno" Visible="False">


                        <!--Ingresar Tipo-->
                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left">Tipo Doc: </label>
                        </div>
                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left" runat="server" id="lblTipoDoc"></label>
                        </div>

                        <!--Ingresar Numero-->
                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left">Documento:</label>
                        </div>
                        <div class="col-md-3 col-xl-auto">
                            <label class="pull-left" runat="server" id="lblDni"></label>
                        </div>
                        
                        <div class="col-md-2 col-xl-auto">
                            <label class="pull-left">Nombre Completo:</label>
                        </div>
                        <div class="col-md-3 col-xl-auto">
                            <label class="pull-left" runat="server" id="lblNombre"></label>
                        </div>

                      
                    </div>

                    <div>
                        <p>&nbsp;</p>
                    </div>

                    <div class="container">

                        <div class="form-group border rounded p-4 ">

                      
                            <div class="row centered justify-content-center">
                                <asp:GridView ID="gvPagos" runat="server" CssClass="table" CellPadding="4" DataKeyNames="Inscripcion" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay pagos para mostrar" OnRowDataBound="gvPagos_RowDataBound">
                                    <Columns>
                                        
                                        <asp:BoundField DataField="TipoPago.Tipo" HeaderText="Tipo" SortExpression="tipo" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="nombre" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="DescripcionObjeto"  HeaderText="Descripción" SortExpression="descripcion" />
                                        <asp:BoundField DataField="MontoString" HeaderText="Monto" SortExpression="monto" />
                                        
                                        <asp:BoundField DataField="PagoString" HeaderText="Pago" SortExpression="pago" />
                                        <asp:BoundField DataField="FechaPago" HeaderText="Fecha Pago" SortExpression="fechaPago" DataFormatString="{0:dd/MM/yyyy}"/>
                                    </Columns>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                    <PagerSettings Position="TopAndBottom" />
                                </asp:GridView>     
                            </div>
                          </div>

                        <div>
                            <p>&nbsp;</p>
                        </div>
                        
                        <div class="row">
                            <asp:HyperLink runat="server" Text="Volver" CssClass="btn btn-link" href="MenuInicial.aspx"></asp:HyperLink>

                        </div>

                    </div>
                </div>
            </asp:Panel>
        </div>

    </form>
</asp:Content>