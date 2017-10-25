<%@ Page Title="Iberdrola-Anexo Mercado-Inicio de sessión" Language="C#" AutoEventWireup="true"    CodeBehind="Default.aspx.cs" Inherits="Ibd.SiMer.Web._Default" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
	<title>Inicio de sesión- Sistema de Anexos de Facturación - Iberdrola</title>
	<meta charset="utf-8">
    
      
        <link rel="icon" href="favicon.ico" />
        <link rel="shortcut icon" href="favicon.ico"/>

        <asp:PlaceHolder runat="server">
        <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />

        <link href="https://cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css" rel="stylesheet" />

        <%: Scripts.Render("~/bundles/js") %>
        <%: Styles.Render("~/bundles/material") %>
        <%: Styles.Render("~/bundles/css") %>

        

    </asp:PlaceHolder>

    <meta http-equiv="x-ua-compatible" content="ie=edge">

      <style>   

           /*.input-field input[type=text]:focus + label {
             color: #e75607;
           } 
           
           .input-field input[type=text]:focus {
             border-bottom: 1px solid #e75607;
             box-shadow: 0 1px 0 0 #e75607;
           }

           
           .input-field input[type=password]:focus + label {
             color: #e75607;
           } 
           
           .input-field input[type=password]:focus {
             border-bottom: 1px solid #e75607;
             box-shadow: 0 1px 0 0 #e75607;
           }

           .input-field .prefix.active {
             color: #e75607;
           }*/
    


      .full {
            /*background: url("content/img/wall.jpg") no-repeat center center fixed;*/
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
       }

       .centered {
          position: fixed;
          top: 50%;
          left: 50%;
          
       }

   </style>

</head>
<body   class="full">

   <div class="container">
    <div class="row centered">
                 
        <div style=" background-color:white;  width:100%; ">

        <div class="col m5 offset-4m" style="  position: fixed;  top: 50%;  left: 50%;  transform: translate(-50%, -50%);    color:rgb(92,136,26); background-color:white;">

              <div class="center-align">
                <img src="content/img/iberdrola-logo.png" />
                              <h6 class="center-align">La nueva energía de México</h6>
              </div>
            <br />
              <div>
                <h4 class="center-align">Herramienta de Anexos de Facturación</h4>

              </div>

            <br />
            <br />

            <h6 class="center-align"  style="color: #427314 !important;">Iniciar sesión</h6>
            <div class="divider"></div>
            <div class="row">
                <form class="col s12"  runat="server" style="background-color:white;">
                    <div class="row" style="background-color:white;">
                        <div class="input-field col s12" style="background-color:white;">
                            <i class="material-icons prefix">account_circle</i>                            
                            <input  id="email" type="text" class="validate" runat="server">                            
                            <label for="email" class="active">Usuario</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s12">
                            <i class="material-icons prefix">vpn_key</i>
                            <input id="pass" type="password" class="validate" runat="server">
                            <label for="pass" class="active">Contraseña</label>
                        </div>
                    </div>
                    
                    <div class="divider"></div>

                    <div class="row">
                        <div class="col s12">
    					    <div runat="server" id="ErrorMsg"  class="alert-warning" ></div>
                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col m12">
                            <p class="right-align">
                                
                                <asp:Button ID="btnLogin" runat="server" class="btn btn-large olive waves-effect waves-light light-green darken-3 " onclick="btnLogin_Click" Text="Entrar" />

                            </p>
                        </div>
                    </div>
                </form>
            </div>
        </div>

            </div>
    </div>
</div>

</body>
</html>
