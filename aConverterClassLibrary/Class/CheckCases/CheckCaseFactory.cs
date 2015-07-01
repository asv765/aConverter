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
            var checkCaseList = new List<CheckCase>();

            //TablePresentCheckCase tpcc = new TablePresentCheckCase();
            //checkCaseList.Add(tpcc);

            //CodePageCheckCase cpcc = new CodePageCheckCase();
            //checkCaseList.Add(cpcc);

            //StructureCheckCase scc1 = new StructureCheckCase();
            //checkCaseList.Add(scc1);


            //var clfcc = new CheckLshetFormatCheckCase();
            //checkCaseList.Add(clfcc);

            //var nulcc = new NotUniqueLshetCheckCase();
            //checkCaseList.Add(nulcc);

            //var fiocc = new FioCheckCase();
            //checkCaseList.Add(fiocc);

            //var nunscc = new NotUniqueNachoplSaldoCheckCase();
            //checkCaseList.Add(nunscc);

            //var nscc = new NachoplSaldoCheckCase();
            //checkCaseList.Add(nscc);

            //var nocc = new NachoplOplataCheckCase();
            //checkCaseList.Add(nocc);

            //var nncc1 = new NachoplNachCheckCase1();
            //checkCaseList.Add(nncc1);

            //var nccc = new NachoplCalculationCheckCase();
            //checkCaseList.Add(nccc);

            //var oemcc = new OplataYearMonthCheckCase();
            //checkCaseList.Add(oemcc);

            //var noymcc = new NachoplYearMonthCheckCase();
            //checkCaseList.Add(noymcc);

            ////NachoplNachCheckCase2 nncc2 = new NachoplNachCheckCase2();
            ////checkCaseList.Add(nncc2);

            //var nuscc = new NotUniqueCounteridCheckCase();
            //checkCaseList.Add(nuscc);

            //var cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "NACHOPL", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "NACH", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "OPLATA", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "CHARS", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "LCHARS", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("COUNTERS", "COUNTERID", "CNTRSIND", "COUNTERID");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "COUNTERS", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "DOGOVOR", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "EQUIPMNT", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "LGOTA", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("ABONENT", "LSHET", "AADDCHAR", "LSHET");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("CHARLST", "ADDCHARCD", "AADDCHAR", "ADDCHARCD");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("CHARLST", "ADDCHARCD", "CHARVALS", "ADDCHARCD");
            //checkCaseList.Add(cifcc);

            //cifcc = new InternalForeighnKeyCheckCase("AADDCHAR", "ADDCHARCD", "CHARVALS", "ADDCHARCD");
            //checkCaseList.Add(cifcc);

            //var ucc = new UncertaintyCheckCase("ABONENT", "ULICAKOD", "ULICANAME", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //var nucvcc = new NotUniqueCharsValuesCheckCase();
            //checkCaseList.Add(nucvcc);

            //var nulcvcc = new NotUniqueLcharsValuesCheckCase();
            //checkCaseList.Add(nulcvcc);

            //var hccc = new HouseCDCheckCase();
            //checkCaseList.Add(hccc);

            //ucc = new UncertaintyCheckCase("ABONENT", "RayonKod", "RayonName", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("ABONENT", "TownsKod", "TownsName", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("CHARS", "CharCD", "CharName", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("LCHARS", "LCharCD", "LCharName", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("LCHARS", "Convert(LCharCD,char) + Convert(Value,char)", "ValueDesc", UncertaintyCDType.Строка);
            //checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("LGOTA", "Lgota", "LgotaName", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //ucc = new UncertaintyCheckCase("OPLATA", "SourceCD", "SourceName", UncertaintyCDType.Число);
            //checkCaseList.Add(ucc);

            //var efkcc = new ExternalForeighnKeyCheckCase("CHARS", "CHARCD", "CCHARSLIST", "KOD");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("LCHARS", "LCHARCD", "LCHARSLIST", "KOD");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("LCHARS", "LCHARCD*10000+Value", "LOGICVALUES", "KOD*10000+Significance");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("COUNTERS", "CNTTYPE", "COUNTERSTYPES", "KOD");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("NACHOPL", "SERVICECD", "BALANCESLIST", "BALANCE_KOD");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("OPLATA", "SERVICECD", "BALANCESLIST", "BALANCE_KOD");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("NACH", "REGIMCD", "RESOURCESREGIMSLIST", "KODREGIM");
            //checkCaseList.Add(efkcc);

            //efkcc = new ExternalForeighnKeyCheckCase("ABONENT", "DUCD", "INFORMATIONOWNERS", "OWNERID");
            //checkCaseList.Add(efkcc);

            //var clccc = new CheckLshetCorrespondenceCheckCase();
            //checkCaseList.Add(clccc);

            //var urpcc = new UnknownRegimPresentCheckCase();
            //checkCaseList.Add(urpcc);

            //var mocc = new MainOrganizationCheckCase();
            //checkCaseList.Add(mocc);

            //var uepcc = new UnknownEmployeePresentCheckCase();
            //checkCaseList.Add(uepcc);

            //// Здесь добавляются свои варианты для проверки и заполнения следующих справочников
            //// 1. Справочник районов области
            ////RegionDistrictCheckCase rdcc = new RegionDistrictCheckCase();
            ////checkCaseList.Add(rdcc);

            //// 2. Справочник населенных пунктов.
            ////PunktCheckCase pcc = new PunktCheckCase();
            ////checkCaseList.Add(pcc);

            //// 3. Справочник улиц.
            ////StreetCheckCase scc = new StreetCheckCase();
            ////checkCaseList.Add(scc);

            //// 4. Справочник районов города.
            ////DistrictCheckCase dcc = new DistrictCheckCase();
            ////checkCaseList.Add(dcc);

            //// 5. Справочник источников оплаты
            ////SourceDocCheckCase sdcc = new SourceDocCheckCase();
            ////checkCaseList.Add(sdcc);

            ////// 6. Справочник групповых счетчиков
            ////CapCheckCase ccc = new CapCheckCase();
            ////checkCaseList.Add(ccc);

            return checkCaseList;
        }
    }
}
