using UnityEngine;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;

namespace YuoTools.UI
{
    public partial class View_GoodsComponent
    {
        public void SetData(GoodsItemData data)
        {
            Data = data;
            RawImage_Icon.texture = data.Path==""?RawImage_Icon.texture:Resources.Load<Texture>(data.Path);
            TextMeshProUGUI_价格.text = data.Price.ToString();
            TextMeshProUGUI_Num.text =$"X{data.Num}";
        }

        public GoodsItemData Data;
    }
}