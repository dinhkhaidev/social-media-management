namespace SocialManager.frm.UserControls
{
    partial class ucDashboard
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
            pnlMain = new Panel();
            lblTitle = new Label();
            pnlStats = new Panel();
            lblTotalPostsValue = new Label();
            lblGrowthValue = new Label();
            lblInteractionsValue = new Label();
            lblActiveAccountsValue = new Label();
            pnlComposer = new Panel();
            lblComposerTitle = new Label();
            txtPostContent = new TextBox();
            cmbPlatform = new ComboBox();
            dtpSchedule = new DateTimePicker();
            btnPostNow = new Button();
            btnSchedule = new Button();
            btnAddPhoto = new Button();
            pnlActivity = new Panel();
            lblActivityTitle = new Label();
            lstRecentActivity = new ListBox();
            btnViewAllActivity = new Button();
            picTotalPosts = new PictureBox();
            picGrowth = new PictureBox();
            picInteractions = new PictureBox();
            picActiveAccounts = new PictureBox();
            pnlMain.SuspendLayout();
            pnlStats.SuspendLayout();
            pnlComposer.SuspendLayout();
            pnlActivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picTotalPosts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picGrowth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picInteractions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picActiveAccounts).BeginInit();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(247, 249, 252);
            pnlMain.Controls.Add(pnlActivity);
            pnlMain.Controls.Add(pnlComposer);
            pnlMain.Controls.Add(pnlStats);
            pnlMain.Controls.Add(lblTitle);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(30, 20, 30, 30);
            pnlMain.Size = new Size(1120, 720);
            pnlMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(30, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(184, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Dashboard";
            // 
            // pnlStats
            // 
            pnlStats.BackColor = Color.White;
            pnlStats.Controls.Add(picActiveAccounts);
            pnlStats.Controls.Add(picInteractions);
            pnlStats.Controls.Add(picGrowth);
            pnlStats.Controls.Add(picTotalPosts);
            pnlStats.Controls.Add(lblActiveAccountsValue);
            pnlStats.Controls.Add(lblInteractionsValue);
            pnlStats.Controls.Add(lblGrowthValue);
            pnlStats.Controls.Add(lblTotalPostsValue);
            pnlStats.Location = new Point(30, 90);
            pnlStats.Name = "pnlStats";
            pnlStats.Padding = new Padding(20);
            pnlStats.Size = new Size(1060, 150);
            pnlStats.TabIndex = 1;
            pnlStats.Paint += pnlCard_Paint;
            // 
            // lblTotalPostsValue
            // 
            lblTotalPostsValue.AutoSize = true;
            lblTotalPostsValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotalPostsValue.ForeColor = Color.FromArgb(52, 152, 219);
            lblTotalPostsValue.Location = new Point(80, 40);
            lblTotalPostsValue.Name = "lblTotalPostsValue";
            lblTotalPostsValue.Size = new Size(73, 41);
            lblTotalPostsValue.TabIndex = 0;
            lblTotalPostsValue.Text = "1.2K";
            // 
            // lblGrowthValue
            // 
            lblGrowthValue.AutoSize = true;
            lblGrowthValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblGrowthValue.ForeColor = Color.FromArgb(39, 174, 96);
            lblGrowthValue.Location = new Point(350, 40);
            lblGrowthValue.Name = "lblGrowthValue";
            lblGrowthValue.Size = new Size(89, 41);
            lblGrowthValue.TabIndex = 1;
            lblGrowthValue.Text = "+12%";
            // 
            // lblInteractionsValue
            // 
            lblInteractionsValue.AutoSize = true;
            lblInteractionsValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblInteractionsValue.ForeColor = Color.FromArgb(231, 76, 60);
            lblInteractionsValue.Location = new Point(620, 40);
            lblInteractionsValue.Name = "lblInteractionsValue";
            lblInteractionsValue.Size = new Size(65, 41);
            lblInteractionsValue.TabIndex = 2;
            lblInteractionsValue.Text = "85K";
            // 
            // lblActiveAccountsValue
            // 
            lblActiveAccountsValue.AutoSize = true;
            lblActiveAccountsValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblActiveAccountsValue.ForeColor = Color.FromArgb(46, 204, 113);
            lblActiveAccountsValue.Location = new Point(890, 40);
            lblActiveAccountsValue.Name = "lblActiveAccountsValue";
            lblActiveAccountsValue.Size = new Size(32, 41);
            lblActiveAccountsValue.TabIndex = 3;
            lblActiveAccountsValue.Text = "5";
            // 
            // pnlComposer
            // 
            pnlComposer.BackColor = Color.White;
            pnlComposer.Controls.Add(btnAddPhoto);
            pnlComposer.Controls.Add(btnSchedule);
            pnlComposer.Controls.Add(btnPostNow);
            pnlComposer.Controls.Add(dtpSchedule);
            pnlComposer.Controls.Add(cmbPlatform);
            pnlComposer.Controls.Add(txtPostContent);
            pnlComposer.Controls.Add(lblComposerTitle);
            pnlComposer.Location = new Point(30, 260);
            pnlComposer.Name = "pnlComposer";
            pnlComposer.Padding = new Padding(20);
            pnlComposer.Size = new Size(640, 400);
            pnlComposer.TabIndex = 2;
            pnlComposer.Paint += pnlCard_Paint;
            // 
            // lblComposerTitle
            // 
            lblComposerTitle.AutoSize = true;
            lblComposerTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblComposerTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblComposerTitle.Location = new Point(20, 20);
            lblComposerTitle.Name = "lblComposerTitle";
            lblComposerTitle.Size = new Size(201, 32);
            lblComposerTitle.TabIndex = 0;
            lblComposerTitle.Text = "Create New Post";
            // 
            // txtPostContent
            // 
            txtPostContent.Location = new Point(20, 70);
            txtPostContent.Multiline = true;
            txtPostContent.Name = "txtPostContent";
            txtPostContent.PlaceholderText = "What's on your mind?";
            txtPostContent.ScrollBars = ScrollBars.Vertical;
            txtPostContent.Size = new Size(600, 200);
            txtPostContent.TabIndex = 1;
            // 
            // cmbPlatform
            // 
            cmbPlatform.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlatform.FormattingEnabled = true;
            cmbPlatform.Items.AddRange(new object[] { "?? Facebook", "?? Twitter", "?? Instagram", "?? LinkedIn", "?? TikTok" });
            cmbPlatform.Location = new Point(20, 290);
            cmbPlatform.Name = "cmbPlatform";
            cmbPlatform.Size = new Size(150, 28);
            cmbPlatform.TabIndex = 2;
            // 
            // dtpSchedule
            // 
            dtpSchedule.Format = DateTimePickerFormat.Short;
            dtpSchedule.Location = new Point(190, 290);
            dtpSchedule.Name = "dtpSchedule";
            dtpSchedule.Size = new Size(150, 27);
            dtpSchedule.TabIndex = 3;
            // 
            // btnPostNow
            // 
            btnPostNow.BackColor = Color.FromArgb(52, 152, 219);
            btnPostNow.FlatStyle = FlatStyle.Flat;
            btnPostNow.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPostNow.ForeColor = Color.White;
            btnPostNow.Location = new Point(20, 340);
            btnPostNow.Name = "btnPostNow";
            btnPostNow.Size = new Size(100, 40);
            btnPostNow.TabIndex = 4;
            btnPostNow.Text = "Post Now";
            btnPostNow.UseVisualStyleBackColor = false;
            btnPostNow.Click += btnPostNow_Click;
            // 
            // btnSchedule
            // 
            btnSchedule.BackColor = Color.FromArgb(230, 126, 34);
            btnSchedule.FlatStyle = FlatStyle.Flat;
            btnSchedule.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSchedule.ForeColor = Color.White;
            btnSchedule.Location = new Point(140, 340);
            btnSchedule.Name = "btnSchedule";
            btnSchedule.Size = new Size(100, 40);
            btnSchedule.TabIndex = 5;
            btnSchedule.Text = "Schedule";
            btnSchedule.UseVisualStyleBackColor = false;
            btnSchedule.Click += btnSchedule_Click;
            // 
            // btnAddPhoto
            // 
            btnAddPhoto.BackColor = Color.FromArgb(149, 165, 166);
            btnAddPhoto.FlatStyle = FlatStyle.Flat;
            btnAddPhoto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddPhoto.ForeColor = Color.White;
            btnAddPhoto.Location = new Point(260, 340);
            btnAddPhoto.Name = "btnAddPhoto";
            btnAddPhoto.Size = new Size(100, 40);
            btnAddPhoto.TabIndex = 6;
            btnAddPhoto.Text = "Add Photo";
            btnAddPhoto.UseVisualStyleBackColor = false;
            btnAddPhoto.Click += btnAddPhoto_Click;
            // 
            // pnlActivity
            // 
            pnlActivity.BackColor = Color.White;
            pnlActivity.Controls.Add(btnViewAllActivity);
            pnlActivity.Controls.Add(lstRecentActivity);
            pnlActivity.Controls.Add(lblActivityTitle);
            pnlActivity.Location = new Point(690, 260);
            pnlActivity.Name = "pnlActivity";
            pnlActivity.Padding = new Padding(20);
            pnlActivity.Size = new Size(400, 400);
            pnlActivity.TabIndex = 3;
            pnlActivity.Paint += pnlCard_Paint;
            // 
            // lblActivityTitle
            // 
            lblActivityTitle.AutoSize = true;
            lblActivityTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblActivityTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblActivityTitle.Location = new Point(20, 20);
            lblActivityTitle.Name = "lblActivityTitle";
            lblActivityTitle.Size = new Size(185, 32);
            lblActivityTitle.TabIndex = 0;
            lblActivityTitle.Text = "Recent Activity";
            // 
            // lstRecentActivity
            // 
            lstRecentActivity.BorderStyle = BorderStyle.None;
            lstRecentActivity.Location = new Point(20, 70);
            lstRecentActivity.Name = "lstRecentActivity";
            lstRecentActivity.Size = new Size(360, 280);
            lstRecentActivity.TabIndex = 1;
            // 
            // btnViewAllActivity
            // 
            btnViewAllActivity.BackColor = Color.FromArgb(149, 165, 166);
            btnViewAllActivity.FlatStyle = FlatStyle.Flat;
            btnViewAllActivity.Font = new Font("Segoe UI", 10F);
            btnViewAllActivity.ForeColor = Color.White;
            btnViewAllActivity.Location = new Point(20, 360);
            btnViewAllActivity.Name = "btnViewAllActivity";
            btnViewAllActivity.Size = new Size(360, 30);
            btnViewAllActivity.TabIndex = 2;
            btnViewAllActivity.Text = "View All Activity";
            btnViewAllActivity.UseVisualStyleBackColor = false;
            btnViewAllActivity.Click += btnViewAllActivity_Click;
            // 
            // picTotalPosts
            // 
            picTotalPosts.Location = new Point(20, 30);
            picTotalPosts.Name = "picTotalPosts";
            picTotalPosts.Size = new Size(50, 50);
            picTotalPosts.TabIndex = 4;
            picTotalPosts.TabStop = false;
            picTotalPosts.Paint += picTotalPosts_Paint;
            // 
            // picGrowth
            // 
            picGrowth.Location = new Point(290, 30);
            picGrowth.Name = "picGrowth";
            picGrowth.Size = new Size(50, 50);
            picGrowth.TabIndex = 5;
            picGrowth.TabStop = false;
            picGrowth.Paint += picGrowth_Paint;
            // 
            // picInteractions
            // 
            picInteractions.Location = new Point(560, 30);
            picInteractions.Name = "picInteractions";
            picInteractions.Size = new Size(50, 50);
            picInteractions.TabIndex = 6;
            picInteractions.TabStop = false;
            picInteractions.Paint += picInteractions_Paint;
            // 
            // picActiveAccounts
            // 
            picActiveAccounts.Location = new Point(830, 30);
            picActiveAccounts.Name = "picActiveAccounts";
            picActiveAccounts.Size = new Size(50, 50);
            picActiveAccounts.TabIndex = 7;
            picActiveAccounts.TabStop = false;
            picActiveAccounts.Paint += picActiveAccounts_Paint;
            // 
            // ucDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlMain);
            Font = new Font("Segoe UI", 9F);
            Name = "ucDashboard";
            Size = new Size(1120, 720);
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            pnlStats.ResumeLayout(false);
            pnlStats.PerformLayout();
            pnlComposer.ResumeLayout(false);
            pnlComposer.PerformLayout();
            pnlActivity.ResumeLayout(false);
            pnlActivity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picTotalPosts).EndInit();
            ((System.ComponentModel.ISupportInitialize)picGrowth).EndInit();
            ((System.ComponentModel.ISupportInitialize)picInteractions).EndInit();
            ((System.ComponentModel.ISupportInitialize)picActiveAccounts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Label lblTitle;
        private Panel pnlStats;
        private Label lblTotalPostsValue;
        private Label lblGrowthValue;
        private Label lblInteractionsValue;
        private Label lblActiveAccountsValue;
        private Panel pnlComposer;
        private Label lblComposerTitle;
        private TextBox txtPostContent;
        private ComboBox cmbPlatform;
        private DateTimePicker dtpSchedule;
        private Button btnPostNow;
        private Button btnSchedule;
        private Button btnAddPhoto;
        private Panel pnlActivity;
        private Label lblActivityTitle;
        private ListBox lstRecentActivity;
        private Button btnViewAllActivity;
        private PictureBox picTotalPosts;
        private PictureBox picGrowth;
        private PictureBox picInteractions;
        private PictureBox picActiveAccounts;
    }
}