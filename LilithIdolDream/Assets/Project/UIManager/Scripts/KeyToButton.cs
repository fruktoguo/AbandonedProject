using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Sirenix.OdinInspector;
namespace YuoTools
{
    [RequireComponent(typeof(Button))]
    public class KeyToButton : MonoBehaviour
    {
        [InfoBox("如果需要这个item被挡住时不使用按键,则需要设置一下他所属的UIItem", InfoMessageType.None)]
        [HorizontalGroup("Item")]
        public UI_Item uiItem;
        [InfoBox("                   ", InfoMessageType.None)]
        [HorizontalGroup("Item")]
        [Button]
        void FindItem()
        {
            var tran = transform;
            for (int i = 0; i < 10; i++)
            {
                if (tran.parent != null)
                {
                    if (tran.parent.GetComponent<UI_Item>() != null)
                    {
                        uiItem = tran.parent.GetComponent<UI_Item>();
                        return;
                    }
                    else
                    {
                        tran = tran.parent;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        public bool UseManager;
        [HideIf("UseManager")]
        public KeyCode key;
        [ShowIf("UseManager")]
        public string KeyName;
        Button btn;
        YuoInput input;
        private void Awake()
        {
            btn = GetComponent<Button>();
            //input = YuoInput.Instance;
        }
        private void OnEnable()
        {
            input = YuoInput.Instance;

            if (UseManager)
            {
                input.AddDown(KeyName, Down);
            }
        }
        private void OnDisable()
        {
            if (UseManager)
            {
                input.RemoveDown(KeyName, Down);
            }
        }
        void Down()
        {
            if (gameObject.activeSelf && !(uiItem == null || UIManager.Instance.NowItem != uiItem))
            {
                //延迟一帧防止一次关掉所有窗口
                this.YuoDelay(() => btn.onClick?.Invoke());
            }
        }
        private void Update()
        {
            if (!UseManager && Input.GetKeyDown(key))
            {
                btn.onClick?.Invoke();
            }
        }
    }
}