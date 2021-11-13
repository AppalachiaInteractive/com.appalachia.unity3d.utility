namespace Appalachia.Utility.Extensions.Cleaning
{
    public class StringCombiner : StringCombinerBase<StringCombiner>
    {
        public StringCombiner(ExecuteClean action, int capacity = 100) : base(action, capacity)
        {
        }
    }
}