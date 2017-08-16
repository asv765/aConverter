using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RsnReader;

namespace aConverterClassLibrary.Class.ConvertCases
{
    public abstract class KvcConvertCase : ConvertCase
    {
        public static Dictionary<string, long> LsMap;
        public static long FirstNotExistedLs;

        public int TotalConvertIterationCount;
        public int CurrentIteration;
        public long IterationMinLs;
        public long IterationMaxLs;

        public DateTime CurrnetConvertDate;

        public void RecalcIterationBorders()
        {
            if (TotalConvertIterationCount == 0)
            {
                IterationMinLs = 0;
                IterationMaxLs = long.MaxValue;
            }
            else
            {
                const long lsPrefix = 99000000;
                int lsCountInItertaion = LsMap.Count / TotalConvertIterationCount;
                if (CurrentIteration == 0)
                {
                    IterationMinLs = 0;
                    IterationMaxLs = lsCountInItertaion + lsPrefix;
                }
                else if (CurrentIteration == TotalConvertIterationCount - 1)
                {
                    IterationMinLs = lsCountInItertaion * CurrentIteration + lsPrefix - 1;
                    IterationMaxLs = FirstNotExistedLs;
                }
                else if (CurrentIteration == TotalConvertIterationCount)
                {
                    IterationMinLs = FirstNotExistedLs - 1;
                    IterationMaxLs = long.MaxValue;
                }
                else
                {
                    IterationMinLs = lsCountInItertaion * CurrentIteration - 1 + lsPrefix;
                    IterationMaxLs = lsCountInItertaion * (CurrentIteration + 1) + lsPrefix;
                }
            }
        }

        public static List<string> NotFoundedLs = new List<string>();

        public bool ShouldConvertInIteration(LsKvc lsKvc)
        {
            long ls;
            if (!LsMap.TryGetValue(lsKvc.Ls, out ls))
            {
                NotFoundedLs.Add(lsKvc.Ls);
                return false;
            }
            //long ls = LsMap[lsKvc.Ls];
            return IterationMinLs < ls && ls < IterationMaxLs;
        }

        public bool ShouldConvert(LsKvc lsKvc)
        {
            //return lsKvc.StreetId == 226 && (lsKvc.HouseId == 13 || lsKvc.HouseId == 14 || lsKvc.HouseId == 15);
            //return lsKvc.Ls == "067-016-03-016-0-98";
            //return lsKvc.Ls == "001-001-01-001-0-33" || lsKvc.Ls == "212-022-11-002-0-04";
            //return lsKvc.StreetId == 226;
            //return lsKvc.StreetId == 110 && lsKvc.HouseId == 16;
            return true;
        }

        public virtual void ActionBeforeConvert() { }
        public virtual void ActionAfterConvert() { }

        public override void DoConvert()
        {
            if (TotalConvertIterationCount == 0)
            {
                ActionBeforeConvert();
                RecalcIterationBorders();
                DoKvcConvert();
                ActionAfterConvert();
            }
            else
            {
                ActionBeforeConvert();
                for(CurrentIteration = 0; CurrentIteration <= TotalConvertIterationCount; CurrentIteration++)
                {
                    RecalcIterationBorders();
                    DoKvcConvert();
                }
                ActionAfterConvert();
            }
        }

        public abstract void DoKvcConvert();

