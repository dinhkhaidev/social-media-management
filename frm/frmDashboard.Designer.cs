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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnNewPost = new System.Windows.Forms.Button();
            this.txtSearch = new SocialManager.controls.RoundedTextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.txtPostId = new SocialManager.controls.RoundedTextBox();
            this.lblPostId = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.flowLayoutPanelPosts = new System.Windows.Forms.FlowLayoutPanel();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.panelFilter.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.btnNewPost);
            this.panelHeader.Controls.Add(this.txtSearch);
            this.panelHeader.Controls.Add(this.lblUsername);
            this.panelHeader.Controls.Add(this.picAvatar);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1262, 80);
            this.panelHeader.TabIndex = 0;
            // 
            // btnNewPost
            // 
            this.btnNewPost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewPost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
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
            this.txtSearch.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
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
            this.lblUsername.Location = new System.Drawing.Point(90, 28);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(97, 23);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "User Name";
            // 
            // picAvatar
            // 
            this.picAvatar.BackColor = System.Drawing.Color.Gainsboro;
            this.picAvatar.Location = new System.Drawing.Point(20, 15);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(50, 50);
            this.picAvatar.TabIndex = 0;
            this.picAvatar.TabStop = false;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.White;
            this.panelFilter.Controls.Add(this.gbFilter);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelFilter.Location = new System.Drawing.Point(0, 80);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Padding = new System.Windows.Forms.Padding(10);
            this.panelFilter.Size = new System.Drawing.Size(300, 693);
            this.panelFilter.TabIndex = 1;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnClearFilter);
            this.gbFilter.Controls.Add(this.btnApplyFilter);
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
            this.gbFilter.Location = new System.Drawing.Point(10, 10);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(280, 673);
            this.gbFilter.TabIndex = 0;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Lọc bài viết";
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClearFilter.FlatAppearance.BorderSize = 0;
            this.btnClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClearFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClearFilter.Location = new System.Drawing.Point(20, 400);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(110, 40);
            this.btnClearFilter.TabIndex = 9;
            this.btnClearFilter.Text = "Xóa lọc";
            this.btnClearFilter.UseVisualStyleBackColor = false;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnApplyFilter.FlatAppearance.BorderSize = 0;
            this.btnApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnApplyFilter.ForeColor = System.Drawing.Color.White;
            this.btnApplyFilter.Location = new System.Drawing.Point(140, 400);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(120, 40);
            this.btnApplyFilter.TabIndex = 8;
            this.btnApplyFilter.Text = "Lọc";
            this.btnApplyFilter.UseVisualStyleBackColor = false;
            // 
            // txtPostId
            // 
            this.txtPostId.BackColor = System.Drawing.SystemColors.Window;
            this.txtPostId.BorderColor = System.Drawing.Color.LightGray;
            this.txtPostId.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
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
            this.lblPostId.Location = new System.Drawing.Point(16, 277);
            this.lblPostId.Name = "lblPostId";
            this.lblPostId.Size = new System.Drawing.Size(81, 20);
            this.lblPostId.TabIndex = 6;
            this.lblPostId.Text = "ID Bài Viết";
            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTo.Location = new System.Drawing.Point(20, 220);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(240, 27);
            this.dtpTo.TabIndex = 5;
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblToDate.Location = new System.Drawing.Point(16, 197);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(76, 20);
            this.lblToDate.TabIndex = 4;
            this.lblToDate.Text = "Đến Ngày";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFrom.Location = new System.Drawing.Point(20, 140);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(240, 27);
            this.dtpFrom.TabIndex = 3;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFromDate.Location = new System.Drawing.Point(16, 117);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(64, 20);
            this.lblFromDate.TabIndex = 2;
            this.lblFromDate.Text = "Từ Ngày";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "Tất cả",
            "Status",
            "Image",
            "Video"});
            this.cboType.Location = new System.Drawing.Point(20, 60);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(240, 28);
            this.cboType.TabIndex = 1;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblType.Location = new System.Drawing.Point(16, 37);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(95, 20);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Loại Bài Viết";
            // 
            // flowLayoutPanelPosts
            // 
            this.flowLayoutPanelPosts.AutoScroll = true;
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
            this.BackColor = System.Drawing.Color.WhiteSmoke;
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