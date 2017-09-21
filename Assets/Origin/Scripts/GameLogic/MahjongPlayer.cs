using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using NetworkInterface;
using odao.scmahjong;

public class MahjongPlayer : MonoBehaviour {
	
	enum LOCATOR
	{
		POCKET = 0,
		PLAY = 1,
		STACK = 2,
		COMBO = 3,
		WIN = 4,
	}

	List<GameObject> _locatorList;
	Vector3 _direction;

	public Transform PocketLocator { get { return _locatorList[(int)LOCATOR.POCKET].transform; } }
	public Transform PlayLocator { get { return _locatorList[(int)LOCATOR.PLAY].transform; } }
	public Transform StackLocator { get { return _locatorList[(int)LOCATOR.STACK].transform; } }
	public Transform ComboLocator { get { return _locatorList[(int)LOCATOR.COMBO].transform; } }
	public Transform WinLocator { get { return _locatorList[(int)LOCATOR.WIN].transform; } }
	public Transform MyShowPocketLocator = null;

	odao.scmahjong.Player _proxy;
	public odao.scmahjong.Player Proxy {
		get{ return _proxy; }
		set { _proxy = value; }
	}

	MahjongPile _pile;
	protected List<MahjongTile> _sortPocketList;

	void Awake() {
		_proxy = odao.scmahjong.Player.Create ();
		_locatorList = new List<GameObject> (5);
		_playedList = new List<MahjongTile> ();
	}

	public void InitLocator(string[] locators)
	{
		GameObject go = null;
		for (int i = 0; i < locators.Length; ++i) {
			go = GameObject.Find (locators [i]);
			go.transform.localScale = Vector3.one;
			go.GetComponent<MeshRenderer> ().enabled = false;
			_locatorList.Add (go);
		}
	}

	public void InitDirection(Vector3 direction)
	{
		_direction = direction;
	}

	public virtual void Reset()
	{
		_playIndex = -1;
		_playedList.Clear ();
		MahjongTile.TotalClickCount = 1;
		if (Proxy != null) {
			Proxy.WinState = false;
		}
	}

	public int SetupStack(MahjongPile pile, int offset, int num)
	{		
		MahjongTile tile = null;
		List<MahjongTile> stack = pile.GetRange (offset, num);
		_pile = pile;
		// 0 2 4 ... 
		// 1 3 5 ...
		for (int i = 0; i < stack.Count; ++i) {

			tile = stack [i];

			tile.transform.parent = null;

			tile.Direction = MahjongTile.Face.PLANE_NEGTIVE;
			float angle = Vector3.Angle (Vector3.right, _direction);
			if (_direction == Vector3.forward) {
				angle = 270f;
			}
			//Debug.Log (_angle + "," + _direction + "," + _stackPosition);
			tile.transform.Rotate (Vector3.up, angle, Space.Self);
			tile.transform.parent = StackLocator;

			int row = i / 2;
			int col = i % 2;

			tile.transform.localPosition = row * _direction * MahjongTile.Width + col * Vector3.up * MahjongTile.Thickness;
		}

		return num;
	}

	#region OLD_CODE
	Vector3 _originPocketPos;
	//Vector3 _direction;
	float _angle;

	protected bool _initDrawFinished;
	public bool InitDrawFinished { 
		get { return _initDrawFinished; } 
		set { _initDrawFinished = value; }
	}

	public bool BaoTing { get; set; }
	protected MahjongTile _willPlay;
	public bool Win { get; set; }
	//public int LackMinID { get; set; }
	//public int LackMaxID { get; set; }

	protected MahjongTile _drawTile;
	private int _playIndex = -1;
	private List<MahjongTile> _playedList;

	public SpawnPool ResPool { set { _resPool = value; } }
	protected SpawnPool _resPool;

