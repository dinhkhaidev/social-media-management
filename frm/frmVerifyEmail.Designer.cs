namespace SocialManager.frm
{
  partial class frmVerifyEmail
  {
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Label lblEmail;
    private System.Windows.Forms.Label lblCode;
    private System.Windows.Forms.Label lblInstruction;
    private System.Windows.Forms.Label lblTimer;
    private System.Windows.Forms.TextBox txtVerificationCode;
    private System.Windows.Forms.Button btnVerify;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnResend;
    private System.Windows.Forms.Panel pnlMain;

    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lblTitle = new System.Windows.Forms.Label();
      this.lblEmail = new System.Windows.Forms.Label();
      this.lblCode = new System.Windows.Forms.Label();
      this.lblInstruction = new System.Windows.Forms.Label();
      this.lblTimer = new System.Windows.Forms.Label();
      this.txtVerificationCode = new System.Windows.Forms.TextBox();
      this.btnVerify = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnResend = new System.Windows.Forms.Button();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.pnlMain.SuspendLayout();
      this.SuspendLayout();
      //
      // lblTitle
      //
      this.lblTitle.AutoSize = true;
      this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
      this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
      this.lblTitle.Location = new System.Drawing.Point(20, 20);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new System.Drawing.Size(250, 41);
      this.lblTitle.TabIndex = 0;
      this.lblTitle.Text = "Xác Minh Email";
      //
      // lblEmail
      //
      this.lblEmail.AutoSize = true;
      this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 11F);
      this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219);
      this.lblEmail.Location = new System.Drawing.Point(20, 75);
      this.lblEmail.Name = "lblEmail";
      this.lblEmail.Size = new System.Drawing.Size(150, 25);
      this.lblEmail.TabIndex = 1;
      this.lblEmail.Text = "Email: user@email.com";
      //
      // lblCode
      //
      this.lblCode.AutoSize = true;
      this.lblCode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
      this.lblCode.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
      this.lblCode.Location = new System.Drawing.Point(20, 110);
      this.lblCode.Name = "lblCode";
      this.lblCode.Size = new System.Drawing.Size(200, 28);
      this.lblCode.TabIndex = 2;
      this.lblCode.Text = "Mã xác minh: 123456";
      //
      // lblInstruction
      //
      this.lblInstruction.Font = new System.Drawing.Font("Segoe UI", 9.5F);
      this.lblInstruction.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
      this.lblInstruction.Location = new System.Drawing.Point(20, 150);
      this.lblInstruction.Name = "lblInstruction";
      this.lblInstruction.Size = new System.Drawing.Size(460, 60);
      this.lblInstruction.TabIndex = 3;
      this.lblInstruction.Text = "Vui lòng nhập mã xác minh 6 số";
      //
      // lblTimer
      //
      this.lblTimer.AutoSize = true;
      this.lblTimer.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.lblTimer.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
      this.lblTimer.Location = new System.Drawing.Point(20, 220);
      this.lblTimer.Name = "lblTimer";
      this.lblTimer.Size = new System.Drawing.Size(250, 23);
      this.lblTimer.TabIndex = 4;
      this.lblTimer.Text = "Mã hết hiệu lực sau: 10:00";
      //
      // txtVerificationCode
      //
      this.txtVerificationCode.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
      this.txtVerificationCode.Location = new System.Drawing.Point(20, 260);
      this.txtVerificationCode.MaxLength = 6;
      this.txtVerificationCode.Name = "txtVerificationCode";
      this.txtVerificationCode.Size = new System.Drawing.Size(460, 43);
      this.txtVerificationCode.TabIndex = 5;
      this.txtVerificationCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      //
      // btnVerify
      //
      this.btnVerify.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
      this.btnVerify.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnVerify.FlatAppearance.BorderSize = 0;
      this.btnVerify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnVerify.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
      this.btnVerify.ForeColor = System.Drawing.Color.White;
      this.btnVerify.Location = new System.Drawing.Point(20, 320);
      this.btnVerify.Name = "btnVerify";
      this.btnVerify.Size = new System.Drawing.Size(220, 45);
      this.btnVerify.TabIndex = 6;
      this.btnVerify.Text = "Xác Minh";
      this.btnVerify.UseVisualStyleBackColor = false;
      this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
      //
      // btnCancel
      //
      this.btnCancel.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
      this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnCancel.FlatAppearance.BorderSize = 0;
      this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
      this.btnCancel.ForeColor = System.Drawing.Color.White;
      this.btnCancel.Location = new System.Drawing.Point(260, 320);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(220, 45);
      this.btnCancel.TabIndex = 7;
      this.btnCancel.Text = "Hủy";
      this.btnCancel.UseVisualStyleBackColor = false;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      //
      // btnResend
      //
      this.btnResend.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
      this.btnResend.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnResend.FlatAppearance.BorderSize = 0;
      this.btnResend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnResend.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.btnResend.ForeColor = System.Drawing.Color.White;
      this.btnResend.Location = new System.Drawing.Point(20, 380);
      this.btnResend.Name = "btnResend";
      this.btnResend.Size = new System.Drawing.Size(460, 40);
      this.btnResend.TabIndex = 8;
      this.btnResend.Text = "Gửi Lại Mã";
      this.btnResend.UseVisualStyleBackColor = false;
      this.btnResend.Click += new System.EventHandler(this.btnResend_Click);
      //
      // pnlMain
      //
      this.pnlMain.BackColor = System.Drawing.Color.White;
      this.pnlMain.Controls.Add(this.lblTitle);
      this.pnlMain.Controls.Add(this.lblEmail);
      this.pnlMain.Controls.Add(this.lblCode);
      this.pnlMain.Controls.Add(this.lblInstruction);
      this.pnlMain.Controls.Add(this.lblTimer);
      this.pnlMain.Controls.Add(this.txtVerificationCode);
      this.pnlMain.Controls.Add(this.btnVerify);
      this.pnlMain.Controls.Add(this.btnCancel);
      this.pnlMain.Controls.Add(this.btnResend);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 0);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(500, 450);
      this.pnlMain.TabIndex = 0;
      //
      // frmVerifyEmail
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(500, 450);
      this.Controls.Add(this.pnlMain);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmVerifyEmail";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Xác Minh Email";
      this.pnlMain.ResumeLayout(false);
      this.pnlMain.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
