using DG.Tweening;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;
namespace YuoTools.UI
{
	public class View_GridCreateSystem :YuoSystem<View_GridComponent>, IUICreate
	{
		public override string Group =>"UI/Grid";

		protected override void Run(View_GridComponent view)
		{
		}
	}
}
