using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace YuoTools
{
    public class YuoSave : MonoBehaviour
    {
        public string FilePath = "Test.ini";
        public TMPro.TMP_Text text;
        private static string defPath;
        private void Start()
        {
            //设置宽高
            Add("Width", x => Screen.SetResolution((int)float.Parse(x), Screen.height, Screen.fullScreen));
            Add("Height", x => Screen.SetResolution(Screen.width, (int)float.Parse(x), Screen.fullScreen));
            //设置全屏模式
            Add("FullScreen", x => Screen.fullScreenMode = (FullScreenMode)(int)float.Parse(x));
            //设置帧率
            Add("fps", x => Application.targetFrameRate = (int)float.Parse(x));
            GetTextFromStreamingAssets(FilePath);
        }

        /// <summary>
        /// 添加一个设置解析器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="check"></param>
        void Add(string name, UnityAction<string> check)
        {
            SettingItem item = new SettingItem();
            item.SetName(name);
            item.CheckAction = check;
            Settings.Add(item);
        }
        public List<SettingItem> Settings = new List<SettingItem>();
        void CheckSetting(string str)
        {
            //移除空格
            str = str.Replace(" ", "");
            //移除tab
            str = str.Replace("\t", "");
            //全体转成大写
            str = str.ToUpper();
            foreach (SettingItem item in Settings)
            {
                CheckItem(str, item);
            }
        }
        void CheckItem(string line, SettingItem item)
        {
            if (line.StartsWith(item.CheckName))
            {
                line = line.Replace(item.CheckName, null);
                item.Value = line;
                item.CheckAction?.Invoke(line);
            }
        }
        [Serializable]
        public class SettingItem
        {
            [HorizontalGroup]
            public string name;
            [HorizontalGroup]
            public string Value;
            public void SetName(string name)
            {
                name = name.ToUpper();
                this.name = name;
                CheckName = name + "=";
            }
            public string CheckName { get; private set; }
            public UnityAction<string> CheckAction;
        }
        /// <summary>
        /// 不同设备的回车不一样
        /// </summary>
        string Enter
        {
            get
            {
                if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
                {
                    return "\r\n";
                }
                else
                {
                    return "\n";
                }
            }
        }

        /// <summary>
        /// 使用WebRequest读取StreamingAssets中的文件
        /// </summary>
        /// <param name="path">StreamingAssets下的文件路径</param>
        public void GetTextFromStreamingAssets(string path)
        {
            //安卓端
            if (Application.platform == RuntimePlatform.Android)
            {
                path = Application.streamingAssetsPath + "/" + path;
            }
            //pc
            else
            {
                path = "file:///" + Application.streamingAssetsPath + "/" + path;
            }
            UnityWebRequest requrest = UnityWebRequest.Get(path);
            requrest.SendWebRequest().completed += x =>
             {
                 //调用结束回调
                 OnLoadEnd(requrest.downloadHandler.text);
             };
        }
        void OnLoadEnd(string str)
        {
            foreach (string s in str.Split(Enter))
            {
                if (s.StartsWith("//")) continue;
                CheckSetting(s);
            }
            text.text = str;
        }
    }
}