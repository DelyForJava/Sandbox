using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTile : SingletonBehaviour<HandTile> {

    public List<Tile> HandTileList = new List<Tile>();//13
    public Tile HoldTile = new Tile();//13

    public GameObject HoldTileObj;
    public GameObject HandTileContainer;
    public List<GameObject> ObjList = new List<GameObject>();//13

    // Use this for initialization
    void Start ()
    {
        HoldTile.SelfObj = HoldTileObj;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetObjList()
    {
        ObjList.Clear();
        foreach (Transform child in HandTileContainer.transform)
        {
            ObjList.Add(child.gameObject);
        }
    }

    public void SetDataToObjList()
    {
        GetObjList();
        //set all image deactive
        foreach (Transform child in HandTileContainer.transform)
        {
            foreach (Transform childImage in child.transform)
            {
                childImage.gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < HandTileList.Count; i++)
        {
            if (i == 13)
            {
                HoldTile.ValueSbyte = HandTileList[i].ValueSbyte;
                return;
            }
            //Debug.Log("i :" + i);
            string typeName;
            if (HandTileList[i].FaceType == TileType.Wan)
            {
                typeName = "W";
            }
            else if (HandTileList[i].FaceType == TileType.Tong)
            {
                typeName = "T";
            }
            else
            {
                typeName = "S";
            }
            ObjList[i].transform.Find("MJ_" + typeName + HandTileList[i].FaceValue).gameObject.SetActive(true);
            HandTileList[i].SelfObj = ObjList[i];
        }
    }

    //add
    public void AddTile(Tile tile)
    {
        HandTileList.Add(tile);
        SortHandTileList();
        SetDataToObjList();
    }

    public void AddTile(sbyte valSbyte)
    {
        var tile = new Tile();
        tile.ValueSbyte = valSbyte;
        HandTileList.Add(tile);
        SortHandTileList();
        SetDataToObjList();
    }

    public void AddTile(List<Tile> tileList)
    {
        HandTileList.AddRange(tileList);
        SortHandTileList();
        SetDataToObjList();
    }

    //delete
    public void DeleteTile(Tile tile)
    {
        HandTileList.Remove(tile);
        SortHandTileList();
        SetDataToObjList();
    }

    public void DeleteTile(sbyte valSbyte)
    {
        var tile = new Tile();
        tile.ValueSbyte = valSbyte;
        for (int j = 0; j < HandTileList.Count; j++)
        {
            if (HandTileList[j].ValueSbyte == valSbyte)
            {
                HandTileList.Remove(HandTileList[j]);
                break;
            }
        }
        SortHandTileList();
        SetDataToObjList();
    }

    public void DeleteTile(sbyte[] tileList)
    {
        for (int i = 0; i < tileList.Length; i++)
        {
            for (int j = 0; j < HandTileList.Count; j++)
            {
                if (HandTileList[j].ValueSbyte == tileList[i])
                {
                    HandTileList.Remove(HandTileList[j]);
                    break;
                }
            }
        }
        SortHandTileList();
        SetDataToObjList();
    }

    //sort
    public void SortHandTileList()
    {
        List<sbyte> handTileListValueToSrot =new List<sbyte>();
        
        foreach (var tile in HandTileList)
        {
            if (tile.ValueSbyte != 0)
            {
                handTileListValueToSrot.Add(tile.ValueSbyte);
            }
        }

        handTileListValueToSrot.Sort();
        handTileListValueToSrot.Reverse();

        HandTileList.Clear();
        foreach (var valSbyte in handTileListValueToSrot)
        {
            Tile tile =new Tile();
            tile.ValueSbyte = valSbyte;
            HandTileList.Add(tile);
        }

    }
    
}
