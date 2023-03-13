using System;
using System.Threading.Tasks;
using Card.CardEntry;
using UnityEngine;
using YuoTools;
using YuoTools.Main.Ecs;
using YuoTools.UI;

namespace Card
{
    public class RoundManager : YuoComponentGet<RoundManager>
    {
        readonly BattleMap[] _battleGround = new BattleMap[3];

        public PlayerComponent player1;
        public PlayerComponent player2;

        public void Init()
        {
            for (int i = 0; i < 3; i++)
            {
                var battleMap = Entity.AddChild<BattleMap>($"BattleMap_{i}");
                _battleGround[i] = battleMap;
            }
        }

        private int _index = 0;
        public BattleMap NowMap => _battleGround[_index];

        /// <summary>
        /// 出牌完毕进入下一个回合
        /// </summary>
        public void NextMap()
        {
            if (_index + 1 >= _battleGround.Length)
            {
                RoundOver();
                return;
            }
            SelectMap(1);
            World.RunSystem<IMapStart>(player1.Entity);
            World.RunSystem<IMapStart>(player2.Entity);
        }

        public async void SelectMap(int addindex)
        {
            addindex += _index;
            addindex.Clamp(2);
            var battleView = UIManagerComponent.Instance.Get<View_BattleComponent>();
            await battleView.HideBattle();
            _index = addindex;

            $"切换战场{_index + 1}".LogMessage();

            battleView.TextMeshProUGUI_Round.text = $"{_index + 1}/3";
            battleView.SetBattle(NowMap);
            battleView.Text_提示.text = $"当前重合数--{NowMap.GetIntersect().intersect.Count}";
            YuoGridItem.SetBattle(NowMap);
            await battleView.ShowBattle();
        }

        public void SetBattle()
        {
            var nowMap = NowMap;
        }

        public void RoundOver()
        {
            _index = 0;
            World.RunSystem<IRoundOver>(player1);
            World.RunSystem<IRoundOver>(player2);
            UIManagerComponent.Instance.Get<View_BattleComponent>().StartBattle();
        }
    }

    public class RoundAwakeSystem : YuoSystem<RoundManager>, IAwake
    {
        protected override void Run(RoundManager component)
        {
            component.Init();
        }
    }
}