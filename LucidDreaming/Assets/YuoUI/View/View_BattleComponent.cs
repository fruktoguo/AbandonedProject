using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using YuoTools.Main.Ecs;
using Sirenix.OdinInspector;
using TMPro;

namespace YuoTools.UI
{
	public partial class View_BattleComponent : UIComponent 
	{


		private Button mButton_Mask;

		public Button Button_Mask
		{
			get
			{
				if (mButton_Mask == null)
					mButton_Mask = rectTransform.Find("C_Mask").GetComponent<Button>();
				return mButton_Mask;
			}
		}


		private Button mButton_Close;

		public Button Button_Close
		{
			get
			{
				if (mButton_Close == null)
					mButton_Close = rectTransform.Find("Item/C_Close").GetComponent<Button>();
				return mButton_Close;
			}
		}


		private Image mImage_棋盘;

		public Image Image_棋盘
		{
			get
			{
				if (mImage_棋盘 == null)
					mImage_棋盘 = rectTransform.Find("Item/C_棋盘").GetComponent<Image>();
				return mImage_棋盘;
			}
		}


		private GridLayoutGroup mGridLayoutGroup_棋盘;

		public GridLayoutGroup GridLayoutGroup_棋盘
		{
			get
			{
				if (mGridLayoutGroup_棋盘 == null)
					mGridLayoutGroup_棋盘 = rectTransform.Find("Item/C_棋盘").GetComponent<GridLayoutGroup>();
				return mGridLayoutGroup_棋盘;
			}
		}


		private Button mButton_顺序按钮上;

		public Button Button_顺序按钮上
		{
			get
			{
				if (mButton_顺序按钮上 == null)
					mButton_顺序按钮上 = rectTransform.Find("Item/水晶/C_顺序按钮上").GetComponent<Button>();
				return mButton_顺序按钮上;
			}
		}


		private Button mButton_顺序按钮下;

		public Button Button_顺序按钮下
		{
			get
			{
				if (mButton_顺序按钮下 == null)
					mButton_顺序按钮下 = rectTransform.Find("Item/水晶/C_顺序按钮下").GetComponent<Button>();
				return mButton_顺序按钮下;
			}
		}


		private TextMeshProUGUI mTextMeshProUGUI_Round;

		public TextMeshProUGUI TextMeshProUGUI_Round
		{
			get
			{
				if (mTextMeshProUGUI_Round == null)
					mTextMeshProUGUI_Round = rectTransform.Find("Item/水晶/C_Round").GetComponent<TextMeshProUGUI>();
				return mTextMeshProUGUI_Round;
			}
		}


		private RectTransform mRectTransform_CardPanel;

		public RectTransform RectTransform_CardPanel
		{
			get
			{
				if (mRectTransform_CardPanel == null)
					mRectTransform_CardPanel = rectTransform.Find("Item/C_CardPanel").GetComponent<RectTransform>();
				return mRectTransform_CardPanel;
			}
		}


		private Image mImage_手牌;

		public Image Image_手牌
		{
			get
			{
				if (mImage_手牌 == null)
					mImage_手牌 = rectTransform.Find("Item/C_手牌").GetComponent<Image>();
				return mImage_手牌;
			}
		}


		private Button mButton_Switch;

		public Button Button_Switch
		{
			get
			{
				if (mButton_Switch == null)
					mButton_Switch = rectTransform.Find("Item/C_Switch").GetComponent<Button>();
				return mButton_Switch;
			}
		}


		private Text mText_提示;

		public Text Text_提示
		{
			get
			{
				if (mText_提示 == null)
					mText_提示 = rectTransform.Find("Item/C_提示").GetComponent<Text>();
				return mText_提示;
			}
		}


		private Image mImage_BattlePanel;

		public Image Image_BattlePanel
		{
			get
			{
				if (mImage_BattlePanel == null)
					mImage_BattlePanel = rectTransform.Find("Item/C_BattlePanel").GetComponent<Image>();
				return mImage_BattlePanel;
			}
		}


		private View_RoleInfoComponent mChild_RoleInfo_Enemy;

