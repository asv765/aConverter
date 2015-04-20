using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    public class CheckCaseFactory
    {
        public static List<CheckCase> GenerateCheckCases()
        {
            List<CheckCase> checkCaseList = new List<CheckCase>();

            //TablePresentCheckCase tpcc = new TablePresentCheckCase();
            //checkCaseList.Add(tpcc);

            //CodePageCheckCase cpcc = new CodePageCheckCase();
            //checkCaseList.Add(cpcc);

            //StructureCheckCase scc1 = new StructureCheckCase();
            //checkCaseList.Add(scc1);

            CheckLshetFormatCheckCase clfcc = new CheckLshetFormatCheckCase();
            checkCaseList.Add(clfcc);

            NotUniqueLshetCheckCase nulcc = new NotUniqueLshetCheckCase();
            checkCaseList.Add(nulcc);

            FioCheckCase fiocc = new FioCheckCase();
            checkCaseList.Add(fiocc);

            NotUniqueNachoplSaldoCheckCase nunscc = new NotUniqueNachoplSaldoCheckCase();
            checkCaseList.Add(nunscc);

            //NachoplSaldoCheckCase nscc = new NachoplSaldoCheckCase();
            //checkCaseList.Add(nscc);

            NachoplOplataCheckCase nocc = new NachoplOplataCheckCase();
            checkCaseList.Add(nocc);

            NachoplNachCheckCase1 nncc1 = new NachoplNachCheckCase1();
            checkCaseList.Add(nncc1);

            NachoplCalculationCheckCase nccc = new NachoplCalculationCheckCase();
            checkCaseList.Add(nccc);

            OplataYearMonthCheckCase oemcc = new OplataYearMonthCheckCase();
            checkCaseList.Add(oemcc);

            NachoplYearMonthCheckCase noymcc = new NachoplYearMonthCheckCase();
            checkCaseList.Add(noymcc);

            //NachoplNachCheckCase2 nncc2 = new NachoplNachCheckCase2();
            //checkCaseList.Add(nncc2);
            NotUniqueCounteridCheckCase nuscc = new NotUniqueCounteridCheckCase();
            checkCaseList.Add(nuscc);

            InternalForeighnKeyCheckCase cifcc;
            cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "NACHOPL", "LSHET");
            checkCaseList.Add(cifcc);

            cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "NACH", "LSHET");
            checkCaseList.Add(cifcc);

            cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "OPLATA", "LSHET");
            checkCaseList.Add(cifcc);

            cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "CHARS", "LSHET");
            checkCaseList.Add(cifcc);

            cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "LCHARS", "LSHET");
            checkCaseList.Add(cifcc);

            cifcc = new InternalForeighnKeyCheckCase("COUNTERS", "COUNTERID", "CNTRSIND", "COUNTERID");
            checkCaseList.Add(cifcc);

            cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "COUNTERS", "LSHET");
            checkCaseList.Add(cifcc);

            cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "DOGOVOR", "LSHET");
            checkCaseList.Add(cifcc);

            cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "EQUIPMNT", "LSHET");
            checkCaseList.Add(cifcc);

            cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "LGOTA", "LSHET");
            checkCaseList.Add(cifcc);

            cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "AADDCHAR", "LSHET");
            checkCaseList.Add(cifcc);

            cifcc = new InternalForeighnKeyCheckCase("CHARLST", "ADDCHARCD", "AADDCHAR", "ADDCHARCD");
            checkCaseList.Add(cifcc);

            cifcc = new InternalForeighnKeyCheckCase("CHARLST", "ADDCHARCD", "CHARVALS", "ADDCHARCD");
            checkCaseList.Add(cifcc);

            cifcc = new InternalForeighnKeyCheckCase("AADDCHAR", "ADDCHARCD", "CHARVALS", "ADDCHARCD");
            checkCaseList.Add(cifcc);

            UncertaintyCheckCase ucc = new UncertaintyCheckCase("ABONENT", "ULICAKOD", "ULICANAME", UncertaintyCDType.Число);
            checkCaseList.Add(ucc);

            NotUniqueCharsValuesCheckCase nucvcc = new NotUniqueCharsValuesCheckCase();
            checkCaseList.Add(nucvcc);

            NotUniqueLcharsValuesCheckCase nulcvcc = new NotUniqueLcharsValuesCheckCase();
            checkCaseList.Add(nulcvcc);

            HouseCDCheckCase hccc = new HouseCDCheckCase();
            checkCaseList.Add(hccc);

            //UncertaintyCheckCase ucc2 = new UncertaintyCheckCase("ABONENT", "HOUSECD", "STR(DISTKOD,6)+STR(TOWNSKOD,6)+STR(ULICAKOD,6)+NDOMA", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc2);

            ucc = new UncertaintyCheckCase("ABONENT", "RayonKod", "RayonName", UncertaintyCDType.Число);
            checkCaseList.Add(ucc);

            ucc = new UncertaintyCheckCase("ABONENT", "TownsKod", "TownsName", UncertaintyCDType.Число);
            checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("ABONENT", "CapCD", "CapName", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            ucc = new UncertaintyCheckCase("CHARS", "CharCD", "CharName", UncertaintyCDType.Число);
            checkCaseList.Add(ucc);

            ucc = new UncertaintyCheckCase("LCHARS", "LCharCD", "LCharName", UncertaintyCDType.Число);
            checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("LCHARS", "Str(LCharCD,5) + Str(Value,5)", "ValueDesc", UncertaintyCDType.Строка);
            ucc = new UncertaintyCheckCase("LCHARS", "Convert(LCharCD,char) + Convert(Value,char)", "ValueDesc", UncertaintyCDType.Строка);
            checkCaseList.Add(ucc);

            ucc = new UncertaintyCheckCase("LGOTA", "Lgota", "LgotaName", UncertaintyCDType.Число);
            checkCaseList.Add(ucc);

            ucc = new UncertaintyCheckCase("OPLATA", "SourceCD", "SourceName", UncertaintyCDType.Число);
            checkCaseList.Add(ucc);

            ExternalForeighnKeyCheckCase efkcc = new ExternalForeighnKeyCheckCase("CHARS", "CHARCD", "CCHARSLIST", "KOD");
            checkCaseList.Add(efkcc);

            efkcc = new ExternalForeighnKeyCheckCase("LCHARS", "LCHARCD", "LCHARSLIST", "KOD");
            checkCaseList.Add(efkcc);

            efkcc = new ExternalForeighnKeyCheckCase("LCHARS", "LCHARCD*10000+Value", "LOGICVALUES", "KOD*10000+Significance");
            checkCaseList.Add(efkcc);

            efkcc = new ExternalForeighnKeyCheckCase("COUNTERS", "CNTTYPE", "COUNTERSTYPES", "KOD");
            checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("COUNTERS", "SERVICECD", "BALANCESLIST", "BALANCE_KOD");
            //checkCaseList.Add(efkcc);

            efkcc = new ExternalForeighnKeyCheckCase("NACHOPL", "SERVICECD", "BALANCESLIST", "BALANCE_KOD");
            checkCaseList.Add(efkcc);

            efkcc = new ExternalForeighnKeyCheckCase("OPLATA", "SERVICECD", "BALANCESLIST", "BALANCE_KOD");
            checkCaseList.Add(efkcc);

            efkcc = new ExternalForeighnKeyCheckCase("NACH", "REGIMCD", "RESOURCESREGIMSLIST", "KODREGIM");
            checkCaseList.Add(efkcc);

            efkcc = new ExternalForeighnKeyCheckCase("ABONENT", "DUCD", "INFORMATIONOWNERS", "OWNERID");
            checkCaseList.Add(efkcc);

            CheckLshetCorrespondenceCheckCase clccc = new CheckLshetCorrespondenceCheckCase();
            checkCaseList.Add(clccc);

            UnknownRegimPresentCheckCase urpcc = new UnknownRegimPresentCheckCase();
            checkCaseList.Add(urpcc);

            MainOrganizationCheckCase mocc = new MainOrganizationCheckCase();
            checkCaseList.Add(mocc);

            UnknownEmployeePresentCheckCase uepcc = new UnknownEmployeePresentCheckCase();
            checkCaseList.Add(uepcc);

            // Здесь добавляются свои варианты для проверки и заполнения следующих справочников
            // 1. Справочник районов области
            //RegionDistrictCheckCase rdcc = new RegionDistrictCheckCase();
            //checkCaseList.Add(rdcc);

            // 2. Справочник населенных пунктов.
            //PunktCheckCase pcc = new PunktCheckCase();
            //checkCaseList.Add(pcc);

            // 3. Справочник улиц.
            //StreetCheckCase scc = new StreetCheckCase();
            //checkCaseList.Add(scc);

            // 4. Справочник районов города.
            //DistrictCheckCase dcc = new DistrictCheckCase();
            //checkCaseList.Add(dcc);

            // 5. Справочник источников оплаты
            //SourceDocCheckCase sdcc = new SourceDocCheckCase();
            //checkCaseList.Add(sdcc);

            //// 6. Справочник групповых счетчиков
            //CapCheckCase ccc = new CapCheckCase();
            //checkCaseList.Add(ccc);

            return checkCaseList;
        }

        public static List<CheckCase> GenerateCheckCases(CheckCaseClass ACheckCaseClass)
        {
            List<CheckCase> lcc = GenerateCheckCases();
            lcc = lcc.Where(c => c.CheckCaseClass == ACheckCaseClass).ToList();
            return lcc;
        }
    }
}
