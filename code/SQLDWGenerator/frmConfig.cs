using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.Xml;
using System.IO;


namespace SQLDwGenerator
{
    public partial class frmConfig : Form
    {
        private string SQLDwConfigurationFolder;
        private frmMain frmParent;

        private const string NEW_CONFIG_FILE = "New Configuration File";

        public frmConfig(string strConfigName, frmMain ParentForm)
        {
            InitializeComponent();
            string strSQLDWGeneratorMainFolder = Properties.Settings.Default.ApplicationFolder;
            SQLDwConfigurationFolder = strSQLDWGeneratorMainFolder + "\\" + Constants.SUB_FOLDER_SETTINGS;
            frmParent = ParentForm;

            cmbAuthentication.Items.Clear();
            cmbAuthentication.Items.Add(Constants.DB_AUTHENTICATION_INTEGRATED);
            cmbAuthentication.Items.Add(Constants.DB_AUTHENTICATION_SQL);
            cmbAuthentication.SelectedIndex = 0;
            txtUserName.Text = "";
            txtPassword.Text = "";

            if (!Directory.Exists(SQLDwConfigurationFolder))
                Directory.CreateDirectory(SQLDwConfigurationFolder);

            DirectoryInfo d = new DirectoryInfo(SQLDwConfigurationFolder);
            FileInfo[] Files = d.GetFiles("*.Config");

            // Clear the main combo box and load the availble configuration files
            cmbConfigFile.Items.Clear();
            foreach (FileInfo file in Files)
            {
                cmbConfigFile.Items.Add(file.Name);
            }
            cmbConfigFile.Items.Add(NEW_CONFIG_FILE);

            string strFolderApplication = UtilGeneral.GetApplicationFolder();
            string strFolderConnectionType = strFolderApplication + "\\" + Constants.FOLDER_CONNECTION_TYPE;
            string[] subdirectoryEntries = Directory.GetDirectories(strFolderConnectionType);
            cmbConnectionType.Items.Clear();
            foreach (string subdirectory in subdirectoryEntries)
            {
                DirectoryInfo di = new DirectoryInfo(subdirectory);
                cmbConnectionType.Items.Add(di.Name);
            }

            for (int i = 0; i < cmbConnectionType.Items.Count; i++)
            {
                if (cmbConnectionType.Items[i].ToString() == "SQL Server")
                {
                    cmbConnectionType.SelectedIndex = i;
                    break;
                }
            }


            LoadInitialData(strConfigName);
        }

        private void LoadInitialData(string strConfigName)
        {

            int intIndex = 0;
            // Check if the current configuration is available in the list and then select it
            strConfigName = strConfigName.ToString().Replace(".Config", "");
            if (strConfigName != "")
            {
                for(int i = 0; i < cmbConfigFile.Items.Count; i++)
                {
                    if(cmbConfigFile.Items[i].ToString() == strConfigName + ".Config")
                    {
                        intIndex = i;
                        break;
                    }
                }
            }

            cmbConfigFile.SelectedIndex = intIndex;

        }

        /// <summary>
        /// The Routine updates the default suggested values on the screen
        /// </summary>
        private void LoadDefaultValues()
        {
            txt7Zip.Text = @"C:\Program Files (x86)\7-Zip\7z.exe";
            txtAZCopy.Text = @"C:\Program Files (x86)\Microsoft SDKs\Azure\AzCopy\AzCopy.exe";
            txtAzContainer.Text = "initial-load";
            txtBCPOutputFolder.Text = @"C:\TEMP\";

            string strConfigName;
            strConfigName = GetConfigName();
            txtConfigName.Text = strConfigName;
        }

        private string GetConfigName()
        {
            string strConfigName;

            if (cmbConfigFile.SelectedItem.ToString() == NEW_CONFIG_FILE)
                strConfigName = "";
            else
            {
                strConfigName = cmbConfigFile.SelectedItem.ToString();
                strConfigName = strConfigName.ToString().Replace(".Config", "");
            }

            return strConfigName;
            

        }

