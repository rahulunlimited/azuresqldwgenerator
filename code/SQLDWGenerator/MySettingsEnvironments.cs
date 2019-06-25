using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml;
using System.Configuration;

namespace SQLDwGenerator
{
    public class MySettingsEnvironments
    {

        /// <summary>
        ///  Manages the configuration for eacch database instance. 
        ///  The class allows to save and read from the specific XML Configuration file.
        /// </summary>

        public string ConnectionType, ServerName, DatabaseName, AuthenticationType, UserName, Password;
        public string BCPOutputFolder, BCPOutputFormat;
        public string AzStorageAccount, AzContainer, AzStorageKey;
        public string File7Zip, FileAZCopy;
        public string ConfigFileName;

        private string ConnectionString, ConnectionStringFormat;
        private string ConfigFileURL;
        private string ConfigurationFolder = Properties.Settings.Default.ApplicationFolder + "\\" + Constants.SUB_FOLDER_SETTINGS;


        /// <summary>
        /// Initialize a new instance of MyApplicationConfiguration with the Config File Name
        /// </summary>
        /// <param name="strConfigFileName"></param>
        public MySettingsEnvironments(string strConfigFileName)
        {
            ConfigFileName = strConfigFileName;
            ConfigFileURL = GetConfigFileURL(strConfigFileName);

            if (ConfigFileExists())
                LoadConfigurationFile();
            else
            {
                if (ConfigFileName == "") ConfigFileURL = "";
                throw new Exception("Configuration File : " + ConfigFileURL + " do not exists. Please select/create a Configuration file to continue.");
            }
        }

        public MySettingsEnvironments()
        {
        }


        private string GetConfigFileURL(string strConfigFileName)
        {
            string strFile = ConfigurationFolder + "\\" + strConfigFileName;
            return strFile;
        }

        /// <summary>
        /// Returns the Source Database connection string for the current configuration
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            //LoadConfigurationFile();
            string strConn;
            strConn = ConnectionStringFormat.Replace("<<ServerName>>", ServerName)
                    .Replace("<<DatabaseName>>", DatabaseName)
                    .Replace("<<UserName>>", UserName)
                    .Replace("<<Password>>", Password);
            ConnectionString = strConn;
            return ConnectionString;
        }



        public bool ConfigFileExists()
        {
            if (File.Exists(ConfigFileURL))
                return true;
            else
                return false;
        }


        /// <summary>
        /// Saves the current Configuration to XML File. The file name is the name Configuration Name specified with .Config
        /// </summary>
        public void SaveConfiguration(
            string strConfigName,
            string strConnectionType, string strServerName, string strDatabaseName, string strAuthenticationType, string strUserName, string strPassword,
            string strBCPOutputFolder, string strBCPOutputFormat,
            string strAzStorageAccount, string strAzContainer, string strAzStorageKey,
            string strFile7Zip, string strFileAZCopy)
        {

            ConfigFileURL = GetConfigFileURL(strConfigName + ".Config");

            try
            {
                if (!Directory.Exists(ConfigurationFolder))
                    Directory.CreateDirectory(ConfigurationFolder);

                ConnectionType = strConnectionType;
                ServerName = strServerName;
                DatabaseName = strDatabaseName;
                AuthenticationType = strAuthenticationType;
                UserName = strUserName;
                Password = strPassword;

                BCPOutputFolder = strBCPOutputFolder;
                BCPOutputFormat = strBCPOutputFormat;
                AzStorageAccount = strAzStorageAccount;
                AzContainer = strAzContainer;
                AzStorageKey = strAzStorageKey;
                File7Zip = strFile7Zip;
                FileAZCopy = strFileAZCopy;

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.NewLineOnAttributes = true;

                XmlWriter w = XmlWriter.Create(ConfigFileURL, settings);
                w.WriteStartDocument();
                w.WriteStartElement("Configuration");

                w.WriteElementString("ConnectionType", ConnectionType);
                w.WriteElementString("ServerName", ServerName);
                w.WriteElementString("DatabaseName", DatabaseName);
                w.WriteElementString("AuthenticationType", AuthenticationType);
                w.WriteElementString("UserName", UserName);
                w.WriteElementString("Password", Password);

                w.WriteElementString("BCPOutputFolder", BCPOutputFolder);
                w.WriteElementString("BCPOutputFormat", BCPOutputFormat);
                w.WriteElementString("AzureStorageAccount", AzStorageAccount);
                w.WriteElementString("AzureContainer", AzContainer);
                w.WriteElementString("AzureStorageKey", AzStorageKey);

                w.WriteElementString("File7Zip", File7Zip);
                w.WriteElementString("FileAZCopy", FileAZCopy);
                w.WriteEndElement();
                w.WriteEndDocument();
                w.Flush();
                w.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Loads configuration information from the XML file to Application
        /// </summary>

        public void LoadConfigurationFile()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(ConfigFileURL);
                ConnectionType = doc.GetElementsByTagName("ConnectionType")[0].InnerXml;
                ServerName = doc.GetElementsByTagName("ServerName")[0].InnerXml;
                DatabaseName = doc.GetElementsByTagName("DatabaseName")[0].InnerXml;
                AuthenticationType = doc.GetElementsByTagName("AuthenticationType")[0].InnerXml;
                UserName = doc.GetElementsByTagName("UserName")[0].InnerXml;
                Password = doc.GetElementsByTagName("Password")[0].InnerXml;


                BCPOutputFolder = doc.GetElementsByTagName("BCPOutputFolder")[0].InnerXml;
                BCPOutputFormat = doc.GetElementsByTagName("BCPOutputFormat")[0].InnerXml;
                AzStorageAccount = doc.GetElementsByTagName("AzureStorageAccount")[0].InnerXml;
                AzContainer = doc.GetElementsByTagName("AzureContainer")[0].InnerXml;
                AzStorageKey = doc.GetElementsByTagName("AzureStorageKey")[0].InnerXml;


                File7Zip = doc.GetElementsByTagName("File7Zip")[0].InnerXml;
                FileAZCopy = doc.GetElementsByTagName("FileAZCopy")[0].InnerXml;

                string strFolderApplication = UtilGeneral.GetApplicationFolder();
                string strConnectionStringFormatFile  = strFolderApplication += "\\" + Constants.FOLDER_CONNECTION_TYPE
                    + "\\" + ConnectionType
                    + "\\" + AuthenticationType + ".ConnectionStringFormat";
                ConnectionStringFormat = System.IO.File.ReadAllText(strConnectionStringFormatFile);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
