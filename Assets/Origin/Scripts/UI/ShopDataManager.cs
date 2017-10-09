using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopData{
    public int shop_id;
    public int item_index;
    public int need_money_type;
    public int need_money_num;
    public int add_money;
    public int extra_add;
}

public class ShopDataManager : MonoBehaviour {

    private JsonData data = new JsonData();
    private List<ShopData> shopDataList = new List<ShopData>();
    public GameObject goldGridObj;
    public GameObject diaGridObj;

    public GameObject goldContextObj;
    public GameObject diaContextObj;

    private void InitData()
    {
        string json= "{'items':[{'shop_id':1,'item_index':1,'need_money_type':1,'need_money_num':6,'add_money':60,'extra_add':0},{ 'shop_id':1,'item_index':2,'need_money_type':1,'need_money_num':18,'add_money':180,'extra_add':10},{ 'shop_id':1,'item_index':3,'need_money_type':1,'need_money_num':30,'add_money':300,'extra_add':15},{ 'shop_id':1,'item_index':4,'need_money_type':1,'need_money_num':68,'add_money':680,'extra_add':35},{ 'shop_id':1,'item_index':5,'need_money_type':1,'need_money_num':118,'add_money':1180,'extra_add':60},{ 'shop_id':1,'item_index':6,'need_money_type':1,'need_money_num':198,'add_money':1980,'extra_add':120},{ 'shop_id':1,'item_index':7,'need_money_type':1,'need_money_num':348,'add_money':3480,'extra_add':210},{ 'shop_id':1,'item_index':8,'need_money_type':1,'need_money_num':648,'add_money':6480,'extra_add':520},{ 'shop_id':2,'item_index':1,'need_money_type':2,'need_money_num':10,'add_money':10000,'extra_add':0},{ 'shop_id':2,'item_index':2,'need_money_type':2,'need_money_num':50,'add_money':50000,'extra_add':0},{ 'shop_id':2,'item_index':3,'need_money_type':2,'need_money_num':300,'add_money':300000,'extra_add':0},{ 'shop_id':2,'item_index':4,'need_money_type':2,'need_money_num':1000,'add_money':1000000,'extra_add':0},{ 'shop_id':2,'item_index':5,'need_money_type':2,'need_money_num':2000,'add_money':2000000,'extra_add':0},{ 'shop_id':2,'item_index':6,'need_money_type':2,'need_money_num':3000,'add_money':3000000,'extra_add':0},{ 'shop_id':2,'item_index':7,'need_money_type':2,'need_money_num':5000,'add_money':5000000,'extra_add':0},{ 'shop_id':2,'item_index':8,'need_money_type':2,'need_money_num':10000,'add_money':10000000,'extra_add':0}]}";

        StringToJson(json);
    }

    private void StringToJson(string json)
    {
        JsonData jd = JsonMapper.ToObject(json);

        int itemCnt = jd["items"].Count;
        
        Debug.Log("itemCnt:" + itemCnt);

        for (int i = 0; i < itemCnt; i++)
        {
            JsonData jdItem = jd["items"][i];
            ShopData shopData = new ShopData();
            shopData.shop_id = (int)jdItem["shop_id"];
            shopData.item_index = (int)jdItem["item_index"];
            shopData.need_money_type = (int)jdItem["need_money_type"];
            shopData.need_money_num = (int)jdItem["need_money_num"];
            shopData.add_money = (int)jdItem["add_money"];
            shopData.extra_add = (int)jdItem["extra_add"];

            shopDataList.Add(shopData);
        }
    }


    // Use this for initialization
    void Start () {
        InitData();
        LoadDataToPanel();
    }


