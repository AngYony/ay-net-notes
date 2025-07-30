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
    //ί������
    public delegate void AlarmEventDelegate(VariableBase variable, AlarmEventArgs e);

    /// <summary>
    /// DeviceBase����
    /// </summary>
    public class DeviceBase
    {
        /// <summary>
        /// �豸����
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// ��С��
        /// </summary>
        public DataFormat DataFormat { get; set; } = DataFormat.ABCD;

        /// <summary>
        /// ����״̬
        /// </summary>
        public bool IsConnected { get; set; } = false;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public int ReConnectTime { get; set; } = 2000;

        /// <summary>
        /// ȡ���̱߳�־λ
        /// </summary>
        public CancellationTokenSource Cts = null;

        /// <summary>
        /// �������ӱ�־λ
        /// </summary>
        public bool FirstConnectSign { get; set; } = true;

        /// <summary>
        /// ��ʱ��
        /// </summary>
        public Stopwatch StopWatch { get; set; }

        /// <summary>
        /// ͨ������
        /// </summary>
        public long CommPeriod { get; set; } = 0;

        /// <summary>
        /// ��ֵ�ԣ������Ǳ������ƣ�ֵ�Ƕ�Ӧ��ֵ
        /// </summary>
        public Dictionary<string, object> CurrentValue = new Dictionary<string, object>();

        /// <summary>
        /// �����¼�
        /// </summary>
        public event AlarmEventDelegate AlarmEvent;

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="variableBase"></param>
        /// <param name="e"></param>
        private void OnAlarmEvent(VariableBase variableBase, AlarmEventArgs e)
        {
            this.AlarmEvent?.Invoke(variableBase, e);
        }

        /// <summary>
        /// ���±���
        /// </summary>
        /// <param name="variable"></param>
        public void UpdateVariable(VariableBase variable)
        {
            //������ֵ
            UpdateValue(variable);

            //���±���
            UpdateAlarm(variable);
        }

        /// <summary>
        /// ������ֵ
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
        /// ���±���
        /// </summary>
        /// <param name="variable"></param>
        private void UpdateAlarm(VariableBase variable)
        {
            //�ж��Ƿ������˱���
            if (variable.HAlarm || variable.LAlarm)
            {
                //��������ԭ��
                //��ǰֵ  �趨ֵ   ����ֵ
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

                    //��ǰֵ�����趨ֵ   ����ֵС���趨ֵ
                    //��ǰֵС���趨ֵ   ����ֵ�����趨ֵ

                    compareResult = Compare(currentValue, variable.HAlarmValue, variable.HCacheValue, true);

                    if (compareResult != 0)
                    {
                        //ͨ���¼�֪ͨ
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
                    //��ǰֵС���趨ֵ   ����ֵ�����趨ֵ
                    //��ǰֵ�����趨ֵ   ����ֵС���趨ֵ

                    compareResult = Compare(currentValue, variable.LAlarmValue, variable.LCacheValue, false);

                    if (compareResult != 0)
                    {
                        //ͨ���¼�֪ͨ
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
        /// �ȽϷ��ؽ��  1��ʾ����  0��ʾ���仯  -1��ʾ����
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
        /// ������
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
