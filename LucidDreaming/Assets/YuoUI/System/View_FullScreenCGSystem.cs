using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public class View_FullScreenCGCreateSystem : YuoSystem<View_FullScreenCGComponent>, IUICreate
    {
        protected override void Run(View_FullScreenCGComponent view)
        {
            //关闭窗口的事件注册,名字不同请自行更
            view.Button_Mask.SetBtnClick(() => World.Main.GetComponent<UIManagerComponent>().Close(view.ViewName));
        }
    }

    public class View_FullScreenCGOpenSystem : YuoSystem<View_FullScreenCGComponent>, IUIOpen
    {
        protected override void Run(View_FullScreenCGComponent view)
        {
            view.Button_Mask.Select();
        }
    }

    public class View_FullScreenCGCloseSystem : YuoSystem<View_FullScreenCGComponent>, IUIClose
    {
        protected override void Run(View_FullScreenCGComponent view)
        {
        }
    }
}