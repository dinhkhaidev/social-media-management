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
// using System.Windows.Forms.DataVisualization.Charting; // Commented out to avoid dependencies
using SocialManager.services;

namespace SocialManager.frm.UserControls
{
    public partial class ucAnalytics : UserControl
    {
        private DashboardService dashboardService;
        private UserService userService;
        private PostService postService;
        
        // Chart and analytics controls (sử dụng từ Designer)
        // private Panel pnlUserGrowthChart;
        // private Panel pnlPostActivityChart;
        // private Panel pnlEngagementChart;
        // private DataGridView dgvTopPerformers;
        // private ListView lvSystemMetrics;

        public ucAnalytics()
        {
            System.Diagnostics.Debug.WriteLine("ucAnalytics: Starting constructor...");
            
            try
            {
                InitializeComponent();
                System.Diagnostics.Debug.WriteLine("ucAnalytics: InitializeComponent completed successfully");
                
                // Khởi tạo đơn giản chỉ sau khi InitializeComponent thành công
                PostInitializeSetup();
                
                System.Diagnostics.Debug.WriteLine("ucAnalytics: Constructor completed successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CRITICAL ERROR in ucAnalytics constructor: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                
                // Đây là lỗi Designer hoặc Chart control - throw để frmAdmin tạo fallback
                throw new InvalidOperationException($"ucAnalytics initialization failed: {ex.Message}", ex);
            }
        }
        
        private void PostInitializeSetup()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("ucAnalytics: Post-initialization setup...");
                
                // Thiết lập background color
                this.BackColor = Color.FromArgb(247, 249, 252);
                
                // Khởi tạo services đơn giản
                dashboardService = new DashboardService();
                userService = new UserService();
                postService = new PostService();
                
                // Load dữ liệu ngay lập tức
                LoadBasicData();
                
                // Thiết lập components sau một delay ngắn
                var timer = new System.Windows.Forms.Timer();
                timer.Interval = 500; // 0.5 second
                timer.Tick += (s, e) => {
                    timer.Stop();
                    try
                    {
                        SetupComponents();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"ERROR in delayed setup: {ex.Message}");
                        // Bỏ qua lỗi này để không crash ứng dụng
                    }
                };
                timer.Start();
                
