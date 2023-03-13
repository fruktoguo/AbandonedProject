using System;
using System.Collections.Generic;
using System.Linq;
using Card;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using YuoTools;
using YuoTools.Extend.UI.YuoLayout;

namespace YuoLayout
{
    public class YuoCardPanel : MonoBehaviour
    {
        public List<CardViewData> LayoutItems = new();
        public bool InAnima;

        private RectTransform RectTran
        {
            get
            {
                if (_rect == null)
                    _rect = transform as RectTransform;
                return _rect;
            }
        }

        public float StartAnimaTime = 0.5f;

        float _offsetValue;

        float _angleValue;

        float _widthValue;

        private RectTransform _rect;

        private void Awake()
        {
            width = RectTran.rect.width;

            _offsetValue = offset;
            _angleValue = angle;
            _widthValue = width;
            Init();
        }

        public Ease ease = Ease.OutBack;
        public float offset = 120;
        public float angle = 30;
        public float width;
        public static YuoCardPanel Instance { get; private set; }

        private void OnEnable()
        {
            Instance = this;
            StartAnima();
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public async void StartAnima()
        {
            if (InAnima) return;
            InAnima = true;

            gameObject.Show();
            
            _offsetValue = 0;
            _angleValue = 0;
            _widthValue = 0;
            
            await DOTween.To(x =>
            {
                _widthValue = Mathf.Lerp(0, width, x);
                _offsetValue = Mathf.Lerp(0, offset, x);
                _angleValue = Mathf.Lerp(0, angle, x);
                RectTran.anchoredPosition = RectTran.anchoredPosition.RSetY((x - 1) * RectTran.sizeDelta.y);
            }, 0, 1, StartAnimaTime).SetEase(ease).AsyncWaitForCompletion();

            _widthValue = width;
            _offsetValue = offset;
            _angleValue = angle;
            RectTran.anchoredPosition = Vector2.zero;
            ViewAnima();
            ResetPanel();
            InAnima = false;
        }

        public async void EndAnima()
        {
            if (InAnima) return;
            InAnima = true;

            DOTween.To(x =>
            {
                _widthValue = Mathf.Lerp(0, width, x);
                _offsetValue = Mathf.Lerp(0, offset, x);
                _angleValue = Mathf.Lerp(0, angle, x);
                RectTran.anchoredPosition = RectTran.anchoredPosition.RSetY((x - 1) * (RectTran.sizeDelta.y + offset));
            }, 1, 0, StartAnimaTime).SetEase(ease);
            ViewAnima();

            RectTran.anchoredPosition = RectTran.anchoredPosition.RSetY(-(RectTran.sizeDelta.y + offset));
            await YuoWait.WaitTimeAsync(StartAnimaTime);
            _widthValue = width;
            _offsetValue = offset;
            _angleValue = angle;
            gameObject.Hide();
            InAnima = false;
        }

        private void Update()
        {
            ViewAnima();
        }

        public GameObject cardPrefab;

        public void CreateCard(CardComponent card)
        {
            var cardView = Instantiate(cardPrefab, RectTran).GetComponent<YuoCardPanelItem>();
            cardView.gameObject.Show();
            var cardViewData = new CardViewData()
            {
                rect = cardView.transform as RectTransform,
                Index = LayoutItems.Count,
                CardComponent = card
            };
            cardView.Data = cardViewData;
            LayoutItems.Add(cardViewData);
            ResetPanel();
        }

        public YuoCardPanelItem GetNewCard(CardComponent cardData)
        {
            var cardView = Instantiate(cardPrefab).GetComponent<YuoCardPanelItem>();
            var cardViewData = new CardViewData()
            {
                rect = cardView.transform as RectTransform,
                Index = LayoutItems.Count,
                CardComponent = cardData
            };
            cardView.Data = cardViewData;
            return cardView;
        }
        
        void Init()
        {
            //子物体数量是否变化
            var children = RectTran.GetComponentsInChildren<RectTransform>().ToList();
            children.Remove(RectTran);
            LayoutItems.Clear();
            for (var index = 0; index < children.Count; index++)
            {
                var item = new CardViewData()
                {
                    rect = children[index],
                };

                if (children[index].TryGetComponent(out YuoCardPanelItem layoutItem))
                {
                    layoutItem.Data = item;
                }

                LayoutItems.Add(item);
            }

            ResetPanel();
        }

        void ViewAnima()
        {
            float sumWidth = 1;
            foreach (var item in LayoutItems)
                sumWidth += item.width;

            float add = 0;

            foreach (var item in LayoutItems)
            {
                //分成两段
                float r = (item.Pos + add) / sumWidth;
                add += r * item.width;
                //当前位置
                float r2 = (item.Pos + add) / sumWidth;
                if (!item.Stop)
                {
                    item.rect.anchoredPosition = GetPos(r2).RAddY(item.Height);
                }

                item.rect.localEulerAngles = Vector3.back * GetAngle(r2) * item.radiusScale;

                //第二段
                add += (1 - r) * item.width;
            }
        }

        private float all;

        float GetAngle(float pos)
        {
            return _offsetValue / 4f * (pos - 0.5f);
        }


        public float Height;

        public void RemoveCard(YuoCardPanelItem item)
        {
            LayoutItems.RemoveAt(item.Data.Index);
            ResetPanel();
            ViewAnima();
        }

        void ResetPanel()
        {
            int count = LayoutItems.Count;
            if (count == 0) return;
            if (count == 1)
            {
                LayoutItems[0].Index = 0;
                LayoutItems[0].Pos = 0.5f;
                LayoutItems[0].defWidth = 1;
                LayoutItems[0].DefPos = GetPos(LayoutItems[0].Pos);
                return;
            }

            float startPos = 0;
            if (count < 6)
            {
                startPos = 0.5f / count;
            }

            float sumWidth = 1f - startPos * 2;
            for (int i = 0; i < count; i++)
            {
                LayoutItems[i].Index = i;
                LayoutItems[i].Pos = startPos + i / (count - 1f) * sumWidth;
                LayoutItems[i].defWidth = sumWidth / (count);
                LayoutItems[i].DefPos = GetPos(LayoutItems[i].Pos);
            }
        }

        Vector2 GetPos(float pos)
        {
            //使用贝塞尔曲线控制点
            return YuoTool.CalculateCubicBezierPoint2D(pos, Vector2.left * _widthValue / 2,
                Vector2.up * (_offsetValue + Height),
                Vector2.right * _widthValue / 2);
        }
    }

    [System.Serializable]
    public class CardViewData
    {
        public RectTransform rect;

        public CardComponent CardComponent;

        //序号
        public int Index;

        //当前位置
        public float Pos;

        //当前宽度
        public float width;

        //默认宽度
        public float defWidth;

        //暂停控制位置
        public bool Stop;

        //偏移高度
        public float Height;

        //旋转缩放
        public float radiusScale = 1;
        public Vector2 DefPos;
    }
}