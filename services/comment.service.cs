using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SocialManager.services
{
  public class CommentService
  {
    private const string COMMENT_FILE_PATH = "datas\\Comment.csv";
    private const string HEADER = "CommentID,PostID,UserID,ParentCommentID,Content,CreatedAt,UpdatedAt,IsDeleted";

    public CommentService()
    {
      EnsureCommentFile();
    }

    private void EnsureCommentFile()
    {
      // Ensure directory exists
      string? directoryPath = Path.GetDirectoryName(COMMENT_FILE_PATH);
      if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
      {
        Directory.CreateDirectory(directoryPath);
      }

      if (!File.Exists(COMMENT_FILE_PATH))
      {
        File.WriteAllText(COMMENT_FILE_PATH, HEADER + Environment.NewLine);
      }
      else
      {
        MigrateCommentCsvIfNeeded();
      }
    }

    /// <summary>
    /// Migration: Thêm cột IsDeleted vào CSV cũ nếu chưa có
    /// </summary>
    private void MigrateCommentCsvIfNeeded()
    {
      try
      {
        var lines = File.ReadAllLines(COMMENT_FILE_PATH, Encoding.UTF8).ToList();
        if (lines.Count == 0)
          return;

        string currentHeader = lines[0];
        bool needsMigration = false;

        // Check nếu header thiếu IsDeleted
        if (!currentHeader.Contains("IsDeleted"))
        {
          lines[0] = HEADER;
          needsMigration = true;

          // Add IsDeleted=False to existing records
          for (int i = 1; i < lines.Count; i++)
          {
            if (!string.IsNullOrWhiteSpace(lines[i]))
            {
              lines[i] += ",False"; // Add default IsDeleted=False
            }
          }
        }

        if (needsMigration)
        {
          File.WriteAllLines(COMMENT_FILE_PATH, lines, Encoding.UTF8);
          Console.WriteLine("Comment CSV migrated to include IsDeleted column");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi migration Comment CSV: {ex.Message}");
      }
    }

    /// <summary>
    /// Lấy tất cả comment (mặc định ẩn comment đã xóa)
    /// </summary>
    public List<Comment> GetAllComments(bool includeDeleted = false)
    {
      try
      {
        var allComments = Comment.GetList(COMMENT_FILE_PATH);

        if (!includeDeleted)
        {
          allComments = allComments.Where(c => !c.IsDeleted).ToList();
        }

        return allComments.OrderByDescending(c => c.CreatedAt).ToList();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi tải comment: {ex.Message}");
        return new List<Comment>();
      }
    }

    /// <summary>
    /// Lấy tất cả comment của một bài viết (mặc định ẩn comment đã xóa)
    /// </summary>
    public List<Comment> GetCommentsByPostId(int postId, bool includeDeleted = false)
    {
      try
      {
        var allComments = Comment.GetList(COMMENT_FILE_PATH);
        var postComments = allComments.Where(c => c.PostID == postId);

        if (!includeDeleted)
        {
          postComments = postComments.Where(c => !c.IsDeleted);
        }

        return postComments.OrderBy(c => c.CreatedAt).ToList();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi tải comment: {ex.Message}");
        return new List<Comment>();
      }
    }

    /// <summary>
    /// Xóa comment (soft delete: đánh dấu IsDeleted = true)
    /// </summary>
    public bool DeleteComment(int commentId)
    {
      return SoftDeleteComment(commentId);
    }

    /// <summary>
    /// Soft delete: Đánh dấu comment là đã xóa (IsDeleted = true)
    /// </summary>
    public bool SoftDeleteComment(int commentId)
    {
      try
      {
        if (!File.Exists(COMMENT_FILE_PATH))
          return false;

        var lines = File.ReadAllLines(COMMENT_FILE_PATH, Encoding.UTF8).ToList();
        bool found = false;

        for (int i = 1; i < lines.Count; i++) // Skip header
        {
          if (string.IsNullOrWhiteSpace(lines[i]))
            continue;

          var parts = lines[i].Split(',');
          if (parts.Length > 0 && int.TryParse(parts[0], out int id) && id == commentId)
          {
            // Update UpdatedAt (column 7) and IsDeleted (column 8)
            if (parts.Length >= 8)
            {
              parts[6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // UpdatedAt
              parts[7] = "True"; // IsDeleted
            }
            else if (parts.Length == 7)
            {
              parts[6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
              lines[i] = string.Join(",", parts) + ",True";
              found = true;
              break;
            }
            else
            {
              // Old format - append both UpdatedAt and IsDeleted
              lines[i] = lines[i] + $",{DateTime.Now:yyyy-MM-dd HH:mm:ss},True";
              found = true;
              break;
            }

            lines[i] = string.Join(",", parts);
            found = true;
            break;
          }
        }

        if (found)
        {
          File.WriteAllLines(COMMENT_FILE_PATH, lines, Encoding.UTF8);
          return true;
        }

        return false;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi xóa comment: {ex.Message}");
        return false;
      }
    }

    /// <summary>
    /// Khôi phục comment đã xóa (IsDeleted = false)
    /// </summary>
    public bool RestoreComment(int commentId)
    {
      try
      {
        if (!File.Exists(COMMENT_FILE_PATH))
          return false;

        var lines = File.ReadAllLines(COMMENT_FILE_PATH, Encoding.UTF8).ToList();
        bool found = false;

        for (int i = 1; i < lines.Count; i++) // Skip header
        {
          if (string.IsNullOrWhiteSpace(lines[i]))
            continue;

          var parts = lines[i].Split(',');
          if (parts.Length > 0 && int.TryParse(parts[0], out int id) && id == commentId)
          {
            // Update IsDeleted (column 8) to False
            if (parts.Length >= 8)
            {
              parts[6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // UpdatedAt
              parts[7] = "False"; // IsDeleted
              lines[i] = string.Join(",", parts);
              found = true;
              break;
            }
          }
        }

        if (found)
        {
          File.WriteAllLines(COMMENT_FILE_PATH, lines, Encoding.UTF8);
          return true;
        }

        return false;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi khôi phục comment: {ex.Message}");
        return false;
      }
    }

    /// <summary>
    /// Tạo comment mới
    /// </summary>
    public bool CreateComment(int postId, Guid userId, string content, int? parentCommentId = null)
    {
      try
      {
        var newComment = new Comment
        {
          PostID = postId,
          UserID = userId,
          ParentCommentID = parentCommentId,
          Content = content,
          CreatedAt = DateTime.Now,
          UpdatedAt = null,
          IsDeleted = false
        };

        return newComment.Save(COMMENT_FILE_PATH);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi tạo comment: {ex.Message}");
        return false;
      }
    }



    /// <summary>
    /// Tố cáo comment: Soft delete comment và ghi lại report
    /// </summary>
    public bool ReportComment(int commentId, Guid reporterUserId, out Guid? reportedUserId)
    {
      reportedUserId = null;

      try
      {
        // Tìm comment để lấy UserID
        var allComments = Comment.GetList(COMMENT_FILE_PATH);
        var comment = allComments.FirstOrDefault(c => c.CommentID == commentId);

        if (comment == null)
          return false;

        reportedUserId = comment.UserID;

        // Kiểm tra không cho tự report chính mình
        if (reporterUserId == comment.UserID)
          return false;

        // Ghi lại report vào Report.csv
        var reportService = new ReportService();
        if (!reportService.CreateReport("Comment", commentId, reporterUserId, comment.UserID))
          return false;

        // Soft delete comment
        if (!SoftDeleteComment(commentId))
          return false;

        // Cập nhật StatusId của user nếu đạt ngưỡng
        var userService = new UserService();
        int reportCount = reportService.GetReportCountForUser(comment.UserID);

        if (reportCount >= 100)
        {
          userService.UpdateUserStatus(comment.UserID, -1); // Ban user
        }

        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi tố cáo comment: {ex.Message}");
        return false;
      }
    }
  }
}