        public void ConvertRsnAbonent(Action<RsnAbonent> convertAction, int year, int month, bool countStpes = true, bool rsn3 = false)
        {
            string fileName = ReadRsnForm.RsnFilePath + @"\rsn" + (rsn3 ? "3" : "0") + RsnHelper.GetShortDate(year, month);
            var rsnFile = new RsnFile(fileName);
            CurrnetConvertDate = new DateTime(year, month, 1);

            if (countStpes) StepStart((int)rsnFile.КоличествоЛС);
            using (BinaryReader reader = new BinaryReader(File.OpenRead(fileName), Encoding.GetEncoding(1251)))
            {
                reader.ReadBytes(8); // пропуск первой служебной 001
                var bytes = new List<byte[]>();
                bytes.Add(reader.ReadBytes(8));
                if (bytes[0][0] != 010) throw new Exception($"Вторая октета в файле не имеет тип 010 ({bytes[0][0]})");
                long lastPos = reader.BaseStream.Length - 8; // игнорирование последней 255
                while (reader.BaseStream.Position < lastPos)
                {
                    var octet = reader.ReadBytes(8);
                    if (octet[0] == 010)
                    {
                        var abonent = new RsnAbonent(bytes, rsnFile);
                        if (ShouldConvert(abonent.LsKvc))
                            convertAction(abonent);
                        if (countStpes) Iterate();
                        bytes = new List<byte[]> { octet };
                        continue;
                    }
                    bytes.Add(octet);
                }
                var lastabonent = new RsnAbonent(bytes, rsnFile);
                if(ShouldConvert(lastabonent.LsKvc))
                    convertAction(lastabonent);
            }
            if (countStpes) StepFinish();
        }

        /// <summary>
        /// Конвертирует полностью первый файл, а в остальных только отсутсвующих абонентов
        /// </summary>
        public void ConvertUniqueRsnAbonents(Action<RsnAbonent> convertAction, string filePath, List<ReadRsnForm.LsNotInFile> lsNotInlastFile, bool straightOrder, DateTime[] fileDates, bool withRsn3 = false)
        {
            var rsn3Date = new DateTime();
            var tempDates = fileDates.ToList();
            if (withRsn3)
            {
                rsn3Date = fileDates.Max().AddMonths(1);
                tempDates.Insert(0, rsn3Date);
            }
            Encoding encoding = Encoding.GetEncoding(1251);
            if (!straightOrder) tempDates.Reverse();
            foreach (var date in tempDates)
            {
                CurrnetConvertDate = date;
                string fileName = filePath + @"\rsn" + (date == rsn3Date ? "3" : "0") + RsnHelper.GetShortDate(date);
                var rsnFile = new RsnFile(fileName);
                string[] lostedLsToConvert = !withRsn3 && date == tempDates.Max() ? null : lsNotInlastFile?.FirstOrDefault(l => l.FileYear == date.Year && l.FileMonth == date.Month)?.LsList;
                //string[] lostedLsToConvert = lsNotInlastFile.FirstOrDefault(l => l.FileYear == date.Year && l.FileMonth == date.Month)?.LsList;

                Func<List<byte[]>, bool> shouldConvertFromOldFile;
                if (lostedLsToConvert == null)
                    shouldConvertFromOldFile = (bb) => true;
                else
                    shouldConvertFromOldFile = (bb) => lostedLsToConvert.Contains(new LsKvc(bb[0].ToUInt32(), bb[1].ToUInt32()).Ls);

                StepStart((int)rsnFile.КоличествоЛС);
                using (BinaryReader reader = new BinaryReader(File.OpenRead(fileName), encoding))
                {
                    reader.ReadBytes(8); // пропуск первой служебной 001
                    var bytes = new List<byte[]>();
                    bytes.Add(reader.ReadBytes(8));
                    if (bytes[0][0] != 010) throw new Exception($"Вторая октета в файле не имеет тип 010 ({bytes[0][0]})");
                    long lastPos = reader.BaseStream.Length - 8; // игнорирование последней 255
                    while (reader.BaseStream.Position < lastPos)
                    {
                        var octet = reader.ReadBytes(8);
                        if (octet[0] == 010)
                        {
                            if (shouldConvertFromOldFile(bytes))
                            {
                                var abonent = new RsnAbonent(bytes, rsnFile);
                                if (ShouldConvert(abonent.LsKvc))
                                    convertAction(abonent);
                            }
                            Iterate();
                            bytes = new List<byte[]> { octet };
                            continue;
                        }
                        bytes.Add(octet);
                    }
                    if (shouldConvertFromOldFile(bytes))
                    {
                        var lastabonent = new RsnAbonent(bytes, rsnFile);
                        if (ShouldConvert(lastabonent.LsKvc))
                            convertAction(lastabonent);
                    }
                }
                StepFinish();
            }
        }