	private IEnumerator initDraw(bool banker)
	{
		_initDrawFinished = false;

		float angle = 0f;
		if (_direction == Vector3.right) {
			//self
			angle = 0f;
		} else if (_direction == Vector3.forward) {
			//right
			angle = -270f;
		} else if (_direction == Vector3.left) {
			//front
			angle = -180;
		} else if (_direction == -Vector3.forward) {
			//left
			angle = -90f;
		}

		RotateBy rb = null;
		TileDef def = null;
		MahjongTile tile = null;
		Transform locator = PocketLocator;

		float duration = 0.1f;
		float waitDuration = 0.3f;

		int index = 0;
		int drawCountPerTime = 4;
		for(int i=0; i<4; ++i) {

			if(i==3) {
				drawCountPerTime = 1;
			}

			for(int j=0; j<drawCountPerTime; ++j) {

				//_pile.PopOneTile ();
				def = _proxy.PocketList [index];
				tile = MahjongTile.Create (def);
				tile.Direction = MahjongTile.Face.PLANE_NEGTIVE;
				tile.transform.Rotate (Vector3.up, angle, Space.Self);
				tile.transform.parent = locator.transform;

				tile.transform.localPosition = index * _direction * MahjongTile.Width;

				rb = RotateBy.Create (duration, Vector3.right, 90f, Space.Self);
				ActionManager.Instance.RunAction (tile.gameObject, rb);

				//yield return new WaitForSeconds (duration);

				++index;
			}

			yield return new WaitForSeconds (waitDuration);
		}

		for (int i = 0; i < locator.childCount; ++i) {
			tile = locator.GetChild (i).GetComponent<MahjongTile> ();
			rb = RotateBy.Create (duration, Vector3.right, -90f, Space.Self);
			ActionManager.Instance.RunAction (tile.gameObject, rb);
		}

		yield return new WaitForSeconds (waitDuration);

		//_proxy.SortPocketList (TileDef.Comparison);
		_sortPocketList = SortMahjongTile (locator);
		for (int i = 0; i < _sortPocketList.Count; ++i) {
			tile = _sortPocketList [i];
			tile.transform.localPosition = i * _direction * MahjongTile.Width;
			rb = RotateBy.Create (duration, Vector3.right, 90f, Space.Self);
			ActionManager.Instance.RunAction (tile.gameObject, rb);
		}

		yield return new WaitForSeconds (waitDuration);

		if (banker) {
			if (_proxy.PocketList.Count == 14) {
				def = _proxy.PocketList [13];
				_proxy.RemovePocketList (def, 1);
				Draw (def);
				_drawTile = null;
			} 
			else {
				def = TileDef.Create ();
				Draw (def);
				_drawTile = null;
			}
		}

		_initDrawFinished = true;
	}

	public virtual void InitDraw (bool banker) {
		StartCoroutine (initDraw (banker));
	}

	public List<MahjongTile> SortMahjongTile(Transform locator)
	{
		List<MahjongTile> list = new List<MahjongTile> ();
		for (int i = 0; i < locator.childCount; ++i) {
			list.Add (locator.GetChild (i).GetComponent<MahjongTile> ());
		}
		list.Sort (delegate(MahjongTile x, MahjongTile y) {
			if ((int)x.Def.SortID > (int)y.Def.SortID)
			{
				return 1;
			}

			if ((int)x.Def.SortID < (int)y.Def.SortID)
			{
				return -1;
			}

			return 0;
		});
		return list;
	}

	public virtual bool Draw(TileDef def)
	{
		Debug.Log (gameObject.name + " draw -> " + def.ToString());

		_drawTile = MahjongTile.Create (def);

		_drawTile.transform.parent = null;

		_drawTile.Direction = MahjongTile.Face.STAND_POSITIVE;
		_angle = Vector3.Angle (Vector3.right, _direction);
		//Debug.Log (angle + "    " + Vector3.right + "," + _direction);
		// unity3d bug from vector3.right to vector3.forward == 90 ?
		if (_direction == Vector3.forward) {
			_angle = 270f;
		}

		_drawTile.transform.Rotate (Vector3.forward, _angle, Space.Self);
		_drawTile.transform.parent = PocketLocator;
		Vector3 newPos = (PocketLocator.childCount - 1 + 0.2f) * _direction * MahjongTile.Width;
		_drawTile.Draw (newPos);

		_sortPocketList.Add (_drawTile);
		_proxy.Draw (def);

		return true;
	}

