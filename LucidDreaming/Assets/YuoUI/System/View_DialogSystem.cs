using DG.Tweening;
using UnityEngine;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public class View_DialogCreateSystem : YuoSystem<View_DialogComponent>, IUICreate
    {
        protected override void Run(View_DialogComponent view)
        {
            //关闭窗口的事件注册,名字不同请自行更
            view.Button_Close.SetBtnClick(() => World.Main.GetComponent<UIManagerComponent>().Close(view.ViewName));
            view.ButtonSwitch_自动.OnClick = state =>
            {
                //切换自动模式
            };
            view.AutoShow = false;
            view.AutoHide = false;
            view.rectTransform.anchoredPosition =
                view.rectTransform.anchoredPosition.RSetY(-view.rectTransform.sizeDelta.y);
        }
    }

    public class View_DialogOpenSystem : YuoSystem<View_DialogComponent>, IUIOpen
    {
        protected override void Run(View_DialogComponent view)
        {
            view.rectTransform.Show();
            view.rectTransform.DOAnchorPosY(0, 0.3f);
        }
    }

    public class View_DialogCloseSystem : YuoSystem<View_DialogComponent>, IUIClose
    {
        protected override async void Run(View_DialogComponent view)
        {
            await view.rectTransform.DOAnchorPosY(-view.rectTransform.sizeDelta.y, 0.3f).AsyncWaitForCompletion();
            view.rectTransform.Hide();
        }
    }
}