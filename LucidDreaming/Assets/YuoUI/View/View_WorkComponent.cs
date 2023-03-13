
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public static partial class ViewType 
	{ 
		public const string Work = "Work";
	}
	public partial class View_WorkComponent : UIComponent 
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

		private Button mButton_外卖;

		public Button Button_外卖
		{
			get
			{
				if (mButton_外卖 == null)
					mButton_外卖 = rectTransform.Find("Item/BackGround/工作/C_外卖").GetComponent<Button>();
				return mButton_外卖;
			}
		}

		private Button mButton_服务员;

		public Button Button_服务员
		{
			get
			{
				if (mButton_服务员 == null)
					mButton_服务员 = rectTransform.Find("Item/BackGround/工作/C_服务员").GetComponent<Button>();
				return mButton_服务员;
			}
		}

		private Button mButton_翻译;

		public Button Button_翻译
		{
			get
			{
				if (mButton_翻译 == null)
					mButton_翻译 = rectTransform.Find("Item/BackGround/工作/C_翻译").GetComponent<Button>();
				return mButton_翻译;
			}
		}

		private Image mImage_选择;

		public Image Image_选择
		{
			get
			{
				if (mImage_选择 == null)
					mImage_选择 = rectTransform.Find("Item/BackGround/工作/C_选择").GetComponent<Image>();
				return mImage_选择;
			}
		}

		private Image mImage_确定;

		public Image Image_确定
		{
			get
			{
				if (mImage_确定 == null)
					mImage_确定 = rectTransform.Find("Item/BackGround/工作/C_确定").GetComponent<Image>();
				return mImage_确定;
			}
		}

		private Text mText_介绍;

		public Text Text_介绍
		{
			get
			{
				if (mText_介绍 == null)
					mText_介绍 = rectTransform.Find("Item/BackGround/工作/介绍/C_介绍").GetComponent<Text>();
				return mText_介绍;
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

		private Image mImage_HealthValue;

		public Image Image_HealthValue
		{
			get
			{
				if (mImage_HealthValue == null)
					mImage_HealthValue = rectTransform.Find("Item/BackGround/状态框/Health/C_HealthValue").GetComponent<Image>();
				return mImage_HealthValue;
			}
		}

		private Image mImage_DefValue;

		public Image Image_DefValue
		{
			get
			{
				if (mImage_DefValue == null)
					mImage_DefValue = rectTransform.Find("Item/BackGround/状态框/Def/C_DefValue").GetComponent<Image>();
				return mImage_DefValue;
			}
		}

		private Image mImage_AgiValue;

		public Image Image_AgiValue
		{
			get
			{
				if (mImage_AgiValue == null)
					mImage_AgiValue = rectTransform.Find("Item/BackGround/状态框/Agi/C_AgiValue").GetComponent<Image>();
				return mImage_AgiValue;
			}
		}

		private TextMeshProUGUI mTextMeshProUGUI_Health;

		public TextMeshProUGUI TextMeshProUGUI_Health
		{
			get
			{
				if (mTextMeshProUGUI_Health == null)
					mTextMeshProUGUI_Health = rectTransform.Find("Item/BackGround/状态框/C_Health").GetComponent<TextMeshProUGUI>();
				return mTextMeshProUGUI_Health;
			}
		}

		private TextMeshProUGUI mTextMeshProUGUI_Def;

		public TextMeshProUGUI TextMeshProUGUI_Def
		{
			get
			{
				if (mTextMeshProUGUI_Def == null)
					mTextMeshProUGUI_Def = rectTransform.Find("Item/BackGround/状态框/C_Def").GetComponent<TextMeshProUGUI>();
				return mTextMeshProUGUI_Def;
			}
		}

		private TextMeshProUGUI mTextMeshProUGUI_Agi;

		public TextMeshProUGUI TextMeshProUGUI_Agi
		{
			get
			{
				if (mTextMeshProUGUI_Agi == null)
					mTextMeshProUGUI_Agi = rectTransform.Find("Item/BackGround/状态框/C_Agi").GetComponent<TextMeshProUGUI>();
				return mTextMeshProUGUI_Agi;
			}
		}

		private Text mText_状态介绍;

		public Text Text_状态介绍
		{
			get
			{
				if (mText_状态介绍 == null)
					mText_状态介绍 = rectTransform.Find("Item/BackGround/状态框/C_状态介绍").GetComponent<Text>();
				return mText_状态介绍;
			}
		}

		public List<Button> all_Button = new();

		public List<Image> all_Image = new();

		public List<Text> all_Text = new();

		public List<TextMeshProUGUI> all_TextMeshProUGUI = new();

		public void FindAll()
		{
				
			all_Button.Add(Button_Mask);
			all_Button.Add(Button_外卖);
			all_Button.Add(Button_服务员);
			all_Button.Add(Button_翻译);
			all_Button.Add(Button_Close);;
				
			all_Image.Add(Image_选择);
			all_Image.Add(Image_确定);
			all_Image.Add(Image_HealthValue);
			all_Image.Add(Image_DefValue);
			all_Image.Add(Image_AgiValue);;
				
			all_Text.Add(Text_介绍);
			all_Text.Add(Text_状态介绍);;
				
			all_TextMeshProUGUI.Add(TextMeshProUGUI_Health);
			all_TextMeshProUGUI.Add(TextMeshProUGUI_Def);
			all_TextMeshProUGUI.Add(TextMeshProUGUI_Agi);;

		}
	}}