	protected void placePlayed(MahjongTile tile, bool withSound = true)
	{
		tile.Reset ();

		_angle = Vector3.Angle (Vector3.right, _direction);
		if (_direction == Vector3.forward) {
			_angle = 270f;
		}

		tile.Direction = MahjongTile.Face.PLANE_POSITIVE;
		tile.transform.Rotate (Vector3.up, _angle, Space.Self);
		Vector3 down = Vector3.Cross(_direction, Vector3.down);
		tile.transform.parent = PlayLocator;
		//Debug.Log (tile.transform.localRotation + "/" + _direction + "/" + _angle);
		int index = PlayLocator.childCount - 1;

		int row = index / 6;
		int col = index % 6;

		Vector3 newpos = col * _direction * MahjongTile.Width + row * down * MahjongTile.Height;

		//Debug.Log (row +":"+col);

		tile.Play (newpos);

		_playedList.Add (tile);

		if (withSound) {
			//AudioManager.Instance.PlayPai (((int)tile.ID).ToString ());
			//AudioManager.Instance.PlaySound (AudioManager.Instance.Luo_Pai);
			//TableController.Instance.SetCurrentPlayedTile (tile);
		}
		//close hupaiTips
		UIGameHuPromptController.Instance.Close();
	}

	//play self pocket mahjongtile
	public virtual bool Play(MahjongTile tile)
	{
		if (((odao.scmahjong.NetworkPlayer)_proxy).Play (tile.Def)) {
			_playIndex = _sortPocketList.IndexOf (tile);
			if (_playIndex >= 0) {
				_sortPocketList.Remove (tile);
				placePlayed (tile);
				return true;
			}
		}
		Debug.LogError ("!!!!ErrorPlay Specify tile");
		return false;
	}

	//random play one tile from network command
	public virtual bool Play(TileDef def)
	{
		MahjongTile tile = null;
		if (((Player)_proxy).Play (def)) {
			for (int i = 0; i < _sortPocketList.Count; ++i) {
				if (_sortPocketList [i].Def.Value == def.Value) {
					_playIndex = i;
					tile = _sortPocketList [_playIndex];	
					_sortPocketList.Remove (tile);
					placePlayed (tile);
					return true;
				}
			}
			Debug.Log (_sortPocketList.Count);
			Debug.Log (Proxy.Index + " Play(TileDef def)####################" + _playIndex);
			tile = MahjongTile.Create (def);
			placePlayed (tile);
			_playIndex = Random.Range (0, _sortPocketList.Count);
			tile = _sortPocketList [_playIndex];
			_sortPocketList.Remove (tile);
			tile.Despawn ();
		} 
		else {
			_proxy.RemovePocketList (1);
			tile = MahjongTile.Create (def);
			placePlayed (tile);
			_playIndex = Random.Range (0, _sortPocketList.Count);
			tile = _sortPocketList [_playIndex];
			_sortPocketList.Remove (tile);
			tile.Despawn ();
		}	

		return false;
	}

	public MahjongTile FindMahjongTileFromPocketList(TileDef def)
	{
		MahjongTile tile = null;
		for (int i = 0; i < _sortPocketList.Count; ++i) {
			tile = _sortPocketList [i];
			if (tile.Def.Value == def.Value) {
				return tile;
			}
		}
		return null;
	}

	public bool RemoveMahjonTileFromPocketList(TileDef def, int num)
	{
		int count = 0;
		for (int i = _sortPocketList.Count - 1; i >= 0; --i) {
			if (_sortPocketList [i].Def.Value == def.Value) {
				_sortPocketList.RemoveAt (i);
				++count;
				if (count == num) {
					return true;
				}
			}
		}
		return false;
	}

