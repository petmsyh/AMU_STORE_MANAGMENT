using System;
using System.Windows.Forms;
using AMU.store.Mngt.Data;

namespace AMU.store.Mngt
{
    public class StoreForm : Form
    {   
        public StoreForm()
        {
            this.Text = "Store - Purchase Receiver";
            this.Load += new EventHandler(StoreForm_Load);
            this.Width = 800;
            this.Height = 450;
        }

        private void StoreForm_Load(object sender, EventArgs e)
        {
            // reuse PurchaseItemsViewForm as the store UI
            var viewer = new PurchaseItemsViewForm();
            viewer.TopLevel = false;
            viewer.FormBorderStyle = FormBorderStyle.None;
            viewer.Dock = DockStyle.Fill;
            this.Controls.Clear();
            this.Controls.Add(viewer);
            viewer.Show();
        }
    }
}
