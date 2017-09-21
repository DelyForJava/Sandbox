using UnityEngine;
using System.Collections;

public class TableDirection : MonoBehaviour
{
	Texture _grayTexture;
	Texture _lightTexture;
	Material _material;
    [SerializeField] float _blinkTime = 0.5f;
    

	public Texture GrayTexture { set { _grayTexture = value; } }
	public Texture LightTexture { set { _lightTexture = value; } }


	public void ToBright ()
	{
        _material.mainTexture = _lightTexture;
		StopCoroutine ("Blink");
	}

	public void ToDark ()
	{
        _material.mainTexture = _grayTexture;
		StopCoroutine ("Blink");
	}

	public void ToBlink ()
	{
        ToBright ();
        StartCoroutine ("Blink");	
	}

	IEnumerator Blink ()
	{
        while (true)
		{
			yield return new WaitForSeconds (_blinkTime);
			_material.mainTexture = _grayTexture;
			yield return new WaitForSeconds (_blinkTime);
            _material.mainTexture = _lightTexture;
		}
	}

	void Awake ()
	{
		_material = GetComponent<Renderer> ().material;
	}
}