using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aConverterClassLibrary;
using System.IO;

namespace aConverterCheckStructure
{
    
    class Program
    {
        static void Main(string[] args)
        {
            // string cp = Utils.CurrentPath;
            string cp = Directory.GetCurrentDirectory();
            Console.WriteLine("Проверяются файлы в каталоге " + cp);
            if (cp[cp.Length - 1] == '\\') cp = cp.Substring(0,cp.Length - 1);
            aConverter_RootSettings.DestDBFFilePath = cp;
            List<CheckCase> lcc = CheckCaseFactory.GenerateCheckCases(CheckCaseClass.Целостность_структуры_конвертируемых_данных);
            lcc.AddRange(CheckCaseFactory.GenerateCheckCases(CheckCaseClass.Целостность_конвертируемых_данных));
            int i = 0;
            bool errorsPresent = false;
            bool isTerminating = false;

            if (args != null)
            {
                if (args.Contains("-s"))
                {
                    if (File.Exists("Statistics.txt")) File.Delete("Statistics.txt");
                }
            }
            foreach (CheckCase cc in lcc)
            {
                i++;
                Console.Write(i.ToString() + ". " + cc.CheckCaseName + "...");
                cc.Analize();

                if (cc.ErrorList.Count > 0)
                {
                    errorsPresent = true;
                    Console.WriteLine("   " + "ОБНАРУЖЕНЫ ОШИБКИ!");
                    foreach (ErrorClass ec in cc.ErrorList)
                    {
                        Console.WriteLine("======");
                        Console.WriteLine(ec.ErrorName);
                        if (ec.DetailPresent)
                        {
                            Console.WriteLine("Детализация ошибки:");
                            foreach (string s in ec.Detail) Console.WriteLine(s);
                        }
                        if (ec.IsTerminating) isTerminating = true;
                        if (args != null)
                        {
                            if (args.Contains("-s"))
                            {
                                if (ec.StatisticSetPresent)
                                {
                                    foreach (Statistic s in ec.StatisticSets)
                                    {
                                        File.AppendAllText("Statistics.txt", "---", Encoding.GetEncoding(1251));
                                        string st = s.StatisticText();
                                        File.AppendAllText("Statistics.txt", st, Encoding.GetEncoding(1251));
                                    }
                                }
                            }
                        }
                    }
                }
                else
                    Console.WriteLine("   " + " ошибок не обнаружено.");
                Console.WriteLine("****************************************************************");
                if (isTerminating) break;
            }
            Console.WriteLine();
            if (errorsPresent)
            {
                Console.WriteLine("ИТОГ: В ПРОЦЕССЕ АНАЛИЗА ВЫЯВЛЕНЫ ОШИБКИ.");
                if (isTerminating)
                    Console.WriteLine("ВЫЯВЛЕННЫЕ ОШИБКИ ЯВЛЯЮТСЯ ТЕРМИНАЛЬНЫМИ. АНАЛИЗ БЫЛ ПРЕРВАН.");
            }
            else
                Console.WriteLine("ИТОГ: в процессе анализа ошибок не выявлено.");

            Console.ReadLine();
        }
    }
}
