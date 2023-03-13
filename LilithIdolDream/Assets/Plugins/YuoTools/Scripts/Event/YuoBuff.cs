using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace YuoTools
{
    public class YuoBuff
    {
        /// <summary>
        /// buff����
        /// </summary>
        public string BuffName;

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public int CountNow;

        /// <summary>
        /// ������
        /// </summary>
        public int CountMax;

        /// <summary>
        /// ������ʱ��
        /// </summary>
        public float TimeMax;

        /// <summary>
        /// ʣ�����ʱ��
        /// </summary>
        public float TimeSurplus;

        /// <summary>
        /// �������
        /// ����д����¼�
        /// </summary>
        public float TimeSpace;

        public YuoEvent<YuoBuff> OnAdd;
        public YuoEvent<YuoBuff> OnRemove;
    }
}