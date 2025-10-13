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
    }
}