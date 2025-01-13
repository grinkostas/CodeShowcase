using System;
using UnityEngine;
using System.Collections.Generic;

namespace Core.Extentions
{
    [Serializable]
    public class SerializableDictionary<T1, T2>
    {
        [SerializeField] private List<TempTuple> _tempTuples;

        [Serializable]
        private class TempTuple
        {
            public T1 Key;
            public T2 Value;
        }

        private Dictionary<T1, T2> _dictionary;
        public Dictionary<T1, T2> dictionary
        {
            get
            {
                if (_dictionary == null)
                {
                    _dictionary = new Dictionary<T1, T2>();
                    foreach (var tempTuple in _tempTuples)
                    {
                        _dictionary.TryAdd(tempTuple.Key, tempTuple.Value);
                    }
                }

                return _dictionary;
            }
        }

        public T2 this[T1 key] => dictionary[key];
    }
}
