using System;
using DG.Tweening;
using Extensions;
using Game.IntroductionWindow.Presenter;
using UnityEngine;
using UnityEngine.UI;

namespace Game.IntroductionWindow.View
{
    public class IntroductionWindowView : MonoBehaviour
    {
        private bool _isOpen;
        
        [SerializeField] private Button _linkButton;
        
        [SerializeField] private Button _exitButton;

        private IntroductionWindowPresenter _introductionWindowPresenter;

        private const float ANIMATION_DURATION = 0.4f;
        
        private void OnEnable()
        {
            _linkButton.onClick.AddListener(() =>
                _introductionWindowPresenter.OnLinkButtonClick()
                );
            
            _exitButton.onClick.AddListener(() =>
                _introductionWindowPresenter.OnExitButtonClick());
        }

        public void Init(IntroductionWindowPresenter introductionWindowPresenter)
        {
            _introductionWindowPresenter = introductionWindowPresenter;
        }

        public void DisplayOrCloseWindow(bool state)
        {
            _isOpen = state;
                        
            if (_isOpen)
            {
                gameObject.ScaleTo(1f, ANIMATION_DURATION);
            }
            else
            {
                gameObject.ScaleTo(0f, ANIMATION_DURATION);
            }
        }
    }
}
