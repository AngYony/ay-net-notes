
namespace xbd.ControlLib
{
    partial class xbdMotor
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.MainPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainPic)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPic
            // 
            this.MainPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPic.Image = global::xbd.ControlLib.Properties.Resources.PumpStop;
            this.MainPic.Location = new System.Drawing.Point(0, 0);
            this.MainPic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainPic.Name = "MainPic";
            this.MainPic.Size = new System.Drawing.Size(89, 127);
            this.MainPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MainPic.TabIndex = 1;
            this.MainPic.TabStop = false;
            this.MainPic.DoubleClick += new System.EventHandler(this.MainPic_DoubleClick);
            // 
            // xbdPump
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainPic);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "xbdPump";
            this.Size = new System.Drawing.Size(89, 127);
            ((System.ComponentModel.ISupportInitialize)(this.MainPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox MainPic;
    }
}
