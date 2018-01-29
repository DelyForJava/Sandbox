using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public UnityEngine.UI.Button exitBtn;
	// Use this for initialization
	void Start () {
        exitBtn.onClick.AddListener(delegate {
            var activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            var currentScene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(1);
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currentScene);

        });

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
