using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using odao.scmahjong;

public class MahjongPile : MonoBehaviour {

	MahjongPileDef _proxy;
	List<MahjongTile> _wall;

	void Awake()
	{
		_proxy = new MahjongPileDef ();
		_wall = new List<MahjongTile> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RebuildStack(int a, int b, int count, int drawFront, int drawBehind)
	{		
		_wall.Clear ();
		MahjongTile tile = null;
		List<TileDef> list = _proxy.RebuildStack (a, b, count, drawFront, drawBehind);
		for (int i = 0; i < list.Count; ++i) {
			tile = MahjongTile.Create (list [i]);
			_wall.Add (tile);
		}
	}

	public List<MahjongTile> GetRange(int offset, int num)
	{
		return _wall.GetRange (offset, num);
	}

	public void PopOneTile(bool front = true)
	{
		MahjongTile tile = _wall [0];
		_wall.Remove (tile);
		MahjongTile.Destroy (tile);
	}
}
