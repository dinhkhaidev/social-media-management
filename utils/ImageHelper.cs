using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialManager.utils
{
    public static class ImageHelper
    {
        private static readonly HttpClient httpClient = new HttpClient();
        
        public static void LoadImageFromFile(string fileName, PictureBox pictureBox)
        {
            try
            {
                string imagePath = Path.Combine(Application.StartupPath, "images", fileName);
                
                if (File.Exists(imagePath))
                {
                    // Check file size
                    var fileInfo = new FileInfo(imagePath);
                    MessageBox.Show($"File found: {imagePath}\nSize: {fileInfo.Length} bytes", "Debug");
                    
                    if (fileInfo.Length > 0)
                    {
                        using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            pictureBox.Image = Image.FromStream(fs);
                            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        MessageBox.Show("Image loaded successfully!", "Success");
                    }
                    else
                    {
                        CreateTestImage(pictureBox, "EMPTY FILE", Color.Orange);
                    }
                }
                else
                {
                    CreateTestImage(pictureBox, "NO FILE", Color.Red);
                }
            }
            catch (Exception ex)
            {
                CreateTestImage(pictureBox, "ERROR", Color.Purple);
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }
        
        private static void CreateTestImage(PictureBox pictureBox, string text, Color color)
        {
            var bitmap = new Bitmap(100, 100);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(color);
                g.DrawString(text, new Font("Arial", 10, FontStyle.Bold), Brushes.White, 5, 40);
                g.DrawString("SolidVerse", new Font("Arial", 8), Brushes.Yellow, 15, 60);
            }
            pictureBox.Image = bitmap;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        
        public static void CreateSolidVerseLogo(PictureBox pictureBox)
        {
            var bitmap = new Bitmap(100, 100);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                
                // Background gradient
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, 100, 100), 
                    Color.FromArgb(52, 152, 219), 
                    Color.FromArgb(41, 128, 185), 
                    45f))
                {
                    g.FillRectangle(brush, 0, 0, 100, 100);
                }
                
                // Draw "S" for SolidVerse
                using (var font = new Font("Arial", 36, FontStyle.Bold))
                {
                    g.DrawString("S", font, Brushes.White, 25, 25);
                }
                
                // Draw small circles for social network effect
                using (var pen = new Pen(Color.White, 2))
                {
                    g.DrawEllipse(pen, 10, 10, 8, 8);
                    g.DrawEllipse(pen, 82, 10, 8, 8);
                    g.DrawEllipse(pen, 10, 82, 8, 8);
                    g.DrawEllipse(pen, 82, 82, 8, 8);
                }
            }
            pictureBox.Image = bitmap;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public static async Task<Image> LoadImageFromUrlAsync(string url)
        {
            try
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    return Image.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image: {ex.Message}");
            }
            return null;
        }

        public static void LoadImageFromUrl(string url, PictureBox pictureBox)
        {
            Task.Run(async () =>
            {
                var image = await LoadImageFromUrlAsync(url);
                if (image != null && !pictureBox.IsDisposed)
                {
                    pictureBox.Invoke(new Action(() =>
                    {
                        pictureBox.Image = image;
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    }));
                }
            });
        }
    }
}