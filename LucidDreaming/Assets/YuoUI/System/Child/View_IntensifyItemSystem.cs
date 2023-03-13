using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public class View_IntensifyItemCreateSystem : YuoSystem<View_IntensifyItemComponent>, IUICreate
    {
        protected override void Run(View_IntensifyItemComponent view)
        {
            //关闭窗口的事件注册,名字不同请自行更
        }
    }
}