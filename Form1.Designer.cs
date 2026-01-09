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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.username_label = new System.Windows.Forms.Label();
            this.password_label = new System.Windows.Forms.Label();
            this.username_input = new System.Windows.Forms.TextBox();
            this.password_input = new System.Windows.Forms.TextBox();
            this.login_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // header_text (Label)
            // 
            this.header_text.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header_text.Location = new System.Drawing.Point(150, 12);
            this.header_text.Name = "header_text";
            this.header_text.Size = new System.Drawing.Size(500, 60);
            this.header_text.TabIndex = 0;
            this.header_text.Text = "AMU STORE MANAGEMENT SYSTEM";
            this.header_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.username_label.Location = new System.Drawing.Point(180, 100);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(70, 16);
            this.username_label.TabIndex = 2;
            this.username_label.Text = "Username";
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Location = new System.Drawing.Point(180, 150);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(67, 16);
            this.password_label.TabIndex = 3;
            this.password_label.Text = "Password";
            this.password_label.Click += new System.EventHandler(this.label2_Click);
            // 
            // username_input
            // 
            this.username_input.Location = new System.Drawing.Point(260, 97);
            this.username_input.Name = "username_input";
            this.username_input.Size = new System.Drawing.Size(280, 25);
            this.username_input.TabIndex = 4;
            // 
            // password_input
            // 
            this.password_input.Location = new System.Drawing.Point(260, 147);
            this.password_input.Name = "password_input";
            this.password_input.Size = new System.Drawing.Size(280, 25);
            this.password_input.TabIndex = 5;
            this.password_input.UseSystemPasswordChar = true;

            // remember checkbox
            this.remember_checkbox = new System.Windows.Forms.CheckBox();
            this.remember_checkbox.Location = new System.Drawing.Point(260, 180);
            this.remember_checkbox.Name = "remember_checkbox";
            this.remember_checkbox.Size = new System.Drawing.Size(120, 24);
            this.remember_checkbox.TabIndex = 7;
            this.remember_checkbox.Text = "Remember me";
            this.remember_checkbox.UseVisualStyleBackColor = true;

            // forgot link
            this.forgot_link = new System.Windows.Forms.LinkLabel();
            this.forgot_link.Location = new System.Drawing.Point(460, 180);
            this.forgot_link.Name = "forgot_link";
            this.forgot_link.Size = new System.Drawing.Size(120, 24);
            this.forgot_link.TabIndex = 8;
            this.forgot_link.TabStop = true;
            this.forgot_link.Text = "Forgot password?";
            this.forgot_link.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // login_btn
            // 
            this.login_btn.Location = new System.Drawing.Point(293, 243);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(75, 23);
            this.login_btn.TabIndex = 6;
            this.login_btn.Text = "Login";
            this.login_btn.UseVisualStyleBackColor = true;
            this.AcceptButton = this.login_btn;

            // status strip
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.toolStripStatusLabel1 });
            this.toolStripStatusLabel1.Text = "Ready";
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            this.Controls.Add(this.statusStrip1);

            // add remember and forgot controls
            this.Controls.Add(this.remember_checkbox);
            this.Controls.Add(this.forgot_link);
            // 
            // Login_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.password_input);
            this.Controls.Add(this.username_input);
            this.Controls.Add(this.password_label);
            this.Controls.Add(this.username_label);
            this.Controls.Add(this.header_text);
            this.Name = "Login_form";
            this.Text = "Login Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label header_text;
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
    }
}

