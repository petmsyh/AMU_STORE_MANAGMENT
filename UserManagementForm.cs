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
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            // Title Panel
            var titlePanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))),
                Padding = new Padding(20)
            };
            var titleLabel = new Label
            {
                Text = "User Management - Manage System Users",
                Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            titlePanel.Controls.Add(titleLabel);

            var main = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, Padding = new Padding(15) };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));

            var top = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            txtSearch = new TextBox 
            { 
                Width = 350,
                Font = new System.Drawing.Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle
            };
            var btnSearch = new Button 
            { 
                Text = "Search",
                Width = 100,
                Height = 35,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += (s, e) => LoadUsers(txtSearch.Text.Trim());
            var btnRefresh = new Button 
            { 
                Text = "Refresh",
                Width = 100,
                Height = 35,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) => LoadUsers(null);
            top.Controls.Add(txtSearch); top.Controls.Add(btnSearch); top.Controls.Add(btnRefresh);

            dgvUsers = new DataGridView 
            { 
                Dock = DockStyle.Fill, 
                ReadOnly = true, 
                AllowUserToAddRows = false, 
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = System.Drawing.Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230))))),
                EnableHeadersVisualStyles = false,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))),
                    ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))),
                    Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                    Padding = new Padding(10, 5, 10, 5),
                    SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))),
                    SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))),
                },
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new System.Drawing.Font("Segoe UI", 9.75F),
                    SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))),
                    SelectionForeColor = System.Drawing.Color.White
                }
            };
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "UserId", HeaderText = "ID", Width = 70 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "FullName", HeaderText = "Full Name", Width = 220 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Username", HeaderText = "Username", Width = 160 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Role", HeaderText = "Role", Width = 120 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "IsActive", HeaderText = "Active", Width = 80 });

            var bottom = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(5, 15, 5, 5) };
            btnAdd = new Button 
            { 
                Text = "+ Add User",
                Width = 140,
                Height = 45,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAdd_Click;
            btnEdit = new Button 
            { 
                Text = "Edit",
                Width = 100,
                Height = 45,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += BtnEdit_Click;
            btnToggleActive = new Button 
            { 
                Text = "Toggle Active",
                Width = 140,
                Height = 45,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnToggleActive.FlatAppearance.BorderSize = 0;
            btnToggleActive.Click += BtnToggleActive_Click;
            bottom.Controls.Add(btnAdd); bottom.Controls.Add(btnEdit); bottom.Controls.Add(btnToggleActive);

            var status = new StatusStrip 
            { 
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))),
                Font = new System.Drawing.Font("Segoe UI", 9F)
            };
            statusLabel = new ToolStripStatusLabel 
            { 
                Text = "Ready",
                ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64))))
            };
            status.Items.Add(statusLabel);

            var gridPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            gridPanel.Controls.Add(dgvUsers);
            gridPanel.Controls.Add(bottom);

            main.Controls.Add(top, 0, 0);
            main.Controls.Add(gridPanel, 0, 1);
            main.Controls.Add(status, 0, 2);
            this.Controls.Add(main);
            this.Controls.Add(titlePanel);

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
