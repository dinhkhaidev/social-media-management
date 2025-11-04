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
    public partial class ucSettings : UserControl
    {
        public ucSettings()
        {
            InitializeComponent();
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            this.BackColor = Color.FromArgb(247, 249, 252);
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Load current settings
            txtAppName.Text = "Social Media Manager";
            txtUserName.Text = "Administrator";
            txtEmail.Text = "admin@socialmedia.com";
            chkNotifications.Checked = true;
            chkAutoSchedule.Checked = false;
            cmbTheme.SelectedIndex = 0;
            cmbLanguage.SelectedIndex = 0;
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppName.Text))
            {
                MessageBox.Show("Vui l?ng nh?p t�n ?ng d?ng.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Vui l?ng nh?p t�n ��ng nh?p.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Save settings (simulate)
            MessageBox.Show("�? l�u c�i �?t th�nh c�ng!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnResetSettings_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("B?n c� ch?c ch?n mu?n �?t l?i t?t c? c�i �?t v? m?c �?nh?", 
                "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                LoadSettings(); // Reset to defaults
                MessageBox.Show("�? �?t l?i c�i �?t v? gi� tr? m?c �?nh!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            MessageBox.Show("T�nh n�ng �?i m?t kh?u s? s?m c�!", "Th�ng tin", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBackupData_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Backup files (*.bak)|*.bak";
                saveFileDialog.Title = "Backup Application Data";
                saveFileDialog.FileName = $"SocialManager_Backup_{DateTime.Now:yyyyMMdd}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show($"Sao l�u d? li?u th�nh c�ng t?i: {saveFileDialog.FileName}", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnRestoreData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Backup files (*.bak)|*.bak";
                openFileDialog.Title = "Restore Application Data";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var result = MessageBox.Show("B?n c� ch?c ch?n mu?n kh�i ph?c d? li?u? �i?u n�y s? ghi �� d? li?u hi?n t?i.", 
                        "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        MessageBox.Show("�? kh�i ph?c d? li?u th�nh c�ng!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
