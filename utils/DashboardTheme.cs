using System.Drawing;

namespace SocialManager.utils
{
  /// <summary>
  /// Dashboard theme configuration following Material Design and modern UI principles
  /// </summary>
  public static class DashboardTheme
  {
    #region Color Palette
    // Primary Colors
    public static readonly Color Primary = Color.FromArgb(52, 152, 219);        // Blue
    public static readonly Color PrimaryDark = Color.FromArgb(41, 128, 185);    // Darker Blue
    public static readonly Color PrimaryLight = Color.FromArgb(174, 214, 241);  // Light Blue

    // Success Colors
    public static readonly Color Success = Color.FromArgb(46, 204, 113);        // Green
    public static readonly Color SuccessDark = Color.FromArgb(39, 174, 96);     // Darker Green

    // Warning Colors
    public static readonly Color Warning = Color.FromArgb(230, 126, 34);        // Orange
    public static readonly Color WarningDark = Color.FromArgb(211, 84, 0);      // Darker Orange

    // Danger Colors
    public static readonly Color Danger = Color.FromArgb(231, 76, 60);          // Red
    public static readonly Color DangerDark = Color.FromArgb(192, 57, 43);      // Darker Red

    // Info Colors
    public static readonly Color Info = Color.FromArgb(155, 89, 182);           // Purple
    public static readonly Color InfoDark = Color.FromArgb(142, 68, 173);       // Darker Purple

    // Neutral Colors
    public static readonly Color BackgroundLight = Color.FromArgb(247, 249, 252);
    public static readonly Color CardBackground = Color.White;
    public static readonly Color TextPrimary = Color.FromArgb(44, 62, 80);      // Dark Gray
    public static readonly Color TextSecondary = Color.FromArgb(127, 140, 141); // Medium Gray
    public static readonly Color TextMuted = Color.FromArgb(149, 165, 166);     // Light Gray

    // Border Colors
    public static readonly Color BorderLight = Color.FromArgb(236, 240, 241);
    public static readonly Color BorderMedium = Color.FromArgb(220, 221, 222);
    public static readonly Color BorderDark = Color.FromArgb(189, 195, 199);

    // Hover/Active States
    public static readonly Color HoverBackground = Color.FromArgb(250, 252, 255);
    public static readonly Color ActiveBackground = Color.FromArgb(245, 247, 250);
    #endregion

    #region Spacing
    public const int SpacingXs = 5;
    public const int SpacingS = 10;
    public const int SpacingM = 15;
    public const int SpacingL = 20;
    public const int SpacingXl = 30;
    public const int SpacingXxl = 40;
    #endregion

    #region Border Radius
    public const int RadiusSmall = 6;
    public const int RadiusMedium = 10;
    public const int RadiusLarge = 12;
    public const int RadiusXLarge = 16;
    #endregion

    #region Shadows
    public static readonly Color ShadowLight = Color.FromArgb(10, 0, 0, 0);
    public static readonly Color ShadowMedium = Color.FromArgb(20, 0, 0, 0);
    public static readonly Color ShadowDark = Color.FromArgb(30, 0, 0, 0);
    #endregion

    #region Typography
    public static Font GetFont(FontSize size, FontWeight weight = FontWeight.Regular)
    {
      float fontSize = size switch
      {
        FontSize.Xs => 7F,
        FontSize.Sm => 8F,
        FontSize.Base => 9F,
        FontSize.Md => 10F,
        FontSize.Lg => 12F,
        FontSize.Xl => 14F,
        FontSize.Xxl => 18F,
        FontSize.Display => 24F,
        FontSize.Hero => 28F,
        _ => 9F
      };

      FontStyle fontStyle = weight switch
      {
        FontWeight.Bold => FontStyle.Bold,
        FontWeight.Italic => FontStyle.Italic,
        FontWeight.BoldItalic => FontStyle.Bold | FontStyle.Italic,
        _ => FontStyle.Regular
      };

      return new Font("Segoe UI", fontSize, fontStyle);
    }
    #endregion

    #region Enums
    public enum FontSize
    {
      Xs,      // 7px
      Sm,      // 8px
      Base,    // 9px
      Md,      // 10px
      Lg,      // 12px
      Xl,      // 14px
      Xxl,     // 18px
      Display, // 24px
      Hero     // 28px
    }

    public enum FontWeight
    {
      Regular,
      Bold,
      Italic,
      BoldItalic
    }
    #endregion

    #region Helper Methods
    public static Color WithOpacity(Color color, int alpha)
    {
      return Color.FromArgb(alpha, color.R, color.G, color.B);
    }

    public static Color Lighten(Color color, double percentage)
    {
      int r = (int)(color.R + (255 - color.R) * percentage);
      int g = (int)(color.G + (255 - color.G) * percentage);
      int b = (int)(color.B + (255 - color.B) * percentage);
      return Color.FromArgb(r, g, b);
    }

    public static Color Darken(Color color, double percentage)
    {
      int r = (int)(color.R * (1 - percentage));
      int g = (int)(color.G * (1 - percentage));
      int b = (int)(color.B * (1 - percentage));
      return Color.FromArgb(r, g, b);
    }
    #endregion

