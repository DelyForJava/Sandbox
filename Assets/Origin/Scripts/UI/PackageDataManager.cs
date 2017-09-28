using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine;
using UnityEngine.UI;


public class ItemData
{
    public int item_id;
    public int item_type;
    public string item_name;
    public string item_icon;
    public string item_desc;
    public string expire_time;
    public int max_num;
}

public class PackageDataManager : SingletonBehaviour<PackageDataManager> {

    
    private List<ItemData> itemDataList = new List<ItemData>();
    private List<GameObject> itemObjInstantiateList = new List<GameObject>();
    private List<GameObject> modelObjInstantiateList = new List<GameObject>();
    private int selectedIndex;

    public GameObject itemPrefabObj;
    public GameObject itemContextObj;
    public GameObject modelContainerObj;
    public GameObject useItemObj;

    public Button preBtn;
    public Button nextBtn;

    public Text item_name;
    public Text item_model_name;
    public Text item_desc;
    public Text expire_time;

    public Text goldNum;
    public Text diamondNum;
    //public Text max_num;

    // Use this for initialization
    void Start () {
        
        //ReqData();
    }

    private void InitData()
    {
        string json = "{'items':" +
                      "[{\"item_id\":\"1\",\"type\":\"1\",\"item_name\":\"返利券\",\"item_icon\":\"item_room\",\"item_desc\":\"使用返利券充值额外赠送10%钻石\",\"expire_time\":\"604800\",\"max_num\":\"9999\"},{\"item_id\":\"2\",\"type\":\"1\",\"item_name\":\"捕鱼礼炮\",\"item_icon\":\"item_room\",\"item_desc\":\"捕鱼游戏中可作为金币使用\",\"expire_time\":\"0\",\"max_num\":\"9999\"},{\"item_id\":\"3\",\"type\":\"1\",\"item_name\":\"豆子\",\"item_icon\":\"item_bean\",\"item_desc\":\"我是豆子\",\"expire_time\":\"0\",\"max_num\":\"9999\"},{\"item_id\":\"4\",\"type\":\"1\",\"item_name\":\"豆子\",\"item_icon\":\"item_bean\",\"item_desc\":\"我是豆子\",\"expire_time\":\"0\",\"max_num\":\"9999\"},{\"item_id\":\"5\",\"type\":\"1\",\"item_name\":\"豆子\",\"item_icon\":\"item_bean\",\"item_desc\":\"我是豆子\",\"expire_time\":\"0\",\"max_num\":\"9999\"},{\"item_id\":\"6\",\"type\":\"1\",\"item_name\":\"豆子\",\"item_icon\":\"item_bean\",\"item_desc\":\"我是豆子\",\"expire_time\":\"0\",\"max_num\":\"9999\"},{\"item_id\":\"7\",\"type\":\"1\",\"item_name\":\"豆子\",\"item_icon\":\"item_bean\",\"item_desc\":\"我是豆子\",\"expire_time\":\"0\",\"max_num\":\"9999\"},{\"item_id\":\"8\",\"type\":\"1\",\"item_name\":\"豆子\",\"item_icon\":\"item_bean\",\"item_desc\":\"我是豆子\",\"expire_time\":\"0\",\"max_num\":\"9999\"},{\"item_id\":\"9\",\"type\":\"1\",\"item_name\":\"豆子\",\"item_icon\":\"item_bean\",\"item_desc\":\"我是豆子\",\"expire_time\":\"0\",\"max_num\":\"9999\"}]}";

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
            ItemData itemData = new ItemData();
            itemData.item_id = Convert.ToInt32((string)jdItem["item_id"]);
            itemData.item_type = Convert.ToInt32((string)jdItem["type"]);
            itemData.item_name = (string)jdItem["item_name"];
            itemData.item_icon = (string)jdItem["item_icon"];
            itemData.item_desc = (string)jdItem["item_desc"];
            itemData.expire_time = (string)jdItem["expire_time"];
            itemData.max_num = Convert.ToInt32((string)jdItem["max_num"]);

            itemDataList.Add(itemData);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        goldNum.text = UIOperation.playerLobbyInfo.llGameCoin.ToString();
        diamondNum.text = UIOperation.playerLobbyInfo.llDiamondNum.ToString();
    }

    public void OpenPackage()
    {
        InitData();
        ReqData();
    }

    public void ClosePackage()
    {
        foreach (var model in modelObjInstantiateList)
        {
            DestroyImmediate(model);
        }
        foreach (var icon in itemObjInstantiateList)
        {
            DestroyImmediate(icon);
        }
        itemDataList.Clear();
        itemObjInstantiateList.Clear();
        modelObjInstantiateList.Clear();
        selectedIndex = 0;
        preBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.RemoveAllListeners();
    }

