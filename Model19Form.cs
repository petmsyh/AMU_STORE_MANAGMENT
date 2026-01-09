using System;
using System.Windows.Forms;
using AMU.store.Mngt.Data;

namespace AMU.store.Mngt
{
    public class Model19Form : Form
    {
        private ComboBox cbPurchaseRef;
        private DataGridView dgvPurchasedItems;
        private CheckBox cbVerified;
        private Button btnReceive;

        public Model19Form()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Model 19 - Store Receiving";
            this.Dock = DockStyle.Fill;

            var main = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3 };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            var top = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(6) };
            top.Controls.Add(new Label { Text = "Purchase Reference:", AutoSize = true, TextAlign = System.Drawing.ContentAlignment.MiddleLeft });
            cbPurchaseRef = new ComboBox { Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            cbPurchaseRef.SelectedIndexChanged += (s, e) => LoadPurchaseItems();
            top.Controls.Add(cbPurchaseRef);

            dgvPurchasedItems = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            dgvPurchasedItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "PropertyName", HeaderText = "Property Name", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvPurchasedItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Quantity", ReadOnly = true, Width = 100 });

            cbVerified = new CheckBox { Text = "Quantity & Quality Verified", AutoSize = true };
            btnReceive = new Button { Text = "Receive Property", AutoSize = true };
            btnReceive.Click += BtnReceive_Click;

            var bottom = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(6) };
            bottom.Controls.Add(btnReceive);
            bottom.Controls.Add(cbVerified);

            main.Controls.Add(top, 0, 0);
            main.Controls.Add(dgvPurchasedItems, 0, 1);
            main.Controls.Add(bottom, 0, 2);
            this.Controls.Add(main);

            LoadPurchaseReferences();
        }

        private void BtnReceive_Click(object sender, EventArgs e)
        {
            if (!cbVerified.Checked)
            {
                MessageBox.Show("Please verify quantity and quality before receiving.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DbInitializer.EnsureSchema();
                DbSeeder.Seed();

                var items = new System.Collections.Generic.List<(string name, int qty)>();
                foreach (DataGridViewRow r in dgvPurchasedItems.Rows)
                {
                    var name = Convert.ToString(r.Cells[0].Value);
                    var qty = int.TryParse(Convert.ToString(r.Cells[1].Value), out var q) ? q : 0;
                    items.Add((name, qty));
                }

                Model19Repository.ReceivePurchase(cbPurchaseRef.SelectedItem?.ToString() ?? "", items);
                MessageBox.Show("Items received and inventory updated.", "Received", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Receive error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPurchaseReferences()
        {
            cbPurchaseRef.Items.Clear();
            try
            {
                using (var con = DbConnection.GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT Reference FROM Purchases ORDER BY PurchaseId DESC";
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) cbPurchaseRef.Items.Add(dr.IsDBNull(0) ? "" : dr.GetString(0));
                    }
                }
            }
            catch { }
            if (cbPurchaseRef.Items.Count > 0) cbPurchaseRef.SelectedIndex = 0;
        }

        private void LoadPurchaseItems()
        {
            dgvPurchasedItems.Rows.Clear();
            if (cbPurchaseRef.SelectedItem == null) return;
            try
            {
                using (var con = DbConnection.GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT PropertyName, Quantity FROM PurchaseItems pi JOIN Purchases p ON pi.PurchaseId=p.PurchaseId WHERE p.Reference=@r";
                    cmd.Parameters.AddWithValue("@r", cbPurchaseRef.SelectedItem.ToString());
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) dgvPurchasedItems.Rows.Add(dr.IsDBNull(0) ? "" : dr.GetString(0), dr.IsDBNull(1) ? 0 : dr.GetInt32(1));
                    }
                }
            }
            catch { }
        }
    }
}
