namespace SocialManager.frm
{
    partial class frmPost
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
            this.lblPostInfo = new System.Windows.Forms.Label();
            this.txtContent = new SocialManager.controls.RoundedTextBox();
            this.btnLike = new System.Windows.Forms.Button();
            this.btnComment = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPostInfo
            // 
            this.lblPostInfo.AutoSize = true;
            this.lblPostInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPostInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblPostInfo.Location = new System.Drawing.Point(25, 25);
            this.lblPostInfo.Name = "lblPostInfo";
            this.lblPostInfo.Size = new System.Drawing.Size(273, 20);
            this.lblPostInfo.TabIndex = 0;
            this.lblPostInfo.Text = "ID: 12345 | Password: N/A | 20/10/2025";
            // 
            // txtContent
            // 
            this.txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContent.BackColor = System.Drawing.SystemColors.Window;
            this.txtContent.BorderColor = System.Drawing.Color.LightGray;
            this.txtContent.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.txtContent.BorderRadius = 15;
            this.txtContent.BorderSize = 1;
            this.txtContent.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtContent.Location = new System.Drawing.Point(29, 60);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtContent.PasswordChar = false;
            this.txtContent.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtContent.PlaceholderText = "Nội dung bài viết...";
            this.txtContent.Size = new System.Drawing.Size(742, 250);
            this.txtContent.TabIndex = 1;
            this.txtContent.UnderlinedStyle = false;
            // 
            // btnLike
            // 
            this.btnLike.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLike.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLike.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnLike.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLike.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLike.Location = new System.Drawing.Point(29, 325);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(120, 32);
            this.btnLike.TabIndex = 3;
            this.btnLike.Text = "Like";
            this.btnLike.UseVisualStyleBackColor = false;
            // 
            // btnComment
            // 
            this.btnComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnComment.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnComment.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnComment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnComment.Location = new System.Drawing.Point(165, 325);
            this.btnComment.Name = "btnComment";
            this.btnComment.Size = new System.Drawing.Size(120, 32);
            this.btnComment.TabIndex = 4;
            this.btnComment.Text = "Comment";
            this.btnComment.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(651, 370);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.Location = new System.Drawing.Point(525, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblStatus.Location = new System.Drawing.Point(650, 332);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(121, 19);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Trạng thái cập nhật";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmPost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 422);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnComment);
            this.Controls.Add(this.btnLike);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.lblPostInfo);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "frmPost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi Tiết Bài Viết";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblPostInfo;
        private controls.RoundedTextBox txtContent;
        private System.Windows.Forms.Button btnLike;
        private System.Windows.Forms.Button btnComment;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblStatus;
    }
}