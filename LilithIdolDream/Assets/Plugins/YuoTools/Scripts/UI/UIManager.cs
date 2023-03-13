using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using YuoTools;

public class UIManager : SingletonSerializedMono<UIManager>
{
    [SerializeField]
    private Dictionary<string, UI_Item> UiItems = new Dictionary<string, UI_Item>();

    public string DefWindow = "DefWindow";

    [BoxGroup("显示的窗口")]
    [ReadOnly]
    [SerializeField]
    private List<UI_Item> OpenItems = new List<UI_Item>();

    public int BaseIndex;

    public UI_Item Register(UI_Item item)
    {
        if (!UiItems.ContainsKey(item.WindowName))
        {
            UiItems.Add(item.WindowName, item);
        }
        return item;
    }

    public List<UI_Item> GetAllWindow()
    {
        return UiItems.Values.ToList();
    }

    public UI_Item GetWindow(string winName)
    {
        if (UiItems.ContainsKey(winName))
            return UiItems[winName];
        return null;
    }

    public void OpenWindow(string winName)
    {
        if (UiItems.ContainsKey(winName))
        {
            UiItems[winName].Show();
        }
        else
        {
            UiItems.Add(winName, AddWindow(winName));
        }
    }

    public void CloseWindow(string winName)
    {
        if (UiItems.ContainsKey(winName))
        {
            UiItems[winName].Hide();
        }
    }

    public void OpenAndCloseOther(string winName)
    {
        foreach (var item in UiItems)
        {
            if (item.Value.WindowName != winName)
            {
                if (OpenItems.Contains(item.Value))
                {
                    item.Value.Hide();
                }
            }
            else
            {
                item.Value.Show();
            }
        }
    }

    public void OpenAndCloseOther(UI_Item uI_Item)
    {
        uI_Item.Show();
        foreach (var item in UiItems)
        {
            if (item.Value != uI_Item)
            {
                if (OpenItems.Contains(item.Value))
                {
                    item.Value.Hide();
                }
            }
        }
    }

    public void CloseAllWindow()
    {
        foreach (var item in UiItems)
        {
            item.Value.Hide();
        }
    }

    public UI_Item CloseLastWindow()
    {
        uiTemp = null;
        if (OpenItems.Count > 0)
        {
            uiTemp = OpenItems.Last();
            uiTemp.Hide();
        }
        return uiTemp;
    }

    [Button]
    public void FindAllWindow()
    {
        UiItems.Clear();
        foreach (var item in transform.GetComponentsInChildren<UI_Item>(true))
        {
            if (!UiItems.ContainsKey(item.WindowName))
            {
                UiItems.Add(item.WindowName, item);
            }
        }
    }

    public void BackDef()
    {
        foreach (var item in UiItems)
        {
            if (item.Value.WindowName != DefWindow)
            {
                if (item.Value.gameObject.activeSelf)
                {
                    item.Value.Hide();
                }
            }
            else
            {
                item.Value.Show();
            }
        }
    }

    private UI_Item uiTemp;

    private void SetWindowLayer(UI_Item item, int layer)
    {
        item.UI_Layer = layer;
        item.transform.SetSiblingIndex(layer + BaseIndex);
    }

    private UI_Item AddWindow(string winName)
    {
        return null;
    }

    public void OnOpen(UI_Item Item)
    {
        if (!OpenItems.Contains(Item))
        {
            OpenItems.Add(Item);
        }
        else
        {
            OpenItems.Remove(Item);
            OpenItems.Add(Item);
        }
        NowItem = Item;
        SetWindowLayer(Item, Item.transform.parent.childCount - 1);
        foreach (var item in OpenItems)
        {
            item.UI_Layer = item.transform.GetSiblingIndex() - BaseIndex;
        }
    }
    public UI_Item NowItem;
    public void Close(UI_Item Item)
    {
        if (OpenItems.Contains(Item))
        {
            OpenItems.Remove(Item);
            if (NowItem == Item)
            {
                NowItem = OpenItems.Count > 0 ? OpenItems[OpenItems.Count - 1] : null;
            }
        }
    }

    /// <summary>
    /// 切换语言时调用,刷新ui面板图片显示
    /// </summary>
    public void SwitchLanguage()
    {
        foreach (var item in UiItems)
        {
            item.Value.Initialization();
        }
    }
}