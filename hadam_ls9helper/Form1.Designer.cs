﻿namespace hadam_ls9helper
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_pMic = new System.Windows.Forms.Button();
            this.btn_cMic = new System.Windows.Forms.Button();
            this.btn_downMain = new System.Windows.Forms.Button();
            this.btn_wMic = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2_targetProgram = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_setting = new System.Windows.Forms.Button();
            this.btn_cOnly = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.btn_leader = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cBox_wMic4 = new System.Windows.Forms.CheckBox();
            this.cBox_wMic3 = new System.Windows.Forms.CheckBox();
            this.cBox_wMic2 = new System.Windows.Forms.CheckBox();
            this.cBox_wMic1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cBox_wMics = new System.Windows.Forms.CheckBox();
            this.cBox_cMic = new System.Windows.Forms.CheckBox();
            this.cBox_pMic = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.timer5 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_pMic
            // 
            this.btn_pMic.Image = global::hadam_ls9helper.Properties.Resources.music_off;
            this.btn_pMic.Location = new System.Drawing.Point(161, 17);
            this.btn_pMic.Name = "btn_pMic";
            this.btn_pMic.Size = new System.Drawing.Size(106, 86);
            this.btn_pMic.TabIndex = 0;
            this.btn_pMic.Text = "관현악\r\n\r\n";
            this.btn_pMic.UseVisualStyleBackColor = true;
            this.btn_pMic.Click += new System.EventHandler(this.btn_pMic_Click);
            // 
            // btn_cMic
            // 
            this.btn_cMic.Image = global::hadam_ls9helper.Properties.Resources.music_off;
            this.btn_cMic.Location = new System.Drawing.Point(161, 119);
            this.btn_cMic.Name = "btn_cMic";
            this.btn_cMic.Size = new System.Drawing.Size(106, 86);
            this.btn_cMic.TabIndex = 1;
            this.btn_cMic.Text = "\r\n찬양대\r\nALT+A\r\n";
            this.btn_cMic.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_cMic.UseVisualStyleBackColor = true;
            this.btn_cMic.Click += new System.EventHandler(this.btn_cMic_Click);
            // 
            // btn_downMain
            // 
            this.btn_downMain.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_downMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_downMain.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btn_downMain.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_downMain.Image = global::hadam_ls9helper.Properties.Resources.music_off;
            this.btn_downMain.Location = new System.Drawing.Point(25, 17);
            this.btn_downMain.Margin = new System.Windows.Forms.Padding(0);
            this.btn_downMain.Name = "btn_downMain";
            this.btn_downMain.Size = new System.Drawing.Size(106, 86);
            this.btn_downMain.TabIndex = 3;
            this.btn_downMain.Text = "\r\n아랫강대상\r\nALT+E\r\n\r\n";
            this.btn_downMain.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_downMain.UseMnemonic = false;
            this.btn_downMain.UseVisualStyleBackColor = false;
            this.btn_downMain.Click += new System.EventHandler(this.btn_downMain_Click);
            // 
            // btn_wMic
            // 
            this.btn_wMic.Image = global::hadam_ls9helper.Properties.Resources.music_off;
            this.btn_wMic.Location = new System.Drawing.Point(25, 222);
            this.btn_wMic.Name = "btn_wMic";
            this.btn_wMic.Size = new System.Drawing.Size(106, 86);
            this.btn_wMic.TabIndex = 4;
            this.btn_wMic.Text = "무선 마이크\r\n\r\n";
            this.btn_wMic.UseVisualStyleBackColor = true;
            this.btn_wMic.Click += new System.EventHandler(this.btn_wMic_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lavender;
            this.panel1.Controls.Add(this.label2_targetProgram);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_setting);
            this.panel1.Controls.Add(this.btn_cOnly);
            this.panel1.Controls.Add(this.btn_refresh);
            this.panel1.Controls.Add(this.btn_leader);
            this.panel1.Controls.Add(this.btn_wMic);
            this.panel1.Controls.Add(this.btn_pMic);
            this.panel1.Controls.Add(this.btn_downMain);
            this.panel1.Controls.Add(this.btn_cMic);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(518, 356);
            this.panel1.TabIndex = 5;
            // 
            // label2_targetProgram
            // 
            this.label2_targetProgram.AutoSize = true;
            this.label2_targetProgram.Font = new System.Drawing.Font("뫼비우스 Bold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2_targetProgram.Location = new System.Drawing.Point(328, 54);
            this.label2_targetProgram.Name = "label2_targetProgram";
            this.label2_targetProgram.Size = new System.Drawing.Size(72, 22);
            this.label2_targetProgram.TabIndex = 9;
            this.label2_targetProgram.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(328, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "자막기프로그램 이름";
            // 
            // btn_setting
            // 
            this.btn_setting.Location = new System.Drawing.Point(32, 314);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(99, 37);
            this.btn_setting.TabIndex = 7;
            this.btn_setting.Text = "설정";
            this.btn_setting.UseVisualStyleBackColor = true;
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // btn_cOnly
            // 
            this.btn_cOnly.Image = global::hadam_ls9helper.Properties.Resources.music_off;
            this.btn_cOnly.Location = new System.Drawing.Point(161, 222);
            this.btn_cOnly.Name = "btn_cOnly";
            this.btn_cOnly.Size = new System.Drawing.Size(106, 86);
            this.btn_cOnly.TabIndex = 6;
            this.btn_cOnly.Text = "\r\n찬양대 Only\r\nALT+C\r\n\r\n";
            this.btn_cOnly.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_cOnly.UseVisualStyleBackColor = true;
            this.btn_cOnly.Click += new System.EventHandler(this.btn_cOnly_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Location = new System.Drawing.Point(161, 311);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(106, 40);
            this.btn_refresh.TabIndex = 5;
            this.btn_refresh.Text = "새로고침";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btn_leader
            // 
            this.btn_leader.Image = global::hadam_ls9helper.Properties.Resources.music_off;
            this.btn_leader.Location = new System.Drawing.Point(25, 119);
            this.btn_leader.Name = "btn_leader";
            this.btn_leader.Size = new System.Drawing.Size(106, 86);
            this.btn_leader.TabIndex = 5;
            this.btn_leader.Text = "인도자\r\n\r\n";
            this.btn_leader.UseVisualStyleBackColor = true;
            this.btn_leader.Click += new System.EventHandler(this.btn_leader_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cBox_wMic4);
            this.groupBox1.Controls.Add(this.cBox_wMic3);
            this.groupBox1.Controls.Add(this.cBox_wMic2);
            this.groupBox1.Controls.Add(this.cBox_wMic1);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 119);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "무선 마이크";
            // 
            // cBox_wMic4
            // 
            this.cBox_wMic4.AutoSize = true;
            this.cBox_wMic4.Location = new System.Drawing.Point(25, 90);
            this.cBox_wMic4.Name = "cBox_wMic4";
            this.cBox_wMic4.Size = new System.Drawing.Size(70, 16);
            this.cBox_wMic4.TabIndex = 3;
            this.cBox_wMic4.Text = "무선 4번";
            this.cBox_wMic4.UseVisualStyleBackColor = true;
            // 
            // cBox_wMic3
            // 
            this.cBox_wMic3.AutoSize = true;
            this.cBox_wMic3.Location = new System.Drawing.Point(25, 67);
            this.cBox_wMic3.Name = "cBox_wMic3";
            this.cBox_wMic3.Size = new System.Drawing.Size(70, 16);
            this.cBox_wMic3.TabIndex = 2;
            this.cBox_wMic3.Text = "무선 3번";
            this.cBox_wMic3.UseVisualStyleBackColor = true;
            // 
            // cBox_wMic2
            // 
            this.cBox_wMic2.AutoSize = true;
            this.cBox_wMic2.Location = new System.Drawing.Point(25, 44);
            this.cBox_wMic2.Name = "cBox_wMic2";
            this.cBox_wMic2.Size = new System.Drawing.Size(70, 16);
            this.cBox_wMic2.TabIndex = 1;
            this.cBox_wMic2.Text = "무선 2번";
            this.cBox_wMic2.UseVisualStyleBackColor = true;
            // 
            // cBox_wMic1
            // 
            this.cBox_wMic1.AutoSize = true;
            this.cBox_wMic1.Location = new System.Drawing.Point(25, 21);
            this.cBox_wMic1.Name = "cBox_wMic1";
            this.cBox_wMic1.Size = new System.Drawing.Size(70, 16);
            this.cBox_wMic1.TabIndex = 0;
            this.cBox_wMic1.Text = "무선 1번";
            this.cBox_wMic1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cBox_wMics);
            this.groupBox2.Controls.Add(this.cBox_cMic);
            this.groupBox2.Controls.Add(this.cBox_pMic);
            this.groupBox2.Location = new System.Drawing.Point(151, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(131, 119);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "연결";
            // 
            // cBox_wMics
            // 
            this.cBox_wMics.AutoSize = true;
            this.cBox_wMics.Location = new System.Drawing.Point(20, 87);
            this.cBox_wMics.Name = "cBox_wMics";
            this.cBox_wMics.Size = new System.Drawing.Size(72, 16);
            this.cBox_wMics.TabIndex = 2;
            this.cBox_wMics.Text = "마이크들";
            this.cBox_wMics.UseVisualStyleBackColor = true;
            // 
            // cBox_cMic
            // 
            this.cBox_cMic.AutoSize = true;
            this.cBox_cMic.Location = new System.Drawing.Point(20, 57);
            this.cBox_cMic.Name = "cBox_cMic";
            this.cBox_cMic.Size = new System.Drawing.Size(60, 16);
            this.cBox_cMic.TabIndex = 1;
            this.cBox_cMic.Text = "찬양대";
            this.cBox_cMic.UseVisualStyleBackColor = true;
            // 
            // cBox_pMic
            // 
            this.cBox_pMic.AutoSize = true;
            this.cBox_pMic.Location = new System.Drawing.Point(20, 27);
            this.cBox_pMic.Name = "cBox_pMic";
            this.cBox_pMic.Size = new System.Drawing.Size(100, 16);
            this.cBox_pMic.TabIndex = 0;
            this.cBox_pMic.Text = "관현악 마이크";
            this.cBox_pMic.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 377);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(518, 146);
            this.panel2.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // timer5
            // 
            this.timer5.Tick += new System.EventHandler(this.timer5_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 523);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_pMic;
        private System.Windows.Forms.Button btn_cMic;
        private System.Windows.Forms.Button btn_downMain;
        private System.Windows.Forms.Button btn_wMic;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cBox_wMic4;
        private System.Windows.Forms.CheckBox cBox_wMic3;
        private System.Windows.Forms.CheckBox cBox_wMic2;
        private System.Windows.Forms.CheckBox cBox_wMic1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cBox_wMics;
        private System.Windows.Forms.CheckBox cBox_cMic;
        private System.Windows.Forms.CheckBox cBox_pMic;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_leader;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Button btn_cOnly;
        private System.Windows.Forms.Timer timer5;
        private System.Windows.Forms.Button btn_setting;
        private System.Windows.Forms.Label label2_targetProgram;
        private System.Windows.Forms.Label label1;
    }
}

