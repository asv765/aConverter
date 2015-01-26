using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public class MakeHouseCDUniqueCorrectionCase: CorrectionCase
    {
        private string findquery;

        public MakeHouseCDUniqueCorrectionCase(string Afindquery)
        {
            this.CorrectionCaseName = String.Format("Сделать поле HOUSECD в ABONENT.DBF уникальным");
            findquery = Afindquery;
        }

        /// <summary>
        /// Создает таблицу на диске
        /// </summary>
        public override void Correct()
        {
            this.Result = CorrectionCaseStatus.Корректировка_выполнена_успешно;
            this.Message = "Корректировка завершилась успешно";
            TableManager tm = new TableManager(aConverter_RootSettings.DestDBFFilePath);
            tm.Init();
            try
            {
                List<int> housecdnumerator = new List<int>();
                DataTable dt = tm.ExecuteQuery("select distinct housecd from abonent");
                foreach (DataRow dr in dt.Rows)
                {
                    int housecd = Convert.ToInt32(dr["housecd"]);
                    if (!housecdnumerator.Contains(housecd)) housecdnumerator.Add(housecd);
                }
                dt = tm.ExecuteQuery(findquery);
                int oldhousecd = -1;
                foreach (DataRow dr in dt.Rows)
                {
                    int housecd = Convert.ToInt32(dr["housecd"]);
                    if (oldhousecd != housecd)
                    {
                        oldhousecd = housecd;
                        if (housecd > 0) continue;
                    }
                    int newhousecd = nexthousecd(ref housecdnumerator);
                    string update = String.Format("UPDATE ABONENT SET HOUSECD = {0} WHERE RAYONKOD = {1} AND TOWNSKOD = {2} AND ULICAKOD = {3} AND NDOMA = '{4}' AND KORPUS = {5}", 
                        newhousecd, dr["RAYONKOD"], dr["TOWNSKOD"], dr["ULICAKOD"], dr["NDOMA"], dr["KORPUS"]);
                    tm.ExecuteNonQuery(update);
                }
            }
            catch (Exception ex)
            {
                this.Result = CorrectionCaseStatus.Ошибка_при_выполнении_корректировки;
                this.Message = "При выполнении корректировки возникла ислючительная ситуация:\r\n" + ex.Message;
            }
            finally
            {
                tm.Dispose();
            }

        }

        private int nexthousecd(ref List<int> housecdnumerator)
        {
            int nextvalue = 1;
            if (housecdnumerator.Count != 0) nextvalue = housecdnumerator.Max() + 1;
            housecdnumerator.Add(nextvalue);
            return nextvalue;

            //for (int i = 1; i < housecdnumerator.Count + 2; i++)
            //{
            //    if (!housecdnumerator.Contains(i))
            //    {
            //        housecdnumerator.Add(i);
            //        return i;
            //    }
            //}
            //throw new Exception("Почему-то не нашли нужный код в диапазоне...");
            // return -1;
        }
    }
}
