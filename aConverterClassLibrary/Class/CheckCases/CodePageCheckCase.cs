using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using aConverterClassLibrary.Class;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public class CodePageCheckCase: CheckCase
    {
        private string path
        {
            get
            {
                if (!String.IsNullOrEmpty(aConverter_RootSettings.DBFConnectionString))
                {
                    // @"Provider=vfpoledb.1;Data Source={0};Collating Sequence=Russian"
                    string[] csa = aConverter_RootSettings.DBFConnectionString.Split(';');
                    foreach (string s in csa)
                    {
                        if (s.Substring(0, 11) == "Data Source")
                        {
                            string[] dss = s.Split('=');
                            return dss[1];
                        }
                    }
                    throw new Exception("В строке подключения не найдена секция Data Source. Не удается определить путь к DBF-файлам");
                }
                else
                    throw new Exception("Строка подключения пустая! Не удается определить путь к DBF-файлам");
            }
        }

        public CodePageCheckCase()
        {
            this.CheckCaseName = "Проверка соответствия кодовой страницы (ожидаем 866 DOS Russian)";
            this.CheckCaseClass = CheckCaseClass.Целостность_структуры_конвертируемых_данных;
            // this.CheckCaseParams = CheckCaseParams;
        }

        public override void Analize()
        {
            // List<ErrorClass> lec = new List<ErrorClass>();
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();
            foreach (Type t in FactoryRecord.GetAllRecordTypes())
            {
                string tableName = TableManager.GetTableName(t);
                string fileName = path + "\\" + tableName + ".DBF";
                if (!File.Exists(fileName))
                {
                    TableNotFoundError tnfe = new TableNotFoundError(t);
                    tnfe.ParentCheckCase = this;
                    this.Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
                    this.ErrorList.Add(tnfe);
                }
                else
                {
                    // Открываем файл для бинарного чтения
                    BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open));
                    byte[] ba = br.ReadBytes(30);
                    br.Close();
                    if (ba[0] < 3 && ba[0] > 5)
                    {
                        WrongFileError wfe = new WrongFileError(t);
                        wfe.ParentCheckCase = this;
                        this.Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
                        this.ErrorList.Add(wfe);
                        continue;
                    }
                    if (ba[29] != 101) // Кодовая страница не DOS 866 Russian
                    {
                        WrongCodePageError wcpe = new WrongCodePageError(t, ba[29]);
                        wcpe.ParentCheckCase = this;
                        this.Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
                        this.ErrorList.Add(wcpe);
                    }
                }
            }
        }
    }
}
