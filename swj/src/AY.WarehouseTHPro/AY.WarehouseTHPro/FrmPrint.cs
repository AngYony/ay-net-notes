using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AY.WarehouseTHPro
{
    public partial class FrmPrint : Form
    {
        public FrmPrint()
        {
            InitializeComponent();
        }

        public FrmPrint(List<string> availableFields)
        {
            InitializeComponent();
            this.Load += FrmPrint_Load;
            foreach (string field in availableFields)
                chklst.Items.Add(field, true);
        }

        void FrmPrint_Load(object sender, EventArgs e)
        {
            rdoAllRows.Checked = true;
            chkFitToPageWidth.Checked = true;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public List<string> GetSelectedColumns()
        {
            List<string> lst = new List<string>();
            foreach (object item in chklst.CheckedItems)
                lst.Add(item.ToString());
            return lst;
        }

        public string PrintTitle
        {
            get { return txtTitle.Text; }
        }

        public bool PrintAllRows
        {
            get { return rdoAllRows.Checked; }
        }

        public bool FitToPageWidth
        {
            get { return chkFitToPageWidth.Checked; }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
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
