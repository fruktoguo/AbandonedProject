using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using YuoTools.Main.Ecs;
using Sirenix.OdinInspector;

namespace YuoTools.UI
{
	public partial class View_RoleInfoComponent : UIComponent 
	{


		private Image mainImage;

		public Image MainImage
		{
			get
			{
				if (mainImage == null)
					mainImage = rectTransform.GetComponent<Image>();
				return mainImage;
			}
		}

		private Text mText_Name;

		public Text Text_Name
		{
			get
			{
				if (mText_Name == null)
					mText_Name = rectTransform.Find("C_Name").GetComponent<Text>();
				return mText_Name;
			}
		}


		private Image mImage_血条;

		public Image Image_血条
		{
			get
			{
				if (mImage_血条 == null)
					mImage_血条 = rectTransform.Find("血条/C_血条").GetComponent<Image>();
				return mImage_血条;
			}
		}


		private Text mText_ATK_Value;

		public Text Text_ATK_Value
		{
			get
			{
				if (mText_ATK_Value == null)
					mText_ATK_Value = rectTransform.Find("C_ATK_Value").GetComponent<Text>();
				return mText_ATK_Value;
			}
		}


		private Text mText_DEF_Value;

		public Text Text_DEF_Value
		{
			get
			{
				if (mText_DEF_Value == null)
					mText_DEF_Value = rectTransform.Find("C_DEF_Value").GetComponent<Text>();
				return mText_DEF_Value;
			}
		}


		private Text mText_DEX_Value;

		public Text Text_DEX_Value
		{
			get
			{
				if (mText_DEX_Value == null)
					mText_DEX_Value = rectTransform.Find("C_DEX_Value").GetComponent<Text>();
				return mText_DEX_Value;
			}
		}



		[FoldoutGroup("ALl")]

		public List<Image> all_Image = new();

		[FoldoutGroup("ALl")]

		public List<Text> all_Text = new();

		public void FindAll()
		{
				
			all_Image.Add(MainImage);
			all_Image.Add(Image_血条);;
				
			all_Text.Add(Text_Name);
			all_Text.Add(Text_ATK_Value);
			all_Text.Add(Text_DEF_Value);
			all_Text.Add(Text_DEX_Value);;


		}
	}}
