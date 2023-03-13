using DG.Tweening;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;
namespace YuoTools.UI
{
	public class View_RoleInfoCreateSystem :YuoSystem<View_RoleInfoComponent>, IUICreate
	{
		public override string Group =>"UI/RoleInfo";

		protected override void Run(View_RoleInfoComponent view)
		{
		}
	}
}
