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
            this.Text = "Approve Requests";
            this.Dock = DockStyle.Fill;
            this.Width = 900; this.Height = 520;

            var main = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3 };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));

            var top = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(6) };
            var txtSearch = new TextBox { Width = 300 };
            var btnSearch = new Button { Text = "Search" };
            var btnRefresh = new Button { Text = "Refresh" };
            top.Controls.Add(txtSearch); top.Controls.Add(btnSearch); top.Controls.Add(btnRefresh);

            dgvRequests = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn { Name = "RequestId", HeaderText = "ID", Width = 60 });
            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn { Name = "Department", HeaderText = "Department", Width = 200 });
            dgvRequests.Columns.Add(new DataGridViewTextBoxColumn { Name = "Purpose", HeaderText = "Purpose", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            btnApprove = new Button { Text = "Approve" }; btnApprove.Click += BtnApprove_Click;
            btnReject = new Button { Text = "Reject" }; btnReject.Click += BtnReject_Click;

            var bottom = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(6) };
            bottom.Controls.Add(btnApprove); bottom.Controls.Add(btnReject);

            var status = new StatusStrip();
            statusLabel = new ToolStripStatusLabel { Text = "Ready" }; status.Items.Add(statusLabel);

            main.Controls.Add(top, 0, 0);
            main.Controls.Add(dgvRequests, 0, 1);
            main.Controls.Add(status, 0, 2);
            this.Controls.Add(main);

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
