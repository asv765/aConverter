// Файл сгенерирован aConverter
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace aConverterClassLibrary.Records
{
    [TableName("LGOTA.DBF")]
    [TableDescription("Справочник граждан и льгот")]
    [Index("LSHET", "LSHET"), Index("CitizenID","CitizenID"), Index("LGOTA", "LGOTA")]
    public class LgotaRecord: AbstractRecord
    {
        private string lshet;
        // <summary>
        // LSHET C(10)
        // </summary>
        [FieldName("LSHET"), FieldType('C'), FieldWidth(10), FieldDescription("Лицевой счет")]
        public string Lshet
        {
            get { return lshet; }
            set { CheckStringData("Lshet", value, 10); lshet = value; }
        }

        private Int64 citizenid;
        // <summary>
        // CITIZENID N(10)
        // </summary>
        [FieldName("CITIZENID"), FieldType('N'), FieldWidth(10), FieldDescription("Код гражданина")]
        public Int64 Citizenid
        {
            get { return citizenid; }
            set { CheckIntegerData("Citizenid", value, 10); citizenid = value; }
        }

        private string fio;
        // <summary>
        // FIO C(50)
        // </summary>
        [FieldName("FIO"), FieldType('C'), FieldWidth(50), FieldDescription("ФИО гражданина")]
        public string Fio
        {
            get { return fio; }
            set { CheckStringData("Fio", value, 50); fio = value; }
        }

        private DateTime birthdate;
        // <summary>
        // BIRTHDATE D
        // </summary>
        [FieldName("BIRTHDATE"), FieldType('D'), FieldDescription("Дата рождения")]
        public DateTime Birthdate
        {
            get { return birthdate; }
            set {  birthdate = value; }
        }

        private DateTime startdate;
        // <summary>
        // STARTDATE D
        // </summary>
        [FieldName("STARTDATE"), FieldType('D'), FieldDescription("Дата регистрации (прописки)")]
        public DateTime Startdate
        {
            get { return startdate; }
            set {  startdate = value; }
        }

        private DateTime enddate;
        // <summary>
        // ENDDATE D
        // </summary>
        [FieldName("ENDDATE"), FieldType('D'), FieldDescription("Дата окончания регистрации (выписки)")]
        public DateTime Enddate
        {
            get { return enddate; }
            set {  enddate = value; }
        }

        private Int64 lgota;
        // <summary>
        // LGOTA N(7)
        // </summary>
        [FieldName("LGOTA"), FieldType('N'), FieldWidth(7), FieldDescription("Код льготы")]
        public Int64 Lgota
        {
            get { return lgota; }
            set { CheckIntegerData("Lgota", value, 7); lgota = value; }
        }

        private string lgotaname;
        // <summary>
        // LGOTANAME C(100)
        // </summary>
        [FieldName("LGOTANAME"), FieldType('C'), FieldWidth(100), FieldDescription("Наименование льготы")]
        public string Lgotaname
        {
            get { return lgotaname; }
            set { CheckStringData("Lgotaname", value, 100); lgotaname = value; }
        }

        private DateTime datwp;
        // <summary>
        // DATWP D
        // </summary>
        [FieldName("DATWP"), FieldType('D'), FieldDescription("Дата начала (последнего возобновления) действия льготы")]
        public DateTime Datwp
        {
            get { return datwp; }
            set {  datwp = value; }
        }

        private DateTime datup;
        // <summary>
        // DATUP D
        // </summary>
        [FieldName("DATUP"), FieldType('D'), FieldDescription("Дата окончания (последнего приостановления) действия льготы. Не указывается, если срок окончания действия неопределен")]
        public DateTime Datup
        {
            get { return datup; }
            set {  datup = value; }
        }

        private string naim1;
        // <summary>
        // NAIM1 C(50)
        // </summary>
        [FieldName("NAIM1"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование первого документа на льготу")]
        public string Naim1
        {
            get { return naim1; }
            set { CheckStringData("Naim1", value, 50); naim1 = value; }
        }

        private string seria1;
        // <summary>
        // SERIA1 C(20)
        // </summary>
        [FieldName("SERIA1"), FieldType('C'), FieldWidth(20), FieldDescription("Серия первого документа на льготу")]
        public string Seria1
        {
            get { return seria1; }
            set { CheckStringData("Seria1", value, 20); seria1 = value; }
        }

        private string nomer1;
        // <summary>
        // NOMER1 C(50)
        // </summary>
        [FieldName("NOMER1"), FieldType('C'), FieldWidth(50), FieldDescription("Номер первого документа на льготу")]
        public string Nomer1
        {
            get { return nomer1; }
            set { CheckStringData("Nomer1", value, 50); nomer1 = value; }
        }

        private DateTime datdn1;
        // <summary>
        // DATDN1 D
        // </summary>
        [FieldName("DATDN1"), FieldType('D'), FieldDescription("Дата первого документа на льготу")]
        public DateTime Datdn1
        {
            get { return datdn1; }
            set {  datdn1 = value; }
        }

        private string dorgname1;
        // <summary>
        // DORGNAME1 C(50)
        // </summary>
        [FieldName("DORGNAME1"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование организации, выдавшей первый документ на льготу")]
        public string Dorgname1
        {
            get { return dorgname1; }
            set { CheckStringData("Dorgname1", value, 50); dorgname1 = value; }
        }

        private string naim2;
        // <summary>
        // NAIM2 C(50)
        // </summary>
        [FieldName("NAIM2"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование второго документа на льготу")]
        public string Naim2
        {
            get { return naim2; }
            set { CheckStringData("Naim2", value, 50); naim2 = value; }
        }

        private string seria2;
        // <summary>
        // SERIA2 C(20)
        // </summary>
        [FieldName("SERIA2"), FieldType('C'), FieldWidth(20), FieldDescription("Серия второго документа на льготу")]
        public string Seria2
        {
            get { return seria2; }
            set { CheckStringData("Seria2", value, 20); seria2 = value; }
        }

        private string nomer2;
        // <summary>
        // NOMER2 C(50)
        // </summary>
        [FieldName("NOMER2"), FieldType('C'), FieldWidth(50), FieldDescription("Номер второго документа на льготу")]
        public string Nomer2
        {
            get { return nomer2; }
            set { CheckStringData("Nomer2", value, 50); nomer2 = value; }
        }

        private DateTime datdn2;
        // <summary>
        // DATDN2 D
        // </summary>
        [FieldName("DATDN2"), FieldType('D'), FieldDescription("Дата второго документа на льготу")]
        public DateTime Datdn2
        {
            get { return datdn2; }
            set {  datdn2 = value; }
        }

        private string dorgname2;
        // <summary>
        // DORGNAME2 C(50)
        // </summary>
        [FieldName("DORGNAME2"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование организации, выдавшей второй документ на льготу")]
        public string Dorgname2
        {
            get { return dorgname2; }
            set { CheckStringData("Dorgname2", value, 50); dorgname2 = value; }
        }

        private string naim3;
        // <summary>
        // NAIM3 C(50)
        // </summary>
        [FieldName("NAIM3"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование третьего документа на льготу")]
        public string Naim3
        {
            get { return naim3; }
            set { CheckStringData("Naim3", value, 50); naim3 = value; }
        }

        private string seria3;
        // <summary>
        // SERIA3 C(20)
        // </summary>
        [FieldName("SERIA3"), FieldType('C'), FieldWidth(20), FieldDescription("Серия третьего документа на льготу")]
        public string Seria3
        {
            get { return seria3; }
            set { CheckStringData("Seria3", value, 20); seria3 = value; }
        }

        private string nomer3;
        // <summary>
        // NOMER3 C(50)
        // </summary>
        [FieldName("NOMER3"), FieldType('C'), FieldWidth(50), FieldDescription("Номер третьего документа на льготу")]
        public string Nomer3
        {
            get { return nomer3; }
            set { CheckStringData("Nomer3", value, 50); nomer3 = value; }
        }

        private DateTime datdn3;
        // <summary>
        // DATDN3 D
        // </summary>
        [FieldName("DATDN3"), FieldType('D'), FieldDescription("Дата выдачи третьго документа на льготу")]
        public DateTime Datdn3
        {
            get { return datdn3; }
            set {  datdn3 = value; }
        }

        private string dorgname3;
        // <summary>
        // DORGNAME3 C(50)
        // </summary>
        [FieldName("DORGNAME3"), FieldType('C'), FieldWidth(50), FieldDescription("Наименование организации, выдавшей третий документ на льготу")]
        public string Dorgname3
        {
            get { return dorgname3; }
            set { CheckStringData("Dorgname3", value, 50); dorgname3 = value; }
        }

        private Int64 kollg;
        // <summary>
        // KOLLG N(4)
        // </summary>
        [FieldName("KOLLG"), FieldType('N'), FieldWidth(4), FieldDescription("Число граждан, на которых действует льгота")]
        public Int64 Kollg
        {
            get { return kollg; }
            set { CheckIntegerData("Kollg", value, 4); kollg = value; }
        }

        private Int64 hoz;
        // <summary>
        // HOZ N(3)
        // </summary>
        [FieldName("HOZ"), FieldType('N'), FieldWidth(3), FieldDescription("Признак ответственного квартиросъемщика (хозяина лицевого счета) (0 – обычный, 1 – хозяин)")]
        public Int64 Hoz
        {
            get { return hoz; }
            set { CheckIntegerData("Hoz", value, 3); hoz = value; }
        }

        private string birthplace;
        // <summary>
        // BIRTHPLACE C(50)
        // </summary>
        [FieldName("BIRTHPLACE"), FieldType('C'), FieldWidth(50), FieldDescription("Место рождения")]
        public string Birthplace
        {
            get { return birthplace; }
            set { CheckStringData("Birthplace", value, 50); birthplace = value; }
        }

        private Int64 sob;
        // <summary>
        // SOB N(3)
        // </summary>
        [FieldName("SOB"), FieldType('N'), FieldWidth(3), FieldDescription("Собственность")]
        public Int64 Sob
        {
            get { return sob; }
            set { CheckIntegerData("Sob", value, 3); sob = value; }
        }

        private decimal dolya;
        // <summary>
        // DOLYA N(7,2)
        // </summary>
        [FieldName("DOLYA"), FieldType('N'), FieldWidth(7), FieldDec(2), FieldDescription("Доля гражданина")]
        public decimal Dolya
        {
            get { return dolya; }
            set { CheckDecimalData("Dolya", value, 7, 2); dolya = value; }
        }

        private string comment;
        // <summary>
        // COMMENT C(30)
        // </summary>
        [FieldName("COMMENT"), FieldType('C'), FieldWidth(30), FieldDescription("Примечание")]
        public string Comment
        {
            get { return comment; }
            set { CheckStringData("Comment", value, 30); comment = value; }
        }

        private string pribyt;
        // <summary>
        // PRIBYT C(100)
        // </summary>
        [FieldName("PRIBYT"), FieldType('C'), FieldWidth(100), FieldDescription("Пункт прибытия")]
        public string Pribyt
        {
            get { return pribyt; }
            set { CheckStringData("Pribyt", value, 100); pribyt = value; }
        }

        private string vremreg;
        // <summary>
        // VREMREG C(100)
        // </summary>
        [FieldName("VREMREG"), FieldType('C'), FieldWidth(100), FieldDescription("Адрес временной регистрации")]
        public string Vremreg
        {
            get { return vremreg; }
            set { CheckStringData("Vremreg", value, 100); vremreg = value; }
        }

        public override void ReadDataRow(System.Data.DataRow ADataRow)
        {
            Lshet = ADataRow["LSHET"].ToString();
            Citizenid = Convert.ToInt64(ADataRow["CITIZENID"]);
            Fio = ADataRow["FIO"].ToString();
            Birthdate = Convert.ToDateTime(ADataRow["BIRTHDATE"]);
            Startdate = Convert.ToDateTime(ADataRow["STARTDATE"]);
            Enddate = Convert.ToDateTime(ADataRow["ENDDATE"]);
            Lgota = Convert.ToInt64(ADataRow["LGOTA"]);
            Lgotaname = ADataRow["LGOTANAME"].ToString();
            Datwp = Convert.ToDateTime(ADataRow["DATWP"]);
            Datup = Convert.ToDateTime(ADataRow["DATUP"]);
            Naim1 = ADataRow["NAIM1"].ToString();
            Seria1 = ADataRow["SERIA1"].ToString();
            Nomer1 = ADataRow["NOMER1"].ToString();
            Datdn1 = Convert.ToDateTime(ADataRow["DATDN1"]);
            Dorgname1 = ADataRow["DORGNAME1"].ToString();
            Naim2 = ADataRow["NAIM2"].ToString();
            Seria2 = ADataRow["SERIA2"].ToString();
            Nomer2 = ADataRow["NOMER2"].ToString();
            Datdn2 = Convert.ToDateTime(ADataRow["DATDN2"]);
            Dorgname2 = ADataRow["DORGNAME2"].ToString();
            Naim3 = ADataRow["NAIM3"].ToString();
            Seria3 = ADataRow["SERIA3"].ToString();
            Nomer3 = ADataRow["NOMER3"].ToString();
            Datdn3 = Convert.ToDateTime(ADataRow["DATDN3"]);
            Dorgname3 = ADataRow["DORGNAME3"].ToString();
            Kollg = Convert.ToInt64(ADataRow["KOLLG"]);
            Hoz = Convert.ToInt64(ADataRow["HOZ"]);
            Birthplace = ADataRow["BIRTHPLACE"].ToString();
            Sob = Convert.ToInt64(ADataRow["SOB"]);
            Dolya = Convert.ToDecimal(ADataRow["DOLYA"]);
            Comment = ADataRow["COMMENT"].ToString();
            Pribyt = ADataRow["PRIBYT"].ToString();
            Vremreg = ADataRow["VREMREG"].ToString();
        }

        public override string GetInsertScript()
        {
           string rs = String.Format("INSERT INTO LGOTA (LSHET, CITIZENID, FIO, BIRTHDATE, STARTDATE, ENDDATE, LGOTA, LGOTANAME, DATWP, DATUP, NAIM1, SERIA1, NOMER1, DATDN1, DORGNAME1, NAIM2, SERIA2, NOMER2, DATDN2, DORGNAME2, NAIM3, SERIA3, NOMER3, DATDN3, DORGNAME3, KOLLG, HOZ, BIRTHPLACE, SOB, DOLYA, COMMENT, PRIBYT, VREMREG) VALUES ('{0}', {1}, '{2}', CTOD('{3}'), CTOD('{4}'), CTOD('{5}'), {6}, '{7}', CTOD('{8}'), CTOD('{9}'), '{10}', '{11}', '{12}', CTOD('{13}'), '{14}', '{15}', '{16}', '{17}', CTOD('{18}'), '{19}', '{20}', '{21}', '{22}', CTOD('{23}'), '{24}', {25}, {26}, '{27}', {28}, {29}, '{30}', '{31}', '{32}')", String.IsNullOrEmpty(Lshet) ? "" : Lshet.Trim(), Citizenid.ToString(), String.IsNullOrEmpty(Fio) ? "" : Fio.Trim(), Birthdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Birthdate.Month, Birthdate.Day, Birthdate.Year), Startdate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Startdate.Month, Startdate.Day, Startdate.Year), Enddate == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Enddate.Month, Enddate.Day, Enddate.Year), Lgota.ToString(), String.IsNullOrEmpty(Lgotaname) ? "" : Lgotaname.Trim(), Datwp == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datwp.Month, Datwp.Day, Datwp.Year), Datup == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datup.Month, Datup.Day, Datup.Year), String.IsNullOrEmpty(Naim1) ? "" : Naim1.Trim(), String.IsNullOrEmpty(Seria1) ? "" : Seria1.Trim(), String.IsNullOrEmpty(Nomer1) ? "" : Nomer1.Trim(), Datdn1 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datdn1.Month, Datdn1.Day, Datdn1.Year), String.IsNullOrEmpty(Dorgname1) ? "" : Dorgname1.Trim(), String.IsNullOrEmpty(Naim2) ? "" : Naim2.Trim(), String.IsNullOrEmpty(Seria2) ? "" : Seria2.Trim(), String.IsNullOrEmpty(Nomer2) ? "" : Nomer2.Trim(), Datdn2 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datdn2.Month, Datdn2.Day, Datdn2.Year), String.IsNullOrEmpty(Dorgname2) ? "" : Dorgname2.Trim(), String.IsNullOrEmpty(Naim3) ? "" : Naim3.Trim(), String.IsNullOrEmpty(Seria3) ? "" : Seria3.Trim(), String.IsNullOrEmpty(Nomer3) ? "" : Nomer3.Trim(), Datdn3 == DateTime.MinValue ? "" : String.Format("{0}/{1}/{2}", Datdn3.Month, Datdn3.Day, Datdn3.Year), String.IsNullOrEmpty(Dorgname3) ? "" : Dorgname3.Trim(), Kollg.ToString(), Hoz.ToString(), String.IsNullOrEmpty(Birthplace) ? "" : Birthplace.Trim(), Sob.ToString(), Dolya.ToString().Replace(',','.'), String.IsNullOrEmpty(Comment) ? "" : Comment.Trim(), String.IsNullOrEmpty(Pribyt) ? "" : Pribyt.Trim(), String.IsNullOrEmpty(Vremreg) ? "" : Vremreg.Trim());
           return rs;
        }
    }
}

