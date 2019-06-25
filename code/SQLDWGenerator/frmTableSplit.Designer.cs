namespace SQLDwGenerator
{
    partial class frmTableSplit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblColType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbColumns = new System.Windows.Forms.ComboBox();
            this.optSplitTimePeriod = new System.Windows.Forms.RadioButton();
            this.optSplitColumn = new System.Windows.Forms.RadioButton();
            this.grpTimeSplit = new System.Windows.Forms.GroupBox();
            this.optTPDay = new System.Windows.Forms.RadioButton();
            this.optTPMonth = new System.Windows.Forms.RadioButton();
            this.optTPYear = new System.Windows.Forms.RadioButton();
            this.lstValues = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grpDate = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.optValueSelect = new System.Windows.Forms.RadioButton();
            this.optValueTable = new System.Windows.Forms.RadioButton();
            this.btnFill = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtBCPValueType = new System.Windows.Forms.TextBox();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.grpTimeSplit.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpDate.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblColType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbColumns);
            this.groupBox1.Controls.Add(this.optSplitTimePeriod);
            this.groupBox1.Controls.Add(this.optSplitColumn);
            this.groupBox1.Location = new System.Drawing.Point(23, 64);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(460, 185);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Split data based on";
            // 
            // lblColType
            // 
            this.lblColType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblColType.Location = new System.Drawing.Point(25, 135);
            this.lblColType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColType.Name = "lblColType";
            this.lblColType.Size = new System.Drawing.Size(425, 27);
            this.lblColType.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Column Name";
            // 
            // cmbColumns
            // 
            this.cmbColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColumns.FormattingEnabled = true;
            this.cmbColumns.Location = new System.Drawing.Point(21, 94);
            this.cmbColumns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbColumns.Name = "cmbColumns";
            this.cmbColumns.Size = new System.Drawing.Size(429, 24);
            this.cmbColumns.TabIndex = 2;
            this.cmbColumns.SelectedIndexChanged += new System.EventHandler(this.cmbColumns_SelectedIndexChanged);
            // 
            // optSplitTimePeriod
            // 
            this.optSplitTimePeriod.AutoSize = true;
            this.optSplitTimePeriod.Location = new System.Drawing.Point(124, 36);
            this.optSplitTimePeriod.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optSplitTimePeriod.Name = "optSplitTimePeriod";
            this.optSplitTimePeriod.Size = new System.Drawing.Size(112, 21);
            this.optSplitTimePeriod.TabIndex = 1;
            this.optSplitTimePeriod.TabStop = true;
            this.optSplitTimePeriod.Text = "Time Periods";
            this.optSplitTimePeriod.UseVisualStyleBackColor = true;
            this.optSplitTimePeriod.CheckedChanged += new System.EventHandler(this.optSplitTimePeriod_CheckedChanged);
            // 
            // optSplitColumn
            // 
            this.optSplitColumn.AutoSize = true;
            this.optSplitColumn.Location = new System.Drawing.Point(21, 36);
            this.optSplitColumn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optSplitColumn.Name = "optSplitColumn";
            this.optSplitColumn.Size = new System.Drawing.Size(76, 21);
            this.optSplitColumn.TabIndex = 0;
            this.optSplitColumn.TabStop = true;
            this.optSplitColumn.Text = "Column";
            this.optSplitColumn.UseVisualStyleBackColor = true;
            this.optSplitColumn.CheckedChanged += new System.EventHandler(this.optSplitColumn_CheckedChanged);
            // 
            // grpTimeSplit
            // 
            this.grpTimeSplit.Controls.Add(this.optTPDay);
            this.grpTimeSplit.Controls.Add(this.optTPMonth);
            this.grpTimeSplit.Controls.Add(this.optTPYear);
            this.grpTimeSplit.Location = new System.Drawing.Point(24, 256);
            this.grpTimeSplit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpTimeSplit.Name = "grpTimeSplit";
            this.grpTimeSplit.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpTimeSplit.Size = new System.Drawing.Size(459, 89);
            this.grpTimeSplit.TabIndex = 1;
            this.grpTimeSplit.TabStop = false;
            this.grpTimeSplit.Text = "Time Period";
            // 
            // optTPDay
            // 
            this.optTPDay.AutoSize = true;
            this.optTPDay.Location = new System.Drawing.Point(236, 41);
            this.optTPDay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optTPDay.Name = "optTPDay";
            this.optTPDay.Size = new System.Drawing.Size(54, 21);
            this.optTPDay.TabIndex = 2;
            this.optTPDay.TabStop = true;
            this.optTPDay.Text = "Day";
            this.optTPDay.UseVisualStyleBackColor = true;
            // 
            // optTPMonth
            // 
            this.optTPMonth.AutoSize = true;
            this.optTPMonth.Location = new System.Drawing.Point(123, 41);
            this.optTPMonth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optTPMonth.Name = "optTPMonth";
            this.optTPMonth.Size = new System.Drawing.Size(68, 21);
            this.optTPMonth.TabIndex = 1;
            this.optTPMonth.TabStop = true;
            this.optTPMonth.Text = "Month";
            this.optTPMonth.UseVisualStyleBackColor = true;
            // 
            // optTPYear
            // 
            this.optTPYear.AutoSize = true;
            this.optTPYear.Location = new System.Drawing.Point(20, 41);
            this.optTPYear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optTPYear.Name = "optTPYear";
            this.optTPYear.Size = new System.Drawing.Size(59, 21);
            this.optTPYear.TabIndex = 0;
            this.optTPYear.TabStop = true;
            this.optTPYear.Text = "Year";
            this.optTPYear.UseVisualStyleBackColor = true;
            // 
            // lstValues
            // 
            this.lstValues.FormattingEnabled = true;
            this.lstValues.ItemHeight = 16;
            this.lstValues.Location = new System.Drawing.Point(885, 20);
            this.lstValues.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstValues.Name = "lstValues";
            this.lstValues.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstValues.Size = new System.Drawing.Size(268, 276);
            this.lstValues.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grpDate);
            this.groupBox3.Controls.Add(this.optValueSelect);
            this.groupBox3.Controls.Add(this.optValueTable);
            this.groupBox3.Location = new System.Drawing.Point(491, 65);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(260, 279);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Split data values";
            // 
            // grpDate
            // 
            this.grpDate.Controls.Add(this.label3);
            this.grpDate.Controls.Add(this.dtpEnd);
            this.grpDate.Controls.Add(this.label2);
            this.grpDate.Controls.Add(this.dtpStart);
            this.grpDate.Location = new System.Drawing.Point(19, 122);
            this.grpDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDate.Name = "grpDate";
            this.grpDate.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDate.Size = new System.Drawing.Size(233, 150);
            this.grpDate.TabIndex = 4;
            this.grpDate.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "End Date";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(13, 102);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(187, 22);
            this.dtpEnd.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Start Date";
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(13, 44);
            this.dtpStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(187, 22);
            this.dtpStart.TabIndex = 0;
            // 
            // optValueSelect
            // 
            this.optValueSelect.AutoSize = true;
            this.optValueSelect.Location = new System.Drawing.Point(25, 74);
            this.optValueSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optValueSelect.Name = "optValueSelect";
            this.optValueSelect.Size = new System.Drawing.Size(206, 21);
            this.optValueSelect.TabIndex = 3;
            this.optValueSelect.TabStop = true;
            this.optValueSelect.Text = "Select Start and End Values";
            this.optValueSelect.UseVisualStyleBackColor = true;
            this.optValueSelect.CheckedChanged += new System.EventHandler(this.optValueSelect_CheckedChanged);
            // 
            // optValueTable
            // 
            this.optValueTable.AutoSize = true;
            this.optValueTable.Location = new System.Drawing.Point(25, 36);
            this.optValueTable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optValueTable.Name = "optValueTable";
            this.optValueTable.Size = new System.Drawing.Size(171, 21);
            this.optValueTable.TabIndex = 2;
            this.optValueTable.TabStop = true;
            this.optValueTable.Text = "Get Values from Table";
            this.optValueTable.UseVisualStyleBackColor = true;
            this.optValueTable.CheckedChanged += new System.EventHandler(this.optValueTable_CheckedChanged);
            // 
            // btnFill
            // 
            this.btnFill.Location = new System.Drawing.Point(759, 169);
            this.btnFill.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(119, 48);
            this.btnFill.TabIndex = 4;
            this.btnFill.Text = "List Values >>";
            this.btnFill.UseVisualStyleBackColor = true;
            this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(17, 383);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 46);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(175, 383);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 46);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtBCPValueType);
            this.groupBox4.Controls.Add(this.btnRemoveItem);
            this.groupBox4.Controls.Add(this.lblName);
            this.groupBox4.Controls.Add(this.btnFill);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Controls.Add(this.grpTimeSplit);
            this.groupBox4.Controls.Add(this.lstValues);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Location = new System.Drawing.Point(16, 2);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(1163, 358);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            // 
            // txtBCPValueType
            // 
            this.txtBCPValueType.Location = new System.Drawing.Point(516, 20);
            this.txtBCPValueType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBCPValueType.Name = "txtBCPValueType";
            this.txtBCPValueType.Size = new System.Drawing.Size(100, 22);
            this.txtBCPValueType.TabIndex = 8;
            this.txtBCPValueType.Visible = false;
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Location = new System.Drawing.Point(887, 309);
            this.btnRemoveItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(267, 34);
            this.btnRemoveItem.TabIndex = 6;
            this.btnRemoveItem.Text = "Remove Selected Item";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // lblName
            // 
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblName.Location = new System.Drawing.Point(24, 20);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(467, 34);
            this.lblName.TabIndex = 5;
            // 
            // frmTableSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1325, 443);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTableSplit";
            this.Text = "Split Table Extract Data";
            this.Load += new System.EventHandler(this.frmBCPSplit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpTimeSplit.ResumeLayout(false);
            this.grpTimeSplit.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpDate.ResumeLayout(false);
            this.grpDate.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbColumns;
        private System.Windows.Forms.RadioButton optSplitTimePeriod;
        private System.Windows.Forms.RadioButton optSplitColumn;
        private System.Windows.Forms.GroupBox grpTimeSplit;
        private System.Windows.Forms.RadioButton optTPDay;
        private System.Windows.Forms.RadioButton optTPMonth;
        private System.Windows.Forms.RadioButton optTPYear;
        private System.Windows.Forms.ListBox lstValues;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox grpDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.RadioButton optValueSelect;
        private System.Windows.Forms.RadioButton optValueTable;
        private System.Windows.Forms.Button btnFill;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblColType;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.TextBox txtBCPValueType;
    }
}