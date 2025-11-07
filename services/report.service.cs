using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SocialManager.models;

namespace SocialManager.services
{
  public class ReportService
  {
    private const string REPORT_FILE_PATH = "datas\\Report.csv";
    private const string HEADER = "ReportID,ReportType,ContentID,ReportedAt,ReporterUserID,ReportedUserID";
    private List<Report> _reports;

    public ReportService()
    {
      _reports = new List<Report>();
      LoadFromDisk();
    }

    // Load tất cả reports từ CSV
    private void LoadFromDisk()
    {
      try
      {
        if (!File.Exists(REPORT_FILE_PATH))
        {
          // Tạo file mới với header
          Directory.CreateDirectory(Path.GetDirectoryName(REPORT_FILE_PATH)!);
          File.WriteAllText(REPORT_FILE_PATH, HEADER + Environment.NewLine, Encoding.UTF8);
          return;
        }

        var lines = File.ReadAllLines(REPORT_FILE_PATH, Encoding.UTF8);
        _reports.Clear();

        for (int i = 1; i < lines.Length; i++) // Skip header
        {
          if (string.IsNullOrWhiteSpace(lines[i])) continue;

          var values = lines[i].Split(',');
          var report = Report.FromColumns(values);
          if (report != null)
          {
            _reports.Add(report);
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error loading reports: {ex.Message}");
      }
    }

    // Lưu tất cả reports vào CSV
    private bool SaveAllReports()
    {
      try
      {
        var lines = new List<string> { HEADER };
        lines.AddRange(_reports.Select(r => r.ToCsvLine()));
        File.WriteAllLines(REPORT_FILE_PATH, lines, Encoding.UTF8);
        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error saving reports: {ex.Message}");
        return false;
      }
    }

    // Tạo report mới
    public bool CreateReport(string reportType, int contentID, Guid reporterUserID, Guid reportedUserID)
    {
      try
      {
        // Kiểm tra không cho tự report chính mình
        if (reporterUserID == reportedUserID)
        {
          return false;
        }

        // Kiểm tra đã report content này chưa (tránh spam)
        var existingReport = _reports.FirstOrDefault(r =>
            r.ReportType == reportType &&
            r.ContentID == contentID &&
            r.ReporterUserID == reporterUserID);

        if (existingReport != null)
        {
          // Đã report rồi, không cho report lại
          return false;
        }

        // Tạo ID mới
        int newID = _reports.Count > 0 ? _reports.Max(r => r.ReportID) + 1 : 1;

        var report = new Report(
            newID,
            reportType,
            contentID,
            DateTime.Now,
            reporterUserID,
            reportedUserID
        );

        _reports.Add(report);
        return SaveAllReports();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error creating report: {ex.Message}");
        return false;
      }
    }

    // Đếm số lần user bị report
    public int GetReportCountForUser(Guid userId)
    {
      return _reports.Count(r => r.ReportedUserID == userId);
    }

    // Lấy tất cả reports của 1 user bị report
    public List<Report> GetReportsForUser(Guid userId)
    {
      return _reports.Where(r => r.ReportedUserID == userId).ToList();
    }

    // Lấy reports theo loại (Post hoặc Comment)
    public List<Report> GetReportsByType(string reportType)
    {
      return _reports.Where(r => r.ReportType == reportType).ToList();
    }

    // Lấy reports cho 1 content cụ thể
    public List<Report> GetReportsForContent(string reportType, int contentID)
    {
      return _reports.Where(r => r.ReportType == reportType && r.ContentID == contentID).ToList();
    }

    // Lấy tất cả reports
    public List<Report> GetAllReports()
    {
      return _reports.ToList();
    }

    // Xóa tất cả reports của 1 user (khi admin xử lý)
    public bool ClearReportsForUser(Guid userId)
    {
      try
      {
        _reports.RemoveAll(r => r.ReportedUserID == userId);
        return SaveAllReports();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error clearing reports: {ex.Message}");
        return false;
      }
    }

    // Xóa reports cho 1 content (khi content bị xóa)
    public bool DeleteReportsForContent(string reportType, int contentID)
    {
      try
      {
        _reports.RemoveAll(r => r.ReportType == reportType && r.ContentID == contentID);
        return SaveAllReports();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error deleting reports for content: {ex.Message}");
        return false;
      }
    }
  }
}
