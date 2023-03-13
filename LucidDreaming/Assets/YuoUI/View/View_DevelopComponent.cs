
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public static partial class ViewType 
	{ 
		public const string Develop = "Develop";
	}
	public partial class View_DevelopComponent : UIComponent 
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

		private Text mText_属性点数_Atk;

		public Text Text_属性点数_Atk
		{
			get
			{
				if (mText_属性点数_Atk == null)
					mText_属性点数_Atk = rectTransform.Find("Item/BackGround/加点框/加点条_Atk/C_属性点数_Atk").GetComponent<Text>();
				return mText_属性点数_Atk;
			}
		}

		private Text mText_数值点数_Atk;

		public Text Text_数值点数_Atk
		{
			get
			{
				if (mText_数值点数_Atk == null)
					mText_数值点数_Atk = rectTransform.Find("Item/BackGround/加点框/加点条_Atk/C_数值点数_Atk").GetComponent<Text>();
				return mText_数值点数_Atk;
			}
		}

		private Button mButton_加点按钮_Atk;

		public Button Button_加点按钮_Atk
		{
			get
			{
				if (mButton_加点按钮_Atk == null)
					mButton_加点按钮_Atk = rectTransform.Find("Item/BackGround/加点框/加点条_Atk/C_加点按钮_Atk").GetComponent<Button>();
				return mButton_加点按钮_Atk;
			}
		}

		private Button mButton_减点按钮_Atk;

		public Button Button_减点按钮_Atk
		{
			get
			{
				if (mButton_减点按钮_Atk == null)
					mButton_减点按钮_Atk = rectTransform.Find("Item/BackGround/加点框/加点条_Atk/C_减点按钮_Atk").GetComponent<Button>();
				return mButton_减点按钮_Atk;
			}
		}

		private Text mText_属性点数_Def;

		public Text Text_属性点数_Def
		{
			get
			{
				if (mText_属性点数_Def == null)
					mText_属性点数_Def = rectTransform.Find("Item/BackGround/加点框/加点条_Def/C_属性点数_Def").GetComponent<Text>();
				return mText_属性点数_Def;
			}
		}

		private Text mText_数值点数_Def;

		public Text Text_数值点数_Def
		{
			get
			{
				if (mText_数值点数_Def == null)
					mText_数值点数_Def = rectTransform.Find("Item/BackGround/加点框/加点条_Def/C_数值点数_Def").GetComponent<Text>();
				return mText_数值点数_Def;
			}
		}

		private Button mButton_加点按钮_Def;

		public Button Button_加点按钮_Def
		{
			get
			{
				if (mButton_加点按钮_Def == null)
					mButton_加点按钮_Def = rectTransform.Find("Item/BackGround/加点框/加点条_Def/C_加点按钮_Def").GetComponent<Button>();
				return mButton_加点按钮_Def;
			}
		}

		private Button mButton_减点按钮_Def;

		public Button Button_减点按钮_Def
		{
			get
			{
				if (mButton_减点按钮_Def == null)
					mButton_减点按钮_Def = rectTransform.Find("Item/BackGround/加点框/加点条_Def/C_减点按钮_Def").GetComponent<Button>();
				return mButton_减点按钮_Def;
			}
		}

		private Text mText_属性点数_Hp;

		public Text Text_属性点数_Hp
		{
			get
			{
				if (mText_属性点数_Hp == null)
					mText_属性点数_Hp = rectTransform.Find("Item/BackGround/加点框/加点条_Hp/C_属性点数_Hp").GetComponent<Text>();
				return mText_属性点数_Hp;
			}
		}

		private Text mText_数值点数_Hp;

		public Text Text_数值点数_Hp
		{
			get
			{
				if (mText_数值点数_Hp == null)
					mText_数值点数_Hp = rectTransform.Find("Item/BackGround/加点框/加点条_Hp/C_数值点数_Hp").GetComponent<Text>();
				return mText_数值点数_Hp;
			}
		}

		private Button mButton_加点按钮_Hp;

		public Button Button_加点按钮_Hp
		{
			get
			{
				if (mButton_加点按钮_Hp == null)
					mButton_加点按钮_Hp = rectTransform.Find("Item/BackGround/加点框/加点条_Hp/C_加点按钮_Hp").GetComponent<Button>();
				return mButton_加点按钮_Hp;
			}
		}

		private Button mButton_减点按钮_Hp;

		public Button Button_减点按钮_Hp
		{
			get
			{
				if (mButton_减点按钮_Hp == null)
					mButton_减点按钮_Hp = rectTransform.Find("Item/BackGround/加点框/加点条_Hp/C_减点按钮_Hp").GetComponent<Button>();
				return mButton_减点按钮_Hp;
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

		public List<Button> all_Button = new();

		public List<Text> all_Text = new();

		public void FindAll()
		{
				
			all_Button.Add(Button_Mask);
			all_Button.Add(Button_加点按钮_Atk);
			all_Button.Add(Button_减点按钮_Atk);
			all_Button.Add(Button_加点按钮_Def);
			all_Button.Add(Button_减点按钮_Def);
			all_Button.Add(Button_加点按钮_Hp);
			all_Button.Add(Button_减点按钮_Hp);
			all_Button.Add(Button_Close);;
				
			all_Text.Add(Text_属性点数_Atk);
			all_Text.Add(Text_数值点数_Atk);
			all_Text.Add(Text_属性点数_Def);
			all_Text.Add(Text_数值点数_Def);
			all_Text.Add(Text_属性点数_Hp);
			all_Text.Add(Text_数值点数_Hp);;

		}
	}}
