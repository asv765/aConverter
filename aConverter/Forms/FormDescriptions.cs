using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using aConverterClassLibrary;
using System.Diagnostics;
using System.IO;
using aConverterClassLibrary.Class;
using DbfClassLibrary;

namespace aConverter.Forms
{
    public partial class FormDescriptions : Form
    {
        public FormDescriptions()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCreateDescription_Click(object sender, EventArgs e)
        {
            List<Type> lt = new List<Type>();
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if (c is CheckBox)
                {
                    if ((c as CheckBox).Checked)
                    {
                        lt.Add((Type)c.Tag);
                    }
                }
            }
            saveFileDialog1.FileName = "Description.html";
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, 
                    TableManager.GetTablesDescription(lt), 
                    Encoding.GetEncoding(1251));
                DialogResult dr2 = MessageBox.Show("Открыть файл в браузере?", "Результаты", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr2 == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start(saveFileDialog1.FileName);
                }
                else
                    MessageBox.Show("Файл успешно сформирован и сохранен на диске.");
            }
        }

        private void FormDescriptions_Load(object sender, EventArgs e)
        {
            //foreach(Type t in FactoryRecord.GetAllRecordTypes())
            //{
            //    CheckBox cb = new CheckBox();
            //    cb.Checked = true;
            //    cb.Tag = t;
            //    cb.Text = "";
            //    cb.AutoSize = true;
            //    cb.Anchor = AnchorStyles.Left;
            //    tableLayoutPanel1.Controls.Add(cb);
            //    Label l = new Label();
            //    l.Text = TableManager.GetTableDescription(t) + " (" + TableManager.GetTableName(t) + ".DBF)";
            //    l.Anchor = AnchorStyles.Left;
            //    l.TextAlign = ContentAlignment.MiddleLeft;
            //    l.AutoSize = true;
            //    tableLayoutPanel1.Controls.Add(l);
            //}
        }
    }
}
