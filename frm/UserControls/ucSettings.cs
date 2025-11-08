using SocialManager.utils;
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
      ApplyRoundedCorners();
    }

    private void ApplyRoundedCorners()
    {
      try
      {
        // Apply rounded corners to textboxes
        if (txtAppName != null)
          UIHelper.ApplyRoundedCorners(txtAppName, 10);

        if (txtUserName != null)
          UIHelper.ApplyRoundedCorners(txtUserName, 10);

        if (txtEmail != null)
          UIHelper.ApplyRoundedCorners(txtEmail, 10);

        // Apply rounded corners to buttons
        if (btnSaveSettings != null)
          UIHelper.ApplyRoundedCorners(btnSaveSettings, 8);

        if (btnResetSettings != null)
          UIHelper.ApplyRoundedCorners(btnResetSettings, 8);

        if (btnChangePassword != null)
          UIHelper.ApplyRoundedCorners(btnChangePassword, 8);

        if (btnBackupData != null)
          UIHelper.ApplyRoundedCorners(btnBackupData, 8);

        if (btnRestoreData != null)
          UIHelper.ApplyRoundedCorners(btnRestoreData, 8);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error applying rounded corners: {ex.Message}");
      }
    }

    private void LoadSettings()
    {
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
        MessageBox.Show("Vui lòng nhập tên ứng dụng.", "Lỗi xác thực",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      if (string.IsNullOrWhiteSpace(txtUserName.Text))
      {
        MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Lỗi xác thực",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      // Save settings (simulate)
      MessageBox.Show("Đã lưu cài đặt thành công!", "Thành công",
          MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnResetSettings_Click(object sender, EventArgs e)
    {
      var result = MessageBox.Show("Bạn có chắc muốn đặt lại tất cả cài đặt về mặc định?",
          "Xác nhận đặt lại", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

      if (result == DialogResult.Yes)
      {
        LoadSettings();
        MessageBox.Show("Đã đặt lại cài đặt về giá trị mặc định!", "Thành công",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private void btnChangePassword_Click(object sender, EventArgs e)
    {
      MessageBox.Show("Tính năng đổi mật khẩu sẽ sớm có!", "Thông tin",
          MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnBackupData_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog saveFileDialog = new SaveFileDialog())
      {
        saveFileDialog.Filter = "Backup files (*.bak)|*.bak";
        saveFileDialog.Title = "Sao lưu dữ liệu ứng dụng";
        saveFileDialog.FileName = $"SocialManager_Backup_{DateTime.Now:yyyyMMdd}";

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
          MessageBox.Show($"Sao lưu dữ liệu thành công tới: {saveFileDialog.FileName}", "Thành công",
              MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      }
    }

    private void btnRestoreData_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog openFileDialog = new OpenFileDialog())
      {
        openFileDialog.Filter = "Backup files (*.bak)|*.bak";
        openFileDialog.Title = "Khôi phục dữ liệu ứng dụng";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
          var result = MessageBox.Show("Bạn có chắc muốn khôi phục dữ liệu? Hành động này sẽ ghi đè dữ liệu hiện tại.",
              "Xác nhận khôi phục", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

          if (result == DialogResult.Yes)
          {
            MessageBox.Show("Đã khôi phục dữ liệu thành công!", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
        }
      }
    }

    private void pnlCard_Paint(object sender, PaintEventArgs e)
    {
      Panel? panel = sender as Panel;
      if (panel != null)
      {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
        {
          GraphicsExtensions.FillRoundedRectangle(e.Graphics, shadowBrush, new Rectangle(3, 3, panel.Width - 3, panel.Height - 3), 12);
        }

        using (var cardBrush = new SolidBrush(Color.White))
        {
          GraphicsExtensions.FillRoundedRectangle(e.Graphics, cardBrush, new Rectangle(0, 0, panel.Width - 3, panel.Height - 3), 12);
        }

        using (var borderPen = new Pen(Color.FromArgb(230, 230, 230), 1))
        {
          GraphicsExtensions.DrawRoundedRectangle(e.Graphics, borderPen, new Rectangle(0, 0, panel.Width - 4, panel.Height - 4), 12);
        }
      }
    }
  }
}
