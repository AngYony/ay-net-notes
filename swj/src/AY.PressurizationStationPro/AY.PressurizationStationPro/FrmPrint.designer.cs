namespace AY.PressurizationStationPro
{
    partial class FrmPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrint));
            this.rdoSelectedRows = new System.Windows.Forms.RadioButton();
            this.rdoAllRows = new System.Windows.Forms.RadioButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.gboxRowsToPrint = new System.Windows.Forms.GroupBox();
            this.lblColumnsToPrint = new System.Windows.Forms.Label();
            this.chklst = new System.Windows.Forms.CheckedListBox();
            this.chkFitToPageWidth = new System.Windows.Forms.CheckBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.lbl_Close = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.gboxRowsToPrint.SuspendLayout();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoSelectedRows
            // 
            this.rdoSelectedRows.AutoSize = true;
            this.rdoSelectedRows.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.rdoSelectedRows.Location = new System.Drawing.Point(154, 39);
            this.rdoSelectedRows.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoSelectedRows.Name = "rdoSelectedRows";
            this.rdoSelectedRows.Size = new System.Drawing.Size(76, 26);
            this.rdoSelectedRows.TabIndex = 1;
            this.rdoSelectedRows.TabStop = true;
            this.rdoSelectedRows.Text = "选择行";
            this.rdoSelectedRows.UseVisualStyleBackColor = true;
            // 
            // rdoAllRows
            // 
            this.rdoAllRows.AutoSize = true;
            this.rdoAllRows.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.rdoAllRows.Location = new System.Drawing.Point(30, 39);
            this.rdoAllRows.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoAllRows.Name = "rdoAllRows";
            this.rdoAllRows.Size = new System.Drawing.Size(76, 26);
            this.rdoAllRows.TabIndex = 0;
            this.rdoAllRows.TabStop = true;
            this.rdoAllRows.Text = "全部行";
            this.rdoAllRows.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(292, 213);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(74, 21);
            this.lblTitle.TabIndex = 36;
            this.lblTitle.Text = "标题设置";
            // 
            // txtTitle
            // 
            this.txtTitle.AcceptsReturn = true;
            this.txtTitle.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.txtTitle.Location = new System.Drawing.Point(290, 256);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTitle.Multiline = true;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTitle.Size = new System.Drawing.Size(265, 81);
            this.txtTitle.TabIndex = 35;
            this.txtTitle.Text = "请填写要打印的标题名称";
            // 
            // gboxRowsToPrint
            // 
            this.gboxRowsToPrint.BackColor = System.Drawing.Color.Transparent;
            this.gboxRowsToPrint.Controls.Add(this.rdoSelectedRows);
            this.gboxRowsToPrint.Controls.Add(this.rdoAllRows);
            this.gboxRowsToPrint.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.gboxRowsToPrint.ForeColor = System.Drawing.Color.White;
            this.gboxRowsToPrint.Location = new System.Drawing.Point(292, 68);
            this.gboxRowsToPrint.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gboxRowsToPrint.Name = "gboxRowsToPrint";
            this.gboxRowsToPrint.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gboxRowsToPrint.Size = new System.Drawing.Size(263, 82);
            this.gboxRowsToPrint.TabIndex = 34;
            this.gboxRowsToPrint.TabStop = false;
            this.gboxRowsToPrint.Text = "打印行范围";
            // 
            // lblColumnsToPrint
            // 
            this.lblColumnsToPrint.AutoSize = true;
            this.lblColumnsToPrint.BackColor = System.Drawing.Color.Transparent;
            this.lblColumnsToPrint.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.lblColumnsToPrint.ForeColor = System.Drawing.Color.White;
            this.lblColumnsToPrint.Location = new System.Drawing.Point(31, 70);
            this.lblColumnsToPrint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColumnsToPrint.Name = "lblColumnsToPrint";
            this.lblColumnsToPrint.Size = new System.Drawing.Size(122, 21);
            this.lblColumnsToPrint.TabIndex = 33;
            this.lblColumnsToPrint.Text = "选择要打印的列";
            // 
            // chklst
            // 
            this.chklst.CheckOnClick = true;
            this.chklst.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chklst.FormattingEnabled = true;
            this.chklst.Location = new System.Drawing.Point(33, 111);
            this.chklst.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chklst.Name = "chklst";
            this.chklst.Size = new System.Drawing.Size(225, 298);
            this.chklst.TabIndex = 30;
            // 
            // chkFitToPageWidth
            // 
            this.chkFitToPageWidth.AutoSize = true;
            this.chkFitToPageWidth.BackColor = System.Drawing.Color.Transparent;
            this.chkFitToPageWidth.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFitToPageWidth.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.chkFitToPageWidth.ForeColor = System.Drawing.Color.White;
            this.chkFitToPageWidth.Location = new System.Drawing.Point(292, 169);
            this.chkFitToPageWidth.Name = "chkFitToPageWidth";
            this.chkFitToPageWidth.Size = new System.Drawing.Size(93, 25);
            this.chkFitToPageWidth.TabIndex = 41;
            this.chkFitToPageWidth.Text = "适应页宽";
            this.chkFitToPageWidth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkFitToPageWidth.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.chkFitToPageWidth.UseVisualStyleBackColor = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel.Location = new System.Drawing.Point(436, 369);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(97, 38);
            this.btn_Cancel.TabIndex = 42;
            this.btn_Cancel.Text = "取消打印";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_OK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_OK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OK.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.btn_OK.ForeColor = System.Drawing.Color.White;
            this.btn_OK.Location = new System.Drawing.Point(306, 369);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(92, 38);
            this.btn_OK.TabIndex = 43;
            this.btn_OK.Text = "确认打印";
            this.btn_OK.UseVisualStyleBackColor = false;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.lbl_Close);
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Controls.Add(this.pictureBox1);
            this.TopPanel.Controls.Add(this.label3);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(4);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(578, 53);
            this.TopPanel.TabIndex = 45;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            // 
            // lbl_Close
            // 
            this.lbl_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_Close.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Close.ForeColor = System.Drawing.Color.White;
            this.lbl_Close.Location = new System.Drawing.Point(530, 0);
            this.lbl_Close.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Close.Name = "lbl_Close";
            this.lbl_Close.Size = new System.Drawing.Size(48, 52);
            this.lbl_Close.TabIndex = 1;
            this.lbl_Close.Text = "X";
            this.lbl_Close.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(60, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "打印确认";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AY.PressurizationStationPro.Properties.Resources.Param;
            this.pictureBox1.Location = new System.Drawing.Point(18, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(0, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(578, 1);
            this.label3.TabIndex = 2;
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.MainPanel.Controls.Add(this.lblColumnsToPrint);
            this.MainPanel.Controls.Add(this.TopPanel);
            this.MainPanel.Controls.Add(this.btn_Cancel);
            this.MainPanel.Controls.Add(this.chklst);
            this.MainPanel.Controls.Add(this.btn_OK);
            this.MainPanel.Controls.Add(this.gboxRowsToPrint);
            this.MainPanel.Controls.Add(this.chkFitToPageWidth);
            this.MainPanel.Controls.Add(this.txtTitle);
            this.MainPanel.Controls.Add(this.lblTitle);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(1, 1);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(578, 432);
            this.MainPanel.TabIndex = 46;
            // 
            // FrmPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(580, 434);
            this.Controls.Add(this.MainPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmPrint";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印设置";
            this.gboxRowsToPrint.ResumeLayout(false);
            this.gboxRowsToPrint.PerformLayout();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.RadioButton rdoSelectedRows;
        internal System.Windows.Forms.RadioButton rdoAllRows;
        internal System.Windows.Forms.Label lblTitle;
        internal System.Windows.Forms.TextBox txtTitle;
        internal System.Windows.Forms.GroupBox gboxRowsToPrint;
        internal System.Windows.Forms.Label lblColumnsToPrint;
        internal System.Windows.Forms.CheckedListBox chklst;
        private System.Windows.Forms.CheckBox chkFitToPageWidth;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label lbl_Close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel MainPanel;
    }
}