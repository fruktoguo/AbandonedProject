using System;
using UnityEngine;
using UnityEngine.EventSystems;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Extend.UI;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    [YuoSave(saveName: "设置",saveType: SaveType.SettingInfo)]
    public class SettingDataComponent : YuoComponent
    {
        public string Language = "zh-CN";
        public float BgVolume = 1;
        public float SoundVolume = 1;
    }

    public class View_SettingCreateSystem : YuoSystem<View_SettingComponent>, IUICreate
    {
        protected override void Run(View_SettingComponent view)
        {
            //关闭窗口的事件注册,名字不同请自行更
            view.Button_Close.SetBtnClick(() => World.Main.GetComponent<UIManagerComponent>().Close(view.ViewName));
            view.Button_Mask.SetBtnClick(() => World.Main.GetComponent<UIManagerComponent>().Close(view.ViewName));
            view.Toggle_FullScreen.SetIsOnWithoutNotify(Screen.fullScreenMode != FullScreenMode.Windowed);
            view.Toggle_FullScreen.onValueChanged.AddListener(isOn =>
            {
                Screen.fullScreenMode = isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            });
            view.Slider_Music.onValueChanged.AddListener(value =>
            {
                World.Main.GetComponent<SettingDataComponent>().BgVolume = value;
                SoundManger.Instance.SetBgVolume(value);
            });
            view.Slider_Sound.onValueChanged.AddListener(value =>
            {
                World.Main.GetComponent<SettingDataComponent>().SoundVolume = value;
                SoundManger.Instance.SetSoundVolume(value);
            });
            var clickSound = Resources.Load<AudioClip>("Sounds/MainMenuClick");

            var trigger = view.Slider_Music.gameObject.AddComponent<EventTrigger>().triggers;
            var selectTrigger = new EventTrigger.Entry()
            {
                eventID = EventTriggerType.PointerUp,
                callback = new EventTrigger.TriggerEvent()
            };

            //添加音量选择键抬起时音效
            selectTrigger.callback.AddListener(_ => { SoundManger.Instance.PlaySound(clickSound); });
            trigger.Add(selectTrigger);
            trigger = view.Slider_Sound.gameObject.AddComponent<EventTrigger>().triggers;
            selectTrigger = new EventTrigger.Entry()
            {
                eventID = EventTriggerType.PointerUp,
                callback = new EventTrigger.TriggerEvent()
            };
            selectTrigger.callback.AddListener(_ => { SoundManger.Instance.PlaySound(clickSound); });
            trigger.Add(selectTrigger);

            //添加关闭按钮的音效
            view.Button_Close.onClick.AddListener(() => { SoundManger.Instance.PlaySound(clickSound); });

            //从数据中获取并设置音量
            view.Slider_Sound.value = World.Main.GetComponent<SettingDataComponent>().SoundVolume;
            view.Slider_Music.value = World.Main.GetComponent<SettingDataComponent>().BgVolume;

            //设置语言
            view.InputField_CreateLanguage.onSubmit.AddListener(async value =>
            {
                var path = await YuoLanguageManager.Instance.CreateLanguageData(value);
                //打开目标文件夹
                System.Diagnostics.Process.Start(path);
            });
            
            view.Button_返回主菜单.onClick.AddListener( () =>
            {
                SoundManger.Instance.PlaySound(clickSound);
                World.Main.GetComponent<UIManagerComponent>().CloseAll();
                World.Main.GetComponent<UIManagerComponent>().Open(ViewType.MainMenu);
            });
        }
    }

    public class View_SettingOpenSystem : YuoSystem<View_SettingComponent>, IUIOpen
    {
        protected override void Run(View_SettingComponent view)
        {
            if (UIManagerComponent.Instance.IsOpen(ViewType.MainMenu))
            {
                view.Button_返回主菜单.transform.Hide();
            }
            else
            {
                view.Button_返回主菜单.transform.Show();
            }
        }
    }

    public class View_SettingCloseSystem : YuoSystem<View_SettingComponent>, IUIClose
    {
        protected override void Run(View_SettingComponent view)
        {
        }
    }
}