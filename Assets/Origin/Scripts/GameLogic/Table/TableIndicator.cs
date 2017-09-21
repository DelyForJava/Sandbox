using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TableIndicator : MonoBehaviour
{
	float _loopTime = 0.3f;
	float _jumpHeight = 2f;
    Vector3 _resetPosition;


	public void SetIndicatorPos (Vector3 target)
	{
		target.y += 10f;
		transform.position = target;

		transform.DOKill ();
		Tweener tw = transform.DOMoveY (transform.position.y + _jumpHeight, _loopTime, false);
		tw.SetLoops (-1, LoopType.Yoyo);
	}

    public void ResetIndicator()
    {
        transform.DOKill ();
        transform.position = _resetPosition;
    }

    void Awake()
    {
        _resetPosition = transform.position;
    }
}