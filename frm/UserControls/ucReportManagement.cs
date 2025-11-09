using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SocialManager.models;
using SocialManager.services;
using SocialManager.constants;
using SocialManager.controls;

namespace SocialManager.frm.UserControls
{
  public class ucReportManagement : UserControl
  {
    #region Fields
    private readonly ReportService reportService;
    private readonly UserService userService;
    private readonly PostService postService;
    private readonly CommentService commentService;

    private DataGridView dgvReports = null!;
    private TextBox txtSearch = null!;
    private ComboBox cmbStatusFilter = null!;
    private ComboBox cmbTypeFilter = null!;
    private DateTimePicker dtpFromDate = null!;
    private DateTimePicker dtpToDate = null!;

    private RoundedButton btnRefresh = null!;
    private RoundedButton btnMarkReviewed = null!;
    private RoundedButton btnHideContent = null!;
    private RoundedButton btnBanUser = null!;
    private RoundedButton btnReject = null!;
    private RoundedButton btnExport = null!;

    private Panel pnlDetailPanel = null!;
    private Label lblNoSelection = null!;
    private Report? currentReport;

    // Material Design Colors
    private readonly Color PrimaryColor = Color.FromArgb(52, 152, 219);
    private readonly Color SuccessColor = Color.FromArgb(46, 204, 113);
    private readonly Color WarningColor = Color.FromArgb(243, 156, 18);
    private readonly Color DangerColor = Color.FromArgb(231, 76, 60);
    private readonly Color DarkColor = Color.FromArgb(44, 62, 80);
    #endregion

    #region Constructor
    public ucReportManagement()
    {
      reportService = new ReportService();
      userService = new UserService();
      postService = new PostService();
      commentService = new CommentService();

      InitializeComponent();
      LoadReports();
    }
    #endregion

    #region Initialize UI
    private void InitializeComponent()
    {
      this.SuspendLayout();

      // Main container
      this.Size = new Size(1200, 800);
      this.BackColor = Color.FromArgb(236, 240, 241);
      this.Padding = new Padding(20);

      // Create main layout
      var mainLayout = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 1,
        BackColor = Color.Transparent
      };
      mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
      mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

      // Left panel (List)
      var leftPanel = CreateLeftPanel();
      mainLayout.Controls.Add(leftPanel, 0, 0);

      // Right panel (Detail)
      var rightPanel = CreateRightPanel();
      mainLayout.Controls.Add(rightPanel, 1, 0);

      this.Controls.Add(mainLayout);
      this.ResumeLayout(false);
    }

    private Panel CreateLeftPanel()
    {
      var panel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        Padding = new Padding(0, 0, 10, 0)
      };

