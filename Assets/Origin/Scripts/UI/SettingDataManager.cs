using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingDataManager : MonoBehaviour
{
    public Button changeAccountBtn;

	// Use this for initialization
	void Start ()
	{
        var canvas = GameObject.Find("Canvas");
        var go = canvas.transform.Find("Login").gameObject;

        changeAccountBtn.onClick.AddListener(delegate
	    {
            go.SetActive(true);
            gameObject.SetActive(false);

            //UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        });

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
