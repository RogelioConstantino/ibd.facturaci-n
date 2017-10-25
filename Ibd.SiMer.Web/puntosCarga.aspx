<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="puntosCarga.aspx.cs" Inherits="Ibd.SiMer.Web.puntosCarga" EnableEventValidation = "false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cuerpo" runat="server">
    <div class="container" id="contenedor">
        <form id="form1" name="form1" runat="server">                
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>

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
        
        <div class="col s12 center">                
            <h5 style="color: #427314 !important;">Módulo de Gestión de Puntos de Carga</h5>
        </div>
                   
            <div style="padding:20px;">            
	            <div class="row">        
                    <div id="Div1"  runat="server">
                        <table id="mytable" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> <!-- end table responsive -->
                 </div>
            </div>
        
        <!-- Modal Structure -->
        <div id="modal_Conf" class="modal" style="width:850px;">
            
            <div class="modal-content">                
                    <h5  class="light-green-text " >Configuración de Punto de Carga</h5>                 
                <div class="divider  light-green darken-3"></div>
                <br/>
                <div class="container" style="width:90%;">
                <div class="row">    
                      <div class="row">
                        <div class="input-field col s5">
                          <input placeholder="00.00" id="txt_porteo" type="text" readonly>
                          <label for="txt_porteo" class="active">Porteo Máximo	</label>
                        </div>
                        <div class="input-field col s5">
                          <input  placeholder="00.00" id="txt_demanda" type="text"  readonly>
                          <label for="txt_demanda" class="active">% Desc. Demanda</label>
                        </div>
                      </div>                      
                      <div class="row">
                        <div class="input-field col s3">
                          <input placeholder="00.00" id="txt_e_b" type="text"  readonly>
                          <label for="txt_e_b" class="active">% Desc. Energia Base</label>
                        </div>
                        <div class="input-field col s3">
                          <input  placeholder="00.00" id="txt_e_i" type="text" readonly>
                          <label for="txt_e_i" class="active">% Desc. Energia Intermedia</label>
                        </div>
                        <div class="input-field col s3">
                          <input  placeholder="00.00" id="txt_e_p" type="text"  readonly>
                          <label for="txt_e_p" class="active">% Desc. Energia Punta</label>
                        </div>
                      </div>  
                </div>
                    </div>
            </div>
            <div class="modal-body" id="edit_modal-body" ></div>
            <div class="modal-footer  light-green darken-3">                
                <a href="#!" class="modal-action modal-close waves-effect waves-green btn-flat white-text">Cerrar</a>
            </div>
        </div>

        <div id="modal_Edit" class="modal" style="width:950px;">
            <div class="modal-content">
                <h5  class="light-green-text " >Editar configuración de punto de carga</h5>                 
                <div class="divider  light-green darken-3"></div>
                <br/>
                <div class="container"  style="width:90%;">
                    <div class="row">    
                          <div class="row">
                            <div class="input-field col s6">                                                            
                                 <asp:DropDownList ID="ddl_Grupos" runat="server" ClientIDMode="Static" style="display:block !important;" >
                                 </asp:DropDownList>
                                 <cc1:CascadingDropDown ID="cdl_Grupos" TargetControlID="ddl_Grupos" PromptText="Seleciona un Grupo"  Enabled="True"
                                    PromptValue="" ServicePath="WebService/wsGrupos.asmx" ServiceMethod="getGrupos" runat="server"
                                    Category="Id" LoadingText="Cargando..." />  
                                <label for="ddl_Grupos" class="active">Grupo	</label>
                                
                            </div>
                            <div class="input-field col s6">
                                 <asp:DropDownList ID="ddl_Clientes" runat="server" ClientIDMode="Static"  Enabled="True" style="display:block !important;" >
                                 </asp:DropDownList>
                                 <cc1:CascadingDropDown ID="cdl_Clientes" TargetControlID="ddl_Clientes" PromptText="Seleciona un Cliente"
                                    PromptValue="" ServicePath="WebService/wsClientes.asmx" ServiceMethod="get" runat="server"
                                    Category="Id" ParentControlID="ddl_Grupos" LoadingText="Cargando..."  />                                
                              <label for="ddl_Clientes" class="active">Cliente</label>
                            </div>
                          </div>                      
                          <div class="row">
                            <div class="input-field col s12">
                              <input placeholder="Punto de carga" id="txt_punto" type="text"  />
                              <label for="txt_punto" class="active">Punto de Carga</label>
                            </div>
                          </div>
                          <div class="row">
                            <div class="input-field col s4">
                              <input placeholder="Código" id="txt_codigo" type="text"  />
                              <label for="txt_codigo" class="active">Código</label>
                            </div>
                            <div class="input-field col s4">
                              <input  placeholder="RPU" id="txt_rpu" type="text" />
                              <label for="txt_rpu" class="active">RPU</label>
                            </div>
                            <div class="input-field col s4">
                              <input  placeholder="RMU" id="txt_rmu" type="text"  />
                              <label for="txt_rmu" class="active">RMU</label>
                            </div>
                          </div>  
                    </div>

                    <div class="row">    
                          <div class="row">
                            <div class="input-field col s5">
                              <input placeholder="00.00" id="txt_porteo_edit" type="text" >
                              <label for="txt_porteo" class="active">Porteo Máximo	</label>
                            </div>
                            <div class="input-field col s5 offset-s2">
                              <input  placeholder="00.00" id="txt_demanda_edit" type="text"  >
                              <label for="txt_demanda" class="active">% Desc. Demanda</label>
                            </div>
                          </div>                      
                          <div class="row">
                            <div class="input-field col s4">
                              <input placeholder="00.00" id="txt_e_b_edit" type="text"  >
                              <label for="txt_e_b" class="active">% Desc. Energia Base</label>
                            </div>
                            <div class="input-field col s4  ">
                              <input  placeholder="00.00" id="txt_e_i_edit" type="text" >
                              <label for="txt_e_i" class="active">% Desc. Energia Intermedia</label>
                            </div>
                            <div class="input-field col s4  ">
                              <input  placeholder="00.00" id="txt_e_p_edit" type="text"  >
                              <label for="txt_e_p" class="active">% Desc. Energia Punta</label>
                            </div>
                          </div>  
                    </div>
                </div>
                
            </div>
            <div class="modal-body" id="edit_modal-body" ></div>
            <div class="modal-footer  light-green darken-3">  
                <a href="#!" class="modal-action modal-close waves-effect waves-green btn-flat  white-text">Guardar</a>
                <a href="#!" class="modal-action modal-close waves-effect waves-green btn-flat  white-text">Cerrar</a>
            </div>
        </div>

        </form>
        <script type="text/javascript">
            $(document).ready(function () {             
                
                $('.modal-trigger').leanModal({
                    dismissible: false, // If true, modal will be dismissed by clicking outside
                    opacity: .5, // Opacity of modal background
                    in_duration: 400
                });

                // the "href" attribute of .modal-trigger must specify the modal ID that wants to be triggered            

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
                          "paging": false,
                          
                          "ordering": true,
                          "info": true
                      });
            });


            function open_Modal_Conf(obj) {                                 

                $('#modal_Conf').openModal({
                    dismissible: false, // If true, modal will be dismissed by clicking outside
                    opacity: .5, // Opacity of modal background
                    in_duration: 300
                });

                //alert(obj) ;

                var columnValues    = obj.split("|");

                var modalBody = $('<div id="modalContent"></div>');
                var modalForm = $('<form role="form"></form>');


                $('#txt_porteo').val(columnValues[1]);
                $('#txt_demanda').val(columnValues[2]);
                $('#txt_e_b').val(columnValues[3]);
                $('#txt_e_i').val(columnValues[4]);
                $('#txt_e_p').val(columnValues[5]);
                              

            }

            function open_Modal_Edit(obj) {

                $('#modal_Edit').openModal({
                    dismissible: false, // If true, modal will be dismissed by clicking outside
                    opacity: .5, // Opacity of modal background
                    in_duration: 300
                });
                                                                                
                var parametros = JSON.stringify({ Id: obj});

                $.ajax({
                    type: "POST",
                    url: "puntosCarga.aspx/getById",
                    data: parametros,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json", async: true,
                    success: function (data) {
                        var reg = data.d;

                        //alert(reg[0].IdPuntoCarga);

                        /*
                            $("#txt_Grupo").val(reg[0].IdGrupo);
                            $("#txt_cliente").val(reg[0].IdCliente);
                        */
                        $("#ddl_Grupos").val(reg[0].IdGrupo);
                        //alert("1");
                        $("#ddl_Grupos").change();
                        $("ddl_Grupos").change();
                        //alert("2");
                        $("#ddl_Clientes").val(reg[0].IdCliente);

                        //CascadingDropDown



                        $("#txt_punto").val(reg[0].PuntoCarga);
                        $("#txt_codigo").val(reg[0].Codigo);
                        $("#txt_rpu").val(reg[0].RPU);
                        $("#txt_rmu").val(reg[0].RMU);

                        //alert(reg[0].PorteoMaximo.toString());

                        $("#txt_porteo_edit").val("" + reg[0].PorteoMaximo.toString());
                        $("#txt_demanda_edit").val(" " + reg[0].DemandaContratada.toString());
                        $("#txt_e_b_edit").val(reg[0].DescEnergiaBase.toString());
                        $("#txt_e_i_edit").val(reg[0].DescEnergiaIntermedia.toString());
                        $("#txt_e_p_edit").val(reg[0].DescEnergiaPunta.toString());
                        

                        /*for (var l = 0; l < anexos.length; l++) {
                            $("#tbl").find("#chk_" + anexos[l].IdPuntoCarga).closest('tr').children('td').eq(5).html('<a href="../' + anexos[l].Ruta + '" download>' + anexos[l].Codigo + '</a>');                            
                        }*/
                        //$('#divFacturar').unblock();

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("¡¡Ocurrio un error en la llamada asíncrona!!");
                        alert(errorThrown);

                        $('#divFacturar').unblock();
                    }
                });





            }

        </script>
    </div>
</asp:Content>
