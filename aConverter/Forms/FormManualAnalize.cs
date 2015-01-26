using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DbfClassLibrary;

namespace aConverter.Forms
{
    public partial class FormManualAnalize : Form
    {
        private TableManager tableManager;

        private string tableName;

        public FormManualAnalize(TableManager ATableManager, string ATableName)
        {
            InitializeComponent();
            this.tableManager = ATableManager;
            this.tableName = ATableName;
            this.textBox1.Text = "SELECT *\r\nFROM " + ATableName;
        }

        private void executeQuery()
        {
            string query = this.textBox1.Text.Replace("\r\n", " ");
            try
            {
                DataTable dt = tableManager.ExecuteQuery(query);
                bindingSource1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка выполнения запроса", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Control)
            {
                executeQuery();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void FormManualAnalize_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = true;
            executeQuery();
            this.WindowState = FormWindowState.Maximized;
        }

        private void buttonExecuteQuery_Click(object sender, EventArgs e)
        {
            executeQuery();
        }
    }
}
