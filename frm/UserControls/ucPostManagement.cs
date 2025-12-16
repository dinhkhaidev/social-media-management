using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using SocialManager.services;

namespace SocialManager
{
    public partial class ucPostManagement : UserControl
    {
        #region Fields
        private PostService postService;
        private UserService userService;
        private ReportService reportService;
        private List<Post> allPosts;
        private List<Post> selectedPosts;
        
        // UI Controls - Left Panel (List)
        private DataGridView dgvPosts = null!;
        private TextBox txtSearch = null!;
        private ComboBox cmbStatusFilter = null!;
        private ComboBox cmbCategoryFilter = null!;
        private Panel pnlActions = null!;
        private Label lblTotalPosts = null!;
        
        // UI Controls - Action Buttons
        private Button btnHide = null!;
        private Button btnUnhide = null!;
        private Button btnDelete = null!;
        private Button btnFeature = null!;
        private Button btnPin = null!;
        private Button btnBulkAction = null!;
        private Button btnExport = null!;
        private Button btnRefresh = null!;
        
        // UI Controls - Right Panel (Detail)
        private Panel pnlPostDetail = null!;
        private Label lblPostAuthor = null!;
        private Label lblPostDate = null!;
        private Label lblPostStatus = null!;
        private RichTextBox txtPostContent = null!;
        private Panel pnlMediaPreview = null!;
        private TabControl tabPostDetails = null!;
        private RichTextBox txtModerationNotes = null!;
        private ListBox lstPostHistory = null!;
        private DataGridView dgvReports = null!;
        
        // Current selection
        private Post? currentPost;
        
        // Sorting state
        private string? lastSortedColumn;
        private bool isAscending = true;
        
        // Pagination
        private const int ItemsPerPage = 15;
        private int currentPage = 1;
        private int totalPages = 1;
        private List<Post> filteredPosts = new List<Post>();
        
        // Pagination controls
        private Button btnFirstPage = null!;
        private Button btnPrevPage = null!;
        private Button btnNextPage = null!;
        private Button btnLastPage = null!;
        private Label lblPageInfo = null!;
        
        // Material Design Colors
        private readonly Color PrimaryColor = Color.FromArgb(52, 152, 219);
        private readonly Color SuccessColor = Color.FromArgb(46, 204, 113);
        private readonly Color WarningColor = Color.FromArgb(243, 156, 18);
        private readonly Color DangerColor = Color.FromArgb(231, 76, 60);
        #endregion

        #region Constructor
        public ucPostManagement()
        {
            postService = new PostService();
            userService = new UserService();
            reportService = new ReportService();
            allPosts = new List<Post>();
            selectedPosts = new List<Post>();
            
            InitializeComponent();
            CreateLayout();
            LoadPosts();
            
            // Apply theme after layout is created
            this.Load += (s, e) => SocialManager.utils.GlobalSettings.ApplyThemeToUserControl(this);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "ucPostManagement";
            this.Size = new Size(1200, 800);
            // Don't set hardcoded BackColor - let theme system handle it
            this.Padding = new Padding(10);
            this.ResumeLayout(false);
        }
        #endregion

        #region Layout Creation
        private void CreateLayout()
        {
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            
            mainLayout.Controls.Add(CreatePostListPanel(), 0, 0);
            mainLayout.Controls.Add(CreatePostDetailPanel(), 1, 0);
            
            this.Controls.Add(mainLayout);
        }