		public View_RoleInfoComponent Child_RoleInfo_Enemy
		{
			get
			{
				if (mChild_RoleInfo_Enemy == null)
				{
					mChild_RoleInfo_Enemy = Entity.AddChild<View_RoleInfoComponent>();
					mChild_RoleInfo_Enemy.Entity.EntityName = "RoleInfo_Enemy";
					mChild_RoleInfo_Enemy.rectTransform = rectTransform.Find("Item/D_RoleInfo_Enemy") as RectTransform;
					World.RunSystem<IUICreate>(mChild_RoleInfo_Enemy);
				}
				return mChild_RoleInfo_Enemy;
			}
		}

		private View_RoleInfoComponent mChild_RoleInfo_Player;

		public View_RoleInfoComponent Child_RoleInfo_Player
		{
			get
			{
				if (mChild_RoleInfo_Player == null)
				{
					mChild_RoleInfo_Player = Entity.AddChild<View_RoleInfoComponent>();
					mChild_RoleInfo_Player.Entity.EntityName = "RoleInfo_Player";
					mChild_RoleInfo_Player.rectTransform = rectTransform.Find("Item/D_RoleInfo_Player") as RectTransform;
					World.RunSystem<IUICreate>(mChild_RoleInfo_Player);
				}
				return mChild_RoleInfo_Player;
			}
		}

		private View_GridComponent mChild_Grid_1;

		public View_GridComponent Child_Grid_1
		{
			get
			{
				if (mChild_Grid_1 == null)
				{
					mChild_Grid_1 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_1.Entity.EntityName = "Grid_1";
					mChild_Grid_1.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_1") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_1);
				}
				return mChild_Grid_1;
			}
		}

		private View_GridComponent mChild_Grid_2;

		public View_GridComponent Child_Grid_2
		{
			get
			{
				if (mChild_Grid_2 == null)
				{
					mChild_Grid_2 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_2.Entity.EntityName = "Grid_2";
					mChild_Grid_2.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_2") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_2);
				}
				return mChild_Grid_2;
			}
		}

		private View_GridComponent mChild_Grid_3;

		public View_GridComponent Child_Grid_3
		{
			get
			{
				if (mChild_Grid_3 == null)
				{
					mChild_Grid_3 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_3.Entity.EntityName = "Grid_3";
					mChild_Grid_3.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_3") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_3);
				}
				return mChild_Grid_3;
			}
		}

		private View_GridComponent mChild_Grid_4;

		public View_GridComponent Child_Grid_4
		{
			get
			{
				if (mChild_Grid_4 == null)
				{
					mChild_Grid_4 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_4.Entity.EntityName = "Grid_4";
					mChild_Grid_4.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_4") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_4);
				}
				return mChild_Grid_4;
			}
		}

		private View_GridComponent mChild_Grid_5;

		public View_GridComponent Child_Grid_5
		{
			get
			{
				if (mChild_Grid_5 == null)
				{
					mChild_Grid_5 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_5.Entity.EntityName = "Grid_5";
					mChild_Grid_5.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_5") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_5);
				}
				return mChild_Grid_5;
			}
		}

		private View_GridComponent mChild_Grid_6;

		public View_GridComponent Child_Grid_6
		{
			get
			{
				if (mChild_Grid_6 == null)
				{
					mChild_Grid_6 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_6.Entity.EntityName = "Grid_6";
					mChild_Grid_6.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_6") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_6);
				}
				return mChild_Grid_6;
			}
		}

		private View_GridComponent mChild_Grid_7;

		public View_GridComponent Child_Grid_7
		{
			get
			{
				if (mChild_Grid_7 == null)
				{
					mChild_Grid_7 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_7.Entity.EntityName = "Grid_7";
					mChild_Grid_7.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_7") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_7);
				}
				return mChild_Grid_7;
			}
		}

		private View_GridComponent mChild_Grid_8;

		public View_GridComponent Child_Grid_8
		{
			get
			{
				if (mChild_Grid_8 == null)
				{
					mChild_Grid_8 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_8.Entity.EntityName = "Grid_8";
					mChild_Grid_8.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_8") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_8);
				}
				return mChild_Grid_8;
			}
		}

		private View_GridComponent mChild_Grid_9;

