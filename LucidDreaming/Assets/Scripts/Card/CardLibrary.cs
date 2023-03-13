using System.Collections.Generic;
using Card.CardEntry;
using Sirenix.OdinInspector;
using YuoLayout;
using YuoTools;
using YuoTools.ECS;
using YuoTools.Extend.YuoMathf;
using YuoTools.Main.Ecs;
using static YuoTools.ECS.SaveManagerComponent;
using Random = UnityEngine.Random;

namespace Card
{
    public class CardLibrary : YuoComponentGet<CardLibrary>
    {
        protected new static YuoEntity instanceEntity = World.Main;
        private string _path;
        private GameData _library;

        private long _cardLibraryID = "CardLibrary".GetHashCode();

        public override string Name => "卡牌库";

        public CardComponent CreateCard(YuoEntity handList, long cardID)
        {
            CardComponent card = handList.AddChild<CardComponent>();
            YuoEntity templateCard = World.Instance.GetEntity(cardID);
            foreach (var component in templateCard.Components)
            {
                if (component.Value is CardEffectBaseComponent cardEffectBaseComponent)
                {
                    var cardComponent = card.Entity.AddComponent(component.Key) as CardEffectBaseComponent;
                    cardComponent?.CopyFrom(cardEffectBaseComponent);
                }
            }

            return card;
        }

        [HorizontalGroup()]
        [Button("保存卡牌库")]
        void Save()
        {
            SaveManagerComponent.Instance.SaveEntity(Entity);
        }

        public void Load()
        {
            SaveManagerComponent.Instance.LoadEntity(Entity);
            Card.Clear();
            foreach (var card in Entity.GetChildren<CardComponent>())
            {
                Card.Add(card.CardName, card.Entity);
            }
        }

        [HorizontalGroup()]
        [Button("重置卡牌库")]
        public void TestAdd()
        {
            for (int i = 0; i < 10; i++)
            {
                var rename = $"测试卡牌_{i}";
                var card = Entity.AddChild<CardComponent>(rename.GetHashCode());
                card.CardId = i;
                card.CardName = rename;
                card.Scope.Add(new YuoVector2Int(1, 0));
                card.Scope.Add(new YuoVector2Int(1, 1));
                card.Scope.Add(new YuoVector2Int(-1, 0));
                var atk = card.Entity.AddComponent<CardBasicAtkComponent>();
                atk.AtkValue = Random.Range(1, 20);
                Card.Add(card.CardName, card.Entity);
            }
        }

        [HorizontalGroup()]
        [Button("清除卡牌库")]
        public void ClearLibrary()
        {
            foreach (var card in Entity.GetChildren<CardComponent>())
            {
                card.Entity.Dispose();
            }

            Card.Clear();
            Save();
        }

        public Dictionary<string, YuoEntity> Card = new();
    }

    public class CardLibrarySystem : YuoSystem<CardLibrary>, IAwake
    {
        protected override async void Run(CardLibrary component)
        {
            await YuoWait.WaitTimeAsync(0);

            component.Entity.EntityName = "卡牌库";
            var player1 = component.Entity.Parent.AddChild<PlayerComponent>("Player1");
            // World.Instance.GetEntity("Player1");
            var player2 = component.Entity.Parent.AddChild<PlayerComponent>("Player2");

            player1.Target = player2;
            player2.Target = player1;

            component.Load();

            if (component.Entity.Children.Count <= 0) return;
            foreach (CardComponent cardComponent in component.Entity.GetChildren<CardComponent>())
            {
                YuoCardPanel.Instance.CreateCard(component.CreateCard(player1.Entity, cardComponent.Entity.ID));
            }
        }
    }
}