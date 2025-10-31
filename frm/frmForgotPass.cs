using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocialManager.controls;
using SocialManager.services;

namespace SocialManager.frm
{
  public partial class frmForgotPass : Form
  {
    private User? currentUser;
    private UserService userService;
    private bool isPasswordValid = false;

    public frmForgotPass()
    {
      InitializeComponent();

      // Khởi tạo service
      userService = new UserService();
      currentUser = AuthSessionService.CurrentUser;

      // Kiểm tra user đã đăng nhập chưa
      if (currentUser == null)
      {
        MessageBox.Show("Bạn cần đăng nhập để đổi mật khẩu!", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        this.Close();
        return;
      }

      // Thêm sự kiện
      this.txtPass2.TextChanged += new EventHandler(this.txtPass2_TextChanged!);
      this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click!);
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click!);
      this.FormClosing += new FormClosingEventHandler(this.frmForgotPass_FormClosing!);

      // Thiết lập placeholder
      txtPass1.PlaceholderText = "Nhập mật khẩu mới...";
      txtPass2.PlaceholderText = "Nhập lại mật khẩu mới...";
    }

    private void txtPass2_TextChanged(object? sender, EventArgs e)
    {
      // Kiểm tra mật khẩu khớp trong real-time
      if (!string.IsNullOrEmpty(txtPass2.Text))
      {
        if (txtPass2.Text == txtPass1.Text)
        {
          // Mật khẩu khớp
          txtPass2.BorderColor = Color.FromArgb(46, 204, 113); // Xanh lá
          isPasswordValid = true;
        }
        else
        {
          // Mật khẩu không khớp
          txtPass2.BorderColor = Color.FromArgb(231, 76, 60); // Đỏ
          isPasswordValid = false;
        }
      }
      else
      {
        txtPass2.BorderColor = Color.LightGray;
        isPasswordValid = false;
      }
    }

    private void btnUpdate_Click(object? sender, EventArgs e)
    {
      // Validate input
      if (!ValidateInput())
        return;

      try
      {
        // Cập nhật mật khẩu
        string newPassword = txtPass1.Text ?? "";
        bool success = UpdatePassword(newPassword);

        if (success)
        {
          MessageBox.Show("Cập nhật mật khẩu thành công!\n\nVui lòng đăng nhập lại với mật khẩu mới.",
              "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

          // Đăng xuất user
          AuthSessionService.Logout();

          this.DialogResult = DialogResult.OK;
          this.Close();
        }
        else
        {
          MessageBox.Show("Không thể cập nhật mật khẩu.\nVui lòng thử lại!",
              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi cập nhật mật khẩu:\n{ex.Message}",
            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private bool ValidateInput()
    {
      // Kiểm tra trường rỗng
      if (string.IsNullOrWhiteSpace(txtPass1.Text))
      {
        MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Cảnh báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtPass1.Focus();
        return false;
      }

      if (string.IsNullOrWhiteSpace(txtPass2.Text))
      {
        MessageBox.Show("Vui lòng xác nhận mật khẩu!", "Cảnh báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtPass2.Focus();
        return false;
      }

      // Kiểm tra độ dài mật khẩu
      if (txtPass1.Text.Length < 6)
      {
        MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Cảnh báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtPass1.Focus();
        return false;
      }

      // Kiểm tra mật khẩu khớp
      if (txtPass1.Text != txtPass2.Text)
      {
        MessageBox.Show("Mật khẩu xác nhận không khớp!\nVui lòng nhập lại.", "Cảnh báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtPass2.Clear();
        txtPass2.Focus();
        return false;
      }

      // Kiểm tra mật khẩu mới khác mật khẩu cũ
      if (currentUser != null && txtPass1.Text == currentUser.Password)
      {
        MessageBox.Show("Mật khẩu mới phải khác mật khẩu cũ!", "Cảnh báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtPass1.Clear();
        txtPass2.Clear();
        txtPass1.Focus();
        return false;
      }

      return true;
    }

    private bool UpdatePassword(string newPassword)
    {
      if (currentUser == null)
        return false;

      try
      {
        // Gọi UserService để cập nhật password
        bool success = userService.UpdatePassword(currentUser.UserID, newPassword);

        if (success)
        {
          // Cập nhật trong currentUser
          currentUser.Password = newPassword;
        }

        return success;
      }
      catch (Exception ex)
      {
        throw new Exception($"Lỗi khi cập nhật mật khẩu: {ex.Message}");
      }
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void frmForgotPass_FormClosing(object? sender, FormClosingEventArgs e)
    {
      // Chỉ hỏi xác nhận nếu đã nhập dữ liệu và chưa update
      if (!string.IsNullOrWhiteSpace(txtPass1.Text) || !string.IsNullOrWhiteSpace(txtPass2.Text))
      {
        if (this.DialogResult != DialogResult.OK)
        {
          DialogResult result = MessageBox.Show(
              "Bạn có chắc chắn muốn thoát mà không cập nhật mật khẩu?",
              "Xác nhận",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);

          if (result == DialogResult.No)
          {
            e.Cancel = true;
          }
        }
      }
    }
  }
}
