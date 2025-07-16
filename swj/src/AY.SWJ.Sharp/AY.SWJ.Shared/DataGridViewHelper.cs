using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AY.SWJ.Shared
{
    /// <summary>
    /// DataGridView
    /// </summary>
    public class DataGridViewHelper
    {

        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewHelper.DgvRowPostPaint(sender as DataGridView, e);
        }

        /// <summary>
        /// 给DataGridView添加行号
        /// 通过RowPostPaint事件来调用该方法
        /// </summary>
        /// <param name="dgv">dgv控件</param>
        /// <param name="e">dgv参数</param>
        public static void DgvRowPostPaint(DataGridView dgv, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                //添加行号 
                SolidBrush solidBrush = new SolidBrush(dgv.RowHeadersDefaultCellStyle.ForeColor);
                string lineNo = (e.RowIndex + 1).ToString();

                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                e.Graphics.DrawString(lineNo, e.InheritedRowStyle.Font, solidBrush, new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgv.RowHeadersWidth, dgv.RowTemplate.Height), sf);
            }
            catch (Exception ex)
            {
                throw new Exception("添加行号时发生错误，错误信息：" + ex.Message);
            }
        }
    }
}
