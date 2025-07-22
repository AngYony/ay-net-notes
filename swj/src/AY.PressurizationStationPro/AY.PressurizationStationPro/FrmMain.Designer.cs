namespace AY.PressurizationStationPro
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label45 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lbl_TempOut = new System.Windows.Forms.Label();
            this.lbl_PreTankOut = new System.Windows.Forms.Label();
            this.lbl_TempIn2 = new System.Windows.Forms.Label();
            this.lbl_TempIn1 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.toggle_Pump1 = new xbd.ControlLib.xbdToggle();
            this.toggle_Pump2 = new xbd.ControlLib.xbdToggle();
            this.motor_Pump1 = new xbd.ControlLib.xbdMotor();
            this.motor_Pump2 = new xbd.ControlLib.xbdMotor();
            this.xbdFlowControl5 = new xbd.ControlLib.xbdFlowControl();
            this.xbdFlowControl9 = new xbd.ControlLib.xbdFlowControl();
            this.xbdFlowControl8 = new xbd.ControlLib.xbdFlowControl();
            this.xbdFlowControl6 = new xbd.ControlLib.xbdFlowControl();
            this.xbdFlowControl7 = new xbd.ControlLib.xbdFlowControl();
            this.xbdFlowControl4 = new xbd.ControlLib.xbdFlowControl();
            this.wave_Tank2 = new xbd.ControlLib.xbdWave();
            this.wave_Tank1 = new xbd.ControlLib.xbdWave();
            this.pump_In2 = new xbd.ControlLib.xbdPump();
            this.pump_In1 = new xbd.ControlLib.xbdPump();
            this.xbdFlowControl11 = new xbd.ControlLib.xbdFlowControl();
            this.xbdFlowControl3 = new xbd.ControlLib.xbdFlowControl();
            this.xbdFlowControl10 = new xbd.ControlLib.xbdFlowControl();
            this.xbdFlowControl2 = new xbd.ControlLib.xbdFlowControl();
            this.xbdFlowControl1 = new xbd.ControlLib.xbdFlowControl();
            this.btn_Pump2 = new System.Windows.Forms.Button();
            this.btn_Pump1 = new System.Windows.Forms.Button();
            this.btn_SysReset = new System.Windows.Forms.Button();
            this.btn_userLogin = new System.Windows.Forms.Button();
            this.valve_Out = new xbd.ControlLib.xbdValve();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.valve_In = new xbd.ControlLib.xbdValve();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ms_PressureTankOut = new AY.PressurizationStationPro.MeterShow();
            this.ms_PressureTank2 = new AY.PressurizationStationPro.MeterShow();
            this.ms_PressureTank1 = new AY.PressurizationStationPro.MeterShow();
            this.ms_TempOut = new AY.PressurizationStationPro.MeterShow();
            this.ms_TempIn2 = new AY.PressurizationStationPro.MeterShow();
            this.ms_TempIn1 = new AY.PressurizationStationPro.MeterShow();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_PressureTankOut = new System.Windows.Forms.Label();
            this.lbl_LevelTank2 = new System.Windows.Forms.Label();
            this.lbl_PressureTank2 = new System.Windows.Forms.Label();
            this.lbl_LevelTank1 = new System.Windows.Forms.Label();
            this.lbl_PressureTank1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.vsp_panel = new AForge.Controls.VideoSourcePlayer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.led_SysAlarmState = new xbd.ControlLib.xbdState();
            this.label10 = new System.Windows.Forms.Label();
            this.led_RunState = new xbd.ControlLib.xbdState();
            this.label8 = new System.Windows.Forms.Label();
            this.led_PLCState = new xbd.ControlLib.xbdState();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl_PressureOut = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.meter_PressureOut = new xbd.ControlLib.xbdAnalogMeter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_PressureIn = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.meter_PressureIn = new xbd.ControlLib.xbdAnalogMeter();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.lbl_LoginName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_ParamSet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label45);
            this.panel1.Controls.Add(this.label50);
            this.panel1.Controls.Add(this.label41);
            this.panel1.Controls.Add(this.btn_Pump2);
            this.panel1.Controls.Add(this.btn_Pump1);
            this.panel1.Controls.Add(this.label37);
            this.panel1.Controls.Add(this.btn_SysReset);
            this.panel1.Controls.Add(this.btn_userLogin);
            this.panel1.Controls.Add(this.lbl_TempOut);
            this.panel1.Controls.Add(this.lbl_PreTankOut);
            this.panel1.Controls.Add(this.lbl_TempIn2);
            this.panel1.Controls.Add(this.lbl_TempIn1);
            this.panel1.Controls.Add(this.label48);
            this.panel1.Controls.Add(this.label43);
            this.panel1.Controls.Add(this.label39);
            this.panel1.Controls.Add(this.toggle_Pump1);
            this.panel1.Controls.Add(this.label35);
            this.panel1.Controls.Add(this.label42);
            this.panel1.Controls.Add(this.label38);
            this.panel1.Controls.Add(this.valve_Out);
            this.panel1.Controls.Add(this.label47);
            this.panel1.Controls.Add(this.label46);
            this.panel1.Controls.Add(this.label34);
            this.panel1.Controls.Add(this.toggle_Pump2);
            this.panel1.Controls.Add(this.motor_Pump1);
            this.panel1.Controls.Add(this.motor_Pump2);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.xbdFlowControl5);
            this.panel1.Controls.Add(this.xbdFlowControl9);
            this.panel1.Controls.Add(this.xbdFlowControl8);
            this.panel1.Controls.Add(this.xbdFlowControl6);
            this.panel1.Controls.Add(this.xbdFlowControl7);
            this.panel1.Controls.Add(this.xbdFlowControl4);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.label33);
            this.panel1.Controls.Add(this.label32);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.wave_Tank2);
            this.panel1.Controls.Add(this.wave_Tank1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.valve_In);
            this.panel1.Controls.Add(this.pump_In2);
            this.panel1.Controls.Add(this.pump_In1);
            this.panel1.Controls.Add(this.xbdFlowControl11);
            this.panel1.Controls.Add(this.xbdFlowControl3);
            this.panel1.Controls.Add(this.xbdFlowControl10);
            this.panel1.Controls.Add(this.xbdFlowControl2);
            this.panel1.Controls.Add(this.xbdFlowControl1);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 643);
            this.panel1.TabIndex = 1;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.BackColor = System.Drawing.Color.Transparent;
            this.label45.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label45.ForeColor = System.Drawing.Color.White;
            this.label45.Location = new System.Drawing.Point(313, 381);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(31, 25);
            this.label45.TabIndex = 0;
            this.label45.Text = "℃";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.BackColor = System.Drawing.Color.Transparent;
            this.label50.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label50.ForeColor = System.Drawing.Color.White;
            this.label50.Location = new System.Drawing.Point(948, 333);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(42, 25);
            this.label50.TabIndex = 0;
            this.label50.Text = "bar";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label41.ForeColor = System.Drawing.Color.White;
            this.label41.Location = new System.Drawing.Point(313, 289);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(31, 25);
            this.label41.TabIndex = 0;
            this.label41.Text = "℃";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label37.ForeColor = System.Drawing.Color.White;
            this.label37.Location = new System.Drawing.Point(314, 119);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(31, 25);
            this.label37.TabIndex = 0;
            this.label37.Text = "℃";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_TempOut
            // 
            this.lbl_TempOut.AutoSize = true;
            this.lbl_TempOut.BackColor = System.Drawing.Color.Transparent;
            this.lbl_TempOut.Font = new System.Drawing.Font("DigifaceWide", 18F);
            this.lbl_TempOut.ForeColor = System.Drawing.Color.White;
            this.lbl_TempOut.Location = new System.Drawing.Point(232, 378);
            this.lbl_TempOut.Name = "lbl_TempOut";
            this.lbl_TempOut.Size = new System.Drawing.Size(90, 29);
            this.lbl_TempOut.TabIndex = 10;
            this.lbl_TempOut.Text = "00.00";
            // 
            // lbl_PreTankOut
            // 
            this.lbl_PreTankOut.AutoSize = true;
            this.lbl_PreTankOut.BackColor = System.Drawing.Color.Transparent;
            this.lbl_PreTankOut.Font = new System.Drawing.Font("DigifaceWide", 18F);
            this.lbl_PreTankOut.ForeColor = System.Drawing.Color.White;
            this.lbl_PreTankOut.Location = new System.Drawing.Point(862, 330);
            this.lbl_PreTankOut.Name = "lbl_PreTankOut";
            this.lbl_PreTankOut.Size = new System.Drawing.Size(90, 29);
            this.lbl_PreTankOut.TabIndex = 10;
            this.lbl_PreTankOut.Text = "00.00";
            // 
            // lbl_TempIn2
            // 
            this.lbl_TempIn2.AutoSize = true;
            this.lbl_TempIn2.BackColor = System.Drawing.Color.Transparent;
            this.lbl_TempIn2.Font = new System.Drawing.Font("DigifaceWide", 18F);
            this.lbl_TempIn2.ForeColor = System.Drawing.Color.White;
            this.lbl_TempIn2.Location = new System.Drawing.Point(232, 286);
            this.lbl_TempIn2.Name = "lbl_TempIn2";
            this.lbl_TempIn2.Size = new System.Drawing.Size(90, 29);
            this.lbl_TempIn2.TabIndex = 10;
            this.lbl_TempIn2.Text = "00.00";
            // 
            // lbl_TempIn1
            // 
            this.lbl_TempIn1.AutoSize = true;
            this.lbl_TempIn1.BackColor = System.Drawing.Color.Transparent;
            this.lbl_TempIn1.Font = new System.Drawing.Font("DigifaceWide", 18F);
            this.lbl_TempIn1.ForeColor = System.Drawing.Color.White;
            this.lbl_TempIn1.Location = new System.Drawing.Point(233, 116);
            this.lbl_TempIn1.Name = "lbl_TempIn1";
            this.lbl_TempIn1.Size = new System.Drawing.Size(90, 29);
            this.lbl_TempIn1.TabIndex = 10;
            this.lbl_TempIn1.Text = "00.00";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.BackColor = System.Drawing.Color.Transparent;
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label48.Location = new System.Drawing.Point(872, 301);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(65, 20);
            this.label48.TabIndex = 9;
            this.label48.Text = "出口压力";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.label43.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label43.Location = new System.Drawing.Point(232, 353);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(65, 20);
            this.label43.TabIndex = 9;
            this.label43.Text = "出水温度";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label39.Location = new System.Drawing.Point(232, 261);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(65, 20);
            this.label39.TabIndex = 9;
            this.label39.Text = "进水温度";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label35.Location = new System.Drawing.Point(233, 91);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(65, 20);
            this.label35.TabIndex = 9;
            this.label35.Text = "进水温度";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label42.Location = new System.Drawing.Point(233, 418);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(65, 20);
            this.label42.TabIndex = 0;
            this.label42.Text = "总出水管";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label38.Location = new System.Drawing.Point(232, 216);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(68, 20);
            this.label38.TabIndex = 0;
            this.label38.Text = "2#进水管";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.BackColor = System.Drawing.Color.Transparent;
            this.label47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label47.Location = new System.Drawing.Point(980, 111);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(65, 20);
            this.label47.TabIndex = 0;
            this.label47.Text = "水箱液位";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.BackColor = System.Drawing.Color.Transparent;
            this.label46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label46.Location = new System.Drawing.Point(711, 111);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(65, 20);
            this.label46.TabIndex = 0;
            this.label46.Text = "水箱液位";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label34.Location = new System.Drawing.Point(233, 46);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(68, 20);
            this.label34.TabIndex = 0;
            this.label34.Text = "1#进水管";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(806, 124);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(54, 20);
            this.label29.TabIndex = 0;
            this.label29.Text = "1#水箱";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.ForeColor = System.Drawing.Color.White;
            this.label28.Location = new System.Drawing.Point(559, 124);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(54, 20);
            this.label28.TabIndex = 0;
            this.label28.Text = "1#水箱";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label33.ForeColor = System.Drawing.Color.White;
            this.label33.Location = new System.Drawing.Point(684, 297);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(68, 20);
            this.label33.TabIndex = 0;
            this.label33.Text = "2#循环泵";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.label32.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label32.ForeColor = System.Drawing.Color.White;
            this.label32.Location = new System.Drawing.Point(489, 298);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(68, 20);
            this.label32.TabIndex = 0;
            this.label32.Text = "1#循环泵";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.ForeColor = System.Drawing.Color.White;
            this.label30.Location = new System.Drawing.Point(352, 353);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(51, 20);
            this.label30.TabIndex = 0;
            this.label30.Text = "出水阀";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.ForeColor = System.Drawing.Color.White;
            this.label27.Location = new System.Drawing.Point(517, 12);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(51, 20);
            this.label27.TabIndex = 0;
            this.label27.Text = "进水阀";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(352, 178);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(68, 20);
            this.label31.TabIndex = 0;
            this.label31.Text = "2#进水泵";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(352, 6);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(68, 20);
            this.label26.TabIndex = 0;
            this.label26.Text = "1#进水泵";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toggle_Pump1
            // 
            this.toggle_Pump1.Checked = false;
            this.toggle_Pump1.FalseColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.toggle_Pump1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toggle_Pump1.Location = new System.Drawing.Point(348, 113);
            this.toggle_Pump1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toggle_Pump1.Name = "toggle_Pump1";
            this.toggle_Pump1.Size = new System.Drawing.Size(77, 33);
            this.toggle_Pump1.SwitchType = xbd.ControlLib.SwitchType.Quadrilateral;
            this.toggle_Pump1.TabIndex = 8;
            this.toggle_Pump1.Texts = null;
            this.toggle_Pump1.TrueColor = System.Drawing.Color.LimeGreen;
            this.toggle_Pump1.CheckedChanged += new System.EventHandler(this.toggle_Pump1_CheckedChanged);
            // 
            // toggle_Pump2
            // 
            this.toggle_Pump2.Checked = false;
            this.toggle_Pump2.FalseColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.toggle_Pump2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toggle_Pump2.Location = new System.Drawing.Point(348, 286);
            this.toggle_Pump2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toggle_Pump2.Name = "toggle_Pump2";
            this.toggle_Pump2.Size = new System.Drawing.Size(77, 33);
            this.toggle_Pump2.SwitchType = xbd.ControlLib.SwitchType.Quadrilateral;
            this.toggle_Pump2.TabIndex = 8;
            this.toggle_Pump2.Texts = null;
            this.toggle_Pump2.TrueColor = System.Drawing.Color.LimeGreen;
            this.toggle_Pump2.CheckedChanged += new System.EventHandler(this.toggle_Pump2_CheckedChanged);
            // 
            // motor_Pump1
            // 
            this.motor_Pump1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.motor_Pump1.Location = new System.Drawing.Point(478, 323);
            this.motor_Pump1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.motor_Pump1.Name = "motor_Pump1";
            this.motor_Pump1.PumpState = xbd.ControlLib.PumpState.停止;
            this.motor_Pump1.Size = new System.Drawing.Size(74, 113);
            this.motor_Pump1.TabIndex = 7;
            // 
            // motor_Pump2
            // 
            this.motor_Pump2.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.motor_Pump2.Location = new System.Drawing.Point(678, 323);
            this.motor_Pump2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.motor_Pump2.Name = "motor_Pump2";
            this.motor_Pump2.PumpState = xbd.ControlLib.PumpState.停止;
            this.motor_Pump2.Size = new System.Drawing.Size(74, 113);
            this.motor_Pump2.TabIndex = 7;
            // 
            // xbdFlowControl5
            // 
            this.xbdFlowControl5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl5.ColorPipeLineCenter = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(135)))), ((int)(((byte)(69)))));
            this.xbdFlowControl5.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xbdFlowControl5.LineCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl5.Location = new System.Drawing.Point(912, 230);
            this.xbdFlowControl5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xbdFlowControl5.MoveSpeed = 0.2F;
            this.xbdFlowControl5.Name = "xbdFlowControl5";
            this.xbdFlowControl5.PipeLineActive = true;
            this.xbdFlowControl5.PipeLineGap = 2;
            this.xbdFlowControl5.PipeLineLength = 2;
            this.xbdFlowControl5.PipeLineStyle = xbd.ControlLib.DirectionStyle.Vertical;
            this.xbdFlowControl5.PipeLineWidth = 5;
            this.xbdFlowControl5.PipeTurnLeft = xbd.ControlLib.PipeTurnDirection.Left;
            this.xbdFlowControl5.PipeTurnRight = xbd.ControlLib.PipeTurnDirection.Right;
            this.xbdFlowControl5.Size = new System.Drawing.Size(10, 56);
            this.xbdFlowControl5.TabIndex = 2;
            // 
            // xbdFlowControl9
            // 
            this.xbdFlowControl9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl9.ColorPipeLineCenter = System.Drawing.Color.White;
            this.xbdFlowControl9.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xbdFlowControl9.LineCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl9.Location = new System.Drawing.Point(238, 402);
            this.xbdFlowControl9.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xbdFlowControl9.MoveSpeed = -0.2F;
            this.xbdFlowControl9.Name = "xbdFlowControl9";
            this.xbdFlowControl9.PipeLineActive = true;
            this.xbdFlowControl9.PipeLineGap = 2;
            this.xbdFlowControl9.PipeLineLength = 2;
            this.xbdFlowControl9.PipeLineStyle = xbd.ControlLib.DirectionStyle.Horizontal;
            this.xbdFlowControl9.PipeLineWidth = 5;
            this.xbdFlowControl9.PipeTurnLeft = xbd.ControlLib.PipeTurnDirection.None;
            this.xbdFlowControl9.PipeTurnRight = xbd.ControlLib.PipeTurnDirection.None;
            this.xbdFlowControl9.Size = new System.Drawing.Size(447, 23);
            this.xbdFlowControl9.TabIndex = 2;
            // 
            // xbdFlowControl8
            // 
            this.xbdFlowControl8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl8.ColorPipeLineCenter = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(135)))), ((int)(((byte)(69)))));
            this.xbdFlowControl8.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xbdFlowControl8.LineCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl8.Location = new System.Drawing.Point(676, 402);
            this.xbdFlowControl8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xbdFlowControl8.MoveSpeed = -0.2F;
            this.xbdFlowControl8.Name = "xbdFlowControl8";
            this.xbdFlowControl8.PipeLineActive = true;
            this.xbdFlowControl8.PipeLineGap = 2;
            this.xbdFlowControl8.PipeLineLength = 2;
            this.xbdFlowControl8.PipeLineStyle = xbd.ControlLib.DirectionStyle.Horizontal;
            this.xbdFlowControl8.PipeLineWidth = 5;
            this.xbdFlowControl8.PipeTurnLeft = xbd.ControlLib.PipeTurnDirection.None;
            this.xbdFlowControl8.PipeTurnRight = xbd.ControlLib.PipeTurnDirection.Up;
            this.xbdFlowControl8.Size = new System.Drawing.Size(371, 23);
            this.xbdFlowControl8.TabIndex = 2;
            // 
            // xbdFlowControl6
            // 
            this.xbdFlowControl6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl6.ColorPipeLineCenter = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(135)))), ((int)(((byte)(69)))));
            this.xbdFlowControl6.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xbdFlowControl6.LineCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl6.Location = new System.Drawing.Point(633, 273);
            this.xbdFlowControl6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xbdFlowControl6.MoveSpeed = 0.2F;
            this.xbdFlowControl6.Name = "xbdFlowControl6";
            this.xbdFlowControl6.PipeLineActive = true;
            this.xbdFlowControl6.PipeLineGap = 2;
            this.xbdFlowControl6.PipeLineLength = 2;
            this.xbdFlowControl6.PipeLineStyle = xbd.ControlLib.DirectionStyle.Horizontal;
            this.xbdFlowControl6.PipeLineWidth = 5;
            this.xbdFlowControl6.PipeTurnLeft = xbd.ControlLib.PipeTurnDirection.Up;
            this.xbdFlowControl6.PipeTurnRight = xbd.ControlLib.PipeTurnDirection.Down;
            this.xbdFlowControl6.Size = new System.Drawing.Size(416, 23);
            this.xbdFlowControl6.TabIndex = 2;
            // 
            // xbdFlowControl7
            // 
            this.xbdFlowControl7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl7.ColorPipeLineCenter = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(135)))), ((int)(((byte)(69)))));
            this.xbdFlowControl7.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xbdFlowControl7.LineCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl7.Location = new System.Drawing.Point(1033, 297);
            this.xbdFlowControl7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xbdFlowControl7.MoveSpeed = 0.2F;
            this.xbdFlowControl7.Name = "xbdFlowControl7";
            this.xbdFlowControl7.PipeLineActive = true;
            this.xbdFlowControl7.PipeLineGap = 2;
            this.xbdFlowControl7.PipeLineLength = 2;
            this.xbdFlowControl7.PipeLineStyle = xbd.ControlLib.DirectionStyle.Vertical;
            this.xbdFlowControl7.PipeLineWidth = 5;
            this.xbdFlowControl7.PipeTurnLeft = xbd.ControlLib.PipeTurnDirection.Left;
            this.xbdFlowControl7.PipeTurnRight = xbd.ControlLib.PipeTurnDirection.None;
            this.xbdFlowControl7.Size = new System.Drawing.Size(10, 121);
            this.xbdFlowControl7.TabIndex = 2;
            // 
            // xbdFlowControl4
            // 
            this.xbdFlowControl4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl4.ColorPipeLineCenter = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(135)))), ((int)(((byte)(69)))));
            this.xbdFlowControl4.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xbdFlowControl4.LineCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl4.Location = new System.Drawing.Point(637, 224);
            this.xbdFlowControl4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xbdFlowControl4.MoveSpeed = 0.2F;
            this.xbdFlowControl4.Name = "xbdFlowControl4";
            this.xbdFlowControl4.PipeLineActive = true;
            this.xbdFlowControl4.PipeLineGap = 2;
            this.xbdFlowControl4.PipeLineLength = 2;
            this.xbdFlowControl4.PipeLineStyle = xbd.ControlLib.DirectionStyle.Vertical;
            this.xbdFlowControl4.PipeLineWidth = 5;
            this.xbdFlowControl4.PipeTurnLeft = xbd.ControlLib.PipeTurnDirection.Left;
            this.xbdFlowControl4.PipeTurnRight = xbd.ControlLib.PipeTurnDirection.None;
            this.xbdFlowControl4.Size = new System.Drawing.Size(13, 61);
            this.xbdFlowControl4.TabIndex = 2;
            // 
            // wave_Tank2
            // 
            this.wave_Tank2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(138)))), ((int)(((byte)(214)))));
            this.wave_Tank2.ConerRadius = 10;
            this.wave_Tank2.FillColor = System.Drawing.Color.Transparent;
            this.wave_Tank2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wave_Tank2.ForeColor = System.Drawing.Color.White;
            this.wave_Tank2.IsRadius = true;
            this.wave_Tank2.IsRectangle = true;
            this.wave_Tank2.IsShowRect = false;
            this.wave_Tank2.Location = new System.Drawing.Point(984, 136);
            this.wave_Tank2.MaxValue = 100;
            this.wave_Tank2.Name = "wave_Tank2";
            this.wave_Tank2.RectColor = System.Drawing.Color.White;
            this.wave_Tank2.RectWidth = 4;
            this.wave_Tank2.Size = new System.Drawing.Size(56, 107);
            this.wave_Tank2.TabIndex = 6;
            this.wave_Tank2.Value = 0;
            this.wave_Tank2.ValueColor = System.Drawing.Color.Lime;
            // 
            // wave_Tank1
            // 
            this.wave_Tank1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(138)))), ((int)(((byte)(214)))));
            this.wave_Tank1.ConerRadius = 10;
            this.wave_Tank1.FillColor = System.Drawing.Color.Transparent;
            this.wave_Tank1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wave_Tank1.ForeColor = System.Drawing.Color.White;
            this.wave_Tank1.IsRadius = true;
            this.wave_Tank1.IsRectangle = true;
            this.wave_Tank1.IsShowRect = false;
            this.wave_Tank1.Location = new System.Drawing.Point(715, 136);
            this.wave_Tank1.MaxValue = 100;
            this.wave_Tank1.Name = "wave_Tank1";
            this.wave_Tank1.RectColor = System.Drawing.Color.White;
            this.wave_Tank1.RectWidth = 4;
            this.wave_Tank1.Size = new System.Drawing.Size(56, 107);
            this.wave_Tank1.TabIndex = 6;
            this.wave_Tank1.Value = 0;
            this.wave_Tank1.ValueColor = System.Drawing.Color.Lime;
            // 
            // pump_In2
            // 
            this.pump_In2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.pump_In2.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(218)))), ((int)(((byte)(227)))));
            this.pump_In2.Color2 = System.Drawing.Color.LightGray;
            this.pump_In2.Color3 = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(135)))), ((int)(((byte)(69)))));
            this.pump_In2.Color4 = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(214)))));
            this.pump_In2.Color5 = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(213)))), ((int)(((byte)(220)))));
            this.pump_In2.Color6 = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(160)))), ((int)(((byte)(169)))));
            this.pump_In2.Color7 = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(100)))), ((int)(((byte)(111)))));
            this.pump_In2.Export = 6;
            this.pump_In2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pump_In2.ForeColor = System.Drawing.Color.White;
            this.pump_In2.IsRun = false;
            this.pump_In2.Location = new System.Drawing.Point(340, 187);
            this.pump_In2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pump_In2.MoveSpeed = 1F;
            this.pump_In2.Name = "pump_In2";
            this.pump_In2.Size = new System.Drawing.Size(92, 89);
            this.pump_In2.TabIndex = 3;
            this.pump_In2.Text = "123";
            // 
            // pump_In1
            // 
            this.pump_In1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.pump_In1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(218)))), ((int)(((byte)(227)))));
            this.pump_In1.Color2 = System.Drawing.Color.LightGray;
            this.pump_In1.Color3 = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(135)))), ((int)(((byte)(69)))));
            this.pump_In1.Color4 = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(208)))), ((int)(((byte)(214)))));
            this.pump_In1.Color5 = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(213)))), ((int)(((byte)(220)))));
            this.pump_In1.Color6 = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(160)))), ((int)(((byte)(169)))));
            this.pump_In1.Color7 = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(100)))), ((int)(((byte)(111)))));
            this.pump_In1.Export = 6;
            this.pump_In1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pump_In1.ForeColor = System.Drawing.Color.White;
            this.pump_In1.IsRun = false;
            this.pump_In1.Location = new System.Drawing.Point(340, 16);
            this.pump_In1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pump_In1.MoveSpeed = 1F;
            this.pump_In1.Name = "pump_In1";
            this.pump_In1.Size = new System.Drawing.Size(92, 89);
            this.pump_In1.TabIndex = 3;
            this.pump_In1.Text = "123";
            // 
            // xbdFlowControl11
            // 
            this.xbdFlowControl11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl11.ColorPipeLineCenter = System.Drawing.Color.Yellow;
            this.xbdFlowControl11.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl11.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xbdFlowControl11.LineCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl11.Location = new System.Drawing.Point(463, 75);
            this.xbdFlowControl11.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xbdFlowControl11.MoveSpeed = -0.2F;
            this.xbdFlowControl11.Name = "xbdFlowControl11";
            this.xbdFlowControl11.PipeLineActive = true;
            this.xbdFlowControl11.PipeLineGap = 2;
            this.xbdFlowControl11.PipeLineLength = 2;
            this.xbdFlowControl11.PipeLineStyle = xbd.ControlLib.DirectionStyle.Vertical;
            this.xbdFlowControl11.PipeLineWidth = 5;
            this.xbdFlowControl11.PipeTurnLeft = xbd.ControlLib.PipeTurnDirection.Right;
            this.xbdFlowControl11.PipeTurnRight = xbd.ControlLib.PipeTurnDirection.None;
            this.xbdFlowControl11.Size = new System.Drawing.Size(13, 159);
            this.xbdFlowControl11.TabIndex = 2;
            // 
            // xbdFlowControl3
            // 
            this.xbdFlowControl3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl3.ColorPipeLineCenter = System.Drawing.Color.Yellow;
            this.xbdFlowControl3.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xbdFlowControl3.LineCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl3.Location = new System.Drawing.Point(623, 74);
            this.xbdFlowControl3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xbdFlowControl3.MoveSpeed = 0.2F;
            this.xbdFlowControl3.Name = "xbdFlowControl3";
            this.xbdFlowControl3.PipeLineActive = true;
            this.xbdFlowControl3.PipeLineGap = 2;
            this.xbdFlowControl3.PipeLineLength = 2;
            this.xbdFlowControl3.PipeLineStyle = xbd.ControlLib.DirectionStyle.Vertical;
            this.xbdFlowControl3.PipeLineWidth = 5;
            this.xbdFlowControl3.PipeTurnLeft = xbd.ControlLib.PipeTurnDirection.Left;
            this.xbdFlowControl3.PipeTurnRight = xbd.ControlLib.PipeTurnDirection.None;
            this.xbdFlowControl3.Size = new System.Drawing.Size(13, 57);
            this.xbdFlowControl3.TabIndex = 2;
            // 
            // xbdFlowControl10
            // 
            this.xbdFlowControl10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl10.ColorPipeLineCenter = System.Drawing.Color.Yellow;
            this.xbdFlowControl10.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xbdFlowControl10.LineCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl10.Location = new System.Drawing.Point(238, 240);
            this.xbdFlowControl10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xbdFlowControl10.MoveSpeed = 0.2F;
            this.xbdFlowControl10.Name = "xbdFlowControl10";
            this.xbdFlowControl10.PipeLineActive = true;
            this.xbdFlowControl10.PipeLineGap = 2;
            this.xbdFlowControl10.PipeLineLength = 2;
            this.xbdFlowControl10.PipeLineStyle = xbd.ControlLib.DirectionStyle.Horizontal;
            this.xbdFlowControl10.PipeLineWidth = 5;
            this.xbdFlowControl10.PipeTurnLeft = xbd.ControlLib.PipeTurnDirection.None;
            this.xbdFlowControl10.PipeTurnRight = xbd.ControlLib.PipeTurnDirection.Up;
            this.xbdFlowControl10.Size = new System.Drawing.Size(235, 13);
            this.xbdFlowControl10.TabIndex = 2;
            // 
            // xbdFlowControl2
            // 
            this.xbdFlowControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl2.ColorPipeLineCenter = System.Drawing.Color.Yellow;
            this.xbdFlowControl2.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xbdFlowControl2.LineCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl2.Location = new System.Drawing.Point(892, 85);
            this.xbdFlowControl2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xbdFlowControl2.MoveSpeed = 0.2F;
            this.xbdFlowControl2.Name = "xbdFlowControl2";
            this.xbdFlowControl2.PipeLineActive = true;
            this.xbdFlowControl2.PipeLineGap = 2;
            this.xbdFlowControl2.PipeLineLength = 2;
            this.xbdFlowControl2.PipeLineStyle = xbd.ControlLib.DirectionStyle.Vertical;
            this.xbdFlowControl2.PipeLineWidth = 5;
            this.xbdFlowControl2.PipeTurnLeft = xbd.ControlLib.PipeTurnDirection.None;
            this.xbdFlowControl2.PipeTurnRight = xbd.ControlLib.PipeTurnDirection.None;
            this.xbdFlowControl2.Size = new System.Drawing.Size(13, 71);
            this.xbdFlowControl2.TabIndex = 2;
            // 
            // xbdFlowControl1
            // 
            this.xbdFlowControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl1.ColorPipeLineCenter = System.Drawing.Color.Yellow;
            this.xbdFlowControl1.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xbdFlowControl1.LineCenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.xbdFlowControl1.Location = new System.Drawing.Point(237, 71);
            this.xbdFlowControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xbdFlowControl1.MoveSpeed = 0.2F;
            this.xbdFlowControl1.Name = "xbdFlowControl1";
            this.xbdFlowControl1.PipeLineActive = true;
            this.xbdFlowControl1.PipeLineGap = 2;
            this.xbdFlowControl1.PipeLineLength = 2;
            this.xbdFlowControl1.PipeLineStyle = xbd.ControlLib.DirectionStyle.Horizontal;
            this.xbdFlowControl1.PipeLineWidth = 5;
            this.xbdFlowControl1.PipeTurnLeft = xbd.ControlLib.PipeTurnDirection.None;
            this.xbdFlowControl1.PipeTurnRight = xbd.ControlLib.PipeTurnDirection.Down;
            this.xbdFlowControl1.Size = new System.Drawing.Size(668, 13);
            this.xbdFlowControl1.TabIndex = 2;
            // 
            // btn_Pump2
            // 
            this.btn_Pump2.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.Border;
            this.btn_Pump2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Pump2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Pump2.FlatAppearance.BorderSize = 0;
            this.btn_Pump2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Pump2.ForeColor = System.Drawing.Color.White;
            this.btn_Pump2.Location = new System.Drawing.Point(749, 358);
            this.btn_Pump2.Name = "btn_Pump2";
            this.btn_Pump2.Size = new System.Drawing.Size(82, 38);
            this.btn_Pump2.TabIndex = 1;
            this.btn_Pump2.Text = "启动";
            this.btn_Pump2.UseVisualStyleBackColor = true;
            this.btn_Pump2.Click += new System.EventHandler(this.btn_Pump2_Click);
            // 
            // btn_Pump1
            // 
            this.btn_Pump1.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.Border;
            this.btn_Pump1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Pump1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Pump1.FlatAppearance.BorderSize = 0;
            this.btn_Pump1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Pump1.ForeColor = System.Drawing.Color.White;
            this.btn_Pump1.Location = new System.Drawing.Point(545, 358);
            this.btn_Pump1.Name = "btn_Pump1";
            this.btn_Pump1.Size = new System.Drawing.Size(82, 38);
            this.btn_Pump1.TabIndex = 1;
            this.btn_Pump1.Text = "启动";
            this.btn_Pump1.UseVisualStyleBackColor = true;
            this.btn_Pump1.Click += new System.EventHandler(this.btn_Pump1_Click);
            // 
            // btn_SysReset
            // 
            this.btn_SysReset.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.Border;
            this.btn_SysReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_SysReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_SysReset.FlatAppearance.BorderSize = 0;
            this.btn_SysReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SysReset.ForeColor = System.Drawing.Color.White;
            this.btn_SysReset.Location = new System.Drawing.Point(966, 12);
            this.btn_SysReset.Name = "btn_SysReset";
            this.btn_SysReset.Size = new System.Drawing.Size(83, 31);
            this.btn_SysReset.TabIndex = 1;
            this.btn_SysReset.Text = "系统复位";
            this.btn_SysReset.UseVisualStyleBackColor = true;
            this.btn_SysReset.Click += new System.EventHandler(this.btn_SysReset_Click);
            // 
            // btn_userLogin
            // 
            this.btn_userLogin.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.Border;
            this.btn_userLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_userLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_userLogin.FlatAppearance.BorderSize = 0;
            this.btn_userLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_userLogin.ForeColor = System.Drawing.Color.White;
            this.btn_userLogin.Location = new System.Drawing.Point(867, 12);
            this.btn_userLogin.Name = "btn_userLogin";
            this.btn_userLogin.Size = new System.Drawing.Size(83, 31);
            this.btn_userLogin.TabIndex = 1;
            this.btn_userLogin.Text = "用户登录";
            this.btn_userLogin.UseVisualStyleBackColor = true;
            this.btn_userLogin.Click += new System.EventHandler(this.btn_userLogin_Click);
            // 
            // valve_Out
            // 
            this.valve_Out.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("valve_Out.BackgroundImage")));
            this.valve_Out.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.valve_Out.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.valve_Out.IsVertical = false;
            this.valve_Out.Location = new System.Drawing.Point(360, 378);
            this.valve_Out.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.valve_Out.Name = "valve_Out";
            this.valve_Out.Size = new System.Drawing.Size(44, 44);
            this.valve_Out.State = false;
            this.valve_Out.TabIndex = 4;
            this.valve_Out.ValveName = "出水阀";
            this.valve_Out.DoubleClick += new System.EventHandler(this.CommonValve_DoubleClick);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::AY.PressurizationStationPro.Properties.Resources.Sensor;
            this.pictureBox3.Location = new System.Drawing.Point(889, 375);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(42, 48);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AY.PressurizationStationPro.Properties.Resources.Tank;
            this.pictureBox2.Location = new System.Drawing.Point(814, 136);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(167, 107);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AY.PressurizationStationPro.Properties.Resources.Tank;
            this.pictureBox1.Location = new System.Drawing.Point(545, 136);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(167, 107);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // valve_In
            // 
            this.valve_In.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("valve_In.BackgroundImage")));
            this.valve_In.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.valve_In.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.valve_In.IsVertical = false;
            this.valve_In.Location = new System.Drawing.Point(521, 41);
            this.valve_In.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.valve_In.Name = "valve_In";
            this.valve_In.Size = new System.Drawing.Size(44, 44);
            this.valve_In.State = false;
            this.valve_In.TabIndex = 4;
            this.valve_In.ValveName = "进水阀";
            this.valve_In.DoubleClick += new System.EventHandler(this.CommonValve_DoubleClick);
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.DataPanel;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.ms_PressureTankOut);
            this.panel4.Controls.Add(this.ms_PressureTank2);
            this.panel4.Controls.Add(this.ms_PressureTank1);
            this.panel4.Controls.Add(this.ms_TempOut);
            this.panel4.Controls.Add(this.ms_TempIn2);
            this.panel4.Controls.Add(this.ms_TempIn1);
            this.panel4.Location = new System.Drawing.Point(17, 444);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(990, 188);
            this.panel4.TabIndex = 1;
            // 
            // ms_PressureTankOut
            // 
            this.ms_PressureTankOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.ms_PressureTankOut.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ms_PressureTankOut.Location = new System.Drawing.Point(817, 11);
            this.ms_PressureTankOut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ms_PressureTankOut.MeterMax = 10F;
            this.ms_PressureTankOut.MeterMin = 0F;
            this.ms_PressureTankOut.Name = "ms_PressureTankOut";
            this.ms_PressureTankOut.ParamName = "水箱出口压力";
            this.ms_PressureTankOut.ParamValue = 0F;
            this.ms_PressureTankOut.Size = new System.Drawing.Size(147, 168);
            this.ms_PressureTankOut.TabIndex = 0;
            this.ms_PressureTankOut.Unit = "bar";
            // 
            // ms_PressureTank2
            // 
            this.ms_PressureTank2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.ms_PressureTank2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ms_PressureTank2.Location = new System.Drawing.Point(659, 11);
            this.ms_PressureTank2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ms_PressureTank2.MeterMax = 10F;
            this.ms_PressureTank2.MeterMin = 0F;
            this.ms_PressureTank2.Name = "ms_PressureTank2";
            this.ms_PressureTank2.ParamName = "2#水箱压力";
            this.ms_PressureTank2.ParamValue = 0F;
            this.ms_PressureTank2.Size = new System.Drawing.Size(147, 168);
            this.ms_PressureTank2.TabIndex = 0;
            this.ms_PressureTank2.Unit = "bar";
            // 
            // ms_PressureTank1
            // 
            this.ms_PressureTank1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.ms_PressureTank1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ms_PressureTank1.Location = new System.Drawing.Point(501, 11);
            this.ms_PressureTank1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ms_PressureTank1.MeterMax = 10F;
            this.ms_PressureTank1.MeterMin = 0F;
            this.ms_PressureTank1.Name = "ms_PressureTank1";
            this.ms_PressureTank1.ParamName = "1#水箱压力";
            this.ms_PressureTank1.ParamValue = 0F;
            this.ms_PressureTank1.Size = new System.Drawing.Size(147, 168);
            this.ms_PressureTank1.TabIndex = 0;
            this.ms_PressureTank1.Unit = "bar";
            // 
            // ms_TempOut
            // 
            this.ms_TempOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.ms_TempOut.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ms_TempOut.Location = new System.Drawing.Point(343, 11);
            this.ms_TempOut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ms_TempOut.MeterMax = 100F;
            this.ms_TempOut.MeterMin = 0F;
            this.ms_TempOut.Name = "ms_TempOut";
            this.ms_TempOut.ParamName = "出水管温度";
            this.ms_TempOut.ParamValue = 0F;
            this.ms_TempOut.Size = new System.Drawing.Size(147, 168);
            this.ms_TempOut.TabIndex = 0;
            this.ms_TempOut.Unit = "℃";
            // 
            // ms_TempIn2
            // 
            this.ms_TempIn2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.ms_TempIn2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ms_TempIn2.Location = new System.Drawing.Point(185, 9);
            this.ms_TempIn2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ms_TempIn2.MeterMax = 100F;
            this.ms_TempIn2.MeterMin = 0F;
            this.ms_TempIn2.Name = "ms_TempIn2";
            this.ms_TempIn2.ParamName = "2#进水管温度";
            this.ms_TempIn2.ParamValue = 0F;
            this.ms_TempIn2.Size = new System.Drawing.Size(147, 168);
            this.ms_TempIn2.TabIndex = 0;
            this.ms_TempIn2.Unit = "℃";
            // 
            // ms_TempIn1
            // 
            this.ms_TempIn1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.ms_TempIn1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ms_TempIn1.Location = new System.Drawing.Point(27, 9);
            this.ms_TempIn1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ms_TempIn1.MeterMax = 100F;
            this.ms_TempIn1.MeterMin = 0F;
            this.ms_TempIn1.Name = "ms_TempIn1";
            this.ms_TempIn1.ParamName = "1#进水管温度";
            this.ms_TempIn1.ParamValue = 0F;
            this.ms_TempIn1.Size = new System.Drawing.Size(147, 168);
            this.ms_TempIn1.TabIndex = 0;
            this.ms_TempIn1.Unit = "℃";
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.ParamPanel;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label25);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.label19);
            this.panel6.Controls.Add(this.label16);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Controls.Add(this.lbl_PressureTankOut);
            this.panel6.Controls.Add(this.lbl_LevelTank2);
            this.panel6.Controls.Add(this.lbl_PressureTank2);
            this.panel6.Controls.Add(this.lbl_LevelTank1);
            this.panel6.Controls.Add(this.lbl_PressureTank1);
            this.panel6.Controls.Add(this.label13);
            this.panel6.Controls.Add(this.label23);
            this.panel6.Controls.Add(this.label20);
            this.panel6.Controls.Add(this.label17);
            this.panel6.Controls.Add(this.label14);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Location = new System.Drawing.Point(1056, 187);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(210, 241);
            this.panel6.TabIndex = 0;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(165, 176);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(31, 20);
            this.label25.TabIndex = 2;
            this.label25.Text = "bar";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(169, 143);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(22, 20);
            this.label22.TabIndex = 2;
            this.label22.Text = "m";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(165, 110);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(31, 20);
            this.label19.TabIndex = 2;
            this.label19.Text = "bar";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(169, 77);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(22, 20);
            this.label16.TabIndex = 2;
            this.label16.Text = "m";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(165, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "bar";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PressureTankOut
            // 
            this.lbl_PressureTankOut.BackColor = System.Drawing.Color.Transparent;
            this.lbl_PressureTankOut.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_PressureTankOut.ForeColor = System.Drawing.Color.White;
            this.lbl_PressureTankOut.Location = new System.Drawing.Point(110, 176);
            this.lbl_PressureTankOut.Name = "lbl_PressureTankOut";
            this.lbl_PressureTankOut.Size = new System.Drawing.Size(55, 20);
            this.lbl_PressureTankOut.TabIndex = 1;
            this.lbl_PressureTankOut.Text = "00.00";
            this.lbl_PressureTankOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_LevelTank2
            // 
            this.lbl_LevelTank2.BackColor = System.Drawing.Color.Transparent;
            this.lbl_LevelTank2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_LevelTank2.ForeColor = System.Drawing.Color.White;
            this.lbl_LevelTank2.Location = new System.Drawing.Point(110, 143);
            this.lbl_LevelTank2.Name = "lbl_LevelTank2";
            this.lbl_LevelTank2.Size = new System.Drawing.Size(55, 20);
            this.lbl_LevelTank2.TabIndex = 1;
            this.lbl_LevelTank2.Text = "00.00";
            this.lbl_LevelTank2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PressureTank2
            // 
            this.lbl_PressureTank2.BackColor = System.Drawing.Color.Transparent;
            this.lbl_PressureTank2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_PressureTank2.ForeColor = System.Drawing.Color.White;
            this.lbl_PressureTank2.Location = new System.Drawing.Point(110, 110);
            this.lbl_PressureTank2.Name = "lbl_PressureTank2";
            this.lbl_PressureTank2.Size = new System.Drawing.Size(55, 20);
            this.lbl_PressureTank2.TabIndex = 1;
            this.lbl_PressureTank2.Text = "00.00";
            this.lbl_PressureTank2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_LevelTank1
            // 
            this.lbl_LevelTank1.BackColor = System.Drawing.Color.Transparent;
            this.lbl_LevelTank1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_LevelTank1.ForeColor = System.Drawing.Color.White;
            this.lbl_LevelTank1.Location = new System.Drawing.Point(110, 77);
            this.lbl_LevelTank1.Name = "lbl_LevelTank1";
            this.lbl_LevelTank1.Size = new System.Drawing.Size(55, 20);
            this.lbl_LevelTank1.TabIndex = 1;
            this.lbl_LevelTank1.Text = "00.00";
            this.lbl_LevelTank1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PressureTank1
            // 
            this.lbl_PressureTank1.BackColor = System.Drawing.Color.Transparent;
            this.lbl_PressureTank1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_PressureTank1.ForeColor = System.Drawing.Color.White;
            this.lbl_PressureTank1.Location = new System.Drawing.Point(110, 44);
            this.lbl_PressureTank1.Name = "lbl_PressureTank1";
            this.lbl_PressureTank1.Size = new System.Drawing.Size(55, 20);
            this.lbl_PressureTank1.TabIndex = 1;
            this.lbl_PressureTank1.Text = "00.00";
            this.lbl_PressureTank1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(18, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "系统参数";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(14, 176);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(93, 20);
            this.label23.TabIndex = 0;
            this.label23.Text = "出水管压力：";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(14, 143);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(96, 20);
            this.label20.TabIndex = 0;
            this.label20.Text = "2#水箱液位：";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(14, 110);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(96, 20);
            this.label17.TabIndex = 0;
            this.label17.Text = "2#水箱压力：";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(14, 77);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 20);
            this.label14.TabIndex = 0;
            this.label14.Text = "1#水箱液位：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(14, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "1#水箱压力：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.ParamPanel;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.vsp_panel);
            this.panel7.Location = new System.Drawing.Point(1016, 443);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(250, 188);
            this.panel7.TabIndex = 0;
            // 
            // vsp_panel
            // 
            this.vsp_panel.Location = new System.Drawing.Point(13, 11);
            this.vsp_panel.Name = "vsp_panel";
            this.vsp_panel.Size = new System.Drawing.Size(224, 166);
            this.vsp_panel.TabIndex = 0;
            this.vsp_panel.Text = "videoSourcePlayer1";
            this.vsp_panel.VideoSource = null;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.ParamPanel;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.led_SysAlarmState);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.led_RunState);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.led_PLCState);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Location = new System.Drawing.Point(1056, 12);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(210, 160);
            this.panel5.TabIndex = 0;
            // 
            // led_SysAlarmState
            // 
            this.led_SysAlarmState.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("led_SysAlarmState.BackgroundImage")));
            this.led_SysAlarmState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.led_SysAlarmState.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.led_SysAlarmState.Location = new System.Drawing.Point(147, 110);
            this.led_SysAlarmState.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.led_SysAlarmState.Name = "led_SysAlarmState";
            this.led_SysAlarmState.Size = new System.Drawing.Size(24, 24);
            this.led_SysAlarmState.State = false;
            this.led_SysAlarmState.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(38, 112);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 20);
            this.label10.TabIndex = 4;
            this.label10.Text = "系统报警状态：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // led_RunState
            // 
            this.led_RunState.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("led_RunState.BackgroundImage")));
            this.led_RunState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.led_RunState.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.led_RunState.Location = new System.Drawing.Point(149, 71);
            this.led_RunState.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.led_RunState.Name = "led_RunState";
            this.led_RunState.Size = new System.Drawing.Size(24, 24);
            this.led_RunState.State = false;
            this.led_RunState.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(38, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "系统运行状态：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // led_PLCState
            // 
            this.led_PLCState.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("led_PLCState.BackgroundImage")));
            this.led_PLCState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.led_PLCState.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.led_PLCState.Location = new System.Drawing.Point(149, 32);
            this.led_PLCState.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.led_PLCState.Name = "led_PLCState";
            this.led_PLCState.Size = new System.Drawing.Size(24, 24);
            this.led_PLCState.State = false;
            this.led_PLCState.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(38, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "PLC连接状态：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(18, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "系统状态";
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.ParamPanel;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.lbl_PressureOut);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.meter_PressureOut);
            this.panel3.Location = new System.Drawing.Point(17, 228);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(210, 200);
            this.panel3.TabIndex = 0;
            // 
            // lbl_PressureOut
            // 
            this.lbl_PressureOut.BackColor = System.Drawing.Color.Lime;
            this.lbl_PressureOut.Location = new System.Drawing.Point(66, 161);
            this.lbl_PressureOut.Name = "lbl_PressureOut";
            this.lbl_PressureOut.Size = new System.Drawing.Size(77, 25);
            this.lbl_PressureOut.TabIndex = 3;
            this.lbl_PressureOut.Text = "0.00 bar";
            this.lbl_PressureOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(18, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "出水压力";
            // 
            // meter_PressureOut
            // 
            this.meter_PressureOut.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.meter_PressureOut.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.meter_PressureOut.Location = new System.Drawing.Point(17, 20);
            this.meter_PressureOut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.meter_PressureOut.MaxValue = 10D;
            this.meter_PressureOut.MinValue = 0D;
            this.meter_PressureOut.Name = "meter_PressureOut";
            this.meter_PressureOut.NeedleColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.meter_PressureOut.Renderer = null;
            this.meter_PressureOut.ScaleColor = System.Drawing.Color.White;
            this.meter_PressureOut.ScaleDivisions = 11;
            this.meter_PressureOut.ScaleSubDivisions = 4;
            this.meter_PressureOut.Size = new System.Drawing.Size(179, 175);
            this.meter_PressureOut.TabIndex = 1;
            this.meter_PressureOut.Value = 0D;
            this.meter_PressureOut.ViewGlass = false;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.ParamPanel;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.lbl_PressureIn);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.meter_PressureIn);
            this.panel2.Location = new System.Drawing.Point(17, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(210, 200);
            this.panel2.TabIndex = 0;
            // 
            // lbl_PressureIn
            // 
            this.lbl_PressureIn.BackColor = System.Drawing.Color.Lime;
            this.lbl_PressureIn.Location = new System.Drawing.Point(66, 161);
            this.lbl_PressureIn.Name = "lbl_PressureIn";
            this.lbl_PressureIn.Size = new System.Drawing.Size(77, 25);
            this.lbl_PressureIn.TabIndex = 3;
            this.lbl_PressureIn.Text = "0.00 bar";
            this.lbl_PressureIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(18, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "进水压力";
            // 
            // meter_PressureIn
            // 
            this.meter_PressureIn.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.meter_PressureIn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.meter_PressureIn.Location = new System.Drawing.Point(17, 20);
            this.meter_PressureIn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.meter_PressureIn.MaxValue = 10D;
            this.meter_PressureIn.MinValue = 0D;
            this.meter_PressureIn.Name = "meter_PressureIn";
            this.meter_PressureIn.NeedleColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.meter_PressureIn.Renderer = null;
            this.meter_PressureIn.ScaleColor = System.Drawing.Color.White;
            this.meter_PressureIn.ScaleDivisions = 11;
            this.meter_PressureIn.ScaleSubDivisions = 4;
            this.meter_PressureIn.Size = new System.Drawing.Size(179, 175);
            this.meter_PressureIn.TabIndex = 1;
            this.meter_PressureIn.Value = 0D;
            this.meter_PressureIn.ViewGlass = false;
            // 
            // TopPanel
            // 
            this.TopPanel.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.TopPanel;
            this.TopPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.TopPanel.Controls.Add(this.lbl_LoginName);
            this.TopPanel.Controls.Add(this.label2);
            this.TopPanel.Controls.Add(this.lbl_Time);
            this.TopPanel.Controls.Add(this.btnExit);
            this.TopPanel.Controls.Add(this.button3);
            this.TopPanel.Controls.Add(this.button2);
            this.TopPanel.Controls.Add(this.btn_ParamSet);
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1280, 77);
            this.TopPanel.TabIndex = 0;
            // 
            // lbl_LoginName
            // 
            this.lbl_LoginName.AutoSize = true;
            this.lbl_LoginName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_LoginName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_LoginName.ForeColor = System.Drawing.Color.White;
            this.lbl_LoginName.Location = new System.Drawing.Point(111, 52);
            this.lbl_LoginName.Name = "lbl_LoginName";
            this.lbl_LoginName.Size = new System.Drawing.Size(51, 20);
            this.lbl_LoginName.TabIndex = 0;
            this.lbl_LoginName.Text = "未登录";
            this.lbl_LoginName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(13, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "当前登录用户：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Time
            // 
            this.lbl_Time.AutoSize = true;
            this.lbl_Time.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Time.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Time.ForeColor = System.Drawing.Color.White;
            this.lbl_Time.Location = new System.Drawing.Point(1068, 52);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(189, 20);
            this.lbl_Time.TabIndex = 0;
            this.lbl_Time.Text = "2024-01-01 00:00:00 星期五";
            this.lbl_Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.Border;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(1174, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(83, 31);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "退出系统";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.Border;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(1076, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 31);
            this.button3.TabIndex = 1;
            this.button3.Text = "数据报表";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.Border;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(115, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 31);
            this.button2.TabIndex = 1;
            this.button2.Text = "历史记录";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_ParamSet
            // 
            this.btn_ParamSet.BackgroundImage = global::AY.PressurizationStationPro.Properties.Resources.Border;
            this.btn_ParamSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ParamSet.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_ParamSet.FlatAppearance.BorderSize = 0;
            this.btn_ParamSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ParamSet.ForeColor = System.Drawing.Color.White;
            this.btn_ParamSet.Location = new System.Drawing.Point(17, 12);
            this.btn_ParamSet.Name = "btn_ParamSet";
            this.btn_ParamSet.Size = new System.Drawing.Size(83, 31);
            this.btn_ParamSet.TabIndex = 1;
            this.btn_ParamSet.Text = "参数设置";
            this.btn_ParamSet.UseVisualStyleBackColor = true;
            this.btn_ParamSet.Click += new System.EventHandler(this.btn_ParamSet_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(449, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(385, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "智慧加压站SCADA监控系统";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TopPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智慧加压站SCADA监控系统";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_ParamSet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.Label label3;
        private xbd.ControlLib.xbdAnalogMeter meter_PressureIn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl_PressureOut;
        private System.Windows.Forms.Label label5;
        private xbd.ControlLib.xbdAnalogMeter meter_PressureOut;
        private System.Windows.Forms.Label lbl_PressureIn;
        private System.Windows.Forms.Panel panel4;
        private MeterShow ms_PressureTank1;
        private MeterShow ms_TempOut;
        private MeterShow ms_TempIn2;
        private MeterShow ms_TempIn1;
        private MeterShow ms_PressureTankOut;
        private MeterShow ms_PressureTank2;
        private System.Windows.Forms.Panel panel5;
        private xbd.ControlLib.xbdState led_SysAlarmState;
        private System.Windows.Forms.Label label10;
        private xbd.ControlLib.xbdState led_RunState;
        private System.Windows.Forms.Label label8;
        private xbd.ControlLib.xbdState led_PLCState;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_PressureTankOut;
        private System.Windows.Forms.Label lbl_LevelTank2;
        private System.Windows.Forms.Label lbl_PressureTank2;
        private System.Windows.Forms.Label lbl_LevelTank1;
        private System.Windows.Forms.Label lbl_PressureTank1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel7;
        private xbd.ControlLib.xbdFlowControl xbdFlowControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private xbd.ControlLib.xbdValve valve_In;
        private xbd.ControlLib.xbdPump pump_In1;
        private System.Windows.Forms.Label label26;
        private xbd.ControlLib.xbdWave wave_Tank1;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private xbd.ControlLib.xbdWave wave_Tank2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private xbd.ControlLib.xbdFlowControl xbdFlowControl2;
        private xbd.ControlLib.xbdFlowControl xbdFlowControl4;
        private xbd.ControlLib.xbdFlowControl xbdFlowControl5;
        private xbd.ControlLib.xbdFlowControl xbdFlowControl3;
        private xbd.ControlLib.xbdFlowControl xbdFlowControl8;
        private xbd.ControlLib.xbdFlowControl xbdFlowControl6;
        private xbd.ControlLib.xbdFlowControl xbdFlowControl7;
        private xbd.ControlLib.xbdMotor motor_Pump2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private xbd.ControlLib.xbdToggle toggle_Pump1;
        private xbd.ControlLib.xbdMotor motor_Pump1;
        private xbd.ControlLib.xbdFlowControl xbdFlowControl9;
        private xbd.ControlLib.xbdValve valve_Out;
        private xbd.ControlLib.xbdToggle toggle_Pump2;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private xbd.ControlLib.xbdPump pump_In2;
        private xbd.ControlLib.xbdFlowControl xbdFlowControl11;
        private xbd.ControlLib.xbdFlowControl xbdFlowControl10;
        private System.Windows.Forms.Label lbl_TempIn1;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Button btn_userLogin;
        private System.Windows.Forms.Label lbl_TempOut;
        private System.Windows.Forms.Label lbl_TempIn2;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Button btn_Pump2;
        private System.Windows.Forms.Button btn_Pump1;
        private System.Windows.Forms.Button btn_SysReset;
        private System.Windows.Forms.Label lbl_PreTankOut;
        private System.Windows.Forms.Label label48;
        private AForge.Controls.VideoSourcePlayer vsp_panel;
        private System.Windows.Forms.Label lbl_LoginName;
        private System.Windows.Forms.Label label2;
    }
}

