namespace SocialManager.frm
{
  partial class frmLogin
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      pnlMain = new Panel();
      pnlLoginCard = new Panel();
      pnlFooter = new Panel();
      llblRegister = new LinkLabel();
      lblRegisterPrompt = new Label();
      llblForgotPassword = new LinkLabel();
      pnlLoginForm = new Panel();
      chkRememberMe = new CheckBox();
      btnLogin = new Button();
      pnlPassword = new Panel();
      txtPassword = new TextBox();
      picPasswordIcon = new PictureBox();
      pnlUsername = new Panel();
      txtUsername = new TextBox();
      picUsernameIcon = new PictureBox();
      pnlHeader = new Panel();
      lblSubtitle = new Label();
      lblTitle = new Label();
      picLogo = new PictureBox();
      lblPassword = new Label();
      lblUsername = new Label();
      pnlBackground = new Panel();
      picAppLogo = new PictureBox();
      lblAppDescription = new Label();
      lblAppName = new Label();
      pnlMain.SuspendLayout();
      pnlLoginCard.SuspendLayout();
      pnlFooter.SuspendLayout();
      pnlLoginForm.SuspendLayout();
      pnlPassword.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)picPasswordIcon).BeginInit();
      pnlUsername.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)picUsernameIcon).BeginInit();
      pnlHeader.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
      pnlBackground.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)picAppLogo).BeginInit();
      SuspendLayout();
      //
      // pnlMain
      //
      pnlMain.BackColor = Color.White;
      pnlMain.Controls.Add(pnlLoginCard);
      pnlMain.Dock = DockStyle.Right;
      pnlMain.Location = new Point(500, 0);
      pnlMain.Name = "pnlMain";
      pnlMain.Padding = new Padding(60, 80, 60, 80);
      pnlMain.Size = new Size(500, 700);
      pnlMain.TabIndex = 0;
      //
      // pnlLoginCard
      //
      pnlLoginCard.BackColor = Color.White;
      pnlLoginCard.Controls.Add(pnlFooter);
      pnlLoginCard.Controls.Add(pnlLoginForm);
      pnlLoginCard.Controls.Add(pnlHeader);
      pnlLoginCard.Dock = DockStyle.Fill;
      pnlLoginCard.Location = new Point(60, 80);
      pnlLoginCard.Name = "pnlLoginCard";
      pnlLoginCard.Size = new Size(380, 540);
      pnlLoginCard.TabIndex = 0;
      pnlLoginCard.Paint += pnlLoginCard_Paint;
      //
      // pnlFooter
      //
      pnlFooter.Controls.Add(llblRegister);
      pnlFooter.Controls.Add(lblRegisterPrompt);
      pnlFooter.Controls.Add(llblForgotPassword);
      pnlFooter.Dock = DockStyle.Bottom;
      pnlFooter.Location = new Point(0, 440);
      pnlFooter.Name = "pnlFooter";
      pnlFooter.Padding = new Padding(40, 20, 40, 30);
      pnlFooter.Size = new Size(380, 100);
      pnlFooter.TabIndex = 2;
      //
      // llblRegister
      //
      llblRegister.AutoSize = true;
      llblRegister.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
      llblRegister.LinkColor = Color.FromArgb(52, 152, 219);
      llblRegister.Location = new Point(215, 55);
      llblRegister.Name = "llblRegister";
      llblRegister.Size = new Size(76, 23);
      llblRegister.TabIndex = 2;
      llblRegister.TabStop = true;
      llblRegister.Text = "Đăng ký";
      llblRegister.LinkClicked += llblRegister_LinkClicked;
      //
      // lblRegisterPrompt
      //
      lblRegisterPrompt.AutoSize = true;
      lblRegisterPrompt.Font = new Font("Segoe UI", 10F);
      lblRegisterPrompt.ForeColor = Color.FromArgb(127, 140, 141);
      lblRegisterPrompt.Location = new Point(40, 55);
      lblRegisterPrompt.Name = "lblRegisterPrompt";
      lblRegisterPrompt.Size = new Size(191, 23);
      lblRegisterPrompt.TabIndex = 1;
      lblRegisterPrompt.Text = "Chưa có tài khoản?";
      //
      // llblForgotPassword
      //
      llblForgotPassword.AutoSize = true;
      llblForgotPassword.Font = new Font("Segoe UI", 10F);
      llblForgotPassword.LinkColor = Color.FromArgb(52, 152, 219);
      llblForgotPassword.Location = new Point(40, 20);
      llblForgotPassword.Name = "llblForgotPassword";
      llblForgotPassword.Size = new Size(143, 23);
      llblForgotPassword.TabIndex = 0;
      llblForgotPassword.TabStop = true;
      llblForgotPassword.Text = "Quên mật khẩu?";
      llblForgotPassword.LinkClicked += llblForgotPassword_LinkClicked;
      //
      // pnlLoginForm
      //
      pnlLoginForm.Controls.Add(chkRememberMe);
      pnlLoginForm.Controls.Add(btnLogin);
      pnlLoginForm.Controls.Add(pnlPassword);
      pnlLoginForm.Controls.Add(pnlUsername);
      pnlLoginForm.Dock = DockStyle.Fill;
      pnlLoginForm.Location = new Point(0, 120);
      pnlLoginForm.Name = "pnlLoginForm";
      pnlLoginForm.Padding = new Padding(40, 30, 40, 30);
      pnlLoginForm.Size = new Size(380, 420);
      pnlLoginForm.TabIndex = 1;
      //
      // chkRememberMe
      //
      chkRememberMe.AutoSize = true;
      chkRememberMe.Font = new Font("Segoe UI", 10F);
      chkRememberMe.ForeColor = Color.FromArgb(127, 140, 141);
      chkRememberMe.Location = new Point(40, 290);
      chkRememberMe.Name = "chkRememberMe";
      chkRememberMe.Size = new Size(133, 27);
      chkRememberMe.TabIndex = 3;
      chkRememberMe.Text = "Ghi nhớ đăng nhập";
      chkRememberMe.UseVisualStyleBackColor = true;
      //
      // btnLogin
      //
      btnLogin.BackColor = Color.FromArgb(52, 152, 219);
      btnLogin.FlatAppearance.BorderSize = 0;
      btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(41, 128, 185);
      btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
      btnLogin.FlatStyle = FlatStyle.Flat;
      btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
      btnLogin.ForeColor = Color.White;
      btnLogin.Location = new Point(40, 220);
      btnLogin.Name = "btnLogin";
      btnLogin.Size = new Size(300, 50);
      btnLogin.TabIndex = 0;
      btnLogin.Text = "ĐĂNG NHẬP";
      btnLogin.UseVisualStyleBackColor = false;
      btnLogin.Click += btnLogin_Click;
      //
      // pnlPassword
      //
      pnlPassword.BackColor = Color.FromArgb(248, 249, 250);
      pnlPassword.Controls.Add(txtPassword);
      pnlPassword.Controls.Add(picPasswordIcon);
      pnlPassword.Location = new Point(40, 120);
      pnlPassword.Name = "pnlPassword";
      pnlPassword.Size = new Size(300, 50);
      pnlPassword.TabIndex = 2;
      pnlPassword.Paint += pnlInput_Paint;
      //
      // txtPassword
      //
      txtPassword.BackColor = Color.FromArgb(248, 249, 250);
      txtPassword.BorderStyle = BorderStyle.None;
      txtPassword.Font = new Font("Segoe UI", 11F);
      txtPassword.ForeColor = Color.FromArgb(44, 62, 80);
      txtPassword.Location = new Point(50, 13);
      txtPassword.Name = "txtPassword";
      txtPassword.PlaceholderText = "Nhập mật khẩu của bạn";
      txtPassword.Size = new Size(240, 25);
      txtPassword.TabIndex = 1;
      txtPassword.UseSystemPasswordChar = true;
      //
      // picPasswordIcon
      //
      picPasswordIcon.Location = new Point(15, 15);
      picPasswordIcon.Name = "picPasswordIcon";
      picPasswordIcon.Size = new Size(20, 20);
      picPasswordIcon.TabIndex = 0;
      picPasswordIcon.TabStop = false;
      picPasswordIcon.Paint += picPasswordIcon_Paint;
      //
      // pnlUsername
      //
      pnlUsername.BackColor = Color.FromArgb(248, 249, 250);
      pnlUsername.Controls.Add(txtUsername);
      pnlUsername.Controls.Add(picUsernameIcon);
      pnlUsername.Location = new Point(40, 40);
      pnlUsername.Name = "pnlUsername";
      pnlUsername.Size = new Size(300, 50);
      pnlUsername.TabIndex = 1;
      pnlUsername.Paint += pnlInput_Paint;
      //
      // txtUsername
      //
      txtUsername.BackColor = Color.FromArgb(248, 249, 250);
      txtUsername.BorderStyle = BorderStyle.None;
      txtUsername.Font = new Font("Segoe UI", 11F);
      txtUsername.ForeColor = Color.FromArgb(44, 62, 80);
      txtUsername.Location = new Point(50, 13);
      txtUsername.Name = "txtUsername";
      txtUsername.PlaceholderText = "Nhập tên đăng nhập hoặc email";
      txtUsername.Size = new Size(240, 25);
      txtUsername.TabIndex = 1;
      //
      // picUsernameIcon
      //
      picUsernameIcon.Location = new Point(15, 15);
      picUsernameIcon.Name = "picUsernameIcon";
      picUsernameIcon.Size = new Size(20, 20);
      picUsernameIcon.TabIndex = 0;
      picUsernameIcon.TabStop = false;
      picUsernameIcon.Paint += picUsernameIcon_Paint;
      //
      // pnlHeader
      //
      pnlHeader.Controls.Add(lblSubtitle);
      pnlHeader.Controls.Add(lblTitle);
      pnlHeader.Controls.Add(picLogo);
      pnlHeader.Dock = DockStyle.Top;
      pnlHeader.Location = new Point(0, 0);
      pnlHeader.Name = "pnlHeader";
      pnlHeader.Padding = new Padding(40, 30, 40, 20);
      pnlHeader.Size = new Size(380, 120);
      pnlHeader.TabIndex = 0;
      //
      // lblSubtitle
      //
      lblSubtitle.AutoSize = true;
      lblSubtitle.Font = new Font("Segoe UI", 10F);
      lblSubtitle.ForeColor = Color.FromArgb(127, 140, 141);
      lblSubtitle.Location = new Point(40, 80);
      lblSubtitle.Name = "lblSubtitle";
      lblSubtitle.Size = new Size(263, 27);
      lblSubtitle.TabIndex = 1;
      lblSubtitle.Text = "Đăng nhập vào tài khoản quản trị";
      //
      // lblTitle
      //
      lblTitle.AutoSize = true;
      lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
      lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
      lblTitle.Location = new Point(40, 30);
      lblTitle.Name = "lblTitle";
      lblTitle.Size = new Size(197, 54);
      lblTitle.TabIndex = 1;
      lblTitle.Text = "Chào mừng";
      //
      // picLogo
      //
      picLogo.Location = new Point(290, 30);
      picLogo.Name = "picLogo";
      picLogo.Size = new Size(50, 50);
      picLogo.TabIndex = 0;
      picLogo.TabStop = false;
      picLogo.Paint += picLogo_Paint;
      //
      // lblPassword
      //
      lblPassword.AutoSize = true;
      lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
      lblPassword.ForeColor = Color.FromArgb(44, 62, 80);
      lblPassword.Location = new Point(40, 95);
      lblPassword.Name = "lblPassword";
      lblPassword.Size = new Size(85, 23);
      lblPassword.TabIndex = 1;
      lblPassword.Text = "Mật khẩu";
      //
      // lblUsername
      //
      lblUsername.AutoSize = true;
      lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
      lblUsername.ForeColor = Color.FromArgb(44, 62, 80);
      lblUsername.Location = new Point(40, 15);
      lblUsername.Name = "lblUsername";
      lblUsername.Size = new Size(134, 23);
      lblUsername.TabIndex = 0;
      lblUsername.Text = "Tên đăng nhập/Email";
      //
      // pnlBackground
      //
      pnlBackground.BackColor = Color.FromArgb(52, 152, 219);
      pnlBackground.Controls.Add(picAppLogo);
      pnlBackground.Controls.Add(lblAppDescription);
      pnlBackground.Controls.Add(lblAppName);
      pnlBackground.Dock = DockStyle.Fill;
      pnlBackground.Location = new Point(0, 0);
      pnlBackground.Name = "pnlBackground";
      pnlBackground.Padding = new Padding(80);
      pnlBackground.Size = new Size(500, 700);
      pnlBackground.TabIndex = 1;
      //
      // picAppLogo
      //
      picAppLogo.Location = new Point(200, 150);
      picAppLogo.Name = "picAppLogo";
      picAppLogo.Size = new Size(80, 80);
      picAppLogo.TabIndex = 2;
      picAppLogo.TabStop = false;
      picAppLogo.Paint += picAppLogo_Paint;
      //
      // lblAppDescription
      //
      lblAppDescription.AutoSize = true;
      lblAppDescription.Font = new Font("Segoe UI Black", 16F);
      lblAppDescription.ForeColor = Color.FromArgb(192, 255, 255);
      lblAppDescription.Location = new Point(98, 323);
      lblAppDescription.Name = "lblAppDescription";
      lblAppDescription.Size = new Size(293, 37);
      lblAppDescription.TabIndex = 1;
      lblAppDescription.Text = "Nền tảng quản lý";
      //
      // lblAppName
      //
      lblAppName.AutoSize = true;
      lblAppName.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
      lblAppName.ForeColor = Color.White;
      lblAppName.Location = new Point(80, 250);
      lblAppName.Name = "lblAppName";
      lblAppName.Size = new Size(351, 72);
      lblAppName.TabIndex = 0;
      lblAppName.Text = "Mạng Xã Hội";
      //
      // frmLogin
      //
      AutoScaleDimensions = new SizeF(8F, 20F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1000, 700);
      Controls.Add(pnlBackground);
      Controls.Add(pnlMain);
      Font = new Font("Segoe UI", 9F);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      MaximizeBox = false;
      MinimizeBox = false;
      Name = "frmLogin";
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Social Media Manager - Login";
      pnlMain.ResumeLayout(false);
      pnlLoginCard.ResumeLayout(false);
      pnlFooter.ResumeLayout(false);
      pnlFooter.PerformLayout();
      pnlLoginForm.ResumeLayout(false);
      pnlLoginForm.PerformLayout();
      pnlPassword.ResumeLayout(false);
      pnlPassword.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)picPasswordIcon).EndInit();
      pnlUsername.ResumeLayout(false);
      pnlUsername.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)picUsernameIcon).EndInit();
      pnlHeader.ResumeLayout(false);
      pnlHeader.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
      pnlBackground.ResumeLayout(false);
      pnlBackground.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)picAppLogo).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private Panel pnlMain;
    private Panel pnlLoginCard;
    private Panel pnlHeader;
    private PictureBox picLogo;
    private Label lblTitle;
    private Label lblSubtitle;
    private Panel pnlLoginForm;
    private Label lblUsername;
    private Panel pnlUsername;
    private PictureBox picUsernameIcon;
    private TextBox txtUsername;
    private Panel pnlPassword;
    private TextBox txtPassword;
    private PictureBox picPasswordIcon;
    private Label lblPassword;
    private Button btnLogin;
    private CheckBox chkRememberMe;
    private Panel pnlFooter;
    private LinkLabel llblForgotPassword;
    private Label lblRegisterPrompt;
    private LinkLabel llblRegister;
    private Panel pnlBackground;
    private Label lblAppName;
    private Label lblAppDescription;
    private PictureBox picAppLogo;
  }
}
