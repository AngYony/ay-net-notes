using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AY.PressurizationStationPro
{
    public partial class FrmMsgWithAck : Form
    {
        public FrmMsgWithAck(string message, string title)
        {
            InitializeComponent();
            this.TopMost = true;
            this.lbl_Msg.Text = message;
            this.lbl_Title.Text = title;
            this.SetWindowDrag(this.lbl_Exit, this.panel1, this.lbl_Title);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
