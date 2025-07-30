namespace Ay.CodeTrace.ControlLib
{
    partial class NoiseStation
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
            this.station1 = new Ay.CodeTrace.ControlLib.Station();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_MotorCode = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_PositiveNoise = new System.Windows.Forms.Label();
            this.lbl_NegativeNoise = new System.Windows.Forms.Label();
            this.lbl_DiffNoise = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // station1
            // 
            this.station1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            this.station1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.station1.JTName = "机台A1";
            this.station1.JTState = Ay.CodeTrace.ControlLib.JTState.待机状态;
            this.station1.Location = new System.Drawing.Point(12, 14);
            this.station1.Margin = new System.Windows.Forms.Padding(5);
            this.station1.Name = "station1";
            this.station1.Size = new System.Drawing.Size(314, 263);
            this.station1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(357, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "电机条码：";
            // 
            // lbl_MotorCode
            // 
            this.lbl_MotorCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_MotorCode.ForeColor = System.Drawing.Color.White;
            this.lbl_MotorCode.Location = new System.Drawing.Point(357, 50);
            this.lbl_MotorCode.Name = "lbl_MotorCode";
            this.lbl_MotorCode.Size = new System.Drawing.Size(198, 79);
            this.lbl_MotorCode.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(357, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "正转噪音：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(357, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "反转噪音：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(357, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "噪音差值：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_NoLoadCurrent
            // 
            this.lbl_PositiveNoise.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_PositiveNoise.ForeColor = System.Drawing.Color.White;
            this.lbl_PositiveNoise.Location = new System.Drawing.Point(442, 144);
            this.lbl_PositiveNoise.Name = "lbl_NoLoadCurrent";
            this.lbl_PositiveNoise.Size = new System.Drawing.Size(72, 25);
            this.lbl_PositiveNoise.TabIndex = 2;
            this.lbl_PositiveNoise.Text = "0.0";
            this.lbl_PositiveNoise.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_NoLoadSpeed
            // 
            this.lbl_NegativeNoise.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_NegativeNoise.ForeColor = System.Drawing.Color.White;
            this.lbl_NegativeNoise.Location = new System.Drawing.Point(442, 193);
            this.lbl_NegativeNoise.Name = "lbl_NoLoadSpeed";
            this.lbl_NegativeNoise.Size = new System.Drawing.Size(72, 25);
            this.lbl_NegativeNoise.TabIndex = 2;
            this.lbl_NegativeNoise.Text = "0.0";
            this.lbl_NegativeNoise.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AxisElongation
            // 
            this.lbl_DiffNoise.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_DiffNoise.ForeColor = System.Drawing.Color.White;
            this.lbl_DiffNoise.Location = new System.Drawing.Point(442, 241);
            this.lbl_DiffNoise.Name = "lbl_AxisElongation";
            this.lbl_DiffNoise.Size = new System.Drawing.Size(72, 25);
            this.lbl_DiffNoise.TabIndex = 2;
            this.lbl_DiffNoise.Text = "0.0";
            this.lbl_DiffNoise.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(520, 145);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 20);
            this.label11.TabIndex = 2;
            this.label11.Text = "db";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(520, 193);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "db";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(520, 242);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 20);
            this.label13.TabIndex = 2;
            this.label13.Text = "db";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NoiseStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            this.Controls.Add(this.lbl_DiffNoise);
            this.Controls.Add(this.lbl_NegativeNoise);
            this.Controls.Add(this.lbl_PositiveNoise);
            this.Controls.Add(this.lbl_MotorCode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.station1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "NoiseStation";
            this.Size = new System.Drawing.Size(583, 293);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Station station1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_MotorCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_PositiveNoise;
        private System.Windows.Forms.Label lbl_NegativeNoise;
        private System.Windows.Forms.Label lbl_DiffNoise;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}
