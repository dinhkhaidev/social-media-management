using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocialManager.services;
using SocialManager.constants;

namespace SocialManager.frm.UserControls
{
    public partial class ucUserManagement : UserControl
    {
        private UserService userService;
        private List<User> allUsers;
        
        // Sorting state
        private string? lastSortedColumn;
        private bool isAscending = true;
        
        // Controls
        private DataGridView dgvUsers;
        private TextBox txtSearch;
        private ComboBox cmbRoleFilter;
        private ComboBox cmbStatusFilter;
        private Button btnAddUser;
        private Button btnEditUser;
        private Button btnDeleteUser;
        private Button btnChangeRole;
        private Button btnToggleStatus;
        private Button btnRefresh;
        private Label lblUserCount;
        
        // User editing form
        private Panel pnlUserForm;
        private TextBox txtUserName;
        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private ComboBox cmbGender;
        private ComboBox cmbRole;
        private ComboBox cmbStatus;
        private DateTimePicker dtpDOB;
        private TextBox txtAddress;
        private Button btnSaveUser;
        private Button btnCancelEdit;

        public ucUserManagement()
        {
            InitializeComponent();
            InitializeUserManagement();
            CreateUserManagementLayout();
            LoadUsers();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // ucUserManagement
            // 
            BackColor = Color.FromArgb(247, 249, 252);
            Name = "ucUserManagement";
            Size = new Size(1019, 539);
            ResumeLayout(false);
        }

        private void InitializeUserManagement()
        {
            userService = new UserService();
            allUsers = new List<User>();
        }

