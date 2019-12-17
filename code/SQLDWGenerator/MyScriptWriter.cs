using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

namespace SQLDwGenerator
{
    /// <summary>
    /// Main class for the Application to help create different type of Scripts
    /// </summary>
    class MyScriptWriter
    {

        private string OutputFolder;
        private string OutputFolderSQLScripts;
        private MySettingsEnvironments SQLDwConfig;
        public const string TAB = "\t";

        /// <summary>
        /// Create an instance of the ScriptWriter class with the Application Configuration
        /// The will create a folder if not present for Scripts
        /// </summary>
        /// <param name="CurrentConfig"></param>
        public MyScriptWriter(MySettingsEnvironments CurrentConfig)
        {
            SQLDwConfig = CurrentConfig;

            string strConfigName;
            strConfigName = SQLDwConfig.ConfigFileName.Replace(".Config", "");
            OutputFolder = Properties.Settings.Default.ApplicationFolder + "\\" + Constants.SUB_FOLDER_SCRIPTS + "\\" + strConfigName;
            OutputFolderSQLScripts = OutputFolder + "\\SQLScripts";

            if (!Directory.Exists(OutputFolder))
                Directory.CreateDirectory(OutputFolder);

            if (!Directory.Exists(OutputFolderSQLScripts))
                Directory.CreateDirectory(OutputFolderSQLScripts);
        }

        /// <summary>
        /// Generate scripts for SQL DW External Table
        /// </summary>
        /// <param name="TableList"></param>
        /// <param name="blnDrop">Specify if the Script should contain Drop and Create</param>
        public string GenerateTableCreateExternal(MyTableList TableList, bool blnDrop, bool ReturnScriptFlag)
        {
            string strSchemaName, strTableName, strTableExists, strTableNameExternal;
            string strFileURL;
            long RowCount = 0;
            ArrayList ALScript = new ArrayList();

            string strFileSuffix;
            strFileSuffix = ".ExternalTable.CREATE.sql";

            // Specify the file name for the Script
            strFileURL = OutputFolder + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", strFileSuffix);

            try
            {

                DataTable dtTable = TableList.GetTableList();
                GenerateSchemaList(TableList, ALScript);

                foreach (DataRow row in dtTable.Rows)
                {
                    if ((string)row[MyTableList.SELECT].ToString() == Boolean.FalseString || (string)row[MyTableList.SELECT].ToString() == "") continue;

                    strSchemaName = row[MyTableList.SCHEMA_NAME].ToString();
                    strTableName = row[MyTableList.TABLE_NAME].ToString();
                    strTableNameExternal = Constants.EXTERNAL_TABLE_PREFIX + strTableName;
                    RowCount = Convert.ToInt64(row[MyTableList.TOTAL_ROWS].ToString());

                    // If the table contains no rows, then do not create External table. 
                    // External Tables are based on BLOB Files which are generated through BCP. So no rows means no external file
                    if (RowCount == 0)
                    {
                        ALScript.Add("PRINT '0 Rows for table [" + strSchemaName + "].[" + strTableNameExternal + "]. External Table Create script not generated.'");
                    }
                    else
                    {

                        // Get SQL String to check if table already exists
                        strTableExists = GetTableExistsSQL(strSchemaName, strTableNameExternal);

                        if (blnDrop)
                        // Create drop table script if specified
                        {
                            strTableExists = "IF EXISTS(" + strTableExists + ")";
                            ALScript.Add(strTableExists);
                            ALScript.Add("DROP EXTERNAL TABLE [" + strSchemaName + "].[" + strTableNameExternal + "]");
                        }
                        else
                        {
                            strTableExists = "IF NOT EXISTS(" + strTableExists + ")";
                            ALScript.Add(strTableExists);
                        }

                        ALScript.Add("BEGIN");
                        ALScript.Add("CREATE EXTERNAL TABLE [" + strSchemaName + "].[" + strTableNameExternal + "] (");

                        // Get list of columns for creating the external table
                        MyColumnList ColumnList = new MyColumnList(strSchemaName, strTableName, SQLDwConfig);
                        string colstring = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_CREATE_TABLE_EXT, row[MyTableList.REPLACE_CRLF].ToString());

                        ALScript.Add(colstring);
                        ALScript.Add(")");
                        ALScript.Add("WITH");
                        ALScript.Add("(");

                        string strBlobLocation;
                        if (row[MyTableList.BCP_SPLIT].ToString() == Constants.YES)
                        {
                            // If the BCP is split across multiple files then specify a folder name for External table
                            strBlobLocation = "/" + strSchemaName + "_" + strTableName;
                        }
                        else
                        {
                            strBlobLocation = "/" + "Tables/" + strSchemaName + "_" + strTableName + Constants.FILE_EXTENSION_BCP_MAIN + Constants.FILE_EXTENSION_GZ_MAIN;
                        }

                        ALScript.Add(Constants.TAB + "LOCATION = '" + strBlobLocation + "',");
                        ALScript.Add(Constants.TAB + "DATA_SOURCE = " + Properties.Settings.Default.AzBLOBDataSourceName + ",");
                        ALScript.Add(Constants.TAB + "FILE_FORMAT = " + Properties.Settings.Default.AzFileFormatCompressed + ",");
                        ALScript.Add(Constants.TAB + "REJECT_TYPE = VALUE,");
                        ALScript.Add(Constants.TAB + "REJECT_VALUE = 0,");
                        ALScript.Add(Constants.TAB + "REJECTED_ROW_LOCATION = '/REJECT/" + strSchemaName + "_" + strTableName + "'");
                        ALScript.Add(")");
                        ALScript.Add("END");
                        //ALScript.Add("GO");
                        ALScript.Add("");
                        ALScript.Add("");
                    }
                }
                GenerateFile(strFileURL, ALScript);

                string strReturn = "";
                if(ReturnScriptFlag)
                    strReturn = string.Join(Environment.NewLine, ALScript.ToArray());

                return strReturn;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }


        /// <summary>
        /// Genreate Script for creating the SQL DW Table
        /// </summary>
        /// <param name="TableList"></param>
        /// <param name="blnDrop">Specify if the Script should contain Drop and Create</param>
        public string GenerateTableCreateDWH(MyTableList TableList, bool blnDrop, bool ReturnScriptFlag)
        {
            ArrayList ALScript = new ArrayList();

            string strSchemaName, strTableName, strTableExists;

            // Specify the file name for the Script
            string strFileURL, strFileSuffix;
            strFileSuffix = ".DWHTable.CREATE.sql";
            strFileURL = OutputFolder + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", strFileSuffix);
            try
            {
                DataTable dtTable = TableList.GetTableList();
                GenerateSchemaList(TableList, ALScript);

                foreach (DataRow row in dtTable.Rows)
                {
                    // Skip table creating script if table is unselected in the Grid
                    if ((string)row[MyTableList.SELECT].ToString() == Boolean.FalseString || (string)row[MyTableList.SELECT].ToString() == "") continue;

                    strSchemaName = row[MyTableList.SCHEMA_NAME].ToString();
                    strTableName = row[MyTableList.TABLE_NAME].ToString();

                    strTableExists = GetTableExistsSQL(strSchemaName, strTableName);

                    if (blnDrop)
                    {
                        // Drop Table if specified
                        strTableExists = "IF EXISTS(" + strTableExists + ")";
                        ALScript.Add(strTableExists);
                        ALScript.Add("DROP TABLE [" + strSchemaName + "].[" + strTableName + "]");
                    }
                    else
                    {
                        strTableExists = "IF NOT EXISTS(" + strTableExists + ")";
                        ALScript.Add(strTableExists);
                    }

                    ALScript.Add("BEGIN");
                    ALScript.Add("CREATE TABLE [" + strSchemaName + "].[" + strTableName + "] (");

                    // Get list of columns for creating the SQL DW table
                    MyColumnList ColumnList = new MyColumnList(strSchemaName, strTableName, SQLDwConfig);
                    string colstring = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_CREATE_TABLE_DWH);

                    ALScript.Add(colstring);
                    ALScript.Add(")");
                    ALScript.Add("WITH");
                    ALScript.Add("(");

                    string strDistribution = GetTableDistributionString(row);
                    ALScript.Add(strDistribution);

                    string strIndex = GetTableIndexString(row);
                    ALScript.Add(strIndex);

                    ALScript.Add(")");
                    ALScript.Add("END");
                    //ALScript.Add("GO");
                    ALScript.Add("");
                    ALScript.Add("");
                }
                GenerateFile(strFileURL, ALScript);

                string strReturn = "";
                if (ReturnScriptFlag)
                    strReturn = string.Join(Environment.NewLine, ALScript.ToArray());

                return strReturn;


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }


