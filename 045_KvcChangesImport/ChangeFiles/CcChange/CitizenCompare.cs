using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using aConverterClassLibrary;
using aConverterClassLibrary.Class;
using aConverterClassLibrary.RecordsDataAccessORM;

namespace _045_KvcChangesImport.ChangeFiles.CcChange
{
    public struct CitizenCompare
    {
        public string Lshet;
        public string Fio;
        public DateTime BirthDate;

        public CitizenCompare(CNV_CITIZEN cnvCitizen)
        {
            Lshet = cnvCitizen.LSHET;
            Fio = GetFioForCompare((cnvCitizen.F ?? "") + (cnvCitizen.I ?? "") + (cnvCitizen.O ?? ""));
            BirthDate = cnvCitizen.BIRTHDATE ?? DateTime.MinValue;
        }

        public static string GetNewUniqueId()
        {
            var fbManager = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            return fbManager.ExecuteScalar(
@"select iif (VARIABLEVALUE is null,
    '00000'||'C'||hash(gen_uuid()),
    VARIABLEVALUE||'C'||hash(gen_uuid()))
from systemvariables 
where VARIABLENAME='DATABASE_IDENTIFER'")
                .ToString();
        }

        public static string GetFioForCompare(string fio)
        {
            return fio.Replace(" ", "").Trim().ToLower();
        }

        public class CitizenIdForComare
        {
            public string UniqueId;
            public bool WasCompared;

            public CitizenIdForComare(string uniqueId)
            {
                UniqueId = uniqueId;
                WasCompared = false;
            }
        }

        public class CityzenCompareOrm
        {
            public CitizenCompare Compare;
            public string UniqueId;
        }

        private static Dictionary<CitizenCompare, CitizenIdForComare[]> _citizenUniqueId;

        public static Dictionary<CitizenCompare, CitizenIdForComare[]> CitizenUniqueId
        {
            get
            {
                if (_citizenUniqueId == null) LoadCitizenUniqueId();
                return _citizenUniqueId;
            }
        }


        public static void LoadCitizenUniqueId()
        {
            _citizenUniqueId = new Dictionary<CitizenCompare, CitizenIdForComare[]>();
            var fbManager = new FbManager(aConverter_RootSettings.FirebirdStringConnection);
            using (var dt = fbManager.ExecuteQuery(GetCitizensUniqueIdSql))
            {
                var tempCitizens = new CityzenCompareOrm[dt.Rows.Count];
                for (int i = 0; i < tempCitizens.Length; i++)
                {
                    var dr = dt.Rows[i];
                    tempCitizens[i] = new CityzenCompareOrm
                    {
                        Compare = new CitizenCompare
                        {
                            Lshet = dr[0].ToString(),
                            Fio = GetFioForCompare((dr.IsNull(1) ? "" : dr[1].ToString()) + (dr.IsNull(2) ? "" : dr[2].ToString()) + (dr.IsNull(3) ? "" : dr[3].ToString())),
                            BirthDate = dr.IsNull(4) ? DateTime.MinValue : Convert.ToDateTime(dr[4]),
                        },
                        UniqueId = dr[5].ToString()
                    };
                }
                _citizenUniqueId = tempCitizens
                    .GroupBy(c => c.Compare, c => c.UniqueId)
                    .ToDictionary(gc => gc.Key, gc =>
                        gc.Select(uid => new CitizenIdForComare(uid)).ToArray());
            }
        }

        public static string GetCitizenId(CNV_CITIZEN compare)
        {
            CitizenIdForComare[] uniqueId;
            if (CitizenUniqueId.TryGetValue(new CitizenCompare(compare), out uniqueId))
            {
                var notComparedId = uniqueId.FirstOrDefault(uid => !uid.WasCompared);
                if (notComparedId != null)
                {
                    notComparedId.WasCompared = true;
                    return notComparedId.UniqueId;
                }
            }
            return GetNewUniqueId();
        }

        public const string GetCitizensUniqueIdSql =
@"select c.lshet, c.ctzfio, c.ctzname, c.ctzparentname, c.birthday, c.uniquecityzenid
from cityzens c
where c.hidden <> 1 and c.uniquecityzenid is not null";
    }
}
