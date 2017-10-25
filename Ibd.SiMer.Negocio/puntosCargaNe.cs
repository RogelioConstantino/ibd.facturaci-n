using System;
using System.Collections.Generic;
using Ibd.SiMer.Entidades;
using Ibd.SiMer.Datos;
using Ibd.SiMer.Negocio.Managers;
using Ibd.Framework;
using System.Data;
using System.Text;

namespace Ibd.SiMer.Negocio
{
    public class puntosCargaNe
    {
        public DataTable Consultar()
        {
            var oDa = new puntosCargaDa(Singleton<ConexionMng>.Single.Default());
            var Dt = oDa.Consultar();
            return Dt;
        }

        public StringBuilder CreateTableHTML(DataTable dtGeneralReport)
        {
            //Building an HTML string.
            StringBuilder html = new StringBuilder();
            try
            {

                html.Append(" <thead>");
                    html.Append("<tr>");
                        html.Append("<th colspan='5' >");
                    html.Append("</th>");                                     
                    html.Append("<th class='text-uppercase text-center' styel='text-align-last:center;'  colspan='2'>");                
                        html.Append("Acciones");
                    html.Append("</th>");
                html.Append("</tr>");

                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    if ( (column.ColumnName == "Grupo" || column.ColumnName == "IdGrupo" || column.ColumnName == "IdCliente" || column.ColumnName == "IdPuntoCarga")
                         ||  (column.ColumnName == "NoCuenta" || column.ColumnName == "PorteoMaximo" || column.ColumnName == "DemandaContratada")
                         || (column.ColumnName == "IdEstatus" || column.ColumnName == "IdZona" || column.ColumnName == "IdTarifa")
                         || (column.ColumnName == "FechaCreacion" || column.ColumnName == "FechaModificacion" || column.ColumnName == "Activo")
                         || (column.ColumnName == "Porteo Máximo" || column.ColumnName == "% Desc. Demanda" || column.ColumnName == "Demanda Contratada")
                         || (column.ColumnName == "% Desc. Energia Base" || column.ColumnName == "% Desc. Energia Intermedia" || column.ColumnName == "% Desc. Energia Punta")
                         )
                    {
                        html.Append("<th class='text-uppercase' style='display:none;'>");
                        html.Append(column.ColumnName);
                        html.Append("</th>");
                    }
                    else {
                        html.Append("<th class='text-uppercase text -enter' >");
                        html.Append(column.ColumnName);
                        html.Append("</th>");
                    }
                    
                }
                html.Append("<th class='text-uppercase text-center' styel='text-align:center; width:80px !impoortat;'  >");
                html.Append("");
                html.Append("</th>");
                html.Append("<th class='text-uppercase text-center' styel='text-align:center; width:60px !impoortat;'  >");
                html.Append("");
                html.Append("</th>");

                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {
                        if ((column.ColumnName == "Grupo" || column.ColumnName == "IdGrupo" || column.ColumnName == "IdCliente" || column.ColumnName == "IdPuntoCarga")
                                      || (column.ColumnName == "NoCuenta" || column.ColumnName == "PorteoMaximo" || column.ColumnName == "DemandaContratada")
                                      || (column.ColumnName == "IdEstatus" || column.ColumnName == "IdZona" || column.ColumnName == "IdTarifa")
                                      || (column.ColumnName == "FechaCreacion" || column.ColumnName == "FechaModificacion" || column.ColumnName == "Activo")
                                      || (column.ColumnName == "Porteo Máximo" || column.ColumnName == "% Desc. Demanda" || column.ColumnName == "Demanda Contratada" )
                                      || (column.ColumnName == "% Desc. Energia Base" || column.ColumnName == "% Desc. Energia Intermedia" || column.ColumnName == "% Desc. Energia Punta")
                                      )
                        {
                            html.Append("<td class='text-uppercase' style='display:none;'>");
                            html.Append(column.ColumnName);
                            html.Append("</td>");
                        }
                        else
                        {
                            html.Append("<td>");
                            html.Append(row[column.ColumnName]);
                            html.Append("</td>");
                        }
                    }
                    html.Append("<td style='width:80px;text-align:center;' onclick='open_Modal_Conf(\"" + row[0]  + "|" + row[15] + "|" + row[16] + "|" + row[17] + "|" + row[18] + "|" + row[19] + "|" + row[20] + "\");' ><a href='#' class=' light-green-text text-darken-4' ><i class='tiny material-icons'>settings</i></a></td>");
                    html.Append("<td style='width:80px;text-align:center;' onclick='open_Modal_Edit(\"" + row[0] + "\");' ><a href='#' class=' light-green-text text-darken-4' ><i class='tiny material-icons'>mode_edit</i></a></td>");                    
                    //html.Append("<td style='width:80px;text-align:center;'><a href='#' ><i class='tiny material-icons light-green-text text-darken-4'>delete</i></a></td>");
                    //html.Append("<td><a href='#' class='btn btn-danger btn-xs' data-toggle='modal' data-target='#delete' contenteditable='false'><span class='glyphicon glyphicon-trash'></span></a></td>");
                    html.Append("</tr>");
                }
                html.Append("</tbody>");
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "CreateTableHTML";
                //clsError.LogWrite();
            }

            return html;
        }

    }
}
