using System;
using System.Drawing;
using System.Windows.Forms;
using SocialManager.services;

namespace SocialManager.frm
{
  public partial class frmVerifyEmail : Form
  {
    private readonly VerifyService verifyService;
    private readonly Guid userId;
    private readonly string email;
    private readonly string verificationCode;
    private int remainingSeconds = 600; // 10 phút
    private System.Windows.Forms.Timer? countdownTimer;

    public bool IsVerified { get; private set; } = false;

    public frmVerifyEmail(Guid userId, string email)
    {
      InitializeComponent();

      this.userId = userId;
      this.email = email;
      this.verifyService = new VerifyService();

      // Tạo mã verification
      this.verificationCode = verifyService.GenerateVerificationCode(userId, email);

      InitializeForm();
      StartCountdown();
    }

    private void InitializeForm()
    {
      // Hiển thị thông tin
      lblEmail.Text = $"Email: {email}";
      lblCode.Text = $"Mã xác minh: {verificationCode}";
      lblInstruction.Text = "Vui lòng nhập mã xác minh 6 số đã được gửi đến email của bạn.\n" +
                            "(Trong môi trường demo, mã sẽ hiển thị trực tiếp)";

      // Setup textbox
      txtVerificationCode.MaxLength = 6;
      txtVerificationCode.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
      txtVerificationCode.TextAlign = HorizontalAlignment.Center;

      // Setup form
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.StartPosition = FormStartPosition.CenterParent;
    }

    private void StartCountdown()
    {
      countdownTimer = new System.Windows.Forms.Timer();
      countdownTimer.Interval = 1000; // 1 giây
      countdownTimer.Tick += CountdownTimer_Tick;
      countdownTimer.Start();
    }

    private void CountdownTimer_Tick(object? sender, EventArgs e)
    {
      remainingSeconds--;

      int minutes = remainingSeconds / 60;
      int seconds = remainingSeconds % 60;

      lblTimer.Text = $"Mã hết hiệu lực sau: {minutes:D2}:{seconds:D2}";

      if (remainingSeconds <= 0)
      {
        countdownTimer?.Stop();
        MessageBox.Show("Mã xác minh đã hết hiệu lực!\nVui lòng thử lại.", "Hết thời gian",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        this.DialogResult = DialogResult.Cancel;
        this.Close();
      }
      else if (remainingSeconds <= 60)
      {
        // Đổi màu đỏ khi còn 1 phút
        lblTimer.ForeColor = Color.Red;
      }
    }

    private void btnVerify_Click(object sender, EventArgs e)
    {
      string enteredCode = txtVerificationCode.Text.Trim();

      if (string.IsNullOrEmpty(enteredCode))
      {
        MessageBox.Show("Vui lòng nhập mã xác minh!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtVerificationCode.Focus();
        return;
      }

      if (enteredCode.Length != 6)
      {
        MessageBox.Show("Mã xác minh phải có 6 số!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtVerificationCode.Focus();
        return;
      }

      // Xác minh mã
      bool isValid = verifyService.VerifyCode(userId, enteredCode);

      if (isValid)
      {
        countdownTimer?.Stop();
        IsVerified = true;

        MessageBox.Show("Xác minh email thành công!", "Thành công",
            MessageBoxButtons.OK, MessageBoxIcon.Information);

        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      else
      {
        MessageBox.Show("Mã xác minh không đúng hoặc đã hết hiệu lực!\nVui lòng kiểm tra lại.", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtVerificationCode.Clear();
        txtVerificationCode.Focus();
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      countdownTimer?.Stop();
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void btnResend_Click(object sender, EventArgs e)
    {
      // Tạo mã mới
      string newCode = verifyService.GenerateVerificationCode(userId, email);
      lblCode.Text = $"Mã xác minh: {newCode}";

      // Reset timer
      remainingSeconds = 600;
      lblTimer.ForeColor = Color.Black;

      MessageBox.Show("Đã gửi lại mã xác minh mới!", "Thông báo",
          MessageBoxButtons.OK, MessageBoxIcon.Information);

      txtVerificationCode.Clear();
      txtVerificationCode.Focus();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      countdownTimer?.Stop();
      countdownTimer?.Dispose();
      base.OnFormClosing(e);
    }
  }
}
