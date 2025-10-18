namespace SocialManager.frm
{
    partial class frmForgotPass
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
            this.lblPass1 = new System.Windows.Forms.Label();
            this.lblPass2 = new System.Windows.Forms.Label();
            this.lblDetail = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            // SỬA LỖI: Đảm bảo đây là 'SocialManager.Controls' (chữ C viết hoa)
            this.txtPass2 = new SocialManager.controls.RoundedTextBox();
            this.txtPass1 = new SocialManager.controls.RoundedTextBox();
            this.SuspendLayout();
            // 
            // lblPass1
            // 
            this.lblPass1.AutoSize = true;
            this.lblPass1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPass1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPass1.Location = new System.Drawing.Point(60, 105);
            this.lblPass1.Name = "lblPass1";
            this.lblPass1.Size = new System.Drawing.Size(155, 23);
            this.lblPass1.TabIndex = 1;
            this.lblPass1.Text = "Enter new password:";
            // 
            // lblPass2
            // 
            this.lblPass2.AutoSize = true;
            this.lblPass2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPass2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPass2.Location = new System.Drawing.Point(60, 185);
            this.lblPass2.Name = "lblPass2";
            this.lblPass2.Size = new System.Drawing.Size(151, 23);
            this.lblPass2.TabIndex = 1;
            this.lblPass2.Text = "Confirm password:";
            // 
            // lblDetail
            // 
            this.lblDetail.AutoSize = true;
            this.lblDetail.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblDetail.Location = new System.Drawing.Point(100, 40);
            this.lblDetail.Name = "lblDetail";
            this.lblDetail.Size = new System.Drawing.Size(298, 32);
            this.lblDetail.TabIndex = 1;
            this.lblDetail.Text = "UPDATE NEW PASSWORD";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(285, 260);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(150, 40);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.Location = new System.Drawing.Point(64, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // txtPass2
            // 
            this.txtPass2.BackColor = System.Drawing.SystemColors.Window;
            this.txtPass2.BorderColor = System.Drawing.Color.LightGray;
            this.txtPass2.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.txtPass2.BorderRadius = 15;
            this.txtPass2.BorderSize = 2;
            this.txtPass2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPass2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPass2.Location = new System.Drawing.Point(64, 211);
            this.txtPass2.Multiline = false;
            this.txtPass2.Name = "txtPass2";
            this.txtPass2.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtPass2.PasswordChar = true;
            this.txtPass2.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtPass2.PlaceholderText = "";
            this.txtPass2.Size = new System.Drawing.Size(371, 38);
            this.txtPass2.TabIndex = 2;
            this.txtPass2.UnderlinedStyle = false;
            // 
            // txtPass1
            // 
            this.txtPass1.BackColor = System.Drawing.SystemColors.Window;
            this.txtPass1.BorderColor = System.Drawing.Color.LightGray;
            this.txtPass1.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.txtPass1.BorderRadius = 15;
            this.txtPass1.BorderSize = 2;
            this.txtPass1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPass1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPass1.Location = new System.Drawing.Point(64, 131);
            this.txtPass1.Multiline = false;
            this.txtPass1.Name = "txtPass1";
            this.txtPass1.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtPass1.PasswordChar = true;
            this.txtPass1.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtPass1.PlaceholderText = "";
            this.txtPass1.Size = new System.Drawing.Size(371, 38);
            this.txtPass1.TabIndex = 1;
            this.txtPass1.UnderlinedStyle = false;
            // 
            // frmForgotPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(516, 350);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtPass2);
            this.Controls.Add(this.txtPass1);
            this.Controls.Add(this.lblPass2);
            this.Controls.Add(this.lblDetail);
            this.Controls.Add(this.lblPass1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmForgotPass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update New Password";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblPass1;
        private System.Windows.Forms.Label lblPass2;
        private System.Windows.Forms.Label lblDetail;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private controls.RoundedTextBox txtPass1;
        private controls.RoundedTextBox txtPass2;
    }
}