using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xbd.DataConvertLib;
using xbd.ModbusLib.Library;

namespace xbd.ModbusPro
{

    public partial class FrmModbusTCP : Form
    {
        public FrmModbusTCP()
        {
            InitializeComponent();
            InitParam();
            this.btn_Connect.Click += Btn_Connect_Click;
            this.btn_DisConnect.Click += Btn_DisConnect_Click;
            this.btn_Read.Click += Btn_Read_Click;
            this.btn_Write.Click += Btn_Write_Click;
        }



        private byte slaveId = 1;
        private ushort start = 0;
        private ushort count = 1;
        private bool isConnected = false;
        private ModbusTCP modbus = new  ModbusTCP(); //通信对象
        private DataType dataType;
        private StoreArea storeArea;
        private DataFormat dataFormat;
        private OperateResult<bool[]> rcResult;
        private OperateResult<byte[]> rrResult;
        private OperateResult wResult;

        private void InitParam()
        {
            this.list_Info.Columns[1].Width = this.list_Info.Width - list_Info.Columns[0].Width - 20;

            //初始化端口号
            string[] portList = SerialPort.GetPortNames();
             

            //初始化大小端
            this.cmb_DataFormat.Items.AddRange(Enum.GetNames(typeof(DataFormat)));
            this.cmb_DataFormat.SelectedIndex = 0;

            //初始化存储区
            this.cmb_StoreArea.Items.AddRange(Enum.GetNames(typeof(StoreArea)));
            this.cmb_StoreArea.SelectedIndex = 0;

            //初始化数据类型
            this.cmb_DataType.Items.AddRange(Enum.GetNames(typeof(DataType)));
            this.cmb_DataType.SelectedIndex = 0;

            this.txt_SlaveId.Text = slaveId.ToString();
            this.txt_start.Text = start.ToString();
            this.txt_count.Text = count.ToString();
        }

        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                AddLog(1, "ModbusTCP已经建立连接");
                return;
            }
            var result = modbus.Connect(this.txt_IP.Text.Trim(), Convert.ToInt32(this.txt_Port.Text.Trim()));
             
