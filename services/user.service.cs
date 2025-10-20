using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialManager.services
{
    public class UserService
    {
        private readonly List<User> _users;
        public UserService()
        {
            _users = User.GetList(GlobalSetting.UsersFilePath);
        }

        // Get all users for admin dashboard
        public List<User> GetAllUsers()
        {
            try
            {
                // Reload from file to get latest data
                return User.GetList(GlobalSetting.UsersFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}");
                return new List<User>();
            }
        }

        // Get user statistics for dashboard
        public UserStatistics GetUserStatistics()
        {
            try
            {
                var allUsers = GetAllUsers();
                return new UserStatistics
                {
                    TotalUsers = allUsers.Count,
                    ActiveUsers = allUsers.Count(u => u.StatusId == 1),
                    InactiveUsers = allUsers.Count(u => u.StatusId == 0),
                    BannedUsers = allUsers.Count(u => u.StatusId == -1),
                    AdminUsers = allUsers.Count(u => u.Role == 1),
                    RegularUsers = allUsers.Count(u => u.Role == 0),
                    NewUsersToday = allUsers.Count(u => u.CreatedAt.Date == DateTime.Today),
                    NewUsersThisWeek = allUsers.Count(u => u.CreatedAt >= DateTime.Today.AddDays(-7)),
                    NewUsersThisMonth = allUsers.Count(u => u.CreatedAt >= DateTime.Today.AddDays(-30))
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating user statistics: {ex.Message}");
                return new UserStatistics();
            }
        }

        public bool UserExists(string username, string email)
        {
            try
            {
                return _users.Any(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) ||
                                     u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking user existence: {ex.Message}");
                return false;
            }
        }

        public bool CreateUser(
            string username, string password, 
            string fullname, string email,
            string phone,int gender,DateTime dob, 
            string address, string bio, string avatarUrl
            )
        {
            try
            {
                // Ensure directory exists
                string directoryPath = Path.GetDirectoryName(GlobalSetting.UsersFilePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Create new user with proper UserID
                var newUser = new User
                {
                    UserID = Guid.NewGuid(),
                    UserName = username,
                    Password = password, // In production, hash this
                    FullName = fullname,
                    Email = email,
                    Phone = phone,
                    Gender = gender,
                    DOB = dob,
                    Address = address,
                    CreatedAt = DateTime.Now,
                    StatusId = 1, // Active
                    Bio = bio,
                    AvatarUrl = avatarUrl,
                    Role = 0 // Default role: User
                };

                // Create CSV line with proper formatting
                string csvLine = $"{newUser.UserID},{newUser.UserName},{newUser.Password},{newUser.Role},{newUser.FullName}," +
                               $"{newUser.Bio},{newUser.AvatarUrl},{newUser.Email},{newUser.Phone}," +
                               $"{newUser.Gender},{newUser.DOB:yyyy-MM-dd},{newUser.Address}," +
                               $"{newUser.CreatedAt:yyyy-MM-dd HH:mm:ss},{newUser.StatusId}";

                // Write to file
                if (!File.Exists(GlobalSetting.UsersFilePath))
                {
                    // Create new file with header
                    string header = "UserID,UserName,Password,Role,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId";
                    File.WriteAllText(GlobalSetting.UsersFilePath, header + Environment.NewLine + csvLine, Encoding.UTF8);
                }
                else
                {
                    // Append to existing file
                    File.AppendAllText(GlobalSetting.UsersFilePath, Environment.NewLine + csvLine, Encoding.UTF8);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating user: {ex.Message}\nStack trace: {ex.StackTrace}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool AuthenticateUser(string username, string password)
        {
            try
            {
                if (!File.Exists(GlobalSetting.UsersFilePath))
                {
                    // Create data directory if it doesn't exist
                    Directory.CreateDirectory(Path.GetDirectoryName(GlobalSetting.UsersFilePath));

                    // Create CSV header and default admin user
                    string csvContent = "UserID,UserName,Password,Role,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId\n" +
                                        $"{Guid.NewGuid()},admin,admin123,1,Administrator,System Administrator,,admin@socialmedia.com,,0,1990-01-01,System,{DateTime.Now:yyyy-MM-dd HH:mm:ss},1";

                    File.WriteAllText(GlobalSetting.UsersFilePath, csvContent);
                }
                var user = _users.FirstOrDefault(u =>
                    (u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) ||
                     u.Email.Equals(username, StringComparison.OrdinalIgnoreCase)) &&
                    u.Password == password &&
                    u.StatusId == 1); // Active user

                return user != null;
            }
            catch
            {
                return false;
            }
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                return _users.FirstOrDefault(u =>
                    u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) ||
                    u.Email.Equals(username, StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                return null;
            }
        }

        public User GetUserById(Guid userId)
        {
            try
            {
                return _users.FirstOrDefault(u => u.UserID == userId);
            }
            catch
            {
                return null;
            }
        }

        // Update user status (for admin actions)
        public bool UpdateUserStatus(Guid userId, int newStatus)
        {
            try
            {
                var allUsers = GetAllUsers();
                var user = allUsers.FirstOrDefault(u => u.UserID == userId);
                if (user == null) return false;

                user.StatusId = newStatus;
                return SaveAllUsers(allUsers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user status: {ex.Message}");
                return false;
            }
        }

        // Save all users back to file
        private bool SaveAllUsers(List<User> users)
        {
            try
            {
                var csv = new StringBuilder();
                csv.AppendLine("UserID,UserName,Password,Role,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId");

                foreach (var user in users)
                {
                    var csvLine = $"{user.UserID},{user.UserName},{user.Password},{user.Role},{user.FullName}," +
                                 $"{user.Bio},{user.AvatarUrl},{user.Email},{user.Phone}," +
                                 $"{user.Gender},{user.DOB:yyyy-MM-dd},{user.Address}," +
                                 $"{user.CreatedAt:yyyy-MM-dd HH:mm:ss},{user.StatusId}";
                    csv.AppendLine(csvLine);
                }

                File.WriteAllText(GlobalSetting.UsersFilePath, csv.ToString(), Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving users: {ex.Message}");
                return false;
            }
        }
    }

    // Helper class for user statistics
    public class UserStatistics
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public int BannedUsers { get; set; }
        public int AdminUsers { get; set; }
        public int RegularUsers { get; set; }
        public int NewUsersToday { get; set; }
        public int NewUsersThisWeek { get; set; }
        public int NewUsersThisMonth { get; set; }
        
        public double ActivePercentage => TotalUsers > 0 ? (ActiveUsers * 100.0 / TotalUsers) : 0;
        public double GrowthWeekly => TotalUsers > 0 ? (NewUsersThisWeek * 100.0 / TotalUsers) : 0;
    }
}
