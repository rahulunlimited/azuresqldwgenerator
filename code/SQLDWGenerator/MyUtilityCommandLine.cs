using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

namespace SQLDwGenerator
{
    public class MyUtilityCommandLine
    {
        public MyUtilityCommandLine()
        {

        }

        public void ExecuteScript(string strFileURL)
        {
            Process prc = new Process();
            prc.StartInfo.FileName = strFileURL;
            prc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            prc.Start();
            prc.WaitForExit();

        }
    }
}
