using YuoTools.Main.Ecs;

namespace Card.CardEntry
{
    public class CardBasicAtkComponent : CardEffectBaseComponent
    {
        public override string Name => "基础攻击效果";

        public override void CopyFrom(CardEffectBaseComponent other)
        {
            if (other is CardBasicAtkComponent c)
                AtkValue = c.AtkValue;
        }

        public int AtkValue;
    }

    public class CardAtkSystem : YuoSystem<CardBasicAtkComponent>, IRoundOver
    {
        protected override void Run(CardBasicAtkComponent component)
        {
           var player =  component.Entity.Parent.GetComponent<PlayerComponent>();
           if (player!=null)
           {
               player.Target.HP -= component.AtkValue;
           }
        }
    }

    public class PlayerComponent : YuoComponent
    {
        public int HP = 20;
        public PlayerComponent Target;
    }
}