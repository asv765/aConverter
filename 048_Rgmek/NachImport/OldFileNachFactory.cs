using System;
using System.IO;
using _048_Rgmek.Records;

namespace _048_Rgmek.NachImport
{
    public class OldFileNachFactory : INachImportFactory
    {
        public INachImport Create(string filePath)
        {
            var match = ConvertNach.FileNameRegex.Match(new FileInfo(filePath).Name.ToLower());
            if (!match.Success) throw new ArgumentException($"Имя файла {filePath} не соответствует формату старого файла", nameof(filePath));
            string addInfo = match.Groups["addInfo"].Value;

            NachExcelRecord.ZoneType zone;
            if (addInfo.Contains("день")) zone = NachExcelRecord.ZoneType.Day;
            else if (addInfo.Contains("ночь")) zone = NachExcelRecord.ZoneType.Night;
            else zone = NachExcelRecord.ZoneType.None;

            NachExcelRecord.TarifType tarif;
            if (addInfo.Contains("газ. плиты"))
                tarif = zone == NachExcelRecord.ZoneType.None
                    ? NachExcelRecord.TarifType.Gp1
                    : NachExcelRecord.TarifType.Gp2;
            else if (addInfo.Contains("эл. плиты"))
                tarif = zone == NachExcelRecord.ZoneType.None
                    ? NachExcelRecord.TarifType.Ep1
                    : NachExcelRecord.TarifType.Ep2;
            else if (addInfo.Contains("сельский"))
                tarif = NachExcelRecord.TarifType.Village;
            else
                throw new Exception($"Не удалось определить тариф старого файла {filePath}");

            return new OldFileNachImport(tarif, zone);
        }
    }
}
