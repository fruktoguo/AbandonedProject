using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace YuoTools
{
    public class InputManger : SingletonMono<InputManger>
    {
        public Dictionary<string, InputItem> all = new Dictionary<string, InputItem>();
        public List<InputItem> _all = new List<InputItem>();

        private void Update()
        {
            UpdateAction();
        }

        public UnityAction BeforeUpdate;
        private void UpdateAction()
        {
            //每次开始检测前调用
            BeforeUpdate?.Invoke();
            foreach (var item in all)
            {
                if (item.Value.LastOnTime > 0.0001f)
                {
                    if (item.Value.OnTime - item.Value.LastOnTime < 0.0001f)
                    {
                        Up();
                    }
                }

                foreach (var item1 in item.Value.Befor)
                {
                    if (all[item1].OnDown)
                    {
                        goto forOut;
                    }
                }

                item.Value.LastOnTime = item.Value.OnTime;
                if (item.Value.canInput != null)
                    //判断是否能按下,不就就跳出
                    if (!item.Value.canInput())
                        goto forOut;
                //判断对应的按键是否按下
                if (Input.GetKeyDown(item.Value.key))
                {
                    item.Value.Down?.Invoke();
                    //重置长摁触发事件
                    if (item.Value.MaxIsInvoke)
                    {
                        item.Value.MaxIsInvoke = false;
                    }
                    //连击触发
                    if (Time.unscaledTime - item.Value.LastClickTime < item.Value.ComboTime)
                    {
                        item.Value.ComboNum++;
                        item.Value.ComboAction?.Invoke(item.Value.ComboNum);
                    }
                    //连击重置
                    else item.Value.ComboNum = 0;
                    //更显点击时间
                    item.Value.LastClickTime = Time.unscaledTime;
                    item.Value.IsKeyDown = true;
                }
                if (Input.GetKey(item.Value.key))
                {
                    //意外事件  没按下就开始触发持续按下事件
                    if (item.Value.UseMustDown && !item.Value.IsKeyDown)
                        goto forOut;
                    //按下持续的时间
                    item.Value.OnTime += Time.unscaledDeltaTime;
                    //如果需要摁下一定时间自动释放
                    if (item.Value.OpenMax)
                    {
                        if (item.Value.OnTime >= item.Value.OnMaxTime)
                        {
                            //没触发过就触发一次
                            if (!item.Value.MaxIsInvoke)
                            {
                                item.Value.OnMax?.Invoke();
                                item.Value.OnBreak?.Invoke(item.Value.OnMaxTime);
                                item.Value.MaxIsInvoke = true;
                            }
                        }
                        else
                        {
                            item.Value.OnDown = true;
                            item.Value.On?.Invoke();
                        }
                    }
                    else
                    {
                        item.Value.OnDown = true;
                        item.Value.On?.Invoke();
                    }
                }
                if (Input.GetKeyUp(item.Value.key))
                {
                    if (item.Value.UseMustDown && !item.Value.IsKeyDown) goto forOut;
                    Up();
                }
            forOut: continue;
                void Up()
                {
                    if (!item.Value.MaxIsInvoke)
                    {
                        if (item.Value.OpenMax)
                        {
                            if (item.Value.OnTime < item.Value.OnMaxTime)
                            {
                                item.Value.OnBreak?.Invoke(item.Value.OnTime);
                            }
                        }
                        item.Value.Up?.Invoke();
                    }
                    item.Value.OnTime = 0;
                    item.Value.LastOnTime = 0;
                    item.Value.OnDown = false;
                    item.Value.IsKeyDown = false;
                }
            }
        }

        public void Add(string KeyName, InputItem item)
        {
            if (!all.ContainsKey(KeyName)) { item.KeyName = KeyName; all.Add(KeyName, item); _all.Add(item); }
            else all[KeyName] = item;
        }
    }

    [Serializable]
    public class InputItem
    {
        public string KeyName;
        public KeyCode key;

        [ReadOnly]
        public bool IsKeyDown;

        [HideInInspector]
        public bool UseMustDown = true;

        [ReadOnly]
        public bool OnDown;

        [ReadOnly]
        public bool OpenMax;

        [ReadOnly]
        public bool MaxIsInvoke;

        [ReadOnly]
        public float OnTime;

        [ReadOnly]
        public float LastOnTime;

        public float OnMaxTime;

        [ReadOnly]
        public float LastClickTime;

        [ReadOnly]
        public int ComboNum;

        public int MaxComboNum;
        public float ComboTime;

        /// <summary>
        /// 按下时持续触发
        /// </summary>
        public UnityAction On;

        /// <summary>
        /// 到达持续按下最大值时触发
        /// </summary>
        public UnityAction OnMax;

        /// <summary>
        /// 被打断时触发,传入按下的时长
        /// </summary>
        public UnityAction<float> OnBreak;

        /// <summary>
        /// 按下时触发一次
        /// </summary>
        public UnityAction Down;

        /// <summary>
        /// 抬起时触发
        /// </summary>
        public UnityAction Up;

        /// <summary>
        /// 判断是否可用
        /// </summary>
        public BoolAction canInput;

        /// <summary>
        /// 连续点击时触发,传入点击次数
        /// </summary>
        public UnityAction<int> ComboAction;

        /// <summary>
        /// 忽略按键名称,当目标按键按下时,这个按键不会触发
        /// </summary>
        public List<string> Befor = new List<string>();

        public InputItem()
        {
        }

        /// <summary>
        /// 设置最大持续按下事件一般用于蓄力233
        /// </summary>
        /// <param name="MaxTime">最大时间</param>
        /// <param name="action">到达最大时间时触发的事件</param>
        /// <returns></returns>
        public InputItem SetOnMax(float MaxTime, UnityAction action = null, UnityAction<float> OnBreak = null)
        {
            OpenMax = true;
            OnMaxTime = MaxTime;
            OnMax = action;
            this.OnBreak = OnBreak;
            return this;
        }

        public InputItem SetCanInput(BoolAction canInput)
        {
            this.canInput = canInput;
            return this;
        }

        public InputItem SetCombo(int max, float comboTime = 0.2f, UnityAction<int> action = null)
        {
            MaxComboNum = max;
            ComboTime = comboTime;
            ComboAction = action;
            return this;
        }
    }
}