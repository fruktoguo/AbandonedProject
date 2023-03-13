
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using YuoTools.ECS;
namespace YuoTools.UI
{
	public static partial class ViewType 
	{ 
		public const string D_Goods = "D_Goods";
	}
	public partial class View_D_GoodsComponent : UIComponent 
	{

		private View_GoodsComponent mChild_Goods;

		public View_GoodsComponent Child_Goods
		{
			get
			{
				if (mChild_Goods == null)
				{
					mChild_Goods = Entity.AddChild<View_GoodsComponent>();
					mChild_Goods.rectTransform = rectTransform.Find("D_Goods") as RectTransform;
				}
				return mChild_Goods;
			}
		}
	}}
