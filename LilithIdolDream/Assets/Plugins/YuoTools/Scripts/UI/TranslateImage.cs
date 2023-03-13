using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using YuoTools;

[RequireComponent(typeof(Image))]
public class TranslateImage : MonoBehaviour
{
    [EnumToggleButtons]
    public TranType tranType;

    [ShowIf("tranType", TranType.Path)]
    public string path;

    [ShowIf("tranType", TranType.Sprite)]
    public SerializableDictionary<LanguageManager.LanType, Sprite> tranSprite = new SerializableDictionary<LanguageManager.LanType, Sprite>();

    public enum TranType
    {
        /// <summary>
        /// Â·¾¶
        /// </summary>
        Path = 0,

        /// <summary>
        /// ¾«Áé
        /// </summary>
        Sprite = 1
    }

    [HideInInspector]
    public Image image;

    public void Initialization()
    {
        image = GetComponent<Image>();
    }
}