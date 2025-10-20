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
            string directory = Path.GetDirectoryName(POST_DATA_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private void EnsurePostFile()
        {
            if (!File.Exists(POST_DATA_PATH))
            {
                // Create header for CSV file
                string header = "PostID,UserID,Content,MediaUrl,Visibility,LikesCount,CommentsCount,CreatedAt,UpdatedAt";
                File.WriteAllText(POST_DATA_PATH, header + Environment.NewLine);
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
                string postLine = $"{newPostId},{currentUser.UserID},\"{content.Replace("\"", "\"\"")}\",{mediaUrl},{visibility},0,0,{postDate:yyyy-MM-dd HH:mm:ss},";
                
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

        private Post ParsePostFromCsv(string csvLine)
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
    }
}

//namespace SocialManager
//{
//    public partial class Post
//    {
//        // Method to set PostID (since it's private set)
//        public void SetPostID(int id)
//        {
//            // Use reflection or make PostID public set in the model
//            var field = typeof(Post).GetField("<PostID>k__BackingField", 
//                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//            field?.SetValue(this, id);
//        }
//    }
//}