		public View_GridComponent Child_Grid_9
		{
			get
			{
				if (mChild_Grid_9 == null)
				{
					mChild_Grid_9 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_9.Entity.EntityName = "Grid_9";
					mChild_Grid_9.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_9") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_9);
				}
				return mChild_Grid_9;
			}
		}

		private View_GridComponent mChild_Grid_10;

		public View_GridComponent Child_Grid_10
		{
			get
			{
				if (mChild_Grid_10 == null)
				{
					mChild_Grid_10 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_10.Entity.EntityName = "Grid_10";
					mChild_Grid_10.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_10") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_10);
				}
				return mChild_Grid_10;
			}
		}

		private View_GridComponent mChild_Grid_11;

		public View_GridComponent Child_Grid_11
		{
			get
			{
				if (mChild_Grid_11 == null)
				{
					mChild_Grid_11 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_11.Entity.EntityName = "Grid_11";
					mChild_Grid_11.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_11") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_11);
				}
				return mChild_Grid_11;
			}
		}

		private View_GridComponent mChild_Grid_12;

		public View_GridComponent Child_Grid_12
		{
			get
			{
				if (mChild_Grid_12 == null)
				{
					mChild_Grid_12 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_12.Entity.EntityName = "Grid_12";
					mChild_Grid_12.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_12") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_12);
				}
				return mChild_Grid_12;
			}
		}

		private View_GridComponent mChild_Grid_13;

		public View_GridComponent Child_Grid_13
		{
			get
			{
				if (mChild_Grid_13 == null)
				{
					mChild_Grid_13 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_13.Entity.EntityName = "Grid_13";
					mChild_Grid_13.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_13") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_13);
				}
				return mChild_Grid_13;
			}
		}

		private View_GridComponent mChild_Grid_14;

		public View_GridComponent Child_Grid_14
		{
			get
			{
				if (mChild_Grid_14 == null)
				{
					mChild_Grid_14 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_14.Entity.EntityName = "Grid_14";
					mChild_Grid_14.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_14") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_14);
				}
				return mChild_Grid_14;
			}
		}

		private View_GridComponent mChild_Grid_15;

		public View_GridComponent Child_Grid_15
		{
			get
			{
				if (mChild_Grid_15 == null)
				{
					mChild_Grid_15 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_15.Entity.EntityName = "Grid_15";
					mChild_Grid_15.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_15") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_15);
				}
				return mChild_Grid_15;
			}
		}

		private View_GridComponent mChild_Grid_16;

		public View_GridComponent Child_Grid_16
		{
			get
			{
				if (mChild_Grid_16 == null)
				{
					mChild_Grid_16 = Entity.AddChild<View_GridComponent>();
					mChild_Grid_16.Entity.EntityName = "Grid_16";
					mChild_Grid_16.rectTransform = rectTransform.Find("Item/C_棋盘/D_Grid_16") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid_16);
				}
				return mChild_Grid_16;
			}
		}

		private View_GridComponent mChild_Grid;

		public View_GridComponent Child_Grid
		{
			get
			{
				if (mChild_Grid == null)
				{
					mChild_Grid = Entity.AddChild<View_GridComponent>();
					mChild_Grid.Entity.EntityName = "Grid";
					mChild_Grid.rectTransform = rectTransform.Find("Item/D_Grid") as RectTransform;
					World.RunSystem<IUICreate>(mChild_Grid);
				}
				return mChild_Grid;
			}
		}

		private View_老虎机列Component mChild_老虎机列_1;

		public View_老虎机列Component Child_老虎机列_1
		{
			get
			{
				if (mChild_老虎机列_1 == null)
				{
					mChild_老虎机列_1 = Entity.AddChild<View_老虎机列Component>();
					mChild_老虎机列_1.Entity.EntityName = "老虎机列_1";
					mChild_老虎机列_1.rectTransform = rectTransform.Find("Item/C_BattlePanel/Random/白底/D_老虎机列_1") as RectTransform;
					World.RunSystem<IUICreate>(mChild_老虎机列_1);
				}
				return mChild_老虎机列_1;
			}
		}

		private View_老虎机列Component mChild_老虎机列_2;