        private Panel CreatePostListPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 10, 0)
            };
            
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 5,
                ColumnCount = 1,
                BackColor = Color.Transparent
            };
            
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Header
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // Search/Filter
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F)); // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // Pagination
            
            layout.Controls.Add(CreateHeaderPanel(), 0, 0);
            layout.Controls.Add(CreateSearchFilterPanel(), 0, 1);
            layout.Controls.Add(CreateActionsPanel(), 0, 2);
            layout.Controls.Add(CreateDataGridPanel(), 0, 3);
            layout.Controls.Add(CreatePaginationPanel(), 0, 4);
            
            panel.Controls.Add(layout);
            return panel;
        }

        private Panel CreateHeaderPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(15, 5, 15, 5)
            };
            
            var title = new Label
            {
                Text = "📝 Danh Sách Bài Viết",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(15, 15)
            };
            
            lblTotalPosts = new Label
            {
                Text = "Tổng: 0 bài viết",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = true,
                Location = new Point(15, 35)
            };
            
            panel.Controls.AddRange(new Control[] { title, lblTotalPosts });
            return panel;
        }

        private Panel CreateSearchFilterPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White, // Will be overridden by theme
                Padding = new Padding(15, 10, 15, 10),
                Margin = new Padding(0, 5, 0, 5)
            };
            
            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectangle(panel.ClientRectangle, 8))
                {
                    using (var brush = new SolidBrush(Color.White))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                    using (var pen = new Pen(Color.FromArgb(230, 235, 240), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };
            
            txtSearch = new TextBox
            {
                Width = 250,
                Height = 35,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(15, 12),
                PlaceholderText = "🔍 Tìm kiếm theo tác giả, nội dung, hashtag..."
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            
            cmbStatusFilter = new ComboBox
            {
                Width = 150,
                Height = 35,
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(275, 12)
            };
            cmbStatusFilter.Items.AddRange(new object[] 
            { 
                "Tất cả trạng thái", 
                "Đã xuất bản", 
                "Đang ẩn", 
                "Đang xem xét",
                "Nổi bật",
                "Đã ghim",
                "🚨 Bị báo cáo",
                "🚫 Người dùng bị khóa",
                "🗑️ Đã xóa"
            });
            cmbStatusFilter.SelectedIndex = 0;
            cmbStatusFilter.SelectedIndexChanged += CmbStatusFilter_SelectedIndexChanged;
            
            cmbCategoryFilter = new ComboBox
            {
                Width = 150,
                Height = 35,
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(435, 12)
            };
            cmbCategoryFilter.Items.AddRange(new object[] 
            { 
                "Tất cả danh mục",
                "Tin tức",
                "Giải trí", 
                "Chính trị",
                "Thể thao",
                "Công nghệ",
                "Khác"
            });
            cmbCategoryFilter.SelectedIndex = 0;
            cmbCategoryFilter.SelectedIndexChanged += CmbCategoryFilter_SelectedIndexChanged;
            
            panel.Controls.AddRange(new Control[] { txtSearch, cmbStatusFilter, cmbCategoryFilter });
            return panel;
        }

        private Panel CreateActionsPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 5, 0, 5),
                AutoScroll = true
            };
            
            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = Color.Transparent
            };
            
            btnHide = CreateActionButton("👁️ Ẩn", Color.FromArgb(243, 156, 18));
            btnHide.Click += BtnHide_Click;
            btnHide.Enabled = false;
            
            btnUnhide = CreateActionButton("👁️‍🗨️ Hiện", Color.FromArgb(46, 204, 113));
            btnUnhide.Click += BtnUnhide_Click;
            btnUnhide.Enabled = false;
            
            btnDelete = CreateActionButton("🗑️ Xóa", Color.FromArgb(231, 76, 60));
            btnDelete.Click += BtnDelete_Click;
            btnDelete.Enabled = false;
            
            btnFeature = CreateActionButton("⭐ Nổi bật", Color.FromArgb(241, 196, 15));
            btnFeature.Click += BtnFeature_Click;
            btnFeature.Enabled = false;
            
            btnPin = CreateActionButton("📌 Ghim", Color.FromArgb(155, 89, 182));
            btnPin.Click += BtnPin_Click;
            btnPin.Enabled = false;
            
            btnBulkAction = CreateActionButton("📋 Hành động hàng loạt", Color.FromArgb(52, 73, 94));
            btnBulkAction.Click += BtnBulkAction_Click;
            btnBulkAction.Enabled = false;
            
            btnExport = CreateActionButton("📊 Xuất file", Color.FromArgb(39, 174, 96));
            btnExport.Click += BtnExport_Click;
            
            btnRefresh = CreateActionButton("🔄 Làm mới", PrimaryColor);
            btnRefresh.Click += BtnRefresh_Click;
            
            flowPanel.Controls.AddRange(new Control[] { 
                btnHide, btnUnhide, btnDelete, btnFeature, btnPin, btnBulkAction, btnExport, btnRefresh
            });
            
            panel.Controls.Add(flowPanel);
            return panel;
        }

        private Panel CreateDataGridPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White, // Will be overridden by theme
                Padding = new Padding(1)
            };
            
            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectangle(panel.ClientRectangle, 8))
                {
                    using (var brush = new SolidBrush(Color.White))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                    using (var pen = new Pen(Color.FromArgb(230, 235, 240), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };
            
            dgvPosts = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                EnableHeadersVisualStyles = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9.5F),
                RowTemplate = { Height = 50 }
            };
            
            dgvPosts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgvPosts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPosts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvPosts.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgvPosts.ColumnHeadersHeight = 45;
            
            dgvPosts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dgvPosts.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvPosts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);
            
            // Add columns
            var checkBoxColumn = new DataGridViewCheckBoxColumn { Name = "Select", HeaderText = "☑️", Width = 40 };
            checkBoxColumn.ReadOnly = false;
            dgvPosts.Columns.Add(checkBoxColumn);
            dgvPosts.Columns.Add(new DataGridViewTextBoxColumn { Name = "PostID", HeaderText = "ID", Width = 60, ReadOnly = true });
            dgvPosts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Author", HeaderText = "Tác giả", Width = 150, ReadOnly = true });
            dgvPosts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Content", HeaderText = "Nội dung", Width = 300, ReadOnly = true });
            dgvPosts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Likes", HeaderText = "👍", Width = 60, ReadOnly = true });
            dgvPosts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Comments", HeaderText = "💬", Width = 60, ReadOnly = true });
            dgvPosts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Trạng thái", Width = 120, ReadOnly = true });
            dgvPosts.Columns.Add(new DataGridViewTextBoxColumn { Name = "CreatedAt", HeaderText = "Ngày đăng", Width = 150, ReadOnly = true });
            
            dgvPosts.SelectionChanged += DgvPosts_SelectionChanged;
            dgvPosts.CellContentClick += DgvPosts_CellContentClick;
            dgvPosts.CellFormatting += DgvPosts_CellFormatting;
            dgvPosts.ColumnHeaderMouseClick += DgvPosts_ColumnHeaderMouseClick;
            
            panel.Controls.Add(dgvPosts);
            return panel;
        }

        private Panel CreatePostDetailPanel()
        {
            pnlPostDetail = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(15, 70, 15, 15)
            };
            
            pnlPostDetail.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectangle(pnlPostDetail.ClientRectangle, 8))
                {
                    using (var brush = new SolidBrush(Color.White))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                    using (var pen = new Pen(Color.FromArgb(230, 235, 240), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };
            
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                BackColor = Color.Transparent
            };
            
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F)); // Header
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));  // Stats
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));  // Tabs
            
            layout.Controls.Add(CreatePostHeaderPanel(), 0, 0);
            layout.Controls.Add(CreatePostStatsPanel(), 0, 1);
            layout.Controls.Add(CreatePostDetailTabs(), 0, 2);
            
            pnlPostDetail.Controls.Add(layout);
            
            // Show "No selection" message initially
            ShowNoSelectionMessage();
            
            return pnlPostDetail;
        }

        private Panel CreatePostHeaderPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            
            lblPostAuthor = new Label
            {
                Text = "Chưa chọn bài viết",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = false,
                Location = new Point(0, 0),
                Width = 400,
                Height = 30
            };
            
            lblPostDate = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = true,
                Location = new Point(0, 35)
            };
            
            lblPostStatus = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = SuccessColor,
                AutoSize = true,
                Padding = new Padding(10, 5, 10, 5),
                Location = new Point(0, 65)
            };
            
            panel.Controls.AddRange(new Control[] { lblPostAuthor, lblPostDate, lblPostStatus });
            return panel;
        }

        private Panel CreatePostStatsPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(247, 249, 252),
                Padding = new Padding(10)
            };
            
            panel.Paint += (s, e) =>
            {
                using (var path = GetRoundedRectangle(panel.ClientRectangle, 6))
                {
                    using (var brush = new SolidBrush(Color.FromArgb(247, 249, 252)))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };
            
            return panel;
        }

        private TabControl CreatePostDetailTabs()
        {
            tabPostDetails = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F)
            };
            
            // Tab 1: Thông tin bài viết
            var tabInfo = new TabPage("ℹ️ Thông tin");
            tabInfo.Controls.Add(LoadPostInfoTab());
            
            // Tab 2: Media Preview
            var tabMedia = new TabPage("🖼️ Media");
            tabMedia.Controls.Add(LoadMediaTab());
            
            // Tab 3: Ghi chú kiểm duyệt
            var tabNotes = new TabPage("📝 Ghi chú");
            tabNotes.Controls.Add(LoadNotesTab());
            
            // Tab 4: Lịch sử chỉnh sửa
            var tabHistory = new TabPage("📜 Lịch sử");
            tabHistory.Controls.Add(LoadHistoryTab());
            
            // Tab 5: Báo cáo
            var tabReports = new TabPage("🚨 Báo cáo");
            tabReports.Controls.Add(LoadReportsTab());
            
            tabPostDetails.TabPages.AddRange(new TabPage[] { tabInfo, tabMedia, tabNotes, tabHistory, tabReports });
            
            return tabPostDetails;
        }

        private Panel LoadPostInfoTab()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                AutoScroll = true,
                Padding = new Padding(10)
            };
            
            txtPostContent = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11F),
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                BackColor = Color.White
            };
            
            panel.Controls.Add(txtPostContent);
            return panel;
        }

        private Panel LoadMediaTab()
        {
            pnlMediaPreview = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(247, 249, 252),
                AutoScroll = true
            };
            
            var lblNoMedia = new Label
            {
                Text = "Không có media",
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(149, 165, 166),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            
            pnlMediaPreview.Controls.Add(lblNoMedia);
            return pnlMediaPreview;
        }

        private Panel LoadNotesTab()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            
            txtModerationNotes = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                Text = "Nhập ghi chú kiểm duyệt..."
            };
            
            var btnSaveNotes = new Button
            {
                Text = "💾 Lưu ghi chú",
                Dock = DockStyle.Fill,
                BackColor = PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSaveNotes.FlatAppearance.BorderSize = 0;
            btnSaveNotes.Click += BtnSaveNotes_Click;
            
            layout.Controls.Add(txtModerationNotes, 0, 0);
            layout.Controls.Add(btnSaveNotes, 0, 1);
            
            panel.Controls.Add(layout);
            return panel;
        }

        private Panel LoadHistoryTab()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            
            lstPostHistory = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(247, 249, 252)
            };
            
            panel.Controls.Add(lstPostHistory);
            return panel;
        }

        private Panel LoadReportsTab()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            
            dgvReports = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9.5F)
            };
            
            dgvReports.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReportID", HeaderText = "ID", Width = 50 });
            dgvReports.Columns.Add(new DataGridViewTextBoxColumn { Name = "Reporter", HeaderText = "Người báo cáo", Width = 150 });
            dgvReports.Columns.Add(new DataGridViewTextBoxColumn { Name = "Reason", HeaderText = "Lý do", Width = 200 });
            dgvReports.Columns.Add(new DataGridViewTextBoxColumn { Name = "Date", HeaderText = "Ngày báo cáo", Width = 150 });
            
            panel.Controls.Add(dgvReports);
            return panel;
        }

        private void ShowNoSelectionMessage()
        {
            foreach (Control ctrl in pnlPostDetail.Controls)
            {
                if (ctrl != pnlPostDetail.Controls[0]) // Keep first control (layout)
                    ctrl.Visible = false;
            }
            
            var lblNoSelection = new Label
            {
                Text = "Chọn một bài viết để xem chi tiết",
                Font = new Font("Segoe UI", 14F),
                ForeColor = Color.FromArgb(149, 165, 166),
                AutoSize = true,
                Location = new Point(50, 200),
                Name = "lblNoSelection"
            };
            
            pnlPostDetail.Controls.Add(lblNoSelection);
            lblNoSelection.BringToFront();
        }
        #endregion

        #region Data Loading
        private void LoadPosts()
        {
            try
            {
                // Get all posts including deleted for admin view
                allPosts = postService.GetAllPosts(includeDeleted: true);
                DisplayPosts(allPosts);
                lblTotalPosts.Text = $"Tổng: {allPosts.Count} bài viết";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách bài viết: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayPosts(List<Post> posts)
        {
            filteredPosts = posts;
            CalculatePagination();
            
            dgvPosts.Rows.Clear();
            
            var pagedPosts = GetCurrentPagePosts();
            foreach (var post in pagedPosts)
            {
                var user = userService.GetUserById(post.UserID);
                string authorName = user?.UserName ?? "Unknown";
                
                // Truncate content
                string shortContent = post.Content.Length > 100 
                    ? post.Content.Substring(0, 100) + "..." 
                    : post.Content;
                
                string status = GetPostStatus(post);
                
                var row = dgvPosts.Rows[dgvPosts.Rows.Add()];
                row.Cells["Select"].Value = false;
                row.Cells["PostID"].Value = post.PostID;
                row.Cells["Author"].Value = authorName;
                row.Cells["Content"].Value = shortContent;
                row.Cells["Likes"].Value = post.LikesCount;
                row.Cells["Comments"].Value = post.CommentsCount;
                row.Cells["Status"].Value = status;
                row.Cells["CreatedAt"].Value = post.CreatedAt.ToString("dd/MM/yyyy HH:mm");
                row.Cells["CreatedAt"].Tag = post.CreatedAt; // Store actual DateTime for sorting
                
                row.Tag = post;
            }
        }

        private string GetPostStatus(Post post)
        {
            // DEBUG: Force check CSV for this specific post
            try
            {
                var csvLines = System.IO.File.ReadAllLines("datas\\Post.csv");
                foreach (var line in csvLines.Skip(1))
                {
                    if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("PostID,"))
                    {
                        var parts = line.Split(',');
                        if (parts.Length > 0 && int.TryParse(parts[0], out int csvPostId) && csvPostId == post.PostID)
                        {
                            var lastCol = parts.Length > 9 ? parts[parts.Length - 1].Trim() : "False";
                            if (lastCol.Equals("True", StringComparison.OrdinalIgnoreCase))
                            {
                                return "🗑️ Đã xóa";
                            }
                            break;
                        }
                    }
                }
            }
            catch { }
            
            // FIRST: Check if post is deleted (highest priority)
            if (post.IsDeleted) 
            {
                return "🗑️ Đã xóa";
            }
            
            // SECOND: Check if user is banned
            var user = userService.GetUserById(post.UserID);
            if (user != null && user.StatusId == -1)
            {
                return "🚫 Người dùng bị khóa";
            }
            
            // THIRD: Check if post has active reports
            var reports = reportService.GetReportsForContent("Post", post.PostID);
            if (reports != null && reports.Any())
            {
                var activeReports = reports.Where(r => r.Status == "New" || r.Status == "In Review").ToList();
                if (activeReports.Any())
                {
                    return "🚨 Bị báo cáo";
                }
            }
            
            // DEFAULT: Published
            return "Đã xuất bản";
        }
        #endregion

        #region Event Handlers - Search & Filter
        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            FilterPosts();
        }

        private void CmbStatusFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            FilterPosts();
        }

        private void CmbCategoryFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            FilterPosts();
        }

        private void FilterPosts()
        {
            var filtered = allPosts.AsEnumerable();
            
            // Search filter
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string search = txtSearch.Text.ToLower();
                filtered = filtered.Where(p =>
                {
                    var user = userService.GetUserById(p.UserID);
                    string authorName = user?.UserName ?? "";
                    return p.Content.ToLower().Contains(search) ||
                           authorName.ToLower().Contains(search);
                });
            }
            
            // Status filter
            if (cmbStatusFilter.SelectedIndex > 0)
            {
                string status = cmbStatusFilter.SelectedItem?.ToString() ?? "";
                filtered = filtered.Where(p => GetPostStatus(p) == status);
            }
            
            DisplayPosts(filtered.ToList());
        }

        private void DgvPosts_SelectionChanged(object? sender, EventArgs e)
        {
            int selectedCount = dgvPosts.SelectedRows.Count;
            bool hasSelection = selectedCount > 0;
            
            btnHide.Enabled = hasSelection;
            btnUnhide.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
            btnFeature.Enabled = hasSelection;
            btnPin.Enabled = hasSelection;
            
            // Load detail if single selection
            if (selectedCount == 1)
            {
                var row = dgvPosts.SelectedRows[0];
                if (row.Tag is Post post)
                {
                    LoadPostDetail(post);
                }
            }
            else
            {
                ShowNoSelectionMessage();
            }
            
            // Update bulk action button
            UpdateBulkActionButton();
        }

        private void DgvPosts_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvPosts.Columns["Select"].Index)
            {
                dgvPosts.EndEdit();
                UpdateBulkActionButton();
            }
        }

        private void DgvPosts_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPosts.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString() ?? "";
                var cell = dgvPosts.Rows[e.RowIndex].Cells[e.ColumnIndex];
                
                switch (status)
                {
                    case "Đã xuất bản":
                        cell.Style.BackColor = Color.FromArgb(46, 204, 113);
                        cell.Style.ForeColor = Color.White;
                        break;
                    case "Đang ẩn":
                        cell.Style.BackColor = Color.FromArgb(243, 156, 18);
                        cell.Style.ForeColor = Color.White;
                        break;
                    case "🗑️ Đã xóa":
                        cell.Style.BackColor = Color.FromArgb(231, 76, 60);
                        cell.Style.ForeColor = Color.White;
                        break;
                    case "🚨 Bị báo cáo":
                        cell.Style.BackColor = Color.FromArgb(192, 57, 43); // Dark red
                        cell.Style.ForeColor = Color.White;
                        cell.Style.Font = new Font(cell.Style.Font ?? dgvPosts.DefaultCellStyle.Font ?? new Font("Segoe UI", 9.5F), FontStyle.Bold);
                        break;
                    case "🚫 Người dùng bị khóa":
                        cell.Style.BackColor = Color.FromArgb(44, 62, 80); // Dark gray
                        cell.Style.ForeColor = Color.White;
                        cell.Style.Font = new Font(cell.Style.Font ?? dgvPosts.DefaultCellStyle.Font ?? new Font("Segoe UI", 9.5F), FontStyle.Bold);
                        break;
                }
            }
        }

        private void UpdateBulkActionButton()
        {
            int checkedCount = GetCheckedPostsCount();
            btnBulkAction.Text = checkedCount > 0 
                ? $"📋 Hành động hàng loạt ({checkedCount})" 
                : "📋 Hành động hàng loạt";
            btnBulkAction.Enabled = checkedCount > 0;
        }

        private void DgvPosts_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0) return;

            var column = dgvPosts.Columns[e.ColumnIndex];
            var columnName = column.Name;

            // Toggle sort direction if same column, otherwise start with ascending
            if (lastSortedColumn == columnName)
            {
                isAscending = !isAscending;
            }
            else
            {
                isAscending = true;
                lastSortedColumn = columnName;
            }

            // Get current posts from grid
            var currentPosts = new List<Post>();
            foreach (DataGridViewRow row in dgvPosts.Rows)
            {
                if (row.Tag is Post post)
                    currentPosts.Add(post);
            }

            // Sort based on column
            switch (columnName)
            {
                case "CreatedAt":
                    currentPosts = isAscending 
                        ? currentPosts.OrderBy(p => p.CreatedAt).ToList()
                        : currentPosts.OrderByDescending(p => p.CreatedAt).ToList();
                    break;
                case "PostID":
                    currentPosts = isAscending 
                        ? currentPosts.OrderBy(p => p.PostID).ToList()
                        : currentPosts.OrderByDescending(p => p.PostID).ToList();
                    break;
                case "Author":
                    currentPosts = isAscending 
                        ? currentPosts.OrderBy(p => userService.GetUserById(p.UserID)?.UserName ?? "").ToList()
                        : currentPosts.OrderByDescending(p => userService.GetUserById(p.UserID)?.UserName ?? "").ToList();
                    break;
                case "Likes":
                    currentPosts = isAscending 
                        ? currentPosts.OrderBy(p => p.LikesCount).ToList()
                        : currentPosts.OrderByDescending(p => p.LikesCount).ToList();
                    break;
                case "Comments":
                    currentPosts = isAscending 
                        ? currentPosts.OrderBy(p => p.CommentsCount).ToList()
                        : currentPosts.OrderByDescending(p => p.CommentsCount).ToList();
                    break;
                default:
                    return;
            }

            // Clear sort glyph from other columns
            foreach (DataGridViewColumn col in dgvPosts.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            // Set sort glyph for current column
            column.HeaderCell.SortGlyphDirection = isAscending ? SortOrder.Ascending : SortOrder.Descending;

            // Redisplay sorted posts
            DisplayPosts(currentPosts);
        }
        
        private Panel CreatePaginationPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(5),
                Height = 40
            };
            
            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectangle(panel.ClientRectangle, 8))
                {
                    using (var brush = new SolidBrush(Color.White))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                    using (var pen = new Pen(Color.FromArgb(230, 235, 240), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };
            
            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.None,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent,
                AutoSize = true,
                Anchor = AnchorStyles.None
            };
            
            flowPanel.Location = new Point((panel.Width - 250) / 2, 5);
            
            btnFirstPage = new Button { Text = "<<", Size = new Size(40, 30), Enabled = false };
            btnPrevPage = new Button { Text = "<", Size = new Size(40, 30), Enabled = false };
            lblPageInfo = new Label { Text = "Trang 1 / 1", AutoSize = true, TextAlign = ContentAlignment.MiddleCenter, Padding = new Padding(10, 8, 10, 0) };
            btnNextPage = new Button { Text = ">", Size = new Size(40, 30), Enabled = false };
            btnLastPage = new Button { Text = ">>", Size = new Size(40, 30), Enabled = false };
            
            btnFirstPage.Click += (s, e) => { currentPage = 1; RefreshCurrentPage(); };
            btnPrevPage.Click += (s, e) => { if (currentPage > 1) { currentPage--; RefreshCurrentPage(); } };
            btnNextPage.Click += (s, e) => { if (currentPage < totalPages) { currentPage++; RefreshCurrentPage(); } };
            btnLastPage.Click += (s, e) => { currentPage = totalPages; RefreshCurrentPage(); };
            
            flowPanel.Controls.AddRange(new Control[] { btnFirstPage, btnPrevPage, lblPageInfo, btnNextPage, btnLastPage });
            panel.Controls.Add(flowPanel);
            return panel;
        }
        
        private void CalculatePagination()
        {
            totalPages = Math.Max(1, (int)Math.Ceiling((double)filteredPosts.Count / ItemsPerPage));
            if (currentPage > totalPages) currentPage = totalPages;
            
            btnFirstPage.Enabled = btnPrevPage.Enabled = currentPage > 1;
            btnNextPage.Enabled = btnLastPage.Enabled = currentPage < totalPages;
            lblPageInfo.Text = $"Trang {currentPage} / {totalPages} ({filteredPosts.Count} bài viết)";
        }
        
        private List<Post> GetCurrentPagePosts()
        {
            int skip = (currentPage - 1) * ItemsPerPage;
            return filteredPosts.Skip(skip).Take(ItemsPerPage).ToList();
        }
        
        private void RefreshCurrentPage()
        {
            CalculatePagination();
            dgvPosts.Rows.Clear();
            
            var pagedPosts = GetCurrentPagePosts();
            foreach (var post in pagedPosts)
            {
                var user = userService.GetUserById(post.UserID);
                string authorName = user?.UserName ?? "Unknown";
                
                string shortContent = post.Content.Length > 100 
                    ? post.Content.Substring(0, 100) + "..." 
                    : post.Content;
                
                string status = GetPostStatus(post);
                
                var row = dgvPosts.Rows[dgvPosts.Rows.Add()];
                row.Cells["Select"].Value = false;
                row.Cells["PostID"].Value = post.PostID;
                row.Cells["Author"].Value = authorName;
                row.Cells["Content"].Value = shortContent;
                row.Cells["Likes"].Value = post.LikesCount;
                row.Cells["Comments"].Value = post.CommentsCount;
                row.Cells["Status"].Value = status;
                row.Cells["CreatedAt"].Value = post.CreatedAt.ToString("dd/MM/yyyy HH:mm");
                row.Cells["CreatedAt"].Tag = post.CreatedAt;
                
                row.Tag = post;
            }
        }
        #endregion

        #region Post Detail Loading
        private void LoadPostDetail(Post post)
        {
            currentPost = post;
            
            // Hide no selection message
            var lblNoSelection = pnlPostDetail.Controls.Find("lblNoSelection", false).FirstOrDefault();
            if (lblNoSelection != null)
            {
                lblNoSelection.Visible = false;
            }
            
            // Show detail controls
            foreach (Control ctrl in pnlPostDetail.Controls)
            {
                if (ctrl.Name != "lblNoSelection")
                    ctrl.Visible = true;
            }
            
            // Load post info
            var user = userService.GetUserById(post.UserID);
            lblPostAuthor.Text = $"👤 {user?.FullName ?? "Unknown"} (@{user?.UserName ?? "unknown"})";
            lblPostDate.Text = $"📅 {post.CreatedAt:dd/MM/yyyy HH:mm}";
            lblPostStatus.Text = GetPostStatus(post);
            lblPostStatus.BackColor = GetPostStatusColor(GetPostStatus(post));
            
            // Load content
            txtPostContent.Text = post.Content;
            
            // Load media
            LoadMediaPreview(post);
            
            // Load notes
            LoadModerationNotes(post);
            
            // Load history
            LoadPostHistory(post);
            
            // Load reports
            LoadPostReports(post);
        }

        private void LoadMediaPreview(Post post)
        {
            pnlMediaPreview.Controls.Clear();
            
            if (string.IsNullOrWhiteSpace(post.MediaUrl))
            {
                var lblNoMedia = new Label
                {
                    Text = "Không có media",
                    Font = new Font("Segoe UI", 12F),
                    ForeColor = Color.FromArgb(149, 165, 166),
                    AutoSize = true,
                    Location = new Point(20, 20)
                };
                pnlMediaPreview.Controls.Add(lblNoMedia);
            }
            else
            {
                var lblMedia = new Label
                {
                    Text = $"📎 Media URL:\n{post.MediaUrl}",
                    Font = new Font("Segoe UI", 10F),
                    AutoSize = true,
                    Location = new Point(20, 20),
                    MaximumSize = new Size(pnlMediaPreview.Width - 40, 0)
                };
                pnlMediaPreview.Controls.Add(lblMedia);
                
                // Add preview button
                var btnPreview = new Button
                {
                    Text = "🔗 Mở liên kết",
                    Location = new Point(20, lblMedia.Bottom + 10),
                    Width = 150,
                    Height = 35,
                    BackColor = PrimaryColor,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };
                btnPreview.FlatAppearance.BorderSize = 0;
                btnPreview.Click += (s, e) => System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = post.MediaUrl,
                    UseShellExecute = true
                });
                pnlMediaPreview.Controls.Add(btnPreview);
            }
        }

        private void LoadModerationNotes(Post post)
        {
            // Load saved notes from storage (implement later)
            txtModerationNotes.Text = $"Ghi chú cho bài viết #{post.PostID}";
        }

        private void LoadPostHistory(Post post)
        {
            lstPostHistory.Items.Clear();
            
            lstPostHistory.Items.Add($"📅 {post.CreatedAt:dd/MM/yyyy HH:mm} - Bài viết được tạo");
            
            if (post.UpdatedAt.HasValue)
            {
                lstPostHistory.Items.Add($"✏️ {post.UpdatedAt.Value:dd/MM/yyyy HH:mm} - Bài viết được cập nhật");
            }
        }

        private void LoadPostReports(Post post)
        {
            dgvReports.Rows.Clear();
            
            // Use GetReportsForContent to get reports for this post
            var reports = reportService.GetReportsForContent("Post", post.PostID);
            
            foreach (var report in reports)
            {
                var reporter = userService.GetUserById(report.ReporterUserID);
                
                dgvReports.Rows.Add(
                    report.ReportID,
                    reporter?.UserName ?? "Unknown",
                    report.ReportType, // Using ReportType as Reason
                    report.ReportedAt.ToString("dd/MM/yyyy HH:mm")
                );
            }
        }

        private Color GetPostStatusColor(string status)
        {
            return status switch
            {
                "Đã xuất bản" => Color.FromArgb(46, 204, 113),
                "Đang ẩn" => Color.FromArgb(243, 156, 18),
                "Đang xem xét" => Color.FromArgb(52, 152, 219),
                "Nổi bật" => Color.FromArgb(241, 196, 15),
                "Đã ghim" => Color.FromArgb(155, 89, 182),
                "Đã xóa" => Color.FromArgb(231, 76, 60),
                "🚨 Bị báo cáo" => Color.FromArgb(192, 57, 43),
                "🚫 Người dùng bị khóa" => Color.FromArgb(44, 62, 80),
                _ => Color.FromArgb(127, 140, 141)
            };
        }
        #endregion

        #region Action Handlers
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            // Force reload services to get fresh data from files
            postService = new PostService();
            userService = new UserService();
            reportService = new ReportService();
            
            LoadPosts();
            
            // Debug: Check CSV content directly
            var csvLines = System.IO.File.ReadAllLines("datas\\Post.csv");
            var debugInfo = "";
            for (int i = 1; i < Math.Min(6, csvLines.Length); i++)
            {
                if (!string.IsNullOrWhiteSpace(csvLines[i]) && !csvLines[i].StartsWith("PostID,"))
                {
                    var parts = csvLines[i].Split(',');
                    if (parts.Length > 0 && int.TryParse(parts[0], out int postId))
                    {
                        var post = allPosts.FirstOrDefault(p => p.PostID == postId);
                        var lastCol = parts.Length > 9 ? parts[parts.Length - 1] : "N/A";
                        debugInfo += $"Post {postId}: CSV_LastCol={lastCol}, Parsed_IsDeleted={post?.IsDeleted}\n";
                    }
                }
            }
            
            var deletedCount = allPosts.Count(p => p.IsDeleted);
            MessageBox.Show($"Đã làm mới!\nDeleted count: {deletedCount}\n\n{debugInfo}", 
                "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnHide_Click(object? sender, EventArgs e)
        {
            var selectedPosts = GetSelectedPosts();
            if (selectedPosts.Count == 0) return;
            
            var result = MessageBox.Show(
                $"Bạn có chắc muốn ẩn {selectedPosts.Count} bài viết?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                foreach (var post in selectedPosts)
                {
                    // Hide post in database
                    postService.SoftDeletePost(post.PostID);
                    
                    // Increase user's violation count
                    var user = userService.GetUserById(post.UserID);
                    if (user != null)
                    {
                        user.ViolationCount++;
                        userService.UpdateUser(user);
                    }
                }
                
                LoadPosts();
                MessageBox.Show($"Đã ẩn {selectedPosts.Count} bài viết và tăng violation count!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnUnhide_Click(object? sender, EventArgs e)
        {
            var selectedPosts = GetSelectedPosts();
            if (selectedPosts.Count == 0) return;
            
            var result = MessageBox.Show(
                $"Bạn có chắc muốn hiện {selectedPosts.Count} bài viết?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                foreach (var post in selectedPosts)
                {
                    // Restore post in database
                    postService.RestorePost(post.PostID);
                }
                
                LoadPosts();
                MessageBox.Show($"Đã hiện {selectedPosts.Count} bài viết!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            var selectedPosts = GetSelectedPosts();
            if (selectedPosts.Count == 0) return;
            
            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa vĩnh viễn {selectedPosts.Count} bài viết?\nHành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                foreach (var post in selectedPosts)
                {
                    postService.DeletePost(post.PostID);
                }
                
                LoadPosts();
                MessageBox.Show($"Đã xóa {selectedPosts.Count} bài viết!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnFeature_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Nổi bật đang được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnPin_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Ghim đang được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBulkAction_Click(object? sender, EventArgs e)
        {
            var checkedPosts = GetCheckedPosts();
            if (checkedPosts.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một bài viết!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var menu = new ContextMenuStrip();
            menu.Items.Add("Ẩn tất cả", null, (s, ev) => BulkHide(checkedPosts));
            menu.Items.Add("Hiện tất cả", null, (s, ev) => BulkUnhide(checkedPosts));
            menu.Items.Add("Xóa tất cả", null, (s, ev) => BulkDelete(checkedPosts));
            menu.Items.Add("Xuất danh sách", null, (s, ev) => BulkExport(checkedPosts));
            
            menu.Show(btnBulkAction, new Point(0, btnBulkAction.Height));
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    FileName = $"posts_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var lines = new List<string>();
                    lines.Add("ID,Tác giả,Nội dung,Lượt thích,Bình luận,Trạng thái,Ngày đăng");
                    
                    foreach (var post in allPosts)
                    {
                        var user = userService.GetUserById(post.UserID);
                        string content = post.Content.Replace(",", ";").Replace("\n", " ");
                        lines.Add($"{post.PostID},{user?.UserName ?? "Unknown"},{content},{post.LikesCount},{post.CommentsCount},{GetPostStatus(post)},{post.CreatedAt:yyyy-MM-dd HH:mm}");
                    }
                    
                    System.IO.File.WriteAllLines(saveDialog.FileName, lines);
                    MessageBox.Show($"Đã xuất {allPosts.Count} bài viết!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất file: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSaveNotes_Click(object? sender, EventArgs e)
        {
            if (currentPost == null) return;
            
            MessageBox.Show("Ghi chú đã được lưu!", "Thành công", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Bulk Actions
        private List<Post> GetSelectedPosts()
        {
            var posts = new List<Post>();
            foreach (DataGridViewRow row in dgvPosts.SelectedRows)
            {
                if (row.Tag is Post post)
                {
                    posts.Add(post);
                }
            }
            return posts;
        }

        private List<Post> GetCheckedPosts()
        {
            var posts = new List<Post>();
            foreach (DataGridViewRow row in dgvPosts.Rows)
            {
                if (row.Cells["Select"].Value is bool isChecked && isChecked && row.Tag is Post post)
                {
                    posts.Add(post);
                }
            }
            return posts;
        }

        private int GetCheckedPostsCount()
        {
            int count = 0;
            foreach (DataGridViewRow row in dgvPosts.Rows)
            {
                if (row.Cells["Select"].Value is bool isChecked && isChecked)
                {
                    count++;
                }
            }
            return count;
        }

        private void BulkHide(List<Post> posts)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc muốn ẩn {posts.Count} bài viết?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                foreach (var post in posts)
                {
                    // Hide post in database
                    postService.SoftDeletePost(post.PostID);
                    
                    // Increase user's violation count
                    var user = userService.GetUserById(post.UserID);
                    if (user != null)
                    {
                        user.ViolationCount++;
                        userService.UpdateUser(user);
                    }
                }
                
                LoadPosts();
                MessageBox.Show($"Đã ẩn {posts.Count} bài viết và tăng violation count!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BulkUnhide(List<Post> posts)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc muốn hiện {posts.Count} bài viết?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                foreach (var post in posts)
                {
                    // Restore post in database
                    postService.RestorePost(post.PostID);
                }
                
                LoadPosts();
                MessageBox.Show($"Đã hiện {posts.Count} bài viết!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BulkDelete(List<Post> posts)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa vĩnh viễn {posts.Count} bài viết?\nHành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                foreach (var post in posts)
                {
                    postService.DeletePost(post.PostID);
                }
                
                LoadPosts();
                MessageBox.Show($"Đã xóa {posts.Count} bài viết!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BulkExport(List<Post> posts)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"selected_posts_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var lines = new List<string>();
                    lines.Add("ID,Tác giả,Nội dung,Lượt thích,Bình luận,Trạng thái,Ngày đăng");
                    
                    foreach (var post in posts)
                    {
                        var user = userService.GetUserById(post.UserID);
                        string content = post.Content.Replace(",", ";").Replace("\n", " ");
                        lines.Add($"{post.PostID},{user?.UserName ?? "Unknown"},{content},{post.LikesCount},{post.CommentsCount},{GetPostStatus(post)},{post.CreatedAt:yyyy-MM-dd HH:mm}");
                    }
                    
                    System.IO.File.WriteAllLines(saveDialog.FileName, lines);
                    MessageBox.Show($"Đã xuất {posts.Count} bài viết!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất file: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Helper Methods
        private Button CreateActionButton(string text, Color color)
        {
            var btn = new Button
            {
                Text = text,
                Width = 140,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                BackColor = color,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(1)
            };
            
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(color, 0.1f);
            btn.MouseLeave += (s, e) => btn.BackColor = color;
            
            return btn;
        }

        private GraphicsPath GetRoundedRectangle(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            
            if (bounds.Width <= 0 || bounds.Height <= 0 || radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }
            
            int diameter = Math.Min(radius * 2, Math.Min(bounds.Width, bounds.Height));
            var arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
            
            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            
            return path;
        }
        #endregion
    }
}
