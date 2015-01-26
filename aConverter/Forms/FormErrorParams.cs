using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using aConverterClassLibrary;

namespace aConverter.Forms
{
    public partial class FormErrorParams : Form
    {
        private Dictionary<ErrorParam, object> errorParams;

        public FormErrorParams(Dictionary<ErrorParam, object> AErrorParams)
        {
            InitializeComponent();
            errorParams = AErrorParams;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void FormErrorParams_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<ErrorParam, object> kvp in errorParams)
            {
                Label l = new Label();
                l.AutoSize = true;
                l.Anchor = AnchorStyles.Left;
                l.Text = kvp.Key.ToString().Replace('_', ' ')+":";
                l.Tag = kvp.Key;
                tableLayoutPanel1.Controls.Add(l);

                Type t = ErrorParamsUtils.GetTypeByErrorParams(kvp.Key);
                if (t.IsEnum)
                {
                    ComboBox cb = new ComboBox();
                    cb.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

                    foreach (string s in Enum.GetNames(t))
                    {
                        cb.Items.Add(s.Replace('_', ' '));
                    }
                    cb.SelectedIndex = Convert.ToInt32(kvp.Value);
                    tableLayoutPanel1.Controls.Add(cb);
                }
                else if (t.ToString() == "System.String")
                {
                    TextBox tb = new TextBox();
                    tb.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                    tableLayoutPanel1.Controls.Add(tb);
                }

            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            for(int i = 0; i<tableLayoutPanel1.Controls.Count / 2; i++)
            {
                Control c = tableLayoutPanel1.GetControlFromPosition(0, i);
                ErrorParam ep = (ErrorParam)c.Tag;
                Type t = ErrorParamsUtils.GetTypeByErrorParams(ep);
                if (t.IsEnum)
                {
                    ComboBox cb = (ComboBox)tableLayoutPanel1.GetControlFromPosition(1, i);
                    errorParams[ep] = cb.SelectedIndex;
                }
                else if (t.ToString() == "System.String")
                {
                    TextBox tb = (TextBox)tableLayoutPanel1.GetControlFromPosition(1, i);
                    errorParams[ep] = tb.Text;
                }
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
