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
    public partial class ucPosts : UserControl
    {
        public ucPosts()
        {
            InitializeComponent();
            InitializePosts();
        }

        private void InitializePosts()
        {
            this.BackColor = Color.FromArgb(247, 249, 252);
            LoadPostsData();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvPosts.Columns.Clear();
            
            var titleColumn = new DataGridViewTextBoxColumn
            {
                Name = "Title",
                HeaderText = "Post Title",
                FillWeight = 30
            };
            
            var platformColumn = new DataGridViewTextBoxColumn
            {
                Name = "Platform",
                HeaderText = "Platform",
                FillWeight = 15
            };
            
            var statusColumn = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                FillWeight = 15
            };
            
            var dateColumn = new DataGridViewTextBoxColumn
            {
                Name = "Date",
                HeaderText = "Published Date",
                FillWeight = 20
            };
            
            var engagementColumn = new DataGridViewTextBoxColumn
            {
                Name = "Engagement",
                HeaderText = "Engagement",
                FillWeight = 20
            };

            dgvPosts.Columns.AddRange(new DataGridViewColumn[] 
            {
                titleColumn, platformColumn, statusColumn, dateColumn, engagementColumn
            });

            // Style the DataGridView
            dgvPosts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvPosts.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvPosts.DefaultCellStyle.BackColor = Color.White;
            dgvPosts.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            dgvPosts.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvPosts.DefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            
            dgvPosts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvPosts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPosts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvPosts.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 12, 10, 12);
            dgvPosts.ColumnHeadersHeight = 45;
            dgvPosts.RowTemplate.Height = 55;
            dgvPosts.EnableHeadersVisualStyles = false;
            dgvPosts.GridColor = Color.FromArgb(234, 236, 238);
        }

        private void LoadPostsData()
        {
            cmbPlatform.SelectedIndex = 0;
            
            // Sample posts data
            dgvPosts.Rows.Clear();
            dgvPosts.Rows.Add("New Product Launch Announcement", "?? Facebook", "? Published", "2024-01-15 10:30", "254 likes, 18 comments");
            dgvPosts.Rows.Add("Behind the Scenes: Our Team", "?? Instagram", "? Published", "2024-01-14 16:45", "189 likes, 12 comments");
            dgvPosts.Rows.Add("Industry Insights and Trends", "?? LinkedIn", "? Published", "2024-01-14 09:15", "87 likes, 25 comments");
            dgvPosts.Rows.Add("Quick Tips for Success", "?? Twitter", "? Scheduled", "2024-01-16 14:00", "Scheduled");
            dgvPosts.Rows.Add("Fun Friday Video", "?? TikTok", "?? Draft", "Not published", "Draft");
            dgvPosts.Rows.Add("Customer Success Story", "?? Facebook", "? Published", "2024-01-13 12:20", "312 likes, 34 comments");
            dgvPosts.Rows.Add("Weekly Motivation Quote", "?? Instagram", "? Published", "2024-01-12 08:00", "445 likes, 67 comments");
        }

        private void btnCreatePost_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPostTitle.Text))
            {
                MessageBox.Show("Please enter a post title.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPostContent.Text))
            {
                MessageBox.Show("Please enter post content.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbPlatform.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a platform.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create new post
            string title = txtPostTitle.Text;
            string platform = cmbPlatform.Text;
            string status = chkSchedulePost.Checked ? "? Scheduled" : "? Published";
            string date = chkSchedulePost.Checked ? dtpScheduleDate.Value.ToString("yyyy-MM-dd HH:mm") : DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string engagement = chkSchedulePost.Checked ? "Scheduled" : "New post";

            dgvPosts.Rows.Insert(0, title, platform, status, date, engagement);

            MessageBox.Show("Post created successfully!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear form
            txtPostTitle.Clear();
            txtPostContent.Clear();
            cmbPlatform.SelectedIndex = 0;
            chkSchedulePost.Checked = false;
        }

        private void btnEditPost_Click(object sender, EventArgs e)
        {
            if (dgvPosts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a post to edit.", "Info", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("Edit post feature coming soon!", "Info", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeletePost_Click(object sender, EventArgs e)
        {
            if (dgvPosts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a post to delete.", "Info", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this post?", "Confirm Delete", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dgvPosts.Rows.RemoveAt(dgvPosts.SelectedRows[0].Index);
                MessageBox.Show("Post deleted successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRefreshPosts_Click(object sender, EventArgs e)
        {
            LoadPostsData();
            MessageBox.Show("Posts data refreshed!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkSchedulePost_CheckedChanged(object sender, EventArgs e)
        {
            dtpScheduleDate.Enabled = chkSchedulePost.Checked;
            if (chkSchedulePost.Checked)
            {
                dtpScheduleDate.Value = DateTime.Now.AddHours(1);
            }
        }

        private void btnAddPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Select an image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show($"Image selected: {Path.GetFileName(openFileDialog.FileName)}", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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
    }
}