        public void ConvertRsnFiles(Action<RsnAbonent> convertAction, Action actionAfterFile, string filePath, bool straightOrder, DateTime[] fileDates, bool withRsn3 = false)
        {
            var rsn3Date = new DateTime();
            var tempDates = fileDates.ToList();
            if (withRsn3)
            {
                rsn3Date = fileDates.Max().AddMonths(1);
                tempDates.Insert(0, rsn3Date);
            }
            Encoding encoding = Encoding.GetEncoding(1251);
            if (!straightOrder) tempDates.Reverse();
            foreach (var date in tempDates)
            {
                CurrnetConvertDate = date;
                string fileName = filePath + @"\rsn" + (date == rsn3Date ? "3" : "0") + RsnHelper.GetShortDate(date);
                var rsnFile = new RsnFile(fileName);
                StepStart((int)rsnFile.КоличествоЛС);
                using (BinaryReader reader = new BinaryReader(File.OpenRead(fileName), encoding))
                {
                    reader.ReadBytes(8); // пропуск первой служебной 001
                    var bytes = new List<byte[]>();
                    bytes.Add(reader.ReadBytes(8));
                    if (bytes[0][0] != 010) throw new Exception($"Вторая октета в файле не имеет тип 010 ({bytes[0][0]})");
                    long lastPos = reader.BaseStream.Length - 8; // игнорирование последней 255
                    while (reader.BaseStream.Position < lastPos)
                    {
                        var octet = reader.ReadBytes(8);
                        if (octet[0] == 010)
                        {
                            var lsKvc = new LsKvc(bytes[0].ToUInt32(), bytes[1].ToUInt32());
                            if (ShouldConvert(lsKvc) && ShouldConvertInIteration(lsKvc))
                                convertAction(new RsnAbonent(bytes, rsnFile));
                            Iterate();
                            bytes = new List<byte[]> { octet };
                            continue;
                        }
                        bytes.Add(octet);
                    }
                    var lastLsKvc = new LsKvc(bytes[0].ToUInt32(), bytes[1].ToUInt32());
                    if (ShouldConvert(lastLsKvc) && ShouldConvertInIteration(lastLsKvc))
                        convertAction(new RsnAbonent(bytes, rsnFile));
                }
                actionAfterFile?.Invoke();
                StepFinish();
            }
        }

