using System;
using System.IO;
using System.Runtime.InteropServices;

using UnityEngine;
using UnityEngine.UI;

namespace YuoTools
{
    public class YuoSystemTool
    {
        public static OpenFileName OpenFileWindow(string FormatName, int fileSize = 256)
        {
            OpenFileName openFileName = new OpenFileName();
            openFileName.structSize = Marshal.SizeOf(openFileName);
            openFileName.filter = $"{FormatName}文件(*.{FormatName})\0*.{FormatName}";
            openFileName.file = new string(new char[fileSize]);
            openFileName.maxFile = openFileName.file.Length;
            openFileName.fileTitle = new string(new char[64]);
            openFileName.maxFileTitle = openFileName.fileTitle.Length;
            openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
            openFileName.title = "选择文件";
            openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

            if (LocalDialog.GetSaveFileName(openFileName))
            {
                UnityEngine.Debug.Log(openFileName.file);
                UnityEngine.Debug.Log(openFileName.fileTitle);
                return openFileName;
            }
            return null;
        }

        /// <summary>
        /// 使用 IO 流加载本地图片，并返回。
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <returns></returns>
        public static Texture2D LoadTexture2DByIO(string path)
        {
            //创建文件读取流
            FileStream _fileStream = new FileStream(path, System.IO.FileMode.Open);
            _fileStream.Seek(0, SeekOrigin.Begin);
            //创建文件长度缓冲区
            byte[] _bytes = new byte[_fileStream.Length];
            _fileStream.Read(_bytes, 0, (int)_fileStream.Length);
            _fileStream.Close();
            _fileStream.Dispose();
            //创建Texture
            Texture2D _texture2D = new Texture2D(1, 1);
            _texture2D.LoadImage(_bytes);
            return _texture2D;
        }

        /// <summary>
        /// 使用 IO 流加载图片，并将图片转换成 Sprite 类型返回
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <returns></returns>
        public static Sprite LoadSpriteByIO(string path)
        {
            Texture2D _texture2D = LoadTexture2DByIO(path);
            Sprite _sprite = Sprite.Create(_texture2D, new Rect(0, 0, _texture2D.width, _texture2D.height), new Vector2(0.5f, 0.5f));
            return _sprite;
        }

        /// <summary>
        /// 屏幕截图,记得隐藏不需要截取的部分
        /// </summary>
        /// <param name="name"></param>
        private static void CaptureScreen(string name)
        {
            ScreenCapture.CaptureScreenshot($"{name}.png", 0);
        }

        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isFile"></param>
        public static void OpenDirectory(string path, bool isFile = false)
        {
            if (string.IsNullOrEmpty(path)) return;
            path = path.Replace("/", "\\");
            if (isFile)
            {
                if (!File.Exists(path))
                {
                    UnityEngine.Debug.LogError("No File: " + path);
                    return;
                }
                path = string.Format("/Select, {0}", path);
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    UnityEngine.Debug.LogError("No Directory: " + path);
                    return;
                }
            }
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        public static MemoryInfo LogMemory()
        {
            MemoryInfo MemInfo = new MemoryInfo();
            GlobalMemoryStatus(ref MemInfo);

            double totalMb = MemInfo.TotalPhysical / 1024 / 1024;
            double avaliableMb = MemInfo.AvailablePhysical / 1024 / 1024;

            Debug.Log($"物理内存共有:{totalMb}MB");
            Debug.Log($"可使用的物理内存:{avaliableMb}MB");
            Debug.Log($"剩余内存百分比：{Math.Round((avaliableMb / totalMb) * 100, 2)}");
            return MemInfo;
        }

        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref MemoryInfo meminfo);

        public static void Command(string cmd)
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    Arguments = "/k" + cmd,
                    CreateNoWindow = false,
                }
            };
            try
            {
                process.Start();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e.Message);
            }
            finally
            {
                process.Close();
            }
        }
    }

    public struct MemoryInfo
    {
        public uint Length;
        public uint MemoryLoad;
        public ulong TotalPhysical;//总内存
        public ulong AvailablePhysical;//可用物理内存
        public ulong TotalPageFile;
        public ulong AvailablePageFile;
        public ulong TotalVirtual;
        public ulong AvailableVirtual;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class OpenFileName
    {
        public int structSize = 0;
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;
        public String filter = null;
        public String customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 0;
        public String file = null;
        public int maxFile = 0;
        public String fileTitle = null;
        public int maxFileTitle = 0;
        public String initialDir = null;
        public String title = null;
        public int flags = 0;
        public short fileOffset = 0;
        public short fileExtension = 0;
        public String defExt = null;
        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;
        public String templateName = null;
        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;
    }

    public class LocalDialog
    {
        //链接指定系统函数       打开文件对话框
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);

        public static bool GetOFN([In, Out] OpenFileName ofn)
        {
            return GetOpenFileName(ofn);
        }

        //链接指定系统函数        另存为对话框
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);

        public static bool GetSFN([In, Out] OpenFileName ofn)
        {
            return GetSaveFileName(ofn);
        }
    }
}