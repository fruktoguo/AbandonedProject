using System.Collections.Generic;
using Card;
using UnityEngine;
using YuoLayout;
using YuoTools;
using YuoTools.Extend.YuoMathf;
using YuoTools.Main.Ecs;
using YuoTools.UI;

    public class BattleMap : YuoComponent
    {
        //4x4
        [SerializeField] private readonly MapItem[][] _maps = new MapItem[4][];

        public Dictionary<YuoVector2Int, CardComponent> Player = new();
        public Dictionary<YuoVector2Int, CardComponent> Enemy = new();

        
        public const int MapSize = 4;
        public class MapItem
        {
            public CardComponent card;
            public YuoVector2Int pos;
        }

        public void Init()
        {
            for (int i = 0; i < _maps.Length; i++)
            {
                _maps[i] = new MapItem[4];
                for (int y = 0; y < 4; y++)
                {
                    _maps[i][y] = new MapItem
                    {
                        pos = new YuoVector2Int(i, y)
                    };
                }
            }
        }

        public CardComponent GetCard(int x, int y)
        {
            return _maps[x][y].card;
        }

        public bool TyeGetCard(int x, int y, out CardComponent card)
        {
            if (_maps[x][y].card != null)
            {
                card = _maps[x][y].card;
                return true;
            }

            card = null;
            return false;
        }


        public MapItem GetMapItem(int index)
        {
            return _maps[index / 4][index % 4];
        }

        public List<MapItem> GetScope(int x, int y, CardComponent card)
        {
            var itemPos = new YuoVector2Int(x, y);
            List<MapItem> list = new List<MapItem>();

            foreach (var pos in card.Scope)
            {
                YuoVector2Int newPos = itemPos + pos;
                if (newPos.x is >= 0 and < 4 && newPos.y is >= 0 and < 4)
                {
                    list.Add(_maps[newPos.y][newPos.x]);
                }
            }

            return list;
        }

        private int _num = 0;

        public (List<MapItem> intersect, List<MapItem> scope) GetIntersect(int x, int y, CardComponent addCard,
            bool isPlayer)
        {
            List<MapItem> playerScope = new();

            foreach (var card in Player)
            {
                var scope = GetScope(card.Key.x, card.Key.y, card.Value);
                foreach (var mapItem in scope)
                {
                    if (!playerScope.Contains(mapItem))
                        playerScope.Add(mapItem);
                }
            }

            List<MapItem> enemyScope = new();

            foreach (var card in Enemy)
            {
                var scope = GetScope(card.Key.x, card.Key.y, card.Value);
                foreach (var mapItem in scope)
                {
                    if (!enemyScope.Contains(mapItem))
                        enemyScope.Add(mapItem);
                }
            }

            //测试用,让玩家可以打出敌人的卡牌
            if (Player.Count > 0) isPlayer = false;

            if (isPlayer)
            {
                var scope = GetScope(x, y, addCard);
                foreach (var mapItem in scope)
                {
                    if (!playerScope.Contains(mapItem))
                        playerScope.Add(mapItem);
                }
            }
            else
            {
                var scope = GetScope(x, y, addCard);
                foreach (var mapItem in scope)
                {
                    if (!enemyScope.Contains(mapItem))
                        enemyScope.Add(mapItem);
                }
            }

            List<MapItem> intersectList = new();
            List<MapItem> scopeList = new();

            foreach (var mapItem in playerScope)
            {
                if (enemyScope.Contains(mapItem))
                    intersectList.Add(mapItem);
            }

            foreach (var mapItem in playerScope)
            {
                if (!intersectList.Contains(mapItem))
                    scopeList.Add(mapItem);
            }

            foreach (var mapItem in enemyScope)
            {
                if (!intersectList.Contains(mapItem))
                    scopeList.Add(mapItem);
            }

            return (intersectList, scopeList);
        }

        public (List<MapItem> intersect, List<MapItem> scope) GetIntersect()
        {
            List<MapItem> playerScope = new();

            foreach (var card in Player)
            {
                var scope = GetScope(card.Key.x, card.Key.y, card.Value);
                foreach (var mapItem in scope)
                {
                    if (!playerScope.Contains(mapItem))
                        playerScope.Add(mapItem);
                }
            }

            List<MapItem> enemyScope = new();

            foreach (var card in Enemy)
            {
                var scope = GetScope(card.Key.x, card.Key.y, card.Value);
                foreach (var mapItem in scope)
                {
                    if (!enemyScope.Contains(mapItem))
                        enemyScope.Add(mapItem);
                }
            }

            List<MapItem> intersectList = new();

            List<MapItem> scopeList = new();

            foreach (var mapItem in playerScope)
            {
                if (enemyScope.Contains(mapItem))
                    intersectList.Add(mapItem);
            }

            foreach (var mapItem in playerScope)
            {
                if (!intersectList.Contains(mapItem))
                    scopeList.Add(mapItem);
            }

            foreach (var mapItem in enemyScope)
            {
                if (!intersectList.Contains(mapItem))
                    scopeList.Add(mapItem);
            }

            return (intersectList, scopeList);
        }

        public bool PlayerCheckAndEnterBoard(int x, int y, YuoCardPanelItem card, bool isPlayer = true)
        {
            if (_num >= 2)
            {
                return false;
            }

            if (_num >= 1)
            {
                isPlayer = false;
            }

            if (TyeGetCard(x, y, out var oldCard))
            {
                $"当前位置{x}_{y}已经有卡牌[{oldCard.CardName}]了".LogMessage();

                World.RunSystem<ICardSwap>(oldCard.Entity);
                return false;
            }

            _maps[x][y].card = card.Data.CardComponent;
            if (isPlayer)
            {
                Player.Add(new YuoVector2Int(x, y), card.Data.CardComponent);
            }
            else
            {
                Enemy.Add(new YuoVector2Int(x, y), card.Data.CardComponent);
            }

            //卡牌入场

            YuoGridItem.NowItem.SetCard(card);


            World.RunSystem<ICardEnterScene>(card.Data.CardComponent.Entity);

            _num++;

            if (_num >= 2)
            {
                Entity.Parent.GetComponent<RoundManager>().NextMap();
            }

            return true;
        }

        public List<(int pos, CardComponent card)> GetAllCard()
        {
            List<(int pos, CardComponent card)> cards = new();
            for (int x = 0; x < _maps.Length; x++)
            {
                for (int y = 0; y < _maps[x].Length; y++)
                {
                    if (_maps[x][y].card != null)
                    {
                        cards.Add((x + y * _maps.Length, _maps[x][y].card));
                    }
                }
            }

            return cards;
        }

        public enum MapState
        {
            Normal = 0,
            Select = 1,
            Coincide = 2
        }
    }

    public class BattleMapSystem : YuoSystem<BattleMap>, IAwake
    {
        protected override void Run(BattleMap component)
        {
            component.Init();
        }
    }