	public void updatePocketPosition()
	{
		if (_direction != Vector3.right)
			return;

		float duration = 0.2f;
		Vector3 to;
		ActionBase act = null;
		int count = PocketLocator.childCount;

		switch(count)
		{
		case 1:
		case 2:
			{
				to = _originPocketPos + _direction * 7 * MahjongTile.Width;
				act = MoveTo.Create(duration, to);
			}
			break;
		case 3:
		case 4:
			{
				to = _originPocketPos + _direction * 5 * MahjongTile.Width;
				act = MoveTo.Create(duration, to);
			}
			break;
		case 5:
		case 6:
			{
				to = _originPocketPos + _direction * 4 * MahjongTile.Width;
				act = MoveTo.Create(duration, to);
			}
			break;
		case 7:
		case 8:
			{
				to = _originPocketPos + _direction * 3 * MahjongTile.Width;
				act = MoveTo.Create(duration, to);
			}
			break;
		case 9:
		case 10:
			{
				to = _originPocketPos + _direction * 2 * MahjongTile.Width;
				act = MoveTo.Create(duration, to);
			}
			break;
		case 11:
		case 12:
			{
				to = _originPocketPos + _direction * 1 * MahjongTile.Width;
				act = MoveTo.Create(duration, to);
			}
			break;
		case 13:
		case 14:
			break;
		}

		if(act != null)
		{
			ActionManager.Instance.RunAction(PocketLocator.gameObject, act);
		}
	}

	public void SortPocketListAfterPlay()
	{
		Debug.Log ("!!!!SortPocketListAfterPlay");
		if (PocketLocator.childCount <= 0) {
			Debug.Log ("nopockets");
			return;
		}

		//updatePocketPosition();
		if (_playIndex == -1)
			Debug.LogError ("errororoororororoor");
		// before play, pockets count == n
		// after play, pockets count == n - 1
		// played index caculated before play
		if (_playIndex == PocketLocator.childCount) {
			_drawTile = null;
			_playIndex = -1;
			Debug.Log ("play last one");
			return;
		}

		MahjongTile iter = null;
		int insertIndex = -1;

		if (_drawTile != null) {
			for (int i = 0; i < _sortPocketList.Count; ++i) {
				iter = _sortPocketList [i];
				iter.ClickCount = 0;
				if (TileDef.Comparison (_drawTile.Def, iter.Def) <= 0) {
					insertIndex = i;
					break;
				}
			}
			Debug.Log ("!!!!_drawTile " + _drawTile + " " + Proxy.Index + " sort after play " + insertIndex + "####" + _playIndex);
		}

		if (insertIndex == -1 && _playIndex >= 0) {
			for (int i = _playIndex; i < _sortPocketList.Count; ++i) {
				Vector3 pos = i * _direction * MahjongTile.Width;
				_sortPocketList [i].Moveto (pos, true);
			}
			Debug.Log ("!!!!return" + Proxy.Index + " sort after play " + insertIndex + "####" + _playIndex);
			return;
		}

		Debug.Log (Proxy.Index + " sort after play " + insertIndex + "####" + _playIndex);

		if (insertIndex > _playIndex) {
			for (int i = _playIndex; i < insertIndex; ++i) {
				Vector3 pos = i * _direction * MahjongTile.Width;
				_sortPocketList [i].ClickCount = 0;
				_sortPocketList [i].Moveto (pos, true);
			}
		} else if (insertIndex < _playIndex) {
			for (int i = insertIndex; i < _playIndex; ++i) {
				Vector3 pos = (i + 1) * _direction * MahjongTile.Width;
				_sortPocketList [i].ClickCount = 0;
				_sortPocketList [i].Moveto (pos, true);
			}
		}
			
		Vector3 insertPosition = insertIndex * _direction * MahjongTile.Width;
		_sortPocketList.Remove (_drawTile);
		_sortPocketList.Insert (insertIndex, _drawTile);
		if (insertIndex < _sortPocketList.Count - 1) {
			_drawTile.Insert (insertPosition, true);
		} else {
			_drawTile.Moveto (insertPosition, true);
		}

		_drawTile = null;
		_playIndex = -1;
		Debug.Log (Proxy.Index + "playindex -1 @@@");
	}
	#endregion

