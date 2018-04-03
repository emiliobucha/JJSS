<%@ Page Title="" Language="C#" MasterPageFile="~/Presentacion/Site.Master" AutoEventWireup="true" CodeBehind="VerTorneo.aspx.cs" Inherits="JJSS.Presentacion.VerTorneo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEncabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphP" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContenido" runat="server">
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

    <div class="container ">
        <div class="row centered">
            <div class=" col-md-4"></div>
            <div class="col-md-4">
                <p>&nbsp;</p>
                <h1>
                    <asp:Label ID="lbl_nombre_torneo" CssClass="pull-left" runat="server" Text=""></asp:Label></h1>
            </div>
        </div>

        <asp:Panel ID="pnl_InfoTorneo" CssClass="panel panel-default" runat="server">

            <!--Fecha-->
            <div class="row centered">
                <p>&nbsp;</p>
            </div>

            <div class="row centered">
                <div class="col-md-2 hidden-sm hidden-xs"></div>
                <div class="col-md-4 ">
                    <strong class="pull-left">Fecha:&nbsp; </strong>
                    <asp:Label ID="lbl_FechaDeTorneo" CssClass="pull-left" runat="server" Text=""></asp:Label>
                    <a class="pull-left">, </a>
                    <asp:Label ID="lbl_HoraTorneo" CssClass="pull-left" runat="server" Text=""></asp:Label>
                    <a class="pull-left">&nbsp;hs </a>
                </div>
            </div>

            <!--Cierre Inscripciones-->

            <div class="row centered">
                <div class="col-md-2 hidden-sm hidden-xs"></div>
                <div class="col-md-6">
                    <strong class="pull-left">Cierre de Inscripción:&nbsp;</strong>
                    <asp:Label ID="lbl_FechaCierreInscripcion" CssClass="pull-left" runat="server" Text=""></asp:Label>
                    <a class="pull-left">,&nbsp; </a>
                    <asp:Label ID="lbl_HoraCierreTorneo" CssClass="pull-left" runat="server" Text=""></asp:Label>
                    <a class="pull-left">&nbsp;hs </a>
                </div>
            </div>
            <!--direccion de la sede-->
            <div class="row centered">
                <p>&nbsp;</p>
            </div>

            <div class="row centered">
                <div class="col-md-2 hidden-sm hidden-xs"></div>
                <div class="col-md-6">
                    <strong class="pull-left">Sede:&nbsp;</strong>
                    <asp:Label ID="lbl_sede" CssClass="pull-left" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="row centered">
                <div class="col-md-2 hidden-sm hidden-xs"></div>
                <div class="col-md-6">
                    <strong class="pull-left">Dirección:&nbsp;</strong>
                    <asp:Label ID="lbl_direccion_sede" CssClass="pull-left" runat="server" Text=""></asp:Label>
                </div>
            </div>


            <!--Precio-->
            <div class="row centered">
                <p>&nbsp;</p>
            </div>

            <div class="row centered">
                <div class="col-md-2 hidden-sm hidden-xs"></div>
                <div class="col-md-5 col-lg-10 col-sm-10">

                    <strong class="pull-left">Costo Categoría:&nbsp; </strong>
                    <asp:Label ID="Label4" CssClass="pull-left" runat="server" Text="$"></asp:Label>
                    <asp:Label ID="lbl_CostoInscripcion" CssClass="pull-left" runat="server" Text=""></asp:Label>

                    <div class="row centered">
                        <p>&nbsp;</p>
                    </div>
                </div>
            </div>
            <div class="row centered">
                <div class="col-md-2 hidden-sm hidden-xs"></div>
                <div class="col-md-5 col-lg-10 col-sm-10">

                    <strong class="pull-left">Costo Absoluto:&nbsp;</strong>
                    <asp:Label ID="Label3" CssClass="pull-left" runat="server" Text="$ "></asp:Label>
                    <asp:Label ID="lbl_CostoInscripcionAbsoluto" CssClass="pull-left" runat="server" Text=""></asp:Label>
                </div>
            </div>


            <div class="row centered">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                </div>
            </div>

            <div class="row centered">
                <p>&nbsp;</p>
            </div>

        </asp:Panel>

        <form id="form" runat="server">
            <h2 class="centered">Resultados</h2>
            &nbsp;
            <asp:Panel ID="pnl_resultados" runat="server">
                <asp:GridView ID="gvResultados" runat="server" CssClass="table" CellPadding="4" OnPageIndexChanging="gvResultados_PageIndexChanging" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No hay resultados de este torneo todavía" AllowPaging="True" PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="categoria" HeaderText="Categoría" />
                        <asp:BoundField DataField="faja" HeaderText="Faja" />
                        <asp:BoundField DataField="primero" HeaderText="Primer Puesto" />
                        <asp:BoundField DataField="segundo" HeaderText="Segundo Puesto" />
                        <asp:BoundField DataField="tercero1" HeaderText="Tercer Puesto 1" />
                        <asp:BoundField DataField="tercero2" HeaderText="Tercer Puesto 2" />
                    </Columns>
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                    <PagerSettings Mode="NextPrevious" Position="TopAndBottom" />
                </asp:GridView>
            </asp:Panel>

            <asp:Panel ID="pnl_botones" runat="server">
                <p>&nbsp;</p>
                <asp:Button ID="btn_cargar_resultados" runat="server" CssClass="btn btn-default pull-right" Text="Cargar Resultados" OnClick="btn_cargar_resultados_Click" Visible="false" />
                <asp:Button ID="btn_editar_resultados" runat="server" CssClass="btn btn-default pull-right" Text="Editar Resultados" OnClick="btn_editar_resultados_Click" Visible="false" />
                <asp:Button ID="btn_inscribir" runat="server" CssClass="btn btn-default pull-right" Text="Inscribir" OnClick="btn_inscribir_Click" Visible="false" />
                <asp:Button ID="btn_cancelar" runat="server" CssClass="btn btn-default pull-right" Text="Cancelar Torneo" OnClick="btn_cancelar_Click" Visible="false" />
                <asp:Button ID="btn_suspender" runat="server" CssClass="btn btn-default pull-right" Text="Suspender Torneo" OnClick="btn_suspender_Click" Visible="false" />
                <asp:Button ID="btn_editar" runat="server" CssClass="btn btn-default pull-right" Text="Editar Torneo" OnClick="btn_editar_Click" Visible="false" />
                <asp:Button ID="btn_habilitar" runat="server" CssClass="btn btn-default pull-right" Text="Habilitar Torneo" OnClick="btn_habilitar_Click" Visible="false" />
                <asp:Button ID="btn_imprimir_listado" runat="server" CssClass="btn btn-default pull-right" Text="Imprimir Listado" OnClick="btn_imprimir_listado_Click" Visible="false" />

            </asp:Panel>
            <asp:Button ID="btn_volver" runat="server" Text="Volver" CssClass="btn-link pull-left" OnClick="btn_volver_Click" />
        </form>
    </div>
</asp:Content>
