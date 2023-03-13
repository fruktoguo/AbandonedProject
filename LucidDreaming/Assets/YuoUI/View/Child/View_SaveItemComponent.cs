
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public partial class View_SaveItemComponent : UIComponent 
	{

		private Button mButton_Click;

		public Button Button_Click
		{
			get
			{
				if (mButton_Click == null)
					mButton_Click = rectTransform.Find("C_Click").GetComponent<Button>();
				return mButton_Click;
			}
		}

		private Button mButton_Delete;

		public Button Button_Delete
		{
			get
			{
				if (mButton_Delete == null)
					mButton_Delete = rectTransform.Find("C_Click/Delete/C_Delete").GetComponent<Button>();
				return mButton_Delete;
			}
		}

		private RawImage mRawImage_SaveIcon;

		public RawImage RawImage_SaveIcon
		{
			get
			{
				if (mRawImage_SaveIcon == null)
					mRawImage_SaveIcon = rectTransform.Find("Mask/C_SaveIcon").GetComponent<RawImage>();
				return mRawImage_SaveIcon;
			}
		}

		private Text mText_Index;

		public Text Text_Index
		{
			get
			{
				if (mText_Index == null)
					mText_Index = rectTransform.Find("Info/C_Index").GetComponent<Text>();
				return mText_Index;
			}
		}

		private Text mText_Day;

		public Text Text_Day
		{
			get
			{
				if (mText_Day == null)
					mText_Day = rectTransform.Find("Info/C_Day").GetComponent<Text>();
				return mText_Day;
			}
		}

		private Text mText_Date;

		public Text Text_Date
		{
			get
			{
				if (mText_Date == null)
					mText_Date = rectTransform.Find("Info/C_Date").GetComponent<Text>();
				return mText_Date;
			}
		}

		private Text mText_Date_Time;

		public Text Text_Date_Time
		{
			get
			{
				if (mText_Date_Time == null)
					mText_Date_Time = rectTransform.Find("Info/C_Date_Time").GetComponent<Text>();
				return mText_Date_Time;
			}
		}
	}}
