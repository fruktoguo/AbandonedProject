
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public static partial class ViewType 
	{ 
		public const string Setting = "Setting";
	}
	public partial class View_SettingComponent : UIComponent 
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

		private YuoDropDown mYuoDropDown_语言选择;

		public YuoDropDown YuoDropDown_语言选择
		{
			get
			{
				if (mYuoDropDown_语言选择 == null)
					mYuoDropDown_语言选择 = rectTransform.Find("Item/BackGround/C_语言选择").GetComponent<YuoDropDown>();
				return mYuoDropDown_语言选择;
			}
		}

		private Toggle mToggle_FullScreen;

		public Toggle Toggle_FullScreen
		{
			get
			{
				if (mToggle_FullScreen == null)
					mToggle_FullScreen = rectTransform.Find("Item/BackGround/C_FullScreen").GetComponent<Toggle>();
				return mToggle_FullScreen;
			}
		}

		private Slider mSlider_Music;

		public Slider Slider_Music
		{
			get
			{
				if (mSlider_Music == null)
					mSlider_Music = rectTransform.Find("Item/BackGround/C_Music").GetComponent<Slider>();
				return mSlider_Music;
			}
		}

		private Slider mSlider_Sound;

		public Slider Slider_Sound
		{
			get
			{
				if (mSlider_Sound == null)
					mSlider_Sound = rectTransform.Find("Item/BackGround/C_Sound").GetComponent<Slider>();
				return mSlider_Sound;
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

		private InputField mInputField_CreateLanguage;

		public InputField InputField_CreateLanguage
		{
			get
			{
				if (mInputField_CreateLanguage == null)
					mInputField_CreateLanguage = rectTransform.Find("Item/BackGround/C_CreateLanguage").GetComponent<InputField>();
				return mInputField_CreateLanguage;
			}
		}

		private Button mButton_返回主菜单;

		public Button Button_返回主菜单
		{
			get
			{
				if (mButton_返回主菜单 == null)
					mButton_返回主菜单 = rectTransform.Find("Item/BackGround/C_返回主菜单").GetComponent<Button>();
				return mButton_返回主菜单;
			}
		}

		public List<Button> all_Button = new();

		public List<YuoDropDown> all_YuoDropDown = new();

		public List<Toggle> all_Toggle = new();

		public List<Slider> all_Slider = new();

		public List<InputField> all_InputField = new();

		public void FindAll()
		{
				
			all_Button.Add(Button_Mask);
			all_Button.Add(Button_Close);
			all_Button.Add(Button_返回主菜单);;
				
			all_YuoDropDown.Add(YuoDropDown_语言选择);;
				
			all_Toggle.Add(Toggle_FullScreen);;
				
			all_Slider.Add(Slider_Music);
			all_Slider.Add(Slider_Sound);;
				
			all_InputField.Add(InputField_CreateLanguage);;

		}
	}}
