namespace SocialManager.frm.UserControls
{
    partial class ucSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tlpMain = new TableLayoutPanel();
            pnlGeneralSettings = new Panel();
            pnlGeneralActions = new Panel();
            btnChangePassword = new Button();
            btnResetSettings = new Button();
            btnSaveSettings = new Button();
            cmbLanguage = new ComboBox();
            lblLanguage = new Label();
            cmbTheme = new ComboBox();
            lblTheme = new Label();
            chkAutoSchedule = new CheckBox();
            chkNotifications = new CheckBox();
            txtEmail = new TextBox();
            lblEmail = new Label();
            txtUserName = new TextBox();
            lblUserName = new Label();
            txtAppName = new TextBox();
            lblAppName = new Label();
            lblGeneralTitle = new Label();
            pnlDataManagement = new Panel();
            pnlDataActions = new Panel();
            btnRestoreData = new Button();
            btnBackupData = new Button();
            lblDataInfo = new Label();
            lblDataTitle = new Label();
            tlpMain.SuspendLayout();
            pnlGeneralSettings.SuspendLayout();
            pnlGeneralActions.SuspendLayout();
            pnlDataManagement.SuspendLayout();
            pnlDataActions.SuspendLayout();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlpMain.Controls.Add(pnlGeneralSettings, 0, 0);
            tlpMain.Controls.Add(pnlDataManagement, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(30, 20, 30, 30);
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1120, 720);
            tlpMain.TabIndex = 0;
            // 
            // pnlGeneralSettings
            // 
            pnlGeneralSettings.BackColor = Color.White;
            pnlGeneralSettings.Controls.Add(pnlGeneralActions);
            pnlGeneralSettings.Controls.Add(cmbLanguage);
            pnlGeneralSettings.Controls.Add(lblLanguage);
            pnlGeneralSettings.Controls.Add(cmbTheme);
            pnlGeneralSettings.Controls.Add(lblTheme);
            pnlGeneralSettings.Controls.Add(chkAutoSchedule);
            pnlGeneralSettings.Controls.Add(chkNotifications);
            pnlGeneralSettings.Controls.Add(txtEmail);
            pnlGeneralSettings.Controls.Add(lblEmail);
            pnlGeneralSettings.Controls.Add(txtUserName);
            pnlGeneralSettings.Controls.Add(lblUserName);
            pnlGeneralSettings.Controls.Add(txtAppName);
            pnlGeneralSettings.Controls.Add(lblAppName);
            pnlGeneralSettings.Controls.Add(lblGeneralTitle);
            pnlGeneralSettings.Dock = DockStyle.Fill;
            pnlGeneralSettings.Location = new Point(30, 20);
            pnlGeneralSettings.Margin = new Padding(0, 0, 15, 0);
            pnlGeneralSettings.Name = "pnlGeneralSettings";
            pnlGeneralSettings.Padding = new Padding(25);
            pnlGeneralSettings.Size = new Size(621, 670);
            pnlGeneralSettings.TabIndex = 0;
            pnlGeneralSettings.Paint += pnlCard_Paint;
            // 
            // pnlGeneralActions
            // 
            pnlGeneralActions.Controls.Add(btnChangePassword);
            pnlGeneralActions.Controls.Add(btnResetSettings);
            pnlGeneralActions.Controls.Add(btnSaveSettings);
            pnlGeneralActions.Dock = DockStyle.Bottom;
            pnlGeneralActions.Location = new Point(25, 605);
            pnlGeneralActions.Name = "pnlGeneralActions";
            pnlGeneralActions.Size = new Size(571, 40);
            pnlGeneralActions.TabIndex = 13;
            // 
            // btnChangePassword
            // 
            btnChangePassword.BackColor = Color.FromArgb(230, 126, 34);
            btnChangePassword.FlatAppearance.BorderSize = 0;
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.Font = new Font("Segoe UI", 9F);
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.Location = new Point(240, 0);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(120, 35);
            btnChangePassword.TabIndex = 2;
            btnChangePassword.Text = "?? Change Password";
            btnChangePassword.UseVisualStyleBackColor = false;
            btnChangePassword.Click += btnChangePassword_Click;
            // 
            // btnResetSettings
            // 
            btnResetSettings.BackColor = Color.FromArgb(149, 165, 166);
            btnResetSettings.FlatAppearance.BorderSize = 0;
            btnResetSettings.FlatStyle = FlatStyle.Flat;
            btnResetSettings.Font = new Font("Segoe UI", 9F);
            btnResetSettings.ForeColor = Color.White;
            btnResetSettings.Location = new Point(110, 0);
            btnResetSettings.Name = "btnResetSettings";
            btnResetSettings.Size = new Size(120, 35);
            btnResetSettings.TabIndex = 1;
            btnResetSettings.Text = "?? Reset";
            btnResetSettings.UseVisualStyleBackColor = false;
            btnResetSettings.Click += btnResetSettings_Click;
            // 
            // btnSaveSettings
            // 
            btnSaveSettings.BackColor = Color.FromArgb(52, 152, 219);
            btnSaveSettings.FlatAppearance.BorderSize = 0;
            btnSaveSettings.FlatStyle = FlatStyle.Flat;
            btnSaveSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSaveSettings.ForeColor = Color.White;
            btnSaveSettings.Location = new Point(0, 0);
            btnSaveSettings.Name = "btnSaveSettings";
            btnSaveSettings.Size = new Size(100, 35);
            btnSaveSettings.TabIndex = 0;
            btnSaveSettings.Text = "?? Save";
            btnSaveSettings.UseVisualStyleBackColor = false;
            btnSaveSettings.Click += btnSaveSettings_Click;
            // 
            // cmbLanguage
            // 
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguage.Font = new Font("Segoe UI", 10F);
            cmbLanguage.FormattingEnabled = true;
            cmbLanguage.Items.AddRange(new object[] { "English", "Vietnamese", "French", "Spanish" });
            cmbLanguage.Location = new Point(25, 520);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new Size(200, 31);
            cmbLanguage.TabIndex = 12;
            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLanguage.ForeColor = Color.FromArgb(44, 62, 80);
            lblLanguage.Location = new Point(25, 490);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(86, 23);
            lblLanguage.TabIndex = 11;
            lblLanguage.Text = "Language";
            // 
            // cmbTheme
            // 
            cmbTheme.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTheme.Font = new Font("Segoe UI", 10F);
            cmbTheme.FormattingEnabled = true;
            cmbTheme.Items.AddRange(new object[] { "Light Theme", "Dark Theme", "Auto (System)" });
            cmbTheme.Location = new Point(25, 440);
            cmbTheme.Name = "cmbTheme";
            cmbTheme.Size = new Size(200, 31);
            cmbTheme.TabIndex = 10;
            // 
            // lblTheme
            // 
            lblTheme.AutoSize = true;
            lblTheme.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTheme.ForeColor = Color.FromArgb(44, 62, 80);
            lblTheme.Location = new Point(25, 410);
            lblTheme.Name = "lblTheme";
            lblTheme.Size = new Size(61, 23);
            lblTheme.TabIndex = 9;
            lblTheme.Text = "Theme";
            // 
            // chkAutoSchedule
            // 
            chkAutoSchedule.AutoSize = true;
            chkAutoSchedule.Font = new Font("Segoe UI", 10F);
            chkAutoSchedule.ForeColor = Color.FromArgb(127, 140, 141);
            chkAutoSchedule.Location = new Point(25, 370);
            chkAutoSchedule.Name = "chkAutoSchedule";
            chkAutoSchedule.Size = new Size(243, 27);
            chkAutoSchedule.TabIndex = 8;
            chkAutoSchedule.Text = "Enable automatic post scheduling";
            chkAutoSchedule.UseVisualStyleBackColor = true;
            // 
            // chkNotifications
            // 
            chkNotifications.AutoSize = true;
            chkNotifications.Font = new Font("Segoe UI", 10F);
            chkNotifications.ForeColor = Color.FromArgb(127, 140, 141);
            chkNotifications.Location = new Point(25, 330);
            chkNotifications.Name = "chkNotifications";
            chkNotifications.Size = new Size(178, 27);
            chkNotifications.TabIndex = 7;
            chkNotifications.Text = "Enable notifications";
            chkNotifications.UseVisualStyleBackColor = true;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(25, 280);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 30);
            txtEmail.TabIndex = 6;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(44, 62, 80);
            lblEmail.Location = new Point(25, 250);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(51, 23);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "Email";
            // 
            // txtUserName
            // 
            txtUserName.BorderStyle = BorderStyle.FixedSingle;
            txtUserName.Font = new Font("Segoe UI", 10F);
            txtUserName.Location = new Point(25, 200);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(300, 30);
            txtUserName.TabIndex = 4;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUserName.ForeColor = Color.FromArgb(44, 62, 80);
            lblUserName.Location = new Point(25, 170);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(94, 23);
            lblUserName.TabIndex = 3;
            lblUserName.Text = "Username";
            // 
            // txtAppName
            // 
            txtAppName.BorderStyle = BorderStyle.FixedSingle;
            txtAppName.Font = new Font("Segoe UI", 10F);
            txtAppName.Location = new Point(25, 120);
            txtAppName.Name = "txtAppName";
            txtAppName.Size = new Size(300, 30);
            txtAppName.TabIndex = 2;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAppName.ForeColor = Color.FromArgb(44, 62, 80);
            lblAppName.Location = new Point(25, 90);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(134, 23);
            lblAppName.TabIndex = 1;
            lblAppName.Text = "Application Name";
            // 
            // lblGeneralTitle
            // 
            lblGeneralTitle.AutoSize = true;
            lblGeneralTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblGeneralTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblGeneralTitle.Location = new Point(25, 25);
            lblGeneralTitle.Name = "lblGeneralTitle";
            lblGeneralTitle.Size = new Size(178, 32);
            lblGeneralTitle.TabIndex = 0;
            lblGeneralTitle.Text = "General Settings";
            // 
            // pnlDataManagement
            // 
            pnlDataManagement.BackColor = Color.White;
            pnlDataManagement.Controls.Add(pnlDataActions);
            pnlDataManagement.Controls.Add(lblDataInfo);
            pnlDataManagement.Controls.Add(lblDataTitle);
            pnlDataManagement.Dock = DockStyle.Fill;
            pnlDataManagement.Location = new Point(681, 20);
            pnlDataManagement.Margin = new Padding(15, 0, 0, 0);
            pnlDataManagement.Name = "pnlDataManagement";
            pnlDataManagement.Padding = new Padding(25);
            pnlDataManagement.Size = new Size(409, 670);
            pnlDataManagement.TabIndex = 1;
            pnlDataManagement.Paint += pnlCard_Paint;
            // 
            // pnlDataActions
            // 
            pnlDataActions.Controls.Add(btnRestoreData);
            pnlDataActions.Controls.Add(btnBackupData);
            pnlDataActions.Location = new Point(25, 180);
            pnlDataActions.Name = "pnlDataActions";
            pnlDataActions.Size = new Size(359, 100);
            pnlDataActions.TabIndex = 2;
            // 
            // btnRestoreData
            // 
            btnRestoreData.BackColor = Color.FromArgb(231, 76, 60);
            btnRestoreData.FlatAppearance.BorderSize = 0;
            btnRestoreData.FlatStyle = FlatStyle.Flat;
            btnRestoreData.Font = new Font("Segoe UI", 10F);
            btnRestoreData.ForeColor = Color.White;
            btnRestoreData.Location = new Point(0, 50);
            btnRestoreData.Name = "btnRestoreData";
            btnRestoreData.Size = new Size(359, 40);
            btnRestoreData.TabIndex = 1;
            btnRestoreData.Text = "?? Restore Data from Backup";
            btnRestoreData.UseVisualStyleBackColor = false;
            btnRestoreData.Click += btnRestoreData_Click;
            // 
            // btnBackupData
            // 
            btnBackupData.BackColor = Color.FromArgb(46, 204, 113);
            btnBackupData.FlatAppearance.BorderSize = 0;
            btnBackupData.FlatStyle = FlatStyle.Flat;
            btnBackupData.Font = new Font("Segoe UI", 10F);
            btnBackupData.ForeColor = Color.White;
            btnBackupData.Location = new Point(0, 0);
            btnBackupData.Name = "btnBackupData";
            btnBackupData.Size = new Size(359, 40);
            btnBackupData.TabIndex = 0;
            btnBackupData.Text = "?? Create Data Backup";
            btnBackupData.UseVisualStyleBackColor = false;
            btnBackupData.Click += btnBackupData_Click;
            // 
            // lblDataInfo
            // 
            lblDataInfo.Font = new Font("Segoe UI", 10F);
            lblDataInfo.ForeColor = Color.FromArgb(127, 140, 141);
            lblDataInfo.Location = new Point(25, 70);
            lblDataInfo.Name = "lblDataInfo";
            lblDataInfo.Size = new Size(359, 100);
            lblDataInfo.TabIndex = 1;
            lblDataInfo.Text = "Manage your application data with backup and restore functions. Regular backups are recommended to prevent data loss.";
            // 
            // lblDataTitle
            // 
            lblDataTitle.AutoSize = true;
            lblDataTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblDataTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblDataTitle.Location = new Point(25, 25);
            lblDataTitle.Name = "lblDataTitle";
            lblDataTitle.Size = new Size(206, 32);
            lblDataTitle.TabIndex = 0;
            lblDataTitle.Text = "Data Management";
            // 
            // ucSettings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpMain);
            Font = new Font("Segoe UI", 9F);
            Name = "ucSettings";
            Size = new Size(1120, 720);
            tlpMain.ResumeLayout(false);
            pnlGeneralSettings.ResumeLayout(false);
            pnlGeneralSettings.PerformLayout();
            pnlGeneralActions.ResumeLayout(false);
            pnlDataManagement.ResumeLayout(false);
            pnlDataManagement.PerformLayout();
            pnlDataActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMain;
        private Panel pnlGeneralSettings;
        private Panel pnlGeneralActions;
        private Button btnChangePassword;
        private Button btnResetSettings;
        private Button btnSaveSettings;
        private ComboBox cmbLanguage;
        private Label lblLanguage;
        private ComboBox cmbTheme;
        private Label lblTheme;
        private CheckBox chkAutoSchedule;
        private CheckBox chkNotifications;
        private TextBox txtEmail;
        private Label lblEmail;
        private TextBox txtUserName;
        private Label lblUserName;
        private TextBox txtAppName;
        private Label lblAppName;
        private Label lblGeneralTitle;
        private Panel pnlDataManagement;
        private Panel pnlDataActions;
        private Button btnRestoreData;
        private Button btnBackupData;
        private Label lblDataInfo;
        private Label lblDataTitle;
    }
}