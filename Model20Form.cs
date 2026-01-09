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
            this.Text = "Model 20 - Property Request";
            this.Dock = DockStyle.Fill;

            cbDepartment = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 300 };
            cbDepartment.Items.AddRange(new object[] { "Faculty of Engineering", "Faculty of Science", "Administration" });
            cbDepartment.SelectedIndex = 0;

            txtPurpose = new TextBox { Dock = DockStyle.Top, Height = 60, Multiline = true };

            dgvItems = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "PropertyName", HeaderText = "Property Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Quantity", Width = 80 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Unit", HeaderText = "Unit", Width = 80 });
            var colRemove = new DataGridViewButtonColumn { Name = "Action", HeaderText = "Action", Text = "Remove", UseColumnTextForButtonValue = true, Width = 80 };
            dgvItems.Columns.Add(colRemove);
            dgvItems.CellClick += DgvItems_CellClick;

            cbProperty = new ComboBox { Width = 200 };
            cbProperty.Items.AddRange(new object[] { "Laptop", "Chair", "Table", "Projector" });
            cbProperty.SelectedIndex = 0;

            nudQuantity = new NumericUpDown { Width = 80, Minimum = 1, Maximum = 1000, Value = 1 };

            btnAddItem = new Button { Text = "Add Item" };
            btnAddItem.Click += BtnAddItem_Click;

            btnSubmit = new Button { Text = "Submit Request" };
            btnSubmit.Click += BtnSubmit_Click;

            var main = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 4 };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            var top = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(6) };
            top.Controls.Add(new Label { Text = "Department:", AutoSize = true });
            top.Controls.Add(cbDepartment);

            var itemPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(6) };
            itemPanel.Controls.Add(cbProperty); itemPanel.Controls.Add(nudQuantity); itemPanel.Controls.Add(btnAddItem);

            var lblPurpose = new Label { Text = "Purpose:" };

            main.Controls.Add(top, 0, 0);
            main.Controls.Add(lblPurpose, 0, 1);
            main.Controls.Add(txtPurpose, 0, 1);
            main.Controls.Add(itemPanel, 0, 1);
            main.Controls.Add(dgvItems, 0, 2);
            main.Controls.Add(new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, Controls = { btnSubmit } }, 0, 3);

            this.Controls.Add(main);
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
