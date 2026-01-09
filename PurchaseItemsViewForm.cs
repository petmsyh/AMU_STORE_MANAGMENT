using System;
using System.Drawing;
using System.Windows.Forms;
using AMU.store.Mngt.Data;

namespace AMU.store.Mngt
{
    public class PurchaseItemsViewForm : Form
    {
        private ComboBox cbPurchases;
        private DataGridView dgvItems;
        private Button btnReceiveSelected;
        private Button btnReceiveAll;
        private TextBox tbSearch;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolTip toolTip;

        public PurchaseItemsViewForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Purchase Items";
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(10);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            toolTip = new ToolTip();

            var main = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 4 };
            main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30)); // header
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30)); // search
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // grid
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30)); // buttons/status

            var lbl = new Label { Text = "Select Purchase:", Anchor = AnchorStyles.Left, AutoSize = true, TextAlign = ContentAlignment.MiddleLeft };
            cbPurchases = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cbPurchases.Margin = new Padding(3, 3, 3, 3);

            main.Controls.Add(lbl, 0, 0);
            main.SetColumnSpan(lbl, 1);
            main.Controls.Add(cbPurchases, 0, 1);

            tbSearch = new TextBox { Dock = DockStyle.Fill };
            toolTip.SetToolTip(tbSearch, "Type to filter properties by name");
            // Note: PlaceholderText is not available on .NET Framework 4.7.2; keep tooltip instead
            main.Controls.Add(tbSearch, 1, 1);

            btnReceiveSelected = new Button { Text = "Receive Selected", Dock = DockStyle.Fill };
            btnReceiveAll = new Button { Text = "Receive All", Dock = DockStyle.Fill }; 
            toolTip.SetToolTip(btnReceiveSelected, "Receive only selected rows and update inventory");
            toolTip.SetToolTip(btnReceiveAll, "Receive all displayed items and update inventory");

            main.Controls.Add(btnReceiveSelected, 1, 3);
            main.Controls.Add(btnReceiveAll, 2, 3);

            dgvItems = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            dgvItems.RowHeadersVisible = false;
            dgvItems.AllowUserToResizeRows = false;
            dgvItems.BackgroundColor = Color.White;
            dgvItems.DefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 153, 255);
            dgvItems.DefaultCellStyle.SelectionForeColor = Color.White;

            // columns
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "PropertyName", HeaderText = "Property", ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderedQty", HeaderText = "Ordered", ReadOnly = true, Width = 80 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReceivedQty", HeaderText = "Received", ReadOnly = false, Width = 80 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "QualityNotes", HeaderText = "Quality Notes", ReadOnly = false });

            main.Controls.Add(dgvItems, 0, 2);
            main.SetColumnSpan(dgvItems, 3);

            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel { Text = "Ready" };
            statusStrip.Items.Add(statusLabel);
            main.Controls.Add(statusStrip, 0, 3);
            main.SetColumnSpan(statusStrip, 1);

            this.Controls.Add(main);

            // events
            cbPurchases.SelectedIndexChanged += CbPurchases_SelectedIndexChanged;
            btnReceiveSelected.Click += BtnReceiveSelected_Click;
            btnReceiveAll.Click += BtnReceiveAll_Click;
            tbSearch.TextChanged += TbSearch_TextChanged;
            dgvItems.CellValidating += DgvItems_CellValidating;

            LoadPurchases();
        }

        private void LoadPurchases()
        {
            cbPurchases.Items.Clear();
            try
            {
                using (var con = DbConnection.GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT PurchaseId, Reference FROM Purchases ORDER BY PurchaseId DESC";
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) cbPurchases.Items.Add(new ComboBoxItem { Id = dr.GetInt32(0), Text = dr.IsDBNull(1) ? "" : dr.GetString(1) });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading purchases: " + ex.Message);
            }
            if (cbPurchases.Items.Count > 0) cbPurchases.SelectedIndex = 0;
        }

        private void CbPurchases_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshItemsGrid();
        }

        private void RefreshItemsGrid()
        {
            dgvItems.Rows.Clear();
            if (!(cbPurchases.SelectedItem is ComboBoxItem it)) return;
            try
            {
                using (var con = DbConnection.GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT PropertyName, Quantity FROM PurchaseItems WHERE PurchaseId=@p";
                    cmd.Parameters.AddWithValue("@p", it.Id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var name = dr.IsDBNull(0) ? "" : dr.GetString(0);
                            var qty = dr.IsDBNull(1) ? 0 : dr.GetInt32(1);
                            dgvItems.Rows.Add(name, qty.ToString(), qty.ToString(), string.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading items: " + ex.Message);
            }
            ApplyFilter();
        }

        private void TbSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var filter = (tbSearch.Text ?? string.Empty).Trim();
            for (int i = 0; i < dgvItems.Rows.Count; i++)
            {
                var cell = dgvItems.Rows[i].Cells[0].Value as string ?? string.Empty;
                dgvItems.Rows[i].Visible = string.IsNullOrEmpty(filter) || cell.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0;
            }
        }

        private void BtnReceiveSelected_Click(object sender, EventArgs e)
        {
            if (!(cbPurchases.SelectedItem is ComboBoxItem it)) return;
            var rows = dgvItems.SelectedRows;
            if (rows.Count == 0) { MessageBox.Show("Select rows to receive."); return; }
            var items = new System.Collections.Generic.List<(string name, int qty)>();
            foreach (DataGridViewRow row in rows)
            {
                var name = Convert.ToString(row.Cells[0].Value);
                int qty = 0; int.TryParse(Convert.ToString(row.Cells[2].Value), out qty);
                items.Add((name, qty));
            }
            ReceiveAndNotify(it.Text, items);
        }

        private void BtnReceiveAll_Click(object sender, EventArgs e)
        {
            if (!(cbPurchases.SelectedItem is ComboBoxItem it)) return;
            var items = new System.Collections.Generic.List<(string name, int qty)>();
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (!row.Visible) continue;
                var name = Convert.ToString(row.Cells[0].Value);
                int qty = 0; int.TryParse(Convert.ToString(row.Cells[2].Value), out qty);
                items.Add((name, qty));
            }
            if (items.Count == 0) { MessageBox.Show("No items to receive."); return; }
            ReceiveAndNotify(it.Text, items);
        }

        private void ReceiveAndNotify(string purchaseRef, System.Collections.Generic.List<(string name, int qty)> items)
        {
            btnReceiveSelected.Enabled = btnReceiveAll.Enabled = false;
            statusLabel.Text = "Processing...";
            try
            {
                Model19Repository.ReceivePurchase(purchaseRef, items);
                statusLabel.Text = "Done";
                MessageBox.Show("Received items and updated inventory.");
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Error";
                MessageBox.Show("Receive error: " + ex.Message);
            }
            finally
            {
                btnReceiveSelected.Enabled = btnReceiveAll.Enabled = true;
            }
        }

        private void DgvItems_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // validate ReceivedQty column (index 2)
            if (dgvItems.Columns[e.ColumnIndex].Name == "ReceivedQty")
            {
                if (!int.TryParse(Convert.ToString(e.FormattedValue), out var v) || v < 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("Received quantity must be a non-negative integer.");
                }
            }
        }

        private class ComboBoxItem { public int Id; public string Text; public override string ToString() => Text; }
    }
}
