using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour {

    public Image image_card;
    public Image image_fish;
    public Image image_excite;
    public Image image_fun;

    public float threshold = 0.5f;

	// Use this for initialization
	void Start () {
        image_card.alphaHitTestMinimumThreshold = threshold;
        image_fish.alphaHitTestMinimumThreshold = threshold;
        image_excite.alphaHitTestMinimumThreshold = threshold;
        image_fun.alphaHitTestMinimumThreshold = threshold;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
