<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_login.aspx.cs" Inherits="GERENTES.frm_login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />

    <title>SCC-GERENTES</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <link href="css/sb-admin.css" rel="stylesheet" />

    <!-- Custom Fonts -->
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet"  />
   

</head>

<body style="background-color:#E40613; background-image:url(images/fondo.jpg)">
    <div id="header">
        <div class="login text-center">
            
            <div class="login-content ">
                
              <form id="form1" runat="server">
                  <img src="images/logo.png">
                    <h3>Ingreso</h3>
                    <asp:TextBox ID="txt_alias" runat="server" placeholder="Usuario" CssClass="form-control"></asp:TextBox>
                    <asp:TextBox ID="txt_clave" type="password" runat="server" placeholder="Contraseña" CssClass="form-control"></asp:TextBox>

<asp:Label ID="lbl_mensaje" runat="server" Text=""></asp:Label>
                    <div class="text-center">
    <asp:Button ID="btn_ingresar" runat="server" class="btn btn-primary" Text="Entrar" 
                            onclick="btn_ingresar_Click" />
                    </div>
                 
                   <div class="brand-logo">
            <img src="images/desarrollado.png" class="img-responsive">
        </div>
                </form>
            </div>
        </div>
    </div>
    <!-- /#wrapper -->
    <!-- jQuery -->
  
</body>

</html>
