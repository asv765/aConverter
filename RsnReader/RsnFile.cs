using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RsnReader
{
    public class RsnFile
    {
        public string FilePath { get; }
        public RsnFileType Type { get; }
        public UInt16 FileYear { get; }
        public UInt16 FileMonth { get; }
        public DateTime FileDate { get; }

        public byte СостояниеМассива { get; }
        public UInt16 РассчетныйГод { get; }
        public UInt16 РассчетныйМесяц { get; }
        public UInt32 КоличествоЛС { get; }
        public UInt32 КонтрольнаяСумма { get; }

        public Encoding Encoding = Encoding.GetEncoding(1251);

        //public byte[][] Bytes { get; }

        public RsnFile(string filePath)
        {
            FilePath = filePath;
            var fileInfo = new FileInfo(filePath);
            var regex = new Regex(@"^rsn(?<type>0|3)(?<year>\d{2})(?<month>\d{2})$", RegexOptions.IgnoreCase);
            var match = regex.Match(fileInfo.Name);
            if (!match.Success) throw new Exception($"Не удалось распарсить имя файла {fileInfo.Name}");
            Type = (RsnFileType)Int32.Parse(match.Groups["type"].Value);
            FileYear = (ushort)(2000 + Int32.Parse(match.Groups["year"].Value));
            FileMonth = UInt16.Parse(match.Groups["month"].Value);
            int day = Type == RsnFileType.BeginMonth ? 1 : DateTime.DaysInMonth(FileYear, FileMonth);
            FileDate = new DateTime(FileYear, FileMonth, day);

            using (BinaryReader reader = new BinaryReader(File.OpenRead(filePath), Encoding.GetEncoding(1251)))
            {
                if (reader.BaseStream.Length % 8 != 0) throw new Exception("Количество байт в файле не делится на 8");

                var firstOctet = reader.ReadBytes(8);
                byte[] lastOctet = new byte[8];
                long lastPos = reader.BaseStream.Length;
                while (reader.BaseStream.Position < lastPos)
                {
                    lastOctet = reader.ReadBytes(8);
                }
                if (firstOctet[0] != 001) throw new Exception($"Первая октета в файле не имеет тип 001 ({firstOctet[0]})");
                if (lastOctet[0] != 255) throw new Exception($"Последняя октета в файле не имеет тип 255 ({lastOctet[0]})");
                СостояниеМассива = firstOctet[1];
                var calcDate = $"{firstOctet.ToUInt16():D4}";
                РассчетныйГод = (ushort)(Int32.Parse(calcDate.Substring(0, 2)) + 2000);
                РассчетныйМесяц = UInt16.Parse(calcDate.Substring(2, 2));
                КоличествоЛС = firstOctet.ToUInt32();
                КонтрольнаяСумма = lastOctet.ToUInt32();
            }
        }

        public delegate void ProgressChangedEventHandler(long complited, long total);
        public static event ProgressChangedEventHandler OnProgressChanged;

        public /*List<RsnAbonent>*/ RsnAbonent[] ExtractAbonents()
        {
            //var abonents = List<RsnAbonent>();
            var abonents = new RsnAbonent[КоличествоЛС];
            using (BinaryReader reader = new BinaryReader(File.OpenRead(FilePath), Encoding.GetEncoding(1251)))
            {
                reader.ReadBytes(8); // пропуск первой служебной 001
                var octetCount = reader.BaseStream.Length / 8;
                long updateProgress = octetCount / 100;
                var bytes = new List<byte[]>();
                bytes.Add(reader.ReadBytes(8));
                if (bytes[0][0] != 010) throw new Exception($"Вторая октета в файле не имеет тип 010 ({bytes[0][0]})");
                long lastPos = reader.BaseStream.Length - 8; // игнорирование последней 255
                int i = 0;
                int a = 0;
                while (reader.BaseStream.Position < lastPos)
                {
                    i++;
                    if (i % updateProgress == 0) OnProgressChanged?.Invoke(i, octetCount);
                    var octet = reader.ReadBytes(8);
                    if (octet[0] == 010)
                    {
                        //abonents.Add(new RsnAbonent(bytes, this));
                        //var abonent = new RsnAbonent(bytes, this);
                        //if (abonent.LsKvc.StreetId == 226)
                            abonents[a] = new RsnAbonent(bytes, this);
                        a++;
                        bytes = new List<byte[]> { octet };
                        continue;
                    }
                    bytes.Add(octet);
                }
                abonents[a] = new RsnAbonent(bytes, this);
                //abonents.Add(new RsnAbonent(bytes, this));
            }
            return abonents/*.Where(a =>a != null).ToArray()*/;
        }

        /// <summary>
        /// Количество октет для каждого из содержащихся типов записей
        /// </summary>
        /// <param name="copyToMemory">Признак копирования текстового вывода в память</param>
        /// <returns>Словарь тип-количество октет</returns>
        public Dictionary<byte, int> CalcOctetTypeCount(bool copyToMemory = false)
        {
            return null;
            //var count = Bytes
            //    .Select(b => b[0])
            //    .Distinct()
            //    .OrderBy(b => b)
            //    .ToDictionary(b => b, b => Bytes.Count(b2 => b2[0] == b));
            //if (copyToMemory)
            //    Clipboard.SetText(String.Join("", count.Select(b => $"{b.Key:D3}\t{b.Value}\r\n").ToArray()));
            //return count;
        }
    }

    public enum RsnFileType
    {
        EndMonth = 0,
        BeginMonth = 3
    }
}
