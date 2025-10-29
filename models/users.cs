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
            
            // CRITICAL FIX: Handle both old format (13 fields) and new format (14 fields with Role)
            // Current CSV Format: UserID,UserName,Password,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId
            // New CSV Format: UserID,UserName,Password,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId,Role
            
            if (values.Length >= 13)
            {
                try
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

                    // Handle Role field (14th field) if exists, otherwise default to 0
                    if (values.Length >= 14)
                    {
                        int.TryParse(values[13], out int role);
                        this.Role = role;
                    }
                    else
                    {
                        // For existing users without Role field, set default role
                        this.Role = 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing user CSV line: {ex.Message}");
                    // Set default values on parse error
                    this.UserID = Guid.Empty;
                    this.Role = 0;
                }
            }
        }

        public User(Guid userID, string userName, string password, string fullName, string bio, string avatarUrl, string email, string phone, int gender, DateTime dob, string address, DateTime createdAt, int statusId, int role = 0)
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
        }

        // --- Methods ---

        // Phương thức tĩnh để đọc toàn bộ file và trả về một List<User>
        public static List<User> GetList(string filePath)
        {
            List<User> userList = new List<User>();

            // Kiểm tra file có tồn tại không trước khi đọc
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Info: File not found at {filePath}, returning empty list");
                return userList;
            }

            try
            {
                // Dùng File.ReadLines để hiệu quả hơn với file lớn
                var lines = File.ReadLines(filePath).ToList();
                
                if (lines.Count == 0)
                {
                    return userList;
                }

                // Skip header line
                for (int i = 1; i < lines.Count; i++)
                {
                    string line = lines[i];
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
            // UPDATED Format to include Role: UserID,UserName,Password,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId,Role
            return $"{UserID},{UserName},{Password},{FullName}," +
                   $"{Bio},{AvatarUrl},{Email},{Phone}," +
                   $"{Gender},{DOB:yyyy-MM-dd HH:mm:ss},{Address}," +
                   $"{CreatedAt:yyyy-MM-dd HH:mm:ss},{StatusId},{Role}";
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
                    // Create new file with updated header including Role
                    string header = "UserID,UserName,Password,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId,Role";
                    File.WriteAllText(filePath, header + Environment.NewLine + this.ToCsvLine(), System.Text.Encoding.UTF8);
                }
                else
                {
                    // Check if header needs updating
                    var existingLines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8).ToList();
                    if (existingLines.Count > 0)
                    {
                        string currentHeader = existingLines[0];
                        string expectedHeader = "UserID,UserName,Password,FullName,Bio,AvatarUrl,Email,Phone,Gender,DOB,Address,CreatedAt,StatusId,Role";
                        
                        if (currentHeader != expectedHeader)
                        {
                            // Update header and migrate existing data
                            existingLines[0] = expectedHeader;
                            
                            // Add Role=0 to existing records if they don't have it
                            for (int i = 1; i < existingLines.Count; i++)
                            {
                                if (!string.IsNullOrWhiteSpace(existingLines[i]))
                                {
                                    var parts = existingLines[i].Split(',');
                                    if (parts.Length == 13) // Old format without Role
                                    {
                                        existingLines[i] += ",0"; // Add default Role=0
                                    }
                                }
                            }
                            
                            // Write updated content
                            File.WriteAllLines(filePath, existingLines, System.Text.Encoding.UTF8);
                        }
                    }
                    
                    // Append new user
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
