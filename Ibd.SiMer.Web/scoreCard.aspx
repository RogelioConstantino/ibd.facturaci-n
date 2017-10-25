
<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="scoreCard.aspx.cs" Inherits="Ibd.SiMer.Web.scoreCard" EnableEventValidation="false" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Cuerpo" runat="server">

    <style>

        .tabs .tab a{
            color:#000;
        } /*Black color to the text*/

        .tabs .tab a:hover {
            /*background-color:#eee;*/
            color:#000;
        } /*Text color on hover*/

        .tabs .tab a.active {
            /*background-color:#888;*/
            color:#000;
        } /*Background and text color when a tab is active*/

        .tabs .indicator {
            background-color:#33691e ;
            border-bottom-width:2px;
        }

    </style>

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
         
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        
         <br/>

                <div class="row">                    
                    <div  style="padding:10px;">
                        <div class="input-field col s1 offset-s9  ">
                            <asp:DropDownList ID="ddl_year" runat="server" ClientIDMode="Static" >
                            </asp:DropDownList >                    
                            <label class="active" for="ddl_year">Año</label>
                        </div>                    
                        <div class="input-field col s1">  
                            <asp:DropDownList ID="ddl_month" runat="server" ClientIDMode="Static"   >
                            </asp:DropDownList>
                            <label class="active" for="ddl_month">Mes</label>
                        </div>
                        <div class="input-field col s1">  
                                <asp:LinkButton ID="lnkActualizar" name="lnkActualizar"  CssClass="btn-floating  olive waves-effect waves-light light-green darken-3 right" OnClick="lnkActualizar_Click" runat="server" Text="Actualizar"  ClientIDMode="Static"  >  
                                    <i class="material-icons right">replay</i>                                        
                                </asp:LinkButton>   
                        </div>
                    </div>
                </div>

                <div class="row">                    
                    <div  style="padding:10px;">
                        <div class="center">                
                            <h5  style="color: #558b2f  !important;">Avance de carga por día</h5>
                        </div>                          
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">         
                          <ContentTemplate>                 
                                <table id="mytableIcons" class="highlight striped bordered display compact cell-border row-border" cellspacing="0" width="100%">
                                    <asp:PlaceHolder ID="DBDataPlaceHolderIcons" runat="server"></asp:PlaceHolder>  
                                </table> 
                          </ContentTemplate>                        
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_month" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <div class="row">
                    <div class="col s12 center">                
                        <h5  style="color: #558b2f   !important;">Totales de carga </h5>
                    </div>
                </div>

                <div class="row">
                    <div class="col s12">
                      <ul id="tabs-swipe-demo" class="tabs">
                        <li class="tab col s3"><a class="active" href="#test-swipe-1"><h6  style="color: #689f38  !important;  text-transform: none !important; ">[kWh]</h6></a></li>
                        <li class="tab col s3"><a href="#test-swipe-2"><h6  style="color: #689f38   !important;  text-transform: none !important; ">[kVArh]</h6></a></li>             
                      </ul>        
                    </div>

                      <div id="test-swipe-1" class="col s12 ">      
                            <div style="padding:20px;">            
                                <div class="row">
                                    <div class="col s6 center">                
                                        <h5  style="color: #558b2f  !important;"></h5>
                                    </div>                          
                                </div>
                                
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">         
                                      <ContentTemplate>
                                        <table id="mytableCargakwhe" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                                            <asp:PlaceHolder ID="DBDataPlaceHolderCargakwhe" runat="server"></asp:PlaceHolder>  
                                        </table>    
                                      </ContentTemplate>                        
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddl_month" />
                                    </Triggers>
                                </asp:UpdatePanel>    
                            </div>     
                      </div>

                      <div id="test-swipe-2" class="col s12 ">

                            <div style="padding:20px;">            
                                <div class="row">
                                    <%--<div class="col s6 center">                
                                        <h5  style="color: #A4BA08 !important;">Totales de carga [kvarh]</h5>
                                    </div>--%>                            
                                    <div class="col s6 center">                
                                        <h5  style="color: #558b2f  !important;"></h5>
                                    </div>                          
                                </div>                                
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">         
                                      <ContentTemplate>
                                            <table id="mytableCargakvarh" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                                                <asp:PlaceHolder ID="DBDataPlaceHolderCargakvarh" runat="server"></asp:PlaceHolder>  
                                            </table>   
                                      </ContentTemplate>                        
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddl_month" />
                                    </Triggers>
                                </asp:UpdatePanel>     
                            </div>        
                      </div>

                </div>
  
                <br/>      
                <br/>      

    </form>

    <script>

        $(document).ready(function () {

            $('ul.tabs').tabs();


            $('select').addClass("browser-default");
            $('select').material_select();

            $('#mytableIcons').DataTable(
                {
                    "language": {
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando..."
                    },
                    "paging": false,
                    "ordering": false,
                    "info": false,
                    "searching": false
                });


            $('#mytableCargakwhe').DataTable(
                {
                    "language": {
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando..."
                    },
                    "paging": false,
                    "ordering": false,
                    "info": false,
                    "searching": false
                });

            $('#mytableCargakvarh').DataTable(
                {
                    "language": {
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando..."
                    },
                    "paging": false,
                    "ordering": false,
                    "info": false,
                    "searching": false
                });
        

        });

        function tabla1() {
            $('#mytableIcons').DataTable(
                {
                    "language": {
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando..."
                    },
                    "paging": false,
                    "ordering": false,
                    "info": false,
                    "searching": false
            });
        }

        function tabla2() {
            $('#mytableCargakwhe').DataTable(
                {
                    "language": {
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando..."
                    },
                    "paging": false,
                    "ordering": false,
                    "info": false,
                    "searching": false
                });
        }

        function tabla3() {
            $('#mytableCargakvarh').DataTable(
                {
                    "language": {
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando..."
                    },
                    "paging": false,
                    "ordering": false,
                    "info": false,
                    "searching": false
                });
        }
    </script>
    
</asp:Content>
