using AY.BusinessServices;
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
    public partial class FrmValueControl : Form
    {
        private readonly string valveName;
        private readonly bool state;
        private readonly PlcDataService plcDataService;

        public FrmValueControl(string valveName, bool state, PlcDataService plcDataService)
        {
            InitializeComponent();
            this.valveName = valveName;
            this.state = state;
            this.plcDataService = plcDataService;

            this.TopMost = true;
            this.lbl_Msg.Text = $"是否确定要{(state ? "关闭" : "打开")}{valveName}？";
            this.SetWindowDrag(this.lbl_Exit, this.panel1, this.lbl_Title);
            
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (plcDataService.IsConnected)
            {
                bool result = false;
                switch (valveName)
                {
                    case "进水阀":
                        result = plcDataService.ValveInControl(!state);
                        break;
                    case "出水阀":
                        result = plcDataService.ValveOutControl(!state);
                        break;
                    default:
                        new FrmMsgNoAck($"未知阀门名称：{valveName}", "错误").ShowDialog();
                        return;
                }
                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                else
                {
                    new FrmMsgNoAck($"操作失败，请检查PLC连接或阀门状态。", "错误").ShowDialog();
                    return;
                }
            }
            new FrmMsgNoAck("PLC未连接，请先连接PLC。", "错误").ShowDialog();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
