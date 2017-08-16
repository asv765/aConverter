using System;
using System.Collections.Generic;
using System.Windows.Forms;
using aConverterClassLibrary;
using _045_KvcChangesImport.ChangeFiles.Abstract;

namespace _045_KvcChangesImport
{
    public abstract class KvcChangesCase : ConvertCase
    {
        protected virtual int AdditionalSteps => 0;
        protected abstract string FilesMask { get; }
        protected abstract string FileDialogTitle { get; }
        protected abstract void DoKvcChangesConvert();

        protected string[] ImportingFiles;

        public override void DoConvert()
        {
            var fileDialog = CreateOpenFileDialog();
            if (fileDialog.ShowDialog() != DialogResult.OK) return;
            ImportingFiles = fileDialog.FileNames;

            var importSettingsForm = new Form_ImportSettings();
            importSettingsForm.ShowDialog();
            ChangesConsts.ImportYear = importSettingsForm.ImportYear;
            ChangesConsts.ImportMonth = importSettingsForm.ImportMonth;

            StepCounter.SetConvertCase(this);
            StepCounter.SetStepsCount(ImportingFiles.Length + AdditionalSteps);

            DoKvcChangesConvert();
        }

        protected void ConvertChanges<T>(Action<IChangeRecord> convertAction) where T : class, IChangeFileFactory, new()
        {
            var factory = new T();
            foreach (var fileName in ImportingFiles)
            {
                var fileInfo = factory.Create(fileName);
                try
                {
                    fileInfo.ConvertFile(convertAction);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + $"\r\nФайл: {fileName}", ex);
                }
            }
        }

        private OpenFileDialog CreateOpenFileDialog()
        {
            return new OpenFileDialog
            {
                Filter = FilesMask,
                Title = FileDialogTitle,
                Multiselect = true,
            };
        }

        public new void SetStepsCount(int maximumSteps)
        {
            base.SetStepsCount(maximumSteps);
        }

        public new void StepStart(int maximumSteps)
        {
            base.StepStart(maximumSteps);
        }

        public new void StepFinish()
        {
            base.StepFinish();
        }

        public new void Iterate()
        {
            base.Iterate();
        }

        public static void FreeListMemory<T>(List<T> list)
        {
            list.Clear();
            list.TrimExcess();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
