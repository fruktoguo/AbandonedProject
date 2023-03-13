using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public partial class View_TipComponent
    {
        List<View_TipMessageComponent> _tipMessageComponents = new();

        //对象池
        List<View_TipMessageComponent> _tipMessagePools = new();

        public int MaxPoolCount = 10;
        public Vector2 messageSize = new Vector2(300, 100);
        public float messageSpace = 10;

        public void CreateMessage(string message, float time = 0.5f)
        {
            View_TipMessageComponent tipMessage = null;
            if (_tipMessagePools.Count > 0)
            {
                tipMessage = _tipMessagePools[0];
                _tipMessagePools.RemoveAt(0);
            }
            else
            {
                tipMessage = Entity.AddChild<View_TipMessageComponent>();
                tipMessage.rectTransform = Object
                    .Instantiate(Child_TipMessage.rectTransform.gameObject, RectTransform_MessagePanel)
                    .transform as RectTransform;
            }

            _tipMessageComponents.Add(tipMessage);
            if (tipMessage.rectTransform == null) return;
            tipMessage.rectTransform.anchoredPosition = new Vector2(0,
                -(messageSize.y * _tipMessageComponents.Count - 1 + messageSpace * (_tipMessageComponents.Count - 2)));
            tipMessage.rectTransform.gameObject.ReShow();
            tipMessage.ShowTime = time;
            tipMessage.Text_Message.text = message;

            World.RunSystem<IUICreate>(tipMessage);
        }

        //回收
        public void Recycle(View_TipMessageComponent tipMessage)
        {
            _tipMessageComponents.Remove(tipMessage);
            if (_tipMessagePools.Count >= MaxPoolCount)
            {
                tipMessage.Destroy();
                return;
            }

            _tipMessagePools.Add(tipMessage);
            if (tipMessage.rectTransform != null) tipMessage.rectTransform.gameObject.Hide();
        }

        public void Layout()
        {
            for (int i = 0; i < _tipMessageComponents.Count; i++)
            {
                _tipMessageComponents[i].TargetPos = new Vector2(0, -i * (messageSize.y + messageSpace));
            }
        }
    }

    public class View_TipCreateSystem : YuoSystem<View_TipComponent>, IUICreate
    {
        public override string Group => "TipMessage";

        protected override void Run(View_TipComponent view)
        {
            view.FindAll();
            view.Child_TipMessage.Entity.EntityName = "消息模板";
            view.messageSize = view.Child_TipMessage.rectTransform.sizeDelta;
        }
    }

    public class View_TipUpdateSystem : YuoSystem<View_TipComponent>, IUpdate
    {
        public override string Group => "TipMessage";

        protected override void Run(View_TipComponent component)
        {
            component.Layout();
            if (Input.GetKeyDown(KeyCode.A))
                component.CreateMessage("测试消息");
        }
    }

    public static class UILogMessageEx
    {
        public static string LogMessage(this string msg)
        {
            UIManagerComponent.Instance?.Get<View_TipComponent>()?.CreateMessage(msg);
            return msg;
        }
    }
}