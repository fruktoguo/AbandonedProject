
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using YuoTools.ECS;
namespace YuoTools.UI
{
	public static partial class ViewType 
	{ 
		public const string MainMenu = "MainMenu";
	}
	public partial class View_MainMenuComponent : UIComponent 
	{

		private Button mButton_开始游戏 ;

		public Button Button_开始游戏 
		{
			get
			{
				if (mButton_开始游戏  == null)
					mButton_开始游戏  = rectTransform.Find("Item/BackGround/MenuItem/C_开始游戏 ").GetComponent<Button>();
				return mButton_开始游戏 ;
			}
		}

		private Button mButton_继续游戏;

		public Button Button_继续游戏
		{
			get
			{
				if (mButton_继续游戏 == null)
					mButton_继续游戏 = rectTransform.Find("Item/BackGround/MenuItem/C_继续游戏").GetComponent<Button>();
				return mButton_继续游戏;
			}
		}

		private Button mButton_CG;

		public Button Button_CG
		{
			get
			{
				if (mButton_CG == null)
					mButton_CG = rectTransform.Find("Item/BackGround/MenuItem/C_CG").GetComponent<Button>();
				return mButton_CG;
			}
		}

		private Button mButton_游戏设置;

		public Button Button_游戏设置
		{
			get
			{
				if (mButton_游戏设置 == null)
					mButton_游戏设置 = rectTransform.Find("Item/BackGround/MenuItem/C_游戏设置").GetComponent<Button>();
				return mButton_游戏设置;
			}
		}

		private Button mButton_结束;

		public Button Button_结束
		{
			get
			{
				if (mButton_结束 == null)
					mButton_结束 = rectTransform.Find("Item/BackGround/MenuItem/C_结束").GetComponent<Button>();
				return mButton_结束;
			}
		}
	}
}
