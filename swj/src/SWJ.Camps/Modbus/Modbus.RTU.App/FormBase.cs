using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modbus.RTU.App
{
    public class FormBase : Form
    {

        public void RegisterDrag(Form form, params Control[] controls)
        {
            
            Point mPoint = this.Location;
            foreach (var control in controls)
            {
                if (control is Button btnClose && btnClose.Name.Contains("Close"))
                {
                    btnClose.Click += (sender, e) =>
                    {
                        form.Close();
                    };
                }

                control.MouseDown += (sender, e) =>
                {
                    mPoint = new Point(e.X, e.Y);
                };
                control.MouseMove += (sender, e) =>
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        form.Location = new Point(form.Location.X + e.X - mPoint.X, form.Location.Y + e.Y - mPoint.Y);
                    }
                };
            }
        }
 
    }
}