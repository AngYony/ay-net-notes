namespace AY.PressurizationStationPro
{
    partial class FrmReport
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
            this.dtp_time = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgv_data = new System.Windows.Forms.DataGridView();
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl_Exit = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Print = new System.Windows.Forms.Button();
            this.btn_export = new System.Windows.Forms.Button();
            this.btn_Query = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmb_reportType = new System.Windows.Forms.ComboBox();
            this.rdo_max = new System.Windows.Forms.RadioButton();
            this.rdo_min = new System.Windows.Forms.RadioButton();
            this.rdo_avg = new System.Windows.Forms.RadioButton();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtp_time
            // 
            this.dtp_time.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtp_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_time.Location = new System.Drawing.Point(301, 27);
            this.dtp_time.Name = "dtp_time";
            this.dtp_time.Size = new System.Drawing.Size(181, 26);
            this.dtp_time.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(229, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "日期时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(13, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "报表类型";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgv_data);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 131);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10);
            this.panel4.Size = new System.Drawing.Size(1138, 444);
            this.panel4.TabIndex = 4;
            // 
            // dgv_data
            // 
            this.dgv_data.AllowUserToAddRows = false;
            this.dgv_data.AllowUserToDeleteRows = false;
            this.dgv_data.AllowUserToResizeColumns = false;
            this.dgv_data.AllowUserToResizeRows = false;
            this.dgv_data.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.dgv_data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_data.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_data.ColumnHeadersHeight = 45;
            this.dgv_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            this.Column11});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_data.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_data.EnableHeadersVisualStyles = false;
            this.dgv_data.Location = new System.Drawing.Point(10, 10);
            this.dgv_data.MultiSelect = false;
            this.dgv_data.Name = "dgv_data";
            this.dgv_data.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_data.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_data.RowHeadersWidth = 55;
            this.dgv_data.RowTemplate.Height = 35;
            this.dgv_data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_data.Size = new System.Drawing.Size(1118, 424);
            this.dgv_data.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "InsertTime";
            this.Column1.HeaderText = "时间日期";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 180;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "PressureIn";
            this.Column2.HeaderText = "进口压力";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "PressureOut";
            this.Column3.HeaderText = "出口压力";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "TempIn1";
            this.Column4.HeaderText = "进口温度1";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 85;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "TempIn2";
            this.Column5.HeaderText = "进口温度2";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 85;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "TempOut";
            this.Column6.HeaderText = "出口温度";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 85;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "PressureTank1";
            this.Column7.HeaderText = "水箱压力1";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 80;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "PressureTank2";
            this.Column8.HeaderText = "水箱压力2";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 80;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "LevelTank1";
            this.Column9.HeaderText = "水箱液位1";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column9.Width = 80;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "LevelTank2";
            this.Column10.HeaderText = "水箱液位2";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Width = 80;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column11.DataPropertyName = "PressureTankOut";
            this.Column11.HeaderText = "水箱出口压力";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rdo_avg);
            this.panel3.Controls.Add(this.rdo_min);
            this.panel3.Controls.Add(this.rdo_max);
            this.panel3.Controls.Add(this.cmb_reportType);
            this.panel3.Controls.Add(this.btn_Print);
            this.panel3.Controls.Add(this.btn_export);
            this.panel3.Controls.Add(this.btn_Query);
            this.panel3.Controls.Add(this.dtp_time);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 51);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1138, 80);
            this.panel3.TabIndex = 3;
            // 
            // lbl_Exit
            // 
            this.lbl_Exit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_Exit.Font = new System.Drawing.Font("微软雅黑", 25.75F);
            this.lbl_Exit.ForeColor = System.Drawing.Color.White;
            this.lbl_Exit.Location = new System.Drawing.Point(1088, 0);
            this.lbl_Exit.Name = "lbl_Exit";
            this.lbl_Exit.Size = new System.Drawing.Size(50, 50);
            this.lbl_Exit.TabIndex = 2;
            this.lbl_Exit.Text = "×";
            this.lbl_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("微软雅黑", 16.75F);
            this.lbl_Title.ForeColor = System.Drawing.Color.White;
            this.lbl_Title.Location = new System.Drawing.Point(63, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(105, 30);
            this.lbl_Title.TabIndex = 2;
            this.lbl_Title.Text = "数据报表";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1138, 1);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbl_Exit);
            this.panel2.Controls.Add(this.lbl_Title);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1138, 51);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1138, 575);
            this.panel1.TabIndex = 1;
            // 
            // btn_Print
            // 
            this.btn_Print.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.Yellow;
            this.btn_Print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Print.FlatAppearance.BorderSize = 0;
            this.btn_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Print.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.btn_Print.Location = new System.Drawing.Point(1006, 22);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(114, 36);
            this.btn_Print.TabIndex = 2;
            this.btn_Print.Text = "打印记录";
            this.btn_Print.UseVisualStyleBackColor = true;
            // 
            // btn_export
            // 
            this.btn_export.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.Green;
            this.btn_export.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_export.FlatAppearance.BorderSize = 0;
            this.btn_export.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_export.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.btn_export.Location = new System.Drawing.Point(869, 22);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(114, 36);
            this.btn_export.TabIndex = 2;
            this.btn_export.Text = "导出记录";
            this.btn_export.UseVisualStyleBackColor = true;
            // 
            // btn_Query
            // 
            this.btn_Query.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.Pink;
            this.btn_Query.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Query.FlatAppearance.BorderSize = 0;
            this.btn_Query.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Query.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.btn_Query.Location = new System.Drawing.Point(734, 22);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(114, 36);
            this.btn_Query.TabIndex = 2;
            this.btn_Query.Text = "查询记录";
            this.btn_Query.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AY.PressurizationStationPro.Properties.Resources.History;
            this.pictureBox1.Location = new System.Drawing.Point(13, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // cmb_reportType
            // 
            this.cmb_reportType.FormattingEnabled = true;
            this.cmb_reportType.Location = new System.Drawing.Point(85, 26);
            this.cmb_reportType.Name = "cmb_reportType";
            this.cmb_reportType.Size = new System.Drawing.Size(121, 28);
            this.cmb_reportType.TabIndex = 3;
            // 
            // rdo_max
            // 
            this.rdo_max.AutoSize = true;
            this.rdo_max.Checked = true;
            this.rdo_max.ForeColor = System.Drawing.Color.White;
            this.rdo_max.Location = new System.Drawing.Point(490, 28);
            this.rdo_max.Name = "rdo_max";
            this.rdo_max.Size = new System.Drawing.Size(69, 24);
            this.rdo_max.TabIndex = 4;
            this.rdo_max.TabStop = true;
            this.rdo_max.Text = "最大值";
            this.rdo_max.UseVisualStyleBackColor = true;
            // 
            // rdo_min
            // 
            this.rdo_min.AutoSize = true;
            this.rdo_min.ForeColor = System.Drawing.Color.White;
            this.rdo_min.Location = new System.Drawing.Point(565, 28);
            this.rdo_min.Name = "rdo_min";
            this.rdo_min.Size = new System.Drawing.Size(69, 24);
            this.rdo_min.TabIndex = 4;
            this.rdo_min.Text = "最小值";
            this.rdo_min.UseVisualStyleBackColor = true;
            // 
            // rdo_avg
            // 
            this.rdo_avg.AutoSize = true;
            this.rdo_avg.ForeColor = System.Drawing.Color.White;
            this.rdo_avg.Location = new System.Drawing.Point(640, 29);
            this.rdo_avg.Name = "rdo_avg";
            this.rdo_avg.Size = new System.Drawing.Size(69, 24);
            this.rdo_avg.TabIndex = 4;
            this.rdo_avg.Text = "平均值";
            this.rdo_avg.UseVisualStyleBackColor = true;
            // 
            // FrmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1140, 577);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmReport";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmReport";
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.DateTimePicker dtp_time;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgv_data;
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
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_Query;
        private System.Windows.Forms.Label lbl_Exit;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmb_reportType;
        private System.Windows.Forms.RadioButton rdo_avg;
        private System.Windows.Forms.RadioButton rdo_min;
        private System.Windows.Forms.RadioButton rdo_max;
    }
}