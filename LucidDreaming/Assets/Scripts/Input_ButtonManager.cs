using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using YuoTools.ECS;
using YuoTools.Main.Ecs;

public class Input_ButtonManager : YuoComponent
{
    public List<Button> Buttons = new();

    public void Add(params Button[] buttons)
    {
        foreach (var button in buttons)
        {
            if (!Buttons.Contains(button))
            {
                Buttons.Add(button);
            }
        }
    }
    public void Remove(params Button[] buttons)
    {
        foreach (var button in buttons)
        {
            if (Buttons.Contains(button))
            {
                Buttons.Remove(button);
            }
        }
    }
    public void Clear()
    {
        Buttons.Clear();
    }
}