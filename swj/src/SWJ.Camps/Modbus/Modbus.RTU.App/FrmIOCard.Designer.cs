namespace Modbus.RTU.App
{
    partial class FrmIOCard
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.topPanel = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.pnl_Output = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pnl_input = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnDisConnect = new System.Windows.Forms.Button();
            this.topPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.pnl_Output.SuspendLayout();
            this.pnl_input.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.topPanel.Controls.Add(this.btnClose);
            this.topPanel.Controls.Add(this.lblTitle);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(730, 53);
            this.topPanel.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(678, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 53);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "×";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(12, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(355, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Modbus 通信协议实现IO采集卡通信";
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.pnl_Output);
            this.mainPanel.Controls.Add(this.pnl_input);
            this.mainPanel.Controls.Add(this.btnConnect);
            this.mainPanel.Controls.Add(this.button2);
            this.mainPanel.Controls.Add(this.btnDisConnect);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 53);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(730, 486);
            this.mainPanel.TabIndex = 1;
            // 
            // pnl_Output
            // 
            this.pnl_Output.Controls.Add(this.label10);
            this.pnl_Output.Controls.Add(this.label11);
            this.pnl_Output.Controls.Add(this.label12);
            this.pnl_Output.Controls.Add(this.label13);
            this.pnl_Output.Controls.Add(this.label14);
            this.pnl_Output.Controls.Add(this.label15);
            this.pnl_Output.Controls.Add(this.label16);
            this.pnl_Output.Controls.Add(this.label17);
            this.pnl_Output.Controls.Add(this.label18);
            this.pnl_Output.Location = new System.Drawing.Point(195, 253);
            this.pnl_Output.Name = "pnl_Output";
            this.pnl_Output.Size = new System.Drawing.Size(457, 189);
            this.pnl_Output.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(35, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 40);
            this.label10.TabIndex = 5;
            this.label10.Tag = "4";
            this.label10.Click += new System.EventHandler(this.PanelOutputLabel_Common_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(35, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 40);
            this.label11.TabIndex = 5;
            this.label11.Tag = "0";
            this.label11.Click += new System.EventHandler(this.PanelOutputLabel_Common_Click);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(141, 120);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 40);
            this.label12.TabIndex = 4;
            this.label12.Tag = "5";
            this.label12.Click += new System.EventHandler(this.PanelOutputLabel_Common_Click);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(141, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 40);
            this.label13.TabIndex = 4;
            this.label13.Tag = "1";
            this.label13.Click += new System.EventHandler(this.PanelOutputLabel_Common_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(247, 120);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 40);
            this.label14.TabIndex = 3;
            this.label14.Tag = "6";
            this.label14.Click += new System.EventHandler(this.PanelOutputLabel_Common_Click);
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(353, 120);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 40);
            this.label15.TabIndex = 2;
            this.label15.Tag = "7";
            this.label15.Click += new System.EventHandler(this.PanelOutputLabel_Common_Click);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(247, 57);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 40);
            this.label16.TabIndex = 3;
            this.label16.Tag = "2";
            this.label16.Click += new System.EventHandler(this.PanelOutputLabel_Common_Click);
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(353, 57);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 40);
            this.label17.TabIndex = 2;
            this.label17.Tag = "3";
            this.label17.Click += new System.EventHandler(this.PanelOutputLabel_Common_Click);
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(457, 40);
            this.label18.TabIndex = 0;
            this.label18.Text = "数据量输出监控";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_input
            // 
            this.pnl_input.Controls.Add(this.label9);
            this.pnl_input.Controls.Add(this.label6);
            this.pnl_input.Controls.Add(this.label8);
            this.pnl_input.Controls.Add(this.label5);
            this.pnl_input.Controls.Add(this.label7);
            this.pnl_input.Controls.Add(this.label2);
            this.pnl_input.Controls.Add(this.label4);
            this.pnl_input.Controls.Add(this.label3);
            this.pnl_input.Controls.Add(this.label1);
            this.pnl_input.Location = new System.Drawing.Point(195, 36);
            this.pnl_input.Name = "pnl_input";
            this.pnl_input.Size = new System.Drawing.Size(457, 189);
            this.pnl_input.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(35, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 40);
            this.label9.TabIndex = 5;
            this.label9.Tag = "4";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(35, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 40);
            this.label6.TabIndex = 5;
            this.label6.Tag = "0";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(141, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 40);
            this.label8.TabIndex = 4;
            this.label8.Tag = "5";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(141, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 40);
            this.label5.TabIndex = 4;
            this.label5.Tag = "1";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(247, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 40);
            this.label7.TabIndex = 3;
            this.label7.Tag = "6";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(353, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 40);
            this.label2.TabIndex = 2;
            this.label2.Tag = "7";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(247, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 40);
            this.label4.TabIndex = 3;
            this.label4.Tag = "2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(353, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 40);
            this.label3.TabIndex = 2;
            this.label3.Tag = "3";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(457, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据量输入监控";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(49, 219);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(109, 40);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "建立连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(49, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 40);
            this.button2.TabIndex = 0;
            this.button2.Text = "参数设置";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnDisConnect
            // 
            this.btnDisConnect.Location = new System.Drawing.Point(49, 282);
            this.btnDisConnect.Name = "btnDisConnect";
            this.btnDisConnect.Size = new System.Drawing.Size(109, 40);
            this.btnDisConnect.TabIndex = 0;
            this.btnDisConnect.Text = "断开连接";
            this.btnDisConnect.UseVisualStyleBackColor = true;
            this.btnDisConnect.Click += new System.EventHandler(this.btnDisConnect_Click);
            // 
            // FrmIOCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 539);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.Name = "FrmIOCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.pnl_Output.ResumeLayout(false);
            this.pnl_input.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnDisConnect;
        private System.Windows.Forms.Panel pnl_input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnl_Output;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
    }
}

