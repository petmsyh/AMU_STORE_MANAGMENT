using System;
using System.Windows.Forms;
using AMU.store.Mngt.Data;

namespace AMU.store.Mngt
{
    public class PurchaseLinkForm : Form
    {
        private ComboBox cbApprovedRequests;
        private TextBox txtPurchaseRef;
        private Button btnGenerate;
        private DataGridView dgvItems;
        private ToolStripStatusLabel statusLabel;

        public PurchaseLinkForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Record Purchase from Approved Request";
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
                Text = "Record Purchase from Approved Request",
                Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            titlePanel.Controls.Add(titleLabel);

            var main = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 4, Padding = new Padding(15) };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));

            var top = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            top.Controls.Add(new Label 
            { 
                Text = "Select Approved Request:", 
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(0, 8, 10, 0)
            });
            cbApprovedRequests = new ComboBox 
            { 
                Width = 400, 
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new System.Drawing.Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat
            };
            cbApprovedRequests.SelectedIndexChanged += (s, e) => LoadRequestItems();
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
            btnRefresh.Click += (s, e) => LoadApprovedRequests();
            top.Controls.Add(cbApprovedRequests); top.Controls.Add(btnRefresh);

            var gridPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            var gridLabel = new Label
            {
                Text = "Items in Request:",
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Dock = DockStyle.Top,
                Padding = new Padding(0, 0, 0, 10)
            };

            dgvItems = new DataGridView 
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
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "PropertyName", HeaderText = "Property", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Quantity", Width = 120 });

            gridPanel.Controls.Add(dgvItems);
            gridPanel.Controls.Add(gridLabel);

            var bottom = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(5, 15, 5, 5) };
            bottom.Controls.Add(new Label 
            { 
                Text = "Purchase Reference:", 
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(0, 12, 10, 0)
            });
            txtPurchaseRef = new TextBox 
            { 
                Width = 250,
                Font = new System.Drawing.Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle
            };
            btnGenerate = new Button 
            { 
                Text = "Generate Purchase",
                Width = 180,
                Height = 45,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnGenerate.FlatAppearance.BorderSize = 0;
            btnGenerate.Click += BtnGenerate_Click;
            bottom.Controls.Add(txtPurchaseRef); bottom.Controls.Add(btnGenerate);

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

            main.Controls.Add(top, 0, 0);
            main.Controls.Add(gridPanel, 0, 1);
            main.Controls.Add(bottom, 0, 2);
            main.Controls.Add(status, 0, 3);
            this.Controls.Add(main);
            this.Controls.Add(titlePanel);

            LoadApprovedRequests();
        }

        private void LoadRequestItems()
        {
            dgvItems.Rows.Clear();
            if (!(cbApprovedRequests.SelectedItem is ComboBoxItem it)) return;
            try
            {
                using (var con = DbConnection.GetConnection())
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT PropertyName, Quantity FROM Model20Items WHERE RequestId=@r";
                    cmd.Parameters.AddWithValue("@r", it.Id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            dgvItems.Rows.Add(dr.IsDBNull(0) ? "" : dr.GetString(0), dr.IsDBNull(1) ? 0 : dr.GetInt32(1));
                        }
                    }
                }
            }
            catch { }
        }

        private void LoadApprovedRequests()
        {
            cbApprovedRequests.Items.Clear();
            using(var con = DbConnection.GetConnection())
            using(var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = "SELECT RequestId, Department FROM Model20Requests WHERE Status='Approved'";
                using(var dr = cmd.ExecuteReader())
                {
                    while(dr.Read()) cbApprovedRequests.Items.Add(new ComboBoxItem { Id = dr.GetInt32(0), Text = dr.IsDBNull(1)?"":dr.GetString(1) });
                }
            }
            if (cbApprovedRequests.Items.Count>0) cbApprovedRequests.SelectedIndex = 0;
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            if (!(cbApprovedRequests.SelectedItem is ComboBoxItem it)) return;
            var purchaseRef = txtPurchaseRef.Text.Trim();
            if (string.IsNullOrEmpty(purchaseRef)) { MessageBox.Show("Enter purchase reference"); return; }
            // if empty, auto-generate simple ref
            if (string.IsNullOrEmpty(purchaseRef)) purchaseRef = "PR-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                // create purchase and items from request into PurchaseItems and Purchases
                using(var con = DbConnection.GetConnection())
                using(var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "INSERT INTO Purchases(Reference) VALUES(@r); SELECT SCOPE_IDENTITY();";
                    cmd.Parameters.AddWithValue("@r", purchaseRef);
                    var id = cmd.ExecuteScalar();
                    var purchaseId = Convert.ToInt32(id);

                    // copy items
                    using(var c2 = con.CreateCommand())
                    {
                        c2.CommandText = "INSERT INTO PurchaseItems(PurchaseId, PropertyName, Quantity) SELECT @p, PropertyName, Quantity FROM Model20Items WHERE RequestId=@req";
                        c2.Parameters.AddWithValue("@p", purchaseId);
                        c2.Parameters.AddWithValue("@req", it.Id);
                        c2.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Purchase recorded.");
            }
            catch(Exception ex) { MessageBox.Show("Error: "+ex.Message); }
        }

        private class ComboBoxItem { public int Id; public string Text; public override string ToString() => Text; }
    }
}
