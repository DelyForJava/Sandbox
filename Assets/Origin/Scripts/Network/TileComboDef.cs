using System.Collections.Generic;
using odao.scmahjong;

public class TileComboDef
{
	//from whom
	public int Index { get; set; }
	public TileDef.ComboType Combo{ get; set; }
	public TileDef Tile { get; set; }

	public static TileComboDef Create(int index, TileDef.ComboType combo, TileDef tile)
	{
		var def = new TileComboDef ();
		def.Index = index;
		def.Combo = combo;
		def.Tile = tile;
		return def;
	}

	public string ToString()
	{
		return string.Format ("{0} {1} from {2}", Combo, Tile.ToString(), Index);
	}
}