		public View_老虎机列Component Child_老虎机列_2
		{
			get
			{
				if (mChild_老虎机列_2 == null)
				{
					mChild_老虎机列_2 = Entity.AddChild<View_老虎机列Component>();
					mChild_老虎机列_2.Entity.EntityName = "老虎机列_2";
					mChild_老虎机列_2.rectTransform = rectTransform.Find("Item/C_BattlePanel/Random/白底/D_老虎机列_2") as RectTransform;
					World.RunSystem<IUICreate>(mChild_老虎机列_2);
				}
				return mChild_老虎机列_2;
			}
		}

		private View_老虎机列Component mChild_老虎机列_3;

		public View_老虎机列Component Child_老虎机列_3
		{
			get
			{
				if (mChild_老虎机列_3 == null)
				{
					mChild_老虎机列_3 = Entity.AddChild<View_老虎机列Component>();
					mChild_老虎机列_3.Entity.EntityName = "老虎机列_3";
					mChild_老虎机列_3.rectTransform = rectTransform.Find("Item/C_BattlePanel/Random/白底/D_老虎机列_3") as RectTransform;
					World.RunSystem<IUICreate>(mChild_老虎机列_3);
				}
				return mChild_老虎机列_3;
			}
		}


		[FoldoutGroup("ALl")]

		public List<Button> all_Button = new();

		[FoldoutGroup("ALl")]

		public List<Image> all_Image = new();

		[FoldoutGroup("ALl")]

		public List<GridLayoutGroup> all_GridLayoutGroup = new();

		[FoldoutGroup("ALl")]

		public List<TextMeshProUGUI> all_TextMeshProUGUI = new();

		[FoldoutGroup("ALl")]

		public List<RectTransform> all_RectTransform = new();

		[FoldoutGroup("ALl")]

		public List<Text> all_Text = new();

		[FoldoutGroup("ALl")]

		public List<View_RoleInfoComponent> all_View_RoleInfoComponent = new();

		[FoldoutGroup("ALl")]

		public List<View_GridComponent> all_View_GridComponent = new();

		[FoldoutGroup("ALl")]

		public List<View_老虎机列Component> all_View_老虎机列Component = new();

		public void FindAll()
		{
				
			all_Button.Add(Button_Mask);
			all_Button.Add(Button_Close);
			all_Button.Add(Button_顺序按钮上);
			all_Button.Add(Button_顺序按钮下);
			all_Button.Add(Button_Switch);;
				
			all_Image.Add(Image_棋盘);
			all_Image.Add(Image_手牌);
			all_Image.Add(Image_BattlePanel);;
				
			all_GridLayoutGroup.Add(GridLayoutGroup_棋盘);;
				
			all_TextMeshProUGUI.Add(TextMeshProUGUI_Round);;
				
			all_RectTransform.Add(RectTransform_CardPanel);;
				
			all_Text.Add(Text_提示);;
				
			all_View_RoleInfoComponent.Add(Child_RoleInfo_Enemy);
			all_View_RoleInfoComponent.Add(Child_RoleInfo_Player);;
				
			all_View_GridComponent.Add(Child_Grid_1);
			all_View_GridComponent.Add(Child_Grid_2);
			all_View_GridComponent.Add(Child_Grid_3);
			all_View_GridComponent.Add(Child_Grid_4);
			all_View_GridComponent.Add(Child_Grid_5);
			all_View_GridComponent.Add(Child_Grid_6);
			all_View_GridComponent.Add(Child_Grid_7);
			all_View_GridComponent.Add(Child_Grid_8);
			all_View_GridComponent.Add(Child_Grid_9);
			all_View_GridComponent.Add(Child_Grid_10);
			all_View_GridComponent.Add(Child_Grid_11);
			all_View_GridComponent.Add(Child_Grid_12);
			all_View_GridComponent.Add(Child_Grid_13);
			all_View_GridComponent.Add(Child_Grid_14);
			all_View_GridComponent.Add(Child_Grid_15);
			all_View_GridComponent.Add(Child_Grid_16);
			all_View_GridComponent.Add(Child_Grid);;
				
			all_View_老虎机列Component.Add(Child_老虎机列_1);
			all_View_老虎机列Component.Add(Child_老虎机列_2);
			all_View_老虎机列Component.Add(Child_老虎机列_3);;


		}
	}}
