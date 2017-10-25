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
    public class horarioNe
    {
        public DataTable Consultar()
        {
            var oDa = new horarioDa(Singleton<ConexionMng>.Single.Default());
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
                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    if (column.ColumnName == "IdCliente"
                        || (column.ColumnName == "IdGrupo")
                         || (column.ColumnName == "IdEstatus")
                         || (column.ColumnName == "IdHorariosVerano")                         
                            || (column.ColumnName == "FechaEliminacion")
                         || (column.ColumnName == "FechaCreacion" || column.ColumnName == "FechaModificacion" || column.ColumnName == "Activo")
                         )
                    {
                        html.Append("<th class='text-uppercase' style='display:none;'>");
                        html.Append(column.ColumnName);
                        html.Append("</th>");
                    }
                    else
                    {
                        html.Append("<th class='text-uppercase text -enter' >");
                        html.Append(column.ColumnName);
                        html.Append("</th>");
                    }

                }
                //html.Append("<th class='text-uppercase text-center' styel='text-align:center;' colspan='2'>");
                //html.Append("Acciones");
                //html.Append("</th>");
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                //Building the Data rows.
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {

                        if ((column.ColumnName == "IdGrupo")
                            || column.ColumnName == "IdCliente"
                                      || (column.ColumnName == "IdEstatus")
                                      || (column.ColumnName == "IdHorariosVerano")
                                      || (column.ColumnName == "FechaEliminacion")
                                      || (column.ColumnName == "FechaCreacion" || column.ColumnName == "FechaModificacion" || column.ColumnName == "Activo")
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
                    //html.Append("<td style='width:80px;text-align:center;'><a href='#' class=' light-green-text text-darken-4' ><i class='tiny material-icons'>business</i></a></td>");
                    //html.Append("<td style='width:80px;text-align:center;'><a href='#' class=' light-green-text text-darken-4' ><i class='tiny material-icons'>mode_edit</i></a></td>");
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
