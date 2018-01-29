using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum SpecialType
{
    SPECIAL_TYPE_GIVEUP = 0x01,//放弃
    SPECIAL_TYPE_PENG = 0x02,//可碰
    SPECIAL_TYPE_MINGGANG = 0x04,//明杠
    SPECIAL_TYPE_ANGANG = 0x08,//暗杠
    SPECIAL_TYPE_PENGGANG = 0x10,//碰杠
    SPECIAL_TYPE_HU = 0x20,//胡
}

public enum WinLostType
{
    WINLOST_ZIMO = 1, //  自摸  
    WINLOST_BEI_ZIMO,//被自摸
    WINLOST_DIANPAO,//点炮
    WINLOST_DIANPAOHU,//点炮胡
    WINLOST_QIANGGANGHU,//抢杠胡
    WINLOST_BEI_QIANGGANGHU,//被抢杠胡
    WINLOST_GANGSHANGHUA,//杠上开花
    WINLOST_BEI_GANGSHANGHUA,//被杠上开花	add by shaxuehui 2017/09/20
    WINLOST_GANGSHANGPAO,//杠上炮
    WINLOST_BEI_GANGSHANGPAO,
    WINLOST_GUAFENG_ZHI,//明杠
    WINLOST_GUAFENG_XIA,//碰杠
    WINLOST_BEI_GUAFENG_ZHI,//被明杠
    WINLOST_BEI_GUAFENG_XIA,//被碰杠
    WINLOST_XIAYU,//暗杠
    WINLOST_BEI_XIAYU,//被暗杠
    WINLOST_HUAZHU,//花猪
    WINLOST_CHAHUAZHU,//查花猪
    WINLOST_CHAJIAO,//查叫
    WINLOST_BEI_CHAJIAO,//被查叫
};


public enum ScmjHuType
{
    HU_NOHU = 0,
    HU_QING18LUOHAN,    //9 fans
    HU_18LUOHAN,    //7 fans
    HU_TIANHU,     // 6 fans
    HU_DIHU,
    HU_QINGLONG7DUI,
    HU_LONG7DUI, // 5 fans
    HU_QING7DUI,
    HU_QING19,
    HU_JIANGJINGOUDIAO,
    HU_QINGJINGOUDIAO,
    HU_QINGDUI,     // 4 fans
    //HU_JIANGDUI,
    HU_QING1SE, // 3 fans清一色
    HU_DAI19,
    HU_7DUI,
    HU_JINGOUDIAO,
    HU_DUIDUIHU,    // 2 fans大对子
    HU_PINGHU,		// 1 fans
};


public class MahjongDisplay : SingletonBehaviour<MahjongDisplay>
{
    public Button exitBtn;
    public Button reconnectBtn;
    public GameClient client;
    public Camera handTileCamera;
    public GameObject timerObj;
    public GameObject directionObj;
    public Text timeVal;
    public Text remainTileText;
    
    public float timePlus;
    public float timeMinus;
    public bool isPlus;
    public string timeDes;
    public int remainTileCount = 108;


    
    //public List<Tile> handTileList = new List<Tile>();
    //public List<Tile> upTileList = new List<Tile>();
    public List<sbyte> upTileValueSbyteList = new List<sbyte>();
    public float handTileY;
    public bool isChangeTile;

    public GameObject deskTileContainer_0;
    public GameObject deskTileContainer_1;
    public GameObject deskTileContainer_2;
    public GameObject deskTileContainer_3;
    public Dictionary<int,List<Tile>> deskDic =new Dictionary<int, List<Tile>>();
    public List<Tile> deskList_0 = new List<Tile>();
    public List<Tile> deskList_1 = new List<Tile>();
    public List<Tile> deskList_2 = new List<Tile>();
    public List<Tile> deskList_3 = new List<Tile>();

    public bool isYourTurn = false;
    public bool isPlayedHoldTile = false;
    public bool isHued = false;
    


    public GameObject riverTileContainer_0;
    public GameObject riverTileContainer_1;
    public GameObject riverTileContainer_2;
    public GameObject riverTileContainer_3;
    public Dictionary<int, List<Tile>> riverDic_0 = new Dictionary<int, List<Tile>>();
    public Dictionary<int, List<Tile>> riverDic_1 = new Dictionary<int, List<Tile>>();
    public Dictionary<int, List<Tile>> riverDic_2 = new Dictionary<int, List<Tile>>();
    public Dictionary<int, List<Tile>> riverDic_3 = new Dictionary<int, List<Tile>>();
    //public List<Tile> riverList_0 = new List<Tile>();

    public int pengGangHuCount = 0;

    public GameObject huTileContainer_0;
    public GameObject huTileContainer_1;
    public GameObject huTileContainer_2;
    public GameObject huTileContainer_3;
    
    public List<Tile> huList_0 = new List<Tile>();
    public List<Tile> huList_1 = new List<Tile>();
    public List<Tile> huList_2 = new List<Tile>();
    public List<Tile> huList_3 = new List<Tile>();

    public GameObject pic1;
    public GameObject pic2;
    public GameObject pic3;
    public GameObject pic4;

    public GameObject wallTileContainer;
    public List<GameObject> wallTileList = new List<GameObject>();

    //public GameObject handTileContainer_0;
    public GameObject handTileContainer_1;
    public GameObject handTileContainer_2;
    public GameObject handTileContainer_3;

    void Start()
    {
        InitTableTiles();
        client = GameClient.Instance;
        exitBtn.onClick.AddListener(delegate
        {
            ClearAllData();
            SceneManager.LoadScene(0);
        });
        reconnectBtn.onClick.AddListener(delegate
        {
            client.MahjongGamePlayer.ReDispenseSend();
        });

        handTileY = HandTile.Instance.HoldTileObj.transform.position.y;
        isChangeTile = false;
        
        
        deskDic.Add(0, deskList_0);
        deskDic.Add(1, deskList_1);
        deskDic.Add(2, deskList_2);
        deskDic.Add(3, deskList_3);

        testBtn.onClick.RemoveAllListeners();
        testBtn.onClick.AddListener(delegate()
        {
            MakeTest();
        });
    }
    
    private float keepTime = 0.2f;
    public bool showStop = false;
    public void WaitForPlayersSpecialShow()
    {
        if (!showStop)
        {
            StartCoroutine(ShowIt1());
            StartCoroutine(ShowIt2());
            StartCoroutine(ShowIt3());
            StartCoroutine(ShowIt4());
        }
    }
    
    private IEnumerator ShowIt1()
    {
        pic1.gameObject.SetActive(true);
        yield return new WaitForSeconds(keepTime);
        pic1.gameObject.SetActive(false);
    }
    private IEnumerator ShowIt2()
    {
        yield return new WaitForSeconds(keepTime);
        pic2.gameObject.SetActive(true);
        yield return new WaitForSeconds(keepTime);
        pic2.gameObject.SetActive(false);
    }
    private IEnumerator ShowIt3()
    {
        yield return new WaitForSeconds(keepTime*2);
        pic3.gameObject.SetActive(true);
        yield return new WaitForSeconds(keepTime);
        pic3.gameObject.SetActive(false);
    }
    private IEnumerator ShowIt4()
    {
        yield return new WaitForSeconds(keepTime*3);
        pic4.gameObject.SetActive(true);
        yield return new WaitForSeconds(keepTime);
        pic4.gameObject.SetActive(false);
        WaitForPlayersSpecialShow();
    }

