using UnityEngine;
using System;

namespace YuoTools
{
    public class YuoInputSetting : MonoBehaviour
    {
        bool Set;
        Array codes;
        private YuoInput input;

        public void StatrSet(YuoInputSettingItem item)
        {
            input.Pause();
            nowItem = item;
            Set = true;
        }
        YuoInputSettingItem nowItem;
        private void Awake()
        {
            codes = Enum.GetValues(typeof(KeyCode));
        }
        private void OnEnable()
        {
            input = YuoInput.Instance;
        }
        private void OnDisable()
        {
            input.Resume();
        }
        private void Update()
        {
            if (Set)
            {
                if (Input.anyKeyDown)
                {
                    foreach (KeyCode keyCode in codes)
                    {
                        if (Input.GetKeyDown(keyCode))
                        {
                            print(keyCode.ToString());
                            if (nowItem != null)
                            {
                                Set = false;
                                nowItem.SetText(keyCode.ToString());
                                var item = input.GetItem(nowItem.nameText.text);
                                item.key = keyCode;
                                item.KeyName = nowItem.nameText.text;
                                input.Resume();
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}