    private void LoadDataToPanel()
    {
        var client = GameClient.Instance;
        foreach (var shopData in shopDataList)
        {
            if (shopData.shop_id == 2)
            {
                goldGridObj.transform.Find("title").GetComponent<Text>().text = shopData.add_money+"金币";
                if (shopData.extra_add == 0)
                {
                    goldGridObj.transform.Find("tip").gameObject.SetActive(false);
                }
                else
                {
                    goldGridObj.transform.Find("tip/Text").GetComponent<Text>().text = "赠 " + shopData.extra_add + "金";
                    goldGridObj.transform.Find("tip").gameObject.SetActive(true);
                }

                Texture2D goldTexture2D;
                if (shopData.item_index <= 6)
                {
                    goldTexture2D = (Texture2D)Resources.Load("Texture_InGame/" + "shop_gold_" + shopData.item_index);
                }
                else
                {
                    goldTexture2D = (Texture2D)Resources.Load("Texture_InGame/" + "shop_gold_6");
                }
                Sprite goldSprite = Sprite.Create(goldTexture2D, new Rect(0, 0, goldTexture2D.width, goldTexture2D.height), new Vector2(0.5f, 0.5f));
                goldGridObj.transform.Find("icon").GetComponent<Image>().sprite = goldSprite;
                goldGridObj.transform.Find("icon").GetComponent<Image>().SetNativeSize();

                goldGridObj.transform.Find("payBtn/payNum").GetComponent<Text>().text = shopData.need_money_num + "钻石";
                
                goldGridObj.transform.SetAsFirstSibling();
                goldGridObj.name = "goldGrid" + shopData.item_index;
                var goldGridObjInstantiate = Instantiate(goldGridObj, goldContextObj.transform);

                goldGridObjInstantiate.transform.Find("payBtn").GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    if (shopData.need_money_num > UIOperation.playerLobbyInfo.llDiamondNum)
                    {
                        //弹窗 金币钻石不足
                        if (GameObject.Find("Canvas/showLog"))
                        {
                            GameObject.Find("Canvas/showLog").transform.Find("logText").GetComponent<Text>().text = "不好意思，您的钻石不足";
                        }
                        else
                        {
                            var showLogInstantiate = Instantiate(Resources.Load("Prefabs_InGame/" + "showLog"), GameObject.Find("Canvas").transform) as GameObject;
                            showLogInstantiate.transform.Find("logText").GetComponent<Text>().text = "不好意思，您的钻石不足";
                        }
                        return;
                    }
                    client.MahjongGamePlayer.BuyGoldReqDef(Convert.ToInt16(shopData.shop_id), Convert.ToInt16(shopData.item_index));
                });
            }

            if (shopData.shop_id == 1)
            {
                diaGridObj.transform.Find("title").GetComponent<Text>().text = shopData.add_money + "钻石";
                if (shopData.extra_add == 0)
                {
                    diaGridObj.transform.Find("tip").gameObject.SetActive(false);
                }
                else
                {
                    diaGridObj.transform.Find("tip/Text").GetComponent<Text>().text = "赠 " + shopData.extra_add + "钻";
                    diaGridObj.transform.Find("tip").gameObject.SetActive(true);
                }

                Texture2D diaTexture2D;
                if (shopData.item_index <= 6)
                {
                    diaTexture2D = (Texture2D)Resources.Load("Texture_InGame/" + "shop_dia_" + shopData.item_index);
                }
                else
                {
                    diaTexture2D = (Texture2D)Resources.Load("Texture_InGame/" + "shop_dia_6");
                }
                Sprite diaSprite = Sprite.Create(diaTexture2D, new Rect(0, 0, diaTexture2D.width, diaTexture2D.height), new Vector2(0.5f, 0.5f));
                diaGridObj.transform.Find("icon").GetComponent<Image>().sprite = diaSprite;
                diaGridObj.transform.Find("icon").GetComponent<Image>().SetNativeSize();

                diaGridObj.transform.Find("payBtn/payNum").GetComponent<Text>().text = shopData.need_money_num + "元";

                diaGridObj.transform.SetAsFirstSibling();
                diaGridObj.name = "diaGrid" + shopData.item_index;
                Instantiate(diaGridObj, diaContextObj.transform);
            }
        }
    }

    //按钮点击事件的方法  
    void Btn_Test()
    {
        Debug.Log("这是一个按钮点击事件！哈哈");
    }


    // Update is called once per frame
    void Update () {
		
	}



}
