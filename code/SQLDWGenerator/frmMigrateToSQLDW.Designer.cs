namespace SQLDwGenerator
{
    partial class frmMigrateToSQLDW
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
            this.gbxConnection = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAzServer = new System.Windows.Forms.TextBox();
            this.cmbConnections = new System.Windows.Forms.ComboBox();
            this.btnAzTestConnection = new System.Windows.Forms.Button();
            this.txtAzDatabase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAzPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAzUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMigrate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCaption = new System.Windows.Forms.Label();
            this.gbxMigrationOption = new System.Windows.Forms.GroupBox();
            this.dgvMigrate = new System.Windows.Forms.DataGridView();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.gbxConnection.SuspendLayout();
            this.gbxMigrationOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMigrate)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxConnection
            // 
            this.gbxConnection.Controls.Add(this.label5);
            this.gbxConnection.Controls.Add(this.txtAzServer);
            this.gbxConnection.Controls.Add(this.cmbConnections);
            this.gbxConnection.Controls.Add(this.btnAzTestConnection);
            this.gbxConnection.Controls.Add(this.txtAzDatabase);
            this.gbxConnection.Controls.Add(this.label2);
            this.gbxConnection.Controls.Add(this.txtAzPassword);
            this.gbxConnection.Controls.Add(this.label4);
            this.gbxConnection.Controls.Add(this.txtAzUser);
            this.gbxConnection.Controls.Add(this.label3);
            this.gbxConnection.Controls.Add(this.label1);
            this.gbxConnection.Location = new System.Drawing.Point(12, 12);
            this.gbxConnection.Name = "gbxConnection";
            this.gbxConnection.Size = new System.Drawing.Size(919, 352);
            this.gbxConnection.TabIndex = 0;
            this.gbxConnection.TabStop = false;
            this.gbxConnection.Text = "SQL Data Warehouse Connection";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Connections:";
            // 
            // txtAzServer
            // 
            this.txtAzServer.Location = new System.Drawing.Point(159, 70);
            this.txtAzServer.Name = "txtAzServer";
            this.txtAzServer.Size = new System.Drawing.Size(403, 22);
            this.txtAzServer.TabIndex = 12;
            // 
            // cmbConnections
            // 
            this.cmbConnections.FormattingEnabled = true;
            this.cmbConnections.Location = new System.Drawing.Point(159, 34);
            this.cmbConnections.Name = "cmbConnections";
            this.cmbConnections.Size = new System.Drawing.Size(403, 24);
            this.cmbConnections.TabIndex = 11;
            this.cmbConnections.SelectedIndexChanged += new System.EventHandler(this.cmbConnections_SelectedIndexChanged);
            // 
            // btnAzTestConnection
            // 
            this.btnAzTestConnection.Location = new System.Drawing.Point(579, 161);
            this.btnAzTestConnection.Name = "btnAzTestConnection";
            this.btnAzTestConnection.Size = new System.Drawing.Size(178, 43);
            this.btnAzTestConnection.TabIndex = 10;
            this.btnAzTestConnection.Text = "Test Connection";
            this.btnAzTestConnection.UseVisualStyleBackColor = true;
            this.btnAzTestConnection.Click += new System.EventHandler(this.btnAzTestConnection_Click);
            // 
            // txtAzDatabase
            // 
            this.txtAzDatabase.Location = new System.Drawing.Point(159, 182);
            this.txtAzDatabase.Name = "txtAzDatabase";
            this.txtAzDatabase.Size = new System.Drawing.Size(403, 22);
            this.txtAzDatabase.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Database";
            // 
            // txtAzPassword
            // 
            this.txtAzPassword.Location = new System.Drawing.Point(159, 144);
            this.txtAzPassword.Name = "txtAzPassword";
            this.txtAzPassword.Size = new System.Drawing.Size(403, 22);
            this.txtAzPassword.TabIndex = 7;
            this.txtAzPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password:";
            // 
            // txtAzUser
            // 
            this.txtAzUser.Location = new System.Drawing.Point(159, 106);
            this.txtAzUser.Name = "txtAzUser";
            this.txtAzUser.Size = new System.Drawing.Size(403, 22);
            this.txtAzUser.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "User Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name:";
            // 
            // btnMigrate
            // 
            this.btnMigrate.Location = new System.Drawing.Point(333, 436);
            this.btnMigrate.Name = "btnMigrate";
            this.btnMigrate.Size = new System.Drawing.Size(145, 41);
            this.btnMigrate.TabIndex = 1;
            this.btnMigrate.Text = "&Migrate";
            this.btnMigrate.UseVisualStyleBackColor = true;
            this.btnMigrate.Click += new System.EventHandler(this.btnMigrate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(494, 436);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(155, 41);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // lblCaption
            // 
            this.lblCaption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.Location = new System.Drawing.Point(12, 381);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(919, 52);
            this.lblCaption.TabIndex = 3;
            // 
            // gbxMigrationOption
            // 
            this.gbxMigrationOption.Controls.Add(this.dgvMigrate);
            this.gbxMigrationOption.Location = new System.Drawing.Point(141, 69);
            this.gbxMigrationOption.Name = "gbxMigrationOption";
            this.gbxMigrationOption.Size = new System.Drawing.Size(592, 252);
            this.gbxMigrationOption.TabIndex = 4;
            this.gbxMigrationOption.TabStop = false;
            this.gbxMigrationOption.Text = "Migration Steps";
            this.gbxMigrationOption.Visible = false;
            // 
            // dgvMigrate
            // 
            this.dgvMigrate.AllowUserToAddRows = false;
            this.dgvMigrate.AllowUserToDeleteRows = false;
            this.dgvMigrate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMigrate.Location = new System.Drawing.Point(20, 28);
            this.dgvMigrate.MultiSelect = false;
            this.dgvMigrate.Name = "dgvMigrate";
            this.dgvMigrate.RowTemplate.Height = 24;
            this.dgvMigrate.Size = new System.Drawing.Size(566, 211);
            this.dgvMigrate.TabIndex = 0;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(12, 436);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(145, 41);
            this.btnPrevious.TabIndex = 5;
            this.btnPrevious.Text = "&Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(171, 436);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(145, 41);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "&Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // frmMigrateToSQLDW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(954, 500);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.gbxMigrationOption);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnMigrate);
            this.Controls.Add(this.gbxConnection);
            this.MaximizeBox = false;
            this.Name = "frmMigrateToSQLDW";
            this.Text = "Migrate to SQL Data Warehouse";
            this.gbxConnection.ResumeLayout(false);
            this.gbxConnection.PerformLayout();
            this.gbxMigrationOption.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMigrate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxConnection;
        private System.Windows.Forms.Button btnAzTestConnection;
        private System.Windows.Forms.TextBox txtAzDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAzPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAzUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMigrate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.ComboBox cmbConnections;
        private System.Windows.Forms.TextBox txtAzServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbxMigrationOption;
        private System.Windows.Forms.DataGridView dgvMigrate;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
    }
}