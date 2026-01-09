using System;
using System.Windows.Forms;

namespace AMU.store.Mngt
{
    public class Model22Form : Form
    {
        private ComboBox cbRequests;
        private DataGridView dgvItems;
        private Button btnIssue;
        private TextBox txtIssuedTo;

        public Model22Form()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // subscribe to approvals
            AMU.store.Mngt.Events.EventBus.OnEvent += EventBus_OnEvent;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            AMU.store.Mngt.Events.EventBus.OnEvent -= EventBus_OnEvent;
        }

        private void EventBus_OnEvent(string name, object payload)
        {
            if (name == "RequestApproved")
            {
                // refresh UI on UI thread
                if (this.IsHandleCreated)
                    this.BeginInvoke(new Action(() => LoadRequests()));
            }
        }

        private void InitializeComponent()
        {
            this.Text = "Model 22 - Store Issuing";
            this.Dock = DockStyle.Fill;

            this.Padding = new Padding(8);
            cbRequests = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 320 };
            var btnRefresh = new Button { Text = "Refresh" };
            btnRefresh.Click += (s, e) => LoadRequests();

            var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40 }; top.Controls.Add(cbRequests); top.Controls.Add(btnRefresh);

            dgvItems = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "PropertyName", HeaderText = "Property", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "RequestedQty", HeaderText = "Requested", Width = 80, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "AvailableQty", HeaderText = "Available", Width = 80, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "IssueQty", HeaderText = "Issue", Width = 80 });

            txtIssuedTo = new TextBox { Width = 320 };
            btnIssue = new Button { Text = "Issue" };
            btnIssue.Click += BtnIssue_Click;

            var bottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 48, FlowDirection = FlowDirection.RightToLeft }; bottom.Controls.Add(btnIssue); bottom.Controls.Add(txtIssuedTo);

            this.Controls.Add(dgvItems);
            this.Controls.Add(top);
            this.Controls.Add(bottom);

            LoadRequests();
            cbRequests.SelectedIndexChanged += CbRequests_SelectedIndexChanged;
        }

        private void LoadRequests()
        {
            try
            {
                AMU.store.Mngt.Data.DbInitializer.EnsureSchema();
                using(var con = AMU.store.Mngt.Data.DbConnection.GetConnection())
                using(var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT RequestId, Department FROM Model20Requests WHERE Status='Approved'";
                    using(var dr = cmd.ExecuteReader())
                    {
                        while(dr.Read()) cbRequests.Items.Add(new ComboBoxItem { Id = dr.GetInt32(0), Text = dr.IsDBNull(1)?"":dr.GetString(1) });
                    }
                }
                if (cbRequests.Items.Count>0) cbRequests.SelectedIndex = 0;
            }
            catch{}
        }

        private void CbRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvItems.Rows.Clear();
            if (cbRequests.SelectedItem is ComboBoxItem it)
            {
                using(var con = AMU.store.Mngt.Data.DbConnection.GetConnection())
                using(var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT PropertyName, Quantity FROM Model20Items WHERE RequestId=@r";
                    cmd.Parameters.AddWithValue("@r", it.Id);
                    using(var dr = cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            var name = dr.GetString(0);
                            var req = dr.GetInt32(1);
                            int avail = 0;
                            using(var c2 = con.CreateCommand())
                            {
                                c2.CommandText = "SELECT Quantity FROM Inventory WHERE PropertyName=@n";
                                c2.Parameters.AddWithValue("@n", name);
                                var v = c2.ExecuteScalar();
                                avail = v==null?0:Convert.ToInt32(v);
                            }
                            dgvItems.Rows.Add(name, req.ToString(), avail.ToString(), req.ToString());
                        }
                    }
                }
            }
        }

        private void BtnIssue_Click(object sender, EventArgs e)
        {
            if (!(cbRequests.SelectedItem is ComboBoxItem it)) return;
            var items = new System.Collections.Generic.List<(string name,int qty)>();
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (!row.Visible) continue;
                var name = Convert.ToString(row.Cells[0].Value);
                var qty = int.TryParse(Convert.ToString(row.Cells[3].Value), out var q) ? q : 0;
                items.Add((name, qty));
            }

            try
            {
                var issueId = AMU.store.Mngt.Data.Model22Repository.CreateIssue(it.Id, LoggedInUser.UserId, txtIssuedTo.Text ?? "", items, null);
                MessageBox.Show($"Issued Id={issueId}", "Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Issue error: "+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class ComboBoxItem { public int Id; public string Text; public override string ToString() => Text; }
    }
}
