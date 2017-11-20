using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ItemTest();
	    TaskTest();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public Button addItemBtn;
    public InputField IdInputField;
    public InputField NumInputField;


    public Button deleteItemBtn;
    public Button updateItemBtn;

    public void ItemTest()
    {
        var client = GameClient.Instance;
        addItemBtn.onClick.AddListener(delegate ()
        {
            client.MahjongGamePlayer.PackageAddItemReqDef(Convert.ToInt32(IdInputField.text), Convert.ToInt32(NumInputField.text));
        });

        deleteItemBtn.onClick.AddListener(delegate ()
        {
            client.MahjongGamePlayer.PackageRemoveItemReqDef(Convert.ToInt32(IdInputField.text), Convert.ToInt32(NumInputField.text));
        });

        updateItemBtn.onClick.AddListener(delegate ()
        {
            client.MahjongGamePlayer.PackageUpdateItemReqDef(Convert.ToInt32(IdInputField.text), (sbyte)Convert.ToInt32(NumInputField.text));
        });
    }


    public Button updateProgressBtn;
    public Button taskSubmitBtn;
    //public Button taskSubmitBtn;
    public InputField taskIdInputField;
    public InputField taskAddProgressInputField;

    public void TaskTest()
    {
        var client = GameClient.Instance;
        updateProgressBtn.onClick.AddListener(delegate ()
        {
            client.MahjongGamePlayer.TaskProgressUpdateReqDef(Convert.ToInt32(taskIdInputField.text), Convert.ToInt32(taskAddProgressInputField.text));
        });

        taskSubmitBtn.onClick.AddListener(delegate ()
        {
            client.MahjongGamePlayer.TaskSubmitReqDef(Convert.ToInt32(taskIdInputField.text));
        });
    }

}