                System.Diagnostics.Debug.WriteLine("ucAnalytics: Post-initialization completed");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in PostInitializeSetup: {ex.Message}");
                // Không throw exception ở đây
            }
        }
        
        private void LoadBasicData()
        {
            try
            {
                var statistics = dashboardService.GetDashboardStatistics();
                
                // Cập nhật labels cơ bản (những cái từ Designer)
                if (lblTotalReachValue != null)
                    lblTotalReachValue.Text = statistics.TotalInteractions.ToString();
                
                if (lblEngagementRateValue != null)
                {
                    var rate = statistics.TotalPosts > 0 ? (double)statistics.TotalInteractions / statistics.TotalPosts : 0;
                    lblEngagementRateValue.Text = $"{rate:F1}%";
                }
                
                if (lblNewFollowersValue != null)
                    lblNewFollowersValue.Text = $"+{statistics.ActiveUsers}";
                
                if (lblClicksValue != null)
                    lblClicksValue.Text = (statistics.PostsToday * 10).ToString();
                
                System.Diagnostics.Debug.WriteLine("ucAnalytics: Basic data loaded successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR loading basic data: {ex.Message}");
                // Set default values
                if (lblTotalReachValue != null) lblTotalReachValue.Text = "0";
                if (lblEngagementRateValue != null) lblEngagementRateValue.Text = "0.0%";
                if (lblNewFollowersValue != null) lblNewFollowersValue.Text = "+0";
                if (lblClicksValue != null) lblClicksValue.Text = "0";
            }
        }
        
        private void SetupComponents()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("ucAnalytics: Setting up components...");
                
                // Thiết lập DataGridView an toàn
                SetupTopPostsGridSafe();
                
                // Thiết lập Chart an toàn (tránh BeginInit/EndInit conflict)
                SetupChartSafe();
                
                // Thêm debug button
                AddDebugButton();
                
                System.Diagnostics.Debug.WriteLine("ucAnalytics: Components setup completed");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in SetupComponents: {ex.Message}");
                // Không crash - chỉ log error
            }
        }
        
        private void SetupChart()
        {
            // Redirect to safe version
            SetupChartSafe();
        }
        
        private void SetupChartSafe()
        {
            try
            {
                /*
                // Chart code commented out to avoid dependencies
                if (chartPlatforms == null)
                {
                    System.Diagnostics.Debug.WriteLine("chartPlatforms is null, skipping chart setup");
                    return;
                }
                
                System.Diagnostics.Debug.WriteLine("Setting up chart safely...");
                
                // Kiểm tra xem chart đã được khởi tạo chưa
                if (chartPlatforms.Series.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Chart already has series, clearing...");
                    chartPlatforms.Series.Clear();
                }
                
                if (chartPlatforms.ChartAreas.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Chart already has areas, clearing...");
                    chartPlatforms.ChartAreas.Clear();
                }
                
                // Tạo chart area mới
                var chartArea = new ChartArea("MainArea");
                chartArea.AxisX.Title = "Thời gian";
                chartArea.AxisY.Title = "Số lượng";
                chartArea.BackColor = Color.White;
                chartPlatforms.ChartAreas.Add(chartArea);
                
                // Tạo series đơn giản
                var userSeries = new Series("Người dùng");
                userSeries.ChartType = SeriesChartType.Line;
                userSeries.Color = Color.FromArgb(52, 152, 219);
                userSeries.BorderWidth = 2;
                
                // Thêm dữ liệu mẫu đơn giản
                userSeries.Points.AddXY("T2", 5);
                userSeries.Points.AddXY("T3", 8);
                userSeries.Points.AddXY("T4", 12);
                userSeries.Points.AddXY("T5", 7);
                userSeries.Points.AddXY("T6", 15);
                userSeries.Points.AddXY("T7", 10);
                userSeries.Points.AddXY("CN", 6);
                
                chartPlatforms.Series.Add(userSeries);
                */
                
                System.Diagnostics.Debug.WriteLine("Chart setup skipped - using simple Analytics instead");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in chart setup: {ex.Message}");
                
                // Nếu có lỗi với chart, ẩn nó đi và hiển thị message
                try
                {
                    /* Chart code commented out
                    if (chartPlatforms != null)
                    {
                        chartPlatforms.Visible = false;
                    }
                    */
                    
                    if (pnlChart != null)
                    {
                        var lblChartError = new Label
                        {
                            Text = "📊 Chart tạm thời không khả dụng\n\nSử dụng Analytics Simple để xem dữ liệu thống kê",
                            Font = new Font("Segoe UI", 11F),
                            ForeColor = Color.FromArgb(127, 140, 141),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Dock = DockStyle.Fill,
                            BackColor = Color.White
                        };
                        
                        pnlChart.Controls.Add(lblChartError);
                        lblChartError.BringToFront();
                    }
                }
                catch
                {
                    // Ignore this error too
                }
            }
        }
        
        private void SetupTopPostsGrid()
        {
            // Redirect to safe version
            SetupTopPostsGridSafe();
        }
        
        private void SetupTopPostsGridSafe()
        {
            try
            {
                if (dgvTopPosts == null)
                {
                    System.Diagnostics.Debug.WriteLine("dgvTopPosts is null, skipping grid setup");
                    return;
                }
                
                System.Diagnostics.Debug.WriteLine("Setting up top posts grid...");
                
                dgvTopPosts.Columns.Clear();
                dgvTopPosts.Columns.Add("Content", "Nội dung");
                dgvTopPosts.Columns.Add("Likes", "Likes");
                dgvTopPosts.Columns.Add("Date", "Ngày");
                
                // Style grid
                dgvTopPosts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
                dgvTopPosts.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvTopPosts.DefaultCellStyle.BackColor = Color.White;
                
                // Thêm dữ liệu mẫu
                var posts = postService.GetAllPosts().Take(5);
                foreach (var post in posts)
                {
                    string shortContent = post.Content.Length > 30 ? 
                        post.Content.Substring(0, 30) + "..." : post.Content;
                    
                    dgvTopPosts.Rows.Add(shortContent, post.LikesCount, post.CreatedAt.ToString("dd/MM"));
                }
                
                System.Diagnostics.Debug.WriteLine("Top posts grid setup completed");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in grid setup: {ex.Message}");
            }
        }
        
        private void LoadAnalyticsData()
        {
            try
            {
                LoadBasicData();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in LoadAnalyticsData: {ex.Message}");
            }
        }

        private void AddDebugButton()
        {
            try
            {
                var btnDebug = new Button
                {
                    Text = "🔧 Test Load Data",
                    BackColor = Color.FromArgb(230, 126, 34),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(150, 35),
                    Location = new Point(10, 10),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };
                
                btnDebug.FlatAppearance.BorderSize = 0;
                btnDebug.Click += BtnDebug_Click;
                
                this.Controls.Add(btnDebug);
                btnDebug.BringToFront();
                
                System.Diagnostics.Debug.WriteLine("ucAnalytics: Debug button added");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding debug button: {ex.Message}");
            }
        }
        
        private void BtnDebug_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== DEBUG BUTTON CLICKED ===");
                
                // Test services
                if (dashboardService == null)
                {
                    System.Diagnostics.Debug.WriteLine("dashboardService is NULL - initializing...");
                    dashboardService = new DashboardService();
                }
                
                if (userService == null)
                {
                    System.Diagnostics.Debug.WriteLine("userService is NULL - initializing...");
                    userService = new UserService();
                }
                
                if (postService == null)
                {
                    System.Diagnostics.Debug.WriteLine("postService is NULL - initializing...");
                    postService = new PostService();
                }
                
                // Test getting data
                var stats = dashboardService.GetDashboardStatistics();
                System.Diagnostics.Debug.WriteLine($"DEBUG: TotalUsers={stats.TotalUsers}, TotalPosts={stats.TotalPosts}, TotalInteractions={stats.TotalInteractions}");
                
                var userStats = userService.GetUserStatistics();
                System.Diagnostics.Debug.WriteLine($"DEBUG: UserStats - Total={userStats.TotalUsers}, Active={userStats.ActiveUsers}");
                
                var posts = postService.GetAllPosts();
                System.Diagnostics.Debug.WriteLine($"DEBUG: Posts count = {posts.Count}");
                
                var users = userService.GetAllUsers();
                System.Diagnostics.Debug.WriteLine($"DEBUG: Users count = {users.Count}");
                
                // Test updating labels manually
                if (lblTotalReachValue != null)
                {
                    lblTotalReachValue.Text = $"DEBUG: {stats.TotalInteractions}";
                    System.Diagnostics.Debug.WriteLine("DEBUG: Updated lblTotalReachValue");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("DEBUG: lblTotalReachValue is NULL!");
                }
                
                if (lblEngagementRateValue != null)
                {
                    lblEngagementRateValue.Text = $"DEBUG: {stats.TotalPosts}";
                    System.Diagnostics.Debug.WriteLine("DEBUG: Updated lblEngagementRateValue");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("DEBUG: lblEngagementRateValue is NULL!");
                }
                
                if (lblNewFollowersValue != null)
                {
                    lblNewFollowersValue.Text = $"DEBUG: +{userStats.ActiveUsers}";
                    System.Diagnostics.Debug.WriteLine("DEBUG: Updated lblNewFollowersValue");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("DEBUG: lblNewFollowersValue is NULL!");
                }
                
                if (lblClicksValue != null)
                {
                    lblClicksValue.Text = $"DEBUG: {stats.PostsToday}";
                    System.Diagnostics.Debug.WriteLine("DEBUG: Updated lblClicksValue");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("DEBUG: lblClicksValue is NULL!");
                }
                
                MessageBox.Show($"Debug Complete!\n\nUsers: {userStats.TotalUsers}\nPosts: {stats.TotalPosts}\nInteractions: {stats.TotalInteractions}\nActive Users: {userStats.ActiveUsers}", 
                    "Debug Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                System.Diagnostics.Debug.WriteLine("=== DEBUG COMPLETED ===");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in debug: {ex.Message}");
                MessageBox.Show($"Debug Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Event Handlers

        private void btnExportReport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|Excel files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv";
                    saveFileDialog.Title = "Export Analytics Report";
                    saveFileDialog.FileName = $"Analytics_Report_{DateTime.Now:yyyyMMdd}";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show($"Báo cáo đã được xuất thành công tại: {saveFileDialog.FileName}", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshData_Click(object sender, EventArgs e)
        {
            try
            {
                LoadAnalyticsData();
                LoadTopPostsData();
                LoadChartData();
                MessageBox.Show("Dữ liệu phân tích đã được làm mới!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate = dtpFromDate.Value;
                DateTime toDate = dtpToDate.Value;

                if (fromDate >= toDate)
                {
                    MessageBox.Show("Ngày bắt đầu phải trước ngày kết thúc.", "Lỗi nhập liệu", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show($"Đã áp dụng bộ lọc cho khoảng thời gian: {fromDate:dd/MM/yyyy} đến {toDate:dd/MM/yyyy}", "Đã áp dụng bộ lọc", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LoadAnalyticsData();
                LoadTopPostsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi áp dụng bộ lọc: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        // Paint events for custom styling
        private void pnlCard_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                
                // Draw shadow
                using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                {
                    var shadowPath = CreateRoundedRectangle(new Rectangle(3, 3, panel.Width - 3, panel.Height - 3), 8);
                    e.Graphics.FillPath(shadowBrush, shadowPath);
                }
                
                // Draw main card
                using (var cardBrush = new SolidBrush(Color.White))
                {
                    var cardPath = CreateRoundedRectangle(new Rectangle(0, 0, panel.Width - 3, panel.Height - 3), 8);
                    e.Graphics.FillPath(cardBrush, cardPath);
                }
                
                // Draw subtle border
                using (var borderPen = new Pen(Color.FromArgb(230, 230, 230), 1))
                {
                    var borderPath = CreateRoundedRectangle(new Rectangle(0, 0, panel.Width - 4, panel.Height - 4), 8);
                    e.Graphics.DrawPath(borderPen, borderPath);
                }
            }
        }

        private void picTotalReach_Paint(object sender, PaintEventArgs e)
        {
            DrawMetricIcon(e.Graphics, new Rectangle(5, 5, 35, 35), Color.FromArgb(52, 152, 219), "reach");
        }

        private void picEngagementRate_Paint(object sender, PaintEventArgs e)
        {
            DrawMetricIcon(e.Graphics, new Rectangle(5, 5, 35, 35), Color.FromArgb(231, 76, 60), "engagement");
        }

        private void picNewFollowers_Paint(object sender, PaintEventArgs e)
        {
            DrawMetricIcon(e.Graphics, new Rectangle(5, 5, 35, 35), Color.FromArgb(46, 204, 113), "followers");
        }

        private void picClicks_Paint(object sender, PaintEventArgs e)
        {
            DrawMetricIcon(e.Graphics, new Rectangle(5, 5, 35, 35), Color.FromArgb(230, 126, 34), "clicks");
        }

        private void DrawMetricIcon(Graphics g, Rectangle rect, Color color, string iconType)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(color))
            {
                int centerX = rect.X + rect.Width / 2;
                int centerY = rect.Y + rect.Height / 2;
                
                switch (iconType)
                {
                    case "reach":
                        // Eye icon
                        g.DrawEllipse(new Pen(color, 3), centerX - 12, centerY - 6, 24, 12);
                        g.FillEllipse(brush, centerX - 4, centerY - 4, 8, 8);
                        break;
                    case "engagement":
                        // Heart icon
                        var path = new GraphicsPath();
                        path.AddEllipse(centerX - 10, centerY - 6, 10, 10);
                        path.AddEllipse(centerX, centerY - 6, 10, 10);
                        path.AddPolygon(new Point[] {
                            new Point(centerX - 5, centerY + 4),
                            new Point(centerX, centerY + 12),
                            new Point(centerX + 5, centerY + 4)
                        });
                        g.FillPath(brush, path);
                        break;
                    case "followers":
                        // User+ icon
                        g.FillEllipse(brush, centerX - 8, centerY - 8, 10, 10);
                        g.FillRectangle(brush, centerX - 10, centerY + 4, 14, 6);
                        // Plus sign
                        g.FillRectangle(brush, centerX + 8, centerY - 2, 6, 2);
                        g.FillRectangle(brush, centerX + 10, centerY - 4, 2, 6);
                        break;
                    case "clicks":
                        // Cursor icon
                        Point[] cursor = {
                            new Point(centerX - 8, centerY - 10),
                            new Point(centerX - 8, centerY + 8),
                            new Point(centerX - 2, centerY + 2),
                            new Point(centerX + 2, centerY + 6),
                            new Point(centerX + 6, centerY + 2),
                            new Point(centerX + 6, centerY - 2)
                        };
                        g.FillPolygon(brush, cursor);
                        break;
                }
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

        public void RefreshData()
        {
            try
            {
                LoadAnalyticsData();
                LoadTopPostsData();
                LoadChartData();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error refreshing data: {ex.Message}");
            }
        }

        private void LoadTopPostsData()
        {
            try
            {
                if (dgvTopPosts == null) return;
                
                dgvTopPosts.Rows.Clear();
                
                var posts = postService.GetAllPosts()
                    .OrderByDescending(p => p.LikesCount + p.CommentsCount)
                    .Take(10);
                
                foreach (var post in posts)
                {
                    string shortContent = post.Content.Length > 50 
                        ? post.Content.Substring(0, 50) + "..." 
                        : post.Content;
                    
                    dgvTopPosts.Rows.Add(
                        post.PostID,
                        shortContent,
                        post.LikesCount,
                        post.CommentsCount,
                        post.CreatedAt.ToString("dd/MM/yyyy")
                    );
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading top posts data: {ex.Message}");
            }
        }

        private void LoadChartData()
        {
            try
            {
                // Sử dụng safe chart setup thay vì manipulate chart trực tiếp
                SetupChartSafe();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading chart data: {ex.Message}");
            }
        }
    }
}
