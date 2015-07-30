using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace aConverter.Forms
{
    public partial class FormDataTable : Form
    {
        private readonly string _headerText;

        private readonly DataTable _dataTable;

        public FormDataTable(string headerText, DataTable dataTable)
        {
            InitializeComponent();
            _headerText = headerText;
            _dataTable = dataTable;
        }

        private void FormDataTable_Load(object sender, EventArgs e)
        {
            Text = _headerText;
            dataGridViewMain.AutoGenerateColumns = true;
            bindingSourceMain.DataSource = _dataTable;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
