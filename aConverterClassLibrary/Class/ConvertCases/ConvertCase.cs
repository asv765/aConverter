using System;
using System.Collections.Generic;
using System.Linq;
using aConverterClassLibrary.RecordsDataAccessORM;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public abstract class ConvertCase : IDisposable
    {
        private string _convertCaseName = "ИМЯ НЕ ОПРЕДЕЛЕНО";
        /// <summary>
        /// Наименование варианта корректировки
        /// </summary>
        public string ConvertCaseName//--------------------------------------------после 23
        {
            get { return _convertCaseName; }
            set { _convertCaseName = value; }
        }

        /// <summary>
        /// Выбран для конвертации
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Является инициализируюим - выполняется первым, после выполнения добавляется в список
        /// InitialConvertCases других ConvertCase-ов
        /// </summary>
        public bool IsInitial { get; set; }

        /// <summary>
        /// Список инициализирующих ConvertCase-ов. Удобны, например, для однократной загрузки 
        /// исходных данных
        /// </summary>
        public List<ConvertCase> InitialConvertCases { get; set; }

        /// <summary>
        /// Позиция внутри списка шагов конвертации (определяет порядок выполнения)
        /// </summary>
        public int Position { get; set; }

        private ConvertCaseStatus _result = ConvertCaseStatus.Шаг_не_выполнен;
        /// <summary>
        /// Результаты выполнения варианта корректировки
        /// </summary>
        public ConvertCaseStatus Result
        {
            get { return _result; }
            set { _result = value; }
        }

        /// <summary>
        /// Сообщение об ошибке (если есть)
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Каталог с исходными данными для конвертации
        /// </summary>
        protected string SourceDir { get; set; }

        /// <summary>
        /// Менеджер для работы с DBF-файлами источника
        /// </summary>
        protected TableManager Tmsource;

        private bool _visible = true;

        public ConvertCase()
        {
            InitialConvertCases = new List<ConvertCase>();
            IsChecked = false;
        }

        /// <summary>
        /// Признак, является ли класс "видимым" в списке шагов конвертации
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public delegate void SetStepsCountHandler(int maximumSteps);
        /// <summary>
        /// Событие, возникающее, когда становится известным максимальное количество шагов
        /// </summary>
        public event SetStepsCountHandler OnSetStepsCount;
        public void SetStepsCount(int maximumSteps)
        {
            if (OnSetStepsCount != null) OnSetStepsCount(maximumSteps);
        }

        public delegate void StepStartHandler(int progressBarMaximumValue);
        /// <summary>
        /// Событие, возникающее, когда становится известным максимальное количество итераций
        /// </summary>
        public event StepStartHandler OnStepStart;
        protected void StepStart(int maximumSteps)
        {
            if (OnStepStart != null) OnStepStart(maximumSteps);
        }

        public delegate void IterateHandler();
        /// <summary>
        /// Событие, возникающее при каждой очередной итерации конвертера.
        /// </summary>
        public event IterateHandler OnIterate;
        protected void Iterate()
        {
            if (OnIterate != null) OnIterate(); 
        }

        public delegate void StepFinishHandler();
        /// <summary>
        /// Событие, возникающее, когда очередной шаг завершен
        /// </summary>
        public event StepFinishHandler OnStepFinish;
        protected void StepFinish()//--------------------------------------------------------неведомая вещь 1
        {
            if (OnStepFinish != null) OnStepFinish();
        }

        public delegate bool ErrorOpenFile(object sender, Exception errorMessage);
        /// <summary>
        /// Обработчик событий ошибок при открытии файла ---------------------------------------------------1
        /// </summary>
        public event ErrorOpenFile ErrorOpenFileEvent;

        public void InitializeManager(string aSourceDir)
        {
            SourceDir = aSourceDir;
            Tmsource = new TableManager(aSourceDir);
            Tmsource.Init();
            Tmsource.ErrorOpenFileEvent += tm_ErrorOpenFileEvent;
        }

        bool tm_ErrorOpenFileEvent(object sender, Exception errorMessage)
        {
            if (ErrorOpenFileEvent != null)
                return ErrorOpenFileEvent(sender, errorMessage);
            return false;
        }

        public abstract void DoConvert();

        public void SaveList<T>(IEnumerable<T> listToSave, int commitStep, bool countSteps = true)
        {
            var list = listToSave.ToList();
            int stepsCount = (list.Count / commitStep) + 1;
            if (countSteps) StepStart(stepsCount);
            while (list.Count > 0)
            {
                int count = Math.Min(commitStep, list.Count);
                var sublist = new List<object>();
                for (int i = 0; i < count; i++)
                {
                    sublist.Add(list[i]);
                }
                list.RemoveRange(0, count);
                SaveContextPart(sublist);
                if (countSteps) Iterate();
            }
            if (countSteps) StepFinish();
        }

        public void SaveContextPart(IEnumerable<object> entitiesList)
        {
            using (var context = new AbonentConvertationEntitiesModel(aConverter_RootSettings.FirebirdStringConnection))
            {
                context.Add(entitiesList);
                context.SaveChanges();
            }
        }


        public void Dispose()
        {
            Tmsource.ErrorOpenFileEvent -= tm_ErrorOpenFileEvent;
            Tmsource.Dispose();
        }
    }

    public enum ConvertCaseStatus
    {
        Шаг_не_выполнен = 0,
        Шаг_выполнен_успешно = 1,
        Ошибка_при_выполнении_шага = 2
    }
}
