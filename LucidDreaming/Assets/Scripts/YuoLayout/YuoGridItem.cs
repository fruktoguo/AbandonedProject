using System;
using System.Collections;
using System.Collections.Generic;
using Card;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YuoLayout;
using YuoTools;
using YuoTools.Extend.UI.YuoLayout;
using YuoTools.Extend.YuoMathf;
using YuoTools.UI;

public class YuoGridItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Image image;
    private static YuoGridItem _nowItem;
    public YuoVector2Int Pos;

    public static YuoGridItem NowItem
    {
        get => _nowItem;
        set
        {
            if (_nowItem != value)
            {
                _nowItem = value;
                RefreshColor();
            }
        }
    }

    [ShowInInspector] public BattleMap.MapItem MapItem;

    public static void SetBattle(BattleMap map)
    {
        foreach (var item in _items)
        {
            item.MapItem = map.GetMapItem(item.index);
        }

        RefreshColor();
    }

    private int index;
    private static YuoGridItem[] _items = new YuoGridItem[16];
    public Image card;

    private void Awake()
    {
        image = GetComponent<Image>();
        index = transform.GetSiblingIndex();
        Pos.x = index % 4;
        Pos.y = index / 4;
        _items[index] = this;
    }

    public bool CheckBoard(YuoCardPanelItem item)
    {
        return RoundManager.Instance.NowMap.PlayerCheckAndEnterBoard(Pos.x, Pos.y, item);
    }

    public void SetCard(YuoCardPanelItem item)
    {
        card.sprite = item.cardImage.sprite;
        card.gameObject.Show();
    }

    static void SetGridColor(YuoVector2Int pos, Color color)
    {
        int index = pos.x * 4 + pos.y;
        _items[index].image.color = color;
    }

    public static void RefreshColor()
    {
        if (NowItem == null || YuoCardPanelItem.SDragItem == null)
        {
            var data = RoundManager.Instance.NowMap.GetIntersect();

            foreach (var item in _items)
            {
                item.image.color = Color.white;
            }

            foreach (var mapItem in data.scope)
            {
                SetGridColor(mapItem.pos, Color.green);
            }

            foreach (var mapItem in data.intersect)
            {
                SetGridColor(mapItem.pos, Color.red);
            }
        }
        else
        {
            var data = RoundManager.Instance.NowMap.GetIntersect(NowItem.Pos.x, NowItem.Pos.y,
                YuoCardPanelItem.SDragItem.Data.CardComponent, true);

            foreach (var item in _items)
            {
                item.image.color = Color.white;
            }

            foreach (var mapItem in data.scope)
            {
                SetGridColor(mapItem.pos, Color.green);
            }

            foreach (var mapItem in data.intersect)
            {
                SetGridColor(mapItem.pos, Color.red);
            }

            NowItem.image.color = Color.yellow;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        NowItem = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (NowItem == this) NowItem = null;
    }
}