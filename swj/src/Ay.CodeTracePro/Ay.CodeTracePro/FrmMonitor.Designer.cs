namespace Ay.CodeTracePro
{
    partial class FrmMonitor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.noiseStation2 = new Ay.CodeTrace.ControlLib.NoiseStation();
            this.noiseStation1 = new Ay.CodeTrace.ControlLib.NoiseStation();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.noLoadStation2 = new Ay.CodeTrace.ControlLib.NoLoadStation();
            this.noLoadStation1 = new Ay.CodeTrace.ControlLib.NoLoadStation();
            this.loadStation1 = new Ay.CodeTrace.ControlLib.LoadStation();
            this.loadStation2 = new Ay.CodeTrace.ControlLib.LoadStation();
            this.dgv_Record = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Record)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(386, 322);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "生产监控";
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::Ay.CodeTracePro.Properties.Resources.Border2;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.loadStation2);
            this.panel3.Controls.Add(this.loadStation1);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(1281, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(619, 634);
            this.panel3.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 13.5F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(38, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(539, 1);
            this.label6.TabIndex = 1;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("微软雅黑", 13.5F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(158, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(296, 28);
            this.label7.TabIndex = 1;
            this.label7.Text = "负载检测";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Ay.CodeTracePro.Properties.Resources.Border2;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.noiseStation2);
            this.panel2.Controls.Add(this.noiseStation1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(651, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(619, 634);
            this.panel2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 13.5F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(38, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(539, 1);
            this.label4.TabIndex = 1;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // noiseStation2
            // 
            this.noiseStation2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            this.noiseStation2.DiffNoise = "0.000";
            this.noiseStation2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.noiseStation2.JTName = "机台A4";
            this.noiseStation2.JTState = Ay.CodeTrace.ControlLib.JTState.待机状态;
            this.noiseStation2.Location = new System.Drawing.Point(18, 335);
            this.noiseStation2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.noiseStation2.MotorCode = null;
            this.noiseStation2.Name = "noiseStation2";
            this.noiseStation2.NegativeNoise = "0.000";
            this.noiseStation2.PositiveNoise = "0.000";
            this.noiseStation2.Size = new System.Drawing.Size(583, 293);
            this.noiseStation2.TabIndex = 2;
            // 
            // noiseStation1
            // 
            this.noiseStation1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            this.noiseStation1.DiffNoise = "0.000";
            this.noiseStation1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.noiseStation1.JTName = "机台A3";
            this.noiseStation1.JTState = Ay.CodeTrace.ControlLib.JTState.待机状态;
            this.noiseStation1.Location = new System.Drawing.Point(18, 38);
            this.noiseStation1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.noiseStation1.MotorCode = null;
            this.noiseStation1.Name = "noiseStation1";
            this.noiseStation1.NegativeNoise = "0.000";
            this.noiseStation1.PositiveNoise = "0.000";
            this.noiseStation1.Size = new System.Drawing.Size(583, 293);
            this.noiseStation1.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑", 13.5F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(158, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(296, 28);
            this.label5.TabIndex = 1;
            this.label5.Text = "噪音检测";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Ay.CodeTracePro.Properties.Resources.Border2;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.noLoadStation2);
            this.panel1.Controls.Add(this.noLoadStation1);
            this.panel1.Location = new System.Drawing.Point(21, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(619, 634);
            this.panel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 13.5F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(38, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(539, 1);
            this.label3.TabIndex = 1;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 13.5F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(158, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(296, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "空载检测";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // noLoadStation2
            // 
            this.noLoadStation2.AxisElongation = "0.000";
            this.noLoadStation2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            this.noLoadStation2.Diameter = "0.000";
            this.noLoadStation2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.noLoadStation2.JTName = "机台A2";
            this.noLoadStation2.JTState = Ay.CodeTrace.ControlLib.JTState.待机状态;
            this.noLoadStation2.Location = new System.Drawing.Point(18, 335);
            this.noLoadStation2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.noLoadStation2.MotorCode = null;
            this.noLoadStation2.Name = "noLoadStation2";
            this.noLoadStation2.NoLoadCurrent = "0.000";
            this.noLoadStation2.NoLoadSpeed = "0.000";
            this.noLoadStation2.Size = new System.Drawing.Size(583, 293);
            this.noLoadStation2.TabIndex = 0;
            // 
            // noLoadStation1
            // 
            this.noLoadStation1.AxisElongation = "0.000";
            this.noLoadStation1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            this.noLoadStation1.Diameter = "0.000";
            this.noLoadStation1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.noLoadStation1.JTName = "机台A1";
            this.noLoadStation1.JTState = Ay.CodeTrace.ControlLib.JTState.待机状态;
            this.noLoadStation1.Location = new System.Drawing.Point(18, 38);
            this.noLoadStation1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.noLoadStation1.MotorCode = null;
            this.noLoadStation1.Name = "noLoadStation1";
            this.noLoadStation1.NoLoadCurrent = "0.000";
            this.noLoadStation1.NoLoadSpeed = "0.000";
            this.noLoadStation1.Size = new System.Drawing.Size(583, 293);
            this.noLoadStation1.TabIndex = 0;
            // 
            // loadStation1
            // 
            this.loadStation1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            this.loadStation1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loadStation1.JTName = "机台A5";
            this.loadStation1.JTState = Ay.CodeTrace.ControlLib.JTState.待机状态;
            this.loadStation1.LoadCurrent = "0.000";
            this.loadStation1.LoadSpeed = "0.000";
            this.loadStation1.Location = new System.Drawing.Point(18, 37);
            this.loadStation1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loadStation1.MotorCode = null;
            this.loadStation1.Name = "loadStation1";
            this.loadStation1.Size = new System.Drawing.Size(583, 293);
            this.loadStation1.TabIndex = 2;
            // 
            // loadStation2
            // 
            this.loadStation2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            this.loadStation2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loadStation2.JTName = "机台A6";
            this.loadStation2.JTState = Ay.CodeTrace.ControlLib.JTState.待机状态;
            this.loadStation2.LoadCurrent = "0.000";
            this.loadStation2.LoadSpeed = "0.000";
            this.loadStation2.Location = new System.Drawing.Point(18, 335);
            this.loadStation2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loadStation2.MotorCode = null;
            this.loadStation2.Name = "loadStation2";
            this.loadStation2.Size = new System.Drawing.Size(583, 293);
            this.loadStation2.TabIndex = 2;
            // 
            // dgv_Record
            // 
            this.dgv_Record.AllowUserToAddRows = false;
            this.dgv_Record.AllowUserToDeleteRows = false;
            this.dgv_Record.AllowUserToResizeColumns = false;
            this.dgv_Record.AllowUserToResizeRows = false;
            this.dgv_Record.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            this.dgv_Record.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_Record.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Record.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Record.ColumnHeadersHeight = 38;
            this.dgv_Record.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_Record.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Record.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Record.EnableHeadersVisualStyles = false;
            this.dgv_Record.GridColor = System.Drawing.Color.White;
            this.dgv_Record.Location = new System.Drawing.Point(21, 656);
            this.dgv_Record.Name = "dgv_Record";
            this.dgv_Record.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Record.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_Record.RowHeadersWidth = 50;
            this.dgv_Record.RowTemplate.Height = 23;
            this.dgv_Record.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Record.Size = new System.Drawing.Size(1879, 312);
            this.dgv_Record.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "测试时间";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 190;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "电机条码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 560;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "当前状态";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "空载电流";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "空载转速";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "轴伸长度";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "滚花直径";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "正转噪音";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "反转噪音";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "噪音差值";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "负载电流";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column12.HeaderText = "负载转速";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(29)))), ((int)(((byte)(118)))));
            this.ClientSize = new System.Drawing.Size(1920, 980);
            this.Controls.Add(this.dgv_Record);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmMonitor";
            this.Text = "生产监控";
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Record)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private CodeTrace.ControlLib.NoLoadStation noLoadStation1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private CodeTrace.ControlLib.NoLoadStation noLoadStation2;
        private System.Windows.Forms.Panel panel2;
        private CodeTrace.ControlLib.NoiseStation noiseStation1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private CodeTrace.ControlLib.NoiseStation noiseStation2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private CodeTrace.ControlLib.LoadStation loadStation2;
        private CodeTrace.ControlLib.LoadStation loadStation1;
        private System.Windows.Forms.DataGridView dgv_Record;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
    }
}