using System.Collections;
using System.Collections.Generic;
using Card;
using UnityEngine;
using YuoLayout;
using YuoTools.Main.Ecs;

public class EnemyAI : YuoComponent
{
    public void RandomCard()
    {
        var card = CardLibrary.Instance.CreateCard(Entity, 1);
        YuoCardPanelItem carditem = YuoCardPanel.Instance.GetNewCard(card);
        //随机在一个位置出牌
        BattleMap map = RoundManager.Instance.NowMap;
        int x = Random.Range(0, BattleMap.MapSize);
        int y = Random.Range(0, BattleMap.MapSize);
        while (map.GetCard(x, y) != null)
        {
            x = Random.Range(0, BattleMap.MapSize);
            y = Random.Range(0, BattleMap.MapSize);
        }

        map.PlayerCheckAndEnterBoard(x, y, carditem);
    }
}

public class EnemyAISystem : YuoSystem<EnemyAI>
{
    protected override void Run(EnemyAI component)
    {
    }
}