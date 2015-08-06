using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using aConverterClassLibrary;
using System.IO;
using aConverter.Forms;
using DbfClassLibrary;
using FirebirdSql.Data.FirebirdClient;

namespace aConverter
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void экспериментыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string s = "0";
            string[] sa = s.Replace("..", "~").Split('~');
            MessageBox.Show(sa.Length.ToString(CultureInfo.InvariantCulture));
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fs = new FormSettings();
            fs.ShowDialog();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text += " " + aConverterClassLibrary.Utils.GetVersion().ToString();
            WindowState = FormWindowState.Maximized;
        }

        private void файлаcsПеречисленийДляLCHARSDBFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connectionString = aConverter_RootSettings.FirebirdStringConnection;
            using (FbConnection connection = new FbConnection(connectionString))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    try
                    {
                        #region Получаем данные от базы Firebird
                        command.CommandText = "select ll.kod, ll.name, lv.significance, lv.logicsignificance " +
                            "from lcharslist ll inner join logicvalues lv on ll.kod = lv.kod " +
                            "order by ll.kod, lv.significance";
                        connection.Open();
                        DataTable tmpDataTable = new DataTable();
                        FbDataAdapter da = new FbDataAdapter(command);
                        da.Fill(tmpDataTable);
                        #endregion

                        #region Формируем содержимое
                        int counter = 1;
                        string fileBody = "    public enum lChars_enum\r\n"; fileBody += "    {\r\n";
                        foreach (DataRow row in tmpDataTable.Rows)
                        {
                            string lvName = row["name"].ToString();
                            lvName = lvName.Trim().Replace(".", "_").Replace("+", "_").Replace(",", "").Replace("(", "").Replace(")", "").Replace("-", "_").Replace(" ", "_").Replace("\"", "_").Replace("\\","_").Replace("/","_");
                            string lvValue = row["logicsignificance"].ToString();
                            lvValue = lvValue.Trim().Replace(".", "_").Replace("+", "_").Replace(",", "").Replace("(", "").Replace(")", "").Replace("-", "_").Replace(" ", "_").Replace("\"", "_").Replace("\\", "_").Replace("/", "_");

                            int enumCod = Convert.ToInt32(row["kod"]) * 1000 + Convert.ToInt32(row["significance"]);
                            string newString = String.Format("        {0}_{1}_{2}_{3} = {4}", lvName, row["kod"], lvValue, row["significance"].ToString().Replace("-","m"), enumCod);
                            if (counter < tmpDataTable.Rows.Count) newString += ", ";
                            fileBody += newString + "\r\n";
                            counter++;
                        }
                        fileBody += "    }\r\n";
                        fileBody = aConverter_RootSettings.CoverFileBodyPattern.Replace("%s", fileBody);
                        #endregion

                        #region Сохраняем в файл
                        File.WriteAllText(aConverter_RootSettings.GeneratedFilePath + "\\" + "lChars_enum.cs",
                            fileBody, Encoding.GetEncoding(1251));
                        #endregion

                        MessageBox.Show(@"Файл сгенерирован успешно.", @"Результаты", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"Ошибка выполнения команды:" + ex.Message,
                            @"Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void файлcsОберткаДляРаботыСDBFтаблицейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Алгоритм
            // Инициализации
            string dataSource = String.Format(TableManager.VFPOLEDBConnectionString,
                aConverter_RootSettings.SourceDbfFilePath);

            // 1. Получаем список всех файлов в исходном каталоге
            string[] files = Directory.GetFiles(aConverter_RootSettings.SourceDbfFilePath, "*.DBF");
            // 2. Запускаем цикл по файлам
            int counter = 0;

            var crgc = new CoverRecordGeneratorClass(dataSource, aConverter_RootSettings.CoverFileBodyPattern);
            foreach (string fn in files)
            {
                string shortFileName = Path.GetFileNameWithoutExtension(fn);
                // Шаблон для имени .cs файлов (%f - имя (без расширения) исходного DBF-файла)
                const string coverFileNamePattern = "%fRecord.cs";
                string destFileName = coverFileNamePattern.Replace("%f",
                        shortFileName.Substring(0, 1).ToUpper() + shortFileName.Substring(1, shortFileName.Length - 1).ToLower());
                try
                {
                    string fileBody = crgc.Generate(shortFileName);
                    File.WriteAllText(aConverter_RootSettings.GeneratedFilePath + "\\" + destFileName,
                        fileBody, Encoding.GetEncoding(1251));
                    counter++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при генерации файла " + destFileName + ":\n" + ex.Message,
                        "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (counter > 0)
                MessageBox.Show(String.Format("Успешно сгенерировано {0} файлов!", counter),
                    "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Файлы не были сгенерированы. Возможно отсутствуют исходные DBF-файлы для генерации!",
                    "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void файлаcsПеречисленияДляCHARSDBFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connectionString = aConverter_RootSettings.FirebirdStringConnection;
            using (FbConnection connection = new FbConnection(connectionString))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    try
                    {
                        #region Получаем данные от базы Firebird
                        command.CommandText = "select cl.kod, cl.name " +
                            "from ccharslist cl " +
                            "where cl.iscalculated = 0 " +
                            "order by cl.kod, cl.name";
                        connection.Open();
                        DataTable tmpDataTable = new DataTable();
                        FbDataAdapter da = new FbDataAdapter(command);
                        da.Fill(tmpDataTable);
                        #endregion

                        #region Формируем содержимое
                        int counter = 1;
                        string fileBody = "    public enum cChars_enum\r\n"; fileBody += "    {\r\n";
                        foreach (DataRow row in tmpDataTable.Rows)
                        {
                            string clName = row["name"].ToString();
                            clName = clName.Trim().Replace(".", "_").Replace("+", "_").Replace(",", "").Replace("(", "").Replace(")", "").Replace("-", "_").Replace(" ", "_").Replace("\"","_");

                            int enumCod = Convert.ToInt32(row["kod"]);
                            
                            string newString = String.Format("        {0}_{1} = {2}", clName, row["kod"], enumCod);
                            if (counter < tmpDataTable.Rows.Count) newString += ", ";
                            fileBody += newString + "\r\n";
                            counter++;
                        }
                        fileBody += "    }\r\n";
                        fileBody = aConverter_RootSettings.CoverFileBodyPattern.Replace("%s", fileBody);
                        #endregion

                        #region Сохраняем в файл
                        File.WriteAllText(aConverter_RootSettings.GeneratedFilePath + "\\" + "cChars_enum.cs",
                            fileBody, Encoding.GetEncoding(1251));
                        #endregion

                        MessageBox.Show("Файл сгенерирован успешно.", "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка выполнения команды:\n" + ex.Message,
                            "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void файлcsПеречисленияСоСпискомИсточниковПлатежейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connectionString = aConverter_RootSettings.FirebirdStringConnection;
            using (FbConnection connection = new FbConnection(connectionString))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    try
                    {
                        #region Получаем данные от базы Firebird
                        command.CommandText = "select sd.SourceDocCD, sd.SourceDocNm " +
                            "from SourceDoc sd " +
                            "order by sd.SourceDocCD, sd.SourceDocNm";
                        connection.Open();
                        DataTable tmpDataTable = new DataTable();
                        FbDataAdapter da = new FbDataAdapter(command);
                        da.Fill(tmpDataTable);
                        #endregion

                        #region Формируем содержимое
                        int counter = 1;
                        string fileBody = "    public enum SourceDoc_enum\r\n"; fileBody += "    {\r\n";
                        foreach (DataRow row in tmpDataTable.Rows)
                        {
                            string clName = row["SourceDocNm"].ToString();
                            clName = clName.Trim().Replace(".", "_").Replace("+", "_").Replace(",", "").Replace("(", "").Replace(")", "").Replace("-", "_").Replace(" ", "_").Replace("\"", "_");

                            int enumCod = Convert.ToInt32(row["SourceDocCD"]);

                            string newString = String.Format("        {0} = {1}", clName, enumCod);
                            if (counter < tmpDataTable.Rows.Count) newString += ", ";
                            fileBody += newString + "\r\n";
                            counter++;
                        }
                        fileBody += "    }\r\n";
                        fileBody = aConverter_RootSettings.CoverFileBodyPattern.Replace("%s", fileBody);
                        #endregion

                        #region Сохраняем в файл
                        File.WriteAllText(aConverter_RootSettings.GeneratedFilePath + "\\" + "SourceDoc_enum.cs",
                            fileBody, Encoding.GetEncoding(1251));
                        #endregion

                        MessageBox.Show("Файл сгенерирован успешно.", "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка выполнения команды:\n" + ex.Message,
                            "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void файлcaПеречисленияСоСпискомУслугToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connectionString = aConverter_RootSettings.FirebirdStringConnection;
            using (FbConnection connection = new FbConnection(connectionString))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    try
                    {
                        #region Получаем данные от базы Firebird
                        command.CommandText = "select bl.Balance_Kod, bl.Name " +
                            "from BalancesList bl " +
                            "order by bl.Balance_Kod, bl.Name";
                        connection.Open();
                        DataTable tmpDataTable = new DataTable();
                        FbDataAdapter da = new FbDataAdapter(command);
                        da.Fill(tmpDataTable);
                        #endregion

                        #region Формируем содержимое
                        int counter = 1;
                        string fileBody = "    public enum BalancesList_enum\r\n"; fileBody += "    {\r\n";
                        foreach (DataRow row in tmpDataTable.Rows)
                        {
                            string clName = row["Name"].ToString();
                            clName = clName.Trim().Replace(".", "_").Replace("+", "_").Replace(",", "").Replace("(", "").Replace(")", "").Replace("-", "_").Replace(" ", "_").Replace("\"", "_");

                            int enumCod = Convert.ToInt32(row["Balance_Kod"]);

                            string newString = String.Format("        {0}_{1} = {1}", clName, enumCod);
                            if (counter < tmpDataTable.Rows.Count) newString += ", ";
                            fileBody += newString + "\r\n";
                            counter++;
                        }
                        fileBody += "    }\r\n";
                        fileBody = aConverter_RootSettings.CoverFileBodyPattern.Replace("%s", fileBody);
                        #endregion

                        #region Сохраняем в файл
                        File.WriteAllText(aConverter_RootSettings.GeneratedFilePath + "\\" + "BalancesList_enum.cs",
                            fileBody, Encoding.GetEncoding(1251));
                        #endregion

                        MessageBox.Show("Файл сгенерирован успешно.", "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка выполнения команды:\n" + ex.Message,
                            "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void проверкаЦелостностиДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fccle = new FormCheckCasesListExtended {MdiParent = this, WindowState = FormWindowState.Maximized};
            fccle.Show();
        }

        private void файлcsПеречисленияПараметровГражданToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connectionString = aConverter_RootSettings.FirebirdStringConnection;
            using (FbConnection connection = new FbConnection(connectionString))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    try
                    {
                        #region Получаем данные от базы Firebird
                        command.CommandText = "select ac.additionalcharcd, ac.additionalcharname " +
                            "from ADDITIONALCHARS ac " +
                            "where ac.additionalcharsgroupcd = 3 " +
                            "order by ac.additionalcharcd";
                        connection.Open();
                        DataTable tmpDataTable = new DataTable();
                        FbDataAdapter da = new FbDataAdapter(command);
                        da.Fill(tmpDataTable);
                        #endregion

                        #region Формируем содержимое
                        int counter = 1;
                        string fileBody = "    public enum AdditionalChar\r\n"; fileBody += "    {\r\n";
                        foreach (DataRow row in tmpDataTable.Rows)
                        {
                            string clName = row["additionalcharname"].ToString();
                            
                            clName = clName.Trim().Replace(".", "_").Replace("+", "_").Replace(",", "").Replace("(", "").Replace(")", "").Replace("-", "_").Replace(" ", "_").Replace("\"", "_").Replace(":", "_").Replace("/", "_");
                            
                            int enumCod = Convert.ToInt32(row["additionalcharcd"]);
                            
                            clName += "_" + enumCod.ToString();

                            string newString = String.Format("        {0} = {1}", clName, enumCod);
                            if (counter < tmpDataTable.Rows.Count) newString += ", ";
                            fileBody += newString + "\r\n";
                            counter++;
                        }
                        fileBody += "    }\r\n";
                        fileBody = aConverter_RootSettings.CoverFileBodyPattern.Replace("%s", fileBody);
                        #endregion

                        #region Сохраняем в файл
                        File.WriteAllText(aConverter_RootSettings.GeneratedFilePath + "\\" + "AdditionalChar_enum.cs",
                            fileBody, Encoding.GetEncoding(1251));
                        #endregion

                        MessageBox.Show("Файл сгенерирован успешно.", "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка выполнения команды:\n" + ex.Message,
                            "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void описаниеТиповойСтруктурыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDescriptions fd = new FormDescriptions();
            fd.ShowDialog();
        }

        private void статистикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormStatistics fss = new FormStatistics(aConverter_RootSettings.ReadStatistics());
            fss.MdiParent = this;
            fss.Show();
        }

        private void шаблоныToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormPatterns fp = new FormPatterns();
            fp.MdiParent = this;
            fp.Show();
        }

        private void конвертацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConvert fc = new FormConvert();
            fc.MdiParent = this;
            fc.Show();
        }

        private void сгенерироватьОберткуПоОдномуDBFфайлуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\Work\aConverter_Data";
            openFileDialog1.Filter = "dbf files (*.dbf)|*.dbf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                // Алгоритм
                // Инициализации
                string filePath = Path.GetDirectoryName(openFileDialog1.FileName);
                string dataSource = String.Format(TableManager.VFPOLEDBConnectionString, filePath);

                CoverRecordGeneratorClass crgc = new CoverRecordGeneratorClass(dataSource, aConverter_RootSettings.CoverFileBodyPattern);
                
                string shortFileName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                //// Шаблон для имени .cs файлов (%f - имя (без расширения) исходного DBF-файла)
                //string coverFileNamePattern = "%fRecord.cs";
                //string destFileName = coverFileNamePattern.Replace("%f",
                //        shortFileName.Substring(0, 1).ToUpper() + shortFileName.Substring(1, shortFileName.Length - 1).ToLower());
                try
                {
                    string fileBody = crgc.Generate(shortFileName);
                    FormText ft = new FormText(fileBody);
                    ft.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при генерации обертки по файлу " + openFileDialog1.FileName + ":\n" + ex.Message,
                        "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void анализИсходныхФайловToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSourceAnalize fsa = new FormSourceAnalize();
            fsa.MdiParent = this;
            fsa.Show();

        }
    }
}
