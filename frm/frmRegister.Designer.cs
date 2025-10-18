namespace SocialManager.frm
{
    partial class frmRegister
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMain = new Panel();
            pnlRegisterCard = new Panel();
            pnlFooter = new Panel();
            llblLogin = new LinkLabel();
            lblLoginPrompt = new Label();
            pnlRegisterForm = new Panel();
            tlpForm = new TableLayoutPanel();
            pnlUsername = new Panel();
            txtUsername = new TextBox();
            picUsernameIcon = new PictureBox();
            pnlEmail = new Panel();
            txtEmail = new TextBox();
            picEmailIcon = new PictureBox();
            pnlFullName = new Panel();
            txtFullName = new TextBox();
            picFullNameIcon = new PictureBox();
            pnlPhone = new Panel();
            txtPhone = new TextBox();
            picPhoneIcon = new PictureBox();
            pnlPassword = new Panel();
            txtPassword = new TextBox();
            picPasswordIcon = new PictureBox();
            pnlConfirmPassword = new Panel();
            txtConfirmPassword = new TextBox();
            picConfirmPasswordIcon = new PictureBox();
            pnlGender = new Panel();
            cmbGender = new ComboBox();
            picGenderIcon = new PictureBox();
            pnlDateOfBirth = new Panel();
            dtpDateOfBirth = new DateTimePicker();
            picDateIcon = new PictureBox();
            pnlTerms = new Panel();
            chkTerms = new CheckBox();
            btnRegister = new Button();
            pnlHeader = new Panel();
            lblSubtitle = new Label();
            lblTitle = new Label();
            picLogo = new PictureBox();
            pnlBackground = new Panel();
            btnBack = new Button();
            picAppLogo = new PictureBox();
            lblAppDescription = new Label();
            lblAppName = new Label();
            pnlMain.SuspendLayout();
            pnlRegisterCard.SuspendLayout();
            pnlFooter.SuspendLayout();
            pnlRegisterForm.SuspendLayout();
            tlpForm.SuspendLayout();
            pnlUsername.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUsernameIcon).BeginInit();
            pnlEmail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picEmailIcon).BeginInit();
            pnlFullName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picFullNameIcon).BeginInit();
            pnlPhone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPhoneIcon).BeginInit();
            pnlPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPasswordIcon).BeginInit();
            pnlConfirmPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picConfirmPasswordIcon).BeginInit();
            pnlGender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picGenderIcon).BeginInit();
            pnlDateOfBirth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picDateIcon).BeginInit();
            pnlTerms.SuspendLayout();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAppLogo).BeginInit();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.AutoScroll = true;
            pnlMain.BackColor = Color.White;
            pnlMain.Controls.Add(pnlRegisterCard);
            pnlMain.Dock = DockStyle.Right;
            pnlMain.Location = new Point(600, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(40, 30, 40, 30);
            pnlMain.Size = new Size(600, 800);
            pnlMain.TabIndex = 0;
            // 
            // pnlRegisterCard
            // 
            pnlRegisterCard.BackColor = Color.White;
            pnlRegisterCard.Controls.Add(pnlFooter);
            pnlRegisterCard.Controls.Add(pnlRegisterForm);
            pnlRegisterCard.Controls.Add(pnlHeader);
            pnlRegisterCard.Dock = DockStyle.Top;
            pnlRegisterCard.Location = new Point(40, 30);
            pnlRegisterCard.MinimumSize = new Size(520, 850);
            pnlRegisterCard.Name = "pnlRegisterCard";
            pnlRegisterCard.Size = new Size(520, 850);
            pnlRegisterCard.TabIndex = 0;
            pnlRegisterCard.Paint += pnlRegisterCard_Paint;
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(llblLogin);
            pnlFooter.Controls.Add(lblLoginPrompt);
            pnlFooter.Dock = DockStyle.Top;
            pnlFooter.Location = new Point(0, 800);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(40, 20, 40, 30);
            pnlFooter.Size = new Size(520, 50);
            pnlFooter.TabIndex = 2;
            // 
            // llblLogin
            // 
            llblLogin.AutoSize = true;
            llblLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            llblLogin.LinkColor = Color.FromArgb(52, 152, 219);
            llblLogin.Location = new Point(255, 20);
            llblLogin.Name = "llblLogin";
            llblLogin.Size = new Size(55, 23);
            llblLogin.TabIndex = 1;
            llblLogin.TabStop = true;
            llblLogin.Text = "Login";
            llblLogin.LinkClicked += llblLogin_LinkClicked;
            // 
            // lblLoginPrompt
            // 
            lblLoginPrompt.AutoSize = true;
            lblLoginPrompt.Font = new Font("Segoe UI", 10F);
            lblLoginPrompt.ForeColor = Color.FromArgb(127, 140, 141);
            lblLoginPrompt.Location = new Point(40, 20);
            lblLoginPrompt.Name = "lblLoginPrompt";
            lblLoginPrompt.Size = new Size(206, 23);
            lblLoginPrompt.TabIndex = 0;
            lblLoginPrompt.Text = "Already have an account?";
            // 
            // pnlRegisterForm
            // 
            pnlRegisterForm.Controls.Add(tlpForm);
            pnlRegisterForm.Dock = DockStyle.Top;
            pnlRegisterForm.Location = new Point(0, 120);
            pnlRegisterForm.Name = "pnlRegisterForm";
            pnlRegisterForm.Padding = new Padding(40, 20, 40, 30);
            pnlRegisterForm.Size = new Size(520, 680);
            pnlRegisterForm.TabIndex = 1;
            // 
            // tlpForm
            // 
            tlpForm.ColumnCount = 2;
            tlpForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpForm.Controls.Add(pnlUsername, 0, 0);
            tlpForm.Controls.Add(pnlEmail, 1, 0);
            tlpForm.Controls.Add(pnlFullName, 0, 1);
            tlpForm.Controls.Add(pnlPhone, 1, 1);
            tlpForm.Controls.Add(pnlPassword, 0, 2);
            tlpForm.Controls.Add(pnlConfirmPassword, 1, 2);
            tlpForm.Controls.Add(pnlGender, 0, 3);
            tlpForm.Controls.Add(pnlDateOfBirth, 1, 3);
            tlpForm.Controls.Add(pnlTerms, 0, 5);
            tlpForm.Controls.Add(btnRegister, 0, 6);
            tlpForm.Dock = DockStyle.Fill;
            tlpForm.Location = new Point(40, 20);
            tlpForm.Name = "tlpForm";
            tlpForm.RowCount = 7;
            tlpForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tlpForm.Size = new Size(440, 630);
            tlpForm.TabIndex = 0;
            // 
            // pnlUsername
            // 
            pnlUsername.BackColor = Color.FromArgb(248, 249, 250);
            pnlUsername.Controls.Add(txtUsername);
            pnlUsername.Controls.Add(picUsernameIcon);
            pnlUsername.Dock = DockStyle.Fill;
            pnlUsername.Location = new Point(3, 3);
            pnlUsername.Margin = new Padding(3, 3, 8, 8);
            pnlUsername.Name = "pnlUsername";
            pnlUsername.Size = new Size(209, 49);
            pnlUsername.TabIndex = 0;
            pnlUsername.Paint += pnlInput_Paint;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(248, 249, 250);
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.ForeColor = Color.FromArgb(44, 62, 80);
            txtUsername.Location = new Point(35, 12);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Username";
            txtUsername.Size = new Size(165, 23);
            txtUsername.TabIndex = 1;
            // 
            // picUsernameIcon
            // 
            picUsernameIcon.Location = new Point(10, 14);
            picUsernameIcon.Name = "picUsernameIcon";
            picUsernameIcon.Size = new Size(20, 20);
            picUsernameIcon.TabIndex = 0;
            picUsernameIcon.TabStop = false;
            picUsernameIcon.Paint += picUsernameIcon_Paint;
            // 
            // pnlEmail
            // 
            pnlEmail.BackColor = Color.FromArgb(248, 249, 250);
            pnlEmail.Controls.Add(txtEmail);
            pnlEmail.Controls.Add(picEmailIcon);
            pnlEmail.Dock = DockStyle.Fill;
            pnlEmail.Location = new Point(228, 3);
            pnlEmail.Margin = new Padding(8, 3, 3, 8);
            pnlEmail.Name = "pnlEmail";
            pnlEmail.Size = new Size(209, 49);
            pnlEmail.TabIndex = 1;
            pnlEmail.Paint += pnlInput_Paint;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.FromArgb(248, 249, 250);
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.ForeColor = Color.FromArgb(44, 62, 80);
            txtEmail.Location = new Point(35, 12);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Email Address";
            txtEmail.Size = new Size(165, 23);
            txtEmail.TabIndex = 1;
            // 
            // picEmailIcon
            // 
            picEmailIcon.Location = new Point(10, 14);
            picEmailIcon.Name = "picEmailIcon";
            picEmailIcon.Size = new Size(20, 20);
            picEmailIcon.TabIndex = 0;
            picEmailIcon.TabStop = false;
            picEmailIcon.Paint += picEmailIcon_Paint;
            // 
            // pnlFullName
            // 
            pnlFullName.BackColor = Color.FromArgb(248, 249, 250);
            pnlFullName.Controls.Add(txtFullName);
            pnlFullName.Controls.Add(picFullNameIcon);
            pnlFullName.Dock = DockStyle.Fill;
            pnlFullName.Location = new Point(3, 68);
            pnlFullName.Margin = new Padding(3, 8, 8, 8);
            pnlFullName.Name = "pnlFullName";
            pnlFullName.Size = new Size(209, 44);
            pnlFullName.TabIndex = 2;
            pnlFullName.Paint += pnlInput_Paint;
            // 
            // txtFullName
            // 
            txtFullName.BackColor = Color.FromArgb(248, 249, 250);
            txtFullName.BorderStyle = BorderStyle.None;
            txtFullName.Font = new Font("Segoe UI", 10F);
            txtFullName.ForeColor = Color.FromArgb(44, 62, 80);
            txtFullName.Location = new Point(35, 12);
            txtFullName.Name = "txtFullName";
            txtFullName.PlaceholderText = "Full Name";
            txtFullName.Size = new Size(165, 23);
            txtFullName.TabIndex = 1;
            // 
            // picFullNameIcon
            // 
            picFullNameIcon.Location = new Point(10, 14);
            picFullNameIcon.Name = "picFullNameIcon";
            picFullNameIcon.Size = new Size(20, 20);
            picFullNameIcon.TabIndex = 0;
            picFullNameIcon.TabStop = false;
            picFullNameIcon.Paint += picFullNameIcon_Paint;
            // 
            // pnlPhone
            // 
            pnlPhone.BackColor = Color.FromArgb(248, 249, 250);
            pnlPhone.Controls.Add(txtPhone);
            pnlPhone.Controls.Add(picPhoneIcon);
            pnlPhone.Dock = DockStyle.Fill;
            pnlPhone.Location = new Point(228, 68);
            pnlPhone.Margin = new Padding(8, 8, 3, 8);
            pnlPhone.Name = "pnlPhone";
            pnlPhone.Size = new Size(209, 44);
            pnlPhone.TabIndex = 3;
            pnlPhone.Paint += pnlInput_Paint;
            // 
            // txtPhone
            // 
            txtPhone.BackColor = Color.FromArgb(248, 249, 250);
            txtPhone.BorderStyle = BorderStyle.None;
            txtPhone.Font = new Font("Segoe UI", 10F);
            txtPhone.ForeColor = Color.FromArgb(44, 62, 80);
            txtPhone.Location = new Point(35, 12);
            txtPhone.Name = "txtPhone";
            txtPhone.PlaceholderText = "Phone Number";
            txtPhone.Size = new Size(165, 23);
            txtPhone.TabIndex = 1;
            // 
            // picPhoneIcon
            // 
            picPhoneIcon.Location = new Point(10, 14);
            picPhoneIcon.Name = "picPhoneIcon";
            picPhoneIcon.Size = new Size(20, 20);
            picPhoneIcon.TabIndex = 0;
            picPhoneIcon.TabStop = false;
            picPhoneIcon.Paint += picPhoneIcon_Paint;
            // 
            // pnlPassword
            // 
            pnlPassword.BackColor = Color.FromArgb(248, 249, 250);
            pnlPassword.Controls.Add(txtPassword);
            pnlPassword.Controls.Add(picPasswordIcon);
            pnlPassword.Dock = DockStyle.Fill;
            pnlPassword.Location = new Point(3, 128);
            pnlPassword.Margin = new Padding(3, 8, 8, 8);
            pnlPassword.Name = "pnlPassword";
            pnlPassword.Size = new Size(209, 44);
            pnlPassword.TabIndex = 4;
            pnlPassword.Paint += pnlInput_Paint;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(248, 249, 250);
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.ForeColor = Color.FromArgb(44, 62, 80);
            txtPassword.Location = new Point(35, 12);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Password";
            txtPassword.Size = new Size(165, 23);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // picPasswordIcon
            // 
            picPasswordIcon.Location = new Point(10, 14);
            picPasswordIcon.Name = "picPasswordIcon";
            picPasswordIcon.Size = new Size(20, 20);
            picPasswordIcon.TabIndex = 0;
            picPasswordIcon.TabStop = false;
            picPasswordIcon.Paint += picPasswordIcon_Paint;
            // 
            // pnlConfirmPassword
            // 
            pnlConfirmPassword.BackColor = Color.FromArgb(248, 249, 250);
            pnlConfirmPassword.Controls.Add(txtConfirmPassword);
            pnlConfirmPassword.Controls.Add(picConfirmPasswordIcon);
            pnlConfirmPassword.Dock = DockStyle.Fill;
            pnlConfirmPassword.Location = new Point(228, 128);
            pnlConfirmPassword.Margin = new Padding(8, 8, 3, 8);
            pnlConfirmPassword.Name = "pnlConfirmPassword";
            pnlConfirmPassword.Size = new Size(209, 44);
            pnlConfirmPassword.TabIndex = 5;
            pnlConfirmPassword.Paint += pnlInput_Paint;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BackColor = Color.FromArgb(248, 249, 250);
            txtConfirmPassword.BorderStyle = BorderStyle.None;
            txtConfirmPassword.Font = new Font("Segoe UI", 10F);
            txtConfirmPassword.ForeColor = Color.FromArgb(44, 62, 80);
            txtConfirmPassword.Location = new Point(35, 12);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PlaceholderText = "Confirm Password";
            txtConfirmPassword.Size = new Size(165, 23);
            txtConfirmPassword.TabIndex = 1;
            txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // picConfirmPasswordIcon
            // 
            picConfirmPasswordIcon.Location = new Point(10, 14);
            picConfirmPasswordIcon.Name = "picConfirmPasswordIcon";
            picConfirmPasswordIcon.Size = new Size(20, 20);
            picConfirmPasswordIcon.TabIndex = 0;
            picConfirmPasswordIcon.TabStop = false;
            picConfirmPasswordIcon.Paint += picConfirmPasswordIcon_Paint;
            // 
            // pnlGender
            // 
            pnlGender.BackColor = Color.FromArgb(248, 249, 250);
            pnlGender.Controls.Add(cmbGender);
            pnlGender.Controls.Add(picGenderIcon);
            pnlGender.Dock = DockStyle.Fill;
            pnlGender.Location = new Point(3, 188);
            pnlGender.Margin = new Padding(3, 8, 8, 8);
            pnlGender.Name = "pnlGender";
            pnlGender.Size = new Size(209, 44);
            pnlGender.TabIndex = 6;
            pnlGender.Paint += pnlInput_Paint;
            // 
            // cmbGender
            // 
            cmbGender.BackColor = Color.FromArgb(248, 249, 250);
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.FlatStyle = FlatStyle.Flat;
            cmbGender.Font = new Font("Segoe UI", 10F);
            cmbGender.ForeColor = Color.FromArgb(44, 62, 80);
            cmbGender.FormattingEnabled = true;
            cmbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
            cmbGender.Location = new Point(35, 11);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(165, 31);
            cmbGender.TabIndex = 1;
            // 
            // picGenderIcon
            // 
            picGenderIcon.Location = new Point(10, 14);
            picGenderIcon.Name = "picGenderIcon";
            picGenderIcon.Size = new Size(20, 20);
            picGenderIcon.TabIndex = 0;
            picGenderIcon.TabStop = false;
            picGenderIcon.Paint += picGenderIcon_Paint;
            // 
            // pnlDateOfBirth
            // 
            pnlDateOfBirth.BackColor = Color.FromArgb(248, 249, 250);
            pnlDateOfBirth.Controls.Add(dtpDateOfBirth);
            pnlDateOfBirth.Controls.Add(picDateIcon);
            pnlDateOfBirth.Dock = DockStyle.Fill;
            pnlDateOfBirth.Location = new Point(228, 188);
            pnlDateOfBirth.Margin = new Padding(8, 8, 3, 8);
            pnlDateOfBirth.Name = "pnlDateOfBirth";
            pnlDateOfBirth.Size = new Size(209, 44);
            pnlDateOfBirth.TabIndex = 7;
            pnlDateOfBirth.Paint += pnlInput_Paint;
            // 
            // dtpDateOfBirth
            // 
            dtpDateOfBirth.CalendarForeColor = Color.FromArgb(44, 62, 80);
            dtpDateOfBirth.CalendarMonthBackground = Color.FromArgb(248, 249, 250);
            dtpDateOfBirth.Font = new Font("Segoe UI", 10F);
            dtpDateOfBirth.Format = DateTimePickerFormat.Short;
            dtpDateOfBirth.Location = new Point(35, 11);
            dtpDateOfBirth.MaxDate = new DateTime(2010, 12, 31, 0, 0, 0, 0);
            dtpDateOfBirth.MinDate = new DateTime(1940, 1, 1, 0, 0, 0, 0);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(165, 30);
            dtpDateOfBirth.TabIndex = 1;
            dtpDateOfBirth.Value = new DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // picDateIcon
            // 
            picDateIcon.Location = new Point(10, 14);
            picDateIcon.Name = "picDateIcon";
            picDateIcon.Size = new Size(20, 20);
            picDateIcon.TabIndex = 0;
            picDateIcon.TabStop = false;
            picDateIcon.Paint += picDateIcon_Paint;
            // 
            // pnlTerms
            // 
            tlpForm.SetColumnSpan(pnlTerms, 2);
            pnlTerms.Controls.Add(chkTerms);
            pnlTerms.Dock = DockStyle.Fill;
            pnlTerms.Location = new Point(3, 308);
            pnlTerms.Margin = new Padding(3, 8, 3, 8);
            pnlTerms.Name = "pnlTerms";
            pnlTerms.Size = new Size(434, 44);
            pnlTerms.TabIndex = 9;
            // 
            // chkTerms
            // 
            chkTerms.AutoSize = true;
            chkTerms.Font = new Font("Segoe UI", 10F);
            chkTerms.ForeColor = Color.FromArgb(127, 140, 141);
            chkTerms.Location = new Point(0, 10);
            chkTerms.Name = "chkTerms";
            chkTerms.Size = new Size(403, 27);
            chkTerms.TabIndex = 0;
            chkTerms.Text = "I agree to the Terms of Service and Privacy Policy";
            chkTerms.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.FromArgb(46, 204, 113);
            tlpForm.SetColumnSpan(btnRegister, 2);
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.FlatAppearance.MouseDownBackColor = Color.FromArgb(39, 174, 96);
            btnRegister.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 174, 96);
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(3, 368);
            btnRegister.Margin = new Padding(3, 8, 3, 8);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(434, 50);
            btnRegister.TabIndex = 10;
            btnRegister.Text = "CREATE ACCOUNT";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(picLogo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(40, 30, 40, 20);
            pnlHeader.Size = new Size(520, 120);
            pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(127, 140, 141);
            lblSubtitle.Location = new Point(40, 80);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(254, 23);
            lblSubtitle.TabIndex = 2;
            lblSubtitle.Text = "Create your new admin account";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(40, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(178, 54);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Register";
            // 
            // picLogo
            // 
            picLogo.Location = new Point(430, 30);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(50, 50);
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            picLogo.Paint += picLogo_Paint;
            // 
            // pnlBackground
            // 
            pnlBackground.BackColor = Color.FromArgb(46, 204, 113);
            pnlBackground.Controls.Add(btnBack);
            pnlBackground.Controls.Add(picAppLogo);
            pnlBackground.Controls.Add(lblAppDescription);
            pnlBackground.Controls.Add(lblAppName);
            pnlBackground.Dock = DockStyle.Fill;
            pnlBackground.Location = new Point(0, 0);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.Padding = new Padding(80);
            pnlBackground.Size = new Size(600, 800);
            pnlBackground.TabIndex = 1;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(192, 255, 192);
            btnBack.ForeColor = SystemColors.ControlText;
            btnBack.Location = new Point(12, 12);
            btnBack.Name = "btnBack";
            btnBack.RightToLeft = RightToLeft.No;
            btnBack.Size = new Size(76, 40);
            btnBack.TabIndex = 3;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += button1_Click;
            // 
            // picAppLogo
            // 
            picAppLogo.Location = new Point(260, 200);
            picAppLogo.Name = "picAppLogo";
            picAppLogo.Size = new Size(80, 80);
            picAppLogo.TabIndex = 2;
            picAppLogo.TabStop = false;
            picAppLogo.Click += picAppLogo_Click;
            picAppLogo.Paint += picAppLogo_Paint;
            // 
            // lblAppDescription
            // 
            lblAppDescription.AutoSize = true;
            lblAppDescription.Font = new Font("Segoe UI Black", 16F);
            lblAppDescription.ForeColor = Color.Honeydew;
            lblAppDescription.Location = new Point(41, 388);
            lblAppDescription.Name = "lblAppDescription";
            lblAppDescription.Size = new Size(544, 74);
            lblAppDescription.TabIndex = 1;
            lblAppDescription.Text = "Join our platform to manage and grow \r\nyour social media presence effectively";
            lblAppDescription.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblAppName.ForeColor = Color.White;
            lblAppName.Location = new Point(80, 300);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(471, 72);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "Join Social Media";
            // 
            // frmRegister
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 800);
            Controls.Add(pnlBackground);
            Controls.Add(pnlMain);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmRegister";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Social Media Manager - Register";
            pnlMain.ResumeLayout(false);
            pnlRegisterCard.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            pnlRegisterForm.ResumeLayout(false);
            tlpForm.ResumeLayout(false);
            pnlUsername.ResumeLayout(false);
            pnlUsername.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picUsernameIcon).EndInit();
            pnlEmail.ResumeLayout(false);
            pnlEmail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picEmailIcon).EndInit();
            pnlFullName.ResumeLayout(false);
            pnlFullName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picFullNameIcon).EndInit();
            pnlPhone.ResumeLayout(false);
            pnlPhone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPhoneIcon).EndInit();
            pnlPassword.ResumeLayout(false);
            pnlPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPasswordIcon).EndInit();
            pnlConfirmPassword.ResumeLayout(false);
            pnlConfirmPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picConfirmPasswordIcon).EndInit();
            pnlGender.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picGenderIcon).EndInit();
            pnlDateOfBirth.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picDateIcon).EndInit();
            pnlTerms.ResumeLayout(false);
            pnlTerms.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlBackground.ResumeLayout(false);
            pnlBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAppLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Panel pnlRegisterCard;
        private Panel pnlHeader;
        private PictureBox picLogo;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlRegisterForm;
        private TableLayoutPanel tlpForm;
        private Panel pnlUsername;
        private TextBox txtUsername;
        private PictureBox picUsernameIcon;
        private Panel pnlEmail;
        private TextBox txtEmail;
        private PictureBox picEmailIcon;
        private Panel pnlFullName;
        private TextBox txtFullName;
        private PictureBox picFullNameIcon;
        private Panel pnlPhone;
        private TextBox txtPhone;
        private PictureBox picPhoneIcon;
        private Panel pnlPassword;
        private TextBox txtPassword;
        private PictureBox picPasswordIcon;
        private Panel pnlConfirmPassword;
        private TextBox txtConfirmPassword;
        private PictureBox picConfirmPasswordIcon;
        private Panel pnlGender;
        private ComboBox cmbGender;
        private PictureBox picGenderIcon;
        private Panel pnlDateOfBirth;
        private DateTimePicker dtpDateOfBirth;
        private PictureBox picDateIcon;
        private Panel pnlTerms;
        private CheckBox chkTerms;
        private Button btnRegister;
        private Panel pnlFooter;
        private LinkLabel llblLogin;
        private Label lblLoginPrompt;
        private Panel pnlBackground;
        private PictureBox picAppLogo;
        private Label lblAppDescription;
        private Label lblAppName;
        private Button btnBack;
    }
}