using System.Reflection;
using DG.Tweening;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public class View_SceneCreateSystem : YuoSystem<View_SceneComponent>, IUICreate
    {
        protected override void Run(View_SceneComponent view)
        {
            //关闭窗口的事件注册,名字不同请自行更
            view.Button_设置.SetUIOpen(ViewType.Setting);
            view.Button_商城.SetUIOpen(ViewType.Shop);
            view.Button_商城.SetUIOpen(ViewType.Shop);
            view.Button_工作.SetUIOpen(ViewType.Work);
            view.Button_状态  .SetUIOpen(ViewType.Develop);
            view.Button_调查  .SetUIOpen(ViewType.Map);
            
            view.ButtonSwitch_PanelSwitch.OnClick = x =>
            {
                view.Image_选项.rectTransform.DOAnchorPosX(x ? view.Image_选项.rectTransform.rect.width : 0, 0.5f)
                    .SetId("选项");
            };
            view.Entity.EntityName = "游戏场景面板";
        }
    }

    public class View_SceneOpenSystem : YuoSystem<View_SceneComponent>, IUIOpen
    {
        protected override async void Run(View_SceneComponent view)
        {
            (await UIManagerComponent.Instance.Open(ViewType.Top)).Entity.EntityName = "对话框";
            (await UIManagerComponent.Instance.Open(ViewType.Dialog)).Entity.EntityName = "顶部栏";
        }
    }

    public class View_SceneCloseSystem : YuoSystem<View_SceneComponent>, IUIClose
    {
        protected override void Run(View_SceneComponent view)
        {
            UIManagerComponent.Instance.Close(ViewType.Top);
            UIManagerComponent.Instance.Close(ViewType.Dialog);
        }
    }
}