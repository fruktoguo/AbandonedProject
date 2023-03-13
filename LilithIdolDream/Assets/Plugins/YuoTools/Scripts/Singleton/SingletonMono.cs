using System.Threading;

using UnityEngine;

namespace YuoTools
{
    public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
                return instance;
            }
            protected set => instance = value;
        }

        public virtual void Awake()
        {
            instance = this as T;
        }
    }
}