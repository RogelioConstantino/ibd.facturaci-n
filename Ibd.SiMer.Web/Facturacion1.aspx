<%@ Page Title="" Language="C#" MasterPageFile="~/Main2.Master" AutoEventWireup="true" CodeBehind="Facturacion1.aspx.cs" Inherits="Ibd.SiMer.Web.Facturacion1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Cuerpo" runat="server">
    
    <link rel="stylesheet" href="Content/materialize/css/materialize-stepper.min.css"/>
    <script src="Scripts/materialize/materialize-stepper.min.js"></script>

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
                         
        <div class="col s12 center">                
            <h5 style="color: #427314 !important;">Generación de Anexos </h5>
        </div>

        <form id="form1" name="form1" runat="server">

             <div class="row">       
                    
                        <asp:HiddenField ID="hdUsuario"   runat="server"  ClientIDMode="Static" />

                        <div class="input-field col s1">
                            <asp:DropDownList ID="ddl_year" runat="server" ClientIDMode="Static" >
                            </asp:DropDownList >                    
                            <label class="active" for="ddl_year">Año</label>
                        </div>
                    
                        <div class="input-field col s1">  
                            <asp:DropDownList ID="ddl_month" runat="server" ClientIDMode="Static" >
                            </asp:DropDownList>
                            <label class="active" for="ddl_month">Mes</label>
                        </div>

<%--                        <div class="input-field col s3">  
                            <asp:DropDownList ID="ddl_comportamiento" runat="server" ClientIDMode="Static" >
                            </asp:DropDownList>
                            <label class="active" for="ddl_comportamiento">Comportamiento</label>
                        </div>--%>

                        <%--<div class="input-field col s4">  
                            <asp:DropDownList ID="ddl_puntos" runat="server" ClientIDMode="Static" >
                            </asp:DropDownList>
                            <label class="active" for="ddl_month">Punto de Carga</label>
                        </div> --%>                   
                        
    <%--                <div class="input-field col s2  offset-s2 text-right">
                        <asp:LinkButton ID="lnkBuscar" name="lnkBuscar"  CssClass="btn   olive waves-effect waves-light light-green darken-3 right" runat="server" Text="Buscar"  OnClick="lnkBuscar_Click" ClientIDMode="Static"  >  
                            <i class="material-icons right Medium">pageview</i>
                            Vista Previa de Anexo
                        </asp:LinkButton>                        
                    </div> --%>                   

                 

              
                    <div class="input-field col s2 text-right   offset-s3"  >
                        <asp:LinkButton ID="LinkButton1" name="lnkBuscar"  CssClass="btn olive waves-effect waves-light light-green darken-3 right" runat="server" Text="Buscar"   ClientIDMode="Static"  OnClientClick="CargarTablas();" >  
                            <i class="material-icons right">search</i>
                            Buscar
                        </asp:LinkButton>                        
                    </div>
                   </div>

                    <div class="input-field col s2" style="display:none;">  
                        <asp:DropDownList ID="DropDownList2" runat="server" ClientIDMode="Static" style="display:none;" >
                        </asp:DropDownList>
                        <label class="active" for="DropDownList1" style="display:none;" >Grupo</label>
                    </div>

                    <div class="input-field col s3" style="display:none;">  
                        <asp:DropDownList ID="DropDownList1" runat="server" ClientIDMode="Static" style="display:none;"  >
                        </asp:DropDownList>
                        <label class="active" for="DropDownList1" style="display:none;" >Cliente</label>
                    </div>

                    <div class="input-field col s3" style="display:none;">  
                        <asp:DropDownList ID="ddl_puntos" runat="server" ClientIDMode="Static"  style="display:none;"  >
                        </asp:DropDownList>
                        <label class="active" for="ddl_month" style="display:none;" >Punto de Carga</label>
                    </div>

            <br />
                 <div class="row">
                    <div class="col s2   text-right offset-s10  center-align ">
                        <a id="btnIniciar" class="waves-effect  light-green darken-3  btn">
                             <i class="material-icons right">receipt</i>                             
                            Procesar</a>
                    </div>

             </div>

            <div class="row center" id="divFacturar">                                
                <div class="col s12 s10 ">
                    <table id="tbl" class="highlight striped bordered  display compact cell-border tbl" cellspacing="0" width="100%" align="center">
                        <thead>
                            <tr>
                                <%--<th style="width:190px !important;">Región</th>--%>
                                <th style="width:190px !important;">Grupo</th>
                                <th>Cliente</th>
                                <th style="width:190px !important;">Punto Carga</th>                                
                                <th  style="width:80px !important;"><input type="checkbox" id="myCheckbox" class="filled-in" /><label for="myCheckbox"></label>
                                    Generar</th>
                                <th style="width:100px !important;">Anexo</th>
                                <th style="width:100px !important;">PDF</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>                                
            </div>


