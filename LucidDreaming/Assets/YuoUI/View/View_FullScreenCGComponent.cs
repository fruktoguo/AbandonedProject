
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public static partial class ViewType 
	{ 
		public const string FullScreenCG = "FullScreenCG";
	}
	public partial class View_FullScreenCGComponent : UIComponent 
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

		private Image mImage_CG;

		public Image Image_CG
		{
			get
			{
				if (mImage_CG == null)
					mImage_CG = rectTransform.Find("Item/BackGround/C_CG").GetComponent<Image>();
				return mImage_CG;
			}
		}

		public List<Button> all_Button = new();

		public List<Image> all_Image = new();

		public void FindAll()
		{
				
			all_Button.Add(Button_Mask);;
				
			all_Image.Add(Image_CG);;

		}
	}}
