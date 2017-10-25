<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="rptAnalitica.aspx.cs" Inherits="Ibd.SiMer.Web.rptAnalitica" %>
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
            <h5 style="color: #427314 !important;">Detalle y Analítica de Facturación</h5>            
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
                    <div class="input-field col s2 text-right offset-s2">
                        <asp:LinkButton ID="LinkButton1" name="lnkBuscar"  CssClass="btn olive waves-effect waves-light light-green darken-3 right" runat="server" Text="Buscar"  OnClick="lnkBuscar_Click" ClientIDMode="Static"  >  
                            <i class="material-icons right">search</i>Buscar
                        </asp:LinkButton>                        
                    </div>
                    <div class="input-field col s2 ">
                        <asp:LinkButton ID="lnkExportar" name="lnkExportar"  CssClass="btn olive waves-effect waves-light light-green darken-3 right" runat="server" Text="Exportar"  OnClick="lnkExportar_Click" ClientIDMode="Static"  >                                  
                            <i class="material-icons left">system_update_alt</i>     Exportar
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

        <script type="text/javascript">
            $(document).ready(function () {
                $('select').addClass("browser-default");
                $('select').material_select();
                var table = $('#mytable').DataTable({
                       scrollY: "500px",
                       scrollX: true,
                       scrollCollapse: false,
                       paging: false,
                       sort: false,
                       "info": false,
                       "searching": false,
                       fixedColumns: {
                           leftColumns: 1
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
                   $('#contenedor select').addClass("browser-default");
                   $('#contenedor select').material_select();

               });
        </script>

</asp:Content>
