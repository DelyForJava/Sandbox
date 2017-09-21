using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TableController : SingletonMonoBehaviour<TableController>
{
	[SerializeField] GameObject _tableInputBox;	
	[SerializeField] TableCounter _counter;
    [SerializeField] TableIndicator _indicator;
	[SerializeField] Texture[] _directionLightTexture;         // 东，南，西，北
    [SerializeField] Texture[] _directionGrayTexture;
    [SerializeField] TableDirection[] _tableDirection;         // 逆时针排序，“0” 是自己
    public int _roundTime = 10;
	bool _openTableInput = false;
	public bool _openTableUI = true;

#region close 
    /// <summary>
    /// 关闭计时器
    /// </summary>
    public void CloseCounter()
    {
		if (_counter == null)
			return;

		_counter.StopCountdown();
    }

    /// <summary>
    /// 关闭出牌指示箭头
    /// </summary>
    public void CloseIndicator()
    {
		if (_indicator == null)
			return;
		
        _indicator.ResetIndicator();
    }

    /// <summary>
    /// 关闭方向指示器
    /// </summary>
    public void CloseDirection()
    {
        if (_tableDirection == null)
            return;

        for (int i = 0; i < 4; ++i)
		{
			if (_tableDirection [i] != null)
				_tableDirection[i].ToDark();
		}
    }
#endregion

#region set table state
    /// <summary>
    /// 设置桌面的东南西北
    /// </summary>
    /// <param name="eastIndex">东的方向在显示上的index</param>
    public void SetTableDirection(int eastIndex)
    {
		if (_tableDirection == null || _directionLightTexture == null || _directionGrayTexture == null)
			return;

		for (int i = 0; i < 4; ++i)
		{
			// 图片的索引
			int texIndex = (i + 4 - eastIndex) % 4;

			_tableDirection[i].LightTexture = _directionLightTexture[texIndex];
			_tableDirection[i].GrayTexture = _directionGrayTexture[texIndex];
			_tableDirection[i].ToDark();
		}	
    }

    /// <summary>
    /// 设置跑马灯
    /// 设置倒计时
    /// 收到他人抓牌 或者 吃碰杠
    /// </summary>
    /// <param name="index">index 0为自己, 1,2顺势为右手边</param>
    public void SetCurrentPlayer(int displayIndex)
    {
		if (displayIndex < 0 || displayIndex > 3)
            return;

		if (_counter == null || _tableDirection == null)
			return;
		
		CloseDirection ();

        _counter.StartCountdown(_roundTime);
		_tableDirection[displayIndex].ToBlink();
    }
	/*
    public void SetCurrentPlayedTile(Pai tile)
    {
		if (_indicator == null)
			return;
		
        if (tile != null)
            _indicator.SetIndicatorPos(tile.transform.position);
    }*/
#endregion

#region table input collider
	public void OpenInput ()
	{
		_openTableInput = true;
	}

	public void CloseInput ()
	{
		_openTableInput = false;
	}
#endregion

#region mono behaviour
	void Update ()
	{
		if (_openTableInput)
		{
#if UNITY_IOS || UNITY_ANDROID
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)
#else 
			if (Input.GetMouseButtonDown(0))
#endif
			{
#if UNITY_IOS || UNITY_ANDROID
				if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
					return;
#else
				if (EventSystem.current.IsPointerOverGameObject())
					return;
#endif
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, Mathf.Infinity))
				{
					if (hit.collider.gameObject == _tableInputBox)
					{
						if (_openTableUI)
						{
//							UIControllerGame.Instance.CloseFinished ();
							//UIControllerLocator.Instance.CloseScorePanel ();
                            //UIControllerGame.Instance.CloseMask ();
							_openTableUI = false;
						}
						else
						{
//							UIControllerGame.Instance.OpenFinished ();
							//UIControllerLocator.Instance.OpenScorePanel ();
                            //UIControllerGame.Instance.OpenMask ();
							_openTableUI = true;
						}
					}
				}	
			}
		}
	}
#endregion
}