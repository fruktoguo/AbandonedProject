using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using YuoTools.Main.Ecs;
using Sirenix.OdinInspector;

namespace YuoTools.UI
{
	public partial class View_GridComponent : UIComponent 
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

		private Image mImage_Color;

		public Image Image_Color
		{
			get
			{
				if (mImage_Color == null)
					mImage_Color = rectTransform.Find("C_Color").GetComponent<Image>();
				return mImage_Color;
			}
		}


		private Image mImage_Card;

		public Image Image_Card
		{
			get
			{
				if (mImage_Card == null)
					mImage_Card = rectTransform.Find("C_Card").GetComponent<Image>();
				return mImage_Card;
			}
		}



		[FoldoutGroup("ALl")]

		public List<Image> all_Image = new();

		public void FindAll()
		{
				
			all_Image.Add(MainImage);
			all_Image.Add(Image_Color);
			all_Image.Add(Image_Card);;


		}
	}}
