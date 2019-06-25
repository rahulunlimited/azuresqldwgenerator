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
    /// The class gets database column details for each table. 
    /// It generates different scripts for e.g. Create Table, Select, Insert etc.
    /// </summary>
    internal class MyColumnList
    {
        DataTable ColumnList;

        public const string COLUMN_NAME = "ColumnName";
        public const string DATA_TYPE = "DataType";
        public const string LENGTH = "ColumnLength";
        public const string PRECISION = "Precision";
        public const string SCALE = "Scale";
        public const string IS_NULLABLE = "IS_NULLABLE";
        public const string ORDER = "ColumnOrder";

        public const int MODE_CRLF_NONE = 0;
        public const int MODE_CRLF_REPLACE = 1;
        public const int MODE_CRLF_REVERT = 2;

        public const string COL_SCRIPT_TYPE_INSERT = "INSERT";
        public const string COL_SCRIPT_TYPE_SELECT = "SELECT";
        public const string COL_SCRIPT_TYPE_FILE_HEADER = "FILEH_EADER";
        public const string COL_SCRIPT_TYPE_SELECT_WITH_CRLF_REPLACE = "SELECT_CRLF_REPLACE";
        public const string COL_SCRIPT_TYPE_SELECT_WITH_CRLF_REVERT = "SELECT_CRLF_REVERT";
        public const string COL_SCRIPT_TYPE_CREATE_TABLE_DWH = "CREATE-TABLE-DWH";
        public const string COL_SCRIPT_TYPE_CERATE_TABLE_EXT = "CREATE-TABLE-EXT";

        public const string COLUMN_SEPERATOR = ", ";

        /// <summary>
        /// Creates a new ColumnList instance based on Schema and Table Name.
        /// Uses the current configuration data for more details
        /// </summary>
        /// <param name="strSchemaName"></param>
        /// <param name="strTableName"></param>
        /// <param name="SQLDwConfig">The application configuration is passed to get all details related to currently selected configuration</param>

        internal MyColumnList(string strSchemaName, string strTableName, MySettingsEnvironments SQLDwConfig)
        {
            ColumnList = new DataTable();
            ColumnList.Columns.Add(COLUMN_NAME);
            ColumnList.Columns.Add(DATA_TYPE);
            ColumnList.Columns.Add(LENGTH, Type.GetType("System.Int32"));
            ColumnList.Columns.Add(PRECISION, Type.GetType("System.Int32"));
            ColumnList.Columns.Add(SCALE, Type.GetType("System.Int32"));
            ColumnList.Columns.Add(IS_NULLABLE, Type.GetType("System.Int32"));
            ColumnList.Columns.Add(ORDER, Type.GetType("System.Int32"));

            // Get the connection string from the current selected configuration
            string connstr = SQLDwConfig.GetConnectionString();

            string strFolderApplication = UtilGeneral.GetApplicationFolder();
            string strFileTableList = strFolderApplication + "\\" + Constants.FOLDER_CONNECTION_TYPE + "\\" + SQLDwConfig.ConnectionType + "\\" + Constants.FILE_COLUMN_LIST;
            string sql = System.IO.File.ReadAllText(strFileTableList);

            SqlConnection conn = new SqlConnection(connstr);

            conn.Open();
            SqlCommand comm = new SqlCommand(sql, conn);

            comm.Parameters.Add(new SqlParameter("@SchemaName", SqlDbType.VarChar));
            comm.Parameters["@SchemaName"].Value = strSchemaName;

            comm.Parameters.Add(new SqlParameter("@TableName", SqlDbType.VarChar));
            comm.Parameters["@TableName"].Value = strTableName;

            try
            {
                SqlDataReader rdr = comm.ExecuteReader();
                ColumnList.Load(rdr);
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable GetColumnsList()
        {
            return ColumnList;
        }

        /// <summary>
        /// Get Column list string for Creating a Table
        /// </summary>
        /// <param name="TableType">Specify Table Type as DW Table or External Table</param>
        /// <param name="ReplaceCRLF">Specify if you want to replace CRLF from text column. If so, the text columns on External Table will be created with double length</param>
        /// <returns></returns>

        private string GetColListForCREATETABLE(string TableType, string ReplaceCRLF)
        {
            string colstring = "";

            foreach (DataRow col in ColumnList.Rows)
            {
                string datatype;
                int length, precision, scale, isnullable, order;
                string colName;

                datatype = col[MyColumnList.DATA_TYPE].ToString();
                length = (int)col[MyColumnList.LENGTH];
                precision = (int)col[MyColumnList.PRECISION];
                scale = (int)col[MyColumnList.SCALE];
                isnullable = (int)col[MyColumnList.IS_NULLABLE];
                order = (int)col[MyColumnList.ORDER];
                colName = col[COLUMN_NAME].ToString();

                int maxLength = 0;
                if (datatype == "varchar" || datatype == "char" || datatype == "varbinary")
                    maxLength = Constants.MAX_CHARACTER_NUMBER;
                else if (datatype == "nvarchar" || datatype == "nchar")
                    maxLength = Constants.MAX_CHARACTER_NUMBER / 2;

                // If the length is MAX (-1) set the maximum length as 8000. If the column type is nvarchar or nchar then the max length is 4000
                if (length == -1)
                {
                    // If lenght is MAX, change the length to 8000
                    length = maxLength;
                }

                if (TableType == Constants.TABLE_TYPE_EXTERNAL && datatype == "varbinary") continue;
                if (datatype == "xml") continue;

                colstring += Constants.TAB + UtilGeneral.GetQuotedString(colName) + " ";

                switch (datatype)
                {
                    case "varchar":
                    case "char":
                    case "nvarchar":
                    case "nchar":
                    case "binary":
                    case "varbinary":
                        //If ReplaceCRLF then increase the column length for External table as the BCP will out more characters <CRLF> for each CRLF
                        //These would however be replaced back in the INSERT script to main DWH table
                        if (ReplaceCRLF == Constants.YES && TableType == Constants.TABLE_TYPE_EXTERNAL) 
                            length = length + Properties.Settings.Default.AddCharactersForCRLF;

                        if (length > maxLength || length == -1)
                            length = maxLength;

                        colstring += datatype + "(" + length + ")";
                        break;
                    case "time":
                    case "datetimeoffset":
                    case "datetime2":
                        colstring += datatype + "(" + scale + ")";
                        break;
                    case "decimal":
                    case "numeric":
                        colstring += datatype + "(" + precision + "," + scale + ")";
                        break;
                    case "int":
                    case "bigint":
                    case "tinyint":
                    case "bit":
                    case "smallint":
                    case "smallmoney":
                    case "money":
                    case "float":
                    case "real":
                    case "date":
                    case "smalldatetime":
                    case "datetime":
                    case "image":
                    case "sysname":
                        colstring += datatype;
                        break;
                    case "uniqueidentifier":
                        // UniqueIdentifier columns are not supported in Exernal Table. Change this to varchar to allow storing the GuID data.
                        if (TableType == Constants.TABLE_TYPE_EXTERNAL)
                            colstring += "varchar(100)";
                        else
                            colstring += datatype;
                        break;
                    //SQL DW does not support geography and geometry. Change the column to varbinary(8000)
                    case "geometry":
                    case "geography":
                        colstring += "varbinary(" + Constants.MAX_CHARACTER_NUMBER +")";
                        break;
                }

                if (isnullable == 0)
                    colstring += " NOT NULL";
                else
                    colstring += " NULL";

                colstring += COLUMN_SEPERATOR + Environment.NewLine;
            }

            colstring = colstring.TrimEnd();
            //Remove the last comma
            if (colstring.EndsWith(COLUMN_SEPERATOR.Trim()))
                colstring = colstring.Substring(0, colstring.Length - 1);

            return colstring;
        }

        /// <summary>
        /// Get Column list for INSERT from External Table to DW table
        /// </summary>
        /// <returns></returns>
        private string GetColListForINSERT()
        {
            string colString;

            colString = "(";
            foreach (DataRow col in ColumnList.Rows)
            {
                string colName;
                colName = col[MyColumnList.COLUMN_NAME].ToString();
                colName = UtilGeneral.GetQuotedString(colName);

                int length;
                length = (int)col[MyColumnList.LENGTH];

                switch (col[MyColumnList.DATA_TYPE].ToString())
                {
                    case "varchar":
                    case "char":
                    case "nvarchar":
                    case "nchar":
                        colString += colName + COLUMN_SEPERATOR;
                        break;
                    case "xml":
                        colString += "";
                        break;
                    case "varbinary":
                        if (length == -1 || length > Constants.MAX_CHARACTER_NUMBER)
                            //If Varbinary(MAX) or VARBINARY(>8000) then ignore because SQL DW will not allow more than 8000 characters
                            colString += "";
                        else
                            colString += colName + COLUMN_SEPERATOR;
                        break;
                    default:
                        colString += colName + COLUMN_SEPERATOR;
                        break;
                }
            }

            colString = colString.TrimEnd();

            // Remove comma at the end
            if (colString.EndsWith(COLUMN_SEPERATOR.Trim()))
                colString = colString.Substring(0, colString.Length - 1);

            colString += ")";

            return colString;

        }


        /// <summary>
        /// Get column list for creating an empty file header
        /// </summary>
        /// <param name="strColSeparator">Specify the Column Delimiter for File Header</param>
        /// <returns></returns>

        private string GetColListForFILEHEADER(string strColSeparator)
        {
            string colString;

            colString = "";
            foreach (DataRow col in ColumnList.Rows)
            {
                string colName;
                colName = col[MyColumnList.COLUMN_NAME].ToString();
                colString += colName + strColSeparator;
            }

            colString = colString.TrimEnd();

            //Remove the last Column Delimiter
            if (colString.EndsWith(strColSeparator))
                colString = colString.Substring(0, colString.Length - strColSeparator.Length);

            return colString;

        }

        /// <summary>
        /// Get Column for SELECT SQL
        /// </summary>
        /// <param name="CRLFMode">Specify if CRLF is to be handled. Text Columns will have code to replace CRLF with [CRLF]</FR></param>
        /// <returns></returns>

        private string GetColListForSELECT(int CRLFMode)
        {
            string colString;

            colString = "";
            foreach (DataRow col in ColumnList.Rows)
            {
                string colName, dataType;
                colName = col[MyColumnList.COLUMN_NAME].ToString();
                dataType = col[MyColumnList.DATA_TYPE].ToString();

                colName = UtilGeneral.GetQuotedString(colName);

                int length;
                length = (int)col[MyColumnList.LENGTH];

                switch (dataType)
                {
                    case "varchar":
                    case "char":
                    case "nvarchar":
                    case "nchar":
                        switch (CRLFMode)
                        {
                            //While generating data for BCP replace CRLF with [CR][LF]
                            case MyColumnList.MODE_CRLF_REPLACE:
                                int colLength;
                                colLength = length + Properties.Settings.Default.AddCharactersForCRLF;
                                colString += "LEFT(REPLACE(REPLACE(" + colName + ", CHAR(13), '<CR>'), CHAR(10), '<LF>'), " + colLength + ") " + colName;
                                break;
                            //While generating SELECT for Insert from BLOB to Main revernt [CR][LF] with actual CRLF
                            case MyColumnList.MODE_CRLF_REVERT:
                                colString += "REPLACE(REPLACE(" + colName + ", '<CR>', CHAR(13)), '<LF>', CHAR(10))";
                                break;
                            default:
                                colString += colName;
                                break;
                        }
                        colString += COLUMN_SEPERATOR;
                        break;
                    case "xml":
                        // Ignore XML data columns.
                        colString += "";
                        break;
                    case "varbinary":
                        if (length == -1 || length > Constants.MAX_CHARACTER_NUMBER)
                            //If Varbinary(MAX) or VARBINARY(>8000) then ignore
                            colString += "";
                        else
                            // Ensure values are not more than max allowable length
                            colString += "LEFT(" + colName + ", " + Constants.MAX_CHARACTER_NUMBER + ")" + COLUMN_SEPERATOR;
                        break;
                    default:
                        colString += colName + COLUMN_SEPERATOR;
                        break;
                }
            }

            colString = colString.TrimEnd();

            // Remove comma at the end
            if (colString.EndsWith(COLUMN_SEPERATOR.Trim()))
                colString = colString.Substring(0, colString.Length - 1);

            return colString;
        }


        /// <summary>
        /// Main Routine to generate Column List
        /// </summary>
        /// <param name="ScriptType">Specify what type of Column list is needed</param>
        /// <returns></returns>
        public string GetColumnListSQL(string ScriptType)
        {
            string colString = "";

            switch(ScriptType)
            {
                case MyColumnList.COL_SCRIPT_TYPE_SELECT:
                    colString = GetColListForSELECT(MyColumnList.MODE_CRLF_NONE);
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_SELECT_WITH_CRLF_REPLACE:
                    colString = GetColListForSELECT(MyColumnList.MODE_CRLF_REPLACE);
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_SELECT_WITH_CRLF_REVERT:
                    colString = GetColListForSELECT(MyColumnList.MODE_CRLF_REVERT);
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_INSERT:
                    colString = GetColListForINSERT();
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_FILE_HEADER:
                    colString = GetColListForFILEHEADER("|~|");
                    break;
            }

            return colString;
        }

        /// <summary>
        /// Main Routine to generate Column List
        /// </summary>
        /// <param name="ScriptType">Specify what type of Column list is needed</param>
        /// <param name="ReplaceCRLF">Specify if CRLF special characater is to be handled</param>
        /// <returns></returns>
        public string GetColumnListSQL(string ScriptType, string ReplaceCRLF)
        {
            string colString = "";

            switch (ScriptType)
            {
                case MyColumnList.COL_SCRIPT_TYPE_CERATE_TABLE_EXT:
                    colString = GetColListForCREATETABLE(Constants.TABLE_TYPE_EXTERNAL, ReplaceCRLF);
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_CREATE_TABLE_DWH:
                    colString = GetColListForCREATETABLE(Constants.TABLE_TYPE_DWH, "");
                    break;
            }

            return colString;
        }

    }

}