	#region chow pong kong
	public void Combo(TileComboDef def, bool withAnimation = true)
	{
		int num = 0;
		int flipNum = 0;
		switch (def.Combo) {
		case TileDef.ComboType.CHOW:
			break;
		case TileDef.ComboType.PONG:
			num = 3;
			break;
		case TileDef.ComboType.KONG:
			num = 4;
			break;
		case TileDef.ComboType.KONG_DARK:
			num = 4;
			flipNum = 2;
			break;
		case TileDef.ComboType.KONG_TURN:
			num = 4;
			break;
		}

		Combo (def.Tile, num, flipNum, withAnimation);
	}

	// param id is a combo list
	public void Combo(TileDef def, int num, int flipNum = 0, bool withAnimation = true)
	{
		MahjongTile tile = null;
		Vector3 localPos = Vector3.zero;
		if (ComboLocator.childCount > 0) {
			localPos = Vector3.zero - MahjongTile.Width * _direction * ComboLocator.childCount - MahjongTile.Width * _direction * 0.2f;
		} else {
			localPos = Vector3.zero;
		}

		for (int i = 0; i < num; ++i) {
			tile = MahjongTile.Create (def);
			tile.Reset ();

			if (i < flipNum) {

				tile.Direction = MahjongTile.Face.PLANE_NEGTIVE;

				_angle = Vector3.Angle (Vector3.right, _direction);
				//Debug.Log (_angle + "    " + Vector3.right + "," + _direction);
				// unity3d bug from vector3.right to vector3.forward == 90 ?
				if (_direction == Vector3.forward) {
					_angle = 270f;
				}
				tile.transform.Rotate (Vector3.up, _angle, Space.Self);
			} else {
				_angle = Vector3.Angle (Vector3.right, _direction);
				//Debug.Log (_angle + "    " + Vector3.right + "," + _direction);
				// unity3d bug from vector3.right to vector3.forward == 90 ?
				if (_direction == Vector3.forward) {
					_angle = 270f;
				}
				tile.transform.Rotate (Vector3.up, _angle, Space.Self);
			}

			//往左偏移的起始位置
			tile.transform.parent = ComboLocator;
			tile.transform.localPosition = localPos - i * MahjongTile.Width * _direction;

			if (withAnimation) {
				//再往左偏移一段位置从起始位置起 然后播放动画
				Vector3 localDstPosition = tile.transform.localPosition;
				tile.transform.localPosition = tile.transform.localPosition - _direction * MahjongTile.Width * 3f;

				ActionBase act = MoveTo.Create (0.2f, localDstPosition, true);
				ActionManager.Instance.RunAction (tile.gameObject, act);
			}
		}

		//Debug.Log (_comboLocator.transform.childCount);
	}
	#endregion

	public void SetLackState()
	{
		for (int i = 0; i < _sortPocketList.Count; ++i) {
			if (_sortPocketList [i].Def.GetKind () == _proxy.LackTileKind) {
				_sortPocketList [i].SetColor (new Color (0.5f, 0.5f, 0.5f, 1f));
			}
		}
	}

	public void ResetPocketList(bool lastGap = false)
	{
		TileDef def = null;
		MahjongTile tile = null;

		destroyChildren (PocketLocator);

		if (_direction == Vector3.right) {
			//self
			_angle = 0f;
		} else if (_direction == Vector3.forward) {
			//right
			_angle = -270f;
		} else if (_direction == Vector3.left) {
			//front
			_angle = -180;
		} else if (_direction == -Vector3.forward) {
			//left
			_angle = -90f;
		}

		if (_sortPocketList == null) {
			_sortPocketList = new List<MahjongTile> ();
		} else {
			_sortPocketList.Clear ();
		}

		Proxy.PocketList.Sort (TileDef.Comparison);
		for (int i = 0; i < Proxy.PocketList.Count; ++i) {
			def = _proxy.PocketList [i];
			tile = MahjongTile.Create (def);
			tile.Direction = MahjongTile.Face.PLANE_POSITIVE;
			tile.transform.Rotate (Vector3.right, -90f, Space.Self);
			tile.transform.Rotate (-Vector3.forward, _angle, Space.Self);//local space forward
			tile.transform.parent = PocketLocator;
			tile.transform.localPosition = i * _direction * MahjongTile.Width;
			_sortPocketList.Add (tile);
		}

		if (lastGap) {
			tile.transform.localPosition = (Proxy.PocketList.Count - 0.8f) * _direction * MahjongTile.Width;
		}
	}

