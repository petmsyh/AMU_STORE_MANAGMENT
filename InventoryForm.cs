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

            var main = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));

            var pnlTop = new FlowLayoutPanel { Dock = DockStyle.Fill, Height = 48, Padding = new Padding(8) };
            var txtSearch = new TextBox { Width = 300, Name = "txtSearch" };
            var btnSearch = new Button { Text = "Search", AutoSize = true };
            var btnRefresh = new Button { Text = "Refresh", AutoSize = true };
            var btnExport = new Button { Text = "Export CSV", AutoSize = true };
            pnlTop.Controls.Add(txtSearch); pnlTop.Controls.Add(btnSearch); pnlTop.Controls.Add(btnRefresh); pnlTop.Controls.Add(btnExport);

            var dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Property", HeaderText = "Property Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Quantity", Width = 120 });

            var status = new StatusStrip();
            var statusLabel = new ToolStripStatusLabel { Text = "Ready" };
            status.Items.Add(statusLabel);

            main.Controls.Add(pnlTop, 0, 0);
            main.Controls.Add(dgv, 0, 1);
            main.Controls.Add(status, 0, 2);
            this.Controls.Add(main);

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
