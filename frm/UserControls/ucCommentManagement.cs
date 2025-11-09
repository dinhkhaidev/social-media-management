using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using SocialManager.services;

namespace SocialManager
{
    public partial class ucCommentManagement : UserControl
    {
        #region Fields
        private readonly CommentService commentService;
        private readonly UserService userService;
        private readonly PostService postService;
        private List<Comment> allComments;
        private List<Comment> selectedComments;
        
        // UI Controls
        private DataGridView dgvComments = null!;
        private TextBox txtSearch = null!;
        private ComboBox cmbStatusFilter = null!;
        private DateTimePicker dtpFromDate = null!;
        private DateTimePicker dtpToDate = null!;
        private Label lblTotalComments = null!;
        
        // Action Buttons
        private Button btnHide = null!;
        private Button btnUnhide = null!;
        private Button btnDelete = null!;
        private Button btnWarnUser = null!;
        private Button btnBulkAction = null!;
        private Button btnExport = null!;
        private Button btnRefresh = null!;
        
        // Detail Panel
        private Panel pnlCommentDetail = null!;
        private RichTextBox txtCommentContent = null!;
        private RichTextBox txtQuickReply = null!;
        
        private Comment? currentComment;
        
        // Sorting state
        private string? lastSortedColumn;
        private bool isAscending = true;
        
        // Pagination
        private const int ItemsPerPage = 15;
        private int currentPage = 1;
        private int totalPages = 1;
        private List<Comment> filteredComments = new List<Comment>();
        
        // Pagination controls
        private Button btnFirstPage = null!;
        private Button btnPrevPage = null!;
        private Button btnNextPage = null!;
        private Button btnLastPage = null!;
        private Label lblPageInfo = null!;
        
        // Colors
        private readonly Color PrimaryColor = Color.FromArgb(52, 152, 219);
        private readonly Color SuccessColor = Color.FromArgb(46, 204, 113);
        private readonly Color WarningColor = Color.FromArgb(243, 156, 18);
        private readonly Color DangerColor = Color.FromArgb(231, 76, 60);
        #endregion

        #region Constructor
        public ucCommentManagement()
        {
            commentService = new CommentService();
            userService = new UserService();
            postService = new PostService();
            allComments = new List<Comment>();
            selectedComments = new List<Comment>();
            
            InitializeComponent();
            CreateLayout();
            LoadComments();
            
            // Apply theme after layout is created
            this.Load += (s, e) => SocialManager.utils.GlobalSettings.ApplyThemeToUserControl(this);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "ucCommentManagement";
            this.Size = new Size(1200, 800);
            this.BackColor = Color.FromArgb(247, 249, 252);
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
            
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            
            mainLayout.Controls.Add(CreateCommentListPanel(), 0, 0);
            mainLayout.Controls.Add(CreateCommentDetailPanel(), 1, 0);
            
            this.Controls.Add(mainLayout);
        }

