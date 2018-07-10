<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="VerTorneo.aspx.cs" Inherits="JJSS.Presentacion.VerTorneo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
            <script type='text/javascript'>
        function openModal(id) {
            $('[id*=confirmacion]').modal('show');
            return false;
    }   
</script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContenido" runat="server">

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

        </asp:Panel>
    </div>

    <div class="row centered">
        <p>&nbsp;</p>
    </div>

    <div class="row centered justify-content-center">
        <p>&nbsp;</p>
        <h1>
            <asp:Label ID="lbl_nombre_torneo" CssClass="" runat="server" Text=""></asp:Label>
        </h1>
        <p>&nbsp;</p>
    </div>

    <div class="row centered">
        <p>&nbsp;</p>
    </div>

    <div class="container">
        <asp:Panel ID="pnl_InfoTorneo" CssClass="panel centered justify-content-center border rounded p-2" runat="server">

            <!--Fecha-->
            <div class="row centered">
                <p>&nbsp;</p>
            </div>

            <div class="row centered justify-content-center">
                <div class="col-md-2 hidden-sm hidden-xs"></div>
                <div class="col-md-6 col-sm-10 ">
                    <asp:label id="lbl1" runat="server" class="pull-left h6">Fecha:&nbsp;</asp:label>
                    <asp:Label ID="lbl_FechaDeTorneo" CssClass="pull-left" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Label2" CssClass="pull-left" runat="server" Text=", "></asp:Label>
                    <asp:Label ID="lbl_HoraTorneo" CssClass="pull-left" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Label6" CssClass="pull-left" runat="server" Text=" hs"></asp:Label>
                </div>
            </div>

            <!--Cierre Inscripciones-->

            <div class="row centered justify-content-center">
                <div class="col-md-2 hidden-sm hidden-xs"></div>
                <div class="col-md-6 col-sm-10 ">
                    <asp:label id="lbl2" runat="server" class="pull-left h6">Cierre de Inscripción:&nbsp;</asp:label>
                    <asp:Label ID="lbl_FechaCierreInscripcion" CssClass="pull-left" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Label7" CssClass="pull-left" runat="server" Text=", "></asp:Label>
                    <asp:Label ID="lbl_HoraCierreTorneo" CssClass="pull-left" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Label9" CssClass="pull-left" runat="server" Text=" hs"></asp:Label>
                </div>
            </div>

            <!--direccion de la sede-->

            <div class="row centered justify-content-center">
                <div class="col-md-2 hidden-sm hidden-xs"></div>
                <div class="col-md-6 col-sm-10 ">
                    <asp:label id="lbl3" runat="server" class="pull-left h6">Sede:&nbsp;</asp:label>
                    <asp:Label ID="lbl_sede" CssClass="pull-left" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row centered justify-content-center">
                <div class="col-md-2 hidden-sm hidden-xs"></div>
                <div class="col-md-6 col-sm-10 ">
                    <label class="pull-left h6">Dirección:&nbsp;</label>
                    <asp:Label ID="lbl_direccion_sede" CssClass="pull-left" runat="server" Text=""></asp:Label>
                </div>
            </div>


            <!--Precio-->

            <div class="row centered justify-content-center">
                <div class="col-md-2 hidden-sm hidden-xs"></div>
                <div class="col-md-6 col-sm-10">

                    <asp:label id="lbl4" runat="server" class="pull-left h6">Costo Categoría:&nbsp; </asp:label>
                    <asp:Label ID="Label4" CssClass="pull-left" runat="server" Text="$"></asp:Label>
                    <asp:Label ID="lbl_CostoInscripcion" CssClass="pull-left" runat="server" Text=""></asp:Label>


                </div>
            </div>
            <div class="row centered justify-content-center">
                <div class="col-md-2 hidden-sm hidden-xs"></div>
                <div class="col-md-6 col-sm-10">

                    <asp:label id="lbl5" runat="server" class="pull-left h6">Costo Absoluto:&nbsp;</asp:label>
                    <asp:Label ID="Label3" CssClass="pull-left" runat="server" Text="$ "></asp:Label>
                    <asp:Label ID="lbl_CostoInscripcionAbsoluto" CssClass="pull-left" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <div class="row centered">
                <p>&nbsp;</p>
            </div>

        </asp:Panel>
    </div>

    <div class="row centered">
        <p>&nbsp;</p>
    </div>

    <div class=" container">
        <form id="form" runat="server">
            <asp:Panel ID="pnl_resultados" runat="server">
                <asp:GridView ID="gvResultados" runat="server" CssClass="table" CellPadding="4" OnPageIndexChanging="gvResultados_PageIndexChanging" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay resultados de este torneo todavía" AllowPaging="True" PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="categoria" HeaderText="Categoria" />
                        <asp:BoundField DataField="faja" HeaderText="Faja" />
                        <asp:BoundField DataField="primero" HeaderText="Primer Puesto" />
                        <asp:BoundField DataField="segundo" HeaderText="Segundo Puesto" />
                        <asp:BoundField DataField="tercero1" HeaderText="Tercer Puesto 1" />
                        <asp:BoundField DataField="tercero2" HeaderText="Tercer Puesto 2" />
                    </Columns>
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                    <PagerSettings Position="TopAndBottom" />
                </asp:GridView>
            </asp:Panel>

            <asp:Panel ID="pnl_botones" CssClass="centered justify-content-center" runat="server">
                <p>&nbsp;</p>
                <asp:Button ID="btn_cargar_resultados" runat="server" CssClass="btn btn-outline-dark " Text="Cargar Resultados" OnClick="btn_cargar_resultados_Click" Visible="false" />
                <asp:Button ID="btn_editar_resultados" runat="server" CssClass="btn btn-outline-dark " Text="Editar Resultados" OnClick="btn_editar_resultados_Click" Visible="false" />
                <asp:Button ID="btn_inscribir" runat="server" CssClass="btn btn-outline-dark" Text="Inscribir" OnClick="btn_inscribir_Click" Visible="false" />
                <asp:Button ID="btn_cancelar" runat="server" CssClass="btn btn-outline-dark" Text="Cancelar Torneo" OnClientClick="return openModal()" Visible="false" />
                <asp:Button ID="btn_suspender" runat="server" CssClass="btn btn-outline-dark" Text="Suspender Torneo" OnClick="btn_suspender_Click" Visible="false" />
                <asp:Button ID="btn_editar" runat="server" CssClass="btn btn-outline-dark" Text="Editar Torneo" OnClick="btn_editar_Click" Visible="false" />
                <asp:Button ID="btn_habilitar" runat="server" CssClass="btn btn-outline-dark" Text="Habilitar Torneo" OnClick="btn_habilitar_Click" Visible="false" />
                <asp:Button ID="btn_imprimir_listado" runat="server" CssClass="btn btn-outline-dark" Text="Imprimir Listado de Participantes" OnClick="btn_imprimir_listado_Click" Visible="false" />
                <asp:Button ID="btn_imprimir_resultados" runat="server" CssClass="btn btn-outline-dark" Text="Imprimir Listado de Resultados" OnClick="btn_imprimir_resultados_Click" Visible="false" />
            
            </asp:Panel>


            <div class="row centered">
                <p>&nbsp;</p>
            </div>

            <asp:Button ID="btn_volver" runat="server" Text="Volver" CssClass=" btn btn-link pull-left" OnClick="btn_volver_Click1"/>

                    
        <div class="modal fade col-lg-12 col-md-12 col-xs-8 col-sm-8" id="confirmacion" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabe2">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <!--Cabecera-->
                    <div class="modal-header">
                        <h4 class="modal-title" id="exampleModalLabe2">¿Seguro que desea eliminar el torneo?</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>

                    <!--Botonero-->
                    <div class="modal-footer">
                        <asp:button ID="btn_si" type="button" runat="server" class="btn btn-outline-dark" OnClick="btn_si_Click1"  TExt="SI"/>
                        <Button ID="btn_no" type="button" class="btn btn-default"  value="No" data-dismiss="modal">No</button>

                    </div>

                </div>
            </div>
        </div>
        <asp:TextBox ID ="txtIDSeleccionado" runat="server" Text="" hidden="true"></asp:TextBox>


        </form>

        <div class="row centered">
            <p>&nbsp;</p>
        </div>

    </div>




</asp:Content>
