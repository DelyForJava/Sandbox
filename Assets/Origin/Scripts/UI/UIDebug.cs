using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDebug : MonoBehaviour {

	public Text _debugInfo;
    public bool _isNeedUpdate;
    public bool _isVisibleRotateImg;
    public GameObject _objInfo;

    public string _strText;
    public int _infoTextIdx;
    public float _dt;

	void Awake()
	{
        _isVisibleRotateImg = false;
        _isNeedUpdate = false;
        _infoTextIdx = 1;
        _dt = 0;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(_isNeedUpdate)
        {
            if (_isVisibleRotateImg && _objInfo)
            {
                rotateImageUI();
            }

            if (_strText.Length > 0)
            {
                _dt += Time.deltaTime;
                if (_dt >= 0.5f)
                {
                    _dt = 0;
                    updateInfoText();
                }
            }
            
        }
        
	}

 #region update debugUI
    void rotateImageUI()
    {
        _objInfo.transform.RotateAround(_objInfo.transform.position, new Vector3(0, 0, 90.0f), -2.0f);
    }
    void updateInfoText()
    {
        _debugInfo.text = _strText.Substring(0, _infoTextIdx);
        _infoTextIdx = ++_infoTextIdx % _strText.Length;
        if (_infoTextIdx == 0)
            _infoTextIdx = 1;
    }

 #endregion
}
