using System;
using Game.HorizontalSlidingArea.Presenter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.HorizontalSlidingArea.View
{
    public class HorizontalSlideView: MonoBehaviour, IPointerDownHandler, IPointerUpHandler       
    {
        private HorizontalSlidePresenter _horizontalSlidePresenter;

        [SerializeField] private Scrollbar _scrollbar;

        private void Update()
        {
            _scrollbar.value = _horizontalSlidePresenter.UpdateScrollValue();
        }

        public void Init(HorizontalSlidePresenter horizontalSlidePresenter)
        {
            _horizontalSlidePresenter = horizontalSlidePresenter;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _horizontalSlidePresenter.SetPlayerInteraction(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _horizontalSlidePresenter.SetPlayerInteraction(false);
        }

        public float CurrentScrollbarValue()
        {
            return _scrollbar.value;
        }
    }
}