namespace Appalachia.Utility.Logging.Contexts
{
    public abstract class AppaLogContext<T> : AppaLogContextBase
        where T : AppaLogContext<T>, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
    }
}
