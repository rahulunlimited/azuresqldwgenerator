using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace SQLDwGenerator
{

    /// <summary>
    /// Manages User Configuration Settings in a UserConfig.config file in the Application folder
    /// </summary>
    public class MySettingsUserConfig
    {
        const string USER_SETTINGS_FILENAME = "UserConfig.config";
        public const string KEY_CONFIG_NAME = "ConfigurationFile";
        public const string KEY_BCP_ROW_COUNT_OVERRIDE = "BCPRowCountOverRide";

        private Configuration ConfigFile;

        public MySettingsUserConfig()
        {
            try
            {
                string appPath = UtilGeneral.GetApplicationFolder();
                string configFileName = System.IO.Path.Combine(appPath, USER_SETTINGS_FILENAME);

                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = configFileName;

                ConfigFile = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            }

        /// <summary>
        /// Save the User Configuration
        /// </summary>
        /// <param name="strConfigKey">Configuration Key</param>
        /// <param name="strConfigValue">Configuration Value</param>
        public void SetCurrentConfiguration(string strConfigKey, string strConfigValue)
        {
            try
            {
                ConfigFile.AppSettings.Settings[strConfigKey].Value = strConfigValue;
                ConfigFile.Save();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Returns the Configuration from the UserConfig file for a given Configuration Key
        /// </summary>
        /// <param name="strConfigKey">Configuration Key</param>
        /// <returns></returns>
        public string GetCurrentConfigurationName(string strConfigKey)
        {
            string strConfigValue;
            strConfigValue = ConfigFile.AppSettings.Settings[strConfigKey].Value;
            return strConfigValue;
        }

    }

}
