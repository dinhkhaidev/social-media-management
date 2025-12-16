using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace SocialManager.utils
{
    /// <summary>
    /// Quản lý cấu hình toàn cục của hệ thống
    /// </summary>
    public static class GlobalSettings
    {
        private static Dictionary<string, string> _settings;
        private const string SETTINGS_FILE = "appsettings.json";

        // Events để thông báo khi settings thay đổi
        public static event EventHandler<SettingsChangedEventArgs> SettingsChanged;

        static GlobalSettings()
        {
            LoadSettings();
        }

        #region Properties - Typed Access to Settings
        
        public static string SystemName 
        { 
            get => GetSetting("SystemName", "SolidVerse");
            set => SetSetting("SystemName", value);
        }

        public static string Theme 
        { 
            get => GetSetting("Theme", "Light");
            set 
            {
                SetSetting("Theme", value);
                ApplyTheme(value);
            }
        }

        public static int SessionTimeout 
        { 
            get => int.Parse(GetSetting("SessionTimeout", "30"));
            set => SetSetting("SessionTimeout", value.ToString());
        }

        public static bool MaintenanceMode 
        { 
            get => bool.Parse(GetSetting("MaintenanceMode", "False"));
            set 
            {
                SetSetting("MaintenanceMode", value.ToString());
                SaveSettings(); // Auto save when maintenance mode changes
            }
        }

        public static bool DarkMode
        {
            get => Theme?.ToLower() == "dark";
        }

        // Email Settings
        public static bool EmailEnabled 
        { 
            get => bool.Parse(GetSetting("EmailEnabled", "True"));
            set => SetSetting("EmailEnabled", value.ToString());
        }

        public static string SMTPServer 
        { 
            get => GetSetting("SMTPServer", "smtp.gmail.com");
            set => SetSetting("SMTPServer", value);
        }

        public static int SMTPPort 
        { 
            get => int.Parse(GetSetting("SMTPPort", "587"));
            set => SetSetting("SMTPPort", value.ToString());
        }

        public static string SMTPUsername 
        { 
            get => GetSetting("SMTPUsername", "");
            set => SetSetting("SMTPUsername", value);
        }

        public static string SMTPPassword 
        { 
            get => GetSetting("SMTPPassword", "");
            set => SetSetting("SMTPPassword", value);
        }

        public static bool SMTPSSL 
        { 
            get => bool.Parse(GetSetting("SMTPSSL", "True"));
            set => SetSetting("SMTPSSL", value.ToString());
        }

        // Backup Settings
        public static string BackupPath 
        { 
            get => GetSetting("BackupPath", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SocialManager_Backups"));
            set => SetSetting("BackupPath", value);
        }

        public static bool AutoBackup 
        { 
            get => bool.Parse(GetSetting("AutoBackup", "False"));
            set => SetSetting("AutoBackup", value.ToString());
        }

        public static int BackupInterval 
        { 
            get => int.Parse(GetSetting("BackupInterval", "30"));
            set => SetSetting("BackupInterval", value.ToString());
        }

        #endregion

        #region Core Methods

        private static string GetSetting(string key, string defaultValue)
        {
            if (_settings == null)
                LoadSettings();

            return _settings.ContainsKey(key) ? _settings[key] : defaultValue;
        }

        private static void SetSetting(string key, string value)
        {
            if (_settings == null)
                LoadSettings();

            var oldValue = _settings.ContainsKey(key) ? _settings[key] : null;
            _settings[key] = value;

            // Raise event
            SettingsChanged?.Invoke(null, new SettingsChangedEventArgs(key, oldValue, value));
        }

        public static void LoadSettings()
        {
            _settings = new Dictionary<string, string>();
            
            if (File.Exists(SETTINGS_FILE))
            {
                try
                {
                    var json = File.ReadAllText(SETTINGS_FILE);
                    _settings = JsonSerializer.Deserialize<Dictionary<string, string>>(json) 
                        ?? new Dictionary<string, string>();
                }
                catch
                {
                    // Use default settings if file is corrupted
                    _settings = new Dictionary<string, string>();
                }
            }
        }

        public static void SaveSettings()
        {
            try
            {
                var json = JsonSerializer.Serialize(_settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SETTINGS_FILE, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu cấu hình: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static Dictionary<string, string> GetAllSettings()
        {
            if (_settings == null)
                LoadSettings();
            
            return new Dictionary<string, string>(_settings);
        }

        public static void SetAllSettings(Dictionary<string, string> settings)
        {
            _settings = new Dictionary<string, string>(settings);
            SaveSettings();
            
            // Apply theme if changed
            if (settings.ContainsKey("Theme"))
            {
                ApplyTheme(settings["Theme"]);
            }
        }

        #endregion

        #region Apply Settings

        /// <summary>
        /// Áp dụng theme cho toàn bộ ứng dụng
        /// </summary>
        public static void ApplyTheme(string themeName)
        {
            Color backgroundColor, textColor, panelColor;

            switch (themeName)
            {
                case "Dark":
                    backgroundColor = Color.FromArgb(34, 40, 49);   // Dark background
                    textColor = Color.FromArgb(255, 255, 255);      // White text
                    panelColor = Color.FromArgb(44, 62, 80);        // Dark panels
                    break;

                case "Auto":
                case "Light":
                default:
                    backgroundColor = Color.FromArgb(247, 249, 252); // Light Background
                    textColor = Color.FromArgb(44, 62, 80);         // Dark Text
                    panelColor = Color.White;                       // White Panel
                    break;
            }

            // Apply to all open forms except login and register forms
            foreach (Form form in Application.OpenForms)
            {
                // Skip theme application for login and register forms to preserve their original design
                if (form.GetType().Name == "frmLogin" || form.GetType().Name == "frmRegister")
                    continue;
                
                // Skip sidebar completely to preserve navigation button colors
                if (form.GetType().Name == "frmAdmin")
                {
                    ApplyThemeToMainContentOnly(form, backgroundColor, textColor, panelColor);
                }
                else
                {
                    ApplyThemeToControl(form, backgroundColor, textColor, panelColor);
                }
                form.Refresh(); // Force refresh to apply changes
            }
        }

        private static void ApplyThemeToControl(Control control, Color backgroundColor, Color textColor, Color panelColor = default)
        {
            try
            {
                // Apply to the control based on type
                if (control is Form form)
                {
                    form.BackColor = backgroundColor;
                    form.ForeColor = textColor;
                }
                else if (control is UserControl uc)
                {
                    // UserControls should use background color
                    uc.BackColor = backgroundColor;
                    uc.ForeColor = textColor;
                }
                else if (control is Button btn)
                {
                    // Skip all navigation and styled buttons to preserve their original design
                    bool skipButton = btn.Name != null && (
                        btn.Name.StartsWith("btn") || 
                        btn.Parent?.Name == "pnlSidebar" ||
                        btn.FlatStyle == FlatStyle.Flat ||
                        btn.BackColor == Color.FromArgb(52, 152, 219) ||
                        btn.BackColor == Color.FromArgb(46, 204, 113) ||
                        btn.BackColor == Color.FromArgb(231, 76, 60) ||
                        btn.BackColor == Color.FromArgb(230, 126, 34)
                    );
                    
                    if (!skipButton)
                    {
                        // Only apply theme to basic buttons
                        btn.BackColor = backgroundColor;
                        btn.ForeColor = textColor;
                    }
                }
                else if (control is TextBox txt)
                {
                    txt.BackColor = backgroundColor == Color.FromArgb(34, 40, 49) 
                        ? Color.FromArgb(52, 73, 94)  // Dark theme
                        : Color.White;                 // Light theme
                    txt.ForeColor = textColor;
                }
                else if (control is ComboBox cmb)
                {
                    cmb.BackColor = backgroundColor == Color.FromArgb(34, 40, 49)
                        ? Color.FromArgb(52, 73, 94)  // Dark theme
                        : Color.White;                 // Light theme
                    cmb.ForeColor = textColor;
                }
                else if (control is TabPage tabPage)
                {
                    tabPage.BackColor = backgroundColor;
                    tabPage.ForeColor = textColor;
                }
                else if (control is Panel || control is GroupBox)
                {
                    control.BackColor = panelColor;
                    control.ForeColor = textColor;
                }
                else if (control is Label lbl)
                {
                    // Keep label background transparent but change text color
                    if (lbl.BackColor == Color.Transparent || lbl.BackColor.A < 255)
                    {
                        lbl.ForeColor = textColor;
                    }
                    else
                    {
                        // For labels with background, use appropriate color
                        if (backgroundColor == Color.FromArgb(34, 40, 49)) // Dark theme
                        {
                            lbl.BackColor = Color.FromArgb(44, 62, 80);
                        }
                        else
                        {
                            lbl.BackColor = Color.White;
                        }
                        lbl.ForeColor = textColor;
                    }
                }
                else if (control is DataGridView dgv)
                {
                    dgv.BackgroundColor = backgroundColor;
                    dgv.DefaultCellStyle.BackColor = backgroundColor == Color.FromArgb(34, 40, 49)
                        ? Color.FromArgb(52, 73, 94)  // Dark theme
                        : Color.White;                 // Light theme
                    dgv.DefaultCellStyle.ForeColor = textColor;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = panelColor;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = textColor;
                }
                else if (control is TabControl tabControl)
                {
                    if (backgroundColor == Color.FromArgb(34, 40, 49)) // Dark theme
                    {
                        tabControl.BackColor = Color.FromArgb(44, 62, 80);
                        tabControl.ForeColor = Color.White;
                        tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
                        tabControl.DrawItem -= TabControl_DrawItem;
                        tabControl.DrawItem += TabControl_DrawItem;
                    }
                    else // Light theme - keep original styling
                    {
                        tabControl.DrawMode = TabDrawMode.Normal;
                        tabControl.DrawItem -= TabControl_DrawItem;
                        // Don't change colors for light theme to preserve original appearance
                    }
                }
                else
                {
                    // For all other controls
                    if (backgroundColor == Color.FromArgb(34, 40, 49)) // Dark theme
                    {
                        control.BackColor = Color.FromArgb(44, 62, 80);
                    }
                    else
                    {
                        control.BackColor = backgroundColor;
                    }
                    control.ForeColor = textColor;
                }

                // Recursively apply to child controls, but skip sidebar
                foreach (Control child in control.Controls)
                {
                    // Skip sidebar panel completely
                    if (child.Name == "pnlSidebar")
                        continue;
                        
                    ApplyThemeToControl(child, backgroundColor, textColor, panelColor);
                }
            }
            catch
            {
                // Ignore errors for controls that don't support color changes
            }
        }

        /// <summary>
        /// Cập nhật tiêu đề của tất cả các form
        /// </summary>
        public static void UpdateSystemName(string newName)
        {
            SystemName = newName;
            
            foreach (Form form in Application.OpenForms)
            {
                if (form.Text.Contains("Social Media Management") || 
                    form.Text.Contains("Admin") || 
                    form.Text.StartsWith("Dashboard"))
                {
                    form.Text = $"{newName} - Admin Panel";
                }
            }
        }

        /// <summary>
        /// Apply theme to a specific UserControl
        /// </summary>
        public static void ApplyThemeToUserControl(UserControl userControl)
        {
            Color backgroundColor, textColor, panelColor;
            string currentTheme = Theme;

            switch (currentTheme)
            {
                case "Dark":
                    backgroundColor = Color.FromArgb(34, 40, 49);
                    textColor = Color.FromArgb(255, 255, 255);
                    panelColor = Color.FromArgb(44, 62, 80);
                    break;

                case "Auto":
                case "Light":
                default:
                    backgroundColor = Color.FromArgb(247, 249, 252);
                    textColor = Color.FromArgb(44, 62, 80);
                    panelColor = Color.White;
                    break;
            }

            ApplyThemeToControl(userControl, backgroundColor, textColor, panelColor);
            userControl.Refresh();
        }

        /// <summary>
        /// Apply theme only to main content area, skip sidebar
        /// </summary>
        private static void ApplyThemeToMainContentOnly(Form form, Color backgroundColor, Color textColor, Color panelColor)
        {
            // Find the main panel (pnlMain) and apply theme only to it
            foreach (Control control in form.Controls)
            {
                if (control.Name == "pnlMain")
                {
                    ApplyThemeToControl(control, backgroundColor, textColor, panelColor);
                    break;
                }
            }
        }

        /// <summary>
        /// Apply theme to all UserControls in the form
        /// </summary>
        private static void ApplyThemeToAllUserControls(Form form, Color backgroundColor, Color textColor, Color panelColor)
        {
            ApplyThemeToUserControlsRecursive(form, backgroundColor, textColor, panelColor);
        }

        private static void ApplyThemeToUserControlsRecursive(Control parent, Color backgroundColor, Color textColor, Color panelColor)
        {
            foreach (Control control in parent.Controls)
            {
                // Skip sidebar panel
                if (control.Name == "pnlSidebar")
                    continue;
                    
                if (control is UserControl uc)
                {
                    // Use the same method as individual UserControl theme application
                    ApplyThemeToUserControl(uc);
                }
                else
                {
                    // Recursively check child controls
                    ApplyThemeToUserControlsRecursive(control, backgroundColor, textColor, panelColor);
                }
            }
        }

        /// <summary>
        /// Custom draw handler for TabControl in dark theme
        /// </summary>
        private static void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabControl = sender as TabControl;
            if (tabControl == null) return;

            var tabPage = tabControl.TabPages[e.Index];
            var tabRect = tabControl.GetTabRect(e.Index);
            
            // Dark theme colors
            var defaultBackColor = Color.FromArgb(44, 62, 80);     // Default tab
            var selectedBackColor = Color.FromArgb(52, 152, 219);  // Selected tab
            var hoverBackColor = Color.FromArgb(52, 73, 94);       // Hover tab
            var textColor = Color.White;                           // Text color
            
            bool isSelected = (e.Index == tabControl.SelectedIndex);
            bool isHovered = tabRect.Contains(tabControl.PointToClient(Cursor.Position));
            
            Color backColor;
            if (isSelected)
                backColor = selectedBackColor;
            else if (isHovered)
                backColor = hoverBackColor;
            else
                backColor = defaultBackColor;
            
            // Draw background
            using (var brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, tabRect);
            }
            
            // Draw text
            using (var textBrush = new SolidBrush(textColor))
            {
                var format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                e.Graphics.DrawString(tabPage.Text, tabControl.Font, textBrush, tabRect, format);
            }
        }

        #endregion
    }

    /// <summary>
    /// Event args cho sự kiện settings thay đổi
    /// </summary>
    public class SettingsChangedEventArgs : EventArgs
    {
        public string Key { get; }
        public string OldValue { get; }
        public string NewValue { get; }

        public SettingsChangedEventArgs(string key, string oldValue, string newValue)
        {
            Key = key;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
