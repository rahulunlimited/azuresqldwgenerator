namespace SQLDwGenerator
{
    partial class frmMain
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.dgvTables = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnMigrate = new System.Windows.Forms.Button();
            this.tlpRight = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSaveXML = new System.Windows.Forms.Button();
            this.btnReadXML = new System.Windows.Forms.Button();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtTable = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.optSelectAll = new System.Windows.Forms.CheckBox();
            this.sbrMain = new System.Windows.Forms.StatusBar();
            this.sbpConfig = new System.Windows.Forms.StatusBarPanel();
            this.sbpServer = new System.Windows.Forms.StatusBarPanel();
            this.sbpDatabase = new System.Windows.Forms.StatusBarPanel();
            this.sbpIP = new System.Windows.Forms.StatusBarPanel();
            this.sbpMessage = new System.Windows.Forms.StatusBarPanel();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tlpRight.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sbpConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.02941F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.97059F));
            this.tlpMain.Controls.Add(this.dgvTables, 0, 0);
            this.tlpMain.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tlpMain.Controls.Add(this.tlpRight, 1, 0);
            this.tlpMain.Location = new System.Drawing.Point(2, 3);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(1224, 641);
            this.tlpMain.TabIndex = 0;
            // 
            // dgvTables
            // 
            this.dgvTables.AllowUserToAddRows = false;
            this.dgvTables.AllowUserToDeleteRows = false;
            this.dgvTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTables.Location = new System.Drawing.Point(3, 3);
            this.dgvTables.MultiSelect = false;
            this.dgvTables.Name = "dgvTables";
            this.dgvTables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTables.Size = new System.Drawing.Size(1046, 570);
            this.dgvTables.TabIndex = 2;
            this.dgvTables.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTables_CellContentClick);
            this.dgvTables.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTables_CellDoubleClick);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.92857F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.07143F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 225F));
            this.tableLayoutPanel3.Controls.Add(this.btnGenerate, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.cmbDataType, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnMigrate, 4, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 579);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(821, 43);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(387, 13);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(174, 27);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // cmbDataType
            // 
            this.cmbDataType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Location = new System.Drawing.Point(75, 16);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(306, 21);
            this.cmbDataType.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Script Type";
            // 
            // btnMigrate
            // 
            this.btnMigrate.Location = new System.Drawing.Point(597, 12);
            this.btnMigrate.Margin = new System.Windows.Forms.Padding(2);
            this.btnMigrate.Name = "btnMigrate";
            this.btnMigrate.Size = new System.Drawing.Size(190, 28);
            this.btnMigrate.TabIndex = 3;
            this.btnMigrate.Text = "Migrate to SQL Data Warehouse";
            this.btnMigrate.UseVisualStyleBackColor = true;
            this.btnMigrate.Click += new System.EventHandler(this.btnMigrate_Click);
            // 
            // tlpRight
            // 
            this.tlpRight.ColumnCount = 1;
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRight.Controls.Add(this.flowLayoutPanel3, 0, 2);
            this.tlpRight.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpRight.Location = new System.Drawing.Point(1055, 3);
            this.tlpRight.Name = "tlpRight";
            this.tlpRight.RowCount = 3;
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRight.Size = new System.Drawing.Size(166, 570);
            this.tlpRight.TabIndex = 6;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel3.Controls.Add(this.btnSaveXML);
            this.flowLayoutPanel3.Controls.Add(this.btnReadXML);
            this.flowLayoutPanel3.Controls.Add(this.btnLoadData);
            this.flowLayoutPanel3.Controls.Add(this.btnConfig);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 406);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(160, 161);
            this.flowLayoutPanel3.TabIndex = 3;
            // 
            // btnSaveXML
            // 
            this.btnSaveXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveXML.Location = new System.Drawing.Point(3, 3);
            this.btnSaveXML.Name = "btnSaveXML";
            this.btnSaveXML.Size = new System.Drawing.Size(151, 35);
            this.btnSaveXML.TabIndex = 1;
            this.btnSaveXML.Text = "Save List";
            this.btnSaveXML.UseVisualStyleBackColor = true;
            this.btnSaveXML.Click += new System.EventHandler(this.btnSaveXML_Click);
            // 
            // btnReadXML
            // 
            this.btnReadXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadXML.Location = new System.Drawing.Point(3, 44);
            this.btnReadXML.Name = "btnReadXML";
            this.btnReadXML.Size = new System.Drawing.Size(151, 35);
            this.btnReadXML.TabIndex = 2;
            this.btnReadXML.Text = "Load Saved List";
            this.btnReadXML.UseVisualStyleBackColor = true;
            this.btnReadXML.Click += new System.EventHandler(this.btnReadXML_Click);
            // 
            // btnLoadData
            // 
            this.btnLoadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadData.Location = new System.Drawing.Point(3, 85);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(151, 35);
            this.btnLoadData.TabIndex = 0;
            this.btnLoadData.Text = "Reset";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfig.Location = new System.Drawing.Point(3, 126);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(151, 35);
            this.btnConfig.TabIndex = 5;
            this.btnConfig.Text = "Configuration";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtTable, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.optSelectAll, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(160, 100);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // txtTable
            // 
            this.txtTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTable.Location = new System.Drawing.Point(3, 78);
            this.txtTable.Name = "txtTable";
            this.txtTable.Size = new System.Drawing.Size(154, 20);
            this.txtTable.TabIndex = 3;
            this.txtTable.TextChanged += new System.EventHandler(this.txtTable_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Filter";
            // 
            // optSelectAll
            // 
            this.optSelectAll.AutoSize = true;
            this.optSelectAll.Location = new System.Drawing.Point(2, 2);
            this.optSelectAll.Margin = new System.Windows.Forms.Padding(2);
            this.optSelectAll.Name = "optSelectAll";
            this.optSelectAll.Size = new System.Drawing.Size(70, 17);
            this.optSelectAll.TabIndex = 4;
            this.optSelectAll.Text = "Select All";
            this.optSelectAll.UseVisualStyleBackColor = true;
            this.optSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // sbrMain
            // 
            this.sbrMain.Location = new System.Drawing.Point(0, 671);
            this.sbrMain.Name = "sbrMain";
            this.sbrMain.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sbpConfig,
            this.sbpServer,
            this.sbpDatabase,
            this.sbpIP,
            this.sbpMessage});
            this.sbrMain.ShowPanels = true;
            this.sbrMain.Size = new System.Drawing.Size(1226, 22);
            this.sbrMain.TabIndex = 1;
            // 
            // sbpConfig
            // 
            this.sbpConfig.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.sbpConfig.Name = "sbpConfig";
            this.sbpConfig.Width = 10;
            // 
            // sbpServer
            // 
            this.sbpServer.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.sbpServer.Name = "sbpServer";
            this.sbpServer.Width = 10;
            // 
            // sbpDatabase
            // 
            this.sbpDatabase.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.sbpDatabase.Name = "sbpDatabase";
            this.sbpDatabase.Width = 10;
            // 
            // sbpIP
            // 
            this.sbpIP.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.sbpIP.Name = "sbpIP";
            this.sbpIP.Width = 10;
            // 
            // sbpMessage
            // 
            this.sbpMessage.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.sbpMessage.Name = "sbpMessage";
            this.sbpMessage.Width = 1169;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 693);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.sbrMain);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQLDwGenerator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tlpRight.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sbpConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpMessage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.StatusBar sbrMain;
        protected System.Windows.Forms.StatusBarPanel sbpConfig;
        protected System.Windows.Forms.StatusBarPanel sbpServer;
        protected System.Windows.Forms.StatusBarPanel sbpDatabase;
        protected System.Windows.Forms.StatusBarPanel sbpMessage;



        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.DataGridView dgvTables;
        private System.Windows.Forms.Button btnSaveXML;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button btnReadXML;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.CheckBox optSelectAll;
        private System.Windows.Forms.StatusBarPanel sbpIP;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnMigrate;
    }
}