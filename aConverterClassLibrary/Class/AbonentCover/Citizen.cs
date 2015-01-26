using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class Citizen: AbonentCover
    {
        #region Поля с данными
        private int? citizenID;
        /// <summary>
        /// CITYZEN_ID            IDENTIFER NOT NULL /* IDENTIFER = INTEGER */,
        /// </summary>
        public int? CitizenID
        {
            get { return citizenID; }
            set { citizenID = value; }
        }

        private string lshet;
        /// <summary>
        /// LSHET                 VARCHAR(10)
        /// </summary>
        public string Lshet
        {
            get { return lshet; }
            set { lshet = value; }
        }

        private bool? ismaincitizen;
        /// <summary>
        /// ISMAINCITYZEN         BOOLEAN /* BOOLEAN = SMALLINT CHECK (VALUE IN (0, 1)) */,
        /// </summary>
        public bool? Ismaincitizen
        {
            get { return ismaincitizen; }
            set { ismaincitizen = value; }
        }

        private string pasportno = null;
        /// <summary>
        /// PASPORTNO             VARCHAR(20),
        /// </summary>
        public string Pasportno
        {
            get { return pasportno; }
            set { pasportno = value; }
        }

        private string pasportsr = null;
        /// <summary>
        /// PASPORTSR             VARCHAR(10)
        /// </summary>
        public string Pasportsr
        {
            get { return pasportsr; }
            set { pasportsr = value; }
        }

        private DateTime? passportdate;
        /// <summary>
        /// PASSPORTDATE          TDATE /* TDATE = DATE */,
        /// </summary>
        public DateTime? Passportdate
        {
            get { return passportdate; }
            set { passportdate = value; }
        }

        private string passportnote = null;
        /// <summary>
        /// PASSPORTNOTE          TEXT_BLOB /* TEXT_BLOB = BLOB SUB_TYPE 1 SEGMENT SIZE 80 */,
        /// </summary>
        public string Passportnote
        {
            get { return passportnote; }
            set { passportnote = value; }
        }

        private string ctzfio = null;
        /// <summary>
        /// CTZFIO                VARCHAR(30),
        /// </summary>
        public string Ctzfio
        {
            get { return ctzfio; }
            set { ctzfio = value; }
        }

        private string ctzname = null;
        /// <summary>
        /// CTZNAME               VARCHAR(20),
        /// </summary>
        public string Ctzname
        {
            get { return ctzname; }
            set { ctzname = value; }
        }

        private string ctzparentname = null;
        /// <summary>
        /// CTZPARENTNAME         VARCHAR(20),
        /// </summary>
        public string Ctzparentname
        {
            get { return ctzparentname; }
            set { ctzparentname = value; }
        }

        private DateTime? startdate;
        /// <summary>
        /// STARTDATE             TDATE /* TDATE = DATE */,
        /// </summary>
        public DateTime? Startdate
        {
            get { return startdate; }
            set { startdate = value; }
        }

        private DateTime? enddate;
        /// <summary>
        /// ENDDATE               TDATE /* TDATE = DATE */,
        /// </summary>
        public DateTime? Enddate
        {
            get { return enddate; }
            set { enddate = value; }
        }

        private string note = null;
        /// <summary>
        /// NOTE                  TEXT_BLOB /* TEXT_BLOB = BLOB SUB_TYPE 1 SEGMENT SIZE 80 */,
        /// </summary>
        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        private DateTime? birthday;
        /// <summary>
        /// BIRTHDAY              TDATE /* TDATE = DATE */,
        /// </summary>
        public DateTime? Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        private string uniquecitizenid = null;
        /// <summary>
        /// UNIQUECITYZENID       VARCHAR(45),
        /// </summary>
        public string Uniquecitizenid
        {
            get { return uniquecitizenid; }
            set { uniquecitizenid = value; }
        }

        private Document changedocument;
        /// <summary>
        /// CHANGEDOCUMENTCD      INTEGER,
        /// </summary>
        public Document Changedocument
        {
            get { return changedocument; }
            set { changedocument = value; }
        }

        private int? sex;
        /// <summary>
        /// SEX                   INTEGER,
        /// </summary>
        public int? Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        private bool? hidden;
        /// <summary>
        /// HIDDEN                BOOLEAN /* BOOLEAN = SMALLINT CHECK (VALUE IN (0, 1)) */,
        /// </summary>
        public bool? Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }

        private int? ownershipnumerator;
        /// <summary>
        /// OWNERSHIPNUMERATOR    INTEGER,
        /// </summary>
        public int? Ownershipnumerator
        {
            get { return ownershipnumerator; }
            set { ownershipnumerator = value; }
        }

        private int? ownershipdenominator;
        /// <summary>
        /// OWNERSHIPDENOMINATOR  INTEGER,
        /// </summary>
        public int? Ownershipdenominator
        {
            get { return ownershipdenominator; }
            set { ownershipdenominator = value; }
        }

        private int? ownershiptype;
        /// <summary>
        /// OWNERSHIPTYPE         INTEGER DEFAULT 0,
        /// </summary>
        public int? Ownershiptype
        {
            get { return ownershiptype; }
            set { ownershiptype = value; }
        }

        private int? maindoc;
        /// <summary>
        /// MAINDOC               INTEGER,
        /// </summary>
        public int? Maindoc
        {
            get { return maindoc; }
            set { maindoc = value; }
        }

        private string birthcountry = null;
        /// <summary>
        /// BIRTHCOUNTRY          VARCHAR(500),
        /// </summary>
        public string Birthcountry
        {
            get { return birthcountry; }
            set { birthcountry = value; }
        }

        private string birthdistrict = null;
        /// <summary>
        /// BIRTHDISTRICT         VARCHAR(500),
        /// </summary>
        public string Birthdistrict
        {
            get { return birthdistrict; }
            set { birthdistrict = value; }
        }

        private string birthregion = null;
        /// <summary>
        /// BIRTHREGION           VARCHAR(500),
        /// </summary>
        public string Birthregion
        {
            get { return birthregion; }
            set { birthregion = value; }
        }

        private string birthcity = null;
        /// <summary>
        /// BIRTHCITY             VARCHAR(500),
        /// </summary>
        public string Birthcity
        {
            get { return birthcity; }
            set { birthcity = value; }
        }

        private string birthvillage = null;
        /// <summary>
        /// BIRTHVILLAGE          VARCHAR(500),
        /// </summary>
        public string Birthvillage
        {
            get { return birthvillage; }
            set { birthvillage = value; }
        }

        private CitizenState? citizenstateid;
        /// <summary>
        /// CITIZENSTATEID        KEYS /* KEYS = INTEGER */,
        /// </summary>
        public CitizenState? Citizenstateid
        {
            get { return citizenstateid; }
            set { citizenstateid = value; }
        }

        private string passportkod = null;
        /// <summary>
        /// PASSPORTKOD           VARCHAR(10)
        /// </summary>
        public string Passportkod
        {
            get { return passportkod; }
            set { passportkod = value; }
        }
        #endregion

        #region Ссылки на дочерние таблицы
        private List<CitizenAdditionChar> additionalChars = new List<CitizenAdditionChar>();
        /// <summary>
        /// Дополнительные характеристики гражданина
        /// </summary>
        public List<CitizenAdditionChar> AdditionalChars
        {
            get { return additionalChars; }
            set { additionalChars = value; }
        }

        private List<CitizenMigration> migrations = new List<CitizenMigration>();
        /// <summary>
        /// Движения по гражданину
        /// </summary>
        public List<CitizenMigration> Migrations
        {
            get { return migrations; }
            set { migrations = value; }
        }

        private List<CitizenDocuments> citizendocuments = new List<CitizenDocuments>();
        /// <summary>
        /// Документы гражданина
        /// </summary>
        public List<CitizenDocuments> CitizenDocuments
        {
            get { return citizendocuments; }
            set { citizendocuments = value; }
        }
        #endregion

        public string InsertRecord
        {
            get
            {
                string s = "";
                if (Changedocument != null)
                {
                    s += Changedocument.InsertRecord + "\r\n";
                }
                s += String.Format(
                     "INSERT INTO CITYZENS (CITYZEN_ID, LSHET, ISMAINCITYZEN, PASPORTNO, PASPORTSR, PASSPORTDATE, PASSPORTNOTE, " +
                        "CTZFIO, CTZNAME, CTZPARENTNAME, STARTDATE, ENDDATE, NOTE, BIRTHDAY, UNIQUECITYZENID, CHANGEDOCUMENTCD, SEX, " +
                        "HIDDEN, OWNERSHIPNUMERATOR, OWNERSHIPDENOMINATOR, OWNERSHIPTYPE, MAINDOC, BIRTHCOUNTRY, BIRTHDISTRICT, BIRTHREGION, " +
                        "BIRTHCITY, BIRTHVILLAGE, CITIZENSTATEID, PASSPORTKOD) VALUES " +
                          "({0}, {1}, {2}, {3}, {4}, {5}, {6}, " +
                           "{7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, " +
                           "{17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, " +
                           "{25}, {26}, {27}, {28});",
                           this.CitizenID == null ? "GEN_ID(CITYZENS_GEN,1)" : GetValueForInsert(this.citizenID),
                           GetValueForInsert(this.Lshet),
                           GetValueForInsert(this.Ismaincitizen),
                           GetValueForInsert(this.Pasportno),
                           GetValueForInsert(this.Pasportsr),
                           GetValueForInsert(this.Passportdate, DateTimeType.Только_дата),
                           GetValueForInsert(this.Passportnote),
                           GetValueForInsert(this.Ctzfio),
                           GetValueForInsert(this.Ctzname),
                           GetValueForInsert(this.Ctzparentname),
                           GetValueForInsert(this.Startdate, DateTimeType.Только_дата),
                           GetValueForInsert(this.Enddate, DateTimeType.Только_дата),
                           GetValueForInsert(this.Note),
                           GetValueForInsert(this.Birthday, DateTimeType.Только_дата),
                           GetValueForInsert(this.Uniquecitizenid),
                           this.Changedocument == null ? "NULL" : GetValueForInsert(this.Changedocument.Documentcd),
                           GetValueForInsert(this.Sex),
                           GetValueForInsert(this.Hidden),
                           GetValueForInsert(this.Ownershipnumerator),
                           GetValueForInsert(this.Ownershipdenominator),
                           GetValueForInsert(this.Ownershiptype),
                           GetValueForInsert(this.Maindoc),
                           GetValueForInsert(this.Birthcountry),
                           GetValueForInsert(this.Birthdistrict),
                           GetValueForInsert(this.Birthregion),
                           GetValueForInsert(this.Birthcity),
                           GetValueForInsert(this.Birthvillage),
                           GetValueForInsert((int?)this.Citizenstateid),
                           GetValueForInsert(this.Passportkod));

                return s;
            }
        }

        public string Script
        {
            get
            {
                string s = this.InsertRecord;
                foreach (CitizenAdditionChar cac in AdditionalChars)
                {
                    s += "\r\n";
                    s += cac.InsertRecord;
                }

                foreach (CitizenMigration cm in Migrations)
                {
                    s += "\r\n";
                    s += cm.InsertRecord;
                }

                foreach (CitizenDocuments cd in CitizenDocuments)
                {
                    s += "\r\n";
                    s += cd.InsertRecord;
                }

                return s;
            }
        }
    }

    public class CitizenAdditionChar: AbonentCover
    {
        private int citizenid;
        /// <summary>
        /// CITYZEN_ID        IDENTIFER NOT NULL /* IDENTIFER = INTEGER */,
        /// </summary>
        public int Citizenid
        {
            get { return citizenid; }
            set { citizenid = value; }
        }

        private int additionalcharcd;
        /// <summary>
        /// ADDITIONALCHARCD  KEYS2 NOT NULL /* KEYS2 = INTEGER */,
        /// </summary>
        public int Additionalcharcd
        {
            get { return additionalcharcd; }
            set { additionalcharcd = value; }
        }

        private string significance;
        /// <summary>
        /// SIGNIFICANCE      VARCHAR(2000),
        /// </summary>
        public string Significance
        {
            get { return significance; }
            set { significance = value; }
        }

        private Document changedocument;
        /// <summary>
        /// CHANGEDOCUMENTCD  INTEGER
        /// </summary>
        public Document Changedocument
        {
            get { return changedocument; }
            set { changedocument = value; }
        }

        public string InsertRecord
        {
            get
            {
                string s = "";
                if (Changedocument != null)
                {
                    s += Changedocument.InsertRecord + "\r\n";
                }
                s += String.Format("INSERT INTO CITYZENADDITIONCHARS (CITYZEN_ID, ADDITIONALCHARCD, SIGNIFICANCE, " +
                        "CHANGEDOCUMENTCD) VALUES ({0}, {1}, {2}, {3});",
                           GetValueForInsert(this.Citizenid),
                           GetValueForInsert(this.Additionalcharcd),
                           GetValueForInsert(this.Significance),
                           this.Changedocument == null ? "NULL" : GetValueForInsert(this.Changedocument.Documentcd));

                return s;
            }
        }
    }

    public class CitizenMigration: AbonentCover
    {
        private int? migrationid;
        /// <summary>
        /// MIGRATIONID    KEYS NOT NULL /* KEYS = INTEGER */,
        /// </summary>
        public int? Migrationid
        {
            get { return migrationid; }
            set { migrationid = value; }
        }

        private Document document;
        /// <summary>
        /// DOCUMENTCD     IDENTIFER /* IDENTIFER = INTEGER */,
        /// </summary>
        public Document Document
        {
            get { return document; }
            set { document = value; }
        }

        private int citizenid;
        /// <summary>
        /// CITYZEN_ID     IDENTIFER /* IDENTIFER = INTEGER */,
        /// </summary>
        public int Citizenid
        {
            get { return citizenid; }
            set { citizenid = value; }
        }

        private DateTime? migrationdate;
        /// <summary>
        /// MIGRATIONDATE  TDATE /* TDATE = DATE */,
        /// </summary>
        public DateTime? Migrationdate
        {
            get { return migrationdate; }
            set { migrationdate = value; }
        }

        private int? migrationtype;
        /// <summary>
        /// MIGRATIONTYPE  INTEGER,
        /// </summary>
        public int? Migrationtype
        {
            get { return migrationtype; }
            set { migrationtype = value; }
        }

        private MigrationDirection? direction;
        /// <summary>
        /// DIRECTION      INTEGER,
        /// </summary>
        public MigrationDirection? Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private DateTime? regdate;
        /// <summary>
        /// REGDATE        TDATE /* TDATE = DATE */
        /// </summary>
        public DateTime? Regdate
        {
            get { return regdate; }
            set { regdate = value; }
        }

        public string InsertRecord
        {
            get
            {
                string s = "";
                if (Document != null)
                {
                    s += Document.InsertRecord + "\r\n";
                }
                s += String.Format(
                        "INSERT INTO CITYZENMIGRATION (MIGRATIONID, DOCUMENTCD, CITYZEN_ID, MIGRATIONDATE, " +
                        "MIGRATIONTYPE, DIRECTION, REGDATE) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6});",
                           this.Migrationid == null ? "GEN_ID(CITYZENMIGRATION_G, 1)" : GetValueForInsert(this.Migrationid),
                           this.Document == null ? "NULL" : GetValueForInsert(this.Document.Documentcd),
                           GetValueForInsert(this.Citizenid),
                           GetValueForInsert(this.Migrationdate, DateTimeType.Только_дата),
                           GetValueForInsert(this.Migrationtype),
                           GetValueForInsert((int?)this.Direction),
                           GetValueForInsert(this.Regdate, DateTimeType.Только_дата));
                return s;
            }
        }
    }

    public class CitizenDocuments : AbonentCover
    {
        private int citizenid;
        /// <summary>
        /// CITYZEN_ID     IDENTIFER /* IDENTIFER = INTEGER */,
        /// </summary>
        public int Citizenid
        {
            get { return citizenid; }
            set { citizenid = value; }
        }

        private Document document;
        /// <summary>
        /// DOCUMENTCD     IDENTIFER /* IDENTIFER = INTEGER */,
        /// </summary>
        public Document Document
        {
            get { return document; }
            set { document = value; }
        }

        public string InsertRecord
        {
            get
            {
                string s = "";
                if (Document != null)
                {
                    s += Document.InsertRecord + "\r\n";
                }
                s += String.Format(
                        "INSERT INTO CITYZENDOCUMENTS (DOCUMENTCD, CITYZEN_ID) VALUES ({0}, {1});",
                           this.Document == null ? "NULL" : GetValueForInsert(this.Document.Documentcd),
                           GetValueForInsert(this.Citizenid));
                return s;
            }
        }

    }

    public class Relatives: AbonentCover
    {
        private int relative1;
        /// <summary>
        /// RELATIVE1      IDENTIFER NOT NULL /* IDENTIFER = INTEGER */,
        /// </summary>
        public int Relative1
        {
            get { return relative1; }
            set { relative1 = value; }
        }

        private int relative2;
        /// <summary>
        /// RELATIVE2      IDENTIFER NOT NULL /* IDENTIFER = INTEGER */,
        /// </summary>
        public int Relative2
        {
            get { return relative2; }
            set { relative2 = value; }
        }

        private Relation relation;
        /// <summary>
        /// RELATIONID     IDENTIFER /* IDENTIFER = INTEGER */,
        /// </summary>
        public Relation Relation
        {
            get { return relation; }
            set { relation = value; }
        }

        private string otherrelation;
        /// <summary>
        /// OTHERRELATION  VARCHAR(1000)
        /// </summary>
        public string Otherrelation
        {
            get { return otherrelation; }
            set { otherrelation = value; }
        }

        public string InsertRecord
        {
            get
            {
                string s = String.Format(
                        "INSERT INTO RELATIVES (RELATIVE1, RELATIVE2, RELATIONID, OTHERRELATION) " +
                        "VALUES ({0}, {1}, {2}, {3});",
                            GetValueForInsert(this.Relative1),
                            GetValueForInsert(this.Relative2),
                            GetValueForInsert((int)this.Relation),
                            GetValueForInsert(this.Otherrelation));
                return s;
            }
        }
    }

    public enum CitizenState
    {
        Собственник = 1,
        Умерший_собственник = 2,
        Не_собственник = 3,
        Наниматель = 4,
        Умерший_наниматель = 5
    }

    public enum Relation
    {
        Без_родств_отношений	=	0,
        Жена = 1,
        Муж = 2	,
        Отец =	3	,
        Мать =	4	,
        Сын	=	5	,
        Дочь =	6	,
        Брат	=	7	,
        Сестра	=	8	,
        Дедушка	=	9	,
        Бабушка	=	10	,
        Внук	=	11	,
        Внучка	=	12	,
        Бабушка_двоюродная	=	13	,
        Бабушка_жены	=	14	,
        Бабушка_мужа	=	15	,
        Внучка_жены	=	16	,
        Внучка_мужа	=	17	,
        Дочь_жены	=	18	,
        Дочь_мужа	=	19	,
        Жена_брата	=	20	,
        Жена_бывшая	=	21	,
        Жена_внука	=	22	,
        Жена_деда	=	23	,
        Жена_отца	=	24	,
        Жена_племянника	=	25	,
        Жена_сына	=	26	,
        Мать_жены	=	27	,
        Мать_мужа	=	28	,
        Племянник	=	29	,
        Племянница	=	30	,
        Дядя	=	31	,
        Тетя	=	32	,
        Прадедушка	=	33	,
        Прабабушка	=	34	,
        Правнук	=	35	,
        Правнучка	=	36	,
        Брат_двоюродный	=	37	,
        Сестра_двоюродная	=	38	,
        Сестра_жены	=	39	,
        Сестра_мужа	=	40	,
        Тетя_жены	=	41	,
        Тетя_мужа	=	42	,
        Дядя_мужа	=	43	,
        Дядя_жены	=	44	,
        Дедушка_двоюродный	=	45	,
        Дедушка_жены	=	46	,
        Дедушка_мужа	=	47	,
        Внук_жены	=	48	,
        Внук_мужа	=	49	,
        Муж_сестры	=	50	,
        Муж_бывший	=	51	,
        Муж_внучки	=	52	,
        Муж_бабушки	=	53	,
        Муж_матери	=	54	,
        Муж_племянницы	=	55	,
        Муж_дочери	=	56	,
        Отец_жены	=	57	,
        Отец_мужа	=	58	
    }

    public enum MigrationDirection
    {
        Прибытие = 1,
        Выбытие = 2
    }

}
