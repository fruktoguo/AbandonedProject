using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public partial class View_MainMenuComponent
    {
        public List<Button> buttons = new List<Button>();
        public Button nowButton;

        public void NextButton(int i = 1)
        {
            if (buttons.Count == 0)
            {
                nowButton = null;
                return;
            }

            if (nowButton == null)
            {
                nowButton = buttons[0];
            }
            else
            {
                nowButton = buttons[(buttons.IndexOf(nowButton) + i).Residual(buttons.Count)];
            }

            HighLightManagerComponent.Instance.Select(nowButton);
        }
    }

    public class View_MainMenuCreateSystem : YuoSystem<View_MainMenuComponent>, IUICreate
    {
        protected override async void Run(View_MainMenuComponent view)
        {
            view.Entity.EntityName = "主菜单";
            //加载设置数据,并设置音量
            SoundManger.Instance.SetSoundVolume(World.Main.GetComponent<SettingDataComponent>().SoundVolume);
            SoundManger.Instance.SetBgVolume(World.Main.GetComponent<SettingDataComponent>().BgVolume);

            World.Main.GetComponent<YuoInputComponent>().Add(new("截屏")
            {
                key = KeyCode.Alpha3,
                OnDown = () =>
                {
                    TextureHelper.ScreenShootAndSave(Application.persistentDataPath + "/ScreenShoot.png");
                }
            });

            World.Scene.GetComponent<HighLightManagerComponent>().HighLight =
                Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Other/HighLight")).transform as RectTransform;
            World.Scene.GetComponent<HighLightManagerComponent>().Local = true;

            view.Button_开始游戏.SetBtnClick(async () =>
            {
                if (World.Main.GetComponent<UIManagerComponent>().TopView == view)
                {
                    await World.Main.GetComponent<UIManagerComponent>()?.Open(ViewType.Scene)!;
                    UIManagerComponent.Instance.Close(view.ViewName);
                }
            });
            view.Button_继续游戏.SetBtnClick(async () =>
            {
                if (World.Main.GetComponent<UIManagerComponent>().TopView == view)
                {
                    await World.Main.GetComponent<UIManagerComponent>()?.Open(ViewType.Save)!;
                }
            });
            view.Button_CG.SetBtnClick(async () =>
            {
                if (World.Main.GetComponent<UIManagerComponent>().TopView == view)
                {
                    await World.Main.GetComponent<UIManagerComponent>()?.Open(ViewType.CG)!;
                }
            });
            view.Button_游戏设置.SetBtnClick(async () =>
            {
                await World.Main.GetComponent<UIManagerComponent>()?.Open(ViewType.Setting)!;
            });
            view.Button_结束.SetBtnClick(() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            });
            var clickSound = await YuoWait.ResourcesLoadAsync<AudioClip>("Sounds/MainMenuClick");

            view.buttons.Adds(view.Button_开始游戏, view.Button_继续游戏, view.Button_CG, view.Button_游戏设置, view.Button_结束);

            for (int i = 0; i < view.buttons.Count; i++)
            {
                view.buttons[i].onClick.AddListener(ClickSound);
                view.buttons[i].SetHighLight(b => { view.nowButton = b as Button; });
            }

            view.Button_开始游戏.Select();

            void ClickSound()
            {
                SoundManger.Instance.PlaySound(clickSound);
            }
        }
    }

    public class View_MainMenuInputSystem : YuoSystem<View_MainMenuComponent, TopViewComponent>, IUpdate
    {
        float lastVertical;

        protected override void Run(View_MainMenuComponent view, TopViewComponent component2)
        {
            if (Input.GetButtonDown("Submit"))
            {
                if (view.nowButton != null)
                    ExecuteEvents.Execute(view.nowButton.gameObject, new PointerEventData(EventSystem.current),
                        ExecuteEvents.pointerClickHandler);
            }

            float axis = -Input.GetAxis("Vertical");
            if (axis.RAbs() > 0.1f)
            {
                if (lastVertical.ApEqual(0, 0.001f))
                {
                    //说明是刚刚按下
                    view.NextButton(axis > 0 ? 1 : -1);
                }

                lastVertical = axis;
            }
            else
            {
                lastVertical = 0;
            }
        }
    }

    public class View_MainMenuOpenSystem : YuoSystem<View_MainMenuComponent>, IUIActive
    {
        protected override void Run(View_MainMenuComponent view)
        {
            var sound = Resources.Load<AudioClip>("Sounds/BG_1");
            SoundManger.Instance.PlayBg(sound);
        }
    }
}