<%--            <div style="padding:20px;">            
	            <div class="row">        
                    <div id="Div1"  runat="server">
                        <table id="mytable" class="highlight striped bordered  display compact cell-border"  cellspacing="0" width="100%">
                            <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>  
                        </table>
                    </div> <!-- end table responsive -->
                 </div>
            </div>--%>

        </form>
   

        <script type="text/javascript">

            $(document).ready(function () {

                $('select').addClass("browser-default");
                $('select').material_select();

                CargarTablas();


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

                    var valAnio = $('#ddl_year').val();
                    var valMes = $('#ddl_month').val();
                    var valTipo = "2"; //$('#ddl_comportamiento').val();


                    var parametros = JSON.stringify({ Anio: valAnio, Mes: valMes, Tipo: valTipo });

                    $.ajax({
                        type: "POST",
                        url: "facturacion.aspx/Consulta",
                        data: parametros,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json", async: true,
                        success: function (data) {

                            var registros = data.d;
                            $('#tbl tbody').empty(); //Limpiamos la tabla

                            for (var k = 0; k < registros.length; k++) {
                                forHtml = '<tr>';
                                //forHtml = '<tr><td>' + registros[k].Region + '</td>';
                                forHtml += '<td >' + registros[k].Grupo + '</td>';
                                forHtml += '<td >' + registros[k].Cliente + '</td>';
                                forHtml += '<td >' + registros[k].Codigo + '</td>';
                                forHtml += '<td ><p class="center-align"><input type="checkbox" id="chk_' + registros[k].IdPuntoCarga + '" class="filled-in" /><label for="chk_' + registros[k].IdPuntoCarga + '"></label></p></td>';
                                //forHtml += '<td > <a href="../AnexoMercado/Reportes/' + registros[k].Ruta + '" download>' + registros[k].Ruta + '</a></td></tr>';

                                if (registros[k].Ruta != "")
                                    forHtml += '<td > <a href="../AnexoMercado/Reportes/' + registros[k].Ruta + '" download>xls</a></td>';
                                else
                                    forHtml += '<td > &nbsp; </td>';

                                if (registros[k].RutaPDF != "")
                                    forHtml += '<td > <a href="../AnexoMercado/Reportes/' + registros[k].RutaPDF + '" download>pdf</a></td></tr>';
                                else
                                    forHtml += '<td > &nbsp; </td></tr>';

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


                $('#btnIniciar').click(function () {

                    var valAnio = $('#ddl_year').val();
                    var valMes = $('#ddl_month').val();
                    var valUsr = $('#hdUsuario').val();
                    var valTipo = "2";   //$('#ddl_comportamiento').val();

                    if (valAnio == "" || valMes == "") {
                        return;
                    }

                    if (valMes == "10" || valMes == "11" || valMes == "12")
                        valMes = valAnio + valMes;
                    else
                        valMes = valAnio + "0" + valMes;

                    var selectedItems = "";
                    $("#tbl input[type=checkbox]:checked").each(function () {
                        if ($(this).attr("id") != "myCheckbox") {
                            if (selectedItems == "") {
                                selectedItems += $(this).attr("id").replace("chk_", "");
                            }
                            else {
                                selectedItems += ',' + $(this).attr("id").replace("chk_", "");
                            }
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

                        var valMinutales = "0";
                        //var valMes = $("#slcMes option:selected").val();
                        var parametros = JSON.stringify({ Ids: selectedItems, Mes: valMes, Usuario: valUsr, Tipo: valTipo, Minutales: valMinutales });

                        $.ajax({
                            type: "POST",
                            url: "Facturacion.aspx/GenerarAnexos",
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
                                    $("#tbl").find("#chk_" + anexos[l].IdPuntoCarga).closest('tr').children('td').eq(5).html('<a href="../' + anexos[l].Ruta + '" download>' + anexos[l].Codigo + '</a>');
                                    //$row.children('td').eq(6).html('<a href="http://www.example.com/index.html" download>' + anexos[l].IdPuntoCarg+'</a>');

                                }

                                $('#divFacturar').unblock();
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                //alert("¡¡Ocurrio un error en la llamada asíncrona!!");
                                //alert(errorThrown);

                                $('#divFacturar').unblock();
                            }
                        });
                    }




                });



                $("#myCheckbox").click(function () {
                    //$(".checks").prop('checked', $(this).prop('checked'));
                    var c = this.checked;
                    $(':checkbox').prop('checked', c);

                });





                //var table = $('#mytable').DataTable({
                //    scrollY: "500px",
                //    scrollX: true,
                //    scrollCollapse: false,
                //    paging: false,
                //    sort: false,
                //    "info": false,
                //    "searching": false,
                //    fixedColumns: {
                //        leftColumns: 2
                //    },
                //    "language": {
                //        "sLengthMenu": "Mostrar _MENU_ registros",
                //        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                //        "sZeroRecords": "No se encontraron resultados",
                //        "sEmptyTable": "Ningún dato disponible",
                //        "sInfoEmpty": "",
                //        "sSearch": "Buscar:",
                //        "sUrl": "",
                //        "sInfoThousands": ",",
                //        "sLoadingRecords": "Cargando...",
                //        "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                //        "oPaginate": {
                //            "sFirst": "Primero",
                //            "sLast": "Último",
                //            "sNext": "Siguiente",
                //            "sPrevious": "Anterior"
                //        }
                //    }

                //});


            });

        </script>

</div>
</asp:Content>
