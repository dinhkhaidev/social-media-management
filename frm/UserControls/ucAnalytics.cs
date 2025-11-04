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

namespace SocialManager.frm.UserControls
{
    public partial class ucAnalytics : UserControl
    {
        public ucAnalytics()
        {
            InitializeComponent();
            InitializeAnalytics();
        }

        private void InitializeAnalytics()
        {
            this.BackColor = Color.FromArgb(247, 249, 252);
            SetupTopPostsGrid();
            LoadAnalyticsData();
        }

        private void LoadAnalyticsData()
        {
            // Load performance metrics
            lblTotalReachValue.Text = "125.4K";
            lblEngagementRateValue.Text = "3.8%";
            lblNewFollowersValue.Text = "+542";
            lblClicksValue.Text = "1.2K";

            // Load platform performance chart data
            chartPlatforms.Series.Clear();
            
            var series = chartPlatforms.Series.Add("Engagement");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            series.Color = Color.FromArgb(52, 152, 219);
            
            series.Points.AddXY("Facebook", 320);
            series.Points.AddXY("Instagram", 450);
            series.Points.AddXY("Twitter", 180);
            series.Points.AddXY("LinkedIn", 120);
            series.Points.AddXY("TikTok", 290);

            // Customize chart appearance
            chartPlatforms.ChartAreas[0].BackColor = Color.White;
            chartPlatforms.ChartAreas[0].BorderWidth = 0;
            chartPlatforms.BackColor = Color.White;
            
            // Load top performing posts
            dgvTopPosts.Rows.Clear();
            dgvTopPosts.Rows.Add("New Product Launch", "?? Facebook", "1,245", "89", "4.2%", "2024-01-15");
            dgvTopPosts.Rows.Add("Behind the Scenes", "?? Instagram", "956", "67", "5.1%", "2024-01-14");
            dgvTopPosts.Rows.Add("Industry Insights", "?? LinkedIn", "723", "45", "3.8%", "2024-01-13");
            dgvTopPosts.Rows.Add("Quick Tips", "?? Twitter", "634", "23", "2.9%", "2024-01-12");
            dgvTopPosts.Rows.Add("Fun Friday", "?? TikTok", "1,087", "156", "7.2%", "2024-01-11");

            // Setup date filter
            dtpFromDate.Value = DateTime.Now.AddDays(-30);
            dtpToDate.Value = DateTime.Now;
        }

