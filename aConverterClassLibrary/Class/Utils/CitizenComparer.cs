using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using aConverterClassLibrary.RecordsDataAccessORM;

namespace aConverterClassLibrary.Class.Utils
{
    /// <summary>
    /// Позволяет првоести сопоставление граждан с БД на основе составного ключа (ФИО + дата рождения)
    /// </summary>
    public class CitizenComparer
    {
        private string _connectionString;

        public CitizenComparer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public const string GetNewUniqueIdSql =
            "select iif (VARIABLEVALUE is null, " +
            "'00000'||'C'||hash(gen_uuid()), " +
            "VARIABLEVALUE||'C'||hash(gen_uuid())) " +
            "from systemvariables " +
            "where VARIABLENAME = 'DATABASE_IDENTIFER';";

        private const string GetCitizensSql =
            "select c.lshet, c.ctzfio, c.ctzname, c.ctzparentname, c.birthday, c.uniquecityzenid, c.cityzen_id " +
            "from cityzens c " +
            "where c.uniquecityzenid is not null " +
            "order by c.hidden asc;";

        private Dictionary<CompareKey, CitizenIdToCompare[]> _citizens;

        public void LoadCitizens()
        {
            _citizens = new Dictionary<CompareKey, CitizenIdToCompare[]>();
            var fbManager = new FbManager(_connectionString);
            var sqlResult = new List<GetCitizensSqlResult>();
            fbManager.ExecuteQueryByRow(GetCitizensSql, dr =>
            {
                sqlResult.Add(new GetCitizensSqlResult
                {
                    Lshet = dr["lshet"].ToString(),
                    F = dr.IsNull("ctzfio") ? "" : dr["ctzfio"].ToString(),
                    I = dr.IsNull("ctzname") ? "" : dr["ctzname"].ToString(),
                    O = dr.IsNull("ctzparentname") ? "" : dr["ctzparentname"].ToString(),
                    BirthDate = dr.IsNull("birthday") ? DateTime.MinValue : Convert.ToDateTime(dr["birthday"]),
                    UniqueId = dr["uniquecityzenid"].ToString(),
                    Citizenid = Convert.ToInt32(dr["cityzen_id"])
                });
            });
            _citizens = sqlResult
                .GroupBy(c => new CompareKey(c.Lshet, GetCompareString(c.F, c.I, c.O, c.BirthDate)))
                .ToDictionary(gc => gc.Key, gc =>
                    gc.Select(c => new CitizenIdToCompare(c.Citizenid, c.UniqueId)).ToArray());
        }

        private CitizenIdToCompare GetCitizen(CompareKey compareKey)
        {
            if (_citizens == null) LoadCitizens();
            CitizenIdToCompare[] citizen;
            if (_citizens.TryGetValue(compareKey, out citizen))
            {
                var notAlreadyComparedCitizen = citizen.FirstOrDefault(c => !c.WasCompared);
                if (notAlreadyComparedCitizen != null)
                {
                    notAlreadyComparedCitizen.WasCompared = true;
                    return notAlreadyComparedCitizen;
                }
            }
            return null;
        }

        public string GetCitizenUniqueId(CompareKey compareKey, bool withNewId = false)
        {
            var citizen = GetCitizen(compareKey);
            return citizen != null
                ? citizen.UniqueId
                : withNewId
                    ? GetNewUniqueId()
                    : null;
        }

        public int? GetCitizenId(CompareKey compareKey)
        {
            var citizen = GetCitizen(compareKey);
            return citizen?.CitizenId;
        }

        private static string GetFioForCompare(string fio)
        {
            return String.IsNullOrWhiteSpace(fio) ? "" : fio.Replace(" ", "").ToLower();
        }

        private static string GetFioForCompare(string f, string i, string o)
        {
            return GetFioForCompare((f ?? "") + (i ?? "") + (o ?? ""));
        }

        private static string GetDateForComare(DateTime? date)
        {
            return (date ?? DateTime.MinValue).ToString("dd.MM.yyyy");
        }

        private static string GetHashString(string input)
        {
            return aConverterClassLibrary.Utils.GetMD5Hash(input).ToLower();
        }

        public static string GetCompareString(string fio, DateTime? birthDate)
        {
            return GetHashString(GetFioForCompare(fio) + GetDateForComare(birthDate));
        }

        public static string GetCompareString(string f, string i, string o, DateTime? birthDate)
        {
            return GetHashString(GetFioForCompare(f, i, o) + GetDateForComare(birthDate));
        }

        public static string GetNewUniqueId()
        {
            return new FbManager(aConverter_RootSettings.FirebirdStringConnection)
                .ExecuteScalar(GetNewUniqueIdSql)
                .ToString();
        }


        private class GetCitizensSqlResult
        {
            public string Lshet;
            public string F;
            public string I;
            public string O;
            public DateTime? BirthDate;
            public string UniqueId;
            public int Citizenid;
        }

        private class CitizenIdToCompare
        {
            public readonly int CitizenId;
            public readonly string UniqueId;

            public bool WasCompared { get; set; }

            public CitizenIdToCompare(int citizenId, string uniqueId)
            {
                CitizenId = citizenId;
                UniqueId = uniqueId;
                WasCompared = false;
            }
        }
    }

    public struct CompareKey
    {
        public readonly string Lshet;
        public readonly string CompareString;

        public CompareKey(string lshet, string compareString)
        {
            Lshet = lshet;
            CompareString = compareString;
        }

        public CompareKey(string lshet, string fio, DateTime? birthDate)
        {
            Lshet = lshet;
            CompareString = CitizenComparer.GetCompareString(fio, birthDate);
        }

        public CompareKey(string lshet, string f, string i, string o, DateTime? birthDate)
        {
            Lshet = lshet;
            CompareString = CitizenComparer.GetCompareString(f, i, o, birthDate);
        }

        public CompareKey(CNV_CITIZEN citizen)
        {
            Lshet = citizen.LSHET;
            CompareString = CitizenComparer.GetCompareString(citizen.F, citizen.I, citizen.O, citizen.BIRTHDATE);
        }
    }
}
