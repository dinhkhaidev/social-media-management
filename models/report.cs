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

    public Report()
    {
      ReportType = "";
      ReportedAt = DateTime.Now;
    }

    public Report(int reportID, string reportType, int contentID, DateTime reportedAt, Guid reporterUserID, Guid reportedUserID)
    {
      ReportID = reportID;
      ReportType = reportType;
      ContentID = contentID;
      ReportedAt = reportedAt;
      ReporterUserID = reporterUserID;
      ReportedUserID = reportedUserID;
    }

    // Factory method để tạo Report từ CSV columns
    public static Report? FromColumns(string[] values)
    {
      // Format: ReportID,ReportType,ContentID,ReportedAt,ReporterUserID,ReportedUserID
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

          return new Report(reportID, reportType, contentID, reportedAt, reporterUserID, reportedUserID);
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
      // Format: ReportID,ReportType,ContentID,ReportedAt,ReporterUserID,ReportedUserID
      return $"{ReportID},{ReportType},{ContentID},{ReportedAt:yyyy-MM-dd HH:mm:ss},{ReporterUserID},{ReportedUserID}";
    }
  }
}
