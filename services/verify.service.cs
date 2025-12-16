using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SocialManager.services
{
  public class EmailVerification
  {
    public Guid UserID { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public string VerificationCode { get; set; } = string.Empty;
    public DateTime? VerificationSentAt { get; set; }
    public DateTime? VerifiedAt { get; set; }
  }

  public class VerifyService
  {
    private readonly string verifyFilePath;
    private List<EmailVerification> verifications;

    public VerifyService()
    {
      // Đường dẫn tới file CSV
      string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      verifyFilePath = Path.Combine(baseDirectory, "datas", "checkVerifyEmail.csv");

      // Tạo thư mục nếu chưa tồn tại
      string? directory = Path.GetDirectoryName(verifyFilePath);
      if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
      {
        Directory.CreateDirectory(directory);
      }

      // Tạo file nếu chưa tồn tại
      if (!File.Exists(verifyFilePath))
      {
        File.WriteAllText(verifyFilePath, "UserID,Email,IsVerified,VerificationCode,VerificationSentAt,VerifiedAt\n");
      }

      verifications = new List<EmailVerification>();
      LoadVerifications();
    }

    private void LoadVerifications()
    {
      try
      {
        verifications.Clear();
        var lines = File.ReadAllLines(verifyFilePath);

        for (int i = 1; i < lines.Length; i++) // Bỏ qua header
        {
          var parts = lines[i].Split(',');
          if (parts.Length >= 6)
          {
            var verification = new EmailVerification
            {
              UserID = Guid.Parse(parts[0]),
              Email = parts[1],
              IsVerified = bool.Parse(parts[2]),
              VerificationCode = parts[3],
              VerificationSentAt = string.IsNullOrEmpty(parts[4]) ? null : DateTime.Parse(parts[4]),
              VerifiedAt = string.IsNullOrEmpty(parts[5]) ? null : DateTime.Parse(parts[5])
            };
            verifications.Add(verification);
          }
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Lỗi khi load verifications: {ex.Message}");
      }
    }

    private void SaveVerifications()
    {
      try
      {
        var sb = new StringBuilder();
        sb.AppendLine("UserID,Email,IsVerified,VerificationCode,VerificationSentAt,VerifiedAt");

        foreach (var v in verifications)
        {
          sb.AppendLine($"{v.UserID},{v.Email},{v.IsVerified},{v.VerificationCode}," +
                        $"{v.VerificationSentAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""}," +
                        $"{v.VerifiedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""}");
        }

        File.WriteAllText(verifyFilePath, sb.ToString());
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Lỗi khi save verifications: {ex.Message}");
      }
    }

    // Kiểm tra email đã verify chưa
    public bool IsEmailVerified(Guid userId)
    {
      var verification = verifications.FirstOrDefault(v => v.UserID == userId);
      return verification != null && verification.IsVerified;
    }

    // Tạo mã verification mới
    public string GenerateVerificationCode(Guid userId, string email)
    {
      // Tạo mã verification 6 số
      Random random = new Random();
      string code = random.Next(100000, 999999).ToString();

      // Kiểm tra xem user đã có verification chưa
      var existing = verifications.FirstOrDefault(v => v.UserID == userId);

      if (existing != null)
      {
        // Cập nhật mã mới
        existing.VerificationCode = code;
        existing.VerificationSentAt = DateTime.Now;
        existing.Email = email;
      }
      else
      {
        // Tạo mới
        verifications.Add(new EmailVerification
        {
          UserID = userId,
          Email = email,
          IsVerified = false,
          VerificationCode = code,
          VerificationSentAt = DateTime.Now
        });
      }

      SaveVerifications();
      return code;
    }

    // Xác minh mã
    public bool VerifyCode(Guid userId, string code)
    {
      var verification = verifications.FirstOrDefault(v => v.UserID == userId);

      if (verification == null)
        return false;

      // Kiểm tra mã có đúng không
      if (verification.VerificationCode != code)
        return false;

      // Kiểm tra mã còn hiệu lực không (trong vòng 10 phút)
      if (verification.VerificationSentAt.HasValue)
      {
        var timeDiff = DateTime.Now - verification.VerificationSentAt.Value;
        if (timeDiff.TotalMinutes > 10)
          return false;
      }

      // Xác minh thành công
      verification.IsVerified = true;
      verification.VerifiedAt = DateTime.Now;
      SaveVerifications();

      return true;
    }

    // Lấy thông tin verification
    public EmailVerification? GetVerification(Guid userId)
    {
      return verifications.FirstOrDefault(v => v.UserID == userId);
    }

    // Reset verification (dùng khi đổi email)
    public void ResetVerification(Guid userId)
    {
      var verification = verifications.FirstOrDefault(v => v.UserID == userId);
      if (verification != null)
      {
        verification.IsVerified = false;
        verification.VerificationCode = string.Empty;
        verification.VerificationSentAt = null;
        verification.VerifiedAt = null;
        SaveVerifications();
      }
    }
  }
}
