using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using System.Text.Json;
using SocialManager.utils;

namespace SocialManager.frm.UserControls
{
    public partial class ucSettings : UserControl
    {
        // UI Components - Main
        private Panel mainPanel;
        private Panel topControlPanel;
        private TabControl tabSettings;
        private Button btnSaveAll;
        private Button btnResetAll;
        private Button btnExportConfig;
        private Button btnImportConfig;

        // Tab Pages
        private TabPage tabGeneral;
        private TabPage tabNotifications;
        private TabPage tabBackup;

        // General Settings
        private TextBox txtSystemName;
        private ComboBox cmbTheme;
        private NumericUpDown nudSessionTimeout;
        private CheckBox chkMaintenanceMode;

        // Notification Settings
        private CheckBox chkEmailEnabled;
        private TextBox txtSMTPServer;
        private NumericUpDown nudSMTPPort;
        private TextBox txtSMTPUsername;
        private TextBox txtSMTPPassword;
        private CheckBox chkSMTPSSL;
        private Button btnTestEmail;

        // Backup Settings
        private TextBox txtBackupPath;
        private CheckBox chkAutoBackup;
        private NumericUpDown nudBackupInterval;
        private ListBox lstBackupHistory;
        private Button btnBackupNow;
        private Button btnRestoreBackup;

        private Dictionary<string, string> currentSettings;
        private const string SETTINGS_FILE = "appsettings.json";
        private bool _isUpdatingUI = false; // Prevent event loops

        public ucSettings()
        {
            InitializeComponent();
            InitializeCustomUI();
            LoadSettings();
            LoadSettingsToUI(); // Load saved settings into UI controls
            
            // Subscribe to global settings changes
            GlobalSettings.SettingsChanged += OnGlobalSettingsChanged;
            
            // Apply theme after layout is created
            this.Load += (s, e) => GlobalSettings.ApplyThemeToUserControl(this);
        }
        
        private void OnGlobalSettingsChanged(object sender, SettingsChangedEventArgs e)
        {
            // Reload UI when settings change from other sources
            LoadSettingsToUI();
        }

        private void InitializeCustomUI()
        {
            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(0)
            };

            topControlPanel = CreateTopControlPanel();
            tabSettings = CreateSettingsTabs();
            tabSettings.Margin = new Padding(0, 85, 0, 0);

            mainPanel.Controls.Add(tabSettings);
            mainPanel.Controls.Add(topControlPanel);

            this.Controls.Add(mainPanel);
            
            // Set size
            this.Size = new Size(1200, 800);
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.AutoScroll = true;
        }

