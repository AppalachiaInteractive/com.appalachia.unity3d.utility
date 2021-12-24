using System;
using System.Collections.Generic;
using System.Text;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Appalachia.Utility.DataStructures.Dictionaries
{
    /// <summary>
    ///     Open Addressing Data Structure
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class OpenAddressingHashTable<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        /// <summary>
        ///     CONSTRUCTOR
        /// </summary>
        public OpenAddressingHashTable(int size)
        {
            _size = size;
            _loadFactor = 0.40;
            _inTable = 0;
            _table = new OAHashEntry<TKey, TValue>[_size];
            _keys = new List<TKey>();
            _values = new List<TValue>();

            //initialize all values to -1
            for (var i = 0; i < _table.Length; i++)
            {
                //initialize each slot
                _table[i] = new OAHashEntry<TKey, TValue>(default(TKey), default(TValue), false);
            }
        }

        #region Fields and Autoproperties

        private double _loadFactor { get; set; }
        private int _inTable { get; set; }

        private int _size { get; set; }
        private List<TKey> _keys { get; set; }
        private List<TValue> _values { get; set; }
        private OAHashEntry<TKey, TValue>[] _table { get; set; }

        #endregion

        //finds the key and returns index in the table
        public int search(TKey key)
        {
            var i = 0;

            do
            {
                //calculate index
                var index = _double_hash(key, i);
                if (Equals(_table[index].key, key))
                {
                    return index;
                }

                i++;
            } while (i < _size);

            return -1;
        }

        private int _double_hash(TKey key, int i)
        {
            var hash_value = 0;
            var second_hash_value = 0;
            var slot = 0;

            //grabs hash value for a string
            if (typeof(TKey) == typeof(string))
            {
                //https://stackoverflow.com/questions/4092393/value-of-type-t-cannot-be-converted-to
                var newTkeyString = key;
                var newTkeyString2 = (string)(object)newTkeyString;

                //https://stackoverflow.com/questions/400733/how-to-get-ascii-value-of-string-in-c-sharp
                var asciiBytes = Encoding.ASCII.GetBytes(newTkeyString2);

                var string_value = 0;
                foreach (var bite in asciiBytes)
                {
                    string_value += bite;
                }

                //calculates first hash values
                hash_value = Convert.ToInt32(string_value) % _size;

                //calculate second hash value
                second_hash_value = 1 + (Convert.ToInt32(string_value) % (_size - 1));
            }

            //grabs a hash value for a char
            else if (typeof(TKey) == typeof(char))
            {
                //https://stackoverflow.com/questions/4092393/value-of-type-t-cannot-be-converted-to
                var newTkeyChar = key;
                var newTkeyChar2 = (char)(object)newTkeyChar;

                int char_value = newTkeyChar2;

                //calculates first hash values
                hash_value = Convert.ToInt32(char_value) % _size;

                //calculate second hash value
                second_hash_value = 1 + (Convert.ToInt32(char_value) % (_size - 1));
            }
            else
            {
                //calculates first hash values
                hash_value = Convert.ToInt32(key) % _size;

                //calculate second hash value
                second_hash_value = 1 + (Convert.ToInt32(key) % (_size - 1));
            }

            //slot index based on first hash value, second hash value as an offset based on a counter, will also guarentee that the slot will be within the range 0 to size
            slot = (hash_value + (i * second_hash_value)) % _size;

            return slot;
        }

        //doubles the size of the table
        private void _expand()
        {
            //will hold contents of _table to copy over
            var temp = new OAHashEntry<TKey, TValue>[_size];
            temp = _table;

            //double the size and rehash
            _size *= 2;
            var exp = new OAHashEntry<TKey, TValue>[_size];
            for (var i = 0; i < exp.Length; i++)
            {
                //initialize each slot
                exp[i] = new OAHashEntry<TKey, TValue>(default(TKey), default(TValue), false);
            }

            _inTable = 0;
            _table = exp;

            //rehash over the newly sized table
            for (var i = 0; i < temp.Length; i++)
            {
                //this should rehash since the size is now doubled so the hashing will be different
                //inserts the key into _table
                Add(temp[i].key, temp[i].value);
            }
        }

        //rehashes table
        private void _rehash()
        {
            //will hold contents of _table to copy over
            var temp = new OAHashEntry<TKey, TValue>[_size];
            temp = _table;

            var rehash = new OAHashEntry<TKey, TValue>[_size];
            for (var i = 0; i < rehash.Length; i++)
            {
                //initialize each slot
                rehash[i] = new OAHashEntry<TKey, TValue>(default(TKey), default(TValue), false);
            }

            _inTable = 0;
            _table = rehash;

            //rehash over the newly sized table
            for (var i = 0; i < temp.Length; i++)
            {
                //this should rehash since the size is now doubled so the hashing will be different
                //inserts the key into _table
                Add(temp[i].key, temp[i].value);
            }
        }

        #region IDictionary<TKey,TValue> Members

        public void Add(TKey key, TValue value)
        {
            var i = 0;

            //makes sure there are no duplicate keys
            if (ContainsKey(key))
            {
                return;
            }

            do
            {
                //calculate index
                var index = _double_hash(key, i);

                if (_table[index].occupied == false)
                {
                    var newEntry = new OAHashEntry<TKey, TValue>(key, value, true);
                    _keys.Add(key);
                    _values.Add(value);

                    //set value and key
                    _table[index] = newEntry;

                    //increment how many items are in the table
                    _inTable++;
                    break;
                }

                i++;
            } while (i != _size);

            //every slot is in the table is occupied
            if (_inTable == _size)
            {
                //expand and rehash
                _expand();
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        //returns value
        public TValue this[TKey key]
        {
            get
            {
                var index = search(key);

                if (index != -1)
                {
                    return _table[index].value;
                }

                throw new KeyNotFoundException();
            }
            set
            {
                if (ContainsKey(key))
                {
                    var index = search(key);

                    _table[index].value = value;

                    throw new KeyNotFoundException();
                }

                throw new KeyNotFoundException();
            }
        }

        //removes key-value pair from the table
        public bool Remove(TKey key)
        {
            if (ContainsKey(key))
            {
                //find position and reset values
                var index = search(key);
                _keys.Clear();
                _values.Clear();

                _table[index].key = default(TKey);
                _table[index].value = default(TValue);
                _table[index].occupied = false;

                //number of items in the table decreases
                _inTable--;

                //rehash table --necessary for Open Addressing since keys could have different positions due to this key
                _rehash();

                return true;
            }

            return false;
        }

        //removes key-value pair from the table
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        //clears table of all values
        public void Clear()
        {
            _keys.Clear();
            _values.Clear();
            for (var i = 0; i < _table.Length; i++)
            {
                _table[i].key = default(TKey);
                _table[i].value = default(TValue);
                _table[i].occupied = false;
            }

            _inTable = 0;
        }

        //Tries to get the value of key which might not be in the dictionary.
        public bool TryGetValue(TKey key, out TValue value)
        {
            var index = search(key);

            if (index != -1)
            {
                value = _table[index].value;

                return true;
            }

            //not found
            value = default(TValue);
            return false;
        }

        //returns true if the key is in the table
        public bool ContainsKey(TKey key)
        {
            if (search(key) != -1)
            {
                return true;
            }

            return false;
        }

        //returns true if the key is in the table
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (ContainsKey(item.Key))
            {
                return true;
            }

            return false;
        }

        //returns the number of items in the table
        public int Count => _inTable;

        //returns bool depending on whether or not the table is read only
        public bool IsReadOnly => false;

        //returns a list of keys in the table
        public ICollection<TKey> Keys => _keys;

        //returns a list of values in the table
        public ICollection<TValue> Values => _values;

        //----------------------------------------------------------------------
        //-----------------------NOT IMPLEMENTED YET----------------------------
        //----------------------------------------------------------------------

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Nested type: OAHashEntry

        /// <summary>
        ///     Open Addressing Entry
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
#pragma warning disable CS0693
        private class OAHashEntry<TKey, TValue>
#pragma warning restore CS0693
            where TKey : IComparable<TKey>
        {
            public OAHashEntry(TKey Key, TValue Value, bool occp)
            {
                key = Key;
                value = Value;
                occupied = occp;
            }

            #region Fields and Autoproperties

            public bool occupied { get; set; }
            public TKey key { get; set; }
            public TValue value { get; set; }

            #endregion
        }

        #endregion
    }
}
