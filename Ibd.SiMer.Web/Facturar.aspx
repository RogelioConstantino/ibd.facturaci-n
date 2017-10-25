<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Facturar.aspx.cs" Inherits="Ibd.SiMer.Web.Facturar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cuerpo" runat="server">
    <link rel="stylesheet" href="Content/materialize/css/materialize-stepper.min.css"/>
    <script src="Scripts/materialize/materialize-stepper.min.js"></script>
    <br />


    <style type="text/css">

        
/*.tbl td, th { padding: 5px 5px ;}*/



        /*
            table.mytable { border-collapse: collapse; border: 1px solid #839E99; 
            background: #f1f8ee; font: .9em/1.2em Georgia, "Times New Roman", Times, serif; color: #033; }
            .mytable caption { font-size: 1.3em; font-weight: bold; text-align: left; padding: 1em 4px; }
            .mytable td, th { padding: 3px 3px .75em 3px; line-height: 1.3em; }
            .mytable th { background: #839E99; color: #fff; font-weight: bold; text-align: left; padding-right: .5em; vertical-align: top; }
            .mytable thead th { background: #2C5755; text-align: center; }
            .mytable .odd td { background: #DBE6DD; }
            .mytable .odd th { background: #6E8D88; }
            .mytable td a, td a:link { color: #325C91; }
            .mytable td a:visited { color: #466C8E; }
            .mytable td a:hover, td a:focus { color: #1E4C94; }
            .mytable th a, td a:active { color: #fff; }
            .mytable tfoot th, tfoot td { background: #2C5755; color: #fff; }
            .mytable th + td { padding-left: .5em; }
        }*/
    </style>

    <div class="container" id="contenedor">

        
        <div class="col s12 center">                
            <h5 style="color: #427314 !important;">Módulo de Facturación</h5>
        </div>

        <div class="row">


            <form class="col s12 m12" id="frmPeriodo" name="frmPeriodo">

                <div class="col m12 center-align">
                   

                    <div class="col s12 m6 left-align">
                        <label id="lblAnio"></label>
                        <div class="input-field col s12">
                            <select id="slcAnio" name="slcAnio">
                                <option value="" disabled selected>Selecione Año:</option>
                                <option value="2017">2017</option>
                            </select>
                            <label class="active" >Año:</label>
                        </div>

                    </div>
                    <div class="col s12 m6 left-align">
                        <label id="lblMes"></label>
                        <div class="input-field col s12">
                            <select id="slcMes">
                                <option value="" disabled selected>Selecione Mes:</option>
                                <option value="201703">Marzo</option>
                                <option value="201704">Abril</option>
                                <option value="201705">Mayo</option>
                                <option value="201706">Junio</option>
                                <option value="201707">Julio</option>
                                <option value="201708">Agosto</option>
                                <option value="201709">Septiembre</option>
                                <option value="201710">Octubre</option>
                                <option value="201711">Noviembre</option>
                                <option value="201712">Diciembre</option>
                            </select>
                            <label  class="active" >Mes:</label>
                        </div>
                    </div>

                </div>
            </form>
        </div>

        <div class="row">
            <div class="col s12 center-align">
                <a id="btnIniciar" class="waves-effect  light-green darken-3  btn">  Iniciar Facturación</a>
            </div>
        </div>

        <div id="Wizard" class="modal"  ">
            <div class="modal-content" style="padding:15px !important;">
                <h4 id="mesfacturacion" class="light-green-text " >Materialize Modal</h4>
                <div class="divider  light-green darken-3"></div>
                <br />
                <ul class="stepper horizontal"  style="margin-top:0px !important;"   >
                    <li class="step active">
                        <div class="step-title waves-effect">Configuraciones</div>
                        <div class="step-content">
                            <div class="row">
                                <div class="input-field col s12">
                                    <label for="first_name">Validación de Parametros y configuraciones</label>                                    
                                </div>
                            </div>
                            <div class="step-actions">
                                <button class="waves-effect waves-dark btn next-step">Validar</button>
                            </div>
                        </div>
                    </li>
                    <li class="step">
                        <div class="step-title waves-effect">Cargas</div>
                        <div class="step-content">
                            <div class="row">
                                <div class="input-field col s12">
                                    <label for="first_name">Validación de Cargas de archivos</label>
                                    
                                </div>
                            </div>
                            <div class="step-actions">
                                <button class="waves-effect waves-dark btn next-step">Validar</button>
                                <%--<button class="waves-effect waves-dark btn-flat previous-step">BACK</button>--%>
                            </div>
                        </div>
                    </li>

                    <li class="step">
                        <div class="step-title waves-effect">Facturar</div>
                        <div class="step-content">
                            <div class="row center" id="divFacturar">                                
                                <div class="col s12 s10 ">
                                    <table id="tbl" class="highlight striped bordered  display compact cell-border tbl" cellspacing="0" width="100%" align="center">
                                        <thead>
                                            <tr>
                                                <th>Punto Carga</th>
                                                <th>Porteo Máximo</th>
                                                <th>Demanda</th>
                                                <th>Tarifa</th>
                                                <th>Región</th>
                                                <th><input type="checkbox" id="myCheckbox" class="filled-in" /><label for="myCheckbox"></label>
                                                    Generar</th>
                                                <th>Anexo</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>                                
                            </div>
                        </div>
                    </li>
                    <li class="step">
                        <div class="step-title waves-effect">Resumen</div>
                        <div class="step-content">
                            Resumen todal de lo facturado!
                            Montos
                            Grupos
                            Clientes
                         <div class="step-actions">
                             <button class="waves-effect waves-dark btn" type="submit">Facturar</button>
                         </div>
                        </div>
                    </li>
                </ul>
                                
            </div>
            <div class="modal-footer light-green darken-3">                        
                <a href="#!" class=" modal-action modal-close btn-flat white-text">Cerrar</a>
                <a id="btnAnexos" class=" btn-flat  white-text">Generar Anexos</a>

                <a id="btnSig" class="modal-action   btn-flat  white-text"><i class="medium material-icons right">skip_next</i>Siguiente</a>
                <a id="btnPre" class="modal-action   btn-flat  white-text"><i class="small material-icons left">skip_previous</i>Previo</a>
                
                </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {

            $('select').addClass("browser-default");
            $('select').material_select();

            // the "href" attribute of .modal-trigger must specify the modal ID that   wants to be triggered
            //$('#tbl').DataTable();
            $('.modal-trigger').leanModal({
                dismissible: false, // If true, modal will be dismissed by clicking outside
                opacity: .5, // Opacity of modal background
                in_duration: 400
            });

            $('#btnIniciar').click(function () {

                var valAnio = $("#slcAnio option:selected").val();
                var valMes = $("#slcMes option:selected").val();

                $('#lblAnio').text('');
                $('#lblMes').text('');
                if (valAnio == "" || valMes == "")
                {
                    if (valAnio == "") {
                        $('#lblAnio').text('Seleccione año');
                    }
                    if (valMes == "") {
                        $('#lblMes').text('Seleccione mes');
                    }
                    return;
                }

                $('#mesfacturacion').text('Facturación - ' + $("#slcMes option:selected").text() + ' ' + $("#slcAnio option:selected").text());

                $('#Wizard').openModal({
                    dismissible: false, // If true, modal will be dismissed by clicking outside
                    opacity: .5, // Opacity of modal background
                    in_duration: 300
                });
            
               CargarTablas();

                //var val = $("#mesfacturacion option:selected").val();
            });

            // Bonos que enviará los anexos seleccionados
            $('#btnAnexos').click(function () {





                var selectedItems = "";
                $("#tbl input[type=checkbox]:checked").each(function () {
                    if (selectedItems == "") {
                        selectedItems += $(this).attr("id").replace("chk_", "");
                    }
                    else {
                        selectedItems += ','+$(this).attr("id").replace("chk_", "");
                    }
                });

                if (selectedItems != "") {


                    //Se bloquea el div mientras se hace la llamada asíncrona
                    $('#divFacturar').block({
                        message: '  <div class="preloader-wrapper small active"><div class="spinner-layer spinner-green-only"><div class="circle-clipper left"><div class="circle"></div></div><div class="gap-patch"><div class="circle"></div></div><div class="circle-clipper right"><div class="circle"></div></div></div></div>',
                        overlayCSS: { opacity: 0.3 },
                        css: {
                            width: '150px'
                        }
                    });


                    var valMes = $("#slcMes option:selected").val();
                    var parametros = JSON.stringify({ Ids: selectedItems, Mes: valMes });

                    $.ajax({
                        type: "POST",
                        url: "Facturar.aspx/GenerarAnexos",
                        data: parametros,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json", async: true,
                        success: function (data) {

                            var anexos = data.d;
                            //alert(anexos);


                            //var sarchivo = anexos[l].Ruta;
                            //var archivo = sarchivo.split("/");

                            //var archivoDowload = archivo[7];

                            //alert(archivoDowload);

                            //var path_archivoDowload = "AnexoMercado/Reportes/" + archivoDowload;
                            //alert(path_archivoDowload);

                            for (var l = 0; l < anexos.length; l++) {
                                $("#tbl").find("#chk_" + anexos[l].IdPuntoCarga).closest('tr').children('td').eq(6).html('<a href="../' + anexos[l].Ruta + '" download>' + anexos[l].Codigo + '</a>');
                                //$row.children('td').eq(6).html('<a href="http://www.example.com/index.html" download>' + anexos[l].IdPuntoCarg+'</a>');

                            }


                            $('#divFacturar').unblock();
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("¡¡Ocurrio un error en la llamada asíncrona!!");
                            alert(errorThrown);

                            $('#divFacturar').unblock();
                        }
                    });
                }


                

            });

            $('.stepper').activateStepper();

            function CargarTablas() {
                //Se bloquea el div mientras se hace la llamada asíncrona
                $('#divFacturar').block({
                    message: '<table><tr><td><img alt="" src="../Recursos/loadinfo.gif" /></td><td>&nbsp;Por favor espere...</td></tr></table>',
                    overlayCSS: { opacity: 0.3 },
                    css: {
                        width: '150px'
                    }
                });

                var forHtml;

                $.ajax({
                    type: "POST",
                    url: "Facturar.aspx/Consulta",
                    //data: parametros,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json", async: true,
                    success: function (data) {

                        var registros = data.d;
                        $('#tbl tbody').empty(); //Limpiamos la tabla

                        for (var k = 0; k < registros.length; k++) {
                            forHtml = '<tr><td>' + registros[k].Codigo + '</td>';
                            forHtml += '<td>' + registros[k].PorteoMaximo + '</td>';
                            forHtml += '<td>' + registros[k].DemandaContratada + '</td>';
                            forHtml += '<td>' + registros[k].Tarifa + '</td>';
                            forHtml += '<td>' + registros[k].Region + '</td>';
                            forHtml += '<td><p class="center-align"><input type="checkbox" id="chk_' + registros[k].IdPuntoCarga + '" class="filled-in" /><label for="chk_' + registros[k].IdPuntoCarga + '"></label></p></td>';
                            forHtml += '<td></td></tr>';
                            $(forHtml).hide().appendTo('#tbl tbody').fadeIn();
                        }

                        $('#divFacturar').unblock();
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("¡¡Ocurrio un error en la llamada asíncrona!!");
                        $('#divFacturar').unblock();
                    }
                });
            }

            $('#tbl').DataTable(
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
                 "ordering": false,
                 "info": false,
                 "searching": false
             });


            $("#myCheckbox").click(function () {
                $(".checks").prop('checked', $(this).prop('checked'));
            });



        });

    </script>
</asp:Content>
