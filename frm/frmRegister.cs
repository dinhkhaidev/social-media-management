using Microsoft.VisualBasic.ApplicationServices;
using SocialManager.services;
using SocialManager.validations.register;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;
namespace SocialManager.frm;

public partial class frmRegister : Form
{
    private readonly UserService _userService;
    private readonly frmLogin _parentLoginForm; // Reference to parent login form
    
    // Constructor c? (ð? backward compatibility)
    public frmRegister() : this(null)
    {
    }
    
    // Constructor m?i nh?n login form reference
    public frmRegister(frmLogin parentLoginForm)
    {
        InitializeComponent();
        InitializeForm();
        _userService = new UserService();
        _parentLoginForm = parentLoginForm;
    }

    private void InitializeForm()
    {
        // Setup form properties
        this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

        // Setup validation
        SetupValidation();

        // Set default gender
        cmbGender.SelectedIndex = 0;

        // Setup Enter key handling
        this.AcceptButton = btnRegister;
    }

    private void SetupValidation()
    {
        // FIX: S? d?ng static methods
        txtUsername.TextChanged += inputValidateEvent.ValidateUsername;
        txtEmail.TextChanged += inputValidateEvent.ValidateEmail;
        txtPassword.TextChanged += inputValidateEvent.ValidatePassword;
        txtConfirmPassword.TextChanged += inputValidateEvent.ValidateConfirmPassword;
    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
        if (!ValidateForm())
            return;

        btnRegister.Text = "CREATING ACCOUNT...";
        btnRegister.Enabled = false;
        this.Cursor = Cursors.WaitCursor;

        try
        {
            if (_userService.CreateUser(
                txtUsername.Text.Trim(),
                txtPassword.Text,
                txtFullName.Text.Trim(),
                txtEmail.Text.Trim(),
                txtPhone.Text?.Trim() ?? "",
                cmbGender.SelectedIndex,
                dtpDateOfBirth.Value,
                "",
                "",
                ""
            ))
            {
                string username = txtUsername.Text.Trim();
                
                // Force refresh user service to ensure latest data is available for login
                _userService.RefreshUser();
                
                MessageBox.Show("Tài kho?n ð? ðý?c t?o thành công! B?n có th? ðãng nh?p ngay bây gi?.",
                    "Registration Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // FIX: S? d?ng parent login form n?u có, không t?o m?i
                if (_parentLoginForm != null)
                {
                    // Refresh parent login form's user data
                    _parentLoginForm.RefreshUserData();
                    
                    // Set username trong parent login form
                    _parentLoginForm.SetUsername(username);
                    
                    // Close register form và parent login s? hi?n l?i t? ð?ng
                    this.Close();
                }
                else
                {
                    // Fallback: t?o login form m?i n?u không có parent
                    frmLogin loginForm = new frmLogin();
                    this.Hide();
                    loginForm.SetUsername(username);
                    loginForm.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Không th? t?o tài kho?n. Vui l?ng th? l?i.",
                    "Ðãng k? th?t b?i", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ð? x?y ra l?i khi ðãng k?: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            // Reset button state
            btnRegister.Text = "CREATE ACCOUNT";
            btnRegister.Enabled = true;
            this.Cursor = Cursors.Default;
        }
    }

    private bool ValidateForm()
    {
        // Username validation
        if (string.IsNullOrWhiteSpace(txtUsername.Text))
        {
            ShowValidationError("Please enter a username.", txtUsername);
            return false;
        }

        if (txtUsername.Text.Length < 3)
        {
            ShowValidationError("Username must be at least 3 characters long.", txtUsername);
            return false;
        }

        // Email validation
        if (string.IsNullOrWhiteSpace(txtEmail.Text))
        {
            ShowValidationError("Please enter an email address.", txtEmail);
            return false;
        }

        if (!inputValidateEvent.IsValidEmail(txtEmail.Text))
        {
            ShowValidationError("Please enter a valid email address.", txtEmail);
            return false;
        }

        // Full name validation
        if (string.IsNullOrWhiteSpace(txtFullName.Text))
        {
            ShowValidationError("Please enter your full name.", txtFullName);
            return false;
        }

        // Password validation
        if (string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            ShowValidationError("Please enter a password.", txtPassword);
            return false;
        }

        if (txtPassword.Text.Length < 6)
        {
            ShowValidationError("Password must be at least 6 characters long.", txtPassword);
            return false;
        }

        // Confirm password validation
        if (txtConfirmPassword.Text != txtPassword.Text)
        {
            ShowValidationError("Passwords do not match.", txtConfirmPassword);
            return false;
        }

        // Terms validation
        if (!chkTerms.Checked)
        {
            MessageBox.Show("Vui l?ng ch?p nh?n Ði?u kho?n d?ch v? và Chính sách b?o m?t.",
                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // Check if username or email already exists
        if (_userService.UserExists(txtUsername.Text, txtEmail.Text))
        {
            MessageBox.Show("Tên ðãng nh?p ho?c email ð? t?n t?i. Vui l?ng ch?n tên khác.",
                "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }


    private void ShowValidationError(string message, TextBox textBox)
    {
        MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        textBox.Focus();
    }


    private void llblLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        // FIX: Không t?o form Login m?i, ch? ðóng register form
        // Parent login form s? hi?n l?i t? ð?ng
        this.Close();
    }
    
    private void button1_Click(object sender, EventArgs e)
    {
        // FIX: Same logic nhý llblLogin_LinkClicked
        this.Close();
    }


    // Custom paint events for modern UI - using GraphicsExtensions from Admin form
    private void pnlRegisterCard_Paint(object sender, PaintEventArgs e)
    {
        Panel panel = sender as Panel;
        if (panel != null)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw subtle shadow
            using (var shadowBrush = new SolidBrush(Color.FromArgb(15, 0, 0, 0)))
            {
                GraphicsExtensions.FillRoundedRectangle(e.Graphics, shadowBrush, new Rectangle(5, 5, panel.Width - 5, panel.Height - 5), 15);
            }

            // Draw main card
            using (var cardBrush = new SolidBrush(Color.White))
            {
                GraphicsExtensions.FillRoundedRectangle(e.Graphics, cardBrush, new Rectangle(0, 0, panel.Width - 5, panel.Height - 5), 15);
            }
        }
    }

    private void pnlInput_Paint(object sender, PaintEventArgs e)
    {
        Panel panel = sender as Panel;
        if (panel != null)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brush = new SolidBrush(panel.BackColor))
            {
                GraphicsExtensions.FillRoundedRectangle(e.Graphics, brush, new Rectangle(0, 0, panel.Width, panel.Height), 8);
            }

            // Draw border
            Color borderColor = panel.BackColor == Color.FromArgb(240, 248, 255) ?
                Color.FromArgb(52, 152, 219) : Color.FromArgb(220, 221, 222);
            using (var pen = new Pen(borderColor, 1))
            {
                GraphicsExtensions.DrawRoundedRectangle(e.Graphics, pen, new Rectangle(0, 0, panel.Width - 1, panel.Height - 1), 8);
            }
        }
    }


    // Icon paint events
    private void picLogo_Paint(object sender, PaintEventArgs e)
    {
        DrawLogo(e.Graphics, new Rectangle(5, 5, 40, 40), Color.FromArgb(46, 204, 113));
    }

    private void picAppLogo_Paint(object sender, PaintEventArgs e)
    {
        DrawLogo(e.Graphics, new Rectangle(10, 10, 60, 60), Color.White);
    }

    private void picUsernameIcon_Paint(object sender, PaintEventArgs e)
    {
        DrawUserIcon(e.Graphics, new Rectangle(2, 2, 16, 16), Color.FromArgb(127, 140, 141));
    }

    private void picEmailIcon_Paint(object sender, PaintEventArgs e)
    {
        DrawEmailIcon(e.Graphics, new Rectangle(2, 2, 16, 16), Color.FromArgb(127, 140, 141));
    }

    private void picFullNameIcon_Paint(object sender, PaintEventArgs e)
    {
        DrawPersonIcon(e.Graphics, new Rectangle(2, 2, 16, 16), Color.FromArgb(127, 140, 141));
    }

    private void picPhoneIcon_Paint(object sender, PaintEventArgs e)
    {
        DrawPhoneIcon(e.Graphics, new Rectangle(2, 2, 16, 16), Color.FromArgb(127, 140, 141));
    }

    private void picPasswordIcon_Paint(object sender, PaintEventArgs e)
    {
        DrawLockIcon(e.Graphics, new Rectangle(2, 2, 16, 16), Color.FromArgb(127, 140, 141));
    }

    private void picConfirmPasswordIcon_Paint(object sender, PaintEventArgs e)
    {
        DrawLockIcon(e.Graphics, new Rectangle(2, 2, 16, 16), Color.FromArgb(127, 140, 141));
    }

    private void picGenderIcon_Paint(object sender, PaintEventArgs e)
    {
        DrawGenderIcon(e.Graphics, new Rectangle(2, 2, 16, 16), Color.FromArgb(127, 140, 141));
    }

    private void picDateIcon_Paint(object sender, PaintEventArgs e)
    {
        DrawCalendarIcon(e.Graphics, new Rectangle(2, 2, 16, 16), Color.FromArgb(127, 140, 141));
    }

    private void picAddressIcon_Paint(object sender, PaintEventArgs e)
    {
        DrawLocationIcon(e.Graphics, new Rectangle(2, 2, 16, 16), Color.FromArgb(127, 140, 141));
    }

    // Icon drawing methods
    private void DrawLogo(Graphics g, Rectangle rect, Color color)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using (var brush = new SolidBrush(color))
        using (var pen = new Pen(Color.White, 2))
        {
            g.FillEllipse(brush, rect);
            g.DrawEllipse(pen, rect.X + 8, rect.Y + 8, 12, 12);
            g.DrawEllipse(pen, rect.X + 25, rect.Y + 8, 12, 12);
            g.DrawEllipse(pen, rect.X + 16, rect.Y + 25, 12, 12);
            g.DrawLine(pen, rect.X + 20, rect.Y + 14, rect.X + 25, rect.Y + 14);
            g.DrawLine(pen, rect.X + 22, rect.Y + 20, rect.X + 22, rect.Y + 25);
        }
    }

    private void DrawUserIcon(Graphics g, Rectangle rect, Color color)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pen = new Pen(color, 1.5f))
        {
            g.DrawEllipse(pen, rect.X + 4, rect.Y + 2, 8, 8);
            g.DrawArc(pen, rect.X + 2, rect.Y + 8, 12, 8, 0, 180);
        }
    }

    private void DrawEmailIcon(Graphics g, Rectangle rect, Color color)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pen = new Pen(color, 1.5f))
        {
            g.DrawRectangle(pen, rect.X + 1, rect.Y + 4, 14, 10);
            g.DrawLine(pen, rect.X + 1, rect.Y + 4, rect.X + 8, rect.Y + 10);
            g.DrawLine(pen, rect.X + 8, rect.Y + 10, rect.X + 15, rect.Y + 4);
        }
    }

    private void DrawPersonIcon(Graphics g, Rectangle rect, Color color)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pen = new Pen(color, 1.5f))
        {
            g.DrawEllipse(pen, rect.X + 5, rect.Y + 1, 6, 6);
            g.DrawEllipse(pen, rect.X + 3, rect.Y + 8, 10, 7);
        }
    }

    private void DrawPhoneIcon(Graphics g, Rectangle rect, Color color)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pen = new Pen(color, 1.5f))
        {
            DrawRoundRectangle(g, pen, rect.X + 4, rect.Y + 1, 8, 14, 2);
            g.DrawLine(pen, rect.X + 6, rect.Y + 3, rect.X + 10, rect.Y + 3);
            g.FillEllipse(new SolidBrush(color), rect.X + 7, rect.Y + 12, 2, 2);
        }
    }

    private void DrawLockIcon(Graphics g, Rectangle rect, Color color)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pen = new Pen(color, 1.5f))
        {
            g.DrawRectangle(pen, rect.X + 3, rect.Y + 8, 10, 6);
            g.DrawArc(pen, rect.X + 5, rect.Y + 3, 6, 8, 180, 180);
            g.FillEllipse(new SolidBrush(color), rect.X + 7, rect.Y + 10, 2, 2);
        }
    }

    private void DrawGenderIcon(Graphics g, Rectangle rect, Color color)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pen = new Pen(color, 1.5f))
        {
            // Combined male/female symbol
            g.DrawEllipse(pen, rect.X + 3, rect.Y + 5, 6, 6);
            g.DrawLine(pen, rect.X + 9, rect.Y + 3, rect.X + 12, rect.Y + 1);
            g.DrawLine(pen, rect.X + 6, rect.Y + 11, rect.X + 6, rect.Y + 14);
            g.DrawLine(pen, rect.X + 4, rect.Y + 13, rect.X + 8, rect.Y + 13);
        }
    }

    private void DrawCalendarIcon(Graphics g, Rectangle rect, Color color)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pen = new Pen(color, 1.5f))
        {
            g.DrawRectangle(pen, rect.X + 2, rect.Y + 3, 12, 11);
            g.DrawLine(pen, rect.X + 2, rect.Y + 6, rect.X + 14, rect.Y + 6);
            g.DrawLine(pen, rect.X + 5, rect.Y + 1, rect.X + 5, rect.Y + 5);
            g.DrawLine(pen, rect.X + 11, rect.Y + 1, rect.X + 11, rect.Y + 5);
        }
    }

    private void DrawLocationIcon(Graphics g, Rectangle rect, Color color)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pen = new Pen(color, 1.5f))
        {
            g.DrawEllipse(pen, rect.X + 4, rect.Y + 2, 8, 8);
            g.DrawLine(pen, rect.X + 8, rect.Y + 10, rect.X + 8, rect.Y + 14);
            g.FillEllipse(new SolidBrush(color), rect.X + 7, rect.Y + 5, 2, 2);
        }
    }

    private void DrawRoundRectangle(Graphics graphics, Pen pen, int x, int y, int width, int height, int radius)
    {
        using (GraphicsPath path = new GraphicsPath())
        {
            path.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            path.AddArc(x + width - radius * 2, y, radius * 2, radius * 2, 270, 90);
            path.AddArc(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            graphics.DrawPath(pen, path);
        }
    }

    private void txtAddress_TextChanged(object sender, EventArgs e)
    {

    }

    private void picAppLogo_Click(object sender, EventArgs e)
    {

    }
}

