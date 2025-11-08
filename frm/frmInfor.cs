using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocialManager.services;
using SocialManager.utils;

namespace SocialManager.frm
{
  public partial class frmInfor : Form
  {
    private User? currentUser;
    private UserService? userService;
    private bool isDarkMode = false; // Theme state
    private const string DarkModeKey = "isDarkMode"; // Registry key

    public frmInfor()
    {
      InitializeComponent();

      // Skip initialization in design mode
      if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
      {
        return;
      }

      // Khởi tạo service
      userService = new UserService();

      // Đăng ký sự kiện
      this.lnkForgotPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkForgotPassword_LinkClicked);
      this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click!);
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click!);

      // Load theme từ Dashboard
      isDarkMode = LoadDarkModePreference();
      ApplyTheme(isDarkMode);

      LoadUserData();
      ApplyRoundedCorners();
    }

    private void ApplyRoundedCorners()
    {
      try
      {
        // Apply rounded corners to buttons
        UIHelper.ApplyRoundedCorners(btnUpdate, 10);
        UIHelper.ApplyRoundedCorners(btnCancel, 10);

        // Avatar circular
        if (picAvatar != null)
          UIHelper.MakeCircular(picAvatar);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error applying rounded corners: {ex.Message}");
      }
    }

    private void LoadUserData()
    {
      currentUser = AuthSessionService.CurrentUser;

      if (currentUser == null)
      {
        MessageBox.Show("Không tìm thấy thông tin người dùng!", "Lỗi",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
        this.Close();
        return;
      }

      txtUserName.Text = currentUser.UserName;
      txtPassword.Text = "********";
      txtFullName.Text = currentUser.FullName;

      if (currentUser.Gender == 0)
        cboGender.SelectedIndex = 0; // Male
      else if (currentUser.Gender == 1)
        cboGender.SelectedIndex = 1; // Female
      else
        cboGender.SelectedIndex = 2; // Other

      // Xử lý DateTimePicker với giới hạn an toàn
      try
      {
        if (currentUser.DOB >= dtpDOB.MinDate && currentUser.DOB <= dtpDOB.MaxDate)
        {
          dtpDOB.Value = currentUser.DOB;
        }
        else
        {
          // Nếu ngày sinh nằm ngoài phạm vi, set về giá trị mặc định
          dtpDOB.Value = DateTime.Now.AddYears(-18);
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error setting DOB: {ex.Message}");
        dtpDOB.Value = DateTime.Now.AddYears(-18);
      }

      txtEmail.Text = currentUser.Email ?? "";
      txtPhone.Text = currentUser.Phone ?? "";
      txtAddress.Text = currentUser.Address ?? "";

      lblCreatedAt.Text = $"Tạo lúc: {currentUser.CreatedAt:dd/MM/yyyy HH:mm}";

      string status = currentUser.StatusId switch
      {
        1 => "Active",
        0 => "Inactive",
        -1 => "Banned",
        _ => "Unknown"
      };
      lblStatusId.Text = $"Trạng thái: {status}";

      // Lấy số lần bị tố cáo từ ReportService
      int reportCount = userService?.GetReportCount(currentUser.UserID) ?? 0;
      currentUser.ReportCount = reportCount; // Sync lại

      // Hiển thị thông tin vi phạm và trạng thái tài khoản
      if (reportCount > 0)
      {
        lblStatusId.Text += $"\n\nVi phạm: {reportCount} lần";
        lblStatusId.Text += $"\nTình trạng: {currentUser.AccountStatus}";

        // Đổi màu cảnh báo
        if (reportCount >= 100)
        {
          lblStatusId.ForeColor = Color.Red;
        }
        else if (reportCount >= 50)
        {
          lblStatusId.ForeColor = Color.Orange;
        }
        else
        {
          lblStatusId.ForeColor = isDarkMode ? Color.FromArgb(200, 200, 200) : Color.FromArgb(66, 66, 66);
        }
      }

      LoadAvatar();
    }

    private void LoadAvatar()
    {
      if (currentUser != null && !string.IsNullOrEmpty(currentUser.AvatarUrl) && System.IO.File.Exists(currentUser.AvatarUrl))
      {
        try
        {
          picAvatar.Image = Image.FromFile(currentUser.AvatarUrl);
          picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
        }
        catch
        {
          // Nếu load lỗi, để avatar mặc định
          picAvatar.BackColor = Color.LightGray;
        }
      }
      else
      {
        // Avatar mặc định
        picAvatar.BackColor = Color.LightGray;
      }
    }

    private void lnkForgotPassword_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
      frmForgotPass forgotPassForm = new frmForgotPass();
      DialogResult result = forgotPassForm.ShowDialog();

      // Nếu đổi mật khẩu thành công, đóng form Infor và quay về Dashboard
      if (result == DialogResult.OK)
      {
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
    }

    private void btnUpdate_Click(object? sender, EventArgs e)
    {
      // Validate input
      if (!ValidateInput())
        return;

      try
      {
        // Cập nhật thông tin user
        bool success = UpdateUserInfo();

        if (success)
        {
          MessageBox.Show("Cập nhật thông tin thành công!", "Thành công",
              MessageBoxButtons.OK, MessageBoxIcon.Information);

          // Reload lại dữ liệu
          LoadUserData();
        }
        else
        {
          MessageBox.Show("Không thể cập nhật thông tin.\nVui lòng thử lại!", "Lỗi",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi cập nhật thông tin:\n{ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private bool ValidateInput()
    {
      // Kiểm tra Username
      if (string.IsNullOrWhiteSpace(txtUserName.Text))
      {
        MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Cảnh báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtUserName.Focus();
        return false;
      }

      // Kiểm tra Full Name
      if (string.IsNullOrWhiteSpace(txtFullName.Text))
      {
        MessageBox.Show("Vui lòng nhập họ tên!", "Cảnh báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtFullName.Focus();
        return false;
      }

      // Kiểm tra Email format
      if (!string.IsNullOrWhiteSpace(txtEmail.Text))
      {
        if (!IsValidEmail(txtEmail.Text))
        {
          MessageBox.Show("Email không hợp lệ!", "Cảnh báo",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtEmail.Focus();
          return false;
        }
      }

      // Kiểm tra Phone format
      if (!string.IsNullOrWhiteSpace(txtPhone.Text))
      {
        if (!IsValidPhone(txtPhone.Text))
        {
          MessageBox.Show("Số điện thoại không hợp lệ!", "Cảnh báo",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtPhone.Focus();
          return false;
        }
      }

      return true;
    }

    private bool IsValidEmail(string email)
    {
      try
      {
        var addr = new System.Net.Mail.MailAddress(email);
        return addr.Address == email;
      }
      catch
      {
        return false;
      }
    }

    private bool IsValidPhone(string phone)
    {
      // Kiểm tra số điện thoại (chỉ cho phép số và dấu +, -, khoảng trắng)
      string pattern = @"^[\d\s\-\+\(\)]+$";
      return System.Text.RegularExpressions.Regex.IsMatch(phone, pattern) && phone.Length >= 10;
    }

    private bool UpdateUserInfo()
    {
      if (currentUser == null)
        return false;

      try
      {
        // Check if services are initialized
        if (currentUser == null || userService == null)
        {
          throw new Exception("Không tìm thấy thông tin người dùng hoặc dịch vụ chưa được khởi tạo.");
        }

        // Cập nhật thông tin vào currentUser object
        currentUser.UserName = txtUserName.Text?.Trim() ?? "";
        currentUser.FullName = txtFullName.Text?.Trim() ?? "";
        currentUser.Email = txtEmail.Text?.Trim() ?? "";
        currentUser.Phone = txtPhone.Text?.Trim() ?? "";
        currentUser.Gender = cboGender.SelectedIndex;
        currentUser.DOB = dtpDOB.Value;
        currentUser.Address = txtAddress.Text?.Trim() ?? "";

        // Gọi UserService để cập nhật
        bool success = userService.UpdateUser(currentUser);

        if (success)
        {
          // Cập nhật lại AuthSessionService
          AuthSessionService.UpdateCurrentUser(currentUser);
        }

        return success;
      }
      catch (Exception ex)
      {
        throw new Exception($"Lỗi khi cập nhật thông tin: {ex.Message}");
      }
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
      this.Close();
    }

    // Theme management - đồng bộ với frmDashboard
    private bool LoadDarkModePreference()
    {
      try
      {
        using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\SocialManager"))
        {
          if (key != null)
          {
            object? value = key.GetValue(DarkModeKey);
            if (value != null)
            {
              return (int)value == 1;
            }
          }
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error loading dark mode preference: {ex.Message}");
      }
      return false;
    }

    private void ApplyTheme(bool darkMode)
    {
      if (darkMode)
      {
        // Dark theme
        this.BackColor = Color.FromArgb(24, 25, 26);

        // GroupBoxes
        gbAccount.ForeColor = Color.FromArgb(242, 242, 242);
        gbDetails.ForeColor = Color.FromArgb(242, 242, 242);

        // Labels
        foreach (Control ctrl in gbAccount.Controls)
        {
          if (ctrl is Label lbl)
          {
            lbl.ForeColor = Color.FromArgb(200, 200, 200);
          }
        }

        foreach (Control ctrl in gbDetails.Controls)
        {
          if (ctrl is Label lbl)
          {
            lbl.ForeColor = Color.FromArgb(200, 200, 200);
          }
        }

        // Buttons
        btnUpdate.BackColor = Color.FromArgb(10, 102, 194);
        btnUpdate.ForeColor = Color.White;
        btnCancel.BackColor = Color.FromArgb(60, 60, 60);
        btnCancel.ForeColor = Color.FromArgb(242, 242, 242);
      }
      else
      {
        // Light theme (default)
        this.BackColor = Color.FromArgb(240, 242, 245);

        // GroupBoxes
        gbAccount.ForeColor = Color.FromArgb(33, 33, 33);
        gbDetails.ForeColor = Color.FromArgb(33, 33, 33);

        // Labels
        foreach (Control ctrl in gbAccount.Controls)
        {
          if (ctrl is Label lbl)
          {
            lbl.ForeColor = Color.FromArgb(66, 66, 66);
          }
        }

        foreach (Control ctrl in gbDetails.Controls)
        {
          if (ctrl is Label lbl)
          {
            lbl.ForeColor = Color.FromArgb(66, 66, 66);
          }
        }

        // Buttons
        btnUpdate.BackColor = Color.FromArgb(10, 102, 194);
        btnUpdate.ForeColor = Color.White;
        btnCancel.BackColor = Color.FromArgb(228, 230, 235);
        btnCancel.ForeColor = Color.FromArgb(33, 33, 33);
      }
    }
  }
}
