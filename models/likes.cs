using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SocialManager
{
  public class Like
  {
    public int LikeID { get; set; }
    public int PostID { get; set; }
    public Guid UserID { get; set; }
    public DateTime CreatedAt { get; set; }

    // --- Constructors ---
    public Like()
    {
      CreatedAt = DateTime.Now;
    }

    public Like(string csvLine)
    {
      string[] values = csvLine.Split(',');
      if (values.Length >= 4)
      {
        int.TryParse(values[0], out int likeId);
        this.LikeID = likeId;

        int.TryParse(values[1], out int postId);
        this.PostID = postId;

        Guid.TryParse(values[2], out Guid userId);
        this.UserID = userId;

        DateTime.TryParse(values[3], out DateTime createdAt);
        this.CreatedAt = createdAt;
      }
    }

    // --- Static Methods ---
    public static List<Like> GetList(string filePath)
    {
      List<Like> likeList = new List<Like>();

      if (!File.Exists(filePath))
      {
        Console.WriteLine($"Error: File not found at {filePath}");
        return likeList;
      }

      try
      {
        var lines = File.ReadLines(filePath).Skip(1); // Skip header

        foreach (string line in lines)
        {
          if (!string.IsNullOrWhiteSpace(line))
          {
            Like like = new Like(line);
            if (like.LikeID > 0)
            {
              likeList.Add(like);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while reading the like file: {ex.Message}");
      }

      return likeList;
    }

    // Lấy danh sách likes của một post
    public static List<Like> GetLikesByPostId(string filePath, int postId)
    {
      var allLikes = GetList(filePath);
      return allLikes.Where(l => l.PostID == postId).ToList();
    }

    // Kiểm tra user đã like post chưa
    public static bool HasUserLikedPost(string filePath, int postId, Guid userId)
    {
      var allLikes = GetList(filePath);
      return allLikes.Any(l => l.PostID == postId && l.UserID == userId);
    }

    // --- Instance Methods ---
    public string ToCsvLine()
    {
      return $"{LikeID},{PostID},{UserID},{CreatedAt:yyyy-MM-dd HH:mm:ss}";
    }

    // Thêm like mới
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

        // Tạo LikeID mới
        if (this.LikeID == 0)
        {
          var allLikes = GetList(filePath);
          this.LikeID = allLikes.Count > 0 ? allLikes.Max(l => l.LikeID) + 1 : 1;
        }

        // Write to file
        if (!File.Exists(filePath))
        {
          // Create new file with header
          string header = "LikeID,PostID,UserID,CreatedAt";
          File.WriteAllText(filePath, header + Environment.NewLine + this.ToCsvLine(), Encoding.UTF8);
        }
        else
        {
          // Append to existing file
          File.AppendAllText(filePath, Environment.NewLine + this.ToCsvLine(), Encoding.UTF8);
        }

        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error saving like: {ex.Message}");
        return false;
      }
    }

    // Xóa like (unlike)
    public bool Delete(string filePath)
    {
      try
      {
        if (!File.Exists(filePath))
          return false;

        var lines = File.ReadAllLines(filePath, Encoding.UTF8).ToList();
        bool deleted = false;

        // Tìm và xóa like
        for (int i = 1; i < lines.Count; i++) // Skip header
        {
          var parts = lines[i].Split(',');
          if (parts.Length >= 3)
          {
            if (int.TryParse(parts[1], out int postId) &&
                Guid.TryParse(parts[2], out Guid userId))
            {
              if (postId == this.PostID && userId == this.UserID)
              {
                lines.RemoveAt(i);
                deleted = true;
                break;
              }
            }
          }
        }

        if (deleted)
        {
          // Ghi lại file
          File.WriteAllLines(filePath, lines, Encoding.UTF8);
          return true;
        }

        return false;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error deleting like: {ex.Message}");
        return false;
      }
    }
  }
}
