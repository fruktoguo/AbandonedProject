using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Extend.UI.YuoLayout;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public partial class View_TipMessageComponent
    {
        public float ShowTime = 0.5f;
        public Vector2 TargetPos;
    }

    public class View_TipMessageActiveSystem : YuoSystem<View_TipMessageComponent>, IUICreate
    {
        public override string Group => "TipMessage";
        protected override async void Run(View_TipMessageComponent view)
        {
            view.rectTransform.SetSizeX(view.Text_Message.rectTransform.GetWidth() + 40);
            view.rectTransform.SetAsLastSibling();
            //透明度归零
            view.Image_TipMessage.SetColorA(0);
            view.Text_Message.color = view.Image_TipMessage.color;
            //渐入
            view.Image_TipMessage.DOFade(1, 0.3f);
            view.Text_Message.DOFade(1, 0.3f);
            //等待
            await YuoWait.WaitTimeAsync(view.ShowTime + 0.3f);
            //渐出
            view.Image_TipMessage.DOFade(0, 0.3f);
            view.Text_Message.DOFade(0, 0.3f);
            await YuoWait.WaitTimeAsync(0.3f);

            view.Entity.Parent.GetComponent<View_TipComponent>().Recycle(view);
        }
    }

    public class View_TipMessageUpdateSystem : YuoSystem<View_TipMessageComponent>, IUpdate
    {
        public override string Group => "TipMessage";
        protected override void Run(View_TipMessageComponent view)
        {
            if (Vector2.Distance(view.TargetPos, view.rectTransform.anchoredPosition) > 0.1f)
            {
                view.rectTransform.anchoredPosition = Vector2.Lerp(view.rectTransform.anchoredPosition,
                    view.TargetPos, 10f * Time.deltaTime);
            }
        }
    }
    public class View_TipMessageDestroySystem : YuoSystem<View_TipMessageComponent>, IDestroy
    {
        public override string Group => "TipMessage";

        protected override void Run(View_TipMessageComponent view)
        {
            Object.Destroy(view.rectTransform.gameObject);
        }
    }
}