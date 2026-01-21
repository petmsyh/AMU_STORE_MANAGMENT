using System;
using System.Windows.Forms;

namespace AMU.store.Mngt
{
    public class InventoryForm : Form
    {
        public InventoryForm()
        {
            this.Text = "Inventory - Current Stock";
            this.Load += new EventHandler(InventoryForm_Load);
            this.Width = 900;
            this.Height = 500;
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            this.Controls.Clear();
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
                Text = "Inventory Management - Current Stock Levels",
                Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            titlePanel.Controls.Add(titleLabel);

            var main = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(15) };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));

            var pnlTop = new FlowLayoutPanel { Dock = DockStyle.Fill, Height = 60, Padding = new Padding(5) };
            var txtSearch = new TextBox 
            { 
                Width = 350, 
                Name = "txtSearch",
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
            var btnExport = new Button 
            { 
                Text = "Export CSV",
                Width = 120,
                Height = 35,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnExport.FlatAppearance.BorderSize = 0;
            pnlTop.Controls.Add(txtSearch); pnlTop.Controls.Add(btnSearch); pnlTop.Controls.Add(btnRefresh); pnlTop.Controls.Add(btnExport);

            var dgv = new DataGridView 
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
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Property", HeaderText = "Property Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Quantity", Width = 150 });

            var status = new StatusStrip 
            { 
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))),
                Font = new System.Drawing.Font("Segoe UI", 9F)
            };
            var statusLabel = new ToolStripStatusLabel 
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

            Action loadAll = () =>
            {
                dgv.Rows.Clear();
                try
                {
                    using (var con = AMU.store.Mngt.Data.DbConnection.GetConnection())
                    using (var cmd = con.CreateCommand())
                    {
                        con.Open();
                        cmd.CommandText = "SELECT PropertyName, Quantity FROM Inventory";
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read()) dgv.Rows.Add(dr.IsDBNull(0) ? "" : dr.GetString(0), dr.IsDBNull(1) ? "0" : dr.GetInt32(1).ToString());
                        }
                    }
                    statusLabel.Text = $"Loaded {dgv.Rows.Count} items";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Load inventory error: " + ex.Message);
                    statusLabel.Text = "Error";
                }
            };

            btnRefresh.Click += (s, ev) => loadAll();
            btnSearch.Click += (s, ev) =>
            {
                var q = txtSearch.Text.Trim();
                dgv.Rows.Clear();
                if (string.IsNullOrEmpty(q)) { loadAll(); return; }
                using (var con = AMU.store.Mngt.Data.DbConnection.GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT PropertyName, Quantity FROM Inventory WHERE PropertyName LIKE @p";
                    cmd.Parameters.AddWithValue("@p", "%" + q + "%");
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) dgv.Rows.Add(dr.IsDBNull(0) ? "" : dr.GetString(0), dr.IsDBNull(1) ? "0" : dr.GetInt32(1).ToString());
                    }
                }
                statusLabel.Text = $"Filter: {q} - {dgv.Rows.Count} results";
            };

            btnExport.Click += (s, ev) =>
            {
                try
                {
                    using (var dlg = new SaveFileDialog { Filter = "CSV files (*.csv)|*.csv", FileName = "inventory.csv" })
                    {
                        if (dlg.ShowDialog() != DialogResult.OK) return;
                        using (var sw = new System.IO.StreamWriter(dlg.FileName))
                        {
                            sw.WriteLine("Property,Quantity");
                            foreach (DataGridViewRow r in dgv.Rows)
                            {
                                sw.WriteLine($"\"{r.Cells[0].Value}\",{r.Cells[1].Value}");
                            }
                        }
                    }
                    MessageBox.Show("Exported successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export error: " + ex.Message);
                }
            };

            // initial load
            loadAll();
        }
    }
}
