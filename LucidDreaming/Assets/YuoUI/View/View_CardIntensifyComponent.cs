
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public static partial class ViewType 
	{ 
		public const string CardIntensify = "CardIntensify";
	}
	public partial class View_CardIntensifyComponent : UIComponent 
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

		private Image mImage_卡牌;

		public Image Image_卡牌
		{
			get
			{
				if (mImage_卡牌 == null)
					mImage_卡牌 = rectTransform.Find("Item/BackGround/C_卡牌").GetComponent<Image>();
				return mImage_卡牌;
			}
		}

		private Button mButton_合成公式按钮;

		public Button Button_合成公式按钮
		{
			get
			{
				if (mButton_合成公式按钮 == null)
					mButton_合成公式按钮 = rectTransform.Find("Item/BackGround/C_合成公式按钮").GetComponent<Button>();
				return mButton_合成公式按钮;
			}
		}

		private Image mImage_说明框;

		public Image Image_说明框
		{
			get
			{
				if (mImage_说明框 == null)
					mImage_说明框 = rectTransform.Find("Item/BackGround/C_说明框").GetComponent<Image>();
				return mImage_说明框;
			}
		}

		private Button mButton_上一页;

		public Button Button_上一页
		{
			get
			{
				if (mButton_上一页 == null)
					mButton_上一页 = rectTransform.Find("Item/BackGround/C_上一页").GetComponent<Button>();
				return mButton_上一页;
			}
		}

		private Button mButton_下一页;

		public Button Button_下一页
		{
			get
			{
				if (mButton_下一页 == null)
					mButton_下一页 = rectTransform.Find("Item/BackGround/C_下一页").GetComponent<Button>();
				return mButton_下一页;
			}
		}

		private Image mImage_强化格子_1;

		public Image Image_强化格子_1
		{
			get
			{
				if (mImage_强化格子_1 == null)
					mImage_强化格子_1 = rectTransform.Find("Item/BackGround/C_强化格子_1").GetComponent<Image>();
				return mImage_强化格子_1;
			}
		}

		private Image mImage_强化格子_2;

		public Image Image_强化格子_2
		{
			get
			{
				if (mImage_强化格子_2 == null)
					mImage_强化格子_2 = rectTransform.Find("Item/BackGround/C_强化格子_2").GetComponent<Image>();
				return mImage_强化格子_2;
			}
		}

		private Image mImage_强化格子_3;

		public Image Image_强化格子_3
		{
			get
			{
				if (mImage_强化格子_3 == null)
					mImage_强化格子_3 = rectTransform.Find("Item/BackGround/C_强化格子_3").GetComponent<Image>();
				return mImage_强化格子_3;
			}
		}

		private Image mImage_强化格子_4;

		public Image Image_强化格子_4
		{
			get
			{
				if (mImage_强化格子_4 == null)
					mImage_强化格子_4 = rectTransform.Find("Item/BackGround/C_强化格子_4").GetComponent<Image>();
				return mImage_强化格子_4;
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

		private View_IntensifyItemComponent mChild_IntensifyItem;

		public View_IntensifyItemComponent Child_IntensifyItem
		{
			get
			{
				if (mChild_IntensifyItem == null)
				{
					mChild_IntensifyItem = Entity.AddChild<View_IntensifyItemComponent>();
					mChild_IntensifyItem.rectTransform = rectTransform.Find("Item/BackGround/道具栏/D_IntensifyItem") as RectTransform;
				}
				return mChild_IntensifyItem;
			}
		}

		public List<Button> all_Button = new();

		public List<Image> all_Image = new();

		public List<View_IntensifyItemComponent> all_View_IntensifyItemComponent = new();

		public void FindAll()
		{
				
			all_Button.Add(Button_Mask);
			all_Button.Add(Button_合成公式按钮);
			all_Button.Add(Button_上一页);
			all_Button.Add(Button_下一页);
			all_Button.Add(Button_Close);;
				
			all_Image.Add(Image_卡牌);
			all_Image.Add(Image_说明框);
			all_Image.Add(Image_强化格子_1);
			all_Image.Add(Image_强化格子_2);
			all_Image.Add(Image_强化格子_3);
			all_Image.Add(Image_强化格子_4);;
				
			all_View_IntensifyItemComponent.Add(Child_IntensifyItem);;

		}
	}}
