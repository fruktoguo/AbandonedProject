using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public class View_MapCreateSystem : YuoSystem<View_MapComponent>, IUICreate
    {
        protected override void Run(View_MapComponent view)
        {
            //关闭窗口的事件注册,名字不同请自行更
            view.Button_Close.SetBtnClick(() => World.Main.GetComponent<UIManagerComponent>().Close(view.ViewName));
            view.Button_Mask.SetBtnClick(() => World.Main.GetComponent<UIManagerComponent>().Close(view.ViewName));
        }
    }

    public class View_MapOpenSystem : YuoSystem<View_MapComponent>, IUIOpen
    {
        protected override void Run(View_MapComponent view)
        {
        }
    }

    public class View_MapCloseSystem : YuoSystem<View_MapComponent>, IUIClose
    {
        protected override void Run(View_MapComponent view)
        {
        }
    }
}