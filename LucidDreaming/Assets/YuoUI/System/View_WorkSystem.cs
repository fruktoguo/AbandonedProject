using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public class View_WorkCreateSystem : YuoSystem<View_WorkComponent>, IUICreate
    {
        protected override void Run(View_WorkComponent view)
        {
            //关闭窗口的事件注册,名字不同请自行更
            view.Button_Close.SetUIClose(view.ViewName);
            view.Button_Mask.SetUIClose(view.ViewName);

            view.Button_外卖.SetBtnClick(() =>
            {
                view.Image_选择.sprite = view.Button_外卖.image.sprite;
                view.Text_介绍.text =
                    $"送外卖的,跑快快,日薪<color={YuoColorText.橙红色}>150</color>元,敏捷加<color={YuoColorText.卡其布}>200</color>点";
            });
            view.Button_服务员.SetBtnClick(() =>
            {
                view.Image_选择.sprite = view.Button_服务员.image.sprite;
                view.Text_介绍.text =
                    $"服务员,慢悠悠,日薪<color={YuoColorText.橙红色}>250</color>元,智力加<color={YuoColorText.军校蓝}>200</color>点";
            });
            view.Button_翻译.SetBtnClick(() =>
            {
                view.Image_选择.sprite = view.Button_翻译.image.sprite;
                view.Text_介绍.text =
                    $"翻译,动脑子,日薪<color={YuoColorText.橙红色}>500</color>元,智力加<color={YuoColorText.巧克力}>200</color>点";
            });
        }
    }

    public class View_WorkOpenSystem : YuoSystem<View_WorkComponent>, IUIOpen
    {
        protected override void Run(View_WorkComponent view)
        {
        }
    }

    public class View_WorkCloseSystem : YuoSystem<View_WorkComponent>, IUIClose
    {
        protected override void Run(View_WorkComponent view)
        {
        }
    }
}