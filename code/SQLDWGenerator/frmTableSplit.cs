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

namespace SQLDwGenerator
{
    public partial class frmTableSplit : Form
    {
        private string SchemaName;
        private string TableName;

        MySettingsEnvironments SQLDwConfig;

        DataTable colList;

        private frmTableDetails frmParent;

        public frmTableSplit(frmTableDetails ParentForm, string strSchema, string strTable, string strBCPCol, string strBCPValues, string strBCPValueType, MySettingsEnvironments CurrentConfig)
        {
            InitializeComponent();
            this.frmParent = ParentForm;
            SQLDwConfig = CurrentConfig;

            SchemaName = strSchema;

            TableName = strTable;

            //Load Initial Values
            lblName.Text = SchemaName + "." + TableName;
            optSplitColumn.Checked = true;
            optTPYear.Checked = true;
            optValueTable.Checked = true;
            dtpStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEnd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            txtBCPValueType.Text = strBCPValueType;

            //Load Column List
            MyColumnList col = new MyColumnList(SchemaName, TableName, SQLDwConfig);
            colList = col.GetColumnsList();
            cmbColumns.Items.Clear();
            foreach (DataRow row in colList.Rows)
            {
                cmbColumns.Items.Add(row[MyColumnList.COLUMN_NAME]);
            }

            strBCPCol =  strBCPCol.Replace("[", "");
            strBCPCol = strBCPCol.Replace("]", "");

            if (strBCPCol != "")
                cmbColumns.SelectedItem = strBCPCol;
            else
                cmbColumns.SelectedIndex = 0;

            if (strBCPValueType == Constants.BCP_SPLIT_TYPE_VALUE)
                optValueSelect.Checked = false;
            else
            {
                optSplitTimePeriod.Checked = true;
                optValueSelect.Checked = true;
            }


            switch (strBCPValueType)
            {
                case Constants.BCP_SPLIT_TYPE_TIME_YEAR:
                    optTPYear.Checked = true;
                    break;
                case Constants.BCP_SPLIT_TYPE_TIME_MONTH:
                    optTPMonth.Checked = true;
                    break;
                case Constants.BCP_SPLIT_TYPE_TIME_DATE:
                case Constants.BCP_SPLIT_TYPE_TIME_INT_DATE:
                    optTPDay.Checked = true;
                    break;
                default:
                    optSplitColumn.Checked = true;
                    break;
            }

            lstValues.Items.Clear();
            if (strBCPValues != "")
            {
                string[] BCPVals = strBCPValues.Split(',');
                foreach (string val in BCPVals)
                {
                    lstValues.Items.Add(val);
                }

                if (strBCPValueType == Constants.BCP_SPLIT_TYPE_TIME_MONTH)
                {
                    lstValues.Items.RemoveAt(0);
                    lstValues.Items.RemoveAt(lstValues.Items.Count - 1);
                }
            }

        }

        /// <summary>
        /// Chose the Selection option based on either a Value Type or Time data type
        /// </summary>
        private void ChooseSplitSelection()
        {
            if (optSplitColumn.Checked == true)
                // If Split Column is selected, then disable all time selection options
                DoDisableTimeSelection();
            else
                switch(lblColType.Text)
                {
                    // Time Selection is possible only with date types or integer (sometimes dates are stored as integers)
                    case "int":
                    case "bigint":
                    case "date":
                    case "datetime":
                    case "smalldatetime":
                    case "datetime2":
                    case "datetimeoffset":
                        DoEnableTimeSelection();
                        break;

                    default:
                        // For all other data types, disable time selection
                        DoDisableTimeSelection();
                        break;
                }
        }

        private void DoEnableTimeSelection()
        {
            grpTimeSplit.Enabled = true;
            optValueSelect.Enabled = true;
        }

        private void DoDisableTimeSelection()
        {
            grpTimeSplit.Enabled = false;
            optValueSelect.Enabled = false;
            optValueTable.Checked = true;
        }
        
