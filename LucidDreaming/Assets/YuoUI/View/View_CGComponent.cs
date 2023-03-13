
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public static partial class ViewType 
	{ 
		public const string CG = "CG";
	}
	public partial class View_CGComponent : UIComponent 
	{

		private ScrollRect mScrollRect_ScrollView;

		public ScrollRect ScrollRect_ScrollView
		{
			get
			{
				if (mScrollRect_ScrollView == null)
					mScrollRect_ScrollView = rectTransform.Find("Item/BackGround/C_ScrollView").GetComponent<ScrollRect>();
				return mScrollRect_ScrollView;
			}
		}

		private RectTransform mRectTransform_Content;

		public RectTransform RectTransform_Content
		{
			get
			{
				if (mRectTransform_Content == null)
					mRectTransform_Content = rectTransform.Find("Item/BackGround/C_ScrollView/Viewport/C_Content").GetComponent<RectTransform>();
				return mRectTransform_Content;
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

		public List<ScrollRect> all_ScrollRect = new();

		public List<RectTransform> all_RectTransform = new();

		public List<Button> all_Button = new();

		public void FindAll()
		{
				
			all_ScrollRect.Add(ScrollRect_ScrollView);;
				
			all_RectTransform.Add(RectTransform_Content);;
				
			all_Button.Add(Button_Close);;

		}
	}}
