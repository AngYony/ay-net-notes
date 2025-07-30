namespace Ay.CodeTrace.ControlLib
{
    partial class NoLoadStation
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
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_NoLoadCurrent = new System.Windows.Forms.Label();
            this.lbl_NoLoadSpeed = new System.Windows.Forms.Label();
            this.lbl_AxisElongation = new System.Windows.Forms.Label();
            this.lbl_Diameter = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
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
            this.label3.Text = "空载电流：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(357, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "空载转速：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(357, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "轴伸长度：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(357, 253);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "滚花直径：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_NoLoadCurrent
            // 
            this.lbl_NoLoadCurrent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_NoLoadCurrent.ForeColor = System.Drawing.Color.White;
            this.lbl_NoLoadCurrent.Location = new System.Drawing.Point(442, 144);
            this.lbl_NoLoadCurrent.Name = "lbl_NoLoadCurrent";
            this.lbl_NoLoadCurrent.Size = new System.Drawing.Size(72, 25);
            this.lbl_NoLoadCurrent.TabIndex = 2;
            this.lbl_NoLoadCurrent.Text = "0.000";
            this.lbl_NoLoadCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_NoLoadSpeed
            // 
            this.lbl_NoLoadSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_NoLoadSpeed.ForeColor = System.Drawing.Color.White;
            this.lbl_NoLoadSpeed.Location = new System.Drawing.Point(442, 181);
            this.lbl_NoLoadSpeed.Name = "lbl_NoLoadSpeed";
            this.lbl_NoLoadSpeed.Size = new System.Drawing.Size(72, 25);
            this.lbl_NoLoadSpeed.TabIndex = 2;
            this.lbl_NoLoadSpeed.Text = "0.000";
            this.lbl_NoLoadSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AxisElongation
            // 
            this.lbl_AxisElongation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_AxisElongation.ForeColor = System.Drawing.Color.White;
            this.lbl_AxisElongation.Location = new System.Drawing.Point(442, 216);
            this.lbl_AxisElongation.Name = "lbl_AxisElongation";
            this.lbl_AxisElongation.Size = new System.Drawing.Size(72, 25);
            this.lbl_AxisElongation.TabIndex = 2;
            this.lbl_AxisElongation.Text = "0.000";
            this.lbl_AxisElongation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Diameter
            // 
            this.lbl_Diameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Diameter.ForeColor = System.Drawing.Color.White;
            this.lbl_Diameter.Location = new System.Drawing.Point(442, 252);
            this.lbl_Diameter.Name = "lbl_Diameter";
            this.lbl_Diameter.Size = new System.Drawing.Size(72, 25);
            this.lbl_Diameter.TabIndex = 2;
            this.lbl_Diameter.Text = "0.000";
            this.lbl_Diameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(520, 145);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 20);
            this.label11.TabIndex = 2;
            this.label11.Text = "A";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(520, 181);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "r/min";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(520, 217);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 20);
            this.label13.TabIndex = 2;
            this.label13.Text = "mm";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(520, 254);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 20);
            this.label14.TabIndex = 2;
            this.label14.Text = "mm";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NoLoadStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            this.Controls.Add(this.lbl_Diameter);
            this.Controls.Add(this.lbl_AxisElongation);
            this.Controls.Add(this.lbl_NoLoadSpeed);
            this.Controls.Add(this.lbl_NoLoadCurrent);
            this.Controls.Add(this.lbl_MotorCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.station1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "NoLoadStation";
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_NoLoadCurrent;
        private System.Windows.Forms.Label lbl_NoLoadSpeed;
        private System.Windows.Forms.Label lbl_AxisElongation;
        private System.Windows.Forms.Label lbl_Diameter;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}
