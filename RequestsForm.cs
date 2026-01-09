using System;
using System.Data;
using System.Windows.Forms;

namespace AMU.store.Mngt
{
    public class RequestsForm : Form
    {
        public RequestsForm()
        {
            this.Text = "Requests";
            this.Load += RequestsForm_Load;
            this.Width = 900;
            this.Height = 500;
        }

        private DataGridView dgv;
        private ToolStripStatusLabel statusLabel;

        private void RequestsForm_Load(object sender, EventArgs e)
        {
            var main = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3 };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));

            var pnlTop = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(6) };
            var txtSearch = new TextBox { Width = 300 };
            var btnSearch = new Button { Text = "Search" };
            var btnRefresh = new Button { Text = "Refresh" };
            pnlTop.Controls.Add(txtSearch); pnlTop.Controls.Add(btnSearch); pnlTop.Controls.Add(btnRefresh);

            dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "RequestId", HeaderText = "ID", Width = 60 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Department", HeaderText = "Department", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Purpose", HeaderText = "Purpose", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.CellDoubleClick += Dgv_CellDoubleClick;

            var status = new StatusStrip();
            statusLabel = new ToolStripStatusLabel { Text = "Ready" };
            status.Items.Add(statusLabel);

            main.Controls.Add(pnlTop, 0, 0);
            main.Controls.Add(dgv, 0, 1);
            main.Controls.Add(status, 0, 2);
            this.Controls.Add(main);

            btnSearch.Click += (s, ev) => LoadRequests(txtSearch.Text.Trim());
            btnRefresh.Click += (s, ev) => LoadRequests(null);

            LoadRequests(null);
        }

        private void LoadRequests(string filter)
        {
            dgv.Rows.Clear();
            try
            {
                AMU.store.Mngt.Data.DbInitializer.EnsureSchema();
                using (var con = Data.DbConnection.GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT RequestId, Department, Purpose FROM Model20Requests WHERE Status='Pending'";
                    if (!string.IsNullOrEmpty(filter)) cmd.CommandText += " AND (Department LIKE @f OR Purpose LIKE @f)";
                    if (!string.IsNullOrEmpty(filter)) cmd.Parameters.AddWithValue("@f", "%" + filter + "%");
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            dgv.Rows.Add(dr.GetInt32(0), dr.IsDBNull(1) ? "" : dr.GetString(1), dr.IsDBNull(2) ? "" : dr.GetString(2));
                        }
                    }
                }
                statusLabel.Text = $"{dgv.Rows.Count} pending requests";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load requests error: " + ex.Message);
                statusLabel.Text = "Error";
            }
        }

        private void Dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[0].Value);
            var dlg = new Form { Text = "Request " + id, Width = 600, Height = 400 };
            var tb = new TextBox { Multiline = true, Dock = DockStyle.Fill, ReadOnly = true };
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            using (var con = Data.DbConnection.GetConnection())
            using (var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = "SELECT PropertyName, Quantity FROM Model20Items WHERE RequestId=@r";
                cmd.Parameters.AddWithValue("@r", id);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) sb.AppendLine($"{dr.GetString(0)} - {dr.GetInt32(1)}");
                }
            }
            tb.Text = sb.ToString();
            dlg.Controls.Add(tb);
            dlg.ShowDialog();
        }
    }
}
