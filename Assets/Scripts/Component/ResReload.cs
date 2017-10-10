using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Bean.Hall
{
    public class ResReload : MonoBehaviour
    {
        public Image[] images;

        void Awake()
        {
            images = transform.GetComponentsInChildren<Image>();
        }

        void OnStepImage(int index)
        {
            var image = images[index];
            var name = image.mainTexture.name;
            var path = "Assets/Art/" + gameObject.name + "/" + name + ".png";
			Debug.LogMsg (path);
            var newSprite = ResourcesManager.Load<Sprite>(path);
            image.sprite = newSprite;
        }

    }

}