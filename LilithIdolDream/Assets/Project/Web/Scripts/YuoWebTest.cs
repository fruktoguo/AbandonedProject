using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;
using YuoTools;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public class YuoWebTest : SingletonMono<YuoWebTest>
{
    // Start is called before the first frame update
    public TMP_Text text;
    private Texture2D pic;
    public TMP_InputField input;
    void Start()
    {
        //Test();
        StartCoroutine(LoadTextureFromInternet(ImageUrl, image));
        Tranlslate("apple");
        input.onSubmit.AddListener(x => Tranlslate(x));
    }
    public Image image;
    public string ImageUrl;
    /// <summary>
    /// 使用协程从网络加载图片
    /// </summary>
    /// <param name="path"></param>
    /// <param name="image"></param>
    /// <returns></returns>
    IEnumerator LoadTextureFromInternet(string path, Image image)
    {
        UnityWebRequest request = new UnityWebRequest(path);
        DownloadHandlerTexture textureDownload = new DownloadHandlerTexture(true);
        request.downloadHandler = textureDownload;
        yield return request.SendWebRequest();
        if (string.IsNullOrEmpty(request.error))
        {
            pic = textureDownload.texture;
        }
        else
            Debug.Log(textureDownload.error);
        Sprite sp = Sprite.Create((Texture2D)pic, new Rect(0, 0, pic.width, pic.height), Vector2.zero);
        image.sprite = sp;
        image.SetNativeSize();
    }
    public TMP_Text TranslateText;
    [Button("翻译一下")]
    public void Tranlslate(string str)
    {
        TranslateContent(str, x =>
        {
            // var trans = JsonConvert.DeserializeObject(x);
            JSONObject note = new JSONObject(x);
            Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");//正则表达式规定格式
            TranslateText.text =
            reg.Replace(note["trans_result"][0]["dst"].str, m =>
             {
                 return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString();
             });
        });
    }
    public class TransItem
    {
        public string from;
        public string to;
        public List<TransResult> trans_result;
    }

    public class TransResult
    {
        public string src;
        public string dst;
    }
    public DateTime GetNetDateTime()
    {
        WebRequest request = null;
        WebResponse response = null;
        WebHeaderCollection headerCollection = null;
        string datetime = string.Empty;
        DateTime timeNow = DateTime.MinValue;
        try
        {
            request = WebRequest.Create("https://www.microsoft.com");
            request.Timeout = 3000;
            request.Credentials = CredentialCache.DefaultCredentials;
            response = (WebResponse)request.GetResponse();
            headerCollection = response.Headers;
            foreach (var h in headerCollection.AllKeys)
            {
                if (h == "Date")
                {
                    datetime = headerCollection[h];
                    timeNow = Convert.ToDateTime(datetime);
                }
            }
            return timeNow;
        }
        catch (Exception)
        {
            Debug.LogError("获取网络时间错误");
            return DateTime.Now;
        }
        finally
        {
            if (request != null)
            { request.Abort(); }
            if (response != null)
            { response.Close(); }
            if (headerCollection != null)
            { headerCollection.Clear(); }
        }
    }
    string mTranslateAppID = "20200728000528239";
    string mTranslatePassword = "K2gRwYhRVbziZ70ex82d";
    void TranslateContent(string myInputString, UnityAction<string> callback)
    {
        int mySalt = new System.Random().Next();

        //mAppIDmyInputStringmySaltmySecurityID
        StringBuilder mySignString = new StringBuilder();
        string myMd5Result = string.Empty;
        //1.拼接字符,为了生成sign
        mySignString.Append(mTranslateAppID);
        mySignString.Append(myInputString);
        mySignString.Append(mySalt);
        mySignString.Append(mTranslatePassword);
        //2.通过md5获取sign
        byte[] sourceMd5Byte = Encoding.Default.GetBytes(mySignString.ToString());
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] destMd5Byte = md5.ComputeHash(sourceMd5Byte);
        myMd5Result = BitConverter.ToString(destMd5Byte).Replace("-", "");
        myMd5Result = myMd5Result.ToLower();
        //3.获取web翻译的json结果
        string url = $"http://api.fanyi.baidu.com/api/trans/vip/translate?q={myInputString}&from=auto&to=zh&appid={mTranslateAppID}&salt={mySalt}&sign={myMd5Result}";
        StartCoroutine(YuoWebRequst(url, callback));
    }
    IEnumerator YuoWebRequst(string url, UnityAction<string> callback)
    {
        UnityWebRequest request = new UnityWebRequest(url);
        DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer();
        request.downloadHandler = downloadHandler;
        yield return request.SendWebRequest();
        if (string.IsNullOrEmpty(request.error))
        {
            callback?.Invoke(downloadHandler.text);
        }
        else
            callback?.Invoke(downloadHandler.error);
        yield break;
    }
}
