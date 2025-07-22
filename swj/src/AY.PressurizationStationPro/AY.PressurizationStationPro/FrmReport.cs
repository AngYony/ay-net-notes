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
    public partial class FrmReport : Form
    {
        public FrmReport()
        {
            InitializeComponent();
            this.SetWindowDrag(this.lbl_Exit, this.panel2, this.lbl_Title);
        }
    }
}
