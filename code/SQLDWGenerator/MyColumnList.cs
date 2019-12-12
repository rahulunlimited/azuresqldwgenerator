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
        public const string PKCOLUMN = "PKColumn";
        public const string PKDESCENDING = "PKDescending";

        public const int MODE_CRLF_NONE = 0;
        public const int MODE_CRLF_REPLACE = 1;
        public const int MODE_CRLF_REVERT = 2;

        public const int DB_MODE_SQLDW = 1;
        public const int DB_MODE_SQLDB = 2;

        public const string COL_SCRIPT_TYPE_INSERT = "INSERT";
        public const string COL_SCRIPT_TYPE_SELECT = "SELECT";
        public const string COL_SCRIPT_TYPE_FILE_HEADER = "FILEH_EADER";
        public const string COL_SCRIPT_TYPE_SELECT_WITH_CRLF_REPLACE = "SELECT_CRLF_REPLACE";
        public const string COL_SCRIPT_TYPE_SELECT_WITH_CRLF_REVERT = "SELECT_CRLF_REVERT";
        public const string COL_SCRIPT_TYPE_CREATE_TABLE_DWH = "CREATE-TABLE-DWH";
        public const string COL_SCRIPT_TYPE_CREATE_TABLE_EXT = "CREATE-TABLE-EXT";
        public const string COL_SCRIPT_TYPE_CREATE_TABLE_STG = "CREATE-TABLE-STG";
        public const string COL_SCRIPT_TYPE_CREATE_TABLE_PSA = "CREATE-TABLE-PSA";
        public const string COL_SCRIPT_TYPE_PRIMARY_KEY = "PRIMARY-KEY";

        public const string COL_SCRIPT_TYPE_MERGE_SRC_SELECT = "MERGE-SRC-SELECT";
        public const string COL_SCRIPT_TYPE_MERGE_KEY_COL_JOIN = "MERGE-COL-KEY-JOIN";
        public const string COL_SCRIPT_TYPE_MERGE_COL_UPDATE = "MERGE-COL-UPDATE";
        public const string COL_SCRIPT_TYPE_MERGE_COL_INSERT = "MERGE-COL-INSERT";

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
            ColumnList.Columns.Add(PKCOLUMN, Type.GetType("System.Int32"));
            ColumnList.Columns.Add(PKDESCENDING, Type.GetType("System.Int32"));

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

                int colLength;
                // If the length is MAX (-1) set the maximum length as 8000. If the column type is nvarchar or nchar then the max length is 4000
                if (length == -1)
                    // If lenght is MAX, change the length to 8000
                    colLength = maxLength;
                else
                    colLength = length;

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

                        if (TableType == Constants.TABLE_TYPE_STG || TableType == Constants.TABLE_TYPE_PERSISTENT)
                            if(length == -1)
                                colstring += datatype + "(MAX)";
                            else
                                colstring += datatype + "(" + colLength + ")";
                        else
                        {

                            //If ReplaceCRLF then increase the column length for External table as the BCP will out more characters <CRLF> for each CRLF
                            //These would however be replaced back in the INSERT script to main DWH table
                            if (ReplaceCRLF == Constants.YES && TableType == Constants.TABLE_TYPE_EXTERNAL)
                                colLength = colLength + Properties.Settings.Default.AddCharactersForCRLF;

                            if (colLength > maxLength)
                                colLength = maxLength;

                            colstring += datatype + "(" + colLength + ")";
                        }
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
                        if (TableType == Constants.TABLE_TYPE_EXTERNAL && TableType == Constants.TABLE_TYPE_DWH)
                            colstring += "varbinary(" + Constants.MAX_CHARACTER_NUMBER +")";
                        else
                            colstring += datatype;
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
            if (TableType != Constants.TABLE_TYPE_PERSISTENT)
            {
                if (colstring.EndsWith(COLUMN_SEPERATOR.Trim()))
                    colstring = colstring.Substring(0, colstring.Length - 1);
            }
            return colstring;
        }



        private string GetColListForPrimaryKey()
        {
            string colstring = "";

            foreach (DataRow col in ColumnList.Rows)
            {

                string colName;
                colName = col[COLUMN_NAME].ToString();
                colName = UtilGeneral.GetQuotedString(colName);

                if (col[PKCOLUMN].ToString() == "1")
                {
                    colstring += Constants.TAB + colName;
                    if (col[PKDESCENDING].ToString() == "1")
                        colstring += " DESC";
                    else
                        colstring += " ASC";
                    colstring += COLUMN_SEPERATOR + Environment.NewLine;
                }
            }

            colstring = colstring.TrimEnd();
            //Remove the last comma
            if (colstring.EndsWith(COLUMN_SEPERATOR.Trim()))
                colstring = colstring.Substring(0, colstring.Length - 1);

            return colstring;
        }


        private string GetColListForMerge (string MergeScriptType, string SourceAlias, string TargetAlias)
        {
            string colstring = "";

            foreach (DataRow col in ColumnList.Rows)
            {

                string colName;
                colName = col[COLUMN_NAME].ToString();
                colName = UtilGeneral.GetQuotedString(colName);

                switch(MergeScriptType)
                {
                    case COL_SCRIPT_TYPE_MERGE_KEY_COL_JOIN:
                        if (col[PKCOLUMN].ToString() == "1")
                        {
                            colstring += Constants.TAB + SourceAlias + "." + colName + " = " + TargetAlias + "." + colName;
                            colstring += COLUMN_SEPERATOR + Environment.NewLine;
                        }
                        break;
                    case COL_SCRIPT_TYPE_MERGE_COL_UPDATE:
                        if (col[PKCOLUMN].ToString() != "1")
                        {
                            colstring += Constants.TAB + Constants.TAB + TargetAlias + "." + colName + " = " + SourceAlias + "." + colName;
                            colstring += COLUMN_SEPERATOR + Environment.NewLine;
                        }
                        break;
                }
            }

            if (MergeScriptType == COL_SCRIPT_TYPE_MERGE_COL_UPDATE)
            {
                string strHasColumn = UtilGeneral.GetQuotedString(Constants.MERGE_HASH_COL);
                colstring += Constants.TAB + Constants.TAB + Constants.MERGE_TARGET_ALIAS + "." + strHasColumn + " = " + Constants.MERGE_SOURCE_ALIAS + "." + strHasColumn;
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
            string colString = "";

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

        private string GetColListForSELECT(int CRLFMode, int DBMode, string SourceAlias)
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

                if (DBMode == DB_MODE_SQLDW)
                {
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
                else if (DBMode == DB_MODE_SQLDB)
                {
                    if (SourceAlias == "")
                        colString += colName + COLUMN_SEPERATOR;
                    else
                        colString += SourceAlias + "." + colName + COLUMN_SEPERATOR;
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
            string colstring = "";

            switch(ScriptType)
            {
                case MyColumnList.COL_SCRIPT_TYPE_SELECT:
                    colstring = GetColListForSELECT(MyColumnList.MODE_CRLF_NONE, DB_MODE_SQLDW, "");
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_SELECT_WITH_CRLF_REPLACE:
                    colstring = GetColListForSELECT(MyColumnList.MODE_CRLF_REPLACE, DB_MODE_SQLDW, "");
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_SELECT_WITH_CRLF_REVERT:
                    colstring = GetColListForSELECT(MyColumnList.MODE_CRLF_REVERT, DB_MODE_SQLDW, "");
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_MERGE_SRC_SELECT:
                    colstring = GetColListForSELECT(MyColumnList.MODE_CRLF_REVERT, DB_MODE_SQLDB, "");
                    break;

                case MyColumnList.COL_SCRIPT_TYPE_INSERT:
                    colstring = GetColListForINSERT();
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_FILE_HEADER:
                    colstring = GetColListForFILEHEADER("|~|");
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_CREATE_TABLE_DWH:
                    colstring = GetColListForCREATETABLE(Constants.TABLE_TYPE_DWH, "");
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_CREATE_TABLE_STG:
                    colstring = GetColListForCREATETABLE(Constants.TABLE_TYPE_STG, "");
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_CREATE_TABLE_PSA:
                    colstring = GetColListForCREATETABLE(Constants.TABLE_TYPE_PERSISTENT, "");
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_PRIMARY_KEY:
                    colstring = GetColListForPrimaryKey();
                    break;
            }

            return colstring;
        }

        public string GetColumnListSQL(string ScriptType, string strSourceAlias, string strTargetAlias)
        {

            string colstring = "";

            switch(ScriptType)
            {
                case MyColumnList.COL_SCRIPT_TYPE_MERGE_COL_UPDATE:
                case MyColumnList.COL_SCRIPT_TYPE_MERGE_KEY_COL_JOIN:
                    colstring = GetColListForMerge(ScriptType, strSourceAlias, strTargetAlias);
                    break;
            }

            return colstring;
        }

        /// <summary>
        /// Main Routine to generate Column List
        /// </summary>
        /// <param name="ScriptType">Specify what type of Column list is needed</param>
        /// <param name="ReplaceCRLF">Specify if CRLF special characater is to be handled</param>
        /// <returns></returns>
        public string GetColumnListSQL(string ScriptType, string Parameter)
        {
            string colString = "";

            switch (ScriptType)
            {
                case MyColumnList.COL_SCRIPT_TYPE_CREATE_TABLE_EXT:
                    colString = GetColListForCREATETABLE(Constants.TABLE_TYPE_EXTERNAL, Parameter);
                    break;
                case MyColumnList.COL_SCRIPT_TYPE_MERGE_COL_INSERT:
                    colString = GetColListForSELECT(MyColumnList.MODE_CRLF_REVERT, DB_MODE_SQLDB, Parameter);
                    break;
            }

            return colString;
        }

    }

}
