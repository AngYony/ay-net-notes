using AY.BusinessServices;
using AY.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AY.PressurizationStationPro
{
    public partial class FrmLogin : Form
    {
        private SysAdminService adminService = new SysAdminService();

        public FrmLogin()
        {
            InitializeComponent();
            this.SetWindowDrag(this.lbl_Exit, this.panel1, this.lbl_Title);
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            var sysAdmins = adminService.QuerySysAdmins();
            if (sysAdmins != null && sysAdmins.Count > 0)
            {
                this.cmb_user.DataSource = sysAdmins;
                this.cmb_user.DisplayMember = "LoginName";
                this.cmb_user.ValueMember = "LoginName";
            }
            else
            {
                new FrmMsgNoAck("没有可用的用户，请先添加用户。", "错误").ShowDialog();
                this.Close();
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.cmb_user.Text))
            {
                new FrmMsgNoAck("用户名不能为空，请输入用户名。", "错误").ShowDialog();
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txt_Pwd.Text))
            {
                new FrmMsgNoAck("密码不能为空，请输入密码。", "错误").ShowDialog();
                return;
            }
            SysAdmin sysAdmin = new SysAdmin()
            {
                LoginName = this.cmb_user.Text.Trim(),
                LoginPwd = this.txt_Pwd.Text.Trim()
            };
            sysAdmin = adminService.AdminLogin(sysAdmin);
            if (sysAdmin == null)
            {
                new FrmMsgNoAck("用户名或密码错误，请重新输入。", "错误").ShowDialog();
            }
            else
            {
                sysAdmin.LoginTime = DateTime.Now;
                Program.CurrentUser = sysAdmin;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}