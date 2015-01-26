using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aConverterClassLibrary
{
    // Назначение класса и логика его работы
    // 1. Предназначен для упрощенного перекодирования совокупности параметров потребления
    //    в параметры потребления целевой БД.
    // 2. Метод парсер. На вход валится Dictionary с парами "наименование параметра" (строка), "значение" (decimal или Int)
    //    и строка с выражением. Результат вычисления - логическое значение true или false, которое
    //    показывает, удовлетворяет набор параметров или нет.
    // 3. Еще один метод более высокого уровня по отношению к парсеру. На входе два Dictionary.
    //     - аналогичен Dictionary из предыдущего метода;
    //     - второй состоит из пар - "строка с выражением", "код" (целое число).
    //    Метод возвращает список кодов из второго Dictionary, значение строк которых удовлетворяет заданному 
    //    логическому выражению.
    // 4. Вспомогательный статический метод, который по переданной DataRow формирует Dictionary
    //    по полям, имеющим числовой значение.

    // Перед началом конвертации формируется текстовый файл с парами значений:
    //    L<КОД ПАРАМЕТРА>_V<ЗНАЧЕНИЕ ПАРАМЕТРА> = <СТРОКА ЛОГИЧЕСКОГО ВЫРАЖЕНИЯ>
    // Данный файл преобразуется в Dictionary, который поступает на вход метода номер 3.
    // В строке логического выражения    


    /// <summary>
    /// Абстрактный класс для парсинга параметров потребления
    /// </summary>
    public abstract class QualityParamsParser
    {
        // Метода
    }

    public class QualityParam
    {
        public QualityParam(byte AParamCD, byte AParamValue)
        {
            paramCd = AParamCD;
            paramValue = AParamValue;
        }

        private byte paramCd;
        /// <summary>
        /// Код параметра
        /// </summary>
        public byte ParamCd
        {
            get { return paramCd; }
            set { paramCd = value; }
        }

        private byte paramValue;
        /// <summary>
        /// Значение параметра
        /// </summary>
        public byte ParamValue
        {
            get { return paramValue; }
            set { paramValue = value; }
        }

        /// <summary>
        /// Составное значение paramCd * 1000 + paramValue одним числом
        /// </summary>
        public UInt32 ComplexValue
        {
            get
            {
                return Convert.ToUInt32(paramCd * 1000 + paramValue);
            }
            set
            {
                paramCd = Convert.ToByte((value - (value % 1000)) / 1000);
                paramValue = Convert.ToByte(value % 1000);
            }
        }

        /// <summary>
        /// Преобразует значение величины к нужному Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetEnumValue<T>()
        {
            return (T)Enum.Parse(typeof(T), ComplexValue.ToString());
        }
    }
}
