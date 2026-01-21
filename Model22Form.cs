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
            this.Text = "Model 22 - Property Issuing";
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
                Text = "Property Issue Form (Model 22)",
                Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            titlePanel.Controls.Add(titleLabel);

            this.Padding = new Padding(15);
            cbRequests = new ComboBox 
            { 
                DropDownStyle = ComboBoxStyle.DropDownList, 
                Width = 400,
                Font = new System.Drawing.Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat
            };
            
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
            btnRefresh.Click += (s, e) => LoadRequests();

            var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 60, Padding = new Padding(5) };
            top.Controls.Add(new Label 
            { 
                Text = "Select Request:", 
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(0, 8, 10, 0)
            });
            top.Controls.Add(cbRequests);
            top.Controls.Add(btnRefresh);

            var gridPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            var gridLabel = new Label
            {
                Text = "Items to Issue:",
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Dock = DockStyle.Top,
                Padding = new Padding(0, 0, 0, 10)
            };

            dgvItems = new DataGridView 
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
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "PropertyName", HeaderText = "Property", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "RequestedQty", HeaderText = "Requested", Width = 100, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "AvailableQty", HeaderText = "Available", Width = 100, ReadOnly = true });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "IssueQty", HeaderText = "Issue", Width = 100 });

            gridPanel.Controls.Add(dgvItems);
            gridPanel.Controls.Add(gridLabel);

            txtIssuedTo = new TextBox 
            { 
                Width = 350,
                Font = new System.Drawing.Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle
            };
            
            btnIssue = new Button 
            { 
                Text = "Issue Items",
                Width = 180,
                Height = 45,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnIssue.FlatAppearance.BorderSize = 0;
            btnIssue.Click += BtnIssue_Click;

            var bottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 70, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(5, 15, 5, 5) };
            bottom.Controls.Add(btnIssue);
            bottom.Controls.Add(txtIssuedTo);
            bottom.Controls.Add(new Label 
            { 
                Text = "Issued To:", 
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(0, 8, 10, 0)
            });

            this.Controls.Add(gridPanel);
            this.Controls.Add(top);
            this.Controls.Add(bottom);
            this.Controls.Add(titlePanel);

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
