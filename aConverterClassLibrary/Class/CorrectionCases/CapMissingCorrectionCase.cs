using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace aConverterClassLibrary
{
    public class CapMissingCorrectionCase: CorrectionCase
    {
        private List<DataRow> missingValues;
        private int counterTypeId;

        public CapMissingCorrectionCase(List<DataRow> AMissingValues, int ACounterTypeId)
        {
            this.CorrectionCaseName = String.Format("Добавить в таблицу RESOURCECOUNTERS и связанную с ней PARENTEQUIPMENT {0} записей", AMissingValues.Count);
            missingValues = AMissingValues;
            counterTypeId = ACounterTypeId;
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
                        foreach (DataRow dr in missingValues)
                        {
                            command.CommandText = String.Format("SELECT COUNT(*) FROM PARENTEQUIPMENT WHERE EquipmentID = {0}", dr["CapCD"]);
                            int count = Convert.ToInt32(command.ExecuteScalar());
                            if (count == 0)
                            {
                                command.CommandText = String.Format("INSERT INTO ParentEquipment  ( EquipmentID, Note ) VALUES ({0}, '{1}')",
                                    dr["CapCD"], Convert.ToString(dr["CapName"]).Trim());
                                command.ExecuteNonQuery();
                            }

                            command.CommandText = String.Format("INSERT INTO ResourceCounters  ( Kod, KodCountersTypes, Name, SourcePPR, IsDefault, Counter_Level ) " +
                                "VALUES ({0}, {1}, '{2}', 0, 0, 1)",
                                dr["CapCD"], counterTypeId, Convert.ToString(dr["CapName"]).Trim());
                            command.ExecuteNonQuery();

                            CorrectionCase.SetFdbGenerator("CAPACITY_G", "CAPACITY", "CAPACITYCD");
                            CorrectionCase.SetFdbGenerator("PARENTEQUI_GEN", "PARENTEQUIPMENT", "EQUIPMENTID");
                        }
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
