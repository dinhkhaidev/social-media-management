using SocialManager.services;
using SocialManager.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#pragma warning disable CS8602, CS8604 // Suppress null reference warnings for this UserControl

namespace SocialManager.frm.UserControls
{
  public partial class ucPosts : UserControl
  {
    private PostService? postService;
    private UserService? userService;
    private List<Post>? allPosts;
    private string selectedImagePath = "";

    public ucPosts()
    {
      InitializeComponent();
      InitializePosts();
      LoadPostsData();
    }

    private void InitializePosts()
    {
      try
      {
        this.BackColor = Color.FromArgb(247, 249, 252);
        postService = new PostService();
        userService = new UserService();
        allPosts = new List<Post>();

        SetupFonts();
        SetupPlaceholderTexts();
        SetupDataGridView();

        if (cmbVisibilityFilter != null && cmbVisibilityFilter.Items.Count > 0)
          cmbVisibilityFilter.SelectedIndex = 0;

        // Apply rounded corners to UI elements
        ApplyRoundedCorners();
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error in InitializePosts: {ex.Message}");
      }
    }

    private void SetupFonts()
    {
      try
      {
        var defaultFont = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        var titleFont = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
        var buttonFont = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        var textBoxFont = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        var boldButtonFont = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);

        this.Font = defaultFont;

        if (lblPostCreatorTitle != null)
          lblPostCreatorTitle.Font = titleFont;

        if (lblPostsListTitle != null)
          lblPostsListTitle.Font = titleFont;

        if (txtPostTitle != null)
          txtPostTitle.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);

        if (txtPostContent != null)
          txtPostContent.Font = textBoxFont;

        if (txtSearch != null)
          txtSearch.Font = defaultFont;

        if (btnCreatePost != null)
          btnCreatePost.Font = boldButtonFont;

        if (btnAddPhoto != null)
          btnAddPhoto.Font = defaultFont;

        if (btnEditPost != null)
          btnEditPost.Font = defaultFont;

        if (btnDeletePost != null)
          btnDeletePost.Font = defaultFont;

        if (btnRefreshPosts != null)
          btnRefreshPosts.Font = defaultFont;

        if (cmbPlatform != null)
          cmbPlatform.Font = defaultFont;

        if (cmbVisibilityFilter != null)
          cmbVisibilityFilter.Font = defaultFont;

        if (chkSchedulePost != null)
          chkSchedulePost.Font = defaultFont;

        if (dtpScheduleDate != null)
          dtpScheduleDate.Font = defaultFont;

        if (lblSearch != null)
          lblSearch.Font = defaultFont;

