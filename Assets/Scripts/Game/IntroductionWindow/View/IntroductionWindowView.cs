using System;
using DG.Tweening;
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
                gameObject.transform.DOScale(1f, 0.5f).SetEase(Ease.InOutSine);
            }
            else
            {
                gameObject.transform.DOScale(0f, 0.5f).SetEase(Ease.InOutSine);  
            }
        }
    }
}
