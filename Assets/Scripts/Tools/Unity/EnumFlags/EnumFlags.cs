using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Unity.EnumFlags
{
    [Serializable]
    public class EnumFlags<T> where T : Enum
    {
        [SerializeField] private int _flags;

        public IEnumerable<T> GetCollection()
        {
            var list = new List<T>();
            
            for (var flagIterator = 0; flagIterator < 32; flagIterator++)
            {
                var bitValue = 1 << flagIterator;

                if ((_flags & bitValue) == 0) 
                    continue;

                if (!Enum.IsDefined(typeof(T), bitValue)) 
                    continue;
                
                var enumValue = (T)Enum.ToObject(typeof(T), bitValue);
                list.Add(enumValue);
            }
            return list;
        }
    }
}