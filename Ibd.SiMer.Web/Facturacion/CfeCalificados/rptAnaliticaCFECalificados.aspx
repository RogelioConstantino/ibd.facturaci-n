<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="rptAnaliticaCFECalificados.aspx.cs" Inherits="Ibd.SiMer.Web.Facturacion.CfeCalificados.rptAnaliticaCFECalificados" %>
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
            <h5 style="color: #427314 !important;"> <i class="material-icons center">playlist_play</i> CFE Calificados</h5>            
        </div>         
        <br />                
        <form id="form1" name="form1" runat="server">
            <div class="row">
                    <div class="input-field col s1 offset-s4">
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
                        <asp:LinkButton ID="LinkButton1" name="lnkBuscar"  CssClass="btn olive waves-effect waves-light light-green darken-3 right" runat="server" Text="Buscar"  OnClick="lnkBuscar_Click" ClientIDMode="Static"  >  
                            <i class="material-icons right">search</i>Buscar
                        </asp:LinkButton>
                    </div>
                    <div class="input-field col s2 ">
                        <asp:LinkButton ID="lnkExportar" name="lnkExportar"  CssClass="btn olive waves-effect waves-light light-green darken-3 right" runat="server" Text="Exportar"  OnClick="lnkExportar_Click" ClientIDMode="Static"  >                                  
                            <i class="material-icons left">system_update_alt</i>Exportar
                        </asp:LinkButton>
                    </div>     
             </div>
            
            <br />
            <br />
            <br />

<%--            <div class="col s12 center">             
                <h6 style="color: #427314 !important;"> <i class="material-icons center">playlist_play</i> Totales</h6>            
            </div>         
            <div style="padding:20px;">            
                <div class="row">        
                    <div id="Div1"  runat="server">
                        <table id="mytable" class="highlight striped bordered  display compact cell-border"  cellspacing="0" >
                            <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> 
                </div>
            </div>--%>

            <div class="row">
                <div class="col s4 offset-s4 center">
                    <h5 style="color: #427314 !important;">Resumen</h5>
                </div>
                <div class="col s1"> 
                     <a class="btn-floating btn waves-effect waves-light  light-green darken-3 right"><i class="material-icons">system_update_alt</i></a>
                </div>
            </div>

            <div style="padding:20px;">
                <div class="row">        

                    <div class="col s5 offset-s4">
                        <div id="Div3"  runat="server">
                            <table id="mytable0" class="display compact"  cellspacing="0" >
                                <tr style="  border-top: none;    color: white;    background-color: rgb(115,166,22); text-align:center !important;">
                                    <th ><asp:Label  ID="lblAño" runat="server" > ago-2017</asp:Label></th>
                                    <th style="text-align: center;"><asp:Label  ID="Label1" runat="server"  >Cargo por Energía TBFin</asp:Label></th>
                                    <th style="text-align: center;"><asp:Label  ID="Label2" runat="server" >Cargo por Potencia Potencia</asp:Label></th>
                                </tr>
                                <tr>
                                    <th style="text-align: center;color:rgb(92,136,26)"><asp:Label  ID="Label3" runat="server" >USD</asp:Label></th>
                                    <td style="text-align: center;color:rgb(164,186,22)"><asp:Label  ID="energia_usd" runat="server" > $00,000.00 </asp:Label></td>
                                    <td style="text-align: center;color:rgb(164,186,22)"><asp:Label  ID="potencia_usd" runat="server" > $00,000.00 </asp:Label></td>                                
                                </tr>
                                <tr>
                                    <th style="text-align: center;color:rgb(92,136,26)"><asp:Label  ID="Label4" runat="server" >MXP</asp:Label></th>
                                    <td style="text-align: center;color:rgb(164,186,22)"><asp:Label  ID="energia_mxp" runat="server" > $00,000.00 </asp:Label></td>
                                    <td style="text-align: center;color:rgb(164,186,22)"><asp:Label  ID="potencia_mxp" runat="server" > $0,000.00 </asp:Label></td>
                                </tr>
                                <tr>
                                    <th style="text-align: center;color:rgb(92,136,26)"><asp:Label  ID="Label6" runat="server" >TC</asp:Label></th>
                                    <td style="text-align: center;color:rgb(164,186,22)"><asp:Label  ID="Label7" runat="server" > </asp:Label></td>
                                    <td style="text-align: center;color:rgb(164,186,22)"><asp:Label  ID="tc" runat="server" >00.0000 </asp:Label></td>
                                </tr>                                
                                <tr>
                                    <th style="text-align: center;color:rgb(92,136,26)"><asp:Label  ID="Label8" runat="server" > </asp:Label></th>
                                    <td style="text-align: center;color:rgb(255,144,83)"><asp:Label  ID="Label9" runat="server" >Fix del día hábil inmediato anterior publicado en DOF </asp:Label></td>
                                    <td style="text-align: center;color:rgb(255,144,83)"><asp:Label  ID="Label10" runat="server" >Promedio publicado en DOF</asp:Label></td>
                                </tr>
                                
                            </table>
                        </div> 
                    </div>

                </div>

              
                <div class="row">        
                    <div class="col s4 offset-s4"> 
                        <asp:Label  ID="Label11" runat="server"  style="text-align: left;color:rgb(255,144,83)"> Importes antes de IVA. </asp:Label>
                    </div>
                </div>    
       

            </div>



         

            <div class="row">
                <div class="col s4 offset-s4 center">
                    <h5 style="color: #427314 !important;">  Detalle </h5>
                </div>
                <div class="col s1"> 
                     <a class="btn-floating btn waves-effect waves-light  light-green darken-3 right"><i class="material-icons">system_update_alt</i></a>
                </div>
            </div>


            <div style="padding:20px;">            
                <div class="row">        
                    <div id="Div2"  runat="server">
                        <table id="mytable2" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="DBDataPlaceHolder2" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> 
                </div>
            </div>
            
        </form>

        <script type="text/javascript">
            $(document).ready(function () {
                $('select').addClass("browser-default");
                $('select').material_select();




                var table = $('#mytable0').DataTable({                    
                    scrollX: "300px",
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

                var table = $('#mytable').DataTable({
                       scrollY: "40px",
                       scrollX: true,
                       scrollCollapse: false,
                       paging: false,
                       sort: false,
                       "info": false,
                       "searching": false,
                       
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



                var table = $('#mytable2').DataTable({
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
