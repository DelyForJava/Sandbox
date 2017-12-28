using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkInterface;
using MP;

public partial class GameClient : SingletonBehaviour<GameClient>, IMonoBehaviourEventHandler {

	odao.scmahjong.NetworkPlayer _self;
	public odao.scmahjong.NetworkPlayer MahjongGamePlayer { get { return _self; } }
	public MahjongGame MG { get { return (MahjongGame)CurrentGameMode; } }
    public IAssetLoader AssetLoader { get; set; }
	public GameMessage.GameStateDef GameStateDef;
	public byte SpecialCard { get; set; }
	public byte SpecialType { get; set; }

	public int Port = 36667;
	public int UserId = 30056;

	public void EventAwake()
	{
		//Port = 16905;
		Port = 36667;
		//UserId = 30056;
		//UserId = 10817;
		//UserId = 10819;
		UserId = 30058;

        AssetLoader = gameObject.AddComponent<ResourceLoader> ();
		UnitySystemConsoleRedirector.Redirect ();
		_self = odao.scmahjong.NetworkPlayer.Create ();
		_self.AddEventListener (GSNetWorkStateEvent);
	}

	public void EventStart()
	{
	}

	public void EventUpdate()
	{
		MahjongGame game = (MahjongGame)CurrentGameMode;
		if (game != null) {
			game.Tick ();
		}
	}

	public void EventFixedUpdate(float time)
	{
		//ActionManager.Instance.Update (time);
	}

	public void EventDestroy()
	{
	}

	public void EventApplicationQuit()
	{
	}

	public bool IsInMahjongGame()
	{
		return UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex == 3;
	}

	/// <summary>
	/// Game Server network state event.
	/// </summary>
	/// <returns>The network state event.</returns>
	/// <param name="state">State.</param>
	void GSNetWorkStateEvent(NetWorkState state)
	{
		Debug.Log ("================");
		if (state == NetWorkState.CONNECTED) {
			Debug.Log ("connected");
		}

		if (state == NetWorkState.TIMEOUT) {
			Debug.Log ("TIMEOUT");
		}

		if (state == NetWorkState.ERROR_CONNECT) {
			Debug.Log ("ERROR_CONNECT");
		}

		if(state == NetWorkState.DISCONNECTED)
		{
			Debug.Log ("disconnected");
			var client = GameClient.Instance;
			client.MahjongGamePlayer.ConnectGameServer ("login.dv.7pmigame.com", Port, delegate() {
				//client.MahjongGamePlayer.StartAuth(GameClient.Instance.UserId);
			});
		}

		if(state==NetWorkState.CLOSED)
		{
			Debug.Log ("closed");
		}
	}
}
