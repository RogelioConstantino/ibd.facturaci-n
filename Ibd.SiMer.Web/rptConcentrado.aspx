<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="rptConcentrado.aspx.cs" Inherits="Ibd.SiMer.Web.rptConcentrado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cuerpo" runat="server">

    

        <style type="text/css">
            .picker__date-display {
              background-color:#558b2f ;
            }
            .picker__weekday-display {
              background-color:#33691e  ;
            }
            .picker__day--selected, .picker__day--selected:hover, .picker--focused .picker__day--selected {
              background-color:#558b2f ;
            }
            .picker__close, .picker__today {
                color:#558b2f ;
            }
            .picker__day.picker__day--today {
                
                color:#558b2f ;
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

      <%--  <ul class="arrows">
            <li><a href="#">Reportes</a></li>
            <li><a href="#">Concentrado por Mes</a></li>
        </ul>--%>
    
         <br />

        <div class="col s12 center">             
                <h5 style="color: #427314 !important;">Concentrado por Mes</h5>            
        </div>
        
        <br />
                
        <form id="form1" name="form1" runat="server">

            <div class="row">                             
                    <div class="input-field col s1 offset-s1">
                        <input type="date" class="datepicker" id="fecIni" runat="server"  ClientIDMode="Static" />
                        <label class="active">Del</label>
                    </div>
                    <div class="input-field col s1">                        
                        <input type="date" class="datepicker" id="fecFin" runat="server" ClientIDMode="Static" />
                        <label class="active" >al</label> 
                    </div>
                    <div class="input-field col s3">  
                        <asp:DropDownList ID="ddl_puntos" runat="server" ClientIDMode="Static" >
                        </asp:DropDownList>
                        <label class="active" for="ddl_month">Punto de Carga</label>
                    </div>
            
                    <div class="input-field col s2 text-right offset-s1">
                        <asp:LinkButton ID="LinkButton1" name="lnkBuscar"  CssClass="btn olive waves-effect waves-light light-green darken-3 right" runat="server" Text="Buscar"  OnClick="lnkBuscar_Click" ClientIDMode="Static"  >  
                            <i class="material-icons right">search</i>
                            Buscar
                        </asp:LinkButton>                        
                    </div>
                    <div class="input-field col s2 ">
                        <a class="btn olive waves-effect waves-light light-green darken-3 right modal-trigger" href="#modal1">
                            <i class="material-icons left">system_update_alt</i>Exportar ...
                        </a>                        
                    </div>     
             </div>
            <div style="padding:20px;">            
                <div class="row">        
                    <div id="Div1"  runat="server">
                        <table id="mytable" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> 
                </div>
            </div>

            <!-- Modal Structure -->
            <div id="modal1" class="modal " style="    height: 330px !important;">
                <div class="modal-content">              
                    <h4 class="light-green-text text-darken-4">Exportar Concentrado por Mes</h4>
                    <p>Exportar en un archivo de excel el concentrado de todos los puntos de carga en el mes.</p>              
                    <div class="row">
                        <div class="input-field col s6">
                            <asp:DropDownList ID="ddl_year" runat="server" ClientIDMode="Static" >
                            </asp:DropDownList >                    
                            <label class="active" for="ddl_year">Año</label>
                        </div>
                    
                        <div class="input-field col s6">  
                            <asp:DropDownList ID="ddl_month" runat="server" ClientIDMode="Static" >
                            </asp:DropDownList>
                            <label class="active" for="ddl_month">Mes</label>
                        </div>
                    </div>                                  
                </div>
                <div class="modal-footer">
                    <div class="container">
                        <div class="row">
                            <div class="col l2 offset-s6 s4 " style="height:50px !important;">
                                <asp:LinkButton ID="lnkExportar" name="lnkExportar"  CssClass="btn-large olive waves-effect waves-light light-green darken-3 right" runat="server" Text="Exportar"  OnClick="lnkExportar_Click" ClientIDMode="Static"  >                                  
                                        Exportar
                                </asp:LinkButton>   
                            </div>               
                            <div class="col l2 s4"  style="height:50px !important;">
                                <asp:LinkButton ID="LinkButton2" name="lnkExportar"  CssClass="modal-action modal-close btn-large olive waves-effect waves-light light-green darken-3 right" runat="server" Text="CErrar"   ClientIDMode="Static"  >  
                                    Cerrar
                                </asp:LinkButton>            
                            </div>                           
                        </div>
                    </div>
                </div>
        
            </div>

        </form>

        <script type="text/javascript">

            $(document).ready(function () {

                $('select').addClass("browser-default");
                $('select').material_select();


                $('.datepicker').pickadate({
                    selectMonths: true, // Creates a dropdown to control month
                    selectYears: 15 // Creates a dropdown of 15 years to control year
                     , labelMonthNext: 'Siguiente'
                     , labelMonthPrev: 'Anterior'
                     , labelMonthSelect: 'Selecciona un mes'
                     , labelYearSelect: 'Selecciona un año'
                     , monthsFull: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre']
                     , monthsShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
                     , weekdaysFull: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado']
                     , weekdaysShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab']
                     , weekdaysLetter: ['D', 'L', 'M', 'X', 'J', 'V', 'S']
                     , today: 'Hoy'
                     , clear: 'Limpiar'
                     , close: 'Cerrar'
                     , closeOnSelect: true
                     , format: 'yyyy-mm-dd'
                });


                // Return today's date and time
                var currentTime = new Date()

                // returns the month (from 0 to 11)
                var month = currentTime.getMonth() + 1
                //alert(month);
                // returns the day of the month (from 1 to 31)
                var day = currentTime.getDate()

                // returns the year (four digits)
                var year = currentTime.getFullYear()
                //alert(year);

                // write output MM/dd/yyyy
                //document.write(month + "/" + day + "/" + year)


                //alert(month.len);

                if (day < 10) day = '0' + day;
                if (month < 10) month = '0' + month;


                if ($("#fecIni").val() == "") {
                    var $input = $('#fecIni').pickadate()
                    var picker = $input.pickadate('picker')
                    //'picker.set('select', year + '-' + month + '-' + '01', { format: 'yyyy-mm-dd' })
                    picker.set('select', year + '-' + month + '-' + day, { format: 'yyyy-mm-dd' })
                }
                if ($("#fecFin").val() == "") {
                    var $input2 = $('#fecFin').pickadate()
                    var picker2 = $input2.pickadate('picker')
                    picker2.set('select', year + '-' + month + '-' + day, { format: 'yyyy-mm-dd' })
                }

                   $('#contenedor select').addClass("browser-default");
                   $('#contenedor select').material_select();

                   $('.modal-trigger').leanModal();

                   var table = $('#mytable').DataTable({
                       scrollY: "500px",
                       scrollX: true,
                       scrollCollapse: false,
                       paging: false,
                       sort: false,
                       "info": false,
                       "searching": false,
                       fixedColumns: {
                           leftColumns: 2
                       },
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
                       }                      
                       
                   });





               });

        </script>

    

</asp:Content>
