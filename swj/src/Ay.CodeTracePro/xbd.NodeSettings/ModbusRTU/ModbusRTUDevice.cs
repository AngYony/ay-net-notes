

using AY.CommunicationLib;
using AY.CommunicationLib.DataConvert;
using AY.CommunicationLib.Modbus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace xbd.NodeSettings
{
    /// <summary>
    /// ModbusRTUDevice�豸
    /// </summary>
    public class ModbusRTUDevice : DeviceBase
    {
        /// <summary>
        /// �˿ں�
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// У��λ
        /// </summary>
        public Parity Parity { get; set; }

        /// <summary>
        /// ����λ
        /// </summary>
        public int DataBits { get; set; }

        /// <summary>
        /// ֹͣλ
        /// </summary>
        public StopBits StopBits { get; set; }

        /// <summary>
        /// ͨ���鼯��
        /// </summary>
        public List<ModbusRTUGroup> GroupList { get; set; } = new List<ModbusRTUGroup>();

        /// <summary>
        /// ͨ�Ŷ���
        /// </summary>
        private ModbusRTU modbus = new ModbusRTU();

        /// <summary>
        /// �����ֵ伯��
        /// </summary>
        public Dictionary<string, ModbusRTUVariable> VariableDicList = new Dictionary<string, ModbusRTUVariable>();

        /// <summary>
        /// �Ƿ�ʹ�ò���ģʽ
        /// </summary>
        public bool OfflineCheckPolicy { get; set; } = true;

        /// <summary>
        /// ���ѭ����������
        /// </summary>
        public int CheckTimesSet { get; set; } = 100;

        //ѭ������
        private int cycleTimers = 0;

        /// <summary>
        /// ��ʼ�������ֵ伯��
        /// </summary>
        public void Init()
        {
            foreach (var gp in this.GroupList)
            {
                foreach (var variable in gp.VarList)
                {
                    if (VariableDicList.ContainsKey(variable.VarName))
                    {
                        variable.SlaveId = gp.GroupId;
                        VariableDicList[variable.VarName] = variable;
                    }
                    else
                    {
                        variable.SlaveId = gp.GroupId;
                        VariableDicList.Add(variable.VarName, variable);
                    }
                }
            }
        }

        /// <summary>
        /// �������߳�
        /// </summary>
        public void Start()
        {
            Init();

            Cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                GetModbusRTUValue();
            }, Cts.Token);
        }

        /// <summary>
        /// ���̷߳���ѭ����ȡ����
        /// </summary>
        private void GetModbusRTUValue()
        {
            while (!Cts.IsCancellationRequested)
            {
                //������ӳɹ�
                if (IsConnected)
                {
                    this.StopWatch = Stopwatch.StartNew();
                    if (this.OfflineCheckPolicy == false)
                    {
                        //ѭ����ȡ
                        foreach (var gp in this.GroupList)
                        {
                            gp.IsOK = GetGroupValue(gp);
                        }
                    }
                    else
                    {
                        //��ȡ���е�OK��
                        List<ModbusRTUGroup> OKGroupList = this.GroupList.Where(c => c.IsOK).ToList();
                        //ѭ����ȡOK��
                        foreach (var gp in OKGroupList)
                        {
                            gp.IsOK = GetGroupValue(gp);
                        }
                        cycleTimers++;

                        if (cycleTimers>=CheckTimesSet)
                        {
                            //��ȡ���е�NG��
                            List<ModbusRTUGroup> NGGroupList = this.GroupList.Where(c => c.IsOK==false).ToList();
                            //ѭ����ȡOK��
                            foreach (var gp in NGGroupList)
                            {
                                gp.IsOK = GetGroupValue(gp);
                            }
                            cycleTimers = 0;
                        }
                    }

                    //������е��鶼��ȡʧ�ܣ����Ҷ˿ںŲ�����
                    if (this.GroupList.Where(c => c.IsOK == false).Count() == this.GroupList.Count)
                    {
                        if (!Common.GetPortNames().Contains(this.PortName))
                        {
                            this.IsConnected = false;
                        }
                    }
                    this.CommPeriod = this.StopWatch.ElapsedMilliseconds;
                }
                else
                {
                    //������ʱ
                    if (!FirstConnectSign)
                    {
                        Thread.Sleep(this.ReConnectTime);
                    }

                    this.IsConnected = this.modbus.Open(this.PortName, this.BaudRate, this.Parity, this.DataBits, this.StopBits).IsSuccess;

                    //��λ������־λ
                    if (FirstConnectSign)
                    {
                        FirstConnectSign = false;
                    }
                }
            }
        }

        /// <summary>
        /// ����ͨ�����ȡ
        /// </summary>
        /// <param name="gp"></param>
        /// <returns></returns>
        private bool GetGroupValue(ModbusRTUGroup gp)
        {
            for (int i = 0; i < gp.ReadTimes; i++)
            {
                OperateResult<bool[]> cResult = OperateResult.CreateFailResult<bool[]>("");
                OperateResult<byte[]> rResult = OperateResult.CreateFailResult<byte[]>("");

                if (gp.StoreArea == ModbusStore.������Ȧ || gp.StoreArea == ModbusStore.�����Ȧ)
                {
                    switch (gp.StoreArea)
                    {
                        case ModbusStore.�����Ȧ:
                            cResult = modbus.ReadCoils(gp.Start, gp.Length, gp.GroupId);
                            break;
                        case ModbusStore.������Ȧ:
                            cResult = modbus.ReadInputs(gp.Start, gp.Length, gp.GroupId);
                            break;
                        default:
                            break;
                    }

                    //���ȵ���֤
                    if (cResult.IsSuccess && cResult.Content.Length == gp.Length)
                    {
                        //��������
                        foreach (var variable in gp.VarList)
                        {
                            var add = AnalysisAddress(variable.Address);

                            if (add.IsSuccess == false)
                            {
                                continue;
                            }

                            int start = add.Content1 - gp.Start;

                            switch (variable.DataType)
                            {
                                case DataType.Bool:
                                    variable.VarValue = BitLib.GetBitFromBitArray(cResult.Content, start);
                                    break;
                                default:
                                    break;
                            }

                            //���±���
                            this.UpdateVariable(variable);
                        }

                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    switch (gp.StoreArea)
                    {
                        case ModbusStore.����Ĵ���:
                            rResult = modbus.ReadInputRegisters(gp.Start, gp.Length, gp.GroupId);
                            break;
                        case ModbusStore.�����ͼĴ���:
                            rResult = modbus.ReadHoldingRegisters(gp.Start, gp.Length, gp.GroupId);
                            break;
                        default:
                            break;
                    }

                    //��ȡ�ɹ�
                    if (rResult.IsSuccess && rResult.Content.Length == gp.Length * 2)
                    {
                        foreach (var variable in gp.VarList)
                        {
                            var add = AnalysisAddress(variable.Address);

                            if (add.IsSuccess == false)
                            {
                                continue;
                            }


                            //0 100   200      10 => 20
                            int start = add.Content1 - gp.Start;
                            int offsetOrLength = add.Content2;

                            start *= 2;

                            switch (variable.DataType)
                            {
                                case DataType.Bool:
                                    variable.VarValue = BitLib.GetBitFrom2BytesArray(rResult.Content, start, offsetOrLength, DataFormat == EndianType.BADC || DataFormat == EndianType.DCBA);
                                    break;
                                case DataType.Short:
                                    variable.VarValue = ShortLib.GetShortFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.UShort:
                                    variable.VarValue = UShortLib.GetUShortFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.Int:
                                    variable.VarValue = IntLib.GetIntFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.UInt:
                                    variable.VarValue = UIntLib.GetUIntFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.Float:
                                    variable.VarValue = FloatLib.GetFloatFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.Double:
                                    variable.VarValue = DoubleLib.GetDoubleFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.Long:
                                    variable.VarValue = LongLib.GetLongFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.ULong:
                                    variable.VarValue = ULongLib.GetULongFromByteArray(rResult.Content, start, DataFormat);
                                    break;
                                case DataType.String:
                                    //10.5
                                    byte[] bytes = ByteArrayLib.GetByteArrayFromByteArray(rResult.Content, start, offsetOrLength * 2);

                                    //С�˴���
                                    if (this.DataFormat == EndianType.BADC || this.DataFormat == EndianType.DCBA)
                                    {
                                        variable.VarValue = StringLib.GetStringFromByteArrayByEncoding(bytes, 0, bytes.Length, Encoding.ASCII).Replace("\0", "");
                                    }
                                    else
                                    {
                                        //1 2 3 4    2 1 4 3
                                        List<byte> data = new List<byte>();

                                        for (int j = 0; j < bytes.Length; j += 2)
                                        {
                                            data.Add(bytes[j + 1]);
                                            data.Add(bytes[j]);
                                        }
                                        variable.VarValue = StringLib.GetStringFromByteArrayByEncoding(data.ToArray(), 0, data.Count, Encoding.ASCII).Replace("\0", "");
                                    }
                                    break;
                                case DataType.ByteArray:
                                    variable.VarValue = ByteArrayLib.GetByteArrayFromByteArray(rResult.Content, start, offsetOrLength * 2);
                                    break;
                                default:
                                    break;
                            }

                            //��������ת��
                            variable.VarValue = MigrationLib.GetMigrationValue(variable.VarValue, variable.Scale, variable.Offset).Content;

                            this.UpdateVariable(variable);
                        }

                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// ֹͣ���߳�
        /// </summary>
        public void Stop()
        {
            Cts?.Cancel();

            if (IsConnected)
            {
                modbus.Close();
            }
        }

        /// <summary>
        /// ��ַ����
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private OperateResult<ushort, ushort> AnalysisAddress(string address)
        {
            ushort start = 0;
            ushort offsetOrLength = 0;

            if (address.Contains('.'))
            {
                string[] result = address.Split('.');

                if (result.Length == 2)
                {
                    if (ushort.TryParse(result[0], out start) && ushort.TryParse(result[1], out offsetOrLength))
                    {
                        return OperateResult.CreateSuccessResult(start, offsetOrLength);
                    }
                    else
                    {
                        return OperateResult.CreateFailResult<ushort, ushort>("��ַ��ʽ����ȷ��" + address);
                    }
                }
                else
                {
                    return OperateResult.CreateFailResult<ushort, ushort>("��ַ��ʽ����ȷ��" + address);
                }
            }
            else
            {
                if (ushort.TryParse(address, out start))
                {
                    return OperateResult.CreateSuccessResult(start, offsetOrLength);
                }
                else
                {
                    return OperateResult.CreateFailResult<ushort, ushort>("��ַ��ʽ����ȷ��" + address);
                }
            }
        }


        /// <summary>
        /// ͨ��д�뷽��
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="varValue"></param>
        /// <returns></returns>
        public OperateResult Write(string varName, string varValue)
        {
            if (VariableDicList.ContainsKey(varName))
            {
                return OperateResult.CreateFailResult("�޷�ͨ�����������ҵ���������");
            }
            ModbusRTUVariable variable = VariableDicList[varName];
            var result = AnalysisAddress(variable.Address);

            if (result.IsSuccess == false)
            {
                return result;
            }

            //�õ�ƫ����
            ushort start = result.Content1;
            ushort offsetOrLength = result.Content2;

            //��д��ֵ�����������ת��
            var migrationResult = MigrationLib.SetMigrationValue(varValue, variable.DataType, variable.Scale, variable.Offset);
            if (!migrationResult.IsSuccess) return migrationResult;

            //����д��ֵ
            varValue = migrationResult.Content;
            switch (variable.DataType)
            {
                case DataType.Bool:
                    return modbus.WriteSingleCoil(start, varValue == "1" || varValue.ToLower() == "true",variable.SlaveId);


                case DataType.Short:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromShort(Convert.ToInt16(varValue), this.DataFormat), variable.SlaveId);

                case DataType.UShort:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromUShort(Convert.ToUInt16(varValue), this.DataFormat), variable.SlaveId);

                case DataType.Int:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromInt(Convert.ToInt32(varValue), this.DataFormat), variable.SlaveId);
                case DataType.UInt:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromUInt(Convert.ToUInt32(varValue), this.DataFormat), variable.SlaveId);
                case DataType.Float:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromFloat(Convert.ToSingle(varValue), this.DataFormat), variable.SlaveId);
                case DataType.Double:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromDouble(Convert.ToDouble(varValue), this.DataFormat), variable.SlaveId);
                case DataType.Long:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromLong(Convert.ToInt64(varValue), this.DataFormat), variable.SlaveId);
                case DataType.ULong:
                    return modbus.WriteSingleRegister(start, ByteArrayLib.GetByteArrayFromULong(Convert.ToUInt64(varValue), this.DataFormat), variable.SlaveId);
                case DataType.String:
                    if (varValue.Length % 2 != 0)
                    {
                        varValue += " ";
                    }
                    byte[] bytes = ByteArrayLib.GetByteArrayFromString(varValue, Encoding.ASCII);
                    if (this.DataFormat == EndianType.BADC || this.DataFormat == EndianType.DCBA)
                    {
                        return modbus.WriteMultipleRegisters(start, bytes, variable.SlaveId);
                    }
                    else
                    {
                        //ÿ�����2���ֽڵߵ�һ��
                        List<byte> data = new List<byte>();
                        for (int i = 0; i < bytes.Length; i += 2)
                        {
                            data.Add(bytes[i + 1]);
                            data.Add(bytes[i]);
                        }
                        return modbus.WriteMultipleRegisters(start, data.ToArray(), variable.SlaveId);

                    }

                case DataType.ByteArray:
                    //�ո�ָ�
                    List<byte> value = ByteArrayLib.GetByteArrayFromHexString(varValue).ToList();
                    if (value.Count % 2 != 0)
                    {
                        value.Add(0x00);
                    }
                    return modbus.WriteMultipleRegisters(start, value.ToArray(), variable.SlaveId);


                default:
                    return OperateResult.CreateFailResult("�ݲ�֧�ָ���������");

            }

        }
    }
}
