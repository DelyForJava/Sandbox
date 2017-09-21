using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameClient : SingletonBehaviour<GameClient>, IMonoBehaviourEventHandler {

	private GameMode _gameMode = null;

	/// <summary>
	/// Protected constructor, since this is a singleton pattern class.
	/// </summary>
	private GameClient() {
		Debug.Log ("GameClient Constructor");
	}

	void Awake()
	{
		EventAwake ();
	}

	void Start () 
	{
		Debug.Log("=============GameClient start up.=============");
		Application.targetFrameRate = 30;
		gameObject.AddComponent<Loom> ();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		EventStart();
	}

	new void OnDestroy() {
		Debug.Log("=============GameClient shut down.=============");
		base.OnDestroy();			
		EventDestroy ();
	}

	void OnApplicationQuit()
	{
		//EventApplicationQuit ();
	}

	/// <summary>
	/// Current game mode.
	/// </summary>
	public GameMode CurrentGameMode 
	{
		get { return _gameMode; }
		set 
		{
			if (_gameMode != null) 
			{
				_gameMode.Leave();
			}

			if (value == null) {
				GameObject.Destroy (_gameMode.gameObject);
			}

			_gameMode = value;

			if (_gameMode != null) {
				_gameMode.Enter ();
			}
		}
	}

	/// <summary>
	/// Update once per frame
	/// </summary>
	void Update () {
		EventUpdate ();
	}


	/// <summary>
	/// Update at fixed rate, the interval is 0.02 second.
	/// </summary>
	void FixedUpdate() {
		EventFixedUpdate (Time.fixedDeltaTime);
	}
}

