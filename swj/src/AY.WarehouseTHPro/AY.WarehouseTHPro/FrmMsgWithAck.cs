using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AY.WarehouseTHPro
{
    public partial class FrmMsgWithAck : Form
    {
        public FrmMsgWithAck(string message, string title)
        {
            InitializeComponent();

            this.TopMost = true;

            this.lbl_Message.Text = message;
            this.lbl_Title.Text = title;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void lbl_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static DialogResult Show(string message, string title = "提示")
        {
            using (FrmMsgWithAck frm = new FrmMsgWithAck(message, title))
            {
                return frm.ShowDialog();
            }
        }

        #region 无边框拖动 

        private Point mPoint;
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }
        #endregion
    }
}
