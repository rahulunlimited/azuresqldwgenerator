using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace SQLDwGenerator
{
    /// <summary>
    /// The class manages the main list of Tables with important information like number of rows, size etc.
    /// </summary>
    public class MyTableList
    {
        DataTable TableList;

        public const string TABLE_ID = "TableID";
        public const string SCHEMA_NAME = "SchemaName";
        public const string TABLE_NAME = "TableName";
        public const string TOTAL_ROWS = "TotalRows";
        public const string DATA_SIZE_MB = "DataSizeMB";
        public const string DISTRIBUTION_TYPE = "DistributionType";
        public const string DISTRIBUTION_COLUMN = "DistributionColumn";
        public const string INDEX_TYPE = "IndexType";
        public const string INDEX_COLUMN = "IndexColumn";
        public const string BCP_SPLIT = "TableSplit";
        public const string BCP_SPLIT_COLUMN = "SplitColumn";
        public const string BCP_SPLIT_VALUES = "SplitValues";
        public const string BCP_SPLIT_VALUE_TYPE = "ValueType";
        public const string REPLACE_CRLF = "ReplaceNewLine";
        public const string SELECT = "SelectTable";

        public const string NAME = "SQLDwTable";

        /// <summary>
        /// Creates a table list and refreshes values from database
        /// </summary>
        /// <param name="SQLDwConfig">ApplicationConfiguration to get the database connection</param>
        public MyTableList(MySettingsEnvironments SQLDwConfig)
        {
            TableList = new DataTable();
            UpdateTableName();

            AddColumns();

            RefreshTableListFromDB(SQLDwConfig);
        }


        /// <summary>
        /// Creates a table list. But does not refreshes it from the database
        /// </summary>
        public MyTableList()
        {
            TableList = new DataTable();
            UpdateTableName();

            AddColumns();
        }

        /// <summary>
        /// Adds the default list of columns to the table
        /// </summary>
        private void AddColumns()
        {
            TableList.Columns.Add(TABLE_ID);
            TableList.Columns.Add(SCHEMA_NAME);
            TableList.Columns.Add(TABLE_NAME);
            TableList.Columns.Add(TOTAL_ROWS, Type.GetType("System.Int32"));
            TableList.Columns.Add(DATA_SIZE_MB, Type.GetType("System.Int32"));
            TableList.Columns.Add(DISTRIBUTION_TYPE);
            TableList.Columns.Add(DISTRIBUTION_COLUMN);
            TableList.Columns.Add(INDEX_TYPE);
            TableList.Columns.Add(INDEX_COLUMN);
            TableList.Columns.Add(BCP_SPLIT);
            TableList.Columns.Add(BCP_SPLIT_COLUMN);
            TableList.Columns.Add(BCP_SPLIT_VALUES);
            TableList.Columns.Add(BCP_SPLIT_VALUE_TYPE);
            TableList.Columns.Add(REPLACE_CRLF);
            TableList.Columns.Add(SELECT, typeof(bool));

        }


        /// <summary>
        /// Gets the table size, row count etc. from the Database
        /// </summary>
        /// <param name="SQLDwConfig"></param>
        public void RefreshTableListFromDB(MySettingsEnvironments SQLDwConfig)
        {
            string connstr = SQLDwConfig.GetConnectionString();
            string sql;

            string strFolderApplication = UtilGeneral.GetApplicationFolder();
            string strFileTableList = strFolderApplication + "\\" + Constants.FOLDER_CONNECTION_TYPE + "\\" + SQLDwConfig.ConnectionType + "\\" + Constants.FILE_TABLE_LIST;
            sql = System.IO.File.ReadAllText(strFileTableList);

            SqlConnection conn = new SqlConnection(connstr);

            conn.Open();
            SqlCommand comm = new SqlCommand(sql, conn);

            try
            {
                comm.CommandTimeout = 120;
                SqlDataReader rdr = comm.ExecuteReader();
                TableList.Clear();
                TableList.Load(rdr);
                rdr = null;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable GetTableList()
        {
            return TableList;
        }


        /// <summary>
        /// Replaces the currently available table list with the new table updated on the User Grid
        /// </summary>
        /// <param name="dtNewTable"></param>
        public void ReplaceTableList(DataTable dtNewTable)
        {
            TableList = dtNewTable;
            UpdateTableName();
        }

        /// <summary>
        /// Save the Table list to an XML file so that it can be reused later
        /// </summary>
        /// <param name="strFolder">Folder where XML file is to be created</param>
        /// <param name="strFileName">XML File name to save</param>
        public void SaveListToXML(string strFolder, string strFileName)
        {
            if (!Directory.Exists(strFolder))
                Directory.CreateDirectory(strFolder);

            TableList.WriteXml(strFolder + "\\" + strFileName, XmlWriteMode.WriteSchema);
        }

        /// <summary>
        /// Updates a specific row on the table based on User input.
        /// Can be used to specify what is the table distribution, index, BCP details
        /// </summary>
        /// <param name="TableID"></param>
        /// <param name="DistributionType"></param>
        /// <param name="DistributionColumn"></param>
        /// <param name="IndexType"></param>
        /// <param name="IndexColumn"></param>
        /// <param name="BCPSplit"></param>
        /// <param name="BCPColumns"></param>
        /// <param name="BCPValues"></param>
        /// <param name="BCPValueType"></param>
        /// <param name="ReplaceCRLF"></param>
        public void UpdateRow(string TableID, string DistributionType, string DistributionColumn, string IndexType, string IndexColumn, string BCPSplit, string BCPColumns, string BCPValues, string BCPValueType, string ReplaceCRLF)
        {
            DataRow[] foundRow = TableList.Select(MyTableList.TABLE_ID + " = '" + TableID + "'");
            foundRow[0][MyTableList.DISTRIBUTION_TYPE] = DistributionType;
            foundRow[0][MyTableList.DISTRIBUTION_COLUMN] = DistributionColumn;
            foundRow[0][MyTableList.INDEX_TYPE] = IndexType;
            foundRow[0][MyTableList.INDEX_COLUMN] = IndexColumn;
            foundRow[0][MyTableList.BCP_SPLIT] = BCPSplit;
            foundRow[0][MyTableList.BCP_SPLIT_COLUMN] = BCPColumns;
            foundRow[0][MyTableList.BCP_SPLIT_VALUES] = BCPValues;
            foundRow[0][MyTableList.BCP_SPLIT_VALUE_TYPE] = BCPValueType;
            foundRow[0][MyTableList.REPLACE_CRLF] = ReplaceCRLF;
        }

        /// <summary>
        /// Give the Table a default name
        /// </summary>
        private void UpdateTableName()
        {
            TableList.TableName = "SQLDwTable";
        }

        /// <summary>
        /// Gets the distinct list of values for any column. Mainly used to generate the list of Schema for script creation
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public DataTable GetDistinctColumnValues(string colName)
        {
            DataTable uniqueCols = TableList.DefaultView.ToTable(true, colName);
            return uniqueCols;
        }

    }
}
