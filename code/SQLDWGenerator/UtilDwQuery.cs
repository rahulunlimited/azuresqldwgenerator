using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SQLDwGenerator
{

    /// <summary>
    /// Utility Class for general database queries
    /// </summary>
    static class UtilDwQuery
    {
        /// <summary>
        /// Returns the distinct values for any column from the database table
        /// This is needed on the BCP Split screen to get the distinct list of values for splitting the BCP file
        /// </summary>
        /// <param name="SchemaName"></param>
        /// <param name="TableName"></param>
        /// <param name="ColumnName"></param>
        /// <param name="AppConfig"></param>
        /// <returns></returns>
        public static DataTable GetColumnValues(string SchemaName, string TableName, string ColumnName, MySettingsEnvironments AppConfig)
        {
            DataTable dt = new DataTable();

            string connstr = AppConfig.GetConnectionString();
            SqlConnection conn = new SqlConnection(connstr);

            string sql = "SELECT DISTINCT [" + ColumnName + "] Col FROM [" + SchemaName + "].[" + TableName + "] ORDER BY Col";


            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandTimeout = 0;

                SqlDataReader rdr = comm.ExecuteReader();
                dt.Load(rdr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// private functionto to format the date column based on date types
        /// </summary>
        /// <param name="strColumnName"></param>
        /// <param name="strDataType"></param>
        /// <param name="TimePeriodType"></param>
        /// <returns></returns>
        private static string GetDateSQLString(string strColumnName, string strDataType, int TimePeriodType)
        {
            string strSQL;

            if (strDataType == Constants.DATA_TYPE_INT)
                strSQL = "CONVERT(DATE, CONVERT(VARCHAR, " + strColumnName + "), 112)";
            else
                strSQL = strColumnName;

            switch (TimePeriodType)
            {
                case Constants.DATE_TYPE_YEAR:
                    strSQL = "YEAR(" + strSQL + ")";
                    break;
                case Constants.DATE_TYPE_MONTH:
                    strSQL = "YEAR(" + strSQL + ")*100 + MONTH(" + strSQL + ")";
                    break;
                case Constants.DATE_TYPE_DAY:
                    strSQL = "FORMAT(" + strSQL + ", 'yyyy-MMM-dd')";
                    break;
            }

            return strSQL;


        }


        /// <summary>
        /// Get Distinct list of Date values from database based for any Date format. 
        /// This is needed on the BCP Split screen to get the distinct list of values for splitting the BCP file
        /// </summary>
        /// <param name="SchemaName"></param>
        /// <param name="TableName"></param>
        /// <param name="ColumnName"></param>
        /// <param name="DataType"></param>
        /// <param name="TimePeriodType"></param>
        /// <param name="AppConfig"></param>
        /// <returns></returns>
        public static DataTable GetColumnValuesWithDateFormat(string SchemaName, string TableName, string ColumnName, string DataType, int TimePeriodType, MySettingsEnvironments AppConfig)
        {
            DataTable dt = new DataTable();

            string connstr = AppConfig.GetConnectionString();
            SqlConnection conn = new SqlConnection(connstr);

            string strColumnFormatted = null;

            TableName = UtilGeneral.GetQuotedString(TableName);


            string sql = "WITH AllDates AS ( ";
            sql += "SELECT DISTINCT " + ColumnName + " FROM " + SchemaName + "." + TableName + " ";
            sql += ")";
            sql += "SELECT DISTINCT ";

            strColumnFormatted = GetDateSQLString(ColumnName, DataType, TimePeriodType);

            //In case there are invalid values, then use that value instead of formatting it
            if (DataType == Constants.DATA_TYPE_INT)
                strColumnFormatted = "CASE WHEN LEN(" + ColumnName + ") = 8 THEN " + strColumnFormatted + " ELSE CONVERT(VARCHAR, " + ColumnName + ") END ";

            strColumnFormatted = strColumnFormatted + " " + ColumnName;

            sql += strColumnFormatted;
            sql += " FROM AllDates ORDER BY " + ColumnName;


            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandTimeout = 0;
                SqlDataReader rdr = comm.ExecuteReader();
                dt.Load(rdr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

    }




}
