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
    public partial class frmTableDetails : Form
    {
        private DataGridViewRow TableRow;
        private frmMain frmParent;
        MySettingsEnvironments SQLDwConfig;
        private bool firstload;

        public frmTableDetails(frmMain ParentForm, DataGridViewRow SelectedRow, MySettingsEnvironments CurrentConfig)
        {
            InitializeComponent();
            SQLDwConfig = CurrentConfig;

            this.frmParent = ParentForm;
            this.TableRow = SelectedRow;

            string strSchema = TableRow.Cells[MyTableList.SCHEMA_NAME].Value.ToString();
            string strTable = TableRow.Cells[MyTableList.TABLE_NAME].Value.ToString();

            firstload = true;
            LoadDefaultValues(strSchema, strTable);
            firstload = false;

        }

        /// <summary>
        /// Load the default initial values for the Table Details form
        /// </summary>
        /// <param name="strSchemaName"></param>
        /// <param name="strTableName"></param>
        private void LoadDefaultValues(string strSchemaName, string strTableName)
        {
            cmbDistributionType.Items.Clear();
            cmbDistributionType.Items.Add(Constants.DISTRIBUTION_TYPE_ROUND_ROBIN);
            cmbDistributionType.Items.Add(Constants.DISTRIBUTION_TYPE_HASH);
            cmbDistributionType.Items.Add(Constants.DISTRIBUTION_TYPE_REPLICATE);
            cmbDistributionType.SelectedItem = TableRow.Cells[MyTableList.DISTRIBUTION_TYPE].Value.ToString();

            cmbIndexType.Items.Clear();
            cmbIndexType.Items.Add(Constants.INDEX_TYPE_COLUMNSTORE);
            cmbIndexType.Items.Add(Constants.INDEX_TYPE_CLUSTERED);
            cmbIndexType.Items.Add(Constants.INDEX_TYPE_HEAP);
            cmbIndexType.SelectedItem = TableRow.Cells[MyTableList.INDEX_TYPE].Value.ToString();

            txtSchema.Text = TableRow.Cells[MyTableList.SCHEMA_NAME].Value.ToString();
            txtTable.Text = TableRow.Cells[MyTableList.TABLE_NAME].Value.ToString();
            txtRowCount.Text = TableRow.Cells[MyTableList.TOTAL_ROWS].Value.ToString();
            txtSize.Text = TableRow.Cells[MyTableList.DATA_SIZE_MB].Value.ToString();
            txtBCPColumn.Text = TableRow.Cells[MyTableList.BCP_SPLIT_COLUMN].Value.ToString();
            txtBCPValues.Text = TableRow.Cells[MyTableList.BCP_SPLIT_VALUES].Value.ToString();
            txtBCPValueType.Text = TableRow.Cells[MyTableList.BCP_SPLIT_VALUE_TYPE].Value.ToString();

            MyColumnList col = new MyColumnList(strSchemaName, strTableName, SQLDwConfig);
            DataTable colList = col.GetColumnsList();

            cmbDistributionColumn.Items.Clear();
            foreach (DataRow row in colList.Rows)
            {
                cmbDistributionColumn.Items.Add(row[MyColumnList.COLUMN_NAME]);
            }
            if (TableRow.Cells[MyTableList.DISTRIBUTION_COLUMN].Value.ToString() == "")
                cmbDistributionColumn.SelectedIndex = 0;
            else
                cmbDistributionColumn.SelectedItem = TableRow.Cells[MyTableList.DISTRIBUTION_COLUMN].Value.ToString();

            cmbIndexColumn.Items.Clear();
            foreach (DataRow row in colList.Rows)
            {
                cmbIndexColumn.Items.Add(row[MyColumnList.COLUMN_NAME]);
            }
            if (TableRow.Cells[MyTableList.INDEX_COLUMN].Value.ToString() == "")
                cmbIndexColumn.SelectedIndex = 0;
            else
                cmbIndexColumn.SelectedItem = TableRow.Cells[MyTableList.INDEX_COLUMN].Value.ToString();

            if (TableRow.Cells[MyTableList.BCP_SPLIT].Value.ToString() == Constants.YES)
                optBCPSplit.Checked = true;
            else
                optBCPFull.Checked = true;

            if (TableRow.Cells[MyTableList.REPLACE_CRLF].Value.ToString() == Constants.YES)
                chkCRLF.Checked = true;
            else
                chkCRLF.Checked = false;
        }

        /// <summary>
        /// Public Function called from BCP Form to update the BCP details
        /// </summary>
        /// <param name="strColumn"></param>
        /// <param name="strValues"></param>
        /// <param name="strBCPValueType"></param>
        public void UpdateBCPSelection(string strColumn, string strValues, string strBCPValueType)
        {
            txtBCPColumn.Text = strColumn;
            txtBCPValues.Text = strValues;
            txtBCPValueType.Text = strBCPValueType;
        }

        /// <summary>
        /// Save details for the currently selected table back to main Table Grid
        /// </summary>
        private void SaveUpdates()
        {

            string strTableID = null;
            string strDistributionType, strDistributionColumn, strIndextype, strIndexColumn = null;
            string strBCPSplit, strBCPColumn, strBCPValues, strBCPValueType = null;

            strTableID = txtSchema.Text + "." + txtTable.Text;
            strDistributionType = (string)cmbDistributionType.SelectedItem;
            if (strDistributionType == Constants.DISTRIBUTION_TYPE_HASH)
                strDistributionColumn = (string)cmbDistributionColumn.SelectedItem;
            else
                strDistributionColumn = "";

            strIndextype = (string)cmbIndexType.SelectedItem;
            if (strIndextype == Constants.INDEX_TYPE_CLUSTERED)
                strIndexColumn = (string)cmbIndexColumn.SelectedItem;
            else
                strIndexColumn = "";

            if (optBCPFull.Checked)
                strBCPSplit = Constants.NO;
            else
                strBCPSplit = Constants.YES;

            if (strBCPSplit == Constants.YES)
            {
                strBCPColumn = txtBCPColumn.Text;
                strBCPValues = txtBCPValues.Text;
                strBCPValueType = txtBCPValueType.Text;
            }
            else
            {
                strBCPColumn = "";
                strBCPValues = "";
                strBCPValueType = "";
            }

            string strCRLF = null;
            if (chkCRLF.Checked)
                strCRLF = Constants.YES;
            else
                strCRLF = Constants.NO;

            frmParent.UpdateRow(strTableID, strDistributionType, strDistributionColumn, strIndextype, strIndexColumn, strBCPSplit, strBCPColumn, strBCPValues, strBCPValueType, strCRLF);
        }

        /// <summary>
        /// Helper function to open the BCP Split form if option is selected
        /// </summary>
        private void doBCPSlit()
        {
            if (optBCPSplit.Checked)
            {
                if (firstload == true) return;

                grpBCPDetails.Visible = true;
                frmTableSplit f = new frmTableSplit(this, txtSchema.Text, txtTable.Text, txtBCPColumn.Text, txtBCPValues.Text, txtBCPValueType.Text, SQLDwConfig);
                f.ShowDialog();
            }
            else
                grpBCPDetails.Visible = false;

        }


        //UI Actions
        private void cmbDistributionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strDistributionType = null;
            strDistributionType = (string) cmbDistributionType.SelectedItem;

            if (strDistributionType == Constants.DISTRIBUTION_TYPE_HASH)
                cmbDistributionColumn.Visible = true;
            else
                cmbDistributionColumn.Visible = false;
        }

        private void cmbIndexType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strIndexType = null;
            strIndexType = (string)cmbIndexType.SelectedItem;

            if (strIndexType == Constants.INDEX_TYPE_CLUSTERED)
                cmbIndexColumn.Visible = true;
            else
                cmbIndexColumn.Visible = false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveUpdates();
                this.Close();
            }
            catch (Exception ex)
            {
                UtilGeneral.ShowError(ex.Message);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void optBCPSplit_CheckedChanged(object sender, EventArgs e)
        {
            doBCPSlit();
        }

        private void frmTableDetails_Load(object sender, EventArgs e)
        {

        }


    }
}
