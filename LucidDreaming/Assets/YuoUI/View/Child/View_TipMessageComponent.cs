
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public partial class View_TipMessageComponent : UIComponent 
	{

		private Image mImage_TipMessage;

		public Image Image_TipMessage
		{
			get
			{
				if (mImage_TipMessage == null)
					mImage_TipMessage = rectTransform.GetComponent<Image>();
				return mImage_TipMessage;
			}
		}

		private Text mText_Message;

		public Text Text_Message
		{
			get
			{
				if (mText_Message == null)
					mText_Message = rectTransform.Find("C_Message").GetComponent<Text>();
				return mText_Message;
			}
		}
	}}