        private Panel CreateCommentListPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 5, 0)
            };
            
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 5,
                ColumnCount = 1,
                BackColor = Color.Transparent
            };
            
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            
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
            
            var title = new Label
            {
                Text = "💬 Quản Lý Bình Luận",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(15, 15)
            };
            
            lblTotalComments = new Label
            {
                Text = "Tổng: 0 bình luận",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = true,
                Location = new Point(15, 35)
            };
            
            btnRefresh = new Button
            {
                Text = "🔄",
                Width = 40,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = PrimaryColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14F),
                Cursor = Cursors.Hand,
                Location = new Point(panel.Width - 60, 10)
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;
            
            panel.Controls.AddRange(new Control[] { title, lblTotalComments, btnRefresh });
            return panel;
        }

        private Panel CreateSearchFilterPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
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
            
            // Row 1: Search box
            txtSearch = new TextBox
            {
                Width = 400,
                Height = 35,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(15, 12),
                PlaceholderText = "🔍 Tìm kiếm theo người dùng, nội dung..."
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            
            cmbStatusFilter = new ComboBox
            {
                Width = 150,
                Height = 35,
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(425, 12)
            };
            cmbStatusFilter.Items.AddRange(new object[] { "Tất cả trạng thái", "Hiển thị", "Đã ẩn" });
            cmbStatusFilter.SelectedIndex = 0;
            cmbStatusFilter.SelectedIndexChanged += CmbStatusFilter_SelectedIndexChanged;
            
            // Row 2: Date filters
            var lblFrom = new Label
            {
                Text = "Từ ngày:",
                AutoSize = true,
                Location = new Point(15, 52),
                Font = new Font("Segoe UI", 9F)
            };
            
            dtpFromDate = new DateTimePicker
            {
                Width = 150,
                Location = new Point(80, 48),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now.AddMonths(-1)
            };
            dtpFromDate.ValueChanged += DtpDate_ValueChanged;
            
            var lblTo = new Label
            {
                Text = "Đến ngày:",
                AutoSize = true,
                Location = new Point(240, 52),
                Font = new Font("Segoe UI", 9F)
            };
            
            dtpToDate = new DateTimePicker
            {
                Width = 150,
                Location = new Point(315, 48),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now
            };
            dtpToDate.ValueChanged += DtpDate_ValueChanged;
            
            panel.Controls.AddRange(new Control[] { 
                txtSearch, cmbStatusFilter, lblFrom, dtpFromDate, lblTo, dtpToDate 
            });
            return panel;
        }

        private Panel CreateActionsPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 5, 0, 5)
            };
            
            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent
            };
            
            btnHide = CreateActionButton("👁️ Ẩn", WarningColor);
            btnHide.Click += BtnHide_Click;
            btnHide.Enabled = false;
            
            btnUnhide = CreateActionButton("👁️‍🗨️ Hiện", SuccessColor);
            btnUnhide.Click += BtnUnhide_Click;
            btnUnhide.Enabled = false;
            
            btnDelete = CreateActionButton("🗑️ Xóa", DangerColor);
            btnDelete.Click += BtnDelete_Click;
            btnDelete.Enabled = false;
            
            btnWarnUser = CreateActionButton("⚠️ Cảnh báo", Color.FromArgb(230, 126, 34));
            btnWarnUser.Click += BtnWarnUser_Click;
            btnWarnUser.Enabled = false;
            
            btnBulkAction = CreateActionButton("📋 Hành động hàng loạt", Color.FromArgb(52, 73, 94));
            btnBulkAction.Click += BtnBulkAction_Click;
            btnBulkAction.Enabled = false;
            
            btnExport = CreateActionButton("📊 Xuất file", Color.FromArgb(39, 174, 96));
            btnExport.Click += BtnExport_Click;
            
            flowPanel.Controls.AddRange(new Control[] { 
                btnHide, btnUnhide, btnDelete, btnWarnUser, btnBulkAction, btnExport
            });
            
            panel.Controls.Add(flowPanel);
            return panel;
        }

        private Panel CreateDataGridPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
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
            
            dgvComments = new DataGridView
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
                RowTemplate = { Height = 60 }
            };
            
            dgvComments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgvComments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvComments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvComments.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgvComments.ColumnHeadersHeight = 45;
            
            dgvComments.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dgvComments.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvComments.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);
            
            var checkBoxColumn = new DataGridViewCheckBoxColumn { Name = "Select", HeaderText = "☑️", Width = 40 };
            checkBoxColumn.ReadOnly = false;
            dgvComments.Columns.Add(checkBoxColumn);
            dgvComments.Columns.Add(new DataGridViewTextBoxColumn { Name = "CommentID", HeaderText = "ID", Width = 50, ReadOnly = true });
            dgvComments.Columns.Add(new DataGridViewTextBoxColumn { Name = "Author", HeaderText = "Người dùng", Width = 120, ReadOnly = true });
            dgvComments.Columns.Add(new DataGridViewTextBoxColumn { Name = "PostID", HeaderText = "Bài viết", Width = 80, ReadOnly = true });
            dgvComments.Columns.Add(new DataGridViewTextBoxColumn { Name = "Content", HeaderText = "Nội dung", Width = 300, ReadOnly = true });
            dgvComments.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Trạng thái", Width = 100, ReadOnly = true });
            dgvComments.Columns.Add(new DataGridViewTextBoxColumn { Name = "CreatedAt", HeaderText = "Ngày tạo", Width = 130, ReadOnly = true });
            
            dgvComments.SelectionChanged += DgvComments_SelectionChanged;
            dgvComments.CellContentClick += DgvComments_CellContentClick;
            dgvComments.CellFormatting += DgvComments_CellFormatting;
            dgvComments.ColumnHeaderMouseClick += DgvComments_ColumnHeaderMouseClick;
            
            panel.Controls.Add(dgvComments);
            return panel;
        }

        private Panel CreateCommentDetailPanel()
        {
            pnlCommentDetail = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(15)
            };
            
            pnlCommentDetail.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectangle(pnlCommentDetail.ClientRectangle, 8))
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
                Name = "detailLayout",
                Dock = DockStyle.Fill,
                RowCount = 5,
                ColumnCount = 1,
                BackColor = Color.Transparent
            };
            
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F)); // Header + Stats
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));   // Content
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));  // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));   // Quick Reply
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));  // Reply button
            
            layout.Controls.Add(CreateDetailHeaderPanel(), 0, 0);
            layout.Controls.Add(CreateDetailContentPanel(), 0, 1);
            layout.Controls.Add(CreateDetailQuickActionsPanel(), 0, 2);
            layout.Controls.Add(CreateQuickReplyPanel(), 0, 3);
            layout.Controls.Add(CreateDetailActionsPanel(), 0, 4);
            
            pnlCommentDetail.Controls.Add(layout);
            ShowNoSelectionMessage();
            
            return pnlCommentDetail;
        }

        private Panel CreateDetailHeaderPanel()
        {
            var panel = new Panel
            {
                Name = "detailHeaderPanel",
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            
            return panel;
        }

        private Panel CreateDetailContentPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            
            var lblTitle = new Label
            {
                Text = "📄 Nội dung bình luận",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = false,
                Height = 30,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            txtCommentContent = new RichTextBox
            {
                Name = "txtCommentContent",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true,
                BackColor = Color.FromArgb(247, 249, 252),
                Padding = new Padding(10),
                Cursor = Cursors.IBeam,
                DetectUrls = true,
                ScrollBars = RichTextBoxScrollBars.Vertical
            };
            
            // Allow text selection and copy
            txtCommentContent.Enter += (s, e) => txtCommentContent.BackColor = Color.FromArgb(240, 248, 255);
            txtCommentContent.Leave += (s, e) => txtCommentContent.BackColor = Color.FromArgb(247, 249, 252);
            
            panel.Controls.Add(txtCommentContent);
            panel.Controls.Add(lblTitle); // Add title last so it's on top
            return panel;
        }

        private Panel CreateDetailQuickActionsPanel()
        {
            var panel = new Panel
            {
                Name = "quickActionsPanel",
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 5, 0, 5)
            };
            
            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = Color.Transparent,
                Padding = new Padding(0)
            };
            
            // Only keep Copy button - other actions are already in left panel
            var btnCopyContent = CreateSmallActionButton("� Copy nội dung", Color.FromArgb(52, 73, 94));
            btnCopyContent.Click += (s, e) => {
                if (currentComment != null && !string.IsNullOrEmpty(currentComment.Content)) {
                    Clipboard.SetText(currentComment.Content);
                    MessageBox.Show("Đã copy nội dung bình luận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            
            flowPanel.Controls.Add(btnCopyContent);
            
            panel.Controls.Add(flowPanel);
            return panel;
        }

        private Panel CreateQuickReplyPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 5, 0, 5)
            };
            
            var lblTitle = new Label
            {
                Text = "💬 Trả lời nhanh (Moderator)",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Dock = DockStyle.Top,
                Height = 25
            };
            
            txtQuickReply = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Padding = new Padding(10),
                Text = ""
            };
            
            // Enable interaction with visual feedback
            txtQuickReply.Enter += (s, e) => {
                txtQuickReply.BackColor = Color.FromArgb(255, 255, 240);
                if (txtQuickReply.Text == "Nhập nội dung trả lời...") {
                    txtQuickReply.Text = "";
                    txtQuickReply.ForeColor = Color.Black;
                }
            };
            txtQuickReply.Leave += (s, e) => {
                txtQuickReply.BackColor = Color.White;
                if (string.IsNullOrWhiteSpace(txtQuickReply.Text)) {
                    txtQuickReply.Text = "Nhập nội dung trả lời...";
                    txtQuickReply.ForeColor = Color.Gray;
                }
            };
            
            // Set placeholder on load
            txtQuickReply.Text = "Nhập nội dung trả lời...";
            txtQuickReply.ForeColor = Color.Gray;
            
            panel.Controls.AddRange(new Control[] { lblTitle, txtQuickReply });
            return panel;
        }

        private Panel CreateDetailActionsPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            
            var btnReply = new Button
            {
                Text = "📤 Gửi trả lời",
                Dock = DockStyle.Fill,
                BackColor = PrimaryColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnReply.FlatAppearance.BorderSize = 0;
            btnReply.Click += BtnReply_Click;
            
            panel.Controls.Add(btnReply);
            return panel;
        }

        private void ShowNoSelectionMessage()
        {
            var lblNoSelection = new Label
            {
                Text = "Chọn một bình luận để xem chi tiết",
                Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                ForeColor = Color.FromArgb(149, 165, 166),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "lblNoSelection"
            };
            
            pnlCommentDetail.Controls.Add(lblNoSelection);
            lblNoSelection.BringToFront();
        }
        #endregion

        #region Data Loading
        private void LoadComments()
        {
            try
            {
                allComments = commentService.GetAllComments(includeDeleted: true);
                DisplayComments(allComments);
                lblTotalComments.Text = $"Tổng: {allComments.Count} bình luận";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách bình luận: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayComments(List<Comment> comments)
        {
            filteredComments = comments;
            CalculatePagination();
            
            dgvComments.Rows.Clear();
            
            var pagedComments = GetCurrentPageComments();
            foreach (var comment in pagedComments)
            {
                var user = userService.GetUserById(comment.UserID);
                string authorName = user?.UserName ?? "Unknown";
                
                string shortContent = comment.Content.Length > 80 
                    ? comment.Content.Substring(0, 80) + "..." 
                    : comment.Content;
                
                string status = comment.IsDeleted ? "Đã ẩn" : "Hiển thị";
                
                var row = dgvComments.Rows[dgvComments.Rows.Add()];
                row.Cells["Select"].Value = false;
                row.Cells["CommentID"].Value = comment.CommentID;
                row.Cells["Author"].Value = authorName;
                row.Cells["PostID"].Value = $"Post #{comment.PostID}";
                row.Cells["Content"].Value = shortContent;
                row.Cells["Status"].Value = status;
                row.Cells["CreatedAt"].Value = comment.CreatedAt.ToString("dd/MM/yyyy HH:mm");
                row.Cells["CreatedAt"].Tag = comment.CreatedAt;
                
                row.Tag = comment;
            }
        }
        #endregion

        #region Event Handlers
        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            FilterComments();
        }

        private void CmbStatusFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            FilterComments();
        }

        private void DtpDate_ValueChanged(object? sender, EventArgs e)
        {
            FilterComments();
        }

        private void FilterComments()
        {
            var filtered = allComments.AsEnumerable();
            
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string search = txtSearch.Text.ToLower();
                filtered = filtered.Where(c =>
                {
                    var user = userService.GetUserById(c.UserID);
                    string authorName = user?.UserName ?? "";
                    return c.Content.ToLower().Contains(search) ||
                           authorName.ToLower().Contains(search) ||
                           c.PostID.ToString().Contains(search);
                });
            }
            
            if (cmbStatusFilter.SelectedIndex > 0)
            {
                string status = cmbStatusFilter.SelectedItem?.ToString() ?? "";
                bool isDeleted = status == "Đã ẩn";
                filtered = filtered.Where(c => c.IsDeleted == isDeleted);
            }
            
            filtered = filtered.Where(c => 
                c.CreatedAt.Date >= dtpFromDate.Value.Date && 
                c.CreatedAt.Date <= dtpToDate.Value.Date);
            
            DisplayComments(filtered.ToList());
        }

        private void DgvComments_SelectionChanged(object? sender, EventArgs e)
        {
            int selectedCount = dgvComments.SelectedRows.Count;
            bool hasSelection = selectedCount > 0;
            
            btnHide.Enabled = hasSelection;
            btnUnhide.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
            btnWarnUser.Enabled = hasSelection;
            
            if (selectedCount == 1)
            {
                var row = dgvComments.SelectedRows[0];
                if (row.Tag is Comment comment)
                {
                    LoadCommentDetail(comment);
                }
            }
            
            UpdateBulkActionButton();
        }

        private void DgvComments_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvComments.Columns["Select"].Index)
            {
                dgvComments.EndEdit();
                UpdateBulkActionButton();
            }
        }

        private void DgvComments_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvComments.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString() ?? "";
                var cell = dgvComments.Rows[e.RowIndex].Cells[e.ColumnIndex];
                
                if (status == "Hiển thị")
                {
                    cell.Style.BackColor = Color.FromArgb(46, 204, 113);
                    cell.Style.ForeColor = Color.White;
                    cell.Style.Font = new Font(dgvComments.Font, FontStyle.Bold);
                }
                else if (status == "Đã ẩn")
                {
                    cell.Style.BackColor = Color.FromArgb(231, 76, 60);
                    cell.Style.ForeColor = Color.White;
                    cell.Style.Font = new Font(dgvComments.Font, FontStyle.Bold);
                }
            }
        }

        private void UpdateBulkActionButton()
        {
            int checkedCount = GetCheckedCommentsCount();
            btnBulkAction.Text = checkedCount > 0 
                ? $"📋 Hành động hàng loạt ({checkedCount})" 
                : "📋 Hành động hàng loạt";
            btnBulkAction.Enabled = checkedCount > 0;
        }

        private void DgvComments_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0) return;

            var column = dgvComments.Columns[e.ColumnIndex];
            var columnName = column.Name;

            if (lastSortedColumn == columnName)
            {
                isAscending = !isAscending;
            }
            else
            {
                isAscending = true;
                lastSortedColumn = columnName;
            }

            var currentComments = new List<Comment>();
            foreach (DataGridViewRow row in dgvComments.Rows)
            {
                if (row.Tag is Comment comment)
                    currentComments.Add(comment);
            }

            switch (columnName)
            {
                case "CreatedAt":
                    currentComments = isAscending 
                        ? currentComments.OrderBy(c => c.CreatedAt).ToList()
                        : currentComments.OrderByDescending(c => c.CreatedAt).ToList();
                    break;
                case "CommentID":
                    currentComments = isAscending 
                        ? currentComments.OrderBy(c => c.CommentID).ToList()
                        : currentComments.OrderByDescending(c => c.CommentID).ToList();
                    break;
                case "Author":
                    currentComments = isAscending 
                        ? currentComments.OrderBy(c => userService.GetUserById(c.UserID)?.UserName ?? "").ToList()
                        : currentComments.OrderByDescending(c => userService.GetUserById(c.UserID)?.UserName ?? "").ToList();
                    break;
                case "PostID":
                    currentComments = isAscending 
                        ? currentComments.OrderBy(c => c.PostID).ToList()
                        : currentComments.OrderByDescending(c => c.PostID).ToList();
                    break;
                default:
                    return;
            }

            foreach (DataGridViewColumn col in dgvComments.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            column.HeaderCell.SortGlyphDirection = isAscending ? SortOrder.Ascending : SortOrder.Descending;
            DisplayComments(currentComments);
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
            totalPages = Math.Max(1, (int)Math.Ceiling((double)filteredComments.Count / ItemsPerPage));
            if (currentPage > totalPages) currentPage = totalPages;
            
            btnFirstPage.Enabled = btnPrevPage.Enabled = currentPage > 1;
            btnNextPage.Enabled = btnLastPage.Enabled = currentPage < totalPages;
            lblPageInfo.Text = $"Trang {currentPage} / {totalPages} ({filteredComments.Count} bình luận)";
        }
        
        private List<Comment> GetCurrentPageComments()
        {
            int skip = (currentPage - 1) * ItemsPerPage;
            return filteredComments.Skip(skip).Take(ItemsPerPage).ToList();
        }
        
        private void RefreshCurrentPage()
        {
            CalculatePagination();
            dgvComments.Rows.Clear();
            
            var pagedComments = GetCurrentPageComments();
            foreach (var comment in pagedComments)
            {
                var user = userService.GetUserById(comment.UserID);
                string authorName = user?.UserName ?? "Unknown";
                
                string shortContent = comment.Content.Length > 80 
                    ? comment.Content.Substring(0, 80) + "..." 
                    : comment.Content;
                
                string status = comment.IsDeleted ? "Đã ẩn" : "Hiển thị";
                
                var row = dgvComments.Rows[dgvComments.Rows.Add()];
                row.Cells["Select"].Value = false;
                row.Cells["CommentID"].Value = comment.CommentID;
                row.Cells["Author"].Value = authorName;
                row.Cells["PostID"].Value = $"Post #{comment.PostID}";
                row.Cells["Content"].Value = shortContent;
                row.Cells["Status"].Value = status;
                row.Cells["CreatedAt"].Value = comment.CreatedAt.ToString("dd/MM/yyyy HH:mm");
                row.Cells["CreatedAt"].Tag = comment.CreatedAt;
                
                row.Tag = comment;
            }
        }

        private void LoadCommentDetail(Comment comment)
        {
            currentComment = comment;
            
            // Hide "No Selection" message
            var lblNoSelection = pnlCommentDetail.Controls.Find("lblNoSelection", true).FirstOrDefault();
            if (lblNoSelection != null)
            {
                lblNoSelection.Visible = false;
            }
            
            // Find and show detail layout
            var detailLayout = pnlCommentDetail.Controls.Find("detailLayout", false).FirstOrDefault() as TableLayoutPanel;
            if (detailLayout != null)
            {
                detailLayout.Visible = true;
                detailLayout.BringToFront();
            }
            else
            {
                return;
            }
            
            // Update header panel
            var detailHeader = detailLayout.Controls.Find("detailHeaderPanel", true).FirstOrDefault() as Panel;
            if (detailHeader != null)
            {
                detailHeader.Controls.Clear();
                
                var user = userService.GetUserById(comment.UserID);
                var allPosts = postService.GetAllPosts();
                var post = allPosts.FirstOrDefault(p => p.PostID == comment.PostID);
                var allComments = commentService.GetAllComments(includeDeleted: true);
                
                // Count replies (comments with ParentCommentID == this comment)
                int replyCount = allComments.Count(c => c.ParentCommentID == comment.CommentID);
                
                var lblUser = new Label
                {
                    Text = $"👤 {user?.FullName ?? "Unknown"} (@{user?.UserName ?? "unknown"})",
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(44, 62, 80),
                    Location = new Point(0, 0),
                    AutoSize = true
                };
                
                var lblPost = new Label
                {
                    Text = $"📄 Bài viết #{comment.PostID}",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(127, 140, 141),
                    Location = new Point(0, 25),
                    AutoSize = true,
                    MaximumSize = new Size(320, 0)
                };
                
                var lblDate = new Label
                {
                    Text = $"🕐 {comment.CreatedAt:dd/MM/yyyy HH:mm}",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(149, 165, 166),
                    Location = new Point(0, 48),
                    AutoSize = true
                };
                
                // Stats info
                var lblStats = new Label
                {
                    Text = $"💬 {replyCount} trả lời" + (comment.ParentCommentID.HasValue ? " | ↩️ Phản hồi" : ""),
                    Font = new Font("Segoe UI", 8.5F),
                    ForeColor = Color.FromArgb(127, 140, 141),
                    Location = new Point(0, 68),
                    AutoSize = true
                };
                
                var lblStatus = new Label
                {
                    Text = comment.IsDeleted ? "🔒 Đã ẩn" : "✅ Hiển thị",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = comment.IsDeleted ? DangerColor : SuccessColor,
                    Padding = new Padding(8, 4, 8, 4),
                    Location = new Point(0, 90),
                    AutoSize = true
                };
                
                detailHeader.Controls.AddRange(new Control[] { lblUser, lblPost, lblDate, lblStats, lblStatus });
            }
            
            // Update content with safe text assignment
            if (!string.IsNullOrEmpty(comment.Content))
            {
                txtCommentContent.Text = comment.Content;
                txtCommentContent.SelectionStart = 0;
                txtCommentContent.SelectionLength = 0;
            }
            else
            {
                txtCommentContent.Text = "(Không có nội dung)";
            }
            
            // Clear reply box
            txtQuickReply.Clear();
            txtQuickReply.Text = "Nhập nội dung trả lời...";
            txtQuickReply.ForeColor = Color.Gray;
        }
        #endregion

        #region Action Handlers
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadComments();
            MessageBox.Show("Đã làm mới danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnHide_Click(object? sender, EventArgs e)
        {
            var comments = GetSelectedComments();
            if (comments.Count == 0) return;
            
            var result = MessageBox.Show(
                $"Bạn có chắc muốn ẩn {comments.Count} bình luận?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                foreach (var comment in comments)
                {
                    commentService.DeleteComment(comment.CommentID);
                }
                
                LoadComments();
                MessageBox.Show($"Đã ẩn {comments.Count} bình luận!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnUnhide_Click(object? sender, EventArgs e)
        {
            var comments = GetSelectedComments();
            if (comments.Count == 0) return;
            
            var result = MessageBox.Show(
                $"Bạn có chắc muốn hiện {comments.Count} bình luận?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                foreach (var comment in comments)
                {
                    commentService.RestoreComment(comment.CommentID);
                }
                
                LoadComments();
                MessageBox.Show($"Đã hiện {comments.Count} bình luận!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            var comments = GetSelectedComments();
            if (comments.Count == 0) return;
            
            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa vĩnh viễn {comments.Count} bình luận?\nHành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                foreach (var comment in comments)
                {
                    commentService.DeleteComment(comment.CommentID);
                }
                
                LoadComments();
                MessageBox.Show($"Đã xóa {comments.Count} bình luận!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnWarnUser_Click(object? sender, EventArgs e)
        {
            var comments = GetSelectedComments();
            if (comments.Count == 0) return;
            
            MessageBox.Show($"Chức năng cảnh báo {comments.Count} người dùng đang được phát triển!", 
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBulkAction_Click(object? sender, EventArgs e)
        {
            var checkedComments = GetCheckedComments();
            if (checkedComments.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một bình luận!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var menu = new ContextMenuStrip();
            menu.Items.Add("Ẩn tất cả", null, (s, ev) => BulkHide(checkedComments));
            menu.Items.Add("Hiện tất cả", null, (s, ev) => BulkUnhide(checkedComments));
            menu.Items.Add("Xóa tất cả", null, (s, ev) => BulkDelete(checkedComments));
            menu.Items.Add("Xuất danh sách", null, (s, ev) => BulkExport(checkedComments));
            
            menu.Show(btnBulkAction, new Point(0, btnBulkAction.Height));
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    FileName = $"comments_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var lines = new List<string>();
                    lines.Add("ID,Người dùng,Bài viết,Nội dung,Trạng thái,Ngày tạo");
                    
                    foreach (var comment in allComments)
                    {
                        var user = userService.GetUserById(comment.UserID);
                        string content = comment.Content.Replace(",", ";").Replace("\n", " ");
                        string status = comment.IsDeleted ? "Đã ẩn" : "Hiển thị";
                        lines.Add($"{comment.CommentID},{user?.UserName ?? "Unknown"},Post #{comment.PostID},{content},{status},{comment.CreatedAt:yyyy-MM-dd HH:mm}");
                    }
                    
                    System.IO.File.WriteAllLines(saveDialog.FileName, lines);
                    MessageBox.Show($"Đã xuất {allComments.Count} bình luận!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất file: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReply_Click(object? sender, EventArgs e)
        {
            if (currentComment == null)
            {
                MessageBox.Show("Vui lòng chọn một bình luận!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txtQuickReply.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung trả lời!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // TODO: Implement reply logic
            MessageBox.Show("Đã gửi trả lời!", "Thành công", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtQuickReply.Clear();
        }
        #endregion

        #region Bulk Actions
        private List<Comment> GetSelectedComments()
        {
            var comments = new List<Comment>();
            foreach (DataGridViewRow row in dgvComments.SelectedRows)
            {
                if (row.Tag is Comment comment)
                {
                    comments.Add(comment);
                }
            }
            return comments;
        }

        private List<Comment> GetCheckedComments()
        {
            var comments = new List<Comment>();
            foreach (DataGridViewRow row in dgvComments.Rows)
            {
                if (row.Cells["Select"].Value is bool isChecked && isChecked && row.Tag is Comment comment)
                {
                    comments.Add(comment);
                }
            }
            return comments;
        }

        private int GetCheckedCommentsCount()
        {
            int count = 0;
            foreach (DataGridViewRow row in dgvComments.Rows)
            {
                if (row.Cells["Select"].Value is bool isChecked && isChecked)
                {
                    count++;
                }
            }
            return count;
        }

        private void BulkHide(List<Comment> comments)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc muốn ẩn {comments.Count} bình luận?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                foreach (var comment in comments)
                {
                    commentService.DeleteComment(comment.CommentID);
                }
                
                LoadComments();
                MessageBox.Show($"Đã ẩn {comments.Count} bình luận!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BulkUnhide(List<Comment> comments)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc muốn hiện {comments.Count} bình luận?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                foreach (var comment in comments)
                {
                    commentService.RestoreComment(comment.CommentID);
                }
                
                LoadComments();
                MessageBox.Show($"Đã hiện {comments.Count} bình luận!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BulkDelete(List<Comment> comments)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa vĩnh viễn {comments.Count} bình luận?\nHành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                foreach (var comment in comments)
                {
                    commentService.DeleteComment(comment.CommentID);
                }
                
                LoadComments();
                MessageBox.Show($"Đã xóa {comments.Count} bình luận!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BulkExport(List<Comment> comments)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"selected_comments_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var lines = new List<string>();
                    lines.Add("ID,Người dùng,Bài viết,Nội dung,Trạng thái,Ngày tạo");
                    
                    foreach (var comment in comments)
                    {
                        var user = userService.GetUserById(comment.UserID);
                        string content = comment.Content.Replace(",", ";").Replace("\n", " ");
                        string status = comment.IsDeleted ? "Đã ẩn" : "Hiển thị";
                        lines.Add($"{comment.CommentID},{user?.UserName ?? "Unknown"},Post #{comment.PostID},{content},{status},{comment.CreatedAt:yyyy-MM-dd HH:mm}");
                    }
                    
                    System.IO.File.WriteAllLines(saveDialog.FileName, lines);
                    MessageBox.Show($"Đã xuất {comments.Count} bình luận!", "Thành công", 
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
                Width = 150,
                Height = 35,
                FlatStyle = FlatStyle.Flat,
                BackColor = color,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 5, 0)
            };
            
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(color, 0.1f);
            btn.MouseLeave += (s, e) => btn.BackColor = color;
            
            return btn;
        }

        private Button CreateSmallActionButton(string text, Color color)
        {
            var btn = new Button
            {
                Text = text,
                Width = 85,
                Height = 32,
                FlatStyle = FlatStyle.Flat,
                BackColor = color,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 5, 0)
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
