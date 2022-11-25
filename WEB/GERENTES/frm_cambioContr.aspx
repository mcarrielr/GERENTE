<%@ Page Title="" Language="C#" MasterPageFile="~/NUO.Master" AutoEventWireup="true" CodeBehind="frm_cambioContr.aspx.cs" Inherits="GERENTES.frm_cambioContr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main">
						<div class="inner">
                            
   <asp:UpdatePanel ID="panGlobal" runat="server"> 
        <ContentTemplate>
            <div class="row"   runat="server"  id="alerta">
    <asp:Panel ID="pn_alerta" Width="100%" runat="server">
      
    </asp:Panel>     
                </div>
							<!-- Header -->
								<header id="header">
									<a href="frm_cambioContr.aspx" class="logo"><strong>Mantenimiento de </strong>  <asp:Label ID="lbl_usuario"  runat="server"  Text="Contraseña"></asp:Label></a>
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
                      <asp:Button ID="btn_siguiente" runat="server"  CausesValidation="true"
                                 Text="Siguiente" onclick="btn_siguiente_Click" />
                     </div>
                <div style="float:right;">
                     <asp:Button ID="btn_Guardar" runat="server"  
                                 onclick="btn_Guardar_Click" Text="Finalizar" />
                     </div>
                <br />
                <br />
                <br />
     <table border="0" style="width:100%" > 
                     <div runat="server" id="div_actual">
                     <tr >
                         <td style="width:30%">
                             <asp:Label ID="lbl_1" runat="server" Text="Contraseña Actual:"></asp:Label>
                         </td>
                         <td style="width:70%">
                             <asp:TextBox ID="txt_contraseniaActual"  runat="server" 
                                 MaxLength="150" Width="90%"  TextMode="Password"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                 ControlToValidate="txt_contraseniaActual" CssClass="estilo4" ErrorMessage="***" />
                         </td>
                     </tr>
                     </div>
                     <div runat="server" id="div_contra">
                     <tr class="estilo5">
                         <td>
                             <asp:Label ID="lbl_2" runat="server" Text="Contraseña Nueva:"></asp:Label>
                         </td>
                         <td>
                             <asp:TextBox ID="txt_contrasenia" runat="server"   
                                 MaxLength="150" Width="90%" TextMode="Password" ></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                 ControlToValidate="txt_contrasenia" CssClass="estilo4" ErrorMessage="***" />
                         </td>
                     </tr>
                    <tr class="estilo5">
                        <td>
                            <asp:Label ID="lbl_3" runat="server" Text="Verificar Contraseña:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_verContrasenia" runat="server" MaxLength="150" Width="90%" 
                              TextMode="Password" 
                                ></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txt_verContrasenia" CssClass="estilo4" ErrorMessage="***" />

                        </td>
                    </tr>
                    </div>
                </table>
                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
            </ContentTemplate>
       </asp:UpdatePanel>
                            </div>
        </div>
</asp:Content>

