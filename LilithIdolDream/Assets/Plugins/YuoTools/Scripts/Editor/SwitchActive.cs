using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace YuoTools
{
    public class SwitchActive : Editor
    {
        [MenuItem("Tools/通用工具/切换物体显隐状态 &q")]
        public static void SetObjActive()
        {
            GameObject[] selectObjs = Selection.gameObjects;
            int objCtn = selectObjs.Length;
            for (int i = 0; i < objCtn; i++)
            {
                bool isAcitve = selectObjs[i].activeSelf;
                selectObjs[i].SetActive(!isAcitve);
            }
        }
    }

    public class SwitchSpriteLayer : Editor
    {
        [MenuItem("Tools/通用工具/切换精灵层级-10 &w")]
        public static void SetSortingOrder10()
        {
            SetSortingOrder(10);
        }

        [MenuItem("Tools/通用工具/切换精灵层级-1 &e")]
        public static void SetSortingOrder1()
        {
            SetSortingOrder(1);
        }

        private static void SetSortingOrder(int i)
        {
            List<SpriteRenderer> selectObjs = new List<SpriteRenderer>();

            foreach (var item in Selection.gameObjects)
            {
                selectObjs.Add(item.GetComponent<SpriteRenderer>());
            }
            foreach (var item in selectObjs)
            {
                item.sortingOrder -= i;
            }
        }
    }
}