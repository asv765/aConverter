using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aConverterClassLibrary.Records;
using DbfClassLibrary;

namespace aConverterClassLibrary.Class
{
    public class FactoryRecord
    {
        /// <summary>
        /// Возвращает список всех типов-неследников Record. Каждый
        /// тип соответствует своей DBF-таблице
        /// </summary>
        /// <returns></returns>
        public static List<Type> GetAllRecordTypes()
        {
            List<Type> lt = new List<Type>();

            lt.Add(typeof(AbonentRecord));
            lt.Add(typeof(AAddcharRecord));
            lt.Add(typeof(HAddcharRecord));
            lt.Add(typeof(CharlstRecord));
            lt.Add(typeof(CharsRecord));
            lt.Add(typeof(CntrsindRecord));
            lt.Add(typeof(CountersRecord));
            lt.Add(typeof(DogovorRecord));
            lt.Add(typeof(EquipmntRecord));
            lt.Add(typeof(LcharsRecord));
            lt.Add(typeof(LgotaRecord));
            lt.Add(typeof(NachoplRecord));
            lt.Add(typeof(NachRecord));
            lt.Add(typeof(OplataRecord));
            lt.Add(typeof(GcounterRecord));
            lt.Add(typeof(SupplnetRecord));
            lt.Add(typeof(CharvalsRecord));

            return lt;
        }

        /// <summary>
        /// (Пере)создает все результирующие таблицы
        /// </summary>
        public static void CreateAllTables(TableManager tm)
        {
            foreach (Type t in GetAllRecordTypes()) tm.CreateTable(t);
        }
    }
}
