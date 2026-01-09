using System;
using System.Windows.Forms;
using AMU.store.Mngt.Data;

namespace AMU.store.Mngt
{
    public class UserManagementForm : Form
    {
        private DataGridView dgvUsers;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnToggleActive;
        private TextBox txtSearch;
        private ToolStripStatusLabel statusLabel;

        public UserManagementForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "User Management";
            this.Dock = DockStyle.Fill;
            this.Width = 900; this.Height = 520;

            var main = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3 };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));

            var top = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(6) };
            txtSearch = new TextBox { Width = 300 };
            var btnSearch = new Button { Text = "Search" };
            btnSearch.Click += (s, e) => LoadUsers(txtSearch.Text.Trim());
            var btnRefresh = new Button { Text = "Refresh" };
            btnRefresh.Click += (s, e) => LoadUsers(null);
            top.Controls.Add(txtSearch); top.Controls.Add(btnSearch); top.Controls.Add(btnRefresh);

            dgvUsers = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "UserId", HeaderText = "ID", Width = 60 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "FullName", HeaderText = "Full Name", Width = 220 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Username", HeaderText = "Username", Width = 160 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Role", HeaderText = "Role", Width = 120 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "IsActive", HeaderText = "Active", Width = 80 });

            var bottom = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(6) };
            btnAdd = new Button { Text = "Add" }; btnAdd.Click += BtnAdd_Click;
            btnEdit = new Button { Text = "Edit" }; btnEdit.Click += BtnEdit_Click;
            btnToggleActive = new Button { Text = "Activate/Deactivate" }; btnToggleActive.Click += BtnToggleActive_Click;
            bottom.Controls.Add(btnAdd); bottom.Controls.Add(btnEdit); bottom.Controls.Add(btnToggleActive);

            var status = new StatusStrip();
            statusLabel = new ToolStripStatusLabel { Text = "Ready" }; status.Items.Add(statusLabel);

            main.Controls.Add(top, 0, 0);
            main.Controls.Add(dgvUsers, 0, 1);
            main.Controls.Add(status, 0, 2);
            this.Controls.Add(main);

            LoadUsers(null);
        }

        private void LoadUsers(string filter)
        {
            dgvUsers.Rows.Clear();
            try
            {
                using(var con = DbConnection.GetConnection())
                using(var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT UserId, FullName, Username, Role, IsActive FROM Users";
                    if (!string.IsNullOrEmpty(filter)) { cmd.CommandText += " WHERE FullName LIKE @f OR Username LIKE @f OR Role LIKE @f"; cmd.Parameters.AddWithValue("@f", "%"+filter+"%"); }
                    using(var dr = cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            var id = dr.GetInt32(0);
                            var full = dr.IsDBNull(1)?"":dr.GetString(1);
                            var user = dr.IsDBNull(2)?"":dr.GetString(2);
                            var role = dr.IsDBNull(3)?"":dr.GetString(3);
                            var active = dr.IsDBNull(4)?false:dr.GetBoolean(4);
                            dgvUsers.Rows.Add(id, full, user, role, active?"Yes":"No");
                        }
                    }
                }
                statusLabel.Text = $"Loaded {dgvUsers.Rows.Count} users";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Load users error: "+ex.Message);
                statusLabel.Text = "Error";
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var dlg = new UserEditDialog();
            if (dlg.ShowDialog()==DialogResult.OK)
            {
                LoadUsers(null);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count==0) return;
            var id = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells[0].Value);
            var dlg = new UserEditDialog(id);
            if (dlg.ShowDialog()==DialogResult.OK) LoadUsers(null);
        }

        private void BtnToggleActive_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count==0) return;
            var id = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells[0].Value);
            try
            {
                using(var con = DbConnection.GetConnection())
                using(var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "UPDATE Users SET IsActive = CASE WHEN IsActive=1 THEN 0 ELSE 1 END WHERE UserId=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                LoadUsers(null);
            }
            catch(Exception ex){ MessageBox.Show("Toggle error: "+ex.Message); }
        }
    }
}
