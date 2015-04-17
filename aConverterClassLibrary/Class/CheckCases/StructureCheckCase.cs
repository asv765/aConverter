using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aConverterClassLibrary;
using System.IO;
using System.Data.OleDb;
using System.Data;
using aConverterClassLibrary.Class;
using DbfClassLibrary;
using MySql.Data.MySqlClient;

namespace aConverterClassLibrary
{
    public class StructureCheckCase: CheckCase
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

        public StructureCheckCase()
        {
            this.CheckCaseName = "Проверка наличия на диске и соответствия структуры файлов для конвертации";
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
                List<string> errorDescriptionList = new List<string>();
                string tableName = TableManager.GetTableName(t);
                if (!File.Exists(path + "\\" + tableName + ".DBF"))
                {
                    TableNotFoundError tnfe = new TableNotFoundError(t);
                    tnfe.ParentCheckCase = this;
                    this.Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
                    this.ErrorList.Add(tnfe);
                }
                else // Проверяем структуру файла 
                {
                    #region 1. Для записи данного типа получаем список всех полей и запускаем цикл по ним
                    List<DbfField> ldfStandart = TableManager.GetFieldList(t);
                    List<DbfField> ldfFact = CoverRecordGeneratorClass.GetFieldList(tableName,
                        String.Format(TableManager.VFPOLEDBConnectionString, aConverter_RootSettings.DestDBFFilePath));
                    
                    List<DbfField> modifiedField = new List<DbfField>();
                    List<DbfField> addedField = new List<DbfField>();

                    bool fileStructureMismatch = false;
                    foreach (DbfField dfStandart in ldfStandart)
                    {
                        bool fieldNotPresent = true;
                        // 2. Для каждого поля ищем соответствующее ему по имени в анализируемом файле
                        foreach (DbfField dfFact in ldfFact)
                        {
                            // 4. Если поле найдено, проверяем, соответствует ли тип
                            if (dfStandart.FieldName.ToUpper() == dfFact.FieldName.ToUpper())
                            {
                                fieldNotPresent = false;
                                bool fieldStructureMismatch = false;
                                if (dfFact.FieldType != dfStandart.FieldType) fieldStructureMismatch = true;
                                else if (dfFact.FieldType == dfStandart.FieldType && dfFact.FieldWidth < dfStandart.FieldWidth) fieldStructureMismatch = true;
                                else if (dfFact.FieldType == dfStandart.FieldType && dfFact.FieldWidth >= dfStandart.FieldWidth && dfFact.FieldDec < dfStandart.FieldDec) fieldStructureMismatch = true;
                                if (fieldStructureMismatch)
                                {
                                    fileStructureMismatch = true;
                                    string errorString = "";
                                    errorString = String.Format("Тип поля {0} в проверяемом файле {1} (тип {2}, длина {3}, точность {4}) не соответствует стандартному типу (тип {5}, длина {6}, точность {7})",
                                        dfStandart.FieldName, tableName + ".DBF",
                                        dfFact.FieldType, dfFact.FieldWidth, dfFact.FieldDec,
                                        dfStandart.FieldType, dfStandart.FieldWidth, dfStandart.FieldDec);
                                    errorDescriptionList.Add(errorString);
                                    modifiedField.Add(dfStandart);
                                }
                            }
                        }
                        if (fieldNotPresent)
                        {
                            errorDescriptionList.Add(
                                String.Format("Отсутствует поле {0}, тип {1}, длина {2}, точность {3}",
                                dfStandart.FieldName, dfStandart.FieldType, dfStandart.FieldWidth, dfStandart.FieldDec) 
                                );
                            addedField.Add(dfStandart);
                            fileStructureMismatch = true;
                        }
                        // 3. Если поле не найдено или структура его не совпадает, генерируем ошибку
                    }
                    if (fileStructureMismatch)
                    {
                        // Генерируется ошибка несовпадения структуры
                        DbfStructureError dse = new DbfStructureError(t, modifiedField, addedField);
                        dse.ParentCheckCase = this;
                        dse.Detail = errorDescriptionList;
                        this.Result = CheckCaseStatus.Выявлена_терминальная_ошибка;
                        this.ErrorList.Add(dse);
                    }

                    #endregion

                    // 6. Исправлять ошибки типа можно, например, так:
                    //     - ошибочный файл переименовывается;
                    //     - создается файл с типовой структурой;
                    //     - коммандой APPEND FROM в созданный файл добавляются записи;
                    //     - файл с ошибочной структурой удаляется.
                    // 7. Проверяется наличие нужных индексов. 
                    // 8. Если индексы отсутствуют, либо выражение фильтрации отличается от заданного генерируется ошибка несоответствия индексов
                    // 9. Ошибка несоответствия типов исправляется так:
                    //      - все индексы удаляются;
                    //      - все типовые индексы создаются заново.
                    // 10. Проверку индексов, возможно, сделать отдельным CheckCase-ом
                }
            }
        }

    }
}
