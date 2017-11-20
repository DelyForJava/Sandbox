using System;
using System.Collections;
using System.Collections.Generic;
using Bean.Hall;
using LitJson;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class ExchangeData
{
    public int type;
    public int item_index;
    public string item_desc;
    public string item_icon;
    public int need_bean_num;
    public int reward_type;
    public int reward_num;
}


public class ExchangeDataManager : MonoBehaviour {
    
    private List<ExchangeData> exchangeDataList = new List<ExchangeData>();
    public GameObject gridObj;

    public GameObject cardContextObj;
    public GameObject packetContextObj;
    public GameObject goldContextObj;

    public GameObject awardInfoPanelObj;
    public GameObject addressPanelObj;
    public Toggle agreeToggle;
    public Image awardImage;
    public Text awardName;
    public Text awardPrice;
    public Text awardDesc;
    public Button editAddressBtn;
    public Button confirmBtn;
    public Text mobileNum;
    public Text telNum;
    public Text addressText;


    public Text coinTextObj;
    public Text diaTextObj;

    private void InitData()
    {
        string json = "{'items':[{\"type\":\"1\",\"item_index\":\"1\",\"item_desc\":\"10元话费直充\",\"item_icon\":\"cha_card_01\",\"need_bean_num\":\"10\",\"reward_type\":\"2\",\"reward_num\":\"10\"}, {\"type\":\"1\",\"item_index\":\"2\",\"item_desc\":\"50元话费直充\",\"item_icon\":\"cha_card_02\",\"need_bean_num\":\"50\",\"reward_type\":\"2\",\"reward_num\":\"50\"}, {\"type\":\"1\",\"item_index\":\"3\",\"item_desc\":\"100元话费直充\",\"item_icon\":\"cha_card_03\",\"need_bean_num\":\"100\",\"reward_type\":\"2\",\"reward_num\":\"100\"}, {\"type\":\"1\",\"item_index\":\"4\",\"item_desc\":\"50元京东卡\",\"item_icon\":\"cha_card_04\",\"need_bean_num\":\"50\",\"reward_type\":\"3\",\"reward_num\":\"50\"}, {\"type\":\"1\",\"item_index\":\"5\",\"item_desc\":\"100元京东卡\",\"item_icon\":\"cha_card_05\",\"need_bean_num\":\"100\",\"reward_type\":\"3\",\"reward_num\":\"100\"}, {\"type\":\"1\",\"item_index\":\"6\",\"item_desc\":\"500元加油卡\",\"item_icon\":\"cha_card_06\",\"need_bean_num\":\"500\",\"reward_type\":\"4\",\"reward_num\":\"500\"}, {\"type\":\"2\",\"item_index\":\"1\",\"item_desc\":\"10元红包\",\"item_icon\":\"cha_red_01\",\"need_bean_num\":\"10\",\"reward_type\":\"5\",\"reward_num\":\"10\"}, {\"type\":\"2\",\"item_index\":\"2\",\"item_desc\":\"20元红包\",\"item_icon\":\"cha_red_02\",\"need_bean_num\":\"20\",\"reward_type\":\"5\",\"reward_num\":\"20\"}, {\"type\":\"2\",\"item_index\":\"3\",\"item_desc\":\"50元红包\",\"item_icon\":\"cha_red_03\",\"need_bean_num\":\"50\",\"reward_type\":\"5\",\"reward_num\":\"50\"}, {\"type\":\"2\",\"item_index\":\"4\",\"item_desc\":\"100元红包\",\"item_icon\":\"cha_red_04\",\"need_bean_num\":\"100\",\"reward_type\":\"5\",\"reward_num\":\"100\"}, {\"type\":\"2\",\"item_index\":\"5\",\"item_desc\":\"200元红包\",\"item_icon\":\"cha_red_05\",\"need_bean_num\":\"200\",\"reward_type\":\"5\",\"reward_num\":\"200\"}, {\"type\":\"3\",\"item_index\":\"1\",\"item_desc\":\"5万金币\",\"item_icon\":\"cha_gold_01\",\"need_bean_num\":\"5\",\"reward_type\":\"1\",\"reward_num\":\"50000\"}, {\"type\":\"3\",\"item_index\":\"2\",\"item_desc\":\"11万金币\",\"item_icon\":\"cha_gold_02\",\"need_bean_num\":\"10\",\"reward_type\":\"1\",\"reward_num\":\"110000\"}, {\"type\":\"3\",\"item_index\":\"3\",\"item_desc\":\"60万金币\",\"item_icon\":\"cha_gold_03\",\"need_bean_num\":\"50\",\"reward_type\":\"1\",\"reward_num\":\"600000\"}]}";

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
            ExchangeData exchangeData = new ExchangeData();
            exchangeData.type = Convert.ToInt32((string)jdItem["type"]);
            exchangeData.item_index = Convert.ToInt32((string)jdItem["item_index"]);
            exchangeData.item_desc = (string)jdItem["item_desc"];
            exchangeData.item_icon = (string)jdItem["item_icon"];
            exchangeData.need_bean_num = Convert.ToInt32((string)jdItem["need_bean_num"]);
            exchangeData.reward_type = Convert.ToInt32((string)jdItem["reward_type"]);
            exchangeData.reward_num = Convert.ToInt32((string)jdItem["reward_num"]);

            exchangeDataList.Add(exchangeData);
        }
    }


    // Use this for initialization
    void Start()
    {
        InitData();
        LoadDataToPanel();
    }


    private void LoadDataToPanel()
    {
        var client = GameClient.Instance;
        foreach (var exchangeData in exchangeDataList)
        {
            if (exchangeData.type == 1)
            {
                gridObj.transform.Find("title").GetComponent<Text>().text = exchangeData.item_desc;

                Texture2D texture2D;
                if ((Texture2D)Resources.Load("Texture_InGame/exchange/" + "cha_card_0" + exchangeData.item_index))
                {
                    texture2D = (Texture2D)Resources.Load("Texture_InGame/exchange/" + "cha_card_0" + exchangeData.item_index);
                }
                else
                {
                    texture2D = (Texture2D)Resources.Load("Texture_InGame/exchange/" + "cha_card_01");
                }

                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                gridObj.transform.Find("icon").GetComponent<Image>().sprite = sprite;
                gridObj.transform.Find("icon").GetComponent<Image>().SetNativeSize();

                gridObj.transform.Find("payBtn/payNum").GetComponent<Text>().text = exchangeData.need_bean_num + "金豆";

                gridObj.transform.SetAsFirstSibling();
                gridObj.name = "cardGrid" + exchangeData.item_index;
                var cardGridObjInstantiate = Instantiate(gridObj, cardContextObj.transform);

                cardGridObjInstantiate.transform.Find("payBtn").GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    if (exchangeData.need_bean_num > UIOperation.playerLobbyInfo.llGoldBean)
                    {
                        //弹窗 金币钻石不足
                        if (GameObject.Find("Canvas/showLog"))
                        {
                            GameObject.Find("Canvas/showLog").transform.Find("logText").GetComponent<Text>().text = "不好意思，您的金豆不足";
                        }
                        else
                        {
                            var showLogInstantiate = Instantiate(Resources.Load("Prefabs_InGame/" + "showLog"), GameObject.Find("Canvas").transform) as GameObject;
                            showLogInstantiate.transform.Find("logText").GetComponent<Text>().text = "不好意思，您的金豆不足";
                        }
                        return;
                    }
                    //client.MahjongGamePlayer.BuyGoldReqDef(Convert.ToInt16(shopData.shop_id), Convert.ToInt16(shopData.item_index)); 
                    //Application.OpenURL("https://www.baidu.com");
                    awardInfoPanelObj.SetActive(true);
                    awardImage.sprite = sprite;
                    awardImage.SetNativeSize();
                    awardPrice.text = exchangeData.need_bean_num.ToString();
                    awardName.text = exchangeData.item_desc.ToString();
                    awardDesc.text = "商品详情：我是" + exchangeData.item_desc.ToString();
                    editAddressBtn.onClick.AddListener(delegate()
                    {
                        //弹出地址编辑
                        addressPanelObj.SetActive(true);
                    });
                    confirmBtn.onClick.AddListener(delegate ()
                    {
                        if (agreeToggle.isOn && (mobileNum.text!="" ||telNum.text!= ""))
                        {
                            client.MahjongGamePlayer.ExchangeReqDef(Convert.ToUInt16(exchangeData.type), Convert.ToUInt16(exchangeData.item_index), mobileNum.text, telNum.text, addressText.text);
                            awardInfoPanelObj.SetActive(false);
                        }
                    });

                    
                });
            }

            if (exchangeData.type == 2)
            {
                gridObj.transform.Find("title").GetComponent<Text>().text = exchangeData.item_desc;

                Texture2D texture2D;
                if ((Texture2D)Resources.Load("Texture_InGame/exchange/" + "cha_red_0" + exchangeData.item_index))
                {
                    texture2D = (Texture2D)Resources.Load("Texture_InGame/exchange/" + "cha_red_0" + exchangeData.item_index);
                }
                else
                {
                    texture2D = (Texture2D)Resources.Load("Texture_InGame/exchange/" + "cha_red_01");
                }

                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                gridObj.transform.Find("icon").GetComponent<Image>().sprite = sprite;
                gridObj.transform.Find("icon").GetComponent<Image>().SetNativeSize();

                gridObj.transform.Find("payBtn/payNum").GetComponent<Text>().text = exchangeData.need_bean_num + "金豆";

                gridObj.transform.SetAsFirstSibling();
                gridObj.name = "packetGrid" + exchangeData.item_index;
                var packetGridObjInstantiate = Instantiate(gridObj, packetContextObj.transform);

                packetGridObjInstantiate.transform.Find("payBtn").GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    if (exchangeData.need_bean_num > UIOperation.playerLobbyInfo.llGoldBean)
                    {
                        //弹窗 金币钻石不足
                        if (GameObject.Find("Canvas/showLog"))
                        {
                            GameObject.Find("Canvas/showLog").transform.Find("logText").GetComponent<Text>().text = "不好意思，您的金豆不足";
                        }
                        else
                        {
                            var showLogInstantiate = Instantiate(Resources.Load("Prefabs_InGame/" + "showLog"), GameObject.Find("Canvas").transform) as GameObject;
                            showLogInstantiate.transform.Find("logText").GetComponent<Text>().text = "不好意思，您的金豆不足";
                        }
                        return;
                    }
                    //client.MahjongGamePlayer.BuyGoldReqDef(Convert.ToInt16(shopData.shop_id), Convert.ToInt16(shopData.item_index)); 
                    //Application.OpenURL("https://www.baidu.com");
                    awardInfoPanelObj.SetActive(true);
                    awardImage.sprite = sprite;
                    awardImage.SetNativeSize();
                    awardPrice.text = exchangeData.need_bean_num.ToString();
                    awardName.text = exchangeData.item_desc.ToString();
                    awardDesc.text = "商品详情：我是" + exchangeData.item_desc.ToString();
                    editAddressBtn.onClick.AddListener(delegate ()
                    {
                        //弹出地址编辑
                        addressPanelObj.SetActive(true);
                    });
                    confirmBtn.onClick.AddListener(delegate ()
                    {
                        if (agreeToggle.isOn && (mobileNum.text != "" || telNum.text != ""))
                        {
                            client.MahjongGamePlayer.ExchangeReqDef(Convert.ToUInt16(exchangeData.type), Convert.ToUInt16(exchangeData.item_index), mobileNum.text, telNum.text, addressText.text);
                            awardInfoPanelObj.SetActive(false);
                        }
                    });
                });
            }

            if (exchangeData.type == 3)
            {
                gridObj.transform.Find("title").GetComponent<Text>().text = exchangeData.item_desc;

                Texture2D texture2D;
                if ((Texture2D)Resources.Load("Texture_InGame/exchange/" + "cha_gold_0" + exchangeData.item_index))
                {
                    texture2D = (Texture2D)Resources.Load("Texture_InGame/exchange/" + "cha_gold_0" + exchangeData.item_index);
                }
                else
                {
                    texture2D = (Texture2D)Resources.Load("Texture_InGame/exchange/" + "cha_gold_01");
                }

                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                gridObj.transform.Find("icon").GetComponent<Image>().sprite = sprite;
                gridObj.transform.Find("icon").GetComponent<Image>().SetNativeSize();

                gridObj.transform.Find("payBtn/payNum").GetComponent<Text>().text = exchangeData.need_bean_num + "金豆";

                gridObj.transform.SetAsFirstSibling();
                gridObj.name = "cardGrid" + exchangeData.item_index;
                var goldGridObjInstantiate = Instantiate(gridObj, goldContextObj.transform);

                goldGridObjInstantiate.transform.Find("payBtn").GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    if (exchangeData.need_bean_num > UIOperation.playerLobbyInfo.llGoldBean)
                    {
                        //弹窗 金币钻石不足
                        if (GameObject.Find("Canvas/showLog"))
                        {
                            GameObject.Find("Canvas/showLog").transform.Find("logText").GetComponent<Text>().text = "不好意思，您的金豆不足";
                        }
                        else
                        {
                            var showLogInstantiate = Instantiate(Resources.Load("Prefabs_InGame/" + "showLog"), GameObject.Find("Canvas").transform) as GameObject;
                            showLogInstantiate.transform.Find("logText").GetComponent<Text>().text = "不好意思，您的金豆不足";
                        }
                        return;
                    }
                    //client.MahjongGamePlayer.BuyGoldReqDef(Convert.ToInt16(shopData.shop_id), Convert.ToInt16(shopData.item_index)); 
                    //Application.OpenURL("https://www.baidu.com");
                    awardInfoPanelObj.SetActive(true);
                    awardImage.sprite = sprite;
                    awardImage.SetNativeSize();
                    awardPrice.text = exchangeData.need_bean_num.ToString();
                    awardName.text = exchangeData.item_desc.ToString();
                    awardDesc.text = "商品详情：我是"+exchangeData.item_desc.ToString();
                    editAddressBtn.onClick.AddListener(delegate ()
                    {
                        //弹出地址编辑
                        addressPanelObj.SetActive(true);
                    });
                    confirmBtn.onClick.AddListener(delegate ()
                    {
                        if (agreeToggle.isOn && (mobileNum.text != "" || telNum.text != ""))
                        {
                            client.MahjongGamePlayer.ExchangeReqDef(Convert.ToUInt16(exchangeData.type), Convert.ToUInt16(exchangeData.item_index), mobileNum.text, telNum.text, addressText.text);
                            awardInfoPanelObj.SetActive(false);
                        }
                    });
                });
            }
        }
    }
    // Update is called once per frame
    void Update () {
        coinTextObj.text = HallData.llGameCoin.ToString();
        diaTextObj.text = HallData.llDiamondNum.ToString();
    }
}
