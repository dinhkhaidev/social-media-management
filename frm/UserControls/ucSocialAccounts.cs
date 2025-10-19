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
    public partial class ucSocialAccounts : UserControl
    {
        public ucSocialAccounts()
        {
            InitializeComponent();
            InitializeSocialAccounts();
        }

        private void InitializeSocialAccounts()
        {
            this.BackColor = Color.FromArgb(247, 249, 252);
            SetupDataGridView();
            LoadSocialAccountsData();
        }

        private void SetupDataGridView()
        {
            dgvSocialAccounts.Columns.Clear();
            
            var platformColumn = new DataGridViewTextBoxColumn
            {
                Name = "Platform",
                HeaderText = "Platform",
                FillWeight = 20
            };
            
            var usernameColumn = new DataGridViewTextBoxColumn
            {
                Name = "Username", 
                HeaderText = "Username",
                FillWeight = 25
            };
            
            var lastPostColumn = new DataGridViewTextBoxColumn
            {
                Name = "LastPost",
                HeaderText = "Last Activity",
                FillWeight = 35
            };
            
            var statusColumn = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status", 
                FillWeight = 20
            };

            dgvSocialAccounts.Columns.AddRange(new DataGridViewColumn[] 
            {
                platformColumn, usernameColumn, lastPostColumn, statusColumn
            });

            // Style the DataGridView
            dgvSocialAccounts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvSocialAccounts.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvSocialAccounts.DefaultCellStyle.BackColor = Color.White;
            dgvSocialAccounts.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            dgvSocialAccounts.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvSocialAccounts.DefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            
            dgvSocialAccounts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvSocialAccounts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSocialAccounts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvSocialAccounts.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 12, 10, 12);
            dgvSocialAccounts.ColumnHeadersHeight = 45;
            dgvSocialAccounts.RowTemplate.Height = 50;
            dgvSocialAccounts.EnableHeadersVisualStyles = false;
            dgvSocialAccounts.GridColor = Color.FromArgb(234, 236, 238);
        }

        private void LoadSocialAccountsData()
        {
            // Add sample social accounts data with better formatting
            dgvSocialAccounts.Rows.Clear();
            dgvSocialAccounts.Rows.Add("?? Facebook", "@mycompany", "Product launch announcement - 2 hours ago", "? Connected");
            dgvSocialAccounts.Rows.Add("?? Twitter", "@mycompany", "Quick industry update - 4 hours ago", "? Connected");
            dgvSocialAccounts.Rows.Add("?? Instagram", "@mycompany", "Behind-the-scenes story - 1 day ago", "? Connected");
            dgvSocialAccounts.Rows.Add("?? LinkedIn", "@mycompany", "Professional milestone post - 3 days ago", "? Disconnected");
            dgvSocialAccounts.Rows.Add("?? TikTok", "@mycompany", "Trending video content - 1 week ago", "?? Pending");
            dgvSocialAccounts.Rows.Add("?? Pinterest", "@mycompany", "Creative board update - 5 days ago", "? Connected");
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            // Show add account dialog (simulate)
            MessageBox.Show("Add Account feature coming soon!\n\nThis will open a dialog to connect new social media accounts.", 
                "Add Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (dgvSocialAccounts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an account to disconnect.", "Info", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvSocialAccounts.SelectedRows[0];
            string platform = selectedRow.Cells["Platform"].Value.ToString();

            var result = MessageBox.Show($"Are you sure you want to disconnect {platform}?", 
                "Confirm Disconnect", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                selectedRow.Cells["Status"].Value = "? Disconnected";
                selectedRow.Cells["LastPost"].Value = "Account disconnected";
                MessageBox.Show($"{platform} has been disconnected!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSocialAccounts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an account to edit.", "Info", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("Edit Account feature coming soon!", "Info", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSocialAccountsData();
            MessageBox.Show("Social accounts data refreshed!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnConnectAccount_Click(object sender, EventArgs e)
        {
            if (cmbPlatformSelect.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a platform to connect.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAccountUsername.Text))
            {
                MessageBox.Show("Please enter an account username.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Simulate account connection
            string platform = cmbPlatformSelect.Text;
            string username = txtAccountUsername.Text;

            // Add to grid
            dgvSocialAccounts.Rows.Insert(0, platform, $"@{username}", "Just connected", "? Connected");

            MessageBox.Show($"Successfully connected to {platform}!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear form
            cmbPlatformSelect.SelectedIndex = -1;
            txtAccountUsername.Clear();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (dgvSocialAccounts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an account to test.", "Info", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvSocialAccounts.SelectedRows[0];
            string platform = selectedRow.Cells["Platform"].Value.ToString();

            // Simulate connection test
            MessageBox.Show($"Testing connection to {platform}...\n\n? Connection successful!", 
                "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
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