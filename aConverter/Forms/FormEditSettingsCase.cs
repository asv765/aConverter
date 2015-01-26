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
    public partial class FormEditSettingsCase : Form
    {
        private string settingsCaseName = "";
        /// <summary>
        /// Наименование варианта настройки
        /// </summary>
        public string SettingsCaseName
        {
            get { return settingsCaseName; }
            set { settingsCaseName = value; }
        }


        public FormEditSettingsCase()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.SettingsCaseName = textBoxSettingsCaseName.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void FormEditSettingsCase_Load(object sender, EventArgs e)
        {
            textBoxSettingsCaseName.Text = SettingsCaseName;
        }
    }
}
