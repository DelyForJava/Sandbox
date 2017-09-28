using System.Collections;
using System.Collections.Generic;
using Bean.Hall;
using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;

namespace Bean.Hall
{
    public class ResReload : MonoBehaviour
    {
        private Image[] images_;

        void Awake()
        {
            images_ = transform.GetComponentsInChildren<Image>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnReload(string sceneName)
        {
            //Debug.LogMsg("Where am I 222");
            StopCoroutine("Reload");
            StartCoroutine( Reload(sceneName) );
        }

        IEnumerator Reload(string sceneName)
        {
            foreach (Image image in images_)
            {
                var name = image.mainTexture.name;

                //if (name!="logoo" || name!="fishh")
                //    continue;
                var name2 = image.sprite.texture.name;
                var path = "Assets/Art/" + sceneName + "/" + name + ".png";
                var newSprite = ResourcesManager.Load<Sprite>(path);
                image.sprite = newSprite;
                yield return null;
            }

        }

    }

}