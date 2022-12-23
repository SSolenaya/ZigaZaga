using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class ClickObserver : MonoBehaviour, IPointerClickHandler
    {
        private Action _onClick;

        public void SubscribeForClick(Action act)
        {
            _onClick += act;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            _onClick?.Invoke();
        }

        public void UnsubscribeAll()
        {
            _onClick = null;
        }
    }
}
