using UnityEngine;

namespace Appalachia.Utility.MultiScene
{
    [System.Serializable]
    public struct GenericData
    {
        public GenericData(Object obj)
        {
            @object = obj;
            @string = null;
        }

        public GenericData(string str)
        {
            @object = null;
            @string = str;
        }

        #region Fields

        public Object @object;
        public string @string;

        #endregion

        public static implicit operator GenericData(Object obj)
        {
            return new GenericData(obj);
        }

        public static implicit operator GenericData(string str)
        {
            return new GenericData(str);
        }
    }
}
