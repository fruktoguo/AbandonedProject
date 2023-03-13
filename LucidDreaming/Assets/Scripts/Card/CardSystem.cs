using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuoTools.ECS;
using YuoTools.Extend.YuoMathf;
using YuoTools.Main.Ecs;

namespace Card
{
    public class CardSystem : YuoSystem<CardComponent>, IAwake
    {
        protected override void Run(CardComponent card)
        {
            
        }
    }

    public static partial class SaveGroup
    {
        public const string CardLibrary = "CardLibrary";
    }

    public class CardComponent : CardEffectBaseComponent
    {
        public int CardId;
        public string CardName;
        public string CardImage = "CardImage/01";
        public readonly List<YuoVector2Int> Scope = new();
        public override string Name => "基础卡牌数据";

        public override void CopyFrom(CardEffectBaseComponent other)
        {
            if (other is CardComponent c)
            {
                CardId = c.CardId;
                CardName = c.CardName;
                CardImage = c.CardImage;
                Scope.Clear();
                Scope.AddRange(c.Scope);

                Entity.EntityName = CardName;
            }
        }
    }


    public abstract class CardEffectBaseComponent : YuoComponent
    {
        public abstract void CopyFrom(CardEffectBaseComponent other);
    }

    #region System

    public class CardEnterSystem : YuoSystem<CardComponent>, ICardEnterScene
    {
        protected override void Run(CardComponent component)
        {
            ($"{component.CardName}__进入战场").Log();
        }
    }

    #endregion

    #region 接口

    public interface ICardEnterHand : ISystemTag
    {
    }

    public interface ICardEnterScene : ISystemTag
    {
    }
    public interface IMapStart : ISystemTag
    {
    }
    public interface IRoundOver : ISystemTag
    {
    }

    public interface IDestroy : ISystemTag
    {
    }

    public interface ICardSwap : ISystemTag
    {
    }

    #endregion
}