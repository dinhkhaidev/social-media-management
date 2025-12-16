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
        // Skip header lines
        if (csvLine.StartsWith("PostID,") || string.IsNullOrWhiteSpace(csvLine))
          return null;

        // Simple CSV parsing - in production you'd want more robust parsing
        string[] values = csvLine.Split(',');

        if (values.Length < 8)
          return null;

        // Skip if first column is not a valid PostID
        if (!int.TryParse(values[0], out int postId))
          return null;

        var post = new Post();
        post.PostID = postId;

        if (Guid.TryParse(values[1], out Guid userId))
          post.UserID = userId;
        else
          return null; // Invalid UserID

        post.Content = values[2].Trim('"').Replace("\"\"", "\"");
        post.MediaUrl = values[3];
        post.Visibility = values[4];

        if (int.TryParse(values[5], out int likes))
          post.LikesCount = likes;

        if (int.TryParse(values[6], out int comments))
          post.CommentsCount = comments;

        if (DateTime.TryParse(values[7], out DateTime created))
          post.CreatedAt = created;

        if (values.Length > 8 && !string.IsNullOrWhiteSpace(values[8]) && DateTime.TryParse(values[8], out DateTime updated))
          post.UpdatedAt = updated;

        // Parse IsDeleted (column 10) - handle different positions due to CSV inconsistency
        bool isDeleted = false;
        if (values.Length > 9)
        {
          // Try parsing the last column as IsDeleted
          string lastValue = values[values.Length - 1].Trim();
          if (bool.TryParse(lastValue, out isDeleted))
          {
            post.IsDeleted = isDeleted;
          }
          else
          {
            // If last column is not boolean, try column 9
            if (values.Length > 9 && bool.TryParse(values[9], out isDeleted))
              post.IsDeleted = isDeleted;
            else
              post.IsDeleted = false;
          }
        }
        else
        {
          post.IsDeleted = false;
        }

        return post;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error parsing post from CSV: {ex.Message} - Line: {csvLine}");
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
          if (string.IsNullOrWhiteSpace(lines[i]) || lines[i].StartsWith("PostID,"))
            continue;

          var parts = lines[i].Split(',');
          if (parts.Length > 0 && int.TryParse(parts[0], out int id) && id == postId)
          {
            // Ensure we have enough columns (minimum 10 for IsDeleted)
            var newParts = new List<string>();
            
            // Copy existing parts (up to 8 columns: PostID to CreatedAt)
            for (int j = 0; j < Math.Min(parts.Length, 8); j++)
            {
              newParts.Add(parts[j]);
            }
            
            // Add UpdatedAt (column 9)
            newParts.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
            // Add IsDeleted (column 10)
            newParts.Add("True");
            
            lines[i] = string.Join(",", newParts);
            found = true;
            break;
          }
        }

        if (found)
        {
          File.WriteAllLines(POST_DATA_PATH, lines);
          System.Diagnostics.Debug.WriteLine($"Successfully soft deleted post {postId}");
          return true;
        }

        System.Diagnostics.Debug.WriteLine($"Post {postId} not found for soft delete");
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
          if (string.IsNullOrWhiteSpace(lines[i]) || lines[i].StartsWith("PostID,"))
            continue;

          var parts = lines[i].Split(',');
          if (parts.Length > 0 && int.TryParse(parts[0], out int id) && id == postId)
          {
            // Ensure we have enough columns (minimum 10 for IsDeleted)
            var newParts = new List<string>();
            
            // Copy existing parts (up to 8 columns: PostID to CreatedAt)
            for (int j = 0; j < Math.Min(parts.Length, 8); j++)
            {
              newParts.Add(parts[j]);
            }
            
            // Add UpdatedAt (column 9)
            newParts.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
            // Add IsDeleted (column 10)
            newParts.Add("False");
            
            lines[i] = string.Join(",", newParts);
            found = true;
            break;
          }
        }

        if (found)
        {
          File.WriteAllLines(POST_DATA_PATH, lines);
          System.Diagnostics.Debug.WriteLine($"Successfully restored post {postId}");
          return true;
        }

        System.Diagnostics.Debug.WriteLine($"Post {postId} not found for restore");
        return false;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error restoring post: {ex.Message}");
        return false;
      }
    }

    public static bool ReportPost(int postId, Guid reporterUserId, out Guid? reportedUserId, string reason = "")
    {
      reportedUserId = null;
      try
      {
        var postService = new PostService();
        var post = postService.GetAllPosts(includeDeleted: true).FirstOrDefault(p => p.PostID == postId);

        if (post == null)
        {
          System.Windows.Forms.MessageBox.Show("Không tìm thấy bài viết!", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
          return false;
        }

        reportedUserId = post.UserID;

        if (post.UserID == reporterUserId)
        {
          System.Windows.Forms.MessageBox.Show("Bạn không thể tố cáo bài viết của chính mình!", "Cảnh báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
          return false;
        }

        var reportService = new ReportService();
        bool reportCreated = reportService.CreateReport("Post", postId, reporterUserId, post.UserID, reason);
        if (!reportCreated)
        {
          System.Windows.Forms.MessageBox.Show("Không thể tạo báo cáo tố cáo!", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
          return false;
        }

        bool postDeleted = postService.SoftDeletePost(postId);
        if (!postDeleted)
        {
          System.Windows.Forms.MessageBox.Show("Đã tạo báo cáo nhưng không thể ẩn bài viết!", "Cảnh báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
        }

        var userService = new UserService();
        var reportedUser = userService.GetAllUsers().FirstOrDefault(u => u.UserID == post.UserID);
        if (reportedUser != null)
        {
          reportedUser.ReportCount++;
          userService.UpdateUser(reportedUser);
        }

        return true;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error reporting post: {ex.Message}");
        System.Windows.Forms.MessageBox.Show($"Lỗi khi tố cáo bài viết: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        return false;
      }
    }
/// <summary>
    /// Get posts that a user has commented on
    /// </summary>
    public List<Post> GetPostsCommentedByUser(Guid userId)
    {
      try
      {
        var commentService = new CommentService();
        var allComments = commentService.GetAllComments();

        // Get unique post IDs that user has commented on
        var postIds = allComments
          .Where(c => c.UserID == userId && !c.IsDeleted)
          .Select(c => c.PostID)
          .Distinct()
          .ToList();

        // Get posts for those IDs
        var allPosts = GetAllPosts(includeDeleted: false);
        return allPosts
          .Where(p => postIds.Contains(p.PostID))
          .OrderByDescending(p => p.CreatedAt)
          .ToList();
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error getting commented posts: {ex.Message}");
        return new List<Post>();
      }
    }

    /// <summary>
    /// Get posts that a user has liked
    /// </summary>
    public List<Post> GetPostsLikedByUser(Guid userId)
    {
      try
      {
        const string LIKES_FILE_PATH = "datas\\Likes.csv";

        if (!File.Exists(LIKES_FILE_PATH))
          return new List<Post>();

        var likedPostIds = new List<int>();
        var lines = File.ReadAllLines(LIKES_FILE_PATH).Skip(1); // Skip header

        foreach (string line in lines)
        {
          if (string.IsNullOrWhiteSpace(line))
            continue;

          var parts = line.Split(',');
          if (parts.Length >= 3)
          {
            // Format: LikeID,PostID,UserID,CreatedAt
            if (Guid.TryParse(parts[2], out Guid likeUserId) && likeUserId == userId)
            {
              if (int.TryParse(parts[1], out int postId))
              {
                likedPostIds.Add(postId);
              }
            }
          }
        }

        // Get posts for those IDs
        var allPosts = GetAllPosts(includeDeleted: false);
        return allPosts
          .Where(p => likedPostIds.Contains(p.PostID))
          .OrderByDescending(p => p.CreatedAt)
          .ToList();
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error getting liked posts: {ex.Message}");
        return new List<Post>();
      }
    }
  }
}