        /// <summary>
        /// Genreate Script for creating the STG Table
        /// </summary>
        /// <param name="TableList"></param>
        /// <param name="blnDrop">Specify if the Script should contain Drop and Create</param>
        public string GenerateTableCreateSTG(MyTableList TableList, bool blnDrop, bool ReturnScriptFlag)
        {
            ArrayList ALScript = new ArrayList();

            string strSchemaName, strTableName, strTableNameSTG, strSchemaNameSTG, strTableExists;

            // Specify the file name for the Script
            string strFileURL, strFileSuffix;
            strFileSuffix = ".STGTable.CREATE.sql";
            strFileURL = OutputFolderSQLScripts + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", strFileSuffix);
            try
            {
                strSchemaNameSTG = Constants.SCHEMA_STAGE;
                DataTable dtTable = TableList.GetTableList();
                ALScript.Add(GenerateSchema(strSchemaNameSTG));

                foreach (DataRow row in dtTable.Rows)
                {
                    // Skip table creating script if table is unselected in the Grid
                    if ((string)row[MyTableList.SELECT].ToString() == Boolean.FalseString || (string)row[MyTableList.SELECT].ToString() == "") continue;

                    strSchemaName = row[MyTableList.SCHEMA_NAME].ToString();
                    strTableName = row[MyTableList.TABLE_NAME].ToString();
                    strTableNameSTG = Constants.SQL_SCRIPT_SOURCE + "_" + strTableName;

                    strTableExists = GetTableExistsSQL(strSchemaNameSTG, strTableNameSTG);

                    if (blnDrop)
                    {
                        // Drop Table if specified
                        strTableExists = "IF EXISTS(" + strTableExists + ")";
                        ALScript.Add(strTableExists);
                        ALScript.Add("DROP TABLE [" + strSchemaNameSTG + "].[" + strTableNameSTG + "]");
                    }
                    else
                    {
                        strTableExists = "IF NOT EXISTS(" + strTableExists + ")";
                        ALScript.Add(strTableExists);
                    }

                    ALScript.Add("BEGIN");
                    ALScript.Add("CREATE TABLE [" + strSchemaNameSTG + "].[" + strTableNameSTG + "] (");

                    // Get list of columns for creating the SQL DW table
                    MyColumnList ColumnList = new MyColumnList(strSchemaName, strTableName, SQLDwConfig);
                    string colstring = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_CREATE_TABLE_STG);

                    ALScript.Add(colstring);
                    ALScript.Add(")");

                    ALScript.Add("END");
                    //ALScript.Add("GO");
                    ALScript.Add("");
                    ALScript.Add("");
                }
                GenerateFile(strFileURL, ALScript);

                string strReturn = "";
                if (ReturnScriptFlag)
                    strReturn = string.Join(Environment.NewLine, ALScript.ToArray());

                return strReturn;


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }



        /// <summary>
        /// Genreate Script for creating the STG Table
        /// </summary>
        /// <param name="TableList"></param>
        /// <param name="blnDrop">Specify if the Script should contain Drop and Create</param>
        public string GenerateTableCreatePersistent(MyTableList TableList, bool blnDrop, bool ReturnScriptFlag)
        {
            ArrayList ALScript = new ArrayList();

            string strSchemaName, strTableName, strTableNamePSA, strSchemaNamePSA, strTableExists;

            // Specify the file name for the Script
            string strFileURL, strFileSuffix;
            strFileSuffix = ".PersistentTable.CREATE.sql";
            strFileURL = OutputFolderSQLScripts + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", strFileSuffix);
            try
            {
                strSchemaNamePSA = Constants.SCHEMA_PERSISTENT;

                DataTable dtTable = TableList.GetTableList();
                ALScript.Add(GenerateSchema(strSchemaNamePSA));

                foreach (DataRow row in dtTable.Rows)
                {
                    // Skip table creating script if table is unselected in the Grid
                    if ((string)row[MyTableList.SELECT].ToString() == Boolean.FalseString || (string)row[MyTableList.SELECT].ToString() == "") continue;

                    strSchemaName = row[MyTableList.SCHEMA_NAME].ToString();
                    strTableName = row[MyTableList.TABLE_NAME].ToString();
                    strTableNamePSA = Constants.SQL_SCRIPT_SOURCE + "_" + strTableName;

                    strTableExists = GetTableExistsSQL(strSchemaNamePSA, strTableNamePSA);

                    if (blnDrop)
                    {
                        // Drop Table if specified
                        strTableExists = "IF EXISTS(" + strTableExists + ")";
                        ALScript.Add(strTableExists);
                        ALScript.Add("DROP TABLE [" + strSchemaNamePSA + "].[" + strTableNamePSA + "]");
                    }
                    else
                    {
                        strTableExists = "IF NOT EXISTS(" + strTableExists + ")";
                        ALScript.Add(strTableExists);
                    }

                    ALScript.Add("BEGIN");
                    ALScript.Add("CREATE TABLE [" + strSchemaNamePSA + "].[" + strTableNamePSA + "] (");

                    // Get list of columns for creating the SQL DW table
                    MyColumnList ColumnList = new MyColumnList(strSchemaName, strTableName, SQLDwConfig);
                    string colstring = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_CREATE_TABLE_PSA);
                    ALScript.Add(colstring);
                    ALScript.Add(Constants.TAB + "[" + Constants.MERGE_HASH_COL + "]" + " binary(64) NOT NULL,");
                    ALScript.Add(Constants.TAB + "[ValidFrom] datetime2(2) GENERATED ALWAYS AS ROW START NOT NULL,");
                    ALScript.Add(Constants.TAB + "[ValidTo] datetime2(2) GENERATED ALWAYS AS ROW END NOT NULL,");

                    string strPKName = "PK_" + strSchemaNamePSA + "_" + strTableNamePSA + "_ID";
                    ALScript.Add("CONSTRAINT " + strPKName + " PRIMARY KEY CLUSTERED");
                    colstring = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_PRIMARY_KEY);
                    ALScript.Add("(");
                    ALScript.Add(colstring);
                    ALScript.Add(")");
                    ALScript.Add("WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],");
                    ALScript.Add(Constants.TAB + "PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])");
                    ALScript.Add(") ON [PRIMARY]");
                    ALScript.Add("WITH");
                    ALScript.Add("(");
                    string strHistoryTable = "[" + strSchemaNamePSA + "].[" + strTableNamePSA + "_HISTORY" + "]";
                    ALScript.Add(Constants.TAB + "SYSTEM_VERSIONING = ON (HISTORY_TABLE = " + strHistoryTable + ")");
                    ALScript.Add(")");

                    ALScript.Add("END");
                    //ALScript.Add("GO");
                    ALScript.Add("");
                    ALScript.Add("");
                }
                GenerateFile(strFileURL, ALScript);

                string strReturn = "";
                if (ReturnScriptFlag)
                    strReturn = string.Join(Environment.NewLine, ALScript.ToArray());

                return strReturn;


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        private string GetTableDistributionString(DataRow row)
        {
            string strDistribution;
            strDistribution = Constants.TAB + "DISTRIBUTION = ";

            // Check the Distribution type for the SQL DW table
            switch (row[MyTableList.DISTRIBUTION_TYPE].ToString())
            {
                case Constants.DISTRIBUTION_TYPE_HASH:
                    // If Distribution type is HASH then specify the column on which table is to be distributed
                    strDistribution = strDistribution + Constants.DISTRIBUTION_TYPE_HASH + "(" + UtilGeneral.GetQuotedString(row[MyTableList.DISTRIBUTION_COLUMN].ToString()) + "),";
                    break;
                default:
                    strDistribution = strDistribution + row[MyTableList.DISTRIBUTION_TYPE].ToString() + ",";
                    break;
            }

            return strDistribution;
        }


