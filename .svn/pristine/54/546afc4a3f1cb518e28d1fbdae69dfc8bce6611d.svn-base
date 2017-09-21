using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using odao.scmahjong;

public class MahjongTile : MonoBehaviour {

	/*
	public static float Width = 7.25f;	//x-axis
	public static float Thickness = 5f;	//y-axis
	public static float Height = 10f;		//z-axis
	public static Vector3 ColliderSize = new Vector3 (6f, 5f, 10f * 2f);
	*/

	public static float Width = 0.35f;	//x-axis
	public static float Thickness = 0.22f;	//y-axis
	public static float Height = 0.47f;		//z-axis
	public static Vector3 ColliderSize = new Vector3 (0.3500016f, 0.2141244f, 0.4600004f);
	public static int TotalClickCount = 1;

	private static SpawnPool _Pool;

	TileDef _def;
	public TileDef Def { get { return _def; } }
	//public int Index;

	public enum Face {
		NONE = -1,
		PLANE_POSITIVE = 0,	//default
		PLANE_NEGTIVE,
		STAND_POSITIVE,
		STAND_NEGTIVE,
		NUM_FACE,
	}

	public Face _lastDirection;
	public Face _direction;
	public Face Direction
	{
		get {
			return _direction;
		}
		set {
			_lastDirection = _direction;
			_direction = value;
			transform.rotation = Quaternion.identity;
			transform.position = Vector3.zero;
			switch (value) {
			case Face.PLANE_POSITIVE:
				break;
			case Face.PLANE_NEGTIVE:
				transform.Rotate (Vector3.left, 180f, Space.Self);
				break;
			case Face.STAND_POSITIVE:
				transform.Rotate (Vector3.left, 90f, Space.Self);
				break;
			case Face.STAND_NEGTIVE:
				transform.Rotate (Vector3.left, 90f, Space.Self);
				transform.Rotate (Vector3.forward, 180f, Space.Self);
				break;
			}
		}
	}

	private List<FiniteTimeAction> _insertActionList;
	private List<FiniteTimeAction> _flipActionList;
	void Awake()
	{
		_insertActionList = new List<FiniteTimeAction> ();
		_flipActionList = new List<FiniteTimeAction> ();
	}

	public static MahjongTile Create(TileDef def)
	{
		if (!TileDef.IsValid (def.Value)) {
			Debug.LogError("tile : " + def.Value + " is not a valid tile");
			return null;
		}

		Transform inst = null;
		MahjongTile._Pool = PoolManager.Pools ["mahjongres"];

		int point = def.GetPoint ();
		switch (def.GetKind ()) {
		case TileDef.Kind.CRAK:
			inst = MahjongTile._Pool.Spawn ("Crak_" + point);
			break;
		case TileDef.Kind.BAM:
			inst = MahjongTile._Pool.Spawn ("Bam_" + point);
			break;
		case TileDef.Kind.DOT:
			inst = MahjongTile._Pool.Spawn ("Dot_" + point);
			break;
		default:
			Debug.LogError ("UnValid kind : " + def.GetKind());
			return null;
		}

		inst.gameObject.SetActive (true);
		inst.transform.parent = null;
		inst.transform.rotation = Quaternion.identity;
		inst.transform.position = Vector3.one * 9999f;

		MahjongTile tile = inst.gameObject.GetComponent<MahjongTile> () ?? inst.gameObject.AddComponent<MahjongTile> ();
		tile._def = def;
		tile.ClickCount = 0;

		BoxCollider collider = inst.gameObject.AddComponent<BoxCollider> ();
		collider.center = Vector3.zero;
		collider.size = ColliderSize;

		return tile;
	}

	public static void Destroy(MahjongTile tile)
	{
		tile.Reset ();
		_Pool.Despawn (tile.transform);
	}

	public void Reset()
	{
		transform.parent = null;
		transform.rotation = Quaternion.identity;
		transform.position = Vector3.one * 9999f;
		Direction = Face.PLANE_POSITIVE;
		ResetColor ();
		_enabled = true;
	}

	bool _willPlay = false;
	public void WillPlay()
	{
		if (_enabled == false)
			return;

		if (_willPlay == false) {
			transform.localPosition = transform.localPosition + Vector3.up * 4f;
			_willPlay = true;
		}
	}

	public void NoPlay()
	{
		if (_enabled == false)
			return;

		if (_willPlay) {
			transform.localPosition = transform.localPosition + Vector3.down * 4f;
			_willPlay = false;
		}
	}

	int _clickCount = 0;
	public int ClickCount { 
		get { return _clickCount; } 
		set { _clickCount = value; }
	}
	public int clickPlay()
	{
		if(_enabled == false)
			return -1;

		_clickCount = ++_clickCount % 2;
		if(_clickCount == 0)
		{
			transform.localPosition = transform.localPosition + Vector3.down * MahjongTile.Thickness * 0.5f;
			transform.localScale = Vector3.one;
		}
		else
		{
			transform.localPosition = transform.localPosition + Vector3.up * MahjongTile.Thickness * 0.5f;
			transform.localScale = Vector3.one * 1.2f;
			UIGameHuPromptController.Instance.OpenHuPrompt (_def);
			// 播放选牌声音
			//AudioManager.Instance.PlaySound (AudioManager.Instance.Xuan_Pai);
		}

		return _clickCount;
	}

	private IEnumerator play(Vector3 dstPosition)
	{
		transform.localPosition = dstPosition;
		yield return null;
	}

	public void Play(Vector3 dstPosition)
	{
		StartCoroutine (play (dstPosition));
	}

	private IEnumerator draw(Vector3 dstPosition)
	{	
		transform.localPosition = dstPosition;
		yield return null;
	}

