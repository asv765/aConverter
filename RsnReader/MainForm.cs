using System;
using System.Windows.Forms;

namespace RsnReader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OpenReadRsnForm_button_Click(object sender, EventArgs e)
        {
            new ReadRsnForm().ShowDialog();
            GC.Collect();
        }

        private void OpenAnalyzeForm_button_Click(object sender, EventArgs e)
        {
            new AnalyzeForm().ShowDialog();
        }

        private void ReadSut_button_Click(object sender, EventArgs e)
        {
            new ReadSutForm().ShowDialog();
        }

        private void ReadCc_button_Click(object sender, EventArgs e)
        {
            new ReadCcForm().ShowDialog();
        }

        private void button_ReadIdom_Click(object sender, EventArgs e)
        {
            new ReadIdomForm().ShowDialog();
        }

        private void button_Isch_Click(object sender, EventArgs e)
        {
            new ReadIschForm().ShowDialog();
        }

        private void button_ReadChanges_Click(object sender, EventArgs e)
        {
            new ReadChangesGGMMForm().ShowDialog();
        }
    }
}
