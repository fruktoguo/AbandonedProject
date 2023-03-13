
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public partial class View_PageItemComponent : UIComponent 
	{

		private Toggle mToggle_Toggle;

		public Toggle Toggle_Toggle
		{
			get
			{
				if (mToggle_Toggle == null)
					mToggle_Toggle = rectTransform.Find("C_Toggle").GetComponent<Toggle>();
				return mToggle_Toggle;
			}
		}

		private Text mText_Index;

		public Text Text_Index
		{
			get
			{
				if (mText_Index == null)
					mText_Index = rectTransform.Find("C_Index").GetComponent<Text>();
				return mText_Index;
			}
		}
	}}
