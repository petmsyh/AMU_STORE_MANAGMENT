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
                Text = "Purchase Requests Overview",
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

            var pnlTop = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            var txtSearch = new TextBox 
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
            pnlTop.Controls.Add(txtSearch); pnlTop.Controls.Add(btnSearch); pnlTop.Controls.Add(btnRefresh);

            dgv = new DataGridView 
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
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "RequestId", HeaderText = "ID", Width = 70 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Department", HeaderText = "Department", Width = 250 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Purpose", HeaderText = "Purpose", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.CellDoubleClick += Dgv_CellDoubleClick;

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

            main.Controls.Add(pnlTop, 0, 0);
            main.Controls.Add(dgv, 0, 1);
            main.Controls.Add(status, 0, 2);
            this.Controls.Add(main);
            this.Controls.Add(titlePanel);

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