    private void ReqData()
    {
        var client = GameClient.Instance;
        //client.MahjongGamePlayer.BuyGoldReqDef();

        //foreach (var itemData in itemDataList)
        //{
        //    itemPrefabObj.transform.Find()
        //    itemData.
        //client.MahjongGamePlayer.PackageAddItemReqDef(3,50);
        //client.MahjongGamePlayer.PackageAddItemReqDef(4,100);
        //client.MahjongGamePlayer.PackageAddItemReqDef(5,55);
        //client.MahjongGamePlayer.PackageAddItemReqDef(6,77);
        //client.MahjongGamePlayer.PackageAddItemReqDef(7,770);
        //client.MahjongGamePlayer.PackageAddItemReqDef(8,99);
        //client.MahjongGamePlayer.PackageAddItemReqDef(9,1111);
        client.MahjongGamePlayer.PackageInfoReqDef(UIOperation.playerLobbyInfo.iUserID);


        //}
    }



    public void LoadDataToPanel(List<BaseMessage.ItemInfo> itemInfoList)
    {
        foreach (var itemData in itemInfoList)
        {
            if (itemData.iItemID!=0)
            {
                var dataList = itemDataList.Where(q => q.item_id == itemData.iItemID)
                    .Select(q => q)
                    .ToList();

                Texture2D itemTexture2D = (Texture2D)Resources.Load("Texture_InGame/" + dataList[0].item_icon);
                Sprite itemSprite = Sprite.Create(itemTexture2D, new Rect(0, 0, itemTexture2D.width, itemTexture2D.height), new Vector2(0.5f, 0.5f));
                itemPrefabObj.transform.Find("Button/itemImage").GetComponent<Image>().sprite = itemSprite;
                itemPrefabObj.transform.Find("Button/itemImage").GetComponent<Image>().SetNativeSize();
                itemPrefabObj.transform.Find("Button/itemCount").GetComponent<Text>().text="x"+ itemData.iItemNum;
                itemPrefabObj.transform.SetAsFirstSibling();
                itemPrefabObj.name = dataList[0].item_icon;

                var itemObjInstantiate = Instantiate(itemPrefabObj, itemContextObj.transform);
                itemObjInstantiate.name = dataList[0].item_icon + dataList[0].item_id;
                itemObjInstantiateList.Add(itemObjInstantiate);
                itemObjInstantiate.transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate()
                {
                    selectedIndex = itemObjInstantiateList.IndexOf(itemObjInstantiate);
                    foreach (var itemObj in itemObjInstantiateList)
                    {
                        if (itemObj.name != itemObjInstantiate.gameObject.name && itemObj.transform.Find("outLine").gameObject)
                        {
                            itemObj.transform.Find("outLine").gameObject.SetActive(false);
                        }
                    }
                    item_name.text=dataList[0].item_name;
                    item_model_name.text=dataList[0].item_name;
                    item_desc.text=dataList[0].item_desc;
                    if (itemData.LExpireTime != 0)
                    {
                        var date = ConvertStringToDateTime(itemData.LExpireTime);
                        var span = date - DateTime.Now;
                        expire_time.text = span.Days + "天" + span.Hours + "小时";
                    }
                    else
                    {
                        expire_time.text = "永 久";
                    }
                    useItemObj.SetActive(dataList[0].item_type == 1);
                    useItemObj.GetComponent<Button>().onClick.AddListener(delegate()
                    {
                        //使用道具
                    });

                    //模型生成及切换
                    bool isInstantiated = false;
                    foreach (var model in modelObjInstantiateList)
                    {
                        if (model.name== dataList[0].item_icon + dataList[0].item_id)
                        {
                            isInstantiated = true;
                        }
                    }

                    if (isInstantiated==false)
                    {
                        var modelObjInstantiate = Instantiate(Resources.Load("Prefabs_InGame/Item_Model/" + dataList[0].item_icon), modelContainerObj.transform) as GameObject;
                        modelObjInstantiate.name = dataList[0].item_icon + dataList[0].item_id;
                        modelObjInstantiateList.Add(modelObjInstantiate);
                    }
                    
                    foreach (var model in modelObjInstantiateList)
                    {
                        if (model.name != dataList[0].item_icon + dataList[0].item_id)
                        {
                            model.SetActive(false);
                        }
                        else
                        {
                            model.SetActive(true);
                        }
                    }

                });
            }
        }
        itemObjInstantiateList[0].transform.Find("Button").GetComponent<Button>().onClick.Invoke();
        selectedIndex = 0;
        preBtn.onClick.AddListener(delegate()
        {
            if (selectedIndex!=0)
            {
                itemObjInstantiateList[selectedIndex - 1].transform.Find("Button").GetComponent<Button>().onClick.Invoke();
            }
        });

        nextBtn.onClick.AddListener(delegate ()
        {
            if (selectedIndex != itemObjInstantiateList.Count-1)
            {
                itemObjInstantiateList[selectedIndex + 1].transform.Find("Button").GetComponent<Button>().onClick.Invoke();
            }
        });
    }

    /// <summary>        
    /// 时间戳转为C#格式时间        
    /// </summary>        
    /// <param name=”timeStamp”></param>        
    /// <returns></returns>        
    public static DateTime ConvertStringToDateTime(long unixTimeStamp)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        DateTime dt = startTime.AddSeconds(unixTimeStamp);
        return dt;
    }
}
