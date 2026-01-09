using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using AMU.store.Mngt.Data;

namespace AMU.store.Mngt
{
    public partial class Login_form : Form
    {
        public Login_form()
        {
            InitializeComponent();

            // Ensure the button is wired (designer may not have hooked it)
            this.login_btn.Click += btnLogin_Click;

            // Hide password characters
            this.password_input.UseSystemPasswordChar = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = username_input.Text.Trim();
            var password = password_input.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var con = DbConnection.GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT UserId, FullName, Role
                        FROM Users
                        WHERE Username = @username
                          AND PasswordHash = @password
                          AND IsActive = 1";
                    // add parameters safely (avoid AddWithValue null argument issues)
                    var pUser = cmd.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 50);
                    pUser.Value = (object)username ?? DBNull.Value;
                    var pPass = cmd.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 255);
                    pPass.Value = (object)password ?? DBNull.Value;

                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            // Read user info
                            var userId = dr.GetInt32(0);
                            var fullName = dr.IsDBNull(1) ? string.Empty : dr.GetString(1);
                            var role = dr.IsDBNull(2) ? string.Empty : dr.GetString(2);

                            // Store globally if needed
                            LoggedInUser.UserId = userId;
                            LoggedInUser.FullName = fullName;
                            LoggedInUser.Role = role;

                            // Open main dashboard (Form2)
                            this.Hide();
                            var dashboard = new Form2();
                            // When dashboard closes, show the login form again (do not exit application)
                            dashboard.FormClosed += (s, args) =>
                            {
                                // clear sensitive state
                                LoggedInUser.UserId = 0;
                                LoggedInUser.FullName = null;
                                LoggedInUser.Role = null;

                                // clear inputs and show login
                                try
                                {
                                    username_input.Text = string.Empty;
                                    password_input.Text = string.Empty;
                                }
                                catch { }

                                this.Show();
                            };
                            dashboard.Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Show full exception details for debugging
                var debug = new System.Text.StringBuilder();
                debug.AppendLine("Exception:");
                debug.AppendLine(ex.ToString());
                debug.AppendLine();
                try
                {
                    debug.AppendLine($"username_input is null: {username_input == null}");
                    debug.AppendLine($"password_input is null: {password_input == null}");
                    debug.AppendLine($"username length: {(username == null ? "<null>" : username.Length.ToString())}");
                    debug.AppendLine($"password length: {(password == null ? "<null>" : password.Length.ToString())}");
                }
                catch { }

                MessageBox.Show(debug.ToString(), "Login error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }   
}
