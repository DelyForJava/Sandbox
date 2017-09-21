using UnityEngine;
using System.Collections;

public class TableCounter  : MonoBehaviour
{
	[SerializeField] Texture[] _numberTexture;
	MeshRenderer _meshRenderer;
	Material _material;
	int _count;
	bool _countdown = false;
	float _countdownTime;


	public void StartCountdown (int count)
	{
		// set count max number
		_count = count > _numberTexture.Length + 1 ? _numberTexture.Length + 1 : count;
		_material.mainTexture = _numberTexture [_count];
		_meshRenderer.enabled = true;

		_countdown = true;
		_countdownTime = 0f;
	}

	public void StopCountdown ()
	{
		_countdown = false;
		_meshRenderer.enabled = false;
	}

	void Start ()
	{
		_meshRenderer = GetComponent<MeshRenderer> ();
		_meshRenderer.enabled = false;
		_material = _meshRenderer.material;

		// rotate model "counter", look to player
		transform.Rotate (0f, 180f, 0f, Space.Self);
	}

	void Update ()
	{
		if (_countdown)
		{
			_countdownTime += Time.deltaTime;
			if (_countdownTime.CompareTo (1f) >= 0)
			{
				_countdownTime -= 1f;
				--_count;
				if (_count > -1)
					_material.mainTexture = _numberTexture [_count];
				else
					_countdown = false;
			}
		}
	}
}