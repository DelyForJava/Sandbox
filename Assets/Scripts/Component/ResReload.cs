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

        public void OnStepImage(string index)
        {
            //return;
            var image = images[int.Parse(index)];
            var name = image.mainTexture.name;
            
            //Debug.LogMsg(System.IO.Path.GetFullPath(name));
            //Debug.LogMsg(Directory.GetDirectoryRoot(name));
            
            var path = "Assets/Art/" + gameObject.name + "/" + name;
            //Debug.LogMsg(path);
            var newSprite = ResourcesManager.Load<Sprite>(path + ".png");
            if(newSprite==null)
               newSprite = ResourcesManager.Load<Sprite>(path + ".jpg");

            image.sprite = newSprite;
        }

    }

}