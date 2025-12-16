using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using SocialManager.services;
using SocialManager.constants;
using SocialManager.models;

namespace SocialManager.frm.UserControls
{
    public partial class ucUserManagementNew : UserControl
    {
        private UserService userService = null!;
        private PostService postService = null!;
        private ReportService reportService = null!;
        
        private List<User> allUsers = new List<User>();
        private List<User> selectedUsers = new List<User>();
        private User? currentUser = null;
        
        // Sorting state
        private string? lastSortedColumn;
        private bool isAscending = true;
        
        // Pagination
        private const int ItemsPerPage = 15;
        private int currentPage = 1;
        private int totalPages = 1;
        private List<User> filteredUsers = new List<User>();
        
        // Pagination controls
        private Button btnFirstPage = null!;
        private Button btnPrevPage = null!;
        private Button btnNextPage = null!;
        private Button btnLastPage = null!;
        private Label lblPageInfo = null!;
        
        // Main controls
        private DataGridView dgvUsers;
        private TextBox txtSearch;
        private ComboBox cmbRoleFilter;
        private ComboBox cmbStatusFilter;
        private Panel pnlActions;
        private Panel pnlUserDetail;
        private Label lblTotalUsers;
        
        // Action buttons
        private Button btnBan;
        private Button btnUnban;
        private Button btnWarning;
        private Button btnResetPassword;
        private Button btnChangeRole;
        private Button btnBulkAction;
        private Button btnExport;
        private Button btnRefresh;
        
        // User detail panel controls
        private Label lblUserName;
        private Label lblUserEmail;
        private Label lblUserRole;
        private Label lblUserStatus;
        private Label lblJoinDate;
        private TabControl tabUserDetails;
        
        // Analytics
        private Panel pnlUserAnalytics;

        public ucUserManagementNew()
        {
            InitializeComponent();
            InitializeServices();
            CreateLayout();
            LoadUsers();
            
            // Apply theme after layout is created
            this.Load += (s, e) => SocialManager.utils.GlobalSettings.ApplyThemeToUserControl(this);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            BackColor = Color.FromArgb(247, 249, 252);
            Name = "ucUserManagementNew";
            Size = new Size(1200, 700);
            Padding = new Padding(10); // Reduced padding
            ResumeLayout(false);
        }

        private void InitializeServices()
        {
            userService = new UserService();
            postService = new PostService();
            reportService = new ReportService();
            allUsers = new List<User>();
        }

        private void CreateLayout()
        {
            // Main layout with 3 columns
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            
            // Left: User list (60%)
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            // Right: User detail (40%)
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            
            // Left panel
            var leftPanel = CreateUserListPanel();
            mainLayout.Controls.Add(leftPanel, 0, 0);
            
            // Right panel
            pnlUserDetail = CreateUserDetailPanel();
            mainLayout.Controls.Add(pnlUserDetail, 1, 0);
            
            this.Controls.Add(mainLayout);
        }

