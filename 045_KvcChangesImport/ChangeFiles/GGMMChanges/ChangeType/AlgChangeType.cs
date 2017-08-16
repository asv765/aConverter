using System;
using System.Collections.Generic;
using System.Linq;
using aConverterClassLibrary.RecordsDataAccessORM;
using _049_Zheu18;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public class AlgChangeType : IGGMMChangeType
    {
        public List<CNV_LCHAR> Lchars;

        public AlgChangeType(List<CNV_LCHAR> lchars)
        {
            Lchars = lchars;
        }

        public void Convert(GGMMChangeRecord record)
        {
            string lshet = Consts.LsDic[record.LsKvc.Ls];

            foreach (var lchar in Lchars)
            {
                lchar.LSHET = lshet;
                DigitChangesImport.LcharsList.Add(lchar);
            }
        }
    }

    public class AlgChangeTypeFactory : IGGMMChangeTypeFactory
    {
        private static readonly ConvertLchars.Recode[] AlgRecode;

        static AlgChangeTypeFactory()
        {
            AlgRecode = ConvertLchars.Recode.GetRecodeTable(ChangesConsts.AlgChangeRecodeFilePath, "Лист1");
        }

        public IGGMMChangeType Create(GGMMChangeRecord record)
        {
            if (record.ТипНачисления == 6)
            {
                if (record.Графа >= 14 && record.Графа <= 33)
                {
                    var graphInfo = record.GetGraphInfo();
                    if (!DigitChangesImport.IsServiceActive(record.LsKvc.CombinedLs, graphInfo.Vid))
                    {
                        DigitChangesImport.ErrorLog.Add(record, $"Запрещено изменять алгоритм. Вид {graphInfo.Vid} не открыт или закрыт");
                        return new SkipChangeType();
                    }

                    var lchars = GetLchars(record);
                    if (record.Графа == 14) // смена водоотведения при холодной воде
                    {
                        var alg = DigitChangesImport.AlgSpr.First(a => a.R1 == 5 && a.R2 == record.Значение);
                        if (alg.R3 == 0) // если алгоритм хвс не поддерживает водоотведение
                            lchars.Add(new CNV_LCHAR
                            {
                                LCHARCD = 8,
                                VALUE_ = 0,
                                DATE_ = record.FileDate
                            });
                        else
                            lchars.Add(new CNV_LCHAR
                            {
                                LCHARCD = 8,
                                VALUE_ = 1,
                                DATE_ = record.FileDate
                            });
                    }

                    bool isCounter = DigitChangesImport.AbonentCounters[record.LsKvc.CombinedLs][Consts.resourceRecode[graphInfo.Vid]].IsCounter;
                    // смена типа учета
                    if ((isCounter && record.Значение < 100) || (!isCounter && record.Значение >= 100)) 
                    {
                        foreach (var lchar in lchars)
                        {
                            lchar.DATE_ = lchar.DATE_.Value.AddMonths(1);
                        }
                    }


                    return new AlgChangeType(lchars);
                }
                else 
                    return null;
            }
            else 
                return null;
        }

        private static List<CNV_LCHAR> GetLchars(GGMMChangeRecord record)
        {
            var lchars = new List<CNV_LCHAR>();
            var graphInfo = record.GetGraphInfo();
            foreach (var recode in AlgRecode)
            {
                if (graphInfo.Vid != recode.Vid) continue;
                decimal checkField;
                bool allConditionTrue = false;
                for (int j = 0; j < recode.ConditionFunc.Length; j++)
                {
                    switch (recode.CheckField[j])
                    {
                        case ConvertLchars.Recode.CheckFieldEnum.AlgId:
                            checkField = record.Значение;
                            break;
                        default:
                            throw new Exception("Необработанная перекодировка " + recode.CheckField[j]);
                    }
                    allConditionTrue = recode.ConditionFunc[j](checkField);
                    if (!allConditionTrue) break;
                }
                if (allConditionTrue)
                {
                    int val = 0;
                    switch (recode.ValueType)
                    {
                        case ConvertLchars.Recode.ValueTypeEnum.RecodeValue:
                            val = recode.Value;
                            break;
                    }

                    lchars.Add(new CNV_LCHAR
                    {
                        LCHARCD = recode.LcharId,
                        VALUE_ = val,
                        DATE_ = record.FileDate
                    });
                }
            }
            return lchars;
        }
    }
}
