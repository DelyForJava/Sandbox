using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bean.Hall;
using LitJson;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class TaskData
{
    public int task_id;
    public int type;
    public string task_name;
    public string task_desc;
    public int task_target;
    public int task_param;
    public int add_degree;
    public int add_exp;
    public string image_name;
}

public class TaskDataManager : SingletonBehaviour<TaskDataManager> {

    private List<TaskData> taskDataList = new List<TaskData>();
    private List<GameObject> taskObjInstantiateList = new List<GameObject>();

    public Button dailyOpenBtn1;
    public Button dailyOpenBtn2;
    public Button dailyOpenBtn3;
    public Button dailyOpenBtn4;
    public Button weeklyOpenBtn1;
    public Button weeklyOpenBtn2;

    public Text dailyDegreeText;
    public Text weeklyDegreeText;

    public Slider dailyDegreeSlider;

    // Use this for initialization
    void Start()
    {

        //ReqData();
    }

    private void InitData()
    {
        string json = "{'items':" +
                      "[{\"task_id\":\"1\",\"type\":\"1\",\"task_name\":\" 签到转盘\",\"task_desc\":\"获得一次转盘奖励\",\"task_target\":\"1\",\"task_param\":\"0\",\"add_degree\":\"10\",\"add_exp\":\"10\",\"image_name\":\"task_pic_sign\"},{\"task_id\":\"2\",\"type\":\"2\",\"task_name\":\"游戏分享\",\"task_desc\":\"分享一次游戏\",\"task_target\":\"1\",\"task_param\":\"0\",\"add_degree\":\"10\",\"add_exp\":\"10\",\"image_name\":\"task_pic_share\"},{\"task_id\":\"3\",\"type\":\"3\",\"task_name\":\"体验1款游戏\",\"task_desc\":\"玩任意1款游戏\",\"task_target\":\"1\",\"task_param\":\"0\",\"add_degree\":\"10\",\"add_exp\":\"30\",\"image_name\":\"task_pic_game\"},{\"task_id\":\"4\",\"type\":\"4\",\"task_name\":\"游戏30分钟\",\"task_desc\":\"任意游戏内玩30分钟\",\"task_target\":\"1800\",\"task_param\":\"0\",\"add_degree\":\"10\",\"add_exp\":\"50\",\"image_name\":\"task_pic_fish\"},{\"task_id\":\"5\",\"type\":\"5\",\"task_name\":\"玩游戏得金豆\",\"task_desc\":\"任意游戏内获得2金豆\",\"task_target\":\"2\",\"task_param\":\"0\",\"add_degree\":\"20\",\"add_exp\":\"100\",\"image_name\":\"task_pic_sign\"},{\"task_id\":\"6\",\"type\":\"6\",\"task_name\":\"玩游戏赢金币\",\"task_desc\":\"游戏内单次赢20000积分5次\",\"task_target\":\"5\",\"task_param\":\"20000\",\"add_degree\":\"20\",\"add_exp\":\"100\",\"image_name\":\"task_pic_sign\"},{\"task_id\":\"7\",\"type\":\"7\",\"task_name\":\"玩游戏累计赢金币\",\"task_desc\":\"游戏累计赢50万金币\",\"task_target\":\"500000\",\"task_param\":\"0\",\"add_degree\":\"20\",\"add_exp\":\"100\",\"image_name\":\"task_pic_sign\"},{\"task_id\":\"8\",\"type\":\"8\",\"task_name\":\"获得礼品\",\"task_desc\":\"在大厅中获得一次礼品\",\"task_target\":\"1\",\"task_param\":\"0\",\"add_degree\":\"20\",\"add_exp\":\"100\",\"image_name\":\"task_pic_sign\"},{\"task_id\":\"9\",\"type\":\"9\",\"task_name\":\"任意充值\",\"task_desc\":\"充值任意金额一次\",\"task_target\":\"1\",\"task_param\":\"0\",\"add_degree\":\"20\",\"add_exp\":\"200\",\"image_name\":\"task_pic_sign\"}]}";

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
            TaskData taskData = new TaskData();
            taskData.task_id = Convert.ToInt32((string)jdItem["task_id"]);
            taskData.type = Convert.ToInt32((string)jdItem["type"]);
            taskData.task_name = (string)jdItem["task_name"];
            taskData.task_desc = (string)jdItem["task_desc"];
            taskData.task_target = Convert.ToInt32((string)jdItem["task_target"]);
            taskData.task_param = Convert.ToInt32((string)jdItem["task_param"]);
            taskData.add_degree = Convert.ToInt32((string)jdItem["add_degree"]);
            taskData.add_exp = Convert.ToInt32((string)jdItem["add_exp"]);
            taskData.image_name = (string)jdItem["image_name"];

            taskDataList.Add(taskData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //goldNum.text = UIOperation.playerLobbyInfo.llGameCoin.ToString();
        //diamondNum.text = UIOperation.playerLobbyInfo.llDiamondNum.ToString();
    }

    public void OpenTask()
    {
        InitData();
        ReqData();
    }

    //public void ClosePackage()
    //{
    //    foreach (var model in modelObjInstantiateList)
    //    {
    //        DestroyImmediate(model);
    //    }
    //    foreach (var icon in itemObjInstantiateList)
    //    {
    //        DestroyImmediate(icon);
    //    }
    //    itemDataList.Clear();
    //    itemObjInstantiateList.Clear();
    //    modelObjInstantiateList.Clear();
    //    selectedIndex = 0;
    //    preBtn.onClick.RemoveAllListeners();
    //    nextBtn.onClick.RemoveAllListeners();
    //}

    private bool isInited = false;
    private void ReqData()
    {
        if (!isInited)
        {
            InitDataToPanel();
        }
        var client = GameClient.Instance;
        
        client.MahjongGamePlayer.TaskInfoReqDef();

       
    }

    private GameObject taskPrefabObj;
    public GameObject taskContextObj;
    

    public void InitDataToPanel()
    {
        var client = GameClient.Instance;
        taskPrefabObj = ResourcesManager.Load<GameObject>("Assets/Art/Common/taskGrid.prefab");
        foreach (var localData in taskDataList)
        {
            var itemTexture2D = ResourcesManager.Load<Texture2D>("Assets/Art/Common/" + localData.image_name + ".png");
            Sprite itemSprite = Sprite.Create(itemTexture2D, new Rect(0, 0, itemTexture2D.width, itemTexture2D.height), new Vector2(0.5f, 0.5f));
            taskPrefabObj.transform.Find("icon").GetComponent<Image>().sprite = itemSprite;
            taskPrefabObj.transform.Find("icon").GetComponent<Image>().SetNativeSize();
            taskPrefabObj.transform.Find("title").GetComponent<Text>().text = localData.task_name;
            taskPrefabObj.transform.Find("desc").GetComponent<Text>().text = localData.task_desc;
            taskPrefabObj.transform.Find("progress/val").GetComponent<Text>().text =  "0/" + localData.task_target;
            taskPrefabObj.transform.Find("act/Text").GetComponent<Text>().text = "x" + localData.add_degree;
            taskPrefabObj.transform.Find("exp/Text").GetComponent<Text>().text = "x" + localData.add_exp;

            var getBtn = taskPrefabObj.transform.Find("status/getBtn").gameObject;
            var goBtn = taskPrefabObj.transform.Find("status/goBtn").gameObject;

            

            //未完成
            getBtn.SetActive(false);
            goBtn.SetActive(true);
            


            taskPrefabObj.transform.SetAsFirstSibling();
            taskPrefabObj.name = localData.image_name;

            var itemObjInstantiate = Instantiate(taskPrefabObj, taskContextObj.transform);
            itemObjInstantiate.name = localData.task_id.ToString();

            itemObjInstantiate.transform.Find("status/getBtn").GetComponent<Button>().onClick.AddListener(delegate ()
            {
                client.MahjongGamePlayer.TaskSubmitReqDef(localData.task_id);
            });

            taskObjInstantiateList.Add(itemObjInstantiate);

            isInited = true;
        }


        weeklyOpenBtn1.onClick.AddListener(delegate ()
        {
            client.MahjongGamePlayer.TaskDegreeGetReqDef(2, 1);
        });
        weeklyOpenBtn2.onClick.AddListener(delegate ()
        {
            client.MahjongGamePlayer.TaskDegreeGetReqDef(2, 2);
        });

        dailyOpenBtn1.onClick.AddListener(delegate ()
        {
            client.MahjongGamePlayer.TaskDegreeGetReqDef(1, 1);
        });
        dailyOpenBtn2.onClick.AddListener(delegate ()
        {
            client.MahjongGamePlayer.TaskDegreeGetReqDef(1, 2);
        });
        dailyOpenBtn3.onClick.AddListener(delegate ()
        {
            client.MahjongGamePlayer.TaskDegreeGetReqDef(1, 3);
        });
        dailyOpenBtn4.onClick.AddListener(delegate ()
        {
            client.MahjongGamePlayer.TaskDegreeGetReqDef(1, 4);
        });
    }


    public void RefreshDataToPanel(List<BaseMessage.TaskInfo> taskInfoList, int iDailyDegree, int iWeeklyDegree, short sDailyFlag, short sWeeklyFlag)
    {
        foreach (var itemObjInstantiate in taskObjInstantiateList)
        {
            foreach (var taskData in taskInfoList)
            {
                if (taskData.iTaskID.ToString() == itemObjInstantiate.name)
                {
                    var dataList = taskDataList.Where(q => q.task_id == taskData.iTaskID)
                        .Select(q => q)
                        .ToList();
                    itemObjInstantiate.transform.Find("progress/val").GetComponent<Text>().text = taskData.iTaskProgress + "/" + dataList[0].task_target;
                    var getBtn = itemObjInstantiate.transform.Find("status/getBtn").gameObject;
                    var goBtn = itemObjInstantiate.transform.Find("status/goBtn").gameObject;
                    if (taskData.iTaskProgress >= dataList[0].task_target)
                    {
                        if (taskData.cIsGet == 0)
                        {
                            //已完成未领取
                            getBtn.SetActive(true);
                            goBtn.SetActive(false);
                        }
                        else
                        {
                            //已完成已领取
                            getBtn.SetActive(false);
                            goBtn.SetActive(false);
                        }
                    }
                    else
                    {
                        //未完成
                        getBtn.SetActive(false);
                        goBtn.SetActive(true);
                    }
                }
            }
        }

        //更新活跃度
        dailyDegreeText.text = iDailyDegree.ToString();
        weeklyDegreeText.text = iWeeklyDegree.ToString();
        
        dailyDegreeSlider.value = (float)iDailyDegree/120;

        List<int> dailyFlagList = new List<int>();
        List<int> weekliFlagList = new List<int>();
        dailyFlagList.Clear();
        weekliFlagList.Clear();
        
        for (int i = 1; i < 5; i++)
        {
            dailyFlagList.Add(GetFlag(sDailyFlag,i));
        }
        for (int i = 1; i < 3; i++)
        {
            weekliFlagList.Add(GetFlag(sWeeklyFlag,i));
        }

        if (weekliFlagList[0]==0)
        {
            if (iWeeklyDegree >= 500)
            {
                weeklyOpenBtn1.transform.parent.Find("boxClosed").gameObject.SetActive(true);
                weeklyOpenBtn1.transform.parent.Find("boxOpened").gameObject.SetActive(false);
                weeklyOpenBtn1.transform.parent.Find("boxClosed/light").gameObject.SetActive(true);
            }
            else
            {
                weeklyOpenBtn1.transform.parent.Find("boxClosed").gameObject.SetActive(true);
                weeklyOpenBtn1.transform.parent.Find("boxOpened").gameObject.SetActive(false);
                weeklyOpenBtn1.transform.parent.Find("boxClosed/light").gameObject.SetActive(false);
            }
        }
        else
        {
            weeklyOpenBtn1.transform.parent.Find("boxClosed").gameObject.SetActive(false);
            weeklyOpenBtn1.transform.parent.Find("boxOpened").gameObject.SetActive(true);
        }
        if (weekliFlagList[1] == 0)
        {
            if (iWeeklyDegree >= 800)
            {
                weeklyOpenBtn2.transform.parent.Find("boxClosed").gameObject.SetActive(true);
                weeklyOpenBtn2.transform.parent.Find("boxOpened").gameObject.SetActive(false);
                weeklyOpenBtn2.transform.parent.Find("boxClosed/light").gameObject.SetActive(true);
            }
            else
            {
                weeklyOpenBtn2.transform.parent.Find("boxClosed").gameObject.SetActive(true);
                weeklyOpenBtn2.transform.parent.Find("boxOpened").gameObject.SetActive(false);
                weeklyOpenBtn2.transform.parent.Find("boxClosed/light").gameObject.SetActive(false);
            }
        }
        else
        {
            weeklyOpenBtn2.transform.parent.Find("boxClosed").gameObject.SetActive(false);
            weeklyOpenBtn2.transform.parent.Find("boxOpened").gameObject.SetActive(true);
        }
        
        if (dailyFlagList[0] == 0)
        {
            if (iDailyDegree >= 30)
            {
                dailyOpenBtn1.transform.Find("closed").gameObject.SetActive(false);
                dailyOpenBtn1.transform.Find("opened").gameObject.SetActive(false);
                dailyOpenBtn1.transform.Find("activated").gameObject.SetActive(true);
                dailyDegreeSlider.transform.Find("activateC1").gameObject.SetActive(true);
            }
            else
            {
                dailyOpenBtn1.transform.Find("closed").gameObject.SetActive(true);
                dailyOpenBtn1.transform.Find("opened").gameObject.SetActive(false);
                dailyOpenBtn1.transform.Find("activated").gameObject.SetActive(false);
                dailyDegreeSlider.transform.Find("activateC1").gameObject.SetActive(false);
            }
        }
        else
        {
             dailyOpenBtn1.transform.Find("closed").gameObject.SetActive(false);
             dailyOpenBtn1.transform.Find("opened").gameObject.SetActive(true);
             dailyOpenBtn1.transform.Find("activated").gameObject.SetActive(false);
             dailyDegreeSlider.transform.Find("activateC1").gameObject.SetActive(true);
        }

        if (dailyFlagList[1] == 0)
        {
            if (iDailyDegree >= 60)
            {
                dailyOpenBtn2.transform.Find("closed").gameObject.SetActive(false);
                dailyOpenBtn2.transform.Find("opened").gameObject.SetActive(false);
                dailyOpenBtn2.transform.Find("activated").gameObject.SetActive(true);
                dailyDegreeSlider.transform.Find("activateC2").gameObject.SetActive(true);
            }
            else
            {
                dailyOpenBtn2.transform.Find("closed").gameObject.SetActive(true);
                dailyOpenBtn2.transform.Find("opened").gameObject.SetActive(false);
                dailyOpenBtn2.transform.Find("activated").gameObject.SetActive(false);
                dailyDegreeSlider.transform.Find("activateC2").gameObject.SetActive(false);
            }
        }
        else
        {
            dailyOpenBtn2.transform.Find("closed").gameObject.SetActive(false);
            dailyOpenBtn2.transform.Find("opened").gameObject.SetActive(true);
            dailyOpenBtn2.transform.Find("activated").gameObject.SetActive(false);
            dailyDegreeSlider.transform.Find("activateC2").gameObject.SetActive(true);
        }

        if (dailyFlagList[2] == 0)
        {
            if (iDailyDegree >= 90)
            {
                dailyOpenBtn3.transform.Find("closed").gameObject.SetActive(false);
                dailyOpenBtn3.transform.Find("opened").gameObject.SetActive(false);
                dailyOpenBtn3.transform.Find("activated").gameObject.SetActive(true);
                dailyDegreeSlider.transform.Find("activateC3").gameObject.SetActive(true);
            }
            else
            {
                dailyOpenBtn3.transform.Find("closed").gameObject.SetActive(true);
                dailyOpenBtn3.transform.Find("opened").gameObject.SetActive(false);
                dailyOpenBtn3.transform.Find("activated").gameObject.SetActive(false);
                dailyDegreeSlider.transform.Find("activateC3").gameObject.SetActive(false);
            }
        }
        else
        {
            dailyOpenBtn3.transform.Find("closed").gameObject.SetActive(false);
            dailyOpenBtn3.transform.Find("opened").gameObject.SetActive(true);
            dailyOpenBtn3.transform.Find("activated").gameObject.SetActive(false);
            dailyDegreeSlider.transform.Find("activateC3").gameObject.SetActive(true);
        }

        if (dailyFlagList[3] == 0)
        {
            if (iDailyDegree >= 120)
            {
                dailyOpenBtn4.transform.Find("closed").gameObject.SetActive(false);
                dailyOpenBtn4.transform.Find("opened").gameObject.SetActive(false);
                dailyOpenBtn4.transform.Find("activated").gameObject.SetActive(true);
                dailyDegreeSlider.transform.Find("activateC4").gameObject.SetActive(true);
            }
            else
            {
                dailyOpenBtn4.transform.Find("closed").gameObject.SetActive(true);
                dailyOpenBtn4.transform.Find("opened").gameObject.SetActive(false);
                dailyOpenBtn4.transform.Find("activated").gameObject.SetActive(false);
                dailyDegreeSlider.transform.Find("activateC4").gameObject.SetActive(false);
            }
        }
        else
        {
            dailyOpenBtn4.transform.Find("closed").gameObject.SetActive(false);
            dailyOpenBtn4.transform.Find("opened").gameObject.SetActive(true);
            dailyOpenBtn4.transform.Find("activated").gameObject.SetActive(false);
            dailyDegreeSlider.transform.Find("activateC4").gameObject.SetActive(true);
        }
    }


    //public void Refresh


    public int GetFlag(short allFlags, int index)
    {
        //allFlags = 0x0001;
        //allFlags = 0x0003;
        //allFlags = 0x0007;
        //allFlags = 0x000f;

        var log = string.Format("{0:00}", allFlags.ToString("X"));
        int ret = 0;
        ret = ( (allFlags >> (index - 1) & 0x0001) == 1)? 1 : 0;

        Debug.Log(log+"ret:" +ret);

        return ret;
    }

}
