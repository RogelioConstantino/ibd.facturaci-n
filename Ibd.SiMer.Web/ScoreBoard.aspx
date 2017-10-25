<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ScoreBoard.aspx.cs" Inherits="Ibd.SiMer.Web.ScoreBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cuerpo" runat="server">
    
      <div class="container" id="contenedor">

        <style>
            .indice_pagina{
                        background: #A4BA08 !important;
                }

            .mdl-button--raised.mdl-button--colored
            {
                        background: #A4BA08 !important;
                }

            .breadcrumb-verde{
                 background-color:white;
                color: black;
                padding-top:4px;  padding-bottom:2px;  margin-bottom: 2px;
            }

            .breadcrumIBR {
                color: black !important;
                background-color: #ffffff !important;
                width: 100%;
                height: 56px;
                line-height: 56px;
               box-shadow: 0 0 0 0 rgba(255,255,255,0), 0 0 0 0 rgba(255,255,255,0);
            }
            .breadcrumbIBR {
            color: black !important;
                background-color: #ffffff !important;
            }

            .breadcrumb { 
              list-style: none; 
              overflow: hidden; 
              font: 18px Sans-Serif;
            }
            .breadcrumb li { 
              float: left; 
            }
            .breadcrumb li a {
              color: white;
              text-decoration: none; 
              padding: 10px 0 10px 65px;
              background: brown; /* fallback color */
              background: hsla(34,85%,35%,1); 
              position: relative; 
              display: block;
              float: left;
            }

            .breadcrumb li a::after { 
              content: " "; 
              display: block; 
              width: 0; 
              height: 0;
              border-top: 50px solid transparent; /* Go big on the size, and let overflow hide */
              border-bottom: 50px solid transparent;
              border-left: 30px solid hsla(34,85%,35%,1);
              position: absolute;
              top: 50%;
              margin-top: -50px; 
              left: 100%;
              z-index: 2; 
            }

            .breadcrumb li a::before { 
              content: " "; 
              display: block; 
              width: 0; 
              height: 0;
              border-top: 50px solid transparent;       
              border-bottom: 50px solid transparent;
              border-left: 30px solid white;
              position: absolute;
              top: 50%;
              margin-top: -50px; 
              margin-left: 1px;
              left: 100%;
              z-index: 1; 
            }

            .breadcrumb li:first-child a {
              padding-left: 10px;
            }
            .breadcrumb li:nth-child(2) a       { background:        hsla(34,85%,45%,1); }
            .breadcrumb li:nth-child(2) a:after { border-left-color: hsla(34,85%,45%,1); }
            .breadcrumb li:nth-child(3) a       { background:        hsla(34,85%,55%,1); }
            .breadcrumb li:nth-child(3) a:after { border-left-color: hsla(34,85%,55%,1); }
            .breadcrumb li:nth-child(4) a       { background:        hsla(34,85%,65%,1); }
            .breadcrumb li:nth-child(4) a:after { border-left-color: hsla(34,85%,65%,1); }
            .breadcrumb li:nth-child(5) a       { background:        hsla(34,85%,75%,1); }
            .breadcrumb li:nth-child(5) a:after { border-left-color: hsla(34,85%,75%,1); }
            .breadcrumb li:last-child a {
              background: transparent !important;
              color: black;
              pointer-events: none;
              cursor: default;
            }
            .breadcrumb li:last-child a::after { 
              border: 0; 
            }
            
            .arrows { white-space: nowrap; }
            .arrows li {
                display: inline-block;
                line-height: 14px;
                margin: 0 9px 0 -10px;
                padding: 0 20px;
                position: relative;
            }
            .arrows li::before,
            .arrows li::after {
                border-right: 1px solid #666666;
                content: '';
                display: block;
                height: 50%;
                position: absolute;
                left: 0;
                right: 0;
                top: 0;
                z-index: -1;
                transform: skewX(45deg);   
            }
            .arrows li::after {
                bottom: 0;
                top: auto;
                transform: skewX(-45deg);
            }

            .arrows li:last-of-type::before, 
            .arrows li:last-of-type::after { 
                display: none; 
            }

            .arrows li a { 
               font: bold 14px Sans-Serif;  
               letter-spacing: -1px; 
               text-decoration: none;
            }

            .arrows li:nth-of-type(1) a { color: hsl(0, 0%, 70%); } 
            .arrows li:nth-of-type(2) a { color: hsl(0, 0%, 65%); } 
            .arrows li:nth-of-type(3) a { color: hsl(0, 0%, 50%); } 
            .arrows li:nth-of-type(4) a { color: hsl(0, 0%, 45%); } 
            
            .tbody 
            {
               overflow: auto !important;
               height: 100px !important;
            }
        </style>

        <ul class="arrows">
            <li><a href="#">Inicio</a></li>
            <li><a href="#">Tablero Principal</a></li>
        </ul>
                 
        <div class="col s12 center">                
            <h2>Tablero Principal</h2>
        </div>

        <div class="row">
            <div class="col l12">
                <div class="z-depth-2" style="padding:20px; padding-bottom:60px">                    
                        <div class="col l12">
                            <div class="col l6">
                                <h5>Avance de Cargas</h5>
                            </div>
                            <div class="col l2 col-lg-offset-2 text-right" style="margin-top:18px !important;">
                                <span class="card-subtitle" >Año:</span><span class="card-subtitle  green-text darken-4-text">2017</span>
                            </div>
                            <div class="col l2 text-right" style="margin-top:18px !important;"> 
                                <span class="card-subtitle" >Mes:</span><span class="card-subtitle  green-text darken-4-text">Marzo</span>
                            </div>
                            <div class="col l2 text-right" style="margin-top:18px !important;">
                                <a class="btn-medium waves-effect waves-light  white darken-3"><i class="material-icons light-green-text">system_update_alt</i></a>
                            </div>
                        </div>
                        <table id="mytableNum" class="highlight striped bordered"   >
                            <asp:PlaceHolder ID="DBDataPlaceHolderNum" runat="server"></asp:PlaceHolder>  
                        </table>
                        <div class="footer-copyright">
                            <div class="col l12">
                                <div class="col l2"><i class='small material-icons  light-green-text text-darken-3 ' style="margin-top:3px">done</i>&nbsp;Correcto</div>
                                <div class="col l3"><i class='small material-icons blue-text ' style="margin-top:3px">error</i> &nbsp;  Error<span class="text-info">.-Mas registros de los validos</span> </div>                                
                                <div class="col l3"><i class='small material-icons ' style="margin-top:3px">warning</i>&nbsp;Precaución<span class=" text-info">.-Una hora de desfase</span></div>
                                <div class="col l3"><i class='small material-icons red-text text-darken-3' style="margin-top:3px">report_problem</i>&nbsp;Problema<span class="text-info">.-Faltan registros</span></div>                                                               
                            </div>
                        </div>                        
                    </div>                                   
                </div>            
        </div>    
              
          <br>
                  
        <div class="row">
            <div class="col l12">
                <div class="z-depth-2" style="padding:20px; padding-bottom:60px">                       
                        <div class="col l12">
                            <div class="col l6">
                                <h5>Total de cargas por día y punto de carga</h5>
                            </div>
                            <div class="col l2 col-lg-offset-2 text-right"  style="margin-top:18px !important;">
                                <span class="card-subtitle">Año:</span><span class="card-subtitle  green-text darken-4-text">2017</span>
                            </div>
                            <div class="col l2 col-lg-offset-1  text-right"  style="margin-top:18px !important;">
                                <span class="card-subtitle">Mes:</span><span class="card-subtitle  green-text darken-4-text">Marzo</span>
                            </div>
                            <div class="col l2 text-right" style="margin-top:18px !important;">
                                <a class="btn-medium waves-effect waves-light  white darken-3"><i class="material-icons light-green-text">system_update_alt</i></a>
                            </div>
                        </div>                                                                
                        <div style=" width:88em;    overflow-x: auto;  white-space: nowrap;" >
                            <table id="mytableCarga" class=" display compact highlight striped bordered " cellspacing="0" width="100%" >
                                <asp:PlaceHolder ID="DBDataPlaceHolderCarga" runat="server"></asp:PlaceHolder>  
                            </table>   
                       </div>                    
                    </div>                    
               
                            
            </div>
        
      </div>
                            
</div>  
         
    <script>

        $(document).ready(function () {

            $('#myTable2').DataTable();
        
            var table = $('#mytableCarga').DataTable({ paging: false, search: false});
        
        });

    </script>
    
</asp:Content>
