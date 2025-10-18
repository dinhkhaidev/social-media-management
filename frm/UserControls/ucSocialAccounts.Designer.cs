namespace SocialManager.frm.UserControls
{
    partial class ucSocialAccounts
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
            pnlAccountsList = new Panel();
            pnlAccountActions = new Panel();
            btnTestConnection = new Button();
            btnRefresh = new Button();
            btnEdit = new Button();
            btnDisconnect = new Button();
            btnAddAccount = new Button();
            dgvSocialAccounts = new DataGridView();
            lblAccountsListTitle = new Label();
            pnlConnectAccount = new Panel();
            pnlConnectionActions = new Panel();
            btnConnectAccount = new Button();
            txtAccountUsername = new TextBox();
            lblAccountUsername = new Label();
            cmbPlatformSelect = new ComboBox();
            lblPlatformSelect = new Label();
            lblConnectionInfo = new Label();
            lblConnectTitle = new Label();
            tlpMain.SuspendLayout();
            pnlAccountsList.SuspendLayout();
            pnlAccountActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSocialAccounts).BeginInit();
            pnlConnectAccount.SuspendLayout();
            pnlConnectionActions.SuspendLayout();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpMain.Controls.Add(pnlAccountsList, 0, 0);
            tlpMain.Controls.Add(pnlConnectAccount, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(30, 20, 30, 30);
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1120, 720);
            tlpMain.TabIndex = 0;
            // 
            // pnlAccountsList
            // 
            pnlAccountsList.BackColor = Color.White;
            pnlAccountsList.Controls.Add(pnlAccountActions);
            pnlAccountsList.Controls.Add(dgvSocialAccounts);
            pnlAccountsList.Controls.Add(lblAccountsListTitle);
            pnlAccountsList.Dock = DockStyle.Fill;
            pnlAccountsList.Location = new Point(30, 20);
            pnlAccountsList.Margin = new Padding(0, 0, 15, 0);
            pnlAccountsList.Name = "pnlAccountsList";
            pnlAccountsList.Padding = new Padding(25);
            pnlAccountsList.Size = new Size(742, 670);
            pnlAccountsList.TabIndex = 0;
            pnlAccountsList.Paint += pnlCard_Paint;
            // 
            // pnlAccountActions
            // 
            pnlAccountActions.Controls.Add(btnTestConnection);
            pnlAccountActions.Controls.Add(btnRefresh);
            pnlAccountActions.Controls.Add(btnEdit);
            pnlAccountActions.Controls.Add(btnDisconnect);
            pnlAccountActions.Controls.Add(btnAddAccount);
            pnlAccountActions.Dock = DockStyle.Bottom;
            pnlAccountActions.Location = new Point(25, 605);
            pnlAccountActions.Name = "pnlAccountActions";
            pnlAccountActions.Size = new Size(692, 40);
            pnlAccountActions.TabIndex = 2;
            // 
            // btnTestConnection
            // 
            btnTestConnection.BackColor = Color.FromArgb(155, 89, 182);
            btnTestConnection.FlatAppearance.BorderSize = 0;
            btnTestConnection.FlatStyle = FlatStyle.Flat;
            btnTestConnection.Font = new Font("Segoe UI", 9F);
            btnTestConnection.ForeColor = Color.White;
            btnTestConnection.Location = new Point(400, 0);
            btnTestConnection.Name = "btnTestConnection";
            btnTestConnection.Size = new Size(100, 35);
            btnTestConnection.TabIndex = 4;
            btnTestConnection.Text = "?? Test";
            btnTestConnection.UseVisualStyleBackColor = false;
            btnTestConnection.Click += btnTestConnection_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(149, 165, 166);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(290, 0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 35);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "?? Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(230, 126, 34);
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Segoe UI", 9F);
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(180, 0);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 35);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "?? Edit";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.BackColor = Color.FromArgb(231, 76, 60);
            btnDisconnect.FlatAppearance.BorderSize = 0;
            btnDisconnect.FlatStyle = FlatStyle.Flat;
            btnDisconnect.Font = new Font("Segoe UI", 9F);
            btnDisconnect.ForeColor = Color.White;
            btnDisconnect.Location = new Point(70, 0);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(100, 35);
            btnDisconnect.TabIndex = 1;
            btnDisconnect.Text = "?? Disconnect";
            btnDisconnect.UseVisualStyleBackColor = false;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnAddAccount
            // 
            btnAddAccount.BackColor = Color.FromArgb(52, 152, 219);
            btnAddAccount.FlatAppearance.BorderSize = 0;
            btnAddAccount.FlatStyle = FlatStyle.Flat;
            btnAddAccount.Font = new Font("Segoe UI", 9F);
            btnAddAccount.ForeColor = Color.White;
            btnAddAccount.Location = new Point(0, 0);
            btnAddAccount.Name = "btnAddAccount";
            btnAddAccount.Size = new Size(60, 35);
            btnAddAccount.TabIndex = 0;
            btnAddAccount.Text = "? Add";
            btnAddAccount.UseVisualStyleBackColor = false;
            btnAddAccount.Click += btnAddAccount_Click;
            // 
            // dgvSocialAccounts
            // 
            dgvSocialAccounts.AllowUserToAddRows = false;
            dgvSocialAccounts.AllowUserToDeleteRows = false;
            dgvSocialAccounts.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvSocialAccounts.BackgroundColor = Color.White;
            dgvSocialAccounts.BorderStyle = BorderStyle.None;
            dgvSocialAccounts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSocialAccounts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSocialAccounts.GridColor = Color.FromArgb(234, 236, 238);
            dgvSocialAccounts.Location = new Point(25, 65);
            dgvSocialAccounts.MultiSelect = false;
            dgvSocialAccounts.Name = "dgvSocialAccounts";
            dgvSocialAccounts.ReadOnly = true;
            dgvSocialAccounts.RowHeadersVisible = false;
            dgvSocialAccounts.RowHeadersWidth = 51;
            dgvSocialAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSocialAccounts.Size = new Size(692, 530);
            dgvSocialAccounts.TabIndex = 1;
            // 
            // lblAccountsListTitle
            // 
            lblAccountsListTitle.AutoSize = true;
            lblAccountsListTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblAccountsListTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblAccountsListTitle.Location = new Point(25, 25);
            lblAccountsListTitle.Name = "lblAccountsListTitle";
            lblAccountsListTitle.Size = new Size(194, 32);
            lblAccountsListTitle.TabIndex = 0;
            lblAccountsListTitle.Text = "Social Accounts";
            // 
            // pnlConnectAccount
            // 
            pnlConnectAccount.BackColor = Color.White;
            pnlConnectAccount.Controls.Add(pnlConnectionActions);
            pnlConnectAccount.Controls.Add(txtAccountUsername);
            pnlConnectAccount.Controls.Add(lblAccountUsername);
            pnlConnectAccount.Controls.Add(cmbPlatformSelect);
            pnlConnectAccount.Controls.Add(lblPlatformSelect);
            pnlConnectAccount.Controls.Add(lblConnectionInfo);
            pnlConnectAccount.Controls.Add(lblConnectTitle);
            pnlConnectAccount.Dock = DockStyle.Fill;
            pnlConnectAccount.Location = new Point(802, 20);
            pnlConnectAccount.Margin = new Padding(15, 0, 0, 0);
            pnlConnectAccount.Name = "pnlConnectAccount";
            pnlConnectAccount.Padding = new Padding(25);
            pnlConnectAccount.Size = new Size(288, 670);
            pnlConnectAccount.TabIndex = 1;
            pnlConnectAccount.Paint += pnlCard_Paint;
            // 
            // pnlConnectionActions
            // 
            pnlConnectionActions.Controls.Add(btnConnectAccount);
            pnlConnectionActions.Location = new Point(25, 300);
            pnlConnectionActions.Name = "pnlConnectionActions";
            pnlConnectionActions.Size = new Size(238, 50);
            pnlConnectionActions.TabIndex = 6;
            // 
            // btnConnectAccount
            // 
            btnConnectAccount.BackColor = Color.FromArgb(46, 204, 113);
            btnConnectAccount.FlatAppearance.BorderSize = 0;
            btnConnectAccount.FlatStyle = FlatStyle.Flat;
            btnConnectAccount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConnectAccount.ForeColor = Color.White;
            btnConnectAccount.Location = new Point(0, 0);
            btnConnectAccount.Name = "btnConnectAccount";
            btnConnectAccount.Size = new Size(238, 40);
            btnConnectAccount.TabIndex = 0;
            btnConnectAccount.Text = "?? Connect Account";
            btnConnectAccount.UseVisualStyleBackColor = false;
            btnConnectAccount.Click += btnConnectAccount_Click;
            // 
            // txtAccountUsername
            // 
            txtAccountUsername.BorderStyle = BorderStyle.FixedSingle;
            txtAccountUsername.Font = new Font("Segoe UI", 10F);
            txtAccountUsername.Location = new Point(25, 250);
            txtAccountUsername.Name = "txtAccountUsername";
            txtAccountUsername.PlaceholderText = "Enter username...";
            txtAccountUsername.Size = new Size(238, 30);
            txtAccountUsername.TabIndex = 5;
            // 
            // lblAccountUsername
            // 
            lblAccountUsername.AutoSize = true;
            lblAccountUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAccountUsername.ForeColor = Color.FromArgb(44, 62, 80);
            lblAccountUsername.Location = new Point(25, 220);
            lblAccountUsername.Name = "lblAccountUsername";
            lblAccountUsername.Size = new Size(133, 23);
            lblAccountUsername.TabIndex = 4;
            lblAccountUsername.Text = "Account Name:";
            // 
            // cmbPlatformSelect
            // 
            cmbPlatformSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlatformSelect.Font = new Font("Segoe UI", 10F);
            cmbPlatformSelect.FormattingEnabled = true;
            cmbPlatformSelect.Items.AddRange(new object[] { "?? Facebook", "?? Twitter", "?? Instagram", "?? LinkedIn", "?? TikTok", "?? Pinterest", "?? Snapchat", "?? YouTube" });
            cmbPlatformSelect.Location = new Point(25, 170);
            cmbPlatformSelect.Name = "cmbPlatformSelect";
            cmbPlatformSelect.Size = new Size(238, 31);
            cmbPlatformSelect.TabIndex = 3;
            // 
            // lblPlatformSelect
            // 
            lblPlatformSelect.AutoSize = true;
            lblPlatformSelect.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPlatformSelect.ForeColor = Color.FromArgb(44, 62, 80);
            lblPlatformSelect.Location = new Point(25, 140);
            lblPlatformSelect.Name = "lblPlatformSelect";
            lblPlatformSelect.Size = new Size(84, 23);
            lblPlatformSelect.TabIndex = 2;
            lblPlatformSelect.Text = "Platform:";
            // 
            // lblConnectionInfo
            // 
            lblConnectionInfo.Font = new Font("Segoe UI", 10F);
            lblConnectionInfo.ForeColor = Color.FromArgb(127, 140, 141);
            lblConnectionInfo.Location = new Point(25, 70);
            lblConnectionInfo.Name = "lblConnectionInfo";
            lblConnectionInfo.Size = new Size(238, 60);
            lblConnectionInfo.TabIndex = 1;
            lblConnectionInfo.Text = "Connect your social media accounts to manage all your posts from one place.";
            // 
            // lblConnectTitle
            // 
            lblConnectTitle.AutoSize = true;
            lblConnectTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblConnectTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblConnectTitle.Location = new Point(25, 25);
            lblConnectTitle.Name = "lblConnectTitle";
            lblConnectTitle.Size = new Size(199, 32);
            lblConnectTitle.TabIndex = 0;
            lblConnectTitle.Text = "Connect Account";
            // 
            // ucSocialAccounts
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpMain);
            Font = new Font("Segoe UI", 9F);
            Name = "ucSocialAccounts";
            Size = new Size(1120, 720);
            tlpMain.ResumeLayout(false);
            pnlAccountsList.ResumeLayout(false);
            pnlAccountsList.PerformLayout();
            pnlAccountActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSocialAccounts).EndInit();
            pnlConnectAccount.ResumeLayout(false);
            pnlConnectAccount.PerformLayout();
            pnlConnectionActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMain;
        private Panel pnlAccountsList;
        private Panel pnlAccountActions;
        private Button btnTestConnection;
        private Button btnRefresh;
        private Button btnEdit;
        private Button btnDisconnect;
        private Button btnAddAccount;
        private DataGridView dgvSocialAccounts;
        private Label lblAccountsListTitle;
        private Panel pnlConnectAccount;
        private Panel pnlConnectionActions;
        private Button btnConnectAccount;
        private TextBox txtAccountUsername;
        private Label lblAccountUsername;
        private ComboBox cmbPlatformSelect;
        private Label lblPlatformSelect;
        private Label lblConnectionInfo;
        private Label lblConnectTitle;
    }
}