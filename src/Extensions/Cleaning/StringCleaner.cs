namespace Appalachia.Utility.Extensions.Cleaning
{
    public class StringCleaner : StringCleanerBase<StringCleaner>
    {
        public StringCleaner(ExecuteClean action, int capacity = 100) : base(action, capacity)
        {
        }
    }
}
