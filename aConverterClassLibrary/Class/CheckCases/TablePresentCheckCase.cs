using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using aConverterClassLibrary.Class;
using DbfClassLibrary;
using MySql.Data.MySqlClient;

namespace aConverterClassLibrary
{
    public class TablePresentCheckCase: CheckCase
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

        public TablePresentCheckCase()
        {
            this.CheckCaseName = "Проверка наличия на диске файлов для конвертации";
            this.CheckCaseClass = CheckCaseClass.Целостность_структуры_конвертируемых_данных;
            this.CheckCaseParams = CheckCaseParams;
        }

        public override void Analize()
        {
            // List<ErrorClass> lec = new List<ErrorClass>();
            this.Result = CheckCaseStatus.Ошибок_не_выявлено;
            this.ErrorList.Clear();
            foreach (Type t in FactoryRecord.GetAllRecordTypes())
            {
                string tableName = TableManager.GetTableName(t);
                if (!File.Exists(path + "\\" + tableName + ".DBF"))
                {
                    TableNotFoundError tnfe = new TableNotFoundError(t);
                    tnfe.ParentCheckCase = this;
                    this.Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
                    this.ErrorList.Add(tnfe);
                }
            }
        }
    }
}
