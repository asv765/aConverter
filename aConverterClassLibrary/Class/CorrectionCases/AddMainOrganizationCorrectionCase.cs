using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary.Class.CorrectionCases
{
    public class AddMainOrganizationCorrectionCase : CorrectionCase
    {
        public AddMainOrganizationCorrectionCase()
        {
            this.CorrectionCaseName = String.Format("Добавить организацию с кодом 1");
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Correct()
        {
            this.Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            this.Message = "Корректировка завершилась успешно";

            using (FbConnection connection = new FbConnection(aConverter_RootSettings.FirebirdStringConnection))
            {
                using (FbCommand command = connection.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        command.CommandText = String.Format("INSERT INTO EXTORGSPR (EXTORGCD, EXTORGNM, CANGIVELGOT, PAYIMPORT, CHARSIMPORT, ISACCOUNT, LSHETFORMAT, HASOWNADRESSCD, EQUIPMENTSALE, EQUIPMENTMAKE, ISPROVIDER, JURIDICALADDRESS, POSTADDRESS, PHONE, INN, KPP, RS, DIRECTOR, MAINACCOUNTANT, NOTE, OKPO, BANK, KORACCOUNT, BIK, ISEXTERNALCALC, CANISSUEPASSP, ISBASEORGANIZATION, SECTORADDRESS, SECTORPHONE, SECTORWORKMODE, ISDEFAULTORG, ISMILITARYCOMISSION, ISTAX) " +
                                                            "VALUES (1, 'Неизвестная организация (наименование и параметры уточнить в справочнике организаций)', 0, 0, 1, 0, NULL, 0, 0, 0, 1, NULL, NULL, '(99999)9-99-99', '9999999999', '9999999999', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, NULL, NULL, NULL, 1, 0, 0);");
                        command.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        this.Result = CorrectionCaseStatus.Ошибка_при_выполнении_корректировки;
                        this.Message = "При выполнении корректировки возникла ислючительная ситуация:\r\n" + ex.Message;
                    }
                }
            }
        }

    }
}
