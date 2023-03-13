
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public static partial class ViewType 
	{ 
		public const string Tip = "Tip";
	}
	public partial class View_TipComponent : UIComponent 
	{

		private RectTransform mRectTransform_MessagePanel;

		public RectTransform RectTransform_MessagePanel
		{
			get
			{
				if (mRectTransform_MessagePanel == null)
					mRectTransform_MessagePanel = rectTransform.Find("C_MessagePanel").GetComponent<RectTransform>();
				return mRectTransform_MessagePanel;
			}
		}

		private View_TipMessageComponent mChild_TipMessage;

		public View_TipMessageComponent Child_TipMessage
		{
			get
			{
				if (mChild_TipMessage == null)
				{
					mChild_TipMessage = Entity.AddChild<View_TipMessageComponent>();
					mChild_TipMessage.rectTransform = rectTransform.Find("C_MessagePanel/D_TipMessage") as RectTransform;
				}
				return mChild_TipMessage;
			}
		}

		public List<RectTransform> all_RectTransform = new();

		public List<View_TipMessageComponent> all_View_TipMessageComponent = new();

		public void FindAll()
		{
				
			all_RectTransform.Add(RectTransform_MessagePanel);;
				
			all_View_TipMessageComponent.Add(Child_TipMessage);;

		}
	}}
