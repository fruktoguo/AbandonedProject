using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public partial class View_SaveComponent
    {
        public List<View_SaveItemComponent> saveItems = new();
        public int NowSaveIndex = -1;
    }

    public class View_SaveCreateSystem : YuoSystem<View_SaveComponent>, IUICreate
    {
        protected override async void Run(View_SaveComponent view)
        {
            view.Entity.EntityName = "存档面板";
            //关闭窗口的事件注册,名字不同请自行更
            view.Button_Close.SetBtnClick(() => World.Main.GetComponent<UIManagerComponent>().Close(view.ViewName));

            var item = view.Child_SaveItem;
            var save = World.Main.GetComponent<SaveManagerComponent>();
            view.NowSaveIndex = save.SaveCount;
            GameObject go = item.rectTransform.gameObject;
            Transform parent = item.rectTransform.parent;
            go.SetActive(false);
            view.Entity.RemoveChild(view.Child_SaveItem.Entity);
            view.RectTransform_AddItem.Hide();

            for (int i = 0; i < view.NowSaveIndex; i++)
            {
                await YuoWait.WaitTimeAsync(0.05f);
                var rect =
                    Object.Instantiate(go, parent).transform as
                        RectTransform;
                rect.gameObject.SetActive(true);
                var component = view.Entity.AddChild<View_SaveItemComponent>();
                component.rectTransform = rect;
                component.SaveItemId = i;
                view.saveItems.Add(component);
                World.RunSystem<IUICreate>(component);
                World.RunSystem<IUIActive>(component);
            }

            view.RectTransform_AddItem.Show();
            view.RectTransform_AddItem.SetAsLastSibling();

            view.RectTransform_AddItem.DOScaleX(0, 0.5f).From();
            view.RectTransform_AddItem.DOScaleY(0, 0.2f).From();

            view.Button_AddItem.SetBtnClick(AddItem);

            async void AddItem()
            {
                var rect =
                    Object.Instantiate(go, parent).transform as
                        RectTransform;
                if (rect != null)
                {
                    view.RectTransform_AddItem.Hide();

                    await World.Main.GetComponent<SaveManagerComponent>().CreateData(view.NowSaveIndex);
                    
                    rect.gameObject.SetActive(true);
                    var component = view.Entity.AddChild<View_SaveItemComponent>();
                    component.rectTransform = rect;
                    component.SaveItemId = view.NowSaveIndex++;
                    view.saveItems.Add(component);
                    World.RunSystem<IUICreate>(component);
                    World.RunSystem<IUIActive>(component);

                    await YuoWait.WaitTimeAsync(0.2f);
                    view.RectTransform_AddItem.Show();
                    view.RectTransform_AddItem.SetAsLastSibling();
                    view.RectTransform_AddItem.DOScaleX(0, 0.5f).From();
                    view.RectTransform_AddItem.DOScaleY(0, 0.2f).From();
                }
            }
        }
    }

    public class View_SaveOpenSystem : YuoSystem<View_SaveComponent>, IUIOpen
    {
        protected override void Run(View_SaveComponent view)
        {
        }
    }

    public class View_SaveCloseSystem : YuoSystem<View_SaveComponent>, IUIClose
    {
        protected override void Run(View_SaveComponent view)
        {
        }
    }
}