using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SocialManager.services
{
  public class PostService
  {
    private const string POST_DATA_PATH = "datas\\Post.csv";

    public PostService()
    {
      // Ensure data directory exists
      EnsureDataDirectory();
      EnsurePostFile();
    }

    private void EnsureDataDirectory()
    {
      string? directory = Path.GetDirectoryName(POST_DATA_PATH);
      if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
      {
        Directory.CreateDirectory(directory);
      }
    }

    private void EnsurePostFile()
    {
      if (!File.Exists(POST_DATA_PATH))
      {
        // Create header for CSV file with IsDeleted column
        string header = "PostID,UserID,Content,MediaUrl,Visibility,LikesCount,CommentsCount,CreatedAt,UpdatedAt,IsDeleted";
        File.WriteAllText(POST_DATA_PATH, header + Environment.NewLine);
      }
      else
      {
        // Migration: Add IsDeleted column if not exists
        MigratePostCsvIfNeeded();
      }
    }

    /// <summary>
    /// Migration: Thêm cột IsDeleted vào CSV cũ nếu chưa có
    /// </summary>
    private void MigratePostCsvIfNeeded()
    {
      try
      {
        var lines = File.ReadAllLines(POST_DATA_PATH).ToList();
        if (lines.Count == 0)
          return;

        string currentHeader = lines[0];
        bool needsMigration = false;

        // Check nếu header thiếu IsDeleted
        if (!currentHeader.Contains("IsDeleted"))
        {
          lines[0] = "PostID,UserID,Content,MediaUrl,Visibility,LikesCount,CommentsCount,CreatedAt,UpdatedAt,IsDeleted";
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
          File.WriteAllLines(POST_DATA_PATH, lines);
          Console.WriteLine("Post CSV migrated to include IsDeleted column");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi migration Post CSV: {ex.Message}");
      }
    }

    public bool CreatePost(string content, string mediaUrl, string visibility, DateTime? scheduledDate = null)
    {
      try
      {
        // Get current user from session
        var currentUser = AuthSessionService.CurrentUser;
        if (currentUser == null)
        {
          throw new InvalidOperationException("User not authenticated");
        }

        // Generate new post ID
        int newPostId = GetNextPostId();

        // Create post entry
        DateTime postDate = scheduledDate ?? DateTime.Now;
        string postLine = $"{newPostId},{currentUser.UserID},\"{content.Replace("\"", "\"\"")}\",{mediaUrl},{visibility},0,0,{postDate:yyyy-MM-dd HH:mm:ss},,False";

        // Append to file
        File.AppendAllText(POST_DATA_PATH, postLine + Environment.NewLine);

        return true;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error creating post: {ex.Message}");
        return false;
      }
    }

    public List<Post> GetUserPosts(Guid userId)
    {
      List<Post> posts = new List<Post>();

      if (!File.Exists(POST_DATA_PATH))
        return posts;

      try
      {
        var lines = File.ReadAllLines(POST_DATA_PATH).Skip(1); // Skip header

        foreach (string line in lines)
        {
          if (string.IsNullOrWhiteSpace(line))
            continue;

          var post = ParsePostFromCsv(line);
          if (post != null && post.UserID == userId)
          {
            posts.Add(post);
          }
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error reading posts: {ex.Message}");
      }

      return posts.OrderByDescending(p => p.CreatedAt).ToList();
    }

    public List<Post> GetAllPosts()
    {
      return GetAllPosts(includeDeleted: false);
    }

    public List<Post> GetAllPosts(bool includeDeleted)
    {
      List<Post> posts = new List<Post>();

      if (!File.Exists(POST_DATA_PATH))
        return posts;

      try
      {
        var lines = File.ReadAllLines(POST_DATA_PATH).Skip(1); // Skip header

        foreach (string line in lines)
        {
          if (string.IsNullOrWhiteSpace(line))
            continue;

          var post = ParsePostFromCsv(line);
          if (post != null)
          {
            // Lọc bài viết đã xóa nếu includeDeleted = false
            if (includeDeleted || !post.IsDeleted)
            {
              posts.Add(post);
            }
          }
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error reading posts: {ex.Message}");
      }

      return posts.OrderByDescending(p => p.CreatedAt).ToList();
    }

    private Post? ParsePostFromCsv(string csvLine)
    {
      try
      {
        // Simple CSV parsing - in production you'd want more robust parsing
        string[] values = csvLine.Split(',');

        if (values.Length < 8)
          return null;

        var post = new Post();

        if (int.TryParse(values[0], out int postId))
          post.PostID = postId;

        if (Guid.TryParse(values[1], out Guid userId))
          post.UserID = userId;

        post.Content = values[2].Trim('"').Replace("\"\"", "\"");
        post.MediaUrl = values[3];
        post.Visibility = values[4];

        if (int.TryParse(values[5], out int likes))
          post.LikesCount = likes;

        if (int.TryParse(values[6], out int comments))
          post.CommentsCount = comments;

        if (DateTime.TryParse(values[7], out DateTime created))
          post.CreatedAt = created;

        if (values.Length > 8 && DateTime.TryParse(values[8], out DateTime updated))
          post.UpdatedAt = updated;

        // Parse IsDeleted (column 10)
        if (values.Length > 9 && bool.TryParse(values[9], out bool isDeleted))
          post.IsDeleted = isDeleted;
        else
          post.IsDeleted = false;

        return post;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error parsing post from CSV: {ex.Message}");
        return null;
      }
    }

    private int GetNextPostId()
    {
      if (!File.Exists(POST_DATA_PATH))
        return 1;

      try
      {
        var lines = File.ReadAllLines(POST_DATA_PATH).Skip(1);
        int maxId = 0;

        foreach (string line in lines)
        {
          if (string.IsNullOrWhiteSpace(line))
            continue;

          var parts = line.Split(',');
          if (parts.Length > 0 && int.TryParse(parts[0], out int id))
          {
            maxId = Math.Max(maxId, id);
          }
        }

        return maxId + 1;
      }
      catch
      {
        return 1;
      }
    }

    public bool UpdatePost(int postId, string content)
    {
      try
      {
        if (!File.Exists(POST_DATA_PATH))
          return false;

        var lines = File.ReadAllLines(POST_DATA_PATH).ToList();

        for (int i = 1; i < lines.Count; i++) // Skip header
        {
          if (string.IsNullOrWhiteSpace(lines[i]))
            continue;

          var parts = lines[i].Split(',');
          if (parts.Length > 0 && int.TryParse(parts[0], out int id) && id == postId)
          {
            // Update content and updated date
            parts[2] = $"\"{content.Replace("\"", "\"\"")}\"";
            if (parts.Length > 8)
            {
              parts[8] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            lines[i] = string.Join(",", parts);
            break;
          }
        }

        File.WriteAllLines(POST_DATA_PATH, lines);
        return true;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error updating post: {ex.Message}");
        return false;
      }
    }

    public bool DeletePost(int postId)
    {
      try
      {
        if (!File.Exists(POST_DATA_PATH))
          return false;

        var lines = File.ReadAllLines(POST_DATA_PATH).ToList();

        for (int i = lines.Count - 1; i >= 1; i--) // Skip header, iterate backwards
        {
          if (string.IsNullOrWhiteSpace(lines[i]))
            continue;

          var parts = lines[i].Split(',');
          if (parts.Length > 0 && int.TryParse(parts[0], out int id) && id == postId)
          {
            lines.RemoveAt(i);
            break;
          }
        }

        File.WriteAllLines(POST_DATA_PATH, lines);
        return true;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error deleting post: {ex.Message}");
        return false;
      }
    }

    /// <summary>
    /// Soft delete: Đánh dấu bài viết là đã xóa (IsDeleted = true)
    /// </summary>
    public bool SoftDeletePost(int postId)
    {
      try
      {
        if (!File.Exists(POST_DATA_PATH))
          return false;

        var lines = File.ReadAllLines(POST_DATA_PATH).ToList();
        bool found = false;

        for (int i = 1; i < lines.Count; i++) // Skip header
        {
          if (string.IsNullOrWhiteSpace(lines[i]))
            continue;

          var parts = lines[i].Split(',');
          if (parts.Length > 0 && int.TryParse(parts[0], out int id) && id == postId)
          {
            // Update UpdatedAt (column 9) and IsDeleted (column 10)
            if (parts.Length >= 10)
            {
              parts[8] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // UpdatedAt
              parts[9] = "True"; // IsDeleted
            }
            else if (parts.Length == 9)
            {
              parts[8] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
              lines[i] = string.Join(",", parts) + ",True";
              found = true;
              break;
            }
            else
            {
              // Old format - just append
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
          File.WriteAllLines(POST_DATA_PATH, lines);
          return true;
        }

        return false;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error soft deleting post: {ex.Message}");
        return false;
      }
    }

    /// <summary>
    /// Khôi phục bài viết đã xóa (IsDeleted = false)
    /// </summary>
    public bool RestorePost(int postId)
    {
      try
      {
        if (!File.Exists(POST_DATA_PATH))
          return false;

        var lines = File.ReadAllLines(POST_DATA_PATH).ToList();
        bool found = false;

        for (int i = 1; i < lines.Count; i++) // Skip header
        {
          if (string.IsNullOrWhiteSpace(lines[i]))
            continue;

          var parts = lines[i].Split(',');
          if (parts.Length > 0 && int.TryParse(parts[0], out int id) && id == postId)
          {
            // Update IsDeleted (column 10) to False
            if (parts.Length >= 10)
            {
              parts[8] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // UpdatedAt
              parts[9] = "False"; // IsDeleted
              lines[i] = string.Join(",", parts);
              found = true;
              break;
            }
          }
        }

        if (found)
        {
          File.WriteAllLines(POST_DATA_PATH, lines);
          return true;
        }

        return false;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error restoring post: {ex.Message}");
        return false;
      }
    }
  }
}
