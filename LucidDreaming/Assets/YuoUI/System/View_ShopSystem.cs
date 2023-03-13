using System;
using System.Collections.Generic;
using UnityEngine;
using YuoTools.Deprecated;
using YuoTools.ECS;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace YuoTools.UI
{
    public partial class View_ShopComponent
    {
        public List<View_GoodsComponent> GoodsViewList = new();

        public List<GoodsItemData> GoodsComponents = new();

        //食品
        public List<GoodsItemData> GoodsDataOfFood = new();

        //工具
        public List<GoodsItemData> GoodsDataOfTool = new();

        //礼物
        public List<GoodsItemData> GoodsDataOfGift = new();

        //其他
        public List<GoodsItemData> GoodsDataOfOther = new();

        public int Page;
        public int PageSize = 10;

        public void SortGoodsOfPrice(bool isAsc)
        {
            GoodsComponents.Sort((a, b) => a.Price.CompareTo(b.Price));
            if (!isAsc)
            {
                GoodsComponents.Reverse();
            }

            SetPage(Page);
        }

        public void SetPage(int page)
        {
            if (page > GoodsComponents.Count / PageSize)
            {
                page = GoodsComponents.Count / PageSize;
            }

            Page = page;
            var start = Page * PageSize;
            var end = start + PageSize;
            var count = GoodsComponents.Count;

            foreach (var view in GoodsViewList)
            {
                view.rectTransform.Hide();
            }

            for (var i = 0; i < PageSize; i++)
            {
                var index = start + i;
                if (index >= count)
                {
                    break;
                }

                var goods = GoodsComponents[index];
                var view = GoodsViewList[i];
                view.rectTransform.Show();
                view.SetData(goods);
            }
        }

        public enum GoodsType
        {
            Food,
            Tool,
            Gift,
            Other
        }

        public void SetGoodsType(GoodsType type)
        {
            switch (type)
            {
                case GoodsType.Food:
                    GoodsComponents = GoodsDataOfFood;
                    break;
                case GoodsType.Tool:
                    GoodsComponents = GoodsDataOfTool;
                    break;
                case GoodsType.Gift:
                    GoodsComponents = GoodsDataOfGift;
                    break;
                case GoodsType.Other:
                    GoodsComponents = GoodsDataOfOther;
                    break;
            }

            CreatePage();
            SetPage(Page);
        }

        public void CreatePage()
        {
            //数据分页
            int pageNum = GoodsComponents.Count / PageSize +
                          (GoodsComponents.Count % PageSize > 0 ? 1 : 0);

            //先显示所有页面

            for (var i = 1; i < Child_PageItem.rectTransform.parent.childCount; i++)
            {
                Child_PageItem.rectTransform.parent.GetChild(i).Show();
            }

            if (Child_PageItem.rectTransform.parent.childCount > pageNum + 1)
            {
                //如果页面数量大于需要的页面数量，则隐藏多余的页面
                for (int i = pageNum + 1; i < Child_PageItem.rectTransform.parent.childCount; i++)
                {
                    var page = Child_PageItem.rectTransform.parent.GetChild(i);
                    page.gameObject.Hide();
                }
            }
            else
            {
                //如果页面数量小于需要的页面数量，则创建剩余的页面
                for (int i = Child_PageItem.rectTransform.parent.childCount - 1; i < pageNum; i++)
                {
                    var go = Object.Instantiate(Child_PageItem.rectTransform.gameObject,
                        Child_PageItem.rectTransform.parent);
                    var page = Entity.AddChild<View_PageItemComponent>();
                    page.rectTransform = go.transform as RectTransform;
                    page.Entity.EntityName = "PageItem_" + i;
                    go.Show();
                    int index = i;
                    page.Text_Index.text = (index + 1).ToString();
                    page.Toggle_Toggle.onValueChanged.AddListener((isOn) =>
                    {
                        if (isOn)
                        {
                            SetPage(index);
                        }
                    });
                }
            }
        }
    }

    public class GoodsItemData
    {
        public string Path = "";
        public string Name = "";
        public string Desc = "";
        public int Price = 0;
        public int Num = 0;
        public string Result = "";
    }

    public class View_ShopCreateSystem : YuoSystem<View_ShopComponent>, IUICreate
    {
        protected override void Run(View_ShopComponent view)
        {
            //关闭窗口的事件注册,名字不同请自行更
            view.Button_Close.SetBtnClick(() => World.Main.GetComponent<UIManagerComponent>().Close(view.ViewName));
            view.Button_Mask.SetBtnClick(() => World.Main.GetComponent<UIManagerComponent>().Close(view.ViewName));
            view.Child_Goods.Entity.EntityName = "Goods模板";

            for (int i = 0; i < 10; i++)
            {
                var go = Object.Instantiate(view.Child_Goods.rectTransform.gameObject,
                    view.Child_Goods.rectTransform.parent);
                go.Show();
                var goods = view.Entity.AddChild<View_GoodsComponent>();
                goods.rectTransform = go.GetComponent<RectTransform>();
                goods.Entity.EntityName = "GoodsView_" + i;
                World.RunSystem<IUICreate>(goods);
                goods.Button_Icon.SetBtnClick(() =>
                {
                    view.RawImage_图标.texture = goods.RawImage_Icon.texture;
                    view.Text_名称.text = goods.Data?.Name;
                    view.Text_介绍.text = goods.Data?.Desc;
                    view.Text_效果.text = goods.Data?.Result;
                });
                view.GoodsViewList.Add(goods);
            }

            #region 初始化商品数据

            for (int i = 0; i < 95; i++)
            {
                var data = new GoodsItemData
                {
                    Path = "",
                    Name = "随机食品" + i,
                    Desc = $"这是一个随机的食品介绍--------{i}",
                    Price = Random.Range(1, 1000),
                    Num = Random.Range(2, 10),
                    Result = $"这是一个随机的食品效果--------{i}",
                };
                view.GoodsDataOfFood.Add(data);
            }

            for (int i = 0; i < 78; i++)
            {
                var data = new GoodsItemData
                {
                    Path = "",
                    Name = "随机工具" + i,
                    Desc = $"这是一个随机的工具介绍--------{i}",
                    Price = Random.Range(1, 1000),
                    Num = Random.Range(2, 10),
                    Result = $"这是一个随机的工具效果--------{i}",
                };
                view.GoodsDataOfTool.Add(data);
            }

            for (int i = 0; i < 185; i++)
            {
                var data = new GoodsItemData
                {
                    Path = "",
                    Name = "随机礼物" + i,
                    Desc = $"这是一个随机的礼物介绍--------{i}",
                    Price = Random.Range(1, 1000),
                    Num = Random.Range(2, 10),
                    Result = $"这是一个随机的礼物效果--------{i}",
                };
                view.GoodsDataOfGift.Add(data);
            }

            for (int i = 0; i < 100; i++)
            {
                var data = new GoodsItemData
                {
                    Path = "",
                    Name = "随机其他" + i,
                    Desc = $"这是一个随机的其他介绍--------{i}",
                    Price = Random.Range(1, 1000),
                    Num = Random.Range(2, 10),
                    Result = $"这是一个随机的其他效果--------{i}",
                };
                view.GoodsDataOfOther.Add(data);
            }

            #endregion

            #region 绑定事件

            view.Toggle_其他.onValueChanged.AddListener(value =>
            {
                if (value)
                {
                    view.SetGoodsType(View_ShopComponent.GoodsType.Other);
                }
            });
            view.Toggle_食品.onValueChanged.AddListener(value =>
            {
                if (value)
                {
                    view.SetGoodsType(View_ShopComponent.GoodsType.Food);
                }
            });
            view.Toggle_工具.onValueChanged.AddListener(value =>
            {
                if (value)
                {
                    view.SetGoodsType(View_ShopComponent.GoodsType.Tool);
                }
            });
            view.Toggle_礼物.onValueChanged.AddListener(value =>
            {
                if (value)
                {
                    view.SetGoodsType(View_ShopComponent.GoodsType.Gift);
                }
            });


            view.Toggle_食品.isOn = true;


            view.Toggle_价格从低到高.onValueChanged.AddListener(x =>
            {
                if (!x) return;
                view.SortGoodsOfPrice(false);
            });

            view.Toggle_价格从高到低.onValueChanged.AddListener(x =>
            {
                if (!x) return;
                view.SortGoodsOfPrice(true);
            });

            #endregion

            view.SetGoodsType(View_ShopComponent.GoodsType.Food);
        }
    }

    public class View_ShopOpenSystem : YuoSystem<View_ShopComponent>, IUIOpen
    {
        protected override void Run(View_ShopComponent view)
        {
        }
    }

    public class View_ShopCloseSystem : YuoSystem<View_ShopComponent>, IUIClose
    {
        protected override void Run(View_ShopComponent view)
        {
        }
    }
}