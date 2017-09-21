
	
using System;
using System.Collections;
using System.Collections.Generic;
using odao.scmahjong;

public class MahjongPileDef
{
	private List<TileDef> _wall;
	int[] tiles = { 19,9,7,8,27,1,17,12,8,9,24,2,25,21,28,17,16,26,3,6,22,22,18,5,21,17,19,28,18,29,16,24,6,13,9,25,5,16,14,8,27,7,28,16,22,11,2,15,23,15,25,15,1,11,27,24,21,4,28,3,11,27,5,4,29,23,12,21,12,22,12,3,24,6,13,26,15,7,14,1,6,2,8,14,25,4,13,29,13,14,18,9,19,17,2,23,7,26,18,5,4,23,3,19,29,11,26,1 };

	public MahjongPileDef ()
	{
		_wall = new List<TileDef> ();
	}

	//dealer and opposite dealer are 14 tons, others are 13tons
	public List<TileDef> RebuildStack (int a, int b, int count, int drawFront, int drawBehind)
	{
		int stackIndex = 0;
		int pointMin = Math.Min(a,b);
		int pointSum = a + b;
		int skipCount = pointMin * 2; // pointMin tons to keep

		// 4, 8, 12, banker's left
		if (pointSum % 4 == 0)
			stackIndex = 3;
		// 2, 6, 10, banker's right
		else if (pointSum % 2 == 0)
			stackIndex = 1;
		// 1, 3, 5, 7, 9, 11, banker's front
		else
			stackIndex = 2;

		_wall.Clear ();
		for (int i = 0; i < count; ++i) {
			_wall.Add (TileDef.Create ((byte)0x11));
		}

		/*
		for (int i = 0; i < _playPlayers.Length; ++i) {
			_players [i] = _playPlayers [i];
		}

		//hard code
		if (_playPlayers.Length == 3) {
			if (_players [0] == _self) {
				_players [3] = _players [2];
				_players [2] = _front;
			} else if (_players [1] == _self) {
				_players [3] = _front;
			} else if (_players [2] == _self) {
				_players [0] = _right;
				_players [1] = _front;
				_players [2] = _left;
				_players [3] = _self;
			}
		}

		int stackCount = _players [stackIndex].GetStack ().Count;
		_wall.Clear ();
		_wall.AddRange (_players [stackIndex].GetStack (skipCount, stackCount - skipCount));
		_wall.AddRange (_players [(stackIndex + 3) % 4].GetStack ());
		_wall.AddRange (_players [(stackIndex + 2) % 4].GetStack ());
		_wall.AddRange (_players [(stackIndex + 1) % 4].GetStack ());
		_wall.AddRange (_players [stackIndex].GetStack (0, skipCount));

		UIControllerGame.Instance.SetPaiRestInfo (_wall.Count);
		UIControllerGame.Instance.RefreshPaiRestInfo ();
		*/
		return _wall;
	}
	/*
	public void RestoreBuildStack(int pointMin, int pointSum, int leftTitleCount, int drawFront, int drawBehind)
	{
		for (int i = 0; i < _playPlayers.Length; ++i) {
			_players [i] = _playPlayers [i];
		}

		//hard code
		if (_playPlayers.Length == 3) {
			if (_players [0] == _self) {
				_players [3] = _players [2];
				_players [2] = _front;
			} else if (_players [1] == _self) {
				_players [3] = _front;
			} else if (_players [2] == _self) {
				_players [0] = _right;
				_players [1] = _front;
				_players [2] = _left;
				_players [3] = _self;
			}
		}

		for (int i = 0; i < 4; ++i) {
			if (_players [i] == null) {
				DebugInfo.Message ("!!!!ERROR-> " + i + " null");
			} 
			else {
				_players [i].ClearStack ();
			}
		}

		//庄家和庄家的对面都是14墩
		//其他的都是13墩
		//14墩
		_players[0].InitStack (_wallid.GetRange(0,28));
		//13墩
		_players[1].InitStack (_wallid.GetRange(28,26));
		//14墩
		_players[2].InitStack (_wallid.GetRange(54,28));
		//13墩
		_players[3].InitStack (_wallid.GetRange(82,26));


		List<Pai> list = RebuildStack (pointMin, pointSum);
		//		int end = _wall.Count - leftTitleCount;
		//		for (int i = 0; i < end; ++i) {
		//			Pai tile = _self.PopOneTile (ref _wall);
		//			tile.Despawn();
		//		}
		for (int i = 0; i < drawFront; ++i) {
			Pai tile = _self.PopOneTile (ref _wall);
			tile.Despawn();
		}
		for (int i = 0; i < drawBehind; ++i) {
			Pai tile = _self.PopOneTile (ref _wall, true);
			tile.Despawn();
		}
	}
	*/
	#region mahjong pile operation
	#endregion
}



