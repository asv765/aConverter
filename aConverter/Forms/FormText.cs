using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace aConverter
{
    public partial class FormText : Form
    {
        public FormText(string text)
        {
            InitializeComponent();
            textBoxMain.Text = text;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
