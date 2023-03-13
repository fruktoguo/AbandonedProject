using UnityEngine;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Extend.UI;
using YuoTools.Main.Ecs;
using YuoTools.UI;

namespace YuoTools.Extend
{
    [SystemOrder(short.MinValue)]
    public class SceneInitSystem : YuoSystem<SceneComponent>, IAwake
    {
        public class UnityEngineLog : ECS.YuoLog.LogComponent
        {
            public override T Log<T>(T msg)
            {
                Debug.Log(msg);
                return msg;
            }

            public override T Error<T>(T msg)
            {
                Debug.LogError(msg);
                return msg;
            }
        }
        protected override void Run(SceneComponent component)
        {
            ECS.YuoLog.Open(new UnityEngineLog());
            ECS.YuoLog.Log("Init");
            //UI管理组件
            World.Main.AddComponent<UIManagerComponent>();
            World.Main.AddComponent<UIEventSystemManager>();
            
            //触摸控制器
            World.Main.AddComponent<YuoInputPointerComponent>();
            //控制器
            World.Main.AddComponent<YuoInputComponent>();
            
            CoroutineLockComponent.SetInstance(World.Scene.AddChild());
            
            World.Main.AddComponent<YuoInputCheckComponent>().Connect<InputCheckBaseComponent>();

            //存档管理器
            YuoSaveComponent.SetInstance(World.Main);
            
            // World.Main.AddComponent<SaveManagerComponent>();
            //游戏随机管理器
            World.Scene.AddComponent<YuoRandom>();
        }
    }
}