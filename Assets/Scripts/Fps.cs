using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fps : MonoBehaviour
{
    public float _UpdateInterval = 0.1f;
    private float _LastInterval;
    private int _Frames = 0;

    private float _FPS;
    // Use this for initialization
    void Start()
    {
        _LastInterval = Time.realtimeSinceStartup;
        _Frames = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
            enabled = false;

        _Frames++;
        if (Time.realtimeSinceStartup > _LastInterval + _UpdateInterval)
        {
            _FPS = _Frames / (Time.realtimeSinceStartup - _LastInterval);
            _Frames = 0;
            _LastInterval = Time.realtimeSinceStartup;
        }

    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width-100, Screen.height-50, 100, 50), _FPS.ToString());
    }

}