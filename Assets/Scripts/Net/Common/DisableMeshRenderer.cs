using System.Collections;
using UnityEngine;

public class DisableMeshRenderer : MonoBehaviour {

	MeshRenderer[] renderers;
	//Collider[] colliders;
	void Awake() {
		renderers = GetComponentsInChildren<MeshRenderer> ();
		//colliders = GetComponentsInChildren<MeshCollider> ();
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < renderers.Length; ++i) {
			renderers [i].enabled = false;
		}
	}
}