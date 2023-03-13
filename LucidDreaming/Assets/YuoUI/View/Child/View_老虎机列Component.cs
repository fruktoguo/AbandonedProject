using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using YuoTools.Main.Ecs;
using Sirenix.OdinInspector;

namespace YuoTools.UI
{
	public partial class View_老虎机列Component : UIComponent 
	{


		private RectTransform mainRectTransform;

		public RectTransform MainRectTransform
		{
			get
			{
				if (mainRectTransform == null)
					mainRectTransform = rectTransform.GetComponent<RectTransform>();
				return mainRectTransform;
			}
		}

		private Image mImage_Item__1;

		public Image Image_Item__1
		{
			get
			{
				if (mImage_Item__1 == null)
					mImage_Item__1 = rectTransform.Find("C_Item__1").GetComponent<Image>();
				return mImage_Item__1;
			}
		}


		private Image mImage_Item__2;

		public Image Image_Item__2
		{
			get
			{
				if (mImage_Item__2 == null)
					mImage_Item__2 = rectTransform.Find("C_Item__2").GetComponent<Image>();
				return mImage_Item__2;
			}
		}


		private Image mImage_Item__3;

		public Image Image_Item__3
		{
			get
			{
				if (mImage_Item__3 == null)
					mImage_Item__3 = rectTransform.Find("C_Item__3").GetComponent<Image>();
				return mImage_Item__3;
			}
		}


		private Image mImage_Item__4;

		public Image Image_Item__4
		{
			get
			{
				if (mImage_Item__4 == null)
					mImage_Item__4 = rectTransform.Find("C_Item__4").GetComponent<Image>();
				return mImage_Item__4;
			}
		}


		private Image mImage_Item__5;

		public Image Image_Item__5
		{
			get
			{
				if (mImage_Item__5 == null)
					mImage_Item__5 = rectTransform.Find("C_Item__5").GetComponent<Image>();
				return mImage_Item__5;
			}
		}



		[FoldoutGroup("ALl")]

		public List<RectTransform> all_RectTransform = new();

		[FoldoutGroup("ALl")]

		public List<Image> all_Image = new();

		public void FindAll()
		{
				
			all_RectTransform.Add(MainRectTransform);;
				
			all_Image.Add(Image_Item__1);
			all_Image.Add(Image_Item__2);
			all_Image.Add(Image_Item__3);
			all_Image.Add(Image_Item__4);
			all_Image.Add(Image_Item__5);;


		}
	}}
