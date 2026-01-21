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
            this.Text = "Model 19 - Property Receiving";
            this.Dock = DockStyle.Fill;
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
                Text = "Property Receiving Form (Model 19)",
                Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            titlePanel.Controls.Add(titleLabel);

            var main = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, Padding = new Padding(15) };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));

            var top = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            top.Controls.Add(new Label 
            { 
                Text = "Purchase Reference:", 
                AutoSize = true, 
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(0, 8, 10, 0)
            });
            cbPurchaseRef = new ComboBox 
            { 
                Width = 300, 
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new System.Drawing.Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat
            };
            cbPurchaseRef.SelectedIndexChanged += (s, e) => LoadPurchaseItems();
            top.Controls.Add(cbPurchaseRef);

            var gridPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            var gridLabel = new Label
            {
                Text = "Items to Receive:",
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Dock = DockStyle.Top,
                Padding = new Padding(0, 0, 0, 10)
            };

            dgvPurchasedItems = new DataGridView 
            { 
                Dock = DockStyle.Fill, 
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
            dgvPurchasedItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "PropertyName", HeaderText = "Property Name", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvPurchasedItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Quantity", ReadOnly = true, Width = 120 });

            gridPanel.Controls.Add(dgvPurchasedItems);
            gridPanel.Controls.Add(gridLabel);

            cbVerified = new CheckBox 
            { 
                Text = "✓ Quantity and Quality Verified", 
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69))))
            };
            
            btnReceive = new Button 
            { 
                Text = "Receive Property",
                Width = 180,
                Height = 45,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnReceive.FlatAppearance.BorderSize = 0;
            btnReceive.Click += BtnReceive_Click;

            var bottom = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(5, 15, 5, 5) };
            bottom.Controls.Add(btnReceive);
            bottom.Controls.Add(cbVerified);

            main.Controls.Add(top, 0, 0);
            main.Controls.Add(gridPanel, 0, 1);
            main.Controls.Add(bottom, 0, 2);
            this.Controls.Add(main);
            this.Controls.Add(titlePanel);

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
