using UnityEngine;
using UnityEngine.EventSystems;

namespace Bean.Hall
{
    public class PointerImplement : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
    {
        private const string Anywhere = "Anywhere";
        private const string OnScreenPointerDown = "OnScreenPointerDown";

        public void OnPointerDown(PointerEventData eventData)
        {
            UnityEngine.Debug.LogError("down");
            if (gameObject.name == Anywhere)
                SendMessageUpwards(OnScreenPointerDown, SendMessageOptions.DontRequireReceiver);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            UnityEngine.Debug.LogError("up");
        }

    }

}