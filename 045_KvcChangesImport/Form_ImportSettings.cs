using System;
using System.Windows.Forms;

namespace _045_KvcChangesImport
{
    public partial class Form_ImportSettings : Form
    {
        public int ImportYear;
        public int ImportMonth;

        public Form_ImportSettings()
        {
            InitializeComponent();
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            ImportYear = Int32.Parse(textBox_ImportYear.Text);
            ImportMonth = Int32.Parse(textBox_ImportMonth.Text);
            Close();
        }
    }
}
