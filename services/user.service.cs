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

    public List<User> GetAllUsers()
    {
      try
      {
        return User.GetList(GlobalSetting.UsersFilePath);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error loading users: {ex.Message}");
        return new List<User>();
      }
    }

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
        return _users.Any(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) || u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error checking user existence: {ex.Message}");
        return false;
      }
    }

    public bool CreateUser(string username, string password, string fullname, string email, string phone, int gender, DateTime dob, string address, string bio, string avatarUrl)
    {
      try
      {
        var newUser = new User
        {
          UserID = Guid.NewGuid(),
          UserName = username,
          Password = password,
          FullName = fullname,
          Email = email,
          Phone = phone,
          Gender = gender,
          DOB = dob,
          Address = address,
          CreatedAt = DateTime.Now,
          StatusId = 1,
          Bio = bio,
          AvatarUrl = avatarUrl,
          Role = 0
        };
        return newUser.Save(GlobalSetting.UsersFilePath);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error creating user: {ex.Message}\nStack trace: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }

    public bool AuthenticateUser(string username, string password)
    {
      try
      {
        if (!File.Exists(GlobalSetting.UsersFilePath))
        {
          string directoryPath = Path.GetDirectoryName(GlobalSetting.UsersFilePath);
          if (!string.IsNullOrEmpty(directoryPath))
          {
            Directory.CreateDirectory(directoryPath);
          }
          var adminUser = new User
          {
            UserID = Guid.NewGuid(),
            UserName = "admin",
            Password = "admin123",
            Role = 1,
            FullName = "Administrator",
            Bio = "System Administrator",
            AvatarUrl = "",
            Email = "admin@socialmedia.com",
            Phone = "",
            Gender = 0,
            DOB = new DateTime(1990, 1, 1),
            Address = "System",
            CreatedAt = DateTime.Now,
            StatusId = 1
          };
          adminUser.Save(GlobalSetting.UsersFilePath);
        }
        var user = _users.FirstOrDefault(u => (u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) || u.Email.Equals(username, StringComparison.OrdinalIgnoreCase)) && u.Password == password && u.StatusId == 1);
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
        return _users.FirstOrDefault(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) || u.Email.Equals(username, StringComparison.OrdinalIgnoreCase));
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

    public bool UpdateUser(User user)
    {
      try
      {
        return user.Update(GlobalSetting.UsersFilePath);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error updating user: {ex.Message}");
        return false;
      }
    }

    public bool UpdatePassword(Guid userId, string newPassword)
    {
      try
      {
        var user = GetUserById(userId);
        if (user == null) return false;
        return user.UpdatePassword(GlobalSetting.UsersFilePath, newPassword);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error updating password: {ex.Message}");
        return false;
      }
    }

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

    private bool SaveAllUsers(List<User> users)
    {
      try
      {
        var csv = new StringBuilder();
        csv.AppendLine("UserID,UserName,Password,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId");
        foreach (var user in users)
        {
          csv.AppendLine(user.ToCsvLine());
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
