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
            label2 = new Label();
            pnlActivity = new Panel();
            btnViewAllActivity = new Button();
            lstRecentActivity = new ListBox();
            lblActivityTitle = new Label();
            pnlComposer = new Panel();
            lblComposerTitle = new Label();
            pnlStats = new Panel();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            lblInteractionsValue = new Label();
            lblGrowthValue = new Label();
            lblTotalPostsValue = new Label();
            lblTitle = new Label();
            pnlMain.SuspendLayout();
            pnlActivity.SuspendLayout();
            pnlComposer.SuspendLayout();
            pnlStats.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(247, 249, 252);
            pnlMain.Controls.Add(label2);
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(935, 32);
            label2.Name = "label2";
            label2.Size = new Size(168, 25);
            label2.TabIndex = 9;
            label2.Text = "Welcome, Admin!";
            label2.Click += label2_Click;
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
            // lstRecentActivity
            // 
            lstRecentActivity.BorderStyle = BorderStyle.None;
            lstRecentActivity.Location = new Point(20, 70);
            lstRecentActivity.Name = "lstRecentActivity";
            lstRecentActivity.Size = new Size(360, 280);
            lstRecentActivity.TabIndex = 1;
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
            // pnlComposer
            // 
            pnlComposer.BackColor = Color.White;
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
            lblComposerTitle.Size = new Size(177, 32);
            lblComposerTitle.TabIndex = 0;
            lblComposerTitle.Text = "Analysis Chart";
            lblComposerTitle.Click += lblComposerTitle_Click;
            // 
            // pnlStats
            // 
            pnlStats.BackColor = Color.White;
            pnlStats.Controls.Add(label4);
            pnlStats.Controls.Add(label3);
            pnlStats.Controls.Add(label1);
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
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(879, 82);
            label4.Name = "label4";
            label4.Size = new Size(114, 28);
            label4.TabIndex = 10;
            label4.Text = "Total Posts";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(515, 80);
            label3.Name = "label3";
            label3.Size = new Size(114, 28);
            label3.TabIndex = 9;
            label3.Text = "Total Posts";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(105, 80);
            label1.Name = "label1";
            label1.Size = new Size(116, 28);
            label1.TabIndex = 8;
            label1.Text = "Total Users";
            // 
            // lblInteractionsValue
            // 
            lblInteractionsValue.AutoSize = true;
            lblInteractionsValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblInteractionsValue.ForeColor = Color.FromArgb(231, 76, 60);
            lblInteractionsValue.Location = new Point(896, 41);
            lblInteractionsValue.Name = "lblInteractionsValue";
            lblInteractionsValue.Size = new Size(71, 41);
            lblInteractionsValue.TabIndex = 2;
            lblInteractionsValue.Text = "85K";
            // 
            // lblGrowthValue
            // 
            lblGrowthValue.AutoSize = true;
            lblGrowthValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblGrowthValue.ForeColor = Color.FromArgb(39, 174, 96);
            lblGrowthValue.Location = new Point(515, 40);
            lblGrowthValue.Name = "lblGrowthValue";
            lblGrowthValue.Size = new Size(99, 41);
            lblGrowthValue.TabIndex = 1;
            lblGrowthValue.Text = "+12%";
            // 
            // lblTotalPostsValue
            // 
            lblTotalPostsValue.AutoSize = true;
            lblTotalPostsValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotalPostsValue.ForeColor = Color.FromArgb(52, 152, 219);
            lblTotalPostsValue.Location = new Point(123, 39);
            lblTotalPostsValue.Name = "lblTotalPostsValue";
            lblTotalPostsValue.Size = new Size(79, 41);
            lblTotalPostsValue.TabIndex = 0;
            lblTotalPostsValue.Text = "1.2K";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(30, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(229, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Dashboard";
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
            pnlActivity.ResumeLayout(false);
            pnlActivity.PerformLayout();
            pnlComposer.ResumeLayout(false);
            pnlComposer.PerformLayout();
            pnlStats.ResumeLayout(false);
            pnlStats.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Label lblTitle;
        private Panel pnlStats;
        private Label lblTotalPostsValue;
        private Label lblGrowthValue;
        private Label lblInteractionsValue;
        private Panel pnlComposer;
        private Panel pnlActivity;
        private Label lblActivityTitle;
        private ListBox lstRecentActivity;
        private Button btnViewAllActivity;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label3;
        private Label lblComposerTitle;
    }
}