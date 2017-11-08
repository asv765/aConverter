using System;
using System.Data;

namespace _048_Rgmek
{
    public class CommonAddCharRecord
    {
        public string Owner;
        public string CharId;
        public string CharName;
        public string Value;
        public DateTime Date;

        public CommonAddCharRecord(DataRow dr)
        {
            Owner = dr[0].ToString().Trim();
            CharId = dr[1].ToString().Trim();
            CharName = dr[2].ToString().Trim();
            string valueS = dr[4].ToString();
            if (String.IsNullOrWhiteSpace(valueS))
            {
                var valueD = Convert.ToDateTime(dr[5]);
                if (valueD == Consts.NullDate)
                {
                    var valueN = Convert.ToDecimal(dr[3]);
                    Value = valueN - (int)valueN == 0
                        ? ((int)valueN).ToString()
                        : valueN.ToString();
                }
                else
                    Value = valueD.ToString();
            }
            else
                Value = valueS.Trim();
            Date = Convert.ToDateTime(dr[6]);
        }
    }
}