    #region Theme Application for User Forms
    /// <summary>
    /// Dark mode colors for user-facing forms
    /// </summary>
    public static class DarkTheme
    {
      public static readonly Color Background = Color.FromArgb(24, 25, 26);        // Main background
      public static readonly Color Surface = Color.FromArgb(36, 37, 38);           // Cards/Panels
      public static readonly Color SurfaceVariant = Color.FromArgb(42, 43, 44);    // Hover states
      public static readonly Color TextPrimary = Color.FromArgb(242, 243, 245);    // Main text
      public static readonly Color TextSecondary = Color.FromArgb(176, 179, 184);  // Secondary text
      public static readonly Color Border = Color.FromArgb(54, 57, 63);            // Borders
      public static readonly Color Accent = Color.FromArgb(88, 101, 242);          // Blue accent
    }

    /// <summary>
    /// Light mode colors for user-facing forms
    /// </summary>
    public static class LightTheme
    {
      public static readonly Color Background = Color.FromArgb(240, 242, 245);     // Main background
      public static readonly Color Surface = Color.White;                          // Cards/Panels
      public static readonly Color SurfaceVariant = Color.FromArgb(250, 250, 250); // Hover states
      public static readonly Color TextPrimary = Color.FromArgb(5, 5, 5);          // Main text
      public static readonly Color TextSecondary = Color.FromArgb(101, 103, 107);  // Secondary text
      public static readonly Color Border = Color.FromArgb(228, 230, 235);         // Borders
      public static readonly Color Accent = Color.FromArgb(24, 119, 242);          // Blue accent
    }

    /// <summary>
    /// Apply theme to a user-facing form (Newsfeed, Profile, etc.)
    /// </summary>
    public static void ApplyThemeToForm(System.Windows.Forms.Form form, bool isDarkMode)
    {
      var bgColor = isDarkMode ? DarkTheme.Background : LightTheme.Background;
      var textColor = isDarkMode ? DarkTheme.TextPrimary : LightTheme.TextPrimary;
      var surfaceColor = isDarkMode ? DarkTheme.Surface : LightTheme.Surface;

      form.BackColor = bgColor;
      form.ForeColor = textColor;

      ApplyThemeToControlRecursive(form, isDarkMode);
    }

    /// <summary>
    /// Apply theme to a UserControl
    /// </summary>
    public static void ApplyThemeToUserControl(System.Windows.Forms.UserControl control, bool isDarkMode)
    {
      var bgColor = isDarkMode ? DarkTheme.Background : LightTheme.Background;
      var textColor = isDarkMode ? DarkTheme.TextPrimary : LightTheme.TextPrimary;

      control.BackColor = bgColor;
      control.ForeColor = textColor;

      ApplyThemeToControlRecursive(control, isDarkMode);
    }

    private static void ApplyThemeToControlRecursive(System.Windows.Forms.Control control, bool isDarkMode)
    {
      var bgColor = isDarkMode ? DarkTheme.Background : LightTheme.Background;
      var surfaceColor = isDarkMode ? DarkTheme.Surface : LightTheme.Surface;
      var textColor = isDarkMode ? DarkTheme.TextPrimary : LightTheme.TextPrimary;
      var textSecondary = isDarkMode ? DarkTheme.TextSecondary : LightTheme.TextSecondary;
      var borderColor = isDarkMode ? DarkTheme.Border : LightTheme.Border;

      try
      {
        // Skip controls that should keep their custom colors
        if (control.Name != null && (
            control.Name.Contains("btn") && (
                control.BackColor == Primary ||
                control.BackColor == Success ||
                control.BackColor == Danger ||
                control.BackColor == Warning ||
                control.BackColor == Color.FromArgb(24, 119, 242) ||
                control.BackColor == Color.FromArgb(0, 123, 255)
            )
        ))
        {
          // Keep colored buttons, only update text if needed
          if (isDarkMode)
            control.ForeColor = Color.White;
          return;
        }

        // Apply based on control type
        if (control is System.Windows.Forms.Panel panel)
        {
          // Main panels use background, cards use surface
          if (panel.Name == "pnlHeader" || panel.Name == "pnlSidebar" || panel.Name == "pnlFilterSection")
            panel.BackColor = surfaceColor;
          else if (panel.BackColor == Color.White || panel.BackColor == Color.FromArgb(240, 242, 245))
            panel.BackColor = surfaceColor;
        }
        else if (control is System.Windows.Forms.Label label)
        {
          if (label.ForeColor == System.Drawing.Color.Gray || label.ForeColor == System.Drawing.Color.FromArgb(101, 103, 107))
            label.ForeColor = textSecondary;
          else if (label.BackColor == System.Drawing.Color.Transparent || label.BackColor.A < 255)
            label.ForeColor = textColor;
        }
        else if (control is System.Windows.Forms.TextBox textBox)
        {
          textBox.BackColor = isDarkMode ? DarkTheme.SurfaceVariant : Color.White;
          textBox.ForeColor = textColor;
        }
        else if (control is System.Windows.Forms.ComboBox comboBox)
        {
          comboBox.BackColor = isDarkMode ? DarkTheme.SurfaceVariant : Color.White;
          comboBox.ForeColor = textColor;
        }
        else if (control is System.Windows.Forms.GroupBox groupBox)
        {
          groupBox.BackColor = surfaceColor;
          groupBox.ForeColor = textColor;
        }
        else if (control is System.Windows.Forms.FlowLayoutPanel flp)
        {
          if (flp.BackColor == Color.White || flp.BackColor == Color.FromArgb(240, 242, 245) || flp.BackColor == Color.FromArgb(250, 250, 250))
            flp.BackColor = isDarkMode ? DarkTheme.Background : LightTheme.Background;
        }

        // Recursively apply to children
        foreach (System.Windows.Forms.Control child in control.Controls)
        {
          ApplyThemeToControlRecursive(child, isDarkMode);
        }
      }
      catch { }
    }
    #endregion
  }
}
