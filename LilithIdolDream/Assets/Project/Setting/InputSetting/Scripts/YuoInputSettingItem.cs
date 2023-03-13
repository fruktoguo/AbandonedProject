using System.Net.Mime;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace YuoTools
{
    public class YuoInputSettingItem : MonoBehaviour
    {
        public TMP_Text text;
        public TMP_Text nameText;
        public void SetText(string text)
        {
            this.text.text = text;
        }
    }
}