<%@ Page Title="" Language="C#" MasterPageFile="~/NUO.Master" AutoEventWireup="true" CodeBehind="frm_correo.aspx.cs" Inherits="GERENTES.frm_correo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            
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
									<a href="frm_correo.aspx" class="logo"><strong>Mantenimiento de </strong>  <asp:Label ID="lbl_usuario"  runat="server"  Text="Correos"></asp:Label></a>
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
                <div style="float:right;">
                         &nbsp;<asp:Button ID="btnNuevo" runat="server" CausesValidation="false" 
                             CssClass="btn btn-primary" onclick="btnNuevo_Click" Text="Nuevo" />
                </div><br />
                <br />
            <table style="width:100%" >   
                                    
                 <tr >
                     <td>
                         <asp:GridView ID="grid" runat="server" AllowPaging="true" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="3" 
                             CssClass="grid_interno" GridLines="Vertical" onpageindexchanging="grid_PageIndexChanging"  onselectedindexchanging="grid_SelectedIndexChanging" PageSize="10">
                             <AlternatingRowStyle BackColor="#CCCCCC" />
                             <Columns>
                                 <asp:BoundField DataField="CODIGO_COR"  HeaderText="#. REGISTRO" ItemStyle-Width="20%" SortExpression="Name">
                                 <HeaderStyle HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="Center" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="CORREO_COR" ItemStyle-HorizontalAlign="Right" HeaderText="CORREO" ItemStyle-Width="70%" SortExpression="Name">
                                 <HeaderStyle HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="Center" />
                                 </asp:BoundField>
                                
                                 <asp:CommandField ButtonType="Button" SelectText="Editar" ShowSelectButton="True">
                                 <ControlStyle CssClass="btn btn-sm btn-primary" />
                                 </asp:CommandField>
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
										<asp:UpdatePanel ID="panCrear" runat="server">
        <ContentTemplate>
        <center>
            <br />
            <div style="float:left;">
                <asp:Button ID="btnCerrar" runat="server" CausesValidation="false" 
                                            Text="Cancelar" OnClick="btnCerrar_Click" />
                </div>
            <div style="float:right;">
                 <asp:Button ID="btnGuardar" runat="server"  CausesValidation="true"
                                      Text="Guardar" OnClick="btnGuardar_Click" />
                </div>
            <br /><br />
          <table border="0"  style="width:80%; ">  
              
               
                    <tr>
                        <td style="width:20%">
                            <asp:Label ID="Label31" runat="server" Text="Descripción:"></asp:Label>
                        </td>
                        <td style="width:80%">
                            <asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control"  MaxLength="150"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txt_nombre" CssClass="estilo4" ErrorMessage="***" />
                        </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        &nbsp;</td>
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
