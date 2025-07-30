using AY.CommunicationLib.DataConvert;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace xbd.NodeSettings
{
    //委托声明
    public delegate void AlarmEventDelegate(VariableBase variable, AlarmEventArgs e);

    /// <summary>
    /// DeviceBase基类
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
        /// 取消线程标志位
        /// </summary>
        public CancellationTokenSource Cts = null;

        /// <summary>
        /// 初次连接标志位
        /// </summary>
        public bool FirstConnectSign { get; set; } = true;

        /// <summary>
        /// 计时器
        /// </summary>
        public Stopwatch StopWatch { get; set; }

        /// <summary>
        /// 通信周期
        /// </summary>
        public long CommPeriod { get; set; } = 0;

        /// <summary>
        /// 键值对，键名是变量名称，值是对应的值
        /// </summary>
        public Dictionary<string, object> CurrentValue = new Dictionary<string, object>();

        /// <summary>
        /// 定义事件
        /// </summary>
        public event AlarmEventDelegate AlarmEvent;

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="variableBase"></param>
        /// <param name="e"></param>
        private void OnAlarmEvent(VariableBase variableBase, AlarmEventArgs e)
        {
            this.AlarmEvent?.Invoke(variableBase, e);
        }

        /// <summary>
        /// 更新变量
        /// </summary>
        /// <param name="variable"></param>
        public void UpdateVariable(VariableBase variable)
        {
            //更新数值
            UpdateValue(variable);

            //更新报警
            UpdateAlarm(variable);
        }

        /// <summary>
        /// 更新数值
        /// </summary>
        /// <param name="variable"></param>
        private void UpdateValue(VariableBase variable)
        {
            if (CurrentValue.ContainsKey(variable.VarName))
            {
                CurrentValue[variable.VarName] = variable.VarValue;
            }
            else
            {
                CurrentValue.Add(variable.VarName, variable.VarValue);
            }
        }

        /// <summary>
        /// 更新报警
        /// </summary>
        /// <param name="variable"></param>
        private void UpdateAlarm(VariableBase variable)
        {
            //判断是否配置了报警
            if (variable.HAlarm || variable.LAlarm)
            {
                //报警检测的原理
                //当前值  设定值   缓冲值
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

                    //当前值大于设定值   缓存值小于设定值
                    //当前值小于设定值   缓存值大于设定值

                    compareResult = Compare(currentValue, variable.HAlarmValue, variable.HCacheValue, true);

                    if (compareResult != 0)
                    {
                        //通过事件通知
                        OnAlarmEvent(variable, new AlarmEventArgs()
                        {
                            DeviceName = this.DeviceName,
                            VarName = variable.VarName,
                            CurrentValue = variable.VarValue.ToString(),
                            AlarmValue = variable.HAlarmValue.ToString(),
                            AlarmNote = variable.HAlarmNote.ToString(),
                            IsTriggered = compareResult == 1
                        });
                    }

                    variable.HCacheValue = currentValue;
                }
                if (variable.LAlarm)
                {
                    //当前值小于设定值   缓存值大于设定值
                    //当前值大于设定值   缓存值小于设定值

                    compareResult = Compare(currentValue, variable.LAlarmValue, variable.LCacheValue, false);

                    if (compareResult != 0)
                    {
                        //通过事件通知
                        OnAlarmEvent(variable, new AlarmEventArgs()
                        {
                            DeviceName = this.DeviceName,
                            VarName = variable.VarName,
                            CurrentValue = variable.VarValue.ToString(),
                            AlarmValue = variable.LAlarmValue.ToString(),
                            AlarmNote = variable.LAlarmNote.ToString(),
                            IsTriggered = compareResult == 1
                        });
                    }

                    variable.LCacheValue = currentValue;
                }
            }
        }

        /// <summary>
        /// 比较返回结果  1表示触发  0表示不变化  -1表示消除
        /// </summary>
        /// <param name="current"></param>
        /// <param name="set"></param>
        /// <param name="cache"></param>
        /// <param name="isPositive"></param>
        /// <returns></returns>
        private int Compare(float current, float set, float cache, bool isPositive)
        {
            if (isPositive)
            {
                if (current >= set && cache < set)
                {
                    return 1;
                }
                if (current < set && cache >= set)
                {
                    return -1;
                }
            }
            else
            {
                if (current <= set && cache > set)
                {
                    return 1;
                }
                if (current > set && cache <= set)
                {
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                if (CurrentValue.ContainsKey(key))
                {
                    return CurrentValue[key];
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
