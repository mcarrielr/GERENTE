<%@ Page Title="" Language="C#" MasterPageFile="~/NUO.Master" AutoEventWireup="true" CodeBehind="frm_log.aspx.cs" Inherits="GERENTES.frm_log" %>
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
        
        .auto-style1 {
            height: 130px;
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
									<a href="frm_log.aspx" class="logo"><strong>Mantenimiento de </strong>  <asp:Label ID="lbl_usuario"  runat="server"  Text="Logs"></asp:Label></a>
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
                                        <asp:UpdatePanel ID="panGrid" runat="server">
            <ContentTemplate>
                <br />
                <div style="float:left;">
                         &nbsp;<asp:Button ID="btnNuevo" runat="server" CausesValidation="false" 
                             CssClass="btn btn-primary" Text="Buscar" OnClick="btnNuevo_Click" />
                </div>
                 <div style="float:right;">
                         &nbsp;<asp:Button ID="btn_limpiar" runat="server" CausesValidation="false" 
                             CssClass="btn btn-primary" Text="Limpiar Logs" OnClick="btn_limpiar_Click"  />
                </div>
                <br />
                <br />
            <table style="width:100%" >   
                                
                 <tr >
                     <td style="width:33%">
                         <asp:Label ID="Label31" runat="server" Text="Fecha Inicio"></asp:Label>
                     </td>
                     <td  style="width:33%">
                         <asp:Label ID="Label32" runat="server" Text="Fecha Final"></asp:Label>
                     </td>
                     <td  style="width:33%">
                         &nbsp;</td>
                 </tr>
               
                 <tr>
                     <td>
                         <asp:TextBox ID="txt_fechaIni" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                          <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="txt_fechaIni">
                         </cc1:CalendarExtender>
                     </td>
                     <td>
                         <asp:TextBox ID="txt_fechaFin" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                          <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txt_fechaFin">
                         </cc1:CalendarExtender>
                     </td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td colspan="3">
                         <asp:GridView ID="grid" runat="server" AllowPaging="true" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="3"
                             CssClass="grid_interno" GridLines="Vertical" onpageindexchanging="grid_PageIndexChanging"  PageSize="10">
                             <AlternatingRowStyle BackColor="#CCCCCC" />
                             <Columns>
                                 <asp:BoundField DataField="METODO_LOG" HeaderText="METODO"  ItemStyle-Width="30%" SortExpression="Name">
                                 <HeaderStyle HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="Left" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="FECHA_CRE_LOG" HeaderText="FECHA INICIO"  ItemStyle-Width="20%" SortExpression="Name">
                                  <HeaderStyle HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="Left" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="FECHA_FIN_LOG" HeaderText="FECHA FIN"  ItemStyle-Width="20%" SortExpression="Name">
                                  <HeaderStyle HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="Left" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="ERROR_LOG" HeaderText="ERROR" ItemStyle-Width="30%" SortExpression="Name">
                                  <HeaderStyle HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="Left" />
                                 </asp:BoundField>
                             </Columns>
                             <HeaderStyle CssClass="grid_cabecera" Font-Bold="True" />
                             <PagerSettings FirstPageText="Anterior" LastPageText="Final" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PageButtonCount="4" PreviousPageText="Inicio" />
                             <PagerStyle CssClass="active" HorizontalAlign="Center" />
                         </asp:GridView>
                         <br />
                     </td>
                 </tr>
               
            </table>
            </ContentTemplate>
                                             <Triggers>
        <asp:AsyncPostBackTrigger ControlID="grid" />
    </Triggers>
            </asp:UpdatePanel>
									</div>

            </ContentTemplate>
       </asp:UpdatePanel>
						</div>
					</div>
</asp:Content>

