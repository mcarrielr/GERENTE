<%@ Page Title="" Language="C#" MasterPageFile="~/NUO.Master" AutoEventWireup="true" CodeBehind="frm_usuario.aspx.cs" Inherits="GERENTES.frm_usuario" %>
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
									<a href="frm_usuario.aspx" class="logo"><strong>Mantenimiento de </strong>  <asp:Label ID="lbl_usuario"  runat="server"  Text="Usuarios"></asp:Label></a>
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
                         <asp:Button ID="btn_abrirBuscar" runat="server" CausesValidation="false" 
                             CssClass="btn btn-primary" onclick="btn_abrirBuscar_Click" Text="Buscar" />
                         &nbsp;<asp:Button ID="btnNuevo" runat="server" CausesValidation="false" 
                             CssClass="btn btn-primary" onclick="btnNuevo_Click" Text="Nuevo" />
                </div><br />
                <br />
            <table style="width:100%" >   
                                    
                 <tr >
                     <td style="width:33%">
                         <asp:Label ID="Label44" runat="server" Text="Nombre:"></asp:Label>
                     </td>
                     <td style="width:33%">
                         &nbsp;</td>
                     <td style="width:33%">
                         &nbsp;</td>
                 </tr>
               
                 <tr>
                     <td>
                         <asp:TextBox ID="txt_busNombre" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox>
                     </td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td colspan="3">
                         <asp:GridView ID="grid" runat="server" AllowPaging="true" AutoGenerateColumns="False" OnRowCommand="CustomersGridView_RowCommand" 
                           BorderWidth="1px" CellPadding="3" CssClass="grid_interno" GridLines="Vertical" onpageindexchanging="grid_PageIndexChanging" OnRowDeleting="gridShow_RowDeleting" onselectedindexchanging="grid_SelectedIndexChanging" PageSize="10">
                             <AlternatingRowStyle BackColor="#CCCCCC" />
                             <Columns>
                                 <asp:BoundField DataField="APELLIDO_USR" HeaderText="APELLIDO" ItemStyle-Width="30%" SortExpression="Name">
                                 <HeaderStyle HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="left" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="NOMBRE_USR" HeaderText="NOMBRE" ItemStyle-Width="30%" SortExpression="Name">
                                 <HeaderStyle HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="left" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="DESCRIPCION_PUS" HeaderText="PERFIL" ItemStyle-Width="20%" SortExpression="Name">
                                 <HeaderStyle HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="left" />
                                 </asp:BoundField>
                                 <asp:CommandField ButtonType="Button" SelectText="Editar" ShowSelectButton="True">
                                 <ControlStyle CssClass="btn btn-sm btn-primary" />
                                 </asp:CommandField>
                                 <asp:CommandField ButtonType="Button" DeleteText="Rol" HeaderText="" ShowDeleteButton="True">
                                 <ControlStyle CssClass="btn btn-sm btn-primary" />
                                 </asp:CommandField>
                                 <asp:ButtonField ButtonType="Button" CommandName="ELIMINAR" Text="Eliminar" >
                                  <ControlStyle CssClass="button2" />
                                  </asp:ButtonField>
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
             <div style="float:right;">
                                        <asp:Button ID="btnGuardar" runat="server"  CausesValidation="true"
                                      Text="Guardar" OnClick="btnGuardar_Click" />
                                         </div>
                                    <div style="float:left;">
                                        <asp:Button ID="btnCerrar" runat="server" CausesValidation="false" 
                                            Text="Cancelar" OnClick="btnCerrar_Click" /></td>
                    </div>
            <br />
            <br /><br />
          <table border="0"  style="width:80%; ">   
                
                    <tr  class="espacio">
                        <td style="width:20%">
                            <asp:Label ID="Label45" runat="server" Text="Centro:"></asp:Label>
                        </td>
                        <td style="width:80%">
                            
                            <asp:DropDownList ID="lst_centro" runat="server" class="select-wrapper">
                            </asp:DropDownList>
                        </td>
                </tr>
                    <tr  class="espacio">
                        <td style="width:20%">
                            <asp:Label ID="Label31" runat="server" Text="Nombre:"></asp:Label>
                        </td>
                        <td style="width:80%">
                            <div style="float:left; width:90%" ><asp:TextBox ID="txt_nombre" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox></div>
                            <div style="float:left;" ><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_nombre" CssClass="estilo4" ErrorMessage="***" /></div>
                        </td>
                    </tr>
                    <tr class="espacio">
                        <td style="width:20%">
                            <asp:Label ID="Label42" runat="server" Text="Apellido:"></asp:Label>
                        </td>
                        <td style="width:80%">
                            <div style="float:left; width:90%" ><asp:TextBox ID="txt_apellido" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox></div>
                            <div style="float:left;"><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txt_apellido" CssClass="estilo4" ErrorMessage="***" /></div>
                        </td>
                </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label32" runat="server" Text="Alias:"></asp:Label>
                        </td>
                        <td >
                           <div style="float:left; width:90%" > <asp:TextBox ID="txt_alias" runat="server" CssClass="form-control" AutoPostBack="true" MaxLength="150" OnTextChanged="txt_alias_TextChanged"></asp:TextBox></div>
                            <div style="float:left;" ><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txt_alias" CssClass="estilo4" ErrorMessage="***" /></div>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:Label ID="Label34" runat="server" Text="Perfil:"></asp:Label>
                        </td>
                        <td >
                           <div style="float:left; width:90%" > <asp:DropDownList ID="lst_perfil" runat="server" class="select-wrapper" >
                            </asp:DropDownList> </div>
                            <div style="float:left; " ><asp:CustomValidator ID="CustomValidator10" runat="server" 
                                                 ClientValidationFunction="validationFunction" 
                                                 ControlToValidate="lst_perfil" CssClass="validationClass estilo4" 
                                                 ErrorMessage="Seleccione una Empresa" ValidateEmptyText="true">***</asp:CustomValidator></div>
                        </td>
                    </tr>
              
              <div runat="server" id="div_contrasenia" >
                <tr>
                    <td>
                        <asp:Label ID="Label39" runat="server" Text="Contraseña:"></asp:Label>
                    </td>
                    <td>
                        <div style="float:left; width:90%" ><asp:TextBox ID="txt_contrasenia" runat="server" 
                            TextMode="Password"></asp:TextBox></div>
                     <div style="float:left; " ><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txt_contrasenia" CssClass="estilo4" ErrorMessage="***" /></div>
                    </td>
                </tr>
                  </div>
              <div runat="server" id="divReseteo" >
                  <tr>
                        <td >
                            <asp:Label ID="lbl_resetear" runat="server" Text="Reseterar Contraseña:"></asp:Label>
                        </td>
                        <td  >
                                <asp:DropDownList ID="lst_cambioContrasenia" runat="server" class="select-wrapper">
                                    <asp:ListItem Value="1">SI</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                </asp:DropDownList>
                            
                        </td>
                    </tr>
              </div>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                </table>
           </center>
        </ContentTemplate>
    </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="panRol" runat="server">
        <ContentTemplate>
        <center>
            <br /><br />
                                        <div class="table-responsive">
        <div style="float:left;">
            <asp:Button ID="btn_cerrarRol" runat="server" CausesValidation="false" 
                                            CssClass="btn btn-primary" Text="Regresar" OnClick="btn_cerrarRol_Click" 
                                         /></div>
                                            <div style="float:right;">
                                                <asp:Button ID="btn_sigRol" runat="server" CausesValidation="false" 
                                            CssClass="btn btn-primary" Text="Finalizar" OnClick="btn_sigRol_Click"  
                                         />
                                                </div>
            <br /><br />
          <table class="table-wrapper" >   
                    
                    <tr>
                        <td align="center" style="width:45%">
                            <asp:Label ID="Label3" runat="server" Text="Roles no pertenecientes al Usuario"></asp:Label>
                        </td>
                        <td style="width:10%">&nbsp;</td>
                        <td align="center" style="width:45%">
                            <asp:Label ID="Label5" runat="server" Text="Roles pertenecientes al Usuario"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <div style="float:left; width:100%;">
                                  <asp:ListBox ID="listaGrupos_Izquierda" runat="server" Height="150px"
                                   ></asp:ListBox>
                                </div>
                          
                            
                          
                        </td>
                        <td align="center">
                            <div style="float:left;">
                            <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/images/iconos/next.gif" onclick="btnAgregar_Click" title="Agregar" Width="16px" />
                                </div>
                            <div style="float:right;">
                            <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/images/iconos/prev.gif" onclick="btnEliminar_Click" title="Eliminar" />
                            </div>
                        </td>
                        <td >
                              <div style="float:right; width:100%;">
                                  <asp:ListBox ID="listaGrupos_Derech" runat="server" Height="150px"></asp:ListBox>
                            </div></td>
                </tr>
                </table>
                </div>

             </center>
        </ContentTemplate>
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
