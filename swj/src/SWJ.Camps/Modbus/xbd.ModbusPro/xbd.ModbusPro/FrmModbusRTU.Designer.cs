namespace xbd.ModbusPro
{
    partial class FrmModbusRTU
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmModbusRTU));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupbox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_PortName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_BaudRate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_Parity = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_DataBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_StopBits = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_DataFormat = new System.Windows.Forms.ComboBox();
            this.btn_Open = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_SlaveId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_StoreArea = new System.Windows.Forms.ComboBox();
            this.cmb_DataType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_start = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_count = new System.Windows.Forms.TextBox();
            this.btn_Read = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_Write = new System.Windows.Forms.TextBox();
            this.btn_Write = new System.Windows.Forms.Button();
            this.list_Info = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.groupbox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Close);
            this.groupBox1.Controls.Add(this.btn_Open);
            this.groupBox1.Controls.Add(this.cmb_DataBits);
            this.groupBox1.Controls.Add(this.cmb_Parity);
            this.groupBox1.Controls.Add(this.cmb_DataFormat);
            this.groupBox1.Controls.Add(this.cmb_BaudRate);
            this.groupBox1.Controls.Add(this.cmb_StopBits);
            this.groupBox1.Controls.Add(this.cmb_PortName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(905, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通信参数";
            // 
            // groupbox2
            // 
            this.groupbox2.Controls.Add(this.list_Info);
            this.groupbox2.Controls.Add(this.txt_count);
            this.groupbox2.Controls.Add(this.btn_Write);
            this.groupbox2.Controls.Add(this.btn_Read);
            this.groupbox2.Controls.Add(this.txt_Write);
            this.groupbox2.Controls.Add(this.txt_start);
            this.groupbox2.Controls.Add(this.label11);
            this.groupbox2.Controls.Add(this.txt_SlaveId);
            this.groupbox2.Controls.Add(this.label12);
            this.groupbox2.Controls.Add(this.label10);
            this.groupbox2.Controls.Add(this.label7);
            this.groupbox2.Controls.Add(this.label9);
            this.groupbox2.Controls.Add(this.cmb_DataType);
            this.groupbox2.Controls.Add(this.label8);
            this.groupbox2.Controls.Add(this.cmb_StoreArea);
            this.groupbox2.Location = new System.Drawing.Point(12, 156);
            this.groupbox2.Name = "groupbox2";
            this.groupbox2.Size = new System.Drawing.Size(905, 483);
            this.groupbox2.TabIndex = 0;
            this.groupbox2.TabStop = false;
            this.groupbox2.Text = "读写测试";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "端口号：";
            // 
            // cmb_PortName
            // 
            this.cmb_PortName.FormattingEnabled = true;
            this.cmb_PortName.Location = new System.Drawing.Point(87, 34);
            this.cmb_PortName.Name = "cmb_PortName";
            this.cmb_PortName.Size = new System.Drawing.Size(121, 28);
            this.cmb_PortName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "波特率：";
            // 
            // cmb_BaudRate
            // 
            this.cmb_BaudRate.FormattingEnabled = true;
            this.cmb_BaudRate.Location = new System.Drawing.Point(305, 34);
            this.cmb_BaudRate.Name = "cmb_BaudRate";
            this.cmb_BaudRate.Size = new System.Drawing.Size(121, 28);
            this.cmb_BaudRate.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(464, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "校验位：";
            // 
            // cmb_Parity
            // 
            this.cmb_Parity.FormattingEnabled = true;
            this.cmb_Parity.Location = new System.Drawing.Point(526, 34);
            this.cmb_Parity.Name = "cmb_Parity";
            this.cmb_Parity.Size = new System.Drawing.Size(121, 28);
            this.cmb_Parity.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(689, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "数据位：";
            // 
            // cmb_DataBits
            // 
            this.cmb_DataBits.FormattingEnabled = true;
            this.cmb_DataBits.Location = new System.Drawing.Point(751, 34);
            this.cmb_DataBits.Name = "cmb_DataBits";
            this.cmb_DataBits.Size = new System.Drawing.Size(121, 28);
            this.cmb_DataBits.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "停止位：";
            // 
            // cmb_StopBits
            // 
            this.cmb_StopBits.FormattingEnabled = true;
            this.cmb_StopBits.Location = new System.Drawing.Point(87, 86);
            this.cmb_StopBits.Name = "cmb_StopBits";
            this.cmb_StopBits.Size = new System.Drawing.Size(121, 28);
            this.cmb_StopBits.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(243, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "大小端：";
            // 
            // cmb_DataFormat
            // 
            this.cmb_DataFormat.FormattingEnabled = true;
            this.cmb_DataFormat.Location = new System.Drawing.Point(305, 86);
            this.cmb_DataFormat.Name = "cmb_DataFormat";
            this.cmb_DataFormat.Size = new System.Drawing.Size(121, 28);
            this.cmb_DataFormat.TabIndex = 1;
            // 
            // btn_Open
            // 
            this.btn_Open.Location = new System.Drawing.Point(463, 82);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(101, 35);
            this.btn_Open.TabIndex = 2;
            this.btn_Open.Text = "打开串口";
            this.btn_Open.UseVisualStyleBackColor = true;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(603, 82);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(101, 35);
            this.btn_Close.TabIndex = 2;
            this.btn_Close.Text = "关闭串口";
            this.btn_Close.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "从站地址：";
            // 
            // txt_SlaveId
            // 
            this.txt_SlaveId.Location = new System.Drawing.Point(110, 37);
            this.txt_SlaveId.Name = "txt_SlaveId";
            this.txt_SlaveId.Size = new System.Drawing.Size(150, 26);
            this.txt_SlaveId.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(309, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "存储区：";
            // 
            // cmb_StoreArea
            // 
            this.cmb_StoreArea.FormattingEnabled = true;
            this.cmb_StoreArea.Location = new System.Drawing.Point(380, 37);
            this.cmb_StoreArea.Name = "cmb_StoreArea";
            this.cmb_StoreArea.Size = new System.Drawing.Size(144, 28);
            this.cmb_StoreArea.TabIndex = 1;
            // 
            // cmb_DataType
            // 
            this.cmb_DataType.FormattingEnabled = true;
            this.cmb_DataType.Location = new System.Drawing.Point(686, 37);
            this.cmb_DataType.Name = "cmb_DataType";
            this.cmb_DataType.Size = new System.Drawing.Size(144, 28);
            this.cmb_DataType.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(601, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "数据类型：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 90);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "起始地址：";
            // 
            // txt_start
            // 
            this.txt_start.Location = new System.Drawing.Point(110, 87);
            this.txt_start.Name = "txt_start";
            this.txt_start.Size = new System.Drawing.Size(150, 26);
            this.txt_start.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(295, 93);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "读取数量：";
            // 
            // txt_count
            // 
            this.txt_count.Location = new System.Drawing.Point(380, 90);
            this.txt_count.Name = "txt_count";
            this.txt_count.Size = new System.Drawing.Size(144, 26);
            this.txt_count.TabIndex = 1;
            // 
            // btn_Read
            // 
            this.btn_Read.Location = new System.Drawing.Point(603, 83);
            this.btn_Read.Name = "btn_Read";
            this.btn_Read.Size = new System.Drawing.Size(101, 35);
            this.btn_Read.TabIndex = 2;
            this.btn_Read.Text = "读取数据";
            this.btn_Read.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 147);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(163, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "写入数据（空格分隔）：";
            // 
            // txt_Write
            // 
            this.txt_Write.Location = new System.Drawing.Point(184, 144);
            this.txt_Write.Name = "txt_Write";
            this.txt_Write.Size = new System.Drawing.Size(340, 26);
            this.txt_Write.TabIndex = 1;
            // 
            // btn_Write
            // 
            this.btn_Write.Location = new System.Drawing.Point(603, 140);
            this.btn_Write.Name = "btn_Write";
            this.btn_Write.Size = new System.Drawing.Size(101, 35);
            this.btn_Write.TabIndex = 2;
            this.btn_Write.Text = "写入数据";
            this.btn_Write.UseVisualStyleBackColor = true;
            // 
            // list_Info
            // 
            this.list_Info.BackColor = System.Drawing.SystemColors.Control;
            this.list_Info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.list_Info.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.list_Info.HideSelection = false;
            this.list_Info.Location = new System.Drawing.Point(21, 181);
            this.list_Info.Name = "list_Info";
            this.list_Info.ShowItemToolTips = true;
            this.list_Info.Size = new System.Drawing.Size(862, 279);
            this.list_Info.SmallImageList = this.imageList1;
            this.list_Info.TabIndex = 3;
            this.list_Info.UseCompatibleStateImageBehavior = false;
            this.list_Info.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "日期时间";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "日志信息";
            this.columnHeader2.Width = 460;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "info.ico");
            this.imageList1.Images.SetKeyName(1, "warning.ico");
            this.imageList1.Images.SetKeyName(2, "error.ico");
            // 
            // FrmModbusRTU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 651);
            this.Controls.Add(this.groupbox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmModbusRTU";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupbox2.ResumeLayout(false);
            this.groupbox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupbox2;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.ComboBox cmb_DataBits;
        private System.Windows.Forms.ComboBox cmb_Parity;
        private System.Windows.Forms.ComboBox cmb_DataFormat;
        private System.Windows.Forms.ComboBox cmb_BaudRate;
        private System.Windows.Forms.ComboBox cmb_StopBits;
        private System.Windows.Forms.ComboBox cmb_PortName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_SlaveId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmb_StoreArea;
        private System.Windows.Forms.TextBox txt_count;
        private System.Windows.Forms.Button btn_Write;
        private System.Windows.Forms.Button btn_Read;
        private System.Windows.Forms.TextBox txt_Write;
        private System.Windows.Forms.TextBox txt_start;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmb_DataType;
        private System.Windows.Forms.ListView list_Info;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageList1;
    }
}

