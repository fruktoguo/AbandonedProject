using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    [YuoSave(saveName: "CG存档数据", saveType: SaveType.SettingInfo, serializeType: SerializeType.NewtonsoftJson)]
    public class CGSaveDataComponent : YuoComponent
    {
        public Dictionary<int, bool> cgSaveData = new();
    }

    public class View_CGCreateSystem : YuoSystem<View_CGComponent>, IUICreate
    {
        // ReSharper disable Unity.PerformanceAnalysis
        protected override void Run(View_CGComponent view)
        {
            "创建CG界面".Log();
            //关闭窗口的事件注册,名字不同请自行更

            view.Button_Close.SetBtnClick(() =>
            {
                World.Main.GetComponent<UIManagerComponent>().Close(view.ViewName);
                // World.Main.GetComponent<UIManagerComponent>().Open(ViewType.MainMenu);
            });
            var save = World.Main.GetComponent<CGSaveDataComponent>() ?? World.Main.AddComponent<CGSaveDataComponent>();
            for (int i = 0; i < view.RectTransform_Content.childCount; i++)
            {
                if (save.cgSaveData.TryGetValue(i, out var open))
                {
                    if (open)
                    {
                        //隐藏遮罩
                        view.RectTransform_Content.GetChild(i).GetChild(0).gameObject.Hide();
                        //显示cg略缩图
                        var button = view.RectTransform_Content.GetChild(i).GetComponent<Button>();
                        button.image.enabled = true;
                        //添加事件
                        button.SetBtnClick(async () =>
                        {
                            var cgView =
                                (await World.Main.GetComponent<UIManagerComponent>().Open(ViewType.FullScreenCG)) as
                                View_FullScreenCGComponent;
                            cgView.Image_CG.sprite = button.image.sprite;
                        });
                    }
                }
                else
                {
                    save.cgSaveData.Add(i, false);
                }
            }
        }
    }

    public class View_CGOpenSystem : YuoSystem<View_CGComponent>, IUIOpen
    {
        protected override async void Run(View_CGComponent view)
        {
            await YuoWait.WaitTimeAsync(0.1f);
            view.ScrollRect_ScrollView.normalizedPosition = Vector2.one;
        }
    }

    public class View_CGActiveSystem : YuoSystem<View_CGComponent>, IUIActive
    {
        protected override void Run(View_CGComponent view)
        {
        }
    }
}