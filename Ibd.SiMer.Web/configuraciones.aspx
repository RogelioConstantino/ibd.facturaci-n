<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="configuraciones.aspx.cs" Inherits="Ibd.SiMer.Web.configuraciones" %>
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

        <form id="form1" name="form1" runat="server">
            <br /><br />
            <div class="col s12 center">                
                <h5 style="color: #427314 !important;">Gestión de Empresas</h5>
            </div>
            <hr />
            <div style="padding:20px;">  
                <div class="row">        
                    <div class="col s2 offset-s11">             
                        <a class="btn-floating btn waves-effect waves-light  light-green darken-3 olive  modal-trigger" href="#modal1"><i class="material-icons">add</i></a>                        
                    </div> 
                </div> 
	            <div class="row">        
                    <div id="Div1"  runat="server">
                        <table id="mytable" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="DBDataPlaceHolder_Empresa" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> <!-- end table responsive -->
                </div>
            </div>         
        
            
            <br /><br />
            <div class="col s12 center">                
                <h5 style="color: #427314 !important;">Gestión de Grupos</h5>                
            </div>                
            <hr />
            <div style="padding:20px;">            
                <div class="row">        
                    <div class="col s2 offset-s11">             
                        <a class="btn-floating btn waves-effect waves-light  light-green darken-3 olive  modal-trigger" href="#modal2"><i class="material-icons">add</i></a>                        
                    </div> 
                </div> 
	            <div class="row">        
                    <div id="Div2"  runat="server">
                        <table id="mytable2" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="PlaceHolderGpo" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> <!-- end table responsive -->
                </div>
            </div>         
        
            
            <br /><br /><br />
            <div class="col s12 center">                
                <h5 style="color: #427314 !important;">Gestión de Clientes</h5>                
            </div>                
            <hr />
            <div style="padding:20px;">            
                <div class="row">        
                    <div class="col s2 offset-s11">             
                        <a class="btn-floating btn waves-effect waves-light  light-green darken-3 olive  modal-trigger" href="#modal3"><i class="material-icons">add</i></a>                        
                    </div> 
                </div> 
	            <div class="row">        
                    <div id="Div3"  runat="server">
                        <table id="mytable3" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="PlaceHolderCte" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> <!-- end table responsive -->
                </div>
            </div> 

            <br /><br /><br />
            <div class="col s12 center">                
                <h5 style="color: #427314 !important;">Gestión de Puntos de Carga</h5>
            </div>                
            <hr />
            <div style="padding:20px;">
                <div class="row">        
                    <div class="col s2 offset-s11">             
                        <a class="btn-floating btn waves-effect waves-light  light-green darken-3 olive  modal-trigger" href="#modal4"><i class="material-icons">add</i></a>                        
                    </div> 
                </div>                 
	            <div class="row">        
                    <div id="Div4"  runat="server">
                        <table id="mytable4" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="PlaceHolderPC" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> <!-- end table responsive -->
                </div>
            </div> 
            <br /><br /><br />
            <div class="col s12 center">                
                <h5 style="color: #427314 !important;">Gestión de Contratos</h5>
            </div>                
            <hr />
            <div style="padding:20px;">            
                <div class="row">        
                    <div class="col s2 offset-s11">             
                        <a class="btn-floating btn waves-effect waves-light  light-green darken-3 olive  modal-trigger" href="#modal5"><i class="material-icons">add</i></a>                        
                    </div> 
                </div> 
	            <div class="row">        
                    <div id="Div5"  runat="server">
                        <table id="mytable5" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="PlaceHolderCtto" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> <!-- end table responsive -->
                </div>
            </div> 

            <br /><br /><br />
            <div class="col s12 center">                
                <h5 style="color: #427314 !important;">Gestión de Productos</h5>
            </div>                
            <hr />
            <div style="padding:20px;">            
                <div class="row">        
                    <div class="col s2 offset-s11">             
                        <a class="btn-floating btn waves-effect waves-light  light-green darken-3 olive  modal-trigger" href="#modal5"><i class="material-icons">add</i></a>                        
                    </div> 
                </div> 
	            <div class="row">        
                    <div id="Div6"  runat="server">
                        <table id="mytable6" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="PlaceHolderProducto" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> <!-- end table responsive -->
                </div>
            </div> 


            <br /><br /><br />
            <div class="col s12 center">                
                <h5 style="color: #427314 !important;">Gestión de Comportamientos</h5>
            </div>                
            <hr />
            <div style="padding:20px;">            
                <div class="row">        
                    <div class="col s2 offset-s11">             
                        <a class="btn-floating btn waves-effect waves-light  light-green darken-3 olive  modal-trigger" href="#modal5"><i class="material-icons">add</i></a>                        
                    </div> 
                </div> 
	            <div class="row">        
                    <div id="Div7"  runat="server">
                        <table id="mytable7" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="PlaceHolderComportamientos" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> <!-- end table responsive -->
                </div>
            </div> 


            

            <br /><br /><br />
            <div class="col s12 center">                
                <h5 style="color: #427314 !important;">Gestión de Comportamientos Contratos</h5>
            </div>                
            <hr />
            <div style="padding:20px;">            
                <div class="row">        
                    <div class="col s2 offset-s11">             
                        <a class="btn-floating btn waves-effect waves-light  light-green darken-3 olive  modal-trigger" href="#modal5"><i class="material-icons">add</i></a>                        
                    </div> 
                </div> 
	            <div class="row">        
                    <div id="Div8"  runat="server">
                        <table id="mytable8" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="PlaceHolderComportamientocontratos" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> <!-- end table responsive -->
                </div>
            </div> 

            
            <br /><br /><br />
            <div class="col s12 center">                
                <h5 style="color: #427314 !important;">Gestión de Factores de Redución</h5>
            </div>                
            <hr />
            <div style="padding:20px;">            
                <div class="row">        
                    <div class="col s2 offset-s11">             
                        <a class="btn-floating btn waves-effect waves-light  light-green darken-3 olive  modal-trigger" href="#modal5"><i class="material-icons">add</i></a>                        
                    </div> 
                </div> 
	            <div class="row">        
                    <div id="Div9"  runat="server">
                        <table id="mytable9" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="PlaceHolderFactoresReduccion" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> <!-- end table responsive -->
                </div>
            </div> 

            <br /><br /><br />
            <div class="col s12 center">                
                <h5 style="color: #427314 !important;">Gestión Tarifas CFE</h5>
            </div>                
            <hr />
            <div style="padding:20px;">                            
	            <div class="row">        
                    <div id="Div10"  runat="server">
                        <table id="mytable10" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="PlaceHolderTarifas" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> <!-- end table responsive -->
                </div>
            </div> 

        </form>


        <script type="text/javascript">

            $('#mytable').DataTable(
                {
                    "language": {
                        "sLengthMenu": "Mostrar _MENU_ registros",
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando...",
                        "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "Último",
                            "sNext": "Siguiente",
                            "sPrevious": "Anterior"
                        }
                    },
                    "paging": true,
                    "ordering": true,
                    "info": true
                });

            $('#mytable2').DataTable(
                {
                    "language": {
                        "sLengthMenu": "Mostrar _MENU_ registros",
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando...",
                        "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "Último",
                            "sNext": "Siguiente",
                            "sPrevious": "Anterior"
                        }
                    },
                    "paging": true,
                    "ordering": true,
                    "info": true
                });

            $('#mytable3').DataTable(
                {
                    "language": {
                        "sLengthMenu": "Mostrar _MENU_ registros",
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando...",
                        "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "Último",
                            "sNext": "Siguiente",
                            "sPrevious": "Anterior"
                        }
                    },
                    "paging": true,
                    "ordering": true,
                    "info": true
                });

            $('#mytable4').DataTable(
                {
                    "language": {
                        "sLengthMenu": "Mostrar _MENU_ registros",
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando...",
                        "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "Último",
                            "sNext": "Siguiente",
                            "sPrevious": "Anterior"
                        }
                    },
                    "paging": true,
                    "ordering": true,
                    "info": true
                });

            $('#mytable5').DataTable(
                {
                    "language": {
                        "sLengthMenu": "Mostrar _MENU_ registros",
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando...",
                        "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "Último",
                            "sNext": "Siguiente",
                            "sPrevious": "Anterior"
                        }
                    },
                    "paging": true,
                    "ordering": true,
                    "info": true,
                    "pageLength": 25
                });

            $('#mytable6').DataTable(
                {
                    "language": {
                        "sLengthMenu": "Mostrar _MENU_ registros",
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando...",
                        "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "Último",
                            "sNext": "Siguiente",
                            "sPrevious": "Anterior"
                        }
                    },
                    "paging": true,
                    "ordering": true,
                    "info": true                    
                });

            $('#mytable7').DataTable(
                {
                    "language": {
                        "sLengthMenu": "Mostrar _MENU_ registros",
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando...",
                        "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "Último",
                            "sNext": "Siguiente",
                            "sPrevious": "Anterior"
                        }
                    },
                    "paging": true,
                    "ordering": true,
                    "info": true
                });

            $('#mytable8').DataTable(
                {
                    "language": {
                        "sLengthMenu": "Mostrar _MENU_ registros",
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando...",
                        "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "Último",
                            "sNext": "Siguiente",
                            "sPrevious": "Anterior"
                        }
                    },
                    "paging": true,
                    "ordering": true,
                    "info": true,
                    "pageLength": 25
                });

            $('#mytable9').DataTable(
                {
                    "language": {
                        "sLengthMenu": "Mostrar _MENU_ registros",
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando...",
                        "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "Último",
                            "sNext": "Siguiente",
                            "sPrevious": "Anterior"
                        }
                    },
                    "paging": true,
                    "ordering": true,
                    "info": true                    
                });

            $('#mytable10').DataTable(
                {
                    "language": {
                        "sLengthMenu": "Mostrar _MENU_ registros",
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando...",
                        "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "Último",
                            "sNext": "Siguiente",
                            "sPrevious": "Anterior"
                        }
                    },
                    "paging": true,
                    "ordering": true,
                    "info": true
                });

            </script>   
    </div>
    
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>--%>
        
