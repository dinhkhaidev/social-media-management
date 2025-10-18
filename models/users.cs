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
        public DateTime CreatedAt { get; set; }
        public int StatusId { get; set; }

        // --- Constructors ---

        // Constructor mặc định
        public User()
        {
            UserName = Password = FullName = Bio = AvatarUrl = Email = Phone = Address = "";
        }

        // Constructor để tạo đối tượng từ một dòng CSV
        public User(string csvLine)
        {
            string[] values = csvLine.Split(',');

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
        public User(Guid userID, string userName, string password, string fullName, string bio, string avatarUrl, string email, string phone, int gender, DateTime dOB, string address, DateTime createdAt, int statusId)
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
            DOB = dOB;
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
    }
}