        private string GetTableIndexString(DataRow row)
        {
            string strIndex = "";

            // Check the Index for the SQL DW table
            switch (row[MyTableList.INDEX_TYPE].ToString())
            {
                case Constants.INDEX_TYPE_COLUMNSTORE:
                case Constants.INDEX_TYPE_HEAP:
                    strIndex = row[MyTableList.INDEX_TYPE].ToString();
                    break;
                case Constants.INDEX_TYPE_CLUSTERED:
                    // If Index type is CLUSTERED then specify the column on which CLUSTERED index is to be created
                    strIndex = row[MyTableList.INDEX_TYPE].ToString() + " (" + UtilGeneral.GetQuotedString(row[MyTableList.INDEX_COLUMN].ToString()) + ")";
                    break;
            }
            strIndex = Constants.TAB + strIndex;
            return strIndex;
        }


        /// <summary>
        /// Generate script for DROP table
        /// </summary>
        /// <param name="TableList"></param>
        /// <param name="TableType">Specify if External or SQL DW table</param>
        public void GenerateTableDrop(MyTableList TableList, string TableType)
        {
            long RowCount = 0;
            ArrayList ALScript = new ArrayList();

            string strSchemaName, strTableName, strTableExists, strSQL;
            string strFileURL;
            string strFileSuffix;

            if (TableType == Constants.TABLE_TYPE_DWH)
                strFileSuffix = ".DWHTable.DROP.sql";
            else
                strFileSuffix = ".ExternalTable.DROP.sql";

            // Specify the file name for the Script
            strFileURL = OutputFolder + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", strFileSuffix);
            try
            {
                DataTable dtTable = TableList.GetTableList();


                foreach (DataRow row in dtTable.Rows)
                {
                    // Skip if table is unselected on the Table Grid
                    if ((string)row[MyTableList.SELECT].ToString() == Boolean.FalseString || (string)row[MyTableList.SELECT].ToString() == "") continue;

                    strSchemaName = row[MyTableList.SCHEMA_NAME].ToString();
                    strTableName = row[MyTableList.TABLE_NAME].ToString();

                    // If External Table then prefix the table name with EXT
                    if (TableType == Constants.TABLE_TYPE_EXTERNAL)
                        strTableName = Constants.EXTERNAL_TABLE_PREFIX + strTableName;

                    RowCount = Convert.ToInt64(row[MyTableList.TOTAL_ROWS].ToString());

                    strTableExists = GetTableExistsSQL(strSchemaName, strTableName);
                    strTableExists = "IF EXISTS(" + strTableExists + ")";
                    ALScript.Add(strTableExists);

                    strSQL = "DROP";

                    if (TableType == Constants.TABLE_TYPE_EXTERNAL)
                        strSQL +=  " EXTERNAL";

                    strSQL += " " + "TABLE [" + strSchemaName + "].[" + strTableName + "]";
                    ALScript.Add(strSQL);
                    ALScript.Add("");
                }
                GenerateFile(strFileURL, ALScript);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }


        /// <summary>
        /// Generate SQL Scripts for TRUNCATE SQL DW table
        /// </summary>
        /// <param name="TableList"></param>
        /// <param name="TableType"></param>
        public void GenerateTableTruncate(MyTableList TableList)
        {
            long RowCount = 0;
            ArrayList ALScript = new ArrayList();

            string strSchemaName, strTableName;
            string strFileURL;
            string strFileSuffix;
            strFileSuffix = ".DWHTable.TRUNCATE.sql";

            // Specify the file name for the Script
            strFileURL = OutputFolder + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", strFileSuffix);

            try
            {
                DataTable dtTable = TableList.GetTableList();


                string strSQL;
                foreach (DataRow row in dtTable.Rows)
                {
                    // Skip if table is unselected on the Table Grid
                    if ((string)row[MyTableList.SELECT].ToString() == Boolean.FalseString || (string)row[MyTableList.SELECT].ToString() == "") continue;

                    strSchemaName = row[MyTableList.SCHEMA_NAME].ToString();
                    strTableName = row[MyTableList.TABLE_NAME].ToString();

                    RowCount = Convert.ToInt64(row[MyTableList.TOTAL_ROWS].ToString());

                    strSQL = "TRUNCATE TABLE [" + strSchemaName + "].[" + strTableName + "]";
                    ALScript.Add(strSQL);
                    ALScript.Add("");
                }
                GenerateFile(strFileURL, ALScript);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }


        /// <summary>
        /// Generate SQL Scripts for TRUNCATE SQL DW table
        /// </summary>
        /// <param name="TableList"></param>
        /// <param name="TableType"></param>
        public void GenerateTableRefactor(MyTableList TableList)
        {
            ArrayList ALScript = new ArrayList();

            string strSchemaName, strTableName;
            string strFileURL;
            string strFileSuffix;
            strFileSuffix = ".DWHTable.REFACTOR.sql";

            // Specify the file name for the Script
            strFileURL = OutputFolder + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", strFileSuffix);

            try
            {
                DataTable dtTable = TableList.GetTableList();


                string strSQL;
                foreach (DataRow row in dtTable.Rows)
                {
                    // Skip if table is unselected on the Table Grid
                    if ((string)row[MyTableList.SELECT].ToString() == Boolean.FalseString || (string)row[MyTableList.SELECT].ToString() == "") continue;

                    strSchemaName = row[MyTableList.SCHEMA_NAME].ToString();
                    strTableName = row[MyTableList.TABLE_NAME].ToString();

                    string strTableNameNew, strTableNameOld;
                    strTableNameNew = strTableName + "_NEW";
                    strTableNameOld = strTableName + "_OLD";

                    strSchemaName = UtilGeneral.GetQuotedString(strSchemaName);
                    strTableName = UtilGeneral.GetQuotedString(strTableName);
                    strTableNameNew = UtilGeneral.GetQuotedString(strTableNameNew);
                    strTableNameOld = UtilGeneral.GetQuotedString(strTableNameOld);

                    strSQL = "CREATE TABLE " + strSchemaName + "." + strTableNameNew;
                    ALScript.Add(strSQL);
                    ALScript.Add("WITH(");

                    string strDistribution = GetTableDistributionString(row);
                    ALScript.Add(strDistribution);

                    string strIndex = GetTableIndexString(row);
                    ALScript.Add(strIndex);
                    ALScript.Add(Constants.TAB + ")");
                    ALScript.Add("AS");
                    ALScript.Add("SELECT * FROM " + strSchemaName + "." + strTableName);
                    ALScript.Add("");
                    ALScript.Add("RENAME OBJECT " + strSchemaName + "." + strTableName + " TO " + strSchemaName + "." + strTableNameOld);
                    ALScript.Add("RENAME OBJECT " + strSchemaName + "." + strTableNameNew + " TO " + strSchemaName + "." + strTableName);
                    ALScript.Add("");
                    ALScript.Add("DROP TABLE " + strSchemaName + "." + strTableNameOld);
                    ALScript.Add("GO");
                    ALScript.Add("");
                    ALScript.Add("");
                    ALScript.Add("");
                }
                GenerateFile(strFileURL, ALScript);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        
        /// <summary>
                 /// Generate Script for BCP file generation
                 /// </summary>
                 /// <param name="TableList"></param>
                 /// <param name="VersionSQLUTF8">Specify if SQL Server version supports BCP output with UTF8 format</param>
        public string GenerateBCPScript(MyTableList TableList, bool EmptyFile, bool ReturnScriptURL)
        {
            string strBCP;
            long RowCount;
            ArrayList ALScript = new ArrayList();

            string strSchemaName, strTableName;
            string strFileURL;
            // Specify the file name for the Script
            strFileURL = OutputFolder + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", ".BCP.bat");

            try
            {
                string strBCPFolder, strBCPFileName, strBCPFileTemp;
                string strCreateFolder;

                string strBCPFileExt, strEncoding, strBCPOption;

                // Specify the encoding based on SQL Server version. If it supports UTF8 then generate with encoding 65001
                if (SQLDwConfig.BCPOutputFormat == Constants.BCP_OUT_FORMAT_UTF8)
                {
                    strBCPFileExt = Constants.FILE_EXTENSION_BCP_MAIN;
                    strEncoding = "-c -C 65001";
                }
                else
                {
                    strBCPFileExt = Constants.FILE_EXTENSION_BCP_MAIN_2014_BELOW;
                    strEncoding = "-w";
                }

                // Specify the BCP output options including DB connection
                strBCPOption = " -S " + SQLDwConfig.ServerName + " -T ";
                strBCPOption += Properties.Settings.Default.BCPOptions;
                strBCPOption += " " + strEncoding;
                // Get the BCP Column Delimiter from Settings. The same delimiter will be used for BLOB file format
                strBCPOption += " -t " + "\"" + Properties.Settings.Default.ColumnDelimiter + "\"";
                strBCPOption += " -r " + "\"" + "\\n" + "\"";


                DataTable dtTable = TableList.GetTableList();

                // Check if the default output folder is available if not create the folder in the BCP batch file
                strBCPFolder = SQLDwConfig.BCPOutputFolder + "\\Tables\\";
                strCreateFolder = "IF NOT EXIST \"" + strBCPFolder + "\" MD \"" + strBCPFolder + "\"";
                ALScript.Add(strCreateFolder);

                foreach (DataRow row in dtTable.Rows)
                {
                    // Skip if the table is unselected in the User Grid
                    if (row[MyTableList.SELECT].ToString() == Boolean.FalseString || row[MyTableList.SELECT].ToString() =="") continue;

                    string strBCPSplit = "";
                    string colstring;
                    string strBCPOverride = "";


                    strSchemaName = row[MyTableList.SCHEMA_NAME].ToString();
                    strTableName = row[MyTableList.TABLE_NAME].ToString();

                    MyColumnList ColumnList;
                    ColumnList = new MyColumnList(strSchemaName, strTableName, SQLDwConfig);

                    if (EmptyFile)
                    {
                        strBCPSplit = Constants.NO;
                        RowCount = -1;
                        colstring = "*";
                        strBCPOverride = "TOP 0 ";
                    }
                    else
                    {
                        strBCPSplit = row[MyTableList.BCP_SPLIT].ToString();
                        RowCount = Convert.ToInt64(row[MyTableList.TOTAL_ROWS].ToString());

                        // Check if there is any Row Count override specified in the User settings file.
                        // If specified BCP SELECT will be generated with TOP Rowcount
                        string strBCPOverrideCount;
                        strBCPOverrideCount = UtilGeneral.GetConfigValue(MySettingsUserConfig.KEY_BCP_ROW_COUNT_OVERRIDE);
                        int intBCPRowCountOverride;
                        Int32.TryParse(strBCPOverrideCount, out intBCPRowCountOverride);

                        // If an override is specified, do a TOP n. This is mainly used for test scenarios
                        if (strBCPOverrideCount != "" && intBCPRowCountOverride >= 0)
                            strBCPOverride = "TOP " + intBCPRowCountOverride + " ";

                        // Get the list of Columns for BCP genration. Specify if CRLF special character is to be handled.
                        if (row[MyTableList.REPLACE_CRLF].ToString() == Constants.YES)
                            //If CRLF is to be handled
                            colstring = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_SELECT_WITH_CRLF_REPLACE);
                        else
                            colstring = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_SELECT);
                    }

                    // If there are no records in the table, skip the BCP script generation
                    if (RowCount == 0)
                    {
                        strBCP = "REM 0 Rows for [" + strSchemaName + "].[" + strTableName + "]. No BCP Script Generated";
                        ALScript.Add(strBCP);
                    }
                    else
                    {
                        // This block of code generates scripts for BCP when it is to be split in multiple files
                        if (strBCPSplit == Constants.YES)
                        {
                            string strSplitValues = row[MyTableList.BCP_SPLIT_VALUES].ToString();
                            List<string> lstSplitValues = strSplitValues.Split(',').ToList<string>();

                            // Create a folder with Schema Name and Table Name to store multiple BCP files for 1 table
                            strBCPFolder = SQLDwConfig.BCPOutputFolder + "\\" + strSchemaName + "_" + strTableName + "\\";
                            strCreateFolder = "IF NOT EXIST \"" + strBCPFolder + "\" MD \"" + strBCPFolder + "\"";
                            ALScript.Add(strCreateFolder);

                            // Find the column on which data is to be slit
                            string strSplitValuesDataType, strSplitOnColumn, strOperator;
                            strSplitValuesDataType = row[MyTableList.BCP_SPLIT_VALUE_TYPE].ToString();
                            strSplitOnColumn = row[MyTableList.BCP_SPLIT_COLUMN].ToString();
                            strSplitOnColumn = UtilGeneral.GetQuotedString(strSplitOnColumn);

                            // Loop through multiple values on the BCP Split column
                            foreach (string strVal in lstSplitValues)
                            {

                                string strBCPSplitValue = strVal.Trim();

                                strBCP = "BCP ";
                                strBCP += "\"";
                                strBCP += "SELECT ";
                                strBCP += strBCPOverride;
                                strBCP += colstring;
                                strBCP += " FROM [" + SQLDwConfig.DatabaseName + "].[" + strSchemaName + "].[" + strTableName + "]";
                                strBCP += " WHERE ";

                                // if the column is split on Month or Year, then cover the outer boundaries as well with <= and >= opeartors
                                if (strSplitValuesDataType == Constants.BCP_SPLIT_TYPE_TIME_YEAR || strSplitValuesDataType == Constants.BCP_SPLIT_TYPE_TIME_MONTH 
                                    || strSplitValuesDataType == Constants.BCP_SPLIT_TYPE_TIME_INT_YEAR || strSplitValuesDataType == Constants.BCP_SPLIT_TYPE_TIME_INT_MONTH)
                                {
                                    // If it is the first value in the list then use the <= to cover all values before the current period
                                    if (strBCPSplitValue == lstSplitValues[0])
                                        strOperator = "<=";
                                    // If it is the last value in the list then use the >= to cover all values after the current period
                                    else if (strBCPSplitValue == lstSplitValues[lstSplitValues.Count - 1])
                                        strOperator = ">=";
                                    else
                                        // If the period is not the first and last period then split only based on the given period
                                        strOperator = "=";
                                }
                                else
                                {
                                    // If the BCP split is not on Month or Year then split data based on given value
                                    strOperator = "=";
                                }

                                string strColumn;
                                // If the date was integer date, then conver it to DATE format

                                switch (strSplitValuesDataType)
                                {
                                    case Constants.BCP_SPLIT_TYPE_TIME_INT_YEAR:
                                    case Constants.BCP_SPLIT_TYPE_TIME_INT_MONTH:
                                    case Constants.BCP_SPLIT_TYPE_TIME_INT_DATE:
                                        strColumn = "CONVERT(DATE, CONVERT(VARCHAR, " + strSplitOnColumn + "))";
                                        break;
                                    default:
                                        strColumn = strSplitOnColumn;
                                        break;
                                }

                                // Handle different date data types
                                switch (strSplitValuesDataType)
                                {
                                    // Simple, if it is not time type
                                    case Constants.BCP_SPLIT_TYPE_VALUE:
                                    // If Date, then split based on value
                                    case Constants.BCP_SPLIT_TYPE_TIME_DATE:
                                    case Constants.BCP_SPLIT_TYPE_TIME_INT_DATE:
                                        strBCP += strColumn + " " + strOperator + " '" + strBCPSplitValue + "' ";
                                        break;
                                    // If Year, then use the operator from the last step to cover <= and >= scenario
                                    case Constants.BCP_SPLIT_TYPE_TIME_YEAR:
                                    case Constants.BCP_SPLIT_TYPE_TIME_INT_YEAR:
                                        strBCP += "YEAR(" + strColumn + ") " + strOperator + " " + strBCPSplitValue;
                                        break;
                                    // If Month and operator is = then split based on Year and Month
                                    case Constants.BCP_SPLIT_TYPE_TIME_MONTH:
                                    case Constants.BCP_SPLIT_TYPE_TIME_INT_MONTH:
                                        if (strOperator == "=")
                                        {
                                            strBCP += "YEAR(" + strColumn + ") " + strOperator + " " + strBCPSplitValue.Substring(0, 4);
                                            strBCP += " AND MONTH(" + strColumn + ") " + strOperator + " " + strBCPSplitValue.Substring(4, 2);
                                        }
                                        // If Month and operator is not =, meaning it is first or last perid then use the <= or >= as the case is
                                        else
                                        {
                                            strBCP += "YEAR(" + strColumn + ") " + strOperator + " " + strBCPSplitValue.Substring(0, 4);
                                        }
                                        break;
                                }


                                strBCP += "\"";
                                strBCP += " queryout ";
                                strBCP += "\"";
                                strBCPFileTemp = strBCPFolder + strSchemaName + "_" + strTableName + "_" + strBCPSplitValue + Constants.FILE_EXTENSION_BCP_TEMP;
                                strBCPFileName = strBCPFileTemp.Replace(Constants.FILE_EXTENSION_BCP_TEMP, strBCPFileExt);
                                strBCP += strBCPFileTemp;
                                strBCP += "\"";
                                strBCP += strBCPOption;
                                ALScript.Add(@strBCP);
                                strBCP = "MOVE /y \"" + strBCPFileTemp + "\" \"" + strBCPFileName + "\"";
                                ALScript.Add(strBCP);
                            }
                            ALScript.Add("");
                        }
                        else
                        {
                            // This block handles scenario when whole table is output

                            strBCPFolder = SQLDwConfig.BCPOutputFolder + "\\Tables\\";
                            strBCP = "BCP ";
                            strBCP += "\"";
                            strBCP += "SELECT ";
                            strBCP += strBCPOverride;
                            strBCP += colstring;
                            strBCP += " FROM [" + SQLDwConfig.DatabaseName + "].[" + strSchemaName + "].[" + strTableName + "]";
                            strBCP += "\"";
                            strBCP += " queryout ";
                            strBCP += "\"";
                            strBCPFileTemp = strBCPFolder + strSchemaName + "_" + strTableName + Constants.FILE_EXTENSION_BCP_TEMP;
                            strBCPFileName = strBCPFileTemp.Replace(Constants.FILE_EXTENSION_BCP_TEMP, strBCPFileExt);
                            strBCP += strBCPFileTemp;
                            strBCP += "\"";
                            strBCP += strBCPOption;

                            ALScript.Add(@strBCP);
                            strBCP = "MOVE /y \"" + strBCPFileTemp + "\" \"" + strBCPFileName + "\"";
                            ALScript.Add(strBCP);
                            ALScript.Add("");
                        }
                    }
                }
                GenerateFile(strFileURL, ALScript);

                if(ReturnScriptURL)
                {
                    return strFileURL;
                }

                return "";

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }



        /// <summary>
        /// The routine generates Insert Script from External Table to SQL DW Table
        /// </summary>
        /// <param name="TableList"></param>
        public string GenerateInsertFromExternalTable(MyTableList TableList, bool ReturnScriptFlag)
        {
            string strSQL;
            long RowCount;
            ArrayList ALScript = new ArrayList();

            string strSchemaName, strTableName;
            string strFileURL;
            // Specify the file name for the Script
            strFileURL = OutputFolder + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", ".INSERT.sql");

            try
            {
                DataTable dtTable = TableList.GetTableList();

                foreach (DataRow row in dtTable.Rows)
                {
                    if ((string)row[MyTableList.SELECT].ToString() == Boolean.FalseString || (string)row[MyTableList.SELECT].ToString() == "") continue;

                    strSchemaName = row[MyTableList.SCHEMA_NAME].ToString();
                    strTableName = row[MyTableList.TABLE_NAME].ToString();
                    RowCount = Convert.ToInt64(row[MyTableList.TOTAL_ROWS].ToString());

                    // Generate scripts together for both INSERT and SELECT part
                    string colstringINSERTTable, colstringSELECTTable;
                    MyColumnList ColumnList = new MyColumnList(strSchemaName, strTableName, SQLDwConfig);

                    // Check if CRLF special character is to be handled. INSERT remains same. SELECT will revert back the <CR><LF> to CRLF
                    if (row[MyTableList.REPLACE_CRLF].ToString() == Constants.YES)
                    {
                        colstringINSERTTable = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_INSERT);
                        colstringSELECTTable = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_SELECT_WITH_CRLF_REVERT);
                    }
                    else
                    {
                        colstringINSERTTable = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_INSERT);
                        colstringSELECTTable = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_SELECT);
                    }

                    strSQL = "";
                    // If no records in table than generate a comment, if not generate a script
                    if (RowCount == 0)
                    {
                        ALScript.Add("/* 0 Rows for table [" + strSchemaName + "].[" + strTableName + "] */");
                    }
                    else
                    {
                        string strTableNameExternal;
                        strTableNameExternal = Constants.EXTERNAL_TABLE_PREFIX + strTableName;
                        strSQL += "INSERT INTO ";
                        strSQL += UtilGeneral.GetQuotedString(strSchemaName) + "." + UtilGeneral.GetQuotedString(strTableName) ;
                        strSQL += "(";
                        strSQL += colstringINSERTTable;
                        strSQL += ")";
                        strSQL += Environment.NewLine + "SELECT ";
                        strSQL += colstringSELECTTable;
                        strSQL += " FROM " + UtilGeneral.GetQuotedString(strSchemaName) + "." + UtilGeneral.GetQuotedString(strTableNameExternal) + " ";
                        ALScript.Add(@strSQL);
                    }
                    ALScript.Add("");
                }
                GenerateFile(strFileURL, ALScript);

                string strReturn = "";
                if (ReturnScriptFlag)
                    strReturn = string.Join(Environment.NewLine, ALScript.ToArray());

                return strReturn;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string GenerateMergeSQL(MyTableList TableList, bool ReturnScriptFlag)
        {
            string strSQL;
            ArrayList ALScript = new ArrayList();

            string strFileURL;
            // Specify the file name for the Script
            strFileURL = OutputFolderSQLScripts + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", ".MERGE.sql");

            string strHasColumn = UtilGeneral.GetQuotedString(Constants.MERGE_HASH_COL);

            try
            {
                DataTable dtTable = TableList.GetTableList();

                foreach (DataRow row in dtTable.Rows)
                {
                    if ((string)row[MyTableList.SELECT].ToString() == Boolean.FalseString || (string)row[MyTableList.SELECT].ToString() == "") continue;

                    string strSchemaName, strTableName;
                    strSchemaName = row[MyTableList.SCHEMA_NAME].ToString();
                    strTableName = row[MyTableList.TABLE_NAME].ToString();
                    MyColumnList ColumnList = new MyColumnList(strSchemaName, strTableName, SQLDwConfig);

                    strTableName = Constants.SQL_SCRIPT_SOURCE + "_" + row[MyTableList.TABLE_NAME].ToString();

                    string strMergeSQL;
                    strMergeSQL = "CREATE OR ALTER PROCEDURE ";
                    strMergeSQL += Constants.SCHEMA_PERSISTENT + "." + "sp_Load_" + strTableName + " AS";
                    ALScript.Add(strMergeSQL);
                    strMergeSQL = "MERGE " + Constants.SCHEMA_PERSISTENT + "." + strTableName + " AS " + Constants.MERGE_TARGET_ALIAS;
                    ALScript.Add(strMergeSQL);
                    ALScript.Add("USING (");

                    string strColList;
                    strColList = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_MERGE_SRC_SELECT);

                    ALScript.Add(Constants.TAB + "SELECT ");
                    ALScript.Add(Constants.TAB + Constants.TAB + strColList);
                    ALScript.Add(Constants.TAB + Constants.TAB + ",HASHBYTES('" + Constants.HASH_ALGORITHM + "', CONCAT(" + strColList + ")) AS " + Constants.MERGE_HASH_COL);
                    ALScript.Add(Constants.TAB + "FROM " + Constants.SCHEMA_STAGE + "." + strTableName);
                    ALScript.Add(") AS " + Constants.MERGE_SOURCE_ALIAS);

                    string strKeyJoinSQL;
                    strKeyJoinSQL = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_MERGE_KEY_COL_JOIN, Constants.MERGE_SOURCE_ALIAS, Constants.MERGE_TARGET_ALIAS);
                    ALScript.Add("ON (");
                    ALScript.Add(strKeyJoinSQL);
                    ALScript.Add(")");

                    ALScript.Add("WHEN MATCHED");
                    ALScript.Add(Constants.TAB + "AND " + Constants.MERGE_TARGET_ALIAS + "." + strHasColumn + " <> " + Constants.MERGE_SOURCE_ALIAS + "." + strHasColumn);
                    ALScript.Add(Constants.TAB + "THEN UPDATE SET");
                    string strUpdateSet;
                    strUpdateSet = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_MERGE_COL_UPDATE, Constants.MERGE_SOURCE_ALIAS, Constants.MERGE_TARGET_ALIAS);
                    ALScript.Add(strUpdateSet);

                    ALScript.Add("WHEN NOT MATCHED THEN");
                    ALScript.Add(Constants.TAB + "INSERT (");
                    string colstringINSERTTable;
                    colstringINSERTTable = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_INSERT);
                    colstringINSERTTable += ", " + strHasColumn;
                    ALScript.Add(Constants.TAB + Constants.TAB + colstringINSERTTable);
                    ALScript.Add(Constants.TAB + ")");
                    string strInsertCol;
                    ALScript.Add(Constants.TAB + "VALUES (");
                    strInsertCol = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_MERGE_COL_INSERT, Constants.MERGE_SOURCE_ALIAS);
                    strInsertCol += ", " + Constants.MERGE_SOURCE_ALIAS + "." + strHasColumn;
                    ALScript.Add(Constants.TAB + Constants.TAB + strInsertCol);
                    ALScript.Add(Constants.TAB + ")");
                    ALScript.Add("WHEN NOT MATCHED BY SOURCE");
                    ALScript.Add(Constants.TAB + "THEN DELETE");
                    ALScript.Add(";");
                    ALScript.Add("GO");