        #region User List Panel
        private Panel CreateUserListPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 0, 10, 0),
                BackColor = Color.Transparent
            };
            
            var container = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 5,
                ColumnCount = 1,
                BackColor = Color.Transparent
            };
            
            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Header (reduced from 80)
            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Search & Filter
            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));  // Actions
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // DataGridView
            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));  // Pagination
            
            // 1. Header
            var headerPanel = CreateHeaderPanel();
            container.Controls.Add(headerPanel, 0, 0);
            
            // 2. Search and Filter
            var searchPanel = CreateSearchFilterPanel();
            container.Controls.Add(searchPanel, 0, 1);
            
            // 3. Actions
            pnlActions = CreateActionsPanel();
            container.Controls.Add(pnlActions, 0, 2);
            
            // 4. DataGridView
            var gridPanel = CreateDataGridPanel();
            container.Controls.Add(gridPanel, 0, 3);
            
            // 5. Pagination
            var paginationPanel = CreatePaginationPanel();
            container.Controls.Add(paginationPanel, 0, 4);
            
            panel.Controls.Add(container);
            return panel;
        }

        private Panel CreateHeaderPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(5, 5, 5, 5)
            };
            
            var lblTitle = new Label
            {
                Text = "Danh Sách Người Dùng",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(10, 10),
                AutoSize = true
            };
            
            lblTotalUsers = new Label
            {
                Text = "Tổng: 0 người dùng",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(10, 35),
                AutoSize = true
            };
            
            btnRefresh = new Button
            {
                Text = "🔄 Làm mới",
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;
            
            // Position refresh button properly
            panel.Resize += (s, e) => {
                btnRefresh.Location = new Point(panel.Width - 110, 10);
            };
            btnRefresh.Location = new Point(400, 10);
            
            panel.Controls.AddRange(new Control[] { lblTitle, lblTotalUsers, btnRefresh });
            return panel;
        }

        private Panel CreateSearchFilterPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 10, 0, 0)
            };
            
            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent
            };
            
            // Search box
            txtSearch = new TextBox
            {
                Width = 300,
                Height = 35,
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "🔍 Tìm kiếm theo tên, email..."
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            
            // Role filter
            cmbRoleFilter = new ComboBox
            {
                Width = 150,
                Height = 35,
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRoleFilter.Items.AddRange(new object[] { "Tất cả vai trò", "Người dùng", "Đã xác minh", "Kiểm duyệt viên", "Quản trị viên" });
            cmbRoleFilter.SelectedIndex = 0;
            cmbRoleFilter.SelectedIndexChanged += CmbRoleFilter_SelectedIndexChanged;
            
            // Status filter
            cmbStatusFilter = new ComboBox
            {
                Width = 150,
                Height = 35,
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatusFilter.Items.AddRange(new object[] { "Tất cả trạng thái", "Bình thường", "Cảnh báo", "Tạm ngừng" });
            cmbStatusFilter.SelectedIndex = 0;
            cmbStatusFilter.SelectedIndexChanged += CmbStatusFilter_SelectedIndexChanged;
            
            flowPanel.Controls.AddRange(new Control[] { txtSearch, cmbRoleFilter, cmbStatusFilter });
            panel.Controls.Add(flowPanel);
            
            return panel;
        }

        private Panel CreateActionsPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            
            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = Color.Transparent
            };
            
            btnBan = CreateActionButton("🚫 Ban", Color.FromArgb(231, 76, 60));
            btnBan.Click += BtnBan_Click;
            btnBan.Enabled = false;
            
            btnUnban = CreateActionButton("✅ Unban", Color.FromArgb(46, 204, 113));
            btnUnban.Click += BtnUnban_Click;
            btnUnban.Enabled = false;
            
            btnWarning = CreateActionButton("⚠️ Cảnh báo", Color.FromArgb(243, 156, 18));
            btnWarning.Click += BtnWarning_Click;
            btnWarning.Enabled = false;
            
            btnResetPassword = CreateActionButton("🔑 Reset MK", Color.FromArgb(52, 152, 219));
            btnResetPassword.Click += BtnResetPassword_Click;
            btnResetPassword.Enabled = false;
            
            btnChangeRole = CreateActionButton("👤 Đổi role", Color.FromArgb(155, 89, 182));
            btnChangeRole.Click += BtnChangeRole_Click;
            btnChangeRole.Enabled = false;
            
            btnBulkAction = CreateActionButton("📋 Hành động hàng loạt", Color.FromArgb(52, 73, 94));
            btnBulkAction.Click += BtnBulkAction_Click;
            btnBulkAction.Enabled = false;
            
            btnExport = CreateActionButton("📊 Xuất file", Color.FromArgb(39, 174, 96));
            btnExport.Click += BtnExport_Click;
            
            var btnRefreshAction = CreateActionButton("🔄 Làm mới", Color.FromArgb(52, 152, 219));
            btnRefreshAction.Click += BtnRefresh_Click;
            
            flowPanel.Controls.AddRange(new Control[] { 
                btnBan, btnUnban, btnWarning, btnResetPassword, btnChangeRole, btnBulkAction, btnExport, btnRefreshAction
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
            
            dgvUsers = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false,
                Font = new Font("Segoe UI", 9F),
                RowTemplate = { Height = 45 }
            };
            
            // Column headers
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvUsers.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgvUsers.ColumnHeadersHeight = 40;
            
            // Alternating row colors
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);
            dgvUsers.RowsDefaultCellStyle.BackColor = Color.White;
            dgvUsers.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvUsers.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            
            // Add columns
            var checkBoxColumn = new DataGridViewCheckBoxColumn { Name = "Select", HeaderText = "☑️", Width = 40 };
            checkBoxColumn.ReadOnly = false;
            dgvUsers.Columns.Add(checkBoxColumn);
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "UserID", HeaderText = "ID", Visible = false });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "UserName", HeaderText = "Tên đăng nhập", Width = 150, ReadOnly = true });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "FullName", HeaderText = "Họ tên", Width = 180, ReadOnly = true });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "Email", Width = 200, ReadOnly = true });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Role", HeaderText = "Vai trò", Width = 100, ReadOnly = true });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Trạng thái", Width = 100, ReadOnly = true });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "CreatedAt", HeaderText = "Ngày tham gia", Width = 120, ReadOnly = true });
            
            dgvUsers.SelectionChanged += DgvUsers_SelectionChanged;
            dgvUsers.CellClick += DgvUsers_CellClick;
            dgvUsers.CellFormatting += DgvUsers_CellFormatting;
            dgvUsers.ColumnHeaderMouseClick += DgvUsers_ColumnHeaderMouseClick;
            
            panel.Controls.Add(dgvUsers);
            return panel;
        }
        #endregion

        #region User Detail Panel
        private Panel CreateUserDetailPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(15),
                Margin = new Padding(0, 0, 0, 0) // No margin needed, will use inner padding
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
            
            var container = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 70, 0, 0) // Add top padding to avoid being covered by left header (60px + 10px margin)
            };
            
            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 150)); // Profile header
            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Quick stats
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Tabs
            
            // Profile header
            var profileHeader = CreateProfileHeader();
            container.Controls.Add(profileHeader, 0, 0);
            
            // Quick stats
            var statsPanel = CreateQuickStatsPanel();
            container.Controls.Add(statsPanel, 0, 1);
            
            // Tabs
            tabUserDetails = CreateUserDetailTabs();
            container.Controls.Add(tabUserDetails, 0, 2);
            
            panel.Controls.Add(container);
            return panel;
        }

        private Panel CreateProfileHeader()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Name = "profileHeaderPanel"
            };
            
            var lblNoSelection = new Label
            {
                Text = "Chọn một người dùng để xem chi tiết",
                Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                ForeColor = Color.FromArgb(149, 165, 166),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "lblNoSelection"
            };
            
            panel.Controls.Add(lblNoSelection);
            return panel;
        }

        private Panel CreateQuickStatsPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Name = "quickStatsPanel"
            };
            
            return panel;
        }

        private TabControl CreateUserDetailTabs()
        {
            var tabs = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9.5F),
                Name = "tabUserDetails"
            };
            
            // Tab 1: Thông tin cá nhân
            var tabProfile = new TabPage("📋 Thông tin");
            tabs.TabPages.Add(tabProfile);
            
            // Tab 2: Bài viết
            var tabPosts = new TabPage("📝 Bài viết");
            tabs.TabPages.Add(tabPosts);
            
            // Tab 3: Bình luận
            var tabComments = new TabPage("💬 Bình luận");
            tabComments.Controls.Add(CreateCommentsPanel());
            tabs.TabPages.Add(tabComments);
            
            // Tab 4: Báo cáo
            var tabReports = new TabPage("⚠️ Báo cáo");
            tabReports.Controls.Add(CreateReportsPanel());
            tabs.TabPages.Add(tabReports);
            
            // Tab 5: Lịch sử cảnh báo
            var tabWarnings = new TabPage("🚨 Cảnh báo");
            tabWarnings.Controls.Add(CreateWarningsPanel());
            tabs.TabPages.Add(tabWarnings);
            
            // Tab 6: Analytics
            var tabAnalytics = new TabPage("📊 Phân tích");
            tabs.TabPages.Add(tabAnalytics);
            
            return tabs;
        }

        private Panel CreateCommentsPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            var lblPlaceholder = new Label
            {
                Text = "Danh sách bình luận của người dùng sẽ hiển thị ở đây",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            panel.Controls.Add(lblPlaceholder);
            return panel;
        }

        private Panel CreateReportsPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            var dgvReports = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9.5F),
                Name = "dgvUserReports"
            };

            dgvReports.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(231, 76, 60);
            dgvReports.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReports.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvReports.ColumnHeadersHeight = 40;

            dgvReports.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReportID", HeaderText = "ID", Width = 50 });
            dgvReports.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReportType", HeaderText = "Loại", Width = 80 });
            dgvReports.Columns.Add(new DataGridViewTextBoxColumn { Name = "ContentID", HeaderText = "Nội dung ID", Width = 100 });
            dgvReports.Columns.Add(new DataGridViewTextBoxColumn { Name = "Reporter", HeaderText = "Người báo cáo", Width = 150 });
            dgvReports.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReportedAt", HeaderText = "Ngày báo cáo", Width = 150 });

            panel.Controls.Add(dgvReports);
            return panel;
        }

        private Panel CreateWarningsPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            var lstWarnings = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(247, 249, 252),
                Name = "lstUserWarnings"
            };

            panel.Controls.Add(lstWarnings);
            return panel;
        }
        #endregion

        #region Data Loading
        private void LoadUsers()
        {
            try
            {
                allUsers = userService.GetAllUsers();
                DisplayUsers(allUsers);
                UpdateUserCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách người dùng: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayUsers(List<User> users)
        {
            filteredUsers = users;
            CalculatePagination();
            
            // Temporarily disable selection changed event
            dgvUsers.SelectionChanged -= DgvUsers_SelectionChanged;
            
            dgvUsers.Rows.Clear();
            
            var pagedUsers = GetCurrentPageUsers();
            foreach (var user in pagedUsers)
            {
                int rowIndex = dgvUsers.Rows.Add();
                var row = dgvUsers.Rows[rowIndex];
                
                row.Cells["Select"].Value = false;
                row.Cells["UserID"].Value = user.UserID;
                row.Cells["UserName"].Value = user.UserName;
                row.Cells["FullName"].Value = user.FullName;
                row.Cells["Email"].Value = user.Email;
                row.Cells["Role"].Value = GetRoleName(user.Role);
                row.Cells["Status"].Value = user.AccountStatus;
                row.Cells["CreatedAt"].Value = user.CreatedAt.ToString("dd/MM/yyyy");
                row.Cells["CreatedAt"].Tag = user.CreatedAt;
                row.Tag = user;
            }
            
            // Re-enable selection changed event
            dgvUsers.SelectionChanged += DgvUsers_SelectionChanged;
            
            // Update user count label
            lblTotalUsers.Text = $"Tổng: {filteredUsers.Count} người dùng";
        }

        private void UpdateUserCount()
        {
            lblTotalUsers.Text = $"Tổng: {allUsers.Count} người dùng";
        }
        #endregion

        #region Event Handlers
        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            FilterUsers();
        }

        private void CmbRoleFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            FilterUsers();
        }

        private void CmbStatusFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            FilterUsers();
        }

        private void FilterUsers()
        {
            var filtered = allUsers.AsEnumerable();
            
            // Search filter
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                string search = txtSearch.Text.ToLower();
                filtered = filtered.Where(u => 
                    u.UserName.ToLower().Contains(search) || 
                    u.Email.ToLower().Contains(search) ||
                    u.FullName.ToLower().Contains(search));
            }
            
            // Role filter
            if (cmbRoleFilter.SelectedIndex > 0)
            {
                string role = cmbRoleFilter.SelectedItem?.ToString() ?? "";
                int roleId = GetRoleId(role);
                filtered = filtered.Where(u => u.Role == roleId);
            }
            
            // Status filter
            if (cmbStatusFilter.SelectedIndex > 0)
            {
                string status = cmbStatusFilter.SelectedItem?.ToString() ?? "";
                filtered = filtered.Where(u => u.AccountStatus == status);
            }
            
            DisplayUsers(filtered.ToList());
        }

        private void DgvUsers_SelectionChanged(object? sender, EventArgs e)
        {
            // Update action buttons
            int selectedCount = dgvUsers.SelectedRows.Count;
            bool hasSelection = selectedCount > 0;
            
            btnBan.Enabled = hasSelection;
            btnUnban.Enabled = hasSelection;
            btnWarning.Enabled = hasSelection;
            btnResetPassword.Enabled = hasSelection;
            btnChangeRole.Enabled = hasSelection;
            
            // Update selected users list
            selectedUsers.Clear();
            foreach (DataGridViewRow row in dgvUsers.SelectedRows)
            {
                if (row.Tag is User user)
                {
                    selectedUsers.Add(user);
                }
            }
            
            // Show user detail if single selection
            if (selectedCount == 1 && dgvUsers.SelectedRows[0].Tag is User selectedUser)
            {
                ShowUserDetail(selectedUser);
            }
            else if (selectedCount == 0)
            {
                // Show "no selection" message
                HideUserDetail();
            }
            
            // Update bulk action button
            int checkedCount = GetCheckedUsersCount();
            btnBulkAction.Enabled = checkedCount > 0;
            btnBulkAction.Text = checkedCount > 0 ? $"📋 Hành động hàng loạt ({checkedCount})" : "📋 Hành động hàng loạt";
        }

        private void DgvUsers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Handle checkbox column
                if (e.ColumnIndex == dgvUsers.Columns["Select"].Index)
                {
                    dgvUsers.EndEdit();
                    DgvUsers_SelectionChanged(sender, e);
                }
                else
                {
                    // Show user detail
                    var row = dgvUsers.Rows[e.RowIndex];
                    if (row.Tag is User user)
                    {
                        ShowUserDetail(user);
                    }
                }
            }
        }

        private void DgvUsers_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvUsers.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString() ?? "";
                switch (status)
                {
                    case "Bình thường":
                        e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96);
                        e.CellStyle.Font = new Font(dgvUsers.Font, FontStyle.Bold);
                        break;
                    case "Tạm ngừng":
                        e.CellStyle.ForeColor = Color.FromArgb(192, 57, 43);
                        e.CellStyle.Font = new Font(dgvUsers.Font, FontStyle.Bold);
                        break;
                    case "Cảnh báo":
                        e.CellStyle.ForeColor = Color.FromArgb(243, 156, 18);
                        e.CellStyle.Font = new Font(dgvUsers.Font, FontStyle.Bold);
                        break;
                    default:
                        e.CellStyle.ForeColor = Color.FromArgb(149, 165, 166);
                        break;
                }
            }
            
            if (dgvUsers.Columns[e.ColumnIndex].Name == "Role" && e.Value != null)
            {
                string role = e.Value.ToString() ?? "";
                switch (role)
                {
                    case "Admin":
                        e.CellStyle.ForeColor = Color.FromArgb(192, 57, 43);
                        e.CellStyle.Font = new Font(dgvUsers.Font, FontStyle.Bold);
                        break;
                    case "Moderator":
                        e.CellStyle.ForeColor = Color.FromArgb(155, 89, 182);
                        e.CellStyle.Font = new Font(dgvUsers.Font, FontStyle.Bold);
                        break;
                    case "User":
                        e.CellStyle.ForeColor = Color.FromArgb(52, 73, 94);
                        break;
                }
            }
        }

        private void ShowUserDetail(User user)
        {
            currentUser = user;
            
            // Find the profile header panel
            var profileHeader = pnlUserDetail.Controls.Find("profileHeaderPanel", true).FirstOrDefault() as Panel;
            if (profileHeader == null) return;
            
            // Clear previous content
            profileHeader.Controls.Clear();
            
            // Avatar (placeholder)
            var picAvatar = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(15, 15),
                BackColor = Color.FromArgb(52, 152, 219),
                BorderStyle = BorderStyle.None
            };
            
            picAvatar.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, 79, 79);
                    picAvatar.Region = new Region(path);
                }
                
                // Draw initials
                string initials = GetInitials(user.FullName);
                using (var font = new Font("Segoe UI", 24F, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.White))
                {
                    var size = e.Graphics.MeasureString(initials, font);
                    var x = (picAvatar.Width - size.Width) / 2;
                    var y = (picAvatar.Height - size.Height) / 2;
                    e.Graphics.DrawString(initials, font, brush, x, y);
                }
            };
            
            // User info
            var lblName = new Label
            {
                Text = user.FullName,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(105, 15),
                AutoSize = true
            };
            
            var lblUsername = new Label
            {
                Text = $"@{user.UserName}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(105, 40),
                AutoSize = true
            };
            
            var lblEmail = new Label
            {
                Text = user.Email,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(149, 165, 166),
                Location = new Point(105, 60),
                AutoSize = true
            };
            
            // Status badge
            var lblStatus = new Label
            {
                Text = user.AccountStatus,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = GetAccountStatusColor(user.AccountStatus),
                AutoSize = true,
                Padding = new Padding(8, 4, 8, 4),
                Location = new Point(105, 85)
            };
            
            // Role badge
            var lblRole = new Label
            {
                Text = GetRoleName(user.Role),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = GetRoleColorByInt(user.Role),
                AutoSize = true,
                Padding = new Padding(8, 4, 8, 4),
                Location = new Point(105 + lblStatus.Width + 10, 85)
            };
            
            profileHeader.Controls.AddRange(new Control[] { 
                picAvatar, lblName, lblUsername, lblEmail, lblStatus, lblRole 
            });
            
            // Load user data into tabs
            LoadUserProfileTab(user);
            LoadUserPostsTab(user);
            LoadUserReportsTab(user);
            LoadUserAnalyticsTab(user);
        }

        private void HideUserDetail()
        {
            currentUser = null;
            
            // Find the profile header panel
            var profileHeader = pnlUserDetail.Controls.Find("profileHeaderPanel", true).FirstOrDefault() as Panel;
            if (profileHeader == null) return;
            
            // Clear and show "no selection" message
            profileHeader.Controls.Clear();
            
            var lblNoSelection = new Label
            {
                Text = "Chọn một người dùng để xem chi tiết",
                Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                ForeColor = Color.FromArgb(149, 165, 166),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "lblNoSelection"
            };
            
            profileHeader.Controls.Add(lblNoSelection);
            
            // Clear all tabs
            foreach (TabPage tab in tabUserDetails.TabPages)
            {
                tab.Controls.Clear();
            }
        }

        private void LoadUserProfileTab(User user)
        {
            var tab = tabUserDetails.TabPages[0];
            tab.Controls.Clear();
            
            var tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                AutoScroll = true,
                Padding = new Padding(15),
                ColumnCount = 4,
                RowCount = 7
            };
            
            // Set column styles: Label - Value - Label - Value
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F)); // Label 1
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));   // Value 1
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F)); // Label 2
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));   // Value 2
            
            // Set row styles
            for (int i = 0; i < 7; i++)
            {
                tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }
            
            // Row 0: ID (full width with truncation)
            AddGridField(tableLayout, 0, 0, "ID:", user.UserID.ToString().Substring(0, 8) + "...", user.UserID.ToString(), true);
            
            // Row 1: Username | Full Name
            AddGridField(tableLayout, 1, 0, "Tên đăng nhập:", user.UserName);
            AddGridField(tableLayout, 1, 2, "Họ tên:", user.FullName);
            
            // Row 2: Email (full width)
            AddGridField(tableLayout, 2, 0, "Email:", user.Email, null, true);
            
            // Row 3: Phone | Gender
            AddGridField(tableLayout, 3, 0, "Số điện thoại:", user.Phone ?? "N/A");
            AddGridField(tableLayout, 3, 2, "Giới tính:", user.Gender == 0 ? "Nam" : "Nữ");
            
            // Row 4: DOB | Address
            AddGridField(tableLayout, 4, 0, "Ngày sinh:", user.DOB.ToString("dd/MM/yyyy"));
            AddGridField(tableLayout, 4, 2, "Địa chỉ:", user.Address ?? "N/A");
            
            // Row 5: Created At | Role
            AddGridField(tableLayout, 5, 0, "Ngày tạo:", user.CreatedAt.ToString("dd/MM/yyyy HH:mm"));
            AddGridField(tableLayout, 5, 2, "Vai trò:", GetRoleName(user.Role));
            
            // Row 6: Status | Report Count
            AddGridField(tableLayout, 6, 0, "Trạng thái:", user.AccountStatus);
            AddGridField(tableLayout, 6, 2, "Số lần báo cáo:", user.ReportCount.ToString());
            
            tab.Controls.Add(tableLayout);
        }

        private void AddGridField(TableLayoutPanel table, int row, int col, string label, string value, string? tooltip = null, bool fullWidth = false)
        {
            var lblLabel = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Padding = new Padding(0, 5, 0, 5)
            };
            
            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Padding = new Padding(0, 5, 0, 5)
            };
            
            // Add tooltip if provided
            if (!string.IsNullOrEmpty(tooltip))
            {
                var toolTip = new ToolTip();
                toolTip.SetToolTip(lblValue, tooltip);
            }
            
            table.Controls.Add(lblLabel, col, row);
            table.Controls.Add(lblValue, col + 1, row);
            
            // If full width, merge columns
            if (fullWidth)
            {
                table.SetColumnSpan(lblValue, 3);
            }
        }

        private void LoadUserPostsTab(User user)
        {
            var tab = tabUserDetails.TabPages[1];
            tab.Controls.Clear();
            
            var posts = postService.GetAllPosts()
                .Where(p => p.UserID == user.UserID)
                .ToList();
            
            var lblCount = new Label
            {
                Text = $"Tổng số bài viết: {posts.Count}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(15, 15),
                AutoSize = true
            };
            
            var listBox = new ListBox
            {
                Location = new Point(15, 45),
                Size = new Size(tab.Width - 30, tab.Height - 60),
                Font = new Font("Segoe UI", 9F),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            
            foreach (var post in posts.Take(20))
            {
                string preview = post.Content.Length > 60 ? post.Content.Substring(0, 60) + "..." : post.Content;
                listBox.Items.Add($"{post.CreatedAt:dd/MM/yyyy} - {preview}");
            }
            
            tab.Controls.AddRange(new Control[] { lblCount, listBox });
        }

        private void LoadUserReportsTab(User user)
        {
            // Tab 4: Báo cáo (index 3)
            var tab = tabUserDetails.TabPages[3];
            
            // Find the DataGridView in the tab
            var dgvReports = tab.Controls.Find("dgvUserReports", true).FirstOrDefault() as DataGridView;
            if (dgvReports == null) return;
            
            dgvReports.Rows.Clear();
            
            // Remove any existing "no reports" label
            var existingLabel = tab.Controls.Find("lblNoReports", true).FirstOrDefault();
            if (existingLabel != null)
            {
                tab.Controls.Remove(existingLabel);
                existingLabel.Dispose();
            }
            
            // Get all reports where this user is the reported user
            var reports = reportService.GetAllReports()
                .Where(r => r.ReportedUserID == user.UserID)
                .ToList();
            
            if (reports.Count == 0)
            {
                // Hide the DataGridView and show "no reports" message
                dgvReports.Visible = false;
                
                var lblNoReports = new Label
                {
                    Name = "lblNoReports",
                    Text = "Người dùng này chưa bị báo cáo",
                    Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(149, 165, 166),
                    AutoSize = true,
                    Location = new Point(20, 20)
                };
                tab.Controls.Add(lblNoReports);
                lblNoReports.BringToFront();
            }
            else
            {
                // Show the DataGridView and populate it
                dgvReports.Visible = true;
                
                foreach (var report in reports)
                {
                    var reporter = userService.GetUserById(report.ReporterUserID);
                    
                    dgvReports.Rows.Add(
                        report.ReportID,
                        report.ReportType,
                        report.ContentID,
                        reporter?.UserName ?? "Unknown",
                        report.ReportedAt.ToString("dd/MM/yyyy HH:mm")
                    );
                }
            }
        }

        private void LoadUserAnalyticsTab(User user)
        {
            var tab = tabUserDetails.TabPages[5];
            tab.Controls.Clear();
            
            var posts = postService.GetAllPosts()
                .Where(p => p.UserID == user.UserID)
                .ToList();
            var reports = reportService.GetAllReports()
                .Where(r => r.ReportedUserID == user.UserID)
                .ToList();
            
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(15)
            };
            
            int yPos = 10;
            
            // Stats cards
            AddStatCard(panel, "📝 Tổng bài viết", posts.Count.ToString(), new Point(10, yPos));
            AddStatCard(panel, "💬 Tổng bình luận", "0", new Point(140, yPos)); // TODO: Get from service
            AddStatCard(panel, "⚠️ Báo cáo nhận", reports.Count.ToString(), new Point(270, yPos));
            
            tab.Controls.Add(panel);
        }

        private void AddStatCard(Panel parent, string title, string value, Point location)
        {
            var card = new Panel
            {
                Size = new Size(120, 80),
                Location = location,
                BackColor = Color.FromArgb(240, 248, 255),
                Padding = new Padding(10)
            };
            
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectangle(card.ClientRectangle, 6))
                {
                    using (var brush = new SolidBrush(card.BackColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };
            
            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 45
            };
            
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(127, 140, 141),
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 25
            };
            
            card.Controls.AddRange(new Control[] { lblValue, lblTitle });
            parent.Controls.Add(card);
        }
        #endregion

        #region Action Handlers
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            // Force reload services to get fresh data from files
            userService = new UserService();
            postService = new PostService();
            reportService = new ReportService();
            
            LoadUsers();
            MessageBox.Show("Đã làm mới danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBan_Click(object? sender, EventArgs e)
        {
            if (selectedUsers.Count == 0) return;
            
            var result = MessageBox.Show(
                $"Bạn có chắc muốn ban {selectedUsers.Count} người dùng?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                foreach (var user in selectedUsers)
                {
                    user.StatusId = -1; // Ban user
                    userService.UpdateUser(user);
                }
                
                LoadUsers();
                MessageBox.Show($"Đã ban {selectedUsers.Count} người dùng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnUnban_Click(object? sender, EventArgs e)
        {
            if (selectedUsers.Count == 0) return;
            
            var result = MessageBox.Show(
                $"Bạn có chắc muốn unban {selectedUsers.Count} người dùng?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                foreach (var user in selectedUsers)
                {
                    user.StatusId = 1; // Unban user (Active)
                    user.ReportCount = 0; // Reset report count
                    userService.UpdateUser(user);
                }
                
                LoadUsers();
                MessageBox.Show($"Đã unban {selectedUsers.Count} người dùng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnWarning_Click(object? sender, EventArgs e)
        {
            if (selectedUsers.Count == 0) return;
            
            var form = new Form
            {
                Text = "Gửi cảnh báo",
                Size = new Size(400, 250),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };
            
            var lblMessage = new Label
            {
                Text = "Nội dung cảnh báo:",
                Location = new Point(20, 20),
                AutoSize = true
            };
            
            var txtMessage = new TextBox
            {
                Location = new Point(20, 45),
                Size = new Size(340, 100),
                Multiline = true,
                Font = new Font("Segoe UI", 10F)
            };
            
            var btnSend = new Button
            {
                Text = "Gửi cảnh báo",
                Location = new Point(260, 160),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(243, 156, 18),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            
            btnSend.Click += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtMessage.Text))
                {
                    MessageBox.Show("Vui lòng nhập nội dung cảnh báo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // TODO: Save warning to database
                MessageBox.Show($"Đã gửi cảnh báo đến {selectedUsers.Count} người dùng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                form.Close();
            };
            
            form.Controls.AddRange(new Control[] { lblMessage, txtMessage, btnSend });
            form.ShowDialog();
        }

        private void BtnResetPassword_Click(object? sender, EventArgs e)
        {
            if (selectedUsers.Count == 0) return;
            
            var result = MessageBox.Show(
                $"Bạn có chắc muốn reset mật khẩu cho {selectedUsers.Count} người dùng?\nMật khẩu mới sẽ được gửi qua email.",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                // TODO: Implement password reset logic
                MessageBox.Show($"Đã reset mật khẩu cho {selectedUsers.Count} người dùng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnChangeRole_Click(object? sender, EventArgs e)
        {
            if (selectedUsers.Count == 0) return;
            
            var form = new Form
            {
                Text = "Thay đổi vai trò",
                Size = new Size(350, 180),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };
            
            var lblRole = new Label
            {
                Text = "Chọn vai trò mới:",
                Location = new Point(20, 20),
                AutoSize = true
            };
            
            var cmbRole = new ComboBox
            {
                Location = new Point(20, 45),
                Size = new Size(300, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRole.Items.AddRange(new object[] { "User", "Verified", "Moderator", "Admin" });
            cmbRole.SelectedIndex = 0;
            
            var btnChange = new Button
            {
                Text = "Thay đổi",
                Location = new Point(220, 95),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            
            btnChange.Click += (s, ev) =>
            {
                string newRole = cmbRole.SelectedItem?.ToString() ?? "User";
                int newRoleId = GetRoleId(newRole);
                
                foreach (var user in selectedUsers)
                {
                    user.Role = newRoleId;
                    userService.UpdateUser(user);
                }
                
                LoadUsers();
                MessageBox.Show($"Đã thay đổi vai trò cho {selectedUsers.Count} người dùng thành {newRole}!", 
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                form.Close();
            };
            
            form.Controls.AddRange(new Control[] { lblRole, cmbRole, btnChange });
            form.ShowDialog();
        }

        private void BtnBulkAction_Click(object? sender, EventArgs e)
        {
            var checkedUsers = GetCheckedUsers();
            if (checkedUsers.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Show bulk action menu
            var menu = new ContextMenuStrip();
            menu.Items.Add("Ban tất cả", null, (s, ev) => BulkBan(checkedUsers));
            menu.Items.Add("Unban tất cả", null, (s, ev) => BulkUnban(checkedUsers));
            menu.Items.Add("Gửi cảnh báo", null, (s, ev) => BulkWarning(checkedUsers));
            menu.Items.Add("Thay đổi role", null, (s, ev) => BulkChangeRole(checkedUsers));
            menu.Items.Add("Export danh sách", null, (s, ev) => BulkExport(checkedUsers));
            
            menu.Show(btnBulkAction, new Point(0, btnBulkAction.Height));
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    FileName = $"users_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var lines = new List<string>();
                    lines.Add("ID,Tên đăng nhập,Họ tên,Email,Vai trò,Trạng thái,Ngày tạo");
                    
                    foreach (var user in allUsers)
                    {
                        lines.Add($"{user.UserID},{user.UserName},{user.FullName},{user.Email},{GetRoleName(user.Role)},{user.AccountStatus},{user.CreatedAt:yyyy-MM-dd}");
                    }
                    
                    System.IO.File.WriteAllLines(saveDialog.FileName, lines);
                    MessageBox.Show($"Đã export {allUsers.Count} người dùng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi export: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Bulk Actions
        private List<User> GetCheckedUsers()
        {
            var users = new List<User>();
            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                if (row.Cells["Select"].Value is bool isChecked && isChecked && row.Tag is User user)
                {
                    users.Add(user);
                }
            }
            return users;
        }

        private int GetCheckedUsersCount()
        {
            int count = 0;
            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                if (row.Cells["Select"].Value is bool isChecked && isChecked)
                {
                    count++;
                }
            }
            return count;
        }

        private void BulkBan(List<User> users)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc muốn ban {users.Count} người dùng?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                foreach (var user in users)
                {
                    user.StatusId = -1; // Ban user
                    userService.UpdateUser(user);
                }
                LoadUsers();
                MessageBox.Show($"Đã ban {users.Count} người dùng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BulkUnban(List<User> users)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc muốn unban {users.Count} người dùng?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                foreach (var user in users)
                {
                    user.ReportCount = 0;
                    userService.UpdateUser(user);
                }
                LoadUsers();
                MessageBox.Show($"Đã unban {users.Count} người dùng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BulkWarning(List<User> users)
        {
            // Similar to BtnWarning_Click but for multiple users
            MessageBox.Show($"Chức năng gửi cảnh báo hàng loạt cho {users.Count} người dùng!", 
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BulkChangeRole(List<User> users)
        {
            // Similar to BtnChangeRole_Click but for multiple users
            MessageBox.Show($"Chức năng thay đổi role hàng loạt cho {users.Count} người dùng!", 
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BulkExport(List<User> users)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"selected_users_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var lines = new List<string>();
                    lines.Add("ID,Tên đăng nhập,Họ tên,Email,Vai trò,Trạng thái,Ngày tạo");
                    
                    foreach (var user in users)
                    {
                        lines.Add($"{user.UserID},{user.UserName},{user.FullName},{user.Email},{GetRoleName(user.Role)},{user.AccountStatus},{user.CreatedAt:yyyy-MM-dd}");
                    }
                    
                    System.IO.File.WriteAllLines(saveDialog.FileName, lines);
                    MessageBox.Show($"Đã export {users.Count} người dùng!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi export: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Helper Methods
        private Button CreateIconButton(string text, string tooltip, Point location)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(100, 35),
                Location = location,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Cursor = Cursors.Hand
            };
            
            btn.FlatAppearance.BorderSize = 0;
            
            var toolTip = new ToolTip();
            toolTip.SetToolTip(btn, tooltip);
            
            return btn;
        }

        private Button CreateActionButton(string text, Color color)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(120, 35),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5F),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 3, 0)
            };
            
            btn.FlatAppearance.BorderSize = 0;
            
            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Dark(color, 0.1f);
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

        private string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return "?";
            
            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                return $"{parts[0][0]}{parts[parts.Length - 1][0]}".ToUpper();
            }
            return fullName[0].ToString().ToUpper();
        }

        private Color GetStatusColor(string status)
        {
            return status switch
            {
                "Hoạt động" => Color.FromArgb(39, 174, 96),
                "Bị cấm" => Color.FromArgb(192, 57, 43),
                "Chờ duyệt" => Color.FromArgb(243, 156, 18),
                "Không hoạt động" => Color.FromArgb(149, 165, 166),
                _ => Color.FromArgb(127, 140, 141)
            };
        }

        private Color GetAccountStatusColor(string status)
        {
            return status switch
            {
                "Bình thường" => Color.FromArgb(39, 174, 96),
                "Cảnh báo" => Color.FromArgb(243, 156, 18),
                "Tạm ngừng" => Color.FromArgb(192, 57, 43),
                _ => Color.FromArgb(127, 140, 141)
            };
        }

        private Color GetRoleColor(string role)
        {
            return role switch
            {
                "Quản trị viên" => Color.FromArgb(192, 57, 43),
                "Kiểm duyệt viên" => Color.FromArgb(155, 89, 182),
                "Đã xác minh" => Color.FromArgb(52, 152, 219),
                "Người dùng" => Color.FromArgb(52, 73, 94),
                _ => Color.FromArgb(127, 140, 141)
            };
        }

        private Color GetRoleColorByInt(int role)
        {
            return role switch
            {
                2 => Color.FromArgb(192, 57, 43),      // Admin
                1 => Color.FromArgb(155, 89, 182),      // Moderator
                0 => Color.FromArgb(52, 73, 94),        // User
                _ => Color.FromArgb(127, 140, 141)
            };
        }

        private string GetRoleName(int role)
        {
            return role switch
            {
                2 => "Quản trị viên",
                1 => "Kiểm duyệt viên",
                0 => "Người dùng",
                _ => "Không xác định"
            };
        }

        private void DgvUsers_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0) return;

            var column = dgvUsers.Columns[e.ColumnIndex];
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

            var currentUsers = new List<User>();
            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                if (row.Tag is User user)
                    currentUsers.Add(user);
            }

            switch (columnName)
            {
                case "CreatedAt":
                    currentUsers = isAscending 
                        ? currentUsers.OrderBy(u => u.CreatedAt).ToList()
                        : currentUsers.OrderByDescending(u => u.CreatedAt).ToList();
                    break;
                case "UserName":
                    currentUsers = isAscending 
                        ? currentUsers.OrderBy(u => u.UserName).ToList()
                        : currentUsers.OrderByDescending(u => u.UserName).ToList();
                    break;
                case "FullName":
                    currentUsers = isAscending 
                        ? currentUsers.OrderBy(u => u.FullName).ToList()
                        : currentUsers.OrderByDescending(u => u.FullName).ToList();
                    break;
                case "Email":
                    currentUsers = isAscending 
                        ? currentUsers.OrderBy(u => u.Email).ToList()
                        : currentUsers.OrderByDescending(u => u.Email).ToList();
                    break;
                case "Role":
                    currentUsers = isAscending 
                        ? currentUsers.OrderBy(u => u.Role).ToList()
                        : currentUsers.OrderByDescending(u => u.Role).ToList();
                    break;
                case "Status":
                    currentUsers = isAscending 
                        ? currentUsers.OrderBy(u => u.AccountStatus).ToList()
                        : currentUsers.OrderByDescending(u => u.AccountStatus).ToList();
                    break;
                default:
                    return;
            }

            foreach (DataGridViewColumn col in dgvUsers.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            column.HeaderCell.SortGlyphDirection = isAscending ? SortOrder.Ascending : SortOrder.Descending;
            DisplayUsers(currentUsers);
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
            totalPages = Math.Max(1, (int)Math.Ceiling((double)filteredUsers.Count / ItemsPerPage));
            if (currentPage > totalPages) currentPage = totalPages;
            
            btnFirstPage.Enabled = btnPrevPage.Enabled = currentPage > 1;
            btnNextPage.Enabled = btnLastPage.Enabled = currentPage < totalPages;
            lblPageInfo.Text = $"Trang {currentPage} / {totalPages} ({filteredUsers.Count} người dùng)";
        }
        
        private List<User> GetCurrentPageUsers()
        {
            int skip = (currentPage - 1) * ItemsPerPage;
            return filteredUsers.Skip(skip).Take(ItemsPerPage).ToList();
        }
        
        private void RefreshCurrentPage()
        {
            CalculatePagination();
            
            dgvUsers.SelectionChanged -= DgvUsers_SelectionChanged;
            dgvUsers.Rows.Clear();
            
            var pagedUsers = GetCurrentPageUsers();
            foreach (var user in pagedUsers)
            {
                int rowIndex = dgvUsers.Rows.Add();
                var row = dgvUsers.Rows[rowIndex];
                
                row.Cells["Select"].Value = false;
                row.Cells["UserID"].Value = user.UserID;
                row.Cells["UserName"].Value = user.UserName;
                row.Cells["FullName"].Value = user.FullName;
                row.Cells["Email"].Value = user.Email;
                row.Cells["Role"].Value = GetRoleName(user.Role);
                row.Cells["Status"].Value = user.AccountStatus;
                row.Cells["CreatedAt"].Value = user.CreatedAt.ToString("dd/MM/yyyy");
                row.Cells["CreatedAt"].Tag = user.CreatedAt;
                row.Tag = user;
            }
            
            dgvUsers.SelectionChanged += DgvUsers_SelectionChanged;
            lblTotalUsers.Text = $"Tổng: {filteredUsers.Count} người dùng";
        }

        private int GetRoleId(string roleName)
        {
            return roleName switch
            {
                "Quản trị viên" => 2,
                "Kiểm duyệt viên" => 1,
                "Đã xác minh" => 1,
                "Người dùng" => 0,
                _ => 0
            };
        }
        #endregion
    }
}
