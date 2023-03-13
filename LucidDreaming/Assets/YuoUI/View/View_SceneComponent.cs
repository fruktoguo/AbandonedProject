
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public static partial class ViewType 
	{ 
		public const string Scene = "Scene";
	}
	public partial class View_SceneComponent : UIComponent 
	{

		private Image mImage_选项;

		public Image Image_选项
		{
			get
			{
				if (mImage_选项 == null)
					mImage_选项 = rectTransform.Find("Item/BackGround/C_选项").GetComponent<Image>();
				return mImage_选项;
			}
		}

		private ButtonSwitch mButtonSwitch_PanelSwitch;

		public ButtonSwitch ButtonSwitch_PanelSwitch
		{
			get
			{
				if (mButtonSwitch_PanelSwitch == null)
					mButtonSwitch_PanelSwitch = rectTransform.Find("Item/BackGround/C_选项/C_PanelSwitch").GetComponent<ButtonSwitch>();
				return mButtonSwitch_PanelSwitch;
			}
		}

		private Button mButton_工作;

		public Button Button_工作
		{
			get
			{
				if (mButton_工作 == null)
					mButton_工作 = rectTransform.Find("Item/BackGround/C_选项/C_工作").GetComponent<Button>();
				return mButton_工作;
			}
		}

		private Button mButton_商城;

		public Button Button_商城
		{
			get
			{
				if (mButton_商城 == null)
					mButton_商城 = rectTransform.Find("Item/BackGround/C_选项/C_商城").GetComponent<Button>();
				return mButton_商城;
			}
		}

		private Button mButton_调查;

		public Button Button_调查
		{
			get
			{
				if (mButton_调查 == null)
					mButton_调查 = rectTransform.Find("Item/BackGround/C_选项/C_调查").GetComponent<Button>();
				return mButton_调查;
			}
		}

		private Button mButton_状态;

		public Button Button_状态
		{
			get
			{
				if (mButton_状态 == null)
					mButton_状态 = rectTransform.Find("Item/BackGround/C_选项/C_状态").GetComponent<Button>();
				return mButton_状态;
			}
		}

		private Button mButton_设置;

		public Button Button_设置
		{
			get
			{
				if (mButton_设置 == null)
					mButton_设置 = rectTransform.Find("Item/BackGround/C_选项/C_设置").GetComponent<Button>();
				return mButton_设置;
			}
		}

		public List<Image> all_Image = new();

		public List<ButtonSwitch> all_ButtonSwitch = new();

		public List<Button> all_Button = new();

		public void FindAll()
		{
				
			all_Image.Add(Image_选项);;
				
			all_ButtonSwitch.Add(ButtonSwitch_PanelSwitch);;
				
			all_Button.Add(Button_工作);
			all_Button.Add(Button_商城);
			all_Button.Add(Button_调查);
			all_Button.Add(Button_状态);
			all_Button.Add(Button_设置);;

		}
	}}
