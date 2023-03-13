using System.IO;
using System.Linq;

using UnityEditor;

using UnityEngine;

namespace YuoTools
{
    public class ProjectWindowUtilEx
    {
        public static void CreateScriptUtil(string templete, string path)
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templete, path);
        }

        [MenuItem("Assets/YuoTools/CreateScript", false, priority = 0)]
        private static void CreateToolsScripts()
        {
            ProjectWindowUtilEx.CreateScriptUtil(Application.dataPath + @"/Plugins/YuoTools/Scripts/Editor/YuoScriptTemp.cs.txt", "NewTools.cs");
        }

        [MenuItem("Assets/YuoTools/CreateScript", true, priority = 0)]
        private static bool CreateScriptUtilValidate()
        {
            var folderObj = Selection.objects.FirstOrDefault();

            var folderPath = AssetDatabase.GetAssetPath(folderObj);

            if (!Directory.Exists(folderPath)) return false;

            //用来限定这个脚本只能在Script文件下创建
            return folderPath.Contains("Script");
        }
    }
}