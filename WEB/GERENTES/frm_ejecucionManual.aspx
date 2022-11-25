<%@ Page Title="" Language="C#" MasterPageFile="~/NUO.Master" AutoEventWireup="true" CodeBehind="frm_ejecucionManual.aspx.cs" Inherits="GERENTES.frm_ejecucionManual" %>
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
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="panGlobal">
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
                            
   <asp:UpdatePanel ID="panGlobal" runat="server"> 
        <ContentTemplate>
            <div class="row"   runat="server"  id="alerta">
    <asp:Panel ID="pn_alerta" Width="100%" runat="server">
      
    </asp:Panel>     
                </div>
							<!-- Header -->
								<header id="header">
									<a href="frm_ejecucionManual.aspx" class="logo"><strong>Ejecución Manual del  </strong>  <asp:Label ID="lbl_usuario"  runat="server"  Text="Demonio"></asp:Label></a>
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
                <center>
                         <asp:Button ID="btn_ejecutar" runat="server" CausesValidation="false" 
                             CssClass="btn btn-primary" Text="Ejecutar Carga de Archivos" OnClick="btn_ejecutar_Click" />
                        <br />
                     <br />
                     <asp:Button ID="btn_ejecutaExt" runat="server" CausesValidation="false" 
                             CssClass="btn btn-primary" Text="Ejecutar Carga de Archivos Extractor" OnClick="btn_ejecutaExt_Click" />
                         <br />
                     <br />
                     <asp:Button ID="btn_ejecutaProy" runat="server" CausesValidation="false" 
                             CssClass="btn btn-primary" Text="Ejecutar Carga de Archivos Proyección" OnClick="btn_ejecutaProy_Click" />
                             <br />
                     <br />
                     <asp:Button ID="btn_ejectuaVent" runat="server" CausesValidation="false" 
                             CssClass="btn btn-primary" Text="Ejecutar Carga de Archivos Ventas" OnClick="btn_ejectuaVent_Click"  />
                </center><br />
                <br />
            </ContentTemplate>
                                             <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_ejecutar" />
    </Triggers>
            </asp:UpdatePanel>
									</div>
                                    <%--<span class="image object">
										<img src="images/pic10.jpg" alt="" />
									</span>--%>
							

            </ContentTemplate>
       </asp:UpdatePanel>
                            
						</div>
					</div>
</asp:Content>

