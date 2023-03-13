using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;
namespace YuoTools.UI{
public class View_DevelopCreateSystem :YuoSystem<View_DevelopComponent>, IUICreate {
protected override void Run(View_DevelopComponent view) {
            //关闭窗口的事件注册,名字不同请自行更
view.Button_Close.SetBtnClick(() => World.Main.GetComponent<UIManagerComponent>().Close(view.ViewName));
 view.Button_Mask.SetBtnClick(() => World.Main.GetComponent<UIManagerComponent>().Close(view.ViewName));
}
}
public class View_DevelopOpenSystem :YuoSystem<View_DevelopComponent>, IUIOpen {
protected override void Run(View_DevelopComponent view) {
}
}
public class View_DevelopCloseSystem :YuoSystem<View_DevelopComponent>, IUIClose {
protected override void Run(View_DevelopComponent view) {
}
}
}
