using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Ibd.SiMer.Datos
{
    public class ConnectionDB
    {

        private SqlDataAdapter myAdapter;
        private SqlConnection conn;
        DataTable dtExecSP;
        DataSet dtDS;

        /// <constructor>
        /// Initialise Connection
        /// </constructor>
        public SqlConnection dbConnection()
        {
            myAdapter = new SqlDataAdapter();
            conn = new SqlConnection(ConfigurationManager.AppSettings["appConnectionString"].ToString() );

            return conn;
        }


        /// <method>
        /// Open Database Connection if Closed or Broken
        /// </method>
        private SqlConnection openConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State ==
                        ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }

        /// <summary>
        /// If the connection os opened then close it
        /// </summary>
        /// <returns></returns>
        private SqlConnection CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return conn;
        }
        /// <method>
        /// Select Query
        /// </method>
        public DataTable executeSelectQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myCommand.ExecuteNonQuery();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                //clsError.logMessage = "Error - Connection.executeSelectQuery - Query: " + _query + " \nException: " + e.ToString();
                //clsError.LogWrite();

                return null;
            }
            finally
            {
                myCommand.Connection = CloseConnection();
            }
            return dataTable;
        }

        /// <method>
        /// Insert Query
        /// </method>
        public bool executeInsertQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.InsertCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                //clsError.logMessage = "Error - Connection.executeSelectQuery - Query: " + _query + " \nException: " + e.ToString();
                //clsError.LogWrite();
                return false;
            }
            finally
            {
                myCommand.Connection = CloseConnection();
            }
            return true;
        }

        /// <method>
        /// Update Query
        /// </method>
        public bool executeUpdateQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.UpdateCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                //clsError.logMessage = "Error - Connection.executeSelectQuery - Query: " + _query + " \nException: " + e.ToString();
                //clsError.LogWrite();
                return false;
            }
            finally
            {
                myCommand.Connection = CloseConnection();
            }
            return true;
        }

        public DataTable executeStoreProcedure(String _query, SqlParameter[] sqlParameter)
        {
            SqlDataReader drStoreProcedure = null;

            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                //''--alor de Timeout caducado. El período de tiempo de espera caducó antes de completar la operación o el servidor no responde."}
                myCommand.CommandTimeout = 660;
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myCommand.CommandType = CommandType.StoredProcedure;
                drStoreProcedure = myCommand.ExecuteReader();
                dtExecSP = new DataTable();
                dtExecSP.Load(drStoreProcedure);
            }
            catch (SqlException e)
            {
                //clsError.logMessage = "Error - Connection.executeSelectQuery - Query: " + _query + " \nException: " + e.ToString();
                //clsError.LogWrite();
                throw e;
            }
            finally
            {
                myCommand.Connection = CloseConnection();
            }

            return dtExecSP;
        }

        public DataSet executeStoreProcedureDS(String _query, SqlParameter[] sqlParameter)
        {
            //SqlDataReader drStoreProcedure = null;


            SqlCommand cmd = new SqlCommand();


            SqlCommand myCommand = new SqlCommand();
            try
            {                
                SqlDataAdapter da = new SqlDataAdapter();
                cmd = new SqlCommand();
                cmd.Connection = openConnection();
                cmd.CommandText = _query;
                cmd.Parameters.AddRange(sqlParameter);
                cmd.CommandType = CommandType.StoredProcedure;

                da = new SqlDataAdapter(cmd);
                dtDS = new DataSet();
                da.Fill(dtDS);
                return dtDS;
            }
            catch (SqlException e)
            {
                //clsError.logMessage = "Error - Connection.executeSelectQuery - Query: " + _query + " \nException: " + e.ToString();
                //clsError.LogWrite();
                throw e;
            }
            finally
            {
                myCommand.Connection = CloseConnection();
            }

           // return dtDS;
        }


    }

}