    public void InitTableTiles()
    {
        //desk
        foreach (Transform child in deskTileContainer_0.transform)
        {
            foreach (Transform childImage in child.transform)
            {
                childImage.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in deskTileContainer_1.transform)
        {
            foreach (Transform childImage in child.transform)
            {
                childImage.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in deskTileContainer_2.transform)
        {
            foreach (Transform childImage in child.transform)
            {
                childImage.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in deskTileContainer_3.transform)
        {
            foreach (Transform childImage in child.transform)
            {
                childImage.gameObject.SetActive(false);
            }
        }

        //river
        foreach (Transform childGroup in riverTileContainer_0.transform)
        {
            foreach (Transform child in childGroup.transform)
            {
                foreach (Transform childImage in child.transform)
                {
                    childImage.gameObject.SetActive(false);
                }
            }
        }
        foreach (Transform childGroup in riverTileContainer_1.transform)
        {
            foreach (Transform child in childGroup.transform)
            {
                foreach (Transform childImage in child.transform)
                {
                    childImage.gameObject.SetActive(false);
                }
            }
        }
        foreach (Transform childGroup in riverTileContainer_2.transform)
        {
            foreach (Transform child in childGroup.transform)
            {
                foreach (Transform childImage in child.transform)
                {
                    childImage.gameObject.SetActive(false);
                }
            }
        }
        foreach (Transform childGroup in riverTileContainer_3.transform)
        {
            foreach (Transform child in childGroup.transform)
            {
                foreach (Transform childImage in child.transform)
                {
                    childImage.gameObject.SetActive(false);
                }
            }
        }


        //hu
        foreach (Transform child in huTileContainer_0.transform)
        {
            foreach (Transform childImage in child.transform)
            {
                childImage.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in huTileContainer_1.transform)
        {
            foreach (Transform childImage in child.transform)
            {
                childImage.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in huTileContainer_2.transform)
        {
            foreach (Transform childImage in child.transform)
            {
                childImage.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in huTileContainer_3.transform)
        {
            foreach (Transform childImage in child.transform)
            {
                childImage.gameObject.SetActive(false);
            }
        }

        //wall
        foreach (Transform wallTileContainerWithIndex in wallTileContainer.transform)
        {
            foreach (Transform dunTile in wallTileContainerWithIndex.transform)
            {
                foreach (Transform tile in dunTile.transform)
                {
                    wallTileList.Add(tile.gameObject);
                    tile.gameObject.SetActive(false);
                }
            }
        }

        //hand
        handTileContainer_1.SetActive(false);
        handTileContainer_2.SetActive(false);
        handTileContainer_3.SetActive(false);
    }

    public void RefreshWallTile()
    {
        foreach (var tile in wallTileList)
        {
            tile.gameObject.SetActive(false);
        }
        for (int i = 0; i < remainTileCount; i++)
        {
            wallTileList[i].SetActive(true);
        }
    }

    private int lastDeskAddIndex = 0;
    public void RefreshDeskTileData(sbyte index,sbyte valueSbyte)
    {
        lastDeskAddIndex = index;
        Tile tile = new Tile();
        tile.ValueSbyte = valueSbyte;
        switch (index)
        {
            case 0:
                deskList_0.Add(tile);
                RefreshDeskTileImage(deskTileContainer_0, deskList_0);
                break;
            case 1:
                deskList_1.Add(tile);
                RefreshDeskTileImage(deskTileContainer_1, deskList_1);
                break;
            case 2:
                deskList_2.Add(tile);
                RefreshDeskTileImage(deskTileContainer_2, deskList_2);
                break;
            case 3:
                deskList_3.Add(tile);
                RefreshDeskTileImage(deskTileContainer_3, deskList_3);
                break;
        }
    }

    public void RefreshDeskTileImage(GameObject containerObj,List<Tile> list)
    {
        int count = 0;
        foreach (Transform child in containerObj.transform)
        {
            foreach (Transform childImage in child.transform)
            {
                childImage.gameObject.SetActive(false);
            }
            if (count < list.Count)
            {
                if (list[count].FaceValue == 0)
                {
                    foreach (Transform childImage in child.transform)
                    {
                        childImage.gameObject.SetActive(false);
                    }
                }
                else
                {
                    string typeName;
                    if (list[count].FaceType == TileType.Wan)
                    {
                        typeName = "W";
                    }
                    else if (list[count].FaceType == TileType.Tong)
                    {
                        typeName = "T";
                    }
                    else
                    {
                        typeName = "S";
                    }
                    child.Find("MJ_" + typeName + list[count].FaceValue).gameObject.SetActive(true);
                }
            }
            
            count++;
        }
    }

    public void DeleteDeskTile(int index)
    {
        switch (index)
        {
            case 0:
                deskList_0.Remove(deskList_0[deskList_0.Count - 1]);
                RefreshDeskTileImage(deskTileContainer_0, deskList_0);
                break;
            case 1:
                deskList_1.Remove(deskList_1[deskList_1.Count - 1]);
                RefreshDeskTileImage(deskTileContainer_1, deskList_1);
                break;
            case 2:
                deskList_2.Remove(deskList_2[deskList_2.Count - 1]);
                RefreshDeskTileImage(deskTileContainer_2, deskList_2);
                break;
            case 3:
                deskList_3.Remove(deskList_3[deskList_3.Count - 1]);
                RefreshDeskTileImage(deskTileContainer_3, deskList_3);
                break;
        }
    }

    public void RefreshRiverTileData(sbyte index, sbyte valueSbyte, SpecialType specialType, int count)
    {
        Tile tile = new Tile();
        tile.ValueSbyte = valueSbyte;
        List<Tile> list = new List<Tile>();
        switch (specialType)
        {
            case SpecialType.SPECIAL_TYPE_PENG:
                for (int i = 0; i < 3; i++)
                {
                    list.Add(tile);
                }
                break;
            case SpecialType.SPECIAL_TYPE_PENGGANG:
                tile.ValueSbyte = HandTile.Instance.HoldTile.ValueSbyte;
                break;
            case SpecialType.SPECIAL_TYPE_ANGANG:
                tile.ValueSbyte = HandTile.Instance.HoldTile.ValueSbyte;
                for (int i = 0; i < 4; i++)
                {
                    list.Add(tile);
                }
                break;
            case SpecialType.SPECIAL_TYPE_MINGGANG:
                for (int i = 0; i < 4; i++)
                {
                    list.Add(tile);
                }
                break;
            default:
                
                break;
        }

        switch (index)
        {
            case 0:
                if (specialType == SpecialType.SPECIAL_TYPE_PENGGANG)
                {
                    foreach (var item in riverDic_0)
                    {
                        if (item.Value[0].ValueSbyte==tile.ValueSbyte)
                        {
                            item.Value.Add(tile);
                        }
                    }
                }
                else
                {
                    riverDic_0.Add(count, list);
                }
                RefreshRiverTileImage(riverTileContainer_0, riverDic_0);
                break;
            default:
                break;
        }

        if (specialType != SpecialType.SPECIAL_TYPE_PENGGANG && specialType != SpecialType.SPECIAL_TYPE_ANGANG)
        {
            DeleteDeskTile(lastDeskAddIndex);
        }

    }

    public void RefreshRiverTileImage(GameObject containerObj, Dictionary<int, List<Tile>> dictionary)
    {
        int count = 0;
        foreach (Transform childGroup in containerObj.transform)
        {
            if (count < dictionary.Count)
            {
                List<Transform> childTransformList = new List<Transform>();
                foreach (Transform child in childGroup.transform)
                {
                    childTransformList.Add(child);
                }
                for (int i = 0; i < dictionary[count].Count; i++)
                {
                    if (dictionary[count][i].FaceValue == 0)
                    {
                        foreach (Transform childImage in childTransformList[i].transform)
                        {
                            childImage.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        string typeName;
                        if (dictionary[count][i].FaceType == TileType.Wan)
                        {
                            typeName = "W";
                        }
                        else if (dictionary[count][i].FaceType == TileType.Tong)
                        {
                            typeName = "T";
                        }
                        else
                        {
                            typeName = "S";
                        }
                        childTransformList[i].Find("MJ_" + typeName + dictionary[count][i].FaceValue).gameObject.SetActive(true);
                    }
                }
                
            }
            count++;
        }
    }
    public void RefreshHuTileData(sbyte index, sbyte valueSbyte)
    {
        Tile tile = new Tile();
        tile.ValueSbyte = valueSbyte;
        switch (index)
        {
            case 0:
                huList_0.Add(tile);
                RefreshHuTileImage(huTileContainer_0, huList_0);
                break;
            case 1:
                huList_1.Add(tile);
                RefreshHuTileImage(huTileContainer_1, huList_1);
                break;
            case 2:
                huList_2.Add(tile);
                RefreshHuTileImage(huTileContainer_2, huList_2);
                break;
            case 3:
                huList_3.Add(tile);
                RefreshHuTileImage(huTileContainer_3, huList_3);
                break;
        }
        DeleteDeskTile(lastDeskAddIndex);
    }

    public void RefreshHuTileImage(GameObject containerObj, List<Tile> list)
    {
        int count = 0;
        foreach (Transform child in containerObj.transform)
        {
            foreach (Transform childImage in child.transform)
            {
                childImage.gameObject.SetActive(false);
            }
            if (count < list.Count)
            {
                if (list[count].FaceValue == 0)
                {
                    foreach (Transform childImage in child.transform)
                    {
                        childImage.gameObject.SetActive(false);
                    }
                }
                else
                {
                    string typeName;
                    if (list[count].FaceType == TileType.Wan)
                    {
                        typeName = "W";
                    }
                    else if (list[count].FaceType == TileType.Tong)
                    {
                        typeName = "T";
                    }
                    else
                    {
                        typeName = "S";
                    }
                    child.Find("MJ_" + typeName + list[count].FaceValue).gameObject.SetActive(true);
                }
            }

            count++;
        }
    }

    public void SetDerection(sbyte bankerIndex)
    {
        directionObj.transform.rotation= Quaternion.Euler(0.0f, (-90) * (bankerIndex + 1), 0.0f); 
    }


    

    public void GetHandCardData(sbyte[] cards)
    {
        List<Tile> tmpList=new List<Tile>();
        Array.Sort(cards);
        
        for (int i = 0; i < cards.Length; i++)
        {
            var tile = new Tile();
            tile.ValueSbyte = cards[i];
            tmpList.Add(tile);
        }
        HandTile.Instance.AddTile(tmpList);
    }

    //public void SortHandTileData()
    //{
    //    List<Tile> totalTileList = new List<Tile>();
    //    totalTileList.AddRange(handTileList);
    //    for (int i = 0; i < totalTileList.Count; i++)
    //    {
    //        if (totalTileList[i].ValueSbyte == 0)
    //        {
    //            totalTileList.Remove(totalTileList[i]);
    //        }
    //    }
    //    totalTileList.Sort((x, y) => x.ValueSbyte.CompareTo(y.ValueSbyte));

    //    for (int i = 0; i < totalTileList.Count; i++)
    //    {
    //        if (totalTileList[i].ValueSbyte == 0)
    //        {
    //            totalTileList.Remove(totalTileList[i]);
    //        }
    //    }
    //    //Debug.Log("totalTileList_sort~~~~~~~~~~~~~~~~~~~");
    //    //for (int i = 0; i < totalTileList.Count; i++)
    //    //{
    //    //    Debug.Log("totalTileList index:" + i + " value:" + totalTileList[i].ValueSbyte + " name:" + totalTileList[i].SelfObj.name);
    //    //}
    //    handTileList.Clear();
    //    handTileList.AddRange(totalTileList);
    //    totalTileList.Clear();
    //}


    //public void RefreshHandTileData()
    //{
    //    //SortHandTileData();
    //    int index = 0;
    //    foreach (Transform child in handTileContainer.transform)
    //    {
            
    //        foreach (Transform childImage in child.transform)
    //        {
    //            childImage.gameObject.SetActive(false);
    //        }
    //        if (index < handTileList.Count)
    //        {
    //            if (handTileList[index].FaceValue == 0)
    //            {
    //                child.gameObject.SetActive(false);
    //            }
    //            else
    //            {
    //                string typeName;
    //                if (handTileList[index].FaceType == TileType.Wan)
    //                {
    //                    typeName = "W";
    //                }
    //                else if (handTileList[index].FaceType == TileType.Tong)
    //                {
    //                    typeName = "T";
    //                }
    //                else
    //                {
    //                    typeName = "S";
    //                }
    //                child.Find("MJ_" + typeName + handTileList[index].FaceValue).gameObject.SetActive(true);
    //            }
    //        }
            
    //        index++;
    //    }
    //    ReSetContainerData(handTileContainer, handTileList);
    //}

	
	void Update ()
	{
	    timePlus += Time.deltaTime;
	    if (timeMinus >= 0)
	    {
	        timeMinus -= Time.deltaTime;
        }
	    if (isPlus)
	    {
	        timeVal.text = ((int)timePlus).ToString();
        }
	    else
	    {
	        timeVal.text = ((int)timeMinus).ToString();
        }

	    remainTileText.text = "剩余牌：" + remainTileCount;



        if (Input.GetMouseButtonUp(0))
	    {
	        Ray ray = handTileCamera.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线
	        RaycastHit hitInfo;
	        if (Physics.Raycast(ray, out hitInfo))
	        {
	            GameObject gameObj = hitInfo.collider.gameObject;
	            if (gameObj.tag == "HandTile")
	            {
	                var handTileList = HandTile.Instance.HandTileList;
	                var holdTile = HandTile.Instance.HoldTile;
	                //handTileList.Add(HandTile.Instance.HoldTile);
                    if (isYourTurn && isHued==false)
	                {
	                    if (gameObj.transform.position.y == 0.35f)
	                    {
                            //将牌打出
                            foreach (var tile in handTileList)
	                        {
	                            if (tile.SelfObj && tile.SelfObj.name == gameObj.name)
	                            {
	                                client.MahjongGamePlayer.GamePlayCardSend(tile.ValueSbyte);
                                    //播放丢出此牌的动画
                                    break;
                                }
	                        }
	                        if (gameObj.name == holdTile.SelfObj.name)
	                        {
	                            isPlayedHoldTile = true;
                                client.MahjongGamePlayer.GamePlayCardSend(HandTile.Instance.HoldTile.ValueSbyte);
	                        }
	                        isYourTurn = false;

                            //手牌整理
	                        foreach (Transform child in HandTile.Instance.HandTileContainer.transform)
	                        {
	                            child.transform.position = new Vector3(child.transform.position.x, handTileY, child.transform.position.z);
	                        }
	                        holdTile.SelfObj.transform.position = new Vector3(holdTile.SelfObj.transform.position.x, handTileY, holdTile.SelfObj.transform.position.z);
                        }
	                    else
	                    {
	                        gameObj.transform.position = new Vector3(gameObj.transform.position.x, 0.35f, gameObj.transform.position.z);
                            //显示能胡的牌
	                        foreach (var tile in handTileList)
	                        {
	                            if (tile.SelfObj && tile.SelfObj.name == gameObj.name)
	                            {
	                                if (!canHuDic.ContainsKey(tile.ValueSbyte))
	                                {
	                                    ClearCanHuTileList();
                                    }
	                                else
	                                {
	                                    ShowCanHuTile(canHuDic[tile.ValueSbyte]);
	                                    break;
                                    }
	                                
                                }
	                        }
	                        if (gameObj.name == holdTile.SelfObj.name)
	                        {
	                            if (holdTile.SelfObj && holdTile.SelfObj.name == gameObj.name)
	                            {
	                                if (canHuDic.ContainsKey(holdTile.ValueSbyte))
	                                {
	                                    ShowCanHuTile(canHuDic[holdTile.ValueSbyte]);
	                                }
	                                else
	                                {
	                                    ClearCanHuTileList();
	                                }
	                            }
                            }


                        }


	                    //换牌时
	                    if (isChangeTile)
	                    {
	                        if (upTileValueSbyteList.Count < 3)
	                        {
	                            foreach (var tile in handTileList)
	                            {
                                    if (tile.SelfObj.name == gameObj.name)
                                    {
                                        upTileValueSbyteList.Add(tile.ValueSbyte);
                                        //upTileList.Add(tile);
                                    }
	                            }
	                            if (gameObj.name == holdTile.SelfObj.name)
	                            {
	                                upTileValueSbyteList.Add(holdTile.ValueSbyte);
                                }

                            }
	                        else
	                        {
	                            finalUpList[0].SelfObj.transform.position = new Vector3(finalUpList[0].SelfObj.transform.position.x, handTileY, finalUpList[0].SelfObj.transform.position.z);
	                            finalUpList.Remove(finalUpList[0]);
	                            

                                foreach (var tile in handTileList)
	                            {
                                    if (tile.SelfObj && tile.SelfObj.name == gameObj.name)
                                    {
                                        upTileValueSbyteList.Add(tile.ValueSbyte);
                                        finalUpList.Add(tile);
                                    }
                                }
	                            if (gameObj.name == holdTile.SelfObj.name)
	                            {
	                                upTileValueSbyteList.Add(holdTile.ValueSbyte);
	                                finalUpList.Add(holdTile);
                                }
                                upTileValueSbyteList.Remove(upTileValueSbyteList[0]);
                            }
	                    }
	                    else
	                    {
	                        foreach (var tile in handTileList)
	                        {
	                            if (tile.SelfObj && tile.SelfObj.name != gameObj.name)
	                            {
	                                tile.SelfObj.transform.position = new Vector3(tile.SelfObj.transform.position.x, handTileY, tile.SelfObj.transform.position.z);
	                            }
	                        }
	                    }
                    }
                    //非自己顺序
	                else
	                {
	                    if (gameObj.transform.position.y == 0.35f)
	                    {
	                        gameObj.transform.position = new Vector3(gameObj.transform.position.x, handTileY, gameObj.transform.position.z);
                        }
	                    else
	                    {
	                        gameObj.transform.position = new Vector3(gameObj.transform.position.x, 0.35f, gameObj.transform.position.z);
                        }
                        foreach (var tile in handTileList)
                        {
                            

                            if (tile.SelfObj && tile.SelfObj.name != gameObj.name)
	                        {
                                tile.SelfObj.transform.position = new Vector3(tile.SelfObj.transform.position.x, handTileY, tile.SelfObj.transform.position.z);
	                        }
	                    }
                    }
	                

	            }
	        }
	    }
    }

    public GameObject turnObj;
    public void ShowTurn(sbyte index)
    {
        turnObj.transform.rotation = Quaternion.Euler(0.0f, (-90) * index, 0.0f);
    }

    public void ShowTime(string timeDes,int duration)
    {
        timerObj.SetActive(true);
        timerObj.transform.Find("timeDes").GetComponent<Text>().text = timeDes;
        if (duration == 0)
        {
            timePlus = 0;
            isPlus = true;
        }
        else
        {
            timeMinus = duration;
            isPlus = false;
        }
        
    }

    public void CloseTime()
    {
        timerObj.SetActive(false);
    }

    public GameObject playerInfoPanel;

    public void StartGame()
    {
        playerInfoPanel.gameObject.SetActive(true);
    }



    public GameObject exchangeUIPanel;
    public Button exchangeConfirm;
    private List<Tile> finalUpList =new List<Tile>();
    public void ExchangeTiles(sbyte time_left)
    {
        isChangeTile = true;
        ShowTime("还剩", time_left);
        StartCoroutine(ExchangeEnumerator(time_left));
        exchangeUIPanel.SetActive(true);
        exchangeConfirm.onClick.RemoveAllListeners();
        exchangeConfirm.onClick.AddListener(delegate()
        {
            client.MahjongGamePlayer.GameExchangeCardsSend(upTileValueSbyteList[0], upTileValueSbyteList[1], upTileValueSbyteList[2], 3);
            //for (int i = 0; i < upTileValueSbyteList.Count; i++)
            //{
            //    Debug.Log("upTileValueSbyteList:" + upTileValueSbyteList[i]);
            //}
            exchangeUIPanel.SetActive(false);
            isChangeTile = false;

            var ObjList = HandTile.Instance.ObjList;
            foreach (var tile in ObjList)
            {
                tile.transform.position = new Vector3(tile.transform.position.x, handTileY, tile.transform.position.z);
            }
            
        });

        var upTileList = ChangeTileAlgorithm(HandTile.Instance.HandTileList);
        foreach (var upTile in upTileList)
        {
            if (upTile.SelfObj)
            {
                upTile.SelfObj.transform.position = new Vector3(upTile.SelfObj.transform.position.x, 0.35f, upTile.SelfObj.transform.position.z);
                finalUpList.Add(upTile);
            }
            else
            {
                HandTile.Instance.HoldTileObj.transform.position = new Vector3(HandTile.Instance.HoldTileObj.transform.position.x, 0.35f, HandTile.Instance.HoldTileObj.transform.position.z);
                finalUpList.Add(HandTile.Instance.HoldTile);
            }
            upTileValueSbyteList.Add(upTile.ValueSbyte);
        }

    }

    private IEnumerator ExchangeEnumerator(sbyte sec)
    {
        yield return new WaitForSeconds(sec);
        if (exchangeUIPanel.activeSelf)
        {
            exchangeUIPanel.SetActive(false);
            isChangeTile = false;
        }

        var ObjList = HandTile.Instance.ObjList;
        foreach (var tile in ObjList)
        {
            tile.transform.position = new Vector3(tile.transform.position.x, handTileY, tile.transform.position.z);
        }
    }


    public GameObject fixUIPanel;
    public Button fixConfirm;
    public ToggleGroup fixGroup;
    private TileType dingQueType;
    public void FixTiles(sbyte time_left)
    {
        ShowTime("还剩", time_left);
        StartCoroutine(FixEnumerator(time_left));
        fixUIPanel.SetActive(true);
        fixConfirm.onClick.RemoveAllListeners();
        fixConfirm.onClick.AddListener(delegate ()
        {
            var toggles = fixGroup.ActiveToggles();
            sbyte index = 0;
            foreach (var toggle in toggles)
            {
                if (toggle.isOn)
                {
                    switch (toggle.name)
                    {
                        case "ToggleWan":
                            index=0;
                            dingQueType = TileType.Wan;
                            break;
                        case "ToggleTiao":
                            index = 1;
                            dingQueType = TileType.Tiao;
                            break;
                        case "ToggleTong":
                            index=2;
                            dingQueType = TileType.Tong;
                            break;
                    }
                }
            }
            client.MahjongGamePlayer.GameFixTypeSend(index);
            fixUIPanel.SetActive(false);
            timerObj.transform.Find("timeDes").GetComponent<Text>().text = "请等待其他玩家";
        });
    }

    public GameObject fixObj_0;
    public GameObject fixObj_1;
    public GameObject fixObj_2;
    public GameObject fixObj_3;
    public void ShowFixUI(sbyte[] fixArray)
    {
        List<GameObject> fixObjList = new List<GameObject>();
        fixObjList.Add(fixObj_0);
        fixObjList.Add(fixObj_1);
        fixObjList.Add(fixObj_2);
        fixObjList.Add(fixObj_3);
        for (int i = 0; i < fixArray.Length; i++)
        {
            //Debug.LogError("fixArray:  "+i+":  "+fixArray[i].ToString());
            fixObjList[i].transform.Find(fixArray[i].ToString()).gameObject.SetActive(true);  
        }
        switch (fixArray[0])
        {
            case 0:
                dingQueType =TileType.Wan;
                break;
            case 1:
                dingQueType = TileType.Tiao;
                break;
            case 2:
                dingQueType = TileType.Tong;
                break;
        }
    }



    private IEnumerator FixEnumerator(sbyte sec)
    {
        yield return new WaitForSeconds(sec);
        if (fixUIPanel.activeSelf)
        {
            fixUIPanel.SetActive(false);
        }
    }

    public sbyte lastPlayedCard;
    public GameObject pengGangHuUIPanel;
    public Button pengConfirm;
    public Button gangConfirm;
    public Button huConfirm;
    public Button giveUp;
    public void PengGangHuDeal(int specialType)
    {
        //bool isGiveUp = GetBitValue(specialType,0);
        bool isPeng = GetBitValue(specialType,1);
        bool isMingGang = GetBitValue(specialType,2);
        bool isAnGang = GetBitValue(specialType,3);
        bool isPengGang = GetBitValue(specialType,4);
        bool isHu = GetBitValue(specialType,5);

        pengGangHuUIPanel.SetActive(true);
        if (isPeng)
        {
            pengConfirm.gameObject.SetActive(true);
            pengConfirm.onClick.RemoveAllListeners();
            pengConfirm.onClick.AddListener(delegate()
            {
                HandTile.Instance.DeleteTile(lastPlayedCard);
                HandTile.Instance.DeleteTile(lastPlayedCard);
                client.MahjongGamePlayer.GamePengGangHuSend(Convert.ToSByte(SpecialType.SPECIAL_TYPE_PENG), lastPlayedCard);
                pengConfirm.gameObject.SetActive(false);
                gangConfirm.gameObject.SetActive(false);
                huConfirm.gameObject.SetActive(false);
                giveUp.gameObject.SetActive(false);
                pengGangHuUIPanel.SetActive(false);

                GetTingAndShowHu();
            });
        }
        if (isMingGang|| isAnGang|| isPengGang)
        {
            gangConfirm.gameObject.SetActive(true);
            gangConfirm.onClick.RemoveAllListeners();
            gangConfirm.onClick.AddListener(delegate ()
            {
                
                if (isMingGang)
                {
                    HandTile.Instance.DeleteTile(lastPlayedCard);
                    HandTile.Instance.DeleteTile(lastPlayedCard);
                    HandTile.Instance.DeleteTile(lastPlayedCard);
                    client.MahjongGamePlayer.GamePengGangHuSend(Convert.ToSByte(SpecialType.SPECIAL_TYPE_MINGGANG), lastPlayedCard);
                }
                if (isAnGang)
                {
                    HandTile.Instance.DeleteTile(GetAnGangTile().ValueSbyte);
                    HandTile.Instance.DeleteTile(GetAnGangTile().ValueSbyte);
                    HandTile.Instance.DeleteTile(GetAnGangTile().ValueSbyte);
                    client.MahjongGamePlayer.GamePengGangHuSend(Convert.ToSByte(SpecialType.SPECIAL_TYPE_ANGANG), GetAnGangTile().ValueSbyte);
                }
                if (isPengGang)
                {
                    client.MahjongGamePlayer.GamePengGangHuSend(Convert.ToSByte(SpecialType.SPECIAL_TYPE_PENGGANG), HandTile.Instance.HoldTile.ValueSbyte);
                }
                pengConfirm.gameObject.SetActive(false);
                gangConfirm.gameObject.SetActive(false);
                huConfirm.gameObject.SetActive(false);
                giveUp.gameObject.SetActive(false);
                pengGangHuUIPanel.SetActive(false);
            });
        }
        if (isHu)
        {
            huConfirm.gameObject.SetActive(true);
            huConfirm.onClick.RemoveAllListeners();
            huConfirm.onClick.AddListener(delegate ()
            {
                isHued = true;
                client.MahjongGamePlayer.GamePengGangHuSend(Convert.ToSByte(SpecialType.SPECIAL_TYPE_HU), lastPlayedCard);
                pengConfirm.gameObject.SetActive(false);
                gangConfirm.gameObject.SetActive(false);
                huConfirm.gameObject.SetActive(false);
                giveUp.gameObject.SetActive(false);
                pengGangHuUIPanel.SetActive(false);
            });
        }
        if (isPeng|| isMingGang || isAnGang || isPengGang||isHu)
        {
            giveUp.gameObject.SetActive(true);
            giveUp.onClick.RemoveAllListeners();
            giveUp.onClick.AddListener(delegate ()
            {
                client.MahjongGamePlayer.GamePengGangHuSend(Convert.ToSByte(SpecialType.SPECIAL_TYPE_GIVEUP), lastPlayedCard);
                pengConfirm.gameObject.SetActive(false);
                gangConfirm.gameObject.SetActive(false);
                huConfirm.gameObject.SetActive(false);
                giveUp.gameObject.SetActive(false);
                pengGangHuUIPanel.SetActive(false);
            });
        }

    }

    public Tile GetAnGangTile()
    {
        List<Tile> list=new List<Tile>();
        list.AddRange(HandTile.Instance.HandTileList);
        list.Add(HandTile.Instance.HoldTile);

        Tile tileTmp =new Tile();
        for (int i = 0; i < list.Count; i++)
        {
            int count = 0;
            for (int j = 0; j < list.Count; j++)
            {
                if (list[i].ValueSbyte == list[j].ValueSbyte)
                {
                    count++;
                }
            }
            if (count==4)
            {
                tileTmp = list[i];
            }
        }
        return tileTmp;

    }

    public SpecialType SpecialTypeAnalysis(sbyte specialType)
    {
        bool isPeng = GetBitValue(specialType, 1);
        bool isMingGang = GetBitValue(specialType, 2);
        bool isAnGang = GetBitValue(specialType, 3);
        bool isPengGang = GetBitValue(specialType, 4);
        bool isHu = GetBitValue(specialType, 5);
        if (isPeng)
        {
            return SpecialType.SPECIAL_TYPE_PENG;
        }
        else if (isMingGang)
        {
            return SpecialType.SPECIAL_TYPE_MINGGANG;
        }
        else if (isAnGang)
        {
            return SpecialType.SPECIAL_TYPE_ANGANG;
        }
        else if (isPengGang)
        {
            return SpecialType.SPECIAL_TYPE_PENGGANG;
        }
        else
        {
            return SpecialType.SPECIAL_TYPE_HU;
        }

    }

    /// 根据Int类型的值，返回用1或0(对应True或Flase)填充的数组
    /// <remarks>从右侧开始向左索引(0~31)</remarks>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IEnumerable<bool> GetBitList(int value)
    {
        var list = new List<bool>(32);
        for (var i = 0; i <= 31; i++)
        {
            var val = 1 << i;
            list.Add((value & val) == val);
            Debug.LogError((value & val) == val);
        }
        return list;
    }
    /// <summary>
    /// 返回Int数据中某一位是否为1
    /// </summary>
    /// <param name="value"></param>
    /// <param name="index">32位数据的从右向左的偏移位索引(0~31)</param>
    /// <returns>true表示该位为1，false表示该位为0</returns>
    public static bool GetBitValue(int value, ushort index)
    {
        if (index > 31) throw new ArgumentOutOfRangeException("index"); //索引出错
        var val = 1 << index;
        return (value & val) == val;
    }


    public List<Tile> ChangeTileAlgorithm(List<Tile> getlist)
    {
        //WTiaoTong
        List<Tile> reslist=new List<Tile>();
        List<Tile> wanlist=new List<Tile>();
        List<Tile> tiaolist=new List<Tile>();
        List<Tile> tonglist=new List<Tile>();

        foreach (var tile in getlist)
        {
            switch (tile.FaceType)
            {
                case TileType.Wan:
                    wanlist.Add(tile);
                    break;
                case TileType.Tiao:
                    tiaolist.Add(tile);
                    break;
                case TileType.Tong:
                    tonglist.Add(tile);
                    break;
            }
        }

        int needCount=0;
        List<int> countlist = new List<int>();
        countlist.Add(wanlist.Count);
        countlist.Add(tiaolist.Count);
        countlist.Add(tonglist.Count);
        countlist.Sort();

        foreach (var count in countlist)
        {
            if (count >= 3)
            {
                needCount = count;
                break;
            }
        }

        List<Tile> needList=new List<Tile>();
        if (wanlist.Count== needCount)
        {
            needList = wanlist;
        }
        else if (tiaolist.Count == needCount)
        {
            needList = tiaolist;
        }
        else if (tonglist.Count == needCount)
        {
            needList = tonglist;
        }
        
        
        for (int i = needList.Count-1; i > needList.Count - 4; i--)
        {
            reslist.Add(needList[i]);
        }
        
        return reslist;
    }

    private Dictionary<sbyte,List<Tile>> canHuDic=new Dictionary<sbyte, List<Tile>>();
    public void GetTingAndShowHu()
    {
        List<Tile> totalList=new List<Tile>();
        totalList.AddRange(HandTile.Instance.HandTileList);
        if (HandTile.Instance.HoldTile.FaceValue!=0)
        {
            totalList.Add(HandTile.Instance.HoldTile);
        }

        List<sbyte> totalTileListValueToSrot = new List<sbyte>();

        foreach (var tile in totalList)
        {
            if (tile.ValueSbyte != 0)
            {
                totalTileListValueToSrot.Add(tile.ValueSbyte);
            }
        }

        totalTileListValueToSrot.Sort();

        totalList.Clear();
        foreach (var valSbyte in totalTileListValueToSrot)
        {
            Tile tile = new Tile();
            tile.ValueSbyte = valSbyte;
            totalList.Add(tile);
        }

        foreach (var tile in totalList)
        {
            List<Tile> totalList13 = new List<Tile>();
            totalList13.AddRange(totalList);
            totalList13.Remove(tile);
            var huTileList = TingAlgorithm(totalList13);
            if (huTileList.Count > 0)
            {
                Debug.LogError("--------------------------!!!!!!!!!!!!!!!!!!!!!!!!!!!扔掉即可听的牌 :" + tile.FaceValue);
                //于面板上显示胡牌信息
                for (int i = 0; i < huTileList.Count; i++)
                {
                    Debug.LogError("可胡的牌 huTileList[i].FaceValue:" + huTileList[i].FaceValue);
                }


                //扔掉即可听的牌，做标记
                foreach (var handeTile in HandTile.Instance.HandTileList)
                {
                    if (handeTile.ValueSbyte == tile.ValueSbyte)
                    {
                        handeTile.SelfObj.transform.Find("tingObj").gameObject.SetActive(true);
                    }
                }
                if (HandTile.Instance.HoldTile.ValueSbyte == tile.ValueSbyte)
                {
                    HandTile.Instance.HoldTile.SelfObj.transform.Find("tingObj").gameObject.SetActive(true);
                }
                if (!canHuDic.ContainsKey(tile.ValueSbyte))
                {
                    canHuDic.Add(tile.ValueSbyte, huTileList);
                }
            }


        }

    }

    private int removedDuiZiCount=0;
    private Tile removedDuiZiTile=new Tile();
    private List<Tile> canHuTilelist = new List<Tile>();
    public List<Tile> TingAlgorithm(List<Tile> getlist)
    {
        //WTiaoTong
        List<Tile> reslist = new List<Tile>();
        List<Tile> wanlist = new List<Tile>();
        List<Tile> tiaolist = new List<Tile>();
        List<Tile> tonglist = new List<Tile>();

        foreach (var tile in getlist)
        {
            switch (tile.FaceType)
            {
                case TileType.Wan:
                    wanlist.Add(tile);
                    break;
                case TileType.Tiao:
                    tiaolist.Add(tile);
                    break;
                case TileType.Tong:
                    tonglist.Add(tile);
                    break;
            }
        }

        //wanlist.Reverse();
        //tiaolist.Reverse();
        //tonglist.Reverse();
        //int needCount = 0;
        //List<int> countlist = new List<int>();
        //countlist.Add(wanlist.Count);
        //countlist.Add(tiaolist.Count);
        //countlist.Add(tonglist.Count);

        if (dingQueType==TileType.Wan && wanlist.Count>0 || dingQueType == TileType.Tiao && tiaolist.Count > 0|| dingQueType == TileType.Tong && tonglist.Count > 0)
        {
            //null
            return reslist;
        }

        //非7对,普通胡,先刻后对
        List<Tile> wanlist1 = new List<Tile>();
        List<Tile> tiaolist1 = new List<Tile>();
        List<Tile> tonglist1 = new List<Tile>();
        wanlist1.AddRange(wanlist);
        tiaolist1.AddRange(tiaolist);
        tonglist1.AddRange(tonglist);

        Remove3LianDuiSpecial(wanlist1);
        Remove3LianDuiSpecial(tiaolist1);
        Remove3LianDuiSpecial(tonglist1);

        RemoveShunKe(wanlist1);
        RemoveShunKe(tiaolist1);
        RemoveShunKe(tonglist1);

        removedDuiZiCount = 0;
        RemoveDuiZi(wanlist1);
        RemoveDuiZi(tiaolist1);
        RemoveDuiZi(tonglist1);


        List<Tile> remainList1 =new List<Tile>();
        remainList1.AddRange(wanlist1);
        remainList1.AddRange(tiaolist1);
        remainList1.AddRange(tonglist1);

        reslist.AddRange(TingAlgorithmSec(remainList1));



        //非7对,普通胡,先对后刻
        List<Tile> wanlist2 = new List<Tile>();
        List<Tile> tiaolist2 = new List<Tile>();
        List<Tile> tonglist2 = new List<Tile>();
        wanlist2.AddRange(wanlist);
        tiaolist2.AddRange(tiaolist);
        tonglist2.AddRange(tonglist);

        Remove3LianDuiSpecial(wanlist2);
        Remove3LianDuiSpecial(tiaolist2);
        Remove3LianDuiSpecial(tonglist2);

        removedDuiZiCount = 0;
        RemoveDuiZi(wanlist2);
        RemoveDuiZi(tiaolist2);
        RemoveDuiZi(tonglist2);

        RemoveShunKe(wanlist2);
        RemoveShunKe(tiaolist2);
        RemoveShunKe(tonglist2);

        List<Tile> remainList2 = new List<Tile>();
        remainList2.AddRange(wanlist2);
        remainList2.AddRange(tiaolist2);
        remainList2.AddRange(tonglist2);

        reslist.AddRange(TingAlgorithmSec(remainList2));


        //剔重
        List<Tile> reslistWithDt=new List<Tile>();

        //foreach (var tile in reslist)
        //{
        //    //foreach (var val in reslistWithDt)
        //    //{
        //    //    if (val.ValueSbyte!= tile.ValueSbyte)
        //    //    {
        //    //        reslistWithDt.Add(tile);
        //    //    }
        //    //}
        //    if (!reslistWithDt.Contains(tile))
        //    {
        //        reslistWithDt.Add(tile);
        //    }
        //}

        foreach (var tileToAdd in reslist)
        {
            bool isExist = false;
            foreach (var tileAdded in reslistWithDt)
            {
                if (tileAdded.FaceType == tileToAdd.FaceType && tileAdded.FaceValue == tileToAdd.FaceValue)
                {
                    isExist = true;
                }
            }
            if (!isExist)
            {
                reslistWithDt.Add(tileToAdd);
            }

        }


        return reslistWithDt;
        //if (remainList.Count>2 )
        //{
        //    //null
        //    return reslist;
        //}
        //else if(remainList.Count == 2 && remainList[0].FaceType != remainList[1].FaceType)
        //{
        //    //null
        //    return reslist;
        //}
        //else if (remainList.Count == 2 && remainList[0].FaceType == remainList[1].FaceType)
        //{
        //    //123,11,123,123,12
        //    if (remainList[0].FaceValue == remainList[1].FaceValue-1)
        //    {
        //        if (remainList[0].FaceValue>1)
        //        {
        //            Tile tile = new Tile();
        //            tile.FaceType = remainList[0].FaceType;
        //            tile.FaceValue = remainList[0].FaceValue - 1;
        //            reslist.Add(tile);
        //        }
        //        if (remainList[1].FaceValue < 9)
        //        {
        //            Tile tile = new Tile();
        //            tile.FaceType = remainList[0].FaceType;
        //            tile.FaceValue = remainList[0].FaceValue + 2 ;
        //            reslist.Add(tile);
        //        }
        //        canHuTilelist.AddRange(remainList);
        //        return reslist;
        //    }
        //    //123,11,123,123,24
        //    else if (remainList[0].FaceValue == remainList[1].FaceValue - 2)
        //    {
        //        Tile tile = new Tile();
        //        tile.FaceType = remainList[0].FaceType;
        //        tile.FaceValue = remainList[0].FaceValue + 1;
        //        reslist.Add(tile);
        //        canHuTilelist.AddRange(remainList);
        //        return reslist;
        //    }
        //    //123,11,123,123,22
        //    else if (remainList[0].FaceValue == remainList[1].FaceValue)
        //    {
        //        reslist.Add(remainList[0]);
        //        reslist.Add(removedDuiZiTile);
        //        canHuTilelist.AddRange(remainList);
        //        return reslist;
        //    }
        //    else
        //    {
        //        //null
        //        return reslist;
        //    }
        //}
        //else
        //{
        //    //123,123,123,123,1
        //    reslist.AddRange(remainList);
        //    canHuTilelist.AddRange(remainList);
        //    return reslist;
        //}


    }

    public List<Tile> TingAlgorithmSec(List<Tile> remainList)
    {
        List<Tile>  reslist =new List<Tile>();
        if (remainList.Count > 2)
        {
            //null
            return reslist;
        }
        else if (remainList.Count == 2 && remainList[0].FaceType != remainList[1].FaceType)
        {
            //null
            return reslist;
        }
        else if (remainList.Count == 2 && remainList[0].FaceType == remainList[1].FaceType)
        {
            //123,11,123,123,12
            if (remainList[0].FaceValue == remainList[1].FaceValue - 1)
            {
                if (remainList[0].FaceValue > 1)
                {
                    Tile tile = new Tile();
                    tile.FaceType = remainList[0].FaceType;
                    tile.FaceValue = remainList[0].FaceValue - 1;
                    reslist.Add(tile);
                }
                if (remainList[1].FaceValue < 9)
                {
                    Tile tile = new Tile();
                    tile.FaceType = remainList[0].FaceType;
                    tile.FaceValue = remainList[0].FaceValue + 2;
                    reslist.Add(tile);
                }
                canHuTilelist.AddRange(remainList);
                return reslist;
            }
            //123,11,123,123,24
            else if (remainList[0].FaceValue == remainList[1].FaceValue - 2)
            {
                Tile tile = new Tile();
                tile.FaceType = remainList[0].FaceType;
                tile.FaceValue = remainList[0].FaceValue + 1;
                reslist.Add(tile);
                canHuTilelist.AddRange(remainList);
                return reslist;
            }
            //123,11,123,123,22
            else if (remainList[0].FaceValue == remainList[1].FaceValue)
            {
                reslist.Add(remainList[0]);
                reslist.Add(removedDuiZiTile);
                canHuTilelist.AddRange(remainList);
                return reslist;
            }
            //***123,123,222,1234做特殊处理
            //else if (remainList[0].FaceValue == remainList[1].FaceValue)
            //{
            //    reslist.Add(remainList[0]);
            //    reslist.Add(removedDuiZiTile);
            //    canHuTilelist.AddRange(remainList);
            //    return reslist;
            //}
            else
            {
                //null
                return reslist;
            }
        }
        else
        {
            //123,123,123,123,1
            reslist.AddRange(remainList);
            canHuTilelist.AddRange(remainList);
            return reslist;
        }
    }


    private void RemoveShunKe(List<Tile> list)
    {
        List<int> toBeDeleteIndexList=new List<int>();

        //for (int k = 0; k < 5; k++)
        //{
            if (list.Count >= 3)
            {
                for (int i = 0; i < list.Count - 2; i++)
                {
                    //if ((list[0].FaceValue == list[0 + 1].FaceValue - 1 && list[0].FaceValue == list[0 + 2].FaceValue - 2) || (list[0].FaceValue == list[0 + 1].FaceValue && list[0].FaceValue == list[0 + 2].FaceValue))
                    //{
                    //    list.RemoveRange(0, 3);
                    //}
                    if ((list[i].FaceValue == list[i + 1].FaceValue - 1 && list[i].FaceValue == list[i + 2].FaceValue - 2) || (list[i].FaceValue == list[i + 1].FaceValue && list[i].FaceValue == list[i + 2].FaceValue))
                    {

                        toBeDeleteIndexList.Add(i);
                        toBeDeleteIndexList.Add(i+1);
                        toBeDeleteIndexList.Add(i+2);
                        i = i + 2;
                        //list.RemoveRange(i, 3);
                    }
                    
                }
            }
        //}
        for (int j = toBeDeleteIndexList.Count - 1; j >= 0; j--)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (i == toBeDeleteIndexList[j])
                {
                    list.RemoveAt(toBeDeleteIndexList[j]);
                }
            }
        }

        
        
    }

    private void RemoveDuiZi(List<Tile> list)
    {
        if (list.Count >= 2 && removedDuiZiCount == 0)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i].FaceValue == list[i + 1].FaceValue)
                {
                    removedDuiZiTile = list[i];
                    list.RemoveRange(i,2);
                    removedDuiZiCount++;
                    break;
                }
            }
        }
    }

    //private int removed3LianDuiCount;
    private void Remove3LianDuiSpecial(List<Tile> list)
    {
        for (int j = 0; j < 2; j++)
        {
            if (list.Count >= 6)
            {
                for (int i = 0; i < list.Count - 5; i++)
                {
                    if (list[i].FaceValue == list[i + 1].FaceValue && list[i+2].FaceValue == list[i + 3].FaceValue && list[i + 4].FaceValue == list[i + 5].FaceValue&& list[i].FaceValue == list[i + 2].FaceValue-1 && list[i].FaceValue == list[i + 4].FaceValue - 2 ||
                        list[i].FaceValue == list[i + 1].FaceValue-1 && list[i + 1].FaceValue == list[i + 2].FaceValue && list[i + 2].FaceValue == list[i + 3].FaceValue-1 && list[i+3].FaceValue == list[i + 4].FaceValue  && list[i+4].FaceValue == list[i + 5].FaceValue - 1)
                    {
                        list.RemoveRange(i, 6);
                    }
                }
            }
        }
    }

    public GameObject canHuTileContainer;
    public List<GameObject> canHuTileObjList = new List<GameObject>();
    public void ShowCanHuTile(List<Tile> canHuTileValList)
    {
        canHuTileObjList.Clear();
        foreach (Transform child in canHuTileContainer.transform)
        {
            canHuTileObjList.Add(child.gameObject);
        }
        foreach (Transform child in canHuTileContainer.transform)
        {
            foreach (Transform childImage in child.transform)
            {
                childImage.gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < canHuTileValList.Count; i++)
        {
            string typeName;
            if (canHuTileValList[i].FaceType == TileType.Wan)
            {
                typeName = "W";
            }
            else if (canHuTileValList[i].FaceType == TileType.Tong)
            {
                typeName = "T";
            }
            else
            {
                typeName = "S";
            }
            canHuTileObjList[i].transform.Find("MJ_" + typeName + canHuTileValList[i].FaceValue).gameObject.SetActive(true);
        }
        
    }

    public void ClearCanHuTileList()
    {
        foreach (Transform child in canHuTileContainer.transform)
        {
            foreach (Transform childImage in child.transform)
            {
                childImage.gameObject.SetActive(false);
            }
        }
    }

    public void ClearAllData()
    {
        deskDic.Clear();
        deskList_0.Clear();
        deskList_1.Clear();
        deskList_2.Clear();
        deskList_3.Clear();

        isYourTurn = false;
        isPlayedHoldTile = false;
        isHued = false;

        riverDic_0.Clear();
        riverDic_1.Clear();
        riverDic_2.Clear();
        riverDic_3.Clear();

        pengGangHuCount = 0;
        huList_0.Clear();
        huList_1.Clear();
        huList_2.Clear();
        huList_3.Clear();

        lastDeskAddIndex = 0;
        finalUpList.Clear();
        lastPlayedCard = 0;
        canHuDic.Clear();
        removedDuiZiCount = 0;
        canHuTilelist.Clear();
        remainTileCount = 108;

        wallTileList.Clear();
    }


    #region ForTest

    public Button testBtn;

    public void MakeTest()
    {
        
        //WaitForPlayersSpecialShow();
        testList.Clear();
        AddDefTile(TileType.Wan, 1);
        AddDefTile(TileType.Wan, 2);
        AddDefTile(TileType.Wan, 3);

        AddDefTile(TileType.Wan, 6);
        AddDefTile(TileType.Wan, 7);
        AddDefTile(TileType.Wan, 8);
        AddDefTile(TileType.Wan, 9);

        AddDefTile(TileType.Tiao, 1);
        //AddDefTile(TileType.Tiao, 2);
        //AddDefTile(TileType.Tiao, 3);

        //AddDefTile(TileType.Tiao, 5);
        //AddDefTile(TileType.Tiao, 5);

        //AddDefTile(TileType.Tiao, 6);
        //AddDefTile(TileType.Tiao, 7);
        //AddDefTile(TileType.Tiao, 7);

        dingQueType = TileType.Tong;
        foreach (var tile in testList)
        {
            List<Tile> totalList13 = new List<Tile>();
            totalList13.AddRange(testList);
            totalList13.Remove(tile);
            var huTileList = TingAlgorithm(totalList13);
            if (huTileList.Count > 0)
            {
                Debug.LogError("!!!!!!!!!!!!!!!!!!!!!!!!!!!扔掉即可听的牌 :" + tile.FaceValue);
                //于面板上显示胡牌信息
                for (int i = 0; i < huTileList.Count; i++)
                {
                    Debug.LogError("可胡的牌 huTileList[i].FaceValue:" + huTileList[i].FaceValue);
                }
            }
        }


        //暗杠Test
        //List<Tile> list = new List<Tile>();
        //list.AddRange(testList);

        //Tile tileTmp = new Tile();
        //for (int i = 0; i < list.Count; i++)
        //{
        //    int count = 0;
        //    for (int j = 0; j < list.Count; j++)
        //    {
        //        if (list[i].FaceValue == list[j].FaceValue && list[i].FaceType == list[j].FaceType)
        //        {
        //            count++;
        //        }
        //    }
        //    if (count == 4)
        //    {
        //        tileTmp = list[i];
        //    }
        //}
        //Debug.Log("tileTmp.FaceValue: "+tileTmp.FaceValue);

        //碰杠
        //Tile pgTile=new Tile();
        //pgTile.ValueSbyte = 9;
        //List<Tile> pgList=new List<Tile>();
        //pgList.Add(pgTile);
        //pgList.Add(pgTile);
        //pgList.Add(pgTile);
        //riverDic_0.Add(0, pgList);
        //HandTile.Instance.HoldTile.ValueSbyte = 9;
        //RefreshRiverTileData(0, 9, SpecialType.SPECIAL_TYPE_PENGGANG, 0);


    }
    private List<Tile> testList = new List<Tile>();
    private void AddDefTile(TileType tileType, int tileValue)
    {
        Tile tile = new Tile();
        tile.FaceType = tileType;
        tile.FaceValue = tileValue;
        testList.Add(tile);
    }

    #endregion


}

public enum TileType
{
    Wan,
    Tong,
    Tiao
}

public enum TileState
{
    InHand,
    Peng,
    Gang,
    Hu,
    Dropped
}
public class Tile
{
    private sbyte valueSbyte;
    public sbyte ValueSbyte
    {
        get { return valueSbyte; }
        set
        {
            valueSbyte = value;
            FaceValue = Convert.ToInt32(Convert.ToString(valueSbyte, 2).PadLeft(8, '0').Substring(4, 4), 2);
            string typeStr = Convert.ToString(valueSbyte, 2).PadLeft(8, '0').Substring(0, 4);
            string typeName;
            if (typeStr == "0000")
            {
                typeName = "W";
                FaceType = TileType.Wan;
            }
            else if (typeStr == "0010")
            {
                typeName = "T";
                FaceType = TileType.Tong;
            }
            else
            {
                typeName = "S";
                FaceType = TileType.Tiao;
            }

            if (SelfObj)
            {
                foreach (Transform child in SelfObj.transform)
                {
                    child.gameObject.SetActive(false);
                }
                if (FaceValue == 0)
                {
                    foreach (Transform child in SelfObj.transform)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
                else
                {
                    SelfObj.transform.Find("MJ_" + typeName + FaceValue).gameObject.SetActive(true);
                }
            }
            
        }
    }

    public GameObject SelfObj { get; set; }
    
    public int FaceValue { get; set; }

    public TileType FaceType { get; set; }

    public bool IsSelected { get; set; }
}
