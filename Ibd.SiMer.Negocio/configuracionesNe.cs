using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
//using Mercado.Class.ADO;
using System.Data.SqlClient;
using System.Text;
using Ibd.SiMer.Datos;

namespace Ibd.SiMer.Negocio
{
    public class configuracionesNe
    {
        DataTable dtData;
        DataSet dtSet;
        public DataTable getEmpresa()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Core]..[obtieneEmpresas]");
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@intEmpresa", SqlDbType.NVarChar);
                sqlParameters[0].Value = 0;
                               

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }


        public DataTable getGrupo()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Core]..[obtieneGrupos]");
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@intGrupo", SqlDbType.NVarChar);
                sqlParameters[0].Value = 0;


                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }

        
        public DataTable getCliente()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Core]..[obtieneClientes]");
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@intCliente", SqlDbType.NVarChar);
                sqlParameters[0].Value = 0;


                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }
        
        public DataTable getPuntosCarga()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Core]..[obtienePuntosCarga]");
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@iCodGrupo", SqlDbType.NVarChar);
                sqlParameters[0].Value = 0;

                sqlParameters[1] = new SqlParameter("@iCodRegionCFE", SqlDbType.NVarChar);
                sqlParameters[1].Value = 0;

                sqlParameters[2] = new SqlParameter("@iCodTipoCliente", SqlDbType.NVarChar);
                sqlParameters[2].Value = 0;

                sqlParameters[3] = new SqlParameter("@iCodTarifaCFE", SqlDbType.NVarChar);
                sqlParameters[3].Value = 0;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }

        public DataTable getContratos()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Core]..[obtieneContratos]");
                SqlParameter[] sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@iCodContrato", SqlDbType.NVarChar);
                sqlParameters[0].Value = 0;

                sqlParameters[1] = new SqlParameter("@iCodEmpresa", SqlDbType.NVarChar);
                sqlParameters[1].Value = 0;

                sqlParameters[2] = new SqlParameter("@iCodRegionCFE", SqlDbType.NVarChar);
                sqlParameters[2].Value = 0;

                sqlParameters[3] = new SqlParameter("@iCodTarifaCFE", SqlDbType.NVarChar);
                sqlParameters[3].Value = 0;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }


        public DataTable getProductos()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Core]..[obtieneProductos]");
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@iCodProducto", SqlDbType.NVarChar);
                sqlParameters[0].Value = 0;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }
        
        public DataTable getComportamientos()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Core]..[obtieneComportamientos]");
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@id", SqlDbType.NVarChar);
                sqlParameters[0].Value = 0;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }
                
        public DataTable getComportamientosContratos()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Core]..[obtieneComportamientoContratos]");
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@id", SqlDbType.NVarChar);
                sqlParameters[0].Value = 0;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }
                
        public DataTable getFactoresReduccion()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Core]..[obtieneFactoresReduccion]");
                SqlParameter[] sqlParameters = new SqlParameter[0];
                
                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }

        public DataTable getTarifas()
        {
            try
            {
                ConnectionDB con = new ConnectionDB();
                string query = string.Format("[IBD.Core]..[obtieneTarifas]");

                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@idRegion", SqlDbType.NVarChar);
                sqlParameters[0].Value = 0;

                sqlParameters[1] = new SqlParameter("@idTarifa", SqlDbType.NVarChar);
                sqlParameters[1].Value = 0;

                con.dbConnection();
                dtData = con.executeStoreProcedure(query, sqlParameters);
            }
            catch (Exception ex)
            {
                //LogError.LogErrorMedicion clsError = new LogError.LogErrorMedicion();
                //clsError.logMessage = ex.ToString();
                //clsError.logModule = "GetGeneralReport";
                //clsError.LogWrite();

            }
            return dtData;
        }
        public StringBuilder CreateTableHTML(DataTable dtGeneralReport)
        {
            StringBuilder html = new StringBuilder();
            try
            {
                html.Append(" <thead>");
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    html.Append("<th class='text-uppercase text -enter' >");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }

                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {
                        html.Append("<td>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
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
        
        public StringBuilder CreateTableHTML_Emp(DataTable dtGeneralReport)
        {
            StringBuilder html = new StringBuilder();
            try
            {
                html.Append(" <thead>");
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    if (    column.ColumnName == "Id") 
                        html.Append("<th class='text-uppercase text -enter' style='display:none;' >");
                    else
                        html.Append("<th class='text-uppercase text -enter' >");

                    html.Append(column.ColumnName);
                    html.Append("</th>");
                    
                }

                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {
                        if (column.ColumnName == "Id"      )
                            html.Append("<td style='display:none;' >");
                        else
                            html.Append("<td>");

                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
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

        public StringBuilder CreateTableHTML_Gpo(DataTable dtGeneralReport)
        {
            StringBuilder html = new StringBuilder();
            try
            {
                html.Append(" <thead>");
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    if (column.ColumnName == "iCodTarifaCFE"
                        || column.ColumnName == "iCodGrupo"
                        )
                        html.Append("<th class='text-uppercase text -enter' style='display:none;' >");
                    else
                        html.Append("<th class='text-uppercase text -enter' >");

                    html.Append(column.ColumnName);
                    html.Append("</th>");

                }

                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {
                        if (column.ColumnName == "iCodTarifaCFE"
                            || column.ColumnName == "iCodGrupo"
                            )
                            html.Append("<td style='display:none;' >");                        
                        else
                            html.Append("<td>");

                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
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
        
        public StringBuilder CreateTableHTML_PC(DataTable dtGeneralReport)
        {
            StringBuilder html = new StringBuilder();
            try
            {
                html.Append(" <thead>");
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    if (
                         column.ColumnName == "iCodCliente"
                        || column.ColumnName == "iCodGrupo"
                        || column.ColumnName == "iCodCliente"
                        || column.ColumnName == "iCodGrupo"
                        || column.ColumnName == "iCodRegionCFE"
                        || column.ColumnName == "iCodTarifaCFE"
                        || column.ColumnName == "iCodTipoCliente"
                        || column.ColumnName == "iDivCliente"
                        || column.ColumnName == "iSocCliente"
                        || column.ColumnName == "vchNumSociedad"
                        || column.ColumnName == "vchNumProveedor"
                        || column.ColumnName == "iNumVersion"
                        || column.ColumnName == "iCodConsumidor"
                        || column.ColumnName == "vchDirCliente"
                        || column.ColumnName == "vchPobCliente"
                        || column.ColumnName == "vchCalle"
                        || column.ColumnName == "vchNumeroExt"
                        || column.ColumnName == "vchNumeroInt"
                        || column.ColumnName == "vchColonia"
                        || column.ColumnName == "vchLocalidad"
                        || column.ColumnName == "iCodCiudad"
                        || column.ColumnName == "vchCP"
                        )
                        html.Append("<th class='text-uppercase text -enter' style='display:none;' >");
                    else
                        html.Append("<th class='text-uppercase text -enter' >");

                    html.Append(column.ColumnName);
                    html.Append("</th>");

                }

                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {
                        if (column.ColumnName == "iCodCliente"
                            || column.ColumnName == "iCodGrupo"
                            || column.ColumnName == "iCodCliente"
                            || column.ColumnName == "iCodGrupo"
                            || column.ColumnName == "iCodRegionCFE"
                            || column.ColumnName == "iCodTarifaCFE"
                            || column.ColumnName == "iCodTipoCliente"
                            || column.ColumnName == "iDivCliente"
                            || column.ColumnName == "iSocCliente"
                            || column.ColumnName == "vchNumSociedad"
                            || column.ColumnName == "vchNumProveedor"
                            || column.ColumnName == "iNumVersion"
                            || column.ColumnName == "iCodConsumidor"
                            || column.ColumnName == "vchDirCliente"
                            || column.ColumnName == "vchPobCliente"
                            || column.ColumnName == "vchCalle"
                            || column.ColumnName == "vchNumeroExt"
                            || column.ColumnName == "vchNumeroInt"
                            || column.ColumnName == "vchColonia"
                            || column.ColumnName == "vchLocalidad"
                            || column.ColumnName == "iCodCiudad"
                            || column.ColumnName == "vchCP"
                            )
                            html.Append("<td style='display:none;' >");
                        else
                            html.Append("<td>");

                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
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

        public StringBuilder CreateTableHTML_Cte(DataTable dtGeneralReport)
        {
            StringBuilder html = new StringBuilder();
            try
            {
                html.Append(" <thead>");
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    if (column.ColumnName == "iCodConsumidor"
                        || column.ColumnName == "iCodGrupo"
                        )
                        html.Append("<th class='text-uppercase text -enter' style='display:none;' >");
                    else
                        html.Append("<th class='text-uppercase text -enter' >");

                    html.Append(column.ColumnName);
                    html.Append("</th>");

                }

                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {                        
                        if (column.ColumnName == "iCodConsumidor"
                            || column.ColumnName == "iCodGrupo"
                            )
                            html.Append("<td style='display:none;' >");                        
                        else
                            html.Append("<td>");

                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
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

        public StringBuilder CreateTableHTML_Ctto(DataTable dtGeneralReport)
        {
            StringBuilder html = new StringBuilder();
            try
            {
                html.Append(" <thead>");
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    if (column.ColumnName == "iCodEmpresa"
                        || column.ColumnName == "iCodRegionCFE"
                        || column.ColumnName == "iCodTarifaCFE"
                            || column.ColumnName == "Base Contrato"
                            || column.ColumnName == "Base IES"
                            || column.ColumnName == "Base Indices"
                            || column.ColumnName == "Cod Empresa"
                        )
                        html.Append("<th class='text-uppercase text -enter' style='display:none;' >");
                    else
                        html.Append("<th class='text-uppercase text-enter' >");

                    html.Append(column.ColumnName);
                    html.Append("</th>");

                }

                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {
                        if (column.ColumnName == "iCodEmpresa"
                        || column.ColumnName == "iCodRegionCFE"
                        || column.ColumnName == "iCodTarifaCFE"
                            || column.ColumnName == "Base Contrato"
                            || column.ColumnName == "Base IES"
                            || column.ColumnName == "Base Indices"
                            || column.ColumnName == "Cod Empresa"
                            )
                            html.Append("<td style='display:none;' >");
                        else
                            html.Append("<td>");

                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
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
                
        public StringBuilder CreateTableHTML_Productos(DataTable dtGeneralReport)
        {
            StringBuilder html = new StringBuilder();
            try
            {
                html.Append(" <thead>");
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    
                        html.Append("<th class='text-uppercase text -enter' >");

                    html.Append(column.ColumnName);
                    html.Append("</th>");

                }

                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {
                    
                            html.Append("<td>");

                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
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
                
        public StringBuilder CreateTableHTML_Comportamientos(DataTable dtGeneralReport)
        {
            StringBuilder html = new StringBuilder();
            try
            {
                html.Append(" <thead>");
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    if (column.ColumnName == "IdProducto"
                        || column.ColumnName == "IdEmpresa"
                        || column.ColumnName == "vchCodEmpresa"
                        || column.ColumnName == "vchDesEmpresa"
                        )
                        html.Append("<th class='text-uppercase text -enter' style='display:none;' >");
                    else
                        html.Append("<th class='text-uppercase text -enter' >");

                    html.Append(column.ColumnName);
                    html.Append("</th>");

                }

                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {

                        if (column.ColumnName == "IdProducto"
                            || column.ColumnName == "IdEmpresa"
                            || column.ColumnName == "vchCodEmpresa"
                            || column.ColumnName == "vchDesEmpresa"
                            )
                            html.Append("<td class='text-uppercase text-enter' style='display:none;' >");
                        else
                            html.Append("<td>");

                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
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
                
        public StringBuilder CreateTableHTML_ComportamientosContratos(DataTable dtGeneralReport)
        {
            StringBuilder html = new StringBuilder();
            try
            {
                html.Append(" <thead>");
                html.Append("<tr>");
                foreach (DataColumn column in dtGeneralReport.Columns)
                {
                    if (column.ColumnName == "IdProducto"
                        || column.ColumnName == "IdEmpresa"
                        || column.ColumnName == "vchCodEmpresa"
                        || column.ColumnName == "vchDesEmpresa"
                        )
                        html.Append("<th class='text-uppercase text -enter' style='display:none;' >");
                    else
                        html.Append("<th class='text-uppercase text -enter' >");

                    html.Append(column.ColumnName);
                    html.Append("</th>");

                }

                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody id='myTable'> ");
                foreach (DataRow row in dtGeneralReport.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtGeneralReport.Columns)
                    {

                        if (column.ColumnName == "IdProducto"
                            || column.ColumnName == "IdEmpresa"
                            || column.ColumnName == "vchCodEmpresa"
                            || column.ColumnName == "vchDesEmpresa"
                            )
                            html.Append("<td class='text-uppercase text-enter' style='display:none;' >");
                        else
                            html.Append("<td>");

                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
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
