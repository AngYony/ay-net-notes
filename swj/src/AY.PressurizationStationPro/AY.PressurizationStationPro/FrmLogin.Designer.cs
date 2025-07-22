namespace AY.PressurizationStationPro
{
    partial class FrmLogin
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Login = new System.Windows.Forms.Button();
            this.txt_Pwd = new System.Windows.Forms.TextBox();
            this.cmb_user = new System.Windows.Forms.ComboBox();
            this.lbl_Exit = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.Login;
            this.panel1.Controls.Add(this.btn_Login);
            this.panel1.Controls.Add(this.txt_Pwd);
            this.panel1.Controls.Add(this.cmb_user);
            this.panel1.Controls.Add(this.lbl_Exit);
            this.panel1.Controls.Add(this.lbl_Title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(433, 315);
            this.panel1.TabIndex = 0;
            // 
            // btn_Login
            // 
            this.btn_Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(20)))), ((int)(((byte)(62)))));
            this.btn_Login.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Login.ForeColor = System.Drawing.Color.White;
            this.btn_Login.Location = new System.Drawing.Point(118, 226);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(206, 30);
            this.btn_Login.TabIndex = 5;
            this.btn_Login.Text = "登  录";
            this.btn_Login.UseVisualStyleBackColor = false;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // txt_Pwd
            // 
            this.txt_Pwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(20)))), ((int)(((byte)(62)))));
            this.txt_Pwd.ForeColor = System.Drawing.Color.White;
            this.txt_Pwd.Location = new System.Drawing.Point(118, 184);
            this.txt_Pwd.Name = "txt_Pwd";
            this.txt_Pwd.PasswordChar = '*';
            this.txt_Pwd.ShortcutsEnabled = false;
            this.txt_Pwd.Size = new System.Drawing.Size(206, 26);
            this.txt_Pwd.TabIndex = 4;
            // 
            // cmb_user
            // 
            this.cmb_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(20)))), ((int)(((byte)(62)))));
            this.cmb_user.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_user.ForeColor = System.Drawing.Color.White;
            this.cmb_user.FormattingEnabled = true;
            this.cmb_user.Location = new System.Drawing.Point(118, 142);
            this.cmb_user.Name = "cmb_user";
            this.cmb_user.Size = new System.Drawing.Size(206, 28);
            this.cmb_user.TabIndex = 3;
            // 
            // lbl_Exit
            // 
            this.lbl_Exit.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Exit.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.lbl_Exit.ForeColor = System.Drawing.Color.White;
            this.lbl_Exit.Location = new System.Drawing.Point(393, 0);
            this.lbl_Exit.Name = "lbl_Exit";
            this.lbl_Exit.Size = new System.Drawing.Size(40, 40);
            this.lbl_Exit.TabIndex = 2;
            this.lbl_Exit.Text = "×";
            this.lbl_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Title.Font = new System.Drawing.Font("微软雅黑", 13.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.ForeColor = System.Drawing.Color.White;
            this.lbl_Title.Location = new System.Drawing.Point(22, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(239, 25);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "智慧加压站SCADA监控系统";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.btn_Login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(433, 315);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_Exit;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.TextBox txt_Pwd;
        private System.Windows.Forms.ComboBox cmb_user;
    }
}