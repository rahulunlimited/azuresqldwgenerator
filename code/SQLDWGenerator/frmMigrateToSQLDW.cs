using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace SQLDwGenerator
{

    public partial class frmMigrateToSQLDW : Form
    {

        const string DGV_COL_STEPS = "Steps";
        const string DGV_COL_SELECT = "Select";

        private frmMain frmParent;
        MyTableList dtTable;
        MySettingsEnvironments SQLDwConfig;
        String AzConnString;
        private string SQLDwConnFolder;

        public frmMigrateToSQLDW(MyTableList TableList, frmMain ParentForm, MySettingsEnvironments Config)
        {
            InitializeComponent();
            frmParent = ParentForm;
            this.dtTable = TableList;
            this.SQLDwConfig = Config;
            this.btnMigrate.Enabled = false;
            lblCaption.Text = "";
            SQLDwConnFolder = Properties.Settings.Default.ApplicationFolder + "\\" + Constants.SUB_FOLDER_SQLDW_CONN;

            dgvMigrate.Columns.Clear();
            dgvMigrate.Columns.Add(DGV_COL_STEPS, DGV_COL_STEPS);
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            dgvMigrate.Columns.Add(chk);
            chk.HeaderText = DGV_COL_SELECT;
            chk.Name = DGV_COL_SELECT;

            gbxMigrationOption.Visible = false;
            gbxMigrationOption.Width = gbxConnection.Width;
            gbxMigrationOption.Height = gbxConnection.Height;
            gbxMigrationOption.Left = gbxConnection.Left;
            gbxMigrationOption.Top = gbxConnection.Top;
            dgvMigrate.Width = gbxMigrationOption.Width - 30;
            dgvMigrate.Height = gbxMigrationOption.Height - 30;
            

            dgvMigrate.Rows.Add(Constants.MIGRATE_STEP_BCP_DATA);
            dgvMigrate.Rows.Add(Constants.MIGRATE_STEP_UTF8_CONVERT);
            dgvMigrate.Rows.Add(Constants.MIGRATE_STEP_COMPRESS_FILES);
            dgvMigrate.Rows.Add(Constants.MIGRATE_STEP_BLOB_UPLOAD);
            dgvMigrate.Rows.Add(Constants.MIGRATE_STEP_AZURE_ENV_PREPARE);
            dgvMigrate.Rows.Add(Constants.MIGRATE_STEP_EXTERNAL_TABLE);
            dgvMigrate.Rows.Add(Constants.MIGRATE_STEP_DATA_WAREHOUSE_TABLE);
            dgvMigrate.Rows.Add(Constants.MIGRATE_STEP_INSERT);

            dgvMigrate.Columns[DGV_COL_STEPS].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvMigrate.Columns[DGV_COL_STEPS].ReadOnly = true;

            foreach (DataGridViewRow row in dgvMigrate.Rows)
                row.Cells[DGV_COL_SELECT].Value = true;

            /*
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnMigrate.Enabled = false;
            */
            LoadInitialData();
        }

        private bool CheckMigrationStep(string strOption)
        {
            foreach(DataGridViewRow row in dgvMigrate.Rows)
            {
                if(row.Cells[DGV_COL_STEPS].Value.ToString() == strOption)
                {
                    return (bool)row.Cells[DGV_COL_SELECT].Value;
                }
            }
            return false;
        }


        private void MigrateData()
        {

            MyScriptWriter ScriptWriter = new MyScriptWriter(SQLDwConfig);
            string strScript = "";

            MyUtilityPowerShell MPS = new MyUtilityPowerShell();
            MyUtilityCommandLine CLU = new MyUtilityCommandLine();

            if (CheckMigrationStep(Constants.MIGRATE_STEP_BCP_DATA))
            {
                ShowProgressMessage("Generating BCP Files");
                strScript = ScriptWriter.GenerateBCPScript(dtTable, false, true);
                CLU.ExecuteScript(strScript);
            }

            if (CheckMigrationStep(Constants.MIGRATE_STEP_UTF8_CONVERT))
            {
                ShowProgressMessage("Converting Files to UTF8");
                strScript = ScriptWriter.GeneratePSFileUTF8(true);
                MPS.ExecuteScript(strScript);
            }

            if (CheckMigrationStep(Constants.MIGRATE_STEP_COMPRESS_FILES))
            {
                ShowProgressMessage("Compressing Files");
                strScript = ScriptWriter.GeneratePSFileZip(true);
                MPS.ExecuteScript(strScript);
            }

            if (CheckMigrationStep(Constants.MIGRATE_STEP_BLOB_UPLOAD))
            {
                ShowProgressMessage("Copying Files to BLOB");
                strScript = ScriptWriter.GenerateAZCopyFile(true);
                CLU.ExecuteScript(strScript);
            }

            SqlConnection conn;
            conn = new SqlConnection(AzConnString);
            try
            {
                string SQL = "";
                conn.Open();
                SqlCommand comm;

                if (CheckMigrationStep(Constants.MIGRATE_STEP_AZURE_ENV_PREPARE))
                {
                    ShowProgressMessage("Preparing Azure Environment");
                    SQL = ScriptWriter.GenerateAzurePrepSQL(true);
                    comm = new SqlCommand(SQL, conn);
                    comm.ExecuteNonQuery();
                }

                if (CheckMigrationStep(Constants.MIGRATE_STEP_EXTERNAL_TABLE))
                {


                    ShowProgressMessage("Creating External Tables");
                    SQL = ScriptWriter.GenerateTableCreateExternal(dtTable, true, true);
                    comm = new SqlCommand(SQL, conn);
                    comm.CommandTimeout = 0;
                    comm.ExecuteNonQuery();
                }

                if (CheckMigrationStep(Constants.MIGRATE_STEP_DATA_WAREHOUSE_TABLE))
                {
                    ShowProgressMessage("Creating Data Warehouse Tables");
                    SQL = ScriptWriter.GenerateTableCreateDWH(dtTable, true, true);
                    comm = new SqlCommand(SQL, conn);
                    comm.CommandTimeout = 0;
                    comm.ExecuteNonQuery();
                }

                if (CheckMigrationStep(Constants.MIGRATE_STEP_INSERT))
                {
                    ShowProgressMessage("Inserting data");
                    SQL = ScriptWriter.GenerateInsertFromExternalTable(dtTable, true);
                    comm = new SqlCommand(SQL, conn);
                    comm.CommandTimeout = 0;
                    comm.ExecuteNonQuery();
                }

                ShowProgressMessage("Finished");
            }
            catch (Exception ex)
            {
                ShowProgressMessage("Error");
                UtilGeneral.ShowError(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void LoadInitialData()
        {
            if (!Directory.Exists(SQLDwConnFolder))
                Directory.CreateDirectory(SQLDwConnFolder);

            DirectoryInfo d = new DirectoryInfo(SQLDwConnFolder);
            FileInfo[] Files = d.GetFiles("*.Connection");

            // Clear the main combo box and load the availble configuration files
            cmbConnections.Items.Clear();
            string strConnection;
            foreach (FileInfo file in Files)
            {
                strConnection = file.Name.ToString().Replace(".Connection", "");
                cmbConnections.Items.Add(strConnection);
            }
            if (cmbConnections.Items.Count>0)
                cmbConnections.SelectedIndex = 0;




        }

        private void TestConnection()
        {
            string strConnString;

            string strServer, strUser, strPassword, strDatabase, strConn = "";

            strServer = txtAzServer.Text;
            strUser = txtAzUser.Text;
            strPassword = txtAzPassword.Text;
            strDatabase = txtAzDatabase.Text;
            strConn = strServer + "." + strDatabase;

            strConnString = "Server=tcp:" + strServer + ";Database=" + strDatabase + ";Uid=" + strUser + ";Password=" + strPassword;
            SqlConnection conn;
            conn = new SqlConnection(strConnString);
            try
            {

                conn.Open();

                UtilGeneral.ShowMessage("Connection successful");

                MySettingsSQLDwConnList SQLDwList = new MySettingsSQLDwConnList(strConn);
                //strPassword = UtilCryptor.Crypt(strPassword);
                SQLDwList.SaveConnection(strServer, strDatabase, strUser, strPassword);

                this.btnNext.Enabled = true;
                this.AzConnString = strConnString;
            }
            catch
            {
                this.btnMigrate.Enabled = false;
                this.AzConnString = "";
                UtilGeneral.ShowError("Error connecting to database");
            }
            finally
            {
                conn.Close();
            }

        }

        private void ShowProgressMessage(string strMessage)
        {
            lblCaption.Text = strMessage + "...";
            lblCaption.Refresh();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMigrate_Click(object sender, EventArgs e)
        {
            MigrateData();

        }

        private void btnAzTestConnection_Click(object sender, EventArgs e)
        {
            TestConnection();
        }

        private void cmbConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadConnection();
        }

        private void LoadConnection()
        {
            string strConn;
            strConn = cmbConnections.SelectedItem.ToString();
            MySettingsSQLDwConnList ConnList = new MySettingsSQLDwConnList(strConn);
            ConnList.LoadConfigurationFile();

            txtAzServer.Text = ConnList.ServerName;
            txtAzDatabase.Text = ConnList.DatabaseName;
            txtAzUser.Text = ConnList.UserID;
            //txtAzPassword.Text = UtilCryptor.Decrypt( ConnList.Password);
            txtAzPassword.Text = ConnList.Password;

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            gbxConnection.Visible = false;
            gbxMigrationOption.Visible = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnMigrate.Enabled = true;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            gbxConnection.Visible = true;
            gbxMigrationOption.Visible = false;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnMigrate.Enabled = false;
        }
    }
}
