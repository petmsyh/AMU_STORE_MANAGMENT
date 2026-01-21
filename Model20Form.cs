using System;
using System.Windows.Forms;
using AMU.store.Mngt.Data;

namespace AMU.store.Mngt
{
    public class Model20Form : Form
    {
        private ComboBox cbDepartment;
        private TextBox txtPurpose;
        private DataGridView dgvItems;
        private ComboBox cbProperty;
        private NumericUpDown nudQuantity;
        private Button btnAddItem;
        private Button btnSubmit;

        public Model20Form()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Model 20 - Property Purchase Request";
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
                Text = "Property Purchase Request Form (Model 20)",
                Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            titlePanel.Controls.Add(titleLabel);

            cbDepartment = new ComboBox 
            { 
                DropDownStyle = ComboBoxStyle.DropDownList, 
                Width = 350,
                Font = new System.Drawing.Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat
            };
            cbDepartment.Items.AddRange(new object[] { "Faculty of Engineering", "Faculty of Science", "Faculty of Business and Economics", "Faculty of Social Sciences", "Administration", "Library", "ICT Department" });
            cbDepartment.SelectedIndex = 0;

            txtPurpose = new TextBox 
            { 
                Width = 600,
                Height = 80, 
                Multiline = true,
                Font = new System.Drawing.Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle,
                ScrollBars = ScrollBars.Vertical
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
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "PropertyName", HeaderText = "Property Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Quantity", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Unit", HeaderText = "Unit", Width = 100 });
            var colRemove = new DataGridViewButtonColumn { Name = "Action", HeaderText = "Action", Text = "Remove", UseColumnTextForButtonValue = true, Width = 100 };
            dgvItems.Columns.Add(colRemove);
            dgvItems.CellClick += DgvItems_CellClick;

            cbProperty = new ComboBox 
            { 
                Width = 220,
                Font = new System.Drawing.Font("Segoe UI", 10F),
                FlatStyle = FlatStyle.Flat
            };
            cbProperty.Items.AddRange(new object[] { "Laptop", "Desktop Computer", "Chair", "Desk", "Table", "Projector", "Printer", "Whiteboard", "Office Cabinet", "Air Conditioner" });
            cbProperty.SelectedIndex = 0;

            nudQuantity = new NumericUpDown 
            { 
                Width = 100, 
                Minimum = 1, 
                Maximum = 1000, 
                Value = 1,
                Font = new System.Drawing.Font("Segoe UI", 10F)
            };

            btnAddItem = new Button 
            { 
                Text = "Add Item",
                Width = 120,
                Height = 35,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAddItem.FlatAppearance.BorderSize = 0;
            btnAddItem.Click += BtnAddItem_Click;

            btnSubmit = new Button 
            { 
                Text = "Submit Request",
                Width = 180,
                Height = 45,
                BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Click += BtnSubmit_Click;

            var main = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 5, Padding = new Padding(15) };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));

            var deptPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            deptPanel.Controls.Add(new Label 
            { 
                Text = "Department:", 
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(0, 8, 10, 0)
            });
            deptPanel.Controls.Add(cbDepartment);

            var purposePanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            var lblPurpose = new Label 
            { 
                Text = "Purpose of Request:", 
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(0, 0, 0, 5)
            };
            purposePanel.Controls.Add(lblPurpose);
            purposePanel.Controls.Add(txtPurpose);
            txtPurpose.Location = new System.Drawing.Point(5, 25);

            var itemPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            itemPanel.Controls.Add(new Label 
            { 
                Text = "Add Items to Request:", 
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(0, 8, 15, 0),
                Width = 150
            });
            itemPanel.Controls.Add(cbProperty);
            itemPanel.Controls.Add(new Label { Text = "Qty:", AutoSize = true, Padding = new Padding(15, 8, 5, 0) });
            itemPanel.Controls.Add(nudQuantity);
            itemPanel.Controls.Add(btnAddItem);

            var gridPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            var gridLabel = new Label
            {
                Text = "Requested Items:",
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Dock = DockStyle.Top,
                Padding = new Padding(0, 0, 0, 10)
            };
            gridPanel.Controls.Add(dgvItems);
            gridPanel.Controls.Add(gridLabel);

            var bottomPanel = new FlowLayoutPanel 
            { 
                Dock = DockStyle.Fill, 
                FlowDirection = FlowDirection.RightToLeft, 
                Padding = new Padding(5, 10, 5, 5) 
            };
            bottomPanel.Controls.Add(btnSubmit);

            main.Controls.Add(deptPanel, 0, 0);
            main.Controls.Add(purposePanel, 0, 1);
            main.Controls.Add(itemPanel, 0, 2);
            main.Controls.Add(gridPanel, 0, 3);
            main.Controls.Add(bottomPanel, 0, 4);

            this.Controls.Add(main);
            this.Controls.Add(titlePanel);
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            var itemName = cbProperty.SelectedItem?.ToString() ?? "";
            var qty = (int)nudQuantity.Value;
            if (string.IsNullOrEmpty(itemName) || qty <= 0)
            {
                MessageBox.Show("Please select a property and quantity.");
                return;
            }

            dgvItems.Rows.Add(itemName, qty.ToString(), "pcs");
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one item to the request.");
                return;
            }

            // gather items
            var items = new System.Collections.Generic.List<(string name, int qty)>();
            foreach (DataGridViewRow r in dgvItems.Rows)
            {
                var name = Convert.ToString(r.Cells[0].Value);
                var qty = int.TryParse(Convert.ToString(r.Cells[1].Value), out var q) ? q : 0;
                items.Add((name, qty));
            }

            try
            {
                DbInitializer.EnsureSchema();
                DbSeeder.Seed();
                var reqId = Model20Repository.CreateRequest(cbDepartment.SelectedItem?.ToString() ?? "", txtPurpose.Text ?? "", LoggedInUser.UserId, items);
                MessageBox.Show($"Request submitted. RequestId={reqId}", "Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvItems.Rows.Clear();
            }
            catch(System.Exception ex)
            {
                MessageBox.Show("Error submitting request: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvItems.Columns[e.ColumnIndex].Name == "Action")
            {
                dgvItems.Rows.RemoveAt(e.RowIndex);
            }
        }
    }
}
