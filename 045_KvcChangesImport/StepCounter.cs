namespace _045_KvcChangesImport
{
    public class StepCounter
    {
        private static StepCounter _instance;

        private readonly KvcChangesCase _currentCc;

        private StepCounter(KvcChangesCase convertCase)
        {
            _currentCc = convertCase;
            _instance = this;
        }

        public static void SetConvertCase(KvcChangesCase convertCase)
        {
            _instance = new StepCounter(convertCase);
        }

        public static void SetStepsCount(int maximumSteps)
        {
            _instance._currentCc.SetStepsCount(maximumSteps);
        }

        public static void StepStart(int maximumSteps)
        {
            _instance._currentCc.StepStart(maximumSteps);
        }

        public static void StepFinish()
        {
            _instance._currentCc.StepFinish();
        }

        public static void Iterate()
        {
            _instance._currentCc.Iterate();
        }
    }
}
