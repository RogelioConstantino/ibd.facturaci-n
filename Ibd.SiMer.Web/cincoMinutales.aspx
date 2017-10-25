<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="cincoMinutales.aspx.cs" Inherits="Ibd.SiMer.Web.cincoMinutales" %>
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

    <br />

    <div class="col s12 center">             
            <h5 style="color: #427314 !important;">Gestión de Cinco-Minutales</h5> 
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
                        
            
            <div class="col-sm-10">                    
                <asp:FileUpload ID="FileUpload1" runat="server"  />                     
            </div> 
            

    </div> 

    <div class="row">                             

            <div class="input-field col s2 text-right offset-s1">
                <asp:LinkButton ID="LinkButton1" name="lnkBuscar"  CssClass="btn olive waves-effect waves-light light-green darken-3 right" runat="server" Text="Buscar"   OnClientClick = "Confirm()"
                  OnClick="OnConfirm"   ClientIDMode="Static"  >  
                    <i class="material-icons right">search</i>
                    Buscar
                </asp:LinkButton>                        
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

    </form>
    
        <script type = "text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("¿Confirmar carga de archivo para la central selecionada?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
    </script>

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
