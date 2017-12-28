using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MahjongDisplay : SingletonBehaviour<MahjongDisplay>
{

    public GameObject timerObj;
    public Text timeVal;
    
    public float timePlus;
    public float timeMinus;
    public bool isPlus;
    public string timeDes;


	// Use this for initialization
	void Start () {
	    
    }
	
	// Update is called once per frame
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





}
