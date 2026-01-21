namespace AMU.store.Mngt
{
    partial class Login_form
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
            this.components = new System.ComponentModel.Container();
            this.header_text = new System.Windows.Forms.Label();
            this.subtitle_text = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.username_label = new System.Windows.Forms.Label();
            this.password_label = new System.Windows.Forms.Label();
            this.username_input = new System.Windows.Forms.TextBox();
            this.password_input = new System.Windows.Forms.TextBox();
            this.login_btn = new System.Windows.Forms.Button();
            this.loginPanel = new System.Windows.Forms.Panel();
            this.logoPanel = new System.Windows.Forms.Panel();
            this.logoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // logoPanel
            // 
            this.logoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.logoPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.logoPanel.Location = new System.Drawing.Point(0, 0);
            this.logoPanel.Name = "logoPanel";
            this.logoPanel.Size = new System.Drawing.Size(350, 550);
            this.logoPanel.TabIndex = 0;
            // 
            // logoLabel
            // 
            this.logoLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logoLabel.ForeColor = System.Drawing.Color.White;
            this.logoLabel.Location = new System.Drawing.Point(20, 150);
            this.logoLabel.Name = "logoLabel";
            this.logoLabel.Size = new System.Drawing.Size(310, 200);
            this.logoLabel.TabIndex = 0;
            this.logoLabel.Text = "AMU\r\nSTORE\r\nMANAGEMENT";
            this.logoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.logoPanel.Controls.Add(this.logoLabel);
            // 
            // loginPanel
            // 
            this.loginPanel.BackColor = System.Drawing.Color.White;
            this.loginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginPanel.Location = new System.Drawing.Point(350, 0);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(550, 550);
            this.loginPanel.TabIndex = 1;
            // 
            // header_text
            // 
            this.header_text.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header_text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.header_text.Location = new System.Drawing.Point(50, 80);
            this.header_text.Name = "header_text";
            this.header_text.Size = new System.Drawing.Size(450, 50);
            this.header_text.TabIndex = 0;
            this.header_text.Text = "Welcome Back!";
            this.header_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loginPanel.Controls.Add(this.header_text);
            // 
            // subtitle_text
            // 
            this.subtitle_text.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subtitle_text.ForeColor = System.Drawing.Color.Gray;
            this.subtitle_text.Location = new System.Drawing.Point(50, 130);
            this.subtitle_text.Name = "subtitle_text";
            this.subtitle_text.Size = new System.Drawing.Size(450, 30);
            this.subtitle_text.TabIndex = 1;
            this.subtitle_text.Text = "Please login to your account";
            this.subtitle_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loginPanel.Controls.Add(this.subtitle_text);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.username_label.Location = new System.Drawing.Point(100, 200);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(75, 19);
            this.username_label.TabIndex = 2;
            this.username_label.Text = "Username:";
            this.loginPanel.Controls.Add(this.username_label);
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.password_label.Location = new System.Drawing.Point(100, 270);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(73, 19);
            this.password_label.TabIndex = 3;
            this.password_label.Text = "Password:";
            this.password_label.Click += new System.EventHandler(this.label2_Click);
            this.loginPanel.Controls.Add(this.password_label);
            // 
            // username_input
            // 
            this.username_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.username_input.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_input.Location = new System.Drawing.Point(100, 225);
            this.username_input.Name = "username_input";
            this.username_input.Size = new System.Drawing.Size(350, 27);
            this.username_input.TabIndex = 4;
            this.loginPanel.Controls.Add(this.username_input);
            // 
            // password_input
            // 
            this.password_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password_input.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password_input.Location = new System.Drawing.Point(100, 295);
            this.password_input.Name = "password_input";
            this.password_input.Size = new System.Drawing.Size(350, 27);
            this.password_input.TabIndex = 5;
            this.password_input.UseSystemPasswordChar = true;
            this.loginPanel.Controls.Add(this.password_input);

            // remember checkbox
            this.remember_checkbox = new System.Windows.Forms.CheckBox();
            this.remember_checkbox.AutoSize = true;
            this.remember_checkbox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remember_checkbox.ForeColor = System.Drawing.Color.Gray;
            this.remember_checkbox.Location = new System.Drawing.Point(100, 335);
            this.remember_checkbox.Name = "remember_checkbox";
            this.remember_checkbox.Size = new System.Drawing.Size(104, 19);
            this.remember_checkbox.TabIndex = 7;
            this.remember_checkbox.Text = "Remember me";
            this.remember_checkbox.UseVisualStyleBackColor = true;
            this.loginPanel.Controls.Add(this.remember_checkbox);

            // forgot link
            this.forgot_link = new System.Windows.Forms.LinkLabel();
            this.forgot_link.AutoSize = true;
            this.forgot_link.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.forgot_link.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.forgot_link.Location = new System.Drawing.Point(340, 336);
            this.forgot_link.Name = "forgot_link";
            this.forgot_link.Size = new System.Drawing.Size(110, 15);
            this.forgot_link.TabIndex = 8;
            this.forgot_link.TabStop = true;
            this.forgot_link.Text = "Forgot password?";
            this.loginPanel.Controls.Add(this.forgot_link);
            // 
            // login_btn
            // 
            this.login_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.login_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.login_btn.FlatAppearance.BorderSize = 0;
            this.login_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.login_btn.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_btn.ForeColor = System.Drawing.Color.White;
            this.login_btn.Location = new System.Drawing.Point(100, 380);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(350, 45);
            this.login_btn.TabIndex = 6;
            this.login_btn.Text = "LOGIN";
            this.login_btn.UseVisualStyleBackColor = false;
            this.AcceptButton = this.login_btn;
            this.loginPanel.Controls.Add(this.login_btn);

            // status strip
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.toolStripStatusLabel1 });
            this.toolStripStatusLabel1.Text = "© 2026 Arba Minch University - All Rights Reserved";
            this.statusStrip1.Location = new System.Drawing.Point(0, 528);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(900, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            this.Controls.Add(this.statusStrip1);
            // 
            // Login_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.loginPanel);
            this.Controls.Add(this.logoPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Login_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AMU Store Management - Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label header_text;
        private System.Windows.Forms.Label subtitle_text;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.TextBox username_input;
        private System.Windows.Forms.TextBox password_input;
        private System.Windows.Forms.Button login_btn;
        private System.Windows.Forms.CheckBox remember_checkbox;
        private System.Windows.Forms.LinkLabel forgot_link;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.Panel logoPanel;
        private System.Windows.Forms.Label logoLabel;
    }
}

