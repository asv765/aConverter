using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aConverterClassLibrary;
using System.Data;
using System.Windows.Forms;
using aConverter.Forms;



namespace aConverter
{
    public class ShowStatisticClass
    {
        public static string ShowStatistic(Statistic s)
        {
            string rv = "";
            try
            {
                if (s.StatisticType == StatisticType.Таблица)
                {
                    DataTable dt = s.GenerateStatistic();
                    FormStatisticResult fss = new FormStatisticResult(s.StatisticName, dt, s.FieldRecodeList);
                    fss.Show();
                }
                else if (s.StatisticType == StatisticType.Одиночное_значение)
                {
                    DataTable dt = s.GenerateStatistic();
                    rv = Convert.ToString(dt.Rows[0][0]);
                    s.Value = rv;
                }
                else if (s.StatisticType == StatisticType.Не_возвращает_значений)
                {
                    s.GenerateStatistic();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("В результате выполнения возникла ошибка:\r\n" +
                    ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
            return rv;
        }
    }
}