<%--                       
                <div class="row">                    
                    <div  style="padding:10px;">
                        <div class="center">                
                            <h5  style="color: #558b2f  !important;">Empresa</h5>
                        </div>                          
                    </div>
                                <table id="mytableIcons" class="highlight striped bordered display compact cell-border row-border" cellspacing="0" width="100%">
                                    <asp:PlaceHolder ID="DBDataPlaceHolderIcons" runat="server"></asp:PlaceHolder>  
                                </table> 
                     </div>


                <br/>      
                <br/>      


                         <div class="row">                    
                    <div  style="padding:10px;">
                        <div class="center">                
                            <h6  style="color: #558b2f  !important;">Grupo</h6>
                        </div>                          
                    </div>
                                <table id="mytableGrupo" class="highlight striped bordered display compact cell-border row-border" cellspacing="0" width="100%">
                                    <asp:PlaceHolder ID="DBDataPlaceHolderGrupo" runat="server"></asp:PlaceHolder>  
                                </table> 
                     </div>


                <br/>      
                                     <div class="row">                    
                    <div  style="padding:10px;">
                        <div class="center">                
                            <h6  style="color: #558b2f  !important;">Clientes</h6>
                        </div>                          
                    </div>
                                <table id="mytableCliente" class="highlight striped bordered display compact cell-border row-border" cellspacing="0" width="100%">
                                    <asp:PlaceHolder ID="DBDataPlaceHolderCliente" runat="server"></asp:PlaceHolder>  
                                </table> 
                     </div>


                <br/>      
             <div class="row">                    
                    <div  style="padding:10px;">
                        <div class="center">                
                            <h6  style="color: #558b2f  !important;">Puntos de Carga</h6>
                        </div>                          
                    </div>
                                <table id="mytablePuntos" class="highlight striped bordered display compact cell-border row-border" cellspacing="0" width="100%">
                                    <asp:PlaceHolder ID="DBDataPlaceHolderPuntos" runat="server"></asp:PlaceHolder>  
                                </table> 
                     </div>

                <br/> --%>
    
</asp:Content>
