using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialManager.services
{
  public class UserService
  {
    private const string HEADER = "UserID,UserName,Password,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId,Role,ReportCount";
    private List<User> _users;

    public UserService()
    {
      MigrateUserCsvIfNeeded();
      _users = LoadFromDisk();
    }

    // --- Private Helper Methods ---

    /// <summary>
    /// Migration: Đảm bảo CSV có cột Role (14 cột) và ReportCount (15 cột)
    /// </summary>
    private void MigrateUserCsvIfNeeded()
    {
      try
      {
        if (!File.Exists(GlobalSetting.UsersFilePath))
          return;

        var lines = File.ReadAllLines(GlobalSetting.UsersFilePath, Encoding.UTF8).ToList();
        if (lines.Count == 0)
          return;

        string currentHeader = lines[0];
        bool needsMigration = false;

        // Check nếu header thiếu Role hoặc ReportCount
        var headerParts = currentHeader.Split(',');

        if (headerParts.Length < 14 || !currentHeader.Contains("Role"))
        {
          lines[0] = HEADER;
          needsMigration = true;

          // Add Role=0 and ReportCount=0 to existing records
          for (int i = 1; i < lines.Count; i++)
          {
            if (!string.IsNullOrWhiteSpace(lines[i]))
            {
              var parts = lines[i].Split(',');
              if (parts.Length == 13) // Old format without Role and ReportCount
              {
                lines[i] += ",0,0"; // Add default Role=0, ReportCount=0
              }
              else if (parts.Length == 14) // Has Role but no ReportCount
              {
                lines[i] += ",0"; // Add default ReportCount=0
              }
            }
          }
        }
        else if (headerParts.Length < 15 || !currentHeader.Contains("ReportCount"))
        {
          lines[0] = HEADER;
          needsMigration = true;

          // Add ReportCount=0 to existing records
          for (int i = 1; i < lines.Count; i++)
          {
            if (!string.IsNullOrWhiteSpace(lines[i]))
            {
              var parts = lines[i].Split(',');
              if (parts.Length == 14) // Has Role but no ReportCount
              {
                lines[i] += ",0"; // Add default ReportCount=0
              }
            }
          }
        }

        if (needsMigration)
        {
          File.WriteAllLines(GlobalSetting.UsersFilePath, lines, Encoding.UTF8);
          Console.WriteLine("User CSV migrated to include Role and ReportCount columns");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi migration CSV: {ex.Message}");
      }
    }

    // --- Private Helper Methods ---

    /// <summary>
    /// Đọc toàn bộ User từ file CSV
    /// </summary>
    private List<User> LoadFromDisk()
    {
      var userList = new List<User>();

      // Kiểm tra file có tồn tại không
      if (!File.Exists(GlobalSetting.UsersFilePath))
      {
        Console.WriteLine($"Info: File not found at {GlobalSetting.UsersFilePath}, returning empty list");
        return userList;
      }

      try
      {
        var lines = File.ReadLines(GlobalSetting.UsersFilePath, Encoding.UTF8).ToList();

        if (lines.Count == 0)
        {
          return userList;
        }

        // Skip header line (index 0)
        for (int i = 1; i < lines.Count; i++)
        {
          string line = lines[i];
          if (!string.IsNullOrWhiteSpace(line))
          {
            User user = User.FromColumns(line.Split(','));
            // Chỉ thêm vào danh sách nếu UserID hợp lệ
            if (user.UserID != Guid.Empty)
            {
              userList.Add(user);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi đọc file user: {ex.Message}");
      }

      return userList;
    }

    /// <summary>
    /// Lưu toàn bộ User vào file CSV
    /// </summary>
    private bool SaveAllUsers(List<User> users)
    {
      try
      {
        // Đảm bảo thư mục tồn tại
        string? directoryPath = Path.GetDirectoryName(GlobalSetting.UsersFilePath);
        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
        {
          Directory.CreateDirectory(directoryPath);
        }

        var csv = new StringBuilder();
        csv.AppendLine(HEADER);
        foreach (var user in users)
        {
          csv.AppendLine(user.ToCsvLine());
        }
        File.WriteAllText(GlobalSetting.UsersFilePath, csv.ToString(), Encoding.UTF8);
        return true;
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi lưu danh sách người dùng: {ex.Message}");
        return false;
      }
    }

    // --- Public API Methods ---

    public void RefreshUser()
    {
      _users = LoadFromDisk();
    }

    public List<User> GetAllUsers()
    {
      try
      {
        return LoadFromDisk();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi tải danh sách người dùng: {ex.Message}");
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
        MessageBox.Show($"Lỗi khi tính thống kê người dùng: {ex.Message}");
        return new UserStatistics();
      }
    }

    public bool UserExists(string username, string email)
    {
      try
      {
        _users = LoadFromDisk();
        return _users.Any(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) || u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi kiểm tra người dùng tồn tại: {ex.Message}");
        return false;
      }
    }

    public bool CreateUser(string username, string password, string fullname, string email, string phone, int gender, DateTime dob, string address, string bio, string avatarUrl)
    {
      try
      {
        var newUser = new User(
          Guid.NewGuid(),
          username,
          password,
          fullname,
          bio,
          avatarUrl,
          email,
          phone,
          gender,
          dob,
          address,
          DateTime.Now,
          1, // StatusId = Active
          0  // Role = Regular User
        );

        // Refresh và thêm user mới
        _users = LoadFromDisk();
        _users.Add(newUser);
        return SaveAllUsers(_users);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi tạo người dùng: {ex.Message}\nChi tiết lỗi: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }

    public bool AuthenticateUser(string username, string password)
    {
      try
      {
        _users = LoadFromDisk();

        if (!File.Exists(GlobalSetting.UsersFilePath))
        {
          // Tạo admin user mặc định nếu file không tồn tại
          string? directoryPath = Path.GetDirectoryName(GlobalSetting.UsersFilePath);
          if (!string.IsNullOrEmpty(directoryPath))
          {
            Directory.CreateDirectory(directoryPath);
          }

          var adminUser = new User(
            Guid.NewGuid(),
            "admin",
            "admin123",
            "Administrator",
            "System Administrator",
            "",
            "admin@socialmedia.com",
            "",
            0,
            new DateTime(1990, 1, 1),
            "System",
            DateTime.Now,
            1, // StatusId = Active
            1  // Role = Admin
          );

          _users.Add(adminUser);
          SaveAllUsers(_users);

          // Refresh users list after creating admin
          _users = LoadFromDisk();
        }

        var user = _users.FirstOrDefault(u =>
          (u.UserName.Trim().Equals(username.Trim(), StringComparison.OrdinalIgnoreCase) ||
           u.Email.Trim().Equals(username.Trim(), StringComparison.OrdinalIgnoreCase)) &&
          u.Password.Trim() == password.Trim() &&  // ← THÊM .Trim()
          u.StatusId == 1);

        return user != null;
      }
      catch
      {
        return false;
      }
    }

    public User? GetUserByUsername(string username)
    {
      try
      {
        // Refresh users list to get latest data
        _users = LoadFromDisk();
        return _users.FirstOrDefault(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) || u.Email.Equals(username, StringComparison.OrdinalIgnoreCase));
      }
      catch
      {
        return null;
      }
    }

    public User? GetUserById(Guid userId)
    {
      try
      {
        // Refresh the users list to get latest data
        _users = LoadFromDisk();
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
        _users = LoadFromDisk();
        var existingUser = _users.FirstOrDefault(u => u.UserID == user.UserID);
        if (existingUser == null)
          return false;

        // Replace user in list
        int index = _users.IndexOf(existingUser);
        _users[index] = user;

        return SaveAllUsers(_users);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi cập nhật người dùng: {ex.Message}");
        return false;
      }
    }

    public bool UpdatePassword(Guid userId, string newPassword)
    {
      try
      {
        var user = GetUserById(userId);
        if (user == null) return false;

        user.Password = newPassword;
        return UpdateUser(user);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi cập nhật mật khẩu: {ex.Message}");
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
        MessageBox.Show($"Lỗi khi cập nhật trạng thái người dùng: {ex.Message}");
        return false;
      }
    }

    public bool DeleteUser(Guid userId)
    {
      try
      {
        _users = LoadFromDisk();
        var user = _users.FirstOrDefault(u => u.UserID == userId);
        if (user == null)
          return false;

        _users.Remove(user);
        return SaveAllUsers(_users);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi xóa người dùng: {ex.Message}");
        return false;
      }
    }

    /// <summary>
    /// Lấy số lần bị tố cáo từ ReportService
    /// </summary>
    public int GetReportCount(Guid userId)
    {
      try
      {
        var reportService = new ReportService();
        return reportService.GetReportCountForUser(userId);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi lấy report count: {ex.Message}");
        return 0;
      }
    }

    /// <summary>
    /// Cập nhật ReportCount cho user từ ReportService (để sync)
    /// </summary>
    public bool UpdateReportCountFromService(Guid userId)
    {
      try
      {
        _users = LoadFromDisk();
        var user = _users.FirstOrDefault(u => u.UserID == userId);
        if (user == null)
          return false;

        user.ReportCount = GetReportCount(userId);

        // Tự động cập nhật StatusId dựa trên ReportCount
        if (user.ReportCount >= 100)
        {
          user.StatusId = -1; // Tạm ngừng tài khoản
        }
        else if (user.StatusId == -1 && user.ReportCount < 100)
        {
          user.StatusId = 1; // Khôi phục nếu dưới ngưỡng
        }

        return SaveAllUsers(_users);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi cập nhật report count: {ex.Message}");
        return false;
      }
    }

    /// <summary>
    /// [DEPRECATED] Tăng số lần bị tố cáo của user - Dùng ReportService thay thế
    /// </summary>
    [Obsolete("Use ReportService.CreateReport() instead")]
    public bool IncrementReportCount(Guid userId)
    {
      try
      {
        _users = LoadFromDisk();
        var user = _users.FirstOrDefault(u => u.UserID == userId);
        if (user == null)
          return false;

        user.ReportCount++;

        // Tự động cập nhật StatusId dựa trên ReportCount
        if (user.ReportCount >= 100)
        {
          user.StatusId = -1; // Tạm ngừng tài khoản
        }

        return SaveAllUsers(_users);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi khi tăng report count: {ex.Message}");
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
