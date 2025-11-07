using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SocialManager.utils
{
  public static class UIHelper
  {
    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
    );

    /// <summary>
    /// Áp dụng bo góc cho button
    /// </summary>
    public static void ApplyRoundedCorners(Button button, int radius = 10)
    {
      button.FlatStyle = FlatStyle.Flat;
      button.FlatAppearance.BorderSize = 0;

      button.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, radius, radius));

      button.Resize += (s, e) =>
      {
        if (button != null && !button.IsDisposed)
        {
          button.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, radius, radius));
        }
      };
    }

    /// <summary>
    /// Áp dụng bo góc cho panel
    /// </summary>
    public static void ApplyRoundedCorners(Panel panel, int radius = 15)
    {
      panel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel.Width, panel.Height, radius, radius));

      panel.Resize += (s, e) =>
      {
        if (panel != null && !panel.IsDisposed)
        {
          panel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel.Width, panel.Height, radius, radius));
        }
      };
    }

    /// <summary>
    /// Áp dụng bo góc cho textbox
    /// </summary>
    public static void ApplyRoundedCorners(TextBox textBox, int radius = 10)
    {
      textBox.BorderStyle = BorderStyle.FixedSingle;
      textBox.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox.Width, textBox.Height, radius, radius));

      textBox.Resize += (s, e) =>
      {
        if (textBox != null && !textBox.IsDisposed)
        {
          textBox.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, textBox.Width, textBox.Height, radius, radius));
        }
      };
    }

    /// <summary>
    /// Áp dụng bo góc cho groupbox
    /// </summary>
    public static void ApplyRoundedCorners(GroupBox groupBox, int radius = 15)
    {
      groupBox.Paint += (s, e) =>
      {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle rect = new Rectangle(0, 10, groupBox.Width - 1, groupBox.Height - 11);
        using (GraphicsPath path = GetRoundedRectPath(rect, radius))
        using (Pen pen = new Pen(groupBox.ForeColor, 2))
        {
          g.DrawPath(pen, path);
        }

        SizeF textSize = g.MeasureString(groupBox.Text, groupBox.Font);
        Rectangle textRect = new Rectangle(10, 0, (int)textSize.Width + 10, (int)textSize.Height);
        using (SolidBrush brush = new SolidBrush(groupBox.BackColor))
        {
          g.FillRectangle(brush, textRect);
        }

        g.DrawString(groupBox.Text, groupBox.Font, new SolidBrush(groupBox.ForeColor), 15, 0);
      };
    }

    /// <summary>
    /// Tạo GraphicsPath cho rounded rectangle
    /// </summary>
    private static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
    {
      GraphicsPath path = new GraphicsPath();
      float diameter = radius * 2F;

      path.StartFigure();
      path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
      path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
      path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
      path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
      path.CloseFigure();

      return path;
    }

    /// <summary>
    /// Áp dụng bo góc cho PictureBox (avatar)
    /// </summary>
    public static void MakeCircular(PictureBox pictureBox)
    {
      int diameter = Math.Min(pictureBox.Width, pictureBox.Height);
      GraphicsPath path = new GraphicsPath();
      path.AddEllipse(0, 0, diameter, diameter);
      pictureBox.Region = new Region(path);
    }
  }
}
