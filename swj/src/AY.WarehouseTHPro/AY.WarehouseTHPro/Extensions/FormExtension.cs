using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AY.WarehouseTHPro.Extensions
{
    public static class FormExtension
    {
        public static void SetWindowDrag(this System.Windows.Forms.Form form,
            System.Windows.Forms.Control closeControl,
            System.Windows.Forms.Control minControl,
            System.Windows.Forms.Control maxControl,
            params System.Windows.Forms.Control[] dragControls)
        {
            closeControl.Click += (sender, e) =>
            {
                form.Close();
            };
            minControl.Click += (sender, e) =>
            {
                form.WindowState = FormWindowState.Minimized;
            };

            maxControl.Click += (sender, e) =>
            {
                form.WindowState = form.WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
            };

            System.Drawing.Point mPoint = form.Location;
            foreach (var control in dragControls)
            {
                control.MouseDown += (sender, e) =>
                {
                    mPoint = new System.Drawing.Point(e.X, e.Y);
                };
               
                control.MouseMove += (sender, e) =>
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        form.Location = new System.Drawing.Point(form.Location.X + e.X - mPoint.X, form.Location.Y + e.Y - mPoint.Y);
                    }
                };

                control.DoubleClick += (sender, e) => {
                    form.WindowState = form.WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
                };
            }
        }
    }
}
