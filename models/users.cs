using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SocialManager
{
  public class User
  {
    // --- Properties ---
    public Guid UserID { get; set; }  // Changed from private set to public set
    public string UserName { get; set; }
    public string Password { get; set; } // Attention: không nên lưu password dạng plain text trong thực tế
    public string FullName { get; set; }
    public string Bio { get; set; }
    public string AvatarUrl { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int Gender { get; set; } // 0: Nam, 1: Nữ
    public DateTime DOB { get; set; }
    public string Address { get; set; }
    public int Role { get; set; } // 0: User, 1: Admin, 2: Moderator
    public DateTime CreatedAt { get; set; }
    public int StatusId { get; set; }

    // --- Constructors ---

    // Constructor mặc định
    public User()
    {
      UserName = Password = FullName = Bio = AvatarUrl = Email = Phone = Address = "";
      Role = 0;
    }

    // Constructor để tạo đối tượng từ một dòng CSV
    public User(string csvLine)
    {
      // Khởi tạo mặc định để tránh nullable warnings
      UserName = Password = FullName = Bio = AvatarUrl = Email = Phone = Address = "";
      Role = 0;

      string[] values = csvLine.Split(',');
      // CSV Format: UserID,UserName,Password,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId
      // Kiểm tra để tránh lỗi nếu dòng CSV không đủ cột
      if (values.Length >= 13)
      {
        Guid.TryParse(values[0], out Guid userId);
        this.UserID = userId;

        this.UserName = values[1];
        this.Password = values[2];
        this.FullName = values[3];
        this.Bio = values[4];
        this.AvatarUrl = values[5];
        this.Email = values[6];
        this.Phone = values[7];

        int.TryParse(values[8], out int gender);
        this.Gender = gender;

        DateTime.TryParse(values[9], out DateTime dob);
        this.DOB = dob;

        this.Address = values[10];

        DateTime.TryParse(values[11], out DateTime createdAt);
        this.CreatedAt = createdAt;

        int.TryParse(values[12], out int statusId);
        this.StatusId = statusId;
      }
    }
    public User(Guid userID, string userName, string password, string fullName, string bio, string avatarUrl, string email, string phone, int gender, DateTime dob, string address, DateTime createdAt, int statusId)
    {
      UserID = userID;
      UserName = userName;
      Password = password;
      FullName = fullName;
      Bio = bio;
      AvatarUrl = avatarUrl;
      Email = email;
      Phone = phone;
      Gender = gender;
      DOB = dob;
      Role = 0;
      Address = address;
      CreatedAt = createdAt;
      StatusId = statusId;
    }


    // --- Methods ---

    // Phương thức tĩnh để đọc toàn bộ file và trả về một List<User>
    // Tĩnh vì nó không phụ thuộc vào một đối tượng User cụ thể nào
    public static List<User> GetList(string filePath)
    {
      List<User> userList = new List<User>();

      // Kiểm tra file có tồn tại không trước khi đọc
      if (!File.Exists(filePath))
      {
        // Có thể throw exception hoặc trả về danh sách rỗng
        Console.WriteLine($"Error: File not found at {filePath}");
        return userList;
      }

      try
      {
        // Dùng File.ReadLines để hiệu quả hơn với file lớn
        // Skip(1) để bỏ qua dòng tiêu đề
        var lines = File.ReadLines(filePath).Skip(1);

        foreach (string line in lines)
        {
          if (!string.IsNullOrWhiteSpace(line))
          {
            User user = new User(line);
            // Chỉ thêm vào danh sách nếu UserID hợp lệ (được parse thành công)
            if (user.UserID != Guid.Empty)
            {
              userList.Add(user);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred while reading the user file: {ex.Message}");
      }

      return userList;
    }

    // --- Instance Methods for CRUD Operations ---

    // Chuyển đổi User object thành dòng CSV
    public string ToCsvLine()
    {
      // Format: UserID,UserName,Password,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId
      return $"{UserID},{UserName},{Password},{FullName}," +
             $"{Bio},{AvatarUrl},{Email},{Phone}," +
             $"{Gender},{DOB:yyyy-MM-dd HH:mm:ss},{Address}," +
             $"{CreatedAt:yyyy-MM-dd HH:mm:ss},{StatusId}";
    }

    // Cập nhật thông tin user vào file CSV
    public bool Update(string filePath)
    {
      try
      {
        if (!File.Exists(filePath))
          return false;

        var lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8).ToList();
        bool updated = false;

        // Tìm và cập nhật user
        for (int i = 1; i < lines.Count; i++) // Skip header
        {
          var parts = lines[i].Split(',');
          if (parts.Length > 0 && Guid.TryParse(parts[0], out Guid userId))
          {
            if (userId == this.UserID)
            {
              // Thay thế dòng bằng dữ liệu mới
              lines[i] = this.ToCsvLine();
              updated = true;
              break;
            }
          }
        }

        if (updated)
        {
          // Ghi lại file
          File.WriteAllLines(filePath, lines, System.Text.Encoding.UTF8);
          return true;
        }

        return false;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error updating user: {ex.Message}");
        return false;
      }
    }

    // Cập nhật mật khẩu
    public bool UpdatePassword(string filePath, string newPassword)
    {
      try
      {
        this.Password = newPassword;
        return this.Update(filePath);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error updating password: {ex.Message}");
        return false;
      }
    }

    // Lưu user mới vào file CSV
    public bool Save(string filePath)
    {
      try
      {
        // Ensure directory exists
        string? directoryPath = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
        {
          Directory.CreateDirectory(directoryPath);
        }

        // Write to file
        if (!File.Exists(filePath))
        {
          // Create new file with header
          string header = "UserID,UserName,Password,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId";
          File.WriteAllText(filePath, header + Environment.NewLine + this.ToCsvLine(), System.Text.Encoding.UTF8);
        }
        else
        {
          // Append to existing file
          File.AppendAllText(filePath, Environment.NewLine + this.ToCsvLine(), System.Text.Encoding.UTF8);
        }

        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error saving user: {ex.Message}");
        return false;
      }
    }

    // Xóa user khỏi file CSV
    public bool Delete(string filePath)
    {
      try
      {
        if (!File.Exists(filePath))
          return false;

        var lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8).ToList();
        bool deleted = false;

        // Tìm và xóa user
        for (int i = 1; i < lines.Count; i++) // Skip header
        {
          var parts = lines[i].Split(',');
          if (parts.Length > 0 && Guid.TryParse(parts[0], out Guid userId))
          {
            if (userId == this.UserID)
            {
              lines.RemoveAt(i);
              deleted = true;
              break;
            }
          }
        }

        if (deleted)
        {
          // Ghi lại file
          File.WriteAllLines(filePath, lines, System.Text.Encoding.UTF8);
          return true;
        }

        return false;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error deleting user: {ex.Message}");
        return false;
      }
    }
  }
}
