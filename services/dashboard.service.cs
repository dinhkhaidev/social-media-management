using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SocialManager.services
{
    public class DashboardService
    {
        private readonly PostService postService;
        private readonly UserService userService;

        public DashboardService()
        {
            postService = new PostService();
            userService = new UserService();
        }

        public DashboardStatistics GetDashboardStatistics()
        {
            var statistics = new DashboardStatistics();
            
            try
            {
                // Get all posts
                var allPosts = postService.GetAllPosts();
                
                // Total posts
                statistics.TotalPosts = allPosts.Count;
                
                // Total interactions (likes + comments)
                statistics.TotalInteractions = allPosts.Sum(p => p.LikesCount + p.CommentsCount);
                
                // Calculate growth rate (posts from last 30 days vs previous 30 days)
                var today = DateTime.Now.Date;
                var lastMonth = today.AddDays(-30);
                var previousMonth = lastMonth.AddDays(-30);
                
                var recentPosts = allPosts.Count(p => p.CreatedAt.Date >= lastMonth);
                var previousPosts = allPosts.Count(p => p.CreatedAt.Date >= previousMonth && p.CreatedAt.Date < lastMonth);
                
                if (previousPosts > 0)
                {
                    statistics.GrowthPercentage = ((double)(recentPosts - previousPosts) / previousPosts) * 100;
                }
                else if (recentPosts > 0)
                {
                    statistics.GrowthPercentage = 100; // 100% growth if no previous posts
                }
                else
                {
                    statistics.GrowthPercentage = 0;
                }
                
                // Get user statistics
                var userStats = userService.GetUserStatistics();
                statistics.TotalUsers = userStats.TotalUsers;
                statistics.ActiveUsers = userStats.ActiveUsers;
                
                // Posts today
                statistics.PostsToday = allPosts.Count(p => p.CreatedAt.Date == today);
                
                // Average interactions per post
                if (statistics.TotalPosts > 0)
                {
                    statistics.AverageInteractionsPerPost = (double)statistics.TotalInteractions / statistics.TotalPosts;
                }
                
                // Most active day
                var postsGroupedByDay = allPosts
                    .Where(p => p.CreatedAt >= today.AddDays(-30))
                    .GroupBy(p => p.CreatedAt.Date)
                    .OrderByDescending(g => g.Count())
                    .FirstOrDefault();
                
                statistics.MostActiveDay = postsGroupedByDay?.Key.ToString("dd/MM/yyyy") ?? "N/A";
                statistics.MostActiveDayPosts = postsGroupedByDay?.Count() ?? 0;
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calculating dashboard statistics: {ex.Message}");
                // Return default statistics if error occurs
                statistics = new DashboardStatistics();
            }
            
            return statistics;
        }

        public string FormatNumber(int number)
        {
            if (number >= 1000000)
                return $"{number / 1000000.0:F1}M";
            else if (number >= 1000)
                return $"{number / 1000.0:F1}K";
            else
                return number.ToString();
        }

        public string FormatGrowth(double percentage)
        {
            string sign = percentage >= 0 ? "+" : "";
            return $"{sign}{percentage:F1}%";
        }
    }

    public class DashboardStatistics
    {
        public int TotalPosts { get; set; }
        public int TotalInteractions { get; set; }
        public double GrowthPercentage { get; set; }
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int PostsToday { get; set; }
        public double AverageInteractionsPerPost { get; set; }
        public string MostActiveDay { get; set; } = "N/A";
        public int MostActiveDayPosts { get; set; }
    }
}