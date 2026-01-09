namespace AMU.store.Mngt
{
    partial class Form2
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.requestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUserManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuApprovalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPurchaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.model20ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.model22ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.model19ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            // add menu items safely (avoid nulls)
            {
                var _items = new System.Collections.Generic.List<System.Windows.Forms.ToolStripItem>();
                if (this.fileToolStripMenuItem != null) _items.Add(this.fileToolStripMenuItem);
                if (this.requestsToolStripMenuItem != null) _items.Add(this.requestsToolStripMenuItem);
                if (this.menuUserManagementToolStripMenuItem != null) _items.Add(this.menuUserManagementToolStripMenuItem);
                if (this.menuApprovalToolStripMenuItem != null) _items.Add(this.menuApprovalToolStripMenuItem);
                if (this.menuPurchaseToolStripMenuItem != null) _items.Add(this.menuPurchaseToolStripMenuItem);
                if (this.model20ToolStripMenuItem != null) _items.Add(this.model20ToolStripMenuItem);
                if (this.model22ToolStripMenuItem != null) _items.Add(this.model22ToolStripMenuItem);
                if (this.model19ToolStripMenuItem != null) _items.Add(this.model19ToolStripMenuItem);
                // Purchase viewer menu (next to Purchase)
                // we will reuse menuPurchaseToolStripMenuItem click to open link form; add a viewer trigger via right click or additional menu if desired
                if (this.menuPurchaseToolStripMenuItem != null) _items.Add(this.menuPurchaseToolStripMenuItem);
                if (this.storeToolStripMenuItem != null) _items.Add(this.storeToolStripMenuItem);
                if (this.inventoryToolStripMenuItem != null) _items.Add(this.inventoryToolStripMenuItem);
                if (this.reportsToolStripMenuItem != null) _items.Add(this.reportsToolStripMenuItem);
                if (this.logoutToolStripMenuItem != null) _items.Add(this.logoutToolStripMenuItem);
                this.menuStrip1.Items.AddRange(_items.ToArray());
            }
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // requestsToolStripMenuItem
            // 
            this.requestsToolStripMenuItem.Name = "requestsToolStripMenuItem";
            this.requestsToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.requestsToolStripMenuItem.Text = "Requests";
            this.requestsToolStripMenuItem.Click += new System.EventHandler(this.requestsToolStripMenuItem_Click);
            // 
            // menuUserManagementToolStripMenuItem
            // 
            this.menuUserManagementToolStripMenuItem.Name = "menuUserManagementToolStripMenuItem";
            this.menuUserManagementToolStripMenuItem.Size = new System.Drawing.Size(120, 24);
            this.menuUserManagementToolStripMenuItem.Text = "User Management";
            this.menuUserManagementToolStripMenuItem.Click += new System.EventHandler(this.menuUserManagementToolStripMenuItem_Click);
            // 
            // menuApprovalToolStripMenuItem
            // 
            this.menuApprovalToolStripMenuItem.Name = "menuApprovalToolStripMenuItem";
            this.menuApprovalToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.menuApprovalToolStripMenuItem.Text = "Approval";
            this.menuApprovalToolStripMenuItem.Click += new System.EventHandler(this.menuApprovalToolStripMenuItem_Click);
            // 
            // menuPurchaseToolStripMenuItem
            // 
            this.menuPurchaseToolStripMenuItem.Name = "menuPurchaseToolStripMenuItem";
            this.menuPurchaseToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.menuPurchaseToolStripMenuItem.Text = "Purchase";
            this.menuPurchaseToolStripMenuItem.Click += new System.EventHandler(this.menuPurchaseToolStripMenuItem_Click);
            // add context menu to open viewer on right-click
            this.menuPurchaseToolStripMenuItem.MouseDown += (s,e)=>{
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    var f = (AMU.store.Mngt.Form2) this.FindForm();
                    if (f!=null) f.BeginInvoke((System.Windows.Forms.MethodInvoker)(delegate { f.OpenChildForm(new PurchaseItemsViewForm()); }));
                }
            };
            // 
            // storeToolStripMenuItem
            // 
            this.storeToolStripMenuItem.Name = "storeToolStripMenuItem";
            this.storeToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.storeToolStripMenuItem.Text = "Store";
            this.storeToolStripMenuItem.Click += new System.EventHandler(this.storeToolStripMenuItem_Click);
            // 
            // model20ToolStripMenuItem
            // 
            this.model20ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.model20ToolStripMenuItem.Name = "model20ToolStripMenuItem";
            this.model20ToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.model20ToolStripMenuItem.Text = "Model 20";
            this.model20ToolStripMenuItem.Click += new System.EventHandler(this.model20ToolStripMenuItem_Click);
            // 
            // model22ToolStripMenuItem
            // 
            this.model22ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.model22ToolStripMenuItem.Name = "model22ToolStripMenuItem";
            this.model22ToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.model22ToolStripMenuItem.Text = "Model 22";
            this.model22ToolStripMenuItem.Click += new System.EventHandler(this.model22ToolStripMenuItem_Click);
            // 
            // model19ToolStripMenuItem
            // 
            this.model19ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.model19ToolStripMenuItem.Name = "model19ToolStripMenuItem";
            this.model19ToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.model19ToolStripMenuItem.Text = "Model 19";
            this.model19ToolStripMenuItem.Click += new System.EventHandler(this.model19ToolStripMenuItem_Click);
            // 
            // inventoryToolStripMenuItem
            // 
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            this.inventoryToolStripMenuItem.Size = new System.Drawing.Size(78, 24);
            this.inventoryToolStripMenuItem.Text = "Inventory";
            this.inventoryToolStripMenuItem.Click += new System.EventHandler(this.inventoryToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.reportsToolStripMenuItem.Text = "Reports";
            this.reportsToolStripMenuItem.Click += new System.EventHandler(this.reportsToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            // add status labels safely
            {
                var _s = new System.Collections.Generic.List<System.Windows.Forms.ToolStripItem>();
                if (this.toolStripStatusLabelUser != null) _s.Add(this.toolStripStatusLabelUser);
                if (this.toolStripStatusLabelDate != null) _s.Add(this.toolStripStatusLabelDate);
                this.statusStrip1.Items.AddRange(_s.ToArray());
            }
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelUser
            // 
            this.toolStripStatusLabelUser.Name = "toolStripStatusLabelUser";
            this.toolStripStatusLabelUser.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelUser.Text = "Logged in as: (none)";
            // 
            // toolStripStatusLabelDate
            // 
            this.toolStripStatusLabelDate.Name = "toolStripStatusLabelDate";
            this.toolStripStatusLabelDate.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelDate.Text = "Date: 0000-00-00";
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 28);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(800, 400);
            this.mainPanel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "main dashboard";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.Text = "Main dashboard";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem requestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem model20ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem model19ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem model22ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuUserManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuApprovalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuPurchaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelUser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDate;
        private System.Windows.Forms.Panel mainPanel;
    }
}