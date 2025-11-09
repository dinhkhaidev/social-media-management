using System;

namespace SocialManager.models
{
  public class Report
  {
    public int ReportID { get; set; }
    public string ReportType { get; set; } // "Post" hoặc "Comment"
    public int ContentID { get; set; } // PostID hoặc CommentID
    public DateTime ReportedAt { get; set; }
    public Guid ReporterUserID { get; set; } // Người tố cáo
    public Guid ReportedUserID { get; set; } // Người bị tố cáo
    
    // New fields for enhanced report management
    public string Reason { get; set; } // Lý do báo cáo
    public string Status { get; set; } // New / In Review / Resolved / Escalated / Rejected
    public Guid? ReviewerID { get; set; } // Admin xử lý
    public DateTime? ReviewedAt { get; set; }
    public string? AdminNote { get; set; } // Ghi chú của admin
    public string? Action { get; set; } // Hide Content / Ban User / None

    public Report()
    {
      ReportType = "";
      Reason = "";
      Status = "New";
      ReportedAt = DateTime.Now;
    }

    public Report(int reportID, string reportType, int contentID, DateTime reportedAt, 
                  Guid reporterUserID, Guid reportedUserID, string reason = "", 
                  string status = "New", Guid? reviewerID = null, DateTime? reviewedAt = null,
                  string? adminNote = null, string? action = null)
    {
      ReportID = reportID;
      ReportType = reportType;
      ContentID = contentID;
      ReportedAt = reportedAt;
      ReporterUserID = reporterUserID;
      ReportedUserID = reportedUserID;
      Reason = reason;
      Status = status;
      ReviewerID = reviewerID;
      ReviewedAt = reviewedAt;
      AdminNote = adminNote;
      Action = action;
    }

    // Factory method để tạo Report từ CSV columns
    public static Report? FromColumns(string[] values)
    {
      // Format: ReportID,ReportType,ContentID,ReportedAt,ReporterUserID,ReportedUserID,Reason,Status,ReviewerID,ReviewedAt,AdminNote,Action
      if (values.Length >= 6)
      {
        try
        {
          int.TryParse(values[0], out int reportID);
          string reportType = values[1];
          int.TryParse(values[2], out int contentID);
          DateTime.TryParse(values[3], out DateTime reportedAt);
          Guid.TryParse(values[4], out Guid reporterUserID);
          Guid.TryParse(values[5], out Guid reportedUserID);
          
          string reason = values.Length > 6 ? values[6] : "";
          string status = values.Length > 7 ? values[7] : "New";
          Guid? reviewerID = values.Length > 8 && Guid.TryParse(values[8], out Guid rid) ? rid : null;
          DateTime? reviewedAt = values.Length > 9 && DateTime.TryParse(values[9], out DateTime rat) ? rat : null;
          string? adminNote = values.Length > 10 && !string.IsNullOrEmpty(values[10]) ? values[10] : null;
          string? action = values.Length > 11 && !string.IsNullOrEmpty(values[11]) ? values[11] : null;

          return new Report(reportID, reportType, contentID, reportedAt, reporterUserID, reportedUserID, 
                           reason, status, reviewerID, reviewedAt, adminNote, action);
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Error parsing Report from CSV: {ex.Message}");
        }
      }
      return null;
    }

    // Chuyển đổi Report object thành dòng CSV
    public string ToCsvLine()
    {
      // Format: ReportID,ReportType,ContentID,ReportedAt,ReporterUserID,ReportedUserID,Reason,Status,ReviewerID,ReviewedAt,AdminNote,Action
      string reviewerIDStr = ReviewerID.HasValue ? ReviewerID.Value.ToString() : "";
      string reviewedAtStr = ReviewedAt.HasValue ? ReviewedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
      string adminNoteStr = AdminNote ?? "";
      string actionStr = Action ?? "";
      
      return $"{ReportID},{ReportType},{ContentID},{ReportedAt:yyyy-MM-dd HH:mm:ss},{ReporterUserID},{ReportedUserID},{Reason},{Status},{reviewerIDStr},{reviewedAtStr},{adminNoteStr},{actionStr}";
    }
  }
}
