using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using YuoTools;

using Sirenix.OdinInspector;

public class ButtonSwitchShow : MonoBehaviour
{
    public bool Sate;

    [HideInPlayMode]
    public GameObject[] SwitchGos;

    [HorizontalGroup]
    public bool ForceRebuildLayout;

    [HorizontalGroup]
    [Tooltip("�������������ʱ,��������Ѿ�������״̬,�����л���ʾʱ����ȥ��ʾ��")]
    [HideInPlayMode]
    public bool SaveSate;

    [SerializeField]
    [ShowIf("ForceRebuildLayout")]
    [HideInPlayMode]
    private RectTransform[] RebuildTrans;

    private bool[] RebuildSates;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SwitchSate);
        RebuildSates = new bool[SwitchGos.Length];
    }

    public void SwitchSate()
    {
        Sate.Reverse();
        if (SaveSate)
        {
            for (int i = 0; i < SwitchGos.Length; i++)
            {
                if (!Sate)
                {
                    RebuildSates[i] = SwitchGos[i].activeSelf;
                    SwitchGos[i].SetActive(Sate);
                }
                else
                {
                    if (RebuildSates[i])
                    {
                        SwitchGos[i].SetActive(RebuildSates[i]);
                    }
                }
            }
        }
        else
        {
            foreach (var item in SwitchGos)
            {
                item.SetActive(Sate);
            }
        }
        if (ForceRebuildLayout)
        {
            Rebuild();
            this.YuoDelay(Rebuild);
        }
    }

    private void Rebuild()
    {
        foreach (var item in RebuildTrans)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
        }
    }
}