using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;
using YuoTools.ECS;
using YuoTools.Extend.UI;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public partial class View_SaveItemComponent
    {
        public int SaveItemId;
        [HideInInspector]
        public TweenerCore<Vector3, Vector3, VectorOptions> tween;
    }

    public class View_SaveItemCreateSystem : YuoSystem<View_SaveItemComponent>, IUICreate
    {
        protected override void Run(View_SaveItemComponent component)
        {
            component.Entity.EntityName = $"SaveItem_{component.SaveItemId}";
            //添加划过事件
            var eventTrigger = component.Button_Click.GetComponent<EventTrigger>();
            eventTrigger.triggers[0].callback.AddListener(_ =>
            {
                if (component.tween != null) component.tween.Kill();
                component.tween = component.Button_Delete.transform.parent.DOScale(1, 0.3f);
            });
            eventTrigger.triggers[1].callback.AddListener(_ =>
            {
                if (component.tween != null) component.tween.Kill();
                component.Button_Delete.transform.parent.DOScale(0, 0.3f);
            });
        }
    }

    public class View_SaveItemActiveSystem : YuoSystem<View_SaveItemComponent>, IUIActive
    {
        protected override async void Run(View_SaveItemComponent view)
        {
            view.rectTransform.DOScaleX(0, 0.5f).From();
            view.rectTransform.DOScaleY(0, 0.2f).From();


            var save = World.Main.GetComponent<SaveManagerComponent>();
            var data = save.LoadSceneData(view.SaveItemId);
            var info = await save.GetSaveInfo(view.SaveItemId);
            view.Text_Index.text = $"No.{view.SaveItemId:00}";
            view.Text_Date.text = info.LastWriteTime.ToString("yyyy/MM/dd");
            view.Text_Date_Time.text = info.LastWriteTime.ToString("HH:mm:ss");
            view.RawImage_SaveIcon.texture = await save.GetSaveImage(view.SaveItemId);
            if (data.OtherData.TryGetValue(SaveManagerComponent.OtherDataKey.Days, out var value))
            {
                view.Text_Day.text = int.Parse(value).ToString("000") + "天".Language();
            }
            else
            {
                view.Text_Day.text = "001" + "天".Language();
            }
        }
    }
}