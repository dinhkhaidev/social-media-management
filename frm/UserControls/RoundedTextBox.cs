using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SocialManager.controls
{
    [DefaultEvent("TextChanged")]
    public class RoundedTextBox : UserControl
    {
        private Color borderColor = Color.MediumSlateBlue;
        private int borderSize = 2;
        private bool underlinedStyle = false;
        private Color borderFocusColor = Color.HotPink;
        private bool isFocused = false;
        private int borderRadius = 0;
        private Color placeholderColor = Color.DarkGray;
        private string placeholderText = "";
        private bool isPlaceholder = false;
        private bool isPasswordChar = false;
        private TextBox textBox;

        public RoundedTextBox()
        {
            textBox = new TextBox();
            textBox.Dock = DockStyle.Fill;
            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = this.BackColor;
            textBox.ForeColor = this.ForeColor;
            textBox.Font = this.Font;
            this.Controls.Add(textBox);
            this.Padding = new Padding(10, 7, 10, 7);
            this.Size = new Size(250, 30);
            this.BackColor = Color.White;
            this.ForeColor = Color.FromArgb(64, 64, 64);
            textBox.TextChanged += TextBox_TextChanged;
            textBox.Click += TextBox_Click;
            textBox.MouseEnter += TextBox_MouseEnter;
            textBox.MouseLeave += TextBox_MouseLeave;
            textBox.KeyPress += TextBox_KeyPress;
            textBox.Enter += TextBox_Enter;
            textBox.Leave += TextBox_Leave;
        }

        [Category("Custom Appearance")]
        public Color BorderColor { get => borderColor; set { borderColor = value; this.Invalidate(); } }
        [Category("Custom Appearance")]
        public int BorderSize { get => borderSize; set { borderSize = value; this.Invalidate(); } }
        [Category("Custom Appearance")]
        public bool UnderlinedStyle { get => underlinedStyle; set { underlinedStyle = value; this.Invalidate(); } }
        [Category("Custom Appearance")]
        public Color BorderFocusColor { get => borderFocusColor; set { borderFocusColor = value; } }
        [Category("Custom Appearance")]
        public int BorderRadius { get => borderRadius; set { if (value >= 0) { borderRadius = value; this.Invalidate(); } } }
        [Category("Custom Appearance")]
        public Color PlaceholderColor { get => placeholderColor; set { placeholderColor = value; if (isPlaceholder) textBox.ForeColor = value; } }
        [Category("Custom Appearance")]
        public string PlaceholderText { get => placeholderText; set { placeholderText = value; SetPlaceholder(); } }
        [Category("Custom Appearance")]
        public bool PasswordChar { get => isPasswordChar; set { isPasswordChar = value; if (!isPlaceholder) textBox.UseSystemPasswordChar = value; } }
        public override Color BackColor { get => base.BackColor; set { base.BackColor = value; textBox.BackColor = value; } }
        public override Color ForeColor { get => base.ForeColor; set { base.ForeColor = value; textBox.ForeColor = value; } }
        public override Font? Font { get => base.Font; set { base.Font = value; textBox.Font = value; if (this.DesignMode) UpdateControlHeight(); } }
        public override string? Text { get => isPlaceholder ? "" : textBox.Text; set { textBox.Text = value; SetPlaceholder(); } }
        [Browsable(true)]
        public bool Multiline { get => textBox.Multiline; set => textBox.Multiline = value; }

        public void Clear()
        {
            textBox.Clear();
            SetPlaceholder();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;
            if (borderRadius > 0)
            {
                var rectBorderSmooth = this.ClientRectangle;
                var rectBorder = Rectangle.Inflate(rectBorderSmooth, -borderSize, -borderSize);
                int smoothSize = borderSize > 0 ? borderSize : 1;
                Color parentBackColor = this.Parent?.BackColor ?? Color.WhiteSmoke;
                using (GraphicsPath pathBorderSmooth = GetFigurePath(rectBorderSmooth, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penBorderSmooth = new Pen(parentBackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(pathBorderSmooth);
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = PenAlignment.Center;
                    if (isFocused) penBorder.Color = borderFocusColor;
                    if (underlinedStyle)
                    {
                        graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                        graph.SmoothingMode = SmoothingMode.None;
                        graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    }
                    else
                    {
                        graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                        graph.DrawPath(penBorder, pathBorder);
                    }
                }
            }
            else
            {
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(this.ClientRectangle);
                    penBorder.Alignment = PenAlignment.Inset;
                    if (isFocused) penBorder.Color = borderFocusColor;
                    if (underlinedStyle) graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    else graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                }
            }
        }

        protected override void OnResize(EventArgs e) { base.OnResize(e); if (this.DesignMode) UpdateControlHeight(); }
        protected override void OnLoad(EventArgs e) { base.OnLoad(e); UpdateControlHeight(); SetPlaceholder(); }

        private void SetPlaceholder() { if (string.IsNullOrWhiteSpace(textBox.Text) && !string.IsNullOrWhiteSpace(placeholderText)) { isPlaceholder = true; textBox.Text = placeholderText; textBox.ForeColor = placeholderColor; if (isPasswordChar) textBox.UseSystemPasswordChar = false; } }
        private void RemovePlaceholder() { if (isPlaceholder && !string.IsNullOrWhiteSpace(placeholderText)) { isPlaceholder = false; textBox.Text = ""; textBox.ForeColor = this.ForeColor; if (isPasswordChar) textBox.UseSystemPasswordChar = true; } }
        private GraphicsPath GetFigurePath(Rectangle rect, int radius) { GraphicsPath path = new GraphicsPath(); float curveSize = radius * 2F; path.StartFigure(); path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90); path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90); path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90); path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90); path.CloseFigure(); return path; }
        private void UpdateControlHeight() { if (textBox.Multiline == false) { int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 1; textBox.Multiline = true; textBox.MinimumSize = new Size(0, txtHeight); textBox.Multiline = false; this.Height = textBox.Height + this.Padding.Top + this.Padding.Bottom; } }

        private void TextBox_TextChanged(object? sender, EventArgs e) => this.OnTextChanged(e);
        private void TextBox_Click(object? sender, EventArgs e) => this.OnClick(e);
        private void TextBox_MouseEnter(object? sender, EventArgs e) => this.OnMouseEnter(e);
        private void TextBox_MouseLeave(object? sender, EventArgs e) => this.OnMouseLeave(e);
        private void TextBox_KeyPress(object? sender, KeyPressEventArgs e) => this.OnKeyPress(e);
        private void TextBox_Enter(object? sender, EventArgs e) { isFocused = true; this.Invalidate(); RemovePlaceholder(); }
        private void TextBox_Leave(object? sender, EventArgs e) { isFocused = false; this.Invalidate(); SetPlaceholder(); }
    }
}