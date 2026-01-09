using System;
using System.Windows.Forms;
using AMU.store.Mngt.Data;

namespace AMU.store.Mngt
{
    public class UserEditDialog : Form
    {
        private TextBox txtFull, txtUser, txtPass;
        private ComboBox cbRole;
        private Button btnOk;
        private CheckBox chkShowPass;
        private int? userId;

        public UserEditDialog(int? id = null)
        {
            userId = id;
            InitializeComponent();
            if (id.HasValue) LoadUser(id.Value);
        }

        private void InitializeComponent()
        {
            this.Text = userId.HasValue?"Edit User":"Add User";
            this.Width = 420; this.Height = 320;
            var main = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(8), RowCount = 5 };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            main.Controls.Add(new Label { Text = "Full Name:", AutoSize = true }, 0, 0);
            txtFull = new TextBox { Dock = DockStyle.Fill }; main.Controls.Add(txtFull, 1, 0);
            main.Controls.Add(new Label { Text = "Username:", AutoSize = true }, 0, 1);
            txtUser = new TextBox { Dock = DockStyle.Fill }; main.Controls.Add(txtUser, 1, 1);
            main.Controls.Add(new Label { Text = "Password:", AutoSize = true }, 0, 2);
            txtPass = new TextBox { Dock = DockStyle.Fill, UseSystemPasswordChar = true }; main.Controls.Add(txtPass, 1, 2);
            chkShowPass = new CheckBox { Text = "Show" }; chkShowPass.CheckedChanged += (s,e)=> txtPass.UseSystemPasswordChar = !chkShowPass.Checked; main.Controls.Add(chkShowPass, 2, 2);
            main.Controls.Add(new Label { Text = "Role:", AutoSize = true }, 0, 3);
            cbRole = new ComboBox { Dock = DockStyle.Fill }; cbRole.Items.AddRange(new object[]{"Admin","Official","StoreMan"}); cbRole.SelectedIndex = 0; main.Controls.Add(cbRole, 1, 3);

            btnOk = new Button { Text = "Save", Dock = DockStyle.Right }; btnOk.Click += BtnOk_Click; main.Controls.Add(btnOk, 1, 4);
            this.Controls.Add(main);
            this.AcceptButton = btnOk;
        }

        private void LoadUser(int id)
        {
            using(var con = DbConnection.GetConnection())
            using(var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = "SELECT FullName, Username, Role FROM Users WHERE UserId=@id";
                cmd.Parameters.AddWithValue("@id", id);
                using(var dr = cmd.ExecuteReader()){
                    if(dr.Read()){
                        txtFull.Text = dr.IsDBNull(0)?"":dr.GetString(0);
                        txtUser.Text = dr.IsDBNull(1)?"":dr.GetString(1);
                        cbRole.SelectedItem = dr.IsDBNull(2)?"":dr.GetString(2);
                    }
                }
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try{
                if (string.IsNullOrWhiteSpace(txtUser.Text)) { MessageBox.Show("Username is required"); return; }
                if (!userId.HasValue && string.IsNullOrEmpty(txtPass.Text)) { MessageBox.Show("Password is required for new user"); return; }
                using(var con = DbConnection.GetConnection())
                using(var cmd = con.CreateCommand())
                {
                    con.Open();
                    // check unique username for new users or when changed
                    cmd.CommandText = "SELECT COUNT(1) FROM Users WHERE Username=@u AND (@id IS NULL OR UserId<>@id)";
                    cmd.Parameters.AddWithValue("@u", txtUser.Text ?? "");
                    cmd.Parameters.AddWithValue("@id", userId.HasValue ? (object)userId.Value : DBNull.Value);
                    var exists = Convert.ToInt32(cmd.ExecuteScalar());
                    if (exists > 0) { MessageBox.Show("Username already exists"); return; }

                    cmd.Parameters.Clear();
                    if (userId.HasValue)
                    {
                        cmd.CommandText = "UPDATE Users SET FullName=@f, Username=@u, Role=@r WHERE UserId=@id";
                        cmd.Parameters.AddWithValue("@id", userId.Value);
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO Users(FullName, Username, PasswordHash, Role, IsActive) VALUES(@f,@u,@p,@r,1)";
                        cmd.Parameters.AddWithValue("@p", txtPass.Text ?? "");
                    }
                    cmd.Parameters.AddWithValue("@f", txtFull.Text ?? "");
                    cmd.Parameters.AddWithValue("@u", txtUser.Text ?? "");
                    cmd.Parameters.AddWithValue("@r", cbRole.SelectedItem?.ToString() ?? "");
                    cmd.ExecuteNonQuery();
                }
                this.DialogResult = DialogResult.OK; this.Close();
            }
            catch(Exception ex){ MessageBox.Show("Save error: "+ex.Message); }
        }
    }
}
