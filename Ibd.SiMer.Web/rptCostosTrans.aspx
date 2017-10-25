<%@ Page Title="" Language="C#" MasterPageFile="~/costos.Master" AutoEventWireup="true" CodeBehind="rptCostosTrans.aspx.cs" Inherits="Ibd.SiMer.Web.rptCostosTrans" %>
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
        <h5 style="color: #427314 !important;">Reporte de Costos de Transmisión</h5> 
    </div>
        
    <br />
    
    <form id="form1" name="form1" runat="server">        
        <div class="row">                                               
            <div class="input-field col s1 offset-s2">
                <asp:DropDownList ID="ddl_year" runat="server" ClientIDMode="Static" >
                </asp:DropDownList >                    
                <label class="active" for="ddl_year">Año</label>
            </div>                    
            <div class="input-field col s1">  
                <asp:DropDownList ID="ddl_month" runat="server" ClientIDMode="Static" >
                </asp:DropDownList>
                <label class="active" for="ddl_month">Mes</label>
            </div>
            
            <div class="input-field col s2 text-right offset-s1">
                <asp:LinkButton ID="LinkButton2" name="LinkButton2"  CssClass="btn olive waves-effect waves-light light-green darken-3 right" runat="server" Text="Buscar"  OnClick="LinkButton2_Click"  ClientIDMode="Static"  >  
                    <i class="material-icons right">system_update_alt</i>Exportar
                </asp:LinkButton>                        
            </div>            
        </div>
        
        <div style="padding:30px;">            
            <div class="row">
                <div id="Div1"  runat="server">
                    <table id="mytable" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                        <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>
                    </table>
                </div>
            </div>
        </div>
        
    </form>
    	
<%--	
	<script src="http://netdna.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
	<script src="Scripts/jquery.aCollapTable.js"></script>
    
	<script>
        $(document).ready(function () {
            $('.collaptable').aCollapTable({
                startCollapsed: true,
                addColumn: false,
                plusButton: '<span class="i">+</span>',
                minusButton: '<span class="i">-</span>'
            });
        });
	</script>--%>

    <script type="text/javascript">

        $(document).ready(function () {

            $('select').addClass("browser-default");
            //$('select').material_select();
                                           
            var table = $('#mytable').DataTable({
                scrollY: "1000px",
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
