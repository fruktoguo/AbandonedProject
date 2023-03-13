using System.Threading.Tasks;
using Card;
using DG.Tweening;
using UnityEngine;
using YuoLayout;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Extend.UI.YuoLayout;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public partial class View_BattleComponent
    {
        public YuoCardPanel layout;

        public void SwitchCardList()
        {
            if (layout.gameObject.activeSelf)
            {
                layout.EndAnima();
            }
            else
            {
                layout.StartAnima();
            }
        }

        public async Task HideBattle()
        {
            await Image_棋盘.rectTransform.DOScale(Vector3.one.RSetY(0), 0.5f).SetEase(Ease.OutExpo).AsyncWaitForCompletion();
        }

        public async Task ShowBattle()
        {
            await Image_棋盘.rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutExpo).AsyncWaitForCompletion();
        }

        public void SetBattle(BattleMap battle)
        {
            foreach (var grid in all_View_GridComponent)
            {
                grid.Image_Card.sprite = null;
                grid.Image_Card.gameObject.Hide();
            }

            foreach (var item in battle.GetAllCard())
            {
                all_View_GridComponent[item.pos].Image_Card.gameObject.Show();
                all_View_GridComponent[item.pos].Image_Card.sprite = Resources.Load<Sprite>(item.card.CardImage);
            }
        }

        public void StartBattle()
        {
            layout.EndAnima();
        }
    }

    public class View_BattleCreateSystem : YuoSystem<View_BattleComponent>, IUICreate
    {
        public override string Group => "UI/Battle";

        protected override void Run(View_BattleComponent view)
        {
            view.FindAll();
            //关闭窗口的事件注册,名字不同请自行更
            view.Button_Close.SetUIClose(view.ViewName);
            view.layout = view.RectTransform_CardPanel.GetComponent<YuoCardPanel>();
            view.Button_Switch.SetBtnClick(view.SwitchCardList);

            view.Button_顺序按钮上.SetBtnClick(() => RoundManager.Instance.SelectMap(-1));
            view.Button_顺序按钮下.SetBtnClick(() => RoundManager.Instance.SelectMap(1));
        }
    }

    public class View_BattleOpenSystem : YuoSystem<View_BattleComponent>, IUIOpen
    {
        public override string Group => "UI/Battle";
        protected override void Run(View_BattleComponent view)
        {
        }
    }

    public class View_BattleCloseSystem : YuoSystem<View_BattleComponent>, IUIClose
    {
        public override string Group => "UI/Battle";
        protected override void Run(View_BattleComponent view)
        {
        }
    }

    public interface IEnterBattlefield : ISystemTag
    {
    }
}