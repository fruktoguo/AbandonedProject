using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace YuoTools
{
    public class YuoInput : SingletonSerializedMono<YuoInput>
    {
        public Dictionary<string, InputItem> all = new Dictionary<string, InputItem>();
        /// <summary>
        /// 遍历所有的输入项
        /// </summary>
        /// <param name="item"></param>
        void UpdateItem(InputItem item)
        {
            //上一次持续按下的时间不为零
            if (item.LastOnTime > 0.0001f)
            {
                if (item.OnTime - item.LastOnTime < 0.001f)
                {
                    //说明up事件没有正确触发,手动触发一次up事件
                    Up(item);
                }
            }

            //如果按下了这些键,则这个按键不会被监听
            foreach (var befor in item.Befor)
            {
                if (GetItem(befor).Down)
                {
                    return;
                }
            }
            if (item.canInput != null)
                //判断是否能按下,不就就跳出
                if (!item.canInput())
                    return;

            //判断对应的按键是否按下
            if (Input.GetKeyDown(item.key))
            {
                item.OnDown?.Invoke();
                //更显点击时间
                item.LastClickTime = Time.unscaledTime;
                item.Down = true;
            }
            if (Input.GetKey(item.key))
            {
                //意外事件  没按下就开始触发持续按下事件
                if (item.UseMustDown && !item.Down) return;
                //按下持续的时间
                item.OnTime += Time.unscaledDeltaTime;
                item.OnHold?.Invoke();
            }
            if (Input.GetKeyUp(item.key))
            {
                if (item.UseMustDown && !item.Down) return;
                Up(item);
            }
        }
        public void Pause()
        {
            Stop = true;
        }
        public void Resume()
        {
            Stop = false;
        }
        void Up(InputItem item)
        {
            item.OnUp?.Invoke();
            item.OnTime = 0;
            item.LastOnTime = 0;
            item.Down = false;
        }
        public InputItem GetItem(string key)
        {
            if (!all.ContainsKey(key))
            {
                all.Add(key, new InputItem(key));
            }
            return all[key];
        }
        public bool Stop;
        private void Update()
        {
            if (Stop) return;
            foreach (var item in all.Values)
            {
                UpdateItem(item);
            }
        }
        public void Add(InputItem item)
        {
            if (!all.ContainsKey(item.KeyName))
            {
                all.Add(item.KeyName, item);
            }
            else
            {
                Debug.LogWarning("已经存在相同的键名:" + item.KeyName);
                var input = all[item.KeyName];

                input.key = item.key;
                input.OnDown += item.OnDown;
                input.OnUp += item.OnUp;
                input.canInput += item.canInput;
                input.OnHold += item.OnHold;
            }
        }
        public void AddDown(string key, UnityAction down)
        {
            if (!all.ContainsKey(key))
                all.Add(key, new InputItem(key));
            all[key].OnDown += down;
        }
        public void RemoveDown(string key, UnityAction down)
        {
            if (!all.ContainsKey(key))
                all.Add(key, new InputItem(key));
            all[key].OnDown -= down;
        }
        public void Save()
        {

        }
        [System.Serializable]
        public class InputItem
        {
            public InputItem(string name)
            {
                KeyName = name;
            }
            public InputItem(string name, KeyCode key)
            {
                KeyName = name;
                this.key = key;
            }
            /// <summary>
            /// 按键名称
            /// </summary>
            public string KeyName;

            /// <summary>
            /// 按键键码
            /// </summary>
            public KeyCode key;

            [ReadOnly]
            public float OnTime;

            [ReadOnly]
            public float LastOnTime;

            [ReadOnly]
            public float LastClickTime;
            /// <summary>
            /// 按下时持续触发
            /// </summary>
            [NonSerialized]
            public UnityAction OnHold;

            /// <summary>
            /// 按下时触发一次
            /// </summary>
            [NonSerialized]
            public UnityAction OnDown;

            /// <summary>
            /// 抬起时触发
            /// </summary>
            [NonSerialized]
            public UnityAction OnUp;

            /// <summary>
            /// 判断是否可用
            /// </summary>
            [NonSerialized]
            public BoolAction canInput;

            /// <summary>
            /// 忽略按键名称,当目标按键按下时,这个按键不会触发
            /// </summary>
            public List<string> Befor = new List<string>();
            internal bool Down;
            internal bool UseMustDown;

            public void AddBefor(string key)
            {
                if (!Befor.Contains(key))
                {
                    Befor.Add(key);
                }
            }
            public InputItem SetCanInput(BoolAction canInput)
            {
                this.canInput = canInput;
                return this;
            }
        }
    }
}