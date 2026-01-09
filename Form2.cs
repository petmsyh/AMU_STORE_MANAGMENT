using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMU.store.Mngt
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            // apply permissions after controls initialized
            this.Load += (s,e) => ApplyRolePermissions();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // show logged in user and current date
            toolStripStatusLabelUser.Text = $"Logged in as: {LoggedInUser.FullName ?? "(unknown)"} | Role: {LoggedInUser.Role ?? "(none)"}";
            toolStripStatusLabelDate.Text = $"Date: {DateTime.Now:yyyy-MM-dd}";
            // apply role permissions and open default view for the role
            ApplyRolePermissions();

            var role = LoggedInUser.Role ?? string.Empty;
            // open default child per role
            if (role == "Admin")
            {
                OpenChildForm(new RequestsForm());
            }
            else if (role == "Official")
            {
                // Officials create Model20 requests
                OpenChildForm(new Model20Form());
            }
            else if (role == "StoreMan")
            {
                // StoreMan manages receiving/issuing
                OpenChildForm(new Model19Form());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void requestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new RequestsForm());
        }

        private void model20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Model20Form());
        }

        private void model19ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Model19Form());
        }

        private void model22ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Model22Form());
        }

        private void menuUserManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new UserManagementForm());
        }

        private void menuApprovalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ApprovalForm());
        }

        private void menuPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open purchase link form
            OpenChildForm(new PurchaseLinkForm());
        }

        // new menu helper to open purchase items viewer
        private void OpenPurchaseItemsViewer()
        {
            OpenChildForm(new PurchaseItemsViewForm());
        }

        private void storeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new StoreForm());
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new InventoryForm());
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ReportsForm());
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // logout: show login form (create if missing) and close dashboard
            try
            {
                Login_form login = null;
                foreach (Form f in Application.OpenForms)
                {
                    if (f is Login_form lf)
                    {
                        login = lf;
                        break;
                    }
                }

                if (login == null)
                {
                    login = new Login_form();
                    login.StartPosition = FormStartPosition.CenterScreen;
                }

                // show login and close this dashboard
                login.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Logout error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenChildForm(Form child)
        {
            // simple MDI child hosting inside mainPanel
            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;
            // clear existing
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(child);
            child.Show();
        }

        private void ApplyRolePermissions()
        {
            var role = LoggedInUser.Role ?? string.Empty;
            // default hide admin menus
            menuUserManagementToolStripMenuItem.Visible = false;
            menuApprovalToolStripMenuItem.Visible = false;
            menuPurchaseToolStripMenuItem.Visible = false;
            reportsToolStripMenuItem.Visible = false;

            switch (role)
            {
                case "Admin":
                    menuUserManagementToolStripMenuItem.Visible = true;
                    menuApprovalToolStripMenuItem.Visible = true;
                    menuPurchaseToolStripMenuItem.Visible = true;
                    reportsToolStripMenuItem.Visible = true;
                    break;
                case "Official":
                    // Officials can create Model20 and request issues
                    menuUserManagementToolStripMenuItem.Visible = false;
                    menuApprovalToolStripMenuItem.Visible = false;
                    menuPurchaseToolStripMenuItem.Visible = false;
                    reportsToolStripMenuItem.Visible = false;
                    break;
                case "StoreMan":
                    // store man can receive and issue
                    menuUserManagementToolStripMenuItem.Visible = false;
                    menuApprovalToolStripMenuItem.Visible = false;
                    menuPurchaseToolStripMenuItem.Visible = false;
                    reportsToolStripMenuItem.Visible = false;
                    break;
            }
        }
    }
}