        private void SaveConfig()
        {

            string strConnectionType, strServerName, strDatabaseName, strAuthentication, strUserName, strPassword;
            string strBCPOutputFolder, strBCPOutputFormat;
            string strAzStorageAccount, strAzContainer, strAzStorageKey;
            string strFile7Zip, strFileAZCopy;

            string strConfigName;

            if (cmbConfigFile.SelectedItem.ToString() == NEW_CONFIG_FILE)
                strConfigName = txtConfigName.Text;
            else
            {
                strConfigName = cmbConfigFile.SelectedItem.ToString();
                strConfigName = strConfigName.ToString().Replace(".Config", "");
            }

            strConnectionType = cmbConnectionType.SelectedItem.ToString();
            strServerName = txtServerName.Text;
            strDatabaseName = txtDatabaseName.Text;
            strAuthentication = cmbAuthentication.SelectedItem.ToString();
            strUserName = txtUserName.Text;
            strPassword = txtPassword.Text;

            strBCPOutputFolder = txtBCPOutputFolder.Text;
            strAzStorageAccount = txtAzStorageAccount.Text;
            strAzContainer = txtAzContainer.Text;
            strAzStorageKey = txtAzStorageKey.Text;
            strFile7Zip = txt7Zip.Text;
            strFileAZCopy = txtAZCopy.Text;

            if (optBCPUTF8.Checked)
                strBCPOutputFormat = Constants.BCP_OUT_FORMAT_UTF8;
            else
                strBCPOutputFormat = Constants.BCP_OUT_FORMAT_Unicode;


            MySettingsEnvironments cfg = new MySettingsEnvironments() ;
            cfg.SaveConfiguration(strConfigName, strConnectionType, strServerName, strDatabaseName, strAuthentication, strUserName, strPassword,
                strBCPOutputFolder, strBCPOutputFormat,
                strAzStorageAccount, strAzContainer, strAzStorageKey,
                strFile7Zip, strFileAZCopy);

            cmbConfigFile.Items.Add(strConfigName + ".Config");

            LoadInitialData(strConfigName);
        }

        private void SelectConfiguration()
        {
            string strConfig = null;
            if (cmbConfigFile.Items.Count != 0)
                strConfig = cmbConfigFile.SelectedItem.ToString();

            MySettingsEnvironments CurrentConfiguration;
            CurrentConfiguration = new MySettingsEnvironments(strConfig);
            CurrentConfiguration.LoadConfigurationFile();

            txtServerName.Text = CurrentConfiguration.ServerName;
            txtDatabaseName.Text = CurrentConfiguration.DatabaseName;
            txtUserName.Text = CurrentConfiguration.UserName;
            txtPassword.Text = CurrentConfiguration.Password;

            for (int i = 0; i < cmbConnectionType.Items.Count; i++)
            {
                if (cmbConnectionType.Items[i].ToString() == CurrentConfiguration.ConnectionType)
                {
                    cmbConnectionType.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < cmbAuthentication.Items.Count; i++)
            {
                if (cmbAuthentication.Items[i].ToString() == CurrentConfiguration.AuthenticationType)
                {
                    cmbAuthentication.SelectedIndex = i;
                    break;
                }
            }

            txtBCPOutputFolder.Text = CurrentConfiguration.BCPOutputFolder;
            txtAzStorageAccount.Text = CurrentConfiguration.AzStorageAccount;
            txtAzContainer.Text = CurrentConfiguration.AzContainer;
            txtAzStorageKey.Text = CurrentConfiguration.AzStorageKey;
            txt7Zip.Text = CurrentConfiguration.File7Zip;
            txtAZCopy.Text = CurrentConfiguration.FileAZCopy;

            if (CurrentConfiguration.BCPOutputFormat == Constants.BCP_OUT_FORMAT_UTF8)
                optBCPUTF8.Checked = true;
            else
                optBCPUnicode.Checked = true;


            txtConfigName.Text = GetConfigName();
            txtConfigName.Enabled = false;


        }

        private void ChooseConfiguration()
        {
            string strConfig = null;
            if (cmbConfigFile.Items.Count != 0)
                strConfig = cmbConfigFile.SelectedItem.ToString();


            if (strConfig == NEW_CONFIG_FILE)
            {
                txtServerName.Text = "";
                txtDatabaseName.Text = "";
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtBCPOutputFolder.Text = "";
                txtAzStorageAccount.Text = "";
                txtAzContainer.Text = "";
                txtAzStorageKey.Text = "";
                txt7Zip.Text = "";
                txtAZCopy.Text = "";
                txtConfigName.Text = "";
                txtConfigName.Enabled = true;

                btnLoadConfig.Enabled = false;
            }
            else
            {
                txtConfigName.Enabled = false;
                SelectConfiguration();

                btnLoadConfig.Enabled = true;
            }

        }

        // UI Actions
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveConfig();

                DialogResult result = MessageBox.Show("Configuration Saved. Do you want to load the Configuration now!!", "Save", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (result.Equals(DialogResult.OK))
                    LoadConfiguration();
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
        }

        private void cmbConfigFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChooseConfiguration();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            try
            {
                string strConfigFile = null;
                if (cmbConfigFile.Items.Count > 1)
                    strConfigFile = cmbConfigFile.SelectedItem.ToString();


                if (strConfigFile != NEW_CONFIG_FILE)
                {
                    //Set the Current Configuration in Settings file for future persistence
                    new MySettingsUserConfig().SetCurrentConfiguration(MySettingsUserConfig.KEY_CONFIG_NAME, strConfigFile);

                    //Change the Application Configuration
                    MySettingsEnvironments config = new MySettingsEnvironments(strConfigFile);
                    frmParent.UpdateConfig(config);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDefaultValues();
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }
        }

        private void cmbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAuthentication.SelectedItem.ToString() == Constants.DB_AUTHENTICATION_INTEGRATED)
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
            }
        }
    }
}
