namespace hadam_ls9helper
{
    partial class Form2
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
            this.btn_saveSettings = new System.Windows.Forms.Button();
            this.textBox1_targetProgram = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_saveSettings
            // 
            this.btn_saveSettings.Location = new System.Drawing.Point(273, 204);
            this.btn_saveSettings.Name = "btn_saveSettings";
            this.btn_saveSettings.Size = new System.Drawing.Size(148, 54);
            this.btn_saveSettings.TabIndex = 0;
            this.btn_saveSettings.Text = "btn_saveSettings";
            this.btn_saveSettings.UseVisualStyleBackColor = true;
            this.btn_saveSettings.Click += new System.EventHandler(this.btn_saveSettings_Click);
            // 
            // textBox1_targetProgram
            // 
            this.textBox1_targetProgram.Location = new System.Drawing.Point(225, 68);
            this.textBox1_targetProgram.Name = "textBox1_targetProgram";
            this.textBox1_targetProgram.Size = new System.Drawing.Size(166, 21);
            this.textBox1_targetProgram.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "대상자막기";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 285);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1_targetProgram);
            this.Controls.Add(this.btn_saveSettings);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_saveSettings;
        private System.Windows.Forms.TextBox textBox1_targetProgram;
        private System.Windows.Forms.Label label1;
    }
}