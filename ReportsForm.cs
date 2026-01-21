using System;
using System.Windows.Forms;

namespace AMU.store.Mngt
{
    public class ReportsForm : Form
    {
        public ReportsForm()
        {
            this.Text = "Reports and Analytics";
            this.Dock = DockStyle.Fill;
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Load += ReportsForm_Load;
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
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
                Text = "Reports and Analytics Dashboard",
                Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            titlePanel.Controls.Add(titleLabel);

            // Main content panel
            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                ColumnCount = 2,
                RowCount = 3
            };
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));

            // Add report cards
            var requestsCard = CreateReportCard("Purchase Requests Report", "View all purchase requests by status", "View Report");
            var inventoryCard = CreateReportCard("Inventory Report", "Current stock levels and availability", "View Report");
            var issuanceCard = CreateReportCard("Issuance Report", "Track issued items and recipients", "View Report");
            var auditCard = CreateReportCard("Audit Log Report", "System activity and user actions", "View Report");
            var departmentCard = CreateReportCard("Department Report", "Requests grouped by department", "View Report");
            var summaryCard = CreateReportCard("Summary Report", "Overall system statistics", "View Report");

            mainPanel.Controls.Add(requestsCard, 0, 0);
            mainPanel.Controls.Add(inventoryCard, 1, 0);
            mainPanel.Controls.Add(issuanceCard, 0, 1);
            mainPanel.Controls.Add(auditCard, 1, 1);
            mainPanel.Controls.Add(departmentCard, 0, 2);
            mainPanel.Controls.Add(summaryCard, 1, 2);

            this.Controls.Add(mainPanel);
            this.Controls.Add(titlePanel);
        }

        private Panel CreateReportCard(string title, string description, string buttonText)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                Margin = new Padding(10)
            };
            card.BackColor = System.Drawing.Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;

            var lblTitle = new Label
            {
                Text = title,
                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))),
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            var lblDescription = new Label
            {
                Text = description,
                Font = new System.Drawing.Font("Segoe UI", 9.75F),
                ForeColor = System.Drawing.Color.Gray,
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = System.Drawing.ContentAlignment.TopLeft
            };

            var btnView = new Button
            {
                Text = buttonText,
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += (s, e) =>
            {
                MessageBox.Show($"'{title}' report functionality will be implemented here.\n\nThis would typically show:\n- Detailed data tables\n- Export to PDF/Excel options\n- Date range filters\n- Graphical charts", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            card.Controls.Add(btnView);
            card.Controls.Add(lblDescription);
            card.Controls.Add(lblTitle);

            return card;
        }
    }
}
