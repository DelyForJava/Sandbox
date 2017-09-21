
namespace odao.scmahjong
{
    using System.Collections;
    using System.Collections.Generic;

    public class Player
    {
        // 0:down;1:up
        private List<TileDef> _stackList;
        private List<TileDef> _pocketList;
        public List<TileDef> PocketList {
            get { return _pocketList; }
            set {
                _pocketList = value;
            }
        }
        private List<TileDef> _playList;
		public List<TileDef> PlayList { get { return _playList; } set { _playList = value; } }
        private List<TileDef> _discardList;
		public List<TileDef> DiscardList { get { return _discardList; } set { _discardList = value; } }
        private List<TileComboDef> _comboList;
		public List<TileComboDef> ComboList { get { return _comboList; } set { _comboList = value; } }

        private TileDef.Kind _lackTileKind;
        public TileDef.Kind LackTileKind
        {
			get { return _lackTileKind; }
            set
            {
				_lackTileKind = value;
            }
        }

		public bool WinState { get; set; }
        public bool YourTurn { get; set; }
		public int TrusteeShip { get; set;}
		public int Index { get; set; }

		protected Player()
        {
			_stackList = new List<TileDef> ();
			_pocketList = new List<TileDef> ();
            _playList = new List<TileDef> ();
            _discardList = new List<TileDef> ();
			_comboList = new List<TileComboDef> ();

			_lackTileKind = TileDef.Kind.NONE;

			WinState = false;
            YourTurn = false;
            TrusteeShip = 0;
        }

		protected virtual void init()
		{
			_pocketList.Clear ();
		}

		public static Player Create()
		{
			Player inst = new Player();
			inst.init ();
			return inst;
		}

        public virtual void Draw(TileDef tile)
        {
			_pocketList.Add(tile);
        }

		public bool Play(TileDef tile)
		{
			for (int i = _pocketList.Count-1; i >= 0; --i) {
				TileDef thisTile = _pocketList [i];
				if (thisTile.Value == tile.Value) {
					_pocketList.RemoveAt (i);
					_playList.Add (tile);
					return true;
				}
			}
			return false;
		}

		public bool RemovePocketList(int num)
		{
			if (remove (num, ref _pocketList)) {
				return true;
			}
			return false;
		}

		public bool RemovePocketList(TileDef tile, int num)
		{
			if (removeSameTile (tile, num, ref _pocketList) != 0)
				return false;
			return true;
		}

		private bool remove(int num, ref List<TileDef> list)
		{
			if (list.Count >= num) {
				for (int i = 0; i < num; ++i) {
					list.RemoveAt (0);
				}
				return true;
			}
			return false;
		}

		public bool Add(int num, ref List<TileDef> list)
		{
			return true;
		}

		public bool FindPocketTile(TileDef tile, out int index)
		{
			index = 0;
			for (int i = _pocketList.Count-1; i >= 0; --i) {
				TileDef thisTile = _pocketList [i];
				if (thisTile.Value == tile.Value) {
					index = i;
					return true;
				}
			}
			return false;
		}

		private bool CanChow(TileDef tile, int from = -1)
        {
			throw new System.NotImplementedException("not implement chow");
            return false;
        }

		public virtual bool Chow(TileDef tile, int from = -1)
		{
			throw new System.NotImplementedException("not implement chow");
			if (CanChow (tile)) {
				return true;
			}
			return false;
        }

		private bool CanPong(TileDef tile, int from = -1)
        {
            return numTile(tile, ref _pocketList) >= 2;
        }

		public virtual TileComboDef Pong(TileDef tile, int from = -1)
        {
			if (CanPong (tile)) {

				removeSameTile (tile, 2, ref _pocketList);

				var comboDef = new TileComboDef ();
				comboDef.Index = from;
				comboDef.Combo = TileDef.ComboType.PONG;
				comboDef.Tile = tile;
				_comboList.Add (comboDef);

				return comboDef;
			}
			return null;
        }

		private bool CanKong(TileDef tile, out TileComboDef comboDef, int from = -1)
        {
			comboDef = new TileComboDef ();

			comboDef.Index = from;
			comboDef.Tile = tile;
			comboDef.Combo = TileDef.ComboType.NONE;

            if(numTile(tile, ref _pocketList) >= 4)
            {
                //dark
				comboDef.Combo = TileDef.ComboType.KONG_DARK;
                return true;
            }
            else if(numTile(tile, ref _pocketList) >=3)
            {
				//normal
				comboDef.Combo = TileDef.ComboType.KONG;
                return true;
            }
			else if(numTile(tile, ref _comboList) > 0)
            {
                //turn
				comboDef.Combo = TileDef.ComboType.KONG_TURN;
                return true;
            }
            return false;
        }

		public virtual TileComboDef Kong(TileDef tile, int from = -1)
        {
			TileComboDef comboDef = null;
			if (CanKong (tile, out comboDef, from)) {
				List<int> list = new List<int> ();
				int index = 0;
				if (comboDef.Combo == TileDef.ComboType.KONG) {
					removeSameTile (tile, 3, ref _pocketList);
				}
				else if (comboDef.Combo == TileDef.ComboType.KONG_DARK) {
					removeSameTile (tile, 4, ref _pocketList);
				}
				else if (comboDef.Combo == TileDef.ComboType.KONG_TURN) {
					removeSameTile (tile, 1, ref _pocketList);
				}
				_comboList.Add (comboDef);
				return comboDef;
			}
			return null;
        }

		public virtual void Win(byte card)
		{
		}

		public virtual void Pass()
		{
		}

        private int numTile(TileDef tile, ref List<TileDef> list)
        {
            int count = 0;
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].Value == tile.Value)
                {
                    ++count;
                }
            }
            return count;
        }

		private int numTile(TileDef tile, ref List<TileComboDef> list)
		{
			int count = 0;
			for (int i = 0; i < list.Count; ++i)
			{
				if (list[i].Tile.Value == tile.Value)
				{
					++count;
				}
			}
			return count;
		}

		private int removeSameTile(TileDef tile, int num, ref List<TileDef> list)
		{
			int count = 0;
			TileDef def = null;
			for (int i = list.Count - 1; i >= 0; --i) {
				if (list [i].Value == tile.Value) {
					++count;
					list.RemoveAt (i);
					if (num == count)
						break;
				}
			}
			if (count != num) {
				throw new System.Exception("removeSameTile");
				return -1;
			}

			return 0;
		}

        public void SortPocketList(System.Comparison<TileDef> comp)
        {
            _pocketList.Sort(comp);
        }
			
		public virtual void SetLackTileKind(TileDef.Kind kind)
		{
			_lackTileKind = kind;
		}

        public string ToString()
        {
            string str = "";

            for(int i=0; i<_pocketList.Count; ++i)
            {
                str += _pocketList[i].ToString();
                str += ',';
            }
            str += '\n';

			return str;
        }

		#region qmy add funs
		public virtual void LeaveTable()
		{
			
		}

		public virtual void ChangeTable()
		{

		}

		public virtual void ContinueGame()
		{

		}

		public virtual void Huanpai(List<byte> cards)
		{

		}
		#endregion
    }
}