        if (lblFilter != null)
          lblFilter.Font = defaultFont;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error setting up fonts: {ex.Message}");
        this.Font = SystemFonts.DefaultFont;
      }
    }

    private void SetupPlaceholderTexts()
    {
      try
      {
        if (txtPostTitle != null)
        {
          txtPostTitle.PlaceholderText = "";
          txtPostTitle.PlaceholderText = "Nhập tiêu đề bài viết...";
        }

        if (txtPostContent != null)
        {
          txtPostContent.PlaceholderText = "";
          txtPostContent.PlaceholderText = "Viết nội dung bài post tại đây...";
        }

        if (txtSearch != null)
        {
          txtSearch.PlaceholderText = "";
          txtSearch.PlaceholderText = "Tìm kiếm bài viết...";
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error setting up placeholder texts: {ex.Message}");
      }
    }

    private void SetupDataGridView()
    {
      try
      {
        if (dgvPosts == null) return;

        dgvPosts.Columns.Clear();

        var idColumn = new DataGridViewTextBoxColumn
        {
          Name = "PostID",
          HeaderText = "ID",
          Width = 60,
          Visible = false
        };

        var userColumn = new DataGridViewTextBoxColumn
        {
          Name = "User",
          HeaderText = "Người đăng",
          Width = 120
        };

        var contentColumn = new DataGridViewTextBoxColumn
        {
          Name = "Content",
          HeaderText = "Nội dung",
          Width = 250,
          DefaultCellStyle = new DataGridViewCellStyle
          {
            WrapMode = DataGridViewTriState.True
          }
        };

        var visibilityColumn = new DataGridViewTextBoxColumn
        {
          Name = "Visibility",
          HeaderText = "Quyền riêng tư",
          Width = 100
        };

        var likesColumn = new DataGridViewTextBoxColumn
        {
          Name = "Likes",
          HeaderText = "Lượt thích",
          Width = 80
        };

        var commentsColumn = new DataGridViewTextBoxColumn
        {
          Name = "Comments",
          HeaderText = "Bình luận",
          Width = 80
        };

        var dateColumn = new DataGridViewTextBoxColumn
        {
          Name = "CreatedAt",
          HeaderText = "Ngày tạo",
          Width = 130
        };

        var mediaColumn = new DataGridViewTextBoxColumn
        {
          Name = "HasMedia",
          HeaderText = "Hình ảnh",
          Width = 70
        };

        dgvPosts.Columns.AddRange(new DataGridViewColumn[]
        {
          idColumn, userColumn, contentColumn, visibilityColumn,
          likesColumn, commentsColumn, dateColumn, mediaColumn
        });

        var cellFont = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        var headerFont = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);

        dgvPosts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
        dgvPosts.DefaultCellStyle.SelectionForeColor = Color.White;
        dgvPosts.DefaultCellStyle.BackColor = Color.White;
        dgvPosts.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
        dgvPosts.DefaultCellStyle.Font = cellFont;
        dgvPosts.DefaultCellStyle.Padding = new Padding(8, 6, 8, 6);

        dgvPosts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
        dgvPosts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvPosts.ColumnHeadersDefaultCellStyle.Font = headerFont;
        dgvPosts.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 10, 8, 10);
        dgvPosts.ColumnHeadersHeight = 40;
        dgvPosts.RowTemplate.Height = 60;
        dgvPosts.EnableHeadersVisualStyles = false;
        dgvPosts.GridColor = Color.FromArgb(234, 236, 238);
        dgvPosts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error setting up DataGridView: {ex.Message}");
      }
    }

    private void ApplyRoundedCorners()
    {
      try
      {
        // Apply rounded corners to text inputs
        if (txtPostTitle != null)
          UIHelper.ApplyRoundedCorners(txtPostTitle, 10);

        if (txtPostContent != null)
          UIHelper.ApplyRoundedCorners(txtPostContent, 10);

        if (txtSearch != null)
          UIHelper.ApplyRoundedCorners(txtSearch, 10);

        // Apply rounded corners to buttons
        if (btnCreatePost != null)
          UIHelper.ApplyRoundedCorners(btnCreatePost, 10);

        if (btnAddPhoto != null)
          UIHelper.ApplyRoundedCorners(btnAddPhoto, 8);

        if (btnEditPost != null)
          UIHelper.ApplyRoundedCorners(btnEditPost, 8);

        if (btnDeletePost != null)
          UIHelper.ApplyRoundedCorners(btnDeletePost, 8);

        if (btnRefreshPosts != null)
          UIHelper.ApplyRoundedCorners(btnRefreshPosts, 8);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error applying rounded corners: {ex.Message}");
      }
    }

    public void LoadPostsData()
    {
      try
      {
        if (postService == null) return;

        allPosts = postService.GetAllPosts();
        if (allPosts != null)
        {
          DisplayPosts(allPosts);
        }

        if (cmbPlatform != null && cmbPlatform.Items.Count > 0)
          cmbPlatform.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi tải dữ liệu bài viết: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void DisplayPosts(List<Post> posts)
    {
      try
      {
        if (dgvPosts == null) return;

        dgvPosts.Rows.Clear();

        List<Like> likesList = new List<Like>();
        List<Comment> commentsList = new List<Comment>();
        try
        {
          likesList = Like.GetList(GlobalSetting.LikesFilePath);
        }
        catch { }

        try
        {
          commentsList = Comment.GetList(GlobalSetting.CommentsFilePath);
        }
        catch { }

        var likesCountByPost = likesList
          .GroupBy(l => l.PostID)
          .ToDictionary(g => g.Key, g => g.Count());

        var commentsCountByPost = commentsList
          .GroupBy(c => c.PostID)
          .ToDictionary(g => g.Key, g => g.Count());

        foreach (var post in posts.OrderByDescending(p => p.CreatedAt))
        {
          try
          {
            var user = userService.GetUserById(post.UserID);
            string userName = user?.UserName ?? "Unknown User";

            string content = post.Content;
            if (content.Length > 100)
            {
              content = content.Substring(0, 100) + "...";
            }

            string visibility = GetVisibilityDisplay(post.Visibility);
            string createdAt = post.CreatedAt.ToString("dd/MM/yyyy HH:mm");
            string hasMedia = !string.IsNullOrEmpty(post.MediaUrl) ? "Có" : "Không";

            int liveLikes = likesCountByPost.TryGetValue(post.PostID, out var lc) ? lc : 0;
            int liveComments = commentsCountByPost.TryGetValue(post.PostID, out var cc) ? cc : 0;

            dgvPosts.Rows.Add(
                post.PostID,
                userName,
                content,
                visibility,
                liveLikes.ToString(),
                liveComments.ToString(),
                createdAt,
                hasMedia
            );
          }
          catch (Exception ex)
          {
            System.Diagnostics.Debug.WriteLine($"Error displaying post {post.PostID}: {ex.Message}");
          }
        }

        if (lblPostsListTitle != null)
          lblPostsListTitle.Text = $"Quản lý bài viết ({posts.Count} bài viết)";
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error in DisplayPosts: {ex.Message}");
      }
    }

    private string GetVisibilityDisplay(string visibility)
    {
      return visibility switch
      {
        "Public" => "Công khai",
        "Friends" => "Bạn bè",
        "Private" => "Riêng tư",
        _ => "Công khai"
      };
    }

    private void btnCreatePost_Click(object sender, EventArgs e)
    {
      if (btnCreatePost?.Tag != null && btnCreatePost.Text == "CẬP NHẬT BÀI VIẾT")
      {
        UpdatePost();
        return;
      }

      if (!ValidatePostInput())
        return;

      try
      {
        if (btnCreatePost != null)
        {
          btnCreatePost.Text = "ĐANG TẠO...";
          btnCreatePost.Enabled = false;
        }

        string content = txtPostContent?.Text?.Trim() ?? "";
        string visibility = GetVisibilityValue();
        DateTime? postTime = chkSchedulePost?.Checked == true ? dtpScheduleDate?.Value : null;

        bool success = postService.CreatePost(content, selectedImagePath, visibility, postTime);

        if (success)
        {
          MessageBox.Show("Bài viết đã được tạo thành công!", "Thành công",
              MessageBoxButtons.OK, MessageBoxIcon.Information);

          ClearForm();
          LoadPostsData();
        }
        else
        {
          MessageBox.Show("Không thể tạo bài viết. Vui lòng thử lại.", "Lỗi",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        if (btnCreatePost != null)
        {
          btnCreatePost.Text = "TẠO BÀI VIẾT";
          btnCreatePost.Enabled = true;
        }
      }
    }

    private void UpdatePost()
    {
      try
      {
        int postId = Convert.ToInt32(btnCreatePost?.Tag);
        string content = txtPostContent?.Text?.Trim() ?? "";

        bool success = postService.UpdatePost(postId, content);

        if (success)
        {
          MessageBox.Show("Bài viết đã được cập nhật thành công!", "Thành công",
              MessageBoxButtons.OK, MessageBoxIcon.Information);

          // Reset form and reload data
          ClearForm();
          if (btnCreatePost != null)
          {
            btnCreatePost.Text = "TẠO BÀI VIẾT";
            btnCreatePost.Tag = null;
          }
          LoadPostsData();
        }
        else
        {
          MessageBox.Show("Không thể cập nhật bài viết. Vui lòng thử lại.", "Lỗi",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi cập nhật bài viết: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private bool ValidatePostInput()
    {
      if (string.IsNullOrWhiteSpace(txtPostContent?.Text))
      {
        MessageBox.Show("Vui lòng nhập nội dung bài viết.", "Lỗi xác thực",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtPostContent?.Focus();
        return false;
      }

      if (txtPostContent.Text.Length > 500)
      {
        MessageBox.Show("Nội dung bài viết không được vượt quá 500 ký tự.", "Lỗi xác thực",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtPostContent?.Focus();
        return false;
      }

      if (chkSchedulePost?.Checked == true && dtpScheduleDate?.Value <= DateTime.Now)
      {
        MessageBox.Show("Thời gian lên lịch phải ở tương lai.", "Lỗi xác thực",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        dtpScheduleDate?.Focus();
        return false;
      }

      return true;
    }

    private string GetVisibilityValue()
    {
      return "Public";
    }

    private void ClearForm()
    {
      txtPostTitle?.Clear();
      txtPostContent?.Clear();
      selectedImagePath = "";
      if (chkSchedulePost != null)
        chkSchedulePost.Checked = false;
      if (cmbPlatform != null && cmbPlatform.Items.Count > 0)
        cmbPlatform.SelectedIndex = 0;

      if (btnAddPhoto != null)
      {
        btnAddPhoto.Text = "Thêm hình ảnh";
        btnAddPhoto.BackColor = Color.FromArgb(149, 165, 166);
      }
    }

    private void btnEditPost_Click(object sender, EventArgs e)
    {
      if (dgvPosts?.SelectedRows?.Count == 0)
      {
        MessageBox.Show("Vui lòng chọn một bài viết để chỉnh sửa.", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }

      try
      {
        int postId = Convert.ToInt32(dgvPosts.SelectedRows[0].Cells["PostID"].Value);
        var post = allPosts.FirstOrDefault(p => p.PostID == postId);

        if (post == null)
        {
          MessageBox.Show("Không tìm thấy bài viết.", "Lỗi",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        if (txtPostContent != null)
          txtPostContent.Text = post.Content;

        MessageBox.Show("Bạn có thể chỉnh sửa nội dung bài viết và nhấn 'Cập nhật bài viết' để lưu thay đổi.",
            "Chỉnh sửa bài viết", MessageBoxButtons.OK, MessageBoxIcon.Information);

        if (btnCreatePost != null)
        {
          btnCreatePost.Text = "CẬP NHẬT BÀI VIẾT";
          btnCreatePost.Tag = postId;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi chỉnh sửa bài viết: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnDeletePost_Click(object sender, EventArgs e)
    {
      if (dgvPosts?.SelectedRows?.Count == 0)
      {
        MessageBox.Show("Vui lòng chọn một bài viết để xóa.", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }

      var result = MessageBox.Show("Bạn có chắc chắn muốn xóa bài viết này?", "Xác nhận xóa",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

      if (result == DialogResult.Yes)
      {
        try
        {
          int postId = Convert.ToInt32(dgvPosts.SelectedRows[0].Cells["PostID"].Value);

          bool success = postService.DeletePost(postId);

          if (success)
          {
            MessageBox.Show("Bài viết đã được xóa thành công!", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadPostsData();
          }
          else
          {
            MessageBox.Show("Không thể xóa bài viết. Vui lòng thử lại.", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Lỗi khi xóa bài viết: {ex.Message}", "Lỗi",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private void btnRefreshPosts_Click(object sender, EventArgs e)
    {
      LoadPostsData();
      MessageBox.Show("Dữ liệu bài viết đã được làm mới!", "Thành công",
          MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void chkSchedulePost_CheckedChanged(object sender, EventArgs e)
    {
      if (dtpScheduleDate != null)
        dtpScheduleDate.Enabled = chkSchedulePost?.Checked == true;

      if (chkSchedulePost?.Checked == true && dtpScheduleDate != null)
      {
        dtpScheduleDate.Value = DateTime.Now.AddHours(1);
      }
    }

    private void btnAddPhoto_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog openFileDialog = new OpenFileDialog())
      {
        openFileDialog.Filter = "Tệp hình ảnh (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
        openFileDialog.Title = "Chọn hình ảnh";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
          try
          {
            selectedImagePath = openFileDialog.FileName;
            FileInfo fileInfo = new FileInfo(selectedImagePath);

            if (btnAddPhoto != null)
            {
              btnAddPhoto.Text = $"{fileInfo.Name}";
              btnAddPhoto.BackColor = Color.FromArgb(46, 204, 113);
            }

            MessageBox.Show($"Đã chọn hình ảnh: {fileInfo.Name}", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
          catch (Exception ex)
          {
            MessageBox.Show($"Lỗi khi chọn hình ảnh: {ex.Message}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
      }
    }

    private void TxtSearch_TextChanged(object sender, EventArgs e)
    {
      SearchPosts(txtSearch?.Text ?? "");
    }

    private void SearchPosts(string searchTerm)
    {
      if (allPosts == null) return;

      if (string.IsNullOrWhiteSpace(searchTerm))
      {
        DisplayPosts(allPosts);
        return;
      }

      var filteredPosts = allPosts.Where(p =>
          p.Content.ToLower().Contains(searchTerm.ToLower()) ||
          (userService != null && userService.GetUserById(p.UserID)?.UserName?.ToLower().Contains(searchTerm.ToLower()) == true)
      ).ToList();

      DisplayPosts(filteredPosts);
    }

    // Filter by visibility
    private void CmbVisibilityFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
      string? selectedFilter = cmbVisibilityFilter?.SelectedItem?.ToString();
      if (!string.IsNullOrEmpty(selectedFilter))
      {
        FilterPostsByVisibility(selectedFilter);
      }
    }

    private void FilterPostsByVisibility(string visibility)
    {
      if (allPosts == null) return;

      if (string.IsNullOrWhiteSpace(visibility) || visibility == "Tất cả")
      {
        DisplayPosts(allPosts);
        return;
      }

      var filteredPosts = allPosts.Where(p => p.Visibility == visibility).ToList();
      DisplayPosts(filteredPosts);
    }

    private void dgvPosts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && dgvPosts != null)
      {
        try
        {
          int postId = Convert.ToInt32(dgvPosts.Rows[e.RowIndex].Cells["PostID"].Value);
          var post = allPosts.FirstOrDefault(p => p.PostID == postId);

          if (post != null)
          {
            var user = userService.GetUserById(post.UserID);
            string userName = user?.UserName ?? "Unknown User";

            string message = $"Người đăng: {userName}\n\n" +
                           $"Nội dung:\n{post.Content}\n\n" +
                           $"Quyền riêng tư: {GetVisibilityDisplay(post.Visibility)}\n" +
                           $"Lượt thích: {post.LikesCount}\n" +
                           $"Bình luận: {post.CommentsCount}\n" +
                           $"Ngày tạo: {post.CreatedAt:dd/MM/yyyy HH:mm}\n";

            if (!string.IsNullOrEmpty(post.MediaUrl))
              message += $"Hình ảnh: {post.MediaUrl}";

            MessageBox.Show(message, "Chi tiết bài viết",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Lỗi khi hiển thị chi tiết: {ex.Message}", "Lỗi",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private void pnlCard_Paint(object sender, PaintEventArgs e)
    {
      Panel? panel = sender as Panel;
      if (panel != null)
      {
        try
        {
          e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

          using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
          {
            GraphicsExtensions.FillRoundedRectangle(e.Graphics, shadowBrush, new Rectangle(3, 3, panel.Width - 3, panel.Height - 3), 12);
          }

          using (var cardBrush = new SolidBrush(Color.White))
          {
            GraphicsExtensions.FillRoundedRectangle(e.Graphics, cardBrush, new Rectangle(0, 0, panel.Width - 3, panel.Height - 3), 12);
          }

          using (var borderPen = new Pen(Color.FromArgb(230, 230, 230), 1))
          {
            GraphicsExtensions.DrawRoundedRectangle(e.Graphics, borderPen, new Rectangle(0, 0, panel.Width - 4, panel.Height - 4), 12);
          }
        }
        catch (Exception ex)
        {
          System.Diagnostics.Debug.WriteLine($"Error in pnlCard_Paint: {ex.Message}");
          e.Graphics.FillRectangle(Brushes.White, panel.ClientRectangle);
          e.Graphics.DrawRectangle(Pens.LightGray, 0, 0, panel.Width - 1, panel.Height - 1);
        }
      }
    }
  }
}
