
namespace xbd.ControlLib
{
    partial class xbdMotor
    {
        /// <summary> 
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region �����������ɵĴ���

        /// <summary> 
        /// �����֧������ķ��� - ��Ҫ�޸�
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
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
            this.Font = new System.Drawing.Font("΢���ź�", 10.5F);
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
