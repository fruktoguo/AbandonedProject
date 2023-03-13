using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TranslateText : MonoBehaviour
{
    [HideInInspector]
    public Text text;

    private void Awake()
    {
        Initialization();
    }

    public void Initialization()
    {
        text = GetComponent<Text>();
    }
}