namespace SocialManager.frm
{
  partial class frmInfor
  {
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
      picAvatar = new PictureBox();
      lblUserName = new Label();
      txtUserName = new SocialManager.controls.RoundedTextBox();
      lblPassword = new Label();
      txtPassword = new SocialManager.controls.RoundedTextBox();
      lnkForgotPassword = new LinkLabel();
      gbAccount = new GroupBox();
      gbDetails = new GroupBox();
      dtpDOB = new DateTimePicker();
      cboGender = new ComboBox();
      txtAddress = new SocialManager.controls.RoundedTextBox();
      lblAddress = new Label();
      txtPhone = new SocialManager.controls.RoundedTextBox();
      lblPhone = new Label();
      txtEmail = new SocialManager.controls.RoundedTextBox();
      lblEmail = new Label();
      lblDOB = new Label();
      lblGender = new Label();
      txtFullName = new SocialManager.controls.RoundedTextBox();
      lblFullName = new Label();
      lblCreatedAt = new Label();
      lblStatusId = new Label();
      btnCancel = new Button();
      btnUpdate = new Button();
      ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
      gbAccount.SuspendLayout();
      gbDetails.SuspendLayout();
      SuspendLayout();
      //
      // picAvatar
      //
      picAvatar.BackColor = Color.Gainsboro;
      picAvatar.Cursor = Cursors.Hand;
      picAvatar.Location = new Point(18, 35);
      picAvatar.Name = "picAvatar";
      picAvatar.Size = new Size(100, 100);
      picAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
      picAvatar.TabIndex = 18;
      picAvatar.TabStop = false;
      //
      // lblUserName
      //
      lblUserName.AutoSize = true;
      lblUserName.Font = new Font("Segoe UI", 9F);
      lblUserName.Location = new Point(145, 38);
      lblUserName.Name = "lblUserName";
      lblUserName.Size = new Size(96, 20);
      lblUserName.TabIndex = 0;
      lblUserName.Text = "Tên đăng nhập";
      //
      // txtUserName
      //
      txtUserName.BackColor = SystemColors.Window;
      txtUserName.BorderColor = Color.LightGray;
      txtUserName.BorderFocusColor = Color.FromArgb(0, 123, 255);
      txtUserName.BorderRadius = 15;
      txtUserName.BorderSize = 1;
      txtUserName.Font = new Font("Segoe UI", 10F);
      txtUserName.ForeColor = Color.FromArgb(64, 64, 64);
      txtUserName.Location = new Point(139, 48);
      txtUserName.Multiline = false;
      txtUserName.Name = "txtUserName";
      txtUserName.Padding = new Padding(10, 7, 10, 7);
      txtUserName.PasswordChar = false;
      txtUserName.PlaceholderColor = Color.DarkGray;
      txtUserName.PlaceholderText = "";
      txtUserName.Size = new Size(280, 38);
      txtUserName.TabIndex = 0;
      txtUserName.UnderlinedStyle = false;
      //
      // lblPassword
      //
      lblPassword.AutoSize = true;
      lblPassword.Font = new Font("Segoe UI", 9F);
      lblPassword.Location = new Point(37, 98);
      lblPassword.Name = "lblPassword";
      lblPassword.Size = new Size(70, 20);
      lblPassword.TabIndex = 1;
      lblPassword.Text = "Mật khẩu";
      //
      // txtPassword
      //
      txtPassword.BackColor = SystemColors.Window;
      txtPassword.BorderColor = Color.LightGray;
      txtPassword.BorderFocusColor = Color.FromArgb(0, 123, 255);
      txtPassword.BorderRadius = 15;
      txtPassword.BorderSize = 1;
      txtPassword.Font = new Font("Segoe UI", 10F);
      txtPassword.ForeColor = Color.FromArgb(64, 64, 64);
      txtPassword.Location = new Point(139, 118);
      txtPassword.Multiline = false;
      txtPassword.Name = "txtPassword";
      txtPassword.Padding = new Padding(10, 7, 10, 7);
      txtPassword.PasswordChar = true;
      txtPassword.PlaceholderColor = Color.DarkGray;
      txtPassword.PlaceholderText = "••••••••";
      txtPassword.Size = new Size(280, 38);
      txtPassword.TabIndex = 1;
      txtPassword.UnderlinedStyle = false;
      //
      // lnkForgotPassword
      //
      lnkForgotPassword.AutoSize = true;
      lnkForgotPassword.Font = new Font("Segoe UI", 9F);
      lnkForgotPassword.Location = new Point(301, 159);
      lnkForgotPassword.Name = "lnkForgotPassword";
      lnkForgotPassword.Size = new Size(120, 20);
      lnkForgotPassword.TabIndex = 2;
      lnkForgotPassword.TabStop = true;
      lnkForgotPassword.Text = "Quên mật khẩu";
      //
      // gbAccount
      //
      gbAccount.Controls.Add(picAvatar);
      gbAccount.Controls.Add(lnkForgotPassword);
      gbAccount.Controls.Add(lblUserName);
      gbAccount.Controls.Add(txtPassword);
      gbAccount.Controls.Add(txtUserName);
      gbAccount.Controls.Add(lblPassword);
      gbAccount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
      gbAccount.Location = new Point(22, 22);
      gbAccount.Name = "gbAccount";
      gbAccount.Size = new Size(440, 190);
      gbAccount.TabIndex = 0;
      gbAccount.TabStop = false;
      gbAccount.Text = "Thông tin tài khoản";
      //
      // gbDetails
      //
      gbDetails.Controls.Add(dtpDOB);
      gbDetails.Controls.Add(cboGender);
      gbDetails.Controls.Add(txtAddress);
      gbDetails.Controls.Add(lblAddress);
      gbDetails.Controls.Add(txtPhone);
      gbDetails.Controls.Add(lblPhone);
      gbDetails.Controls.Add(txtEmail);
      gbDetails.Controls.Add(lblEmail);
      gbDetails.Controls.Add(lblDOB);
      gbDetails.Controls.Add(lblGender);
      gbDetails.Controls.Add(txtFullName);
      gbDetails.Controls.Add(lblFullName);
      gbDetails.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
      gbDetails.Location = new Point(22, 228);
      gbDetails.Name = "gbDetails";
      gbDetails.Size = new Size(440, 520);
      gbDetails.TabIndex = 1;
      gbDetails.TabStop = false;
      gbDetails.Text = "Thông tin cá nhân";
      //
      // dtpDOB
      //
      dtpDOB.Font = new Font("Segoe UI", 10F);
      dtpDOB.Location = new Point(22, 248);
      dtpDOB.Name = "dtpDOB";
      dtpDOB.Size = new Size(397, 30);
      dtpDOB.TabIndex = 2;
      dtpDOB.MinDate = new DateTime(1900, 1, 1);
      dtpDOB.MaxDate = DateTime.Now;
      //
      // cboGender
      //
      cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
      cboGender.Font = new Font("Segoe UI", 10F);
      cboGender.FormattingEnabled = true;
      cboGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
      cboGender.Location = new Point(22, 178);
      cboGender.Name = "cboGender";
      cboGender.Size = new Size(397, 31);
      cboGender.TabIndex = 1;
      //
      // txtAddress
      //
      txtAddress.BackColor = SystemColors.Window;
      txtAddress.BorderColor = Color.LightGray;
      txtAddress.BorderFocusColor = Color.FromArgb(0, 123, 255);
      txtAddress.BorderRadius = 15;
      txtAddress.BorderSize = 1;
      txtAddress.Font = new Font("Segoe UI", 10F);
      txtAddress.ForeColor = Color.FromArgb(64, 64, 64);
      txtAddress.Location = new Point(22, 468);
      txtAddress.Multiline = false;
      txtAddress.Name = "txtAddress";
      txtAddress.Padding = new Padding(10, 7, 10, 7);
      txtAddress.PasswordChar = false;
      txtAddress.PlaceholderColor = Color.DarkGray;
      txtAddress.PlaceholderText = "";
      txtAddress.Size = new Size(397, 38);
      txtAddress.TabIndex = 5;
      txtAddress.UnderlinedStyle = false;
      //
      // lblAddress
      //
      lblAddress.AutoSize = true;
      lblAddress.Font = new Font("Segoe UI", 9F);
      lblAddress.Location = new Point(18, 445);
      lblAddress.Name = "lblAddress";
      lblAddress.Size = new Size(62, 20);
      lblAddress.TabIndex = 16;
      lblAddress.Text = "Địa chỉ";
      //
      // txtPhone
      //
      txtPhone.BackColor = SystemColors.Window;
      txtPhone.BorderColor = Color.LightGray;
      txtPhone.BorderFocusColor = Color.FromArgb(0, 123, 255);
      txtPhone.BorderRadius = 15;
      txtPhone.BorderSize = 1;
      txtPhone.Font = new Font("Segoe UI", 10F);
      txtPhone.ForeColor = Color.FromArgb(64, 64, 64);
      txtPhone.Location = new Point(22, 398);
      txtPhone.Multiline = false;
      txtPhone.Name = "txtPhone";
      txtPhone.Padding = new Padding(10, 7, 10, 7);
      txtPhone.PasswordChar = false;
      txtPhone.PlaceholderColor = Color.DarkGray;
      txtPhone.PlaceholderText = "";
      txtPhone.Size = new Size(397, 38);
      txtPhone.TabIndex = 4;
      txtPhone.UnderlinedStyle = false;
      //
      // lblPhone
      //
      lblPhone.AutoSize = true;
      lblPhone.Font = new Font("Segoe UI", 9F);
      lblPhone.Location = new Point(18, 375);
      lblPhone.Name = "lblPhone";
      lblPhone.Size = new Size(50, 20);
      lblPhone.TabIndex = 14;
      lblPhone.Text = "Điện thoại";
      //
      // txtEmail
      //
      txtEmail.BackColor = SystemColors.Window;
      txtEmail.BorderColor = Color.LightGray;
      txtEmail.BorderFocusColor = Color.FromArgb(0, 123, 255);
      txtEmail.BorderRadius = 15;
      txtEmail.BorderSize = 1;
      txtEmail.Font = new Font("Segoe UI", 10F);
      txtEmail.ForeColor = Color.FromArgb(64, 64, 64);
      txtEmail.Location = new Point(22, 328);
      txtEmail.Multiline = false;
      txtEmail.Name = "txtEmail";
      txtEmail.Padding = new Padding(10, 7, 10, 7);
      txtEmail.PasswordChar = false;
      txtEmail.PlaceholderColor = Color.DarkGray;
      txtEmail.PlaceholderText = "";
      txtEmail.Size = new Size(397, 38);
      txtEmail.TabIndex = 3;
      txtEmail.UnderlinedStyle = false;
      //
      // lblEmail
      //
      lblEmail.AutoSize = true;
      lblEmail.Font = new Font("Segoe UI", 9F);
      lblEmail.Location = new Point(18, 305);
      lblEmail.Name = "lblEmail";
      lblEmail.Size = new Size(46, 20);
      lblEmail.TabIndex = 12;
      lblEmail.Text = "Email";
      //
      // lblDOB
      //
      lblDOB.AutoSize = true;
      lblDOB.Font = new Font("Segoe UI", 9F);
      lblDOB.Location = new Point(18, 225);
      lblDOB.Name = "lblDOB";
      lblDOB.Size = new Size(94, 20);
      lblDOB.TabIndex = 10;
      lblDOB.Text = "Ngày sinh";
      //
      // lblGender
      //
      lblGender.AutoSize = true;
      lblGender.Font = new Font("Segoe UI", 9F);
      lblGender.Location = new Point(18, 155);
      lblGender.Name = "lblGender";
      lblGender.Size = new Size(57, 20);
      lblGender.TabIndex = 8;
      lblGender.Text = "Giới tính";
      //
      // txtFullName
      //
      txtFullName.BackColor = SystemColors.Window;
      txtFullName.BorderColor = Color.LightGray;
      txtFullName.BorderFocusColor = Color.FromArgb(0, 123, 255);
      txtFullName.BorderRadius = 15;
      txtFullName.BorderSize = 1;
      txtFullName.Font = new Font("Segoe UI", 10F);
      txtFullName.ForeColor = Color.FromArgb(64, 64, 64);
      txtFullName.Location = new Point(22, 68);
      txtFullName.Multiline = false;
      txtFullName.Name = "txtFullName";
      txtFullName.Padding = new Padding(10, 7, 10, 7);
      txtFullName.PasswordChar = false;
      txtFullName.PlaceholderColor = Color.DarkGray;
      txtFullName.PlaceholderText = "";
      txtFullName.Size = new Size(397, 38);
      txtFullName.TabIndex = 0;
      txtFullName.UnderlinedStyle = false;
      //
      // lblFullName
      //
      lblFullName.AutoSize = true;
      lblFullName.Font = new Font("Segoe UI", 9F);
      lblFullName.Location = new Point(18, 45);
      lblFullName.Name = "lblFullName";
      lblFullName.Size = new Size(76, 20);
      lblFullName.TabIndex = 6;
      lblFullName.Text = "Họ và tên";
      //
      // lblCreatedAt
      //
      lblCreatedAt.AutoSize = true;
      lblCreatedAt.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
      lblCreatedAt.ForeColor = SystemColors.ControlDarkDark;
      lblCreatedAt.Location = new Point(22, 815);
      lblCreatedAt.Name = "lblCreatedAt";
      lblCreatedAt.Size = new Size(73, 20);
      lblCreatedAt.TabIndex = 20;
      lblCreatedAt.Text = "CreatedAt";
      //
      // lblStatusId
      //
      lblStatusId.AutoSize = true;
      lblStatusId.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
      lblStatusId.ForeColor = SystemColors.ControlDarkDark;
      lblStatusId.Location = new Point(26, 775);
      lblStatusId.Name = "lblStatusId";
      lblStatusId.Size = new Size(60, 20);
      lblStatusId.TabIndex = 21;
      lblStatusId.Text = "StatusId";
      //
      // btnCancel
      //
      btnCancel.BackColor = Color.Gainsboro;
      btnCancel.FlatAppearance.BorderSize = 0;
      btnCancel.FlatStyle = FlatStyle.Flat;
      btnCancel.Font = new Font("Segoe UI", 10F);
      btnCancel.ForeColor = Color.FromArgb(64, 64, 64);
      btnCancel.Location = new Point(197, 775);
      btnCancel.Name = "btnCancel";
      btnCancel.Size = new Size(120, 49);
      btnCancel.TabIndex = 3;
      btnCancel.Text = "Hủy";
      btnCancel.UseVisualStyleBackColor = false;
      //
      // btnUpdate
      //
      btnUpdate.BackColor = Color.FromArgb(0, 123, 255);
      btnUpdate.FlatAppearance.BorderSize = 0;
      btnUpdate.FlatStyle = FlatStyle.Flat;
      btnUpdate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
      btnUpdate.ForeColor = Color.White;
      btnUpdate.Location = new Point(323, 775);
      btnUpdate.Name = "btnUpdate";
      btnUpdate.Size = new Size(140, 49);
      btnUpdate.TabIndex = 2;
      btnUpdate.Text = "Cập nhật";
      btnUpdate.UseVisualStyleBackColor = false;
      //
      // frmInfor
      //
      AutoScaleDimensions = new SizeF(8F, 20F);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = Color.WhiteSmoke;
      ClientSize = new Size(482, 853);
      Controls.Add(btnUpdate);
      Controls.Add(btnCancel);
      Controls.Add(lblStatusId);
      Controls.Add(lblCreatedAt);
      Controls.Add(gbDetails);
      Controls.Add(gbAccount);
      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = false;
      Name = "frmInfor";
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Thông tin người dùng";
      ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
      gbAccount.ResumeLayout(false);
      gbAccount.PerformLayout();
      gbDetails.ResumeLayout(false);
      gbDetails.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private System.Windows.Forms.PictureBox picAvatar;
    private System.Windows.Forms.Label lblUserName;
    private controls.RoundedTextBox txtUserName;
    private System.Windows.Forms.Label lblPassword;
    private controls.RoundedTextBox txtPassword;
    private System.Windows.Forms.LinkLabel lnkForgotPassword;
    private System.Windows.Forms.GroupBox gbAccount;
    private System.Windows.Forms.GroupBox gbDetails;
    private controls.RoundedTextBox txtFullName;
    private System.Windows.Forms.Label lblFullName;
    private System.Windows.Forms.Label lblGender;
    private System.Windows.Forms.Label lblDOB;
    private controls.RoundedTextBox txtEmail;
    private System.Windows.Forms.Label lblEmail;
    private controls.RoundedTextBox txtPhone;
    private System.Windows.Forms.Label lblPhone;
    private controls.RoundedTextBox txtAddress;
    private System.Windows.Forms.Label lblAddress;
    private System.Windows.Forms.Label lblCreatedAt;
    private System.Windows.Forms.Label lblStatusId;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.ComboBox cboGender;
    private System.Windows.Forms.DateTimePicker dtpDOB;
  }
}
