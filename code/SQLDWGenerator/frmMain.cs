using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLDwGenerator
{
    public partial class frmMain : Form
    {
        private MyTableList dtTable;
        private string CurrentConfigFileName;
        private MySettingsEnvironments SQLDwConfig;

        /// <summary>
        /// Initial routine for the form
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            this.Top = 0;
            this.Left = 0;

            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            tlpMain.Width = this.Width;
            dgvTables.Width = (int)(tlpMain.Width * 0.9);

            tlpMain.Height = this.Height - (sbrMain.Height * 5);
            dgvTables.Height = (int)(tlpMain.Height * 0.9);
            tlpRight.Height = (int)(tlpMain.Height * 0.9);

            this.Show();

            try
            {
                CurrentConfigFileName = UtilGeneral.GetConfigValue(MySettingsUserConfig.KEY_CONFIG_NAME);

                if (CurrentConfigFileName == "")
                    ShowConfigForm();
                else
                {
                    SQLDwConfig = new MySettingsEnvironments(CurrentConfigFileName);

                    if (!SQLDwConfig.ConfigFileExists())
                    {
                        UtilGeneral.ShowMessage("Configuration File do not exist. Please create a Configuration file to continue");
                        ShowConfigForm();
                    }
                    else
                    {
                        LoadData();
                    }
                }


                if (dgvTables.ColumnCount > 0)
                {

                    dgvTables.Columns[MyTableList.TABLE_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvTables.Columns[MyTableList.INDEX_TYPE].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvTables.Columns[MyTableList.BCP_SPLIT_VALUE_TYPE].Width = 30;
                }
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }
        }

        public void ShowConfigForm()
        {
            CurrentConfigFileName = UtilGeneral.GetConfigValue(MySettingsUserConfig.KEY_CONFIG_NAME);

            frmConfig f = new frmConfig(CurrentConfigFileName, this);
            f.ShowDialog();

        }

        /// <summary>
        /// This routine updates a new configuration file from Configuation file and loads new data for it
        /// </summary>
        /// <param name="NewConfig"></param>
        public void UpdateConfig(MySettingsEnvironments NewConfig)
        {
            CurrentConfigFileName = new MySettingsUserConfig().GetCurrentConfigurationName(MySettingsUserConfig.KEY_CONFIG_NAME);
            SQLDwConfig = NewConfig;
            LoadData();
        }

        /// <summary>
        /// Default Routine to update information on Form
        /// </summary>
        public void LoadData()
        {
            dtTable = new MyTableList(SQLDwConfig);
            this.dgvTables.DataSource = dtTable.GetTableList();


            cmbDataType.Items.Clear();
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_ALL);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_DWH_TABLE_CREATE);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_EXTERNAL_TABLE_CREATE);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_INSERT_FROM_EXT_TABLE);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_DWH_TABLE_DROP);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_EXTERNAL_TABLE_DROP);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_DWH_TABLE_TRUNCATE);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_AZURE);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_BCP);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_TABLE_REFACTOR);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_BCP_EMPTY_FILE);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_DWH_TABLE_CREATE_DROP);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_EXTERNAL_TABLE_CREATE_DROP);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_SELECT_SQL);
            cmbDataType.Items.Add(Constants.SCRIPT_TYPE_FILE_HEADER);
            cmbDataType.SelectedIndex = 0;

            this.sbpConfig.Text = CurrentConfigFileName;
            this.sbpServer.Text = SQLDwConfig.ServerName;
            this.sbpDatabase.Text = SQLDwConfig.DatabaseName;

            optSelectAll.Checked = true;

            this.dgvTables.Columns[MyTableList.SCHEMA_NAME].ReadOnly = true;
            this.dgvTables.Columns[MyTableList.TABLE_NAME].ReadOnly = true;
            this.dgvTables.Columns[MyTableList.TOTAL_ROWS].ReadOnly = true;
            this.dgvTables.Columns[MyTableList.DATA_SIZE_MB].ReadOnly = true;
            this.dgvTables.Columns[MyTableList.DISTRIBUTION_TYPE].ReadOnly = true;
            this.dgvTables.Columns[MyTableList.DISTRIBUTION_COLUMN].ReadOnly = true;
            this.dgvTables.Columns[MyTableList.INDEX_TYPE].ReadOnly = true;
            this.dgvTables.Columns[MyTableList.INDEX_COLUMN].ReadOnly = true;
            this.dgvTables.Columns[MyTableList.BCP_SPLIT].ReadOnly = true;
            this.dgvTables.Columns[MyTableList.BCP_SPLIT_COLUMN].ReadOnly = true;
            this.dgvTables.Columns[MyTableList.BCP_SPLIT_VALUES].ReadOnly = true;
            this.dgvTables.Columns[MyTableList.REPLACE_CRLF].ReadOnly = true;
            this.dgvTables.Columns[MyTableList.BCP_SPLIT_VALUE_TYPE].ReadOnly = true;
            this.dgvTables.Columns[MyTableList.SELECT].ReadOnly = false;

            //Initial formatting for DataGrid
            this.dgvTables.Columns[MyTableList.TOTAL_ROWS].DefaultCellStyle.Format = "#,##0";
            this.dgvTables.Columns[MyTableList.DATA_SIZE_MB].DefaultCellStyle.Format = "#,##0";
            this.dgvTables.Columns[MyTableList.TOTAL_ROWS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvTables.Columns[MyTableList.DATA_SIZE_MB].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvTables.Columns[MyTableList.TABLE_ID].Visible = false;


        }

        /// <summary>
        /// Public function called from Table Details form to update the currently edited Row
        /// </summary>
        /// <param name="strTableID"></param>
        /// <param name="strDistributionType"></param>
        /// <param name="strDistributionColumn"></param>
        /// <param name="strIndexType"></param>
        /// <param name="strIndexColumn"></param>
        /// <param name="strBCPSplit"></param>
        /// <param name="strBCPColumns"></param>
        /// <param name="strBCPValues"></param>
        /// <param name="strBCPValueType"></param>
        /// <param name="strReplaceCRLF"></param>
        public void UpdateRow(string strTableID, string strDistributionType, string strDistributionColumn, string strIndexType, string strIndexColumn, string strBCPSplit, string strBCPColumns, string strBCPValues, string strBCPValueType, string strReplaceCRLF)
        {
            dtTable.UpdateRow(strTableID, strDistributionType, strDistributionColumn, strIndexType, strIndexColumn, strBCPSplit, strBCPColumns, strBCPValues, strBCPValueType, strReplaceCRLF);
        }

        /// <summary>
        /// Load the Table list from the database. This will return the current state
        /// </summary>
        private void LoadTableListFromDatabase()
        {
            dtTable.RefreshTableListFromDB(SQLDwConfig);
            this.dgvTables.DataSource = dtTable.GetTableList();
        }

        /// <summary>
        /// Save Table list of XML file
        /// </summary>
        private void SaveListToXML()
        {
            txtTable.Text = "";

            //Replace the Data Grid as we want to save the updated list.
            DataTable dtgv = new DataTable();
            foreach (DataGridViewColumn col in this.dgvTables.Columns)
            {
                if(col.HeaderText == MyTableList.DATA_SIZE_MB || col.HeaderText == MyTableList.TOTAL_ROWS)
                    dtgv.Columns.Add(col.HeaderText, Type.GetType("System.Int32"));
                else
                    dtgv.Columns.Add(col.HeaderText);
            }

            foreach (DataGridViewRow row in this.dgvTables.Rows)
            {
                DataRow dRow = dtgv.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dtgv.Rows.Add(dRow);
            }
            dtTable.ReplaceTableList(dtgv);

            string strFolder, strFileName;
            strFolder = Properties.Settings.Default.ApplicationFolder + "\\" + Constants.SUB_FOLDER_LIST;
            strFileName = SQLDwConfig.ConfigFileName.Replace(".Config", ".xml");
            dtTable.SaveListToXML(strFolder, strFileName);

            //After the XML file is saved. Read it back on the Grid from the file
            ReadFromXML();

        }

        /// <summary>
        /// Read Saved list from XML to Grid
        /// </summary>
        private void ReadFromXML()
        {
            txtTable.Text = "";
            DataTable dtFromFile = new DataTable();

            string strFolder, strFileName, strFileURL;
            strFolder = Properties.Settings.Default.ApplicationFolder + "\\" + Constants.SUB_FOLDER_LIST;
            strFileName = SQLDwConfig.ConfigFileName.Replace(".Config", ".xml");
            strFileURL = strFolder + "\\" + strFileName;

            dtFromFile.ReadXml(strFileURL);

            //Repalce the Table List and update the Data Source to start referring the new list from XML file
            dtTable.ReplaceTableList(dtFromFile);
            this.dgvTables.DataSource = dtTable.GetTableList();
        }


        /// <summary>
        /// General Routing to generate Script based on the script type selected
        /// </summary>
        private void GenerateScript()
        {
            sbpMessage.Text = "Generating Files....";
            DataTable dtgv = GetDataTableFromGridView();
            MyTableList dtTable = new MyTableList();
            dtTable.ReplaceTableList(dtgv);

            string strScriptType;
            strScriptType = cmbDataType.SelectedItem.ToString();

            MyScriptWriter ScriptWriter = new MyScriptWriter(SQLDwConfig);

            switch (strScriptType)
            {
                case Constants.SCRIPT_TYPE_EXTERNAL_TABLE_CREATE:
                    ScriptWriter.GenerateTableCreateExternal(dtTable, false, false);
                    break;
                case Constants.SCRIPT_TYPE_DWH_TABLE_CREATE:
                    ScriptWriter.GenerateTableCreateDWH(dtTable, false, false);
                    break;
                case Constants.SCRIPT_TYPE_EXTERNAL_TABLE_CREATE_DROP:
                    ScriptWriter.GenerateTableCreateExternal(dtTable, true, false);
                    break;
                case Constants.SCRIPT_TYPE_DWH_TABLE_CREATE_DROP:
                    ScriptWriter.GenerateTableCreateDWH(dtTable, true, false);
                    break;
                case Constants.SCRIPT_TYPE_BCP:
                    ScriptWriter.GenerateBCPScript(dtTable, false, false);
                    break;
                case Constants.SCRIPT_TYPE_INSERT_FROM_EXT_TABLE:
                    ScriptWriter.GenerateInsertFromExternalTable(dtTable, false);
                    break;
                case Constants.SCRIPT_TYPE_EXTERNAL_TABLE_DROP:
                    ScriptWriter.GenerateTableDrop(dtTable, Constants.TABLE_TYPE_EXTERNAL);
                    break;
                case Constants.SCRIPT_TYPE_DWH_TABLE_DROP:
                    ScriptWriter.GenerateTableDrop(dtTable, Constants.TABLE_TYPE_DWH);
                    break;
                case Constants.SCRIPT_TYPE_DWH_TABLE_TRUNCATE:
                    ScriptWriter.GenerateTableTruncate(dtTable);
                    break;
                case Constants.SCRIPT_TYPE_AZURE:
                    ScriptWriter.GeneratePSFileUTF8(false);
                    ScriptWriter.GeneratePSFileZip(false);
                    ScriptWriter.GenerateAZCopyFile(false);
                    ScriptWriter.GenerateAzurePrepSQL(false);
                    break;
                case Constants.SCRIPT_TYPE_FILE_HEADER:
                    ScriptWriter.GenerateFileHeaderWithColumnNames(dtTable);
                    break;
                case Constants.SCRIPT_TYPE_SELECT_SQL:
                    ScriptWriter.GenerateTableSELECTFile(dtTable);
                    break;
                case Constants.SCRIPT_TYPE_TABLE_REFACTOR:
                    ScriptWriter.GenerateTableRefactor(dtTable);
                    break;
                case Constants.SCRIPT_TYPE_BCP_EMPTY_FILE:
                    ScriptWriter.GenerateBCPScript(dtTable, true, false);
                    break;
                case Constants.SCRIPT_TYPE_ALL:
                    //Genreate All Scripts
                    //ScriptWriter.GenerateTableCreateExternal(dtTable, false, false);
                    //ScriptWriter.GenerateTableCreateDWH(dtTable, false, false);
                    //ScriptWriter.GenerateBCPScript(dtTable, false, false);
                    //ScriptWriter.GenerateInsertFromExternalTable(dtTable, false);
                    //ScriptWriter.GenerateTableDrop(dtTable, Constants.TABLE_TYPE_EXTERNAL);
                    //ScriptWriter.GenerateTableDrop(dtTable, Constants.TABLE_TYPE_DWH);
                    //ScriptWriter.GenerateTableTruncate(dtTable);
                    //ScriptWriter.GeneratePSFileUTF8(false);
                    //ScriptWriter.GeneratePSFileZip(false);
                    //ScriptWriter.GenerateAZCopyFile(false);
                    //ScriptWriter.GenerateAzurePrepSQL(false);
                    ScriptWriter.GenerateTableCreateSTG(dtTable, false, false);
                    ScriptWriter.GenerateTableCreatePersistent(dtTable, false, false);
                    ScriptWriter.GenerateMergeSQL(dtTable, false);
                    ScriptWriter.GenerateAlterTableForPSA(dtTable, false);
                    break;
            }
            sbpMessage.Text = "File(s) Generated";


        }

        private DataTable GetDataTableFromGridView()
        {
            DataTable dtgv = new DataTable();
            foreach(DataGridViewColumn col in this.dgvTables.Columns)
            {
                dtgv.Columns.Add(col.HeaderText);
            }

            foreach (DataGridViewRow row in this.dgvTables.Rows)
            {
                DataRow dRow = dtgv.NewRow();
                foreach(DataGridViewCell  cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dtgv.Rows.Add(dRow);
            }
            return dtgv;
        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {


        }

        //Form UI Actions
        private void dgvTables_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    frmTableDetails f = new frmTableDetails(this, dgvTables.Rows[e.RowIndex], SQLDwConfig);
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }
        }

        private void txtTable_TextChanged(object sender, EventArgs e)
        {
            dtTable.GetTableList().DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "TableID", txtTable.Text);
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvTables.Rows)
            {
                if (optSelectAll.Checked)
                {
                    row.Cells[MyTableList.SELECT].Value = true;
                }
                else
                {
                    row.Cells[MyTableList.SELECT].Value = false;
                }
            }
        }

        private void dgvTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Button Actions

        private void btnSaveXML_Click(object sender, EventArgs e)
        {
            try
            {
                SaveListToXML();
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                LoadTableListFromDatabase();
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }
        }

        private void btnReadXML_Click(object sender, EventArgs e)
        {
            try
            {
                ReadFromXML();
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateScript();
            }
            catch(Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            try
            {
                ShowConfigForm();
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }
        }

        private void btnMigrate_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtgv = GetDataTableFromGridView();
                MyTableList dtTable = new MyTableList();
                dtTable.ReplaceTableList(dtgv);

                frmMigrateToSQLDW f = new frmMigrateToSQLDW(dtTable, this, SQLDwConfig);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }

        }
    }
}
