using Card;
using UnityEngine;
using YuoTools.Extend.UI;
using YuoTools.Main.Ecs;
using YuoTools.UI;

public class SceneFactory : YuoSystem<SceneComponent>, IAwake
{
    protected override void Run(SceneComponent component)
    {
        Debug.Log("SceneFactoryFactory");

        World.Scene.AddComponent<HighLightManagerComponent>();
        World.Main.AddComponent<SettingDataComponent>();
        World.Main.AddComponent<YuoLanguageManager>();
        World.Main.AddComponent<YuoUIDragManager>();

        CardLibrary.SetInstance(World.Main.AddChild("CardLibrary"));
        
        RoundManager.SetInstance(World.Main.AddChild("RoundManager"));
    }
}