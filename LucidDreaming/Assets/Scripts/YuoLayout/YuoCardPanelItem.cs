using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YuoTools;
using YuoTools.Extend.UI.YuoLayout;

namespace YuoLayout
{
    public class YuoCardPanelItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
        IPointerUpHandler
    {
        public CardViewData Data;
        private MaskableGraphic _maskableGraphic;
        [HideInInspector] public Image cardImage;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (SDragItem || _isReturn) return;
            isEnter = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (SDragItem || _isReturn) return;
            isEnter = false;
        }

        bool isEnter;
        public bool IsDrag;
        public float power = 0.3f;

        private void Awake()
        {
            _maskableGraphic = GetComponent<MaskableGraphic>();
            cardImage = GetComponent<Image>();
        }

        public static YuoCardPanelItem SDragItem;

        private void Update()
        {
            if (_isReturn) return;
            if (IsDrag)
            {
                //拖拽
                Data.rect.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Data.rect.SetPosZ(0);
                //离原始位置越远,受到动画影响越小
                float r = (Vector2.Distance(clickPos, Data.rect.anchoredPosition) / 500f).RClamp(1);
                OnlyScale(r);
                return;
            }

            var target = isEnter ? power : 0f;

            if (Data.width.ApEqual(target, 0.0001f)) return;

            Data.width = Mathf.Lerp(Data.width, target, Time.deltaTime * 10);

            OnAnima();
        }

        void OnlyScale(float r)
        {
            Data.width = power * (1 - r) - Data.defWidth * r;
            Data.width.Clamp();
            transform.localScale = Vector3.one * (Data.width + 1);
        }

        void OnAnima()
        {
            transform.localScale = Vector3.one * (Data.width + 1);
            Data.Height = Data.rect.sizeDelta.y * Data.width * 0.5f;
            Data.radiusScale = 1 - Data.width / power;
        }

        public Vector2 clickPos;

        public void StartDrag()
        {
            clickPos = Data.rect.anchoredPosition;
            transform.SetSiblingIndex(transform.parent.childCount - 1);
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            IsDrag = true;
            Data.Stop = true;
            SDragItem = this;
            _maskableGraphic.raycastTarget = false;
        }

        public void EndDrag()
        {
            if (YuoGridItem.NowItem != null)
            {
                //移除卡片
                if (YuoGridItem.NowItem.CheckBoard(this))
                {
                    ExitCardList();
                    return;
                }
            }
            ReturnCardList();
        }

        bool _isReturn;

        async void ReturnCardList()
        {
            _isReturn = true;
            transform.SetSiblingIndex(Data.Index);
            await Data.rect.DOAnchorPos(Data.DefPos, 0.5f).OnUpdate(() =>
            {
                float r = (Vector2.Distance(clickPos, Data.rect.anchoredPosition) / 500f).RClamp(1);
                OnlyScale(r);
            }).AsyncWaitForCompletion();

            await DOTween.To(x =>
            {
                Data.width = x;
                OnAnima();
            }, Data.width, 0, 0.3f).AsyncWaitForCompletion();
            
            _isReturn = false;
            ResetState();
        }

        void ResetState()
        {
            Data.width = 0;
            transform.localScale = Vector3.one;
            IsDrag = false;
            Data.Stop = false;
            SDragItem = null;
            _maskableGraphic.raycastTarget = true;
            isEnter = false;
            OnAnima();
        }

        void ExitCardList()
        {
            gameObject.SetActive(false);
            transform.SetSiblingIndex(Data.Index);
            YuoCardPanel.Instance.RemoveCard(this);
            Data.width = -Data.defWidth;
            transform.localScale = Vector3.one;
            _maskableGraphic.raycastTarget = true;
            SDragItem = null;
            isEnter = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isReturn) return;
            StartDrag();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isReturn) return;
            EndDrag();
        }
    }
}