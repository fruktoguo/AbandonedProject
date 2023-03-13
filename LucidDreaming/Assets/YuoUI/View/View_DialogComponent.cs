
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public static partial class ViewType 
	{ 
		public const string Dialog = "Dialog";
	}
	public partial class View_DialogComponent : UIComponent 
	{

		private Button mButton_回放;

		public Button Button_回放
		{
			get
			{
				if (mButton_回放 == null)
					mButton_回放 = rectTransform.Find("Item/BackGround/Frame/C_回放").GetComponent<Button>();
				return mButton_回放;
			}
		}

		private ButtonSwitch mButtonSwitch_自动;

		public ButtonSwitch ButtonSwitch_自动
		{
			get
			{
				if (mButtonSwitch_自动 == null)
					mButtonSwitch_自动 = rectTransform.Find("Item/BackGround/Frame/C_自动").GetComponent<ButtonSwitch>();
				return mButtonSwitch_自动;
			}
		}

		private Button mButton_快进;

		public Button Button_快进
		{
			get
			{
				if (mButton_快进 == null)
					mButton_快进 = rectTransform.Find("Item/BackGround/Frame/C_快进").GetComponent<Button>();
				return mButton_快进;
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

		public List<Button> all_Button = new();

		public List<ButtonSwitch> all_ButtonSwitch = new();

		public void FindAll()
		{
				
			all_Button.Add(Button_回放);
			all_Button.Add(Button_快进);
			all_Button.Add(Button_Close);;
				
			all_ButtonSwitch.Add(ButtonSwitch_自动);;

		}
	}}
