using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using YuoTools;

[RequireComponent(typeof(UI_Item_Anima))]

public class UI_Item : SerializedMonoBehaviour
{
    [HorizontalGroup("Top")]
    //[Title("窗口名称", TitleAlignment = TitleAlignments.Split, Bold = true, HorizontalLine = true)]
    [LabelText("窗口名称")]
    public string WindowName;

    [HorizontalGroup("Top")]
    public bool Translation;

    [ShowIf("Translation", true)]
    [Title("需要语言切换的图片", TitleAlignment = TitleAlignments.Split, Bold = true, HorizontalLine = true)]
    [SerializeField]
    private List<TranslateImage> translates;

    [SerializeField]
    private Dictionary<string, Text> Texts = new Dictionary<string, Text>();

    [SerializeField]
    private Dictionary<string, Button> buttons = new Dictionary<string, Button>();

    [ShowIf("Translation", true)]
    public bool IsSpriteatlas = false;

    [ShowIf("Translation", true)]
    [ShowIf("IsSpriteatlas", true)]
    [SerializeField]
    private string SpriteatlasName;

    [ReadOnly]
    public int UI_Layer;

    private bool LoadIsComplete = false;
    private bool IsStartLoad;
    private int index = 0;
    private UI_Item_Anima _Item_Anima;

    public UI_Item_Anima Item_Anima
    {
        get
        {
            if (!_Item_Anima) _Item_Anima = GetComponent<UI_Item_Anima>();
            return _Item_Anima;
        }
    }

    private void Awake()
    {
        UIManager.Instance.Register(this);
        Initialization();
        _OpenTools = 1 == 1 ? _OpenTools : _OpenTools;
    }

    private void Start()
    {
        UIManager.Instance.Register(this);
        Show();
    }

    [HorizontalGroup()]
    [SerializeField]
    private bool _OpenTools = false;

    [Button]
    [BoxGroup("初始化", centerLabel: true)]
    [ShowIf("_OpenTools", true)]
    public void Initialization()
    {
        IsStartLoad = true;
        if (translates.Count > 0)
        {
            LoadIsComplete = false;
            index = 0;
            foreach (var item in translates)
            {
                item.Initialization();
                switch (item.tranType)
                {
                    case TranslateImage.TranType.Path:
                        if (IsSpriteatlas)
                        {
                            AssetsLoader.GetSpriteatlas($"{LanguageManager.Instance.lanType}/{SpriteatlasName}.spriteatlas", item.path, X =>
                             {
                                 item.image.sprite = X;
                                 index++;
                             });
                        }
                        else
                        {
                            AssetsLoader.GetSprite($"{LanguageManager.Instance.lanType}/{WindowName}/{item.path}", X =>
                            {
                                item.image.sprite = X;
                                index++;
                            });
                        }
                        break;

                    case TranslateImage.TranType.Sprite:
                        if (item.tranSprite.ContainsKey(LanguageManager.Instance.lanType))
                        {
                            item.image.sprite = item.tranSprite[LanguageManager.Instance.lanType];
                        }
                        index++;
                        break;

                    default:
                        index++;
                        break;
                }
            }
            YuoDelayCon.Instance.StartCoroutine(WaitLoad());
        }
        else
        {
            LoadIsComplete = true;
        }
    }

    [ButtonGroup]
    [ShowIf("_OpenTools", true)]
    public void Show()
    {
        if (!IsStartLoad)
        {
            Initialization();
        }
        if (LoadIsComplete)
        {
            Item_Anima.Show();
        }
    }

    private void OnEnable()
    {
        UIManager.Instance.OnOpen(this);
    }

    private void OnDisable()
    {
        UIManager.Instance.Close(this);
    }

    [ButtonGroup]
    [ShowIf("_OpenTools", true)]
    public void Hide()
    {
        UIManager.Instance.Close(this);
        Item_Anima.Hide();
    }

    [ShowIf("_OpenTools", true)]
    [Button]
    public void FindAllTranslateImage()
    {
        translates.Clear();
        translates.AddRange(transform.GetComponentsInChildren<TranslateImage>());
    }

    [ShowIf("_OpenTools", true)]
    [FoldoutGroup("Raycast")]
    public List<MaskableGraphic> maskableGraphics = new List<MaskableGraphic>();

    [ShowIf("_OpenTools", true)]
    [FoldoutGroup("Raycast")]
    [Button(ButtonHeight = 30, Name = "获取所有开启了Raycast的物体")]
    public void FindAllRaycast()
    {
        maskableGraphics.Clear();
        foreach (var item in transform.GetComponentsInChildren<MaskableGraphic>())
        {
            if (item.raycastTarget)
            {
                maskableGraphics.Add(item);
            }
        }
    }

    [ShowIf("_OpenTools", true)]
    [FoldoutGroup("Raycast")]
    [Button(ButtonHeight = 30, Name = "清除剩余Raycast")]
    public void CloseRaycast()
    {
        foreach (var item in maskableGraphics)
        {
            item.raycastTarget = false;
        }
    }

    [ShowIf("_OpenTools", true)]
    [Button]
    public void FindAllText()
    {
        Texts.Clear();
        var texts = transform.GetComponentsInChildren<Text>();
        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].name.StartsWith("S"))
            {
                Texts.Add(texts[i].name.Replace("S", null), texts[i]);
            }
        }
    }

    [ShowIf("_OpenTools", true)]
    [Button]
    public void FindAllButton()
    {
        buttons.Clear();
        var btns = transform.GetComponentsInChildren<Button>();
        for (int i = 0; i < btns.Length; i++)
        {
            if (!buttons.ContainsKey(btns[i].name))
            {
                buttons.Add(btns[i].name, btns[i]);
            }
        }
    }

    public Button GetButton(string name)
    {
        if (buttons.ContainsKey(name))
        {
            return buttons[name];
        }
        return null;
    }

    public Text GetText(string name)
    {
        if (Texts.ContainsKey(name))
        {
            return Texts[name];
        }
        return null;
    }

    private IEnumerator WaitLoad()
    {
        while (index < translates.Count)
        {
            yield return null;
        }
        LoadIsComplete = true;
    }
}