            if (result.IsSuccess)
            {
                AddLog(0, "ModubusTCP连接成功");
                this.isConnected = true;
            }
            else
            {
                AddLog(2, "ModubusTCP连接失败:" + result.Message);
                isConnected = false;

            }

        }

        private void Btn_DisConnect_Click(object sender, EventArgs e)
        {
            modbus.DisConnect();
            isConnected = false;
            AddLog(0, "ModubusTCP断开连接成功");

        }


        private void AddLog(int index, string log)
        {
            var listViewItem = new ListViewItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), index);
            listViewItem.SubItems.Add(log);
            this.list_Info.Items.Insert(0, listViewItem);
        }

        private void Btn_Read_Click(object sender, EventArgs e)
        {
            if (CommonVerify())
            {
                switch (dataType)
                {
                    case DataType.Bool:
                        ReadBool(storeArea, slaveId, start, count);
                        break;

                    case DataType.Short:
                        ReadShort(storeArea, slaveId, start, count);
                        break;
                    case DataType.UShort:
                        ReadUShort(storeArea, slaveId, start, count);
                        break;
                    case DataType.Int:
                        ReadInt(storeArea, slaveId, start, count);
                        break;
                    case DataType.UInt:
                        ReadUInt(storeArea, slaveId, start, count);
                        break;
                    case DataType.Float:
                        ReadFloat(storeArea, slaveId, start, count);
                        break;

                    default:
                        AddLog(3, "读取失败，暂时不支持该类型");
                        break;
                }
            }
        }

        /// <summary>
        /// 读取布尔类型数据
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="slaveId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        private void ReadBool(StoreArea storeArea, byte slaveId, ushort start, ushort count)
        {
            switch (storeArea)
            {
                case StoreArea.输出线圈0x:
                    rcResult = modbus.ReadOutputCoils(start, count, slaveId);
                    break;
                case StoreArea.输入线圈1x:
                    rcResult = modbus.ReadInputCoils(start, count, slaveId);
                    break;

                default:
                    rcResult = OperateResult.CreateFailResult<bool[]>("暂时不支持该存储区");
                    break;
            }

            if (rcResult.IsSuccess)
            {
                AddLog(0, "读取成功：" + StringLib.GetStringFromValueArray(rcResult.Content));
            }
            else
            {
                AddLog(3, "读取失败：" + rcResult.Message);
            }

        }

        /// <summary>
        /// 读取寄存器数据
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="slaveId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        private void ReadShort(StoreArea storeArea, byte slaveId, ushort start, ushort count)
        {
            switch (storeArea)
            {
                case StoreArea.输入寄存器3x:
                    rrResult = modbus.ReadInputRegisters(start, count, slaveId);
                    break;
                case StoreArea.保持型寄存器4x:
                    rrResult = modbus.ReadHoldingRegisters(start, count, slaveId);
                    break;

                default:
                    rrResult = OperateResult.CreateFailResult<byte[]>("暂时不支持该存储区");
                    break;
            }

            if (rrResult.IsSuccess)
            {
                //将字节数组转换为short数组
                var data = ShortLib.GetShortArrayFromByteArray(rrResult.Content, this.dataFormat);
                //将short数组转换为字符串
                AddLog(0, "读取成功：" + StringLib.GetStringFromValueArray(data));

            }
            else
            {
                AddLog(3, "读取失败：" + rrResult.Message);
            }

        }

        /// <summary>
        /// 读取寄存器数据
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="slaveId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        private void ReadUShort(StoreArea storeArea, byte slaveId, ushort start, ushort count)
        {
            switch (storeArea)
            {
                case StoreArea.输入寄存器3x:
                    rrResult = modbus.ReadInputRegisters(start, count, slaveId);
                    break;
                case StoreArea.保持型寄存器4x:
                    rrResult = modbus.ReadHoldingRegisters(start, count, slaveId);
                    break;

                default:
                    rrResult = OperateResult.CreateFailResult<byte[]>("暂时不支持该存储区");
                    break;
            }

            if (rrResult.IsSuccess)
            {
                //将字节数组转换为short数组
                var data = UShortLib.GetUShortArrayFromByteArray(rrResult.Content, this.dataFormat);
                //将short数组转换为字符串
                AddLog(0, "读取成功：" + StringLib.GetStringFromValueArray(data));
            }
            else
            {
                AddLog(3, "读取失败：" + rrResult.Message);
            }

        }



        /// <summary>
        /// 读取寄存器数据
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="slaveId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        private void ReadInt(StoreArea storeArea, byte slaveId, ushort start, ushort count)
        {

            switch (storeArea)
            {
                case StoreArea.输入寄存器3x:
                    //一个int数据占用4字节，需要2个寄存器，所以count*2
                    rrResult = modbus.ReadInputRegisters(start, (ushort)(count * 2), slaveId);
                    break;
                case StoreArea.保持型寄存器4x:
                    rrResult = modbus.ReadHoldingRegisters(start, (ushort)(count * 2), slaveId);
                    break;

                default:
                    rrResult = OperateResult.CreateFailResult<byte[]>("暂时不支持该存储区");
                    break;
            }

            if (rrResult.IsSuccess)
            {
                //将字节数组转换为int数组
                var data = IntLib.GetIntArrayFromByteArray(rrResult.Content, this.dataFormat);
                //将int数组转换为字符串
                AddLog(0, "读取成功：" + StringLib.GetStringFromValueArray(data));

            }
            else
            {
                AddLog(3, "读取失败：" + rrResult.Message);
            }

        }

        /// <summary>
        /// 读取寄存器数据
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="slaveId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        private void ReadUInt(StoreArea storeArea, byte slaveId, ushort start, ushort count)
        {

            switch (storeArea)
            {
                case StoreArea.输入寄存器3x:
                    //一个int数据占用4字节，需要2个寄存器，所以count*2
                    rrResult = modbus.ReadInputRegisters(start, (ushort)(count * 2), slaveId);
                    break;
                case StoreArea.保持型寄存器4x:
                    rrResult = modbus.ReadHoldingRegisters(start, (ushort)(count * 2), slaveId);
                    break;

                default:
                    rrResult = OperateResult.CreateFailResult<byte[]>("暂时不支持该存储区");
                    break;
            }

            if (rrResult.IsSuccess)
            {
                //将字节数组转换为int数组
                var data = UIntLib.GetUIntArrayFromByteArray(rrResult.Content, this.dataFormat);
                //将int数组转换为字符串
                AddLog(0, "读取成功：" + StringLib.GetStringFromValueArray(data));

            }
            else
            {
                AddLog(3, "读取失败：" + rrResult.Message);
            }

        }

        /// <summary>
        /// 读取寄存器数据
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="slaveId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        private void ReadFloat(StoreArea storeArea, byte slaveId, ushort start, ushort count)
        {

            switch (storeArea)
            {
                case StoreArea.输入寄存器3x:
                    //一个float数据占用4字节，需要2个寄存器，所以count*2
                    rrResult = modbus.ReadInputRegisters(start, (ushort)(count * 2), slaveId);
                    break;
                case StoreArea.保持型寄存器4x:
                    rrResult = modbus.ReadHoldingRegisters(start, (ushort)(count * 2), slaveId);
                    break;

                default:
                    rrResult = OperateResult.CreateFailResult<byte[]>("暂时不支持该存储区");
                    break;
            }

            if (rrResult.IsSuccess)
            {
                //将字节数组转换为int数组
                var data = FloatLib.GetFloatArrayFromByteArray(rrResult.Content, this.dataFormat);
                //将int数组转换为字符串
                AddLog(0, "读取成功：" + StringLib.GetStringFromValueArray(data));

            }
            else
            {
                AddLog(2, "读取失败：" + rrResult.Message);
            }

        }

        /// <summary>
        /// 通用验证方法
        /// </summary>
        /// <returns></returns>
        private bool CommonVerify()
        {
            if (!isConnected)
            {
                AddLog(1, "请检查是否已经建立连接");
                return false;
            }

            if (!byte.TryParse(this.txt_SlaveId.Text, out slaveId))
            {
                AddLog(1, "请检查站地址是否为有效的字节类型");
                return false;
            }
            if (!ushort.TryParse(this.txt_start.Text, out start))
            {
                AddLog(1, "请检查起始地址是否为有效的无符号整型");
                return false;
            }
            if (!ushort.TryParse(this.txt_count.Text, out count))
            {
                AddLog(1, "请检查读取数量是否为有效的无符号整型");
                return false;
            }

            dataType = GetEnumValue<DataType>(this.cmb_DataType.Text);
            storeArea = GetEnumValue<StoreArea>(this.cmb_StoreArea.Text);
            dataFormat = GetEnumValue<DataFormat>(this.cmb_DataFormat.Text);
            return true;
        }


        private void Btn_Write_Click(object sender, EventArgs e)
        {

            if (CommonVerify())
            {
                string writeValue = txt_Write.Text.Trim();
                if (string.IsNullOrEmpty(writeValue))
                {
                    AddLog(2, "写入数据失败，写入的值不能为空");
                    return;
                }
                switch (dataType)
                {
                    case DataType.Bool:
                        WriteBool(storeArea, slaveId, start, writeValue);
                        break;

                    case DataType.Short:
                        WriteShort(storeArea, slaveId, start, writeValue);
                        break;
                    case DataType.UShort:
                        WriteUShort(storeArea, slaveId, start, writeValue);
                        break;
                    case DataType.Int:
                        WriteInt(storeArea, slaveId, start, writeValue);
                        break;
                    case DataType.UInt:
                        WriteUInt(storeArea, slaveId, start, writeValue);
                        break;
                    case DataType.Float:
                        WriteFloat(storeArea, slaveId, start, writeValue);
                        break;
                    default:
                        AddLog(2, "写入失败：暂不支持该数据类型");
                        break;
                }

            }


        }
        /// <summary>
        /// 用于写入线圈
        /// </summary>
        /// <param name="storeArea"></param>
        /// <param name="slaveId"></param>
        /// <param name="start"></param>
        /// <param name="writeValue"></param>

        private void WriteBool(StoreArea storeArea, byte slaveId, ushort start, string writeValue)
        {
            bool[] values = BitLib.GetBitArrayFromBitArrayString(writeValue);

            switch (storeArea)
            {
                case StoreArea.输出线圈0x:
                    //单个线圈写入
                    if (values.Length == 1)
                    {
                        wResult = this.modbus.WriteSingleCoil(start, values[0], slaveId);
                    }
                    else
                    {
                        //多个线圈写入
                        wResult = this.modbus.WriteMultipleCoils(start, values, slaveId);
                    }

                    break;
                case StoreArea.保持型寄存器4x:
                    //修改寄存器中的某个位的值（寄存器位写入）
                    if (values.Length == 1)
                    {
                        wResult = this.modbus.WriteRegisterBit(start + "." + count, values[0],
                            this.dataFormat == DataFormat.ABCD || this.dataFormat == DataFormat.CDAB, slaveId);
                    }
                    else
                    {
                        wResult = OperateResult.CreateFailResult("寄存器位写入只支持单个位写入");
                    }
                    break;
                default:
                    wResult = OperateResult.CreateFailResult("暂不支持该存储区");
                    break;
            }

            if (wResult.IsSuccess)
            {
                AddLog(0, "写入成功");
            }
            else
            {
                AddLog(3, "写入失败：" + wResult.Message);
            }
        }


        /// <summary>
        /// 用于写入寄存器
        /// </summary>
        private void WriteShort(StoreArea storeArea, byte slaveId, ushort start, string writeValue)
        {
            short[] values = ShortLib.GetShortArrayFromString(writeValue);
            switch (storeArea)
            {

                case StoreArea.保持型寄存器4x:
                    //写入单寄存器
                    if (values.Length == 1)
                    {
                        var data = ByteArrayLib.GetByteArrayFromShort(values[0], this.dataFormat);
                        wResult = this.modbus.WriteSingleRegister(start, data, slaveId);
                    }
                    else
                    {
                        //写入多寄存器
                        var data = ByteArrayLib.GetByteArrayFromShortArray(values, this.dataFormat);
                        wResult = this.modbus.WriteMultipleRegisters(start, data, slaveId);
                    }
                    break;
                default:
                    wResult = OperateResult.CreateFailResult("暂不支持该存储区");
                    break;
            }

            if (wResult.IsSuccess)
            {
                AddLog(0, "写入成功");
            }
            else
            {
                AddLog(3, "写入失败：" + wResult.Message);
            }

        }

        private void WriteUShort(StoreArea storeArea, byte slaveId, ushort start, string writeValue)
        {
            ushort[] values = UShortLib.GetUShortArrayFromString(writeValue);
            switch (storeArea)
            {

                case StoreArea.保持型寄存器4x:
                    //写入单寄存器
                    if (values.Length == 1)
                    {
                        var data = ByteArrayLib.GetByteArrayFromUShort(values[0], this.dataFormat);
                        wResult = this.modbus.WriteSingleRegister(start, data, slaveId);
                    }
                    else
                    {
                        //写入多寄存器
                        //写入多寄存器
                        var data = ByteArrayLib.GetByteArrayFromUShortArray(values, this.dataFormat);
                        wResult = this.modbus.WriteMultipleRegisters(start, data, slaveId);
                    }
                    break;
                default:
                    wResult = OperateResult.CreateFailResult("暂不支持该存储区");
                    break;
            }

            if (wResult.IsSuccess)
            {
                AddLog(0, "写入成功");
            }
            else
            {
                AddLog(3, "写入失败：" + wResult.Message);
            }

        }



        /// <summary>
        /// 用于写入寄存器
        /// </summary>
        private void WriteInt(StoreArea storeArea, byte slaveId, ushort start, string writeValue)
        {
            int[] values = IntLib.GetIntArrayFromString(writeValue);
            switch (storeArea)
            {

                case StoreArea.保持型寄存器4x:
                    //写入多寄存器
                    var data = ByteArrayLib.GetByteArrayFromIntArray(values, this.dataFormat);
                    wResult = this.modbus.WriteMultipleRegisters(start, data, slaveId);

                    break;
                default:
                    wResult = OperateResult.CreateFailResult("暂不支持该存储区");
                    break;
            }

            if (wResult.IsSuccess)
            {
                AddLog(0, "写入成功");
            }
            else
            {
                AddLog(3, "写入失败：" + wResult.Message);
            }

        }


        /// <summary>
        /// 用于写入寄存器
        /// </summary>
        private void WriteUInt(StoreArea storeArea, byte slaveId, ushort start, string writeValue)
        {
            uint[] values = UIntLib.GetUIntArrayFromString(writeValue);
            switch (storeArea)
            {

                case StoreArea.保持型寄存器4x:
                    //写入多寄存器
                    var data = ByteArrayLib.GetByteArrayFromUIntArray(values, this.dataFormat);
                    wResult = this.modbus.WriteMultipleRegisters(start, data, slaveId);

                    break;
                default:
                    wResult = OperateResult.CreateFailResult("暂不支持该存储区");
                    break;
            }

            if (wResult.IsSuccess)
            {
                AddLog(0, "写入成功");
            }
            else
            {
                AddLog(3, "写入失败：" + wResult.Message);
            }

        }


        /// <summary>
        /// 用于写入寄存器
        /// </summary>
        private void WriteFloat(StoreArea storeArea, byte slaveId, ushort start, string writeValue)
        {
            float[] values = FloatLib.GetFloatArrayFromString(writeValue);
            switch (storeArea)
            {

                case StoreArea.保持型寄存器4x:
                    //写入多寄存器
                    var data = ByteArrayLib.GetByteArrayFromFloatArray(values, this.dataFormat);
                    wResult = this.modbus.WriteMultipleRegisters(start, data, slaveId);

                    break;
                default:
                    wResult = OperateResult.CreateFailResult("暂不支持该存储区");
                    break;
            }

            if (wResult.IsSuccess)
            {
                AddLog(0, "写入成功");
            }
            else
            {
                AddLog(3, "写入失败：" + wResult.Message);
            }

        }




        private T GetEnumValue<T>(string text) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), text, true);
        }
    }
}
