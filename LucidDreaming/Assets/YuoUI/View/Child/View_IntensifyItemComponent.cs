
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public partial class View_IntensifyItemComponent : UIComponent 
	{

		private Image mImage_Icon;

		public Image Image_Icon
		{
			get
			{
				if (mImage_Icon == null)
					mImage_Icon = rectTransform.Find("C_Icon").GetComponent<Image>();
				return mImage_Icon;
			}
		}

		private TextMeshProUGUI mTextMeshProUGUI_Num;

		public TextMeshProUGUI TextMeshProUGUI_Num
		{
			get
			{
				if (mTextMeshProUGUI_Num == null)
					mTextMeshProUGUI_Num = rectTransform.Find("C_Num").GetComponent<TextMeshProUGUI>();
				return mTextMeshProUGUI_Num;
			}
		}
	}}
