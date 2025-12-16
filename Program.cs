using SocialManager;
using SocialManager.frm;
using System.Text;

namespace SocialManager
{
  internal static class Program
  {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      // Register UTF-8 encoding for Vietnamese support
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
      
      // To customize application configuration such as set high DPI settings or default font,
      // see https://aka.ms/applicationconfiguration.
      ApplicationConfiguration.Initialize();
      
      // Set default font with Vietnamese support
      Application.SetDefaultFont(new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular));
      
      // Start with Login form normally
      Application.Run(new frmLogin());
    }
  }
}
