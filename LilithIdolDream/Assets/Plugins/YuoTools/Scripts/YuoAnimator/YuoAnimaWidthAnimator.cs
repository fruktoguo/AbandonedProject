using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace YuoTools
{
    [RequireComponent(typeof(Animator))]
    public class YuoAnimaWidthAnimator : SerializedMonoBehaviour
    {
        public Animator animator { get; private set; }

        public YuoAnimaScriptable animaScriptable;

        [ReadOnly]
        //[SerializeField]
        private Dictionary<string, YuoAnimaItem> Animas = new Dictionary<string, YuoAnimaItem>();

        [ReadOnly]
        //[SerializeField]
        private Dictionary<int, string> HashToName = new Dictionary<int, string>();

        [ReadOnly]
        public YuoAnimaItem NowAnima;

        public YuoStateMachineBehaviour yuoStateMachineBehaviour { get; private set; }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            var data = Instantiate(animaScriptable);
            Animas = data.Animas;
            HashToName = data.HashToName;
            yuoStateMachineBehaviour = animator.GetBehaviour<YuoStateMachineBehaviour>();
            if (!yuoStateMachineBehaviour)
            {
                Debug.LogError($"请在 [AnimatorController] 的 [layer] 中添加行为 [YuoStateMachineBehaviour] ");
            }
            yuoStateMachineBehaviour.onStateEnter.RemoveAllListeners();
            yuoStateMachineBehaviour.onStateExit.RemoveAllListeners();
            yuoStateMachineBehaviour.onStateMove.RemoveAllListeners();
            yuoStateMachineBehaviour.onStateUpdate.RemoveAllListeners();
            yuoStateMachineBehaviour.onStateEnter.AddListener(OnStateEnter);
            yuoStateMachineBehaviour.onStateExit.AddListener(OnStateExit);
            yuoStateMachineBehaviour.onStateMove.AddListener(OnStateMove);
            yuoStateMachineBehaviour.onStateUpdate.AddListener(OnStateUpdate);
        }

        private void FixedUpdate()
        {
            if (NowAnima != null)
            {
                foreach (var item in NowAnima.transitions)
                {
                    if (item.condition != null)
                    {
                        if (item.condition(item))
                        {
                            item.OnOver?.Invoke();
                            PlayAnima(item.to.AnimaName);
                        }
                    }
                }
            }

            if (lagTime > 0)
            {
                animator.speed = animaSpeed * lagPower;
                lagTime -= Time.fixedDeltaTime;
                if (lagTime <= 0)
                {
                    SetSpeed(animaSpeed);
                }
            }
        }

        private float animaSpeed = 1;

        public void SetSpeed(float speed)
        {
            animaSpeed = speed;
            animator.speed = speed;
        }

        public void Lag(float time = 0.1f, float power = 0.1f)
        {
            lagTime = time;
            lagPower = power;
        }

        private float lagTime = -1;
        private float lagPower = 0.1f;

        private void PlayAnima(string name)
        {
            animator.Play(name);
        }

        public string GetStateName(int hash)
        {
            if (HashToName.ContainsKey(hash))
            {
                return HashToName[hash];
            }
            return "null";
        }

        protected void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (Animas.ContainsKey(HashToName[stateInfo.shortNameHash]))
            {
                NowAnima = Animas[HashToName[stateInfo.shortNameHash]];
                foreach (var item in NowAnima.Events)
                {
                    item.num = 0;
                }
            }
            else
            {
                Debug.LogError($"{HashToName[stateInfo.shortNameHash]}  没有找到");
            }
            //print($"OnStateEnter  {HashToName[stateInfo.shortNameHash]}");
        }

        protected void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //print($"OnStateExit  {HashToName[stateInfo.shortNameHash]}");
        }

        protected void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //print($"OnStateMove  {HashToName[stateInfo.shortNameHash]}");
        }

        protected void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var item in NowAnima.Events)
            {
                if (stateInfo.normalizedTime > item.num && stateInfo.normalizedTime % 1 > item.time)
                {
                    item.num++;
                    item.action?.Invoke();
                }
            }
            //print($"OnStateUpdate  {HashToName[stateInfo.shortNameHash]}");
        }

        public void AddTransition(string[] from, string to, BoolAction<YuoTransition> condition, UnityAction onOver = null)
        {
            foreach (var item in from)
            {
                AddTransition(item, to, condition, onOver);
            }
        }

        public void AddTransition(string from, string to, BoolAction<YuoTransition> condition, UnityAction onOver = null)
        {
            if (Animas.ContainsKey(from) && Animas.ContainsKey(to))
            {
                YuoTransition transition = new YuoTransition(animator, Animas[from], Animas[to]);
                Animas[from].transitions.Add(transition);
                transition.OnOver = onOver;
                transition.condition = condition;
            }
            else
            {
                Debug.LogError("错误的state名称");
            }
        }

        #region 添加事件

        /// <summary>
        /// 添加事件在对应帧数
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="frame"></param>
        /// <param name="action"></param>
        public void AddEventOnFrame(string clip, int frame, UnityAction action)
        {
            if (Animas.ContainsKey(clip))
            {
                frame.Clamp((int)(Animas[clip].Clip.frameRate * Animas[clip].Clip.length));
                AddEvent(clip, frame / Animas[clip].Clip.frameRate, action);
            }
            else
            {
                Debug.LogError($"不存在【{clip}】动画");
            }
        }

        /// <summary>
        /// 添加事件在对应时间
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="time"></param>
        /// <param name="action"></param>
        public void AddEvent(string clip, float time, UnityAction action)
        {
            if (Animas.ContainsKey(clip))
            {
                YuoAnimaEvent animaEvent = new YuoAnimaEvent()
                {
                    clip = Animas[clip].Clip,
                    time = (time / Animas[clip].Clip.length).RClamp(1),
                    action = action
                };
                Animas[clip].Events.Add(animaEvent);
            }
            else
            {
                Debug.LogError($"不存在【{clip}】动画");
            }
        }

        public AnimationEvent AddEventOnClipFrame(string clip, string functionName, int frame)
        {
            if (Animas.ContainsKey(clip))
            {
                frame.Clamp((int)(Animas[clip].Clip.frameRate * Animas[clip].Clip.length));
                return AddEventOnClipTime(clip, functionName, frame / Animas[clip].Clip.frameRate);
            }
            return null;
        }

        public AnimationEvent AddEventOnClipTime(string clip, string functionName, float time)
        {
            if (Animas.ContainsKey(clip))
            {
                time.Clamp(Animas[clip].Clip.length);
                var ae = new AnimationEvent()
                {
                    time = time,
                    functionName = functionName,
                };
                Animas[clip].Clip.AddEvent(ae);
                return ae;
            }
            else
            {
                Debug.LogError($"不存在[{clip}]动画");
                return null;
            }
        }

        #endregion 添加事件

        [System.Serializable]
        public class YuoTransition
        {
            public Animator anima;
            public YuoAnimaItem from;
            public YuoAnimaItem to;
            public BoolAction<YuoTransition> condition;
            public UnityAction OnOver;

            public YuoTransition(Animator animator, YuoAnimaItem from, YuoAnimaItem to)
            {
                anima = animator;
                this.from = from;
                this.to = to;
            }
        }

        [System.Serializable]
        public class YuoAnimaItem
        {
            public string AnimaName = "Null";
            public AnimationClip Clip;

            [HideInInspector]
            public UnityAction OnEnter;

            [ReadOnly]
            public List<YuoAnimaEvent> Events = new List<YuoAnimaEvent>();

            [HideInInspector]
            public UnityAction OnExit;

            public List<YuoTransition> transitions = new List<YuoTransition>();
            public float Speed = 1;
        }

        [System.Serializable]
        public class YuoAnimaEvent
        {
            public float time;
            public AnimationClip clip;
            public UnityAction action;
            public int num;
        }
    }
}