        private Panel CreateTopControlPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            var lblTitle = new Label
            {
                Text = "⚙️ Thiết lập Hệ thống",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 98, 255),
                AutoSize = true,
                Location = new Point(15, 15)
            };

            var lblSubtitle = new Label
            {
                Text = "Cấu hình và quản lý hệ thống",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(15, 45)
            };

            btnSaveAll = CreateButton("💾 Lưu tất cả", new Point(850, 15), Color.FromArgb(67, 160, 71));
            btnSaveAll.Click += BtnSaveAll_Click;

            btnResetAll = CreateButton("🔄 Khôi phục", new Point(970, 15), Color.FromArgb(255, 152, 0));
            btnResetAll.Click += BtnResetAll_Click;

            btnExportConfig = CreateButton("📤 Export", new Point(850, 45), Color.FromArgb(41, 98, 255));
            btnExportConfig.Click += BtnExportConfig_Click;

            btnImportConfig = CreateButton("📥 Import", new Point(970, 45), Color.FromArgb(156, 39, 176));
            btnImportConfig.Click += BtnImportConfig_Click;

            panel.Controls.AddRange(new Control[] {
                lblTitle, lblSubtitle,
                btnSaveAll, btnResetAll, btnExportConfig, btnImportConfig
            });

            return panel;
        }
        
        /// <summary>
        /// Event handler for theme change - apply immediately
        /// </summary>
        private void CmbTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTheme.SelectedItem == null) return;
            
            var selectedTheme = cmbTheme.SelectedItem.ToString();
            if (!string.IsNullOrWhiteSpace(selectedTheme))
            {
                // Apply theme immediately without saving
                GlobalSettings.ApplyTheme(selectedTheme);
            }
        }
        
        private void ChkMaintenanceMode_CheckedChanged(object sender, EventArgs e)
        {
            if (_isUpdatingUI) return; // Skip if we're updating UI programmatically
            
            if (chkMaintenanceMode.Checked)
            {
                var result = MessageBox.Show(
                    "🔧 Chế độ bảo trì sẽ được bật!\n\n" +
                    "- Người dùng không thể đăng nhập\n" +
                    "- Chỉ admin có thể truy cập\n" +
                    "- Chỉ admin có thể tắt chế độ này\n\n" +
                    "Bạn có chắc chắn muốn tiếp tục?",
                    "Xác nhận chế độ bảo trì",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                
                if (result == DialogResult.No)
                {
                    chkMaintenanceMode.Checked = false;
                    return;
                }
                
                GlobalSettings.MaintenanceMode = true;
                MessageBox.Show(
                    "✅ Chế độ bảo trì đã được kích hoạt!\n\n" +
                    "Hệ thống hiện đang ở chế độ bảo trì.",
                    "Chế độ bảo trì",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                GlobalSettings.MaintenanceMode = false;
                MessageBox.Show(
                    "✅ Chế độ bảo trì đã được tắt!\n\n" +
                    "Người dùng có thể đăng nhập bình thường.",
                    "Chế độ bảo trì",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private Button CreateButton(string text, Point location, Color backColor)
        {
            return new Button
            {
                Text = text,
                Location = location,
                Size = new Size(110, 30),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private Button CreateButton(string text, int x, int y)
        {
            return CreateButton(text, new Point(x, y), Color.FromArgb(52, 152, 219));
        }

        private TabControl CreateSettingsTabs()
        {
            var tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Padding = new Point(5, 5),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            tabGeneral = new TabPage("🏠 Cấu hình Chung")
            {
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(10),
                AutoScroll = true
            };
            InitializeGeneralTab();

            tabNotifications = new TabPage("📧 Thông báo & Email")
            {
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(10),
                AutoScroll = true
            };
            InitializeNotificationsTab();

            tabBackup = new TabPage("💾 Backup & Bảo trì")
            {
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(10),
                AutoScroll = true
            };
            InitializeBackupTab();

            tabControl.TabPages.AddRange(new TabPage[] {
                tabGeneral, tabNotifications, tabBackup
            });

            return tabControl;
        }

        #region Tab Initialization - See continuation below
        // Tab initialization methods will be added
        #endregion

        #region Helper Methods
        private Panel CreateSectionPanel(string title, int yPosition)
        {
            var panel = new Panel
            {
                Location = new Point(10, yPosition),
                Size = new Size(700, 200),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblTitle = new Label
            {
                Text = title,
                Location = new Point(15, 10),
                Size = new Size(670, 30),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 98, 255)
            };

            panel.Controls.Add(lblTitle);
            return panel;
        }
        #endregion

        #region Settings Management
        private void LoadSettings()
        {
            // Load from GlobalSettings
            currentSettings = GlobalSettings.GetAllSettings();
        }

        private void SaveSettings()
        {
            try
            {
                // Collect all settings from UI controls
                currentSettings["SystemName"] = txtSystemName?.Text ?? "Social Media Management";
                currentSettings["Theme"] = cmbTheme?.SelectedItem?.ToString() ?? "Light";
                currentSettings["SessionTimeout"] = nudSessionTimeout?.Value.ToString() ?? "30";
                currentSettings["MaintenanceMode"] = chkMaintenanceMode?.Checked.ToString() ?? "False";
                
                // Email settings
                currentSettings["EmailEnabled"] = chkEmailEnabled?.Checked.ToString() ?? "True";
                currentSettings["SMTPServer"] = txtSMTPServer?.Text ?? "smtp.gmail.com";
                currentSettings["SMTPPort"] = nudSMTPPort?.Value.ToString() ?? "587";
                currentSettings["SMTPUsername"] = txtSMTPUsername?.Text ?? "";
                currentSettings["SMTPPassword"] = txtSMTPPassword?.Text ?? "";
                currentSettings["SMTPSSL"] = chkSMTPSSL?.Checked.ToString() ?? "True";
                
                // Backup settings
                currentSettings["BackupPath"] = txtBackupPath?.Text ?? "";
                currentSettings["AutoBackup"] = chkAutoBackup?.Checked.ToString() ?? "False";
                currentSettings["BackupInterval"] = nudBackupInterval?.Value.ToString() ?? "30";
                
                // Save to GlobalSettings (this will trigger events and apply changes)
                GlobalSettings.SetAllSettings(currentSettings);

                MessageBox.Show("✅ Đã lưu và áp dụng cấu hình thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                // Apply specific changes immediately
                ApplySettingsChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Áp dụng các thay đổi settings ngay lập tức
        /// </summary>
        private void ApplySettingsChanges()
        {
            // Apply system name to all forms
            if (!string.IsNullOrWhiteSpace(txtSystemName.Text))
            {
                GlobalSettings.UpdateSystemName(txtSystemName.Text);
            }
            
            // Apply theme
            var selectedTheme = cmbTheme.SelectedItem?.ToString();
            if (!string.IsNullOrWhiteSpace(selectedTheme))
            {
                GlobalSettings.ApplyTheme(selectedTheme);
            }
        }
        
        private void LoadSettingsToUI()
        {
            try
            {
                _isUpdatingUI = true; // Prevent event loops
                
                // Load directly from GlobalSettings to get latest values
                txtSystemName.Text = GlobalSettings.SystemName;
                
                // Load theme
                var theme = GlobalSettings.Theme;
                for (int i = 0; i < cmbTheme.Items.Count; i++)
                {
                    if (cmbTheme.Items[i].ToString() == theme)
                    {
                        cmbTheme.SelectedIndex = i;
                        break;
                    }
                }
                
                nudSessionTimeout.Value = GlobalSettings.SessionTimeout;
                chkMaintenanceMode.Checked = GlobalSettings.MaintenanceMode;
                    
                // Load email settings from GlobalSettings
                chkEmailEnabled.Checked = GlobalSettings.EmailEnabled;
                txtSMTPServer.Text = GlobalSettings.SMTPServer;
                nudSMTPPort.Value = GlobalSettings.SMTPPort;
                txtSMTPUsername.Text = GlobalSettings.SMTPUsername;
                txtSMTPPassword.Text = GlobalSettings.SMTPPassword;
                chkSMTPSSL.Checked = GlobalSettings.SMTPSSL;
                    
                // Load backup settings from GlobalSettings
                txtBackupPath.Text = GlobalSettings.BackupPath;
                chkAutoBackup.Checked = GlobalSettings.AutoBackup;
                nudBackupInterval.Value = GlobalSettings.BackupInterval;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải cấu hình: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isUpdatingUI = false; // Re-enable events
            }
        }
        #endregion

        #region Event Handlers
        private void BtnSaveAll_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Lưu tất cả cấu hình?\n\nCác thay đổi sẽ áp dụng ngay lập tức.", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) 
            {
                SaveSettings();
                LoadSettingsToUI(); // Refresh UI
            }
        }

        private void BtnResetAll_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Khôi phục cấu hình mặc định?\n\n⚠️ Tất cả thay đổi hiện tại sẽ bị mất!", "Cảnh báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                // Reset to default settings
                currentSettings.Clear();
                currentSettings["SystemName"] = "Social Media Management";
                currentSettings["Theme"] = "Light";
                currentSettings["SessionTimeout"] = "30";
                currentSettings["MaintenanceMode"] = "False";
                currentSettings["EmailEnabled"] = "True";
                currentSettings["SMTPServer"] = "smtp.gmail.com";
                currentSettings["SMTPPort"] = "587";
                currentSettings["SMTPSSL"] = "True";
                currentSettings["AutoBackup"] = "False";
                currentSettings["BackupInterval"] = "30";
                currentSettings["BackupPath"] = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SocialManager_Backups");
                
                LoadSettingsToUI();
                SaveSettings();
                MessageBox.Show("Đã khôi phục cấu hình mặc định!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnExportConfig_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                FileName = $"SocialManager_Config_{DateTime.Now:yyyyMMdd_HHmmss}.json",
                Title = "Export Configuration"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Create export data with metadata
                    var exportData = new Dictionary<string, object>
                    {
                        ["ExportDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        ["Version"] = "1.0",
                        ["Settings"] = currentSettings
                    };
                    
                    var json = JsonSerializer.Serialize(exportData, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(saveDialog.FileName, json);
                    
                    MessageBox.Show($"✅ Đã export cấu hình thành công!\n\nFile: {Path.GetFileName(saveDialog.FileName)}", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Lỗi khi export: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnImportConfig_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog 
            { 
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                Title = "Import Configuration"
            };
            
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var json = File.ReadAllText(openDialog.FileName);
                    
                    // Try to parse as export format first
                    try
                    {
                        var exportData = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                        if (exportData != null && exportData.ContainsKey("Settings"))
                        {
                            var settingsJson = exportData["Settings"].ToString();
                            currentSettings = JsonSerializer.Deserialize<Dictionary<string, string>>(settingsJson ?? "{}") 
                                ?? new Dictionary<string, string>();
                        }
                        else
                        {
                            // Fallback to direct settings format
                            currentSettings = JsonSerializer.Deserialize<Dictionary<string, string>>(json) 
                                ?? new Dictionary<string, string>();
                        }
                    }
                    catch
                    {
                        // Fallback to direct settings format
                        currentSettings = JsonSerializer.Deserialize<Dictionary<string, string>>(json) 
                            ?? new Dictionary<string, string>();
                    }
                    
                    LoadSettingsToUI();
                    SaveSettings();
                    
                    MessageBox.Show($"✅ Đã import cấu hình thành công!\n\nFile: {Path.GetFileName(openDialog.FileName)}", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Lỗi khi import: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        // Placeholder methods for tab initialization - will be implemented below
        private void InitializeGeneralTab() 
        {
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20)
            };

            var yPosition = 10;

            // System Name
            var lblSystemName = new Label
            {
                Text = "Tên hệ thống:",
                Location = new Point(20, yPosition),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtSystemName = new TextBox
            {
                Location = new Point(200, yPosition),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 10),
                Text = "SolidVerse"
            };
            mainPanel.Controls.Add(lblSystemName);
            mainPanel.Controls.Add(txtSystemName);
            yPosition += 45;

            // Theme
            var lblTheme = new Label
            {
                Text = "Giao diện:",
                Location = new Point(20, yPosition),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            cmbTheme = new ComboBox
            {
                Location = new Point(200, yPosition),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTheme.Items.AddRange(new object[] { "Light", "Dark", "Auto" });
            cmbTheme.SelectedIndex = 0;
            cmbTheme.SelectedIndexChanged += CmbTheme_SelectedIndexChanged; // Apply theme immediately on change
            mainPanel.Controls.Add(lblTheme);
            mainPanel.Controls.Add(cmbTheme);
            yPosition += 45;

            // Session Timeout
            var lblTimeout = new Label
            {
                Text = "Thời gian timeout (phút):",
                Location = new Point(20, yPosition),
                Size = new Size(180, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            nudSessionTimeout = new NumericUpDown
            {
                Location = new Point(200, yPosition),
                Size = new Size(150, 30),
                Font = new Font("Segoe UI", 10),
                Minimum = 5,
                Maximum = 240,
                Value = 30
            };
            mainPanel.Controls.Add(lblTimeout);
            mainPanel.Controls.Add(nudSessionTimeout);
            yPosition += 45;

            // Maintenance Mode
            chkMaintenanceMode = new CheckBox
            {
                Text = "Bật chế độ bảo trì",
                Location = new Point(20, yPosition),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(231, 76, 60)
            };
            chkMaintenanceMode.CheckedChanged += ChkMaintenanceMode_CheckedChanged;
            mainPanel.Controls.Add(chkMaintenanceMode);

            tabGeneral.Controls.Add(mainPanel);
        }

        private void InitializeNotificationsTab() 
        {
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20)
            };

            var yPosition = 10;

            // Email Enabled
            chkEmailEnabled = new CheckBox
            {
                Text = "Bật gửi Email",
                Location = new Point(20, yPosition),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Checked = true
            };
            mainPanel.Controls.Add(chkEmailEnabled);
            yPosition += 45;

            // SMTP Server
            var lblSMTP = new Label
            {
                Text = "SMTP Server:",
                Location = new Point(20, yPosition),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtSMTPServer = new TextBox
            {
                Location = new Point(200, yPosition),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 10),
                Text = "smtp.gmail.com"
            };
            mainPanel.Controls.Add(lblSMTP);
            mainPanel.Controls.Add(txtSMTPServer);
            yPosition += 45;

            // SMTP Port
            var lblPort = new Label
            {
                Text = "Port:",
                Location = new Point(20, yPosition),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            nudSMTPPort = new NumericUpDown
            {
                Location = new Point(200, yPosition),
                Size = new Size(150, 30),
                Font = new Font("Segoe UI", 10),
                Minimum = 1,
                Maximum = 65535,
                Value = 587
            };
            mainPanel.Controls.Add(lblPort);
            mainPanel.Controls.Add(nudSMTPPort);
            yPosition += 45;

            // Username
            var lblUsername = new Label
            {
                Text = "Username:",
                Location = new Point(20, yPosition),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtSMTPUsername = new TextBox
            {
                Location = new Point(200, yPosition),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 10),
                PlaceholderText = "your-email@gmail.com"
            };
            mainPanel.Controls.Add(lblUsername);
            mainPanel.Controls.Add(txtSMTPUsername);
            yPosition += 45;

            // Password
            var lblPassword = new Label
            {
                Text = "Password:",
                Location = new Point(20, yPosition),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtSMTPPassword = new TextBox
            {
                Location = new Point(200, yPosition),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 10),
                UseSystemPasswordChar = true
            };
            mainPanel.Controls.Add(lblPassword);
            mainPanel.Controls.Add(txtSMTPPassword);
            yPosition += 45;

            // SSL
            chkSMTPSSL = new CheckBox
            {
                Text = "Sử dụng SSL/TLS",
                Location = new Point(20, yPosition),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 10),
                Checked = true
            };
            mainPanel.Controls.Add(chkSMTPSSL);
            yPosition += 45;

            // Test Email Button
            btnTestEmail = CreateButton("📧 Test gửi Email", 20, yPosition);
            btnTestEmail.Click += BtnTestEmail_Click;
            mainPanel.Controls.Add(btnTestEmail);

            tabNotifications.Controls.Add(mainPanel);
        }

        private void InitializeBackupTab() 
        {
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20)
            };

            var yPosition = 10;

            // Backup Path
            var lblPath = new Label
            {
                Text = "Đường dẫn backup:",
                Location = new Point(20, yPosition),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtBackupPath = new TextBox
            {
                Location = new Point(200, yPosition),
                Size = new Size(350, 30),
                Font = new Font("Segoe UI", 10),
                Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SocialManager_Backups")
            };
            var btnBrowse = CreateButton("📁", 560, yPosition);
            btnBrowse.Size = new Size(40, 30);
            btnBrowse.Click += (s, e) =>
            {
                var folderDialog = new FolderBrowserDialog();
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtBackupPath.Text = folderDialog.SelectedPath;
                }
            };
            mainPanel.Controls.Add(lblPath);
            mainPanel.Controls.Add(txtBackupPath);
            mainPanel.Controls.Add(btnBrowse);
            yPosition += 45;

            // Auto Backup
            chkAutoBackup = new CheckBox
            {
                Text = "Tự động backup hàng ngày",
                Location = new Point(20, yPosition),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            mainPanel.Controls.Add(chkAutoBackup);
            yPosition += 45;

            // Backup Interval
            var lblInterval = new Label
            {
                Text = "Giữ lại (ngày):",
                Location = new Point(20, yPosition),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            nudBackupInterval = new NumericUpDown
            {
                Location = new Point(200, yPosition),
                Size = new Size(150, 30),
                Font = new Font("Segoe UI", 10),
                Minimum = 1,
                Maximum = 365,
                Value = 30
            };
            mainPanel.Controls.Add(lblInterval);
            mainPanel.Controls.Add(nudBackupInterval);
            yPosition += 45;

            // Action Buttons
            btnBackupNow = CreateButton("💾 Backup ngay", 20, yPosition);
            btnBackupNow.Click += BtnBackupNow_Click;
            mainPanel.Controls.Add(btnBackupNow);

            btnRestoreBackup = CreateButton("♻️ Khôi phục", 220, yPosition);
            btnRestoreBackup.Click += BtnRestoreBackup_Click;
            mainPanel.Controls.Add(btnRestoreBackup);
            yPosition += 55;

            // Backup History
            var lblHistory = new Label
            {
                Text = "📋 Lịch sử Backup:",
                Location = new Point(20, yPosition),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            mainPanel.Controls.Add(lblHistory);
            yPosition += 35;

            lstBackupHistory = new ListBox
            {
                Location = new Point(20, yPosition),
                Size = new Size(580, 200),
                Font = new Font("Consolas", 9)
            };
            mainPanel.Controls.Add(lstBackupHistory);
            
            // Load backup history
            LoadBackupHistory();

            tabBackup.Controls.Add(mainPanel);
        }

        private void BtnTestEmail_Click(object sender, EventArgs e)
        {
            if (!chkEmailEnabled.Checked)
            {
                MessageBox.Show("⚠️ Chức năng Email đang bị tắt!\n\nVui lòng bật 'Bật gửi Email' trước.", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtSMTPServer.Text))
            {
                MessageBox.Show("❌ Vui lòng nhập SMTP Server!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txtSMTPUsername.Text))
            {
                MessageBox.Show("❌ Vui lòng nhập Username!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txtSMTPPassword.Text))
            {
                MessageBox.Show("❌ Vui lòng nhập Password!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ask for test email recipient
            var inputDialog = new Form
            {
                Text = "Email người nhận",
                Size = new Size(400, 150),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };
            
            var lblPrompt = new Label
            {
                Text = "Nhập email để nhận thử:",
                Location = new Point(20, 20),
                Size = new Size(350, 20),
                Font = new Font("Segoe UI", 10)
            };
            
            var txtEmail = new TextBox
            {
                Location = new Point(20, 50),
                Size = new Size(350, 30),
                Font = new Font("Segoe UI", 10),
                Text = txtSMTPUsername.Text
            };
            
            var btnOK = new Button
            {
                Text = "Gửi",
                DialogResult = DialogResult.OK,
                Location = new Point(190, 85),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            
            var btnCancel = new Button
            {
                Text = "Hủy",
                DialogResult = DialogResult.Cancel,
                Location = new Point(280, 85),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            
            inputDialog.Controls.AddRange(new Control[] { lblPrompt, txtEmail, btnOK, btnCancel });
            inputDialog.AcceptButton = btnOK;
            inputDialog.CancelButton = btnCancel;
            
            if (inputDialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(txtEmail.Text))
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                
                using (var client = new SmtpClient(txtSMTPServer.Text, (int)nudSMTPPort.Value))
                {
                    client.EnableSsl = chkSMTPSSL.Checked;
                    client.Credentials = new NetworkCredential(txtSMTPUsername.Text, txtSMTPPassword.Text);
                    client.Timeout = 10000; // 10 seconds

                    var message = new MailMessage
                    {
                        From = new MailAddress(txtSMTPUsername.Text, currentSettings.ContainsKey("SystemName") ? currentSettings["SystemName"] : "Social Media Management"),
                        Subject = "🧪 Test Email - Social Media Management System",
                        Body = $@"
<html>
<body style='font-family: Arial, sans-serif;'>
    <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f5f5f5;'>
        <div style='background-color: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 5px rgba(0,0,0,0.1);'>
            <h2 style='color: #3498db; margin-top: 0;'>✅ Email Configuration Test</h2>
            <p style='font-size: 16px; color: #333;'>Xin chào,</p>
            <p style='font-size: 14px; color: #666; line-height: 1.6;'>
                Đây là email test từ hệ thống <strong>Social Media Management</strong>.<br>
                Nếu bạn nhận được email này, cấu hình SMTP đã hoạt động chính xác!
            </p>
            <div style='background-color: #ecf0f1; padding: 15px; border-left: 4px solid #3498db; margin: 20px 0;'>
                <p style='margin: 5px 0; color: #555;'><strong>SMTP Server:</strong> {txtSMTPServer.Text}</p>
                <p style='margin: 5px 0; color: #555;'><strong>Port:</strong> {nudSMTPPort.Value}</p>
                <p style='margin: 5px 0; color: #555;'><strong>SSL/TLS:</strong> {(chkSMTPSSL.Checked ? "Enabled" : "Disabled")}</p>
                <p style='margin: 5px 0; color: #555;'><strong>Test Time:</strong> {DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>
            </div>
            <p style='font-size: 12px; color: #999; margin-top: 30px; border-top: 1px solid #ddd; padding-top: 15px;'>
                Đây là email tự động. Vui lòng không trả lời email này.
            </p>
        </div>
    </div>
</body>
</html>",
                        IsBodyHtml = true
                    };
                    
                    message.To.Add(txtEmail.Text);
                    
                    client.Send(message);
                    
                    Cursor = Cursors.Default;
                    MessageBox.Show($"✅ Đã gửi email test thành công!\n\n📧 Người nhận: {txtEmail.Text}\n\nVui lòng kiểm tra hộp thư (kể cả Spam/Junk).", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SmtpException smtpEx)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"❌ Lỗi SMTP:\n\n{smtpEx.Message}\n\n💡 Gợi ý:\n" +
                    "- Kiểm tra SMTP Server và Port\n" +
                    "- Kiểm tra Username/Password\n" +
                    "- Bật 'Less secure app access' (Gmail)\n" +
                    "- Sử dụng App Password thay vì mật khẩu thường", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"❌ Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBackupNow_Click(object sender, EventArgs e)
        {
            try
            {
                var backupPath = txtBackupPath.Text;
                
                if (string.IsNullOrWhiteSpace(backupPath))
                {
                    MessageBox.Show("❌ Vui lòng chọn đường dẫn backup!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Create backup directory if not exists
                if (!Directory.Exists(backupPath))
                {
                    Directory.CreateDirectory(backupPath);
                }
                
                var backupFileName = $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.zip";
                var backupFullPath = Path.Combine(backupPath, backupFileName);
                
                Cursor = Cursors.WaitCursor;
                
                // Source directory (data files)
                var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datas");
                
                if (!Directory.Exists(dataPath))
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("❌ Không tìm thấy thư mục dữ liệu!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Create zip file
                ZipFile.CreateFromDirectory(dataPath, backupFullPath, CompressionLevel.Optimal, false);
                
                // Get file size
                var fileInfo = new FileInfo(backupFullPath);
                var fileSizeMB = fileInfo.Length / (1024.0 * 1024.0);
                
                // Update backup history
                LoadBackupHistory();
                
                Cursor = Cursors.Default;
                MessageBox.Show($"✅ Đã tạo backup thành công!\n\n" +
                    $"📁 File: {backupFileName}\n" +
                    $"📊 Kích thước: {fileSizeMB:F2} MB\n" +
                    $"📂 Đường dẫn: {backupPath}", 
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"❌ Lỗi khi tạo backup:\n\n{ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRestoreBackup_Click(object sender, EventArgs e)
        {
            if (lstBackupHistory.SelectedItem == null)
            {
                MessageBox.Show("❌ Vui lòng chọn file backup cần khôi phục!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedItem = lstBackupHistory.SelectedItem.ToString();
            var fileName = selectedItem.Split(new[] { " - " }, StringSplitOptions.None)[1].Split(' ')[0];
            var backupFilePath = Path.Combine(txtBackupPath.Text, fileName);
            
            if (!File.Exists(backupFilePath))
            {
                MessageBox.Show($"❌ Không tìm thấy file backup:\n\n{backupFilePath}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var result = MessageBox.Show(
                $"⚠️ BẠN CÓ CHẮC MUỐN KHÔI PHỤC DỮ LIỆU?\n\n" +
                $"File: {fileName}\n\n" +
                $"❗ Cảnh báo:\n" +
                $"- Tất cả dữ liệu hiện tại sẽ bị GHI ĐÈ!\n" +
                $"- Thao tác này KHÔNG THỂ HOÀN TÁC!\n" +
                $"- Nên tạo backup hiện tại trước khi restore!\n\n" +
                $"Tiếp tục?", 
                "⚠️ XÁC NHẬN KHÔI PHỤC", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Warning);
            
            if (result != DialogResult.Yes)
                return;
            
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // Restore directory
                var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datas");
                var tempRestorePath = Path.Combine(Path.GetTempPath(), $"restore_{DateTime.Now:yyyyMMddHHmmss}");
                
                // Extract to temp directory first
                ZipFile.ExtractToDirectory(backupFilePath, tempRestorePath);
                
                // Backup current data before restore
                var currentBackupPath = Path.Combine(txtBackupPath.Text, $"before_restore_{DateTime.Now:yyyyMMdd_HHmmss}.zip");
                if (Directory.Exists(dataPath))
                {
                    ZipFile.CreateFromDirectory(dataPath, currentBackupPath, CompressionLevel.Optimal, false);
                }
                
                // Clear current data directory
                if (Directory.Exists(dataPath))
                {
                    Directory.Delete(dataPath, true);
                }
                
                // Move restored data to data directory
                Directory.Move(tempRestorePath, dataPath);
                
                Cursor = Cursors.Default;
                
                MessageBox.Show($"✅ Đã khôi phục dữ liệu thành công!\n\n" +
                    $"📁 File: {fileName}\n\n" +
                    $"💾 Dữ liệu cũ đã được backup tại:\n{Path.GetFileName(currentBackupPath)}\n\n" +
                    $"⚠️ Vui lòng KHỞI ĐỘNG LẠI ứng dụng để áp dụng thay đổi!", 
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"❌ Lỗi khi khôi phục:\n\n{ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadBackupHistory()
        {
            try
            {
                lstBackupHistory.Items.Clear();
                
                var backupPath = txtBackupPath.Text;
                if (string.IsNullOrWhiteSpace(backupPath) || !Directory.Exists(backupPath))
                    return;
                
                var backupFiles = Directory.GetFiles(backupPath, "*.zip")
                    .OrderByDescending(f => new FileInfo(f).CreationTime)
                    .Take(20); // Show last 20 backups
                
                foreach (var file in backupFiles)
                {
                    var fileInfo = new FileInfo(file);
                    var fileSizeMB = fileInfo.Length / (1024.0 * 1024.0);
                    var displayText = $"{fileInfo.CreationTime:yyyy-MM-dd HH:mm:ss} - {fileInfo.Name} ({fileSizeMB:F2} MB)";
                    lstBackupHistory.Items.Add(displayText);
                }
                
                if (lstBackupHistory.Items.Count == 0)
                {
                    lstBackupHistory.Items.Add("📭 Chưa có backup nào");
                }
            }
            catch
            {
                // Ignore errors
            }
        }
    }
}