        private void ManageDateGroup()
        {
            if (optValueSelect.Checked)
                grpDate.Enabled = true;
            else
                grpDate.Enabled = false;

        }

        private void cmbColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow row in colList.Rows)
            {
                if ((string)row["ColumnName"] == (string)cmbColumns.SelectedItem)
                {
                    lblColType.Text = (string)row["DataType"];
                }

            }
            lstValues.Items.Clear();
            ChooseSplitSelection();

        }

        //Anchor function to fill values in the list based on Selection
        private void FillValues()
        {
            DataTable dtColValues = new DataTable();
            try
            {
                // Chose how list will be populated
                // (1) From Database (2) From Database but dates (3) From UI time selection
                if (optValueTable.Checked)
                {
                    if (optSplitColumn.Checked)
                        dtColValues = FillValuesFromDB();
                    else
                        dtColValues = FillValuesFromDBDateFormatted();
                }
                else
                {
                    dtColValues = FillValuesTimeSeries();
                }

                // Fill the list box but do not allow if number of values are more than 500
                lstValues.Items.Clear();
                if (dtColValues.Rows.Count < 500)
                {
                    foreach (DataRow row in dtColValues.Rows)
                    {
                        lstValues.Items.Add(row[0]);
                    }
                }
                else
                {
                    UtilGeneral.ShowMessage("Too Many Values in the Column : " + dtColValues.Rows.Count);
                }
            }
            catch(Exception ex)
            {
                UtilGeneral.ShowError(ex.ToString());
            }
        }

        private DataTable FillValuesFromDB()
        {
            string colName = (string)cmbColumns.SelectedItem;
            DataTable dt = UtilDwQuery.GetColumnValues(SchemaName, TableName, colName, SQLDwConfig);

            return dt;
        }

        private DataTable FillValuesFromDBDateFormatted()
        {
            string strColumnDataType = lblColType.Text;

            switch (strColumnDataType)
            {
                case "int":
                case "bigint":
                    break;

                case "date":
                case "datetime":
                case "smalldatetime":
                case "datetime2":
                case "datetimeoffset":
                    break;

                default:
                    throw new Exception("Only Date or Integer data types can be selected for Date");
            }


            string strColumnName = (string)cmbColumns.SelectedItem;
            strColumnName = UtilGeneral.GetQuotedString(strColumnName);

            int timeperiod = GetTimePeriodType();

            DataTable dt = new DataTable();
            dt = UtilDwQuery.GetColumnValuesWithDateFormat(SchemaName, TableName, strColumnName, strColumnDataType, timeperiod, SQLDwConfig);

            return dt;

        }

        private int GetTimePeriodType()
        {
            int timeperiod;
            if (optTPYear.Checked)
                timeperiod = Constants.DATE_TYPE_YEAR;
            else if (optTPMonth.Checked)
                timeperiod = Constants.DATE_TYPE_MONTH;
            else
                timeperiod = Constants.DATE_TYPE_DAY;

            return timeperiod;

        }
        
        private DataTable FillValuesTimeSeries()
        {
            DateTime startDate;
            DateTime endDate;

            startDate = dtpStart.Value.Date;
            endDate = dtpEnd.Value.Date;

            DataTable dt = new DataTable();
            dt.Columns.Add("DateCol");

            int timeperiod = GetTimePeriodType();

            switch (timeperiod)
            {
                case Constants.DATE_TYPE_YEAR:
                    startDate = new DateTime(startDate.Year, 1, 1);
                    endDate = new DateTime(endDate.Year, 12, 31);
                    break;
                case Constants.DATE_TYPE_MONTH:
                    startDate = new DateTime(startDate.Year, 1, 1);
                    endDate = new DateTime(endDate.Year, 12, 31);
                    break;
            }

            while(startDate <= endDate)
            {
                switch(timeperiod)
                {
                    case Constants.DATE_TYPE_YEAR:
                        dt.Rows.Add(startDate.ToString("yyyy"));
                        startDate = startDate.AddYears(1);
                        break;
                    case Constants.DATE_TYPE_MONTH:
                        dt.Rows.Add(startDate.ToString("yyyyMM"));
                        startDate = startDate.AddMonths(1);
                        break;
                    case Constants.DATE_TYPE_DAY:
                        dt.Rows.Add(startDate.ToString("yyyy-MMM-dd"));
                        startDate = startDate.AddDays(1);
                        break;
                }
            }

            return dt;

        }

        /// <summary>
        /// Routine to save the selected values and return the result back to frmTableDetails
        /// </summary>
        private void SaveData()
        {
            if (lstValues.Items.Count == 0)
            {
                UtilGeneral.ShowMessage("No value selected. Please select the split values.");
                return;
            }

            string colName = (string)cmbColumns.SelectedItem;
            string colDataType = lblColType.Text;
            string strDataType = "";

            // Chose the data type
            if (optSplitTimePeriod.Checked)
            {
                if (optTPYear.Checked)
                {
                    if (colDataType == Constants.DATA_TYPE_INT)
                        strDataType = Constants.BCP_SPLIT_TYPE_TIME_INT_YEAR;
                    else
                        strDataType = Constants.BCP_SPLIT_TYPE_TIME_YEAR;
                }
                else if (optTPMonth.Checked)
                {
                    if (colDataType == Constants.DATA_TYPE_INT)
                        strDataType = Constants.BCP_SPLIT_TYPE_TIME_INT_MONTH;
                    else
                        strDataType = Constants.BCP_SPLIT_TYPE_TIME_INT_MONTH;
                }
                else
                {
                    if (colDataType == Constants.DATA_TYPE_INT)
                        strDataType = Constants.BCP_SPLIT_TYPE_TIME_INT_DATE;
                    else
                        strDataType = Constants.BCP_SPLIT_TYPE_TIME_DATE;
                }
            }
            else
            {
                strDataType = Constants.BCP_SPLIT_TYPE_VALUE;
            }

            List<string> values = new List<string>();

            
            if (optTPMonth.Checked)
            {
                // If month, then add the previous year as a failsafe to chose all data for this and prior period
                int StartYear;
                StartYear = Int32.Parse(lstValues.Items[0].ToString().Substring(0,4))-1;
                values.Add(StartYear.ToString());
            }
            for (int i = 0; i < lstValues.Items.Count; i++)
            {
                values.Add(lstValues.Items[i].ToString());
            }
            if (optTPMonth.Checked)
            {
                // If month, then add the next year as a failsafe to chose all data for this and later period
                int EndYear;
                EndYear = Int32.Parse(lstValues.Items[lstValues.Items.Count-1].ToString().Substring(0, 4)) + 1;
                values.Add(EndYear.ToString());
            }
            string result = String.Join(",", values);

            // Update the BCP Data back to frmTableDetails
            frmParent.UpdateBCPSelection(colName, result, strDataType);
            this.Close();

        }

        //UI Actions

        private void btnFill_Click(object sender, EventArgs e)
        {
            try
            {
                FillValues();
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }
        }

        private void frmBCPSplit_Load(object sender, EventArgs e)
        {
        }

        private void optSplitColumn_CheckedChanged(object sender, EventArgs e)
        {
            ChooseSplitSelection();
        }
        
        private void optSplitTimePeriod_CheckedChanged(object sender, EventArgs e)
        {
            ChooseSplitSelection();
        }

        private void optValueSelect_CheckedChanged(object sender, EventArgs e)
        {
            ManageDateGroup();
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstValues.Items.Count > 0)
                {

                    while (lstValues.SelectedItems.Count > 0)
                    {
                        lstValues.Items.Remove(lstValues.SelectedItem);
                    }
                }
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }
        }

        private void optValueTable_CheckedChanged(object sender, EventArgs e)
        {
            ManageDateGroup();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }
        }
    }
}
