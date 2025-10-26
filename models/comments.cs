using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SocialManager
{
  public class Comment
  {
    // --- Properties ---
    public int CommentID { get; private set; }
    public int PostID { get; set; }
    public Guid UserID { get; set; }
    public int? ParentCommentID { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // --- Constructors ---

    public Comment()
    {
      Content = "";
    }

    public Comment(string csvLine)
    {
      // Khởi tạo mặc định
      Content = "";

      string[] values = csvLine.Split(',');
      if (values.Length >= 7)
      {
        int.TryParse(values[0], out int commentId);
        this.CommentID = commentId;

        int.TryParse(values[1], out int postId);
        this.PostID = postId;

        Guid.TryParse(values[2], out Guid userId);
        this.UserID = userId;

        //ParentCommentID can Null
        if (int.TryParse(values[3], out int parentId))
        {
          this.ParentCommentID = parentId;
        }
        else
        {
          this.ParentCommentID = null; // Gán null nếu không parse được
        }

        this.Content = values[4];

        DateTime.TryParse(values[5], out DateTime createdAt);
        this.CreatedAt = createdAt;

        // Xử lý UpdatedAt có thể rỗng
        if (DateTime.TryParse(values[6], out DateTime updatedAt))
        {
          this.UpdatedAt = updatedAt;
        }
        else
        {
          this.UpdatedAt = null;
        }
      }
    }

    // --- Methods ---

    public static List<Comment> GetList(string filePath)
    {
      List<Comment> commentList = new List<Comment>();

      if (!File.Exists(filePath))
      {
        Console.WriteLine($"Error: File not found at {filePath}");
        return commentList;
      }

      try
      {
        var lines = File.ReadLines(filePath).Skip(1);

        foreach (string line in lines)
        {
          if (!string.IsNullOrWhiteSpace(line))
          {
            Comment comment = new Comment(line);
            // Chỉ thêm nếu parse được CommentID
            if (comment.CommentID > 0)
            {
              commentList.Add(comment);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while reading the comment file: {ex.Message}");
      }

      return commentList;
    }

    // Lấy danh sách comments của một post
    public static List<Comment> GetCommentsByPostId(string filePath, int postId)
    {
      var allComments = GetList(filePath);
      return allComments.Where(c => c.PostID == postId).OrderBy(c => c.CreatedAt).ToList();
    }

    // --- Instance Methods ---
    public string ToCsvLine()
    {
      string parentId = ParentCommentID?.ToString() ?? "";
      string updatedAt = UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "";
      return $"{CommentID},{PostID},{UserID},{parentId},{Content},{CreatedAt:yyyy-MM-dd HH:mm:ss},{updatedAt}";
    }

    // Lưu comment mới
    public bool Save(string filePath)
    {
      try
      {
        // Ensure directory exists
        string? directoryPath = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
        {
          Directory.CreateDirectory(directoryPath);
        }

        // Tạo CommentID mới nếu chưa có
        if (this.CommentID == 0)
        {
          var allComments = GetList(filePath);
          this.CommentID = allComments.Count > 0 ? allComments.Max(c => c.CommentID) + 1 : 1;
        }

        // Write to file
        if (!File.Exists(filePath))
        {
          // Create new file with header
          string header = "CommentID,PostID,UserID,ParentCommentID,Content,CreatedAt,UpdatedAt";
          File.WriteAllText(filePath, header + Environment.NewLine + this.ToCsvLine(), System.Text.Encoding.UTF8);
        }
        else
        {
          // Append to existing file
          File.AppendAllText(filePath, Environment.NewLine + this.ToCsvLine(), System.Text.Encoding.UTF8);
        }

        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error saving comment: {ex.Message}");
        return false;
      }
    }

    // Xóa comment
    public bool Delete(string filePath)
    {
      try
      {
        if (!File.Exists(filePath))
          return false;

        var lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8).ToList();
        bool deleted = false;

        // Tìm và xóa comment
        for (int i = 1; i < lines.Count; i++) // Skip header
        {
          var parts = lines[i].Split(',');
          if (parts.Length > 0 && int.TryParse(parts[0], out int commentId))
          {
            if (commentId == this.CommentID)
            {
              lines.RemoveAt(i);
              deleted = true;
              break;
            }
          }
        }

        if (deleted)
        {
          // Ghi lại file
          File.WriteAllLines(filePath, lines, System.Text.Encoding.UTF8);
          return true;
        }

        return false;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error deleting comment: {ex.Message}");
        return false;
      }
    }
  }
}
