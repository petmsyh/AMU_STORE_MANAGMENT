using System.Windows.Forms;

namespace AMU.store.Mngt
{
    public class ReportsForm : Form
    {
        public ReportsForm()
        {
            this.Text = "Reports";
            var l = new Label { Text = "Reports area (MDI child)", Dock = DockStyle.Fill, TextAlign = System.Drawing.ContentAlignment.MiddleCenter };
            this.Controls.Add(l);
        }
    }
}
