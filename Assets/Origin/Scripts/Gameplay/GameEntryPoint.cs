using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public struct ABC {
	public int i;
}

public struct CCC {
	public string name;
	public List<ABC> list;
}

public struct AAA {
	public int a;
	public int b;
	public float c;
	public string s;
	public List<CCC> list;
	public CCC ccc;
}

public partial class GameEntryPoint : MonoBehaviour
{
	AAA aaa;

	void Awake()
	{
		aaa = new AAA ();
		aaa.a = 100;
		aaa.b = 123;
		aaa.c = 99.5f;
		aaa.s = "ssss";
		aaa.ccc = new CCC ();
		aaa.ccc.name = "ccc";
		aaa.ccc.list = new List<ABC> ();
		ABC abc = new ABC ();
		abc.i = 999;
		aaa.ccc.list.Add (abc);
		aaa.list = new List<CCC> ();
		aaa.list.Add (aaa.ccc);
		var serializer = MsgPack.Serialization.MessagePackSerializer.Get<AAA> ();
		MemoryStream ms = new MemoryStream ();
		serializer.Pack(ms,aaa);
		Debug.Log (ms.Length);
		ms.Position = 0;

		var a5 = serializer.Unpack (ms);
		int ii = 8;
		for (int i = 0; i<a5.ccc.list.Count; ++i) {
			Debug.Log (a5.ccc.list [i]);
		}
	}

	void Start()
	{
		//UIDebugViewController.Instance.OpenViewRoot ();

		int level = SceneManager.GetActiveScene ().buildIndex;
		switch (level) {
		case 0:
			break;
		case 1:
			{
				UILoginController.Instance.Open ();
			}
			break;
		case 2:
			{
				UIMainMenuController.Instance.Open ();
			}
			break;
		case 3:
			{	
				var go = new GameObject("MahjongGame");
				MahjongGame game = go.AddComponent<MahjongGame>();
				GameClient.Instance.CurrentGameMode = game;
				UIGameSetingController.Instance.Open();
				if (GameClient.Instance.GameStateDef != null) {
					game.OnRestoreGameState (GameClient.Instance.GameStateDef);
					GameClient.Instance.GameStateDef = null;
					UIGameSetingController.Instance.OnRefreshHeadAll ();
					GameClient.Instance.MG._shuffle_initpocket_state = 1;
				}
                

			}
			break;
		}
	}
}

