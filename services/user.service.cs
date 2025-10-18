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
        //public User Register()
        //{
        //    return;
        //}
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
                    AvatarUrl = avatarUrl
                };

                // Create CSV line with proper formatting
                string csvLine = $"{newUser.UserID},{newUser.UserName},{newUser.Password},{newUser.FullName}," +
                               $"{newUser.Bio},{newUser.AvatarUrl},{newUser.Email},{newUser.Phone}," +
                               $"{newUser.Gender},{newUser.DOB:yyyy-MM-dd},{newUser.Address}," +
                               $"{newUser.CreatedAt:yyyy-MM-dd HH:mm:ss},{newUser.StatusId}";

                // Write to file
                if (!File.Exists(GlobalSetting.UsersFilePath))
                {
                    // Create new file with header
                    string header = "UserID,UserName,Password,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId";
                    File.WriteAllText(GlobalSetting.UsersFilePath, header + Environment.NewLine + csvLine, Encoding.UTF8);
                }
                else
                {
                    // Append to existing file
                    File.AppendAllText(GlobalSetting.UsersFilePath, Environment.NewLine + csvLine, Encoding.UTF8);
                }

                // FIX: Thêm debug message để kiểm tra
                MessageBox.Show($"User created successfully! File path: {Path.GetFullPath(GlobalSetting.UsersFilePath)}");

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
                    string csvContent = "UserID,UserName,Password,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId\n" +
                                        $"{Guid.NewGuid()},admin,admin123,Administrator,System Administrator,,admin@socialmedia.com,,0,1990-01-01,System,{DateTime.Now:yyyy-MM-dd HH:mm:ss},1";

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
    }
}
