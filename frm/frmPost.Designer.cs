namespace SocialManager.frm
{
    partial class frmPost
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
            dtpScheduleDate = new DateTimePicker();
            chkSchedulePost = new CheckBox();
            cmbVisibility = new ComboBox();
            lblVisibility = new Label();
            lblSettings = new Label();
            pnlImagePreview = new Panel();
            lblImageInfo = new Label();
            btnRemoveImage = new Button();
            picPreview = new PictureBox();
            btnSelectImage = new Button();
            lblMedia = new Label();
            lblCharacterCount = new Label();
            txtContent = new RichTextBox();
            lblContent = new Label();
            lblSubtitle = new Label();
            lblTitle = new Label();
            btnPost = new Button();
            btnCancel = new Button();
            pnlMain.SuspendLayout();
            pnlImagePreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            SuspendLayout();
            //
            // pnlMain
            //
            pnlMain.BackColor = Color.White;
            pnlMain.BorderStyle = BorderStyle.FixedSingle;
            pnlMain.Controls.Add(dtpScheduleDate);
            pnlMain.Controls.Add(chkSchedulePost);
            pnlMain.Controls.Add(cmbVisibility);
            pnlMain.Controls.Add(lblVisibility);
            pnlMain.Controls.Add(lblSettings);
            pnlMain.Controls.Add(pnlImagePreview);
            pnlMain.Controls.Add(btnSelectImage);
            pnlMain.Controls.Add(lblMedia);
            pnlMain.Controls.Add(lblCharacterCount);
            pnlMain.Controls.Add(txtContent);
            pnlMain.Controls.Add(lblContent);
            pnlMain.Controls.Add(lblSubtitle);
            pnlMain.Controls.Add(lblTitle);
            pnlMain.Location = new Point(29, 33);
            pnlMain.Margin = new Padding(3, 4, 3, 4);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(685, 733);
            pnlMain.TabIndex = 0;
            //
            // dtpScheduleDate
            //
            dtpScheduleDate.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpScheduleDate.Font = new Font("Segoe UI", 9F);
            dtpScheduleDate.Format = DateTimePickerFormat.Custom;
            dtpScheduleDate.Location = new Point(458, 610);
            dtpScheduleDate.Margin = new Padding(3, 4, 3, 4);
            dtpScheduleDate.Name = "dtpScheduleDate";
            dtpScheduleDate.ShowUpDown = true;
            dtpScheduleDate.Size = new Size(205, 27);
            dtpScheduleDate.TabIndex = 12;
            dtpScheduleDate.Visible = false;
            //
            // chkSchedulePost
            //
            chkSchedulePost.AutoSize = true;
            chkSchedulePost.Font = new Font("Segoe UI", 10F);
            chkSchedulePost.ForeColor = Color.FromArgb(52, 73, 94);
            chkSchedulePost.Location = new Point(318, 613);
            chkSchedulePost.Margin = new Padding(3, 4, 3, 4);
            chkSchedulePost.Name = "chkSchedulePost";
            chkSchedulePost.Size = new Size(134, 27);
            chkSchedulePost.TabIndex = 11;
            chkSchedulePost.Text = "Lên lịch đăng";
            chkSchedulePost.UseVisualStyleBackColor = true;
            chkSchedulePost.CheckedChanged += chkSchedulePost_CheckedChanged;
            //
            // cmbVisibility
            //
            cmbVisibility.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVisibility.Font = new Font("Segoe UI", 10F);
            cmbVisibility.FormattingEnabled = true;
            cmbVisibility.Items.AddRange(new object[] { "Công khai", "Bạn bè", "Riêng tư" });
            cmbVisibility.Location = new Point(162, 606);
            cmbVisibility.Margin = new Padding(3, 4, 3, 4);
            cmbVisibility.Name = "cmbVisibility";
            cmbVisibility.Size = new Size(137, 31);
            cmbVisibility.TabIndex = 10;
            //
            // lblVisibility
            //
            lblVisibility.AutoSize = true;
            lblVisibility.Font = new Font("Segoe UI", 10F);
            lblVisibility.ForeColor = Color.FromArgb(52, 73, 94);
            lblVisibility.Location = new Point(34, 614);
            lblVisibility.Name = "lblVisibility";
            lblVisibility.Size = new Size(129, 23);
            lblVisibility.TabIndex = 9;
            lblVisibility.Text = "Quyền riêng tư:";
            //
            // lblSettings
            //
            lblSettings.AutoSize = true;
            lblSettings.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSettings.ForeColor = Color.FromArgb(52, 73, 94);
            lblSettings.Location = new Point(34, 576);
            lblSettings.Name = "lblSettings";
            lblSettings.Size = new Size(142, 25);
            lblSettings.TabIndex = 8;
            lblSettings.Text = "Cài đặt bài viết";
            //
            // pnlImagePreview
            //
            pnlImagePreview.BackColor = Color.FromArgb(248, 249, 250);
            pnlImagePreview.BorderStyle = BorderStyle.FixedSingle;
            pnlImagePreview.Controls.Add(btnRemoveImage);
            pnlImagePreview.Controls.Add(lblImageInfo);
            pnlImagePreview.Controls.Add(picPreview);
            pnlImagePreview.Location = new Point(34, 454);
            pnlImagePreview.Margin = new Padding(3, 4, 3, 4);
            pnlImagePreview.Name = "pnlImagePreview";
            pnlImagePreview.Size = new Size(617, 106);
            pnlImagePreview.TabIndex = 7;
            pnlImagePreview.Visible = false;
            //
            // lblImageInfo
            //
            lblImageInfo.AutoSize = true;
            lblImageInfo.Font = new Font("Segoe UI", 10F);
            lblImageInfo.ForeColor = Color.FromArgb(127, 140, 141);
            lblImageInfo.Location = new Point(98, 28);
            lblImageInfo.Name = "lblImageInfo";
            lblImageInfo.Size = new Size(166, 23);
            lblImageInfo.TabIndex = 2;
            lblImageInfo.Text = "Chưa chọn hình ảnh";
            //
            // btnRemoveImage
            //
            btnRemoveImage.BackColor = Color.FromArgb(231, 76, 60);
            btnRemoveImage.FlatAppearance.BorderSize = 0;
            btnRemoveImage.FlatStyle = FlatStyle.Flat;
            btnRemoveImage.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnRemoveImage.ForeColor = Color.White;
            btnRemoveImage.Location = new Point(577, 8);
            btnRemoveImage.Margin = new Padding(3, 4, 3, 4);
            btnRemoveImage.Name = "btnRemoveImage";
            btnRemoveImage.Size = new Size(29, 33);
            btnRemoveImage.TabIndex = 1;
            btnRemoveImage.Text = "×";
            btnRemoveImage.UseVisualStyleBackColor = false;
            btnRemoveImage.Click += btnRemoveImage_Click;
            //
            // picPreview
            //
            picPreview.BackColor = Color.White;
            picPreview.BorderStyle = BorderStyle.FixedSingle;
            picPreview.Location = new Point(11, 8);
            picPreview.Margin = new Padding(3, 4, 3, 4);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(68, 79);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 0;
            picPreview.TabStop = false;
            //
            // btnSelectImage
            //
            btnSelectImage.BackColor = Color.FromArgb(46, 204, 113);
            btnSelectImage.FlatAppearance.BorderSize = 0;
            btnSelectImage.FlatStyle = FlatStyle.Flat;
            btnSelectImage.Font = new Font("Segoe UI", 10F);
            btnSelectImage.ForeColor = Color.White;
            btnSelectImage.Location = new Point(34, 401);
            btnSelectImage.Margin = new Padding(3, 4, 3, 4);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(137, 36);
            btnSelectImage.TabIndex = 6;
            btnSelectImage.Text = "Chọn hình ảnh";
            btnSelectImage.UseVisualStyleBackColor = false;
            btnSelectImage.Click += btnSelectImage_Click;
            //
            // lblMedia
            //
            lblMedia.AutoSize = true;
            lblMedia.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMedia.ForeColor = Color.FromArgb(52, 73, 94);
            lblMedia.Location = new Point(34, 363);
            lblMedia.Name = "lblMedia";
            lblMedia.Size = new Size(240, 25);
            lblMedia.TabIndex = 5;
            lblMedia.Text = "Thêm hình ảnh (tùy chọn)";
            //
            // lblCharacterCount
            //
            lblCharacterCount.AutoSize = true;
            lblCharacterCount.Font = new Font("Segoe UI", 9F);
            lblCharacterCount.ForeColor = Color.FromArgb(149, 165, 166);
            lblCharacterCount.Location = new Point(558, 339);
            lblCharacterCount.Name = "lblCharacterCount";
            lblCharacterCount.Size = new Size(83, 20);
            lblCharacterCount.TabIndex = 4;
            lblCharacterCount.Text = "0/500 ký tự";
            lblCharacterCount.TextAlign = ContentAlignment.MiddleRight;
            //
            // txtContent
            //
            txtContent.BorderStyle = BorderStyle.FixedSingle;
            txtContent.Font = new Font("Segoe UI", 11F);
            txtContent.Location = new Point(34, 136);
            txtContent.Margin = new Padding(3, 4, 3, 4);
            txtContent.MaxLength = 500;
            txtContent.Name = "txtContent";
            txtContent.Size = new Size(617, 199);
            txtContent.TabIndex = 3;
            txtContent.Text = "";
            txtContent.TextChanged += txtContent_TextChanged;
            //
            // lblContent
            //
            lblContent.AutoSize = true;
            lblContent.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblContent.ForeColor = Color.FromArgb(52, 73, 94);
            lblContent.Location = new Point(34, 107);
            lblContent.Name = "lblContent";
            lblContent.Size = new Size(205, 25);
            lblContent.TabIndex = 2;
            lblContent.Text = "Bạn đang nghĩ gì thế?";
            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(127, 140, 141);
            lblSubtitle.Location = new Point(34, 74);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(310, 23);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Chia sẻ suy nghĩ của bạn với mọi người";
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(34, 33);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(246, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Tạo bài viết mới";
            //
            // btnPost
            //
            btnPost.BackColor = Color.FromArgb(52, 152, 219);
            btnPost.FlatAppearance.BorderSize = 0;
            btnPost.FlatStyle = FlatStyle.Flat;
            btnPost.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnPost.ForeColor = Color.White;
            btnPost.Location = new Point(600, 787);
            btnPost.Margin = new Padding(3, 4, 3, 4);
            btnPost.Name = "btnPost";
            btnPost.Size = new Size(114, 60);
            btnPost.TabIndex = 13;
            btnPost.Text = "📤 Đăng";
            btnPost.UseVisualStyleBackColor = false;
            btnPost.Click += btnPost_Click;
            //
            // btnCancel
            //
            btnCancel.BackColor = Color.FromArgb(149, 165, 166);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 11F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(474, 787);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(114, 60);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            //
            // frmPost
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(743, 867);
            Controls.Add(btnCancel);
            Controls.Add(btnPost);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmPost";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tạo bài viết mới";
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            pnlImagePreview.ResumeLayout(false);
            pnlImagePreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.RichTextBox txtContent;
        private System.Windows.Forms.Label lblCharacterCount;
        private System.Windows.Forms.Label lblMedia;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Panel pnlImagePreview;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnRemoveImage;
        private System.Windows.Forms.Label lblImageInfo;
        private System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.Label lblVisibility;
        private System.Windows.Forms.ComboBox cmbVisibility;
        private System.Windows.Forms.CheckBox chkSchedulePost;
        private System.Windows.Forms.DateTimePicker dtpScheduleDate;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button btnCancel;
    }
}
