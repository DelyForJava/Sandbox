using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessage : SingletonBehaviour<ShowMessage>
{
    public GameObject messageObj;
    public Text messageText;
    public Button confirmBtn;
    public Button closeBtn;

    //public delegate void UseItemEventHandler(int id, int count);
    //public event UseItemEventHandler UseItemEventEvent;

    //public void UseItem(int id,int count)
    //{
    //    if (UseItemEventEvent != null)
    //    {
    //        UseItemEventEvent(id,count);
    //    }
    //}

    // Use this for initialization
    void Start () {
	    confirmBtn.onClick.AddListener(delegate ()
	    {
	        //isClicked = true;
            DestroySelf();
	    });
	    closeBtn.onClick.AddListener(delegate ()
	    {
            DestroySelf();
	    });
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //public void ShowMessageLog(string message)
    //{
    //    messageText.text = message;
    //}
    public void DestroySelf()
    {
        DestroyImmediate(this);
        DestroyImmediate(messageObj);
    }

    //public bool isClicked;
    //public void OnClicked()
    //{
    //    isClicked=true;
    //}
}
