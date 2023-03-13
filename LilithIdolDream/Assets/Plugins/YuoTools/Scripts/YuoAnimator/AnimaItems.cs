using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;

using static YuoTools.YuoAnimaCon;

namespace YuoTools
{
    [ExecuteInEditMode]
    public class AnimaItems : SerializedMonoBehaviour
    {
        public List<AnimationClip> animationClips = new List<AnimationClip>();
        public List<AnimaItem> _animaItems = new List<AnimaItem>();
        public Dictionary<string, AnimaItem> animaItems = new Dictionary<string, AnimaItem>();
    }
}