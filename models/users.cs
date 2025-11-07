using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SocialManager
{
  public class User
  {
    // --- Properties ---
    public Guid UserID { get; set; }
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
    public int ReportCount { get; set; } // Số lần bị tố cáo

    // Computed property cho trạng thái tài khoản
    public string AccountStatus
    {
      get
      {
        if (ReportCount >= 100)
          return "Tạm ngừng";
        else if (ReportCount >= 50)
          return "Cảnh báo";
        else
          return "Bình thường";
      }
    }

    public bool CanPost => ReportCount < 100; // Chỉ cho đăng bài nếu < 100 lần tố cáo

    // --- Constructors ---

    // Constructor mặc định
    public User()
    {
      UserName = Password = FullName = Bio = AvatarUrl = Email = Phone = Address = "";
      Role = 0;
      ReportCount = 0;
    }

    // Factory method để tạo User từ CSV columns
    public static User FromColumns(string[] values)
    {
      if (values.Length >= 13)
      {
        try
        {
          Guid.TryParse(values[0], out Guid userId);
          int.TryParse(values[8], out int gender);
          DateTime.TryParse(values[9], out DateTime dob);
          DateTime.TryParse(values[11], out DateTime createdAt);
          int.TryParse(values[12], out int statusId);

          // Handle Role field (14th field) if exists, otherwise default to 0
          int role = 0;
          if (values.Length >= 14)
          {
            int.TryParse(values[13], out role);
          }

          // Handle ReportCount field (15th field) if exists, otherwise default to 0
          int reportCount = 0;
          if (values.Length >= 15)
          {
            int.TryParse(values[14], out reportCount);
          }

          return new User(
              userId,
              values[1].Trim(),      // UserName - THÊM Trim()
              values[2].Trim(),      // Password - THÊM Trim()
              values[3].Trim(),      // FullName
              values[4].Trim(),      // Bio
              values[5].Trim(),      // AvatarUrl
              values[6].Trim(),      // Email
              values[7].Trim(),      // Phone
              gender,
              dob,
              values[10].Trim(),     // Address
              createdAt,
              statusId,
              role,
              reportCount
          );
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Error parsing user CSV line: {ex.Message}");
          return new User(); // Return default user on parse error
        }
      }

      return new User(); // Return default user for invalid format
    }

    public User(Guid userID, string userName, string password, string fullName, string bio, string avatarUrl, string email, string phone, int gender, DateTime dob, string address, DateTime createdAt, int statusId, int role = 0, int reportCount = 0)
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
      Role = role;
      Address = address;
      CreatedAt = createdAt;
      StatusId = statusId;
      ReportCount = reportCount;
    }

    // --- Methods ---

    // Chuyển đổi User object thành dòng CSV
    public string ToCsvLine()
    {
      // Format: UserID,UserName,Password,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId,Role,ReportCount
      return $"{UserID},{UserName},{Password},{FullName}," +
             $"{Bio},{AvatarUrl},{Email},{Phone}," +
             $"{Gender},{DOB:yyyy-MM-dd HH:mm:ss},{Address}," +
             $"{CreatedAt:yyyy-MM-dd HH:mm:ss},{StatusId},{Role},{ReportCount}";
    }
  }
}
