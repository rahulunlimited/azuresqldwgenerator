using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Collections;

namespace SQLDwGenerator
{
    public class MyUtilityPowerShell
    {
        PowerShell PS;
        public MyUtilityPowerShell()
        {
            PS = PowerShell.Create();
        }

        public void ExecuteScript(string strPowerShell)
        {
            PS.AddScript(strPowerShell);
            PS.Invoke();

        }

    }
}
