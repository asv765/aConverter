using System;
using System.Windows.Forms;
using aConverterClassLibrary;

namespace _048_Rgmek.Forms
{
    public partial class Form_ConnectionString : Form
    {
        public string ConnectionString;

        public Form_ConnectionString(string label = "Строка подключения к дополнительной БД")
        {
            InitializeComponent();
            textBox_ConnectionString.Text = aConverter_RootSettings.FirebirdStringConnection;
            label_ConnectionString.Text = label;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            ConnectionString = textBox_ConnectionString.Text;
            Close();
        }
    }
}
