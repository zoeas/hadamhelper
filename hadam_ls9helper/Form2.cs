using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hadam_ls9helper
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox1_targetProgram.Text = Properties.Settings.Default.TargetProgramName;
        }

        private void btn_saveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
            this.Close();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.TargetProgramName = textBox1_targetProgram.Text;
            Properties.Settings.Default.Save();
        }

       
    }
}
