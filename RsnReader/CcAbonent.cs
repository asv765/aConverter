using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RsnReader
{
    public class CcAbonent
    {
        public byte СостояниеЛс;
        public ushort ХозяинЛс;
        public LsKvc LsKvc;
        public List<Citizen> Жители = new List<Citizen>();
        public ushort FileYear;
        public ushort FileMonth;

        public CcAbonent() { }

        public CcAbonent(List<byte[]> bytes, CcFileInfo ccFileInfo)
        {
            FileYear = ccFileInfo.РасчетныйГод;
            FileMonth = ccFileInfo.РасчетныйМесяц;
            var octet = bytes[0];
            uint Адр1 = octet.ToUInt32();
            octet = bytes[1];
            СостояниеЛс = octet[1];
            ХозяинЛс = octet.ToUInt16();
            uint Адр2 = octet.ToUInt32();
            LsKvc = new LsKvc(Адр1, Адр2);
            if (bytes.Count < 4) return;
            var citizenBytes = new List<byte[]>() { bytes[3]};
            for(int i = 4; i < bytes.Count; i++)
            {
                octet = bytes[i];
                if (octet[0] == 171)
                {
                    Жители.Add(new Citizen(citizenBytes, ccFileInfo, LsKvc));
                    citizenBytes = new List<byte[]> { octet };
                    continue;
                }
                citizenBytes.Add(octet);
            }
            Жители.Add(new Citizen(citizenBytes, ccFileInfo, LsKvc));
        }

        public CcAbonent(List<DataRow> rows, ushort fileYear, ushort fileMonth)
        {
            СостояниеЛс = 0;
            ХозяинЛс = 0;
            FileYear = fileYear;
            FileMonth = fileMonth;
            if (rows == null || rows.Count == 0) return;
            LsKvc = new LsKvc(Convert.ToUInt64(rows[0][0]));
            var fileDate = new DateTime(fileYear, fileMonth, 1);
            var dontChangeDate = new DateTime(1900, 01, 01);
            for (int i = 0; i < rows.Count; i++)
            {
                var citizen = new Citizen(rows[i], LsKvc);
                switch (citizen.СтатусРегистрации)
                {
                    case 0:
                        citizen.ДатаРегистрации = dontChangeDate;
                        citizen.ДатаСнятияСРегистрации = fileDate;
                        citizen.ДатаОкончанияВремРегистрации = null;
                        citizen.ДатаОкончанияВыбытия = null;
                        break;
                    case 1:
                        citizen.ДатаРегистрации = fileDate;
                        citizen.ДатаСнятияСРегистрации = null;
                        citizen.ДатаОкончанияВремРегистрации = null;
                        citizen.ДатаОкончанияВыбытия = null;
                        break;
                    case 2:
                        citizen.ДатаРегистрации = fileDate;
                        citizen.ДатаСнятияСРегистрации = null;
                        citizen.ДатаОкончанияВыбытия = null;
                        break;
                    case 3:
                        citizen.ДатаРегистрации = null;
                        citizen.ДатаСнятияСРегистрации = null;
                        citizen.ДатаОкончанияВремРегистрации = null;
                        break;
                }
                Жители.Add(citizen);
            }
        }

        public class Citizen
        {
            /// <summary>
            /// Ключ
            /// </summary>
            public byte НомерЖителя;
            /// <summary>
            /// 0 - прочие. 1 - отв. наниматель или собственник
            /// </summary>
            public byte ПризнакОтветственного;
            /// <summary>
            /// Отношение к собственности
            /// <para>0 - не собственник. 1 - собственник. 2 - умерший собственник. 3 - наниматель по договору соц. найма. 4 - умерший наниматель. 5 - наниматель по договору найма</para> 
            /// </summary>
            public byte СтатусСобственности;
            /// <summary>
            /// Отношение к регистрации. 
            /// <para>0 - не зарегистрирован. 1 - проживающий зарегестрированный член семьи. 2 - прочие зарегестрированные. 3 - временно выбывший член семьи. 9 - признак удаления</para>
            /// </summary>
            public byte СтатусРегистрации;
            /// <summary>
            /// Для статуса регистрации = 3. Окончание временного выбытия
            /// </summary>
            public DateTime? ДатаОкончанияВыбытия;
            /// <summary>
            /// Для статуса регистрации = 2. Окончание временной регистрации
            /// </summary>
            public DateTime? ДатаОкончанияВремРегистрации;

            public byte Пол;
            public DateTime? ДатаРождения;
            public string ДопИнформация;
            public string ФИО;
            public DateTime? ДатаРегистрации;

            /// <summary>
            /// 1 - паспорт. 2 - свидетельство о рождении
            /// </summary>
            public byte КодДокумента;
            public DateTime? ДатаВыдачиДокумента;
            public uint НомерПаспорта;
            public string СерияДокумента;
            public string КемВыданДокумент;

            /// <summary>
            /// spr0.68
            /// </summary>
            public byte ПричинаВыписки;
            public DateTime? ДатаСмерти;
            public DateTime? ДатаСнятияСРегистрации;

            /// <summary>
            /// Страна;область;район;населенный пункт
            /// </summary>
            public string МестоРождения;
            public string Гражданство;
            
            public string ДоляПлощади;

            public string НомерЕГРП;
            public DateTime? ДатаВыдачиЕГРП;

            public DateTime? ДатаОкончанияСобственности;

            public List<Relation> РодственныеСвязи;
            public LsKvc LsKvc;

            public Citizen() { }

            public Citizen(List<byte[]> bytes, CcFileInfo ccFileInfo, LsKvc lsKvc)
            {
                LsKvc = lsKvc;
                ФИО = "";
                ДопИнформация = "";
                СерияДокумента = "";
                КемВыданДокумент = "";
                МестоРождения = "";
                Гражданство = "";
                ДоляПлощади = "";
                НомерЕГРП = "";
                РодственныеСвязи = new List<Relation>();
                byte citRelId = 0;
                var octetCount = bytes.Count;
                for(int i = 0; i < octetCount; i++)
                {
                    var octet = bytes[i];
                    switch (octet[0])
                    {
                        case 171:
                            НомерЖителя = octet[1];
                            ПризнакОтветственного = octet[2];
                            string status = $"{octet[3]:D2}";
                            СтатусСобственности = Convert.ToByte(status.Substring(0, 1));
                            СтатусРегистрации = Convert.ToByte(status.Substring(1, 1));
                            if (octet.IsEmpty(4, 5)) break;
                            try
                            {
                                if (СтатусРегистрации == 2)
                                {
                                    ushort year, month;
                                    octet.ParseShortDate(out year, out month, 4);
                                    ДатаОкончанияВремРегистрации = new DateTime(year, month, 1);
                                }
                                else if (СтатусРегистрации == 3)
                                {
                                    ushort year, month;
                                    octet.ParseShortDate(out year, out month, 4);
                                    ДатаОкончанияВыбытия = new DateTime(year, month, 1);
                                }
                            }
                            catch
                            {
                                
                            }
                            break;
                        case 172:
                            Пол = octet[1];
                            if (!octet.IsEmpty(4)) ДатаРождения = octet.ToDateTime();
                            break;
                        case 173:
                            ДопИнформация += ccFileInfo.Encoding.GetString(octet, 2, 6);
                            break;
                        case 175:
                            ФИО += ccFileInfo.Encoding.GetString(octet, 2, 6);
                            break;
                        case 176:
                            КодДокумента = octet[1];
                            if (!octet.IsEmpty(4)) ДатаВыдачиДокумента = octet.ToDateTime();
                            break;
                        case 177:
                            НомерПаспорта = octet.ToUInt32();
                            break;
                        case 178:
                            СерияДокумента += ccFileInfo.Encoding.GetString(octet, 2, 6);
                            break;
                        case 179:
                            КемВыданДокумент += ccFileInfo.Encoding.GetString(octet, 2, 6);
                            break;
                        case 180:
                            for (int j = 2; j <= 7; j++)
                            {
                                citRelId++;
                                var rel = octet[j];
                                if (rel > 0) РодственныеСвязи.Add(new Relation { НомерЖителя = citRelId, КодСвязи = rel });
                            }
                            break;
                        case 181:
                            if (!octet.IsEmpty(4)) ДатаРегистрации = octet.ToDateTime();
                            break;
                        case 182:
                            ПричинаВыписки = octet[1];
                            if (!octet.IsEmpty(2, 3)) ДатаСмерти = new DateTime(2004, 01, 14).AddDays(octet.ToUInt16());
                            if (!octet.IsEmpty(4)) ДатаСнятияСРегистрации = octet.ToDateTime();
                            break;
                        case 183:
                            МестоРождения += ccFileInfo.Encoding.GetString(octet, 2, 6);
                            break;
                        case 184:
                            Гражданство += ccFileInfo.Encoding.GetString(octet, 2, 6);
                            break;
                        case 185:
                            ДоляПлощади += ccFileInfo.Encoding.GetString(octet, 2, 6);
                            break;
                        case 186:
                            НомерЕГРП += ccFileInfo.Encoding.GetString(octet, 2, 6);
                            break;
                        case 187:
                            if (!octet.IsEmpty(4)) ДатаВыдачиЕГРП = octet.ToDateTime();
                            break;
                        default:
                            break;
                    }
                }
                ДопИнформация = ДопИнформация == "" ? null : ДопИнформация.Replace("\0", "").Trim();
                ФИО = ФИО == "" ? null : ФИО.Replace("\0", "").Trim();
                СерияДокумента = СерияДокумента == "" ? null : СерияДокумента.Replace("\0", "").Trim();
                КемВыданДокумент = КемВыданДокумент == "" ? null : КемВыданДокумент.Replace("\0", "").Trim();
                МестоРождения = МестоРождения == "" ? null : МестоРождения.Replace("\0", "").Trim();
                Гражданство = Гражданство == "" ? null : Гражданство.Replace("\0", "").Trim();
                ДоляПлощади = ДоляПлощади == "" ? null : ДоляПлощади.Replace("\0", "").Trim();
                НомерЕГРП = НомерЕГРП == "" ? null : НомерЕГРП.Replace("\0", "").Trim();
            }

            public Citizen(DataRow row, LsKvc lsKvc)
            {
                LsKvc = lsKvc;
                ФИО = "";
                ДопИнформация = "";
                СерияДокумента = "";
                КемВыданДокумент = "";
                МестоРождения = "";
                Гражданство = "";
                ДоляПлощади = "";
                НомерЕГРП = "";
                РодственныеСвязи = new List<Relation>();

                НомерЖителя = Convert.ToByte(row[1]);
                ПризнакОтветственного = Convert.ToByte(row[2]);
                СтатусСобственности = Convert.ToByte(row[3]);
                СтатусРегистрации = Convert.ToByte(row[4]);
                if (!row.IsNull(5))
                {
                    if (СтатусРегистрации == 2)
                    {
                        ushort year, month;
                        row.ParseShortDate(out year, out month, 5);
                        ДатаОкончанияВремРегистрации = new DateTime(year, month, 1);
                    }
                    else if (СтатусРегистрации == 3)
                    {
                        ushort year, month;
                        row.ParseShortDate(out year, out month, 5);
                        ДатаОкончанияВыбытия = new DateTime(year, month, 1);
                    }
                }
                ФИО = $"{row[6]} {row[7]} {row[8]}".Trim();
                if (!row.IsNull(9)) Пол = Convert.ToByte(row[9]);
                if (!row.IsNull(11) && row[11].ToString() != "0") ДатаРождения = row.ToDateTime(11);
                if (!row.IsNull(12) && row[12].ToString() != "0") ДатаРегистрации = row.ToDateTime(12);
                if (!row.IsNull(13) && row[13].ToString() != "0")
                    if (СтатусРегистрации != 3)   // В выгрузке от ресурсников в эту дату проставлялось последнее выбытие (даже временное)
                        ДатаСнятияСРегистрации = row.ToDateTime(13);
                if (!row.IsNull(14)) ПричинаВыписки = Convert.ToByte(row[14]);
                if (!row.IsNull(15)) КодДокумента = Convert.ToByte(row[15]);
                if (!row.IsNull(16)) НомерПаспорта = Convert.ToUInt32(row[16]);
                if (!row.IsNull(17)) СерияДокумента = row[17].ToString();
                if (!row.IsNull(18)) КемВыданДокумент = row[18].ToString();
                if (!row.IsNull(19)) Гражданство = row[19].ToString();
                МестоРождения = $"{row[20]};{row[21]};{row[22]};{row[23]}";
                ДоляПлощади = row[24].ToString();

                ФИО = ФИО == "" ? null : ФИО.Replace("\0", "").Trim();
                СерияДокумента = СерияДокумента == ""
                    ? null
                    : СерияДокумента.Replace("\0", "").Trim();
                КемВыданДокумент = КемВыданДокумент == ""
                    ? null
                    : КемВыданДокумент.Replace("\0", "").Trim();
                МестоРождения = МестоРождения == "" ? null : МестоРождения.Replace("\0", "").Trim();
                Гражданство = Гражданство == "" ? null : Гражданство.Replace("\0", "").Trim();
                ДоляПлощади = ДоляПлощади == "" ? null : ДоляПлощади.Replace("\0", "").Trim();
            }

            private static Dictionary<string, byte> _currentCitizenNumbers = new Dictionary<string, byte>();

            public static Citizen CreateFromOdatXls(DataRow row, LsKvc lsKvc)
            {
                var citizen = new Citizen
                {
                    LsKvc = lsKvc,
                    ФИО = "",
                    ДопИнформация = "",
                    СерияДокумента = "",
                    КемВыданДокумент = "",
                    МестоРождения = "",
                    Гражданство = "",
                    ДоляПлощади = "",
                    НомерЕГРП = "",
                    РодственныеСвязи = new List<Relation>()
                };

                if (_currentCitizenNumbers.ContainsKey(lsKvc.Ls))
                    _currentCitizenNumbers[lsKvc.Ls]++;
                else
                    _currentCitizenNumbers.Add(lsKvc.Ls, 1);

                citizen.НомерЖителя = _currentCitizenNumbers[lsKvc.Ls];
                citizen.ПризнакОтветственного = DataRowStringToCheck(row[(int)OdantExcelFields.IsMain]) == "да" ? (byte)1 : (byte)0;
                switch (DataRowStringToCheck(row[(int)OdantExcelFields.OwnershipType]))
                {
                    case "не собств.":
                        citizen.СтатусСобственности = 0;
                        break;
                    case "собственник":
                        citizen.СтатусСобственности = 1;
                        break;
                    case "наниматель":
                    case "наниматель по договору найма":
                        citizen.СтатусСобственности = 5;
                        break;
                    case "наниматель по договору соц.найма":
                        citizen.СтатусСобственности = 3;
                        break;
                    case "ум.собств.":
                        citizen.СтатусСобственности = 2;
                        break;
                    case "ум.наним.":
                        citizen.СтатусСобственности = 4;
                        break;
                    case "ум. не собственник":
                        citizen.СтатусСобственности = 7;
                        break;
                    default:
                        citizen.СтатусСобственности = 0;
                        if (!String.IsNullOrWhiteSpace(DataRowStringToCheck(row[(int)OdantExcelFields.OwnershipType])))
                            Task.Factory.StartNew(() => MessageBox.Show($"Необработанный вид собственности: {row[(int)OdantExcelFields.OwnershipType]}"));
                        break;
                }
                if (!String.IsNullOrWhiteSpace(row[(int)OdantExcelFields.TempRegEndDate]?.ToString()))
                {
                    ushort year, month;
                    RsnHelper.ParseShortDate(Convert.ToUInt16(row[(int)OdantExcelFields.TempRegEndDate]), out year, out month);
                    citizen.ДатаОкончанияВремРегистрации = new DateTime(year, month, 1);
                }
                if (!String.IsNullOrWhiteSpace(row[(int)OdantExcelFields.RegDate]?.ToString())) citizen.ДатаРегистрации = Convert.ToDateTime(row[(int)OdantExcelFields.RegDate]);
                //if (DataRowStringToCheck(row[8]) == "да") citizen.СтатусРегистрации = 3;
                //else
                //{
                switch (DataRowStringToCheck(row[(int)OdantExcelFields.RegType]))
                {
                    case "не зарегистрирован":
                        citizen.СтатусРегистрации = 0;
                        break;
                    case "по месту жительства":
                        citizen.СтатусРегистрации = 1;
                        break;
                    case "по месту пребывания":
                        citizen.СтатусРегистрации = 2;
                        break;
                    default:
                        citizen.СтатусРегистрации = 0;
                        if (!String.IsNullOrWhiteSpace(DataRowStringToCheck(row[(int)OdantExcelFields.RegType])))
                            Task.Factory.StartNew(() => MessageBox.Show($"Необработанный вид регистрации: {row[(int)OdantExcelFields.RegType]}"));
                        break;
                }
                //}

                //if (citizen.СтатусРегистрации == 3)
                //{
                //    ushort year, month;
                //    RsnHelper.ParseShortDate(Convert.ToUInt16(row[9]), out year, out month);
                //    citizen.ДатаОкончанияВыбытия = new DateTime(year, month, 1);
                //}

                citizen.ФИО = row[(int)OdantExcelFields.Fio].ToString();
                if (!IsEmptyExcelCell(row[(int)OdantExcelFields.OwnershipPart])) citizen.ДоляПлощади = row[(int)OdantExcelFields.OwnershipPart].ToString();
                if (DataRowStringToCheck(row[(int)OdantExcelFields.Sex]) == "м") citizen.Пол = 1;
                else if (DataRowStringToCheck(row[(int)OdantExcelFields.Sex]) == "ж") citizen.Пол = 2;
                if (!String.IsNullOrWhiteSpace(row[(int)OdantExcelFields.BirthDate]?.ToString())) citizen.ДатаРождения = Convert.ToDateTime(row[(int)OdantExcelFields.BirthDate]);
                if (!IsEmptyExcelCell(row[(int)OdantExcelFields.DocSeries]))
                {
                    citizen.КодДокумента = 1;
                    citizen.СерияДокумента = row[(int)OdantExcelFields.DocSeries].ToString();
                    if (!IsEmptyExcelCell(row[(int)OdantExcelFields.DocNumber])) citizen.НомерПаспорта = Convert.ToUInt32(row[(int)OdantExcelFields.DocNumber]);
                    if (!IsEmptyExcelCell(row[(int)OdantExcelFields.DocDate])) citizen.ДатаВыдачиДокумента = Convert.ToDateTime(row[(int)OdantExcelFields.DocDate]);
                    if (!IsEmptyExcelCell(row[(int)OdantExcelFields.WhoGaveDoc])) citizen.КемВыданДокумент = row[(int)OdantExcelFields.WhoGaveDoc].ToString();
                }
                else if (!IsEmptyExcelCell(row[(int)OdantExcelFields.CertificateSeries]))
                {
                    citizen.КодДокумента = 2;
                    citizen.СерияДокумента = row[(int)OdantExcelFields.CertificateSeries].ToString();
                    if (!IsEmptyExcelCell(row[(int)OdantExcelFields.CertificateNumber])) citizen.НомерПаспорта = Convert.ToUInt32(row[(int)OdantExcelFields.CertificateNumber]);
                    if (!IsEmptyExcelCell(row[(int)OdantExcelFields.CertificateDate])) citizen.ДатаВыдачиДокумента = Convert.ToDateTime(row[(int)OdantExcelFields.CertificateDate]);
                }
                if (!IsEmptyExcelCell(row[(int)OdantExcelFields.Citizenship])) citizen.Гражданство = row[(int)OdantExcelFields.Citizenship].ToString();
                if (!IsEmptyExcelCell(row[(int)OdantExcelFields.Egrp])) citizen.НомерЕГРП = row[(int)OdantExcelFields.Egrp].ToString();
                if (!IsEmptyExcelCell(row[(int)OdantExcelFields.EgrpDate])) citizen.ДатаВыдачиЕГРП = Convert.ToDateTime(row[(int)OdantExcelFields.EgrpDate]);
                if (!IsEmptyExcelCell(row[(int)OdantExcelFields.BirthPlace])) citizen.МестоРождения = row[(int)OdantExcelFields.BirthPlace].ToString();

                if (!IsEmptyExcelCell(row[(int)OdantExcelFields.DeleteDate])) citizen.ДатаСнятияСРегистрации = Convert.ToDateTime(row[(int)OdantExcelFields.DeleteDate]);
                if (DataRowStringToCheck(row[(int) OdantExcelFields.IsDeleted]) == "да") citizen.СтатусРегистрации = 9;
                if (!IsEmptyExcelCell(row[(int)OdantExcelFields.OwnerDeleteDate])) citizen.ДатаОкончанияСобственности = Convert.ToDateTime(row[(int)OdantExcelFields.OwnerDeleteDate]);

                return citizen;
            }

            private static string DataRowStringToCheck(object obj)
            {
                return obj.ToString().Trim().ToLower();
            }

            private static bool IsEmptyExcelCell(object obj)
            {
                return String.IsNullOrWhiteSpace(obj?.ToString());
            }

            private enum OdantExcelFields
            {
                Address = 0,
                Ls,
                Fio,
                BirthDate,
                OwnershipPart,
                IsMain,
                Sex,
                RegDate,
                OwnerDeleteDate,
                DeleteDate,
                IsDeleted,
                OwnershipType,
                DocSeries,
                DocNumber,
                DocDate,
                WhoGaveDoc,
                SubdivisionId,
                CertificateSeries,
                CertificateNumber,
                CertificateDate,
                BirthPlace,
                Citizenship,
                RegType,
                Egrp,
                EgrpDate,
                TempRegEndDate
            }
        }

        public class Relation
        {
            public byte НомерЖителя;
            public byte КодСвязи;
        }
    }

    public class CcFileInfo
    {
        public string FileName;
        public byte СостояниеМассива;
        public ushort РасчетныйГод;
        public ushort РасчетныйМесяц;
        public uint КоличествоЛс;

        public Encoding Encoding = Encoding.GetEncoding(1251);

        public CcFileInfo(string fileName)
        {
            FileName = fileName;
            using (BinaryReader reader = new BinaryReader(File.OpenRead(fileName), Encoding))
            {
                var octet = reader.ReadBytes(8);
                СостояниеМассива = octet[1];
                octet.ParseShortDate(out РасчетныйГод, out РасчетныйМесяц);
                КоличествоЛс = octet.ToUInt32();
            }
        }
    }
}
