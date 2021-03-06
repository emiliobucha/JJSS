﻿<%@ Page Language="C#" MasterPageFile="~/Presentacion/Site.Master"  AutoEventWireup="true" CodeBehind="PagosPanel.aspx.cs" Inherits="JJSS.Presentacion.Pagos.PagosPanel" %>

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
                        <h1>Pagos Pendientes</h1>
                    </div>

                    <div>
                        <p>&nbsp;</p>
                    </div>

                    <div runat="server" class="row centered justify-content-center" id="divDNI" visible="False">


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

                    <div runat="server" class="container" id="divReferencias" visible="false">
                    <div class="form-group border rounded p-4 ">
                        <div class="row centered justify-content-center"><strong>Referencias:</strong></div><br />
                        <div class="row col-md-12 centered justify-content-center">
                            <div class="row col-md-4 centered justify-content-center">
                                <div class="col-md-2"><div class="foo torneo"></div></div>
                                <div class="col-md-2"><a>Torneos</a></div>
                            </div>
                            <div class="row col-md-4 centered justify-content-center">
                                <div class="col-md-2"><div class="foo evento"></div></div>
                                <div class="col-md-2"><a>Eventos</a></div>
                            </div>
                            <div class="row col-md-4 centered justify-content-center">
                                <div class="col-md-2"><div class="foo clase"></div></div>
                                <div class="col-md-2"><a>Clase</a></div>
                            </div>
                        </div>
                    </div>
                    </div>


                    <div class="container">

                        <div class="form-group border rounded p-4 ">

                      
                            <div class="row centered justify-content-center">
                                <asp:GridView ID="gvPagos" runat="server" CssClass="table" CellPadding="4" DataKeyNames="Inscripcion" OnPageIndexChanging="gvPagos_PageIndexChanging"
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay pagos pendientes para mostrar"
                                    OnRowCommand="gvPagos_RowCommand" AllowPaging="True" PageSize="20" OnRowDataBound="gvPagos_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRow" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TipoPago.Tipo" HeaderText="Tipo" SortExpression="tipo" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="nombre" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="DescripcionObjeto"  HeaderText="Descripción" SortExpression="descripcion" />
                                        <asp:BoundField DataField="MontoString" HeaderText="Monto" SortExpression="monto" />
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btn_anular" runat="server" class="btn btn-link" CommandName="anular" Text="Anular pago" Visible="False" CommandArgument=<%# Eval("Inscripcion") %>/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                    <PagerSettings Position="TopAndBottom" />
                                </asp:GridView>     
                            </div>
                            <div class="row justify-content-center p-1">
                                <div class="col-xs-2">
                                    <label>Forma de pago:&nbsp;</label>
                                </div>
                                <div class="col-xs-3">
                                    <asp:DropDownList ID="ddl_forma_pago" runat="server" CssClass="caja2"></asp:DropDownList>
                                </div>
                                <div class="col-xs-3">
                                </div>
                            </div>
                            <div class="row centered justify-content-center p-1">
                                <div class="col-xl-auto">
                                    <asp:Button ID="btnRegistrarPago" formnovalidate="true" CausesValidation="false" runat="server" Text="Aceptar" CssClass="btn btn-outline-dark" OnClick="btnRegistrarPago_Click" />
                                </div>
                            </div>
                        </div>

                        <div>
                            <p>&nbsp;</p>
                        </div>
                        
                        <div class="row">
                            <asp:Button ID="btn_volver" formnovalidate="true" class="btn btn-link pull-left" runat="server" Text="Volver" OnClick="btn_volver_Click" />
                        </div>

                    </div>
                </div>
            </asp:Panel>
        </div>

    </form>
</asp:Content>
