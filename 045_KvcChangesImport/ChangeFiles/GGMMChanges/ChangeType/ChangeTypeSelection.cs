using System;
using System.Linq;
using System.Reflection;

namespace _045_KvcChangesImport.ChangeFiles.GGMMChanges.ChangeType
{
    public static class ChangeTypeSelection
    {
        private static readonly IGGMMChangeTypeFactory[] Factories;

        static ChangeTypeSelection()
        {
            var factoryType = typeof(IGGMMChangeTypeFactory);
            var factoryTypes = Assembly.GetAssembly(factoryType).GetTypes().Where(t => factoryType.IsAssignableFrom(t) && !t.IsInterface).ToArray();
            Factories = new IGGMMChangeTypeFactory[factoryTypes.Length];
            for (int i = 0; i < factoryTypes.Length; i++)
            {
                Factories[i] = (IGGMMChangeTypeFactory)factoryTypes[i].GetConstructor(Type.EmptyTypes).Invoke(null);
            }
        }

        public static IGGMMChangeType GetChangeTypeByFactory(GGMMChangeRecord record)
        {
            foreach (var factory in Factories)
            {
                var selectedFactory = factory.Create(record);
                if (selectedFactory != null) return selectedFactory;
            }
            throw new Exception($"Не удалось определить фабрику для записи\r\nНачисление = {record.ТипНачисления}\r\nГрафа = {record.Графа}\r\nЛС = {record.LsKvc.Ls}");
        }
    }
}
