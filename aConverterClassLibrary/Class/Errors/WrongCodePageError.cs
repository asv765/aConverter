using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public class WrongCodePageError: ErrorClass
    {
        private byte codePageByte;
        private Type recordType;

        public WrongCodePageError(Type ARecordType, byte ACodePageByte)
        {
            string codePageName = "";
            switch (ACodePageByte)
            {
                case 0x00:
                    codePageName = "OEM (0x00)"; break;
                case 0x01:
                    codePageName = "Codepage_437_US_MSDOS"; break;
                case 0x02:
                    codePageName = "Codepage_850_International_MSDOS"; break;
                case 0x03:
                    codePageName = "Codepage_1252_Windows_ANSI"; break;
                case 0x57:
                    codePageName = "ANSI"; break;
                case 0x64:
                    codePageName = "Codepage_852_EasernEuropean_MSDOS"; break;
                case 0x65:
                    codePageName = "Codepage_866_Russian_MSDOS"; break;
                case 0x66:
                    codePageName = "Codepage_865_Nordic_MSDOS"; break;
                case 0x67:
                    codePageName = "Codepage_861_Icelandic_MSDOS"; break;
                case 0x6A:
                    codePageName = "Codepage_737_Greek_MSDOS"; break;
                case 0x6B:
                    codePageName = "Codepage_857_Turkish_MSDOS"; break;
                case 0x78:
                    codePageName = "Codepage_950_Chinese_Windows"; break;
                case 0x7A:
                    codePageName = "Codepage_936_Chinese_Windows"; break;
                case 0x7B:
                    codePageName = "Codepage_932_Japanese_Windows"; break;
                case 0x7D:
                    codePageName = "Codepage_1255_Hebrew_Windows"; break;
                case 0x7E:
                    codePageName = "Codepage_1256_Arabic_Windows"; break;
                case 0xC8:
                    codePageName = "Codepage_1250_Eastern_European_Windows"; break;
                case 0xC9:
                    codePageName = "Codepage_1251_Russian_Windows"; break;
                case 0xCA:
                    codePageName = "Codepage_1254_Turkish_Windows"; break;
                case 0xCB:
                    codePageName = "Codepage_1253_Greek_Windows"; break;
                default:
                    codePageName = String.Format("определить не удалось (0x{0})", ACodePageByte.ToString("x"));
                    break;
            }
            this.ErrorName = 
                String.Format("У файла {0}.DBF установлена кодовая страница, отличная от 866 DOS Russian ({1})",
                   TableManager.GetTableName(ARecordType),
                   codePageName);
            if (ACodePageByte != 0)
            {
                this.ErrorName += ", корректироваться не будет";
            }
            this.IsTerminating = true;
            codePageByte = ACodePageByte;
            recordType = ARecordType;
        }

        public override void GenerateCorrectionCases()
        {
            CorrectionCases.Clear();
            if (codePageByte == 0)
            {
                CodePageCorrectionCase ccpcc = new CodePageCorrectionCase(recordType);
                ccpcc.ParentError = this;
                CorrectionCases.Add(ccpcc);
            }
        }        
    }
}