	public void ResetPlayList()
	{
		TileDef def = null;
		MahjongTile tile = null;

		destroyChildren (PlayLocator);

		for (int i = 0; i < Proxy.PlayList.Count; ++i) {
			def = Proxy.PlayList [i];
			if (TileDef.IsValid(def.Value)) {
				tile = MahjongTile.Create (def);
				if (tile != null) {
					placePlayed (tile, false);
				}
			}
		}
	}

	public void ResetComboList()
	{
		destroyChildren (ComboLocator);
		for (int i = 0; i < Proxy.ComboList.Count; ++i) {
			TileComboDef def = Proxy.ComboList [i];
			Combo (def, false);
		}
	}

	private void destroyChildren(Transform locator)
	{	
		MahjongTile tile = null;
		while (locator.childCount > 0) {
			tile = locator.GetChild (0).GetComponent<MahjongTile> ();
			tile.Despawn ();
		}
	}

	public void RemoveLastPlayed()
	{
		if (_playedList.Count > 0) {
			_playedList [_playedList.Count - 1].Despawn ();
		}
	}

	#region show hand tiles
	public virtual bool ShowPocketList(bool lastGap = true)
	{
		if (MyShowPocketLocator != null) {
			return showPocketList (MyShowPocketLocator, lastGap);
		} else {
			return showPocketList (PocketLocator, lastGap);
		}
		return false;
	}
		
	private bool showPocketList(Transform locator, bool lastGap = true)
	{
		TileDef def = null;
		MahjongTile tile = null;
		FiniteTimeAction delay = null;
		FiniteTimeAction rb = null;
		float angle = 0f;
		if (_direction == Vector3.right) {
			//self
			angle = 0f;
		} else if (_direction == Vector3.forward) {
			//right
			angle = -270f;
		} else if (_direction == Vector3.left) {
			//front
			angle = -180;
		} else if (_direction == -Vector3.forward) {
			//left
			angle = -90f;
		}

		for (int i = 0; i < _sortPocketList.Count; ++i) {
			tile = _sortPocketList [i];
			tile.transform.parent = null;
			tile.Direction = MahjongTile.Face.PLANE_NEGTIVE;
			tile.transform.Rotate (Vector3.up, angle, Space.Self);
			tile.transform.parent = locator;
			tile.transform.localPosition = i * _direction * MahjongTile.Width;
		}

		if (lastGap) {
			tile.transform.localPosition = tile.transform.localPosition + _direction * 0.2f * MahjongTile.Width;
		}

		for (int i = 0; i < locator.transform.childCount; ++i) {
			tile = locator.transform.GetChild (i).GetComponent<MahjongTile> ();
			List<FiniteTimeAction> list = new List<FiniteTimeAction> ();
			/*
			if (_DelayShowHand > 0f) {
				delay = DelayTime.Create (_DelayShowHand);
				list.Add (delay);
			}
			*/
			rb = RotateBy.Create (0.1f, Vector3.right, 180f, Space.Self);
			list.Add (rb);
			ActionManager.Instance.RunAction (tile.gameObject, list);
		}

		return true;
	}
	#endregion

	#region METHOD_EXTENSION
	public void PlaceWin(MahjongTile tile, bool withSound = true)
	{
		tile.Reset ();
		tile.transform.parent = WinLocator;
	}
	#endregion
}

