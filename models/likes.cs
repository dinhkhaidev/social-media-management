using System;

namespace SocialManager
{
    public class Like
    {
        public int LikeID { get; set; }
        public int PostID { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
