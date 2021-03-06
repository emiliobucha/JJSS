﻿<%@ Page Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="VerInscriptos.aspx.cs" Inherits="JJSS.Presentacion.Clases.VerInscriptos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
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
<asp:Content ID="Content3" ContentPlaceHolderID="cphMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContenido" runat="server">

    <form runat="server">
        <div class="row centered">
            <p>&nbsp;</p>
        </div>

        <div class="container">
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
            <h1 class=" centered">Listado de Inscripciones</h1>
        </div>

        <div class="row centered justify-content-center " runat="server" id="div_nombre_clase">
            <div>
                <asp:Label ID="lbl_nombre_clase" CssClass="h3" runat="server" />
            </div>
        </div>
        <br/>




        <div runat="server" id="divFiltros">

            <div class="container">

                <div class="row p-2">

                    <div runat="server" id="div_label_clase" class="col-lg-1 col-md-1 col-sm-12" visible="False">
                        <strong>Clase:</strong>
                    </div>
                    <div runat="server" id="div_combo_clase" class="col-lg-2 col-md-2 col-sm-12 col-xl-2" visible="False">
                        <asp:DropDownList ID="ddl_clase" class="caja2" runat="server" placeholder="Clases"></asp:DropDownList>
                    </div>

                    <div class="col-md-2 col-xl-2">
                        <strong>Fecha desde:</strong>
                    </div>
                    <div class="col col-lg-2 col-md-2 col-sm-12 ">

                        <asp:TextBox ID="dp_fecha_desde" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" value="01/01/2000" required="true" placeholder="Seleccione fecha "></asp:TextBox>
                    </div>


                    <div class="col-md-2 col-xl-2">
                        <strong>Fecha hasta: </strong>
                    </div>
                    <div class="col col-lg-2 col-md-2 col-sm-12">
                        <asp:TextBox ID="dp_fecha_hasta" runat="server" class="datepicker caja2" pattern="^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20|21)\d{2}$" value="01/01/2000" required="true" placeholder="Seleccione fecha "></asp:TextBox>
                    </div>
                </div>

                <div class="row p-2">

                    <div class=" col-lg-1 col-md-1 col-sm-12">
                        <strong>Tipo:</strong>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12">
                        <asp:DropDownList ID="ddl_tipo" class="caja2" runat="server" placeholder="Ingrese Tipo" ValidationGroup="grupoDni"></asp:DropDownList>
                    </div>

                    <div class=" col-lg-1 col-md-1 col-sm-12">
                        <strong>N° Doc:</strong>
                    </div>

                    <div class=" col-lg-2 col-md-2 col-sm-12">
                        <asp:TextBox ID="txt_filtro_dni" type="number" CssClass="caja2" min="0" runat="server"></asp:TextBox>
                    </div>

                    <div class=" col-lg-1 col-md-1 col-sm-12">
                        <strong>Apellido:</strong>
                    </div>

                    <div class=" col-lg-2 col-md-2 col-sm-12">
                        <asp:TextBox ID="txt_filtro_apellido" CssClass="caja2" min="0" runat="server"></asp:TextBox>
                    </div>

                </div>
                <div class="row centered justify-content-center">
                    <!--Boton-->
                    <div class="col-md-1 col-xl-auto">
                        <asp:Button ID="btn_buscar" runat="server" formnovalidate="true" UseSubmitBehaviour="false" CausesValidation="false" Text="Buscar"
                            CssClass="btn btn-outline-dark" ValidationGroup="grupoDni" OnClick="btn_buscar_Click" />
                    </div>
                </div>
            </div>
        </div>


        <div>
            <p>&nbsp;</p>
        </div>






        <asp:Panel ID="pnl_listado" runat="server">
            <div class="container">
                <div class=" row centered justify-content-center" runat="server">
                    <div class="form-group border rounded p-4">
                        <asp:GridView ID="gv_inscripciones" runat="server" DataKeyNames="inscr_id" CssClass="table table-hover"
                            OnRowCommand="gv_inscripciones_RowCommand"
                            CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
                            EmptyDataText="No hay alumnos para mostrar" AllowPaging="True"
                            OnPageIndexChanging="gv_participantes_PageIndexChanging" PageSize="30" OnRowDataBound="gv_inscripciones_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="inscr_apellido" HeaderText="Apellido" />
                                <asp:BoundField DataField="inscr_nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="inscr_sexo" HeaderText="Sexo" />
                                <asp:BoundField DataField="inscr_tipo" HeaderText="Tipo" />
                                <asp:BoundField DataField="inscr_dni" HeaderText="Documento" />
                                <asp:BoundField DataField="inscr_faja" HeaderText="Faja" />
                                <asp:BoundField DataField="inscr_fecha_desde_mensual" HeaderText="Fecha desde" />
                                <asp:BoundField DataField="inscr_fecha_vto_mensual" HeaderText="Fecha vto" />
                                <asp:BoundField DataField="inscr_pago" HeaderText="Pagó periodo" />
                                <asp:BoundField DataField="inscr_recargo" HeaderText="Recargo" />
                                <asp:BoundField DataField="asistio" HeaderText="Asistió" />
                                <asp:TemplateField HeaderText="Permitir moroso">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="habilitar" CommandName="moroso" runat="server" CommandArgument='<%#Eval("inscr_id") %>' Text="Habilitar"> </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="aa" CommandName="modificar" runat="server" CommandArgument='<%#Eval("inscr_id") %>'> Mod. periodo</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                
                            </Columns>
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                        </asp:GridView>
                    </div>

                </div>
                <%-- <div class="row centered justify-content-center ">
                    <asp:Button ID="btn_imprimir" runat="server" Text="Imprimir" CssClass="btn btn-outline-dark" OnClick="btn_imprimir_Click" Visible="true" />
                </div>--%>
                <div class="row centered p-2">
                    <div class="row centered">
                        <div class="col col-auto">
                            <asp:HyperLink ID="lnk_volver" runat="server" Text="Volver" class="btn btn-link" href="Menu_Clase.aspx"></asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</asp:Content>
