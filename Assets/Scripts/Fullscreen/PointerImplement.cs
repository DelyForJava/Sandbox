using UnityEngine;
using UnityEngine.EventSystems;

namespace Bean.Hall
{
    public class PointerImplement : MonoBehaviour, IPointerDownHandler
    {
        private const string Anywhere = "Anywhere";
        private const string OnScreenPointerDown = "OnScreenPointerDown";

        public void OnPointerDown(PointerEventData eventData)
        {
            if (gameObject.name == Anywhere)
                SendMessageUpwards(OnScreenPointerDown, SendMessageOptions.DontRequireReceiver);
        }

    }

}