
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public partial class View_GoodsComponent : UIComponent 
	{

		private Button mButton_Icon;

		public Button Button_Icon
		{
			get
			{
				if (mButton_Icon == null)
					mButton_Icon = rectTransform.Find("C_Icon").GetComponent<Button>();
				return mButton_Icon;
			}
		}

		private RawImage mRawImage_Icon;

		public RawImage RawImage_Icon
		{
			get
			{
				if (mRawImage_Icon == null)
					mRawImage_Icon = rectTransform.Find("C_Icon").GetComponent<RawImage>();
				return mRawImage_Icon;
			}
		}

		private TextMeshProUGUI mTextMeshProUGUI_价格;

		public TextMeshProUGUI TextMeshProUGUI_价格
		{
			get
			{
				if (mTextMeshProUGUI_价格 == null)
					mTextMeshProUGUI_价格 = rectTransform.Find("价格/C_价格").GetComponent<TextMeshProUGUI>();
				return mTextMeshProUGUI_价格;
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
