namespace SQLDwGenerator
{
    partial class frmConfig
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
            this.cmbConnectionType = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbAuthentication = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.optBCPUnicode = new System.Windows.Forms.RadioButton();
            this.optBCPUTF8 = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.txtBCPOutputFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtAzStorageAccount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAzContainer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAzSASToken = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtConfigName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtAZCopy = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt7Zip = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cmbConfigFile = new System.Windows.Forms.ComboBox();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.txtAzStorageKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbConnectionType);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.cmbAuthentication);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtDatabaseName);
            this.groupBox1.Controls.Add(this.txtServerName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(18, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source Connection";
            // 
            // cmbConnectionType
            // 
            this.cmbConnectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnectionType.FormattingEnabled = true;
            this.cmbConnectionType.Location = new System.Drawing.Point(124, 18);
            this.cmbConnectionType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbConnectionType.Name = "cmbConnectionType";
            this.cmbConnectionType.Size = new System.Drawing.Size(175, 21);
            this.cmbConnectionType.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(88, 13);
            this.label17.TabIndex = 10;
            this.label17.Text = "Connection Type";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(441, 67);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(176, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(124, 69);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(176, 20);
            this.txtUserName.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(328, 72);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "Password";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 70);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 13);
            this.label16.TabIndex = 6;
            this.label16.Text = "User Name";
            // 
            // cmbAuthentication
            // 
            this.cmbAuthentication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAuthentication.FormattingEnabled = true;
            this.cmbAuthentication.Location = new System.Drawing.Point(441, 18);
            this.cmbAuthentication.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbAuthentication.Name = "cmbAuthentication";
            this.cmbAuthentication.Size = new System.Drawing.Size(175, 21);
            this.cmbAuthentication.TabIndex = 1;
            this.cmbAuthentication.SelectedIndexChanged += new System.EventHandler(this.cmbAuthentication_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(328, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Authentication";
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(441, 43);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(176, 20);
            this.txtDatabaseName.TabIndex = 3;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(124, 42);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(176, 20);
            this.txtServerName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(328, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.optBCPUnicode);
            this.groupBox2.Controls.Add(this.optBCPUTF8);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtBCPOutputFolder);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(18, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(634, 93);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bulk Copy Options";
            // 
            // optBCPUnicode
            // 
            this.optBCPUnicode.AutoSize = true;
            this.optBCPUnicode.Location = new System.Drawing.Point(183, 58);
            this.optBCPUnicode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.optBCPUnicode.Name = "optBCPUnicode";
            this.optBCPUnicode.Size = new System.Drawing.Size(65, 17);
            this.optBCPUnicode.TabIndex = 2;
            this.optBCPUnicode.Text = "Unicode";
            this.optBCPUnicode.UseVisualStyleBackColor = true;
            // 
            // optBCPUTF8
            // 
            this.optBCPUTF8.AutoSize = true;
            this.optBCPUTF8.Checked = true;
            this.optBCPUTF8.Location = new System.Drawing.Point(122, 58);
            this.optBCPUTF8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.optBCPUTF8.Name = "optBCPUTF8";
            this.optBCPUTF8.Size = new System.Drawing.Size(52, 17);
            this.optBCPUTF8.TabIndex = 1;
            this.optBCPUTF8.TabStop = true;
            this.optBCPUTF8.Text = "UTF8";
            this.optBCPUTF8.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 58);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "BCP Output Format";
            // 
            // txtBCPOutputFolder
            // 
            this.txtBCPOutputFolder.Location = new System.Drawing.Point(122, 28);
            this.txtBCPOutputFolder.Name = "txtBCPOutputFolder";
            this.txtBCPOutputFolder.Size = new System.Drawing.Size(494, 20);
            this.txtBCPOutputFolder.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "BCP Output Folder";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtAzStorageKey);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtAzStorageAccount);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtAzContainer);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtAzSASToken);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(18, 234);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(634, 114);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Azure Storage";
            // 
            // txtAzStorageAccount
            // 
            this.txtAzStorageAccount.Location = new System.Drawing.Point(122, 19);
            this.txtAzStorageAccount.Name = "txtAzStorageAccount";
            this.txtAzStorageAccount.Size = new System.Drawing.Size(176, 20);
            this.txtAzStorageAccount.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Storage Account";
            // 
            // txtAzContainer
            // 
            this.txtAzContainer.Location = new System.Drawing.Point(442, 19);
            this.txtAzContainer.Name = "txtAzContainer";
            this.txtAzContainer.Size = new System.Drawing.Size(176, 20);
            this.txtAzContainer.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(328, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Container Name";
            // 
            // txtAzSASToken
            // 
            this.txtAzSASToken.Location = new System.Drawing.Point(122, 47);
            this.txtAzSASToken.Name = "txtAzSASToken";
            this.txtAzSASToken.Size = new System.Drawing.Size(494, 20);
            this.txtAzSASToken.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "SAS Token";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(25, 599);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtConfigName);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(18, 442);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(634, 53);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Configuration Name";
            // 
            // txtConfigName
            // 
            this.txtConfigName.Location = new System.Drawing.Point(118, 19);
            this.txtConfigName.Name = "txtConfigName";
            this.txtConfigName.Size = new System.Drawing.Size(174, 20);
            this.txtConfigName.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Name";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(155, 599);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Controls.Add(this.groupBox1);
            this.groupBox5.Location = new System.Drawing.Point(25, 74);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(666, 518);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtAZCopy);
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.txt7Zip);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Location = new System.Drawing.Point(18, 354);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(634, 82);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Utility";
            // 
            // txtAZCopy
            // 
            this.txtAZCopy.Location = new System.Drawing.Point(122, 45);
            this.txtAZCopy.Name = "txtAZCopy";
            this.txtAZCopy.Size = new System.Drawing.Size(495, 20);
            this.txtAZCopy.TabIndex = 1;
            this.txtAZCopy.Text = "C:\\Program Files (x86)\\Microsoft SDKs\\Azure\\AzCopy\\AzCopy.exe";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "AZCopy Location";
            // 
            // txt7Zip
            // 
            this.txt7Zip.Location = new System.Drawing.Point(123, 19);
            this.txt7Zip.Name = "txt7Zip";
            this.txt7Zip.Size = new System.Drawing.Size(495, 20);
            this.txt7Zip.TabIndex = 0;
            this.txt7Zip.Text = "C:\\Program Files (x86)\\7-Zip\\7z.exe";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "7-Zip Location";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cmbConfigFile);
            this.groupBox6.Location = new System.Drawing.Point(25, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(666, 55);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Chose Configuration File";
            // 
            // cmbConfigFile
            // 
            this.cmbConfigFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConfigFile.FormattingEnabled = true;
            this.cmbConfigFile.Location = new System.Drawing.Point(25, 24);
            this.cmbConfigFile.Name = "cmbConfigFile";
            this.cmbConfigFile.Size = new System.Drawing.Size(610, 21);
            this.cmbConfigFile.TabIndex = 0;
            this.cmbConfigFile.SelectedIndexChanged += new System.EventHandler(this.cmbConfigFile_SelectedIndexChanged);
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadConfig.Location = new System.Drawing.Point(532, 599);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(159, 25);
            this.btnLoadConfig.TabIndex = 3;
            this.btnLoadConfig.Text = "Load Configuration";
            this.btnLoadConfig.UseVisualStyleBackColor = true;
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(291, 599);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(146, 26);
            this.btnDefault.TabIndex = 2;
            this.btnDefault.Text = "Load Default Values";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // txtAzStorageKey
            // 
            this.txtAzStorageKey.Location = new System.Drawing.Point(122, 73);
            this.txtAzStorageKey.Name = "txtAzStorageKey";
            this.txtAzStorageKey.Size = new System.Drawing.Size(494, 20);
            this.txtAzStorageKey.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Storage Key";
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(703, 636);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnLoadConfig);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.MaximizeBox = false;
            this.Name = "frmConfig";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBCPOutputFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtConfigName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cmbConfigFile;
        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.TextBox txtAzStorageAccount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtAzContainer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAzSASToken;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtAZCopy;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt7Zip;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.RadioButton optBCPUnicode;
        private System.Windows.Forms.RadioButton optBCPUTF8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbAuthentication;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbConnectionType;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtAzStorageKey;
        private System.Windows.Forms.Label label4;
    }
}