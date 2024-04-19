using Game.HeaderPanel.Presenter;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HeaderPanel.View
{
    public class HeaderView : MonoBehaviour
    {
        private HeaderPresenter _headerPresenter;

        [SerializeField] private Button _exitGameButton;

        [SerializeField] private Button _openSettingsButton;
        
        public void Init(HeaderPresenter headerPresenter)
        {
            _headerPresenter = headerPresenter;
        }

        private void OnEnable()
        {
            _exitGameButton.onClick.AddListener(() =>
                _headerPresenter.OnExitGameButtonClick());
            
            _openSettingsButton.onClick.AddListener(() =>
                _headerPresenter.OnOpenSettingsButtonClick());
        }
    }
}
