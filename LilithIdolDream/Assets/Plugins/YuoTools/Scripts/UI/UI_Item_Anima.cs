using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using YuoTools;

public class UI_Item_Anima : MonoBehaviour
{
    [Header("�Ƿ�ӵ�п�������")]
    public bool _OpenAnima = true;

    [ShowIf("_OpenAnima", true)]
    [EnumToggleButtons]
    public HideAnimaType _AnimaOfOpen = HideAnimaType.RightScale;

    [Header("�Ƿ�ӵ�йرն���")]
    public bool _CloseAnima = true;

    [ShowIf("_CloseAnima", true)]
    [EnumToggleButtons]
    public HideAnimaType _AnimaOfClose = HideAnimaType.LeftScale;

    [ShowIf("_CloseAnima", true)]
    [Space]
    [Range(0.5f, 3)]
    public float AnimatorHideTime = 0.5f;

    [Header("�������ŵ��ٶ�")]
    public float AnimaSpeed = 1;

    private Animator animator;
    public bool Offset;

    [ShowIf("Offset", true)]
    public float OffsetLenth;

    private UI_Item _ui_Item;

    public UI_Item uiItem
    {
        get
        {
            if (!_ui_Item) _ui_Item = GetComponent<UI_Item>();
            return _ui_Item;
        }
    }

    public Animator Animator
    {
        get
        {
            if (!animator) animator = GetComponent<Animator>();
            return animator;
        }
    }

    //[System.Flags]
    public enum HideAnimaType
    {
        /// <summary>
        /// ��������
        /// </summary>
        UpScale,
        /// <summary>
        /// ��������
        /// </summary>
        LeftScale,
        /// <summary>
        /// ��������
        /// </summary>
        RightScale,
        /// <summary>
        /// �����г�
        /// </summary>
        LeftRotate,
        /// <summary>
        ///�����г�
        /// </summary>
        RightRotate
    }

    private YuoDelayMod HideMod;

    private void OnEnable()
    {
        if (_OpenAnima)
        {
            Animator.SetFloat("Speed", -AnimaSpeed);
            Animator.Play(_AnimaOfOpen.ToString(), 0, 1);
        }
    }

    private void OnDisable()
    {
        if (HideMod != null)
        {
            HideMod = null;
        }
    }

    public void Show()
    {
        if (HideMod != null)
        {
            HideMod.Stop();
            HideMod = null;
            gameObject.Show();
            //print(uiItem.WindowName + "ReShow");
        }
        else
        {
            //print(uiItem.WindowName + "Show");
            gameObject.Show();
        }
    }

    public void Hide()
    {
        if (gameObject.activeSelf)
        {
            //print(uiItem.WindowName + "Hide");
            if (_CloseAnima)
            {
                Animator.Play(_AnimaOfClose.ToString(), 0, 0);
                Animator.SetFloat("Speed", AnimaSpeed);
                if (HideMod == null)
                {
                    HideMod = this.YuoDelay(() => gameObject.Hide(), AnimatorHideTime / AnimaSpeed * 2).SetName(uiItem.WindowName);
                }
            }
            else
            {
                gameObject.Hide();
            }
        }
    }

    public void AnimatorRasume()
    {
        Animator.speed = 1;
    }

    public void OnAnimationSlider(float value)
    {
        Animator.speed = 0;
        Animator.Play(_AnimaOfClose.ToString(), 0, value);
    }
}