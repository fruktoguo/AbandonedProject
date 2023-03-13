
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
namespace YuoTools.UI
{
	public static partial class ViewType 
	{ 
		public const string Save = "Save";
	}
	public partial class View_SaveComponent : UIComponent 
	{

		private RectTransform mRectTransform_AddItem;

		public RectTransform RectTransform_AddItem
		{
			get
			{
				if (mRectTransform_AddItem == null)
					mRectTransform_AddItem = rectTransform.Find("Item/BackGround/SavePanel/Viewport/Content/C_AddItem").GetComponent<RectTransform>();
				return mRectTransform_AddItem;
			}
		}

		private Button mButton_AddItem;

		public Button Button_AddItem
		{
			get
			{
				if (mButton_AddItem == null)
					mButton_AddItem = rectTransform.Find("Item/BackGround/SavePanel/Viewport/Content/C_AddItem/C_AddItem").GetComponent<Button>();
				return mButton_AddItem;
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

		private View_SaveItemComponent mChild_SaveItem;

		public View_SaveItemComponent Child_SaveItem
		{
			get
			{
				if (mChild_SaveItem == null)
				{
					mChild_SaveItem = Entity.AddChild<View_SaveItemComponent>();
					mChild_SaveItem.rectTransform = rectTransform.Find("Item/BackGround/SavePanel/Viewport/Content/D_SaveItem") as RectTransform;
				}
				return mChild_SaveItem;
			}
		}

		public List<RectTransform> all_RectTransform = new();

		public List<Button> all_Button = new();

		public List<View_SaveItemComponent> all_View_SaveItemComponent = new();

		public void FindAll()
		{
				
			all_RectTransform.Add(RectTransform_AddItem);;
				
			all_Button.Add(Button_AddItem);
			all_Button.Add(Button_Close);;
				
			all_View_SaveItemComponent.Add(Child_SaveItem);;

		}
	}}
