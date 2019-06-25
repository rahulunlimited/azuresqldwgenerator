using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SQLDwGenerator
{
    /// <summary>
    /// A General utility class for common functions
    /// </summary>
    static class UtilGeneral
    {

        /// <summary>
        /// Check if the value is Number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string input)
        {
            int number;
            return int.TryParse(input, out number);
        }

        /// <summary>
        /// Get the configuration value for a given Configuration Key
        /// The function help get the value from the class without creating an instance of the MySettings class in the main code
        /// </summary>
        /// <param name="strConfigKey"></param>
        /// <returns></returns>
        public static string GetConfigValue(string strConfigKey)
        {
            MySettingsUserConfig s = new MySettingsUserConfig();

            string strValue;
            strValue=s.GetCurrentConfigurationName(strConfigKey);

            return strValue;
        }


        public static string GetApplicationFolder()
        {
            string ApplicationFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return ApplicationFolder;

        }

        /// <summary>
        /// Show Message Alerts. Could be used for standard formatting and messages
        /// </summary>
        /// <param name="strMessage"></param>
        public static void ShowMessage(string strMessage)
        {
            System.Windows.Forms.MessageBox.Show(strMessage, "SQLDwGenerator");
        }

        /// <summary>
        /// Show Error Messages. Could be used for standard formatting and messages
        /// </summary>
        /// <param name="strMessage"></param>
        public static void ShowError(string strMessage)
        {
            System.Windows.Forms.MessageBox.Show(strMessage, "SQLDwGenerator-Error");
        }

        public static string GetQuotedString(string strText)
        {
            string strQuotedString;
            if (!strText.StartsWith("["))
                strQuotedString = "[" + strText + "]";
            else
                strQuotedString = strText;

            return strQuotedString;
        }

    }
}
