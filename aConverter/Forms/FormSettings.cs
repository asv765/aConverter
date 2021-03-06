﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using aConverterClassLibrary;
using FirebirdSql.Data.FirebirdClient;

namespace aConverter.Forms
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveConfigData();
            Close();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            loadConfigData();
        }

        private void saveConfigData()
        {
            List<SettingsCase> lsc = new List<SettingsCase>();
            foreach (SettingsCase sc in comboBoxSettingsCase.Items)
            {
                lsc.Add(sc);
            }
            aConverter_RootSettings.WriteSettingsCase(lsc);

            aConverter_RootSettings.SettingsCaseId = comboBoxSettingsCase.SelectedIndex;
            aConverter_RootSettings.FirebirdStringConnection = textBoxFirebirdConnectionString.Text;
            aConverter_RootSettings.SourceDbfFilePath = textBoxSourceDBFFilePath.Text;
            aConverter_RootSettings.PatternsPath = textBoxPatternsPath.Text;
            aConverter_RootSettings.ConvertPath = textBoxConvertPath.Text;
            aConverter_RootSettings.CoverFileBodyPattern = textBoxCoverFileBodyPattern.Text;
            aConverter_RootSettings.GeneratedFilePath = textBoxGeneratedFilePath.Text;
            aConverter_RootSettings.IBScriptPath = textBoxIBEScriptPath.Text;
        }

        private void loadConfigData()
        {
            // Заполняем ComboBox
            List<SettingsCase> lsc = aConverter_RootSettings.ReadSettingsCase();
            foreach (SettingsCase sc in lsc)
            {
                comboBoxSettingsCase.Items.Add(sc);
            }
            comboBoxSettingsCase.SelectedIndex = aConverter_RootSettings.SettingsCaseId;

            textBoxFirebirdConnectionString.Text = aConverter_RootSettings.FirebirdStringConnection; // Строка подключения к базе данных Firebird
            textBoxGeneratedFilePath.Text = aConverter_RootSettings.GeneratedFilePath; // Путь для генерируемых файлов
            // textBoxCoverFilenamePattern.Text = aConverter_RootSettings.CoverFileNamePattern;
            textBoxSourceDBFFilePath.Text = aConverter_RootSettings.SourceDbfFilePath; // Путь к DBF файлам заказчика
            textBoxCoverFileBodyPattern.Text = aConverter_RootSettings.CoverFileBodyPattern; 
            textBoxPatternsPath.Text = aConverter_RootSettings.PatternsPath; // Путь к шаблонам
            textBoxConvertPath.Text = aConverter_RootSettings.ConvertPath; // Путь к модулям импорта
            textBoxIBEScriptPath.Text = aConverter_RootSettings.IBScriptPath; // Путь к IBEScript
        }

        private void buttonCheckConnection_Click(object sender, EventArgs e)
        {
            // string connectionString = textBoxFirebirdConnectionString.Text;
            FbConnectionStringBuilder fcsb = new FbConnectionStringBuilder(textBoxFirebirdConnectionString.Text); //////????????
            fcsb.Charset = "NONE";
            string connectionString = fcsb.ToString();
            using (FbConnection connection = new FbConnection(connectionString))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    try
                    {
                        // command.CommandText = "SELECT COUNT(*) FROM USERSVARIABLES";
                        command.CommandText = "SELECT FIRST 1 * FROM MON$ATTACHMENTS ORDER BY MON$TIMESTAMP DESC";
                        DataTable dt = new DataTable();
                        using (FbDataAdapter fda = new FbDataAdapter(command))
                        {
                            fda.Fill(dt);
                        }
                        // Encoding.Convert()
                        var connectionInfo = String.Format("База:{0}, IP:{1}, Process:{2}",
                            fcsb.Database, 
                            dt.Rows[0]["MON$REMOTE_ADDRESS"],
                            dt.Rows[0]["MON$REMOTE_PROCESS"]
                            );
                        MessageBox.Show(connectionInfo);
                        // int count = Convert.ToInt32(command.ExecuteScalar());
                        int count = dt.Rows.Count;
                        if (count > 0)
                            MessageBox.Show("Соединение успешно установлено!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка подключения:\n" + ex.Message,
                            "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void comboBoxSettingsCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Заполняем ComboBox
            SettingsCase sc = (SettingsCase)comboBoxSettingsCase.SelectedItem;

            textBoxFirebirdConnectionString.Text = sc.FirebirdStringConnection;
            textBoxSourceDBFFilePath.Text = sc.SourceDbfFilePath;
            textBoxPatternsPath.Text = sc.PatternsPath;
            textBoxConvertPath.Text = sc.ConvertPath;
            textBoxCoverFileBodyPattern.Text = sc.CoverFileBodyPattern;
            textBoxGeneratedFilePath.Text = sc.GeneratedFilePath;
        }

        private void buttonAddSettingsCase_Click(object sender, EventArgs e)
        {
            FormEditSettingsCase fesc = new FormEditSettingsCase();
            fesc.ShowDialog();
            if (fesc.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                SettingsCase sc = new SettingsCase();
                sc.SettingsCaseName = fesc.SettingsCaseName;
                sc.FirebirdStringConnection = "";
                sc.SourceDbfFilePath = "";
                sc.PatternsPath = "";
                sc.ConvertPath = "";
                sc.CoverFileBodyPattern = "";
                sc.GeneratedFilePath = "";
                

                //textBoxFirebirdConnectionString.Text = "";
                //textBoxSourceDBFFilePath.Text = "";
                comboBoxSettingsCase.Items.Add(sc);
                comboBoxSettingsCase.SelectedIndex = comboBoxSettingsCase.Items.Count - 1;
            }
        }

        private void buttonDeleteSettingsCase_Click(object sender, EventArgs e)
        {
            SettingsCase sc = (SettingsCase)comboBoxSettingsCase.SelectedItem;
            DialogResult dr = MessageBox.Show("Удалить вариант настройки '" + sc.SettingsCaseName + "'?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                comboBoxSettingsCase.Items.Remove(sc);
                if (comboBoxSettingsCase.Items.Count == 0)
                {
                    comboBoxSettingsCase.Text = "";
                    textBoxFirebirdConnectionString.Text = "";
                    textBoxPatternsPath.Text = "";
                }
                else
                    comboBoxSettingsCase.SelectedIndex = 0;
                comboBoxSettingsCase.Refresh();
            }
        }

        private void buttonEditSettingsCase_Click(object sender, EventArgs e)
        {
            SettingsCase sc = (SettingsCase)comboBoxSettingsCase.SelectedItem;
            FormEditSettingsCase fesc = new FormEditSettingsCase();
            fesc.SettingsCaseName = sc.SettingsCaseName;
            fesc.ShowDialog();
            if (fesc.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                sc.SettingsCaseName = fesc.SettingsCaseName;
                List<SettingsCase> lsc = new List<SettingsCase>();
                foreach (SettingsCase sc1 in comboBoxSettingsCase.Items) lsc.Add(sc1);
                comboBoxSettingsCase.Items.Clear();
                foreach (SettingsCase sc1 in lsc) comboBoxSettingsCase.Items.Add(sc1);
                comboBoxSettingsCase.SelectedItem = sc;
                comboBoxSettingsCase.Refresh();
            }
        }
    }
}
