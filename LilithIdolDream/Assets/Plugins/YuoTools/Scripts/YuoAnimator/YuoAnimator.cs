using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace YuoTools
{
    [RequireComponent(typeof(AnimaItem))]
    [DisallowMultipleComponent]
    public class YuoAnimator : YuoAnimaCon
    {
        [Header("默认动画")]
        public string DefAnima;

        [Header("状态机设置参数")]
        public SetData data = new SetData();

        [SerializeField]
        private Dictionary<string, AnimaActionItem> Actions = new Dictionary<string, AnimaActionItem>();

        [SerializeField]
        private Dictionary<string, AnimaConItem> animas = new Dictionary<string, AnimaConItem>();

        [Header("所有的事件")]
        public List<AnimaActionItem> AllActions = new List<AnimaActionItem>();

        [Header("当前播放的动画片段名字")]
        [ReadOnly]
        public string ClipNow = "Null";

        [Header("已经播放的时间")]
        [ReadOnly]
        public float PlayTime;

        [Header("正在播放的动画长度")]
        [ReadOnly]
        public float ClipLength;

        [Header("已经播放的次数")]
        [ReadOnly]
        public int PlayNum = 0;

        private AnimaActionItem NowAction;
        private AnimaActionItem defaulAction;

        #region 临时变量

        private int IntTemp_1 = 0;
        private string StrTemp1;
        private string StrTemp2;

        #endregion 临时变量

        protected override void Awake()
        {
            base.Awake();
            defaulAction = new AnimaActionItem("");
            NowAction = defaulAction;
            foreach (var item in AllActions)
            {
                Actions.Add(item.animaClipName, item);
            }
            Play(DefAnima);
        }

        private void Start()
        {
        }

        private void Start_()
        {
            //输出所有动画片段  (测试用)
            this.YuoDelay(() =>
            {
                foreach (var item in animas)
                {
                    foreach (var item2 in item.Value.To)
                    {
                        $"{item.Value.name} ---> {item2.anima}".Log();
                    }
                }
            }, 0);
        }

        private void FixedUpdate()
        {
            PlayAnimaForTo();
            PlayAnimaForClip();
            ReSetTrigger();
        }

        /// <summary>
        /// 重置触发器
        /// </summary>
        private void ReSetTrigger()
        {
            if (data.TempBools.Count > 0)
            {
                foreach (var item in data.TempBools)
                {
                    item.Value = item.DefValue;
                }
                data.TempBools.Clear();
            }
            if (data.TempFloats.Count > 0)
            {
                foreach (var item in data.TempFloats)
                {
                    item.Value = item.DefValue;
                }
                data.TempFloats.Clear();
            }
            if (data.TempInts.Count > 0)
            {
                foreach (var item in data.TempInts)
                {
                    item.Value = item.DefValue;
                }
                data.TempInts.Clear();
            }
        }

        /// <summary>
        /// 根据条件播放下一个动画
        /// </summary>
        private void PlayAnimaForTo()
        {
            if (animas.ContainsKey(NowAnima.name))
            {
                IntTemp_1 = 0;
                StrTemp1 = NowAnima.name;
                StrTemp2 = NowAnima.name;
                while (IntTemp_1 < data.MaxSkipNum)
                {
                    StrTemp1 = NextAnima(StrTemp1);

                    if (StrTemp1.Equals(StrTemp2))
                        break;
                    else
                        StrTemp2 = StrTemp1;

                    if (StrTemp1.Equals(NowAnima.name))
                    {
                        return;
                    }
                    IntTemp_1++;
                }
                if (NowAnima.name.Equals(StrTemp1))
                {
                    return;
                }
                Play(StrTemp1);
                PlayTime = 0;
                PlayNum = 0;
                NowAnima.name = StrTemp1;
            }
        }

        /// <summary>
        /// 根据播放的动画执行事件
        /// </summary>
        private void PlayAnimaForClip()
        {
            if (!NowAnima.clip) return;
            //正在播放的动画片段改变,调用新的动画事件
            if (NowAnima.clip.name != ClipNow)
            {
                if (Actions.ContainsKey(ClipNow)) Actions[ClipNow].BreakAction?.Invoke();
                ClipNow = NowAnima.clip.name;
                PlayTime = 0;
                PlayNum = 0;
                ClipLength = NowAnima.length;
                //$" 【{gameObject.name}】的动画片段【{Temp.AnimaClip.name}】开始播放".Log();
                NowAction.ReSetActions();
                ThisPlayAnima();
            }
            //动画不变,开始计时
            else
            {
                PlayTime += Time.fixedDeltaTime;
                foreach (var item in NowAction.actions)
                {
                    if (!item.IsPlay && PlayTime > item.time)
                    {
                        item.action?.Invoke();
                        item.IsPlay = true;
                    }
                }
                NowAction.UpdateAction?.Invoke();
                if (PlayTime >= ClipLength)
                {
                    PlayTime = 0;
                    PlayNum++;
                    if (NowAnima.IsLoop && PlayNum >= 1)
                    {
                        if (Actions.ContainsKey(ClipNow)) Actions[ClipNow].EndAction?.Invoke();
                        NowAction.ReSetActions();
                    }
                    //$"【{gameObject.name}】的动画片段【{Temp.AnimaClip.name}】播放了{PlayNum}遍".Log();
                    ThisPlayAnima();
                }
            }
            void ThisPlayAnima()
            {
                if (Actions.ContainsKey(ClipNow))
                {
                    if (Actions[ClipNow].SpeedAction != null)
                    {
                        Actions[ClipNow].animaSpeed = Actions[ClipNow].SpeedAction();
                    }
                    //是否有速度参数
                    if (!Actions[ClipNow].animaSpeed.InRange(0.999f, 1.001f))
                    {
                        ClipLength /= Actions[ClipNow].animaSpeed;
                        SetItem(ClipNow, Actions[ClipNow].animaSpeed);
                    }
                    else
                    {
                        SetItem(ClipNow);
                    }
                }
                else
                {
                    NowAction = defaulAction;
                }
            }
        }

        /// <summary>
        /// 获取下一个动画
        /// </summary>
        /// <param name="anima"></param>
        /// <returns></returns>
        private string NextAnima(string anima)
        {
            Temp.Int = int.MinValue;
            Temp.Str = anima;
            //$"{anima} :: {animas[anima].To.Count}".Log();
            if (!anima.Equals(NowAnima.name))
            {
                return anima;
            }
            foreach (var item in animas[anima].To)
            {
                //如果开启过渡
                if (item.WaitOver)
                {
                    //如果有判断项
                    if (item.can != null)
                    {
                        //判定未成功
                        if (!item.can())
                        {
                            continue;
                        }
                    }
                    if (item.UseTime)
                    {
                        if (PlayTime + PlayTime * PlayNum > item.WaitTime)
                        {
                            if (item.level > Temp.Int)
                            {
                                Temp.Str = item.anima;
                                Temp.Int = item.level;
                            }
                        }
                    }
                    else if ((PlayTime / ClipLength + PlayNum) > item.WaitTime)
                    {
                        if (item.level > Temp.Int)
                        {
                            Temp.Str = item.anima;
                            Temp.Int = item.level;
                        }
                    }
                }
                else if (item.can())
                {
                    if (item.level > Temp.Int)
                    {
                        Temp.Str = item.anima;
                        Temp.Int = item.level;
                    }
                }
            }
            if (Temp.Str != anima)
            {
                ReSetTrigger();
            }
            return Temp.Str;
        }

        /// <summary>
        /// 判断动画是否存在
        /// </summary>
        /// <param name="animaname"></param>
        /// <returns></returns>
        private bool AnimaIsExist(string animaname)
        {
            return Actions.ContainsKey(animaname);
        }

        /// <summary>
        /// 判断正在播放的动画事件是否存在
        /// </summary>
        private bool NowIsExist
        {
            get
            {
                return AnimaIsExist(ClipNow);
            }
        }

        private void SetItem(string clipName)
        {
            if (Actions.ContainsKey(clipName))
            {
                NowAction = Actions[clipName];
            }
        }

        private void SetItem(string clipName, float speed)
        {
            if (Actions.ContainsKey(clipName))
            {
                Actions[clipName].animaSpeed = speed;
                //animator.SetFloat($"{clipName}Speed", speed);
                NowAction = Actions[clipName];
            }
        }

        private AnimaActionItem itemTemp;

        public void AddAction(string clipName, float time, UnityAction action, FloatAction SpeedAction = null, UnityAction update = null)
        {
            if (!Actions.ContainsKey(clipName))
            {
                itemTemp = new AnimaActionItem(clipName);
                AllActions.Add(itemTemp);
                Actions.Add(clipName, itemTemp);
            }
            else itemTemp = Actions[clipName];
            itemTemp.actions.Add(new AnimaAction(time, action));
            itemTemp.SpeedAction += SpeedAction;
            itemTemp.UpdateAction += update;
        }

        public void SetTo(string[] SetAnima, string NextAnima, BoolAction bAction, int Level = 0)
        {
            foreach (var item in SetAnima)
            {
                SetTo(item, NextAnima, bAction, Level);
            }
        }

        /// <summary>
        /// 设置过渡事件
        /// </summary>
        /// <param name="SetAnima">要设置的动画名字</param>
        /// <param name="NextAnima">要过渡到的动画名字</param>
        /// <param name="can">过渡条件</param>
        /// <param name="level">过渡优先级</param>
        public void SetTo(string SetAnima, string NextAnima, BoolAction bAction, int Level = 0)
        {
            if (!animas.ContainsKey(SetAnima))
            {
                animas.Add(SetAnima, new AnimaConItem() { name = SetAnima });
            }
            if (!animas.ContainsKey(NextAnima))
            {
                animas.Add(NextAnima, new AnimaConItem() { name = NextAnima });
            }
            animas[SetAnima].To.Add(new AnimaConItem.Data()
            {
                anima = NextAnima,
                can = bAction,
                level = Level
            }); ;
        }

        /// <summary>
        /// 设置过渡_SetAnima当前动画_NextAnima要过渡到的动画_can过渡条件_wait是否等待播放完成_waitTime需要等待播放的百分比_level优先等级
        /// </summary>
        /// <param name="SetAnima">当前动画</param>
        /// <param name="NextAnima">要过渡到的动画</param>
        /// <param name="can">过渡条件</param>
        /// <param name="wait">是否等待播放完成</param>
        /// <param name="waitTime">需要等待播放的百分比</param>
        /// <param name="level">优先等级</param>
        public void SetTo(string SetAnima, string NextAnima, BoolAction bAction, bool wait, float waitTime = 1f, bool UseTime = false, int Level = 0)
        {
            if (!animas.ContainsKey(SetAnima))
            {
                animas.Add(SetAnima, new AnimaConItem() { name = SetAnima });
            }
            if (!animas.ContainsKey(NextAnima))
            {
                animas.Add(NextAnima, new AnimaConItem() { name = NextAnima });
            }
            animas[SetAnima].To.Add(new AnimaConItem.Data()
            {
                anima = NextAnima,
                can = bAction,
                WaitOver = wait,
                WaitTime = waitTime,
                UseTime = UseTime,
                level = Level
            }); ;
        }

        /// <summary>
        /// 播放完一遍后自动过渡
        /// </summary>
        /// <param name="SetAnima">当前动画</param>
        /// <param name="NextAnima">要过渡到的动画</param>
        /// <param name="level">优先级</param>
        public void SetAutoTo(string SetAnima, string NextAnima, int Level = 0)
        {
            if (!animas.ContainsKey(SetAnima))
            {
                animas.Add(SetAnima, new AnimaConItem() { name = SetAnima });
            }
            if (!animas.ContainsKey(NextAnima))
            {
                animas.Add(NextAnima, new AnimaConItem() { name = NextAnima });
            }
            animas[SetAnima].To.Add(new AnimaConItem.Data()
            {
                anima = NextAnima,
                can = () => true,
                WaitOver = true,
                WaitTime = 1,
                UseTime = false,
                level = Level
            }); ;
        }

        /// <summary>
        /// 根据名字返回一个bool,如果没有就会创建一个false
        /// </summary>
        /// <param name="animaName"></param>
        /// <returns></returns>
        public bool GetBool(string animaName)
        {
            CanAddＢool(animaName);
            if (data.Bools[animaName].IsTrigger)
            {
                data.TempBools.Add(data.Bools[animaName]);
            }
            return data.Bools[animaName].Value;
        }

        /// <summary>
        /// 根据名字返回一个Float,如果没有就会创建一个0
        /// </summary>
        /// <param name="animaName"></param>
        /// <returns></returns>
        public float GetFloat(string animaName)
        {
            CanAddFloat(animaName);
            if (data.Floats[animaName].IsTrigger)
            {
                data.TempFloats.Add(data.Floats[animaName]);
            }
            return data.Floats[animaName].Value;
        }

        /// <summary>
        /// 根据名字返回一个Int,如果没有就会创建一个0
        /// </summary>
        /// <param name="animaName"></param>
        /// <returns></returns>
        public int GetInt(string animaName)
        {
            CanAddInt(animaName);
            if (data.Ints[animaName].IsTrigger)
            {
                data.TempInts.Add(data.Ints[animaName]);
            }
            return data.Ints[animaName].Value;
        }

        /// <summary>
        /// 根据名字设置一个bool的值,如果没有就会创建一个false
        /// </summary>
        /// <param name="animaName"></param>
        /// <param name="value"></param>
        public void SetBool(string animaName, bool value)
        {
            CanAddＢool(animaName);
            data.Bools[animaName].Value = value;
        }

        /// <summary>
        /// 添加一个触发器
        /// </summary>
        /// <param name="animaName"></param>
        public void AddTrigger(string animaName)
        {
            CanAddＢool(animaName);
            data.Bools[animaName].IsTrigger = true;
        }

        /// <summary>
        /// 根据名字设置一个float的值,如果没有就会创建一个
        /// </summary>
        /// <param name="animaName"></param>
        /// <param name="value"></param>
        public void SetFloat(string animaName, float value)
        {
            CanAddFloat(animaName);
            data.Floats[animaName].Value = value;
        }

        /// <summary>
        /// 根据名字设置一个int的值,如果没有就会创建一个
        /// </summary>
        /// <param name="animaName"></param>
        /// <param name="value"></param>
        public void SetInt(string animaName, int value)
        {
            CanAddInt(animaName);
            data.Ints[animaName].Value = value;
        }

        private void CanAddFloat(string name)
        {
            if (!data.Floats.ContainsKey(name))
            {
                data.Floats.Add(name, new SetData.SkipData<float>());
            }
        }

        private void CanAddInt(string name)
        {
            if (!data.Ints.ContainsKey(name))
            {
                data.Ints.Add(name, new SetData.SkipData<int>());
            }
        }

        private void CanAddＢool(string name)
        {
            if (!data.Bools.ContainsKey(name))
            {
                data.Bools.Add(name, new SetData.SkipData<bool>());
            }
        }

        [System.Serializable]
        public class AnimaActionItem
        {
            [Header("动画片段名字")]
            public string animaClipName;

            [Header("播放速度")]
            public float animaSpeed = 1;

            /// <summary>
            /// 控制动画播放速度的事件
            /// </summary>
            public FloatAction SpeedAction;

            [Header("具体事件")]
            public List<AnimaAction> actions = new List<AnimaAction>();

            /// <summary>
            /// 每帧执行的事件
            /// </summary>
            public UnityAction UpdateAction;

            /// <summary>
            /// 被中断时执行的事件
            /// </summary>
            public UnityAction BreakAction;

            /// <summary>
            /// 执行完毕一次时执行的事件
            /// </summary>
            public UnityAction EndAction;

            public AnimaActionItem(string animaClipName)
            {
                this.animaClipName = animaClipName;
            }

            public void ReSetActions()
            {
                foreach (var item in actions)
                {
                    item.IsPlay = false;
                }
            }
        }

        [System.Serializable]
        public class AnimaConItem
        {
            public string name = "Null";

            /// <summary>
            /// 过渡事件
            /// </summary>
            public List<Data> To = new List<Data>();

            [System.Serializable]
            public class Data
            {
                public string anima;
                public BoolAction can;
                public int level = 0;

                /// <summary>
                /// 等待播放一定时间再进行跳转
                /// </summary>
                public bool WaitOver;

                /// <summary>
                /// 等待的时间(百分比)
                /// </summary>
                public float WaitTime = 1;

                /// <summary>
                /// 将百分比切换为标准时间
                /// </summary>
                public bool UseTime = false;
            }
        }

        [System.Serializable]
        public class AnimaAction
        {
            [Header("执行时间")]
            public float time;

            [Header("具体事件")]
            public UnityAction action;

            /// <summary>
            /// 这个事件是否执行过了
            /// </summary>
            [HideInInspector]
            public bool IsPlay = false;

            public AnimaAction(float time, UnityAction action)
            {
                this.time = time;
                this.action = action;
            }
        }

        [System.Serializable]
        public class SetData
        {
            [Header("单帧最大跳转数量")]
            [Range(1, 49)]
            public int MaxSkipNum = 1;

            /// <summary>
            /// 控制动画传递的布尔参数列表
            /// </summary>
            public Dictionary<string, SkipData<bool>> Bools = new Dictionary<string, SkipData<bool>>();

            /// <summary>
            /// 控制动画传递的浮点参数列表
            /// </summary>
            public Dictionary<string, SkipData<float>> Floats = new Dictionary<string, SkipData<float>>();

            /// <summary>
            /// 控制动画传递的整数参数列表
            /// </summary>
            public Dictionary<string, SkipData<int>> Ints = new Dictionary<string, SkipData<int>>();

            /// <summary>
            /// 当前帧用到的参数
            /// </summary>
            public List<SkipData<bool>> TempBools = new List<SkipData<bool>>();

            public List<SkipData<float>> TempFloats = new List<SkipData<float>>();
            public List<SkipData<int>> TempInts = new List<SkipData<int>>();

            public class SkipData<T>
            {
                /// <summary>
                /// 当前值
                /// </summary>
                public T Value;

                /// <summary>
                /// 默认值
                /// </summary>
                public T DefValue;

                /// <summary>
                /// 是否为触发器
                /// </summary>
                public bool IsTrigger;
            }
        }
    }

    public delegate bool AnimaBoolAction();

    public delegate float AnimaFloatAction();
}