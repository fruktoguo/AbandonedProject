using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace YuoTools
{
    public class YuoBuff
    {
        /// <summary>
        /// buff名字
        /// </summary>
        public string BuffName;

        /// <summary>
        /// 当前层数
        /// </summary>
        public int CountNow;

        /// <summary>
        /// 最大层数
        /// </summary>
        public int CountMax;

        /// <summary>
        /// 最大持续时间
        /// </summary>
        public float TimeMax;

        /// <summary>
        /// 剩余持续时间
        /// </summary>
        public float TimeSurplus;

        /// <summary>
        /// 触发间隔
        /// 如果有触发事件
        /// </summary>
        public float TimeSpace;

        public YuoEvent<YuoBuff> OnAdd;
        public YuoEvent<YuoBuff> OnRemove;
    }
}