using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace AY.NodeSettings
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceBase
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 大小端
        /// </summary>
        public DataFormat DataFormat { get; set; } = DataFormat.ABCD;

        /// <summary>
        /// 连接状态
        /// </summary>
        public bool IsConnected { get; set; } = false;

        /// <summary>
        /// 重连时间
        /// </summary>
        public int ReConnectTime { get; set; } = 2000;

        /// <summary>
        /// 
        /// </summary>
        public Stopwatch Stopwatcher { get; set; }

        /// <summary>
        /// 通信周期
        /// </summary>
        public long CommPeriod { get; set; }

        /// <summary>
        /// Modbus都是使用线程循环重复读取串口数据，所以需要定义一个取消线程的Token
        /// </summary>
        protected CancellationTokenSource Cts = null;

        /// <summary>
        /// 是否为初次连接，后续进行断线重连需要使用
        /// </summary>
        protected bool FirstConnectSign { get; set; } = true;

        public Dictionary<string, object> CurrentValue = new Dictionary<string, object>();

        public event AlarmEventDelegate AlarmEvent;
        //也可以使用系统内置委托Action，区别就是+=订阅事件时，仅仅参数名称不同而已，其他都一样
        //public event Action<VariableBase, AlarmEventArgs> AlarmEvent2;

        public void OnAlarmEvent(VariableBase variable, AlarmEventArgs e)
        {
            //触发报警事件
            AlarmEvent?.Invoke(variable, e);
        }

        public void UpdateVariable(VariableBase variable)
        {
            //更新变量的值
            UpdateValue(variable);

            //更新报警
            UpdateAlarm(variable);
        }

        private void UpdateAlarm(VariableBase variable)
        {
            if (variable.VarName == "A01温度")
            {

            }
            if (variable.HAlarm || variable.LAlarm)
            {
                float currentValue = 0.0f;
                if (variable.DataType == DataType.Bool)
                {
                    currentValue = Convert.ToBoolean(variable.VarValue) ? 1.0f : 0.0f;
                }
                else
                {
                    currentValue = Convert.ToSingle(variable.VarValue);
                }

                int compareResult = 0;
                if (variable.HAlarm)
                {
                    //当前值大于设定值  缓存值小于设定值：报警
                    //当前值小于设定值  缓存值大于等于设定值：恢复
                    compareResult = Compare(currentValue, variable.HAlarmValue, variable.HCacheValue, true);
                    if (compareResult != 0)
                    {
                        //通过事件通知
                        OnAlarmEvent(variable, new AlarmEventArgs
                        {
                            DeviceName = this.DeviceName,
                            VarName = variable.VarName,
                            CurrentValue = variable.VarValue.ToString(),
                            AlarmValue = variable.HAlarmValue.ToString(),
                            AlarmNote = variable.HAlarmNote,
                            IsTriggered = compareResult == 1 // 1表示报警，-1表示恢复
                        });
                    }
                    variable.HCacheValue = currentValue; //更新缓存值，缓存值的作用是防止值未变化时每次都触发报警事件
                }
                if (variable.LAlarm)
                {
                    //当前值小于设定值  缓存值大于设定值：报警
                    //当前值大于设定值  缓存值小于等于设定值：恢复
                    compareResult = Compare(currentValue, variable.LAlarmValue, variable.LCacheValue, false);
                    if (compareResult != 0)
                    {
                        //通过事件通知
                        //通过事件通知
                        OnAlarmEvent(variable, new AlarmEventArgs
                        {
                            DeviceName = this.DeviceName,
                            VarName = variable.VarName,
                            CurrentValue = variable.VarValue.ToString(),
                            AlarmValue = variable.LAlarmValue.ToString(),
                            AlarmNote = variable.LAlarmNote,
                            IsTriggered = compareResult == 1 // 1表示报警，-1表示恢复
                        });
                    }
                    variable.LCacheValue = currentValue; //更新缓存值
                }
            }
        }

        private void UpdateValue(VariableBase variable)
        {
            CurrentValue[variable.VarName] = variable.VarValue;
        }

        /// <summary>
        /// 比较返回结果   1表示报警，-1表示恢复，0表示无报警
        /// </summary>
        /// <param name="current"></param>
        /// <param name="set"></param>
        /// <param name="cache"></param>
        /// <param name="isPositive">true：高报警；  false：低报警</param>
        /// <returns></returns>
        private int Compare(float current, float set, float cache, bool isPositive)
        {
            if (isPositive)
            {
                //当前值大于设定值  缓存值小于设定值：报警
                if (current >= set && cache < set)
                    return 1;
                //当前值小于设定值  缓存值大于等于设定值：恢复
                if (current < set && cache >= set)
                    return -1;

            }
            else
            {
                if ((current <= set && cache > set))
                    return 1;
                if (current > set && cache <= set)
                    return -1;

            }
            return 0; // 无报警
        }

    }

    public delegate void AlarmEventDelegate(VariableBase variable, AlarmEventArgs e);
    public class AlarmEventArgs : EventArgs
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 变量名称
        /// </summary>
        public string VarName { get; set; }
        /// <summary>
        /// 当前值
        /// </summary>
        public string CurrentValue { get; set; }
        /// <summary>
        /// 报警值
        /// </summary>
        public string AlarmValue { get; set; }
        /// <summary>
        /// 报警说明
        /// </summary>
        public string AlarmNote { get; set; }
        /// <summary>
        /// 触发还是消除
        /// </summary>
        public bool IsTriggered { get; set; }


    }
}