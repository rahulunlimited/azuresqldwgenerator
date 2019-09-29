using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDwGenerator
{
    static class Constants
    {
        public const string DISTRIBUTION_TYPE_HASH = "HASH";
        public const string DISTRIBUTION_TYPE_ROUND_ROBIN = "ROUND_ROBIN";
        public const string DISTRIBUTION_TYPE_REPLICATE = "REPLICATE";

        public const int MAX_CHARACTER_NUMBER = 8000;

        public const string INDEX_TYPE_COLUMNSTORE = "CLUSTERED COLUMNSTORE INDEX";
        public const string INDEX_TYPE_CLUSTERED = "CLUSTERED INDEX";
        public const string INDEX_TYPE_HEAP = "HEAP";

        public const string DB_AUTHENTICATION_INTEGRATED = "Integrated";
        public const string DB_AUTHENTICATION_SQL = "SQL";

        public const string YES = "Yes";
        public const string NO = "No";

        public const string TAB = "\t";

        public const string DATA_TYPE_INT = "int";

        public const string SUB_FOLDER_SETTINGS = "Settings";
        public const string SUB_FOLDER_LIST = "Lists";
        public const string SUB_FOLDER_SCRIPTS = "Scripts";
        public const string SUB_FOLDER_SQLDW_CONN = "SQLDWConnections";
        public const string SUB_FOLDER_GZ = "DWExport";

        public const string FOLDER_CONNECTION_TYPE = "SQLConfiguration";
        public const string FILE_TABLE_LIST = "TableList.sql";
        public const string FILE_COLUMN_LIST = "ColumnList.sql";

        public const string FILE_EXTENSION_BCP_MAIN = ".txt.UTF8";
        public const string FILE_EXTENSION_BCP_MAIN_2014_BELOW = ".txt";
        public const string FILE_EXTENSION_BCP_TEMP = ".BCP.TEMP";
        public const string FILE_EXTENSION_UTF8_MAIN = ".UTF8";
        public const string FILE_EXTENSION_UTF8_TEMP = ".UTF8.TEMP";
        public const string FILE_EXTENSION_GZ_MAIN = ".gz";
        public const string FILE_EXTENSION_GZ_TEMP = ".gz.TEMP";

        public const string SCRIPT_TYPE_EXTERNAL_TABLE_CREATE = "Create External Table";
        public const string SCRIPT_TYPE_DWH_TABLE_CREATE = "Create Data Warehouse Table";
        public const string SCRIPT_TYPE_BCP = "BCP Script";
        public const string SCRIPT_TYPE_INSERT_FROM_EXT_TABLE = "Insert from External Table";
        public const string SCRIPT_TYPE_DWH_TABLE_TRUNCATE = "Truncate Data Warehouse Table";
        public const string SCRIPT_TYPE_EXTERNAL_TABLE_DROP = "Drop External Table";
        public const string SCRIPT_TYPE_DWH_TABLE_DROP = "Drop Data Warehouse Table";
        public const string SCRIPT_TYPE_AZURE = "Azure Scripts";
        public const string SCRIPT_TYPE_ALL = "ALL SCRIPTS";
        public const string SCRIPT_TYPE_DWH_TABLE_CREATE_DROP = "Re-Create Data Warehouse Table";
        public const string SCRIPT_TYPE_EXTERNAL_TABLE_CREATE_DROP = "Re-Create External Table";
        public const string SCRIPT_TYPE_TABLE_REFACTOR = "Refactor Table";
        public const string SCRIPT_TYPE_BCP_EMPTY_FILE = "BCP Script for Empty Files";
        public const string SCRIPT_TYPE_FILE_HEADER = "File Header";
        public const string SCRIPT_TYPE_SELECT_SQL = "SELECT SQL";

        public const string TABLE_TYPE_EXTERNAL = "EXTERNAL";
        public const string TABLE_TYPE_DWH = "DWH";

        public const string EXTERNAL_TABLE_PREFIX = "EXT_";

        public const int DATE_TYPE_YEAR = 1;
        public const int DATE_TYPE_MONTH = 2;
        public const int DATE_TYPE_DAY = 3;

        public const string BCP_OUT_FORMAT_UTF8 = "UTF8";
        public const string BCP_OUT_FORMAT_Unicode = "Unicode";

        public const string BCP_SPLIT_TYPE_VALUE = "V";
        public const string BCP_SPLIT_TYPE_TIME_YEAR = "Y";
        public const string BCP_SPLIT_TYPE_TIME_INT_YEAR = "I-Y";
        public const string BCP_SPLIT_TYPE_TIME_MONTH = "M";
        public const string BCP_SPLIT_TYPE_TIME_INT_MONTH = "I-M";
        public const string BCP_SPLIT_TYPE_TIME_DATE = "D";
        public const string BCP_SPLIT_TYPE_TIME_INT_DATE = "I-D";


        public const string MIGRATE_STEP_BCP_DATA = "Generate BCP Data";
        public const string MIGRATE_STEP_UTF8_CONVERT = "Convert to UTF8";
        public const string MIGRATE_STEP_COMPRESS_FILES = "Compress Files";
        public const string MIGRATE_STEP_BLOB_UPLOAD = "Upload to BLOB";
        public const string MIGRATE_STEP_AZURE_ENV_PREPARE = "Prepare Azure SQL Environment";
        public const string MIGRATE_STEP_EXTERNAL_TABLE = "Create External Table";
        public const string MIGRATE_STEP_DATA_WAREHOUSE_TABLE = "Create Data Warehouse Table";
        public const string MIGRATE_STEP_INSERT = "Insert data from BLOB";



    }
}
