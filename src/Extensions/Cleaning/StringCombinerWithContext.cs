namespace Appalachia.Utility.Extensions.Cleaning
{
    public class StringCombinerWithContext<T1> : StringCombinerBase<StringCombinerWithContext<T1>>
    {
        public StringCombinerWithContext(T1 context1, ExecuteClean action, int capacity = 100) : base(
            action,
            capacity
        )
        {
            this.context1 = context1;
        }

        #region Fields and Autoproperties

        public T1 context1;

        #endregion
    }

    public class StringCombinerWithContext<T1, T2> : StringCombinerBase<StringCombinerWithContext<T1, T2>>
    {
        public StringCombinerWithContext(
            T1 context1,
            T2 context2,
            ExecuteClean action,
            int capacity = 100) : base(action, capacity)
        {
            this.context1 = context1;
            this.context2 = context2;
        }

        #region Fields and Autoproperties

        public T1 context1;
        public T2 context2;

        #endregion
    }

    public class
        StringCombinerWithContext<T1, T2, T3> : StringCombinerBase<StringCombinerWithContext<T1, T2, T3>>
    {
        public StringCombinerWithContext(
            T1 context1,
            T2 context2,
            T3 context3,
            ExecuteClean action,
            int capacity = 100) : base(action, capacity)
        {
            this.context1 = context1;
            this.context2 = context2;
            this.context3 = context3;
        }

        #region Fields and Autoproperties

        public T1 context1;
        public T2 context2;
        public T3 context3;

        #endregion
    }
}
