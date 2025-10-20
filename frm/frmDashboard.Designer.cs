namespace SocialManager.frm
{
    partial class frmDashboard
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
            panelHeader = new Panel();
            btnNewPost = new Button();
            txtSearch = new SocialManager.controls.RoundedTextBox();
            lblUsername = new Label();
            picAvatar = new PictureBox();
            panelFilter = new Panel();
            gbFilter = new GroupBox();
            btnClearFilter = new Button();
            btnApplyFilter = new Button();
            txtPostId = new SocialManager.controls.RoundedTextBox();
            lblPostId = new Label();
            dtpTo = new DateTimePicker();
            lblToDate = new Label();
            dtpFrom = new DateTimePicker();
            lblFromDate = new Label();
            cboType = new ComboBox();
            lblType = new Label();
            flowLayoutPanelPosts = new FlowLayoutPanel();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            panelFilter.SuspendLayout();
            gbFilter.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(btnNewPost);
            panelHeader.Controls.Add(txtSearch);
            panelHeader.Controls.Add(lblUsername);
            panelHeader.Controls.Add(picAvatar);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1262, 80);
            panelHeader.TabIndex = 0;
            // 
            // btnNewPost
            // 
            btnNewPost.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNewPost.BackColor = Color.FromArgb(0, 123, 255);
            btnNewPost.Cursor = Cursors.Hand;
            btnNewPost.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNewPost.ForeColor = Color.White;
            btnNewPost.Location = new Point(1180, 15);
            btnNewPost.Name = "btnNewPost";
            btnNewPost.Size = new Size(50, 50);
            btnNewPost.TabIndex = 3;
            btnNewPost.Text = "+";
            btnNewPost.UseVisualStyleBackColor = false;
            btnNewPost.Click += btnNewPost_Click;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.BackColor = SystemColors.Window;
            txtSearch.BorderColor = Color.LightGray;
            txtSearch.BorderFocusColor = Color.FromArgb(0, 123, 255);
            txtSearch.BorderRadius = 18;
            txtSearch.BorderSize = 1;
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.ForeColor = Color.FromArgb(64, 64, 64);
            txtSearch.Location = new Point(400, 21);
            txtSearch.Multiline = false;
            txtSearch.Name = "txtSearch";
            txtSearch.Padding = new Padding(10, 7, 10, 7);
            txtSearch.PasswordChar = false;
            txtSearch.PlaceholderColor = Color.DarkGray;
            txtSearch.PlaceholderText = "Tìm kiếm bài viết...";
            txtSearch.Size = new Size(750, 38);
            txtSearch.TabIndex = 2;
            txtSearch.UnderlinedStyle = false;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsername.Location = new Point(90, 28);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(97, 23);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "User Name";
            lblUsername.Click += lblUsername_Click;
            // 
            // picAvatar
            // 
            picAvatar.BackColor = Color.Gainsboro;
            picAvatar.Location = new Point(20, 15);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(50, 50);
            picAvatar.TabIndex = 0;
            picAvatar.TabStop = false;
            // 
            // panelFilter
            // 
            panelFilter.BackColor = Color.White;
            panelFilter.Controls.Add(gbFilter);
            panelFilter.Dock = DockStyle.Left;
            panelFilter.Location = new Point(0, 80);
            panelFilter.Name = "panelFilter";
            panelFilter.Padding = new Padding(10);
            panelFilter.Size = new Size(300, 693);
            panelFilter.TabIndex = 1;
            // 
            // gbFilter
            // 
            gbFilter.Controls.Add(btnClearFilter);
            gbFilter.Controls.Add(btnApplyFilter);
            gbFilter.Controls.Add(txtPostId);
            gbFilter.Controls.Add(lblPostId);
            gbFilter.Controls.Add(dtpTo);
            gbFilter.Controls.Add(lblToDate);
            gbFilter.Controls.Add(dtpFrom);
            gbFilter.Controls.Add(lblFromDate);
            gbFilter.Controls.Add(cboType);
            gbFilter.Controls.Add(lblType);
            gbFilter.Dock = DockStyle.Fill;
            gbFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbFilter.Location = new Point(10, 10);
            gbFilter.Name = "gbFilter";
            gbFilter.Size = new Size(280, 673);
            gbFilter.TabIndex = 0;
            gbFilter.TabStop = false;
            gbFilter.Text = "Lọc bài viết";
            // 
            // btnClearFilter
            // 
            btnClearFilter.BackColor = Color.Gainsboro;
            btnClearFilter.FlatAppearance.BorderSize = 0;
            btnClearFilter.FlatStyle = FlatStyle.Flat;
            btnClearFilter.Font = new Font("Segoe UI", 10F);
            btnClearFilter.ForeColor = Color.FromArgb(64, 64, 64);
            btnClearFilter.Location = new Point(20, 400);
            btnClearFilter.Name = "btnClearFilter";
            btnClearFilter.Size = new Size(110, 40);
            btnClearFilter.TabIndex = 9;
            btnClearFilter.Text = "Xóa lọc";
            btnClearFilter.UseVisualStyleBackColor = false;
            // 
            // btnApplyFilter
            // 
            btnApplyFilter.BackColor = Color.FromArgb(0, 123, 255);
            btnApplyFilter.FlatAppearance.BorderSize = 0;
            btnApplyFilter.FlatStyle = FlatStyle.Flat;
            btnApplyFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnApplyFilter.ForeColor = Color.White;
            btnApplyFilter.Location = new Point(140, 400);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new Size(120, 40);
            btnApplyFilter.TabIndex = 8;
            btnApplyFilter.Text = "Lọc";
            btnApplyFilter.UseVisualStyleBackColor = false;
            // 
            // txtPostId
            // 
            txtPostId.BackColor = SystemColors.Window;
            txtPostId.BorderColor = Color.LightGray;
            txtPostId.BorderFocusColor = Color.FromArgb(0, 123, 255);
            txtPostId.BorderRadius = 15;
            txtPostId.BorderSize = 1;
            txtPostId.Font = new Font("Segoe UI", 10F);
            txtPostId.ForeColor = Color.FromArgb(64, 64, 64);
            txtPostId.Location = new Point(20, 300);
            txtPostId.Multiline = false;
            txtPostId.Name = "txtPostId";
            txtPostId.Padding = new Padding(10, 7, 10, 7);
            txtPostId.PasswordChar = false;
            txtPostId.PlaceholderColor = Color.DarkGray;
            txtPostId.PlaceholderText = "Nhập ID bài viết";
            txtPostId.Size = new Size(240, 38);
            txtPostId.TabIndex = 7;
            txtPostId.UnderlinedStyle = false;
            // 
            // lblPostId
            // 
            lblPostId.AutoSize = true;
            lblPostId.Font = new Font("Segoe UI", 9F);
            lblPostId.Location = new Point(16, 277);
            lblPostId.Name = "lblPostId";
            lblPostId.Size = new Size(79, 20);
            lblPostId.TabIndex = 6;
            lblPostId.Text = "ID Bài Viết";
            // 
            // dtpTo
            // 
            dtpTo.Font = new Font("Segoe UI", 9F);
            dtpTo.Location = new Point(20, 220);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(240, 27);
            dtpTo.TabIndex = 5;
            // 
            // lblToDate
            // 
            lblToDate.AutoSize = true;
            lblToDate.Font = new Font("Segoe UI", 9F);
            lblToDate.Location = new Point(16, 197);
            lblToDate.Name = "lblToDate";
            lblToDate.Size = new Size(75, 20);
            lblToDate.TabIndex = 4;
            lblToDate.Text = "Đến Ngày";
            // 
            // dtpFrom
            // 
            dtpFrom.Font = new Font("Segoe UI", 9F);
            dtpFrom.Location = new Point(20, 140);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(240, 27);
            dtpFrom.TabIndex = 3;
            // 
            // lblFromDate
            // 
            lblFromDate.AutoSize = true;
            lblFromDate.Font = new Font("Segoe UI", 9F);
            lblFromDate.Location = new Point(16, 117);
            lblFromDate.Name = "lblFromDate";
            lblFromDate.Size = new Size(65, 20);
            lblFromDate.TabIndex = 2;
            lblFromDate.Text = "Từ Ngày";
            // 
            // cboType
            // 
            cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboType.Font = new Font("Segoe UI", 9F);
            cboType.FormattingEnabled = true;
            cboType.Items.AddRange(new object[] { "Tất cả", "Status", "Image", "Video" });
            cboType.Location = new Point(20, 60);
            cboType.Name = "cboType";
            cboType.Size = new Size(240, 28);
            cboType.TabIndex = 1;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Font = new Font("Segoe UI", 9F);
            lblType.Location = new Point(16, 37);
            lblType.Name = "lblType";
            lblType.Size = new Size(92, 20);
            lblType.TabIndex = 0;
            lblType.Text = "Loại Bài Viết";
            // 
            // flowLayoutPanelPosts
            // 
            flowLayoutPanelPosts.AutoScroll = true;
            flowLayoutPanelPosts.Dock = DockStyle.Fill;
            flowLayoutPanelPosts.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelPosts.Location = new Point(300, 80);
            flowLayoutPanelPosts.Name = "flowLayoutPanelPosts";
            flowLayoutPanelPosts.Padding = new Padding(10);
            flowLayoutPanelPosts.Size = new Size(962, 693);
            flowLayoutPanelPosts.TabIndex = 2;
            flowLayoutPanelPosts.WrapContents = false;
            // 
            // frmDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1262, 773);
            Controls.Add(flowLayoutPanelPosts);
            Controls.Add(panelFilter);
            Controls.Add(panelHeader);
            Name = "frmDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Social Manager - Dashboard";
            WindowState = FormWindowState.Maximized;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            panelFilter.ResumeLayout(false);
            gbFilter.ResumeLayout(false);
            gbFilter.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPosts;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Label lblUsername;
        private controls.RoundedTextBox txtSearch;
        private System.Windows.Forms.Button btnNewPost;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblToDate;
        private controls.RoundedTextBox txtPostId;
        private System.Windows.Forms.Label lblPostId;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.Button btnClearFilter;
    }
}