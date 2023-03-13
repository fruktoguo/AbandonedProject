using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;
namespace YuoTools.UI{
public class View_CardIntensifyCreateSystem :YuoSystem<View_CardIntensifyComponent>, IUICreate {
protected override void Run(View_CardIntensifyComponent view) {
            //关闭窗口的事件注册,名字不同请自行更
view.Button_Close.SetUIClose(view.ViewName);
 view.Button_Mask.SetUIClose(view.ViewName);
}
}
public class View_CardIntensifyOpenSystem :YuoSystem<View_CardIntensifyComponent>, IUIOpen {
protected override void Run(View_CardIntensifyComponent view) {
}
}
public class View_CardIntensifyCloseSystem :YuoSystem<View_CardIntensifyComponent>, IUIClose {
protected override void Run(View_CardIntensifyComponent view) {
}
}
}
