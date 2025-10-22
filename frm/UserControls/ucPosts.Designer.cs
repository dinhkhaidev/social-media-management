namespace SocialManager.frm.UserControls
{
    partial class ucPosts
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
            pnlPostCreator = new Panel();
            pnlPostActions = new Panel();
            btnAddPhoto = new Button();
            dtpScheduleDate = new DateTimePicker();
            chkSchedulePost = new CheckBox();
            cmbPlatform = new ComboBox();
            btnCreatePost = new Button();
            txtPostContent = new TextBox();
            txtPostTitle = new TextBox();
            lblPostCreatorTitle = new Label();
            pnlPostsList = new Panel();
            pnlSearchFilter = new Panel();
            cmbVisibilityFilter = new ComboBox();
            lblFilter = new Label();
            txtSearch = new TextBox();
            lblSearch = new Label();
            pnlPostsActions = new Panel();
            btnRefreshPosts = new Button();
            btnDeletePost = new Button();
            btnEditPost = new Button();
            dgvPosts = new DataGridView();
            lblPostsListTitle = new Label();
            tlpMain.SuspendLayout();
            pnlPostCreator.SuspendLayout();
            pnlPostActions.SuspendLayout();
            pnlPostsList.SuspendLayout();
            pnlSearchFilter.SuspendLayout();
            pnlPostsActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPosts).BeginInit();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlpMain.Controls.Add(pnlPostCreator, 0, 0);
            tlpMain.Controls.Add(pnlPostsList, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(30, 20, 30, 30);
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1120, 720);
            tlpMain.TabIndex = 0;
            // 
            // pnlPostCreator
            // 
            pnlPostCreator.BackColor = Color.White;
            pnlPostCreator.Controls.Add(pnlPostActions);
            pnlPostCreator.Controls.Add(txtPostContent);
            pnlPostCreator.Controls.Add(txtPostTitle);
            pnlPostCreator.Controls.Add(lblPostCreatorTitle);
            pnlPostCreator.Dock = DockStyle.Fill;
            pnlPostCreator.Location = new Point(30, 20);
            pnlPostCreator.Margin = new Padding(0, 0, 15, 0);
            pnlPostCreator.Name = "pnlPostCreator";
            pnlPostCreator.Padding = new Padding(25);
            pnlPostCreator.Size = new Size(362, 670);
            pnlPostCreator.TabIndex = 0;
            pnlPostCreator.Paint += pnlCard_Paint;
            // 
            // pnlPostActions
            // 
            pnlPostActions.Controls.Add(btnAddPhoto);
            pnlPostActions.Controls.Add(dtpScheduleDate);
            pnlPostActions.Controls.Add(chkSchedulePost);
            pnlPostActions.Controls.Add(cmbPlatform);
            pnlPostActions.Controls.Add(btnCreatePost);
            pnlPostActions.Dock = DockStyle.Bottom;
            pnlPostActions.Location = new Point(25, 520);
            pnlPostActions.Name = "pnlPostActions";
            pnlPostActions.Size = new Size(312, 125);
            pnlPostActions.TabIndex = 3;
            // 
            // btnAddPhoto
            // 
            btnAddPhoto.BackColor = Color.FromArgb(149, 165, 166);
            btnAddPhoto.FlatAppearance.BorderSize = 0;
            btnAddPhoto.FlatStyle = FlatStyle.Flat;
            btnAddPhoto.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnAddPhoto.ForeColor = Color.White;
            btnAddPhoto.Location = new Point(0, 5);
            btnAddPhoto.Name = "btnAddPhoto";
            btnAddPhoto.Size = new Size(150, 35);
            btnAddPhoto.TabIndex = 0;
            btnAddPhoto.Text = "?? Thêm hình ?nh";
            btnAddPhoto.UseVisualStyleBackColor = false;
            btnAddPhoto.Click += btnAddPhoto_Click;
            // 
            // dtpScheduleDate
            // 
            dtpScheduleDate.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpScheduleDate.Enabled = false;
            dtpScheduleDate.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dtpScheduleDate.Format = DateTimePickerFormat.Custom;
            dtpScheduleDate.Location = new Point(130, 45);
            dtpScheduleDate.Name = "dtpScheduleDate";
            dtpScheduleDate.Size = new Size(180, 27);
            dtpScheduleDate.TabIndex = 3;
            // 
            // chkSchedulePost
            // 
            chkSchedulePost.AutoSize = true;
            chkSchedulePost.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            chkSchedulePost.ForeColor = Color.FromArgb(127, 140, 141);
            chkSchedulePost.Location = new Point(0, 47);
            chkSchedulePost.Name = "chkSchedulePost";
            chkSchedulePost.Size = new Size(124, 24);
            chkSchedulePost.TabIndex = 2;
            chkSchedulePost.Text = "Lên l?ch ??ng:";
            chkSchedulePost.UseVisualStyleBackColor = true;
            chkSchedulePost.CheckedChanged += chkSchedulePost_CheckedChanged;
            // 
            // cmbPlatform
            // 
            cmbPlatform.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlatform.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmbPlatform.FormattingEnabled = true;
            cmbPlatform.Items.AddRange(new object[] { "?? M?ng xã h?i", "?? Facebook", "?? Twitter", "?? Instagram", "?? LinkedIn", "?? TikTok" });
            cmbPlatform.Location = new Point(160, 8);
            cmbPlatform.Name = "cmbPlatform";
            cmbPlatform.Size = new Size(150, 28);
            cmbPlatform.TabIndex = 1;
            // 
            // btnCreatePost
            // 
            btnCreatePost.BackColor = Color.FromArgb(52, 152, 219);
            btnCreatePost.FlatAppearance.BorderSize = 0;
            btnCreatePost.FlatStyle = FlatStyle.Flat;
            btnCreatePost.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnCreatePost.ForeColor = Color.White;
            btnCreatePost.Location = new Point(0, 80);
            btnCreatePost.Name = "btnCreatePost";
            btnCreatePost.Size = new Size(312, 40);
            btnCreatePost.TabIndex = 4;
            btnCreatePost.Text = "T?O BÀI VI?T";
            btnCreatePost.UseVisualStyleBackColor = false;
            btnCreatePost.Click += btnCreatePost_Click;
            // 
            // txtPostContent
            // 
            txtPostContent.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPostContent.BorderStyle = BorderStyle.FixedSingle;
            txtPostContent.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtPostContent.Location = new Point(25, 110);
            txtPostContent.MaxLength = 500;
            txtPostContent.Multiline = true;
            txtPostContent.Name = "txtPostContent";
            txtPostContent.ScrollBars = ScrollBars.Vertical;
            txtPostContent.Size = new Size(312, 400);
            txtPostContent.TabIndex = 2;
            // 
            // txtPostTitle
            // 
            txtPostTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPostTitle.BorderStyle = BorderStyle.FixedSingle;
            txtPostTitle.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            txtPostTitle.Location = new Point(25, 65);
            txtPostTitle.Name = "txtPostTitle";
            txtPostTitle.Size = new Size(312, 32);
            txtPostTitle.TabIndex = 1;
            // 
            // lblPostCreatorTitle
            // 
            lblPostCreatorTitle.AutoSize = true;
            lblPostCreatorTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblPostCreatorTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblPostCreatorTitle.Location = new Point(25, 25);
            lblPostCreatorTitle.Name = "lblPostCreatorTitle";
            lblPostCreatorTitle.Size = new Size(183, 32);
            lblPostCreatorTitle.TabIndex = 0;
            lblPostCreatorTitle.Text = "T?o bài vi?t m?i";
            // 
            // pnlPostsList
            // 
            pnlPostsList.BackColor = Color.White;
            pnlPostsList.Controls.Add(pnlSearchFilter);
            pnlPostsList.Controls.Add(pnlPostsActions);
            pnlPostsList.Controls.Add(dgvPosts);
            pnlPostsList.Controls.Add(lblPostsListTitle);
            pnlPostsList.Dock = DockStyle.Fill;
            pnlPostsList.Location = new Point(422, 20);
            pnlPostsList.Margin = new Padding(15, 0, 0, 0);
            pnlPostsList.Name = "pnlPostsList";
            pnlPostsList.Padding = new Padding(25);
            pnlPostsList.Size = new Size(668, 670);
            pnlPostsList.TabIndex = 1;
            pnlPostsList.Paint += pnlCard_Paint;
            // 
            // pnlSearchFilter
            // 
            pnlSearchFilter.Controls.Add(cmbVisibilityFilter);
            pnlSearchFilter.Controls.Add(lblFilter);
            pnlSearchFilter.Controls.Add(txtSearch);
            pnlSearchFilter.Controls.Add(lblSearch);
            pnlSearchFilter.Dock = DockStyle.Top;
            pnlSearchFilter.Location = new Point(25, 57);
            pnlSearchFilter.Name = "pnlSearchFilter";
            pnlSearchFilter.Size = new Size(618, 60);
            pnlSearchFilter.TabIndex = 3;
            // 
            // cmbVisibilityFilter
            // 
            cmbVisibilityFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVisibilityFilter.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmbVisibilityFilter.FormattingEnabled = true;
            cmbVisibilityFilter.Items.AddRange(new object[] {
            "T?t c?",
            "Public",
            "Friends", 
            "Private"});
            cmbVisibilityFilter.Location = new Point(350, 30);
            cmbVisibilityFilter.Name = "cmbVisibilityFilter";
            cmbVisibilityFilter.Size = new Size(120, 28);
            cmbVisibilityFilter.TabIndex = 3;
            cmbVisibilityFilter.SelectedIndexChanged += CmbVisibilityFilter_SelectedIndexChanged;
            // 
            // lblFilter
            // 
            lblFilter.AutoSize = true;
            lblFilter.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblFilter.ForeColor = Color.FromArgb(127, 140, 141);
            lblFilter.Location = new Point(350, 5);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(97, 20);
            lblFilter.TabIndex = 2;
            lblFilter.Text = "L?c theo quy?n riêng t?:";
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtSearch.Location = new Point(10, 30);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(300, 27);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblSearch.ForeColor = Color.FromArgb(127, 140, 141);
            lblSearch.Location = new Point(10, 5);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(77, 20);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Tìm ki?m:";
            // 
            // pnlPostsActions
            // 
            pnlPostsActions.Controls.Add(btnRefreshPosts);
            pnlPostsActions.Controls.Add(btnDeletePost);
            pnlPostsActions.Controls.Add(btnEditPost);
            pnlPostsActions.Dock = DockStyle.Bottom;
            pnlPostsActions.Location = new Point(25, 605);
            pnlPostsActions.Name = "pnlPostsActions";
            pnlPostsActions.Size = new Size(618, 40);
            pnlPostsActions.TabIndex = 2;
            // 
            // btnRefreshPosts
            // 
            btnRefreshPosts.BackColor = Color.FromArgb(149, 165, 166);
            btnRefreshPosts.FlatAppearance.BorderSize = 0;
            btnRefreshPosts.FlatStyle = FlatStyle.Flat;
            btnRefreshPosts.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnRefreshPosts.ForeColor = Color.White;
            btnRefreshPosts.Location = new Point(230, 0);
            btnRefreshPosts.Name = "btnRefreshPosts";
            btnRefreshPosts.Size = new Size(100, 35);
            btnRefreshPosts.TabIndex = 2;
            btnRefreshPosts.Text = "?? Làm m?i";
            btnRefreshPosts.UseVisualStyleBackColor = false;
            btnRefreshPosts.Click += btnRefreshPosts_Click;
            // 
            // btnDeletePost
            // 
            btnDeletePost.BackColor = Color.FromArgb(231, 76, 60);
            btnDeletePost.FlatAppearance.BorderSize = 0;
            btnDeletePost.FlatStyle = FlatStyle.Flat;
            btnDeletePost.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnDeletePost.ForeColor = Color.White;
            btnDeletePost.Location = new Point(120, 0);
            btnDeletePost.Name = "btnDeletePost";
            btnDeletePost.Size = new Size(100, 35);
            btnDeletePost.TabIndex = 1;
            btnDeletePost.Text = "??? Xóa";
            btnDeletePost.UseVisualStyleBackColor = false;
            btnDeletePost.Click += btnDeletePost_Click;
            // 
            // btnEditPost
            // 
            btnEditPost.BackColor = Color.FromArgb(230, 126, 34);
            btnEditPost.FlatAppearance.BorderSize = 0;
            btnEditPost.FlatStyle = FlatStyle.Flat;
            btnEditPost.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnEditPost.ForeColor = Color.White;
            btnEditPost.Location = new Point(10, 0);
            btnEditPost.Name = "btnEditPost";
            btnEditPost.Size = new Size(100, 35);
            btnEditPost.TabIndex = 0;
            btnEditPost.Text = "?? Ch?nh s?a";
            btnEditPost.UseVisualStyleBackColor = false;
            btnEditPost.Click += btnEditPost_Click;
            // 
            // dgvPosts
            // 
            dgvPosts.AllowUserToAddRows = false;
            dgvPosts.AllowUserToDeleteRows = false;
            dgvPosts.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvPosts.BackgroundColor = Color.White;
            dgvPosts.BorderStyle = BorderStyle.None;
            dgvPosts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPosts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPosts.GridColor = Color.FromArgb(234, 236, 238);
            dgvPosts.Location = new Point(25, 125);
            dgvPosts.MultiSelect = false;
            dgvPosts.Name = "dgvPosts";
            dgvPosts.ReadOnly = true;
            dgvPosts.RowHeadersVisible = false;
            dgvPosts.RowHeadersWidth = 51;
            dgvPosts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPosts.Size = new Size(618, 470);
            dgvPosts.TabIndex = 1;
            dgvPosts.CellDoubleClick += dgvPosts_CellDoubleClick;
            // 
            // lblPostsListTitle
            // 
            lblPostsListTitle.AutoSize = true;
            lblPostsListTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblPostsListTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblPostsListTitle.Location = new Point(25, 25);
            lblPostsListTitle.Name = "lblPostsListTitle";
            lblPostsListTitle.Size = new Size(189, 32);
            lblPostsListTitle.TabIndex = 0;
            lblPostsListTitle.Text = "Qu?n lý bài vi?t";
            // 
            // ucPosts
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpMain);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "ucPosts";
            Size = new Size(1120, 720);
            tlpMain.ResumeLayout(false);
            pnlPostCreator.ResumeLayout(false);
            pnlPostCreator.PerformLayout();
            pnlPostActions.ResumeLayout(false);
            pnlPostActions.PerformLayout();
            pnlPostsList.ResumeLayout(false);
            pnlPostsList.PerformLayout();
            pnlSearchFilter.ResumeLayout(false);
            pnlSearchFilter.PerformLayout();
            pnlPostsActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPosts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMain;
        private Panel pnlPostCreator;
        private Panel pnlPostActions;
        private Button btnAddPhoto;
        private DateTimePicker dtpScheduleDate;
        private CheckBox chkSchedulePost;
        private ComboBox cmbPlatform;
        private Button btnCreatePost;
        private TextBox txtPostContent;
        private TextBox txtPostTitle;
        private Label lblPostCreatorTitle;
        private Panel pnlPostsList;
        private Panel pnlSearchFilter;
        private ComboBox cmbVisibilityFilter;
        private Label lblFilter;
        private TextBox txtSearch;
        private Label lblSearch;
        private Panel pnlPostsActions;
        private Button btnRefreshPosts;
        private Button btnDeletePost;
        private Button btnEditPost;
        private DataGridView dgvPosts;
        private Label lblPostsListTitle;
    }
}