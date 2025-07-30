using AY.NodeSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xbd.WarehouseTHPro;

namespace AY.WarehouseTHPro
{
    public partial class FrmTrend : Form
    {
        public FrmTrend()
        {
            InitializeComponent();
            SetActualTrendStyle();

        }

        private void SetActualTrendStyle()
        {
            //设置X轴类型
            this.chartTrend.XDataType = SeeSharpTools.JY.GUI.StripChartX.XAxisDataType.TimeStamp;
            this.chartTrend.TimeStampFormat = "HH:mm:ss";

            //设置Y轴范围
            this.chartTrend.AxisY.Minimum = 0;
            this.chartTrend.AxisY.Maximum = 100;
            this.chartTrend.AxisY.AutoScale = false;

            //清除曲线
            this.chartTrend.Series.Clear();
            //设置曲线数量
            this.chartTrend.SeriesCount = 12;
            //添加曲线
            if (CommonMethods.DeviceZoneA.VariableDicList.Count > 0 &&
                CommonMethods.DeviceZoneB.VariableDicList.Count > 0)
            {
                var varlist = new List<ModbusRTUVariable>();
                varlist.AddRange(CommonMethods.DeviceZoneA.VariableDicList.Values);
                varlist.AddRange(CommonMethods.DeviceZoneB.VariableDicList.Values);

                for (int i = 0; i < varlist.Count; i++)
                {
                    this.chartTrend.Series[i].Name = varlist[i].VarName;
                    //this.chartTrend.Series[i].Color = Color.FromArgb(255, 0, 0);
                    this.chartTrend.Series[i].Visible = false;

                    //设置曲线样式
                    this.chartTrend.Series[i].Width = SeeSharpTools.JY.GUI.StripChartXSeries.LineWidth.Middle;
                    var chx = this.panel2.Controls.OfType<xbdCheckBox>().FirstOrDefault(c => c.Text == varlist[i].VarName);
                    if (chx != null)
                    {
                        chx.ForeColor = this.chartTrend.Series[i].Color;
                        //默认勾选
                        if (chx.Text == "A01温度" || chx.Text == "B01温度")
                        {
                            chx.Checked = true;
                        }
                    }
                }


            }
        }

        private void Common_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is xbdCheckBox chx)
            {
                var series = this.chartTrend.Series.FirstOrDefault(s => s.Name == chx.Text);
                if (series != null)
                {
                    series.Visible = chx.Checked;
                }
            }
        }


        private List<double> yData = new List<double>();
        public void UpdateTrend()
        {
            yData.Clear();
            var labels = this.panel1.Controls.OfType<Label>().ToList();

            foreach (var item in CommonMethods.DeviceZoneA.VariableDicList)
            {
                if (CommonMethods.DeviceZoneA.IsConnected)
                {
                    yData.Add(Convert.ToDouble(CommonMethods.DeviceZoneA.CurrentValue[item.Key]));
                    labels.FirstOrDefault(a => item.Key.Equals(a.Tag)).Text = CommonMethods.DeviceZoneA.CurrentValue[item.Key].ToString();
                }
                else
                {
                    yData.Add(0.0);
                    labels.FirstOrDefault(a => item.Key.Equals(a.Tag)).Text = "00.00";
                }
            }

            foreach (var item in CommonMethods.DeviceZoneB.VariableDicList)
            {
                if (CommonMethods.DeviceZoneB.IsConnected)
                {
                    yData.Add(Convert.ToDouble(CommonMethods.DeviceZoneB.CurrentValue[item.Key]));
                    labels.FirstOrDefault(a => item.Key.Equals(a.Tag)).Text = CommonMethods.DeviceZoneB.CurrentValue[item.Key].ToString();
                }
                else
                {
                    yData.Add(0.0);
                    labels.FirstOrDefault(a => item.Key.Equals(a.Tag)).Text = "00.0";

                }
            }

            this.chartTrend.PlotSingle(yData.ToArray());
        }



    }
}
