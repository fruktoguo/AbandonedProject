﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using YuoTools;

namespace YuoTools
{
    /// <summary>
    /// 动态格子矩形
    /// </summary>
    [System.Serializable]
    public class YuoLoopListRect
    {
        /// <summary>
        /// 矩形数据
        /// </summary>
        [SerializeField]
        public Rect mRect;

        /// <summary>
        /// 格子索引
        /// </summary>
        public int Index;

        public YuoLoopListRect(float x, float y, float width, float height, int index)
        {
            this.Index = index;
            mRect = new Rect(x, y, width, height);
        }

        /// <summary>
        /// 是否相交
        /// </summary>
        /// <param name="otherRect"></param>
        /// <returns></returns>
        public bool Overlaps(YuoLoopListRect otherRect)
        {
            return mRect.Overlaps(otherRect.mRect);
        }

        /// <summary>
        /// 是否相交
        /// </summary>
        /// <param name="otherRect"></param>
        /// <returns></returns>
        public bool Overlaps(Rect otherRect)
        {
            return mRect.Overlaps(otherRect);
        }

        public override string ToString()
        {
            return string.Format("index:{0},x:{1},y:{2},w:{3},h:{4}", Index, mRect.x, mRect.y, mRect.width, mRect.height);
        }
    }

    public abstract class YuoLoopListItem<T> : MonoBehaviour
    {
        public delegate void OnSelect(YuoLoopListItem<T> item);

        public delegate void OnUpdateData(YuoLoopListItem<T> item);

        public OnSelect OnSelectHandler;
        public OnUpdateData OnUpdateDataHandler;

        protected YuoLoopListRect mDRect;

        [SerializeField]
        /// <summary>
        /// 动态格子数据
        /// </summary>
        protected T mData;

        public YuoLoopListRect DRect
        {
            set
            {
                mDRect = value;
                if (value != null)
                    gameObject.Show();
                else
                    gameObject.Hide();
            }
            get { return mDRect; }
        }

        private void Start()
        {
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="data"></param>
        public void SetData(T data)
        {
            if (data == null)
            {
                return;
            }

            mData = data;
            OnUpdateDataHandler?.Invoke(this);
            OnRenderer();
        }

        /// <summary>
        /// 当item中的数据指向发生改变时调用
        /// </summary>
        protected abstract void OnRenderer();

        /// <summary>
        /// 返回数据
        /// </summary>
        /// <returns></returns>
        public T GetData()
        {
            return mData;
        }
    }
}