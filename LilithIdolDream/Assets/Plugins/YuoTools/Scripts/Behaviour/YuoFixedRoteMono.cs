using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace YuoTools
{
    public abstract class YuoFixedRoteMono : MonoBehaviour
    {
        protected float rote = 1;

        protected virtual void Awake()
        {
            if (rote < Time.unscaledDeltaTime)
                rote = Time.unscaledDeltaTime;
        }

        private float timer;

        // Update is called once per frame
        private void Update()
        {
            timer += Time.unscaledDeltaTime;
            if (timer > rote)
            {
                Fixed(timer);
                timer = 0;
            }
        }

        protected abstract void Fixed(float space);
    }
}