using UnityEngine;
using System.Collections;
//using HedgehogTeam.EasyTouch;

public class GameCamera  : MonoBehaviour
{
	[SerializeField] GameObject _camera16_9;
	[SerializeField] GameObject _camera4_3;
	[SerializeField] GameObject _camera3_2;

	[SerializeField] Camera _orthCamera;		// 正交相机
	[SerializeField] Transform _mahjongGroup;	// 手牌麻将的group


	void Awake ()
	{
		// set perspective camera 
		_camera16_9.SetActive (false);
		_camera4_3.SetActive (false);
		_camera3_2.SetActive (false);

		switch (GameConfig.GetScreenRatio ())
		{
		case GameConfig.ScreenRatio._1610:
			_camera16_9.SetActive (true);
			break;
		case GameConfig.ScreenRatio._169:
			_camera16_9.SetActive (true);
			break;
		case GameConfig.ScreenRatio._43:
			_camera4_3.SetActive (true);
			_orthCamera.orthographicSize = 40.5f;
			_mahjongGroup.position = new Vector3 (_mahjongGroup.position.x, -26f, _mahjongGroup.position.z);
			break;
		case GameConfig.ScreenRatio._32:
			_camera3_2.SetActive (true);
			_orthCamera.orthographicSize = 40.5f;
			_mahjongGroup.position = new Vector3 (_mahjongGroup.position.x, -26f, _mahjongGroup.position.z);
			break;
		default:
			_camera16_9.SetActive (true);
			break;
		}
	}

	void Start ()
	{
		// set easy touch camera
		// remove all camera, then add ortho camera
		//Camera camera = EasyTouch.GetCamera (0);
		//while (camera != null)
		//{
		//	EasyTouch.RemoveCamera (camera);
		//	camera = EasyTouch.GetCamera (0);
		//}
		//EasyTouch.RemoveCamera (camera);

		//camera = GameObject.Find ("Player/SelfCamera/OCamera169").GetComponent<Camera> ();
		//EasyTouch.AddCamera (camera);
	}
}