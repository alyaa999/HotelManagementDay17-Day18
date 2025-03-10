using MetroFramework.Controls;

namespace Hotel_Manager
{
    partial class Login
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
            usernameLabel = new MetroLabel();
            passwordLabel = new MetroLabel();
            signinButton = new MetroButton();
            passwordTextBox = new MetroTextBox();
            usernameTextBox = new MetroTextBox();
            LicenseCallButton = new MetroButton();
            SuspendLayout();
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.CustomBackground = false;
            usernameLabel.FontSize = MetroFramework.MetroLabelSize.Medium;
            usernameLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            usernameLabel.ForeColor = System.Drawing.Color.Black;
            usernameLabel.LabelMode = MetroLabelMode.Default;
            usernameLabel.Location = new System.Drawing.Point(217, 146);
            usernameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new System.Drawing.Size(75, 20);
            usernameLabel.Style = MetroFramework.MetroColorStyle.Blue;
            usernameLabel.StyleManager = null;
            usernameLabel.TabIndex = 3;
            usernameLabel.Text = "Username";
            usernameLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            usernameLabel.UseStyleColors = false;
            usernameLabel.Visible = false;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.CustomBackground = false;
            passwordLabel.FontSize = MetroFramework.MetroLabelSize.Medium;
            passwordLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            passwordLabel.LabelMode = MetroLabelMode.Default;
            passwordLabel.Location = new System.Drawing.Point(216, 235);
            passwordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new System.Drawing.Size(70, 20);
            passwordLabel.Style = MetroFramework.MetroColorStyle.Blue;
            passwordLabel.StyleManager = null;
            passwordLabel.TabIndex = 5;
            passwordLabel.Text = "Password";
            passwordLabel.Theme = MetroFramework.MetroThemeStyle.Light;
            passwordLabel.UseStyleColors = false;
            passwordLabel.Visible = false;
            // 
            // signinButton
            // 
            signinButton.Highlight = false;
            signinButton.Location = new System.Drawing.Point(220, 358);
            signinButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            signinButton.Name = "signinButton";
            signinButton.Size = new System.Drawing.Size(271, 46);
            signinButton.Style = MetroFramework.MetroColorStyle.Green;
            signinButton.StyleManager = null;
            signinButton.TabIndex = 6;
            signinButton.Text = "Sign in";
            signinButton.Theme = MetroFramework.MetroThemeStyle.Light;
            signinButton.Click += signinButton_Click;
            // 
            // passwordTextBox
            // 
            passwordTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            passwordTextBox.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            passwordTextBox.Location = new System.Drawing.Point(219, 269);
            passwordTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            passwordTextBox.Multiline = false;
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.SelectedText = "";
            passwordTextBox.Size = new System.Drawing.Size(272, 45);
            passwordTextBox.Style = MetroFramework.MetroColorStyle.Blue;
            passwordTextBox.StyleManager = null;
            passwordTextBox.TabIndex = 2;
            passwordTextBox.Theme = MetroFramework.MetroThemeStyle.Light;
            passwordTextBox.UseStyleColors = true;
            passwordTextBox.Click += passwordTextBox_Click;
            // 
            // usernameTextBox
            // 
            usernameTextBox.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
            usernameTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            usernameTextBox.FontWeight = MetroFramework.MetroTextBoxWeight.Regular;
            usernameTextBox.Location = new System.Drawing.Point(219, 185);
            usernameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            usernameTextBox.Multiline = false;
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.SelectedText = "";
            usernameTextBox.Size = new System.Drawing.Size(272, 45);
            usernameTextBox.Style = MetroFramework.MetroColorStyle.Blue;
            usernameTextBox.StyleManager = null;
            usernameTextBox.TabIndex = 0;
            usernameTextBox.Theme = MetroFramework.MetroThemeStyle.Light;
            usernameTextBox.UseStyleColors = true;
            usernameTextBox.Click += usernameTextBox_Click;
            // 
            // LicenseCallButton
            // 
            LicenseCallButton.Highlight = false;
            LicenseCallButton.Location = new System.Drawing.Point(624, 545);
            LicenseCallButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            LicenseCallButton.Name = "LicenseCallButton";
            LicenseCallButton.Size = new System.Drawing.Size(64, 35);
            LicenseCallButton.Style = MetroFramework.MetroColorStyle.Blue;
            LicenseCallButton.StyleManager = null;
            LicenseCallButton.TabIndex = 7;
            LicenseCallButton.Text = "License";
            LicenseCallButton.Theme = MetroFramework.MetroThemeStyle.Light;
            LicenseCallButton.Click += LicenseCallButton_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(688, 580);
            Controls.Add(LicenseCallButton);
            Controls.Add(passwordTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(usernameLabel);
            Controls.Add(signinButton);
            Controls.Add(usernameTextBox);
            Location = new System.Drawing.Point(0, 0);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "Login";
            Padding = new System.Windows.Forms.Padding(27, 92, 27, 31);
            Resizable = false;
            Style = MetroFramework.MetroColorStyle.Teal;
            Text = "Login";
            FormClosing += login_FormClosing;
            Load += Login_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MetroFramework.Controls.MetroTextBox usernameTextBox;
        private MetroFramework.Controls.MetroButton signinButton;
        private MetroFramework.Controls.MetroLabel usernameLabel;
        private MetroFramework.Controls.MetroLabel passwordLabel;
        private MetroFramework.Controls.MetroTextBox passwordTextBox;
        private MetroFramework.Controls.MetroButton LicenseCallButton;
        
       
    }
}

