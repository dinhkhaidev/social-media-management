using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SocialManager
{
  public class Post
  {
    public int PostID { get; set; } // Changed from private set to public set
    public Guid UserID { get; set; }
    public string Content { get; set; }
    public string MediaUrl { get; set; }
    public string Visibility { get; set; } // Public, Friends, Private
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Post()
    {
      Content = "";
      MediaUrl = "";
      Visibility = "Public";
      LikesCount = 0;
      CommentsCount = 0;
      CreatedAt = DateTime.Now;
    }

    public Post(string csvLine)
    {
      // Initialize default values
      Content = "";
      MediaUrl = "";
      Visibility = "Public";

      string[] values = csvLine.Split(',');
      if (values.Length >= 8)
      {
        int.TryParse(values[0], out int postId);
        this.PostID = postId;

        Guid.TryParse(values[1], out Guid userId);
        this.UserID = userId;

        this.Content = values[2].Trim('"').Replace("\"\"", "\"");
        this.MediaUrl = values[3];
        this.Visibility = values[4];

        int.TryParse(values[5], out int likesCount);
        this.LikesCount = likesCount;

        int.TryParse(values[6], out int commentsCount);
        this.CommentsCount = commentsCount;

        DateTime.TryParse(values[7], out DateTime createdAt);
        this.CreatedAt = createdAt;

        // Handle UpdatedAt which might be empty
        if (values.Length > 8 && DateTime.TryParse(values[8], out DateTime updatedAt))
        {
          this.UpdatedAt = updatedAt;
        }
        else
        {
          this.UpdatedAt = null;
        }
      }
    }

    public static List<Post> GetList(string filePath)
    {
      List<Post> postList = new List<Post>();

      if (!File.Exists(filePath))
      {
        Console.WriteLine($"Error: File not found at {filePath}");
        return postList;
      }

      try
      {
        var lines = File.ReadLines(filePath).Skip(1); // Skip header

        foreach (string line in lines)
        {
          if (!string.IsNullOrWhiteSpace(line))
          {
            Post post = new Post(line);
            // Only add if PostID was parsed successfully
            if (post.PostID > 0)
            {
              postList.Add(post);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while reading the post file: {ex.Message}");
      }

      return postList;
    }

    public string ToCsvString()
    {
      string updatedAtStr = UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "";
      return $"{PostID},{UserID},\"{Content.Replace("\"", "\"\"")}\"," +
             $"{MediaUrl},{Visibility},{LikesCount},{CommentsCount}," +
             $"{CreatedAt:yyyy-MM-dd HH:mm:ss},{updatedAtStr}";
    }

    public override string ToString()
    {
      return $"Post {PostID}: {Content.Substring(0, Math.Min(50, Content.Length))}...";
    }
  }
}
