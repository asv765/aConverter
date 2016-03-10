using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using aConverterClassLibrary;
using aConverterClassLibrary.RecordsDataAccessORM;
using aConverterClassLibrary.RecordsDataAccessORM.Utils;

namespace _041_Sarai
{
    #region Скрипты
    public static class Scripts
    {
        public const string SelectAbonents = @"SELECT tblАдрес.АдресИД AS LSHET,
                                                       tblАбоненты.Абонент AS FIO,
                                                       tblАдрес.ПунктИД AS TOWNSKOD,
                                                       tblПункт.Пункт AS TOWNSNAME,
                                                       (tblАдрес.ПунктИД*200 + tblАдрес.НазваниеИД) AS ULICAKOD,
                                                       IIf([tblАдрес]![НазваниеИД]=1,'Неизвестна',[tblНазвание]![Название]) AS ULICANAME,
                                                       tblАдрес.АдресДом AS HOUSENO,
                                                       tblАдрес.АдресКорпус AS KORPUSNO,
                                                       tblАдрес.АдресКвартира AS FLATNO
                                                FROM tblПункт
                                                INNER JOIN (tblНазвание
                                                            INNER JOIN (tblГруппа
                                                                        INNER JOIN (tblАбоненты
                                                                                    INNER JOIN tblАдрес 
                                                                                    ON tblАбоненты.АбонентИД = tblАдрес.АбонентИД)
                                                                        ON tblГруппа.ГруппаИД = tblАдрес.ГруппаИД) 
                                                            ON tblНазвание.НазваниеИД = tblАдрес.НазваниеИД) 
                                                ON tblПункт.ПунктИД = tblАдрес.ПунктИД
                                                ORDER BY tblАдрес.АдресИД;";
    }
    #endregion

    public static class Consts
    {
        public const string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=D:\Work\C#\C#Projects\aConverter\041_Sarai\Sources\январь_2016.mdb";

        public static string GetLs(long intls)
        {
            return String.Format("88{0:D6}", intls);
        }
    }

    /// <summary>
    /// Создать базу данных для конвертации
    /// </summary>
    public class CreateAllFiles : ConvertCase
    {
        public CreateAllFiles()
        {
            ConvertCaseName = "Создать таблицы для конвертации";
            Position = 10;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(1);
            StepStart(1);

            BufferEntitiesManager.DropAllProcedures();
            BufferEntitiesManager.DropAllEntities();
            BufferEntitiesManager.CreateAllEntities();
            BufferEntitiesManager.CreateAllProcedures();

            Result = ConvertCaseStatus.Шаг_выполнен_успешно;
            Iterate();
        }
    }

     #region Конвертация

    /// <summary>
    /// Конвертирует данные об абонентах
    /// </summary>
    public class ConvertAbonent : ConvertCase
    {
        public ConvertAbonent()
        {
            ConvertCaseName = "ABONENT - данные об абонентах";
            Position = 20;
            IsChecked = false;
        }

        public override void DoConvert()
        {
            SetStepsCount(4);

            BufferEntitiesManager.DropTableData("CNV$ABONENT");

            var fioRegex = new Regex(@"([а-я]+)[^а-я]*([а-я]+)?[^а-я]*([а-я]+)?\.?(.*)");

            DataTable dt;
            StepStart(1);
            using (var connection = new OleDbConnection(Consts.ConnectionString))
            {
                connection.Open();
                var ad = new OleDbDataAdapter(Scripts.SelectAbonents, connection);
                var ds = new DataSet("ACCESS");
                ad.Fill(ds);
                dt = ds.Tables[0];
            }
            StepFinish();

            StepStart(dt.Rows.Count);
            var lca = new List<CNV_ABONENT>();
            foreach (DataRow dataRow in dt.Rows)
            {
                var abonent = new CNV_ABONENT
                {
                    LSHET = Consts.GetLs(Convert.ToInt64(dataRow["LSHET"])),
                    DISTKOD = 1,
                    DISTNAME = "Рязанская область",
                    RAYONKOD = 2,
                    RAYONNAME = "Сараевский район",
                    DUCD = 1,
                    DUNAME = "",
                    ISDELETED = 0,
                };


            }
        }
    }

    #endregion
}
