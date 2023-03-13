using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

using YuoTools;

namespace YuoTools
{
    public class YuoAnimaCon : SerializedMonoBehaviour
    {
        private Dictionary<string, AnimationClipPlayable> playables = new Dictionary<string, AnimationClipPlayable>();

        [HideInInspector]
        public AnimaItems Animas;

        [ReadOnly]
        public AnimaItem NowAnima;

        private Animator _animator;
        private PlayableGraph graph;
        private AnimationPlayableOutput playableOutput;
        public List<AnimationClip> animationClips = new List<AnimationClip>();
        public Dictionary<string, AnimaItem> animaItems = new Dictionary<string, AnimaItem>();

        protected virtual void Awake()
        {
            var anim = GetComponent<Animator>();
            if (!anim)
            {
                anim = gameObject.AddComponent<Animator>();
            }
            var ac = anim.runtimeAnimatorController;
            anim.GetCurrentAnimatorClipInfo(0);
            _animator = anim;
            //= GetComponent<AnimaItems>();
            animationClips.AddRange(ac.animationClips);
            graph = PlayableGraph.Create($"[{gameObject.name}] YuoAnimator");
            // 创建一个Output节点，类型是Animation，名字是Anima，目标对象是物体上的Animator组件
            playableOutput = AnimationPlayableOutput.Create(graph, "Anima", _animator);
            InitAnimaItem();
        }

        private void OnDestroy()
        {
            graph.Destroy();
        }

        public void Play(string animationName)
        {
            if (!animaItems.ContainsKey(animationName))
            {
                Debug.LogWarning($"[{gameObject.name}] 上不存在 [{animationName}]动画");
                return;
            }
            NowAnima = animaItems[animationName];
            NowAnima.playable = playables[animationName];
            NowAnima.playable.SetTime(0);
            // 将playable连接到output
            playableOutput.SetSourcePlayable(NowAnima.playable);
            // 播放这个graph
            graph.Play();
        }

        private void Update()
        {
            //NowAnima.playable.GetTime().Log();
            NowAnima?.playable.SetSpeed(Time.timeScale * NowAnima.speed);
        }

        private void InitAnimaItem()
        {
            animaItems.Clear();
            foreach (var item in animationClips)
            {
                AnimaItem animaItem = new AnimaItem()
                {
                    name = item.name,
                    clip = item,
                    length = item.length,
                    IsLoop = item.isLooping
                };
                animaItem.playable = AnimationClipPlayable.Create(graph, item);
                // 创建一个动画剪辑Playable，将clip传入进去
                if (animaItems.ContainsKey(animaItem.name))
                {
                    animaItems[animaItem.name] = animaItem;
                    playables[animaItem.name] = animaItem.playable;
                }
                else
                {
                    playables.Add(item.name, animaItem.playable);
                    animaItems.Add(item.name, animaItem);
                }
            }
        }

        public void UpdateClip(string name, AnimationClip clip)
        {
            animaItems[name].clip = clip;
            animaItems[name].length = clip.length;
            animaItems[name].playable = AnimationClipPlayable.Create(graph, clip);
        }

        [System.Serializable]
        public class AnimaItem
        {
            public string name = "null";

            public AnimationClip clip;

            [SerializeField]
            private float _speed = 1;

            public float speed { get => _speed; set { _speed = value; playable.SetSpeed(value); } }

            [ReadOnly]
            public bool IsLoop;

            [ReadOnly]
            public float length;

            [HideInInspector]
            public AnimationClipPlayable playable;
        }
    }
}