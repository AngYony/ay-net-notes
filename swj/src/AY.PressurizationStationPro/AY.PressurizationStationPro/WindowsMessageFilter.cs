using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AY.PressurizationStationPro
{
    public class WindowsMessageFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            //如果检测到有鼠标或则键盘的消息，则使计数为0.....
            if (m.Msg == 0x0200 || m.Msg == 0x0201 || m.Msg == 0x0204 || m.Msg == 0x0207)
            {
                Program.ProgramUseStartTime = DateTime.Now; //重置计时
            }
            return false;
        }
    }
}
