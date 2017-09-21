using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
	static public System.Action EventThrowCompleted;

	[SerializeField] float _timeRoll = 1f;				// dice rotate time before slowdown
	[SerializeField] float _speedSlowdownHigh = 10f;	// rotate speed when slowdown start
	[SerializeField] float _speedSlowdownLow = 9f;		// rotate low speed when slowdown 
	[SerializeField] float _accelerationSlowdown = 10f;	// acceleration when slowdown 
	[SerializeField] float _factorSlowdownRotateAround = 0.1f;
	[SerializeField] float _closeStep = 0.2f;
	float _speedRotateRandom = 5000f;
	float _speedRotateHorizontal = 1000f;
	//float _closeDistance = 1.7f;
	float _closeDistance = 0.3f;
	int _targetPoint;
	Vector3 _closePosition;								// close to closePosition when dice slow down
	Vector3 _originPosition;
	Vector3[] _axis;


	#region method

	public void ThrowPoint (int point, Vector3 closePosition)
	{
		_targetPoint = point;
		_closePosition = closePosition;
		transform.position = _originPosition;
		StopCoroutine ("Throw");
		StartCoroutine ("Throw");
	}

	public void StartRoll ()
	{
		StartCoroutine ("OnlyRoll");
	}

	public void StopRool ()
	{
		StopCoroutine ("OnlyRoll");
	}

	Vector3 GetPointAxis (int n)
	{
		switch (n)
		{
		case 1:		return transform.up * -1;
		case 2:		return transform.right;
		case 3:		return transform.forward;
		case 4:		return transform.right * -1;
		case 5:		return transform.forward * -1;
		case 6:		return transform.up;
		default:	return transform.forward;
		}
	}

	void Rotate ()
	{
		transform.Rotate (_axis [Random.Range (0,10)], Time.deltaTime * _speedRotateRandom, Space.Self);
		transform.RotateAround (Vector3.zero, Vector3.up, Time.deltaTime * _speedRotateHorizontal);	
	}

	#endregion

	#region coroutine

	IEnumerator OnlyRoll ()
	{
		yield return null;

		while (true)
		{
			Rotate ();
			yield return null;
		}
	}

	IEnumerator Throw ()
	{
		yield return null;

		// random rotate
		float time = 0;
		while (time < _timeRoll)
		{
			Rotate ();
			yield return null;
			time += Time.deltaTime;
		}
			
		// slow down and make target point upward 
		float speed = _speedSlowdownHigh;
		Quaternion targetRot = Quaternion.FromToRotation (GetPointAxis (_targetPoint), Vector3.up) * transform.rotation;
		while (true)
		{
			// rotate dice to target direction
			Quaternion preRot = transform.rotation;
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRot, Time.deltaTime * speed);

			// make dice rotate around center
			float angle = Quaternion.Angle (preRot, transform.rotation);
			transform.RotateAround (Vector3.zero, Vector3.up, angle * _factorSlowdownRotateAround);

			// dice close to center 
			Vector3 closeVec = (transform.position - _closePosition).normalized * _closeDistance + _closePosition;
			transform.position = Vector3.MoveTowards (transform.position, closeVec, _closeStep);

			// slowdown spped reduce to low when time change 
			speed = Mathf.Lerp (speed, _speedSlowdownLow, Time.deltaTime * _accelerationSlowdown);

			// check dice's target point is upward
			float curAngle = Vector3.Angle (GetPointAxis (_targetPoint), Vector3.up);
			if (curAngle < 1f)
				break;

			yield return null;	
		}

		if (EventThrowCompleted != null)
			EventThrowCompleted ();
	}

	#endregion

	#region mono behaviour

	void Start ()
	{
		_originPosition = transform.position;

		_axis = new Vector3 [10];
		for (int i = 0; i < 10; ++i)
			_axis [i] = new Vector3 (Random.Range (0, 1f), Random.Range (0, 1f), Random.Range (0, 1f));
	}

	#endregion
}