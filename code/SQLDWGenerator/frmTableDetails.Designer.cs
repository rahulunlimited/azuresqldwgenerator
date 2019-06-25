namespace SQLDwGenerator
{
    partial class frmTableDetails
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
            this.txtSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRowCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTable = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSchema = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbDistributionColumn = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDistributionType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbIndexColumn = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbIndexType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.optBCPSplit = new System.Windows.Forms.RadioButton();
            this.optBCPFull = new System.Windows.Forms.RadioButton();
            this.grpBCPDetails = new System.Windows.Forms.GroupBox();
            this.txtBCPValueType = new System.Windows.Forms.TextBox();
            this.txtBCPValues = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBCPColumn = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkCRLF = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpBCPDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSize);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtRowCount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTable);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSchema);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(29, 26);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(625, 164);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Table Details";
            // 
            // txtSize
            // 
            this.txtSize.Enabled = false;
            this.txtSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSize.Location = new System.Drawing.Point(404, 122);
            this.txtSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(140, 23);
            this.txtSize.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(309, 126);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Size (MB)";
            // 
            // txtRowCount
            // 
            this.txtRowCount.Enabled = false;
            this.txtRowCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRowCount.Location = new System.Drawing.Point(123, 122);
            this.txtRowCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.Size = new System.Drawing.Size(140, 23);
            this.txtRowCount.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 126);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Rows";
            // 
            // txtTable
            // 
            this.txtTable.Enabled = false;
            this.txtTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTable.Location = new System.Drawing.Point(123, 73);
            this.txtTable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTable.Name = "txtTable";
            this.txtTable.Size = new System.Drawing.Size(421, 23);
            this.txtTable.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Table";
            // 
            // txtSchema
            // 
            this.txtSchema.Enabled = false;
            this.txtSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSchema.Location = new System.Drawing.Point(123, 23);
            this.txtSchema.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSchema.Name = "txtSchema";
            this.txtSchema.Size = new System.Drawing.Size(421, 23);
            this.txtSchema.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Schema";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbDistributionColumn);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbDistributionType);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(29, 209);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(491, 130);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Distribution";
            // 
            // cmbDistributionColumn
            // 
            this.cmbDistributionColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistributionColumn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDistributionColumn.FormattingEnabled = true;
            this.cmbDistributionColumn.Location = new System.Drawing.Point(123, 79);
            this.cmbDistributionColumn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbDistributionColumn.Name = "cmbDistributionColumn";
            this.cmbDistributionColumn.Size = new System.Drawing.Size(335, 25);
            this.cmbDistributionColumn.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 82);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "Column";
            // 
            // cmbDistributionType
            // 
            this.cmbDistributionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistributionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDistributionType.FormattingEnabled = true;
            this.cmbDistributionType.Location = new System.Drawing.Point(123, 33);
            this.cmbDistributionType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbDistributionType.Name = "cmbDistributionType";
            this.cmbDistributionType.Size = new System.Drawing.Size(335, 25);
            this.cmbDistributionType.TabIndex = 2;
            this.cmbDistributionType.SelectedIndexChanged += new System.EventHandler(this.cmbDistributionType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 37);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Type";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbIndexColumn);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cmbIndexType);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(541, 209);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(417, 130);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Index";
            // 
            // cmbIndexColumn
            // 
            this.cmbIndexColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIndexColumn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbIndexColumn.FormattingEnabled = true;
            this.cmbIndexColumn.Location = new System.Drawing.Point(95, 82);
            this.cmbIndexColumn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbIndexColumn.Name = "cmbIndexColumn";
            this.cmbIndexColumn.Size = new System.Drawing.Size(300, 25);
            this.cmbIndexColumn.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 82);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "Column";
            // 
            // cmbIndexType
            // 
            this.cmbIndexType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIndexType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbIndexType.FormattingEnabled = true;
            this.cmbIndexType.Location = new System.Drawing.Point(95, 33);
            this.cmbIndexType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbIndexType.Name = "cmbIndexType";
            this.cmbIndexType.Size = new System.Drawing.Size(300, 25);
            this.cmbIndexType.TabIndex = 2;
            this.cmbIndexType.SelectedIndexChanged += new System.EventHandler(this.cmbIndexType_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 37);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "Type";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(32, 550);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(115, 48);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(155, 550);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(115, 48);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.optBCPSplit);
            this.groupBox4.Controls.Add(this.optBCPFull);
            this.groupBox4.Location = new System.Drawing.Point(32, 356);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(198, 158);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Table Extract Options";
            // 
            // optBCPSplit
            // 
            this.optBCPSplit.AutoSize = true;
            this.optBCPSplit.Location = new System.Drawing.Point(9, 81);
            this.optBCPSplit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optBCPSplit.Name = "optBCPSplit";
            this.optBCPSplit.Size = new System.Drawing.Size(96, 21);
            this.optBCPSplit.TabIndex = 1;
            this.optBCPSplit.TabStop = true;
            this.optBCPSplit.Text = "Split Table";
            this.optBCPSplit.UseVisualStyleBackColor = true;
            this.optBCPSplit.CheckedChanged += new System.EventHandler(this.optBCPSplit_CheckedChanged);
            // 
            // optBCPFull
            // 
            this.optBCPFull.AutoSize = true;
            this.optBCPFull.Location = new System.Drawing.Point(9, 37);
            this.optBCPFull.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optBCPFull.Name = "optBCPFull";
            this.optBCPFull.Size = new System.Drawing.Size(91, 21);
            this.optBCPFull.TabIndex = 0;
            this.optBCPFull.TabStop = true;
            this.optBCPFull.Text = "Full Table";
            this.optBCPFull.UseVisualStyleBackColor = true;
            // 
            // grpBCPDetails
            // 
            this.grpBCPDetails.Controls.Add(this.txtBCPValueType);
            this.grpBCPDetails.Controls.Add(this.txtBCPValues);
            this.grpBCPDetails.Controls.Add(this.label10);
            this.grpBCPDetails.Controls.Add(this.txtBCPColumn);
            this.grpBCPDetails.Controls.Add(this.label9);
            this.grpBCPDetails.Location = new System.Drawing.Point(238, 356);
            this.grpBCPDetails.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpBCPDetails.Name = "grpBCPDetails";
            this.grpBCPDetails.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpBCPDetails.Size = new System.Drawing.Size(720, 158);
            this.grpBCPDetails.TabIndex = 6;
            this.grpBCPDetails.TabStop = false;
            this.grpBCPDetails.Text = "Table Extract Details";
            // 
            // txtBCPValueType
            // 
            this.txtBCPValueType.Location = new System.Drawing.Point(16, 110);
            this.txtBCPValueType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBCPValueType.Name = "txtBCPValueType";
            this.txtBCPValueType.Size = new System.Drawing.Size(56, 22);
            this.txtBCPValueType.TabIndex = 4;
            this.txtBCPValueType.Visible = false;
            // 
            // txtBCPValues
            // 
            this.txtBCPValues.Location = new System.Drawing.Point(88, 74);
            this.txtBCPValues.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBCPValues.Multiline = true;
            this.txtBCPValues.Name = "txtBCPValues";
            this.txtBCPValues.Size = new System.Drawing.Size(611, 70);
            this.txtBCPValues.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 74);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 17);
            this.label10.TabIndex = 2;
            this.label10.Text = "Values";
            // 
            // txtBCPColumn
            // 
            this.txtBCPColumn.Location = new System.Drawing.Point(85, 28);
            this.txtBCPColumn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBCPColumn.Name = "txtBCPColumn";
            this.txtBCPColumn.Size = new System.Drawing.Size(614, 22);
            this.txtBCPColumn.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 28);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "Column";
            // 
            // chkCRLF
            // 
            this.chkCRLF.AutoSize = true;
            this.chkCRLF.Location = new System.Drawing.Point(697, 38);
            this.chkCRLF.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkCRLF.Name = "chkCRLF";
            this.chkCRLF.Size = new System.Drawing.Size(206, 21);
            this.chkCRLF.TabIndex = 7;
            this.chkCRLF.Text = "Replace Newline characters";
            this.chkCRLF.UseVisualStyleBackColor = true;
            // 
            // frmTableDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(975, 635);
            this.Controls.Add(this.chkCRLF);
            this.Controls.Add(this.grpBCPDetails);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTableDetails";
            this.Text = "Table Details";
            this.Load += new System.EventHandler(this.frmTableDetails_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpBCPDetails.ResumeLayout(false);
            this.grpBCPDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRowCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSchema;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbDistributionColumn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDistributionType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbIndexColumn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbIndexType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton optBCPSplit;
        private System.Windows.Forms.RadioButton optBCPFull;
        private System.Windows.Forms.GroupBox grpBCPDetails;
        private System.Windows.Forms.TextBox txtBCPValues;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBCPColumn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkCRLF;
        private System.Windows.Forms.TextBox txtBCPValueType;
    }
}

