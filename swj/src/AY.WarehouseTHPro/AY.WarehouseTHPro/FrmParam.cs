using AY.WarehouseTH.BLL;
using AY.WarehouseTH.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xbd.DataConvertLib;

namespace AY.WarehouseTHPro
{
    public partial class FrmParam : Form
    {
        private SysInfo sysInfo = null;
        private readonly string path;

        public FrmParam(string path,SysInfo sysInfo )
        {
            InitializeComponent();
            InitialCmb();
            this.btnOk.Click += BtnOK_Click;
            this.path = path;
            this.sysInfo = sysInfo;
            LadSysInfo();
            this.btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            LadSysInfo();
        }

        

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (this.sysInfo == null)
            {
                this.sysInfo = new SysInfo();
            }

            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    if (textBox.Tag != null && textBox.Tag.ToString().Length > 0)
                    {
                        SetObjectPropertyValue(this.sysInfo, textBox.Tag.ToString(), textBox.Text);
                    }
                }
                else if (control is ComboBox comboBox)
                {
                    if (comboBox.Tag != null && comboBox.Tag.ToString().Length > 0)
                    {
                        SetObjectPropertyValue(this.sysInfo, comboBox.Tag.ToString(), comboBox.Text);
                    }
                }
            }

            var result = SysInfoManager.SetSysInfoToFile(this.sysInfo, this.path);
            if (result.IsSuccess)
            {
                FrmMsgNoAck.Show("保存成功");
            }
            else
            {
                FrmMsgNoAck.Show($"保存失败: {result.Message}");
            }
        }

        private void LadSysInfo()
        {
            if (this.sysInfo == null)
            {
                this.sysInfo = new SysInfo();
            }
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox && textBox.Tag != null && textBox.Tag.ToString().Length > 0)
                {
                    var result = GetOjbectPropertyValue(this.sysInfo, textBox.Tag.ToString());
                    if (result.IsSuccess)
                    {
                        textBox.Text = result.Content?.ToString() ?? string.Empty;
                    }
                    else
                    {
                        FrmMsgNoAck.Show($"获取属性 {textBox.Tag} 失败: {result.Message}");
                    }
                }
                else if (control is ComboBox comboBox && comboBox.Tag != null && comboBox.Tag.ToString().Length > 0)
                {
                    var result = GetOjbectPropertyValue(this.sysInfo, comboBox.Tag.ToString());
                    if (result.IsSuccess)
                    {
                        comboBox.Text = result.Content?.ToString() ?? string.Empty;
                    }
                    else
                    {
                        FrmMsgNoAck.Show($"获取属性 {comboBox.Tag} 失败: {result.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// 根据属性名称设置对象的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private OperateResult SetObjectPropertyValue<T>(T obj, string propertyName, object value)
        {
            try
            {
                var property = typeof(T).GetProperty(propertyName);
                if (property == null)
                {
                    return OperateResult.CreateFailResult($"属性 {propertyName} 不存在");
                }
                if (!property.CanWrite)
                {
                    return OperateResult.CreateFailResult($"属性 {propertyName} 不能写入");
                }
                property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult($"设置属性 {propertyName} 失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 根据属性名称获取对象的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private OperateResult<object> GetOjbectPropertyValue<T>(T obj,string propertyName)
        {
            try
            {
                var property = typeof(T).GetProperty(propertyName);
                if (property == null)
                {
                    return OperateResult.CreateFailResult<object>($"属性 {propertyName} 不存在");
                }
                if (!property.CanRead)
                {
                    return OperateResult.CreateFailResult<object>($"属性 {propertyName} 不能读取");
                }
                var value = property.GetValue(obj);
                return OperateResult.CreateSuccessResult(value);
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<object>($"获取属性 {propertyName} 失败: {ex.Message}");
            }
        }

        private void InitialCmb()
        {
            this.cmb_PortNameA.Items.AddRange(SerialPort.GetPortNames());
            this.cmb_PortNameB.Items.AddRange(SerialPort.GetPortNames());

            this.cmb_BaudRateA.Items.AddRange(new object[] { 9600, 19200, 38400, 57600, 115200 });
            this.cmb_BaudRateB.Items.AddRange(new object[] { 9600, 19200, 38400, 57600, 115200 });

            this.cmb_ParityA.Items.AddRange(Enum.GetNames(typeof(Parity)));
            this.cmb_ParityB.Items.AddRange(Enum.GetNames(typeof(Parity)));

            this.cmb_DataBitsA.Items.AddRange(new object[] { 7, 8 });
            this.cmb_DataBitsB.Items.AddRange(new object[] { 7, 8 });

            this.cmb_StopBitsA.Items.AddRange(Enum.GetNames(typeof(StopBits)));
            this.cmb_StopBitsB.Items.AddRange(Enum.GetNames(typeof(StopBits)));

        }
    }
}
