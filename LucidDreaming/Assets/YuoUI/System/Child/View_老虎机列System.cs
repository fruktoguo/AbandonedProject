using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public partial class View_老虎机列Component
    {
        public float MainPos;
        public float MoveSpeed = 50;
        public float Space = 10;
        public List<RectTransform> items = new List<RectTransform>();

        /// <summary>
        /// 循环滚动
        /// </summary>
        public void Loop()
        {
            foreach (var item in items)
            {
                item.anchoredPosition += Vector2.down * MoveSpeed * Time.deltaTime;
            }

            for (int i = 0; i < items.Count; i++)
            {
                items[i].anchoredPosition += Vector2.down * MoveSpeed * Time.deltaTime;
                if (items[i].anchoredPosition.y < -MainRectTransform.sizeDelta.y)
                {
                    items[i].anchoredPosition = items[0].anchoredPosition + Vector2.up * (Space + items[i].sizeDelta.y);
                }
            }

            items.Sort((a, b) => b.anchoredPosition.y.CompareTo(a.anchoredPosition.y));
        }
    }

    public class View_老虎机列CreateSystem : YuoSystem<View_老虎机列Component>, IUICreate
    {
        public override string Group => "UI/老虎机列";

        protected override void Run(View_老虎机列Component view)
        {
            view.FindAll();
            foreach (var image in view.all_Image)
            {
                view.items.Add(image.rectTransform);
            }

            view.MoveSpeed = Random.Range(50, 500);
        }
    }

    public class View_老虎机列UpdateSystem : YuoSystem<View_老虎机列Component>, IUpdate
    {
        public override string Group => "UI/老虎机列";

        protected override void Run(View_老虎机列Component view)
        {
            view.Loop();
        }
    }
}