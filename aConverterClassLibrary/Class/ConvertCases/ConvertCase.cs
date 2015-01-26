using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbfClassLibrary;

namespace aConverterClassLibrary
{
    public abstract class ConvertCase : IDisposable
    {
        private string convertCaseName = "ИМЯ НЕ ОПРЕДЕЛЕНО";
        /// <summary>
        /// Наименование варианта корректировки
        /// </summary>
        public string ConvertCaseName
        {
            get { return convertCaseName; }
            set { convertCaseName = value; }
        }

        private bool ischecked = false;
        /// <summary>
        /// Выбран для конвертации
        /// </summary>
        public bool IsChecked
        {
            get { return ischecked; }
            set { ischecked = value; }
        }

        private bool isInitial = false;
        /// <summary>
        /// Является инициализируюим - выполняется первым, после выполнения добавляется в списокъ
        /// InitialConvertCases других ConvertCase-ов
        /// </summary>
        public bool IsInitial
        {
            get { return isInitial; }
            set { isInitial = value; }
        }

        private List<ConvertCase> initialConvertCases = new List<ConvertCase>();
        /// <summary>
        /// Список инициализирующих ConvertCase-ов. Удобны, например, для однократной загрузки 
        /// исходных данных
        /// </summary>
        public List<ConvertCase> InitialConvertCases
        {
            get { return initialConvertCases; }
            set { initialConvertCases = value; }
        }

        private int position;
        /// <summary>
        /// Позиция внутри списка шагов конвертации (определяет порядок выполнения)
        /// </summary>
        public int Position
        {
            get { return position; }
            set { position = value; }
        }

        private ConvertCaseStatus result = ConvertCaseStatus.Шаг_не_выполнен;
        /// <summary>
        /// Результаты выполнения варианта корректировки
        /// </summary>
        public ConvertCaseStatus Result
        {
            get { return result; }
            set { result = value; }
        }

        private string errorMessage;
        /// <summary>
        /// Сообщение об ошибке (если есть)
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        private string sourceDir;
        /// <summary>
        /// Каталог с исходными данными для конвертации
        /// </summary>
        protected string SourceDir
        {
            get { return sourceDir; }
            set { sourceDir = value; }
        }

        /// <summary>
        /// Менеджер для работы с DBF-файлами источника
        /// </summary>
        protected TableManager tmsource;

        private string destDir;
        /// <summary>
        /// Каталог с результатами конвертации
        /// </summary>
        protected string DestDir
        {
            get { return destDir; }
            set { destDir = value; }
        }

        /// <summary>
        /// Менеджер для работы с DBF-файлами результата
        /// </summary>
        public TableManager tmdest;

        private bool visible = true;
        /// <summary>
        /// Признак, является ли класс "видимым" в списке шагов конвертации
        /// </summary>
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public delegate void SetStepsCountHandler(int MaximumSteps);
        /// <summary>
        /// Событие, возникающее, когда становится известным максимальное количество шагов
        /// </summary>
        public event SetStepsCountHandler onSetStepsCount;
        public void SetStepsCount(int MaximumSteps)
        {
            if (onSetStepsCount != null) onSetStepsCount(MaximumSteps);
        }

        public delegate void StepStartHandler(int ProgressBarMaximumValue);
        /// <summary>
        /// Событие, возникающее, когда становится известным максимальное количество итераций
        /// </summary>
        public event StepStartHandler onStepStart;
        protected void StepStart(int MaximumSteps)
        {
            if (onStepStart != null) onStepStart(MaximumSteps);
        }

        public delegate void IterateHandler();
        /// <summary>
        /// Событие, возникающее при каждой очередной итерации конвертера.
        /// </summary>
        public event IterateHandler onIterate;
        protected void Iterate()
        {
            if (onIterate != null) onIterate(); 
        }

        public delegate void StepFinishHandler();
        /// <summary>
        /// Событие, возникающее, когда очередной шаг завершен
        /// </summary>
        public event StepFinishHandler onStepFinish;
        protected void StepFinish()
        {
            if (onStepFinish != null) onStepFinish();
        }

        public delegate bool ErrorOpenFile(object sender, Exception errorMessage);
        /// <summary>
        /// Обработчик событий ошибок при открытии файла
        /// </summary>
        public event ErrorOpenFile ErrorOpenFileEvent;

        public void InitializeManager(string ASourceDir, string ADestDir)
        {
            SourceDir = ASourceDir;
            tmsource = new TableManager(ASourceDir);
            tmsource.Init();
            tmsource.ErrorOpenFileEvent += new TableManager.ErrorOpenFile(tm_ErrorOpenFileEvent);
            DestDir = ADestDir;
            tmdest = new TableManager(ADestDir);
            tmdest.Init();
            tmdest.ErrorOpenFileEvent += new TableManager.ErrorOpenFile(tm_ErrorOpenFileEvent);
        }

        bool tm_ErrorOpenFileEvent(object sender, Exception errorMessage)
        {
            if (ErrorOpenFileEvent != null)
                return ErrorOpenFileEvent(sender, errorMessage);
            else
                return false;
        }

        public abstract void DoConvert();

        public void Dispose()
        {
            tmdest.ErrorOpenFileEvent -= new TableManager.ErrorOpenFile(tm_ErrorOpenFileEvent);
            tmdest.Dispose();
            tmsource.ErrorOpenFileEvent -= new TableManager.ErrorOpenFile(tm_ErrorOpenFileEvent);
            tmsource.Dispose();
        }
    }

    public enum ConvertCaseStatus
    {
        Шаг_не_выполнен = 0,
        Шаг_выполнен_успешно = 1,
        Ошибка_при_выполнении_шага = 2
    }
}
