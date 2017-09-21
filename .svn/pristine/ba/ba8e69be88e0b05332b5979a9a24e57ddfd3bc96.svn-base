using System.Collections;
using UnityEngine;
using PathologicalGames;

public class DiceController : Singleton<DiceController>
{
	public bool DiceDone { get { return _diceDone; } }

	bool _diceDone = false;
	int _diceCount;
	Vector3 _centerDiceGroup;
	Transform _diceGroup;
	int[] _point;


	public DiceController ()
	{
		Dice.EventThrowCompleted += ThrowDiceCompleted;
	}

	public Transform GetDiceGroup ()
	{
		return _diceGroup;
	}

    public void ThrowDice(int point1, int point2)
    {
        // get all dice
        Dice[] dice = GameObject.FindObjectsOfType<Dice>();
        _diceCount = dice.Length;
        _diceDone = false;

        // set dice group
        _diceGroup = dice[0].transform.parent;

        // calculate dice group center
        Vector3 center = Vector3.zero;
		for (int i = 0; i < dice.Length; ++i) {
			center += dice [i].transform.position;
		}
        center /= (float)_diceCount;

        // throw dice
        dice [0].ThrowPoint (point1, center);
        dice [1].ThrowPoint (point2, center);
    }

	public int[] ThrowDice (int[] point = null)
	{
		// get all dice
		Dice[] dice = GameObject.FindObjectsOfType<Dice> ();
		_diceCount = dice.Length;
		_diceDone = false;

		// set dice group
		_diceGroup = dice [0].transform.parent;

		// calculate dice group center
		Vector3 center = Vector3.zero;
		foreach (Dice d in dice)
			center += d.transform.position;
		center /= (float)_diceCount;

		// random throw dice point or use get point
		_point = new int[2];
		_point [0] = (point == null || point.Length != dice.Length) ? Random.Range (1, 7) : point [0];
		_point [1] = (point == null || point.Length != dice.Length) ? Random.Range (1, 7) : point [1];

		for (int i = 0, imax = dice.Length; i < imax; ++i)
			dice [i].ThrowPoint (_point [i], center);

		return _point;
	}

	void ThrowDiceCompleted ()
	{
		--_diceCount;

		// if all dice throw completed, do something
		if (_diceCount == 0)
			_diceDone = true;
	}
}