namespace AY.WarehouseTHPro
{
    partial class Monitor
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
            this.headPanel = new xbd.ControlLib.xbdHeadPanel();
            this.lbl_Humidity = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_temp = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.wave_Humidity = new xbd.ControlLib.xbdWave();
            this.the_Temp = new xbd.ControlLib.xbdThermometer();
            this.headPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headPanel
            // 
            this.headPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.headPanel.BorderColor = System.Drawing.Color.White;
            this.headPanel.Controls.Add(this.lbl_Humidity);
            this.headPanel.Controls.Add(this.label5);
            this.headPanel.Controls.Add(this.lbl_temp);
            this.headPanel.Controls.Add(this.label4);
            this.headPanel.Controls.Add(this.label3);
            this.headPanel.Controls.Add(this.label1);
            this.headPanel.Controls.Add(this.wave_Humidity);
            this.headPanel.Controls.Add(this.the_Temp);
            this.headPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headPanel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.headPanel.ForeColor = System.Drawing.Color.White;
            this.headPanel.HeadHeight = 35;
            this.headPanel.LinearGradientRate = 0.4F;
            this.headPanel.Location = new System.Drawing.Point(0, 0);
            this.headPanel.Name = "headPanel";
            this.headPanel.Size = new System.Drawing.Size(347, 253);
            this.headPanel.TabIndex = 0;
            this.headPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.headPanel.ThemeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(106)))), ((int)(((byte)(189)))));
            this.headPanel.ThemeForeColor = System.Drawing.Color.White;
            this.headPanel.TitleText = "仓库分区：A区-01";
            // 
            // lbl_humidity
            // 
            this.lbl_Humidity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Humidity.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.lbl_Humidity.Location = new System.Drawing.Point(110, 190);
            this.lbl_Humidity.Name = "lbl_humidity";
            this.lbl_Humidity.Size = new System.Drawing.Size(97, 37);
            this.lbl_Humidity.TabIndex = 2;
            this.lbl_Humidity.Text = "0.0";
            this.lbl_Humidity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label5.Location = new System.Drawing.Point(213, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "%";
            // 
            // lbl_temp
            // 
            this.lbl_temp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_temp.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.lbl_temp.Location = new System.Drawing.Point(110, 84);
            this.lbl_temp.Name = "lbl_temp";
            this.lbl_temp.Size = new System.Drawing.Size(97, 37);
            this.lbl_temp.TabIndex = 2;
            this.lbl_temp.Text = "0.0";
            this.lbl_temp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label4.Location = new System.Drawing.Point(106, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "湿度值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label3.Location = new System.Drawing.Point(213, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "℃";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label1.Location = new System.Drawing.Point(106, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "温度值";
            // 
            // wave_humidy
            // 
            this.wave_Humidity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(231)))), ((int)(((byte)(237)))));
            this.wave_Humidity.ConerRadius = 10;
            this.wave_Humidity.FillColor = System.Drawing.Color.Transparent;
            this.wave_Humidity.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wave_Humidity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.wave_Humidity.IsRadius = true;
            this.wave_Humidity.IsRectangle = true;
            this.wave_Humidity.IsShowRect = false;
            this.wave_Humidity.Location = new System.Drawing.Point(261, 56);
            this.wave_Humidity.MaxValue = 100;
            this.wave_Humidity.Name = "wave_humidy";
            this.wave_Humidity.RectColor = System.Drawing.Color.White;
            this.wave_Humidity.RectWidth = 4;
            this.wave_Humidity.Size = new System.Drawing.Size(66, 171);
            this.wave_Humidity.TabIndex = 1;
            this.wave_Humidity.Value = 0;
            this.wave_Humidity.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(106)))), ((int)(((byte)(189)))));
            // 
            // the_temp
            // 
            this.the_Temp.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.the_Temp.GlassTubeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.the_Temp.IsUnitVisiable = false;
            this.the_Temp.LeftTemperatureUnit = xbd.ControlLib.TemperatureUnit.C;
            this.the_Temp.Location = new System.Drawing.Point(19, 48);
            this.the_Temp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.the_Temp.MaxValue = 100F;
            this.the_Temp.MercuryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(106)))), ((int)(((byte)(189)))));
            this.the_Temp.MinValue = 0F;
            this.the_Temp.Name = "the_temp";
            this.the_Temp.RightTemperatureUnit = xbd.ControlLib.TemperatureUnit.C;
            this.the_Temp.Size = new System.Drawing.Size(65, 184);
            this.the_Temp.SplitCount = 1;
            this.the_Temp.TabIndex = 0;
            this.the_Temp.Value = 10F;
            // 
            // Monitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.headPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Monitor";
            this.Size = new System.Drawing.Size(347, 253);
            this.headPanel.ResumeLayout(false);
            this.headPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private xbd.ControlLib.xbdHeadPanel headPanel;
        private xbd.ControlLib.xbdThermometer the_Temp;
        private System.Windows.Forms.Label lbl_Humidity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_temp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private xbd.ControlLib.xbdWave wave_Humidity;
    }
}
