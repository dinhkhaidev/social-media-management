using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SocialManager
{
    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(Graphics graphics, Brush brush, Rectangle bounds, int radius)
        {
            if (graphics == null) throw new ArgumentNullException(nameof(graphics));
            if (brush == null) throw new ArgumentNullException(nameof(brush));

            using (GraphicsPath path = CreateRoundedRectanglePath(bounds, radius))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPath(brush, path);
            }
        }

        public static void DrawRoundedRectangle(Graphics graphics, Pen pen, Rectangle bounds, int radius)
        {
            if (graphics == null) throw new ArgumentNullException(nameof(graphics));
            if (pen == null) throw new ArgumentNullException(nameof(pen));

            using (GraphicsPath path = CreateRoundedRectanglePath(bounds, radius))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawPath(pen, path);
            }
        }

        private static GraphicsPath CreateRoundedRectanglePath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // Top left arc
            path.AddArc(arc, 180, 90);

            // Top right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom left arc
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        public static void DrawLineChart(Graphics g, Rectangle bounds, int[] data, string[] labels, Color lineColor, Color pointColor)
        {
            if (data == null || data.Length == 0) return;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            // Calculate points
            int maxValue = data.Max();
            if (maxValue == 0) maxValue = 1;
            
            Point[] points = new Point[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                int x = bounds.X + (i * bounds.Width / Math.Max(data.Length - 1, 1));
                int y = bounds.Y + bounds.Height - (int)((double)data[i] / maxValue * bounds.Height);
                points[i] = new Point(x, y);
            }

            // Draw gradient area under line
            if (points.Length > 1)
            {
                GraphicsPath areaPath = new GraphicsPath();
                areaPath.AddLines(points);
                areaPath.AddLine(points[points.Length - 1].X, points[points.Length - 1].Y, points[points.Length - 1].X, bounds.Bottom);
                areaPath.AddLine(points[points.Length - 1].X, bounds.Bottom, points[0].X, bounds.Bottom);
                areaPath.CloseFigure();

                using (LinearGradientBrush brush = new LinearGradientBrush(
                    bounds,
                    Color.FromArgb(80, lineColor),
                    Color.FromArgb(10, lineColor),
                    90f))
                {
                    g.FillPath(brush, areaPath);
                }
            }

            // Draw line
            using (Pen linePen = new Pen(lineColor, 3))
            {
                linePen.LineJoin = LineJoin.Round;
                if (points.Length > 1)
                    g.DrawLines(linePen, points);
            }

            // Draw points
            using (SolidBrush pointBrush = new SolidBrush(pointColor))
            {
                foreach (var point in points)
                {
                    g.FillEllipse(pointBrush, point.X - 4, point.Y - 4, 8, 8);
                    
                    // Draw white center
                    using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                    {
                        g.FillEllipse(whiteBrush, point.X - 2, point.Y - 2, 4, 4);
                    }
                }
            }

            // Draw labels
            if (labels != null && labels.Length == data.Length)
            {
                using (Font font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point))
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(127, 140, 141)))
                {
                    for (int i = 0; i < points.Length; i++)
                    {
                        string label = labels[i];
                        SizeF labelSize = g.MeasureString(label, font);
                        g.DrawString(label, font, textBrush, 
                            points[i].X - labelSize.Width / 2, 
                            bounds.Bottom + 5);
                    }
                }
            }
        }

        public static void DrawBarChart(Graphics g, Rectangle bounds, int[] data, string[] labels, Color[] colors)
        {
            if (data == null || data.Length == 0) return;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            int maxValue = data.Max();
            if (maxValue == 0) maxValue = 1;
            
            int barWidth = (bounds.Width - (data.Length + 1) * 10) / data.Length;
            if (barWidth < 5) barWidth = 5;

            for (int i = 0; i < data.Length; i++)
            {
                int barHeight = (int)((double)data[i] / maxValue * (bounds.Height - 30));
                int x = bounds.X + 10 + i * (barWidth + 10);
                int y = bounds.Y + bounds.Height - 30 - barHeight;

                Color barColor = colors != null && i < colors.Length ? colors[i] : Color.FromArgb(52, 152, 219);

                // Draw bar with gradient
                Rectangle barRect = new Rectangle(x, y, barWidth, barHeight);
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    barRect,
                    barColor,
                    Color.FromArgb(barColor.R - 30, barColor.G - 30, barColor.B - 30),
                    90f))
                {
                    FillRoundedRectangle(g, brush, barRect, 4);
                }

                // Draw value on top
                using (Font font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point))
                using (SolidBrush textBrush = new SolidBrush(barColor))
                {
                    string valueText = data[i].ToString();
                    SizeF textSize = g.MeasureString(valueText, font);
                    g.DrawString(valueText, font, textBrush, 
                        x + (barWidth - textSize.Width) / 2, 
                        y - 20);
                }

                // Draw label
                if (labels != null && i < labels.Length)
                {
                    using (Font font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point))
                    using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(127, 140, 141)))
                    {
                        string label = labels[i];
                        SizeF labelSize = g.MeasureString(label, font);
                        g.DrawString(label, font, textBrush, 
                            x + (barWidth - labelSize.Width) / 2, 
                            bounds.Y + bounds.Height - 20);
                    }
                }
            }
        }

        public static void DrawPieChart(Graphics g, Rectangle bounds, float[] data, string[] labels, Color[] colors)
        {
            if (data == null || data.Length == 0) return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            float total = data.Sum();
            if (total == 0) return;

            // Calculate center and radius
            int centerX = bounds.X + bounds.Width / 2;
            int centerY = bounds.Y + bounds.Height / 2 - 10;
            int radius = Math.Min(bounds.Width, bounds.Height) / 2 - 40;

            float startAngle = -90; // Start from top

            for (int i = 0; i < data.Length; i++)
            {
                float sweepAngle = (data[i] / total) * 360;
                Color sliceColor = colors != null && i < colors.Length ? colors[i] : 
                    Color.FromArgb(52 + i * 40, 152 - i * 20, 219 - i * 30);

                // Draw pie slice with gradient
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddPie(centerX - radius, centerY - radius, radius * 2, radius * 2, startAngle, sweepAngle);
                    
                    using (PathGradientBrush brush = new PathGradientBrush(path))
                    {
                        brush.CenterColor = sliceColor;
                        brush.SurroundColors = new Color[] { Color.FromArgb(sliceColor.R - 30, sliceColor.G - 30, sliceColor.B - 30) };
                        g.FillPath(brush, path);
                    }
                }

                // Draw border
                using (Pen pen = new Pen(Color.White, 2))
                {
                    g.DrawPie(pen, centerX - radius, centerY - radius, radius * 2, radius * 2, startAngle, sweepAngle);
                }

                // Draw label
                if (labels != null && i < labels.Length)
                {
                    float midAngle = startAngle + sweepAngle / 2;
                    float labelRadius = radius + 30;
                    float labelX = centerX + (float)(labelRadius * Math.Cos(midAngle * Math.PI / 180));
                    float labelY = centerY + (float)(labelRadius * Math.Sin(midAngle * Math.PI / 180));

                    using (Font font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point))
                    using (SolidBrush textBrush = new SolidBrush(sliceColor))
                    {
                        string labelText = $"{labels[i]}\n{data[i]:F0} ({(data[i] / total * 100):F1}%)";
                        SizeF textSize = g.MeasureString(labelText, font);
                        g.DrawString(labelText, font, textBrush, labelX - textSize.Width / 2, labelY - textSize.Height / 2);
                    }
                }

                startAngle += sweepAngle;
            }
        }

        public static void DrawDonutChart(Graphics g, Rectangle bounds, float[] data, string[] labels, Color[] colors)
        {
            if (data == null || data.Length == 0) return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            float total = data.Sum();
            if (total == 0) return;

            int centerX = bounds.X + bounds.Width / 2;
            int centerY = bounds.Y + bounds.Height / 2;
            int outerRadius = Math.Min(bounds.Width, bounds.Height) / 2 - 50;
            int innerRadius = outerRadius - 40;

            float startAngle = -90;

            for (int i = 0; i < data.Length; i++)
            {
                float sweepAngle = (data[i] / total) * 360;
                Color sliceColor = colors != null && i < colors.Length ? colors[i] : 
                    Color.FromArgb(52 + i * 40, 152 - i * 20, 219 - i * 30);

                using (GraphicsPath outerPath = new GraphicsPath())
                using (GraphicsPath innerPath = new GraphicsPath())
                {
                    outerPath.AddArc(centerX - outerRadius, centerY - outerRadius, outerRadius * 2, outerRadius * 2, startAngle, sweepAngle);
                    innerPath.AddArc(centerX - innerRadius, centerY - innerRadius, innerRadius * 2, innerRadius * 2, startAngle + sweepAngle, -sweepAngle);
                    
                    outerPath.AddPath(innerPath, true);
                    outerPath.CloseFigure();

                    using (SolidBrush brush = new SolidBrush(sliceColor))
                    {
                        g.FillPath(brush, outerPath);
                    }

                    using (Pen pen = new Pen(Color.White, 2))
                    {
                        g.DrawPath(pen, outerPath);
                    }
                }

                startAngle += sweepAngle;
            }

            // Draw center circle
            using (SolidBrush centerBrush = new SolidBrush(Color.White))
            {
                g.FillEllipse(centerBrush, centerX - innerRadius, centerY - innerRadius, innerRadius * 2, innerRadius * 2);
            }

            // Draw total in center
            using (Font font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point))
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(52, 73, 94)))
            {
                string totalText = total.ToString("F0");
                SizeF textSize = g.MeasureString(totalText, font);
                g.DrawString(totalText, font, textBrush, centerX - textSize.Width / 2, centerY - textSize.Height / 2 - 10);
            }

            using (Font font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point))
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(127, 140, 141)))
            {
                string label = "Tổng";
                SizeF textSize = g.MeasureString(label, font);
                g.DrawString(label, font, textBrush, centerX - textSize.Width / 2, centerY + 10);
            }
        }
    }
}
