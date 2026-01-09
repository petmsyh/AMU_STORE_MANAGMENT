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
            this.Text = "Record Purchase from Approved Request";
            this.Dock = DockStyle.Fill;
            this.Width = 900; this.Height = 500;

            var main = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3 };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));

            var top = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(6) };
            top.Controls.Add(new Label { Text = "Select Approved Request:", AutoSize = true });
            cbApprovedRequests = new ComboBox { Width = 360, DropDownStyle = ComboBoxStyle.DropDownList };
            var btnRefresh = new Button { Text = "Refresh" }; btnRefresh.Click += (s, e) => LoadApprovedRequests();
            top.Controls.Add(cbApprovedRequests); top.Controls.Add(btnRefresh);

            var mid = new Panel { Dock = DockStyle.Fill };
            dgvItems = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "PropertyName", HeaderText = "Property", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Quantity", Width = 100 });
            mid.Controls.Add(dgvItems);

            var bottom = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(6) };
            bottom.Controls.Add(new Label { Text = "Purchase Reference:" });
            txtPurchaseRef = new TextBox { Width = 200 };
            btnGenerate = new Button { Text = "Generate Purchase" };
            btnGenerate.Click += BtnGenerate_Click;
            bottom.Controls.Add(txtPurchaseRef); bottom.Controls.Add(btnGenerate);

            var status = new StatusStrip();
            statusLabel = new ToolStripStatusLabel { Text = "Ready" }; status.Items.Add(statusLabel);

            main.Controls.Add(top, 0, 0);
            main.Controls.Add(mid, 0, 1);
            main.Controls.Add(bottom, 0, 2);
            this.Controls.Add(main);
            this.Controls.Add(status);

            LoadApprovedRequests();
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
