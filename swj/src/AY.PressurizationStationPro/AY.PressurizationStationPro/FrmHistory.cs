using AY.BusinessServices;
using AY.Entity;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AY.PressurizationStationPro
{
    public partial class FrmHistory : Form
    {
        private HistoryDataService historyDataService = new HistoryDataService();
        public FrmHistory()
        {
            InitializeComponent();
            this.SetWindowDrag(lbl_Exit, lbl_Title, panel2);
            this.Load += FrmHistory_Load;
            this.dgv_data.AutoGenerateColumns = false;
            this.dtp_start.Value = DateTime.Now.AddHours(-2);
            this.dtp_end.Value = DateTime.Now;
            this.btn_Query.Click += Btn_Query_Click;
            this.dgv_data.Columns[0].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            //绘制行号
            this.dgv_data.RowPostPaint += (sender, e) =>
            {
                this.dgv_data.DgvRowPostPaint(e);
            };
            this.btn_export.Click += Btn_export_Click;
        }

        private void Btn_export_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel文件|*.xlsx",
                Title = "导出历史数据",
                FilterIndex = 1,
                RestoreDirectory = true,
                FileName = $"历史数据_{this.dtp_start.Value:yyyyMMddHHmmss}_{this.dtp_end.Value:yyyyMMddHHmmss}.xlsx"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    MiniExcel.SaveAs(filePath, this.dgv_data.DataSource as List<HistoryData>);
                    Process.Start(filePath);
                }
                catch (Exception ex)
                {
                    new FrmMsgNoAck("导出失败:" + ex.Message, "提示").ShowDialog();
                }
            }
        }

        private void FrmHistory_Load(object sender, EventArgs e)
        {

        }

        private void Btn_Query_Click(object sender, EventArgs e)
        {
            if (this.dtp_start.Value > this.dtp_end.Value)
            {
                new FrmMsgNoAck("开始时间不能大于结束时间，请重新选择时间范围。", "错误").ShowDialog();
                return;
            }

            var result = historyDataService.GetHisotryDataByTime(this.dtp_start.Value, this.dtp_end.Value);
            if (result.IsSuccess)
            {
                if (result.Content.Count == 0)
                {
                    new FrmMsgNoAck("没有查询到符合条件的历史数据。", "提示").ShowDialog();
                    return;
                }

                this.dgv_data.DataSource = null;
                this.dgv_data.DataSource = result.Content;

            }
            else
            {
                new FrmMsgNoAck($"查询历史数据失败：{result.Message}", "错误").ShowDialog();
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            DataGridViewHelper.Print_DataGridView(this.dgv_data);
        }
    }
}
