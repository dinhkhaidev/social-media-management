using SocialManager.services;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SocialManager.frm
{
    public partial class frmPost : Form
    {
        private string selectedImagePath = "";
        private PostService postService;
        private const int MAX_CHARACTERS = 500;

        public frmPost()
        {
            InitializeComponent();
            postService = new PostService();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Thiết lập giá trị mặc định
            cmbVisibility.SelectedIndex = 0; // Chọn "Công khai" làm mặc định
            dtpScheduleDate.Value = DateTime.Now.AddHours(1);
            dtpScheduleDate.MinDate = DateTime.Now;
            
            // Focus vào textbox nội dung
            txtContent.Focus();
            
            // Cập nhật character count ban đầu
            UpdateCharacterCount();
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            UpdateCharacterCount();
        }

        private void UpdateCharacterCount()
        {
            int currentLength = txtContent.Text.Length;
            lblCharacterCount.Text = $"{currentLength}/{MAX_CHARACTERS} ký tự";
            
            if (currentLength > MAX_CHARACTERS)
            {
                lblCharacterCount.ForeColor = Color.FromArgb(231, 76, 60); // Đỏ
                btnPost.Enabled = false;
            }
            else if (currentLength > MAX_CHARACTERS * 0.9)
            {
                lblCharacterCount.ForeColor = Color.FromArgb(230, 126, 34); // Cam
                btnPost.Enabled = true;
            }
            else
            {
                lblCharacterCount.ForeColor = Color.FromArgb(149, 165, 166); // Xám
                btnPost.Enabled = true;
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn hình ảnh";
                ofd.Filter = "Tệp hình ảnh|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Tất cả tệp|*.*";
                ofd.FilterIndex = 1;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        selectedImagePath = ofd.FileName;
                        picPreview.Image = Image.FromFile(selectedImagePath);
                        
                        // Cập nhật thông tin hình ảnh
                        FileInfo fileInfo = new FileInfo(selectedImagePath);
                        lblImageInfo.Text = $"Đã chọn: {fileInfo.Name}\nKích thước: {FormatFileSize(fileInfo.Length)}";
                        
                        // Hiển thị panel preview
                        pnlImagePreview.Visible = true;
                        
                        // Điều chỉnh kích thước form
                        AdjustFormSize();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi tải hình ảnh: {ex.Message}", "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            // Giải phóng tài nguyên hình ảnh
            selectedImagePath = "";
            if (picPreview.Image != null)
            {
                picPreview.Image.Dispose();
                picPreview.Image = null;
            }
            
            // Ẩn panel preview
            pnlImagePreview.Visible = false;
            lblImageInfo.Text = "Chưa chọn hình ảnh";
            
            // Điều chỉnh kích thước form
            AdjustFormSize();
        }

        private void chkSchedulePost_CheckedChanged(object sender, EventArgs e)
        {
            dtpScheduleDate.Visible = chkSchedulePost.Checked;
            if (chkSchedulePost.Checked)
            {
                dtpScheduleDate.Value = DateTime.Now.AddHours(1);
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (!ValidatePost())
                return;

            try
            {
                // Thay đổi trạng thái button
                btnPost.Text = "Đang đăng...";
                btnPost.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                // Lấy thông tin từ form
                string content = txtContent.Text.Trim();
                string visibility = GetVisibilityValue();
                DateTime? postTime = chkSchedulePost.Checked ? dtpScheduleDate.Value : (DateTime?)null;
                
                // Tạo bài viết
                bool success = postService.CreatePost(content, selectedImagePath, visibility, postTime);
                
                if (success)
                {
                    MessageBox.Show("Bài viết đã được tạo thành công!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể tạo bài viết. Vui lòng thử lại.", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tạo bài viết: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Khôi phục trạng thái button
                btnPost.Text = "📤 Đăng";
                btnPost.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có thay đổi nào chưa lưu không
            if (!string.IsNullOrWhiteSpace(txtContent.Text) || !string.IsNullOrEmpty(selectedImagePath))
            {
                var result = MessageBox.Show("Bạn có thay đổi chưa được lưu. Bạn có chắc chắn muốn hủy?", 
                    "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.No)
                    return;
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidatePost()
        {
            // Kiểm tra có nội dung hoặc hình ảnh
            if (string.IsNullOrWhiteSpace(txtContent.Text) && string.IsNullOrEmpty(selectedImagePath))
            {
                MessageBox.Show("Vui lòng nhập nội dung hoặc chọn hình ảnh.", "Lỗi xác thực", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContent.Focus();
                return false;
            }

            // Kiểm tra độ dài nội dung
            if (txtContent.Text.Length > MAX_CHARACTERS)
            {
                MessageBox.Show($"Nội dung vượt quá giới hạn {MAX_CHARACTERS} ký tự.", 
                    "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContent.Focus();
                return false;
            }

            // Kiểm tra thời gian lên lịch
            if (chkSchedulePost.Checked && dtpScheduleDate.Value <= DateTime.Now)
            {
                MessageBox.Show("Thời gian lên lịch phải ở tương lai.", "Lỗi xác thực", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpScheduleDate.Focus();
                return false;
            }

            return true;
        }

        private string GetVisibilityValue()
        {
            switch (cmbVisibility.SelectedIndex)
            {
                case 0: return "Public";      // Công khai
                case 1: return "Friends";     // Bạn bè
                case 2: return "Private";     // Riêng tư
                default: return "Public";
            }
        }

        private void AdjustFormSize()
        {
            if (pnlImagePreview.Visible)
            {
                this.Size = new Size(650, 720); // Tăng chiều cao khi có hình ảnh
            }
            else
            {
                this.Size = new Size(650, 650); // Kích thước bình thường
            }
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Giải phóng tài nguyên hình ảnh
            if (picPreview.Image != null)
            {
                picPreview.Image.Dispose();
            }
            base.OnFormClosed(e);
        }
    }
}