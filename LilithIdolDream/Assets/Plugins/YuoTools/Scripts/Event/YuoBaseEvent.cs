using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace YuoTools
{
    public class YuoBaseEvent<T>
    {
        public T Value { get; private set; }
        public YuoEvent<T> OnAtkBefor;
        public YuoEvent<T> OnAtkAfter;
        public YuoEvent<T> OnKill;

        public YuoBaseEvent(T v)
        {
            Value = v;
            OnAtkBefor = new YuoEvent<T>(v);
        }
    }
}