                    ALScript.Add("");
                    ALScript.Add("");
                    ALScript.Add("");
                }
                GenerateFile(strFileURL, ALScript);

                string strReturn = "";
                if (ReturnScriptFlag)
                    strReturn = string.Join(Environment.NewLine, ALScript.ToArray());

                return strReturn;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GenerateAlterTableForPSA(MyTableList TableList, bool ReturnScriptFlag)
        {
            string strSQL;
            ArrayList ALScript = new ArrayList();

            string strFileURL;
            // Specify the file name for the Script
            strFileURL = OutputFolderSQLScripts + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", ".ALTERPSA.sql");

            string strHasColumn = UtilGeneral.GetQuotedString(Constants.MERGE_HASH_COL);

            try
            {
                DataTable dtTable = TableList.GetTableList();

                foreach (DataRow row in dtTable.Rows)
                {
                    if ((string)row[MyTableList.SELECT].ToString() == Boolean.FalseString || (string)row[MyTableList.SELECT].ToString() == "") continue;

                    string strSchemaName, strTableName;
                    strSchemaName = row[MyTableList.SCHEMA_NAME].ToString();
                    strTableName = row[MyTableList.TABLE_NAME].ToString();
                    MyColumnList ColumnList = new MyColumnList(strSchemaName, strTableName, SQLDwConfig);

                    strTableName = Constants.SCHEMA_PERSISTENT + "." + Constants.SQL_SCRIPT_SOURCE + "_" + row[MyTableList.TABLE_NAME].ToString();

                    string strColList;
                    strColList = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_MERGE_SRC_SELECT);

                    ALScript.Add("ALTER TABLE " + strTableName + " DROP COLUMN " + Constants.MERGE_HASH_COL);
                    ALScript.Add("ALTER TABLE " + strTableName + " ADD " + Constants.MERGE_HASH_COL + " BINARY(64)");
                    ALScript.Add("UPDATE " + strTableName + " SET " + Constants.MERGE_HASH_COL + " = " + " HASHBYTES('" + Constants.HASH_ALGORITHM + "', CONCAT(" + strColList + "))");
                    ALScript.Add("GO");


                    ALScript.Add("");
                }
                GenerateFile(strFileURL, ALScript);

                string strReturn = "";
                if (ReturnScriptFlag)
                    strReturn = string.Join(Environment.NewLine, ALScript.ToArray());

                return strReturn;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Generate a script for emtpy file header. The script will generate 1 file for each table in the subfolder FileHeader
        /// </summary>
        /// <param name="TableList"></param>
        public void GenerateFileHeaderWithColumnNames (MyTableList TableList)
        {

            try
            {
                DataTable dtTable = TableList.GetTableList();
                string strOutputFolder;
                strOutputFolder = OutputFolder + "\\" + "FileHeader";

                if (!Directory.Exists(strOutputFolder))
                    Directory.CreateDirectory(strOutputFolder);

                foreach (DataRow row in dtTable.Rows)
                {
                    if ((string)row[MyTableList.SELECT].ToString() == Boolean.FalseString || (string)row[MyTableList.SELECT].ToString() == "") continue;

                    string strSchemaName, strTableName;
                    strSchemaName = row[MyTableList.SCHEMA_NAME].ToString();
                    strTableName = row[MyTableList.TABLE_NAME].ToString();

                    string strFileURL, strHeaderFileName;
                    strHeaderFileName = strSchemaName + "." + strTableName + ".txt";
                    strFileURL = strOutputFolder + "\\" + strHeaderFileName;

                    string colstring;
                    MyColumnList ColumnList = new MyColumnList(strSchemaName, strTableName, SQLDwConfig);
                    colstring = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_FILE_HEADER);

                    ArrayList ALScript = new ArrayList();
                    ALScript.Add(colstring);

                    GenerateFile(strFileURL, ALScript);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Generate a generate SELECT script for each table. 1 file will be generated for each file in the Subfolder SQL
        /// </summary>
        /// <param name="TableList"></param>
        public void GenerateTableSELECTFile(MyTableList TableList)
        {

            try
            {
                DataTable dtTable = TableList.GetTableList();
                string strOutputFolder;
                strOutputFolder = OutputFolder + "\\" + "SQL";

                if (!Directory.Exists(strOutputFolder))
                    Directory.CreateDirectory(strOutputFolder);

                foreach (DataRow row in dtTable.Rows)
                {
                    if ((string)row[MyTableList.SELECT].ToString() == Boolean.FalseString || (string)row[MyTableList.SELECT].ToString() == "") continue;

                    string strSchemaName, strTableName;
                    strSchemaName = row[MyTableList.SCHEMA_NAME].ToString();
                    strTableName = row[MyTableList.TABLE_NAME].ToString();

                    string strFileURL, strHeaderFileName;
                    strHeaderFileName = strSchemaName + "." + strTableName + ".sql";
                    strFileURL = strOutputFolder + "\\" + strHeaderFileName;

                    string colstring, strSQL;
                    MyColumnList ColumnList = new MyColumnList(strSchemaName, strTableName, SQLDwConfig);

                    if (row[MyTableList.REPLACE_CRLF].ToString() == Constants.YES)
                        colstring = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_SELECT_WITH_CRLF_REPLACE);
                    else
                        colstring = ColumnList.GetColumnListSQL(MyColumnList.COL_SCRIPT_TYPE_SELECT);

                    strSQL = "SELECT " + colstring + " FROM [" + SQLDwConfig.DatabaseName + "].[" + strSchemaName + "].[" + strTableName + "]";

                    ArrayList ALScript = new ArrayList();
                    ALScript.Add(strSQL);

                    GenerateFile(strFileURL, ALScript);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Generate SQL Script for Azure Preprartion. Create DB master Key, Credential, BLOB Source and File Format
        /// </summary>
        public string GenerateAzurePrepSQL(bool ReturnScriptFlag)
        {
            ArrayList ALScript = new ArrayList();

            string strSQL;
            string strStorageLocation;

            ALScript.Add("--Cleanup");
            ALScript.Add("/*");
            ALScript.Add("--Ensure no External table is linked to Data Source or File Format, or else the Drop fails.");
            strSQL = "IF EXISTS (SELECT * FROM sys.external_file_formats WHERE name = '" + Properties.Settings.Default.AzFileFormatCompressed + "')";
            ALScript.Add(strSQL);
            strSQL = "DROP EXTERNAL FILE FORMAT " + Properties.Settings.Default.AzFileFormatCompressed + " ";
            ALScript.Add(strSQL);
            ALScript.Add("");
            strSQL = "IF EXISTS (SELECT * FROM sys.external_data_sources WHERE name = '" + Properties.Settings.Default.AzBLOBDataSourceName + "')";
            ALScript.Add(strSQL);
            strSQL = "DROP EXTERNAL DATA SOURCE " + Properties.Settings.Default.AzBLOBDataSourceName + " ";
            ALScript.Add(strSQL);
            ALScript.Add("");
            strSQL = "IF EXISTS (SELECT * FROM sys.database_credentials WHERE name='" + Properties.Settings.Default.AzCredentialName + "')";
            ALScript.Add(strSQL);
            strSQL = "DROP DATABASE SCOPED CREDENTIAL " + Properties.Settings.Default.AzCredentialName + " ";
            ALScript.Add(strSQL);
            ALScript.Add("*/");
            ALScript.Add("");
            ALScript.Add("");


            ALScript.Add("--A: Create database Master Key");
            ALScript.Add("--Only necessary if one does not already exist.");
            ALScript.Add("--Required to encrypt the credential secret in the next step.");
            ALScript.Add("--Password not compulsary for Azure platform.");
            ALScript.Add("IF NOT EXISTS (SELECT * FROM sys.symmetric_keys)");
            ALScript.Add("CREATE MASTER KEY");
            ALScript.Add("");

            ALScript.Add("--B: Create a Database scoped credential");
            ALScript.Add("--IDENTITY: Provide any string, it is not used for authentication to Azure storage.");
            ALScript.Add("--SECRET: Provide your Azure storage account key.");
            ALScript.Add("IF NOT EXISTS (SELECT * FROM sys.database_credentials WHERE name='" + Properties.Settings.Default.AzCredentialName + "')");
            ALScript.Add("CREATE DATABASE SCOPED CREDENTIAL " + Properties.Settings.Default.AzCredentialName + " ");
            ALScript.Add("WITH IDENTITY = 'AzSqlDW_Identity',");
            ALScript.Add("SECRET = '" + SQLDwConfig.AzStorageKey + "'");
            ALScript.Add("");

            strStorageLocation = "'wasbs://" + SQLDwConfig.AzContainer + "@" + SQLDwConfig.AzStorageAccount + ".blob.core.windows.net'";
            ALScript.Add("--C: Create an external data source");
            ALScript.Add("IF NOT EXISTS (SELECT * FROM sys.external_data_sources WHERE name = '" + Properties.Settings.Default.AzBLOBDataSourceName + "')");
            ALScript.Add("CREATE EXTERNAL DATA SOURCE " + Properties.Settings.Default.AzBLOBDataSourceName + " WITH ");
            ALScript.Add("(");
            ALScript.Add(TAB + "TYPE = HADOOP, ");
            ALScript.Add(TAB + "LOCATION = " + strStorageLocation + ",");
            ALScript.Add(TAB + "CREDENTIAL = " + Properties.Settings.Default.AzCredentialName + "");
            ALScript.Add(");");
            ALScript.Add("");

            
            ALScript.Add("--D: Create external file format - Gz");
            ALScript.Add("IF NOT EXISTS (SELECT * FROM sys.external_file_formats WHERE name = '" + Properties.Settings.Default.AzFileFormatCompressed + "')");
            ALScript.Add("CREATE EXTERNAL FILE FORMAT " + Properties.Settings.Default.AzFileFormatCompressed + " WITH ");
            ALScript.Add("(");
            ALScript.Add(TAB + "FORMAT_TYPE = DELIMITEDTEXT, ");
            ALScript.Add(TAB + "FORMAT_OPTIONS (FIELD_TERMINATOR = '" + Properties.Settings.Default.ColumnDelimiter + "'),");
            ALScript.Add(TAB + "DATA_COMPRESSION = 'org.apache.hadoop.io.compress.GzipCodec' ");
            ALScript.Add(");");

            string strFileURL;
            strFileURL = OutputFolder + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", ".AzurePreparation.sql");
            GenerateFile(strFileURL, ALScript);

            string strReturn = "";
            if (ReturnScriptFlag)
                strReturn = string.Join(Environment.NewLine, ALScript.ToArray());

            return strReturn;



        }

        /// <summary>
        /// Generate Powershell script to convert Unicode file to UTF8
        /// </summary>
        public string GeneratePSFileUTF8 (bool ReturnScriptFlag)
        {
            ArrayList ALScript = new ArrayList();
            string strFileURL;
            strFileURL = OutputFolder + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", ".UTF8.ps1");

            try
            {
                ALScript.Add("Param(");
                // Optional Parameters for File Name/File Filter and Root Folder where the txt files are available
                ALScript.Add("[string]$FileFilter = \"*" + Constants.FILE_EXTENSION_BCP_MAIN_2014_BELOW + "\",");
                ALScript.Add("[string]$RootFolder = \"" + SQLDwConfig.BCPOutputFolder + "\"");
                ALScript.Add(")");
                ALScript.Add("");
                ALScript.Add("$ErrorLogFile = $RootFolder + \"\\AzUTF8Error.log\"");
                ALScript.Add("$LogFile = $RootFolder + \"\\AzUTF8.log\"");
                ALScript.Add("try");
                ALScript.Add("{");
                // Loop through all the files in the subfolder
                ALScript.Add(TAB + "foreach($file in Get-ChildItem -Path $RootFolder -Force -Recurse -Filter $FileFilter)");
                ALScript.Add(TAB + "{");
                ALScript.Add(TAB + TAB + "#Skip, if the file is already in UTF8. This may be required when invalid file name is passed as paramter.");
                ALScript.Add(TAB + TAB + "If ($file.Extension -eq \"" + Constants.FILE_EXTENSION_UTF8_MAIN + "\") {continue}");
                ALScript.Add(TAB + TAB + "");
                ALScript.Add(TAB + TAB + "$fileNameTEMP = $file.FullName + \"" + Constants.FILE_EXTENSION_UTF8_TEMP + "\"");
                ALScript.Add(TAB + TAB + "$fileNameUTF8 = $file.FullName + \"" + Constants.FILE_EXTENSION_UTF8_MAIN + "\"");
                ALScript.Add(TAB + TAB + "$fileNameProcessed = $file.FullName + \".PROCESSED\"");
                ALScript.Add(TAB + TAB + "$fileNameProcessedEmpty = $file.FullName + \".EMPTY.PROCESSED\"");
                ALScript.Add(TAB + TAB + "");
                ALScript.Add(TAB + TAB + "#Remove the .EMPTY.PROCESSED file if it exists at the beginning");
                ALScript.Add(TAB + TAB + "If (Test-Path $fileNameTEMP)");
                ALScript.Add(TAB + TAB + "{");
                ALScript.Add(TAB + TAB + TAB + "Remove-Item $fileNameProcessedEmpty");
                ALScript.Add(TAB + TAB + "}");
                ALScript.Add(TAB + TAB + "");
                ALScript.Add(TAB + TAB + "$LogMsg = (Get-Date).toString(\"yyyy/MM/dd HH:mm:ss\") + \" : Started converting  : \" + $file.Name");
                ALScript.Add(TAB + TAB + "Add-Content $LogFile -value $LogMsg");
                ALScript.Add(TAB + TAB + "");
                ALScript.Add(TAB + TAB + "Get-Content $file.FullName -Encoding UNICODE | Set-Content $fileNameTEMP -Encoding UTF8");
                ALScript.Add(TAB + TAB + "");
                ALScript.Add(TAB + TAB + "If (Test-Path $fileNameTEMP)");
                ALScript.Add(TAB + TAB + "{");
                ALScript.Add(TAB + TAB + TAB + "#Rename the .TEMP file to .UTF8 file");
                ALScript.Add(TAB + TAB + TAB + "Move-Item -Path $fileNameTEMP -Destination $fileNameUTF8 -Force");
                ALScript.Add(TAB + TAB + TAB + "#Rename the original file to .PROCESSED");
                ALScript.Add(TAB + TAB + TAB + "Move-Item -Path $file.FullName -Destination $fileNameProcessed -Force");
                ALScript.Add(TAB + TAB + "}");
                ALScript.Add(TAB + TAB + "Else");
                ALScript.Add(TAB + TAB + "{");
                ALScript.Add(TAB + TAB + TAB + "#Empty File");
                ALScript.Add(TAB + TAB + TAB + "Out-File $fileNameUTF8 -Encoding UTF8");
                ALScript.Add(TAB + TAB + TAB + "#Rename the original file to .EMPTY.PROCESSED");
                ALScript.Add(TAB + TAB + TAB + "Move-Item -Path $file.FullName -Destination $fileNameProcessed -Force");
                ALScript.Add(TAB + TAB + TAB + "Move-Item -Path $fileNameProcessed -Destination $fileNameProcessedEmpty -Force");
                ALScript.Add(TAB + TAB + "}");
                ALScript.Add(TAB + TAB + "");
                ALScript.Add(TAB + TAB + "$LogMsg = (Get-Date).toString(\"yyyy/MM/dd HH:mm:ss\") + \" : Finished converting : \" + $file.Name");
                ALScript.Add(TAB + TAB + "Add-Content $LogFile -value $LogMsg");
                ALScript.Add(TAB + TAB + "");
                ALScript.Add(TAB + "}");
                ALScript.Add("}");
                ALScript.Add("catch");
                ALScript.Add("{");
                ALScript.Add(TAB + "$ErrMsg = (Get-Date).toString(\"yyyy/MM/dd HH:mm:ss\") + \" : \" + $_.Exception.Message");
                ALScript.Add(TAB + "Add-Content $ErrorLogFile -value $ErrMsg");
                ALScript.Add(TAB + "Exit 99");
                ALScript.Add("}");
                GenerateFile(strFileURL, ALScript);

                string strReturn = "";
                if (ReturnScriptFlag)
                    strReturn = string.Join(Environment.NewLine, ALScript.ToArray());

                return strReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Generate powershell script to compress the UTF8 files in the gzip format
        /// </summary>
        public string GeneratePSFileZip(bool ReturnScriptFlag)
        {
            ArrayList ALScript = new ArrayList();
            string strFileURL;
            strFileURL = OutputFolder + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", ".ZIP.ps1");

            try
            {
                // Optional Parameters for File Name/File Filter and Root Folder where the UTF8 files are available
                ALScript.Add("Param(");
                ALScript.Add("[string]$FileFilter = \"*" + Constants.FILE_EXTENSION_UTF8_MAIN + "\",");
                ALScript.Add("[string]$RootFolder = \"" + SQLDwConfig.BCPOutputFolder + "\"");
                ALScript.Add(")");
                ALScript.Add("");
                ALScript.Add("$ErrorLogFile = $RootFolder + \"\\AzZipError.log\"");
                ALScript.Add("$LogFile = $RootFolder + \"\\AzZip.log\"");
                ALScript.Add("$7Zip = \"" + SQLDwConfig.File7Zip + "\"");
                ALScript.Add("try");
                ALScript.Add("{");
                // Loop through all the files in the folder
                ALScript.Add(TAB + "foreach($file in Get-ChildItem -Path $RootFolder -Force -Recurse -Filter $FileFilter)");
                ALScript.Add(TAB + "{");
                ALScript.Add(TAB + TAB + "#Skip, if the file is already compressed. This may be required when invalid file name is passed as paramter.");
                ALScript.Add(TAB + TAB + "If ($file.Extension -eq \"" + Constants.FILE_EXTENSION_GZ_MAIN + "\") {continue}");
                ALScript.Add(TAB + TAB + "");
                ALScript.Add(TAB + TAB + "$fileNameTEMP = $file.FullName + \"" + Constants.FILE_EXTENSION_GZ_TEMP + "\"");
                ALScript.Add(TAB + TAB + "$fileNameGz = $RootFolder + \"\\" + Constants.SUB_FOLDER_GZ + "\\\" + $file.Directory.Name + \"\\\" + $file.Name + \"" + Constants.FILE_EXTENSION_GZ_MAIN + "\"");
                ALScript.Add(TAB + TAB + "$fileNameProcessed = $file.FullName + \".PROCESSED\"");
                ALScript.Add(TAB + TAB + "");
                ALScript.Add(TAB + TAB + "$LogMsg = (Get-Date).toString(\"yyyy/MM/dd HH:mm:ss\") + \" : Started compressing  : \" + $file.Name");
                ALScript.Add(TAB + TAB + "Add-Content $LogFile -value $LogMsg");
                ALScript.Add(TAB + TAB + "");
                ALScript.Add(TAB + TAB + "& $7Zip a -tgzip $fileNameTEMP $file.FullName");
                ALScript.Add(TAB + TAB + "");

                ALScript.Add(TAB + TAB + "If (Test-Path $fileNameTEMP)");
                ALScript.Add(TAB + TAB + "{");
                ALScript.Add(TAB + TAB + TAB + "#Rename the file if compression was successful");
                ALScript.Add(TAB + TAB + TAB + "New-Item -ItemType File -Path $fileNameGz -Force");
                ALScript.Add(TAB + TAB + TAB + "Move-Item -Path $fileNameTEMP -Destination $fileNameGz -Force");
                ALScript.Add(TAB + TAB + TAB + "Move-Item -Path $file.FullName -Destination $fileNameProcessed -Force");
                ALScript.Add(TAB + TAB + "}");
                ALScript.Add(TAB + TAB + "");
                ALScript.Add(TAB + TAB + "$LogMsg = (Get-Date).toString(\"yyyy/MM/dd HH:mm:ss\") + \" : Finished compressing : \" + $file.Name");
                ALScript.Add(TAB + TAB + "Add-Content $LogFile -value $LogMsg");
                ALScript.Add(TAB + TAB + "");
                ALScript.Add(TAB + "}");
                ALScript.Add("}");
                ALScript.Add("catch");
                ALScript.Add("{");
                ALScript.Add(TAB + "$ErrMsg = (Get-Date).toString(\"yyyy/MM/dd HH:mm:ss\") + \" : \" + $_.Exception.Message");
                ALScript.Add(TAB + "Add-Content $ErrorLogFile -value $ErrMsg");
                ALScript.Add(TAB + "Exit 99");
                ALScript.Add("}");
                GenerateFile(strFileURL, ALScript);

                string strReturn = "";
                if (ReturnScriptFlag)
                    strReturn = string.Join(Environment.NewLine, ALScript.ToArray());

                return strReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Generate Batch Script file for Azure Copy to copy the files to BLOB Storage
        /// </summary>
        public string GenerateAZCopyFile(bool ReturnScriptURL)
        {
            ArrayList ALScript = new ArrayList();
            string strFileURL;
            strFileURL = OutputFolder + "\\" + SQLDwConfig.ConfigFileName.Replace(".Config", ".AZCopy.bat");

            string strLogFile = SQLDwConfig.BCPOutputFolder + "\\" + "AzureCopy.log";

            try
            {
                string strSASKey = System.Net.WebUtility.HtmlDecode(SQLDwConfig.AzSASToken);
                strSASKey = strSASKey.Replace("%", "%%");
                string strSASURL = "https://" + SQLDwConfig.AzStorageAccount + ".blob.core.windows.net/" + SQLDwConfig.AzContainer + strSASKey;

                string strAZCopy;
                strAZCopy = "\"" + SQLDwConfig.FileAZCopy + "\"";
                strAZCopy += " make";
                strAZCopy += " \"" + strSASURL + "\"";
                ALScript.Add(strAZCopy);

                ALScript.Add("");

                strAZCopy = "\"" + SQLDwConfig.FileAZCopy + "\"";
                strAZCopy += " cp";
                strAZCopy += " \"" + SQLDwConfig.BCPOutputFolder + "\\" + Constants.SUB_FOLDER_GZ + "\\*\"";
                strAZCopy += " \"" + strSASURL + "\"";
                strAZCopy += " --recursive";
                /*
                    strAZCopy += " /pattern:%FilePattern%"; // Specify the default file format at gzip compressed
                    strAZCopy += " /NC:4";
                    strAZCopy += " /S"; // Loop through all subfoldes
                    strAZCopy += " /Y"; // Suppress any prompts
                    strAZCopy += " /XO"; // Only copy new files
                    strAZCopy += " /V:\"" + strLogFile + "\""; // Specify a log file
                */
                ALScript.Add(strAZCopy);
                GenerateFile(strFileURL, ALScript);

                string strReturn = "";
                if (ReturnScriptURL)
                    strReturn = strFileURL;

                return strReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Internal Routing to generate the file from the Array List
        /// </summary>
        /// <param name="FileURL">File URL with File Name</param>
        /// <param name="ALFile"></param>
        private void GenerateFile(string FileURL, ArrayList ALFile)
        {
            try
            {
                File.WriteAllLines(FileURL, ALFile.Cast<string>());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Internal Routine to generate the list of Create Schema from a Table List
        /// </summary>
        /// <param name="tblList"></param>
        /// <param name="alScript"></param>
        private void GenerateSchemaList(MyTableList tblList, ArrayList alScript)
        {
            DataTable dtSchema = tblList.GetDistinctColumnValues(MyTableList.SCHEMA_NAME);
            foreach (DataRow row in dtSchema.Rows)
            {
                alScript.Add(GenerateSchema(row[0].ToString() ));
                alScript.Add("");
            }
            alScript.Add("");
        }

        private string GenerateSchema(string strSchemaName)
        {
            string strSchemaSQL;
            strSchemaSQL = "IF NOT EXISTS(SELECT * FROM sys.schemas WHERE NAME = '" + strSchemaName + "')" + Environment.NewLine;
            strSchemaSQL += "EXEC sp_executesql N'CREATE SCHEMA [" + strSchemaName + "]'" + Environment.NewLine;

            return strSchemaSQL;
        
        }

        private string GetTableExistsSQL(string SchemaName, string TableName)
        {
            string strSQL;
            strSQL = "SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'" + SchemaName + "." + TableName + "') AND type IN ('U')";

            return strSQL;
        }

    }
}
