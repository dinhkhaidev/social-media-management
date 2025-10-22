namespace SocialManager.frm
{
    partial class frmDashboard : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnNewPost = new System.Windows.Forms.Button();
            this.txtSearch = new SocialManager.controls.RoundedTextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnApplyFilter = new SocialManager.controls.RoundedButton();
            this.btnClearFilter = new SocialManager.controls.RoundedButton();
            this.txtPostId = new SocialManager.controls.RoundedTextBox();
            this.lblPostId = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.flowLayoutPanelPosts = new SocialManager.frm.DoubleBufferedFlowLayoutPanel();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.panelFilter.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            panelHeader = new Panel();
            btnNewPost = new Button();
            txtSearch = new SocialManager.controls.RoundedTextBox();
            lblUsername = new Label();
            picAvatar = new PictureBox();
            panelFilter = new Panel();
            gbFilter = new GroupBox();
            btnClearFilter = new SocialManager.controls.RoundedButton();
            btnApplyFilter = new SocialManager.controls.RoundedButton();
            txtPostId = new SocialManager.controls.RoundedTextBox();
            lblPostId = new Label();
            dtpTo = new DateTimePicker();
            lblToDate = new Label();
            dtpFrom = new DateTimePicker();
            lblFromDate = new Label();
            cboType = new ComboBox();
            lblType = new Label();
            flowLayoutPanelPosts = new SocialManager.frm.DoubleBufferedFlowLayoutPanel();
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
            this.btnNewPost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewPost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewPost.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewPost.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewPost.ForeColor = System.Drawing.Color.White;
            this.btnNewPost.Location = new System.Drawing.Point(1180, 15);
            this.btnNewPost.Name = "btnNewPost";
            this.btnNewPost.Size = new System.Drawing.Size(50, 50);
            this.btnNewPost.TabIndex = 3;
            this.btnNewPost.Text = "+";
            this.btnNewPost.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.SystemColors.Window;
            this.txtSearch.BorderColor = System.Drawing.Color.LightGray;
            this.txtSearch.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtSearch.BorderRadius = 18;
            this.txtSearch.BorderSize = 1;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSearch.Location = new System.Drawing.Point(400, 21);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtSearch.PasswordChar = false;
            this.txtSearch.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtSearch.PlaceholderText = "Tìm kiếm bài viết...";
            this.txtSearch.Size = new System.Drawing.Size(750, 38);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.UnderlinedStyle = false;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUsername.Location = new System.Drawing.Point(90, 28);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(97, 23);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "User Name";
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
            lblUsername.Click += lblUsername_Click;            // 
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
            //             this.gbFilter.Controls.Add(this.btnApplyFilter);
            this.gbFilter.Controls.Add(this.btnClearFilter);
            this.gbFilter.Controls.Add(this.txtPostId);
            this.gbFilter.Controls.Add(this.lblPostId);
            this.gbFilter.Controls.Add(this.dtpTo);
            this.gbFilter.Controls.Add(this.lblToDate);
            this.gbFilter.Controls.Add(this.dtpFrom);
            this.gbFilter.Controls.Add(this.lblFromDate);
            this.gbFilter.Controls.Add(this.cboType);
            this.gbFilter.Controls.Add(this.lblType);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gbFilter.Location = new System.Drawing.Point(10, 10);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(280, 673);
            this.gbFilter.TabIndex = 0;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Lọc bài viết";
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnApplyFilter.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnApplyFilter.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnApplyFilter.BorderRadius = 40;
            this.btnApplyFilter.BorderSize = 0;
            this.btnApplyFilter.FlatAppearance.BorderSize = 0;
            this.btnApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnApplyFilter.ForeColor = System.Drawing.Color.White;
            this.btnApplyFilter.Location = new System.Drawing.Point(140, 400);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(120, 40);
            this.btnApplyFilter.TabIndex = 8;
            this.btnApplyFilter.Text = "Lọc";
            this.btnApplyFilter.TextColor = System.Drawing.Color.White;
            this.btnApplyFilter.UseVisualStyleBackColor = false;
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
            // btnClearFilter
            // 
            this.btnClearFilter.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClearFilter.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.btnClearFilter.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnClearFilter.BorderRadius = 40;
            this.btnClearFilter.BorderSize = 0;
            this.btnClearFilter.FlatAppearance.BorderSize = 0;
            this.btnClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClearFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClearFilter.Location = new System.Drawing.Point(20, 400);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(110, 40);
            this.btnClearFilter.TabIndex = 9;
            this.btnClearFilter.Text = "Xóa lọc";
            this.btnClearFilter.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClearFilter.UseVisualStyleBackColor = false;
            // 
            // txtPostId
            // 
            this.txtPostId.BackColor = System.Drawing.SystemColors.Window;
            this.txtPostId.BorderColor = System.Drawing.Color.LightGray;
            this.txtPostId.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtPostId.BorderRadius = 15;
            this.txtPostId.BorderSize = 1;
            this.txtPostId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPostId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPostId.Location = new System.Drawing.Point(20, 300);
            this.txtPostId.Name = "txtPostId";
            this.txtPostId.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtPostId.PasswordChar = false;
            this.txtPostId.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtPostId.PlaceholderText = "Nhập ID bài viết";
            this.txtPostId.Size = new System.Drawing.Size(240, 38);
            this.txtPostId.TabIndex = 7;
            this.txtPostId.UnderlinedStyle = false;
            // 
            // lblPostId
            // 
            this.lblPostId.AutoSize = true;
            this.lblPostId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPostId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPostId.Location = new System.Drawing.Point(16, 277);
            this.lblPostId.Name = "lblPostId";
            this.lblPostId.Size = new System.Drawing.Size(81, 20);
            this.lblPostId.TabIndex = 6;
            this.lblPostId.Text = "ID Bài Viết";
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
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblToDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblToDate.Location = new System.Drawing.Point(16, 197);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(76, 20);
            this.lblToDate.TabIndex = 4;
            this.lblToDate.Text = "Đến Ngày";            lblToDate.AutoSize = true;
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
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFromDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFromDate.Location = new System.Drawing.Point(16, 117);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(64, 20);
            this.lblFromDate.TabIndex = 2;
            this.lblFromDate.Text = "Từ Ngày";
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
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblType.Location = new System.Drawing.Point(16, 37);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(95, 20);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Loại Bài Viết";
            // 
            // flowLayoutPanelPosts
            // 
            this.flowLayoutPanelPosts.AutoScroll = true;
            this.flowLayoutPanelPosts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.flowLayoutPanelPosts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelPosts.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelPosts.Location = new System.Drawing.Point(300, 80);
            this.flowLayoutPanelPosts.Name = "flowLayoutPanelPosts";
            this.flowLayoutPanelPosts.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanelPosts.Size = new System.Drawing.Size(962, 693);
            this.flowLayoutPanelPosts.TabIndex = 2;
            this.flowLayoutPanelPosts.WrapContents = false;
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1262, 773);
            this.Controls.Add(this.flowLayoutPanelPosts);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelHeader);
            this.Name = "frmDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Social Manager - Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.panelFilter.ResumeLayout(false);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.ResumeLayout(false);
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
        private DoubleBufferedFlowLayoutPanel flowLayoutPanelPosts;
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
        private controls.RoundedButton btnApplyFilter;
        private controls.RoundedButton btnClearFilter;
    }
}