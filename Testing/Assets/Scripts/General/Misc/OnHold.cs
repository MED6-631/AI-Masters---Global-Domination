namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using System;

    public class OnHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool buttonHeld;

        public void OnPointerDown(PointerEventData eventData)
        {
            buttonHeld = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            buttonHeld = false;
        }
    }

}
