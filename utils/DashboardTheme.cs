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
    }
}