	public void Draw(Vector3 dstPosition)
	{
		StartCoroutine (draw (dstPosition));
	}

	public void Insert(Vector3 dstPosition, bool isLocal = false)
	{
		_insertActionList.Clear ();
		FiniteTimeAction mby = MoveBy.Create (0.1f, Vector3.up * MahjongTile.Thickness, isLocal);
		FiniteTimeAction mbt1 = MoveTo.Create (0.2f, dstPosition + Vector3.up * MahjongTile.Thickness, isLocal);
		FiniteTimeAction mbt2 = MoveTo.Create (0.1f, dstPosition, isLocal);
		_insertActionList.Add (mby);
		_insertActionList.Add (mbt1);
		_insertActionList.Add (mbt2);
		ActionManager.Instance.RunAction (gameObject, _insertActionList);
	}

	public void Moveto(Vector3 dstPosition, bool isLocal = false)
	{	
		ActionBase mbt = MoveTo.Create (0.2f, dstPosition, isLocal);
		ActionManager.Instance.RunAction (gameObject, mbt);
	}

	//deprecated
	public void Flip(float delay = 0f, bool withAnimation = true)
	{
		/*
		#region NEW_ADD
		int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex;
		if (sceneIndex != (int)EScene.SCENE_Demo) {
			return;
		}
		#endregion
		*/
		Vector3 oldPos = transform.position;
		if (withAnimation) {
			_flipActionList.Clear ();

			FiniteTimeAction delayTime = null;
			if (delay > 0f) {
				delayTime = DelayTime.Create (delay);
			}

			//上移
			FiniteTimeAction mby = MoveBy.Create (0.2f, Vector3.up * 10f);
			//翻过来
			FiniteTimeAction mbt1 = RotateBy.Create (0.2f, Vector3.forward, 180f, Space.Self);
			//下移到原来的地方
			FiniteTimeAction mbt2 = MoveTo.Create (0.2f, oldPos);
			if (delayTime != null) {
				_flipActionList.Add (delayTime);
			}
			_flipActionList.Add (mby);
			_flipActionList.Add (mbt1);
			_flipActionList.Add (mbt2);
			ActionManager.Instance.RunAction (gameObject, _flipActionList);
		} 
		else {
			transform.Rotate (Vector3.forward, 180f, Space.Self);
		}
	}

	//deprecated
	public void FlipFinal(float delay = 0f, bool withAnimation = true)
	{
		/*
		#region NEW_ADD
		int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex;
		if (sceneIndex != (int)EScene.SCENE_Demo) {
			return;
		}
		#endregion
		*/
		Vector3 oldPos = transform.position;
		if (withAnimation) {
			_flipActionList.Clear ();

			FiniteTimeAction delayTime = null;
			if (delay > 0f) {
				delayTime = DelayTime.Create (delay);
			}

			//上移
			FiniteTimeAction mby = MoveBy.Create (0.2f, Vector3.up * 10f);
			//翻过来
			FiniteTimeAction mbt1 = RotateBy.Create (0.2f, Vector3.forward, 180f, Space.Self);
			//下移到原来的地方
			FiniteTimeAction mbt2 = MoveTo.Create (0.2f, oldPos);

			FiniteTimeAction mbt3 = MoveTo.Create (0.2f, Vector3.up * 5f);
			FiniteTimeAction mbt4 = RotateTo.Create (0.2f, Vector3.zero);

			FiniteTimeAction mbt5 = InvokeAction.Create (delegate(GameObject target, object userdata) {
				//TableController.Instance.SetCurrentPlayedTile (this);
			});
			if (delayTime != null) {
				_flipActionList.Add (delayTime);
			}
			_flipActionList.Add (mby);
			_flipActionList.Add (mbt1);
			_flipActionList.Add (mbt2);
			_flipActionList.Add (mbt3);
			_flipActionList.Add (mbt4);
			_flipActionList.Add (mbt5);
			ActionManager.Instance.RunAction (gameObject, _flipActionList);
		} 
		else {
			//transform.Rotate (Vector3.forward, 180f, Space.Self);
			transform.rotation = Quaternion.identity;
			transform.position = Vector3.up * 5f;
			//TableController.Instance.SetCurrentPlayedTile (this);
		}
	}

	public void Despawn()
	{
		//gameObject.SetActive (false);
		MahjongTile._Pool.Despawn (gameObject.transform, MahjongTile._Pool.transform);
	}

	private void OnSpawned(SpawnPool pool)
	{
		Debug.Log
		(
			string.Format
			(
				"MahjongTile {0} | OnSpawned running for '{1}' in pool '{2}'.", 
				this.gameObject.activeSelf,
				this.name, 
				pool.poolName
			)
		);
		this.gameObject.SetActive (true);
		this.ResetColor ();
		this.Enable ();
	}

	private void OnDespawned(SpawnPool pool)
	{
		Debug.Log
		(
			string.Format
			(
				"MahjongTile {0} | OnDespawned running for '{1}' in pool '{2}'.", 
				this.gameObject.activeSelf,
				this.name,
				pool.poolName
			)
		);
	}

	#region property of mahjongtile
	public void SetColor(Color color)
	{
		var mr = gameObject.GetComponent<MeshRenderer> ();
		mr.material.color = color;
	}

	public void ResetColor()
	{	
		var mr = gameObject.GetComponent<MeshRenderer> ();
		mr.material.color = Color.white;
	}

	bool _enabled = true;
	public void Disable()
	{
		_enabled = false;
	}

	public void Enable()
	{
		_enabled = true;
	}

	public bool Enabled()
	{
		return _enabled;
	}
	#endregion
}