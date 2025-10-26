using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialManager
{
  static class GlobalSetting
  {
    // Updated to match the actual file structure
    public static string UsersFilePath { get; } = @"datas\users.csv";

    // Additional file paths for the social media platform
    public static string PostsFilePath { get; } = @"data\Post.csv";
    public static string CommentsFilePath { get; } = @"datas\Comment.csv";
    public static string LikesFilePath { get; } = @"datas\Likes.csv";

    // Platform settings
    public static string PlatformName { get; } = "Social Media Manager";
    public static string PlatformVersion { get; } = "1.0.0";

    // Admin settings
    public static int DefaultAdminRole { get; } = 1;
    public static int DefaultUserRole { get; } = 0;
    public static int ActiveStatus { get; } = 1;
    public static int InactiveStatus { get; } = 0;
    public static int BannedStatus { get; } = -1;
  }
}