        private void CreateUserManagementLayout()
        {
            // Main container
            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 2,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(247, 249, 252)
            };
            
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // Header
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80)); // Controls
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Content
            
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70)); // User list
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30)); // User form
            
            // Header
            var lblHeader = new Label
            {
                Text = "Quản Lý Người Dùng",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            lblUserCount = new Label
            {
                Text = "Tổng: 0 người dùng",
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(127, 140, 141),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight
            };
            
            // Controls panel
            var controlsPanel = CreateControlsPanel();
            var actionPanel = CreateActionPanel();
            
            // Content panels
            var userListPanel = CreateUserListPanel();
            pnlUserForm = CreateUserFormPanel();
            
            mainPanel.Controls.Add(lblHeader, 0, 0);
            mainPanel.Controls.Add(lblUserCount, 1, 0);
            mainPanel.Controls.Add(controlsPanel, 0, 1);
            mainPanel.Controls.Add(actionPanel, 1, 1);
            mainPanel.Controls.Add(userListPanel, 0, 2);
            mainPanel.Controls.Add(pnlUserForm, 1, 2);
            
            this.Controls.Add(mainPanel);
        }

        private Panel CreateControlsPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 4,
                BackColor = Color.Transparent
            };
            
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            
            // Search box
            txtSearch = new TextBox
            {
                PlaceholderText = "Tìm kiếm người dùng...",
                Font = new Font("Segoe UI", 11F),
                Margin = new Padding(5),
                Dock = DockStyle.Fill
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            
            // Role filter
            cmbRoleFilter = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                Margin = new Padding(5),
                Dock = DockStyle.Fill
            };
            cmbRoleFilter.Items.AddRange(new string[] { "Tất cả quyền", "User", "Admin", "Moderator" });
            cmbRoleFilter.SelectedIndex = 0;
            cmbRoleFilter.SelectedIndexChanged += CmbRoleFilter_SelectedIndexChanged;
            
            // Status filter
            cmbStatusFilter = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                Margin = new Padding(5),
                Dock = DockStyle.Fill
            };
            cmbStatusFilter.Items.AddRange(new string[] { "Tất cả trạng thái", "Active", "Inactive", "Banned" });
            cmbStatusFilter.SelectedIndex = 0;
            cmbStatusFilter.SelectedIndexChanged += CmbStatusFilter_SelectedIndexChanged;
            
            // Refresh button
            btnRefresh = new Button
            {
                Text = "🔄 Làm mới",
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(5),
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;
            
            layout.Controls.Add(txtSearch, 0, 0);
            layout.Controls.Add(cmbRoleFilter, 1, 0);
            layout.Controls.Add(cmbStatusFilter, 2, 0);
            layout.Controls.Add(btnRefresh, 3, 0);
            
            panel.Controls.Add(layout);
            return panel;
        }

        private Panel CreateActionPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 2,
                BackColor = Color.Transparent
            };
            
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            
            // Action buttons
            btnAddUser = new Button
            {
                Text = "➕ Thêm",
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(2),
                Dock = DockStyle.Fill
            };
            btnAddUser.FlatAppearance.BorderSize = 0;
            btnAddUser.Click += BtnAddUser_Click;
            
            btnEditUser = new Button
            {
                Text = "✏️ Sửa",
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.FromArgb(243, 156, 18),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(2),
                Dock = DockStyle.Fill
            };
            btnEditUser.FlatAppearance.BorderSize = 0;
            btnEditUser.Click += BtnEditUser_Click;
            
            btnChangeRole = new Button
            {
                Text = "🔑 Quyền",
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(2),
                Dock = DockStyle.Fill
            };
            btnChangeRole.FlatAppearance.BorderSize = 0;
            btnChangeRole.Click += BtnChangeRole_Click;
            
            btnToggleStatus = new Button
            {
                Text = "🔒 Trạng thái",
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(2),
                Dock = DockStyle.Fill
            };
            btnToggleStatus.FlatAppearance.BorderSize = 0;
            btnToggleStatus.Click += BtnToggleStatus_Click;
            
            layout.Controls.Add(btnAddUser, 0, 0);
            layout.Controls.Add(btnEditUser, 1, 0);
            layout.Controls.Add(btnChangeRole, 0, 1);
            layout.Controls.Add(btnToggleStatus, 1, 1);
            
            panel.Controls.Add(layout);
            return panel;
        }

        private Panel CreateUserListPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 10, 10, 0),
                BackColor = Color.White
            };
            
            panel.Paint += (sender, e) => {
                DrawRoundedPanel(e.Graphics, panel.ClientRectangle);
            };
            
            dgvUsers = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(panel.Width - 40, panel.Height - 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9F),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            
            SetupUserDataGridView();
            
            panel.Controls.Add(dgvUsers);
            return panel;
        }

        private void SetupUserDataGridView()
        {
            dgvUsers.Columns.Clear();
            
            dgvUsers.Columns.Add("UserID", "ID");
            dgvUsers.Columns.Add("UserName", "Tên đăng nhập");
            dgvUsers.Columns.Add("FullName", "Họ tên");
            dgvUsers.Columns.Add("Email", "Email");
            dgvUsers.Columns.Add("Role", "Quyền");
            dgvUsers.Columns.Add("Status", "Trạng thái");
            dgvUsers.Columns.Add("CreatedAt", "Ngày tạo");
            
            // Hide ID column
            dgvUsers.Columns["UserID"].Visible = false;
            
            // Set column widths
            dgvUsers.Columns["UserName"].Width = 120;
            dgvUsers.Columns["FullName"].Width = 150;
            dgvUsers.Columns["Email"].Width = 180;
            dgvUsers.Columns["Role"].Width = 80;
            dgvUsers.Columns["Status"].Width = 80;
            dgvUsers.Columns["CreatedAt"].Width = 120;
            
            // Style the DataGridView
            dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvUsers.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvUsers.DefaultCellStyle.BackColor = Color.White;
            dgvUsers.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvUsers.DefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvUsers.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 10, 8, 10);
            dgvUsers.ColumnHeadersHeight = 40;
            dgvUsers.RowTemplate.Height = 45;
            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.GridColor = Color.FromArgb(234, 236, 238);
            
            dgvUsers.ColumnHeaderMouseClick += DgvUsers_ColumnHeaderMouseClick;
        }

        private Panel CreateUserFormPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10, 10, 0, 0),
                BackColor = Color.White,
                Visible = false
            };
            
            panel.Paint += (sender, e) => {
                DrawRoundedPanel(e.Graphics, panel.ClientRectangle);
            };
            
            // Create form controls
            var lblFormHeader = new Label
            {
                Text = "Thông Tin Người Dùng",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 15),
                Size = new Size(250, 30)
            };
            
            int yPos = 60;
            int spacing = 50;
            
            // User name
            var lblUserName = new Label { Text = "Tên đăng nhập:", Location = new Point(20, yPos), Size = new Size(100, 20) };
            txtUserName = new TextBox { Location = new Point(20, yPos + 20), Size = new Size(200, 25) };
            yPos += spacing;
            
            // Full name
            var lblFullName = new Label { Text = "Họ tên:", Location = new Point(20, yPos), Size = new Size(100, 20) };
            txtFullName = new TextBox { Location = new Point(20, yPos + 20), Size = new Size(200, 25) };
            yPos += spacing;
            
            // Email
            var lblEmail = new Label { Text = "Email:", Location = new Point(20, yPos), Size = new Size(100, 20) };
            txtEmail = new TextBox { Location = new Point(20, yPos + 20), Size = new Size(200, 25) };
            yPos += spacing;
            
            // Phone
            var lblPhone = new Label { Text = "Điện thoại:", Location = new Point(20, yPos), Size = new Size(100, 20) };
            txtPhone = new TextBox { Location = new Point(20, yPos + 20), Size = new Size(200, 25) };
            yPos += spacing;
            
            // Gender
            var lblGender = new Label { Text = "Giới tính:", Location = new Point(20, yPos), Size = new Size(100, 20) };
            cmbGender = new ComboBox 
            { 
                Location = new Point(20, yPos + 20), 
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbGender.Items.AddRange(new string[] { "Nam", "Nữ" });
            yPos += spacing;
            
            // Role
            var lblRole = new Label { Text = "Quyền:", Location = new Point(20, yPos), Size = new Size(100, 20) };
            cmbRole = new ComboBox 
            { 
                Location = new Point(20, yPos + 20), 
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRole.Items.AddRange(new string[] { "User", "Admin", "Moderator" });
            yPos += spacing;
            
            // Status
            var lblStatus = new Label { Text = "Trạng thái:", Location = new Point(20, yPos), Size = new Size(100, 20) };
            cmbStatus = new ComboBox 
            { 
                Location = new Point(20, yPos + 20), 
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatus.Items.AddRange(new string[] { "Inactive", "Active", "Banned" });
            yPos += spacing;
            
            // Buttons
            btnSaveUser = new Button
            {
                Text = "💾 Lưu",
                Location = new Point(20, yPos + 20),
                Size = new Size(95, 35),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSaveUser.FlatAppearance.BorderSize = 0;
            btnSaveUser.Click += BtnSaveUser_Click;
            
            btnCancelEdit = new Button
            {
                Text = "❌ Hủy",
                Location = new Point(125, yPos + 20),
                Size = new Size(95, 35),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancelEdit.FlatAppearance.BorderSize = 0;
            btnCancelEdit.Click += BtnCancelEdit_Click;
            
            // Add all controls to panel
            panel.Controls.AddRange(new Control[] {
                lblFormHeader, lblUserName, txtUserName, lblFullName, txtFullName,
                lblEmail, txtEmail, lblPhone, txtPhone, lblGender, cmbGender,
                lblRole, cmbRole, lblStatus, cmbStatus, btnSaveUser, btnCancelEdit
            });
            
            return panel;
        }

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
                MessageBox.Show($"Lỗi khi tải danh sách người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayUsers(List<User> users)
        {
            dgvUsers.Rows.Clear();
            
            foreach (var user in users)
            {
                var row = dgvUsers.Rows.Add(
                    user.UserID.ToString(),
                    user.UserName,
                    user.FullName,
                    user.Email,
                    UserConstant.GetRoleText(user.Role),
                    UserConstant.GetStatusText(user.StatusId),
                    user.CreatedAt.ToString("dd/MM/yyyy")
                );
                
                // Store DateTime for sorting
                dgvUsers.Rows[row].Cells["CreatedAt"].Tag = user.CreatedAt;
                
                // Color coding based on status
                if (user.StatusId == 1) // Active
                    dgvUsers.Rows[row].DefaultCellStyle.ForeColor = Color.FromArgb(46, 204, 113);
                else if (user.StatusId == -1) // Banned
                    dgvUsers.Rows[row].DefaultCellStyle.ForeColor = Color.FromArgb(231, 76, 60);
                else // Inactive
                    dgvUsers.Rows[row].DefaultCellStyle.ForeColor = Color.FromArgb(149, 165, 166);
            }
        }

        private void UpdateUserCount()
        {
            lblUserCount.Text = $"Tổng: {allUsers.Count} người dùng";
        }

        #region Event Handlers

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchUsers(txtSearch.Text);
        }

        private void CmbRoleFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterUsers();
        }

        private void CmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterUsers();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
            MessageBox.Show("Danh sách người dùng đã được làm mới!", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            ClearUserForm();
            pnlUserForm.Visible = true;
            pnlUserForm.Controls[0].Text = "Thêm Người Dùng Mới";
            txtUserName.Tag = null; // No user ID means new user
        }

        private void BtnEditUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để chỉnh sửa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var selectedUserId = Guid.Parse(dgvUsers.SelectedRows[0].Cells["UserID"].Value.ToString());
            var user = allUsers.FirstOrDefault(u => u.UserID == selectedUserId);
            
            if (user != null)
            {
                LoadUserToForm(user);
                pnlUserForm.Visible = true;
                pnlUserForm.Controls[0].Text = "Chỉnh Sửa Người Dùng";
            }
        }

        private void BtnChangeRole_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để thay đổi quyền.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var selectedUserId = Guid.Parse(dgvUsers.SelectedRows[0].Cells["UserID"].Value.ToString());
            var user = allUsers.FirstOrDefault(u => u.UserID == selectedUserId);
            
            if (user != null)
            {
                var roleForm = new Form
                {
                    Text = "Thay Đổi Quyền",
                    Size = new Size(300, 150),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };
                
                var lblRole = new Label { Text = $"Quyền hiện tại: {UserConstant.GetRoleText(user.Role)}", Location = new Point(20, 20), Size = new Size(250, 20) };
                var cmbNewRole = new ComboBox 
                { 
                    Location = new Point(20, 50), 
                    Size = new Size(200, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cmbNewRole.Items.AddRange(new string[] { "User", "Admin", "Moderator" });
                cmbNewRole.SelectedIndex = user.Role;
                
                var btnOK = new Button
                {
                    Text = "OK",
                    Location = new Point(120, 80),
                    Size = new Size(75, 25),
                    DialogResult = DialogResult.OK
                };
                
                var btnCancel = new Button
                {
                    Text = "Hủy",
                    Location = new Point(200, 80),
                    Size = new Size(75, 25),
                    DialogResult = DialogResult.Cancel
                };
                
                roleForm.Controls.AddRange(new Control[] { lblRole, cmbNewRole, btnOK, btnCancel });
                
                if (roleForm.ShowDialog() == DialogResult.OK)
                {
                    user.Role = cmbNewRole.SelectedIndex;
                    if (userService.UpdateUser(user))
                    {
                        LoadUsers();
                        MessageBox.Show("Quyền người dùng đã được cập nhật!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi cập nhật quyền người dùng.", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnToggleStatus_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để thay đổi trạng thái.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var selectedUserId = Guid.Parse(dgvUsers.SelectedRows[0].Cells["UserID"].Value.ToString());
            var user = allUsers.FirstOrDefault(u => u.UserID == selectedUserId);
            
            if (user != null)
            {
                string currentStatus = UserConstant.GetStatusText(user.StatusId);
                string newStatus = user.StatusId == 1 ? "Inactive" : "Active";
                
                var result = MessageBox.Show($"Thay đổi trạng thái từ '{currentStatus}' sang '{newStatus}'?", 
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    user.StatusId = user.StatusId == 1 ? 0 : 1;
                    if (userService.UpdateUser(user))
                    {
                        LoadUsers();
                        MessageBox.Show("Trạng thái người dùng đã được cập nhật!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi cập nhật trạng thái người dùng.", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnSaveUser_Click(object sender, EventArgs e)
        {
            if (!ValidateUserForm())
                return;
            
            try
            {
                bool isNewUser = txtUserName.Tag == null;
                User user;
                
                if (isNewUser)
                {
                    user = new User
                    {
                        UserID = Guid.NewGuid(),
                        CreatedAt = DateTime.Now,
                        Password = "123456" // Default password
                    };
                }
                else
                {
                    var userId = (Guid)txtUserName.Tag;
                    user = allUsers.FirstOrDefault(u => u.UserID == userId);
                    if (user == null)
                    {
                        MessageBox.Show("Không tìm thấy người dùng.", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                
                // Update user properties
                user.UserName = txtUserName.Text.Trim();
                user.FullName = txtFullName.Text.Trim();
                user.Email = txtEmail.Text.Trim();
                user.Phone = txtPhone.Text.Trim();
                user.Gender = cmbGender.SelectedIndex;
                user.Role = cmbRole.SelectedIndex;
                user.StatusId = cmbStatus.SelectedIndex == 2 ? -1 : cmbStatus.SelectedIndex;
                
                bool success;
                if (isNewUser)
                {
                    //success = user.Save(userService.GetUserFilePath());
                    // Replace this block in BtnSaveUser_Click event handler:
                    // success = user.Save(userService.GetUserFilePath());

                    // With this:
                    success = userService.CreateUser(
                        user.UserName,
                        user.Password,
                        user.FullName,
                        user.Email,
                        user.Phone,
                        user.Gender,
                        user.DOB,
                        user.Address,
                        user.Bio,
                        user.AvatarUrl
                    );
                }
                else
                {
                    success = userService.UpdateUser(user);
                }
                
                if (success)
                {
                    MessageBox.Show(isNewUser ? "Người dùng mới đã được tạo thành công!" : "Thông tin người dùng đã được cập nhật!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    pnlUserForm.Visible = false;
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Lỗi khi lưu thông tin người dùng.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelEdit_Click(object sender, EventArgs e)
        {
            pnlUserForm.Visible = false;
        }

        #endregion

        #region Helper Methods

        private void SearchUsers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                FilterUsers();
                return;
            }
            
            var filteredUsers = allUsers.Where(u =>
                u.UserName.ToLower().Contains(searchTerm.ToLower()) ||
                u.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                u.Email.ToLower().Contains(searchTerm.ToLower())
            ).ToList();
            
            DisplayUsers(filteredUsers);
        }

        private void FilterUsers()
        {
            var filteredUsers = allUsers.AsEnumerable();
            
            // Filter by role
            if (cmbRoleFilter.SelectedIndex > 0)
            {
                int roleFilter = cmbRoleFilter.SelectedIndex - 1;
                filteredUsers = filteredUsers.Where(u => u.Role == roleFilter);
            }
            
            // Filter by status
            if (cmbStatusFilter.SelectedIndex > 0)
            {
                int statusFilter = cmbStatusFilter.SelectedIndex == 3 ? -1 : cmbStatusFilter.SelectedIndex - 1;
                filteredUsers = filteredUsers.Where(u => u.StatusId == statusFilter);
            }
            
            DisplayUsers(filteredUsers.ToList());
        }

        private void LoadUserToForm(User user)
        {
            txtUserName.Text = user.UserName;
            txtUserName.Tag = user.UserID;
            txtFullName.Text = user.FullName;
            txtEmail.Text = user.Email;
            txtPhone.Text = user.Phone;
            cmbGender.SelectedIndex = user.Gender;
            cmbRole.SelectedIndex = user.Role;
            cmbStatus.SelectedIndex = user.StatusId == -1 ? 2 : user.StatusId;
        }

        private void ClearUserForm()
        {
            txtUserName.Clear();
            txtUserName.Tag = null;
            txtFullName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            cmbGender.SelectedIndex = 0;
            cmbRole.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 1; // Active by default
        }

        private bool ValidateUserForm()
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Lỗi xác thực",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Lỗi xác thực",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }
            
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
                    if (addr.Address != txtEmail.Text)
                        throw new Exception();
                }
                catch
                {
                    MessageBox.Show("Email không hợp lệ.", "Lỗi xác thực",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }
            
            // Check for duplicate username (only for new users or changed username)
            bool isNewUser = txtUserName.Tag == null;
            if (isNewUser || txtUserName.Text != allUsers.FirstOrDefault(u => u.UserID == (Guid)txtUserName.Tag)?.UserName)
            {
                if (allUsers.Any(u => u.UserName.Equals(txtUserName.Text, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại.", "Lỗi xác thực",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUserName.Focus();
                    return false;
                }
            }
            
            return true;
        }

        private void DrawRoundedPanel(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            // Draw shadow
            using (var shadowBrush = new SolidBrush(Color.FromArgb(15, 0, 0, 0)))
            {
                var shadowRect = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 2, rect.Height - 2);
                var shadowPath = CreateRoundedRectangle(shadowRect, 8);
                g.FillPath(shadowBrush, shadowPath);
            }
            
            // Draw panel background
            using (var panelBrush = new SolidBrush(Color.White))
            {
                var panelRect = new Rectangle(rect.X, rect.Y, rect.Width - 2, rect.Height - 2);
                var panelPath = CreateRoundedRectangle(panelRect, 8);
                g.FillPath(panelBrush, panelPath);
            }
            
            // Draw border
            using (var borderPen = new Pen(Color.FromArgb(240, 240, 240), 1))
            {
                var borderRect = new Rectangle(rect.X, rect.Y, rect.Width - 3, rect.Height - 3);
                var borderPath = CreateRoundedRectangle(borderRect, 8);
                g.DrawPath(borderPen, borderPath);
            }
        }

        private GraphicsPath CreateRoundedRectangle(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseAllFigures();
            return path;
        }

        #endregion

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
                var userId = Guid.Parse(row.Cells["UserID"].Value.ToString());
                var user = allUsers.FirstOrDefault(u => u.UserID == userId);
                if (user != null)
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
                        ? currentUsers.OrderBy(u => u.StatusId).ToList()
                        : currentUsers.OrderByDescending(u => u.StatusId).ToList();
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

        public void RefreshData()
        {
            LoadUsers();
        }
    }
}