
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public static partial class ViewType 
	{ 
		public const string Shop = "Shop";
	}
	public partial class View_ShopComponent : UIComponent 
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
					mButton_Close = rectTransform.Find("Item/BackGround/C_Close").GetComponent<Button>();
				return mButton_Close;
			}
		}

		private Toggle mToggle_食品;

		public Toggle Toggle_食品
		{
			get
			{
				if (mToggle_食品 == null)
					mToggle_食品 = rectTransform.Find("Item/BackGround/类别/C_食品").GetComponent<Toggle>();
				return mToggle_食品;
			}
		}

		private Toggle mToggle_工具;

		public Toggle Toggle_工具
		{
			get
			{
				if (mToggle_工具 == null)
					mToggle_工具 = rectTransform.Find("Item/BackGround/类别/C_工具").GetComponent<Toggle>();
				return mToggle_工具;
			}
		}

		private Toggle mToggle_礼物;

		public Toggle Toggle_礼物
		{
			get
			{
				if (mToggle_礼物 == null)
					mToggle_礼物 = rectTransform.Find("Item/BackGround/类别/C_礼物").GetComponent<Toggle>();
				return mToggle_礼物;
			}
		}

		private Toggle mToggle_其他;

		public Toggle Toggle_其他
		{
			get
			{
				if (mToggle_其他 == null)
					mToggle_其他 = rectTransform.Find("Item/BackGround/类别/C_其他").GetComponent<Toggle>();
				return mToggle_其他;
			}
		}

		private Toggle mToggle_价格从高到低;

		public Toggle Toggle_价格从高到低
		{
			get
			{
				if (mToggle_价格从高到低 == null)
					mToggle_价格从高到低 = rectTransform.Find("Item/BackGround/排序/C_价格从高到低").GetComponent<Toggle>();
				return mToggle_价格从高到低;
			}
		}

		private Toggle mToggle_价格从低到高;

		public Toggle Toggle_价格从低到高
		{
			get
			{
				if (mToggle_价格从低到高 == null)
					mToggle_价格从低到高 = rectTransform.Find("Item/BackGround/排序/C_价格从低到高").GetComponent<Toggle>();
				return mToggle_价格从低到高;
			}
		}

		private ToggleGroup mToggleGroup_PagePanel;

		public ToggleGroup ToggleGroup_PagePanel
		{
			get
			{
				if (mToggleGroup_PagePanel == null)
					mToggleGroup_PagePanel = rectTransform.Find("Item/BackGround/列表/C_PagePanel").GetComponent<ToggleGroup>();
				return mToggleGroup_PagePanel;
			}
		}

		private RawImage mRawImage_图标;

		public RawImage RawImage_图标
		{
			get
			{
				if (mRawImage_图标 == null)
					mRawImage_图标 = rectTransform.Find("Item/BackGround/说明/C_图标").GetComponent<RawImage>();
				return mRawImage_图标;
			}
		}

		private Text mText_名称;

		public Text Text_名称
		{
			get
			{
				if (mText_名称 == null)
					mText_名称 = rectTransform.Find("Item/BackGround/说明/C_名称").GetComponent<Text>();
				return mText_名称;
			}
		}

		private Text mText_效果;

		public Text Text_效果
		{
			get
			{
				if (mText_效果 == null)
					mText_效果 = rectTransform.Find("Item/BackGround/说明/C_效果").GetComponent<Text>();
				return mText_效果;
			}
		}

		private Text mText_介绍;

		public Text Text_介绍
		{
			get
			{
				if (mText_介绍 == null)
					mText_介绍 = rectTransform.Find("Item/BackGround/说明/C_介绍").GetComponent<Text>();
				return mText_介绍;
			}
		}

		private Button mButton_Buy;

		public Button Button_Buy
		{
			get
			{
				if (mButton_Buy == null)
					mButton_Buy = rectTransform.Find("Item/BackGround/说明/C_Buy").GetComponent<Button>();
				return mButton_Buy;
			}
		}

		private View_GoodsComponent mChild_Goods;

		public View_GoodsComponent Child_Goods
		{
			get
			{
				if (mChild_Goods == null)
				{
					mChild_Goods = Entity.AddChild<View_GoodsComponent>();
					mChild_Goods.rectTransform = rectTransform.Find("Item/BackGround/列表/Panel/D_Goods") as RectTransform;
				}
				return mChild_Goods;
			}
		}

		private View_PageItemComponent mChild_PageItem;

		public View_PageItemComponent Child_PageItem
		{
			get
			{
				if (mChild_PageItem == null)
				{
					mChild_PageItem = Entity.AddChild<View_PageItemComponent>();
					mChild_PageItem.rectTransform = rectTransform.Find("Item/BackGround/列表/C_PagePanel/Scroll View/Viewport/Content/D_PageItem") as RectTransform;
				}
				return mChild_PageItem;
			}
		}

		public List<Button> all_Button = new();

		public List<Toggle> all_Toggle = new();

		public List<ToggleGroup> all_ToggleGroup = new();

		public List<RawImage> all_RawImage = new();

		public List<Text> all_Text = new();

		public List<View_GoodsComponent> all_View_GoodsComponent = new();

		public List<View_PageItemComponent> all_View_PageItemComponent = new();

		public void FindAll()
		{
				
			all_Button.Add(Button_Mask);
			all_Button.Add(Button_Close);
			all_Button.Add(Button_Buy);;
				
			all_Toggle.Add(Toggle_食品);
			all_Toggle.Add(Toggle_工具);
			all_Toggle.Add(Toggle_礼物);
			all_Toggle.Add(Toggle_其他);
			all_Toggle.Add(Toggle_价格从高到低);
			all_Toggle.Add(Toggle_价格从低到高);;
				
			all_ToggleGroup.Add(ToggleGroup_PagePanel);;
				
			all_RawImage.Add(RawImage_图标);;
				
			all_Text.Add(Text_名称);
			all_Text.Add(Text_效果);
			all_Text.Add(Text_介绍);;
				
			all_View_GoodsComponent.Add(Child_Goods);;
				
			all_View_PageItemComponent.Add(Child_PageItem);;

		}
	}}
