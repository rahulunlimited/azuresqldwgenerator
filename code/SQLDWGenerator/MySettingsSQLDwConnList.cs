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
    class MySettingsSQLDwConnList
    {

        public string ServerName, DatabaseName, UserID, Password = "";
        private string ConnFileURL;

        private string ConfigurationFolder = Properties.Settings.Default.ApplicationFolder + "\\" + Constants.SUB_FOLDER_SQLDW_CONN;

        public MySettingsSQLDwConnList(string strConnectionName)
        {
            ConnFileURL = ConfigurationFolder + "\\" + strConnectionName + ".Connection";

        }

        public bool ConnFileExists()
        {
            if (File.Exists(ConnFileURL))
                return true;
            else
                return false;
        }


        public void SaveConnection(string strServer, string strDatabase, string strUID, string strPassword)
        {
            try
            {
                if (!Directory.Exists(ConfigurationFolder))
                    Directory.CreateDirectory(ConfigurationFolder);

                ServerName = strServer;
                DatabaseName = strDatabase;
                UserID = strUID;
                Password = strPassword;

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.NewLineOnAttributes = true;

                XmlWriter w = XmlWriter.Create(ConnFileURL, settings);
                w.WriteStartDocument();
                w.WriteStartElement("Connection");
                w.WriteElementString("ServerName", ServerName);
                w.WriteElementString("DatabaseName", DatabaseName);
                w.WriteElementString("UserID", UserID);
                w.WriteElementString("Password", Password);
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

        public void LoadConfigurationFile()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(ConnFileURL);
                ServerName = doc.GetElementsByTagName("ServerName")[0].InnerXml;
                DatabaseName = doc.GetElementsByTagName("DatabaseName")[0].InnerXml;
                Password = doc.GetElementsByTagName("Password")[0].InnerXml;
                UserID = doc.GetElementsByTagName("UserID")[0].InnerXml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