        public void ConvertUniqueCCAbonents(Action<CcAbonent> convertAction, string filePath, List<ReadRsnForm.LsNotInFile> lsNotInlastFile, bool straightOrder, DateTime[] fileDates, bool withRsn3 = false)
        {
            var rsn3Date = new DateTime();
            var tempDates = fileDates.ToList();
            if (withRsn3)
            {
                rsn3Date = fileDates.Max().AddMonths(1);
                tempDates.Insert(0, rsn3Date);
            }
            Encoding encoding = Encoding.GetEncoding(1251);
            if (!straightOrder) fileDates = fileDates.Reverse().ToArray();
            foreach(var date in tempDates)
            {
                CurrnetConvertDate = date;
                string fileName = filePath + @"\cc" + (date == rsn3Date ? "3" : "0") + RsnHelper.GetShortDate(date);
                var ccFile = new CcFileInfo(fileName);
                string[] lostedLsToConvert = !withRsn3 && date == tempDates.Max() ? null : lsNotInlastFile?.FirstOrDefault(l => l.FileYear == date.Year && l.FileMonth == date.Month)?.LsList;
                //string[] lostedLsToConvert = lsNotInlastFile.FirstOrDefault(l => l.FileYear == date.Year && l.FileMonth == date.Month)?.LsList;

                Func<List<byte[]>, bool> shouldConvertFromOldFile;
                if (lostedLsToConvert == null)
                    shouldConvertFromOldFile = (bb) => true;
                else
                    shouldConvertFromOldFile = (bb) => lostedLsToConvert.Contains(new LsKvc(bb[0].ToUInt32(), bb[1].ToUInt32()).Ls);

                StepStart((int)ccFile.КоличествоЛс);
                using (BinaryReader reader = new BinaryReader(File.OpenRead(fileName), encoding))
                {
                    reader.ReadBytes(8); // пропуск первой служебной 001
                    var bytes = new List<byte[]>();
                    bytes.Add(reader.ReadBytes(8));
                    if (bytes[0][0] != 010) throw new Exception($"Вторая октета в файле не имеет тип 010 ({bytes[0][0]})");
                    long lastPos = reader.BaseStream.Length - 8; // игнорирование последней 255
                    while (reader.BaseStream.Position < lastPos)
                    {
                        var octet = reader.ReadBytes(8);
                        if (octet[0] == 010)
                        {
                            if (shouldConvertFromOldFile(bytes))
                            {
                                var abonent = new CcAbonent(bytes, ccFile);
                                if (ShouldConvert(abonent.LsKvc))
                                    convertAction(abonent);
                            }
                            Iterate();
                            bytes = new List<byte[]> { octet };
                            continue;
                        }
                        bytes.Add(octet);
                    }
                    if (shouldConvertFromOldFile(bytes))
                    {
                        var lastabonent = new CcAbonent(bytes, ccFile);
                        if (ShouldConvert(lastabonent.LsKvc))
                            convertAction(lastabonent);
                    }
                }
                StepFinish();
            }
        }

        public void ConvertCcFiles(Action<CcAbonent> convertAction, Action actionAfterFile, string filePath, bool straightOrder, DateTime[] fileDates, bool withRsn3 = false)
        {
            var rsn3Date = new DateTime();
            var tempDates = fileDates.ToList();
            if (withRsn3)
            {
                rsn3Date = fileDates.Max().AddMonths(1);
                tempDates.Insert(0, rsn3Date);
            }
            Encoding encoding = Encoding.GetEncoding(1251);
            if (!straightOrder) tempDates.Reverse();
            foreach (var date in tempDates)
            {
                CurrnetConvertDate = date;
                string fileName = filePath + @"\cc" + (date == rsn3Date ? "3" : "0") + RsnHelper.GetShortDate(date);
                var ccFile = new CcFileInfo(fileName);
                StepStart((int)ccFile.КоличествоЛс);
                using (BinaryReader reader = new BinaryReader(File.OpenRead(fileName), encoding))
                {
                    reader.ReadBytes(8); // пропуск первой служебной 001
                    var bytes = new List<byte[]>();
                    bytes.Add(reader.ReadBytes(8));
                    if (bytes[0][0] != 010) throw new Exception($"Вторая октета в файле не имеет тип 010 ({bytes[0][0]})");
                    long lastPos = reader.BaseStream.Length - 8; // игнорирование последней 255
                    while (reader.BaseStream.Position < lastPos)
                    {
                        var octet = reader.ReadBytes(8);
                        if (octet[0] == 010)
                        {
                            var lsKvc = new LsKvc(bytes[0].ToUInt32(), bytes[1].ToUInt32());
                            if (ShouldConvert(lsKvc) && ShouldConvertInIteration(lsKvc))
                                convertAction(new CcAbonent(bytes, ccFile));
                            Iterate();
                            bytes = new List<byte[]> { octet };
                            continue;
                        }
                        bytes.Add(octet);
                    }
                    var lastLsKvc = new LsKvc(bytes[0].ToUInt32(), bytes[1].ToUInt32());
                    if (ShouldConvert(lastLsKvc) && ShouldConvertInIteration(lastLsKvc))
                        convertAction(new CcAbonent(bytes, ccFile));
                }
                actionAfterFile?.Invoke();
                StepFinish();
            }
        }

        public void FreeListMemory<T>(List<T> list)
        {
            list.Clear();
            list.TrimExcess();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
