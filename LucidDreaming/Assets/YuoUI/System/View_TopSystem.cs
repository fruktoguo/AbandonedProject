using DG.Tweening;
using YuoTools.ECS;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public class View_TopCreateSystem : YuoSystem<View_TopComponent>, IUICreate
    {
        protected override void Run(View_TopComponent view)
        {
            //关闭窗口的事件注册,名字不同请自行更
            //取消默认动画事件
            view.AutoShow = false;
            view.AutoHide = false;
            view.rectTransform.anchoredPosition =
                view.rectTransform.anchoredPosition.RSetY(view.rectTransform.sizeDelta.y);
        }
    }

    public class View_TopOpenSystem : YuoSystem<View_TopComponent>, IUIOpen
    {
        protected override void Run(View_TopComponent view)
        {
            view.rectTransform.Show();
            view.rectTransform.DOAnchorPosY(0, 0.3f);
        }
    }

    public class View_TopCloseSystem : YuoSystem<View_TopComponent>, IUIClose
    {
        protected override async void Run(View_TopComponent view)
        {
            await view.rectTransform.DOAnchorPosY(view.rectTransform.sizeDelta.y, 0.3f).AsyncWaitForCompletion();
            view.rectTransform.Hide();
        }
    }
}