      var layout = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        RowCount = 4,
        ColumnCount = 1,
        BackColor = Color.Transparent
      };
      layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));  // Header
      layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F)); // Search & Filter (tăng lên)
      layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));  // Actions
      layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));  // DataGrid

      // Header
      var header = CreateHeader();
      layout.Controls.Add(header, 0, 0);

      // Search & Filter
      var searchPanel = CreateSearchFilterPanel();
      layout.Controls.Add(searchPanel, 0, 1);

      // Actions
      var actionsPanel = CreateActionsPanel();
      layout.Controls.Add(actionsPanel, 0, 2);

      // DataGrid
      var gridPanel = CreateDataGridPanel();
      layout.Controls.Add(gridPanel, 0, 3);

      panel.Controls.Add(layout);
      return panel;
    }

    private Panel CreateHeader()
    {
      var panel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.Transparent,
        Padding = new Padding(15, 10, 15, 10)
      };

      var lblTitle = new Label
      {
        Text = "🚨 QUẢN LÝ BÁO CÁO VI PHẠM",
        Font = new Font("Segoe UI", 16F, FontStyle.Bold),
        ForeColor = DarkColor,
        AutoSize = true,
        Location = new Point(15, 15)
      };

      panel.Controls.Add(lblTitle);
      return panel;
    }

    private Panel CreateSearchFilterPanel()
    {
      var panel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.FromArgb(247, 249, 252),
        Padding = new Padding(15, 10, 15, 10)
      };

      var mainLayout = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        RowCount = 3,
        ColumnCount = 4,
        BackColor = Color.Transparent
      };

      // Row 1: Search
      mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
      // Row 2: Status & Type
      mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
      // Row 3: Date filters
      mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));

      // Column widths
      mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F)); // Label column
      mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));   // Value column 1
      mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F)); // Label column 2
      mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));   // Value column 2

      // ROW 1: Search box
      var lblSearch = new Label
      {
        Text = "🔍 Tìm kiếm:",
        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
        AutoSize = false,
        TextAlign = ContentAlignment.MiddleLeft,
        Dock = DockStyle.Fill
      };

      txtSearch = new TextBox
      {
        Font = new Font("Segoe UI", 9.5F),
        Dock = DockStyle.Fill,
        PlaceholderText = "Người báo cáo, người bị báo cáo, lý do..."
      };
      txtSearch.TextChanged += (s, e) => ApplyFilters();

      mainLayout.Controls.Add(lblSearch, 0, 0);
      mainLayout.Controls.Add(txtSearch, 1, 0);
      mainLayout.SetColumnSpan(txtSearch, 3); // Span across remaining columns

      // ROW 2: Status & Type filters
      var lblStatus = new Label
      {
        Text = "📊 Trạng thái:",
        Font = new Font("Segoe UI", 9F),
        AutoSize = false,
        TextAlign = ContentAlignment.MiddleLeft,
        Dock = DockStyle.Fill
      };

      cmbStatusFilter = new ComboBox
      {
        Font = new Font("Segoe UI", 9F),
        Dock = DockStyle.Fill,
        DropDownStyle = ComboBoxStyle.DropDownList
      };
      cmbStatusFilter.Items.AddRange(new[] { "Tất cả", "Pending Action", "In Review", "Resolved", "Escalated", "Rejected" });
      cmbStatusFilter.SelectedIndex = 0;
      cmbStatusFilter.SelectedIndexChanged += (s, e) => ApplyFilters();

      var lblType = new Label
      {
        Text = "📌 Loại:",
        Font = new Font("Segoe UI", 9F),
        AutoSize = false,
        TextAlign = ContentAlignment.MiddleLeft,
        Dock = DockStyle.Fill
      };

      cmbTypeFilter = new ComboBox
      {
        Font = new Font("Segoe UI", 9F),
        Dock = DockStyle.Fill,
        DropDownStyle = ComboBoxStyle.DropDownList
      };
      cmbTypeFilter.Items.AddRange(new[] { "Tất cả", "Post", "Comment" });
      cmbTypeFilter.SelectedIndex = 0;
      cmbTypeFilter.SelectedIndexChanged += (s, e) => ApplyFilters();

      mainLayout.Controls.Add(lblStatus, 0, 1);
      mainLayout.Controls.Add(cmbStatusFilter, 1, 1);
      mainLayout.Controls.Add(lblType, 2, 1);
      mainLayout.Controls.Add(cmbTypeFilter, 3, 1);

      // ROW 3: Date filters
      var lblFromDate = new Label
      {
        Text = "📅 Từ ngày:",
        Font = new Font("Segoe UI", 9F),
        AutoSize = false,
        TextAlign = ContentAlignment.MiddleLeft,
        Dock = DockStyle.Fill
      };

      dtpFromDate = new DateTimePicker
      {
        Font = new Font("Segoe UI", 9F),
        Dock = DockStyle.Fill,
        Format = DateTimePickerFormat.Short
      };
      dtpFromDate.Value = DateTime.Now.AddMonths(-1);
      dtpFromDate.ValueChanged += (s, e) => ApplyFilters();

      var lblToDate = new Label
      {
        Text = "📅 Đến ngày:",
        Font = new Font("Segoe UI", 9F),
        AutoSize = false,
        TextAlign = ContentAlignment.MiddleLeft,
        Dock = DockStyle.Fill
      };

      dtpToDate = new DateTimePicker
      {
        Font = new Font("Segoe UI", 9F),
        Dock = DockStyle.Fill,
        Format = DateTimePickerFormat.Short
      };
      dtpToDate.ValueChanged += (s, e) => ApplyFilters();

      mainLayout.Controls.Add(lblFromDate, 0, 2);
      mainLayout.Controls.Add(dtpFromDate, 1, 2);
      mainLayout.Controls.Add(lblToDate, 2, 2);
      mainLayout.Controls.Add(dtpToDate, 3, 2);

      panel.Controls.Add(mainLayout);
      return panel;
    }

    private Panel CreateActionsPanel()
    {
      var panel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        Padding = new Padding(15, 5, 15, 5)
      };

      var flowPanel = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.LeftToRight,
        WrapContents = false,
        BackColor = Color.Transparent
      };

      btnRefresh = CreateActionButton("🔄 Làm mới", PrimaryColor);
      btnRefresh.Click += BtnRefresh_Click;

      btnMarkReviewed = CreateActionButton("✅ Đánh dấu đã xem", SuccessColor);
      btnMarkReviewed.Click += BtnMarkReviewed_Click;

      btnHideContent = CreateActionButton("👁️ Ẩn nội dung", WarningColor);
      btnHideContent.Click += BtnHideContent_Click;

      btnBanUser = CreateActionButton("🚫 Ban người dùng", DangerColor);
      btnBanUser.Click += BtnBanUser_Click;

      btnReject = CreateActionButton("❌ Từ chối", Color.FromArgb(149, 165, 166));
      btnReject.Click += BtnReject_Click;

      btnExport = CreateActionButton("📊 Xuất Excel", Color.FromArgb(52, 73, 94));
      btnExport.Click += BtnExport_Click;

      flowPanel.Controls.AddRange(new Control[] {
                btnRefresh, btnMarkReviewed, btnHideContent, btnBanUser, btnReject, btnExport
            });

      panel.Controls.Add(flowPanel);
      return panel;
    }

    private RoundedButton CreateActionButton(string text, Color bgColor)
    {
      var btn = new RoundedButton
      {
        Text = text,
        Width = 120,
        Height = 35,
        FlatStyle = FlatStyle.Flat,
        BackColor = bgColor,
        BackgroundColor = bgColor,
        BorderRadius = 12,
        ForeColor = Color.White,
        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
        Cursor = Cursors.Hand,
        Margin = new Padding(0, 0, 10, 0)
      };
      btn.FlatAppearance.BorderSize = 0;

      // Hover effect
      btn.MouseEnter += (s, e) => { btn.BackColor = ControlPaint.Dark(bgColor, 0.1f); btn.BackgroundColor = btn.BackColor; };
      btn.MouseLeave += (s, e) => { btn.BackColor = bgColor; btn.BackgroundColor = bgColor; };

      return btn;
    }

    private Panel CreateDataGridPanel()
    {
      var panel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        Padding = new Padding(15, 0, 15, 15)
      };

      dgvReports = new DataGridView
      {
        Dock = DockStyle.Fill,
        BackgroundColor = Color.White,
        BorderStyle = BorderStyle.None,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        ReadOnly = true,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = true,
        AutoGenerateColumns = false,
        RowHeadersVisible = false,
        EnableHeadersVisualStyles = false,
        ColumnHeadersHeight = 40,
        RowTemplate = { Height = 35 },
        Font = new Font("Segoe UI", 9F)
      };

      // Column header style
      dgvReports.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
      {
        BackColor = Color.FromArgb(52, 73, 94),
        ForeColor = Color.White,
        Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
        Alignment = DataGridViewContentAlignment.MiddleLeft,
        Padding = new Padding(5)
      };

      // Row style
      dgvReports.DefaultCellStyle = new DataGridViewCellStyle
      {
        SelectionBackColor = Color.FromArgb(52, 152, 219),
        SelectionForeColor = Color.White,
        Padding = new Padding(5)
      };

      dgvReports.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
      {
        BackColor = Color.FromArgb(247, 249, 252)
      };

      // Columns
      dgvReports.Columns.AddRange(new DataGridViewColumn[]
      {
                new DataGridViewTextBoxColumn { Name = "ReportID", HeaderText = "ID", Width = 50, DataPropertyName = "ReportID" },
                new DataGridViewTextBoxColumn { Name = "ReportType", HeaderText = "Loại", Width = 80, DataPropertyName = "ReportType" },
                new DataGridViewTextBoxColumn { Name = "ContentID", HeaderText = "Nội dung ID", Width = 90, DataPropertyName = "ContentID" },
                new DataGridViewTextBoxColumn { Name = "Reporter", HeaderText = "Người báo cáo", Width = 140 },
                new DataGridViewTextBoxColumn { Name = "Reported", HeaderText = "Người bị báo cáo", Width = 140 },
                new DataGridViewTextBoxColumn { Name = "Reason", HeaderText = "Lý do", Width = 150, DataPropertyName = "Reason" },
                new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Trạng thái", Width = 100, DataPropertyName = "Status" },
                new DataGridViewTextBoxColumn { Name = "ReportedAt", HeaderText = "Thời gian", Width = 130 }
      });

      dgvReports.SelectionChanged += DgvReports_SelectionChanged;

      panel.Controls.Add(dgvReports);
      return panel;
    }

    private Panel CreateRightPanel()
    {
      pnlDetailPanel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        Padding = new Padding(0) // Không padding ở đây, để scroll panel tự xử lý
      };

      lblNoSelection = new Label
      {
        Text = "📋 Chọn một báo cáo để xem chi tiết",
        Font = new Font("Segoe UI", 11F),
        ForeColor = Color.FromArgb(127, 140, 141),
        TextAlign = ContentAlignment.MiddleCenter,
        Dock = DockStyle.Fill
      };

      pnlDetailPanel.Controls.Add(lblNoSelection);
      return pnlDetailPanel;
    }
    #endregion

    #region Load Data
    private void LoadReports()
    {
      try
      {
        var reports = reportService.GetAllReports();

        // Clear existing rows
        dgvReports.Rows.Clear();

        foreach (var report in reports.OrderByDescending(r => r.ReportedAt))
        {
          var reporter = userService.GetUserById(report.ReporterUserID);
          var reported = userService.GetUserById(report.ReportedUserID);

          var row = new DataGridViewRow();
          row.CreateCells(dgvReports);
          row.Cells[0].Value = report.ReportID;
          row.Cells[1].Value = report.ReportType;
          row.Cells[2].Value = report.ContentID;
          row.Cells[3].Value = reporter?.UserName ?? "Unknown";
          row.Cells[4].Value = reported?.UserName ?? "Unknown";
          row.Cells[5].Value = report.Reason;
          row.Cells[6].Value = report.Status;
          row.Cells[7].Value = report.ReportedAt.ToString("dd/MM/yyyy HH:mm");
          row.Tag = report;

          // Color coding by status
          switch (report.Status)
          {
            case "Pending Action":
              row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 152, 0);
              row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
              break;
            case "In Review":
              row.DefaultCellStyle.ForeColor = WarningColor;
              break;
            case "Resolved":
              row.DefaultCellStyle.ForeColor = SuccessColor;
              break;
            case "Escalated":
              row.DefaultCellStyle.ForeColor = Color.FromArgb(192, 57, 43);
              row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
              break;
            case "Rejected":
              row.DefaultCellStyle.ForeColor = Color.Gray;
              break;
          }

          dgvReports.Rows.Add(row);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi tải danh sách báo cáo: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ApplyFilters()
    {
      try
      {
        var reports = reportService.GetAllReports();

        // Filter by search text
        if (!string.IsNullOrWhiteSpace(txtSearch.Text))
        {
          var searchTerm = txtSearch.Text.ToLower();
          reports = reports.Where(r =>
          {
            var reporter = userService.GetUserById(r.ReporterUserID);
            var reported = userService.GetUserById(r.ReportedUserID);
            return (reporter?.UserName.ToLower().Contains(searchTerm) ?? false) ||
                             (reported?.UserName.ToLower().Contains(searchTerm) ?? false) ||
                             r.Reason.ToLower().Contains(searchTerm);
          }).ToList();
        }

        // Filter by status
        if (cmbStatusFilter.SelectedIndex > 0)
        {
          var status = cmbStatusFilter.SelectedItem as string;
          if (!string.IsNullOrEmpty(status))
          {
            reports = reports.Where(r => r.Status == status).ToList();
          }
        }

        // Filter by type
        if (cmbTypeFilter.SelectedIndex > 0)
        {
          var type = cmbTypeFilter.SelectedItem as string;
          if (!string.IsNullOrEmpty(type))
          {
            reports = reports.Where(r => r.ReportType == type).ToList();
          }
        }

        // Filter by date range
        reports = reports.Where(r =>
            r.ReportedAt >= dtpFromDate.Value.Date &&
            r.ReportedAt <= dtpToDate.Value.Date.AddDays(1).AddSeconds(-1)
        ).ToList();

        // Update grid
        dgvReports.Rows.Clear();

        foreach (var report in reports.OrderByDescending(r => r.ReportedAt))
        {
          var reporter = userService.GetUserById(report.ReporterUserID);
          var reported = userService.GetUserById(report.ReportedUserID);

          var row = new DataGridViewRow();
          row.CreateCells(dgvReports);
          row.Cells[0].Value = report.ReportID;
          row.Cells[1].Value = report.ReportType;
          row.Cells[2].Value = report.ContentID;
          row.Cells[3].Value = reporter?.UserName ?? "Unknown";
          row.Cells[4].Value = reported?.UserName ?? "Unknown";
          row.Cells[5].Value = report.Reason;
          row.Cells[6].Value = report.Status;
          row.Cells[7].Value = report.ReportedAt.ToString("dd/MM/yyyy HH:mm");
          row.Tag = report;

          // Color coding
          switch (report.Status)
          {
            case "Pending Action":
              row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 152, 0);
              row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
              break;
            case "In Review":
              row.DefaultCellStyle.ForeColor = WarningColor;
              break;
            case "Resolved":
              row.DefaultCellStyle.ForeColor = SuccessColor;
              break;
            case "Escalated":
              row.DefaultCellStyle.ForeColor = Color.FromArgb(192, 57, 43);
              row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
              break;
            case "Rejected":
              row.DefaultCellStyle.ForeColor = Color.Gray;
              break;
          }

          dgvReports.Rows.Add(row);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
    #endregion

    #region Detail Panel
    private void DgvReports_SelectionChanged(object? sender, EventArgs e)
    {
      if (dgvReports.SelectedRows.Count == 0)
      {
        ShowNoSelection();
        return;
      }

      var selectedRow = dgvReports.SelectedRows[0];
      var report = selectedRow.Tag as Report;

      if (report != null)
      {
        currentReport = report;
        LoadReportDetail(report);
      }
    }

    private void ShowNoSelection()
    {
      pnlDetailPanel.Controls.Clear();
      pnlDetailPanel.Controls.Add(lblNoSelection);
      currentReport = null;
    }

    private void LoadReportDetail(Report report)
    {
      pnlDetailPanel.Controls.Clear();

      // Create scrollable container
      var scrollPanel = new Panel
      {
        Dock = DockStyle.Fill,
        AutoScroll = true,
        BackColor = Color.Transparent,
        Padding = new Padding(5, 5, 5, 5) // Padding đều xung quanh
      };

      var detailLayout = new TableLayoutPanel
      {
        Name = "detailLayout",
        Dock = DockStyle.Top,
        RowCount = 6,
        ColumnCount = 1,
        BackColor = Color.Transparent,
        AutoSize = true,
        Margin = new Padding(0)
      };
      detailLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F)); // Header (tăng thêm)
      detailLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F)); // Content preview (tăng thêm)
      detailLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F)); // Stats (tăng thêm để hiển thị lý do)
      detailLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F)); // Admin note (tăng thêm)
      detailLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F)); // Add note (tăng thêm)
      detailLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));  // Actions

      // Header
      var detailHeader = CreateDetailHeader(report);
      detailLayout.Controls.Add(detailHeader, 0, 0);

      // Content Preview
      var contentPreview = CreateContentPreview(report);
      detailLayout.Controls.Add(contentPreview, 0, 1);

      // Stats
      var stats = CreateStatsPanel(report);
      detailLayout.Controls.Add(stats, 0, 2);

      // Admin Notes
      var notesPanel = CreateNotesPanel(report);
      detailLayout.Controls.Add(notesPanel, 0, 3);
      // Add Note
      var addNotePanel = CreateAddNotePanel(report);
      detailLayout.Controls.Add(addNotePanel, 0, 4);

      // Quick Actions
      var actionsPanel = CreateDetailActionsPanel(report);
      detailLayout.Controls.Add(actionsPanel, 0, 5);

      scrollPanel.Controls.Add(detailLayout);
      pnlDetailPanel.Controls.Add(scrollPanel);
    }

    private Panel CreateDetailHeader(Report report)
    {
      var panel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        Padding = new Padding(15, 10, 15, 10)
      };

      var lblTitle = new Label
      {
        Text = $"🧾 Báo cáo #{report.ReportID}",
        Font = new Font("Segoe UI", 12F, FontStyle.Bold),
        ForeColor = DarkColor,
        AutoSize = false,
        Height = 30,
        Dock = DockStyle.Top
      };

      var lblSub = new Label
      {
        Text = $"Loại: {report.ReportType} • Trạng thái: {report.Status} • Thời gian: {report.ReportedAt:dd/MM/yyyy HH:mm}",
        Font = new Font("Segoe UI", 9F),
        ForeColor = Color.FromArgb(127, 140, 141),
        AutoSize = false,
        Height = 22,
        Dock = DockStyle.Top
      };

      panel.Controls.Add(lblSub);
      panel.Controls.Add(lblTitle);
      return panel;
    }

    private Panel CreateContentPreview(Report report)
    {
      var panel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        Padding = new Padding(15, 10, 15, 10)
      };

      var lblTitle = new Label
      {
        Text = "📄 Nội dung bị báo cáo:",
        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
        ForeColor = DarkColor,
        AutoSize = false,
        Height = 30,
        Dock = DockStyle.Top
      };

      var txtContent = new TextBox
      {
        Multiline = true,
        ReadOnly = true,
        Dock = DockStyle.Fill,
        Font = new Font("Segoe UI", 9.5F),
        BackColor = Color.FromArgb(247, 249, 252),
        BorderStyle = BorderStyle.FixedSingle,
        ScrollBars = ScrollBars.Vertical,
        Padding = new Padding(5)
      };

      // Load content
      if (report.ReportType == "Post")
      {
        var posts = postService.GetAllPosts(true);
        var post = posts.FirstOrDefault(p => p.PostID == report.ContentID);
        txtContent.Text = post?.Content ?? "(Nội dung không tồn tại)";
      }
      else if (report.ReportType == "Comment")
      {
        var comments = commentService.GetAllComments(true);
        var comment = comments.FirstOrDefault(c => c.CommentID == report.ContentID);
        txtContent.Text = comment?.Content ?? "(Bình luận không tồn tại)";
      }

      panel.Controls.Add(txtContent);
      panel.Controls.Add(lblTitle);
      return panel;
    }

    private Panel CreateStatsPanel(Report report)
    {
      var panel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.FromArgb(236, 240, 241),
        Padding = new Padding(15, 10, 15, 10)
      };

      var flowPanel = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.TopDown,
        WrapContents = false,
        AutoScroll = false,
        BackColor = Color.Transparent
      };

      // Lý do - sử dụng label có AutoSize để tự động wrap text
      var lblReasonTitle = new Label
      {
        Text = "⚡ Lý do báo cáo:",
        Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
        ForeColor = DarkColor,
        AutoSize = true,
        Margin = new Padding(0, 0, 0, 3)
      };

      var lblReason = new Label
      {
        Text = report.Reason ?? "(Không có lý do)",
        Font = new Font("Segoe UI", 9.5F),
        ForeColor = DangerColor,
        AutoSize = false,
        MaximumSize = new Size(panel.Width - 30, 0), // Wrap text
        AutoEllipsis = false,
        Padding = new Padding(0, 0, 0, 5)
      };
      // Calculate required height for reason text
      using (Graphics g = panel.CreateGraphics())
      {
        SizeF size = g.MeasureString(lblReason.Text, lblReason.Font, lblReason.MaximumSize.Width);
        lblReason.Height = (int)Math.Ceiling(size.Height) + 5;
      }

      var lblReviewer = new Label
      {
        Text = report.ReviewerID.HasValue
              ? $"👨‍💼 Người xử lý: {userService.GetUserById(report.ReviewerID.Value)?.UserName ?? "Unknown"}"
              : "👨‍💼 Người xử lý: (Chưa có)",
        Font = new Font("Segoe UI", 9F),
        ForeColor = DarkColor,
        AutoSize = true,
        Margin = new Padding(0, 3, 0, 3)
      };

      var lblReviewedAt = new Label
      {
        Text = report.ReviewedAt.HasValue
              ? $"✅ Xử lý lúc: {report.ReviewedAt.Value:dd/MM/yyyy HH:mm}"
              : "⏳ Chưa xử lý",
        Font = new Font("Segoe UI", 9F),
        ForeColor = Color.FromArgb(127, 140, 141),
        AutoSize = true,
        Margin = new Padding(0, 3, 0, 3)
      };

      var lblAction = new Label
      {
        Text = !string.IsNullOrEmpty(report.Action)
              ? $"🔨 Hành động: {report.Action}"
              : "🔨 Hành động: (Chưa có)",
        Font = new Font("Segoe UI", 9F),
        ForeColor = DarkColor,
        AutoSize = true,
        Margin = new Padding(0, 3, 0, 0)
      };

      flowPanel.Controls.AddRange(new Control[] { lblReasonTitle, lblReason, lblReviewer, lblReviewedAt, lblAction });
      panel.Controls.Add(flowPanel);
      return panel;
    }

    private Panel CreateNotesPanel(Report report)
    {
      var panel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        Padding = new Padding(15, 10, 15, 10)
      };

      var lblTitle = new Label
      {
        Text = "📝 Ghi chú của Admin:",
        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
        ForeColor = DarkColor,
        AutoSize = false,
        Height = 30,
        Dock = DockStyle.Top
      };

      var txtNotes = new TextBox
      {
        Multiline = true,
        ReadOnly = true,
        Dock = DockStyle.Fill,
        Font = new Font("Segoe UI", 9F),
        BackColor = Color.FromArgb(254, 249, 231),
        BorderStyle = BorderStyle.FixedSingle,
        ScrollBars = ScrollBars.Vertical,
        Text = report.AdminNote ?? "(Chưa có ghi chú)",
        Padding = new Padding(5)
      };

      panel.Controls.Add(txtNotes);
      panel.Controls.Add(lblTitle);
      return panel;
    }

    private Panel CreateAddNotePanel(Report report)
    {
      var panel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        Padding = new Padding(15, 10, 15, 10)
      };

      var lblTitle = new Label
      {
        Text = "➕ Thêm ghi chú:",
        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
        ForeColor = DarkColor,
        AutoSize = false,
        Height = 30,
        Dock = DockStyle.Top
      };

      var layout = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 1,
        BackColor = Color.Transparent,
        Padding = new Padding(0)
      };
      layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
      layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

      var txtNewNote = new TextBox
      {
        Multiline = true,
        Dock = DockStyle.Fill,
        Font = new Font("Segoe UI", 9F),
        BorderStyle = BorderStyle.FixedSingle,
        ScrollBars = ScrollBars.Vertical,
        BackColor = Color.White,
        ForeColor = Color.Gray,
        Text = "Nhập ghi chú..."
      };

      // Handle placeholder text
      txtNewNote.GotFocus += (s, e) =>
      {
        if (txtNewNote.Text == "Nhập ghi chú...")
        {
          txtNewNote.Text = "";
          txtNewNote.ForeColor = Color.Black;
        }
      };

      txtNewNote.LostFocus += (s, e) =>
      {
        if (string.IsNullOrWhiteSpace(txtNewNote.Text))
        {
          txtNewNote.Text = "Nhập ghi chú...";
          txtNewNote.ForeColor = Color.Gray;
        }
      };

      var btnAddNote = new RoundedButton
      {
        Text = "💾 Lưu",
        Dock = DockStyle.Fill,
        FlatStyle = FlatStyle.Flat,
        BackColor = SuccessColor,
        BackgroundColor = SuccessColor,
        BorderRadius = 10,
        ForeColor = Color.White,
        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
        Cursor = Cursors.Hand
      };
      btnAddNote.FlatAppearance.BorderSize = 0;
      btnAddNote.Click += (s, e) =>
      {
        if (string.IsNullOrWhiteSpace(txtNewNote.Text) || txtNewNote.Text == "Nhập ghi chú...")
        {
          MessageBox.Show("Vui lòng nhập nội dung ghi chú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }

        if (reportService.AddNoteToReport(report.ReportID,
                  $"[{DateTime.Now:dd/MM/yyyy HH:mm}] {txtNewNote.Text}"))
        {
          MessageBox.Show("Đã thêm ghi chú!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
          txtNewNote.Text = "Nhập ghi chú...";
          txtNewNote.ForeColor = Color.Gray;
          LoadReports();
          if (currentReport != null)
          {
            var updatedReport = reportService.GetAllReports().FirstOrDefault(r => r.ReportID == report.ReportID);
            if (updatedReport != null) LoadReportDetail(updatedReport);
          }
        }
      };

      layout.Controls.Add(txtNewNote, 0, 0);
      layout.Controls.Add(btnAddNote, 1, 0);

      panel.Controls.Add(layout);
      panel.Controls.Add(lblTitle);
      return panel;
    }

    private Panel CreateDetailActionsPanel(Report report)
    {
      var panel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.Transparent,
        Padding = new Padding(15, 5, 15, 5)
      };

      var flowPanel = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.LeftToRight,
        WrapContents = true,
        BackColor = Color.Transparent
      };

      var btnViewContent = new RoundedButton
      {
        Text = "👁️ Xem nội dung",
        Width = 120,
        Height = 35,
        FlatStyle = FlatStyle.Flat,
        BackColor = PrimaryColor,
        BackgroundColor = PrimaryColor,
        BorderRadius = 10,
        ForeColor = Color.White,
        Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
        Cursor = Cursors.Hand,
        Margin = new Padding(0, 0, 5, 0)
      };
      btnViewContent.FlatAppearance.BorderSize = 0;
      btnViewContent.Click += (s, e) => ViewContent(report);

      var btnMarkInReview = new RoundedButton
      {
        Text = "🔍 Đang xem xét",
        Width = 120,
        Height = 35,
        FlatStyle = FlatStyle.Flat,
        BackColor = WarningColor,
        BackgroundColor = WarningColor,
        BorderRadius = 10,
        ForeColor = Color.White,
        Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
        Cursor = Cursors.Hand,
        Margin = new Padding(0, 0, 5, 0)
      };
      btnMarkInReview.FlatAppearance.BorderSize = 0;
      btnMarkInReview.Click += (s, e) => UpdateStatus(report, "In Review", null);

      flowPanel.Controls.AddRange(new Control[] { btnViewContent, btnMarkInReview });
      panel.Controls.Add(flowPanel);
      return panel;
    }

    private void ViewContent(Report report)
    {
      string message = "";

      if (report.ReportType == "Post")
      {
        var posts = postService.GetAllPosts(true);
        var post = posts.FirstOrDefault(p => p.PostID == report.ContentID);
        if (post != null)
        {
          var user = userService.GetUserById(post.UserID);
          message = $"📝 Bài viết #{post.PostID}\n" +
                   $"👤 Tác giả: {user?.UserName ?? "Unknown"}\n" +
                   $"🕐 {post.CreatedAt:dd/MM/yyyy HH:mm}\n" +
                   $"❤️ {post.LikesCount} likes | 💬 {post.CommentsCount} comments\n\n" +
                   $"Nội dung:\n{post.Content}";
        }
        else
        {
          message = "Bài viết không tồn tại hoặc đã bị xóa.";
        }
      }
      else if (report.ReportType == "Comment")
      {
        var comments = commentService.GetAllComments(true);
        var comment = comments.FirstOrDefault(c => c.CommentID == report.ContentID);
        if (comment != null)
        {
          var user = userService.GetUserById(comment.UserID);
          message = $"💬 Bình luận #{comment.CommentID}\n" +
                   $"👤 Tác giả: {user?.UserName ?? "Unknown"}\n" +
                   $"📄 Bài viết #{comment.PostID}\n" +
                   $"🕐 {comment.CreatedAt:dd/MM/yyyy HH:mm}\n\n" +
                   $"Nội dung:\n{comment.Content}";
        }
        else
        {
          message = "Bình luận không tồn tại hoặc đã bị xóa.";
        }
      }

      MessageBox.Show(message, "Chi tiết nội dung", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void UpdateStatus(Report report, string status, string? action)
    {
      var currentUserID = AuthSessionService.CurrentUser?.UserID ?? Guid.Empty;

      if (reportService.UpdateReportStatus(report.ReportID, status, currentUserID, null, action))
      {
        MessageBox.Show($"Đã cập nhật trạng thái thành '{status}'!", "Thành công",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadReports();

        var updatedReport = reportService.GetAllReports().FirstOrDefault(r => r.ReportID == report.ReportID);
        if (updatedReport != null) LoadReportDetail(updatedReport);
      }
    }
    #endregion

    #region Action Handlers
    private void BtnRefresh_Click(object? sender, EventArgs e)
    {
      LoadReports();
      MessageBox.Show("Đã làm mới danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void BtnMarkReviewed_Click(object? sender, EventArgs e)
    {
      var selectedReports = GetSelectedReports();
      if (selectedReports.Count == 0)
      {
        MessageBox.Show("Vui lòng chọn ít nhất một báo cáo!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      var result = MessageBox.Show(
          $"Đánh dấu {selectedReports.Count} báo cáo là 'In Review'?",
          "Xác nhận",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Question
      );

      if (result == DialogResult.Yes)
      {
        var currentUserID = AuthSessionService.CurrentUser?.UserID ?? Guid.Empty;
        var reportIDs = selectedReports.Select(r => r.ReportID).ToList();

        if (reportService.BatchUpdateReports(reportIDs, "In Review", currentUserID))
        {
          MessageBox.Show("Đã cập nhật trạng thái!", "Thành công",
              MessageBoxButtons.OK, MessageBoxIcon.Information);
          LoadReports();
        }
      }
    }

    private void BtnHideContent_Click(object? sender, EventArgs e)
    {
      var selectedReports = GetSelectedReports();
      if (selectedReports.Count == 0)
      {
        MessageBox.Show("Vui lòng chọn ít nhất một báo cáo!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      var result = MessageBox.Show(
          $"Ẩn nội dung của {selectedReports.Count} báo cáo và đánh dấu 'Resolved'?",
          "Xác nhận",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Warning
      );

      if (result == DialogResult.Yes)
      {
        var currentUserID = AuthSessionService.CurrentUser?.UserID ?? Guid.Empty;
        int successCount = 0;

        foreach (var report in selectedReports)
        {
          // Hide content
          if (report.ReportType == "Post")
          {
            postService.SoftDeletePost(report.ContentID); // Soft delete
          }
          else if (report.ReportType == "Comment")
          {
            commentService.SoftDeleteComment(report.ContentID); // Soft delete
          }

          // Update report to "Pending Action" first, then "Resolved" after content is hidden
          if (reportService.UpdateReportStatus(report.ReportID, "Pending Action", currentUserID,
              "Đang thực hiện hành động ẩn nội dung", "Processing"))
          {
            // After hiding content, mark as resolved
            if (reportService.UpdateReportStatus(report.ReportID, "Resolved", currentUserID,
                "Đã hoàn thành - Nội dung đã bị ẩn", "Content Hidden"))
            {
              successCount++;
            }
          }
        }

        MessageBox.Show($"Đã ẩn và xử lý {successCount}/{selectedReports.Count} báo cáo!",
            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadReports();
      }
    }

    private void BtnBanUser_Click(object? sender, EventArgs e)
    {
      var selectedReports = GetSelectedReports();
      if (selectedReports.Count == 0)
      {
        MessageBox.Show("Vui lòng chọn ít nhất một báo cáo!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      // Get unique reported users
      var reportedUserIDs = selectedReports.Select(r => r.ReportedUserID).Distinct().ToList();

      var result = MessageBox.Show(
          $"Ban {reportedUserIDs.Count} người dùng bị báo cáo?\n\n" +
          $"⚠️ Hành động này sẽ đánh dấu trạng thái 'Escalated' cho các báo cáo.",
          "Xác nhận Ban User",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Warning
      );

      if (result == DialogResult.Yes)
      {
        var currentUserID = AuthSessionService.CurrentUser?.UserID ?? Guid.Empty;
        int successCount = 0;

        foreach (var userID in reportedUserIDs)
        {
          // Ban user by setting StatusId to -1
          if (userService.UpdateUserStatus(userID, -1))
          {
            successCount++;
          }
        }

        // Update all related reports
        var reportIDs = selectedReports.Select(r => r.ReportID).ToList();
        reportService.BatchUpdateReports(reportIDs, "Escalated", currentUserID, "Ban User");

        MessageBox.Show(
            $"Đã xử lý {successCount} người dùng!\n" +
            $"Các báo cáo đã được đánh dấu 'Escalated'.",
            "Thành công",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );
        LoadReports();
      }
    }

    private void BtnReject_Click(object? sender, EventArgs e)
    {
      var selectedReports = GetSelectedReports();
      if (selectedReports.Count == 0)
      {
        MessageBox.Show("Vui lòng chọn ít nhất một báo cáo!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      var result = MessageBox.Show(
          $"Từ chối {selectedReports.Count} báo cáo?\n\n" +
          $"Báo cáo sẽ được đánh dấu 'Rejected'.",
          "Xác nhận",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Question
      );

      if (result == DialogResult.Yes)
      {
        var currentUserID = AuthSessionService.CurrentUser?.UserID ?? Guid.Empty;
        var reportIDs = selectedReports.Select(r => r.ReportID).ToList();

        if (reportService.BatchUpdateReports(reportIDs, "Rejected", currentUserID, "None"))
        {
          MessageBox.Show("Đã từ chối các báo cáo!", "Thành công",
              MessageBoxButtons.OK, MessageBoxIcon.Information);
          LoadReports();
        }
      }
    }

    private void BtnExport_Click(object? sender, EventArgs e)
    {
      try
      {
        var saveDialog = new SaveFileDialog
        {
          Filter = "CSV files (*.csv)|*.csv",
          FileName = $"Reports_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
        };

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
          var reports = reportService.GetAllReports();
          var lines = new List<string>
                    {
                        "ID,Loại,Nội dung ID,Người báo cáo,Người bị báo cáo,Lý do,Trạng thái,Thời gian,Người xử lý,Thời gian xử lý,Hành động"
                    };

          foreach (var report in reports)
          {
            var reporter = userService.GetUserById(report.ReporterUserID);
            var reported = userService.GetUserById(report.ReportedUserID);
            var reviewer = report.ReviewerID.HasValue
                ? userService.GetUserById(report.ReviewerID.Value)?.UserName
                : "";

            lines.Add($"{report.ReportID},{report.ReportType},{report.ContentID}," +
                     $"{reporter?.UserName},{reported?.UserName},{report.Reason}," +
                     $"{report.Status},{report.ReportedAt:yyyy-MM-dd HH:mm}," +
                     $"{reviewer},{report.ReviewedAt?.ToString("yyyy-MM-dd HH:mm")}," +
                     $"{report.Action}");
          }

          System.IO.File.WriteAllLines(saveDialog.FileName, lines);
          MessageBox.Show("Xuất dữ liệu thành công!", "Thành công",
              MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi xuất dữ liệu: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private List<Report> GetSelectedReports()
    {
      var reports = new List<Report>();
      foreach (DataGridViewRow row in dgvReports.SelectedRows)
      {
        if (row.Tag is Report report)
        {
          reports.Add(report);
        }
      }
      return reports;
    }
    #endregion

    #region Helper Methods
    private string GetStatusText(string status)
    {
      return status switch
      {
        // Removed "New"; initial state represented as "Pending Action"
        "In Review" => "🔍 Đang xem xét",
        "Pending Action" => "⏳ Chờ xử lý",
        "Resolved" => "✅ Đã giải quyết",
        "Escalated" => "⚠️ Nghiêm trọng",
        "Rejected" => "❌ Từ chối",
        _ => status
      };
    }

    private Color GetStatusColor(string status)
    {
      return status switch
      {
        // Removed "New" mapping
        "In Review" => WarningColor,
        "Pending Action" => Color.FromArgb(255, 152, 0), // Orange
        "Resolved" => SuccessColor,
        "Escalated" => Color.FromArgb(192, 57, 43),
        "Rejected" => Color.FromArgb(149, 165, 166),
        _ => Color.Gray
      };
    }
    #endregion
  }
}
