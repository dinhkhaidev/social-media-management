namespace SocialManager.models
{
  public enum ReportStatus
  {
    Pending,        // maps to "Pending Action"
    UnderReview,    // maps to "In Review"
    Resolved        // maps to "Resolved"
  }

  public static class ReportStatusExtensions
  {
    public static string ToStatusString(this ReportStatus status)
    {
      switch (status)
      {
        case ReportStatus.Pending:
          return "Pending Action";
        case ReportStatus.UnderReview:
          return "In Review";
        case ReportStatus.Resolved:
          return "Resolved";
        default:
          return "New";
      }
    }
  }
}
