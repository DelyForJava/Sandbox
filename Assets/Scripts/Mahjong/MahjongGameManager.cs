using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MahjongGameManager : SingletonBehaviour<MahjongGameManager> {

    public Button exitBtn;
    public Button reconnectBtn;

    // Use this for initialization
    void Start () {
        var client = GameClient.Instance;
        exitBtn.onClick.AddListener(delegate
        {
            SceneManager.LoadScene(0);
        });
        reconnectBtn.onClick.AddListener(delegate
        {
            client.MahjongGamePlayer.ReDispenseSend();
        });

        //sbyte[] tmpSbyte = new[] {(sbyte)1, (sbyte)2, (sbyte)3, (sbyte)11, (sbyte)12, (sbyte)13 , (sbyte)21, (sbyte)22, (sbyte)23 , (sbyte)1, (sbyte)2, (sbyte)3, (sbyte)3 };
        //GetHandCardData(tmpSbyte);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private GameObject tileObj;
    public GameObject handTilesContainer;
    private List<GameObject> handList =new List<GameObject>();


    public void SetContainerData(GameObject fatherObj, List<GameObject> list)
    {
        foreach (Transform child in fatherObj.transform)
        {
            //Debug.Log("所有该脚本的物体下的子物体名称:" + child.name);
            list.Add(child.gameObject);
        }
    }

    public void GetHandCardData(sbyte[] cards)
    {
        SetContainerData(handTilesContainer, handList);

        Array.Sort(cards);

        for (int i = 0; i < cards.Length; i++)
        {
            int type = cards[i] / 10;
            int val = cards[i] % 10;
            string typeName;
            if (type==0)
            {
                typeName = "W";
            }
            else if (type == 1)
            {
                typeName = "T";
            }
            else
            {
                typeName = "S";
            }
            foreach (Transform child in handList[i].transform)
            {
                child.gameObject.SetActive(false);
            }
            if (val==0)
            {
                handList[i].gameObject.SetActive(false);
            }
            else
            {
                handList[i].transform.Find("MJ_" + typeName + val).gameObject.SetActive(true);
            }


        }

        //foreach (var cardNum in cards)
        //{
        //    int type = cardNum / 10;
        //    int val = cardNum % 10;
            
        //}
    }

}
