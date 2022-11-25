<%@ Page Title="" Language="C#" MasterPageFile="~/NUO.Master" AutoEventWireup="true" CodeBehind="frm_pronostico.aspx.cs" Inherits="GERENTES.frm_pronostico" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <style>

    .background
    {
        position: fixed;
        top:0px;
        bottom:0px;
        left:0px;
        right:0px;
        overflow: hidden;
        padding: 0;
        margin: 0;
        background-color: #F0F0F0;
        filter: alpha(opacity=80);
        opacity: 0.8;
        z-index: 100000;
        }
    .progress
    {
        position: fixed;
        top: 40%;
        left: 40%;
        height:20%;
        width:20%;
        z-index: 100001;
        background-color: #FFFFFF;
        border: 1px solid Gray;
        background-image: url('images/iconos/cargando.gif');
        background-repeat: no-repeat;
        background-position: center;
        
        }
        
        </style>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
        <div class="background">
        
        </div>
        <div class="progress">
        
            <br />
        </div>
        </ProgressTemplate>
        </asp:UpdateProgress>
    <div id="main">
						<div class="inner">
                            
   <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
        <ContentTemplate>
            <div class="row"   runat="server"  id="alerta">
    <asp:Panel ID="pn_alerta" Width="100%" runat="server">
      
    </asp:Panel>     
                </div>
							<!-- Header -->
								<header id="header">
									<a href="frm_pronostico.aspx" class="logo"><strong>Pronóstico de la tienda </strong>  <asp:Label ID="lbl_titulo"  runat="server"  Text=""></asp:Label></a>
									<%--<ul class="icons">
										<li><a href="#" class="icon fa-twitter"><span class="label">Twitter</span></a></li>
										<li><a href="#" class="icon fa-facebook"><span class="label">Facebook</span></a></li>
										<li><a href="#" class="icon fa-snapchat-ghost"><span class="label">Snapchat</span></a></li>
										<li><a href="#" class="icon fa-instagram"><span class="label">Instagram</span></a></li>
										<li><a href="#" class="icon fa-medium"><span class="label">Medium</span></a></li>
									</ul>--%>
								</header>

							<!-- Banner -->
							
									<div class="content">
										<asp:UpdatePanel ID="pan1" runat="server">
        <ContentTemplate>
        <center>
            <br />
            <div style="float:left;">
                </div>
            <div style="float:right;">
                 <asp:Button ID="btn_siguiente" runat="server"  CausesValidation="true"
                                      Text="Siguiente" OnClick="btn_siguiente_Click" />
                </div>
            <br /><br />
          <table border="0"  style="width:80%; "> 
                    <tr class="espacio" style="height:5px;">
                        <td >
                            <asp:Label ID="Label39" runat="server" Text="CENTRO:"></asp:Label>
                        </td>
                        <td >
                            <asp:TextBox ID="txt_busCentro" runat="server"  MaxLength="5"></asp:TextBox>
                        </td>
                </tr>
                    <tr class="espacio">
                        <td>
                            <asp:Label ID="Label31" runat="server" Text="EAN:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_busEan" AutoPostBack="true" runat="server"  MaxLength="12" OnTextChanged="txt_busEan_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="espacio">
                        <td>
                            <asp:Label ID="Label38" runat="server" Text="SKU:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_busSku" AutoPostBack="true" runat="server" MaxLength="50" OnTextChanged="txt_busSku_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                </table>
           </center>
        </ContentTemplate>
    </asp:UpdatePanel>
									    <br />
                                        <asp:UpdatePanel ID="pan2" runat="server">
                                            <ContentTemplate>
                                                <center>
                                                    <br />
                                                    <div style="float:left;">
                                                        <asp:Button ID="btn_regresarPan2" runat="server" CausesValidation="true"  Text="Regresar" OnClick="btn_regresarPan2_Click" />
                                                    </div>
                                                    <div style="float:left;">
                                                    </div>
                                                    <div style="float:right;">
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <table border="0"   style="width:80%; background-color:white; ">
                                                        <tr style="background-color:white; height:10px;">
                                                            <td colspan="3">
                                                              <h4><asp:Label ID="lbl_tienda" runat="server"></asp:Label></h4>  
                                                            </td>
                                                            <td style="width:25%">
                                                                <div style="float:left;">
                                                                <asp:Label ID="lbl_fecha1" runat="server">Ext:</asp:Label>
                                                                <asp:Label ID="lbl_fechaExt" runat="server"></asp:Label>

                                                                </div>
                                                                 <br />
                                                                <div style="float:left;">
                                                                <asp:Label ID="lbl_fecha0" runat="server">Pro:</asp:Label>
                                                                    <asp:Label ID="lbl_fechaPro" runat="server"></asp:Label>

                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr class="espacio" style="background-color:white;">
                                                            <td>
                                                                 <h4><asp:Label ID="Label32" runat="server" Text="EAN:"></asp:Label> </h4>
                                                            </td>
                                                            <td  >
                                                                <asp:Label ID="lbl_ean" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td >
                                                               <h4> <asp:Label ID="Label2" runat="server" Text="CLASE: "></asp:Label></h4>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="lbl_clase" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="espacio" style="background-color:white;">
                                                            <td >
                                                                <h4> <asp:Label ID="Label33" runat="server" Text="SKU:"></asp:Label> </h4>
                                                            </td>
                                                            <td colspan="3" >
                                                                <asp:Label ID="lbl_sku" runat="server" Text=""></asp:Label>
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr class="espacio" style="background-color:white;">
                                                            <td>
                                                                 <h4><asp:Label ID="Label34" runat="server" Text="ESTADO LOGÍSTICO:"></asp:Label> </h4>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="lbl_estadoLogistico" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td >
                                                                 <h4><asp:Label ID="Label4" runat="server" Text="UXC: "></asp:Label> </h4>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="lbl_uxc" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                 <%--       </table>
                                                         <table border="0"  style="width:80%; background-color:white; ">--%>
                                                        <tr style="background-color:white;">
                                                            <td>
                                                                <h4> <asp:Label ID="Label35" runat="server" Text="INVENTARIO:"></asp:Label> </h4>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_inventario" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td>
                                                               <h4>  <asp:Label ID="Label36" runat="server" Text="EQUIVALE:"></asp:Label> </h4>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_equivale" runat="server" Text=""></asp:Label>
                                                            </td>
                                                         <tr style="background-color:white;">
                                                            <td >
                                                                 <h4><asp:Label ID="Label1" runat="server" Text="EN TRÁNSITO:"></asp:Label> </h4>
                                                            </td>
                                                            <td >
                                                                <asp:Label ID="lbl_transito" runat="server" Text=""></asp:Label>
                                                            </td>
                                                             <td >
                                                                <h4> <asp:Label ID="Label40" runat="server" Text="VNT SEM ANT:"></asp:Label> </h4>
                                                             </td>
                                                             <td >
                                                                 <asp:Label ID="lbl_ventas" runat="server" Text=""></asp:Label>
                                                             </td>
                                                        </tr>
                                                         <tr style="background-color:white;">
                                                            <td >
                                                                 <h4><asp:Label ID="Label3" runat="server" Text="PRONÓSTICO SEMANAL:"></asp:Label> </h4>
                                                            </td>
                                                            
                                                             <td align="left" >
                                                                 <asp:Label ID="lbl_pronostico" runat="server" Text=""></asp:Label>
                                                             </td>
                                                              <td>
                                                             </td>
                                                             <td >&nbsp;</td>
                                                        </tr>
                                                      <%--       </table>
                                                              <table border="0"  style="width:80%; background-color:white; ">--%>
                                                        
                                                            <tr style="background-color:white;">
                                                                <td colspan="4"> 
                                                                    <asp:GridView ID="grid" runat="server" AllowPaging="true"  OnSorting="gridView_Sorting" 
                                                                        AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="3" CssClass="grid_interno" 
                                                                        GridLines="Vertical" onpageindexchanging="grid_PageIndexChanging" PageSize="20">
                                                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="PROMOCION" HeaderText="PROMOCION" ItemStyle-Width="50%" SortExpression="Name">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="FECHA_INI" HeaderText="FECHA INICIO" ItemStyle-Width="25%" SortExpression="Name">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="FECHA_FIN" HeaderText="FECHA FIN" ItemStyle-Width="25%" SortExpression="Name">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="left" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="#E60613" Font-Bold="True" ForeColor="White" />
                                                                        <PagerSettings FirstPageText="Anterior" LastPageText="Final" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PageButtonCount="4" PreviousPageText="Inicio" />
                                                                        <PagerStyle CssClass="active" HorizontalAlign="Center" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        
                                                      
                                                            <%--          </table>
                                                         <table border="0"  style="width:80%; background-color:white; ">--%>
                                                        
                                                      
                                                            <tr style="background-color:white;">
                                                                <td colspan="4">
                                                                    <h4><asp:Label ID="lbl_pedidoPen" runat="server" Text="PEDIDOS PENDIENTES"></asp:Label> </h4>
                                                                </td>
                                                            </tr>
                                                            <tr style="background-color:white;">
                                                                <td colspan="4">
                                                                    <asp:GridView ID="grid_pedPen" runat="server" AllowPaging="true" AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="3" CssClass="grid_interno" GridLines="Vertical" onpageindexchanging="grid_pedPen_PageIndexChanging"  PageSize="20">
                                                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="PEDIDO" HeaderText="PEDIDO" ItemStyle-Width="30%" SortExpression="Name">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="POSICION" HeaderText="POSICION" ItemStyle-Width="15%" SortExpression="Name">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="FECHA" HeaderText="FECHA ENTREGA" ItemStyle-Width="15%" SortExpression="Name">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="CANTIDAD" HeaderText="CANTIDAD" ItemStyle-Width="10%" SortExpression="Name">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="left" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="#E60613" Font-Bold="True" ForeColor="White" />
                                                                        <PagerSettings FirstPageText="Anterior" LastPageText="Final" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PageButtonCount="4" PreviousPageText="Inicio" />
                                                                        <PagerStyle CssClass="active" HorizontalAlign="Center" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        
                                                      
                                                         <tr style="background-color:white;">
                                                            <td >
                                                                <h4>
                                                                    <asp:Label ID="Label7" runat="server" Text="CNT. ASIG. CABECERA:"></asp:Label>
                                                                </h4>
                                                             </td>
                                                            <td >
                                                                <asp:Label ID="lbl_cabecera" runat="server" Text=""></asp:Label>
                                                            </td>
                                                             <td >
                                                                &nbsp;</td>
                                                             <td >
                                                                &nbsp;</td>
                                                             </tr>
                                                            <tr style="background-color:white;">
                                                                <td>
                                                                    <h4>
                                                                        <asp:Label ID="Label9" runat="server" Text="CNT. ASIG. MAYORISTA:"></asp:Label>
                                                                    </h4>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_codMayorista" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </tr>
                                                         </tr>
                                                         </table>
                                                </center>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
									</div>

            </ContentTemplate>
       </asp:UpdatePanel>
						</div>
					</div>
</asp:Content>
