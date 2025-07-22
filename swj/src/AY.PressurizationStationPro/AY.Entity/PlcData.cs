using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.Entity
{
    public class PlcData
    {
        public bool InPump1State { get; set; }
        public bool InPump2State { get; set; }
        public bool CirclePump1State { get; set; }
        public bool CirclePump2State { get; set; }
        public bool ValveInState { get; set; }
        public bool ValveOutState { get; set; }
        public bool SysRunState { get; set; }
        public bool SysAlarmState { get; set; }
        //public byte[] SpareState { get; set; } = new byte[2];
        public float PressureIn { get; set; }
        public float PressureOut { get; set; }
        public float TempIn1 { get; set; }
        public float TempIn2 { get; set; }
        public float TempOut { get; set; }
        public float PressureTank1 { get; set; }
        public float PressureTank2 { get; set; }
        public float LevelTank1 { get; set; }
        public float LevelTank2 { get; set; }
        public float PressureTankOut { get; set; }
        //public byte[] SpareVariable { get; set; } = new byte[56];
        //public bool InPump1Start { get; set; }
        //public bool InPump1Stop { get; set; }
        //public bool InPump2Start { get; set; }
        //public bool InPump2Stop { get; set; }
        //public bool CirclePump1Start { get; set; }
        //public bool CirclePump1Stop { get; set; }
        //public bool CirclePump2Start { get; set; }
        //public bool CirclePump2Stop { get; set; }
        //public bool ValveInOpen { get; set; }
        //public bool ValveInClose { get; set; }
        //public bool ValveOutOpen { get; set; }
        //public bool ValveOutClose { get; set; }

    }
}
