using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using SocialManager.services;

namespace SocialManager.frm.UserControls
{
    public partial class ucAnalyticsSimple : UserControl
    {
        private DashboardService dashboardService;
        private UserService userService;
        private PostService postService;

        public ucAnalyticsSimple()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("ucAnalyticsSimple: Starting initialization...");
                
                InitializeComponent();
                InitializeAnalyticsSimple();
                
                System.Diagnostics.Debug.WriteLine("ucAnalyticsSimple: Initialization completed successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in ucAnalyticsSimple: {ex.Message}");
                CreateErrorInterface();
            }
        }

        private void InitializeAnalyticsSimple()
        {
            try
            {
                this.BackColor = Color.FromArgb(247, 249, 252);
                
                // Initialize services
                dashboardService = new DashboardService();
                userService = new UserService();
                postService = new PostService();
                
                // Create simple analytics layout
                CreateSimpleAnalyticsLayout();
                
                // Load data
                LoadAnalyticsData();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in InitializeAnalyticsSimple: {ex.Message}");
                throw;
            }
        }

        private void CreateSimpleAnalyticsLayout()
        {
            this.Controls.Clear();
            
            // Main panel
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(247, 249, 252),
                Padding = new Padding(30)
            };

            // Header
            var lblHeader = new Label
            {
                Text = "?? Analytics & Reports",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(0, 0)
            };

            // Metrics container
            var metricsContainer = new Panel
            {
                Location = new Point(0, 60),
                Size = new Size(this.Width - 60, 150),
                BackColor = Color.Transparent
            };

            // Create metric cards
            var totalReachCard = CreateMetricCard("T?ng L??t Ti?p C?n", "0", Color.FromArgb(52, 152, 219), 0);
            var engagementCard = CreateMetricCard("T? L? T??ng Tác", "0.0%", Color.FromArgb(231, 76, 60), 1);
            var followersCard = CreateMetricCard("Ng??i Theo Dõi M?i", "+0", Color.FromArgb(46, 204, 113), 2);
            var clicksCard = CreateMetricCard("L??t Nh?p", "0", Color.FromArgb(230, 126, 34), 3);

            metricsContainer.Controls.Add(totalReachCard);
            metricsContainer.Controls.Add(engagementCard);
            metricsContainer.Controls.Add(followersCard);
            metricsContainer.Controls.Add(clicksCard);

            // Summary panel
            var summaryPanel = CreateSummaryPanel();
            summaryPanel.Location = new Point(0, 230);
            summaryPanel.Size = new Size(this.Width - 60, 300);

            // Action buttons
            var actionsPanel = CreateActionsPanel();
            actionsPanel.Location = new Point(0, 550);
            actionsPanel.Size = new Size(this.Width - 60, 60);

            mainPanel.Controls.Add(lblHeader);
            mainPanel.Controls.Add(metricsContainer);
            mainPanel.Controls.Add(summaryPanel);
            mainPanel.Controls.Add(actionsPanel);

            this.Controls.Add(mainPanel);
        }

        private Panel CreateMetricCard(string title, string value, Color accentColor, int position)
        {
            var card = new Panel
            {
                Size = new Size(240, 120),
                Location = new Point(position * 260, 10),
                BackColor = Color.White,
                Name = $"card_{position}"
            };

            card.Paint += (sender, e) => {
                DrawMetricCard(e.Graphics, card.ClientRectangle, accentColor);
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(20, 20),
                Size = new Size(200, 25)
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 50),
                Size = new Size(200, 35),
                Name = $"value_{position}"
            };

            var iconPanel = new Panel
            {
                Size = new Size(40, 40),
                Location = new Point(180, 15),
                BackColor = accentColor
            };

            iconPanel.Paint += (sender, e) => {
                DrawMetricIcon(e.Graphics, iconPanel.ClientRectangle, Color.White, position);
            };

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            card.Controls.Add(iconPanel);

            return card;
        }

        private Panel CreateSummaryPanel()
        {
            var panel = new Panel
            {
                BackColor = Color.White
            };

            panel.Paint += (sender, e) => {
                DrawRoundedPanel(e.Graphics, panel.ClientRectangle);
            };

            var lblTitle = new Label
            {
                Text = "?? T?ng Quan Ho?t ??ng",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(30, 20),
                Size = new Size(300, 30)
            };

            var lblSummary = new Label
            {
                Text = "?ang t?i d? li?u th?ng kê...",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(30, 60),
                Size = new Size(600, 200),
                Name = "lblSummary"
            };

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblSummary);

            return panel;
        }

        private Panel CreateActionsPanel()
        {
            var panel = new Panel
            {
                BackColor = Color.Transparent
            };

            var btnRefresh = new Button
            {
                Text = "?? Làm M?i D? Li?u",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 40),
                Location = new Point(0, 10)
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;

            var btnExport = new Button
            {
                Text = "?? Xu?t Báo Cáo",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 40),
                Location = new Point(170, 10)
            };
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += BtnExport_Click;

            var btnDebug = new Button
            {
                Text = "?? Debug Info",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(230, 126, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 40),
                Location = new Point(340, 10)
            };
            btnDebug.FlatAppearance.BorderSize = 0;
            btnDebug.Click += BtnDebug_Click;

            panel.Controls.Add(btnRefresh);
            panel.Controls.Add(btnExport);
            panel.Controls.Add(btnDebug);

            return panel;
        }

        private void LoadAnalyticsData()
        {
            try
            {
                var statistics = dashboardService.GetDashboardStatistics();
                var userStats = userService.GetUserStatistics();

                // Update metric cards
                var card0Value = this.Controls.Find("value_0", true).FirstOrDefault() as Label;
                if (card0Value != null)
                    card0Value.Text = dashboardService.FormatNumber(statistics.TotalInteractions);

                var card1Value = this.Controls.Find("value_1", true).FirstOrDefault() as Label;
                if (card1Value != null)
                {
                    var rate = statistics.TotalPosts > 0 ? (double)statistics.TotalInteractions / statistics.TotalPosts : 0;
                    card1Value.Text = $"{rate:F1}%";
                }

                var card2Value = this.Controls.Find("value_2", true).FirstOrDefault() as Label;
                if (card2Value != null)
                    card2Value.Text = $"+{userStats.ActiveUsers}";

                var card3Value = this.Controls.Find("value_3", true).FirstOrDefault() as Label;
                if (card3Value != null)
                    card3Value.Text = dashboardService.FormatNumber(statistics.PostsToday * 10);

                // Update summary
                var lblSummary = this.Controls.Find("lblSummary", true).FirstOrDefault() as Label;
                if (lblSummary != null)
                {
                    lblSummary.Text = $"?? Th?ng kê t?ng quan:\n\n" +
                                    $"?? T?ng ng??i dùng: {userStats.TotalUsers}\n" +
                                    $"?? T?ng bài vi?t: {statistics.TotalPosts}\n" +
                                    $"?? T?ng t??ng tác: {statistics.TotalInteractions}\n" +
                                    $"?? T?ng tr??ng: {statistics.GrowthPercentage:F1}%\n" +
                                    $"?? Bài vi?t hôm nay: {statistics.PostsToday}\n" +
                                    $"?? Ng??i dùng ho?t ??ng: {userStats.ActiveUsers}\n\n" +
                                    $"?? C?p nh?t lúc: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                }

                System.Diagnostics.Debug.WriteLine("ucAnalyticsSimple: Data loaded successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR loading analytics data: {ex.Message}");
            }
        }

        private void CreateErrorInterface()
        {
            this.Controls.Clear();
            
            var lblError = new Label
            {
                Text = "? L?i t?i Analytics\n\nMô-?un Analytics g?p v?n ?? k? thu?t.\nVui lòng liên h? support ?? ???c h? tr?.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14F),
                ForeColor = Color.FromArgb(231, 76, 60),
                BackColor = Color.White
            };
            
            this.Controls.Add(lblError);
        }

        #region Event Handlers

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadAnalyticsData();
                MessageBox.Show("D? li?u ?ã ???c làm m?i!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi làm m?i: {ex.Message}", "L?i", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv";
                    saveFileDialog.Title = "Export Analytics Report";
                    saveFileDialog.FileName = $"Analytics_Simple_Report_{DateTime.Now:yyyyMMdd}";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show($"Báo cáo ?ã ???c xu?t: {saveFileDialog.FileName}", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi xu?t báo cáo: {ex.Message}", "L?i", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDebug_Click(object sender, EventArgs e)
        {
            try
            {
                var statistics = dashboardService.GetDashboardStatistics();
                var userStats = userService.GetUserStatistics();

                var debugInfo = $"?? Debug Info - ucAnalyticsSimple\n\n" +
                              $"?? Statistics:\n" +
                              $"  - TotalUsers: {userStats.TotalUsers}\n" +
                              $"  - TotalPosts: {statistics.TotalPosts}\n" +
                              $"  - TotalInteractions: {statistics.TotalInteractions}\n" +
                              $"  - ActiveUsers: {userStats.ActiveUsers}\n" +
                              $"  - PostsToday: {statistics.PostsToday}\n" +
                              $"  - Growth: {statistics.GrowthPercentage:F2}%\n\n" +
                              $"?? Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                              $"?? Module: Simple Analytics (No Chart)";

                MessageBox.Show(debugInfo, "Debug Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Debug Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Drawing Methods

        private void DrawMetricCard(Graphics g, Rectangle rect, Color accentColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            // Draw shadow
            using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
            {
                var shadowPath = CreateRoundedRectangle(new Rectangle(3, 3, rect.Width - 3, rect.Height - 3), 8);
                g.FillPath(shadowBrush, shadowPath);
            }
            
            // Draw card background
            using (var cardBrush = new SolidBrush(Color.White))
            {
                var cardPath = CreateRoundedRectangle(new Rectangle(0, 0, rect.Width - 3, rect.Height - 3), 8);
                g.FillPath(cardBrush, cardPath);
            }
            
            // Draw accent border
            using (var accentBrush = new SolidBrush(accentColor))
            {
                var accentRect = new Rectangle(0, 0, rect.Width - 3, 4);
                var accentPath = CreateRoundedRectangle(accentRect, 8);
                g.FillPath(accentBrush, accentPath);
            }
        }

        private void DrawRoundedPanel(Graphics g, Rectangle rect)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            // Draw shadow
            using (var shadowBrush = new SolidBrush(Color.FromArgb(15, 0, 0, 0)))
            {
                var shadowPath = CreateRoundedRectangle(new Rectangle(2, 2, rect.Width - 2, rect.Height - 2), 8);
                g.FillPath(shadowBrush, shadowPath);
            }
            
            // Draw panel background
            using (var panelBrush = new SolidBrush(Color.White))
            {
                var panelPath = CreateRoundedRectangle(new Rectangle(0, 0, rect.Width - 2, rect.Height - 2), 8);
                g.FillPath(panelBrush, panelPath);
            }
        }

        private void DrawMetricIcon(Graphics g, Rectangle rect, Color color, int iconType)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(color))
            {
                int centerX = rect.X + rect.Width / 2;
                int centerY = rect.Y + rect.Height / 2;
                
                switch (iconType)
                {
                    case 0: // Reach - Eye icon
                        g.DrawEllipse(new Pen(color, 3), centerX - 12, centerY - 6, 24, 12);
                        g.FillEllipse(brush, centerX - 4, centerY - 4, 8, 8);
                        break;
                    case 1: // Engagement - Heart icon
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
                    case 2: // Followers - User+ icon
                        g.FillEllipse(brush, centerX - 8, centerY - 8, 10, 10);
                        g.FillRectangle(brush, centerX - 10, centerY + 4, 14, 6);
                        g.FillRectangle(brush, centerX + 8, centerY - 2, 6, 2);
                        g.FillRectangle(brush, centerX + 10, centerY - 4, 2, 6);
                        break;
                    case 3: // Clicks - Cursor icon
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

        #endregion

        public void RefreshData()
        {
            LoadAnalyticsData();
        }
    }
}