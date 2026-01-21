using System;
using System.Windows.Forms;
using AMU.store.Mngt.Data;

namespace AMU.store.Mngt
{
    public class ApprovalForm : Form
    {
        private DataGridView dgvRequests;
        private Button btnApprove;
        private Button btnReject;
        private ToolStripStatusLabel statusLabel;

        public ApprovalForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Approve Purchase Requests";
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
                Text = "Approval Center - Review Purchase Requests",
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

            var top = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(5) };
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
            top.Controls.Add(txtSearch); top.Controls.Add(btnSearch); top.Controls.Add(btnRefresh);

            dgvRequests = new DataGridView 
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
            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn { Name = "RequestId", HeaderText = "ID", Width = 70 });
            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn { Name = "Department", HeaderText = "Department", Width = 250 });
            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn { Name = "Purpose", HeaderText = "Purpose", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            btnApprove = new Button 
            { 
                Text = "✓ Approve",
                Width = 140,
                Height = 45,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.Click += BtnApprove_Click;
            
            btnReject = new Button 
            { 
                Text = "✗ Reject",
                Width = 140,
                Height = 45,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnReject.FlatAppearance.BorderSize = 0;
            btnReject.Click += BtnReject_Click;

            var bottom = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(5, 15, 5, 5) };
            bottom.Controls.Add(btnApprove); bottom.Controls.Add(btnReject);

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

            var gridPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            gridPanel.Controls.Add(dgvRequests);
            gridPanel.Controls.Add(bottom);

            main.Controls.Add(top, 0, 0);
            main.Controls.Add(gridPanel, 0, 1);
            main.Controls.Add(status, 0, 2);
            this.Controls.Add(main);
            this.Controls.Add(titlePanel);

            btnSearch.Click += (s, e) => LoadPending(txtSearch.Text.Trim());
            btnRefresh.Click += (s, e) => LoadPending(null);

            LoadPending(null);
        }

        private void LoadPending(string filter)
        {
            dgvRequests.Rows.Clear();
            try
            {
                using(var con = DbConnection.GetConnection())
                using(var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT RequestId, Department, Purpose FROM Model20Requests WHERE Status='Pending'";
                    if (!string.IsNullOrEmpty(filter)) { cmd.CommandText += " AND (Department LIKE @f OR Purpose LIKE @f)"; cmd.Parameters.AddWithValue("@f", "%"+filter+"%"); }
                    using(var dr = cmd.ExecuteReader())
                    {
                        while(dr.Read()) dgvRequests.Rows.Add(dr.GetInt32(0), dr.IsDBNull(1)?"":dr.GetString(1), dr.IsDBNull(2)?"":dr.GetString(2));
                    }
                }
                statusLabel.Text = $"{dgvRequests.Rows.Count} pending";
            }
            catch(Exception ex) { MessageBox.Show("Load pending error: "+ex.Message); statusLabel.Text = "Error"; }
        }

        private void BtnApprove_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count==0) return;
            var id = Convert.ToInt32(dgvRequests.SelectedRows[0].Cells[0].Value);
            if (MessageBox.Show($"Approve request {id}?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try{
                using(var con = DbConnection.GetConnection())
                using(var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "UPDATE Model20Requests SET Status='Approved', ApprovedBy=@a, ApprovedAt=GETDATE() WHERE RequestId=@id";
                    cmd.Parameters.AddWithValue("@a", LoggedInUser.UserId);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                AMU.store.Mngt.Data.AuditRepository.Log("ApproveRequest", LoggedInUser.UserId, "Approved RequestId="+id);
                AMU.store.Mngt.Events.EventBus.Publish("RequestApproved", id);
                LoadPending(null);
                MessageBox.Show("Request approved.");
            }
            catch(Exception ex){ MessageBox.Show("Approve error: "+ex.Message); }
        }

        private void BtnReject_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count==0) return;
            var id = Convert.ToInt32(dgvRequests.SelectedRows[0].Cells[0].Value);
            if (MessageBox.Show($"Reject request {id}?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try{
                using(var con = DbConnection.GetConnection())
                using(var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "UPDATE Model20Requests SET Status='Rejected', ApprovedBy=@a, ApprovedAt=GETDATE() WHERE RequestId=@id";
                    cmd.Parameters.AddWithValue("@a", LoggedInUser.UserId);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                AMU.store.Mngt.Data.AuditRepository.Log("RejectRequest", LoggedInUser.UserId, "Rejected RequestId="+id);
                AMU.store.Mngt.Events.EventBus.Publish("RequestRejected", id);
                LoadPending(null);
                MessageBox.Show("Request rejected.");
            }
            catch(Exception ex){ MessageBox.Show("Reject error: "+ex.Message); }
        }
    }
}
