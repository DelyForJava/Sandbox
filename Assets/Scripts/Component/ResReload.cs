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


        void OnReload(string sceneName)
        {
            //Debug.LogMsg("Where am I 222");
            StopCoroutine("Reload");
            StartCoroutine( Reload(sceneName) );
        }

		IEnumerator ReloadImage(string sceneName)
		{
			foreach (Image image in images)
			{
				var name = image.mainTexture.name;
				var name2 = image.sprite.texture.name;
				var path = "Assets/Art/" + sceneName + "/" + name + ".png";
				var newSprite = ResourcesManager.Load<Sprite>(path);
				image.sprite = newSprite;
				yield return null;
			}

		}

        IEnumerator Reload(string sceneName)
        {
			yield return ReloadImage (sceneName);

        }

    }

}