        private void SetupTopPostsGrid()
        {
            dgvTopPosts.Columns.Clear();
            
            var titleColumn = new DataGridViewTextBoxColumn
            {
                Name = "Title",
                HeaderText = "Post Title",
                FillWeight = 25
            };
            
            var platformColumn = new DataGridViewTextBoxColumn
            {
                Name = "Platform",
                HeaderText = "Platform",
                FillWeight = 15
            };
            
            var reachColumn = new DataGridViewTextBoxColumn
            {
                Name = "Reach",
                HeaderText = "Reach",
                FillWeight = 15
            };
            
            var engagementColumn = new DataGridViewTextBoxColumn
            {
                Name = "Engagement",
                HeaderText = "Engagement",
                FillWeight = 15
            };
            
            var rateColumn = new DataGridViewTextBoxColumn
            {
                Name = "Rate",
                HeaderText = "Rate",
                FillWeight = 15
            };
            
            var dateColumn = new DataGridViewTextBoxColumn
            {
                Name = "Date",
                HeaderText = "Date",
                FillWeight = 15
            };

            dgvTopPosts.Columns.AddRange(new DataGridViewColumn[] 
            {
                titleColumn, platformColumn, reachColumn, engagementColumn, rateColumn, dateColumn
            });

            // Style the DataGridView
            dgvTopPosts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvTopPosts.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvTopPosts.DefaultCellStyle.BackColor = Color.White;
            dgvTopPosts.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            dgvTopPosts.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvTopPosts.DefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            
            dgvTopPosts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvTopPosts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTopPosts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvTopPosts.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 10, 8, 10);
            dgvTopPosts.ColumnHeadersHeight = 40;
            dgvTopPosts.RowTemplate.Height = 45;
            dgvTopPosts.EnableHeadersVisualStyles = false;
            dgvTopPosts.GridColor = Color.FromArgb(234, 236, 238);
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|Excel files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Export Analytics Report";
                saveFileDialog.FileName = $"Analytics_Report_{DateTime.Now:yyyyMMdd}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show($"B�o c�o �? ��?c xu?t th�nh c�ng t?i: {saveFileDialog.FileName}", "Th�nh c�ng", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnRefreshData_Click(object sender, EventArgs e)
        {
            LoadAnalyticsData();
            MessageBox.Show("D? li?u ph�n t�ch �? ��?c l�m m?i!", "Th�nh c�ng", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtpFromDate.Value;
            DateTime toDate = dtpToDate.Value;

            if (fromDate >= toDate)
            {
                MessageBox.Show("Ng�y b?t �?u ph?i tr�?c ng�y k?t th�c.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Apply date filter (simulate)
            MessageBox.Show($"�? �p d?ng b? l?c cho kho?ng th?i gian: {fromDate:dd/MM/yyyy} to {toDate:dd/MM/yyyy}", "�? �p d?ng b? l?c", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            LoadAnalyticsData(); // Reload with filter
        }

        // Custom paint events
        private void pnlCard_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                
                // Draw shadow
                using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                {
                    GraphicsExtensions.FillRoundedRectangle(e.Graphics, shadowBrush, new Rectangle(3, 3, panel.Width - 3, panel.Height - 3), 12);
                }
                
                // Draw main card
                using (var cardBrush = new SolidBrush(Color.White))
                {
                    GraphicsExtensions.FillRoundedRectangle(e.Graphics, cardBrush, new Rectangle(0, 0, panel.Width - 3, panel.Height - 3), 12);
                }
                
                // Draw subtle border
                using (var borderPen = new Pen(Color.FromArgb(230, 230, 230), 1))
                {
                    GraphicsExtensions.DrawRoundedRectangle(e.Graphics, borderPen, new Rectangle(0, 0, panel.Width - 4, panel.Height - 4), 12);
                }
            }
        }

        private void picTotalReach_Paint(object sender, PaintEventArgs e)
        {
            DrawMetricIcon(e.Graphics, new Rectangle(5, 5, 30, 30), Color.FromArgb(52, 152, 219), "reach");
        }

        private void picEngagementRate_Paint(object sender, PaintEventArgs e)
        {
            DrawMetricIcon(e.Graphics, new Rectangle(5, 5, 30, 30), Color.FromArgb(231, 76, 60), "engagement");
        }

        private void picNewFollowers_Paint(object sender, PaintEventArgs e)
        {
            DrawMetricIcon(e.Graphics, new Rectangle(5, 5, 30, 30), Color.FromArgb(46, 204, 113), "followers");
        }

        private void picClicks_Paint(object sender, PaintEventArgs e)
        {
            DrawMetricIcon(e.Graphics, new Rectangle(5, 5, 30, 30), Color.FromArgb(230, 126, 34), "clicks");
        }

        private void DrawMetricIcon(Graphics g, Rectangle rect, Color color, string iconType)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(color))
            using (var pen = new Pen(color, 2.5f))
            {
                int centerX = rect.X + rect.Width / 2;
                int centerY = rect.Y + rect.Height / 2;
                
                switch (iconType)
                {
                    case "reach":
                        // Eye icon for reach
                        g.DrawEllipse(pen, centerX - 10, centerY - 5, 20, 10);
                        g.FillEllipse(brush, centerX - 3, centerY - 3, 6, 6);
                        break;
                        
                    case "engagement":
                        // Heart icon for engagement
                        using (var path = new GraphicsPath())
                        {
                            path.AddEllipse(centerX - 8, centerY - 5, 8, 8);
                            path.AddEllipse(centerX, centerY - 5, 8, 8);
                            path.AddPolygon(new Point[] {
                                new Point(centerX - 4, centerY + 3),
                                new Point(centerX, centerY + 12),
                                new Point(centerX + 4, centerY + 3)
                            });
                            g.FillPath(brush, path);
                        }
                        break;
                        
                    case "followers":
                        // Users icon for followers
                        g.FillEllipse(brush, centerX - 8, centerY - 8, 8, 8);
                        g.FillEllipse(brush, centerX + 2, centerY - 8, 8, 8);
                        g.FillRectangle(brush, centerX - 10, centerY + 2, 12, 6);
                        g.FillRectangle(brush, centerX, centerY + 2, 12, 6);
                        break;
                        
                    case "clicks":
                        // Cursor click icon
                        Point[] arrow = {
                            new Point(centerX - 6, centerY - 8),
                            new Point(centerX - 6, centerY + 8),
                            new Point(centerX - 1, centerY + 3),
                            new Point(centerX + 4, centerY + 8),
                            new Point(centerX + 7, centerY + 5),
                            new Point(centerX + 2, centerY),
                            new Point(centerX + 7, centerY - 5)
                        };
                        g.FillPolygon(brush, arrow);
                        break;
                }
            }
        }
    }
}
