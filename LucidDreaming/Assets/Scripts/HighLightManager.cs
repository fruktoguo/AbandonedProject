using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YuoTools;
using YuoTools.ECS;
using YuoTools.Main.Ecs;
using YuoTools.UI;
using Object = UnityEngine.Object;

public class HighLightManagerComponent : YuoComponent
{
    public static HighLightManagerComponent Instance => World.Scene.GetComponent<HighLightManagerComponent>();

    public RectTransform HighLight;

    public bool Local;

    public void Select(Selectable item)
    {
        if (HighLight == null)
        {
            HighLight.transform.SetParent(null);
            return;
        }
        var rect = item.transform as RectTransform;
        if (rect == null) return;
        HighLight.gameObject.Show();

        HighLight.SetParent(Local ? item.transform.parent : UIManagerComponent.Instance.Transform);

        //重置位置大小旋转
        HighLight.localScale = Vector3.one;
        HighLight.anchoredPosition = rect.anchoredPosition;
        HighLight.sizeDelta = rect.sizeDelta;
        HighLight.pivot = rect.pivot;
        HighLight.anchorMin = rect.anchorMin;
        HighLight.anchorMax = rect.anchorMax;
        if (!Local) HighLight.position = item.transform.position;
        HighLight.SetSiblingIndex(HighLight.parent.childCount - 1);
        if (Local) HighLight.SetSiblingIndex(item.transform.GetSiblingIndex());
    }
    
}
public static class HighLightExtension
{
    public static void SetHighLight(this Selectable selectable,Action<Selectable> onSelect)
    {
        //添加进入事件
        var trigger = selectable.AddComponent<EventTrigger>().triggers;
        var Trigger = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerEnter,
            callback = new EventTrigger.TriggerEvent()
        };
        Trigger.callback.AddListener(_ =>
        {
            HighLightManagerComponent.Instance.Select(selectable);
            onSelect?.Invoke(selectable);
        });
        trigger.Add(Trigger);
    }
}

